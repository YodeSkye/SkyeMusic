
Imports System.Drawing.Drawing2D
Imports System.IO
Imports AxWMPLib
Imports SkyeMusic.My
Imports Syncfusion.Windows.Forms

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

    Friend ReadOnly Property Fullscreen As Boolean 'Property to check if the player is in fullscreen mode
        Get
            Return AxPlayer.fullScreen
        End Get
    End Property

    'Form Events                    
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case WinAPI.WM_HOTKEY
                    Debug.Print("HOTKEY " + m.WParam.ToString + " PRESSED")
                    App.PerformHotKeyAction(m.WParam.ToInt32)
                Case WinAPI.WM_ACTIVATE
                    Select Case m.WParam.ToInt32
                        Case 0
                            IsFocused = False
                            SetInactiveColor()
                        Case 1, 2
                            IsFocused = True
                            SetAccentColor()
                    End Select
                Case WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
                Case WinAPI.WM_SIZE
                    If (m.WParam.ToInt32 = 0 Or m.WParam.ToInt32 = 2) AndAlso Lyrics Then ShowMedia()
                Case WinAPI.WM_GET_CUSTOM_DATA
                    App.WriteToLog(WinAPI.WM_GET_CUSTOM_DATA.ToString)
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
            If m.Msg <> WinAPI.WM_GET_CUSTOM_DATA Then MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub Player_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        AxPlayer.settings.volume = 100

        'Set tooltips for buttons
        TipPlayer.SetToolTip(BtnPlay, "Play / Pause")
        TipPlayer.SetToolTip(BtnStop, "Stop Playing")
        TipPlayer.SetToolTip(BtnReverse, "Skip Backward")
        TipPlayer.SetToolTip(BtnForward, "Skip Forward")
        TipPlayer.SetToolTip(BtnMute, "Mute")
        TipPlayer.SetToolTip(LblAlbumArtSelect, "Show Next Album Art")
        TipPlayer.SetToolTip(LblDuration, "Song Duration")
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
        Debug.Print("Player Lost Focus")
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
                'Case Keys.A
                '    For Each item As ListViewItem In LVPlaylist.Items
                '        item.Selected = True
                '    Next
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
            'e.Handled = True
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
                If Computer.FileSystem.FileExists(s) Then files.Add(s)
            Next
            If files.Count > 0 Then : e.Effect = DragDropEffects.Link
            Else : e.Effect = DragDropEffects.None
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
            Dim files As New List(Of String)
            For Each s In filedrop
                If Computer.FileSystem.FileExists(s) Then files.Add(s)
            Next
            If files.Count > 0 Then
                WriteToLog("Drag&Drop Performed (" + files.Count.ToString + " " + IIf(files.Count = 1, "File", "Files").ToString + ")")
                Dim lvi As ListViewItem
                Dim clientpoint = LVPlaylist.PointToClient(New System.Drawing.Point(e.X, e.Y))
                Dim itemover = LVPlaylist.GetItemAt(clientpoint.X, clientpoint.Y)
                For x = 0 To files.Count - 1
                    If LVPlaylist.Items.Count = 0 Then
                        lvi = Nothing
                    Else
                        lvi = LVPlaylist.FindItemWithText(files(x), True, 0)
                    End If
                    If lvi Is Nothing Then
                        'add new playlist entry
                        If ExtensionDictionary.ContainsKey(Path.GetExtension(files(x))) Then
                            lvi = CreateListviewItem()
                            lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = FormatPlaylistTitle(files(x))
                            lvi.SubItems(LVPlaylist.Columns("Path").Index).Text = files(x)
                            App.AddToHistoryFromPlaylist(files(x))
                            GetHistory(lvi, files(x))
                            If itemover Is Nothing Then
                                LVPlaylist.Items.Add(lvi)
                            Else
                                LVPlaylist.ListViewItemSorter = Nothing
                                ClearPlaylistTitles()
                                LVPlaylist.Items.Insert(itemover.Index, lvi)
                            End If
                            SetPlaylistCountText()
                        End If
                    Else
                        'update playlist entry
                        lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = FormatPlaylistTitle(files(x))
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
            files.Clear()
            files = Nothing
            filedrop = Nothing
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
                'Add to History
                App.AddToHistoryFromPlaylist(frmAddStream.NewStream.Path, True)
                'Add to Playlist
                AddToPlaylistFromLibrary(frmAddStream.NewStream.Title, frmAddStream.NewStream.Path)
                LVPlaylist.SelectedIndices.Clear()
                LVPlaylist.SelectedIndices.Add(LVPlaylist.FindItemWithText(frmAddStream.NewStream.Title).Index)
                'Play Stream
                PlayStream(frmAddStream.NewStream.Path)
                Debug.Print("New Stream Added (" + frmAddStream.NewStream.Path + ")")
            Else
                Debug.Print("Invalid Stream")
                WriteToLog("Invalid Stream, New Stream Not Added")
            End If
        Else
            Debug.Print("Add Stream Cancelled")
        End If
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
        If App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(AxPlayer.URL)) And Not PlayState = PlayStates.Stopped Then
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
            MIVisualizer.BackColor = WinAPI.GetSystemColor(COLOR_HIGHLIGHT)
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
            MILyrics.BackColor = WinAPI.GetSystemColor(COLOR_HIGHLIGHT)
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
    End Sub
    Private Sub MILibrary_MouseLeave(sender As Object, e As EventArgs) Handles MILibrary.MouseLeave
        MILibrary.ForeColor = App.CurrentTheme.AccentTextColor
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
        TipPlayer.Hide(Me)
        Static pt As Point
        pt = PointToClient(CMPlaylist.Location)
        If LVPlaylist.SelectedItems.Count > 0 Then TipPlayer.Show(App.History.Find(Function(p) p.Path = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text).ToString, Me, New Point(pt.X + 8, pt.Y - 6), 5000)
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
        TipPlayer.Hide(Me)
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
            item = LVPlaylist.FindItemWithText(AxPlayer.URL, True, 0)
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
        'Try
        Dim lvi As ListViewItem = LVPlaylist.FindItemWithText(AxPlayer.URL, True, 0)
        If lvi Is Nothing Then
            pText = AxPlayer.URL
        Else
            pText = lvi.Text
            lvi = Nothing
        End If
        'Catch
        '    pText = String.Empty
        'End Try
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
    Private Sub TxtBoxLyrics_MouseUp(sender As Object, e As MouseEventArgs) Handles TxtBoxLyrics.MouseUp
        CMLyrics.Display(CType(sender, TextBox), e)
    End Sub
    Private Sub TxtBoxLyrics_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxLyrics.PreviewKeyDown
        CMLyrics.ShortcutKeys(CType(sender, TextBox), e)
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
        AxPlayer.Ctlcontrols.pause()
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
        AxPlayer.Ctlcontrols.currentPosition = TrackBarPosition.Value / TrackBarScale
        AxPlayer.Ctlcontrols.play()
        LVPlaylist.Focus()
    End Sub
    Private Sub TrackBarPosition_MouseWheel(sender As Object, e As MouseEventArgs)
        CType(e, HandledMouseEventArgs).Handled = True
    End Sub
    Private Sub TipPlayer_Popup(sender As Object, e As PopupEventArgs) Handles TipPlayer.Popup
        Static s As SizeF
        s = TextRenderer.MeasureText(TipPlayer.GetToolTip(e.AssociatedControl), App.TipFont)
        s.Width += 14
        s.Height += 16
        e.ToolTipSize = s.ToSize
    End Sub
    Private Sub TipPlayer_Draw(sender As Object, e As DrawToolTipEventArgs) Handles TipPlayer.Draw

        'Declarations
        Dim g As Graphics = e.Graphics

        'Draw background
        Dim brbg As New SolidBrush(App.CurrentTheme.BackColor)
        g.FillRectangle(brbg, e.Bounds)

        'Draw border
        Using p As New Pen(App.CurrentTheme.ButtonBackColor, CInt(App.TipFont.Size / 4)) 'Scale border thickness with font
            g.DrawRectangle(p, 0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1)
        End Using

        'Draw text
        TextRenderer.DrawText(g, e.ToolTipText, App.TipFont, New Point(7, 7), App.CurrentTheme.TextColor)

        'Finalize
        brbg.Dispose()
        g.Dispose()

    End Sub

    'Handlers
    Private Sub AxPlayer_PlayStateChange(sender As Object, e As AxWMPLib._WMPOCXEvents_PlayStateChangeEvent) Handles AxPlayer.PlayStateChange
        Select Case e.newState
            Case 0 'Undefined
            Case 1 'Stopped
                'OnStop()
            Case 2 'Paused
                OnPause()
            Case 3 'Playing
                OnPlay()
            Case 4 'ScanForward
            Case 5 'ScanReverse
            Case 6 'Buffering
            Case 7 'Waiting
            Case 8 'MediaEnded
                PlayState = PlayStates.Stopped
                BtnPlay.Image = App.CurrentTheme.PlayerPlay
                TrackBarPosition.Value = 0
                PEXLeft.Value = 0
                PEXRight.Value = 0
                ResetLblPositionText()
                If Not App.PlayMode = App.PlayModes.None Then PlayNext()
            Case 9 'Transitioning
            Case 10 'Ready
            Case 11 'Reconnecting
            Case 12 'Last
            Case Else 'Unknown State
        End Select
    End Sub
    Private Sub TimerPosition_Tick(sender As Object, e As EventArgs) Handles TimerPosition.Tick
        If AxPlayer.currentMedia IsNot Nothing AndAlso PlayState = PlayStates.Playing Then
            Try
                If Not Stream Then TrackBarPosition.Value = CInt(AxPlayer.Ctlcontrols.currentPosition * TrackBarScale)
                If My.App.PlayerPositionShowElapsed Then
                    LblPosition.Text = FormatPosition(AxPlayer.Ctlcontrols.currentPosition)
                Else
                    If Stream Then
                        LblPosition.Text = "00:00"
                    Else
                        LblPosition.Text = FormatPosition(AxPlayer.currentMedia.duration - AxPlayer.Ctlcontrols.currentPosition)
                    End If
                End If
            Catch
                TrackBarPosition.Value = 0
                LblPosition.Text = "00:00"
            End Try
        End If
    End Sub
    Private Sub TimerMeter_Tick(sender As Object, e As EventArgs) Handles TimerMeter.Tick
        If AxPlayer.currentMedia IsNot Nothing And PlayState = PlayStates.Playing Then
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
        AxPlayer.Ctlcontrols.play()
    End Sub

    'Procedures
    Private Function CreateListviewItem() As ListViewItem
        Dim lvi As New ListViewItem
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
    Private Function FormatPlaylistTitle(filename As String) As String
        FormatPlaylistTitle = String.Empty
        Dim tlfile As TagLib.File = Nothing
        If App.PlaylistTitleFormat <> App.PlaylistTitleFormats.UseFilename Then
            Try
                tlfile = TagLib.File.Create(filename)
            Catch ex As Exception
                WriteToLog("TagLib Error while Formatting Playlist Title, Cannot read from file: " + filename + Chr(13) + ex.Message)
                tlfile = Nothing
            End Try
        End If
        If tlfile Is Nothing Then
            FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
        Else
            Select Case App.PlaylistTitleFormat
                Case App.PlaylistTitleFormats.Song
                    If tlfile.Tag.Title = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Title
                        End If
                    End If
                Case App.PlaylistTitleFormats.SongGenre
                    If tlfile.Tag.Title = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Title
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.SongArtist
                    If tlfile.Tag.Title = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Title
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.SongArtistAlbum
                    If tlfile.Tag.Title = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Title
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.SongAlbumArtist
                    If tlfile.Tag.Title = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Title
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.SongArtistGenre
                    If tlfile.Tag.Title = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Title
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.SongGenreArtist
                    If tlfile.Tag.Title = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Title
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.SongArtistAlbumGenre
                    If tlfile.Tag.Title = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Title
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.SongAlbumArtistGenre
                    If tlfile.Tag.Title = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Title
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.SongGenreArtistAlbum
                    If tlfile.Tag.Title = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Title
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.ArtistSong
                    If tlfile.Tag.FirstPerformer = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.ArtistSongAlbum
                    If tlfile.Tag.FirstPerformer = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.ArtistAlbumSong
                    If tlfile.Tag.FirstPerformer = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.ArtistGenreSong
                    If tlfile.Tag.FirstPerformer = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.ArtistSongGenre
                    If tlfile.Tag.FirstPerformer = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.ArtistSongAlbumGenre
                    If tlfile.Tag.FirstPerformer = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.ArtistGenreSongAlbum
                    If tlfile.Tag.FirstPerformer = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.AlbumSongArtist
                    If tlfile.Tag.Album = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Album
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.AlbumArtistSong
                    If tlfile.Tag.Album = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Album
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.AlbumGenreSongArtist
                    If tlfile.Tag.Album = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Album
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.AlbumGenreArtistSong
                    If tlfile.Tag.Album = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Album
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.AlbumSongArtistGenre
                    If tlfile.Tag.Album = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Album
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.AlbumArtistSongGenre
                    If tlfile.Tag.Album = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.Album
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstGenre
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.GenreSong
                    If tlfile.Tag.FirstGenre = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.GenreSongArtist
                    If tlfile.Tag.FirstGenre = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.GenreArtistSong
                    If tlfile.Tag.FirstGenre = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.GenreAlbumSongArtist
                    If tlfile.Tag.FirstGenre = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.GenreAlbumArtistSong
                    If tlfile.Tag.FirstGenre = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.GenreSongArtistAlbum
                    If tlfile.Tag.FirstGenre = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                    End If
                Case App.PlaylistTitleFormats.GenreSongAlbumArtist
                    If tlfile.Tag.FirstGenre = String.Empty Then
                        FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(filename)
                    Else
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += tlfile.Tag.FirstGenre
                        End If
                        If Not tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Title
                            End If
                        End If
                        If Not tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.Album
                            End If
                        End If
                        If Not tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitle += App.PlaylistTitleSeparator
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitle += tlfile.Tag.FirstPerformer
                            End If
                        End If
                    End If
            End Select
            tlfile = Nothing
        End If
        If App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(filename)) Then
            FormatPlaylistTitle += PlaylistVideoIdentifier
        End If
    End Function
    Private Function VideoGetHeight(width As Integer) As Integer
        If AxPlayer.currentMedia Is Nothing Then
            Return 0
        Else
            Try
                'Debug.Print(AxPlayer.currentMedia.imageSourceHeight.ToString + " " + AxPlayer.currentMedia.imageSourceWidth.ToString)
                Return CInt(Int(AxPlayer.currentMedia.imageSourceHeight * (width / AxPlayer.currentMedia.imageSourceWidth)))
            Catch ex As Exception
                Debug.Print("Error calculating video height: " + ex.Message)
                Return 0
            End Try
        End If
    End Function
    Private Function VideoGetWidth(height As Integer) As Integer
        If AxPlayer.currentMedia Is Nothing Then
            Return 0
        Else
            Try
                Return CInt(Int(AxPlayer.currentMedia.imageSourceWidth * (height / AxPlayer.currentMedia.imageSourceHeight)))
            Catch ex As Exception
                Debug.Print("Error calculating video width: " + ex.Message)
                Return 0
            End Try
        End If
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
    Private Function ClearPlaylistSorts(currentsort As SortOrder) As SortOrder
        ClearPlaylistTitles()
        Return currentsort
    End Function
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
            App.WriteToLog("Playlist Saved (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
        End If
    End Sub
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
                App.WriteToLog("Playlist Loaded (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
            End If
        Else
            App.WriteToLog("Playlist Not Loaded: File does not exist")
        End If
        SetPlaylistCountText()
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
                        lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = FormatPlaylistTitle(ofd.FileNames(x))
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
                    lvi.SubItems(LVPlaylist.Columns("Title").Index).Text = FormatPlaylistTitle(ofd.FileNames(x))
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
    Friend Sub TogglePlay()
        If AxPlayer.currentMedia Is Nothing Then
            If LVPlaylist.Items.Count > 0 Then
                If LVPlaylist.SelectedItems.Count = 0 Then LVPlaylist.Items(0).Selected = True
                PlayFromPlaylist()
            End If
        Else
            If PlayState = PlayStates.Playing Then
                AxPlayer.Ctlcontrols.pause()
                '''OnPause()
            Else
                AxPlayer.Ctlcontrols.play()
                '''OnPlay()
            End If
        End If
        LVPlaylist.Focus()
    End Sub
    Friend Sub StopPlay()
        If AxPlayer.currentMedia IsNot Nothing Then
            AxPlayer.Ctlcontrols.stop()
            OnStop()
        End If
    End Sub
    Private Sub PlayStream(url As String)
        If Not String.IsNullOrEmpty(url) Then
            Stream = True
            Try
                AxPlayer.URL = url
                TrackBarPosition.Enabled = False
                TrackBarPosition.Value = 0
                '''OnPlay()
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
                AxPlayer.URL = path
                If Not My.Computer.FileSystem.FileExists(path) Then
                    LblPosition.Text = String.Empty
                    LblDuration.Text = String.Empty
                End If
                '''OnPlay()
                App.WriteToLog("Playing " + path + " (" + source + ")")
                RandomHistoryAdd(path)
            Catch
                App.WriteToLog("Cannot Play File, Invalid Path: " + path + " (" + source + ")")
            End Try
        End If
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
                'AxPlayer.URL = LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text
                'App.WriteToLog("Playing " + LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text + " (PlayFromPlaylist)")
            End If
            'RandomHistoryAdd(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
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
            LVPlaylist.EnsureVisible(existingitem.Index)
            LVPlaylist.SelectedIndices.Clear()
            LVPlaylist.SelectedIndices.Add(existingitem.Index)
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
                If LVPlaylist.Items.Count = 0 Then
                    'AxPlayer.Ctlcontrols.stop()
                Else
                    Dim item As ListViewItem = LVPlaylist.FindItemWithText(AxPlayer.URL, True, 0)
                    Dim newindex As Integer = LVPlaylist.Items.Count - 1
                    If item Is Nothing Then
                        If IsStream(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                            PlayStream(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            PlayFile(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayPreviousLinear")
                            'AxPlayer.URL = LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text
                            'App.WriteToLog("Playing " + LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text + " (PlayPreviousLinear)")
                        End If
                        'App.WriteToLog("Playing " + LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text + " (PlayPreviousLinear)")
                    ElseIf item.Index = 0 Then
                        If IsStream(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                            PlayStream(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            PlayFile(LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayPreviousLinear")
                            'AxPlayer.URL = LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text
                            'App.WriteToLog("Playing " + LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text + " (PlayPreviousLinear)")
                        End If
                        'App.WriteToLog("Playing " + LVPlaylist.Items(LVPlaylist.Items.Count - 1).SubItems(LVPlaylist.Columns("Path").Index).Text + " (PlayPreviousLinear)")
                    Else
                        newindex = item.Index - 1
                        If IsStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                            PlayStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            PlayFile(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayPreviousLinear")
                            'AxPlayer.URL = LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text
                            'App.WriteToLog("Playing " + LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text + " (PlayPreviousLinear)")
                        End If
                    End If
                    LVPlaylist.SelectedIndices.Clear()
                    LVPlaylist.SelectedIndices.Add(newindex)
                    LVPlaylist.EnsureVisible(newindex)
                    item = Nothing
                    'TimerPlayNext.Start()
                End If
            Case PlayModes.Random
                If RandomHistory.Count > 0 Then
                    If LVPlaylist.Items.Count > 0 Then
                        If RandomHistoryIndex > 0 Then
                            RandomHistoryIndex -= 1
                        Else
                            RandomHistoryIndex = RandomHistory.Count - 1
                        End If
                        If RandomHistory.Item(RandomHistoryIndex) = AxPlayer.URL Then
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
                            LVPlaylist.SelectedIndices.Clear()
                            LVPlaylist.SelectedIndices.Add(item.Index)
                            LVPlaylist.EnsureVisible(item.Index)
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
                    If LVPlaylist.Items.Count = 0 Then
                        'AxPlayer.Ctlcontrols.stop()
                    Else
                        Dim item As ListViewItem = Nothing
                        If LVPlaylist.SelectedItems.Count > 0 Then
                            item = LVPlaylist.FindItemWithText(LVPlaylist.SelectedItems(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            item = LVPlaylist.FindItemWithText(AxPlayer.URL, True, 0)
                        End If
                        Dim newindex As Integer = 0
                        If item Is Nothing OrElse item.Index + 1 = LVPlaylist.Items.Count Then
                            If IsStream(LVPlaylist.Items(0).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                                PlayStream(LVPlaylist.Items(0).SubItems(LVPlaylist.Columns("Path").Index).Text)
                            Else
                                PlayFile(LVPlaylist.Items(0).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayNextLinear")
                                'AxPlayer.URL = LVPlaylist.Items(0).SubItems(LVPlaylist.Columns("Path").Index).Text
                                'App.WriteToLog("Playing " + LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text + " (PlayNextLinear)")
                            End If
                        Else
                            newindex = item.Index + 1
                            If IsStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                                PlayStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                            Else
                                PlayFile(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayNextLinear")
                                'AxPlayer.URL = LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text
                                'App.WriteToLog("Playing " + LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text + " (PlayNextLinear)")
                            End If
                        End If
                        LVPlaylist.SelectedIndices.Clear()
                        LVPlaylist.SelectedIndices.Add(newindex)
                        LVPlaylist.EnsureVisible(newindex)
                        item = Nothing
                        TimerPlayNext.Start()
                    End If
                End If
            Case PlayModes.Random
                If Queue.Count > 0 Then
                    PlayQueued()
                Else
                    If LVPlaylist.Items.Count = 0 Then
                        'StopPlay()
                        'AxPlayer.Ctlcontrols.stop()
                    Else
                        Dim item As ListViewItem = LVPlaylist.FindItemWithText(AxPlayer.URL, True, 0)
                        Dim randomplaylistindex As System.Random = New System.Random()
                        Dim newindex As Integer = 0
                        If RandomHistoryFull() Then RandomHistory.Clear()
                        If item Is Nothing Then
                            newindex = randomplaylistindex.Next(0, LVPlaylist.Items.Count)
                        Else
                            If LVPlaylist.Items.Count = 1 Then
                                newindex = 0
                            Else
                                Do
                                    newindex = randomplaylistindex.Next(0, LVPlaylist.Items.Count)
                                Loop Until newindex <> item.Index And Not RandomHistory.Contains(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                            End If
                        End If
                        If IsStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text) Then
                            PlayStream(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        Else
                            PlayFile(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text, "PlayNextRandom")
                            'AxPlayer.URL = LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text
                            'App.WriteToLog("Playing " + LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text + " (PlayNextRandom)")
                        End If
                        'RandomHistoryAdd(LVPlaylist.Items(newindex).SubItems(LVPlaylist.Columns("Path").Index).Text)
                        LVPlaylist.SelectedIndices.Clear()
                        LVPlaylist.SelectedIndices.Add(newindex)
                        LVPlaylist.EnsureVisible(newindex)
                        item = Nothing
                        randomplaylistindex = Nothing
                        TimerPlayNext.Start()
                    End If
                End If
        End Select
    End Sub
    Private Sub PlayQueued()
        If Queue.Count > 0 Then
            StopPlay()
            If IsStream(Queue(0)) Then
                PlayStream(Queue(0))
            Else
                PlayFile(Queue(0), "PlayQueued")
                'Stream = False
                'AxPlayer.URL = Queue(0)
                'App.WriteToLog("Playing " + AxPlayer.URL + " (PlayQueued)")
            End If
            Dim item As ListViewItem = LVPlaylist.FindItemWithText(Queue(0), True, 0)
            If item IsNot Nothing Then
                LVPlaylist.SelectedIndices.Clear()
                LVPlaylist.SelectedIndices.Add(item.Index)
                LVPlaylist.EnsureVisible(item.Index)
                item = Nothing
            End If
            Queue.RemoveAt(0)
            SetPlaylistCountText()
            TimerPlayNext.Start()
        End If
    End Sub
    Private Sub OnPlay()
        PlayState = PlayStates.Playing
        UpdateHistory(AxPlayer.URL)
        BtnPlay.Image = App.CurrentTheme.PlayerPause
        TrackBarPosition.Maximum = CInt(AxPlayer.currentMedia.duration * TrackBarScale)
        If Not TrackBarPosition.Enabled AndAlso Not Stream Then TrackBarPosition.Enabled = True
        LblDuration.Text = FormatDuration(AxPlayer.currentMedia.duration)
        Try
            If Stream Then
                Text = My.Application.Info.Title + " - " + AxPlayer.URL
            Else
                Text = My.Application.Info.Title + " - " + LVPlaylist.FindItemWithText(AxPlayer.URL, True, 0).Text + " @ " + AxPlayer.URL
            End If
        Catch ex As Exception
            Text = My.Application.Info.Title + " - " + AxPlayer.URL
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
    Private Sub UpdatePosition(ByVal forward As Boolean, Optional ByVal seconds As Byte = 20)
        If AxPlayer.currentMedia IsNot Nothing And PlayState = PlayStates.Playing Then
            If forward Then
                If AxPlayer.Ctlcontrols.currentPosition + seconds > AxPlayer.currentMedia.duration Then
                    AxPlayer.Ctlcontrols.currentPosition = AxPlayer.currentMedia.duration
                Else
                    AxPlayer.Ctlcontrols.currentPosition += seconds
                End If
            Else
                If AxPlayer.Ctlcontrols.currentPosition <= seconds Then
                    AxPlayer.Ctlcontrols.currentPosition = 0
                Else
                    AxPlayer.Ctlcontrols.currentPosition -= seconds
                End If
            End If
            LVPlaylist.Focus()
        End If
    End Sub
    Private Sub ToggleMute()
        If Mute Then
            AxPlayer.settings.volume = 100
            BtnMute.Image = Resources.ImagePlayerSound
            Mute = False
        Else
            AxPlayer.settings.volume = 0
            BtnMute.Image = Resources.ImagePlayerSoundMute
            Mute = True
        End If
    End Sub
    Private Sub ShowMedia()
        If AxPlayer.currentMedia Is Nothing Then
            PicBoxAlbumArt.Visible = False
            LblAlbumArtSelect.Visible = False
            TxtBoxLyrics.Visible = False
            AxPlayer.Visible = False
            PicBoxVisualizer.Visible = False
            TimerVisualizer.Stop()
        Else
            Dim tlfile As TagLib.File
            Try
                If Stream Then
                    tlfile = Nothing
                Else
                    tlfile = TagLib.File.Create(AxPlayer.URL)
                End If
            Catch ex As Exception
                WriteToLog("TagLib Error while Showing Media, Cannot read from file: " + AxPlayer.URL + Chr(13) + ex.Message)
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
                    If App.AudioExtensionDictionary.ContainsKey(Path.GetExtension(AxPlayer.URL)) Then 'Show Album Art
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
                                    PicBoxAlbumArt.Image = Image.FromStream(ms)
                                    PicBoxAlbumArt.Visible = True
                                Catch ex As Exception
                                    WriteToLog("Error Loading Album Art for " + AxPlayer.URL + vbCr + ex.Message)
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
                    ElseIf App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(AxPlayer.URL)) Then 'Show Video
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
        End If
    End Sub
    Friend Sub Suspend() 'Called when the user locks the screen or activates the screen saver
        If App.SuspendOnSessionChange Then
            Debug.Print("Suspending...")
            StopPlay()
            Me.WindowState = FormWindowState.Minimized
            App.WriteToLog("App Suspended @ " & Now)
        End If
    End Sub
    Private Sub VideoSetSize()
        Try
            If App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(AxPlayer.URL)) Then
                If AxPlayer.currentMedia.imageSourceHeight / AxPlayer.currentMedia.imageSourceWidth > PanelMedia.Height / PanelMedia.Width Then
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
    Private Sub SetPlaylistCountText()
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
        If AxPlayer.currentMedia Is Nothing Then
            LblPosition.ResetText()
        Else
            If App.PlayerPositionShowElapsed Then
                LblPosition.Text = "00:00"
            Else
                LblPosition.Text = "-" + FormatDuration(AxPlayer.currentMedia.duration)
            End If
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
    Private Sub SetAccentColor(Optional AsTheme As Boolean = False)
        Static c As Color
        c = App.GetAccentColor()
        If Not AsTheme Then SuspendLayout()
        If IsFocused Then
            MenuPlayer.BackColor = c
            TxtBoxPlaylistSearch.BackColor = c
        End If
        If App.CurrentTheme.IsAccent Then
            BackColor = c
            TxtBoxLyrics.BackColor = c
            TrackBarPosition.TrackBarGradientStart = c
            TrackBarPosition.TrackBarGradientEnd = c
        End If
        If Not AsTheme Then ResumeLayout()
        Debug.Print("Player Accent Color Set")
    End Sub
    Private Sub SetInactiveColor()
        MenuPlayer.BackColor = App.CurrentTheme.InactiveTitleBarColor
        TxtBoxPlaylistSearch.BackColor = App.CurrentTheme.InactiveTitleBarColor
    End Sub
    Friend Sub SetTheme()
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            SetAccentColor(True)
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
        ResumeLayout()
        Debug.Print("Player Theme Set")
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
                s = TextRenderer.MeasureText(CType(sender, ToolTip).GetToolTip(e.AssociatedControl), App.TipFont)
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
                Using p As New Pen(App.CurrentTheme.ButtonBackColor, CInt(App.TipFont.Size / 4)) 'Scale border thickness with font
                    g.DrawRectangle(p, 0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1)
                End Using

                'Draw text
                TextRenderer.DrawText(g, e.ToolTipText, App.TipFont, New Point(7, 7), App.CurrentTheme.TextColor)

                'Finalize
                brbg.Dispose()
                g.Dispose()

            End Sub

    End Sub

End Class
