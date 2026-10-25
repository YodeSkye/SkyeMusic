Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports NAudioStreamEngine

Friend Class LiveAudioServer

    ' Declarations
    Friend Class AudioClientInfo

        Public Property Client As TcpClient
        Public Property NetStream As NetworkStream
        Public Property ConnectedAt As DateTime
        Public Property EndPoint As String
        Public Property BytesStreamed As Long = 0
        Public Property UserAgent As String = "Unknown / Custom Player"

        Public Sub New(client As TcpClient, netStream As NetworkStream)
            Me.Client = client
            Me.NetStream = netStream
            Me.ConnectedAt = DateTime.Now
            Me.EndPoint = If(client.Client?.RemoteEndPoint IsNot Nothing, client.Client.RemoteEndPoint.ToString(), "Unknown")
        End Sub

    End Class
    Friend Enum StreamingState
        Stopped
        Listening
        [Error]
    End Enum
    Private Shared _tcpServer As TcpListener
    Private Shared _listenerThread As Thread
    Private Shared _isListening As Boolean = False
    Private Shared ReadOnly _clientLock As New Object()
    Private Shared ReadOnly _connectedClients As New List(Of AudioClientInfo)()

    Friend Shared ReadOnly Property IsListening As Boolean
        Get
            Return _isListening
        End Get
    End Property
    Friend Shared ReadOnly Property ConnectedClientCount As Integer
        Get
            SyncLock _clientLock
                Return _connectedClients.Count
            End SyncLock
        End Get
    End Property
    Friend Shared ReadOnly Property ActiveClients As List(Of AudioClientInfo)
        Get
            SyncLock _clientLock
                Return New List(Of AudioClientInfo)(_connectedClients)
            End SyncLock
        End Get
    End Property

    Friend Shared Event StreamingStatusChanged As Action(Of StreamingState, String)

    ' Base Methods
    Friend Shared Sub Start(port As Integer)
        If _isListening Then Return

        Dim maxRetries As Integer = 3
        Dim retryDelayMs As Integer = 150

        For attempt As Integer = 1 To maxRetries
            Try
                _tcpServer = New TcpListener(IPAddress.Any, port)
                _tcpServer.Start()
                _isListening = True

                AddHandler AudioStreamCapturer.AudioDataCaptured, AddressOf HandleAudioCaptured
                AudioStreamCapturer.StartCapture(CInt(App.Settings.AudioStreamingBitrate))

                _listenerThread = New Thread(AddressOf AcceptClientsLoop) With {
                    .IsBackground = True,
                    .Name = "LiveAudioTcp_Listener"
                }
                _listenerThread.Start()

                If FrmPlayer?.PlayState = App.PlayStates.Stopped Then
                    NotifyStatusChanged(StreamingState.Stopped, "Server Active & Idle")
                Else
                    NotifyStatusChanged(StreamingState.Listening, "Server Active & Playing")
                End If

                Debug.WriteLine($"[STREAMING SERVER] LAN TCP streaming listener started on port {port}.")
                Skye.Common.Log.Write($"Streaming Server Started On Port {port}")
                Exit For

            Catch ex As SocketException When ex.SocketErrorCode = SocketError.AddressAlreadyInUse AndAlso attempt < maxRetries
                Try
                    _tcpServer?.Stop()
                Catch
                End Try
                _tcpServer = Nothing
                System.Threading.Thread.Sleep(retryDelayMs)

            Catch ex As Exception
                Try
                    _tcpServer?.Stop()
                Catch
                End Try
                _tcpServer = Nothing

                Debug.WriteLine($"[STREAMING SERVER] Failed to start TCP listener: {ex}")
                Skye.Common.Log.Write($"Streaming Server Failed To Start: {ex}")
                NotifyStatusChanged(StreamingState.Error, ex.ToString)
                [Stop](True)
                Exit For
            End Try
        Next
    End Sub
    Friend Shared Sub [Stop](Optional isDueToError As Boolean = False)
        If Not _isListening AndAlso _tcpServer Is Nothing Then Return

        _isListening = False
        RemoveHandler AudioStreamCapturer.AudioDataCaptured, AddressOf HandleAudioCaptured

        Try
            AudioStreamCapturer.StopCapture()
        Catch ex As Exception
            Debug.WriteLine($"[STREAMING SERVER] AudioStreamCapturer error on stop: {ex.Message}")
        End Try

        Try
            If _tcpServer IsNot Nothing Then
                _tcpServer.Stop()
                _tcpServer = Nothing
            End If
        Catch
        End Try

        SyncLock _clientLock
            For Each clientInfo In _connectedClients
                Try
                    clientInfo.NetStream?.Close()
                    clientInfo.Client?.Close()
                Catch
                End Try
            Next
            _connectedClients.Clear()
        End SyncLock

        If Not isDueToError Then
            If FrmPlayer?.PlayState = App.PlayStates.Stopped Then
                NotifyStatusChanged(StreamingState.Stopped, "Server Active & Idle")
            Else
                NotifyStatusChanged(StreamingState.Stopped, "Server Idle")
            End If
        End If
        Debug.WriteLine("[STREAMING SERVER] Streaming server and capture stopped.")
        Skye.Common.Log.Write($"Streaming Server Stopped")
    End Sub
    Friend Shared Sub DisconnectClient(targetEndPoint As String)
        SyncLock _clientLock
            Dim target = _connectedClients.FirstOrDefault(Function(c) c.EndPoint = targetEndPoint)
            If target IsNot Nothing Then
                Try
                    target.NetStream?.Close()
                    target.Client?.Close()
                Catch
                End Try
                _connectedClients.Remove(target)
                Debug.WriteLine($"[STREAMING SERVER] Forcefully disconnected client: {targetEndPoint}")
                Skye.Common.Log.Write($"Streaming Server Forcefully Disconnected Client: {targetEndPoint}")
            End If
        End SyncLock
    End Sub

    Private Shared Sub HandleAudioCaptured(buffer() As Byte, count As Integer)
        BroadcastAudioBuffer(buffer, count)
    End Sub
    Private Shared Sub NotifyStatusChanged(state As StreamingState, message As String)
        RaiseEvent StreamingStatusChanged(state, message)
    End Sub
    Private Shared Sub AcceptClientsLoop()
        While _isListening
            Try
                If _tcpServer IsNot Nothing AndAlso _tcpServer.Pending() Then
                    Dim client As TcpClient = _tcpServer.AcceptTcpClient()

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
        Dim clientInfo As AudioClientInfo = Nothing
        Try
            client.NoDelay = True
            netStream = client.GetStream()

            Dim reader As New StreamReader(netStream, Encoding.ASCII, False, 1024, leaveOpen:=True)
            Dim requestLine As String = reader.ReadLine()

            If String.IsNullOrEmpty(requestLine) Then
                client.Close()
                Exit Sub
            End If

            ' 1. INTERCEPT HEAD REQUESTS: Windows Media Foundation sends HEAD to probe headers. 
            ' Send headers back immediately and close connection without streaming audio or crashing.
            If requestLine.StartsWith("HEAD", StringComparison.OrdinalIgnoreCase) Then
                Dim headResponse = Encoding.ASCII.GetBytes(
                    "HTTP/1.1 200 OK" & vbCrLf &
                    "Content-Type: audio/mpeg" & vbCrLf &
                    "Accept-Ranges: none" & vbCrLf &
                    "icy-metaint: 0" & vbCrLf &
                    "Connection: close" & vbCrLf & vbCrLf
                )
                netStream.Write(headResponse, 0, headResponse.Length)
                netStream.Flush()
                client.Close()
                Exit Sub
            End If

            ' 2. Intercept duplicate GET probes from an IP that is already connected
            Dim remoteEndPoint = TryCast(client.Client?.RemoteEndPoint, IPEndPoint)
            Dim clientIp = If(remoteEndPoint IsNot Nothing, remoteEndPoint.Address.ToString(), "")

            If Not String.IsNullOrEmpty(clientIp) Then
                SyncLock _clientLock
                    Dim alreadyConnected = _connectedClients.Any(Function(c) c.EndPoint.StartsWith(clientIp))
                    If alreadyConnected Then
                        client.Close()
                        Exit Sub
                    End If
                End SyncLock
            End If

            ' 3. Read remaining HTTP headers (like User-Agent) for standard GET requests
            Dim detectedAgent As String = "Generic / VLC"
            While reader.Peek() >= 0
                Dim line As String = reader.ReadLine()
                If String.IsNullOrEmpty(line) Then Exit While ' End of headers

                If line.StartsWith("User-Agent:", StringComparison.OrdinalIgnoreCase) Then
                    detectedAgent = line.Substring("User-Agent:".Length).Trim()
                End If
            End While

            ' Send standard HTTP response headers for MP3 streaming
            Dim headerBytes = Encoding.ASCII.GetBytes(
                "HTTP/1.1 200 OK" & vbCrLf &
                "Content-Type: audio/mpeg" & vbCrLf &
                "Accept-Ranges: none" & vbCrLf &
                "icy-metaint: 0" & vbCrLf &
                "Cache-Control: no-cache, no-store, must-revalidate" & vbCrLf &
                "Connection: keep-alive" & vbCrLf & vbCrLf
            )
            netStream.Write(headerBytes, 0, headerBytes.Length)
            netStream.Flush()

            clientInfo = New AudioClientInfo(client, netStream) With {
                .UserAgent = detectedAgent
            }

            SyncLock _clientLock
                _connectedClients.Add(clientInfo)
            End SyncLock

            NotifyStatusChanged(StreamingState.Listening, "Client Connected")
            Debug.WriteLine($"[STREAMING SERVER] Client connected from {clientInfo.EndPoint} ({detectedAgent})")

            ' Keep thread alive while client is connected and server is running
            While _isListening AndAlso client.Connected
                Thread.Sleep(500)
            End While

        Catch ex As Exception
            Debug.WriteLine($"[STREAMING SERVER] Client connection error: {ex.Message}")
        Finally
            If clientInfo IsNot Nothing Then
                SyncLock _clientLock
                    _connectedClients.Remove(clientInfo)
                End SyncLock
            End If

            Try
                netStream?.Close()
            Catch
            End Try
            Try
                client?.Close()
            Catch
            End Try

            NotifyStatusChanged(StreamingState.Listening, "Client Disconnected")
            Debug.WriteLine("[STREAMING SERVER] Client disconnected.")
        End Try
    End Sub
    Private Shared Sub BroadcastAudioBuffer(buffer As Byte(), count As Integer)
        Dim streamsChanged As Integer = 0

        SyncLock _clientLock
            If _connectedClients.Count = 0 Then Return

            Dim deadClients As New List(Of AudioClientInfo)()

            For Each clientInfo In _connectedClients
                Try
                    clientInfo.NetStream.Write(buffer, 0, count)
                    clientInfo.NetStream.Flush()
                    clientInfo.BytesStreamed += count
                Catch ex As Exception
                    ' Safely catch dropped probe connections or abrupt Windows disconnections
                    deadClients.Add(clientInfo)
                End Try
            Next

            If deadClients.Count > 0 Then
                For Each deadClient In deadClients
                    _connectedClients.Remove(deadClient)
                    Try
                        deadClient.NetStream?.Close()
                        deadClient.Client?.Close()
                    Catch
                    End Try
                    streamsChanged += 1
                Next
            End If
        End SyncLock

        If streamsChanged > 0 Then NotifyStatusChanged(StreamingState.Listening, $"Dead Streams Removed: {streamsChanged}")
    End Sub

End Class