
Imports System.IO
Imports System.Text
Imports LibVLCSharp.Shared
Imports NAudio.Dsp
Imports NAudio.Wave
Imports Skye
Imports Skye.Contracts
Imports SkyeMusic.My

Public Class Player

    'Declarations
    Friend Enum PlayStates
        Playing
        Paused
        Stopped
    End Enum
    Public Structure PlaylistItemType
        Public Title As String
        Public Path As String
    End Structure
    Private MeterAudioCapture As WasapiLoopbackCapture 'Audio Capture for Meters
    Private MeterPeakLeft, MeterPeakRight, MeterDecayLeft, MeterDecayRight As Single 'Meter Values
    Private mMove As Boolean = False 'For Moving the Form
    Private mOffset, mPosition As System.Drawing.Point 'For Moving the Form
    Friend PlayState As PlayStates = PlayStates.Stopped 'Status of the currently playing song
    Private CurrentMediaType As App.MediaSourceTypes 'Type of the current playing media
    Private Mute As Boolean = False 'True if the player is muted
    Private IsFocused As Boolean = True 'Indicates if the player is focused
    Private AlbumArtCount As Byte = 0 'Number of album art available
    Private AlbumArtIndex As Byte = 0 'Index of the current album art
    Private TrackBarScale As Int16 = 100 'TrackBar Scale for Position
    Private PlaylistItemMove As ListViewItem 'Item being moved in the playlist
    Private PlaylistSearchTitle As String 'Title for Playlist Search
    Private PlaylistSearchItems As New List(Of ListViewItem) 'Items found in the playlist search
    Friend PlaylistCurrentText As String 'Text of the current playlist item
    Private RandomHistory As New Generic.List(Of String) 'History of played items for shuffle play mode
    Private RandomHistoryIndex As Integer = 0 'Index for the shuffle history
    Private CurrentAccentColor As Color 'Current Windows Accent Color
    Private AutoNext As Boolean = False 'Used by Plays Database System to indicate if the player will automatically play the next item.
    Private PausedAt As DateTime? = Nothing 'Used by Plays Database System to track when the player was paused.
    Private TotalPausedDuration As TimeSpan = TimeSpan.Zero 'Used by Plays Database System to track total paused duration.
    Private PlaylistBoldFont As Font 'Bold font for playlist titles
    Private TipPlaylistFont As New Font("Segoe UI", 12) 'Font for Playlist Tooltip
    Private TipPlaylist As Skye.UI.ToolTipEX 'Tooltip for Playlist
    Private PicBoxAlbumArtClickTimer As Timer 'Timer for differentiating between clicks and double-clicks on Album Art
    Friend Queue As New Generic.List(Of String) 'Queue of items to play
    Friend Event TitleChanged(newTitle As String)

    'Sort Orders
    Private PlaylistTitleSort As SortOrder = SortOrder.None
    Private PlaylistPathSort As SortOrder = SortOrder.None
    Private PlaylistRatingSort As SortOrder = SortOrder.None
    Private PlaylistPlayCountSort As SortOrder = SortOrder.None
    Private PlaylistLastPlayedSort As SortOrder = SortOrder.None
    Private PlaylistFirstPlayedSort As SortOrder = SortOrder.None
    Private PlaylistAddedSort As SortOrder = SortOrder.None

    'Lyrics
    Public Class TimedLyric
        Public Property Time As TimeSpan
        Public Property Text As String
    End Class
    Private Lyrics As Boolean = False 'Indicates if the lyrics are active
    Private HasLyrics As Boolean = False 'Indicates if the current playing item has lyrics available
    Private HasLyricsSynced As Boolean = False 'Indicates if the current playing item has synced lyrics available
    Private LyricsText As String = String.Empty 'Lyrics text for unsynced lyrics
    Private LyricsSynced As List(Of TimedLyric) 'Lyrics for synced lyrics
    Private LastLyricsIndex As Integer = -1 'Last index used for synced lyrics

    'FullScreen
    Private frmFullScreen As Form 'Fullscreen Form
    Private originalParent As Control 'Original Parent Control of VLC Viewer
    Private originalBounds As Rectangle 'Original Bounds of VLC Viewer
    Private _FullScreen As Boolean = False 'ONLY Used by FullScreen Property Getter/Setter
    Private Property FullScreen As Boolean 'Indicates if the player is in fullscreen mode
        Get
            Return _FullScreen
        End Get
        Set(value As Boolean)
            _FullScreen = value
            SetFullScreen()
        End Set
    End Property

    'Watcher Property
    Private _watchernotification As String
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Friend Property WatcherNotification As String
        Get
            Return _watchernotification
        End Get
        Set(value As String)
            _watchernotification = value
            If Not String.IsNullOrWhiteSpace(_watchernotification) Then ShowStatusMessage(_watchernotification)
        End Set
    End Property

    'Player Interface
    Private _player As Skye.Contracts.IMediaPlayer 'Media Player Interface
    Private VLCHook As VLCViewerHook 'Hook for VLC Viewer Control
    Public Class VLCPlayer
        Implements IMediaPlayer, IDisposable

        Private ReadOnly _libVLC As LibVLC
        Private ReadOnly _mediaPlayer As MediaPlayer
        Private _currentMedia As Media
        Private _currentPath As String
        Private _transitionGate As Integer = 0 'simple reentrancy guard

        Public Sub New(_invoker As Form)
            Dim args As String() = {
                "--aout=wasapi",               '"--aout=directsound",          'force DirectSound output
                "--no-audio-time-stretch",     'can reduce distortion on pitch correction
                "--audio-resampler=soxr",      'higher quality resampler
                "--file-caching=1000",         '1 second buffer for local files
                "--network-caching=1500"}      'if streaming
            _libVLC = New LibVLC(args)
            _mediaPlayer = New MediaPlayer(_libVLC)
            AddHandler _mediaPlayer.Playing,
                Sub(sender, e)
                    _invoker.BeginInvoke(Sub()
                                             RaiseEvent PlaybackStarted()
                                         End Sub)
                End Sub
            AddHandler _mediaPlayer.EndReached,
                 Sub(sender, e)
                     _invoker.BeginInvoke(Sub()
                                              RaiseEvent PlaybackEnded()
                                          End Sub)
                 End Sub
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            _mediaPlayer.Stop()
            _mediaPlayer.Dispose()
            If _currentMedia IsNot Nothing Then
                _currentMedia.Dispose()
                _currentMedia = Nothing
            End If
            _libVLC.Dispose()
        End Sub

        Public Sub Play(path As String) Implements IMediaPlayer.Play
            If Threading.Interlocked.Exchange(_transitionGate, 1) = 1 Then Exit Sub 'Already transitioning; ignore duplicate calls
            Try

                ' Stop current playback first
                _mediaPlayer.Stop()

                ' Dispose old media safely
                If _currentMedia IsNot Nothing Then
                    _currentMedia.Dispose()
                    _currentMedia = Nothing
                End If

                ' Create and keep the new media alive
                _currentPath = path
                _currentMedia = New Media(_libVLC, path, FromType.FromPath)

                ' Attach and play
                _mediaPlayer.Media = _currentMedia
                _mediaPlayer.Play()

            Finally
                Threading.Interlocked.Exchange(_transitionGate, 0)
            End Try
        End Sub
        Public Sub Play(uri As Uri) Implements IMediaPlayer.Play
            If Threading.Interlocked.Exchange(_transitionGate, 1) = 1 Then Exit Sub 'Already transitioning; ignore duplicate calls
            Try

                ' Stop current playback first
                _mediaPlayer.Stop()

                ' Dispose old media safely
                If _currentMedia IsNot Nothing Then
                    _currentMedia.Dispose()
                    _currentMedia = Nothing
                End If

                ' Create and keep the new media alive
                _currentPath = uri.ToString
                _currentMedia = New Media(_libVLC, uri)

                ' Attach and play
                _mediaPlayer.Media = _currentMedia
                _mediaPlayer.Play()

            Finally
                Threading.Interlocked.Exchange(_transitionGate, 0)
            End Try
        End Sub
        Public Sub Play() Implements IMediaPlayer.Play
            _mediaPlayer.Play()
        End Sub
        Public Sub Pause() Implements IMediaPlayer.Pause
            _mediaPlayer.Pause()
        End Sub
        Public Sub [Stop]() Implements IMediaPlayer.Stop
            _mediaPlayer.Stop()
        End Sub

        Public ReadOnly Property MediaPlayer As MediaPlayer
            Get
                Return _mediaPlayer
            End Get
        End Property
        Public ReadOnly Property HasMedia As Boolean Implements IMediaPlayer.HasMedia
            Get
                Return (_mediaPlayer IsNot Nothing) AndAlso (_mediaPlayer.Media IsNot Nothing)
            End Get
        End Property
        Public ReadOnly Property Path As String Implements IMediaPlayer.Path
            Get
                Return _currentPath
            End Get
        End Property
        Public Property Volume As Integer Implements IMediaPlayer.Volume
            Get
                Return _mediaPlayer.Volume
            End Get
            Set(value As Integer)
                _mediaPlayer.Volume = value
            End Set
        End Property
        Public Property Position As Double Implements IMediaPlayer.Position
            Get
                Return _mediaPlayer.Time / 1000.0
            End Get
            Set(value As Double)
                _mediaPlayer.Time = CInt(value * 1000)
            End Set
        End Property
        Public ReadOnly Property Duration As Double Implements IMediaPlayer.Duration
            Get
                Return _mediaPlayer.Length / 1000.0
            End Get
        End Property
        Public ReadOnly Property VideoWidth As Integer Implements IMediaPlayer.VideoWidth
            Get
                Dim w As UInteger, h As UInteger
                If _mediaPlayer.Size(0, w, h) Then
                    Return CInt(w)
                Else
                    Return 0
                End If
            End Get
        End Property
        Public ReadOnly Property VideoHeight As Integer Implements IMediaPlayer.VideoHeight
            Get
                Dim w As UInteger, h As UInteger
                If _mediaPlayer.Size(0, w, h) Then
                    Return CInt(h)
                Else
                    Return 0
                End If
            End Get
        End Property
        Public ReadOnly Property AspectRatio As Double Implements IMediaPlayer.AspectRatio
            Get
                Return If(VideoHeight > 0, CDbl(VideoWidth) / VideoHeight, 0)
            End Get
        End Property

        Public Event PlaybackStarted() Implements IMediaPlayer.PlaybackStarted
        Public Event PlaybackEnded() Implements IMediaPlayer.PlaybackEnded

    End Class
    Public Class VLCViewerHook
        Inherits NativeWindow

        Public Event SingleClick(pt As Point)
        Public Event DoubleClick(pt As Point)
        Public Event RightClick(pt As Point)

        Private lastClick As DateTime = DateTime.MinValue
        Private clickTimer As Timer

        Protected Overrides Sub WndProc(ByRef m As Message)
            If m.Msg = WinAPI.WM_PARENTNOTIFY Then
                Select Case m.WParam.ToInt32()
                    Case WinAPI.WM_LBUTTONDOWN
                        HandleClick()
                    Case WinAPI.WM_RBUTTONDOWN
                        RaiseEvent RightClick(Cursor.Position)
                End Select
            End If
            MyBase.WndProc(m)
        End Sub

        Private Sub HandleClick()
            Dim now = DateTime.Now
            If (now - lastClick).TotalMilliseconds <= SystemInformation.DoubleClickTime Then
                'Second click within double‑click interval → double‑click
                If clickTimer IsNot Nothing Then
                    clickTimer.Stop()
                    clickTimer.Dispose()
                    clickTimer = Nothing
                End If
                RaiseEvent DoubleClick(Cursor.Position)
                lastClick = DateTime.MinValue
            Else
                'First click → start a timer to see if a second one follows
                lastClick = now
                clickTimer = New Timer With {
                    .Interval = SystemInformation.DoubleClickTime}
                AddHandler clickTimer.Tick,
                Sub()
                    clickTimer.Stop()
                    clickTimer.Dispose()
                    clickTimer = Nothing
                    'No second click arrived → treat as single click
                    RaiseEvent SingleClick(Cursor.Position)
                End Sub
                clickTimer.Start()
            End If
        End Sub
    End Class
    Private Function GetVLCChild(parent As IntPtr) As IntPtr
        Dim h As IntPtr = IntPtr.Zero
        Do
            h = WinAPI.FindWindowEx(parent, h, Nothing, Nothing)
            If h = IntPtr.Zero Then Exit Do

            Dim sb As New StringBuilder(256)
            WinAPI.GetClassName(h, sb, sb.Capacity)
            If sb.ToString().StartsWith("VLC video main") Then
                Return h
            End If
        Loop
        Return IntPtr.Zero
    End Function

    'Visualizer Interface
    Private Visualizer As Boolean = False 'Indicates if the visualizer is active
    Friend VisualizerHost As VisualizerHostClass 'Host for Visualizers
    Private VisualizerEngine As VisualizerAudioEngine 'Audio Engine for Visualizers
    Private Sub VisualizerOn() 'Turn on the Visualizer
        If Not Visualizer Then
            Visualizer = True
            LyricsOff()
            MIVisualizer.BackColor = Skye.WinAPI.GetSystemColor(Skye.WinAPI.COLOR_HIGHLIGHT)
        End If
    End Sub
    Friend Sub VisualizerOff() 'Turn off the Visualizer
        If Visualizer Then
            Visualizer = False
            MIVisualizer.BackColor = Color.Transparent
        End If
    End Sub
    Private Sub ToggleVisualizer() 'Toggle the Visualizer On/Off
        If Visualizer Then
            VisualizerOff()
        Else
            VisualizerOn()
        End If
        ShowMedia()
    End Sub
    Friend Interface IVisualizer 'Visualizer Interface

        ReadOnly Property Name As String
        ReadOnly Property DockedControl As Control

        Sub Start()
        Sub [Stop]()
        Sub Update(audioData As Single()) ' for FFT magnitudes
        Sub UpdateWaveform(audioSamples As Single()) ' for raw waveform
        Sub Resize(width As Integer, height As Integer)

    End Interface
    Friend Class VisualizerHostClass 'Host for Visualizers
        Private ReadOnly visualizers As Dictionary(Of String, IVisualizer)
        Private currentVisualizer As IVisualizer
        Private ReadOnly ownerForm As Player
        Private ReadOnly hostPanel As Panel

        'Properties
        Public ReadOnly Property ActiveVisualizerName As String
            Get
                Return If(currentVisualizer IsNot Nothing, currentVisualizer.Name, String.Empty)
            End Get
        End Property

        'Constructor
        Public Sub New(owner As Player, panel As Panel)
            ownerForm = owner
            hostPanel = panel
            visualizers = New Dictionary(Of String, IVisualizer)(StringComparer.OrdinalIgnoreCase)
            AddHandler App.ThemeChanged, AddressOf OnThemeChanged
        End Sub

        'Handlers
        Private Sub OnThemeChanged(sender As Object, e As EventArgs)
            'Update background colors when theme changes
            If currentVisualizer IsNot Nothing Then
                currentVisualizer.DockedControl.BackColor = App.CurrentTheme.BackColor
            End If
            hostPanel.BackColor = App.CurrentTheme.BackColor
        End Sub
        Private Sub OnMouseClick(sender As Object, e As MouseEventArgs)
            If e.Button = MouseButtons.Right Then
                ShowVisualizerMenu()
            Else
                ownerForm.LVPlaylist.Focus()
            End If
        End Sub
        Private Sub OnMouseDoubleClick(sender As Object, e As MouseEventArgs)
            Player.ToggleMaximized()
            ownerForm.LVPlaylist.Focus()
        End Sub

        'Methods
        Public Sub Register(v As IVisualizer)
            visualizers(v.Name) = v
        End Sub
        Public Sub Activate(name As String)
            If visualizers.ContainsKey(name) Then
                LoadVisualizer(visualizers(name))
            End If
        End Sub
        Public Sub LoadVisualizer(v As IVisualizer)
            'Stop and clear old visualizer
            If currentVisualizer IsNot Nothing Then
                Dim oldCtrl As Control = currentVisualizer.DockedControl
                RemoveHandler oldCtrl.MouseClick, AddressOf Me.OnMouseClick
                RemoveHandler oldCtrl.MouseDoubleClick, AddressOf Me.OnMouseDoubleClick
                currentVisualizer.Stop()
                hostPanel.Controls.Clear()
            End If

            currentVisualizer = v

            Dim ctrl As Control = v.DockedControl
            ctrl.Dock = DockStyle.Fill
            ctrl.BackColor = App.CurrentTheme.BackColor
            ctrl.TabStop = False

            'Attach handlers for right-click and double‑click and keydown
            AddHandler ctrl.MouseClick, AddressOf Me.OnMouseClick
            AddHandler ctrl.MouseDoubleClick, AddressOf Me.OnMouseDoubleClick
            AddHandler ctrl.GotFocus,
                Sub(s, e)
                    ownerForm.Focus()
                End Sub

            hostPanel.Controls.Add(ctrl)
            v.Start()
        End Sub
        Public Sub FeedAudio(data As Single())
            currentVisualizer?.Update(data)
        End Sub
        Public Sub FeedWaveform(samples As Single())
            currentVisualizer?.UpdateWaveform(samples)
        End Sub
        Public Sub ResizeHost()
            currentVisualizer?.Resize(hostPanel.Width, hostPanel.Height)
        End Sub
        Private Sub ShowVisualizerMenu()
            Dim menu As New ContextMenuStrip()
            App.ThemeMenu(menu)
            For Each vizName In visualizers.Keys
                Dim item As New ToolStripMenuItem(vizName) With {
                    .Font = ownerForm.Font}

                'Mark the active one with a check
                If vizName.Equals(Me.ActiveVisualizerName, StringComparison.OrdinalIgnoreCase) Then
                    item.Checked = True
                End If

                AddHandler item.Click,
                    Sub(sender, e)
                        App.Visualizer = vizName
                        ownerForm.ShowMedia()
                    End Sub
                menu.Items.Add(item)
            Next

            'Show the menu at mouse position
            menu.Show(Cursor.Position)
        End Sub
        Public Sub SetVisualizersMenu()
            Dim menu As New ContextMenuStrip()
            App.ThemeMenu(menu)
            For Each vizName In visualizers.Keys
                Dim item As New ToolStripMenuItem(vizName) With {
                    .Font = ownerForm.Font}
                AddHandler item.Click,
                    Sub(sender, e)
                        App.Visualizer = vizName
                        ownerForm.VisualizerOn()
                        ownerForm.ShowMedia()
                    End Sub
                menu.Items.Add(item)
            Next
            AddHandler menu.Opening,
                Sub(sender, e)
                    Dim s As ContextMenuStrip = CType(sender, ContextMenuStrip)
                    For Each tsmi As ToolStripMenuItem In s.Items
                        tsmi.Checked = False
                        If tsmi.Text.Equals(App.Visualizer, StringComparison.OrdinalIgnoreCase) Then
                            tsmi.Checked = True
                        End If
                    Next
                End Sub
            ownerForm.MIVisualizers.DropDown = menu
        End Sub
        Public Function GetVisualizerNames() As List(Of String)
            Return visualizers.Keys.ToList
        End Function
        Public Sub SetHyperspaceTunnelParticleCount(count As Integer)
            Dim tunnel = TryCast(visualizers("Hyperspace Tunnel"), VisualizerHyperspaceTunnel)
            tunnel?.SetParticleCount(count)
        End Sub
        Public Sub SetStarFieldStarCount(count As Integer)
            Dim tunnel = TryCast(visualizers("Star Field"), VisualizerStarField)
            tunnel?.SetStarCount(count)
        End Sub

    End Class
    Private Class VisualizerAudioEngine

        'Declarations
        Private capture As WasapiLoopbackCapture
        Private buffer() As Byte
        Private ReadOnly visualizerHost As VisualizerHostClass

        'Constructor
        Public Sub New(host As VisualizerHostClass)
            visualizerHost = host
        End Sub

        'Methods
        Public Sub Start()
            capture = New WasapiLoopbackCapture()
            AddHandler capture.DataAvailable, AddressOf OnDataAvailable
            capture.StartRecording()
        End Sub
        Public Sub [Stop]()
            If capture IsNot Nothing Then
                capture.StopRecording()
                RemoveHandler capture.DataAvailable, AddressOf OnDataAvailable
                capture.Dispose()
            End If
        End Sub

        'Handlers
        'Private Sub OnDataAvailable(sender As Object, e As WaveInEventArgs)
        '    buffer = e.Buffer

        '    'Convert to float samples
        '    Dim sampleCount = e.BytesRecorded \ 4
        '    Dim samples(sampleCount - 1) As Single
        '    For i = 0 To sampleCount - 1
        '        samples(i) = BitConverter.ToSingle(buffer, i * 4)
        '    Next

        '    'Apply FFT
        '    Dim fftSize = 1024
        '    Dim fftBuffer(fftSize - 1) As Complex
        '    For i = 0 To fftSize - 1
        '        If i < samples.Length Then
        '            fftBuffer(i).X = samples(i)
        '            fftBuffer(i).Y = 0
        '        Else
        '            fftBuffer(i).X = 0
        '            fftBuffer(i).Y = 0
        '        End If
        '    Next
        '    FastFourierTransform.FFT(True, CInt(Math.Log(fftSize, 2)), fftBuffer)

        '    'Extract magnitudes
        '    Dim magnitudes(fftSize \ 2 - 1) As Single
        '    For i = 0 To magnitudes.Length - 1
        '        magnitudes(i) = CSng(Math.Sqrt(fftBuffer(i).X ^ 2 + fftBuffer(i).Y ^ 2))
        '    Next

        '    'Feed to visualizer
        '    visualizerHost.FeedAudio(magnitudes)
        'End Sub
        Private Sub OnDataAvailable(sender As Object, e As WaveInEventArgs)
            buffer = e.Buffer

            ' Convert to float samples
            Dim sampleCount = e.BytesRecorded \ 4
            Dim samples(sampleCount - 1) As Single
            For i = 0 To sampleCount - 1
                samples(i) = BitConverter.ToSingle(buffer, i * 4)
            Next

            ' Feed raw waveform to oscilloscope
            visualizerHost.FeedWaveform(samples)

            ' Apply FFT
            Dim fftSize = 1024
            Dim fftBuffer(fftSize - 1) As Complex
            For i = 0 To fftSize - 1
                If i < samples.Length Then
                    fftBuffer(i).X = samples(i)
                    fftBuffer(i).Y = 0
                Else
                    fftBuffer(i).X = 0
                    fftBuffer(i).Y = 0
                End If
            Next
            FastFourierTransform.FFT(True, CInt(Math.Log(fftSize, 2)), fftBuffer)

            ' Extract magnitudes
            Dim magnitudes(fftSize \ 2 - 1) As Single
            For i = 0 To magnitudes.Length - 1
                magnitudes(i) = CSng(Math.Sqrt(fftBuffer(i).X ^ 2 + fftBuffer(i).Y ^ 2))
            Next

            ' Feed spectrum to analyzers
            visualizerHost.FeedAudio(magnitudes)
        End Sub
    End Class
    Private Class VisualizerRainbowBar
        Inherits UserControl
        Implements IVisualizer

        ' Declarations
        Private ReadOnly updateTimer As Timer
        Private audioData(), lastMagnitudes(), peakValues() As Single
        Private peakHold() As Integer
        Private hueOffset As Single = 0.0F

        ' Constructor
        Public Sub New()
            Me.DoubleBuffered = True
            updateTimer = New Timer With {.Interval = 33} '~30 FPS
            AddHandler updateTimer.Tick, AddressOf OnTick
        End Sub

        ' IVisualizer Implementation
        Public Overloads ReadOnly Property Name As String Implements IVisualizer.Name
            Get
                Return "Rainbow Bar"
            End Get
        End Property
        Public ReadOnly Property DockedControl As Control Implements IVisualizer.DockedControl
            Get
                Return Me
            End Get
        End Property
        Public Sub Start() Implements IVisualizer.Start
            updateTimer.Start()
        End Sub
        Public Sub [Stop]() Implements IVisualizer.Stop
            updateTimer.Stop()
        End Sub
        Public Overloads Sub Update(data As Single()) Implements IVisualizer.Update
            audioData = data
        End Sub
        Public Overloads Sub UpdateWaveform(samples As Single()) Implements IVisualizer.UpdateWaveform
            ' Not Implemented
        End Sub
        Public Shadows Sub Resize(width As Integer, height As Integer) Implements IVisualizer.Resize
        End Sub

        ' Handlers
        Private Sub OnTick(sender As Object, e As EventArgs)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            MyBase.OnPaint(pe)
            If audioData Is Nothing OrElse audioData.Length = 0 Then Exit Sub

            Dim g = pe.Graphics
            Dim barCount = App.Visualizers.RainbowBarCount
            Dim barWidth As Single = CSng(Width) / barCount
            Dim maxHeight = Height

            ' Initialize smoothing buffer if needed
            If lastMagnitudes Is Nothing OrElse lastMagnitudes.Length <> barCount Then
                ReDim lastMagnitudes(barCount - 1)
            End If
            If peakValues Is Nothing OrElse peakValues.Length <> barCount Then
                ReDim peakValues(barCount - 1)
            End If
            If peakHold Is Nothing OrElse peakHold.Length <> barCount Then
                ReDim peakHold(barCount - 1)
            End If

            For i = 0 To barCount - 1
                Dim valueIdx = i * audioData.Length \ barCount
                Dim rawMagnitude = audioData(valueIdx)

                ' Apply gain and clamp
                Dim boosted = Math.Min(rawMagnitude * App.Visualizers.RainbowBarGain, 1.0F)

                ' Smooth with previous frame
                Dim smoothed = (lastMagnitudes(i) * 0.7F) + (boosted * 0.3F)
                lastMagnitudes(i) = smoothed

                ' Scale to height
                Dim barHeight = CInt(smoothed * maxHeight)
                Dim x = CInt(i * barWidth)
                Dim y = maxHeight - barHeight
                Dim width = CInt(barWidth) - 2

                ' Draw main bar
                Dim hue As Single = (CSng(i) / barCount * 360.0F + hueOffset) Mod 360.0F
                Dim rainbowColor As Color = ColorFromHSV(hue, 1.0F, 1.0F)
                Using brush As New SolidBrush(rainbowColor)
                    g.FillRectangle(brush, x, y, width, barHeight)
                End Using

                ' Peak bar logic
                If App.Visualizers.RainbowBarShowPeaks Then
                    Dim currentPeak As Integer = CInt(smoothed * maxHeight)

                    If currentPeak > peakValues(i) Then
                        peakValues(i) = currentPeak
                        peakHold(i) = App.Visualizers.RainbowBarPeakHoldFrames
                    Else
                        If peakHold(i) > 0 Then
                            ' Still holding: decrement counter, keep peak stuck
                            peakHold(i) -= 1
                        Else
                            ' No hold left: start decaying
                            peakValues(i) = Math.Max(0, peakValues(i) - App.Visualizers.RainbowBarPeakDecaySpeed)
                        End If
                    End If

                    ' Only draw peak if above threshold
                    If peakValues(i) > App.Visualizers.RainbowBarPeakThreshold Then
                        Dim peakY As Integer = maxHeight - CInt(peakValues(i)) - 1
                        Dim thickness As Integer = App.Visualizers.RainbowBarPeakThickness 'fixed thickness preset
                        Dim peakColor As Color = ColorFromHSV(hue, 1.0F, 1.0F)
                        Using peakbrush As New SolidBrush(peakColor)
                            g.FillRectangle(peakbrush, x, peakY, width, thickness)
                        End Using
                    End If
                End If

            Next

            ' Advance hue offset for next frame
            hueOffset = (hueOffset + App.Visualizers.RainbowBarHueCycleSpeed) Mod 360.0F
        End Sub

        ' Methods
        Private Function ColorFromHSV(hue As Double, saturation As Double, value As Double) As Color
            Dim hi As Integer = CInt(Math.Floor(hue / 60)) Mod 6
            Dim f As Double = hue / 60 - Math.Floor(hue / 60)

            Dim v As Double = value * 255
            Dim p As Double = v * (1 - saturation)
            Dim q As Double = v * (1 - f * saturation)
            Dim t As Double = v * (1 - (1 - f) * saturation)

            Select Case hi
                Case 0 : Return Color.FromArgb(255, CInt(v), CInt(t), CInt(p))
                Case 1 : Return Color.FromArgb(255, CInt(q), CInt(v), CInt(p))
                Case 2 : Return Color.FromArgb(255, CInt(p), CInt(v), CInt(t))
                Case 3 : Return Color.FromArgb(255, CInt(p), CInt(q), CInt(v))
                Case 4 : Return Color.FromArgb(255, CInt(t), CInt(p), CInt(v))
                Case Else : Return Color.FromArgb(255, CInt(v), CInt(p), CInt(q))
            End Select
        End Function

    End Class
    Private Class VisualizerClassicSpectrumAnalyzer
        Inherits UserControl
        Implements IVisualizer

        ' Timer and buffers
        Private ReadOnly updateTimer As Timer
        Private audioData() As Single
        Private rollingBuffer() As Single
        Private rollingIndex As Integer = 0

        ' FFT config
        Private ReadOnly fftSize As Integer = 1024    ' power of 2 (512/1024/2048)
        Private ReadOnly fftBins As Integer = fftSize \ 2

        ' Rendering buffers
        Private spectrumMagnitudes() As Single        ' per-bin magnitudes
        Private barMagnitudes() As Single             ' mapped to barCount
        Private lastBars() As Single                  ' smoothing
        Private peakValues() As Single
        Private peakHold() As Integer

        Public Sub New()
            DoubleBuffered = True
            updateTimer = New Timer With {.Interval = 33} ' ~30 FPS
            AddHandler updateTimer.Tick, AddressOf OnTick
        End Sub

        ' IVisualizer Implementation
        Public Overloads ReadOnly Property Name As String Implements IVisualizer.Name
            Get
                Return "Classic Spectrum Analyzer"
            End Get
        End Property
        Public ReadOnly Property DockedControl As Control Implements IVisualizer.DockedControl
            Get
                Return Me
            End Get
        End Property
        Public Sub Start() Implements IVisualizer.Start
            updateTimer.Start()
        End Sub
        Public Sub [Stop]() Implements IVisualizer.Stop
            updateTimer.Stop()
        End Sub
        Public Overloads Sub Update(data As Single()) Implements IVisualizer.Update
            If rollingBuffer Is Nothing OrElse rollingBuffer.Length <> fftSize Then
                ReDim rollingBuffer(fftSize - 1)
            End If

            For Each sample In data
                rollingBuffer(rollingIndex) = sample
                rollingIndex = (rollingIndex + 1) Mod fftSize
            Next

            ' Copy buffer to audioData for FFT
            audioData = CType(rollingBuffer.Clone(), Single())
        End Sub
        Public Overloads Sub UpdateWaveform(samples As Single()) Implements IVisualizer.UpdateWaveform
            ' Not Implemented
        End Sub
        Public Shadows Sub Resize(width As Integer, height As Integer) Implements IVisualizer.Resize
        End Sub

        ' Handlers
        Private Sub OnTick(sender As Object, e As EventArgs)
            Invalidate()
        End Sub
        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            MyBase.OnPaint(pe)
            If audioData Is Nothing OrElse audioData.Length < fftSize Then Exit Sub
            Dim g = pe.Graphics

            ' Ensure buffers
            If spectrumMagnitudes Is Nothing OrElse spectrumMagnitudes.Length <> fftBins Then
                ReDim spectrumMagnitudes(fftBins - 1)
            End If
            If barMagnitudes Is Nothing OrElse barMagnitudes.Length <> App.Visualizers.ClassicSpectrumAnalyzerBarCount Then
                ReDim barMagnitudes(App.Visualizers.ClassicSpectrumAnalyzerBarCount - 1)
                ReDim lastBars(App.Visualizers.ClassicSpectrumAnalyzerBarCount - 1)
                ReDim peakValues(App.Visualizers.ClassicSpectrumAnalyzerBarCount - 1)
                ReDim peakHold(App.Visualizers.ClassicSpectrumAnalyzerBarCount - 1)
            End If

            ' 1) Compute per-bin magnitudes from latest fftSize samples
            ComputeSpectrum(audioData, fftSize, spectrumMagnitudes)

            ' 2) Map bins -> bars (logarithmic bands for better low-end resolution)
            Select Case App.Visualizers.ClassicSpectrumAnalyzerBandMappingMode
                Case App.VisualizerSettings.ClassicSpectrumAnalyzerBandMappingModes.Linear
                    MapBinsToBarsLinear(spectrumMagnitudes, barMagnitudes)
                Case App.VisualizerSettings.ClassicSpectrumAnalyzerBandMappingModes.Logarithmic
                    MapBinsToBarsLogarithmic(spectrumMagnitudes, barMagnitudes)
            End Select

            ' 3) Draw bars with smoothing, gain, peaks
            Dim barWidth As Single = CSng(Width) / App.Visualizers.ClassicSpectrumAnalyzerBarCount
            Dim maxHeight As Integer = Height

            For i = 0 To App.Visualizers.ClassicSpectrumAnalyzerBarCount - 1
                ' Gain + clamp
                Dim boosted As Single = Math.Min(barMagnitudes(i) * App.Visualizers.ClassicSpectrumAnalyzerGain, 1.0F)

                ' Smooth
                Dim smoothed As Single = (lastBars(i) * App.Visualizers.ClassicSpectrumAnalyzerSmoothing) + (boosted * (1.0F - App.Visualizers.ClassicSpectrumAnalyzerSmoothing))
                lastBars(i) = smoothed

                ' Height
                Dim barHeight As Integer = CInt(smoothed * maxHeight)
                Dim x As Integer = CInt(i * barWidth)
                Dim y As Integer = maxHeight - barHeight
                Dim width As Integer = Math.Max(1, CInt(barWidth) - 2)

                ' Classic color (choose any solid or subtle gradient)
                Using brush As New SolidBrush(App.CurrentTheme.TextColor)
                    g.FillRectangle(brush, x, y, width, barHeight)
                End Using

                ' Peaks
                If App.Visualizers.ClassicSpectrumAnalyzerShowPeaks Then
                    Dim currentPeak As Integer = barHeight
                    If currentPeak > peakValues(i) Then
                        ' New higher peak: set it and reset hold counter
                        peakValues(i) = currentPeak
                        peakHold(i) = App.Visualizers.ClassicSpectrumAnalyzerPeakHoldFrames
                    Else
                        If peakHold(i) > 0 Then
                            ' Still holding: decrement counter, keep peak stuck
                            peakHold(i) -= 1
                        Else
                            ' No hold left: start decaying
                            peakValues(i) = Math.Max(0, peakValues(i) - App.Visualizers.ClassicSpectrumAnalyzerPeakDecay)
                        End If
                    End If
                    If peakValues(i) > 2 Then
                        Dim peakY As Integer = maxHeight - CInt(peakValues(i)) - 1
                        Using pBrush As New SolidBrush(App.CurrentTheme.ButtonTextColor)
                            g.FillRectangle(pBrush, x, peakY, width, 2)
                        End Using
                    End If
                End If
            Next
        End Sub

        ' Methods
        Private Sub ComputeSpectrum(samples() As Single, size As Integer, ByRef mags() As Single)
            ' Compute FFT magnitudes from time-domain samples
            ' Window the latest fftSize samples (Hann window)
            Dim startIdx As Integer = samples.Length - size
            If startIdx < 0 Then startIdx = 0

            Dim data(size - 1) As Complex
            For n = 0 To size - 1
                Dim s As Single = samples(startIdx + n)
                Dim w As Single = Hann(n, size)
                data(n).X = s * w   ' real
                data(n).Y = 0.0F    ' imag
            Next

            ' NAudio FFT: m = log2(size)
            Dim m As Integer = CInt(Math.Log(size, 2))
            FastFourierTransform.FFT(True, m, data)

            ' Convert complex bins to magnitudes (use first half: 0..Nyquist)
            Dim half As Integer = size \ 2
            For i = 0 To half - 1
                Dim re As Single = data(i).X
                Dim im As Single = data(i).Y
                Dim mag As Single = CSng(Math.Sqrt(re * re + im * im))

                ' Optional: compress dynamic range (log-ish)
                mags(i) = Compress(mag)
            Next
        End Sub
        Private Sub MapBinsToBarsLinear(bins() As Single, ByRef bars() As Single)
            ' Linear bin -> bar mapping (simple averaging)
            Dim barCount As Integer = bars.Length
            Dim binsPerBar As Double = bins.Length / CDbl(barCount)

            For b = 0 To barCount - 1
                Dim startBin As Integer = CInt(Math.Floor(b * binsPerBar))
                Dim endBin As Integer = CInt(Math.Floor((b + 1) * binsPerBar))
                If endBin <= startBin Then endBin = startBin + 1
                If endBin > bins.Length Then endBin = bins.Length

                Dim sum As Double = 0
                Dim count As Integer = endBin - startBin
                For i = startBin To endBin - 1
                    sum += bins(i)
                Next
                Dim avg As Single = CSng(sum / Math.Max(1, count))

                ' Normalize roughly into 0..1 (tune scaling here)
                bars(b) = Math.Min(avg * 3.0F, 1.0F)
            Next
        End Sub
        Private Sub MapBinsToBarsLogarithmic(bins() As Single, ByRef bars() As Single)
            Dim barCount As Integer = bars.Length
            Dim nyquist As Double = 22050.0 ' assuming 44.1 kHz sample rate
            Dim binResolution As Double = nyquist / bins.Length

            For b = 0 To barCount - 1
                ' Logarithmic frequency step: from ~20 Hz to Nyquist
                Dim fLow As Double = 20.0 * Math.Pow(nyquist / 20.0, b / CDbl(barCount))
                Dim fHigh As Double = 20.0 * Math.Pow(nyquist / 20.0, (b + 1) / CDbl(barCount))

                ' Convert frequencies to bin indices
                Dim startBin As Integer = CInt(Math.Floor(fLow / binResolution))
                Dim endBin As Integer = CInt(Math.Floor(fHigh / binResolution))
                If endBin <= startBin Then endBin = startBin + 1
                If endBin > bins.Length Then endBin = bins.Length

                ' Average magnitudes in this band
                Dim sum As Double = 0
                Dim count As Integer = endBin - startBin
                For i = startBin To endBin - 1
                    sum += bins(i)
                Next
                Dim avg As Single = CSng(sum / Math.Max(1, count))

                ' Normalize roughly into 0..1 (tune scaling here)
                bars(b) = Math.Min(avg * 3.0F, 1.0F)
            Next
        End Sub
        Private Function Hann(index As Integer, length As Integer) As Single
            ' Hann window: w[n] = 0.5 * (1 - cos(2πn / (N - 1))), for n = 0..N-1
            If length <= 1 Then Return 1.0F
            Dim ratio As Double = (2.0 * Math.PI * index) / (length - 1)
            Dim w As Double = 0.5 * (1.0 - Math.Cos(ratio))
            Return CSng(w)
        End Function
        Private Function Compress(x As Single) As Single
            ' Gentle compression to keep levels readable
            ' Map raw magnitude to a friendlier 0..1 curve (sqrt is simple and effective)
            Return CSng(Math.Sqrt(Math.Min(1.0F, Math.Max(0.0F, x))))
        End Function

    End Class
    Private Class VisualizerCircularSpectrum
        Inherits UserControl
        Implements IVisualizer

        ' Timer and buffers
        Private ReadOnly updateTimer As Timer
        Private audioData() As Single
        Private smoothedBins() As Single

        Public Sub New()
            DoubleBuffered = True
            updateTimer = New Timer With {.Interval = 33} ' ~30 FPS
            AddHandler updateTimer.Tick, AddressOf OnTick
        End Sub

        ' IVisualizer Implementation
        Public Overloads ReadOnly Property Name As String Implements IVisualizer.Name
            Get
                Return "Circular Spectrum"
            End Get
        End Property
        Public ReadOnly Property DockedControl As Control Implements IVisualizer.DockedControl
            Get
                Return Me
            End Get
        End Property
        Public Sub Start() Implements IVisualizer.Start
            updateTimer.Start()
        End Sub
        Public Sub [Stop]() Implements IVisualizer.Stop
            updateTimer.Stop()
        End Sub
        Public Overloads Sub Update(data As Single()) Implements IVisualizer.Update
            audioData = data
        End Sub
        Public Overloads Sub UpdateWaveform(samples As Single()) Implements IVisualizer.UpdateWaveform
            ' Not Implemented
        End Sub
        Public Shadows Sub Resize(width As Integer, height As Integer) Implements IVisualizer.Resize
        End Sub

        ' Handlers
        Private Sub OnTick(sender As Object, e As EventArgs)
            Invalidate()
        End Sub
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            ' Snapshot FFT bins
            Dim bins() As Single
            SyncLock Me
                If audioData Is Nothing OrElse audioData.Length < 2 Then Exit Sub
                bins = CType(audioData.Clone(), Single())
            End SyncLock

            ' Use only first half of FFT (unique frequencies)
            Dim barCount As Integer = bins.Length \ 2
            If barCount < 2 Then Exit Sub

            ' Normalize magnitudes to 0–1
            Dim maxVal As Single = bins.Take(barCount).Max()
            If maxVal > 0 Then
                For i As Integer = 0 To barCount - 1
                    bins(i) /= maxVal
                Next
            End If

            ' Apply frequency weighting based on preset
            Dim sampleRate As Integer = 44100
            For i As Integer = 0 To barCount - 1
                Dim freq As Double = (i * sampleRate) / (2.0 * barCount)
                Dim weight As Single = 1.0F
                Select Case App.Visualizers.CircularSpectrumWeightingMode
                    Case App.VisualizerSettings.CircularSpectrumWeightingModes.Balanced
                        If freq < 200 Then weight = 0.5F
                        If freq >= 200 AndAlso freq < 2000 Then weight = 1.0F
                        If freq >= 2000 AndAlso freq < 8000 Then weight = 1.0F
                        If freq >= 8000 Then weight = 0.7F
                    Case App.VisualizerSettings.CircularSpectrumWeightingModes.BassHeavy
                        If freq < 200 Then weight = 1.2F
                        If freq >= 200 AndAlso freq < 2000 Then weight = 1.0F
                        If freq >= 2000 Then weight = 0.8F
                    Case App.VisualizerSettings.CircularSpectrumWeightingModes.TrebleBright
                        If freq < 200 Then weight = 0.4F
                        If freq >= 2000 Then weight = 1.3F
                    Case App.VisualizerSettings.CircularSpectrumWeightingModes.Raw
                        weight = 1.0F
                    Case App.VisualizerSettings.CircularSpectrumWeightingModes.Warm
                        If freq < 200 Then weight = 1.1F
                        If freq >= 2000 Then weight = 0.8F
                    Case App.VisualizerSettings.CircularSpectrumWeightingModes.VShape
                        If freq < 200 Then weight = 1.2F
                        If freq >= 500 AndAlso freq < 2000 Then weight = 0.8F
                        If freq >= 2000 Then weight = 1.2F
                    Case App.VisualizerSettings.CircularSpectrumWeightingModes.MidFocus
                        If freq >= 500 AndAlso freq < 2000 Then weight = 1.3F
                        If freq < 200 OrElse freq >= 8000 Then weight = 0.6F
                    Case Else
                        weight = 1.0F
                End Select
                bins(i) = bins(i) * weight
            Next

            ' Initialize smoothed buffer if needed
            If smoothedBins Is Nothing OrElse smoothedBins.Length <> barCount Then
                ReDim smoothedBins(barCount - 1)
            End If

            ' Apply smoothing
            For i As Integer = 0 To barCount - 1
                smoothedBins(i) = (App.Visualizers.CircularSpectrumSmoothing * bins(i)) + ((1 - App.Visualizers.CircularSpectrumSmoothing) * smoothedBins(i))
            Next

            ' Center of control
            Dim cx As Single = Me.Width / 2.0F
            Dim cy As Single = Me.Height / 2.0F
            Dim radiusBase As Single = Math.Min(cx, cy) * App.Visualizers.CircularSpectrumRadiusFactor
            Dim angleStep As Single = 360.0F / barCount

            ' Collect inner and outer points
            Dim innerPoints As New List(Of PointF)
            Dim outerPoints As New List(Of PointF)

            For i As Integer = 0 To barCount - 1
                Dim angleRad As Single = CSng((i * angleStep) * Math.PI / 180.0F)
                Dim barLength As Single = CSng(Math.Log(1 + smoothedBins(i) * App.Visualizers.CircularSpectrumGain) * radiusBase)

                ' Inner circle point
                innerPoints.Add(New PointF(
                    CSng(cx + Math.Cos(angleRad) * radiusBase),
                    CSng(cy + Math.Sin(angleRad) * radiusBase)))

                ' Outer tip point
                outerPoints.Add(New PointF(
                    CSng(cx + Math.Cos(angleRad) * (radiusBase + barLength)),
                    CSng(cy + Math.Sin(angleRad) * (radiusBase + barLength))))
            Next

            ' Style
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            If App.Visualizers.CircularSpectrumFill Then
                ' Build band polygon: outer points clockwise, inner points reversed
                Dim bandPoints As New List(Of PointF)(outerPoints)
                innerPoints.Reverse()
                bandPoints.AddRange(innerPoints)

                ' Fill the band
                Using brush As New SolidBrush(App.CurrentTheme.TextColor)
                    e.Graphics.FillPolygon(brush, bandPoints.ToArray())
                End Using

                ' Optional outline
                Using pen As New Pen(App.CurrentTheme.TextColor, App.Visualizers.CircularSpectrumLineWidth)
                    e.Graphics.DrawPolygon(pen, bandPoints.ToArray())
                End Using
            Else
                ' Original spoke style
                Using pen As New Pen(App.CurrentTheme.TextColor, App.Visualizers.CircularSpectrumLineWidth)
                    For i As Integer = 0 To barCount - 1
                        e.Graphics.DrawLine(pen, innerPoints(i), outerPoints(i))
                    Next
                End Using
            End If
        End Sub

    End Class
    Private Class VisualizerWaveform
        Inherits UserControl
        Implements IVisualizer

        ' Declarations
        Private ReadOnly updateTimer As Timer
        Private audioData() As Single

        ' Constructor
        Public Sub New()
            Me.DoubleBuffered = True
            updateTimer = New Timer With {.Interval = 33} ' ~30 FPS
            AddHandler updateTimer.Tick, AddressOf OnTick
        End Sub

        ' IVisualizer Implementation
        Public Overloads ReadOnly Property Name As String Implements IVisualizer.Name
            Get
                Return "Waveform"
            End Get
        End Property
        Public ReadOnly Property DockedControl As Control Implements IVisualizer.DockedControl
            Get
                Return Me
            End Get
        End Property
        Public Sub Start() Implements IVisualizer.Start
            updateTimer.Start()
        End Sub
        Public Sub [Stop]() Implements IVisualizer.Stop
            updateTimer.Stop()
        End Sub
        Public Overloads Sub Update(data As Single()) Implements IVisualizer.Update
            audioData = data
        End Sub
        Public Overloads Sub UpdateWaveform(samples As Single()) Implements IVisualizer.UpdateWaveform
            ' Not Implemented
        End Sub
        Public Shadows Sub Resize(width As Integer, height As Integer) Implements IVisualizer.Resize
        End Sub

        ' Handlers
        Private Sub OnTick(sender As Object, e As EventArgs)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            MyBase.OnPaint(pe)
            If audioData Is Nothing OrElse audioData.Length = 0 Then Exit Sub

            Dim g = pe.Graphics
            Dim stepX As Single = CSng(Me.Width) / audioData.Length

            Dim usableHeight As Single = Me.Height / 1.0F
            Dim baselineY As Single
            If App.Visualizers.WaveformFill Then
                baselineY = Me.Height
            Else
                baselineY = Me.Height - 10   ' anchor near bottom
            End If

            Dim maxSample As Single = audioData.Max(Function(s) Math.Abs(s))
            If maxSample = 0 Then maxSample = 1
            Dim scale As Single = usableHeight / maxSample

            ' Build waveform points
            Dim pts(audioData.Length - 1) As PointF
            For i = 0 To audioData.Length - 1
                Dim sample = audioData(i) * scale
                Dim y = baselineY - sample
                pts(i) = New PointF(i * stepX, y)
            Next

            If App.Visualizers.WaveformFill Then
                ' Build polygon: waveform points + baseline back to start
                Dim poly(pts.Length + 1) As PointF
                Array.Copy(pts, poly, pts.Length)
                poly(pts.Length) = New PointF(pts(pts.Length - 1).X, baselineY)
                poly(pts.Length + 1) = New PointF(pts(0).X, baselineY)
                Using brush As New SolidBrush(Color.FromArgb(128, App.CurrentTheme.TextColor))
                    g.FillPolygon(brush, poly)
                End Using
            End If

            ' Always draw the line on top
            Using pen As New Pen(App.CurrentTheme.TextColor, 2)
                g.DrawLines(pen, pts)
            End Using
        End Sub

    End Class
    Private Class VisualizerOscilloscope
        Inherits UserControl
        Implements IVisualizer

        ' Declarations
        Private ReadOnly updateTimer As Timer

        Private waveform() As Single
        Private lastWaveform() As Single
        Private waveformRight() As Single
        Private lastWaveformRight() As Single
        Private glowBuffer As Bitmap
        Private glowGraphics As Graphics

        ' Constructor
        Public Sub New()
            Me.DoubleBuffered = True
            updateTimer = New Timer With {.Interval = 33} ' ~30 FPS
            AddHandler updateTimer.Tick, AddressOf OnTick
        End Sub

        ' IVisualizer Implementation
        Public Overloads ReadOnly Property Name As String Implements IVisualizer.Name
            Get
                Return "Oscilloscope"
            End Get
        End Property
        Public ReadOnly Property DockedControl As Control Implements IVisualizer.DockedControl
            Get
                Return Me
            End Get
        End Property
        Public Sub Start() Implements IVisualizer.Start
            updateTimer.Start()
        End Sub
        Public Sub [Stop]() Implements IVisualizer.Stop
            updateTimer.Stop()
        End Sub
        Public Overloads Sub Update(data As Single()) Implements IVisualizer.Update
            ' Not Implemented
        End Sub
        Public Sub UpdateWaveform(samples As Single()) Implements IVisualizer.UpdateWaveform
            UpdateWave(samples)
        End Sub
        Public Shadows Sub Resize(width As Integer, height As Integer) Implements IVisualizer.Resize
            ' Prevent invalid bitmap sizes
            If width <= 0 OrElse height <= 0 Then
                Exit Sub
            End If

            ' Recreate glow buffer
            If glowBuffer IsNot Nothing Then glowBuffer.Dispose()
            If glowGraphics IsNot Nothing Then glowGraphics.Dispose()

            glowBuffer = New Bitmap(width, height)
            glowGraphics = Graphics.FromImage(glowBuffer)
            glowGraphics.Clear(App.CurrentTheme.BackColor)
        End Sub

        ' Handlers
        Private Sub OnTick(sender As Object, e As EventArgs)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            ' Snapshot waveform once
            Dim localLeft() As Single
            Dim localRight() As Single = Nothing

            SyncLock Me
                If waveform Is Nothing OrElse waveform.Length < 2 Then Exit Sub
                localLeft = CType(waveform.Clone(), Single())
                If App.Visualizers.OscilloscopeChannelMode = App.VisualizerSettings.OscilloscopeChannelModes.StereoBoth AndAlso waveformRight IsNot Nothing Then
                    localRight = CType(waveformRight.Clone(), Single())
                End If
            End SyncLock

            Dim midY = Me.Height \ 2
            Dim stepX = Me.Width / CSng(localLeft.Length - 1)

            ' If glow is disabled, draw directly to screen
            If Not App.Visualizers.OscilloscopeEnableGlow Then
                Dim g = e.Graphics
                g.Clear(App.CurrentTheme.BackColor)

                Using penLeft As New Pen(App.CurrentTheme.TextColor, App.Visualizers.OscilloscopeLineWidth)
                    For i = 1 To localLeft.Length - 1
                        Dim x1 = (i - 1) * stepX
                        Dim y1 = midY - (localLeft(i - 1) * midY * 0.9F)
                        Dim x2 = i * stepX
                        Dim y2 = midY - (localLeft(i) * midY * 0.9F)
                        g.DrawLine(penLeft, x1, y1, x2, y2)
                    Next
                End Using

                If App.Visualizers.OscilloscopeChannelMode = App.VisualizerSettings.OscilloscopeChannelModes.StereoBoth AndAlso localRight IsNot Nothing Then
                    Using penRight As New Pen(App.CurrentTheme.TextColor, App.Visualizers.OscilloscopeLineWidth)
                        For i = 1 To localRight.Length - 1
                            Dim x1 = (i - 1) * stepX
                            Dim y1 = midY + (localRight(i - 1) * midY * 0.9F)
                            Dim x2 = i * stepX
                            Dim y2 = midY + (localRight(i) * midY * 0.9F)
                            g.DrawLine(penRight, x1, y1, x2, y2)
                        Next
                    End Using
                End If

                Exit Sub
            End If

            ' Ensure glow buffer matches control size
            If glowBuffer Is Nothing OrElse glowBuffer.Width <> Me.Width OrElse glowBuffer.Height <> Me.Height Then
                If glowBuffer IsNot Nothing Then glowBuffer.Dispose()
                If glowGraphics IsNot Nothing Then glowGraphics.Dispose()
                glowBuffer = New Bitmap(Me.Width, Me.Height)
                glowGraphics = Graphics.FromImage(glowBuffer)
                glowGraphics.Clear(App.CurrentTheme.BackColor)
            End If

            ' Fade previous frame
            Dim fadeBrush As New SolidBrush(Color.FromArgb(App.Visualizers.OscilloscopeFadeAlpha, App.CurrentTheme.BackColor))
            glowGraphics.FillRectangle(fadeBrush, Me.ClientRectangle)

            ' Draw left channel to glow buffer
            Using penLeft As New Pen(App.CurrentTheme.TextColor, App.Visualizers.OscilloscopeLineWidth)
                For i = 1 To localLeft.Length - 1
                    Dim x1 = (i - 1) * stepX
                    Dim y1 = midY - (localLeft(i - 1) * midY * 0.9F)
                    Dim x2 = i * stepX
                    Dim y2 = midY - (localLeft(i) * midY * 0.9F)
                    glowGraphics.DrawLine(penLeft, x1, y1, x2, y2)
                Next
            End Using

            ' Draw right channel if StereoBoth
            If App.Visualizers.OscilloscopeChannelMode = App.VisualizerSettings.OscilloscopeChannelModes.StereoBoth AndAlso localRight IsNot Nothing Then
                Using penRight As New Pen(App.CurrentTheme.TextColor, App.Visualizers.OscilloscopeLineWidth)
                    For i = 1 To localRight.Length - 1
                        Dim x1 = (i - 1) * stepX
                        Dim y1 = midY + (localRight(i - 1) * midY * 0.9F)
                        Dim x2 = i * stepX
                        Dim y2 = midY + (localRight(i) * midY * 0.9F)
                        glowGraphics.DrawLine(penRight, x1, y1, x2, y2)
                    Next
                End Using
            End If

            ' Blit glow buffer to screen
            e.Graphics.DrawImageUnscaled(glowBuffer, 0, 0)
        End Sub

        ' Methods
        Public Sub UpdateWave(samples() As Single)
            SyncLock Me
                Dim sampleCount = samples.Length \ 2

                ' Resize buffers if needed
                If waveform Is Nothing OrElse waveform.Length <> sampleCount Then
                    ReDim waveform(sampleCount - 1)
                    ReDim lastWaveform(sampleCount - 1)
                    ReDim waveformRight(sampleCount - 1)
                    ReDim lastWaveformRight(sampleCount - 1)
                End If

                For i = 0 To sampleCount - 1
                    Dim left = samples(i * 2)
                    Dim right = samples(i * 2 + 1)

                    Select Case App.Visualizers.OscilloscopeChannelMode
                        Case App.VisualizerSettings.OscilloscopeChannelModes.Mono
                            Dim mixed = (left + right) / 2.0F
                            waveform(i) = Smooth(i, mixed)
                        Case App.VisualizerSettings.OscilloscopeChannelModes.StereoLeft
                            waveform(i) = Smooth(i, left)
                        Case App.VisualizerSettings.OscilloscopeChannelModes.StereoRight
                            waveform(i) = Smooth(i, right)
                        Case App.VisualizerSettings.OscilloscopeChannelModes.StereoBoth
                            waveform(i) = Smooth(i, left)
                            waveformRight(i) = SmoothRight(i, right)
                    End Select
                Next
            End SyncLock
        End Sub
        Private Function Smooth(i As Integer, sample As Single) As Single
            Dim boosted = Math.Max(-1.0F, Math.Min(sample * App.Visualizers.OscilloscopeGain, 1.0F))
            Dim smoothed = (lastWaveform(i) * App.Visualizers.OscilloscopeSmoothing) + (boosted * (1.0F - App.Visualizers.OscilloscopeSmoothing))
            lastWaveform(i) = smoothed
            Return smoothed
        End Function
        Private Function SmoothRight(i As Integer, sample As Single) As Single
            Dim boosted = Math.Max(-1.0F, Math.Min(sample * App.Visualizers.OscilloscopeGain, 1.0F))
            Dim smoothed = (lastWaveformRight(i) * App.Visualizers.OscilloscopeSmoothing) + (boosted * (1.0F - App.Visualizers.OscilloscopeSmoothing))
            lastWaveformRight(i) = smoothed
            Return smoothed
        End Function

    End Class
    Private Class VisualizerFractalCloud
        Inherits UserControl
        Implements IVisualizer

        Private ReadOnly updateTimer As Timer
        Private audioData() As Single
        Private swirlAngle As Double = 0
        Private time As Double = 0
        Private smoothedLevel As Double = 0

        Public Overloads ReadOnly Property Name As String Implements IVisualizer.Name
            Get
                Return "Fractal Cloud"
            End Get
        End Property
        Public ReadOnly Property DockedControl As Control Implements IVisualizer.DockedControl
            Get
                Return Me
            End Get
        End Property

        Public Sub New()
            Me.DoubleBuffered = True
            updateTimer = New Timer With {.Interval = 16} '=60fps
            AddHandler updateTimer.Tick, AddressOf OnTick
        End Sub

        Public Sub Start() Implements IVisualizer.Start
            updateTimer.Start()
        End Sub
        Public Sub [Stop]() Implements IVisualizer.Stop
            updateTimer.Stop()
        End Sub
        Public Overloads Sub Update(data As Single()) Implements IVisualizer.Update
            audioData = data
        End Sub
        Public Overloads Sub UpdateWaveform(samples As Single()) Implements IVisualizer.UpdateWaveform
            ' Not Implemented
        End Sub
        Public Shadows Sub Resize(width As Integer, height As Integer) Implements IVisualizer.Resize
        End Sub

        Private Sub OnTick(sender As Object, e As EventArgs)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            MyBase.OnPaint(pe)
            Dim g = pe.Graphics
            Dim cx = Me.Width / 2
            Dim cy = Me.Height / 2

            'Audio reactivity
            Dim level As Double = 0
            Dim bass As Double = 0
            Dim treble As Double = 0
            If audioData IsNot Nothing AndAlso audioData.Length > 0 Then
                'RMS for smoother energy
                level = Math.Sqrt(audioData.Average(Function(s) s * s))
                bass = audioData.Take(audioData.Length \ 4).Average(Function(s) Math.Abs(s))
                treble = audioData.Skip(audioData.Length \ 2).Average(Function(s) Math.Abs(s))
            End If

            'Smooth the level
            smoothedLevel = smoothedLevel * 0.9 + level * 0.1

            'Swirl motion: steady baseline + audio modulation
            swirlAngle += App.Visualizers.FractalCloudSwirlSpeedBase + smoothedLevel * App.Visualizers.FractalCloudSwirlSpeedAudioFactor
            time += App.Visualizers.FractalCloudTimeIncrement ' fixed baseline, not audio-driven

            'Soft reset to avoid runaway values
            If swirlAngle > 100000 Then swirlAngle = 0
            If time > 100000 Then time = 0

            'Render to smaller buffer for speed
            Dim renderW = Me.Width \ 3
            Dim renderH = Me.Height \ 3
            Using bmp As New Bitmap(renderW, renderH, Imaging.PixelFormat.Format32bppArgb)
                Dim rect = New Rectangle(0, 0, bmp.Width, bmp.Height)
                Dim bmpData = bmp.LockBits(rect, Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat)
                Dim stride = bmpData.Stride
                Dim ptr = bmpData.Scan0
                Dim bytes = stride * bmp.Height
                Dim rgbValues(bytes - 1) As Byte

                'Fill pixel buffer
                For y = 0 To bmp.Height - 1
                    For x = 0 To bmp.Width - 1
                        Dim fullX = x * Me.Width / renderW
                        Dim fullY = y * Me.Height / renderH
                        Dim dx = fullX - cx
                        Dim dy = fullY - cy

                        ' Swirl transform
                        Dim angle = Math.Atan2(dy, dx) + swirlAngle
                        Dim radius = Math.Sqrt(dx * dx + dy * dy)

                        ' Stable sine fields (no audio modulation here)
                        Dim v1 = Math.Sin(radius * 0.02 + time)
                        Dim v2 = Math.Sin(angle * 3 + time * 0.5)
                        Dim v3 = Math.Sin((radius * 0.05 + angle * 2) + time * 0.3)

                        Dim v = v1 + v2 + v3
                        Dim n = (v + 3) / 6.0 ' normalize to 0–1

                        'Choose palette mode (could be an Enum or just a string)
                        Dim paletteMode As String = "Firestorm" 'Normal, "Firestorm", "Aurora", "CosmicRainbow"
                        Dim hue As Double
                        Dim sat As Double = 1.0
                        Dim val As Double = n ' default brightness
                        Dim col As Color
                        Select Case App.Visualizers.FractalCloudPalette
                            Case App.VisualizerSettings.FractalCloudPalettes.Normal
                                hue = (n * 360 + bass * 400 + treble * 200) Mod 360
                                col = ColorFromHSV(hue, sat, val)
                            Case App.VisualizerSettings.FractalCloudPalettes.Firestorm
                                hue = 0 + (n * 60) ' restrict to red–yellow range
                                col = ColorFromHSV(hue, sat, val)
                            Case App.VisualizerSettings.FractalCloudPalettes.Aurora
                                hue = 120 + (n * 120) ' restrict to green–cyan range
                                col = ColorFromHSV(hue, sat, val)
                            Case App.VisualizerSettings.FractalCloudPalettes.CosmicRainbow
                                hue = (n * 360 + bass * 400 + treble * 200) Mod 360
                                sat = Math.Min(1.0, 0.5 + bass * 5) ' bass drives vividness
                                val = Math.Min(1.0, n * (1.0 - treble * 0.5)) ' treble softens brightness
                                col = ColorFromHSV(hue, sat, val)
                            Case Else
                                hue = (n * 360 + bass * 400 + treble * 200) Mod 360
                                col = ColorFromHSV(hue, sat, val)
                        End Select

                        'Write pixel into buffer (BGRA order)
                        Dim offset = y * stride + x * 4
                        rgbValues(offset) = col.B
                        rgbValues(offset + 1) = col.G
                        rgbValues(offset + 2) = col.R
                        rgbValues(offset + 3) = col.A
                    Next
                Next

                'Copy buffer back to bitmap
                Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes)
                bmp.UnlockBits(bmpData)

                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                g.DrawImage(bmp, 0, 0, Me.Width, Me.Height)
            End Using
        End Sub

        Private Function ColorFromHSV(hue As Double, saturation As Double, value As Double) As Color
            Dim hi As Integer = CInt(Math.Floor(hue / 60)) Mod 6
            Dim f As Double = hue / 60 - Math.Floor(hue / 60)

            Dim v As Double = value * 255
            Dim p As Double = v * (1 - saturation)
            Dim q As Double = v * (1 - f * saturation)
            Dim t As Double = v * (1 - (1 - f) * saturation)

            Select Case hi
                Case 0 : Return Color.FromArgb(255, CInt(v), CInt(t), CInt(p))
                Case 1 : Return Color.FromArgb(255, CInt(q), CInt(v), CInt(p))
                Case 2 : Return Color.FromArgb(255, CInt(p), CInt(v), CInt(t))
                Case 3 : Return Color.FromArgb(255, CInt(p), CInt(q), CInt(v))
                Case 4 : Return Color.FromArgb(255, CInt(t), CInt(p), CInt(v))
                Case Else : Return Color.FromArgb(255, CInt(v), CInt(p), CInt(q))
            End Select
        End Function

    End Class
    Private Class VisualizerFractalJulia
        Inherits UserControl
        Implements IVisualizer

        Private ReadOnly updateTimer As Timer
        Private audioData() As Single

        ' Constructor
        Public Sub New()
            DoubleBuffered = True
            updateTimer = New Timer With {.Interval = 33} ' ~30 FPS
            AddHandler updateTimer.Tick, AddressOf OnTick
        End Sub

        ' IVisualizer Implementation
        Public Overloads ReadOnly Property Name As String Implements IVisualizer.Name
            Get
                Return "Julia Fractal"
            End Get
        End Property
        Public ReadOnly Property DockedControl As Control Implements IVisualizer.DockedControl
            Get
                Return Me
            End Get
        End Property
        Public Sub Start() Implements IVisualizer.Start
            updateTimer.Start()
        End Sub
        Public Sub [Stop]() Implements IVisualizer.Stop
            updateTimer.Stop()
        End Sub
        Public Overloads Sub Update(data As Single()) Implements IVisualizer.Update
            audioData = data
        End Sub
        Public Overloads Sub UpdateWaveform(samples As Single()) Implements IVisualizer.UpdateWaveform
            ' Not Implemented
        End Sub
        Public Shadows Sub Resize(width As Integer, height As Integer) Implements IVisualizer.Resize
            ' Not Implemented
        End Sub

        ' Handlers
        Private Sub OnTick(sender As Object, e As EventArgs)
            Invalidate()
        End Sub
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            ' Bail if no audio
            If audioData Is Nothing OrElse audioData.Length = 0 Then
                e.Graphics.Clear(App.CurrentTheme.BackColor)
                Return
            End If

            ' Target resolution: 1/3 of 1920x1080
            Dim targetW As Integer = 640
            Dim targetH As Integer = 360

            ' Create reduced-resolution bitmap
            Dim bmp As New Bitmap(targetW, targetH, Imaging.PixelFormat.Format32bppArgb)

            ' Lock bits for fast pixel access
            Dim rect As New Rectangle(0, 0, bmp.Width, bmp.Height)
            Dim bmpData As Imaging.BitmapData = bmp.LockBits(rect, Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat)

            Dim stride As Integer = bmpData.Stride
            Dim ptr As IntPtr = bmpData.Scan0
            Dim bytes As Integer = stride * bmp.Height
            Dim rgbValues(bytes - 1) As Byte

            ' Audio bands
            Dim bass As Double = If(audioData.Length > 2, audioData(2), 0)
            Dim mid As Double = If(audioData.Length > 10, audioData(10), 0)
            Dim treble As Double = If(audioData.Length > 30, audioData(30), 0)

            ' Julia constant evolves with audio
            Dim cx As Double = App.Visualizers.JuliaFractalBaseCX + bass * App.Visualizers.JuliaFractalBassInfluence
            Dim cy As Double = App.Visualizers.JuliaFractalBaseCY + mid * App.Visualizers.JuliaFractalMidInfluence

            ' Fill pixel buffer
            For py As Integer = 0 To targetH - 1
                Dim rowOffset As Integer = py * stride
                For px As Integer = 0 To targetW - 1
                    Dim x As Double = 1.5 * (px - targetW / 2) / (0.5 * targetW)
                    Dim y As Double = (py - targetH / 2) / (0.5 * targetH)

                    Dim zx As Double = x
                    Dim zy As Double = y
                    Dim iter As Integer = 0

                    While zx * zx + zy * zy < 4 AndAlso iter < App.Visualizers.JuliaFractalMaxIterations
                        Dim tmp As Double = zx * zx - zy * zy + cx
                        zy = 2.0 * zx * zy + cy
                        zx = tmp
                        iter += 1
                    End While

                    ' Color mapping
                    Dim r As Byte = CByte((iter * 9 + CInt(treble * 255)) Mod 255)
                    Dim gCol As Byte = CByte((iter * 7 + CInt(treble * 128)) Mod 255)
                    Dim b As Byte = CByte((iter * 5 + CInt(treble * 64)) Mod 255)

                    Dim idx As Integer = rowOffset + (px * 4)
                    rgbValues(idx) = b
                    rgbValues(idx + 1) = gCol
                    rgbValues(idx + 2) = r
                    rgbValues(idx + 3) = 255 ' alpha
                Next
            Next

            ' Copy buffer back to bitmap
            Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes)
            bmp.UnlockBits(bmpData)

            ' Draw scaled to full control size
            e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            e.Graphics.DrawImage(bmp, New Rectangle(0, 0, ClientSize.Width, ClientSize.Height))
        End Sub

    End Class
    Friend Class VisualizerHyperspaceTunnel
        Inherits UserControl
        Implements IVisualizer

        Private ReadOnly updateTimer As Timer
        Private audioData() As Single
        Private ReadOnly particles As List(Of Particle)
        Private ReadOnly rnd As New Random()
        Private swirlAngle As Double = 0.0

        Public Overloads ReadOnly Property Name As String Implements IVisualizer.Name
            Get
                Return "Hyperspace Tunnel"
            End Get
        End Property
        Public ReadOnly Property DockedControl As Control Implements IVisualizer.DockedControl
            Get
                Return Me
            End Get
        End Property

        Public Sub New()
            Me.DoubleBuffered = True
            updateTimer = New Timer With {.Interval = 33}
            AddHandler updateTimer.Tick, AddressOf OnTick
            particles = New List(Of Particle)()
            'Initialize particles
            SetParticleCount(App.Visualizers.HyperspaceTunnelParticleCount)
        End Sub

        Public Sub Start() Implements IVisualizer.Start
            updateTimer.Start()
        End Sub
        Public Sub [Stop]() Implements IVisualizer.Stop
            updateTimer.Stop()
        End Sub
        Public Overloads Sub Update(data As Single()) Implements IVisualizer.Update
            audioData = data
        End Sub
        Public Overloads Sub UpdateWaveform(samples As Single()) Implements IVisualizer.UpdateWaveform
            ' Not Implemented
        End Sub
        Public Shadows Sub Resize(width As Integer, height As Integer) Implements IVisualizer.Resize
        End Sub

        Private Sub OnTick(sender As Object, e As EventArgs)
            Invalidate()
        End Sub
        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            MyBase.OnPaint(pe)
            Dim g = pe.Graphics
            Dim cx = Width / 2
            Dim cy = Height / 2

            ' Audio level for reactivity
            Dim level As Double = 0
            If audioData IsNot Nothing AndAlso audioData.Length > 0 Then
                level = Math.Sqrt(audioData.Average(Function(s) s * s)) ' RMS
                level = Math.Min(level * 10, 1.0) ' Scale and clamp here
            End If

            ' Swirl angle increment based on audio
            swirlAngle += App.Visualizers.HyperspaceTunnelSwirlSpeedBase + (level * App.Visualizers.HyperspaceTunnelSwirlSpeedAudioFactor)
            Dim cosA As Double = Math.Cos(swirlAngle)
            Dim sinA As Double = Math.Sin(swirlAngle)

            For Each p In particles
                ' Speed toward viewer based on audio
                Dim speed = App.Visualizers.HyperspaceTunnelParticleSpeedBase + level * App.Visualizers.HyperspaceTunnelParticleSpeedAudioFactor
                p.Z -= speed

                ' Rotate around Z axis for spiral effect
                Dim rx As Double = p.X * cosA - p.Y * sinA
                Dim ry As Double = p.X * sinA + p.Y * cosA

                ' Perspective projection
                Dim scale = (Width / 2) / p.Z
                Dim x = cx + rx * scale
                Dim y = cy + ry * scale

                ' Previous position (slightly further back in Z) for streak
                Dim prevZ = p.Z + speed * 2
                Dim prevScale = (Width / 2) / prevZ
                Dim px = cx + rx * prevScale
                Dim py = cy + ry * prevScale

                ' Color cycling by depth
                Dim color = p.Color

                ' Only draw if both ends are inside the control bounds
                If x >= 0 AndAlso x <= Width AndAlso y >= 0 AndAlso y <= Height AndAlso px >= 0 AndAlso px <= Width AndAlso py >= 0 AndAlso py <= Height Then
                    Using pen As New Pen(Color.FromArgb(200, color), 2)
                        g.DrawLine(pen, CSng(px), CSng(py), CSng(x), CSng(y))
                    End Using
                End If

                ' Reset particle when it passes viewer
                If p.Z < 1 Then
                    p.X = (rnd.NextDouble() * 2 - 1) * 5
                    p.Y = (rnd.NextDouble() * 2 - 1) * 5
                    p.Z = rnd.Next(300, 2000)
                End If
            Next
        End Sub
        Private Shared Function ColorFromHSV(hue As Double, saturation As Double, value As Double) As Color
            Dim hi As Integer = CInt(Math.Floor(hue / 60)) Mod 6
            Dim f As Double = hue / 60 - Math.Floor(hue / 60)

            Dim v As Double = value * 255
            Dim p As Double = v * (1 - saturation)
            Dim q As Double = v * (1 - f * saturation)
            Dim t As Double = v * (1 - (1 - f) * saturation)

            Select Case hi
                Case 0 : Return Color.FromArgb(255, CInt(v), CInt(t), CInt(p))
                Case 1 : Return Color.FromArgb(255, CInt(q), CInt(v), CInt(p))
                Case 2 : Return Color.FromArgb(255, CInt(p), CInt(v), CInt(t))
                Case 3 : Return Color.FromArgb(255, CInt(p), CInt(q), CInt(v))
                Case 4 : Return Color.FromArgb(255, CInt(t), CInt(p), CInt(v))
                Case Else : Return Color.FromArgb(255, CInt(v), CInt(p), CInt(q))
            End Select
        End Function
        Public Sub SetParticleCount(count As Integer)
            particles.Clear()
            For i = 0 To count - 1
                particles.Add(New Particle(rnd))
            Next
        End Sub

        Private Class Particle
            Public X As Double
            Public Y As Double
            Public Z As Double
            Public Color As Color

            Public Sub New(rnd As Random)
                X = (rnd.NextDouble() * 2 - 1) * 5
                Y = (rnd.NextDouble() * 2 - 1) * 5
                Z = rnd.Next(300, 2000) ' start far away
                Dim hue = rnd.Next(0, 360)
                Color = ColorFromHSV(hue, 1, 1)
            End Sub

            Public Sub Update()
                Z -= 5 ' speed toward viewer
            End Sub
            Public Sub Reset(rnd As Random)
                X = (rnd.NextDouble() * 2 - 1) * 5
                Y = (rnd.NextDouble() * 2 - 1) * 5
                Z = rnd.Next(300, 2000) ' always recycle far away
                Dim hue = rnd.Next(0, 360)
                Color = ColorFromHSV(hue, 1, 1)
            End Sub

        End Class

    End Class
    Private Class VisualizerStarField
        Inherits UserControl
        Implements IVisualizer

        ' Declarations
        Private ReadOnly updateTimer As Timer
        Private audioData() As Single
        Private ReadOnly rnd As New Random()
        Private ReadOnly stars As New List(Of Star)
        Private Structure Star ' Star structure
            Public X As Double
            Public Y As Double
            Public Z As Double
            Public Size As Integer
            Public Color As Color
        End Structure

        ' Constructor
        Public Sub New()
            DoubleBuffered = True
            updateTimer = New Timer With {.Interval = 33} ' ~30 FPS
            AddHandler updateTimer.Tick, AddressOf OnTick
        End Sub

        ' IVisualizer Implementation
        Public Overloads ReadOnly Property Name As String Implements IVisualizer.Name
            Get
                Return "Star Field"
            End Get
        End Property
        Public ReadOnly Property DockedControl As Control Implements IVisualizer.DockedControl
            Get
                Return Me
            End Get
        End Property
        Public Sub Start() Implements IVisualizer.Start
            updateTimer.Start()
        End Sub
        Public Sub [Stop]() Implements IVisualizer.Stop
            updateTimer.Stop()
        End Sub
        Public Overloads Sub Update(data As Single()) Implements IVisualizer.Update
            audioData = data
        End Sub
        Public Overloads Sub UpdateWaveform(samples As Single()) Implements IVisualizer.UpdateWaveform
            ' Not Implemented
        End Sub
        Public Shadows Sub Resize(width As Integer, height As Integer) Implements IVisualizer.Resize
        End Sub

        ' Handlers
        Private Sub OnTick(sender As Object, e As EventArgs)
            Invalidate()
        End Sub
        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            BackColor = Color.Black
            MyBase.OnPaint(pe)
            Dim g = pe.Graphics
            Dim cx = Width / 2
            Dim cy = Height / 2

            ' Audio level for reactivity
            Dim level As Double = 0
            If audioData IsNot Nothing AndAlso audioData.Length > 0 Then
                level = Math.Sqrt(audioData.Average(Function(s) s * s)) * 10
            End If

            ' Initialize stars if needed
            If stars.Count = 0 Then SetStarCount(App.Visualizers.StarFieldStarCount)

            ' Update and draw stars
            For i = 0 To stars.Count - 1
                Dim s = stars(i)
                ' Move star forward based on audio
                'settings here for base speed + audio level influence
                Dim speed = App.Visualizers.StarFieldBaseSpeed + level * App.Visualizers.StarFieldAudioSpeedFactor
                s.Z -= speed

                ' Reset if too close
                If s.Z < 1 Then
                    s.X = (rnd.NextDouble() * 2 - 1) * Width / 2
                    s.Y = (rnd.NextDouble() * 2 - 1) * Height / 2
                    s.Z = rnd.Next(500, 1000)
                    s.Size = rnd.Next(1, 4)
                End If

                ' Perspective projection
                Dim scale = 500 / s.Z
                Dim x = cx + s.X * scale
                Dim y = cy + s.Y * scale
                Dim size = Math.Min(s.Size * scale, App.Visualizers.StarFieldMaxStarSize)

                ' Only draw if visible
                If x >= 0 AndAlso x <= Width AndAlso y >= 0 AndAlso y <= Height Then
                    Using brush As New SolidBrush(s.Color)
                        g.FillEllipse(brush, CSng(x), CSng(y), CSng(size), CSng(size))
                    End Using
                End If

                stars(i) = s
            Next
        End Sub

        ' Methods
        Public Sub SetStarCount(count As Integer)
            ' Clamp to safe range
            count = Math.Max(100, Math.Min(2000, count))

            ' Rebuild star list
            stars.Clear()
            For i = 0 To count - 1
                stars.Add(New Star With {
            .X = (rnd.NextDouble() * 2 - 1) * Width / 2,
            .Y = (rnd.NextDouble() * 2 - 1) * Height / 2,
            .Z = rnd.Next(500, 1000),
            .Size = rnd.Next(1, 4),
            .Color = Color.White})
            Next

        End Sub

    End Class
    Private Class VisualizerParticleNebula
        Inherits UserControl
        Implements IVisualizer

        Private ReadOnly updateTimer As Timer
        Private audioData() As Single
        Private ReadOnly particles As New List(Of Particle)
        Private ReadOnly rand As New Random()
        Private oldCenter As PointF

        Private Class Particle
            Public Property Position As PointF
            Public Property Velocity As PointF
            Public Property AngularVelocity As Single
            Public Property Size As Single
            Public Property Color As Color
            Public Property Life As Single ' 0–1, fades out
            Public Property Trail As New Queue(Of PointF)

            Public Sub Update()
                ' Apply angular drift
                Dim cosA As Single = CSng(Math.Cos(AngularVelocity))
                Dim sinA As Single = CSng(Math.Sin(AngularVelocity))

                Dim vx As Single = Velocity.X * cosA - Velocity.Y * sinA
                Dim vy As Single = Velocity.X * sinA + Velocity.Y * cosA
                Velocity = New PointF(vx, vy)

                ' Move particle
                Position = New PointF(Position.X + Velocity.X, Position.Y + Velocity.Y)

                ' Record trail
                Trail.Enqueue(Position)
                If Trail.Count > 10 Then
                    Trail.Dequeue()
                End If

                ' Fade life
                Life -= App.Visualizers.ParticleNebulaFadeRate
                If Life < 0 Then Life = 0
            End Sub

        End Class

        ' Constructor
        Public Sub New()
            DoubleBuffered = True
            updateTimer = New Timer With {.Interval = 33} ' ~30 FPS
            AddHandler updateTimer.Tick, AddressOf OnTick
        End Sub

        ' IVisualizer Implementation
        Public Overloads ReadOnly Property Name As String Implements IVisualizer.Name
            Get
                Return "Particle Nebula"
            End Get
        End Property
        Public ReadOnly Property DockedControl As Control Implements IVisualizer.DockedControl
            Get
                Return Me
            End Get
        End Property
        Public Sub Start() Implements IVisualizer.Start
            oldCenter = New PointF(CSng(Width / 2), CSng(Height / 2))
            updateTimer.Start()
        End Sub
        Public Sub [Stop]() Implements IVisualizer.Stop
            updateTimer.Stop()
        End Sub
        Public Overloads Sub Update(data As Single()) Implements IVisualizer.Update

            ' Apply logarithmic scaling to FFT magnitudes
            Dim scaled(data.Length - 1) As Single
            For i As Integer = 0 To data.Length - 1
                ' Avoid log(0) by adding 1
                scaled(i) = CSng(Math.Log(1 + data(i)) / Math.Log(2))
            Next

            audioData = scaled
        End Sub
        Public Overloads Sub UpdateWaveform(samples As Single()) Implements IVisualizer.UpdateWaveform
            ' Not Implemented
        End Sub
        Public Shadows Sub Resize(width As Integer, height As Integer) Implements IVisualizer.Resize
            Dim newCenter As New PointF(CSng(width / 2), CSng(height / 2))
            Dim dx As Single = newCenter.X - oldCenter.X
            Dim dy As Single = newCenter.Y - oldCenter.Y

            For Each p In particles
                p.Position = New PointF(p.Position.X + dx, p.Position.Y + dy)
            Next

            oldCenter = newCenter
        End Sub

        ' Handlers
        Private Sub OnTick(sender As Object, e As EventArgs)
            Invalidate()
        End Sub
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            ' Spawn new particles
            SpawnParticles()

            ' Update + draw particles
            For i As Integer = particles.Count - 1 To 0 Step -1
                Dim p = particles(i)
                p.Update()

                If p.Life <= 0 Then
                    particles.RemoveAt(i)
                Else
                    Dim alpha As Integer = CInt(p.Life * 255)
                    Using brush As New SolidBrush(Color.FromArgb(alpha, p.Color))
                        e.Graphics.FillEllipse(brush,
                                       p.Position.X - p.Size / 2,
                                       p.Position.Y - p.Size / 2,
                                       p.Size, p.Size)
                    End Using
                End If

                If App.Visualizers.ParticleNebulaShowBloom Then
                    For r As Integer = 1 To App.Visualizers.ParticleNebulaBloomRadius
                        ' Fade alpha outward
                        Dim rawAlpha As Single = p.Life * 255 * App.Visualizers.ParticleNebulaBloomIntensity / r
                        Dim alphaBloom As Integer = Math.Min(255, Math.Max(0, CInt(rawAlpha)))
                        Dim sizeBloom As Single = p.Size + (r * 4) ' expand halo outward
                        Using bloomBrush As New SolidBrush(Color.FromArgb(alphaBloom, p.Color))
                            e.Graphics.FillEllipse(bloomBrush, p.Position.X - sizeBloom / 2, p.Position.Y - sizeBloom / 2, sizeBloom, sizeBloom)
                        End Using
                    Next
                End If

                If App.Visualizers.ParticleNebulaShowTrails AndAlso p.Trail.Count > 1 Then
                    Dim trailPoints() As PointF = p.Trail.ToArray()

                    If App.Visualizers.ParticleNebulaFadeTrails Then
                        ' Gradient fade along trail
                        For i2 As Integer = 1 To trailPoints.Length - 1
                            Dim fadeFactor As Double = i2 / trailPoints.Length
                            Dim alphaTrail As Integer = CInt(p.Life * 255 * fadeFactor * App.Visualizers.ParticleNebulaTrailAlpha)

                            Using segPen As New Pen(Color.FromArgb(alphaTrail, p.Color), p.Size / 2)
                                e.Graphics.DrawLine(segPen, trailPoints(i2 - 1), trailPoints(i2))
                            End Using
                        Next
                    Else
                        ' Solid streak with one alpha
                        Dim alphaTrail As Integer = CInt(p.Life * 255 * App.Visualizers.ParticleNebulaTrailAlpha)
                        Using trailPen As New Pen(Color.FromArgb(alphaTrail, p.Color), p.Size / 2)
                            e.Graphics.DrawLines(trailPen, trailPoints)
                        End Using
                    End If
                End If

            Next
        End Sub

        ' Methods
        Private Sub SpawnParticles()
            If audioData Is Nothing OrElse audioData.Length < 2 Then Exit Sub

            Dim barCount As Integer = audioData.Length \ 2
            Dim sampleRate As Integer = 44100

            For i As Integer = 0 To barCount - 1
                Dim magnitude As Single = audioData(i)
                Dim freq As Double = (i * sampleRate) / (2.0 * barCount)
                Dim maxFreq As Double = sampleRate / 2.0

                ' Spawn probability based on magnitude
                If rand.NextDouble() < magnitude * App.Visualizers.ParticleNebulaSpawnMultiplier Then
                    Dim angle As Double = rand.NextDouble() * 2 * Math.PI
                    Dim speed As Single = Math.Max(1.0F, magnitude * App.Visualizers.ParticleNebulaVelocityScale)
                    Dim p As New Particle With {
                        .Position = New PointF(CSng(Width / 2), CSng(Height / 2)),
                        .Velocity = New PointF(CSng(Math.Cos(angle) * speed), CSng(Math.Sin(angle) * speed)),
                        .AngularVelocity = CSng(((rand.NextDouble() - 0.5) + App.Visualizers.ParticleNebulaSwirlBias) * App.Visualizers.ParticleNebulaSwirlStrength),
                        .Size = CSng(2 + magnitude * App.Visualizers.ParticleNebulaSizeScale),
                        .Color = GetColorForFrequency(freq, maxFreq),
                        .Life = 1.0F}
                    particles.Add(p)
                End If
            Next
        End Sub
        Private Function GetColorForFrequency(freq As Double, maxFreq As Double) As Color
            If App.Visualizers.ParticleNebulaRainbowColors Then
                ' Map frequency to hue (0–360)
                Dim hue As Double = (freq / maxFreq) * 360.0
                Return ColorFromHSV(hue, 1.0, 1.0)
            Else
                ' Default cosmic gradient
                If freq < 200 Then
                    Return App.Visualizers.ParticleNebulaActivePalette.BassColor
                ElseIf freq < 2000 Then
                    Return App.Visualizers.ParticleNebulaActivePalette.MidColor
                Else
                    Return App.Visualizers.ParticleNebulaActivePalette.TrebleColor
                End If
            End If
        End Function
        Private Function ColorFromHSV(hue As Double, saturation As Double, value As Double) As Color
            Dim hi As Integer = CInt(Math.Floor(hue / 60)) Mod 6
            Dim f As Double = hue / 60 - Math.Floor(hue / 60)

            Dim v As Integer = CInt(value * 255)
            Dim p As Integer = CInt(v * (1 - saturation))
            Dim q As Integer = CInt(v * (1 - f * saturation))
            Dim t As Integer = CInt(v * (1 - (1 - f) * saturation))

            Select Case hi
                Case 0 : Return Color.FromArgb(255, v, t, p)
                Case 1 : Return Color.FromArgb(255, q, v, p)
                Case 2 : Return Color.FromArgb(255, p, v, t)
                Case 3 : Return Color.FromArgb(255, p, q, v)
                Case 4 : Return Color.FromArgb(255, t, p, v)
                Case Else : Return Color.FromArgb(255, v, p, q)
            End Select
        End Function

    End Class

    'Form Events                    
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_HOTKEY
                    'Debug.Print("HOTKEY " + m.WParam.ToString + " PRESSED")
                    App.PerformHotKeyAction(m.WParam.ToInt32)
                Case WinAPI.WM_SYSCOMMAND
                    If m.WParam.ToInt32() = WinAPI.SC_MINIMIZE Then
                        If CMPlaylist IsNot Nothing AndAlso CMPlaylist.Visible Then
                            WinAPI.PostMessage(CMPlaylist.Handle, WinAPI.WM_CLOSE, IntPtr.Zero, IntPtr.Zero) 'Closes the Playlist Context Menu on minimize so it doesn't linger upon restore.
                            'If TipPlaylist IsNot Nothing Then TipPlaylist.HideTooltip()
                        End If
                    End If
                Case Skye.WinAPI.WM_ACTIVATE
                    Select Case m.WParam.ToInt32
                        Case 0
                            IsFocused = False
                            'Debug.Print("Player Lost Focus")
                            SetInactiveTitleBarColor()
                        Case 1, 2
                            IsFocused = True
                            'Debug.Print("Player Got Focus")
                            SetActiveTitleBarColor()
                    End Select
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
                Case Skye.WinAPI.WM_SIZE
                    If (m.WParam.ToInt32 = 0 Or m.WParam.ToInt32 = 2) AndAlso Lyrics Then ShowMedia() 'called on restore or maximized to resize the Lyrics text.
                Case Skye.WinAPI.WM_GET_CUSTOM_DATA 'sends data upon request to SkyeVolume
                    Select Case PlayState
                        Case PlayStates.Playing
                            m.Result = New IntPtr(2)
                        Case PlayStates.Paused
                            m.Result = New IntPtr(1)
                        Case PlayStates.Stopped
                            m.Result = New IntPtr(0)
                        Case Else
                            m.Result = New IntPtr(9999)
                    End Select
            End Select
        Catch ex As Exception
            App.WriteToLog("Player WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            If m.Msg <> Skye.WinAPI.WM_GET_CUSTOM_DATA Then MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub Player_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Player Engine
        _player = New VLCPlayer(Me)
        VLCViewer.MediaPlayer = CType(_player, VLCPlayer).MediaPlayer
        AddHandler _player.PlaybackStarted, AddressOf OnPlaybackStarted
        AddHandler _player.PlaybackEnded, AddressOf OnPlaybackEnded
        _player.Volume = 100

        ' For Meters
        MeterAudioCapture = New WasapiLoopbackCapture()
        AddHandler MeterAudioCapture.DataAvailable, AddressOf OnMeterDataAvailable
        MeterAudioCapture.StartRecording()

        ' For Visualizers
        VisualizerHost = New VisualizerHostClass(Me, PanelVisualizer)
        VisualizerHost.Register(New VisualizerRainbowBar)
        VisualizerHost.Register(New VisualizerClassicSpectrumAnalyzer)
        VisualizerHost.Register(New VisualizerCircularSpectrum)
        VisualizerHost.Register(New VisualizerWaveform)
        VisualizerHost.Register(New VisualizerOscilloscope)
        VisualizerHost.Register(New VisualizerFractalCloud)
        VisualizerHost.Register(New VisualizerFractalJulia)
        VisualizerHost.Register(New VisualizerHyperspaceTunnel)
        VisualizerHost.Register(New VisualizerStarField)
        VisualizerHost.Register(New VisualizerParticleNebula)
        VisualizerHost.SetVisualizersMenu()
        VisualizerEngine = New VisualizerAudioEngine(VisualizerHost)

        App.SetNIApp()

        ' Initialize Form
        Text = Application.Info.Title 'Set the form title
        PlaylistSearchTitle = TxtBoxPlaylistSearch.Text 'Default search title
        PlaylistBoldFont = New Font(LVPlaylist.Font, FontStyle.Bold) 'Bold font for playlist titles
        TrackBarPosition.Size = New Size(TrackBarPosition.Size.Width, 26)

        ' Initialize Listview
        Dim header As ColumnHeader
        header = New ColumnHeader With {
            .Name = "Title",
            .Text = "Title",
            .Width = 300}
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader With {
            .Name = "Path",
            .Text = "Path",
            .Width = 550}
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader With {
            .Name = "Rating",
            .Text = "Rating",
            .Width = 70,
            .TextAlign = HorizontalAlignment.Center}
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader With {
            .Name = "PlayCount",
            .Text = "Plays",
            .Width = 60,
            .TextAlign = HorizontalAlignment.Center}
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader With {
            .Name = "LastPlayed",
            .Text = "Last Played",
            .Width = 180}
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader With {
            .Name = "FirstPlayed",
            .Text = "First Played",
            .Width = 180}
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader With {
            .Name = "Added",
            .Text = "Added",
            .Width = 180}
        LVPlaylist.Columns.Add(header)
        header = Nothing

        ' More Form Initialization
        SetAccentColor()
        SetTheme()
        App.ThemeMenu(MenuPlayer)
        App.ThemeMenu(CMPlaylist)
        App.ThemeMenu(CMRatings)
        LoadPlaylist()
        ClearPlaylistTitles()
        ShowPlayMode()

        ' Set ToolTips
        TipPlayer.SetToolTip(BtnPlay, "Play / Pause")
        TipPlayer.SetToolTip(BtnStop, "Stop Playing")
        TipPlayer.SetToolTip(BtnReverse, "Skip Backward")
        TipPlayer.SetToolTip(BtnForward, "Skip Forward")
        TipPlayer.SetToolTip(BtnMute, "Mute")
        TipPlayer.SetToolTip(LblDuration, "Song Duration")
        SetTipPlayer()
        CustomDrawCMToolTip(CMPlaylist)

        ' Place the window where it was last time
#If DEBUG Then
        'If App.SaveWindowMetrics AndAlso App.PlayerLocation.Y >= 0 Then Me.Location = App.PlayerLocation
        'If App.SaveWindowMetrics AndAlso App.PlayerSize.Height >= 0 Then Me.Size = App.PlayerSize
#Else
        If App.SaveWindowMetrics AndAlso App.PlayerLocation.Y >= 0 Then Me.Location = App.PlayerLocation
        If App.SaveWindowMetrics AndAlso App.PlayerSize.Height >= 0 Then Me.Size = App.PlayerSize
#End If

        ' Disable Mouse Wheel support for TrackBar
        AddHandler TrackBarPosition.MouseWheel, AddressOf TrackBarPosition_MouseWheel

    End Sub
    Private Sub Player_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        TopMost = False
        If App.GetSimpleVersion() <> ChangeLogLastVersionShown Then
            ChangeLogLastVersionShown = App.GetSimpleVersion()
            SaveOptions()
            Me.BeginInvoke(Sub()
                               With New ChangeLog
                                   .TopMost = True
                                   .Show()
                                   .Activate()
                                   .TopMost = False
                                   .Visible = False
                                   .ShowDialog()
                               End With
                           End Sub)
        End If
    End Sub
    Private Sub Player_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown, BtnReverse.KeyDown, BtnPlay.KeyDown, BtnForward.KeyDown, TrackBarPosition.KeyDown, BtnStop.KeyDown, BtnNext.KeyDown, BtnPrevious.KeyDown
        If Not TxtBoxPlaylistSearch.Focused Then
            If e.Alt Then
            ElseIf e.Control Then
                Select Case e.KeyCode
                    Case Keys.D
                        If e.Shift Then App.ShowDevTools()
                    Case Keys.Space
                        StopPlay()
                        e.SuppressKeyPress = True
                End Select
            ElseIf e.Shift Then
            Else
                Select Case e.KeyCode
                    Case Keys.Enter
                    Case Keys.Escape
                        If WindowState = FormWindowState.Maximized Then WindowState = FormWindowState.Normal
                    Case Keys.End
                    Case Keys.Up
                    Case Keys.Left
                        UpdatePosition(False, 10)
                        e.SuppressKeyPress = True
                    Case Keys.Right
                        UpdatePosition(True, 10)
                        e.SuppressKeyPress = True
                    Case Keys.Space
                        TogglePlay()
                        e.SuppressKeyPress = True
                    Case Keys.OemQuestion
                    Case Keys.PageUp
                    Case Keys.PageDown
                    Case Keys.Home
                    Case Keys.Delete
                    Case Keys.Insert
                    Case Keys.F, Keys.F11 'Fullscreen
                        FullScreen = Not FullScreen
                        e.SuppressKeyPress = True
                    Case Keys.M
                        ToggleMute()
                        e.SuppressKeyPress = True
                    Case Keys.N
                        PlayNext()
                        e.SuppressKeyPress = True
                    Case Keys.B
                        PlayPrevious()
                        e.SuppressKeyPress = True
                    Case Keys.L
                        App.ShowLibrary()
                        e.SuppressKeyPress = True
                    Case Keys.V
                        ToggleVisualizer()
                        e.SuppressKeyPress = True
                End Select
            End If
        End If
    End Sub
    Private Sub Player_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub Player_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        PEXLeft.Size = New Size(BtnMute.Location.X + BtnMute.Width - PEXLeft.Left, PEXLeft.Height)
        PEXRight.Size = New Size(BtnMute.Location.X + BtnMute.Width - PEXRight.Left, PEXRight.Height)
        VideoSetSize()
    End Sub
    Private Sub Player_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, MenuPlayer.MouseDown, LblPlaylistCount.MouseDown, LblDuration.MouseDown, PEXLeft.MouseDown, PEXRight.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FrameBorderSize.Width - 4, -e.Y - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FrameBorderSize.Width - 4, -e.Y - cSender.Top - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            End If
        End If
    End Sub
    Private Sub Player_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, PicBoxAlbumArt.MouseMove, MenuPlayer.MouseMove, LblPlaylistCount.MouseMove, LblDuration.MouseMove, PEXLeft.MouseMove, PEXRight.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub Player_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, PicBoxAlbumArt.MouseUp, MenuPlayer.MouseUp, LblPlaylistCount.MouseUp, LblDuration.MouseUp, PEXLeft.MouseUp, PEXRight.MouseUp
        mMove = False
    End Sub
    Private Sub Player_Move(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Move
        If Visible AndAlso Me.WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.PlayerLocation = Location
        End If
    End Sub
    Private Sub Player_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.PlayerSize = Size
        End If
        Select Case WindowState
            Case FormWindowState.Minimized
                If App.ShowTrayIcon AndAlso App.MinimizeToTray Then
                    ShowInTaskbar = False
                End If
            Case FormWindowState.Normal, FormWindowState.Maximized
                ShowInTaskbar = True
        End Select
    End Sub
    Private Sub Player_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        ResetTxtBoxPlaylistSearch()
        LVPlaylist.Select()
    End Sub
    Private Sub Player_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SavePlaylist()
        If VLCHook IsNot Nothing Then
            VLCHook.ReleaseHandle()
            RemoveHandler VLCHook.SingleClick, AddressOf VLCViewer_SingleClick
            RemoveHandler VLCHook.DoubleClick, AddressOf VLCViewer_DoubleClick
            RemoveHandler VLCHook.RightClick, AddressOf VLCViewer_RightClick
            VLCHook = Nothing
        End If
        If MeterAudioCapture IsNot Nothing Then
            MeterAudioCapture.StopRecording()
            RemoveHandler MeterAudioCapture.DataAvailable, AddressOf OnMeterDataAvailable
            MeterAudioCapture.Dispose()
            MeterAudioCapture = Nothing
        End If
        My.Finalize()
    End Sub

    'Control Events
    Private Sub VLCViewer_SingleClick(clientPoint As Point)
        FullScreen = Not FullScreen
    End Sub
    Private Sub VLCViewer_DoubleClick(clientPoint As Point)
        If Not FullScreen Then
            If Me.InvokeRequired Then
                Me.BeginInvoke(New MethodInvoker(AddressOf ToggleMaximized))
            Else
                ToggleMaximized()
            End If
        End If
    End Sub
    Private Sub VLCViewer_RightClick(clientPoint As Point)
        Debug.Print("Show Context Menu")
    End Sub
    Private Sub LVPlaylist_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles LVPlaylist.DrawColumnHeader
        Static b As Rectangle
        'Draw Background
        b = e.Bounds
        Using br As Brush = New SolidBrush(App.CurrentTheme.BackColor)
            e.Graphics.FillRectangle(br, b)
        End Using
        'Draw Borders
        b.Width -= 1
        b.Height -= 1
        e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, b)
        b.Width -= 1
        b.Height -= 1
        e.Graphics.DrawLine(SystemPens.ControlLightLight, b.X, b.Y, b.Right, b.Y)
        e.Graphics.DrawLine(SystemPens.ControlLightLight, b.X, b.Y, b.X, b.Bottom)
        e.Graphics.DrawLine(SystemPens.ControlDark, (b.X + 1), b.Bottom, b.Right, b.Bottom)
        e.Graphics.DrawLine(SystemPens.ControlDark, b.Right, (b.Y + 1), b.Right, b.Bottom)
        'Draw Text
        b = e.Bounds
        Dim width As Integer = TextRenderer.MeasureText(" ", e.Font).Width
        b = Rectangle.Inflate(e.Bounds, -2, 0)
        If e.Header.TextAlign = HorizontalAlignment.Center Then
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, b, App.CurrentTheme.TextColor, TextFormatFlags.HorizontalCenter)
        Else
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, b, App.CurrentTheme.TextColor, TextFormatFlags.VerticalCenter)
        End If
    End Sub
    Private Sub LVPlaylist_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles LVPlaylist.DrawItem
        If e.Item.Selected = False Then
            e.DrawDefault = True
        End If
    End Sub
    Private Sub LVPlaylist_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles LVPlaylist.DrawSubItem
        Static b As Rectangle
        Static s As SizeF
        If e.Item.Selected = True Then
            If e.ColumnIndex = LVPlaylist.Columns("Title").Index Then
                b = e.Bounds
                s = e.Graphics.MeasureString(e.SubItem.Text, e.Item.Font, e.Bounds.Size)
                If s.Width <= e.Bounds.Width Then b.Width = CInt(s.Width) + 4
                If b.Width > LVPlaylist.Columns(e.ColumnIndex).Width Then b.Width = LVPlaylist.Columns(e.ColumnIndex).Width
                e.Graphics.FillRectangle(New SolidBrush(App.CurrentTheme.TextColor), b)
                TextRenderer.DrawText(e.Graphics, App.GenerateEllipsis(e.Graphics, e.SubItem.Text, e.Item.Font, b.Width), e.Item.Font, New System.Drawing.Point(b.Left + 2, b.Top + 1), App.CurrentTheme.BackColor, TextFormatFlags.NoPrefix)
            ElseIf e.ColumnIndex = LVPlaylist.Columns("PlayCount").Index Or e.ColumnIndex = LVPlaylist.Columns("Rating").Index Then
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.Item.Font, e.Bounds, App.CurrentTheme.TextColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            Else
                TextRenderer.DrawText(e.Graphics, App.GenerateEllipsis(e.Graphics, e.SubItem.Text, e.Item.Font, e.Bounds.Width), e.Item.Font, New System.Drawing.Point(e.Bounds.Left + 2, e.Bounds.Top + 1), App.CurrentTheme.TextColor, TextFormatFlags.NoPrefix)
            End If
        Else
            b = e.Bounds
            e.Graphics.FillRectangle(New SolidBrush(App.CurrentTheme.BackColor), b)
            If e.ColumnIndex = LVPlaylist.Columns("Title").Index Then
                TextRenderer.DrawText(e.Graphics, App.GenerateEllipsis(e.Graphics, e.SubItem.Text, PlaylistBoldFont, b.Width), PlaylistBoldFont, New System.Drawing.Point(b.Left + 2, b.Top + 2), App.CurrentTheme.TextColor, TextFormatFlags.NoPrefix)
            ElseIf e.ColumnIndex = LVPlaylist.Columns("PlayCount").Index Or e.ColumnIndex = LVPlaylist.Columns("Rating").Index Then
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, LVPlaylist.Font, b, App.CurrentTheme.TextColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            Else
                TextRenderer.DrawText(e.Graphics, App.GenerateEllipsis(e.Graphics, e.SubItem.Text, LVPlaylist.Font, b.Width), LVPlaylist.Font, New System.Drawing.Point(b.Left + 2, b.Top + 2), App.CurrentTheme.TextColor, TextFormatFlags.NoPrefix)
            End If
        End If
    End Sub
    Private Sub LVPlaylist_ColumnReordered(sender As Object, e As ColumnReorderedEventArgs) Handles LVPlaylist.ColumnReordered
        If e.NewDisplayIndex = 0 Or e.OldDisplayIndex = 0 Then e.Cancel = True
    End Sub
    Private Sub LVPlaylist_KeyDown(sender As Object, e As KeyEventArgs) Handles LVPlaylist.KeyDown
        If e.Alt Then
        ElseIf e.Control Then
            Select Case e.KeyCode
            End Select
        ElseIf e.Shift Then
            Select Case e.KeyCode
            End Select
        Else
            Select Case e.KeyCode
                Case Keys.Enter
                    Select Case App.PlaylistDefaultAction
                        Case App.PlaylistActions.Play
                            PlayFromPlaylist()
                        Case App.PlaylistActions.Queue
                            QueueFromPlaylist()
                    End Select
                Case Keys.Escape
                Case Keys.End
                Case Keys.Up
                Case Keys.Down
                Case Keys.Left
                Case Keys.Right
                Case Keys.Space
                Case Keys.OemQuestion
                Case Keys.PageUp
                Case Keys.PageDown
                Case Keys.Home
                Case Keys.Delete : PlaylistRemoveItems()
                Case Keys.Insert
            End Select
        End If
    End Sub
    Private Sub LVPlaylist_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles LVPlaylist.ColumnClick
        If LVPlaylist.Items.Count = 0 Then
            ClearPlaylistTitles()
        Else
            Select Case e.Column
                Case LVPlaylist.Columns("Title").Index
                    PlaylistTitleSort = ClearPlaylistSorts(PlaylistTitleSort)
                    Select Case PlaylistTitleSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            PlaylistTitleSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("Title").Index).Text = "Title ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            PlaylistTitleSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("Title").Index).Text = "Title ↑"
                    End Select
                Case LVPlaylist.Columns("Path").Index
                    PlaylistPathSort = ClearPlaylistSorts(PlaylistPathSort)
                    Select Case PlaylistPathSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            PlaylistPathSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("Path").Index).Text = "Path ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            PlaylistPathSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("Path").Index).Text = "Path ↑"
                    End Select
                Case LVPlaylist.Columns("Rating").Index
                    PlaylistRatingSort = ClearPlaylistSorts(PlaylistRatingSort)
                    Select Case PlaylistRatingSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            PlaylistRatingSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("Rating").Index).Text = "Rating ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            PlaylistRatingSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("Rating").Index).Text = "Rating ↑"
                    End Select
                Case LVPlaylist.Columns("PlayCount").Index
                    PlaylistPlayCountSort = ClearPlaylistSorts(PlaylistPlayCountSort)
                    Select Case PlaylistPlayCountSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemNumberComparer(e.Column, SortOrder.Descending)
                            PlaylistPlayCountSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("PlayCount").Index).Text = "Plays ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemNumberComparer(e.Column, SortOrder.Ascending)
                            PlaylistPlayCountSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("PlayCount").Index).Text = "Plays ↑"
                    End Select
                Case LVPlaylist.Columns("LastPlayed").Index
                    PlaylistLastPlayedSort = ClearPlaylistSorts(PlaylistLastPlayedSort)
                    Select Case PlaylistLastPlayedSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemDateComparer(e.Column, SortOrder.Descending)
                            PlaylistLastPlayedSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("LastPlayed").Index).Text = "Last Played ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemDateComparer(e.Column, SortOrder.Ascending)
                            PlaylistLastPlayedSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("LastPlayed").Index).Text = "Last Played ↑"
                    End Select
                Case LVPlaylist.Columns("FirstPlayed").Index
                    PlaylistFirstPlayedSort = ClearPlaylistSorts(PlaylistFirstPlayedSort)
                    Select Case PlaylistFirstPlayedSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemDateComparer(e.Column, SortOrder.Descending)
                            PlaylistFirstPlayedSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("FirstPlayed").Index).Text = "First Played ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemDateComparer(e.Column, SortOrder.Ascending)
                            PlaylistFirstPlayedSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("FirstPlayed").Index).Text = "First Played ↑"
                    End Select
                Case LVPlaylist.Columns("Added").Index
                    PlaylistAddedSort = ClearPlaylistSorts(PlaylistAddedSort)
                    Select Case PlaylistAddedSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemDateComparer(e.Column, SortOrder.Descending)
                            PlaylistAddedSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("Added").Index).Text = "Added ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New App.ListViewItemDateComparer(e.Column, SortOrder.Ascending)
                            PlaylistAddedSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("Added").Index).Text = "Added ↑"
                    End Select
            End Select
        End If
    End Sub
    Private Sub LVPlaylist_DoubleClick(sender As Object, e As EventArgs) Handles LVPlaylist.DoubleClick
        Select Case App.PlaylistDefaultAction
            Case App.PlaylistActions.Play
                PlayFromPlaylist()
            Case App.PlaylistActions.Queue
                QueueFromPlaylist()
        End Select
    End Sub
    Private Sub LVPlaylist_MouseDown(sender As Object, e As MouseEventArgs) Handles LVPlaylist.MouseDown
        If e.Clicks = 1 Then PlaylistItemMove = LVPlaylist.GetItemAt(e.X, e.Y)
    End Sub
    Private Sub LVPlaylist_MouseMove(sender As Object, e As MouseEventArgs) Handles LVPlaylist.MouseMove
        If PlaylistItemMove IsNot Nothing Then
            Cursor = Cursors.Hand
            Dim lastItemBottom = Math.Min(e.Y, LVPlaylist.Items(LVPlaylist.Items.Count - 1).GetBounds(ItemBoundsPortion.Entire).Bottom - 1)
            Dim itemover = LVPlaylist.GetItemAt(0, lastItemBottom)
            If itemover IsNot Nothing Then
                Dim rc = itemover.GetBounds(ItemBoundsPortion.Entire)
                If e.Y < rc.Top + rc.Height / 2 Then
                    LVPlaylist.LineBefore = itemover.Index
                    LVPlaylist.LineAfter = -1
                Else
                    LVPlaylist.LineBefore = -1
                    LVPlaylist.LineAfter = itemover.Index
                End If
                LVPlaylist.Invalidate()
            End If
        End If
    End Sub
    Private Sub LVPlaylist_MouseUp(sender As Object, e As MouseEventArgs) Handles LVPlaylist.MouseUp
        If PlaylistItemMove IsNot Nothing Then
            Dim lastItemBottom = Math.Min(e.Y, LVPlaylist.Items(LVPlaylist.Items.Count - 1).GetBounds(ItemBoundsPortion.Entire).Bottom - 1)
            Dim itemover = LVPlaylist.GetItemAt(0, lastItemBottom)
            If itemover IsNot Nothing And itemover IsNot PlaylistItemMove Then
                LVPlaylist.ListViewItemSorter = Nothing
                ClearPlaylistTitles()
                Dim insertbefore As Boolean
                Dim rc = itemover.GetBounds(ItemBoundsPortion.Entire)
                If e.Y < rc.Top + rc.Height / 2 Then
                    insertbefore = True
                Else
                    insertbefore = False
                End If
                LVPlaylist.Items.Remove(PlaylistItemMove)
                If Not PlaylistItemMove.Index = itemover.Index Then
                    If insertbefore Then
                        LVPlaylist.Items.Insert(itemover.Index, PlaylistItemMove)
                    Else
                        LVPlaylist.Items.Insert(itemover.Index + 1, PlaylistItemMove)
                    End If
                End If
            End If
            PlaylistItemMove = Nothing
        End If
        PlaylistItemMove = Nothing
        Cursor = Cursors.Default
        LVPlaylist.LineBefore = -1
        LVPlaylist.LineAfter = -1
        LVPlaylist.Invalidate()
    End Sub
    Private Sub LVPlaylist_DragEnter(sender As Object, e As DragEventArgs) Handles LVPlaylist.DragEnter
        Activate()
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim filedrop = DirectCast(e.Data.GetData(DataFormats.FileDrop, True), String())
            Dim files As New List(Of String)
            For Each s In filedrop
                If Computer.FileSystem.FileExists(s) Then
                    Dim ext = IO.Path.GetExtension(s).TrimStart("."c).ToLower()
                    'Check if it's a known media or playlist format
                    Dim isMedia = App.ExtensionDictionary.ContainsKey("." & ext)
                    Dim isPlaylist = App.PlaylistIO.Formats.Any(Function(f) f.FileExtension.TrimStart("."c).ToLower() = ext)
                    If isMedia OrElse isPlaylist Then files.Add(s)
                End If
            Next
            If files.Count > 0 Then
                e.Effect = DragDropEffects.Link
            Else
                e.Effect = DragDropEffects.None
            End If
            files.Clear()
            files = Nothing
            filedrop = Nothing
        Else : e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub LVPlaylist_DragDrop(sender As Object, e As DragEventArgs) Handles LVPlaylist.DragDrop
        If e.Effect = DragDropEffects.Link Then
            Dim filedrop = DirectCast(e.Data.GetData(DataFormats.FileDrop, True), String())
            Dim mediafiles As New List(Of String)
            Dim playlistfiles As New List(Of String)
            For Each s In filedrop
                If Computer.FileSystem.FileExists(s) Then
                    Dim ext = IO.Path.GetExtension(s).ToLower()

                    'Check if it's a known media or playlist format
                    Dim isMedia = App.ExtensionDictionary.ContainsKey(ext)
                    If isMedia Then mediafiles.Add(s)
                    Dim isPlaylist = App.PlaylistIO.Formats.Any(Function(f) "." & f.FileExtension.TrimStart("."c).ToLower() = ext)
                    If isPlaylist Then playlistfiles.Add(s)
                    Debug.Print("LVPlaylist_DragDrop: Media Files (" & mediafiles.Count.ToString & "), Playlist Files (" & playlistfiles.Count.ToString & ")")
                End If
            Next
            If mediafiles.Count > 0 Then
                WriteToLog("Player Drag&Drop Media Performed (" + mediafiles.Count.ToString + " " + IIf(mediafiles.Count = 1, "File", "Files").ToString + ")")
                Dim lvi As ListViewItem
                Dim clientpoint = LVPlaylist.PointToClient(New System.Drawing.Point(e.X, e.Y))
                Dim itemover = LVPlaylist.GetItemAt(clientpoint.X, clientpoint.Y)
                For x = 0 To mediafiles.Count - 1
                    If LVPlaylist.Items.Count = 0 Then
                        lvi = Nothing
                    Else
                        lvi = LVPlaylist.FindItemWithText(mediafiles(x), True, 0)
                    End If
                    If lvi Is Nothing Then
                        'Add new playlist entry
                        lvi = CreateListviewItem()
                        lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = App.FormatPlaylistTitle(mediafiles(x))
                        lvi.SubItems(LVPlaylist.Columns("Path").Index).Text = mediafiles(x)
                        App.AddToHistoryFromPlaylist(mediafiles(x))
                        GetHistory(lvi, mediafiles(x))
                        If itemover Is Nothing Then
                            LVPlaylist.Items.Add(lvi)
                        Else
                            LVPlaylist.ListViewItemSorter = Nothing
                            ClearPlaylistTitles()
                            LVPlaylist.Items.Insert(itemover.Index, lvi)
                        End If
                        SetPlaylistCountText()
                    Else
                        'Update playlist entry
                        lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = App.FormatPlaylistTitle(mediafiles(x))
                        LVPlaylist.Items.RemoveAt(lvi.Index)
                        If itemover Is Nothing Then
                            LVPlaylist.Items.Add(lvi)
                        Else
                            LVPlaylist.ListViewItemSorter = Nothing
                            ClearPlaylistTitles()
                            If itemover.Index >= 0 Then
                                LVPlaylist.Items.Insert(itemover.Index, lvi)
                            Else
                                LVPlaylist.Items.Add(lvi)
                            End If
                        End If
                    End If
                Next
                lvi = Nothing
                clientpoint = Nothing
                itemover = Nothing
            End If
            If playlistfiles.Count > 0 Then
                WriteToLog("Player Drag&Drop Playlist Performed (" + playlistfiles.Count.ToString + " " + IIf(playlistfiles.Count = 1, "File", "Files").ToString + ")")
                For Each file In playlistfiles
                    MergePlaylistFromFile(file)
                Next
            End If
        End If
    End Sub
    Private Sub LVPlaylist_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVPlaylist.SelectedIndexChanged
        SetPlaylistCountText()
        AlbumArtIndex = 0
    End Sub
    Private Sub Panel_DoubleClick(sender As Object, e As EventArgs) Handles PanelMedia.DoubleClick, PanelVisualizer.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub PanelVisualizer_Resize(sender As Object, e As EventArgs) Handles PanelVisualizer.Resize
        VisualizerHost?.ResizeHost()
    End Sub
    Private Sub MenuPlayer_DoubleClick(sender As Object, e As EventArgs) Handles MenuPlayer.DoubleClick
        'Debug.Print(MenuPlayer.PointToClient(MousePosition).ToString)
        'Debug.Print(MIPlayMode.Bounds.ToString)
        Dim p As Point = MenuPlayer.PointToClient(MousePosition)
        Dim rPlayMode As Rectangle = MIPlayMode.Bounds
        Dim rLyrics As Rectangle = MILyrics.Bounds
        If Not (p.X >= rPlayMode.Left And p.Y >= rPlayMode.Top And p.X <= rPlayMode.Right And p.Y <= rPlayMode.Bottom) AndAlso
           Not (p.X >= rLyrics.Left And p.Y >= rLyrics.Top And p.X <= rLyrics.Right And p.Y <= rLyrics.Bottom) Then
            ToggleMaximized()
        End If
    End Sub
    Private Sub MIFile_MouseEnter(sender As Object, e As EventArgs) Handles MIFile.MouseEnter
        MIFile.ForeColor = Color.Black
    End Sub
    Private Sub MIFile_MouseLeave(sender As Object, e As EventArgs) Handles MIFile.MouseLeave
        If Not MIFile.DropDown.Visible Then MIFile.ForeColor = App.CurrentTheme.AccentTextColor
    End Sub
    Private Sub MIFile_DropDownOpening(sender As Object, e As EventArgs) Handles MIFile.DropDownOpening
        If LVPlaylist.Items.Count = 0 Then
            MISavePlaylist.Enabled = False
        Else
            MISavePlaylist.Enabled = True
        End If
    End Sub
    Private Sub MIFile_DropDownClosed(sender As Object, e As EventArgs) Handles MIFile.DropDownClosed
        If Not MIFile.Selected Then MIFile.ForeColor = App.CurrentTheme.AccentTextColor
    End Sub
    Private Sub MIOpen_Click(sender As Object, e As EventArgs) Handles MIOpen.Click
        AddToPlaylistFromFile()
    End Sub
    Private Sub MIOpenURL_Click(sender As Object, e As EventArgs) Handles MIOpenURL.Click
        Dim frmAddStream As New PlayerAddStream
        Dim uriresult As Uri = Nothing
        If Clipboard.ContainsText Then
            frmAddStream.NewStream.Path = Clipboard.GetText
            If Not Uri.TryCreate(frmAddStream.NewStream.Path, UriKind.Absolute, uriresult) Then
                frmAddStream.NewStream.Path = String.Empty
            End If
        End If
        frmAddStream.ShowDialog(Me)
        If frmAddStream.DialogResult = DialogResult.OK Then
            If Uri.TryCreate(frmAddStream.NewStream.Path, UriKind.Absolute, uriresult) Then
                Dim newstream As String = frmAddStream.NewStream.Path.TrimEnd("/"c)
                'Add to History
                App.AddToHistoryFromPlaylist(newstream, True)
                'Add to Playlist
                AddToPlaylistFromLibrary(frmAddStream.NewStream.Title, newstream)
                LVPlaylist.SelectedIndices.Clear()
                LVPlaylist.SelectedIndices.Add(LVPlaylist.FindItemWithText(frmAddStream.NewStream.Title).Index)
                'Play Stream
                PlayStream(newstream)
                Debug.Print("New Stream Added (" + newstream + ")")
            Else
                Debug.Print("Invalid Stream")
                WriteToLog("Invalid Stream, New Stream Not Added")
            End If
        Else
            Debug.Print("Add Stream Cancelled")
        End If
    End Sub
    Private Sub MIOpenPlaylist_Click(sender As Object, e As EventArgs) Handles MIOpenPlaylist.Click
        OpenPlaylist()
    End Sub
    Private Sub MISavePlaylist_Click(sender As Object, e As EventArgs) Handles MISavePlaylist.Click
        SavePlaylistAs()
    End Sub
    Private Sub MIExit_Click(sender As Object, e As EventArgs) Handles MIExit.Click
        Close()
    End Sub
    Private Sub MIView_MouseEnter(sender As Object, e As EventArgs) Handles MIView.MouseEnter
        MIView.ForeColor = Color.Black
    End Sub
    Private Sub MIView_MouseLeave(sender As Object, e As EventArgs) Handles MIView.MouseLeave
        If Not MIView.DropDown.Visible Then MIView.ForeColor = App.CurrentTheme.AccentTextColor
    End Sub
    Private Sub MIViewDropDownOpening(sender As Object, e As EventArgs) Handles MIView.DropDownOpening
        If Not PlayState = PlayStates.Stopped AndAlso App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(_player.Path)) Then
            MIFullscreen.Enabled = True
        Else
            MIFullscreen.Enabled = False
        End If
        MIViewQueue.Text = MIViewQueue.Text.TrimEnd(App.TrimEndSearch) + " (" + Queue.Count.ToString + ")"
        If Queue.Count = 0 Then
            MIViewQueue.Enabled = False
        Else
            MIViewQueue.Enabled = True
        End If
    End Sub
    Private Sub MIView_DropDownClosed(sender As Object, e As EventArgs) Handles MIView.DropDownClosed
        If Not MIView.Selected Then MIView.ForeColor = App.CurrentTheme.AccentTextColor
    End Sub
    Private Sub MIFullscreenClick(sender As Object, e As EventArgs) Handles MIFullscreen.Click
        FullScreen = True
    End Sub
    Private Sub MIMiniPlayer_Click(sender As Object, e As EventArgs) Handles MIMiniPlayer.Click
        App.SetMiniPlayer()
    End Sub
    Private Sub MIViewQueue_Click(sender As Object, e As EventArgs) Handles MIViewQueue.Click
        Dim frm As New PlayerQueue
        frm.ShowDialog()
    End Sub
    Private Sub MIViewHistory_Click(sender As Object, e As EventArgs) Handles MIViewHistory.Click
        App.ShowHistory()
    End Sub
    Private Sub MIOptionsClick(sender As Object, e As EventArgs) Handles MIOptions.Click
        ShowOptions()
    End Sub
    Private Sub MIVisualizer_Click(sender As Object, e As EventArgs) Handles MIVisualizer.Click
        If Visualizer Then
            VisualizerOff()
            ShowMedia()
        Else
            VisualizerOn()
            ShowMedia()
        End If
    End Sub
    Private Sub MILyrics_Click(sender As Object, e As EventArgs) Handles MILyrics.Click
        If Lyrics Then
            LyricsOff()
            ShowMedia()
        Else
            LyricsOn()
            ShowMedia()
        End If
    End Sub
    Private Sub MIPlayMode_Click(sender As Object, e As EventArgs) Handles MIPlayMode.Click
        Dim newIndex As Byte = CType(App.PlayMode + 1, Byte)
        App.ShowToast(Nothing, "Shuffle Play Yet?")
        If newIndex = [Enum].GetNames(GetType(App.PlayModes)).Length Then
            newIndex = 0
        End If
        App.PlayMode = CType(newIndex, App.PlayModes)
        ShowPlayMode()
        SetTipPlayer()
    End Sub
    Private Sub MIPlayMode_MouseEnter(sender As Object, e As EventArgs) Handles MIPlayMode.MouseEnter
        MIPlayMode.ForeColor = Color.Black
    End Sub
    Private Sub MIPlayMode_MouseLeave(sender As Object, e As EventArgs) Handles MIPlayMode.MouseLeave
        MIPlayMode.ForeColor = App.CurrentTheme.AccentTextColor
    End Sub
    Private Sub MILibraryClick(sender As Object, e As EventArgs) Handles MILibrary.Click
        App.ShowLibrary()
    End Sub
    Private Sub MILibrary_MouseEnter(sender As Object, e As EventArgs) Handles MILibrary.MouseEnter
        MILibrary.ForeColor = Color.Black
    End Sub
    Private Sub MILibrary_MouseLeave(sender As Object, e As EventArgs) Handles MILibrary.MouseLeave
        MILibrary.ForeColor = App.CurrentTheme.AccentTextColor
    End Sub
    Private Sub MIShowHelpClick(sender As Object, e As EventArgs) Handles MIShowHelp.Click
        ShowHelp()
    End Sub
    Private Sub MIShowLogClick(sender As Object, e As EventArgs) Handles MIShowLog.Click
        If App.FrmLog Is Nothing Then
            ShowLog()
        Else
            ShowLog(True)
        End If
    End Sub
    Private Sub MIShowAboutClick(sender As Object, e As EventArgs) Handles MIShowAbout.Click
        ShowAbout()
    End Sub
    Private Sub MIAbout_MouseEnter(sender As Object, e As EventArgs) Handles MIAbout.MouseEnter
        MIAbout.ForeColor = Color.Black
    End Sub
    Private Sub MIAbout_MouseLeave(sender As Object, e As EventArgs) Handles MIAbout.MouseLeave
        If Not MIAbout.DropDown.Visible Then MIAbout.ForeColor = App.CurrentTheme.AccentTextColor
    End Sub
    Private Sub MIAbout_DropDownClosed(sender As Object, e As EventArgs) Handles MIAbout.DropDownClosed
        If Not MIAbout.Selected Then MIAbout.ForeColor = App.CurrentTheme.AccentTextColor
    End Sub
    Private Sub CMPlaylist_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMPlaylist.Opening
        If LVPlaylist.SelectedItems.Count > 0 Then
            SetPlaylistToolTip()
            Dim tiptext As String = App.History.Find(Function(p) p.Path = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)?.ToString
            If TipPlaylist IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(tiptext) Then TipPlaylist.ShowTooltipAt(New Point(CMPlaylist.Location.X, CMPlaylist.Location.Y - 37), tiptext)
        End If
        CMIHelperApp1.Text = "Open with " + App.HelperApp1Name
        CMIHelperApp2.Text = "Open with " + App.HelperApp2Name
        If LVPlaylist.Items.Count = 0 Then
            CMIClearPlaylist.Enabled = False
            CMIShowCurrent.Enabled = False
        Else
            CMIClearPlaylist.Enabled = True
            CMIShowCurrent.Enabled = True
        End If
        If LVPlaylist.SelectedItems.Count = 0 Then
            CMIPlaylistRemove.Enabled = False
            CMIEditTitle.Enabled = False
            CMIRating.Enabled = False
            CMIViewInLibrary.Enabled = False
            CMIEditTag.Enabled = False
            CMIEditTag.Text = "Edit Tag"
            CMIPlaylistRemove.Text = CMIPlaylistRemove.Text.TrimEnd(App.TrimEndSearch)
            CMICopyTitle.ToolTipText = String.Empty
            CMICopyFileName.ToolTipText = String.Empty
            CMICopyFilePath.ToolTipText = String.Empty
            CMICopyTitle.Enabled = False
            CMICopyFileName.Enabled = False
            CMICopyFilePath.Enabled = False
            CMIHelperApp1.Visible = False
            CMIHelperApp2.Visible = False
            CMIOpenLocation.Visible = False
            TSSeparatorExternalTools.Visible = False
        Else
            CMIPlaylistRemove.Enabled = True
            CMIEditTitle.Enabled = True
            CMIRating.Enabled = True
            CMIViewInLibrary.Enabled = True
            CMIEditTag.Enabled = True
            If LVPlaylist.SelectedItems.Count = 1 Then
                CMIEditTag.Text = "Edit Tag"
            Else
                CMIEditTag.Text = "Edit Tags " & "(" & LVPlaylist.SelectedItems.Count.ToString & ")"
            End If
            CMIPlaylistRemove.Text = CMIPlaylistRemove.Text.TrimEnd(App.TrimEndSearch)
            CMIPlaylistRemove.Text += " (" + LVPlaylist.SelectedItems.Count.ToString + ")"
            CMICopyTitle.ToolTipText = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Title").Index).Text
            CMICopyFileName.ToolTipText = IO.Path.GetFileNameWithoutExtension(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            CMICopyFilePath.ToolTipText = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text
            CMICopyTitle.Enabled = True
            CMICopyFileName.Enabled = True
            CMICopyFilePath.Enabled = True
            If IO.File.Exists(App.HelperApp1Path) Then
                CMIHelperApp1.Visible = True
            Else
                CMIHelperApp1.Visible = False
            End If
            If IO.File.Exists(App.HelperApp2Path) Then
                CMIHelperApp2.Visible = True
            Else
                CMIHelperApp2.Visible = False
            End If
            CMIOpenLocation.Visible = True
            TSSeparatorExternalTools.Visible = True
        End If
        If LVPlaylist.SelectedItems.Count > 0 Then
            CMIPlay.Enabled = True
            CMIQueue.Enabled = True
            CMIPlayWithWindows.Enabled = True
            Select Case App.PlaylistDefaultAction
                Case App.PlaylistActions.Play
                    CMIPlay.Font = New Font(CMIPlay.Font, FontStyle.Bold)
                    CMIQueue.Font = New Font(CMIQueue.Font, FontStyle.Regular)
                Case App.PlaylistActions.Queue
                    CMIPlay.Font = New Font(CMIPlay.Font, FontStyle.Regular)
                    CMIQueue.Font = New Font(CMIQueue.Font, FontStyle.Bold)
            End Select
        Else
            CMIPlay.Font = New Font(CMIPlay.Font, FontStyle.Regular)
            CMIQueue.Font = New Font(CMIQueue.Font, FontStyle.Regular)
            CMIPlay.Enabled = False
            CMIQueue.Enabled = False
            CMIPlayWithWindows.Enabled = False
        End If
    End Sub
    Private Sub CMPlaylist_Closing(sender As Object, e As ToolStripDropDownClosingEventArgs) Handles CMPlaylist.Closing
        TipPlaylist?.HideTooltip()
        TipPlaylist?.Dispose()
        TipPlaylist = Nothing
    End Sub
    Private Sub CMIPlay_Click(sender As Object, e As EventArgs) Handles CMIPlay.Click
        PlayFromPlaylist()
    End Sub
    Private Sub CMIQueue_Click(sender As Object, e As EventArgs) Handles CMIQueue.Click
        CMPlaylist.Close()
        QueueFromPlaylist()
    End Sub
    Private Sub CMIPlayWithWindowsClick(sender As Object, e As EventArgs) Handles CMIPlayWithWindows.Click
        If LVPlaylist.SelectedItems.Count > 0 Then App.PlayWithWindows(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
    End Sub
    Private Sub CMIPlaylistAddClick(sender As Object, e As EventArgs) Handles CMIPlaylistAdd.Click
        AddToPlaylistFromFile()
    End Sub
    Private Sub CMIPlaylistRemoveClick(sender As Object, e As EventArgs) Handles CMIPlaylistRemove.Click
        PlaylistRemoveItems()
    End Sub
    Private Sub CMIClearPlaylistClick(sender As Object, e As EventArgs) Handles CMIClearPlaylist.Click
        LVPlaylist.Items.Clear()
    End Sub
    Private Sub CMIEditTitle_Click(sender As Object, e As EventArgs) Handles CMIEditTitle.Click
        Dim frmEditTitle As New PlayerEditTitle With {
            .NewTitle = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Title").Index).Text}
        frmEditTitle.ShowDialog(Me)
        If frmEditTitle.DialogResult = DialogResult.OK Then
            If frmEditTitle.NewTitle = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Title").Index).Text Then
                Debug.Print("Edit Title Cancelled, New Title Same As Old Title")
            Else
                LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Title").Index).Text = frmEditTitle.NewTitle
                Debug.Print("Title of " + LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text + " Updated to " + frmEditTitle.NewTitle)
            End If
        Else
            Debug.Print("Edit Title Cancelled")
        End If
    End Sub
    Private Sub CMIShowCurrentClick(sender As Object, e As EventArgs) Handles CMIShowCurrent.Click
        Dim item As ListViewItem
        Try
            item = LVPlaylist.FindItemWithText(_player.Path, True, 0)
            If item IsNot Nothing Then
                LVPlaylist.SelectedItems.Clear()
                item.Selected = True
                item.EnsureVisible()
            End If
        Catch
        End Try
    End Sub
    Private Sub CMIRating5Stars_Click(sender As Object, e As EventArgs) Handles CMIRating5Stars.Click
        If LVPlaylist.SelectedItems.Count > 0 Then
            Dim hindex As Integer = App.History.FindIndex(Function(p) p.Path = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            Dim hsong As App.Song = App.History(hindex)
            hsong.Rating = 5
            App.History(hindex) = hsong
            UpdateHistoryInPlaylist(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            hsong = Nothing
        End If
    End Sub
    Private Sub CMIRating4Stars_Click(sender As Object, e As EventArgs) Handles CMIRating4Stars.Click
        If LVPlaylist.SelectedItems.Count > 0 Then
            Dim hindex As Integer = App.History.FindIndex(Function(p) p.Path = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            Dim hsong As App.Song = App.History(hindex)
            hsong.Rating = 4
            App.History(hindex) = hsong
            UpdateHistoryInPlaylist(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            hsong = Nothing
        End If
    End Sub
    Private Sub CMIRating3Stars_Click(sender As Object, e As EventArgs) Handles CMIRating3Stars.Click
        If LVPlaylist.SelectedItems.Count > 0 Then
            Dim hindex As Integer = App.History.FindIndex(Function(p) p.Path = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            Dim hsong As App.Song = App.History(hindex)
            hsong.Rating = 3
            App.History(hindex) = hsong
            UpdateHistoryInPlaylist(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            hsong = Nothing
        End If
    End Sub
    Private Sub CMIRating2Stars_Click(sender As Object, e As EventArgs) Handles CMIRating2Stars.Click
        If LVPlaylist.SelectedItems.Count > 0 Then
            Dim hindex As Integer = App.History.FindIndex(Function(p) p.Path = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            Dim hsong As App.Song = App.History(hindex)
            hsong.Rating = 2
            App.History(hindex) = hsong
            UpdateHistoryInPlaylist(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            hsong = Nothing
        End If
    End Sub
    Private Sub CMIRating1Star_Click(sender As Object, e As EventArgs) Handles CMIRating1Star.Click
        If LVPlaylist.SelectedItems.Count > 0 Then
            Dim hindex As Integer = App.History.FindIndex(Function(p) p.Path = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            Dim hsong As App.Song = App.History(hindex)
            hsong.Rating = 1
            App.History(hindex) = hsong
            UpdateHistoryInPlaylist(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            hsong = Nothing
        End If
    End Sub
    Private Sub CMIViewInLibraryClick(sender As Object, e As EventArgs) Handles CMIViewInLibrary.Click
        If LVPlaylist.SelectedItems.Count > 0 Then App.FrmLibrary.Show(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
    End Sub
    Private Sub CMIEditTag_Click(sender As Object, e As EventArgs) Handles CMIEditTag.Click
        EditTags()
    End Sub
    Private Sub CMIHelperApp1Click(sender As Object, e As EventArgs) Handles CMIHelperApp1.Click
        If LVPlaylist.SelectedItems.Count > 0 Then App.HelperApp1(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
    End Sub
    Private Sub CMIHelperApp2Click(sender As Object, e As EventArgs) Handles CMIHelperApp2.Click
        If LVPlaylist.SelectedItems.Count > 0 Then App.HelperApp2(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
    End Sub
    Private Sub CMIOpenLocationClick(sender As Object, e As EventArgs) Handles CMIOpenLocation.Click
        If LVPlaylist.SelectedItems.Count > 0 Then App.OpenFileLocation(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
    End Sub
    Private Sub CMICopyTitleClick(sender As Object, e As EventArgs) Handles CMICopyTitle.Click
        If LVPlaylist.SelectedItems.Count > 0 Then Clipboard.SetText(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Title").Index).Text)
    End Sub
    Private Sub CMICopyFileNameClick(sender As Object, e As EventArgs) Handles CMICopyFileName.Click
        If LVPlaylist.SelectedItems.Count > 0 Then Clipboard.SetText(IO.Path.GetFileNameWithoutExtension(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text))
    End Sub
    Private Sub CMICopyFilePathClick(sender As Object, e As EventArgs) Handles CMICopyFilePath.Click
        If LVPlaylist.SelectedItems.Count > 0 Then Clipboard.SetText(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
    End Sub
    Private Sub PicBoxAlbumArt_Paint(sender As Object, e As PaintEventArgs) Handles PicBoxAlbumArt.Paint
        If AlbumArtCount > 1 Then
            Dim g = e.Graphics
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            'Badge background (semi-transparent black circle)
            Dim badgeSize As Integer = 28
            Dim badgeRect As New Rectangle(PicBoxAlbumArt.Width - badgeSize - 6, 6, badgeSize, badgeSize)
            Using bgBrush As New SolidBrush(App.CurrentTheme.BackColor)
                g.FillEllipse(bgBrush, badgeRect)
            End Using

            'Count text
            Dim overlayText As String = AlbumArtCount.ToString()
            Using f As New Font("Segoe UI", 12, FontStyle.Bold),
                textBrush As New SolidBrush(App.CurrentTheme.TextColor),
                sf As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                badgeRect.Offset(1, 1) ' Slight offset for better centering
                g.DrawString(overlayText, f, textBrush, badgeRect, sf)
            End Using

        End If
    End Sub
    Private Sub PicBoxAlbumArt_MouseDown(sender As Object, e As MouseEventArgs) Handles PicBoxAlbumArt.MouseDown
        Select Case e.Button
            Case MouseButtons.Left
                'Start a timer for single-click
                If PicBoxAlbumArtClickTimer Is Nothing Then
                    PicBoxAlbumArtClickTimer = New Timer With {
                        .Interval = SystemInformation.DoubleClickTime}
                    AddHandler PicBoxAlbumArtClickTimer.Tick,
                        Sub()
                            PicBoxAlbumArtClickTimer?.Stop()
                            PicBoxAlbumArtClickTimer?.Dispose()
                            PicBoxAlbumArtClickTimer = Nothing

                            'Single-click action
                            AlbumArtIndex += CByte(1)
                            ShowMedia()
                        End Sub
                    PicBoxAlbumArtClickTimer.Start()
                    Player_MouseDown(sender, e)
                End If
            Case MouseButtons.Right
        End Select
    End Sub
    Private Sub PicBoxAlbumArt_DoubleClick(sender As Object, e As EventArgs) Handles PicBoxAlbumArt.DoubleClick
        'Cancel pending single-click
        PicBoxAlbumArtClickTimer?.Stop()
        PicBoxAlbumArtClickTimer?.Dispose()
        PicBoxAlbumArtClickTimer = Nothing

        'Double-click action
        ToggleMaximized()
    End Sub
    Private Sub PicBoxVisualizer_DoubleClick(sender As Object, e As EventArgs)
        ToggleMaximized()
    End Sub
    Private Sub LblPosition_MouseUp(sender As Object, e As MouseEventArgs) Handles LblPosition.MouseUp
        PlayerPositionShowElapsed = Not PlayerPositionShowElapsed
        ShowPosition()
        SetTipPlayer()
    End Sub
    Private Sub LblPosition_DoubleClick(sender As Object, e As EventArgs) Handles LblPosition.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub LblDuration_DoubleClick(sender As Object, e As EventArgs) Handles LblDuration.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub LblPlaylistCount_DoubleClick(sender As Object, e As EventArgs) Handles LblPlaylistCount.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub LblMedia_Paint(sender As Object, e As PaintEventArgs) Handles LblMedia.Paint

        'Paint the parent background first
        If LblMedia.Parent IsNot Nothing Then
            Dim state = e.Graphics.Save()
            e.Graphics.TranslateTransform(-LblMedia.Left, -LblMedia.Top)
            Dim pe As New PaintEventArgs(e.Graphics, LblMedia.Parent.ClientRectangle)
            InvokePaintBackground(LblMedia.Parent, pe)
            InvokePaint(LblMedia.Parent, pe)
            e.Graphics.Restore(state)
        End If

        Dim flags As TextFormatFlags = TextFormatFlags.EndEllipsis Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine
        TextRenderer.DrawText(e.Graphics, LblMedia.Text, LblMedia.Font, LblMedia.ClientRectangle, LblMedia.ForeColor, flags)

    End Sub
    Private Sub LblMedia_DoubleClick(sender As Object, e As EventArgs) Handles LblMedia.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub TxtBoxPlaylistSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtBoxPlaylistSearch.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Escape)
                ResetTxtBoxPlaylistSearch()
                LVPlaylist.Focus()
                e.Handled = True
        End Select
    End Sub
    Private Sub TxtBoxPlaylistSearch_Enter(sender As Object, e As EventArgs) Handles TxtBoxPlaylistSearch.Enter
        If TxtBoxPlaylistSearch.Text = PlaylistSearchTitle Then
            TxtBoxPlaylistSearch.ResetText()
            TxtBoxPlaylistSearch.ForeColor = App.CurrentTheme.AccentTextColor
        End If
    End Sub
    Private Sub TxtBoxPlaylistSearch_Leave(sender As Object, e As EventArgs) Handles TxtBoxPlaylistSearch.Leave
        If TxtBoxPlaylistSearch.Text = String.Empty Or Not ListBoxPlaylistSearch.Focused Then
            ResetTxtBoxPlaylistSearch()
        End If
    End Sub
    Private Sub TxtBoxPlaylistSearchTextChanged(sender As Object, e As EventArgs) Handles TxtBoxPlaylistSearch.TextChanged
        If TxtBoxPlaylistSearch.Text Is String.Empty And ListBoxPlaylistSearch.Visible Then
            ListBoxPlaylistSearch.Visible = False
            PlaylistSearchItems.Clear()
        ElseIf TxtBoxPlaylistSearch.Text IsNot String.Empty And TxtBoxPlaylistSearch.Text IsNot PlaylistSearchTitle Then
            PlaylistSearchItems.Clear()
            For Each item As ListViewItem In LVPlaylist.Items
                If item.Text.ToLower.Contains(TxtBoxPlaylistSearch.Text.ToLower) Then PlaylistSearchItems.Add(item)
            Next
            ListBoxPlaylistSearch.Items.Clear()
            If PlaylistSearchItems.Count > 0 Then
                For Each item As ListViewItem In PlaylistSearchItems
                    ListBoxPlaylistSearch.Items.Add(item.Text)
                Next
                ListBoxPlaylistSearch.Visible = True
            End If
        End If
    End Sub
    Private Sub ListBoxPlaylistSearchSelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxPlaylistSearch.SelectedIndexChanged
        If ListBoxPlaylistSearch.SelectedItems.Count = 1 Then
            LVPlaylist.EnsureVisible(PlaylistSearchItems.Item(ListBoxPlaylistSearch.SelectedIndex).Index)
            LVPlaylist.SelectedIndices.Clear()
            LVPlaylist.SelectedIndices.Add(PlaylistSearchItems.Item(ListBoxPlaylistSearch.SelectedIndex).Index)
            ResetTxtBoxPlaylistSearch()
            LVPlaylist.Select()
            Select Case App.PlaylistSearchAction
                Case App.PlaylistActions.Play
                    PlayFromPlaylist()
                Case App.PlaylistActions.Queue
                    QueueFromPlaylist()
            End Select
        End If
    End Sub
    Private Sub BtnPlayMouseDown(sender As Object, e As MouseEventArgs) Handles BtnPlay.MouseDown
        TogglePlay()
    End Sub
    Private Sub BtnStopMouseDown(sender As Object, e As MouseEventArgs) Handles BtnStop.MouseDown
        StopPlay()
        LVPlaylist.Focus()
    End Sub
    Private Sub BtnReverseMouseDown(sender As Object, e As MouseEventArgs) Handles BtnReverse.MouseDown
        If Not CurrentMediaType = App.MediaSourceTypes.Stream Then
            UpdatePosition(False, 10)
            LVPlaylist.Focus()
        End If
    End Sub
    Private Sub BtnForwardMouseDown(sender As Object, e As MouseEventArgs) Handles BtnForward.MouseDown
        If Not CurrentMediaType = App.MediaSourceTypes.Stream Then
            UpdatePosition(True, 10)
            LVPlaylist.Focus()
        End If
    End Sub
    Private Sub BtnPreviousMouseDown(sender As Object, e As MouseEventArgs) Handles BtnPrevious.MouseDown
        PlayPrevious()
        LVPlaylist.Focus()
    End Sub
    Private Sub BtnNextMouseDown(sender As Object, e As MouseEventArgs) Handles BtnNext.MouseDown
        PlayNext()
        LVPlaylist.Focus()
    End Sub
    Private Sub BtnMuteMouseDown(sender As Object, e As MouseEventArgs) Handles BtnMute.MouseDown
        ToggleMute()
        LVPlaylist.Focus()
    End Sub
    Private Sub TrackBarPosition_MouseDown(sender As Object, e As MouseEventArgs) Handles TrackBarPosition.MouseDown
        If PlayState = PlayStates.Playing Then TogglePlay()
    End Sub
    Private Sub TrackBarPosition_MouseUp(sender As Object, e As MouseEventArgs) Handles TrackBarPosition.MouseUp
        Dim newposition As Double
        newposition = ((e.X - 7) / (TrackBarPosition.Width - 18)) * (TrackBarPosition.Maximum - TrackBarPosition.Minimum)
        If newposition < 0 Then
            newposition = 0
        ElseIf newposition > TrackBarPosition.Maximum Then
            newposition = TrackBarPosition.Maximum
        End If
        TrackBarPosition.Value = Convert.ToInt32(newposition)
        'Debug.Print((TrackBarPosition.Value / TrackBarScale).ToString)
        _player.Position = TrackBarPosition.Value / TrackBarScale
        TogglePlay()
        LVPlaylist.Focus()
    End Sub
    Private Sub TrackBarPosition_MouseWheel(sender As Object, e As MouseEventArgs)
        CType(e, HandledMouseEventArgs).Handled = True
    End Sub

    'Handlers
    Private Async Sub OnPlaybackStarted()
        OnPlay()

        ' ✅ Safe to dispose frmFullScreen:
        ' - When user exits fullscreen manually (ESC key)
        ' - Before next playback starts (OnPlay)
        ' ❌ NOT safe during playback end — LibVLC may be tearing down
        If frmFullScreen IsNot Nothing Then
            frmFullScreen.Close()
            frmFullScreen.Dispose()
            frmFullScreen = Nothing
        End If

        ' ⚠️ VLCViewerHook attach may fail if LibVLC hasn't created its child window yet.
        ' Use retry loop with delay to wait for GetVLCChild() to return valid handle.
        ' Avoid attaching hooks too early after playback starts.

        'Clean up any previous hook
        If VLCHook IsNot Nothing Then
            VLCHook.ReleaseHandle()
            RemoveHandler VLCHook.SingleClick, AddressOf VLCViewer_SingleClick
            RemoveHandler VLCHook.DoubleClick, AddressOf VLCViewer_DoubleClick
            RemoveHandler VLCHook.RightClick, AddressOf VLCViewer_RightClick
            VLCHook = Nothing
            'Debug.Print("VLC Viewer Hook Detached")
        End If

        'Attach to the new child
        If App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(_player.Path)) Then
            'Debug.Print("Attaching VLC Viewer Hook...")

            Dim child As IntPtr = IntPtr.Zero
            Dim attempts As Integer = 0

            ' Retry loop to wait for LibVLC to create its child window
            Do
                child = GetVLCChild(VLCViewer.Handle)
                If child <> IntPtr.Zero Then Exit Do
                Await Task.Delay(50)
                attempts += 1
            Loop While attempts < 10

            If child = IntPtr.Zero Then
                Debug.Print("VLC Viewer Hook Failed: No child window found after 10 attempts.")
            Else
                VLCHook = New VLCViewerHook()
                VLCHook.AssignHandle(child)
                AddHandler VLCHook.SingleClick, AddressOf VLCViewer_SingleClick
                AddHandler VLCHook.DoubleClick, AddressOf VLCViewer_DoubleClick
                AddHandler VLCHook.RightClick, AddressOf VLCViewer_RightClick
                'Debug.Print("VLC Viewer Hook Attached")
            End If
        End If

    End Sub
    Private Sub OnPlaybackEnded()
        PlayState = PlayStates.Stopped
        BtnPlay.Image = App.CurrentTheme.PlayerPlay
        TrackBarPosition.Value = 0
        PEXLeft.Value = 0
        PEXRight.Value = 0
        ResetLblPositionText()
        If Not App.PlayMode = App.PlayModes.None Then
            AutoNext = True
            PlayNext()
        End If
        ' ⚠️ WARNING: Do NOT call FullScreen = False here.
        ' LibVLC is already tearing down its rendering surface.
        ' Reparenting VLCViewer during this moment causes WinForms to freeze.
        ' Let fullscreen collapse naturally — cleanup happens safely elsewhere, in OnPlaybackStarted.
    End Sub
    Private Sub OnMeterDataAvailable(sender As Object, e As WaveInEventArgs)
        Dim wf = MeterAudioCapture.WaveFormat
        Dim channels As Integer = wf.Channels
        If channels <> 2 Then
            ' Fallback: mono -> duplicate into both channels
            ' Convert appropriately below
        End If

        Dim leftMax As Single = 0.0F
        Dim rightMax As Single = 0.0F

        If wf.Encoding = WaveFormatEncoding.IeeeFloat Then
            ' 32-bit float: 4 bytes per sample per channel, 8 bytes per stereo frame
            Dim waveBuffer As New WaveBuffer(e.Buffer)
            Dim totalSamples As Integer = e.BytesRecorded \ 4
            ' Iterate by frame (two samples per frame)
            For i As Integer = 0 To totalSamples - 2 Step 2
                Dim l As Single = waveBuffer.FloatBuffer(i)
                Dim r As Single = waveBuffer.FloatBuffer(i + 1)
                leftMax = Math.Max(leftMax, Math.Abs(l))
                rightMax = Math.Max(rightMax, Math.Abs(r))
            Next

        ElseIf wf.Encoding = WaveFormatEncoding.Pcm AndAlso wf.BitsPerSample = 16 Then
            ' 16-bit PCM: 2 bytes per sample per channel, 4 bytes per stereo frame
            Dim frames As Integer = e.BytesRecorded \ (channels * 2)
            For f As Integer = 0 To frames - 1
                Dim baseIndex As Integer = f * channels * 2

                ' Little-endian Int16
                Dim lInt As Short = CShort(e.Buffer(baseIndex) Or (e.Buffer(baseIndex + 1) << 8))
                Dim rInt As Short
                If channels >= 2 Then
                    Dim rBase As Integer = baseIndex + 2
                    rInt = CShort(e.Buffer(rBase) Or (e.Buffer(rBase + 1) << 8))
                Else
                    rInt = lInt ' mono fallback
                End If

                ' Normalize to -1..+1
                Dim l As Single = Math.Abs(lInt / 32768.0F)
                Dim r As Single = Math.Abs(rInt / 32768.0F)

                leftMax = Math.Max(leftMax, l)
                rightMax = Math.Max(rightMax, r)
            Next

        Else
            ' Other encodings: add branches as needed (24-bit PCM, etc.)
            ' For safety, bail out or treat as zero
            leftMax = 0.0F
            rightMax = 0.0F
        End If

        ' Optional smoothing: peak hold with gentle decay to avoid jitter
        Dim decay As Single = 0.85F
        MeterDecayLeft = Math.Max(leftMax, MeterDecayLeft * decay)
        MeterDecayRight = Math.Max(rightMax, MeterDecayRight * decay)

        ' Store normalized peaks (0..1). Use smoothed or raw max:
        MeterPeakLeft = MeterDecayLeft
        MeterPeakRight = MeterDecayRight
    End Sub
    Private Sub TimerMeter_Tick(sender As Object, e As EventArgs) Handles TimerMeter.Tick
        If _player.HasMedia AndAlso PlayState = PlayStates.Playing Then
            Dim leftScaled As Single = MeterPeakLeft * 100.0F
            Dim rightScaled As Single = MeterPeakRight * 100.0F
            Dim leftVal As Integer = CInt(Math.Max(PEXLeft.Minimum, Math.Min(PEXLeft.Maximum, leftScaled)))
            Dim rightVal As Integer = CInt(Math.Max(PEXRight.Minimum, Math.Min(PEXRight.Maximum, rightScaled)))
            PEXLeft.Value = leftVal
            PEXRight.Value = rightVal
        End If
    End Sub
    Private Sub TimerPosition_Tick(sender As Object, e As EventArgs) Handles TimerPosition.Tick
        If _player.HasMedia AndAlso PlayState = PlayStates.Playing Then ShowPosition()
    End Sub
    Private Sub TimerShowMedia_Tick(sender As Object, e As EventArgs) Handles TimerShowMedia.Tick
        TimerShowMedia.Stop()
        ShowMedia()
        If Mute Then ToggleMute()
        ShowNowPlayingToast(PlaylistCurrentText)
    End Sub
    Private Sub TimerStatus_Tick(sender As Object, e As EventArgs) Handles TimerStatus.Tick
        TimerStatus.Stop()
        SetPlaylistCountText()
    End Sub
    Private Sub TimerLyrics_Tick(sender As Object, e As EventArgs) Handles TimerLyrics.Tick
        If _player.HasMedia AndAlso PlayState = PlayStates.Playing AndAlso HasLyricsSynced Then

            Dim pos As TimeSpan = TimeSpan.FromSeconds(_player.Position)

            'Find the index of the last lyric whose time is <= current position
            Dim currentIndex As Integer = LyricsSynced.FindLastIndex(Function(l) l.Time <= pos)

            'Get the text safely (empty if we're before the first line)
            Dim currentText As String = If(currentIndex >= 0, LyricsSynced(currentIndex).Text, String.Empty)

            If Lyrics Then

                'Only update if the line actually changed
                If currentIndex <> LastLyricsIndex AndAlso currentIndex >= 0 Then
                    'Reset the old line
                    If LastLyricsIndex >= 0 AndAlso LastLyricsIndex < RTBLyrics.Lines.Length Then
                        Dim startOld = RTBLyrics.GetFirstCharIndexFromLine(LastLyricsIndex)
                        Dim lengthOld = RTBLyrics.Lines(LastLyricsIndex).Length
                        RTBLyrics.Select(startOld, lengthOld)
                        RTBLyrics.SelectionFont = New Font(RTBLyrics.Font, FontStyle.Regular)
                    End If

                    'Bold the new line
                    If currentIndex < RTBLyrics.Lines.Length Then
                        Dim startNew = RTBLyrics.GetFirstCharIndexFromLine(currentIndex)
                        Dim lengthNew = RTBLyrics.Lines(currentIndex).Length
                        RTBLyrics.Select(startNew, lengthNew)
                        RTBLyrics.SelectionFont = New Font(RTBLyrics.Font, FontStyle.Bold)

                        Dim firstVisible = RTBLyrics.GetLineFromCharIndex(RTBLyrics.GetCharIndexFromPosition(New Point(0, 0)))
                        Dim lastVisible = RTBLyrics.GetLineFromCharIndex(RTBLyrics.GetCharIndexFromPosition(New Point(0, RTBLyrics.Height - 1)))
                        If currentIndex < firstVisible OrElse currentIndex > lastVisible Then
                            RTBLyrics.ScrollToCaret()
                        End If

                        RTBLyrics.DeselectAll()
                    End If

                    LastLyricsIndex = currentIndex
                End If
            Else
                'Album art view: always keep label in sync
                If LblMedia.Text <> currentText Then LblMedia.Text = currentText
                If Not LblMedia.Visible Then LblMedia.Visible = True
            End If
        Else
            LblMedia.Text = String.Empty
        End If
    End Sub

    'Methods
    Private Function IsFile(path As String) As Boolean
        If App.History.FindIndex(Function(p) p.Path = path And p.SourceType = App.MediaSourceTypes.File) >= 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function IsStream(path As String) As Boolean
        If App.History.FindIndex(Function(p) p.Path = path And p.SourceType = App.MediaSourceTypes.Stream) >= 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function IsAudioCD(path As String) As Boolean
        If App.History.FindIndex(Function(p) p.Path = path And p.SourceType = App.MediaSourceTypes.AudioCD) >= 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function FormatDuration(duration As Double) As String
        Dim dur As TimeSpan = TimeSpan.FromSeconds(duration)
        Dim durstr As String = ""
        If dur.Hours > 0 Then
            durstr &= dur.Hours.ToString
            durstr &= ":"
        End If
        If dur.Minutes < 10 Then durstr &= "0"
        durstr &= dur.Minutes.ToString
        durstr &= ":"
        If dur.Seconds < 10 Then durstr &= "0"
        durstr &= dur.Seconds.ToString
        Return durstr
    End Function
    Private Function FormatPosition(position As Double) As String
        Dim pos As TimeSpan = TimeSpan.FromSeconds(position)
        Dim posstr As String = ""
        If Not My.App.PlayerPositionShowElapsed Then posstr = "-"
        If pos.Hours > 0 Then
            posstr &= pos.Hours.ToString
            posstr &= ":"
        End If
        If pos.Minutes < 10 Then posstr &= "0"
        posstr &= pos.Minutes.ToString
        posstr &= ":"
        If pos.Seconds < 10 Then posstr &= "0"
        posstr &= pos.Seconds.ToString
        Return posstr
    End Function
    Private Function RandomHistoryFull() As Boolean
        RandomHistoryFull = True
        For Each item As ListViewItem In LVPlaylist.Items
            If Not RandomHistory.Contains(item.SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                RandomHistoryFull = False
                Exit For
            End If
        Next
    End Function
    Private Function VideoGetHeight(width As Integer) As Integer
        If _player.HasMedia Then
            Try
                'Debug.Print("Calculating video height: VideoWidth=" + _player.VideoWidth.ToString + ", VideoHeight=" + _player.VideoHeight.ToString)
                Return CInt(Int(_player.VideoHeight * (width / _player.VideoWidth)))
            Catch ex As Exception
                Debug.Print("Error calculating video height: " + ex.Message)
                Return 0
            End Try
        Else
            Return 0
        End If
    End Function
    Private Function VideoGetWidth(height As Integer) As Integer
        If _player.HasMedia Then
            Try
                Return CInt(Int(_player.VideoWidth * (height / _player.VideoHeight)))
            Catch ex As Exception
                Debug.Print("Error calculating video width: " + ex.Message)
                Return 0
            End Try
        Else
            Return 0
        End If
    End Function
    Private Sub ShowNowPlayingToast(songtext As String)
        If App.ShowNowPlayingToast Then
            Dim npo As New Skye.UI.ToastOptions With {
                .Title = "Now Playing",
                .Message = songtext,
                .Duration = 6000,
                .BackColor = App.CurrentTheme.BackColor,
                .BorderColor = App.CurrentTheme.ButtonBackColor,
                .ForeColor = App.CurrentTheme.TextColor,
                .TitleFont = New Font("Segoe UI", 12),
                .MessageFont = New Font("Segoe UI", 12, FontStyle.Bold),
                .Location = App.NowPlayingToastLocation,
                .Image = PicBoxAlbumArt.Image
            }
            Skye.UI.Toast.ShowToast(npo)
        End If
    End Sub
    Friend Sub TogglePlayer()
        Static lastState As FormWindowState
        Select Case WindowState
            Case FormWindowState.Normal, FormWindowState.Maximized
                lastState = WindowState
                WindowState = FormWindowState.Minimized
            Case FormWindowState.Minimized
                WindowState = lastState
        End Select
    End Sub
    Friend Sub ExitApp()
        Close()
    End Sub
    Private Sub EditTags()
        If LVPlaylist.SelectedItems.Count > 0 Then
            Dim paths As New List(Of String)
            For Each lvi As ListViewItem In LVPlaylist.SelectedItems
                Dim path As String = lvi.SubItems(LVPlaylist.Columns("Path").Index).Text
                If IsFile(path) Then paths.Add(path)
            Next
            If paths.Count > 0 Then
                App.FrmTagEditor = New TagEditor(paths.ToList)
                Dim result = App.FrmTagEditor.ShowDialog(Me)
                If result = DialogResult.OK AndAlso Not App.WatcherUpdatePlaylist Then
                    For Each path In paths
                        AddToPlaylistFromPath(path)
                    Next
                    LVPlaylist.Focus()
                End If
            End If
        End If
    End Sub
    Friend Sub ShowPlayMode()
        Select Case App.PlayMode
            Case PlayModes.None
                MIPlayMode.Text = "Play Once"
            Case PlayModes.Repeat
                MIPlayMode.Text = "Repeat"
            Case PlayModes.Linear
                MIPlayMode.Text = "Play Next"
            Case PlayModes.Random
                MIPlayMode.Text = "Shuffle"
        End Select
    End Sub
    Friend Sub SetTipPlayer()
        Select Case App.PlayMode
            Case App.PlayModes.None, PlayModes.Repeat
                TipPlayer.SetToolTip(BtnPrevious, String.Empty)
                TipPlayer.SetToolTip(BtnNext, String.Empty)
            Case App.PlayModes.Linear
                TipPlayer.SetToolTip(BtnPrevious, "Previous Song In Playlist")
                TipPlayer.SetToolTip(BtnNext, "Next Song In Playlist")
            Case App.PlayModes.Random
                TipPlayer.SetToolTip(BtnPrevious, "Previous Song Played")
                TipPlayer.SetToolTip(BtnNext, "Next Random Song")
        End Select
        Select Case App.PlayerPositionShowElapsed
            Case True
                TipPlayer.SetToolTip(LblPosition, "Elapsed Time")
            Case False
                TipPlayer.SetToolTip(LblPosition, "Time Remaining")
        End Select
    End Sub
    Private Sub ShowStatusMessage(msg As String)
        If App.PlaylistStatusMessageDisplayTime > 0 Then
            LblPlaylistCount.Text = StrConv(msg, VbStrConv.ProperCase)
            TimerStatus.Interval = App.PlaylistStatusMessageDisplayTime * 1000
            TimerStatus.Start()
        End If
    End Sub
    Private Sub ResetLblPositionText()
        If _player.HasMedia Then
            If App.PlayerPositionShowElapsed Then
                LblPosition.Text = "00:00"
            Else
                LblPosition.Text = "-" + FormatDuration(_player.Duration)
            End If
        Else
            LblPosition.ResetText()
        End If
    End Sub
    Private Sub ResetTxtBoxPlaylistSearch()
        If Not TxtBoxPlaylistSearch.Text = PlaylistSearchTitle Then
            TxtBoxPlaylistSearch.Text = PlaylistSearchTitle
            TxtBoxPlaylistSearch.ForeColor = App.CurrentTheme.InactiveSearchTextColor
            ListBoxPlaylistSearch.Visible = False
            ListBoxPlaylistSearch.Items.Clear()
            PlaylistSearchItems.Clear()
            Debug.Print("Playlist Search Reset")
        End If
    End Sub
    Private Sub ToggleMaximized()
        Select Case WindowState
            Case FormWindowState.Normal, FormWindowState.Minimized
                WindowState = FormWindowState.Maximized
            Case FormWindowState.Maximized
                WindowState = FormWindowState.Normal
        End Select
    End Sub
    Friend Sub Suspend() 'Called when the user locks the screen or activates the screen saver
        If App.SuspendOnSessionChange Then
            Debug.Print("Suspending...")
            StopPlay()
            Me.WindowState = FormWindowState.Minimized
            App.WriteToLog("App Suspended @ " & Now)
        End If
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsNormalWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsNormalWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsNormalWindow
        If location.Y < App.AdjustScreenBoundsNormalWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub

    'Playlist
    Private Sub LoadPlaylist()
        If My.Computer.FileSystem.FileExists(App.PlaylistPath) Then
            Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
            Dim reader As New System.Xml.Serialization.XmlSerializer(GetType(System.Collections.Generic.List(Of PlaylistItemType)))
            Dim file As New IO.FileStream(App.PlaylistPath, IO.FileMode.Open)
            Dim items As System.Collections.Generic.List(Of PlaylistItemType)
            Try
                items = DirectCast(reader.Deserialize(file), System.Collections.Generic.List(Of PlaylistItemType))
                For Each item As PlaylistItemType In items
                    Dim lvi As ListViewItem
                    lvi = CreateListviewItem()
                    lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = item.Title
                    lvi.SubItems(LVPlaylist.Columns("Path").Index).Text = item.Path
                    GetHistory(lvi, item.Path)
                    LVPlaylist.Items.Add(lvi)
                    lvi = Nothing
                Next
            Catch
                items = Nothing
            End Try
            file.Close()
            file.Dispose()
            reader = Nothing
            If items Is Nothing Then
                App.WriteToLog("Playlist Not Loaded: File not valid (" + App.PlaylistPath + ")")
            Else
                items.Clear()
                items = Nothing
                App.WriteToLog("Playlist Loaded (" + Skye.Common.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
            End If
        Else
            App.WriteToLog("Playlist Not Loaded: File does not exist")
        End If
        SetPlaylistCountText()
    End Sub
    Private Sub SavePlaylist()
        If LVPlaylist.Items.Count = 0 Then
            If My.Computer.FileSystem.FileExists(App.PlaylistPath) Then My.Computer.FileSystem.DeleteFile(App.PlaylistPath)
        Else
            Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
            Dim items As New System.Collections.Generic.List(Of PlaylistItemType)
            For Each plitem As ListViewItem In LVPlaylist.Items
                Dim newitem As New PlaylistItemType With {
                    .Title = plitem.SubItems(LVPlaylist.Columns("Title").Index).Text,
                    .Path = plitem.SubItems(LVPlaylist.Columns("Path").Index).Text}
                items.Add(newitem)
                newitem = Nothing
            Next
            Dim writer As New System.Xml.Serialization.XmlSerializer(GetType(System.Collections.Generic.List(Of PlaylistItemType)))
            If Not My.Computer.FileSystem.DirectoryExists(App.UserPath) Then
                My.Computer.FileSystem.CreateDirectory(App.UserPath)
            End If
            Dim file As New System.IO.StreamWriter(App.PlaylistPath)
            writer.Serialize(file, items)
            file.Close()
            file.Dispose()
            writer = Nothing
            items.Clear()
            items = Nothing
            App.WriteToLog("Playlist Saved (" + Skye.Common.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
        End If
    End Sub
    Private Sub OpenPlaylist()
        Dim ext As String
        Dim filename As String

        'Build filter string from format interface
        Dim allExtensions As New List(Of String)
        Dim filterParts As New List(Of String)
        For Each fmt In App.PlaylistIO.Formats
            ext = fmt.FileExtension.TrimStart("."c).ToLower()
            allExtensions.Add($"*.{ext}")
            filterParts.Add($"{fmt.Name} (*.{ext})|*.{ext}")
        Next

        'Insert "All Supported Types" at the top
        Dim allSupported = $"All Supported Types ({String.Join(", ", allExtensions)})|{String.Join(";", allExtensions)}"
        filterParts.Insert(0, allSupported)

        'Create and show dialog
        Using ofd As New OpenFileDialog With {
                .Filter = String.Join("|", filterParts),
                .Title = "Import Playlist",
                .Multiselect = False,
                .CheckFileExists = True,
                .FilterIndex = 1} ' ✅ Default to "All Supported Types"    
            If ofd.ShowDialog() <> DialogResult.OK Then Exit Sub
            filename = ofd.FileName
        End Using

        'Import Playlist
        MergePlaylistFromFile(filename)

    End Sub
    Private Sub MergePlaylistFromFile(filename As String)

        'Detect format by extension
        Dim ext = IO.Path.GetExtension(filename).TrimStart("."c).ToLower()
        Dim format = App.PlaylistIO.Formats.FirstOrDefault(Function(f) f.FileExtension.TrimStart("."c).ToLower() = ext)
        If format Is Nothing Then
            ShowStatusMessage("Unsupported Playlist Format")
            Exit Sub
        End If

        'Import
        Try
            Dim items = format.Import(filename)
            For Each item In items
                Dim lvi As ListViewItem = Nothing
                If LVPlaylist.Items.Count > 0 Then lvi = LVPlaylist.FindItemWithText(item.Path, True, 0)
                If lvi Is Nothing Then 'Create new Playlist entry
                    lvi = CreateListviewItem()
                    lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = item.Title
                    lvi.SubItems(LVPlaylist.Columns("Path").Index).Text = item.Path.TrimEnd("/"c)
                    Dim isstream As Boolean = App.IsUrl(item.Path)
                    App.AddToHistoryFromPlaylist(item.Path, isstream)
                    GetHistory(lvi, item.Path)
                    If LVPlaylist.SelectedItems.Count > 0 Then
                        LVPlaylist.Items.Insert(LVPlaylist.SelectedItems(0).Index, lvi)
                    Else
                        LVPlaylist.Items.Add(lvi)
                    End If
                    'Debug.Print("MergePlaylistFromFile isstream = " & isstream.ToString)
                Else 'Update existing Playlist entry
                    If String.IsNullOrWhiteSpace(item.Title) Then
                        lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = IO.Path.GetFileNameWithoutExtension(item.Path)
                        If App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(item.Path)) Then lvi.SubItems(LVPlaylist.Columns("Title").Index).Text += App.PlaylistVideoIdentifier
                    Else
                        lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = item.Title
                    End If
                End If
            Next
            ShowStatusMessage("Playlist Loaded Successfully (" & items.Count.ToString & ")")
            App.WriteToLog("Imported Playlist from " & filename)
        Catch ex As Exception
            ShowStatusMessage("Error Loading Playlist")
            App.WriteToLog("Error Importing Playlist from " & filename & vbCr & ex.Message)
        End Try

        format = Nothing
    End Sub
    Private Sub SavePlaylistAs()
        Dim ext As String
        Dim filename As String

        'Build filter string from your format interface
        Dim filterParts As New List(Of String)
        For Each fmt In App.PlaylistIO.Formats
            ext = fmt.FileExtension.TrimStart("."c).ToLower()
            filterParts.Add($"{fmt.Name} (*.{ext})|*.{ext}")
        Next

        'Create and show dialog
        Using sfd As New SaveFileDialog With {
                .Filter = String.Join("|", filterParts),
                .Title = "Save Playlist As...",
                .AddExtension = True,
                .OverwritePrompt = True,
                .DefaultExt = App.PlaylistIO.Formats(1).FileExtension.TrimStart("."c),
                .FilterIndex = 2}
            If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub
            filename = sfd.FileName
        End Using

        'Detect format by extension
        ext = IO.Path.GetExtension(filename).TrimStart("."c).ToLower()
        Dim format = App.PlaylistIO.Formats.FirstOrDefault(Function(f) f.FileExtension.TrimStart("."c).ToLower() = ext)
        If format Is Nothing Then
            ShowStatusMessage("Unsupported Playlist Format")
            Exit Sub
        End If

        'Build playlist items
        Dim items = LVPlaylist.Items.Cast(Of ListViewItem)().
        Select(Function(lvi) New PlaylistItemType With {.Title = lvi.SubItems(0).Text, .Path = lvi.SubItems(1).Text})

        'Export
        If LVPlaylist.Items.Count = 0 Then
            ShowStatusMessage("Playlist is Empty, Nothing to Save")
            Exit Sub
        End If
        Try
            format.Export(filename, items)
            ShowStatusMessage("Playlist Successfully Saved")
            App.WriteToLog("Exported Playlist to " & filename)
        Catch ex As Exception
            ShowStatusMessage("Error Saving Playlist")
            App.WriteToLog("Error Exporting Playlist to " & filename & vbCr & ex.Message)
        End Try

        format = Nothing
    End Sub
    Private Sub AddToPlaylistFromFile()
        Dim ofd As New OpenFileDialog With {
            .Title = "Select Media File(s)",
            .Filter = "All Files|*.*",
            .Multiselect = True}
        Dim result As DialogResult = ofd.ShowDialog(Me)
        If result = DialogResult.OK AndAlso ofd.FileNames.Length > 0 Then
            Dim lvi As ListViewItem = Nothing
            For x = 0 To ofd.FileNames.Length - 1
                If LVPlaylist.Items.Count > 0 Then lvi = LVPlaylist.FindItemWithText(ofd.FileNames(x), True, 0)
                If LVPlaylist.Items.Count = 0 OrElse lvi Is Nothing Then
                    'Create New Playlist Entry
                    If App.ExtensionDictionary.ContainsKey(Path.GetExtension(ofd.FileNames(x))) Then
                        lvi = CreateListviewItem()
                        lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = App.FormatPlaylistTitle(ofd.FileNames(x))
                        lvi.SubItems(LVPlaylist.Columns("Path").Index).Text = ofd.FileNames(x)
                        App.AddToHistoryFromPlaylist(ofd.FileNames(x))
                        GetHistory(lvi, ofd.FileNames(x))
                        If LVPlaylist.SelectedItems.Count = 0 Then
                            LVPlaylist.Items.Add(lvi)
                        Else
                            LVPlaylist.ListViewItemSorter = Nothing
                            ClearPlaylistTitles()
                            LVPlaylist.Items.Insert(LVPlaylist.SelectedItems(0).Index, lvi)
                        End If
                    End If
                Else
                    'Update Playlist Entry
                    lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = App.FormatPlaylistTitle(ofd.FileNames(x))
                    LVPlaylist.Items.RemoveAt(lvi.Index)
                    If LVPlaylist.SelectedItems.Count = 0 Then
                        LVPlaylist.Items.Add(lvi)
                    Else
                        LVPlaylist.ListViewItemSorter = Nothing
                        ClearPlaylistTitles()
                        LVPlaylist.Items.Insert(LVPlaylist.SelectedItems(0).Index, lvi)
                    End If
                End If
            Next
            lvi = Nothing
        End If
        SetPlaylistCountText()
        ofd.Dispose()
    End Sub
    Private Sub AddToPlaylistFromPath(path As String)
        Dim lvi As ListViewItem
        If LVPlaylist.Items.Count > 0 Then
            lvi = LVPlaylist.FindItemWithText(path, True, 0)
            If lvi IsNot Nothing Then LVPlaylist.Items.Remove(lvi)
        End If
        lvi = CreateListviewItem()
        lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = App.FormatPlaylistTitle(path)
        lvi.SubItems(LVPlaylist.Columns("Path").Index).Text = path
        GetHistory(lvi, path)
        ClearPlaylistTitles()
        LVPlaylist.ListViewItemSorter = Nothing
        If LVPlaylist.SelectedItems.Count = 0 Then
            LVPlaylist.Items.Add(lvi)
        Else
            LVPlaylist.Items.Insert(LVPlaylist.SelectedItems(0).Index, lvi)
        End If
        SetPlaylistCountText()
        lvi = Nothing
    End Sub
    Friend Sub AddToPlaylistFromLibrary(title As String, filename As String)
        Dim lvi As ListViewItem
        If LVPlaylist.Items.Count > 0 Then
            lvi = LVPlaylist.FindItemWithText(filename, True, 0)
            If lvi IsNot Nothing Then LVPlaylist.Items.Remove(lvi)
        End If
        lvi = CreateListviewItem()
        lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = title
        lvi.SubItems(LVPlaylist.Columns("Path").Index).Text = filename
        GetHistory(lvi, filename)
        ClearPlaylistTitles()
        LVPlaylist.ListViewItemSorter = Nothing
        If LVPlaylist.SelectedItems.Count = 0 Then
            LVPlaylist.Items.Add(lvi)
        Else
            LVPlaylist.Items.Insert(LVPlaylist.SelectedItems(0).Index, lvi)
        End If
        SetPlaylistCountText()
        lvi = Nothing
    End Sub
    Friend Sub AddToPlaylistFromHistory(items As List(Of String))
        Dim addedcount As Integer = 0
        LVPlaylist.BeginUpdate()
        For Each item As String In items
            Dim existingitem As ListViewItem = LVPlaylist.FindItemWithText(item, True, 0)
            If existingitem Is Nothing Then
                Dim newlvi As ListViewItem = CreateListviewItem()
                newlvi.SubItems(LVPlaylist.Columns("Title").Index).Text = App.FormatPlaylistTitle(item)
                newlvi.SubItems(LVPlaylist.Columns("Path").Index).Text = item
                GetHistory(newlvi, item)
                ClearPlaylistTitles()
                LVPlaylist.ListViewItemSorter = Nothing
                If LVPlaylist.SelectedItems.Count = 0 Then
                    LVPlaylist.Items.Add(newlvi)
                Else
                    LVPlaylist.Items.Insert(LVPlaylist.SelectedItems(0).Index, newlvi)
                End If
                addedcount += 1
                SetPlaylistCountText()
            End If
        Next
        LVPlaylist.EndUpdate()
        ShowStatusMessage("Added " & addedcount.ToString("N0") & If(addedcount = 1, " song", " songs") & " to playlist")
    End Sub
    Private Sub QueueFromPlaylist()
        If LVPlaylist.SelectedItems.Count > 0 Then
            'Dim found As Boolean = False
            'For Each s As String In Queue
            '    If s = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text Then
            '        found = True
            '        Exit For
            '    End If
            'Next
            'If Not found Then
            'Queue.Add(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            QueuePath(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            'End If
        End If
    End Sub
    Private Sub PlaylistRemoveItems()
        If LVPlaylist.SelectedItems.Count > 0 Then
            If LVPlaylist.SelectedItems.Count = LVPlaylist.Items.Count Then
                LVPlaylist.Items.Clear()
            Else
                LVPlaylist.Enabled = False
                For Each item As ListViewItem In LVPlaylist.SelectedItems
                    If App.PlayMode = App.PlayModes.Random Then
                        RandomHistory.Remove(item.SubItems(LVPlaylist.Columns("Path").Index).Text)
                    End If
                    item.Remove()
                Next
                LVPlaylist.Enabled = True
                SetPlaylistCountText()
                If LVPlaylist.Items.Count = 0 Then ClearPlaylistTitles()
                If RandomHistoryIndex > RandomHistory.Count - 1 Then RandomHistoryIndex = RandomHistory.Count - 1
            End If
        End If
    End Sub
    Friend Sub RemoveFromPlaylistFromLibrary(filename As String)
        If LVPlaylist.Items.Count > 0 Then
            Dim lvi As ListViewItem
            lvi = LVPlaylist.FindItemWithText(filename, True, 0)
            If lvi IsNot Nothing Then LVPlaylist.Items.Remove(lvi)
            SetPlaylistCountText()
        End If
    End Sub
    Private Sub GetHistory(ByRef lvi As ListViewItem, path As String)
        Dim s As App.Song = App.History.Find(Function(p) p.Path = path)
        If s IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Path) Then
            If s.Rating > 0 Then lvi.SubItems(LVPlaylist.Columns("Rating").Index).Text = New String("★"c, s.Rating)
            lvi.SubItems(LVPlaylist.Columns("PlayCount").Index).Text = s.PlayCount.ToString()
            If Not s.LastPlayed = Nothing Then
                lvi.SubItems(LVPlaylist.Columns("LastPlayed").Index).Text = s.LastPlayed.ToString()
            End If
            If Not s.FirstPlayed = Nothing Then
                lvi.SubItems(LVPlaylist.Columns("FirstPlayed").Index).Text = s.FirstPlayed.ToString()
            End If
            If Not s.Added = Nothing Then
                lvi.SubItems(LVPlaylist.Columns("Added").Index).Text = s.Added.ToString()
            End If
        End If
    End Sub
    Friend Sub UpdateHistoryInPlaylist(path As String)
        Dim lvi As ListViewItem
        Try
            lvi = LVPlaylist.FindItemWithText(path, True, 0)
        Catch
            lvi = Nothing
        End Try
        If lvi IsNot Nothing Then
            GetHistory(lvi, path)
            'Debug.Print("Updated History in Playlist for " + path)
        End If
    End Sub
    Private Sub RandomHistoryAdd(songorstream As String)
        If App.PlayMode = App.PlayModes.Random AndAlso LVPlaylist.FindItemWithText(songorstream, True, 0) IsNot Nothing Then
            If RandomHistory.FindIndex(Function(p) p = songorstream) < 0 Then
                App.UpdateRandomHistory(songorstream)
            Else
                'Debug.Print("Not Adding " + songorstream + " to Random History, Already Exists")
            End If
        Else
            'Debug.Print("Not Adding " + songorstream + " to Random History, Song Not Found In Playlist or Not In Random PlayMode")
        End If
    End Sub
    Friend Sub AddToRandomHistory(songorstream As String)
        If App.PlayMode = App.PlayModes.Random Then
            RandomHistory.Add(songorstream)
            RandomHistoryIndex = RandomHistory.Count
            'Debug.Print("Added " + songorstream + " to Random History")
        End If
    End Sub
    Friend Sub RandomHistoryClear()
        RandomHistory.Clear()
    End Sub
    Friend Sub PrunePlaylist()

        Debug.Print("Pruning Playlist..." + LVPlaylist.Items.Count.ToString + " total playlist items...")

        'Find files that don't exist
        Dim prunelist As New System.Collections.Generic.List(Of String)
        For index As Integer = 0 To LVPlaylist.Items.Count - 1
            If Not IsStream(LVPlaylist.Items(index).SubItems(1).Text) AndAlso Not My.Computer.FileSystem.FileExists(LVPlaylist.Items(index).SubItems(1).Text) Then
                prunelist.Add(LVPlaylist.Items(index).SubItems(1).Text)
            End If
        Next

        Debug.Print("Pruning History..." + prunelist.Count.ToString + " items found...")

        'Prune History
        For Each s As String In prunelist
            Debug.Print(s)
            LVPlaylist.Items.RemoveAt(LVPlaylist.FindItemWithText(s, True, 0).Index)
        Next
        Debug.Print("Playlist Pruned (" + prunelist.Count.ToString + ")")
        Debug.Print("Pruning Playlist Complete..." + LVPlaylist.Items.Count.ToString + " total playlist items.")
        WriteToLog("Playlist Pruned (" + prunelist.Count.ToString + ")")

        prunelist = Nothing
        SetPlaylistCountText()

    End Sub
    Private Sub SetPlaylistToolTip()
        If TipPlaylist IsNot Nothing Then
            TipPlaylist?.HideTooltip()
            TipPlaylist?.Dispose()
            TipPlaylist = Nothing
        End If

        TipPlaylist = New Skye.UI.ToolTipEX(components) With {
            .BackColor = App.CurrentTheme.BackColor,
            .ForeColor = App.CurrentTheme.TextColor,
            .BorderColor = App.CurrentTheme.ButtonBackColor,
            .Font = TipPlaylistFont,
            .ShadowAlpha = 200,
            .FadeInRate = 0,
            .FadeOutRate = 0,
            .HideDelay = 1000000,
            .ShowDelay = 1000
        }
    End Sub
    Friend Sub SetPlaylistCountText()
        LblPlaylistCount.ResetText()
        LblPlaylistCount.Text = LVPlaylist.Items.Count.ToString
        If LVPlaylist.Items.Count = 1 Then
            LblPlaylistCount.Text += " Song, "
        Else
            LblPlaylistCount.Text += " Songs, "
        End If
        LblPlaylistCount.Text += LVPlaylist.SelectedItems.Count.ToString + " Selected"
        LblPlaylistCount.Text += ", " + Queue.Count.ToString + " Queued"
    End Sub
    Private Sub ClearPlaylistTitles()
        PlaylistTitleSort = SortOrder.None
        PlaylistPathSort = SortOrder.None
        PlaylistRatingSort = SortOrder.None
        PlaylistPlayCountSort = SortOrder.None
        PlaylistLastPlayedSort = SortOrder.None
        PlaylistFirstPlayedSort = SortOrder.None
        PlaylistAddedSort = SortOrder.None
        LVPlaylist.Columns(LVPlaylist.Columns("Title").Index).Text = "Title"
        LVPlaylist.Columns(LVPlaylist.Columns("Path").Index).Text = "Path"
        LVPlaylist.Columns(LVPlaylist.Columns("Rating").Index).Text = "Rating"
        LVPlaylist.Columns(LVPlaylist.Columns("PlayCount").Index).Text = "Plays"
        LVPlaylist.Columns(LVPlaylist.Columns("LastPlayed").Index).Text = "Last Played"
        LVPlaylist.Columns(LVPlaylist.Columns("FirstPlayed").Index).Text = "First Played"
        LVPlaylist.Columns(LVPlaylist.Columns("Added").Index).Text = "Added"
    End Sub
    Private Sub EnsurePlaylistItemIsVisible(index As Integer)
        If CMPlaylist.Visible Then CMPlaylist.Close()
        LVPlaylist.EnsureVisible(index)
        LVPlaylist.SelectedIndices.Clear()
        LVPlaylist.SelectedIndices.Add(index)
        LVPlaylist.Items(index).Focused = True
    End Sub
    Friend Function CreateListviewItem() As ListViewItem
        Dim lvi As New ListViewItem    'Title
        lvi.SubItems.Add(String.Empty) 'Path
        lvi.SubItems.Add(String.Empty) 'Rating
        lvi.SubItems.Add(String.Empty) 'PlayCount
        lvi.SubItems.Add(String.Empty) 'LastPlayed
        lvi.SubItems.Add(String.Empty) 'FirstPlayed
        lvi.SubItems.Add(String.Empty) 'Added
        lvi.UseItemStyleForSubItems = False
        lvi.SubItems(LVPlaylist.Columns("Title").Index).Font = PlaylistBoldFont
        Return lvi
    End Function
    Private Function ClearPlaylistSorts(currentsort As SortOrder) As SortOrder
        ClearPlaylistTitles()
        Return currentsort
    End Function

    'Queue
    Friend Sub QueuePath(path As String)
        'Dim found As Boolean = False
        'For Each s As String In Queue
        '    If s = path Then
        '        found = True
        '        Exit For
        '    End If
        'Next
        'If Not found Then
        Queue.Add(path)
        SetPlaylistCountText()
        'End If
    End Sub
    Friend Sub RemoveFromQueue(path As String)
        Queue.Remove(path)
        SetPlaylistCountText()
    End Sub
    Friend Sub RemoveFromQueue(index As Integer)
        Queue.RemoveAt(index)
        SetPlaylistCountText()
    End Sub
    Friend Sub PruneQueue()
        Dim count As Integer = 0
        Dim removelist As New System.Collections.Generic.List(Of String)
        For Each item As String In Queue
            If LVPlaylist.FindItemWithText(item, True, 0) Is Nothing Then
                count += 1
                removelist.Add(item)
            End If
        Next
        For Each item As String In removelist
            Queue.Remove(item)
        Next
        SetPlaylistCountText()
        App.WriteToLog("Queue Pruned (" + count.ToString + ")")
    End Sub
    Private Sub PlayQueued()
        If Queue.Count > 0 Then
            StopPlay()
            If IsStream(Queue(0)) Then
                PlayStream(Queue(0))
            Else
                PlayFile(Queue(0), "PlayQueued")
            End If
            Dim item As ListViewItem = LVPlaylist.FindItemWithText(Queue(0), True, 0)
            If item IsNot Nothing Then EnsurePlaylistItemIsVisible(item.Index)
            Queue.RemoveAt(0)
            SetPlaylistCountText()
            TimerShowMedia.Start()
        End If
    End Sub

    'Player
    Friend Sub TogglePlay()
        If _player.HasMedia Then
            If PlayState = PlayStates.Playing Then
                _player.Pause()
                OnPause()
            Else
                _player.Play()
                '''OnPlay()
            End If
        Else
            If LVPlaylist.Items.Count > 0 Then
                If LVPlaylist.SelectedItems.Count = 0 Then LVPlaylist.Items(0).Selected = True
                PlayFromPlaylist()
            End If
        End If
        LVPlaylist.Focus()
    End Sub
    Friend Sub StopPlay()
        If _player.HasMedia Then
            _player.Stop()
            OnStop()
        End If
    End Sub
    Private Sub PlayStream(url As String)
        If Not String.IsNullOrEmpty(url) Then
            CurrentMediaType = App.MediaSourceTypes.Stream
            Try
                _player.Play(New Uri(url))
                '''OnPlay()
                TrackBarPosition.Enabled = False
                TrackBarPosition.Value = 0
                App.WriteToLog("Playing " + url + " (PlayStream)")
                RandomHistoryAdd(url)
            Catch
                App.WriteToLog("Cannot Play Stream, Invalid URL: " + url)
            End Try
        End If
    End Sub
    Private Sub PlayFile(path As String, source As String)
        If Not String.IsNullOrEmpty(path) Then
            CurrentMediaType = App.MediaSourceTypes.File
            Try
                _player.Play(path)
                '''OnPlay()
                If Not My.Computer.FileSystem.FileExists(path) Then
                    LblPosition.Text = String.Empty
                    LblDuration.Text = String.Empty
                End If
                App.WriteToLog("Playing " + path + " (" + source + ")")
                RandomHistoryAdd(path)
            Catch
                App.WriteToLog("Cannot Play File, Invalid Path: " + path + " (" + source + ")")
            End Try
        End If
    End Sub
    Private Sub PlayFromPlaylist()
        If LVPlaylist.SelectedItems.Count > 0 Then
            LyricsOff()
            StopPlay()
            If IsStream(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                PlayStream(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            Else
                'Stream = False
                PlayFile(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayFromPlaylist")
            End If
        End If
    End Sub
    Friend Sub PlayFromLibrary(title As String, filename As String)
        LyricsOff()
        Dim existingitem As ListViewItem = LVPlaylist.FindItemWithText(filename, True, 0)
        If existingitem Is Nothing Then
            Dim lvi As ListViewItem
            lvi = CreateListviewItem()
            lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = title
            lvi.SubItems(LVPlaylist.Columns("Path").Index).Text = filename
            LVPlaylist.ListViewItemSorter = Nothing
            GetHistory(lvi, filename)
            ClearPlaylistTitles()
            If LVPlaylist.SelectedItems.Count = 0 OrElse LVPlaylist.SelectedItems(0).Index = LVPlaylist.Items.Count - 1 Then
                LVPlaylist.Items.Add(lvi)
            Else
                LVPlaylist.Items.Insert(LVPlaylist.SelectedItems(0).Index + 1, lvi)
            End If
            SetPlaylistCountText()
            lvi = Nothing
        Else
            If Not existingitem.SubItems(LVPlaylist.Columns("Title").Index).Text = title Then
                Dim i As Integer = existingitem.Index
                existingitem.SubItems(LVPlaylist.Columns("Title").Index).Text = title
                LVPlaylist.Items.RemoveAt(i)
                LVPlaylist.Items.Insert(i, existingitem)
            End If
            EnsurePlaylistItemIsVisible(existingitem.Index)
        End If
        StopPlay()
        PlayFile(filename, "PlayFromLibrary")
    End Sub
    Friend Sub PlayPrevious()
        'Stream = False
        StopPlay()
        LyricsOff()
        Select Case App.PlayMode
            Case PlayModes.Repeat
                TimerShowMedia.Start()
            Case PlayModes.Linear
                If LVPlaylist.Items.Count > 0 Then
                    Dim item As ListViewItem = LVPlaylist.FindItemWithText(_player.Path, True, 0)
                    Dim newindex As Integer = LVPlaylist.Items.Count - 1
                    If item Is Nothing Then
                        If IsStream(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                            PlayStream(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            PlayFile(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayPreviousLinear")
                        End If
                    ElseIf item.Index = 0 Then
                        If IsStream(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                            PlayStream(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            PlayFile(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayPreviousLinear")
                        End If
                    Else
                        newindex = item.Index - 1
                        If IsStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                            PlayStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            PlayFile(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayPreviousLinear")
                        End If
                    End If
                    EnsurePlaylistItemIsVisible(newindex)
                End If
            Case PlayModes.Random
                If RandomHistory.Count > 0 Then
                    If LVPlaylist.Items.Count > 0 Then
                        If RandomHistoryIndex > 0 Then
                            RandomHistoryIndex -= 1
                        Else
                            RandomHistoryIndex = RandomHistory.Count - 1
                        End If
                        If RandomHistory.Item(RandomHistoryIndex) = _player.Path Then
                            'Already Playing Previous Random
                            RandomHistoryIndex -= 1
                            If RandomHistoryIndex < 0 Then
                                RandomHistoryIndex = RandomHistory.Count - 1
                            End If
                        End If
                        Dim item As ListViewItem
                        Try
                            item = LVPlaylist.FindItemWithText(RandomHistory.Item(RandomHistoryIndex), True, 0)
                        Catch
                            item = Nothing
                        End Try
                        If item IsNot Nothing Then
                            If IsStream(item.SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                                PlayStream(item.SubItems(LVPlaylist.Columns("Path").Index).Text)
                            Else
                                PlayFile(item.SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayPreviousRandom")
                            End If
                            EnsurePlaylistItemIsVisible(item.Index)
                        End If
                    End If
                End If
        End Select
    End Sub
    Friend Sub PlayNext()
        'Stream = False
        StopPlay()
        LyricsOff()
        Select Case App.PlayMode
            Case PlayModes.Repeat
                TogglePlay()
                TimerShowMedia.Start()
            Case PlayModes.Linear
                If Queue.Count > 0 Then
                    PlayQueued()
                Else
                    If LVPlaylist.Items.Count > 0 Then
                        Dim item As ListViewItem
                        If LVPlaylist.SelectedItems.Count > 0 Then
                            item = LVPlaylist.FindItemWithText(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            If _player.Path Is Nothing OrElse _player.Path = String.Empty Then
                                item = Nothing
                            Else
                                item = LVPlaylist.FindItemWithText(_player.Path, True, 0)
                            End If
                        End If
                        Dim newindex As Integer = 0
                        If item Is Nothing OrElse item.Index + 1 = LVPlaylist.Items.Count Then
                            If IsStream(LVPlaylist.Items(0).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                                PlayStream(LVPlaylist.Items(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
                            Else
                                PlayFile(LVPlaylist.Items(0).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayNextLinear")
                            End If
                        Else
                            newindex = item.Index + 1
                            If IsStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                                PlayStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                            Else
                                PlayFile(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayNextLinear")
                            End If
                        End If
                        EnsurePlaylistItemIsVisible(newindex)
                        TimerShowMedia.Start()
                    End If
                End If
            Case PlayModes.Random
                If Queue.Count > 0 Then
                    PlayQueued()
                Else
                    If LVPlaylist.Items.Count > 0 Then
                        Dim item As ListViewItem = Nothing
                        If _player.HasMedia Then LVPlaylist.FindItemWithText(_player.Path, True, 0)
                        Dim newindex As Integer
                        If RandomHistoryFull() Then RandomHistory.Clear()
                        If item Is Nothing Then
                            newindex = Skye.Common.GetRandom(0, LVPlaylist.Items.Count - 1)
                        Else
                            If LVPlaylist.Items.Count = 1 Then
                                newindex = 0
                            Else
                                Do
                                    newindex = Skye.Common.GetRandom(0, LVPlaylist.Items.Count - 1)
                                Loop Until newindex <> item.Index And Not RandomHistory.Contains(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                            End If
                        End If
                        If IsStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                            PlayStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            PlayFile(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayNextRandom")
                        End If
                        EnsurePlaylistItemIsVisible(newindex)
                        TimerShowMedia.Start()
                    End If
                End If
        End Select
    End Sub
    Private Sub OnPlay()
        ' 🧠 Playback lifecycle is sensitive:
        ' - LibVLC creates/destroys native windows asynchronously
        ' - UI transitions (fullscreen, hooks) must be marshaled and timed carefully
        ' - Avoid cross-thread reparenting or teardown collisions
        PlayState = PlayStates.Playing

        'Update The Histories
        UpdateHistory(_player.Path.TrimEnd("/"c)) 'Trimming is needed for uniformity.
        If PausedAt IsNot Nothing Then
            Dim pausedDuration = DateTime.Now - PausedAt.Value
            TotalPausedDuration += pausedDuration
            PausedAt = Nothing
            Debug.Print("Resumed, total paused: " & TotalPausedDuration.TotalSeconds & "s")
        End If
        If App.SongPlayData.StartPlayTime = DateTime.MinValue Then 'Because of OnPlay happening on seek.
            App.SongPlayData.Path = _player.Path.TrimEnd("/"c)
            App.SongPlayData.StartPlayTime = Now
        End If

        BtnPlay.Image = App.CurrentTheme.PlayerPause
        If App.FrmMiniPlayer IsNot Nothing Then App.FrmMiniPlayer.SetPlayState()
        TrackBarPosition.Maximum = CInt(_player.Duration * TrackBarScale)
        If Not TrackBarPosition.Enabled AndAlso Not CurrentMediaType = App.MediaSourceTypes.Stream Then TrackBarPosition.Enabled = True
        LblDuration.Text = FormatDuration(_player.Duration)
        ShowPosition()
        HasLyrics = False
        HasLyricsSynced = False
        Try
            Select Case CurrentMediaType
                Case App.MediaSourceTypes.AudioCD
                    ' not implemented
                Case App.MediaSourceTypes.Stream
                    Dim path As String = _player.Path.TrimEnd("/"c)
                    Dim lvi = LVPlaylist.FindItemWithText(path, True, 0)
                    PlaylistCurrentText = lvi.Text
                    Text = My.Application.Info.Title + " - " + lvi.Text + " @ " + path
                    App.NIApp.Text = My.Application.Info.Title + " - " + lvi.Text
                Case App.MediaSourceTypes.File
                    Dim lvi = LVPlaylist.FindItemWithText(_player.Path, True, 0)
                    PlaylistCurrentText = lvi.Text
                    Text = My.Application.Info.Title + " - " + lvi.Text + " @ " + _player.Path
                    App.NIApp.Text = My.Application.Info.Title + " - " + lvi.Text
                    LoadLyrics(_player.Path)
            End Select
        Catch
            PlaylistCurrentText = Path.GetFileNameWithoutExtension(_player.Path)
            Text = My.Application.Info.Title + " - " + _player.Path
            App.NIApp.Text = Text
        End Try
        RaiseEvent TitleChanged(PlaylistCurrentText)

        TimerShowMedia.Start()
    End Sub
    Private Sub OnPause()
        PlayState = PlayStates.Paused
        If PausedAt Is Nothing Then
            PausedAt = DateTime.Now
            Debug.Print("Paused at: " & PausedAt.ToString())
        End If
        BtnPlay.Image = App.CurrentTheme.PlayerPlay
        If App.FrmMiniPlayer IsNot Nothing Then App.FrmMiniPlayer.SetPlayState()
    End Sub
    Private Sub OnStop()
        PlayState = PlayStates.Stopped
        App.StopHistoryUpdates()

        'Save Song Play Data and reset
        If PausedAt IsNot Nothing Then
            Dim finalPause = DateTime.Now - PausedAt.Value
            TotalPausedDuration += finalPause
            PausedAt = Nothing
            Debug.Print("Final pause added: total paused: " & TotalPausedDuration.TotalSeconds & "s")
        End If
        App.SongPlayData.StopPlayTime = Now
        If App.SongPlayData.IsValid Then App.LogPlayHistory(App.SongPlayData.Path, App.SongPlayData.StartPlayTime, App.SongPlayData.StopPlayTime, CInt((App.SongPlayData.StopPlayTime - App.SongPlayData.StartPlayTime - TotalPausedDuration).TotalSeconds), App.SongPlayData.PlayTrigger)
        Debug.Print("LOGGING PLAY: " + App.SongPlayData.Path + " | " + App.SongPlayData.IsValid.ToString + " | " + App.SongPlayData.StartPlayTime.ToString + " - " + App.SongPlayData.StopPlayTime.ToString + " | Trigger: " + App.SongPlayData.PlayTrigger.ToString)
        App.SongPlayData = New App.PlayData
        TotalPausedDuration = TimeSpan.Zero
        If AutoNext Then
            Select Case App.PlayMode
                Case App.PlayModes.Repeat
                    App.SongPlayData.PlayTrigger = App.PlayTriggers.Repeat
                Case App.PlayModes.Linear
                    App.SongPlayData.PlayTrigger = App.PlayTriggers.Linear
                Case App.PlayModes.Random
                    App.SongPlayData.PlayTrigger = App.PlayTriggers.Random
            End Select
            AutoNext = False
        Else
            App.SongPlayData.PlayTrigger = App.PlayTriggers.Manual
        End If

        BtnPlay.Image = App.CurrentTheme.PlayerPlay
        TrackBarPosition.Value = 0
        TrackBarPosition.Enabled = False
        PEXLeft.Value = 0
        PEXRight.Value = 0
        ResetLblPositionText()
        If FullScreen Then FullScreen = False
        VLCViewer.Visible = False
    End Sub
    Private Sub ShowPosition()
        Try
            If My.App.PlayerPositionShowElapsed Then
                LblPosition.Text = FormatPosition(_player.Position)
            Else
                If CurrentMediaType = App.MediaSourceTypes.Stream Then
                    LblPosition.Text = "00:00"
                Else
                    LblPosition.Text = FormatPosition(_player.Duration - _player.Position)
                End If
            End If
            If Not CurrentMediaType = App.MediaSourceTypes.Stream Then TrackBarPosition.Value = CInt(_player.Position * TrackBarScale)
        Catch
            TrackBarPosition.Value = 0
            LblPosition.Text = "00:00"
        End Try
    End Sub
    Private Sub UpdatePosition(ByVal forward As Boolean, Optional ByVal seconds As Byte = 20)
        If _player.HasMedia AndAlso PlayState = PlayStates.Playing Then
            If forward Then
                If _player.Position + seconds > _player.Duration Then
                    _player.Position = _player.Duration
                Else
                    _player.Position += seconds
                End If
            Else
                If _player.Position <= seconds Then
                    _player.Position = 0
                Else
                    _player.Position -= seconds
                End If
            End If
            ShowPosition()
            LVPlaylist.Focus()
        End If
    End Sub
    Private Sub ToggleMute()
        If Mute Then
            Debug.Print("Enabling Sound")
            _player.Volume = 100
            BtnMute.Image = Resources.ImagePlayerSound
            Mute = False
        Else
            Debug.Print("Disabling Sound")
            _player.Volume = 0
            BtnMute.Image = Resources.ImagePlayerSoundMute
            Mute = True
        End If
    End Sub
    Private Sub VideoSetSize()
        If _player IsNot Nothing AndAlso _player.HasMedia AndAlso App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(_player.Path)) Then
            If _player.VideoHeight / _player.VideoWidth > PanelMedia.Height / PanelMedia.Width Then
                'Height > Width, Set Height then Get Width, then Set Centers
                VLCViewer.Width = VideoGetWidth(PanelMedia.Height)
                VLCViewer.Height = PanelMedia.Height
                VLCViewer.Left = CInt(PanelMedia.Right - ((PanelMedia.Right - PanelMedia.Left) / 2) - ((VLCViewer.Right - VLCViewer.Left) / 2))
                VLCViewer.Top = CInt(PanelMedia.Bottom - ((PanelMedia.Bottom - PanelMedia.Top) / 2) - ((VLCViewer.Bottom - VLCViewer.Top) / 2))
            Else
                'Width > Height, Set Width then Get Height, then Set Centers
                VLCViewer.Width = PanelMedia.Width
                VLCViewer.Height = VideoGetHeight(PanelMedia.Width)
                VLCViewer.Left = CInt(PanelMedia.Right - ((PanelMedia.Right - PanelMedia.Left) / 2) - ((VLCViewer.Right - VLCViewer.Left) / 2))
                VLCViewer.Top = CInt(PanelMedia.Bottom - ((PanelMedia.Bottom - PanelMedia.Top) / 2) - ((VLCViewer.Bottom - VLCViewer.Top) / 2))
            End If
        End If
    End Sub
    Friend Sub ShowMedia()
        If _player.HasMedia Then
            Dim tlfile As TagLib.File
            Try
                If CurrentMediaType = App.MediaSourceTypes.Stream Then
                    tlfile = Nothing
                Else
                    tlfile = TagLib.File.Create(_player.Path)
                End If
            Catch ex As Exception
                WriteToLog("TagLib Error while Showing Media, Cannot read from file: " + _player.Path + Chr(13) + ex.Message)
                tlfile = Nothing
            End Try
            If Lyrics AndAlso Not CurrentMediaType = App.MediaSourceTypes.Stream Then 'Show Lyrics
                Debug.Print("Showing Lyrics...")
                PicBoxAlbumArt.Visible = False
                PicBoxAlbumArt.Image = Nothing
                LblMedia.Visible = False
                PanelVisualizer.Visible = False
                VisualizerEngine?.Stop()
                Visualizer = False
                VLCViewer.Visible = False
                If WindowState = FormWindowState.Maximized Then
                    RTBLyrics.Font = New Font(RTBLyrics.Font.FontFamily, 20, FontStyle.Regular)
                Else
                    RTBLyrics.Font = New Font(RTBLyrics.Font.FontFamily, 12, FontStyle.Regular)
                End If
                If HasLyricsSynced Then
                    RTBLyrics.Lines = LyricsSynced.Select(Function(l) l.Text).ToArray()
                Else
                    RTBLyrics.Text = LyricsText
                End If
                RTBLyrics.SetAlignment(HorizontalAlignment.Center)
                RTBLyrics.Visible = True
            Else
                If Visualizer Then
                    PicBoxAlbumArt.Visible = False
                    PicBoxAlbumArt.Image = Nothing
                    RTBLyrics.Visible = False
                    VLCViewer.Visible = False
                Else
                    If App.AudioExtensionDictionary.ContainsKey(Path.GetExtension(_player.Path)) Then 'Show Album Art
                        VLCViewer.Visible = False
                        PanelVisualizer.Visible = False
                        VisualizerEngine?.Stop()
                        RTBLyrics.Visible = False
                        If tlfile Is Nothing Then
                            PicBoxAlbumArt.Visible = False
                            PicBoxAlbumArt.Image = Nothing
                            App.FrmMiniPlayer?.SetAlbumArt(Nothing)
                        Else
                            If tlfile.Tag.Pictures.Length = 0 Then
                                PicBoxAlbumArt.Visible = False
                                PicBoxAlbumArt.Image = Nothing
                                App.FrmMiniPlayer?.SetAlbumArt(Nothing)
                            Else
                                Debug.Print("Showing Album Art...")
                                If AlbumArtIndex + 1 > tlfile.Tag.Pictures.Count Then AlbumArtIndex = 0
                                Dim ms As New IO.MemoryStream(tlfile.Tag.Pictures(AlbumArtIndex).Data.Data)
                                Try
                                    Dim img As Drawing.Image = Drawing.Image.FromStream(ms)
                                    PicBoxAlbumArt.Image = img
                                    App.FrmMiniPlayer?.SetAlbumArt(img)
                                    PicBoxAlbumArt.Visible = True
                                Catch ex As Exception
                                    WriteToLog("Error Loading Album Art for " + _player.Path + vbCr + ex.Message)
                                    PicBoxAlbumArt.Visible = False
                                    PicBoxAlbumArt.Image = Nothing
                                    App.FrmMiniPlayer?.SetAlbumArt(Nothing)
                                End Try
                                ms.Dispose()
                                ms = Nothing
                                AlbumArtCount = CByte(tlfile.Tag.Pictures.Count)
                                PicBoxAlbumArt.Invalidate()
                            End If
                        End If
                    ElseIf App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(_player.Path)) Then 'Show Video
                        Debug.Print("Showing Video...")
                        PicBoxAlbumArt.Visible = False
                        PicBoxAlbumArt.Image = Nothing
                        App.FrmMiniPlayer?.SetAlbumArt(Nothing)
                        RTBLyrics.Visible = False
                        PanelVisualizer.Visible = False
                        VisualizerEngine?.Stop()
                        VideoSetSize()
                        VLCViewer.Visible = True
                    End If
                End If
                If Visualizer OrElse (Not VLCViewer.Visible AndAlso Not PicBoxAlbumArt.Visible) OrElse CurrentMediaType = App.MediaSourceTypes.Stream Then 'Show Visualizer
                    Debug.Print("Showing Visualizer...")
                    VLCViewer.Visible = False
                    PicBoxAlbumArt.Visible = False
                    PicBoxAlbumArt.Image = Nothing
                    RTBLyrics.Visible = False
                    VisualizerHost.Activate(App.Visualizer)
                    VisualizerEngine?.Start()
                    PanelVisualizer.Visible = True
                    PanelVisualizer.BringToFront()
                End If
                'Show Lyrics Menu Button
                If HasLyrics Then
                    MILyrics.Visible = True
                Else
                    MILyrics.Visible = False
                End If
            End If
            tlfile = Nothing
        Else
            PicBoxAlbumArt.Visible = False
            RTBLyrics.Visible = False
            VLCViewer.Visible = False
            PanelVisualizer.Visible = False
            VisualizerEngine?.Stop()
        End If
    End Sub

    'FullScreen
    Private Sub SetFullScreen()
        ' ⚠️ Handle with care: this method re-parents VLCViewer between forms.
        ' Must only be called from UI thread — use BeginInvoke if needed.
        ' Avoid calling during LibVLC teardown (e.g. OnPlaybackEnded).
        ' Reparenting while VLCViewer's native window is invalid causes hangs.

        If Me.InvokeRequired Then
            Me.BeginInvoke(New MethodInvoker(AddressOf SetFullScreen))
            Return
        End If

        If FullScreen Then
            LyricsOff()
            VisualizerOff()
            ShowMedia()
            If frmFullScreen Is Nothing Then

                'Save original parent and bounds
                originalParent = VLCViewer.Parent
                originalBounds = VLCViewer.Bounds

                'Create fullscreen host form
                frmFullScreen = New FullScreen With {
                    .FormBorderStyle = FormBorderStyle.None,
                    .WindowState = FormWindowState.Maximized,
                    .BackColor = Color.Black,
                    .TopMost = True,
                    .KeyPreview = True} 'So ESC is caught

                'Handle ESC key
                AddHandler frmFullScreen.KeyDown, AddressOf FullScreen_KeyDown

                'Reparent the VideoView into the fullscreen form
                frmFullScreen.Controls.Add(VLCViewer)
                VLCViewer.Dock = DockStyle.Fill

                frmFullScreen.Show()
                frmFullScreen.Focus()

            End If
        Else
            If frmFullScreen IsNot Nothing Then

                'Reparent the VideoView back to original parent
                If VLCViewer.IsHandleCreated AndAlso Not VLCViewer.IsDisposed Then
                    frmFullScreen.Controls.Remove(VLCViewer)
                    originalParent.Controls.Add(VLCViewer)
                    VLCViewer.Dock = DockStyle.None
                    VLCViewer.Bounds = originalBounds
                End If

                'Dispose of fullscreen form
                frmFullScreen.Close()
                frmFullScreen.Dispose()
                frmFullScreen = Nothing

            End If
        End If

    End Sub
    Private Sub FullScreen_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Escape OrElse e.KeyCode = Keys.F OrElse e.KeyCode = Keys.F11 Then FullScreen = False
    End Sub

    'Lryics
    Private Sub LoadLyrics(songPath As String)
        LyricsText = String.Empty
        LyricsSynced = Nothing
        LastLyricsIndex = -1

        Dim lrcPath = IO.Path.ChangeExtension(songPath, ".lrc")
        Dim txtPath = IO.Path.ChangeExtension(songPath, ".txt")

        '1. Try LRC file
        If IO.File.Exists(lrcPath) Then
            Dim lyrics As String = IO.File.ReadAllText(lrcPath)
            LyricsText = ExtractPlainLyrics(lyrics)
            LyricsSynced = ParseLRC(lyrics)
            HasLyrics = True
            HasLyricsSynced = True
            Debug.Print("Loaded Synced Lyrics from " + lrcPath)
            Exit Sub
        End If

        '2. Try TXT file
        If IO.File.Exists(txtPath) Then
            LyricsText = IO.File.ReadAllText(txtPath)
            HasLyrics = True
            Debug.Print("Loaded Lyrics from " + txtPath)
            Exit Sub
        End If

        '3. Try metadata
        Try
            Using f = TagLib.File.Create(songPath)
                If Not String.IsNullOrEmpty(f.Tag.Lyrics) Then
                    LyricsText = f.Tag.Lyrics
                    HasLyrics = True
                    Debug.Print("Loaded Lyrics from metadata for " + songPath)
                End If
            End Using
        Catch ex As Exception
            Debug.Print("Error loading lyrics from metadata for " + songPath + vbCr + ex.Message)
        End Try
    End Sub
    Private Function ParseLRC(lrcContent As String) As List(Of TimedLyric)
        Dim result As New List(Of TimedLyric)
        'Regex now matches ALL [mm:ss.xx] tags in a line
        Dim rx As New System.Text.RegularExpressions.Regex("\[(\d{2}):(\d{2})\.(\d{2})\]")

        For Each line In lrcContent.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
            'Find all timestamps in this line
            Dim matches = rx.Matches(line)
            If matches.Count > 0 Then
                'The lyric text is whatever comes after the last timestamp
                Dim lyric = line.Substring(matches(matches.Count - 1).Index + matches(matches.Count - 1).Length).Trim()

                For Each m As System.Text.RegularExpressions.Match In matches
                    Dim minutes = Integer.Parse(m.Groups(1).Value)
                    Dim seconds = Integer.Parse(m.Groups(2).Value)
                    Dim hundredths = Integer.Parse(m.Groups(3).Value)

                    'hundredths → milliseconds
                    Dim ts = New TimeSpan(0, 0, minutes, seconds, hundredths * 10)
                    result.Add(New TimedLyric With {.Time = ts, .Text = lyric})
                Next
            End If
        Next

        result.Sort(Function(a, b) a.Time.CompareTo(b.Time))
        Return result
    End Function
    Private Function ExtractPlainLyrics(lrcContent As String) As String
        Dim lines As New List(Of String)

        For Each line In lrcContent.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
            ' Remove all [mm:ss.xx] tags
            Dim plain = System.Text.RegularExpressions.Regex.Replace(line, "\[[0-9:\.]+\]", "").Trim()
            If plain.Length > 0 Then
                lines.Add(plain)
            End If
        Next

        Return String.Join(Environment.NewLine, lines)
    End Function
    Private Sub LyricsOn()
        If Not Lyrics Then
            Lyrics = True
            VisualizerOff()
            MILyrics.BackColor = Skye.WinAPI.GetSystemColor(Skye.WinAPI.COLOR_HIGHLIGHT)
        End If
    End Sub
    Friend Sub LyricsOff()
        If Lyrics Then
            Lyrics = False
            MILyrics.BackColor = Color.Transparent
        End If
    End Sub

    'Themes
    Private Sub SetAccentColor(Optional force As Boolean = False)
        Dim accent As Color = App.GetAccentColor()
        If CurrentAccentColor <> accent OrElse force Then
            CurrentAccentColor = accent
            SuspendLayout()
            SetActiveTitleBarColor()
            If App.CurrentTheme.IsAccent Then
                BackColor = CurrentAccentColor
                RTBLyrics.BackColor = CurrentAccentColor
                TrackBarPosition.TrackBarGradientStart = CurrentAccentColor
                TrackBarPosition.TrackBarGradientEnd = CurrentAccentColor
            End If
            ResumeLayout()
            'Debug.Print("Player Accent Color Set")
            Skye.WinAPI.RedrawWindow(Me.Handle, IntPtr.Zero, IntPtr.Zero, Skye.WinAPI.RDW_INVALIDATE Or Skye.WinAPI.RDW_ERASE Or Skye.WinAPI.RDW_FRAME Or Skye.WinAPI.RDW_ALLCHILDREN Or Skye.WinAPI.RDW_UPDATENOW)
            'Debug.Print("Player Repainted")
        End If
    End Sub
    Private Sub SetActiveTitleBarColor()
        If IsFocused And CurrentAccentColor <> Nothing Then
            MenuPlayer.BackColor = CurrentAccentColor
            TxtBoxPlaylistSearch.BackColor = CurrentAccentColor
            'Debug.Print("Player Active Title Bar Color Set: " + CurrentAccentColor.ToString)
        End If
    End Sub
    Private Sub SetInactiveTitleBarColor()
        If Not IsFocused Then
            MenuPlayer.BackColor = App.CurrentTheme.InactiveTitleBarColor
            TxtBoxPlaylistSearch.BackColor = App.CurrentTheme.InactiveTitleBarColor
            'Debug.Print("Player InActive Title Bar Color Set")
        End If
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            LblPlaylistCount.ForeColor = App.CurrentTheme.AccentTextColor
            LblDuration.ForeColor = App.CurrentTheme.AccentTextColor
            LblPosition.ForeColor = App.CurrentTheme.AccentTextColor
            RTBLyrics.ForeColor = App.CurrentTheme.AccentTextColor
            LblMedia.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            TrackBarPosition.TrackBarGradientStart = App.CurrentTheme.BackColor
            TrackBarPosition.TrackBarGradientEnd = App.CurrentTheme.BackColor
            LblPlaylistCount.ForeColor = App.CurrentTheme.TextColor
            LblDuration.ForeColor = App.CurrentTheme.TextColor
            LblPosition.ForeColor = App.CurrentTheme.TextColor
            RTBLyrics.BackColor = App.CurrentTheme.BackColor
            RTBLyrics.ForeColor = App.CurrentTheme.TextColor
            LblMedia.ForeColor = App.CurrentTheme.TextColor
        End If
        LVPlaylist.BackColor = App.CurrentTheme.BackColor
        LVPlaylist.ForeColor = App.CurrentTheme.TextColor
        LVPlaylist.InsertionLineColor = App.CurrentTheme.TextColor
        ListBoxPlaylistSearch.BackColor = App.CurrentTheme.BackColor
        ListBoxPlaylistSearch.ForeColor = App.CurrentTheme.TextColor
        BtnPlay.BackColor = App.CurrentTheme.ButtonBackColor
        BtnStop.BackColor = App.CurrentTheme.ButtonBackColor
        BtnReverse.BackColor = App.CurrentTheme.ButtonBackColor
        BtnForward.BackColor = App.CurrentTheme.ButtonBackColor
        BtnPrevious.BackColor = App.CurrentTheme.ButtonBackColor
        BtnNext.BackColor = App.CurrentTheme.ButtonBackColor
        BtnMute.BackColor = App.CurrentTheme.ButtonBackColor
        PEXLeft.DrawingColor = App.CurrentTheme.TextColor
        PEXRight.DrawingColor = App.CurrentTheme.TextColor
        TrackBarPosition.ButtonColor = App.CurrentTheme.ButtonBackColor
        TrackBarPosition.HighlightedButtonColor = App.CurrentTheme.ButtonBackColor
        TrackBarPosition.PushedButtonEndColor = App.CurrentTheme.TextColor
        If TxtBoxPlaylistSearch.Text = PlaylistSearchTitle Then TxtBoxPlaylistSearch.ForeColor = App.CurrentTheme.InactiveSearchTextColor 'Set the search box inactive text color
        If PlayState = PlayStates.Playing Then
            BtnPlay.Image = App.CurrentTheme.PlayerPause
        Else
            BtnPlay.Image = App.CurrentTheme.PlayerPlay
        End If
        BtnStop.Image = App.CurrentTheme.PlayerStop
        BtnNext.Image = App.CurrentTheme.PlayerNext
        BtnPrevious.Image = App.CurrentTheme.PlayerPrevious
        BtnForward.Image = App.CurrentTheme.PlayerFastForward
        BtnReverse.Image = App.CurrentTheme.PlayerFastReverse
        TipPlayer.BackColor = App.CurrentTheme.BackColor
        TipPlayer.ForeColor = App.CurrentTheme.TextColor
        TipPlayer.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
        'Debug.Print("Player Theme Set")
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor(True)
        SetTheme()
    End Sub
    Friend Sub ReThemeMenus()
        App.ThemeMenu(MenuPlayer)
        App.ThemeMenu(CMPlaylist)
        App.ThemeMenu(CMRatings)
        VisualizerHost.SetVisualizersMenu()
    End Sub
    Private Sub CustomDrawCMToolTip(MyToolStrip As ToolStrip)

        'Initialize
        Dim MyField As Reflection.PropertyInfo = MyToolStrip.GetType().GetProperty("ToolTip", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
        Dim MyToolTip As ToolTip = CType(MyField.GetValue(MyToolStrip), ToolTip)

        'Configure ToolTip
        MyToolTip.OwnerDraw = True

        'Draw
        AddHandler MyToolTip.Popup,
            Sub(sender, e)
                Dim s As SizeF
                s = TextRenderer.MeasureText(CType(sender, ToolTip).GetToolTip(e.AssociatedControl), TipPlaylistFont)
                s.Width += 14
                s.Height += 16
                e.ToolTipSize = s.ToSize
            End Sub
        AddHandler MyToolTip.Draw,
            Sub(sender, e)

                'Declarations
                Dim g As Graphics = e.Graphics

                'Draw background
                Dim brbg As New SolidBrush(App.CurrentTheme.BackColor)
                g.FillRectangle(brbg, e.Bounds)

                'Draw border
                Using p As New Pen(App.CurrentTheme.ButtonBackColor, CInt(TipPlaylistFont.Size / 4)) 'Scale border thickness with font
                    g.DrawRectangle(p, 0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1)
                End Using

                'Draw text
                TextRenderer.DrawText(g, e.ToolTipText, TipPlaylistFont, New Point(7, 7), App.CurrentTheme.TextColor)

                'Finalize
                brbg.Dispose()
                g.Dispose()

            End Sub

    End Sub

End Class
