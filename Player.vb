﻿
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Runtime.CompilerServices
Imports AxWMPLib
Imports Skye
Imports SkyeMusic.My

Public Class Player

    'Declarations
    Private Enum PlayStates
        Playing
        Paused
        Stopped
    End Enum
    Public Structure PlaylistItemType
        Public Title As String
        Public Path As String
    End Structure
    Friend Queue As New Generic.List(Of String) 'Queue of items to play
    Private aDevEnum As New CoreAudio.MMDeviceEnumerator 'Audio Device Enumerator
    Private aDev As CoreAudio.MMDevice = aDevEnum.GetDefaultAudioEndpoint(CoreAudio.EDataFlow.eRender, CoreAudio.ERole.eMultimedia) 'Default Audio Device
    Private cLeftMeter, cRightMeter, visR, visB As Byte 'Meter Values and Visualizer Colors
    Private visV As Single 'Visualizer Volume
    Private PlayState As PlayStates = PlayStates.Stopped 'Status of the currently playing song
    Private Stream As Boolean = False 'True if the current playing item is a stream
    Private Mute As Boolean = False 'True if the player is muted
    Private IsFocused As Boolean = True 'Indicates if the player is focused
    Private Visualizer As Boolean = False 'Indicates if the visualizer is active
    Private Lyrics As Boolean = False 'Indicates if the lyrics are active
    Private AlbumArtIndex As Byte = 0 'Index of the current album art
    Private TrackBarScale As Int16 = 1000 'TrackBar Scale for Position
    Private PlaylistItemMove As ListViewItem 'Item being moved in the playlist
    Private PlaylistSearchTitle As String 'Title for Playlist Search
    Private PlaylistSearchItems As New List(Of ListViewItem) 'Items found in the playlist search
    Private RandomHistory As New Generic.List(Of String) 'History of played items for shuffle play mode
    Private RandomHistoryIndex As Integer = 0 'Index for the shuffle history
    Private PlaylistBoldFont As Font 'Bold font for playlist titles
    Private CurrentAccentColor As Color 'Current Windows Accent Color
    Private mMove As Boolean = False
    Private mOffset, mPosition As System.Drawing.Point

    'Sort Orders
    Private PlaylistTitleSort As SortOrder = SortOrder.None
    Private PlaylistPathSort As SortOrder = SortOrder.None
    Private PlaylistRatingSort As SortOrder = SortOrder.None
    Private PlaylistPlayCountSort As SortOrder = SortOrder.None
    Private PlaylistLastPlayedSort As SortOrder = SortOrder.None
    Private PlaylistFirstPlayedSort As SortOrder = SortOrder.None
    Private PlaylistAddedSort As SortOrder = SortOrder.None

    'Properties
    Friend ReadOnly Property Fullscreen As Boolean 'Property to check if the player is in fullscreen mode
        Get
            Return AxPlayer.fullScreen
        End Get
    End Property
    Private _watchernotification As String
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Friend Property WatcherNotification As String
        Get
            Return _watchernotification
        End Get
        Set(value As String)
            _watchernotification = value
            If String.IsNullOrWhiteSpace(_watchernotification) Then
                MILibrary.Font = New Font(MILibrary.Font, FontStyle.Regular)
                MILibrary.Text = StrConv(MILibrary.Text, VbStrConv.ProperCase)
            Else
                MILibrary.Text = MILibrary.Text.ToUpper
                MILibrary.Font = New Font(MILibrary.Font, FontStyle.Bold Or FontStyle.Underline)
            End If
        End Set
    End Property

    'Interface
    Private _player As Skye.Contracts.IMediaPlayer
    Public Class WMPlayer
        Implements Skye.Contracts.IMediaPlayer

        Private ReadOnly _wmp As AxWMPLib.AxWindowsMediaPlayer

        Public Sub New(wmpControl As AxWMPLib.AxWindowsMediaPlayer)
            _wmp = wmpControl
            AddHandler _wmp.PlayStateChange, AddressOf OnPlayStateChange
        End Sub

        Public Sub Play(path As String) Implements Skye.Contracts.IMediaPlayer.Play
            _wmp.URL = path
        End Sub
        Public Sub Play(uri As Uri) Implements Skye.Contracts.IMediaPlayer.Play
            _wmp.URL = uri.AbsoluteUri
        End Sub
        Public Sub Play() Implements Skye.Contracts.IMediaPlayer.Play
            _wmp.Ctlcontrols.play()
        End Sub
        Public Sub Pause() Implements Skye.Contracts.IMediaPlayer.Pause
            _wmp.Ctlcontrols.pause()
        End Sub
        Public Sub [Stop]() Implements Skye.Contracts.IMediaPlayer.Stop
            _wmp.Ctlcontrols.stop()
        End Sub

        Public ReadOnly Property HasMedia As Boolean Implements Skye.Contracts.IMediaPlayer.HasMedia
            Get
                Return _wmp.currentMedia IsNot Nothing
            End Get
        End Property
        Public ReadOnly Property Path As String Implements Skye.Contracts.IMediaPlayer.Path
            Get
                Return _wmp.URL
            End Get
        End Property
        Public Property Volume As Integer Implements Skye.Contracts.IMediaPlayer.Volume
            Get
                Return _wmp.settings.volume
            End Get
            Set(value As Integer)
                _wmp.settings.volume = value
            End Set
        End Property
        Public Property Position As Double Implements Skye.Contracts.IMediaPlayer.Position
            Get
                Return _wmp.Ctlcontrols.currentPosition
            End Get
            Set(value As Double)
                _wmp.Ctlcontrols.currentPosition = value
            End Set
        End Property
        Public ReadOnly Property Duration As Double Implements Skye.Contracts.IMediaPlayer.Duration
            Get
                If _wmp.currentMedia IsNot Nothing Then
                    Return _wmp.currentMedia.duration
                Else
                    Return 0
                End If
            End Get
        End Property
        Public ReadOnly Property VideoWidth As Integer Implements Skye.Contracts.IMediaPlayer.VideoWidth
            Get
                If _wmp.currentMedia IsNot Nothing Then
                    Return _wmp.currentMedia.imageSourceWidth
                Else
                    Return 0
                End If
            End Get
        End Property
        Public ReadOnly Property VideoHeight As Integer Implements Skye.Contracts.IMediaPlayer.VideoHeight
            Get
                If _wmp.currentMedia IsNot Nothing Then
                    Return _wmp.currentMedia.imageSourceHeight
                Else
                    Return 0
                End If
            End Get
        End Property
        Public ReadOnly Property AspectRatio As Double Implements Skye.Contracts.IMediaPlayer.AspectRatio
            Get
                If _wmp.currentMedia IsNot Nothing AndAlso _wmp.currentMedia.imageSourceHeight > 0 Then
                    Return _wmp.currentMedia.imageSourceWidth / _wmp.currentMedia.imageSourceHeight
                Else
                    Return 0
                End If
            End Get
        End Property

        Private Sub OnPlayStateChange(sender As Object, e As _WMPOCXEvents_PlayStateChangeEvent)
            Select Case e.newState
                Case WMPLib.WMPPlayState.wmppsPlaying
                    RaiseEvent PlaybackStarted()
                Case WMPLib.WMPPlayState.wmppsMediaEnded
                    RaiseEvent PlaybackEnded()
            End Select
        End Sub

        Public Event PlaybackStarted() Implements Skye.Contracts.IMediaPlayer.PlaybackStarted
        Public Event PlaybackEnded() Implements Skye.Contracts.IMediaPlayer.PlaybackEnded

    End Class

    'Form Events                    
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_HOTKEY
                    Debug.Print("HOTKEY " + m.WParam.ToString + " PRESSED")
                    App.PerformHotKeyAction(m.WParam.ToInt32)
                Case WinAPI.WM_SYSCOMMAND
                    If m.WParam.ToInt32() = WinAPI.SC_MINIMIZE Then
                        If CMPlaylist IsNot Nothing AndAlso CMPlaylist.Visible Then
                            WinAPI.PostMessage(CMPlaylist.Handle, WinAPI.WM_CLOSE, IntPtr.Zero, IntPtr.Zero) 'Closes the Playlist Context Menu on minimize so it doesn't linger upon restore.
                            TipPlaylist.HideTooltip()
                        End If
                    End If
                Case Skye.WinAPI.WM_ACTIVATE
                    Select Case m.WParam.ToInt32
                        Case 0
                            IsFocused = False
                            Debug.Print("Player Lost Focus")
                            SetInactiveTitleBarColor()
                        Case 1, 2
                            IsFocused = True
                            Debug.Print("Player Got Focus")
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

        _player = New WMPlayer(AxPlayer)
        AddHandler _player.PlaybackStarted, AddressOf OnPlaybackStarted
        AddHandler _player.PlaybackEnded, AddressOf OnPlaybackEnded

        Text = Application.Info.Title 'Set the form title
        PlaylistSearchTitle = TxtBoxPlaylistSearch.Text 'Default search title
        PlaylistBoldFont = New Font(LVPlaylist.Font, FontStyle.Bold) 'Bold font for playlist titles
        TrackBarPosition.Size = New Size(TrackBarPosition.Size.Width, 26)

        'Initialize Listview
        Dim header As ColumnHeader
        header = New ColumnHeader()
        header.Name = "Title"
        header.Text = "Title"
        header.Width = 300
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Path"
        header.Text = "Path"
        header.Width = 550
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Rating"
        header.Text = "Rating"
        header.Width = 70
        header.TextAlign = HorizontalAlignment.Center
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "PlayCount"
        header.Text = "Plays"
        header.Width = 60
        header.TextAlign = HorizontalAlignment.Center
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "LastPlayed"
        header.Text = "Last Played"
        header.Width = 180
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "FirstPlayed"
        header.Text = "First Played"
        header.Width = 180
        LVPlaylist.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Added"
        header.Text = "Added"
        header.Width = 180
        LVPlaylist.Columns.Add(header)
        header = Nothing

        SetAccentColor()
        SetTheme()
        LoadPlaylist()
        ClearPlaylistTitles()
        ShowPlayMode()

        'Initialize the media player
        AxPlayer.Visible = False
        AxPlayer.uiMode = "none"
        AxPlayer.enableContextMenu = False
        AxPlayer.Ctlenabled = False
        AxPlayer.stretchToFit = True
        _player.Volume = 100

        'Set tooltips for buttons
        TipPlayer.SetText(BtnPlay, "Play / Pause")
        TipPlayer.SetText(BtnStop, "Stop Playing")
        TipPlayer.SetText(BtnReverse, "Skip Backward")
        TipPlayer.SetText(BtnForward, "Skip Forward")
        TipPlayer.SetText(BtnMute, "Mute")
        TipPlayer.SetText(LblAlbumArtSelect, "Show Next Album Art")
        TipPlayer.SetText(LblDuration, "Song Duration")
        SetTipPlayer()
        CustomDrawCMToolTip(CMPlaylist)

#If DEBUG Then
        'If App.SaveWindowMetrics AndAlso App.PlayerLocation.Y >= 0 Then Me.Location = App.PlayerLocation
        'If App.SaveWindowMetrics AndAlso App.PlayerSize.Height >= 0 Then Me.Size = App.PlayerSize
#Else
        If App.SaveWindowMetrics AndAlso App.PlayerLocation.Y >= 0 Then Me.Location = App.PlayerLocation
        If App.SaveWindowMetrics AndAlso App.PlayerSize.Height >= 0 Then Me.Size = App.PlayerSize
#End If

        AddHandler TrackBarPosition.MouseWheel, AddressOf TrackBarPosition_MouseWheel
        System.Windows.Forms.Application.AddMessageFilter(New MessageFilterPlayerIgnoreFullscreenMouseClick())

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
                    Case Keys.Space
                        StopPlay()
                        e.SuppressKeyPress = True
                End Select
            ElseIf e.Shift Then
                Select Case e.KeyCode
                End Select
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
                    Case Keys.F, Keys.F11
                        If AxPlayer.Visible And Not Fullscreen Then AxPlayer.fullScreen = True
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
    Private Sub Player_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, PicBoxAlbumArt.MouseDown, PicBoxVisualizer.MouseDown, MenuPlayer.MouseDown, LblPlaylistCount.MouseDown, LblDuration.MouseDown, PEXLeft.MouseDown, PEXRight.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso Me.WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FrameBorderSize.Width - 4, -e.Y - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FrameBorderSize.Width - 4, -e.Y - cSender.Top - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            End If
        End If
        cSender = Nothing
    End Sub
    Private Sub Player_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, PicBoxAlbumArt.MouseMove, PicBoxVisualizer.MouseMove, MenuPlayer.MouseMove, LblPlaylistCount.MouseMove, LblDuration.MouseMove, PEXLeft.MouseMove, PEXRight.MouseMove
        If mMove Then
            mPosition = Control.MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub Player_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, PicBoxAlbumArt.MouseUp, PicBoxVisualizer.MouseUp, MenuPlayer.MouseUp, LblPlaylistCount.MouseUp, LblDuration.MouseUp, PEXLeft.MouseUp, PEXRight.MouseUp
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
            App.PlayerSize = Me.Size
        End If
    End Sub
    Private Sub Player_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        ResetTxtBoxPlaylistSearch()
        LVPlaylist.Select()
    End Sub
    Private Sub Player_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SavePlaylist()
        My.Finalize()
    End Sub

    'Control Events
    Private Sub AxPlayerKeyUpEvent(sender As Object, e As _WMPOCXEvents_KeyUpEvent) Handles AxPlayer.KeyUpEvent
        If e.nKeyCode = Keys.Escape And Fullscreen Then AxPlayer.fullScreen = False
        Cursor = Cursors.Hand
        Cursor = Cursors.Default
    End Sub
    Private Sub AxPlayerClickEvent(sender As Object, e As _WMPOCXEvents_ClickEvent) Handles AxPlayer.ClickEvent
        If AxPlayer.Visible And Not Fullscreen Then AxPlayer.fullScreen = True
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
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            PlaylistTitleSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("Title").Index).Text = "Title ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            PlaylistTitleSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("Title").Index).Text = "Title ↑"
                    End Select
                Case LVPlaylist.Columns("Path").Index
                    PlaylistPathSort = ClearPlaylistSorts(PlaylistPathSort)
                    Select Case PlaylistPathSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            PlaylistPathSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("Path").Index).Text = "Path ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            PlaylistPathSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("Path").Index).Text = "Path ↑"
                    End Select
                Case LVPlaylist.Columns("Rating").Index
                    PlaylistRatingSort = ClearPlaylistSorts(PlaylistRatingSort)
                    Select Case PlaylistRatingSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            PlaylistRatingSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("Rating").Index).Text = "Rating ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            PlaylistRatingSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("Rating").Index).Text = "Rating ↑"
                    End Select
                Case LVPlaylist.Columns("PlayCount").Index
                    PlaylistPlayCountSort = ClearPlaylistSorts(PlaylistPlayCountSort)
                    Select Case PlaylistPlayCountSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemNumberComparer(e.Column, SortOrder.Descending)
                            PlaylistPlayCountSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("PlayCount").Index).Text = "Plays ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemNumberComparer(e.Column, SortOrder.Ascending)
                            PlaylistPlayCountSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("PlayCount").Index).Text = "Plays ↑"
                    End Select
                Case LVPlaylist.Columns("LastPlayed").Index
                    PlaylistLastPlayedSort = ClearPlaylistSorts(PlaylistLastPlayedSort)
                    Select Case PlaylistLastPlayedSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemDateComparer(e.Column, SortOrder.Descending)
                            PlaylistLastPlayedSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("LastPlayed").Index).Text = "Last Played ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemDateComparer(e.Column, SortOrder.Ascending)
                            PlaylistLastPlayedSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("LastPlayed").Index).Text = "Last Played ↑"
                    End Select
                Case LVPlaylist.Columns("FirstPlayed").Index
                    PlaylistFirstPlayedSort = ClearPlaylistSorts(PlaylistFirstPlayedSort)
                    Select Case PlaylistFirstPlayedSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemDateComparer(e.Column, SortOrder.Descending)
                            PlaylistFirstPlayedSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("FirstPlayed").Index).Text = "First Played ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemDateComparer(e.Column, SortOrder.Ascending)
                            PlaylistFirstPlayedSort = SortOrder.Ascending
                            LVPlaylist.Columns(LVPlaylist.Columns("FirstPlayed").Index).Text = "First Played ↑"
                    End Select
                Case LVPlaylist.Columns("Added").Index
                    PlaylistAddedSort = ClearPlaylistSorts(PlaylistAddedSort)
                    Select Case PlaylistAddedSort
                        Case SortOrder.Ascending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemDateComparer(e.Column, SortOrder.Descending)
                            PlaylistAddedSort = SortOrder.Descending
                            LVPlaylist.Columns(LVPlaylist.Columns("Added").Index).Text = "Added ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVPlaylist.ListViewItemSorter = New My.ListViewItemDateComparer(e.Column, SortOrder.Ascending)
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
                Dim insertbefore = True
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
    Private Sub PanelMedia_DoubleClick(sender As Object, e As EventArgs) Handles PanelMedia.DoubleClick
        ToggleMaximized()
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
        If App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(_player.Path)) And Not PlayState = PlayStates.Stopped Then
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
        If AxPlayer.Visible And Not Fullscreen Then AxPlayer.fullScreen = True
    End Sub
    Private Sub MIViewQueue_Click(sender As Object, e As EventArgs) Handles MIViewQueue.Click
        Dim frm As New PlayerQueue
        frm.ShowDialog()
    End Sub
    Private Sub MIOptionsClick(sender As Object, e As EventArgs) Handles MIOptions.Click
        ShowOptions()
    End Sub
    Private Sub MIVisualizer_Click(sender As Object, e As EventArgs) Handles MIVisualizer.Click
        If Visualizer Then
            VisualizerOff()
            ShowMedia()
        Else
            Visualizer = True
            LyricsOff()
            MIVisualizer.BackColor = Skye.WinAPI.GetSystemColor(Skye.WinAPI.COLOR_HIGHLIGHT)
            ShowMedia()
        End If
    End Sub
    Private Sub MILyrics_Click(sender As Object, e As EventArgs) Handles MILyrics.Click
        If Lyrics Then
            LyricsOff()
            ShowMedia()
        Else
            Lyrics = True
            VisualizerOff()
            MILyrics.BackColor = Skye.WinAPI.GetSystemColor(Skye.WinAPI.COLOR_HIGHLIGHT)
            ShowMedia()
        End If
    End Sub
    Private Sub MIPlayMode_Click(sender As Object, e As EventArgs) Handles MIPlayMode.Click
        Dim newIndex As Byte = CType(App.PlayMode + 1, Byte)
        'Debug.Print(newIndex.ToString)
        If newIndex = [Enum].GetNames(GetType(App.PlayModes)).Length Then
            newIndex = 0
        End If
        App.PlayMode = CType(newIndex, App.PlayModes)
        ShowPlayMode()
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
        If Not String.IsNullOrWhiteSpace(_watchernotification) Then TipWatcherNotification.ShowTooltipAtCursor(_watchernotification)
    End Sub
    Private Sub MILibrary_MouseLeave(sender As Object, e As EventArgs) Handles MILibrary.MouseLeave
        MILibrary.ForeColor = App.CurrentTheme.AccentTextColor
        TipWatcherNotification.HideTooltip()
    End Sub
    Private Sub MIShowHelpClick(sender As Object, e As EventArgs) Handles MIShowHelp.Click
        ShowHelp()
    End Sub
    Private Sub MIShowLogClick(sender As Object, e As EventArgs) Handles MIShowLog.Click
        If App.FRMLog Is Nothing Then
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
        If LVPlaylist.SelectedItems.Count > 0 Then TipPlaylist.ShowTooltipAt(New Point(CMPlaylist.Location.X, CMPlaylist.Location.Y - 37), App.History.Find(Function(p) p.Path = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text).ToString)
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
        TipPlaylist.HideTooltip()
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
        Dim frmEditTitle As New PlayerEditTitle
        Dim uriresult As Uri = Nothing
        frmEditTitle.NewTitle = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Title").Index).Text
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
        Finally
            item = Nothing
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
        If LVPlaylist.SelectedItems.Count > 0 Then App.FRMLibrary.Show(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
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
    Private Sub PicBoxAlbumArt_DoubleClick(sender As Object, e As EventArgs) Handles PicBoxAlbumArt.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub PicBoxVisualizer_DoubleClick(sender As Object, e As EventArgs) Handles PicBoxVisualizer.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub PicBoxVisualizer_Paint(sender As Object, e As PaintEventArgs) Handles PicBoxVisualizer.Paint
        If PlayState = PlayStates.Playing Then
            'Draw the background gradient
            Dim pBrush As New LinearGradientBrush(New Point(0, 0), New Point(Me.ClientSize.Width, 0), Color.Red, Color.Blue)
            Dim pColorBlend As New ColorBlend
            Dim pColorMiddle As New Color
            pColorMiddle = Color.FromArgb(255, visR, 0, visB)
            pColorBlend.Colors = New Color() {BackColor, pColorMiddle, BackColor} '{pColorStartPoint, pColorMiddle, pColorEndPoint}
            pColorBlend.Positions = New Single() {0, 1 - visV, 1}
            pBrush.InterpolationColors = pColorBlend
            e.Graphics.FillRectangle(pBrush, Me.ClientRectangle)
            pBrush.Dispose()
        Else
            'Draw plain background
            Dim pBrush As New SolidBrush(BackColor)
            e.Graphics.FillRectangle(pBrush, Me.ClientRectangle)
            pBrush.Dispose()
        End If
        'Draw the text
        Dim pFont As Font
        Dim pSize As Byte = 42 'Maximum Font Size
        Dim pString_format As New StringFormat
        Dim pText As String
        pString_format.Alignment = StringAlignment.Center
        pString_format.LineAlignment = StringAlignment.Center
        Dim lvi As ListViewItem = Nothing
        If LVPlaylist.Items.Count > 0 Then lvi = LVPlaylist.FindItemWithText(_player.Path.TrimEnd("/"c), True, 0)
        If lvi Is Nothing Then
            pText = _player.Path.TrimEnd("/"c)
        Else
            pText = lvi.Text
            lvi = Nothing
        End If
        pFont = New Font(Me.Font.FontFamily, pSize, FontStyle.Bold)
        If Not String.IsNullOrEmpty(pText) Then
            Do
                pSize -= CByte(1)
                pFont = New Font(Me.Font.FontFamily, pSize, FontStyle.Bold)
                If pSize = 8 Then Exit Do 'Minimum Font Size
            Loop Until e.Graphics.MeasureString(pText, pFont).Width < PicBoxVisualizer.Width
            Dim pBrush As SolidBrush
            If App.CurrentTheme.IsAccent Then
                pBrush = New SolidBrush(App.CurrentTheme.AccentTextColor)
            Else
                pBrush = New SolidBrush(App.CurrentTheme.TextColor)
            End If
            e.Graphics.DrawString(pText, pFont, pBrush, PicBoxVisualizer.ClientSize.Width \ 2, PicBoxVisualizer.ClientSize.Height \ 2, pString_format)
        End If
        pString_format.Dispose()
        pFont.Dispose()
    End Sub
    Private Sub LblAlbumArtSelectClick(sender As Object, e As EventArgs) Handles LblAlbumArtSelect.Click
        AlbumArtIndex += CByte(1)
        ShowMedia()
    End Sub
    Private Sub LblPosition_MouseUp(sender As Object, e As MouseEventArgs) Handles LblPosition.MouseUp
        PlayerPositionShowElapsed = Not PlayerPositionShowElapsed
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
    Private Sub TxtBoxLyrics_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxLyrics.PreviewKeyDown
        CMLyrics.ShortcutKeys(CType(sender, System.Windows.Forms.TextBox), e)
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
    Private Sub BtnReverseMouseDown(sender As Object, e As MouseEventArgs) Handles BtnReverse.MouseDown, BtnReverse.MouseDown
        If Not Stream Then
            UpdatePosition(False, 10)
            LVPlaylist.Focus()
        End If
    End Sub
    Private Sub BtnForwardMouseDown(sender As Object, e As MouseEventArgs) Handles BtnForward.MouseDown
        If Not Stream Then
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
    Private Sub TrackBarPosition_Scroll(sender As Object, e As EventArgs) Handles TrackBarPosition.Scroll
        _player.Pause()
        OnPause()
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
        Debug.Print((TrackBarPosition.Value / TrackBarScale).ToString)
        _player.Position = TrackBarPosition.Value / TrackBarScale
        _player.Play()
        LVPlaylist.Focus()
    End Sub
    Private Sub TrackBarPosition_MouseWheel(sender As Object, e As MouseEventArgs)
        CType(e, HandledMouseEventArgs).Handled = True
    End Sub

    'Handlers
    Private Sub OnPlaybackStarted()
        OnPlay()
    End Sub
    Private Sub OnPlaybackEnded()
        PlayState = PlayStates.Stopped
        BtnPlay.Image = App.CurrentTheme.PlayerPlay
        TrackBarPosition.Value = 0
        PEXLeft.Value = 0
        PEXRight.Value = 0
        ResetLblPositionText()
        If Not App.PlayMode = App.PlayModes.None Then PlayNext()
    End Sub
    Private Sub TimerPosition_Tick(sender As Object, e As EventArgs) Handles TimerPosition.Tick
        If _player.HasMedia AndAlso PlayState = PlayStates.Playing Then
            Try
                If Not Stream Then TrackBarPosition.Value = CInt(_player.Position * TrackBarScale)
                If My.App.PlayerPositionShowElapsed Then
                    LblPosition.Text = FormatPosition(_player.Position)
                Else
                    If Stream Then
                        LblPosition.Text = "00:00"
                    Else
                        LblPosition.Text = FormatPosition(_player.Duration - _player.Position)
                    End If
                End If
            Catch
                TrackBarPosition.Value = 0
                LblPosition.Text = "00:00"
            End Try
        End If
    End Sub
    Private Sub TimerMeter_Tick(sender As Object, e As EventArgs) Handles TimerMeter.Tick
        If _player.HasMedia AndAlso PlayState = PlayStates.Playing Then
            Try : cLeftMeter = CByte(aDev.AudioMeterInformation.PeakValues(0) * 100)
            Catch : cLeftMeter = 0
            End Try
            Try : cRightMeter = CByte(aDev.AudioMeterInformation.PeakValues(1) * 100)
            Catch : cRightMeter = 0
            End Try
            PEXLeft.Value = cLeftMeter
            PEXRight.Value = cRightMeter
        End If
    End Sub
    Private Sub TimerVisualizer_Tick(sender As Object, e As EventArgs) Handles TimerVisualizer.Tick
        Try : visB = CByte(aDev.AudioMeterInformation.PeakValues(0) * 255)
        Catch : visB = 0
        End Try
        Try : visR = CByte(aDev.AudioMeterInformation.PeakValues(1) * 255)
        Catch : visR = 0
        End Try
        Try : visV = aDev.AudioEndpointVolume.MasterVolumeLevelScalar
        Catch : visV = 0
        End Try
        PicBoxVisualizer.Invalidate()
    End Sub
    Private Sub TimerPlayNext_Tick(sender As Object, e As EventArgs) Handles TimerPlayNext.Tick
        TimerPlayNext.Stop()
        _player.Play()
    End Sub
    Private Sub TimerStatus_Tick(sender As Object, e As EventArgs) Handles TimerStatus.Tick
        TimerStatus.Stop()
        SetPlaylistCountText()
    End Sub

    'Functions
    Private Function IsStream(path As String) As Boolean
        If App.History.FindIndex(Function(p) p.Path = path And p.IsStream = True) >= 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function FormatDuration(duration As Double) As String
        Dim dur As TimeSpan = TimeSpan.FromSeconds(duration)
        Dim durstr As String = ""
        If dur.Hours > 0 Then
            durstr = durstr & dur.Hours.ToString
            durstr = durstr & ":"
        End If
        If dur.Minutes < 10 Then durstr = durstr & "0"
        durstr = durstr & dur.Minutes.ToString
        durstr = durstr & ":"
        If dur.Seconds < 10 Then durstr = durstr & "0"
        durstr = durstr & dur.Seconds.ToString
        Return durstr
    End Function
    Private Function FormatPosition(position As Double) As String
        Dim pos As TimeSpan = TimeSpan.FromSeconds(position)
        Dim posstr As String = ""
        If Not My.App.PlayerPositionShowElapsed Then posstr = "-"
        If pos.Hours > 0 Then
            posstr = posstr & pos.Hours.ToString
            posstr = posstr & ":"
        End If
        If pos.Minutes < 10 Then posstr = posstr & "0"
        posstr = posstr & pos.Minutes.ToString
        posstr = posstr & ":"
        If pos.Seconds < 10 Then posstr = posstr & "0"
        posstr = posstr & pos.Seconds.ToString
        posstr = posstr & "."
        posstr = posstr & Int(pos.Milliseconds / 100).ToString
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

    'Procedures
    Friend Sub QueueFromLibrary(path As String)
        Dim found As Boolean = False
        For Each s As String In Queue
            If s = path Then
                found = True
                Exit For
            End If
        Next
        If Not found Then
            Queue.Add(path)
            SetPlaylistCountText()
        End If
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
        removelist = Nothing
        SetPlaylistCountText()
        App.WriteToLog("Queue Pruned (" + count.ToString + ")")
    End Sub
    Friend Sub RemoveFromQueue(path As String)
        Queue.Remove(path)
        Debug.Print(path + " Removed From Queue")
        SetPlaylistCountText()
    End Sub
    Friend Sub Suspend() 'Called when the user locks the screen or activates the screen saver
        If App.SuspendOnSessionChange Then
            Debug.Print("Suspending...")
            StopPlay()
            Me.WindowState = FormWindowState.Minimized
            App.WriteToLog("App Suspended @ " & Now)
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
                TipPlayer.SetText(BtnPrevious, String.Empty)
                TipPlayer.SetText(BtnNext, String.Empty)
            Case App.PlayModes.Linear
                TipPlayer.SetText(BtnPrevious, "Previous Song In Playlist")
                TipPlayer.SetText(BtnNext, "Next Song In Playlist")
            Case App.PlayModes.Random
                TipPlayer.SetText(BtnPrevious, "Previous Song Played")
                TipPlayer.SetText(BtnNext, "Next Random Song")
        End Select
        Select Case App.PlayerPositionShowElapsed
            Case True
                TipPlayer.SetText(LblPosition, "Elapsed Time")
            Case False
                TipPlayer.SetText(LblPosition, "Time Remaining")
        End Select
    End Sub
    Private Sub ShowStatusMessage(msg As String)
        If App.PlaylistStatusMessageDisplayTime > 0 Then
            LblPlaylistCount.Text = StrConv(msg, VbStrConv.ProperCase)
            TimerStatus.Interval = App.PlaylistStatusMessageDisplayTime * 1000
            TimerStatus.Start()
        End If
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
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsNormalWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsNormalWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsNormalWindow
        If location.Y < App.AdjustScreenBoundsNormalWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
    Private Sub ToggleMaximized()
        Select Case WindowState
            Case FormWindowState.Normal, FormWindowState.Minimized
                WindowState = FormWindowState.Maximized
            Case FormWindowState.Maximized
                WindowState = FormWindowState.Normal
        End Select
    End Sub
    Private Sub LyricsOff()
        If Lyrics Then
            Lyrics = False
            MILyrics.BackColor = Color.Transparent
        End If
    End Sub
    Private Sub VisualizerOff()
        If Visualizer Then
            Visualizer = False
            MIVisualizer.BackColor = Color.Transparent
        End If
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
                Dim newitem As New PlaylistItemType
                newitem.Title = plitem.SubItems(LVPlaylist.Columns("Title").Index).Text
                newitem.Path = plitem.SubItems(LVPlaylist.Columns("Path").Index).Text
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
        Dim ofd As New OpenFileDialog
        ofd.Title = "Select Media File(s)"
        ofd.Filter = "All Files|*.*"
        ofd.Multiselect = True
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
        result = Nothing
        ofd.Dispose()
        ofd = Nothing
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
    Private Sub QueueFromPlaylist()
        If LVPlaylist.SelectedItems.Count > 0 Then
            Dim found As Boolean = False
            For Each s As String In Queue
                If s = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text Then
                    found = True
                    Exit For
                End If
            Next
            If Not found Then
                Queue.Add(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
                SetPlaylistCountText()
            End If
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
        End If
    End Sub
    Private Sub GetHistory(ByRef lvi As ListViewItem, path As String)
        Dim s As App.Song = App.History.Find(Function(p) p.Path = path)
        If Not String.IsNullOrEmpty(s.Path) Then
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
            Debug.Print("Updated History in Playlist for " + path)
        End If
    End Sub
    Private Sub RandomHistoryAdd(songorstream As String)
        If App.PlayMode = App.PlayModes.Random AndAlso LVPlaylist.FindItemWithText(songorstream, True, 0) IsNot Nothing Then
            If RandomHistory.FindIndex(Function(p) p = songorstream) < 0 Then
                App.UpdateRandomHistory(songorstream)
            Else
                Debug.Print("Not Adding " + songorstream + " to Random History, Already Exists")
            End If
        Else
            Debug.Print("Not Adding " + songorstream + " to Random History, Song Not Found In Playlist or Not In Random PlayMode")
        End If
    End Sub
    Friend Sub AddToRandomHistory(songorstream As String)
        If App.PlayMode = App.PlayModes.Random Then
            RandomHistory.Add(songorstream)
            RandomHistoryIndex = RandomHistory.Count
            Debug.Print("Added " + songorstream + " to Random History")
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
    End Sub
    Private Function CreateListviewItem() As ListViewItem
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
            Stream = True
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
            Stream = False
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
    Private Sub PlayQueued()
        If Queue.Count > 0 Then
            StopPlay()
            If IsStream(Queue(0)) Then
                PlayStream(Queue(0))
            Else
                PlayFile(Queue(0), "PlayQueued")
            End If
            Dim item As ListViewItem = LVPlaylist.FindItemWithText(Queue(0), True, 0)
            If item IsNot Nothing Then
                EnsurePlaylistItemIsVisible(item.Index)
                item = Nothing
            End If
            Queue.RemoveAt(0)
            SetPlaylistCountText()
            TimerPlayNext.Start()
        End If
    End Sub
    Private Sub PlayFromPlaylist()
        If LVPlaylist.SelectedItems.Count > 0 Then
            LyricsOff()
            StopPlay()
            If Mute Then ToggleMute()
            If IsStream(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                PlayStream(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
            Else
                Stream = False
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
            existingitem = Nothing
        End If
        StopPlay()
        If Mute Then ToggleMute()
        PlayFile(filename, "PlayFromLibrary")
    End Sub
    Friend Sub PlayPrevious()
        Stream = False
        StopPlay()
        LyricsOff()
        Select Case App.PlayMode
            Case PlayModes.Repeat
                TimerPlayNext.Start()
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
                    item = Nothing
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
                            item = Nothing
                        End If
                    End If
                End If
        End Select
    End Sub
    Friend Sub PlayNext()
        Stream = False
        StopPlay()
        LyricsOff()
        Select Case App.PlayMode
            Case PlayModes.Repeat
                TimerPlayNext.Start()
            Case PlayModes.Linear
                If Queue.Count > 0 Then
                    PlayQueued()
                Else
                    If LVPlaylist.Items.Count > 0 Then
                        Dim item As ListViewItem = Nothing
                        If LVPlaylist.SelectedItems.Count > 0 Then
                            item = LVPlaylist.FindItemWithText(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            item = LVPlaylist.FindItemWithText(_player.Path, True, 0)
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
                        item = Nothing
                        TimerPlayNext.Start()
                    End If
                End If
            Case PlayModes.Random
                If Queue.Count > 0 Then
                    PlayQueued()
                Else
                    If LVPlaylist.Items.Count > 0 Then
                        Dim item As ListViewItem = LVPlaylist.FindItemWithText(_player.Path, True, 0)
                        Dim randomplaylistindex As System.Random = New System.Random()
                        Dim newindex As Integer = 0
                        If RandomHistoryFull() Then RandomHistory.Clear()
                        If item Is Nothing Then
                            newindex = Skye.Common.GetRandom(0, LVPlaylist.Items.Count - 1)
                        Else
                            If LVPlaylist.Items.Count = 1 Then
                                newindex = 0
                            Else
                                Do
                                    newindex = Skye.Common.GetRandom(0, LVPlaylist.Items.Count - 1) 'randomplaylistindex.Next(0, LVPlaylist.Items.Count)
                                Loop Until newindex <> item.Index And Not RandomHistory.Contains(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                            End If
                        End If
                        If IsStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                            PlayStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            PlayFile(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayNextRandom")
                        End If
                        EnsurePlaylistItemIsVisible(newindex)
                        item = Nothing
                        randomplaylistindex = Nothing
                        TimerPlayNext.Start()
                    End If
                End If
        End Select
    End Sub
    Private Sub OnPlay()
        PlayState = PlayStates.Playing
        UpdateHistory(_player.Path.TrimEnd("/"c)) 'Trimming is needed because Windows Media Player will add a "/" to the end of streams.
        BtnPlay.Image = App.CurrentTheme.PlayerPause
        TrackBarPosition.Maximum = CInt(_player.Duration * TrackBarScale)
        If Not TrackBarPosition.Enabled AndAlso Not Stream Then TrackBarPosition.Enabled = True
        LblDuration.Text = FormatDuration(_player.Duration)
        Try
            If Stream Then
                Text = My.Application.Info.Title + " - " + _player.Path
            Else
                Text = My.Application.Info.Title + " - " + LVPlaylist.FindItemWithText(_player.Path, True, 0).Text + " @ " + _player.Path
            End If
        Catch ex As Exception
            Text = My.Application.Info.Title + " - " + _player.Path
        End Try
        ShowMedia()
    End Sub
    Private Sub OnPause()
        PlayState = PlayStates.Paused
        BtnPlay.Image = App.CurrentTheme.PlayerPlay
    End Sub
    Private Sub OnStop()
        PlayState = PlayStates.Stopped
        App.StopHistoryUpdates()
        BtnPlay.Image = App.CurrentTheme.PlayerPlay
        TrackBarPosition.Value = 0
        PEXLeft.Value = 0
        PEXRight.Value = 0
        ResetLblPositionText()
        AxPlayer.Visible = False
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
            LVPlaylist.Focus()
        End If
    End Sub
    Private Sub ToggleMute()
        If Mute Then
            _player.Volume = 100
            BtnMute.Image = Resources.ImagePlayerSound
            Mute = False
        Else
            _player.Volume = 0
            BtnMute.Image = Resources.ImagePlayerSoundMute
            Mute = True
        End If
    End Sub
    Private Sub VideoSetSize()
        Try
            If App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(_player.Path)) Then
                If _player.VideoHeight / _player.VideoWidth > PanelMedia.Height / PanelMedia.Width Then
                    'Height > Width, Set Height then Get Width, then Set Centers
                    AxPlayer.Width = VideoGetWidth(PanelMedia.Height)
                    AxPlayer.Height = PanelMedia.Height
                    AxPlayer.Left = CInt(PanelMedia.Right - ((PanelMedia.Right - PanelMedia.Left) / 2) - ((AxPlayer.Right - AxPlayer.Left) / 2))
                    AxPlayer.Top = CInt(PanelMedia.Bottom - ((PanelMedia.Bottom - PanelMedia.Top) / 2) - ((AxPlayer.Bottom - AxPlayer.Top) / 2))
                Else
                    'Width > Height, Set Width then Get Height, then Set Centers
                    AxPlayer.Width = PanelMedia.Width
                    AxPlayer.Height = VideoGetHeight(PanelMedia.Width)
                    AxPlayer.Left = CInt(PanelMedia.Right - ((PanelMedia.Right - PanelMedia.Left) / 2) - ((AxPlayer.Right - AxPlayer.Left) / 2))
                    AxPlayer.Top = CInt(PanelMedia.Bottom - ((PanelMedia.Bottom - PanelMedia.Top) / 2) - ((AxPlayer.Bottom - AxPlayer.Top) / 2))
                End If
            End If
        Catch
        End Try
    End Sub
    Private Sub ShowMedia()
        If _player.HasMedia Then
            Dim tlfile As TagLib.File
            Try
                If Stream Then
                    tlfile = Nothing
                Else
                    tlfile = TagLib.File.Create(_player.Path)
                End If
            Catch ex As Exception
                WriteToLog("TagLib Error while Showing Media, Cannot read from file: " + _player.Path + Chr(13) + ex.Message)
                tlfile = Nothing
            End Try
            If Lyrics AndAlso Not Stream Then 'Show Lyrics
                Debug.Print("Showing Lyrics...")
                PicBoxAlbumArt.Visible = False
                LblAlbumArtSelect.Visible = False
                PicBoxVisualizer.Visible = False
                Visualizer = False
                TimerVisualizer.Stop()
                AxPlayer.Visible = False
                If WindowState = FormWindowState.Maximized Then
                    TxtBoxLyrics.Font = New Font(TxtBoxLyrics.Font.FontFamily, 20, FontStyle.Bold)
                Else
                    TxtBoxLyrics.Font = New Font(TxtBoxLyrics.Font.FontFamily, 12, FontStyle.Bold)
                End If
                TxtBoxLyrics.Text = tlfile.Tag.Lyrics
                TxtBoxLyrics.Visible = True
            Else
                If Visualizer Then
                    PicBoxAlbumArt.Visible = False
                    LblAlbumArtSelect.Visible = False
                    TxtBoxLyrics.Visible = False
                    AxPlayer.Visible = False
                Else
                    If App.AudioExtensionDictionary.ContainsKey(Path.GetExtension(_player.Path)) Then 'Show Album Art
                        AxPlayer.Visible = False
                        PicBoxVisualizer.Visible = False
                        TxtBoxLyrics.Visible = False
                        TimerVisualizer.Stop()
                        If tlfile Is Nothing Then
                            PicBoxAlbumArt.Visible = False
                            LblAlbumArtSelect.Visible = False
                        Else
                            If tlfile.Tag.Pictures.Length = 0 Then
                                PicBoxAlbumArt.Visible = False
                                LblAlbumArtSelect.Visible = False
                            Else
                                Debug.Print("Showing Album Art...")
                                If AlbumArtIndex + 1 > tlfile.Tag.Pictures.Count Then AlbumArtIndex = 0
                                Dim ms As New IO.MemoryStream(tlfile.Tag.Pictures(AlbumArtIndex).Data.Data)
                                Try
                                    PicBoxAlbumArt.Image = Drawing.Image.FromStream(ms)
                                    PicBoxAlbumArt.Visible = True
                                Catch ex As Exception
                                    WriteToLog("Error Loading Album Art for " + _player.Path + vbCr + ex.Message)
                                    PicBoxAlbumArt.Visible = False
                                    LblAlbumArtSelect.Visible = False
                                End Try
                                ms.Dispose()
                                ms = Nothing
                                If tlfile.Tag.Pictures.Count > 1 Then : LblAlbumArtSelect.Visible = True
                                Else : LblAlbumArtSelect.Visible = False
                                End If
                            End If
                        End If
                    ElseIf App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(_player.Path)) Then 'Show Video
                        Debug.Print("Showing Video...")
                        PicBoxAlbumArt.Visible = False
                        LblAlbumArtSelect.Visible = False
                        TxtBoxLyrics.Visible = False
                        PicBoxVisualizer.Visible = False
                        TimerVisualizer.Stop()
                        VideoSetSize()
                        AxPlayer.Visible = True
                    End If
                End If
                If Visualizer OrElse (Not AxPlayer.Visible AndAlso Not PicBoxAlbumArt.Visible) OrElse Stream Then 'Show Visualizer
                    Debug.Print("Showing Visualizer...")
                    AxPlayer.Visible = False
                    PicBoxAlbumArt.Visible = False
                    LblAlbumArtSelect.Visible = False
                    TxtBoxLyrics.Visible = False
                    TimerVisualizer.Start()
                    PicBoxVisualizer.Visible = True
                    PicBoxVisualizer.BringToFront()
                End If
                'Show Lyrics Menu Button
                If tlfile Is Nothing Then
                    MILyrics.Visible = False
                Else
                    If String.IsNullOrEmpty(tlfile.Tag.Lyrics) Then
                        MILyrics.Visible = False
                    Else
                        MILyrics.Visible = True
                    End If
                End If
            End If
            tlfile = Nothing
        Else
            PicBoxAlbumArt.Visible = False
            LblAlbumArtSelect.Visible = False
            TxtBoxLyrics.Visible = False
            AxPlayer.Visible = False
            PicBoxVisualizer.Visible = False
            TimerVisualizer.Stop()
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
                TxtBoxLyrics.BackColor = CurrentAccentColor
                TrackBarPosition.TrackBarGradientStart = CurrentAccentColor
                TrackBarPosition.TrackBarGradientEnd = CurrentAccentColor
            End If
            ResumeLayout()
            Debug.Print("Player Accent Color Set")
            Skye.WinAPI.RedrawWindow(Me.Handle, IntPtr.Zero, IntPtr.Zero, Skye.WinAPI.RDW_INVALIDATE Or Skye.WinAPI.RDW_ERASE Or Skye.WinAPI.RDW_FRAME Or Skye.WinAPI.RDW_ALLCHILDREN Or Skye.WinAPI.RDW_UPDATENOW)
            Debug.Print("Player Repainted")
        End If
    End Sub
    Private Sub SetActiveTitleBarColor()
        If IsFocused And CurrentAccentColor <> Nothing Then
            MenuPlayer.BackColor = CurrentAccentColor
            TxtBoxPlaylistSearch.BackColor = CurrentAccentColor
            Debug.Print("Player Active Title Bar Color Set: " + CurrentAccentColor.ToString)
        End If
    End Sub
    Private Sub SetInactiveTitleBarColor()
        If Not IsFocused Then
            MenuPlayer.BackColor = App.CurrentTheme.InactiveTitleBarColor
            TxtBoxPlaylistSearch.BackColor = App.CurrentTheme.InactiveTitleBarColor
            Debug.Print("Player InActive Title Bar Color Set")
        End If
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            LblPlaylistCount.ForeColor = App.CurrentTheme.AccentTextColor
            LblDuration.ForeColor = App.CurrentTheme.AccentTextColor
            LblPosition.ForeColor = App.CurrentTheme.AccentTextColor
            TxtBoxLyrics.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            TrackBarPosition.TrackBarGradientStart = App.CurrentTheme.BackColor
            TrackBarPosition.TrackBarGradientEnd = App.CurrentTheme.BackColor
            LblPlaylistCount.ForeColor = App.CurrentTheme.TextColor
            LblDuration.ForeColor = App.CurrentTheme.TextColor
            LblPosition.ForeColor = App.CurrentTheme.TextColor
            TxtBoxLyrics.BackColor = App.CurrentTheme.BackColor
            TxtBoxLyrics.ForeColor = App.CurrentTheme.TextColor
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
        TipPlaylist.BackColor = App.CurrentTheme.BackColor
        TipPlaylist.ForeColor = App.CurrentTheme.TextColor
        TipPlaylist.BorderColor = App.CurrentTheme.ButtonBackColor
        TipWatcherNotification.BackColor = App.CurrentTheme.BackColor
        TipWatcherNotification.ForeColor = App.CurrentTheme.TextColor
        TipWatcherNotification.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
        Debug.Print("Player Theme Set")
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor(True)
        SetTheme()
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
                s = TextRenderer.MeasureText(CType(sender, ToolTip).GetToolTip(e.AssociatedControl), TipPlayer.Font)
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
                Using p As New Pen(App.CurrentTheme.ButtonBackColor, CInt(TipPlayer.Font.Size / 4)) 'Scale border thickness with font
                    g.DrawRectangle(p, 0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1)
                End Using

                'Draw text
                TextRenderer.DrawText(g, e.ToolTipText, TipPlayer.Font, New Point(7, 7), App.CurrentTheme.TextColor)

                'Finalize
                brbg.Dispose()
                g.Dispose()

            End Sub

    End Sub

End Class
