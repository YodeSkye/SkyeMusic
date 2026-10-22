Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports NAudioStreamEngine

Friend Class LiveAudioServer

    ' Declarations
    Friend Enum StreamingState
        Stopped
        Listening
        [Error]
    End Enum
    Private Shared _tcpServer As TcpListener
    Private Shared _listenerThread As Thread
    Private Shared _isListening As Boolean = False
    Private Shared ReadOnly _clientLock As New Object()
    Private Shared ReadOnly _connectedStreams As New List(Of NetworkStream)()
    Friend Shared ReadOnly Property IsListening As Boolean
        Get
            Return _isListening
        End Get
    End Property
    Friend Shared ReadOnly Property ConnectedClientCount As Integer
        Get
            SyncLock _clientLock
                Return _connectedStreams.Count
            End SyncLock
        End Get
    End Property
    Friend Shared Event StreamingStatusChanged As Action(Of StreamingState, String)

    ' Base Methods
    ''' <summary>
    ''' Starts the raw TCP audio streaming server on any user-configured port for the LAN without admin rights.
    ''' </summary>
    Friend Shared Sub Start(port As Integer)
        If _isListening Then Return

        Try
            ' Bind to IPAddress.Any to accept LAN connections on a custom port without admin rights or HTTP.SYS restrictions
            _tcpServer = New TcpListener(IPAddress.Any, port)
            _tcpServer.Start()
            _isListening = True

            ' 1. Wire up the C# audio capture event to feed this LiveAudioServer broadcaster
            AddHandler AudioStreamCapturer.AudioDataCaptured, AddressOf HandleAudioCaptured

            ' 2. Start the modern C# MP3 audio recorder using your settings enum cast to Integer
            AudioStreamCapturer.StartCapture(CInt(App.Settings.AudioStreamingBitrate))

            ' 3. Start background thread to accept incoming client connections
            _listenerThread = New Thread(AddressOf AcceptClientsLoop) With {
                .IsBackground = True,
                .Name = "LiveAudioTcp_Listener"
            }
            _listenerThread.Start()

            NotifyStatusChanged(StreamingState.Listening, "Server Active")
            Debug.WriteLine($"[STREAMING SERVER] LAN TCP streaming listener started on port {port}.")
            Skye.Common.Log.Write($"Streaming Server Started On Port {port}")
        Catch ex As Exception
            Debug.WriteLine($"[STREAMING SERVER] Failed to start TCP listener: {ex.Message}")
            Skye.Common.Log.Write($"Streaming Server Failed To Start: {ex.Message}")
            NotifyStatusChanged(StreamingState.Error, ex.Message)
            [Stop](True)
        End Try
    End Sub
    ''' <summary>
    ''' Stops the server and closes all client connections.
    ''' </summary>
    Friend Shared Sub [Stop](Optional isDueToError As Boolean = False)
        ' 1. Guard clause: If we aren't listening and the server is null, bail out immediately!
        If Not _isListening AndAlso _tcpServer Is Nothing Then Return

        ' 2. Stop the modern C# audio recorder first
        AudioStreamCapturer.StopCapture()
        ' 3. Safely remove the event handler so it doesn't duplicate on restart
        RemoveHandler AudioStreamCapturer.AudioDataCaptured, AddressOf HandleAudioCaptured

        _isListening = False

        Try
            If _tcpServer IsNot Nothing Then
                _tcpServer.Stop()
                _tcpServer = Nothing
            End If
        Catch
        End Try

        SyncLock _clientLock
            For Each stream In _connectedStreams
                Try
                    stream.Close()
                Catch
                End Try
            Next
            _connectedStreams.Clear()
        End SyncLock

        If Not isDueToError Then NotifyStatusChanged(StreamingState.Stopped, "Server Idle")
        Debug.WriteLine("[STREAMING SERVER] Streaming server and capture stopped.")
        Skye.Common.Log.Write($"Streaming Server Stopped")
    End Sub

    ' Handlers
    Private Shared Sub HandleAudioCaptured(buffer() As Byte, count As Integer)
        BroadcastAudioBuffer(buffer, count)
    End Sub

    ' Methods
    Private Shared Sub NotifyStatusChanged(state As StreamingState, message As String)
        RaiseEvent StreamingStatusChanged(state, message)
    End Sub
    Private Shared Sub AcceptClientsLoop()
        While _isListening
            Try
                If _tcpServer IsNot Nothing AndAlso _tcpServer.Pending() Then
                    Dim client As TcpClient = _tcpServer.AcceptTcpClient()

                    ' Handle client handshake and streaming on a background thread
                    Dim clientThread As New Thread(Sub() HandleClientConnection(client)) With {
                        .IsBackground = True
                    }
                    clientThread.Start()
                Else
                    Thread.Sleep(50)
                End If
            Catch ex As Exception
                If Not _isListening Then Exit While
            End Try
        End While
    End Sub
    Private Shared Sub HandleClientConnection(client As TcpClient)
        Dim netStream As NetworkStream = Nothing
        Try
            client.NoDelay = True ' Disable Nagle's algorithm for real-time audio latency
            netStream = client.GetStream()

            ' Read incoming HTTP request headers from the client (VLC / Browser) so it doesn't block or stall
            Dim reader As New StreamReader(netStream, Encoding.ASCII, False, 1024, leaveOpen:=True)
            While reader.Peek() >= 0
                Dim line As String = reader.ReadLine()
                If String.IsNullOrEmpty(line) Then Exit While ' End of HTTP headers
            End While

            ' Send standard HTTP response headers for MP3 streaming
            Dim headerBytes = Encoding.ASCII.GetBytes(
                "HTTP/1.1 200 OK" & vbCrLf &
                "Content-Type: audio/mpeg" & vbCrLf &
                "Cache-Control: no-cache, no-store, must-revalidate" & vbCrLf &
                "Connection: keep-alive" & vbCrLf & vbCrLf
            )
            netStream.Write(headerBytes, 0, headerBytes.Length)
            netStream.Flush()

            ' Register the stream for broadcasting
            SyncLock _clientLock
                _connectedStreams.Add(netStream)
            End SyncLock

            NotifyStatusChanged(StreamingState.Listening, "Client Connected")
            Debug.WriteLine($"[STREAMING SERVER] Client connected from {client.Client.RemoteEndPoint}")

            ' Keep thread alive while client is connected and server is running
            While _isListening AndAlso client.Connected
                Thread.Sleep(500)
            End While

        Catch ex As Exception
            Debug.WriteLine($"[STREAMING SERVER] Client connection error: {ex.Message}")
            Skye.Common.Log.Write($"Streaming Server Client Connection Error: {ex.Message}")
        Finally
            If netStream IsNot Nothing Then
                SyncLock _clientLock
                    _connectedStreams.Remove(netStream)
                End SyncLock
                Try
                    netStream.Close()
                Catch
                End Try
            End If
            Try
                client.Close()
            Catch
            End Try
            NotifyStatusChanged(StreamingState.Listening, "Client Disconnected")
            Debug.WriteLine("[STREAMING SERVER] Client disconnected.")
        End Try
    End Sub
    Private Shared Sub BroadcastAudioBuffer(buffer As Byte(), count As Integer) ' Broadcasts encoded MP3 audio buffer bytes to all active connected clients.
        ' Track whether we actually removed anyone so we only notify when needed
        Dim streamsChanged As Integer = 0

        SyncLock _clientLock
            If _connectedStreams.Count = 0 Then Return

            Dim deadStreams As New List(Of NetworkStream)()

            For Each netStream In _connectedStreams
                Try
                    netStream.Write(buffer, 0, count)
                    netStream.Flush()
                Catch ex As Exception
                    deadStreams.Add(netStream)
                End Try
            Next

            ' If we found any dead streams, clean them up!
            If deadStreams.Count > 0 Then
                For Each deadStream In deadStreams
                    _connectedStreams.Remove(deadStream)
                    Try
                        deadStream.Close()
                    Catch
                    End Try
                    streamsChanged += 1
                Next
            End If
        End SyncLock

        If streamsChanged > 0 Then NotifyStatusChanged(StreamingState.Listening, $"Dead Streams Removed: {streamsChanged}")
    End Sub

End Class