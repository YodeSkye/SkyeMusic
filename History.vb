
Imports LibVLCSharp.Shared

Public Class History

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private Enum HistoryView
        MostPlayed
        RecentlyPlayed
        Favorites
    End Enum
    Private CurrentView As HistoryView = HistoryView.MostPlayed
    Private CurrentViewMaxRecords As Integer = CInt(App.HistoryViewMaxRecords)
    Private views As List(Of App.SongView)

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            App.WriteToLog("Help WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Async Sub History_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title + " History & Statistics"
        SetAccentColor()
        SetTheme()
#If DEBUG Then
        'If App.SaveWindowMetrics AndAlso App.HistoryLocation.Y >= 0 Then Me.Location = App.HistoryLocation
        'If App.SaveWindowMetrics AndAlso App.HistorySize.Height >= 0 Then Me.Size = App.HistorySize
#Else
        If App.SaveWindowMetrics AndAlso App.HistoryLocation.Y >= 0 Then Me.Location = App.HistoryLocation
        If App.SaveWindowMetrics AndAlso App.HistorySize.Height >= 0 Then Me.Size = App.HistorySize
#End If

        views = Await GetDataAsync()
        PutData()

        'Define 7 Columns
        Dim header As ColumnHeader
        header = New ColumnHeader()
        header.Name = "Title"
        header.Text = "Title"
        header.Width = 200
        LVHistory.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Artist"
        header.Text = "Artist"
        header.Width = 200
        LVHistory.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Album"
        header.Text = "Album"
        header.Width = 200
        LVHistory.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Genre"
        header.Text = "Genre"
        header.Width = 100
        LVHistory.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "PlayCount"
        header.Text = "Plays"
        header.Width = 50
        LVHistory.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "LastPlayed"
        header.Text = "Last Played"
        header.Width = 200
        LVHistory.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Path"
        header.Text = "Path"
        header.Width = 400
        LVHistory.Columns.Add(header)

        PutViewData()

        Select Case CurrentView
            Case HistoryView.MostPlayed
                RadBtnMostPlayed.Checked = True
            Case HistoryView.RecentlyPlayed
                RadBtnRecentlyPlayed.Checked = True
            Case HistoryView.Favorites
                RadBtnFavorites.Checked = True
        End Select
        Select Case App.HistoryViewMaxRecords
            Case 0
                TxtBoxMaxRecords.Text = "All"
            Case Else
                TxtBoxMaxRecords.Text = App.HistoryViewMaxRecords.ToString
        End Select
        SetShowAll()

        BtnOK.Focus()
    End Sub
    Private Sub History_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If CurrentViewMaxRecords <> App.HistoryViewMaxRecords Then
            App.HistoryViewMaxRecords = CUShort(CurrentViewMaxRecords)
            App.SaveOptions()
        End If
    End Sub
    Private Sub History_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, LblMostPlayedSong.MouseDown, LblSessionPlayedSongs.MouseDown, LblTotalDuration.MouseDown, LblTotalPlayedSongs.MouseDown, GrpBoxHistory.MouseDown
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
        cSender = Nothing
    End Sub
    Private Sub History_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, LblMostPlayedSong.MouseMove, LblSessionPlayedSongs.MouseMove, LblTotalDuration.MouseMove, LblTotalPlayedSongs.MouseMove, GrpBoxHistory.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub History_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, LblMostPlayedSong.MouseUp, LblSessionPlayedSongs.MouseUp, LblTotalDuration.MouseUp, LblTotalPlayedSongs.MouseUp, GrpBoxHistory.MouseUp
        mMove = False
    End Sub
    Private Sub History_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.HistoryLocation = Location
        End If
    End Sub
    Private Sub History_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.HistorySize = Me.Size
        End If
    End Sub
    Private Sub History_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub

    'Control Events
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub
    Private Sub BtnShowAll_Click(sender As Object, e As EventArgs) Handles BtnShowAll.Click
        CurrentViewMaxRecords = 0
        TxtBoxMaxRecords.Text = "All"
        SetShowAll()
        PutViewData()
    End Sub
    Private Sub BtnQueueAll_Click(sender As Object, e As EventArgs) Handles BtnQueueAll.Click
        Queue(True)
    End Sub
    Private Sub BtnAddAllToPlaylist_Click(sender As Object, e As EventArgs) Handles BtnAddAllToPlaylist.Click
        AddToPlaylist(True)
    End Sub
    Private Sub CMHistoryView_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMHistoryView.Opening
        CMIQueue.Text = CMIQueue.Text.TrimEnd(App.TrimEndSearch) & " (" & LVHistory.SelectedItems.Count.ToString("N0") & ")"
        CMIAddToPlaylist.Text = CMIAddToPlaylist.Text.TrimEnd(App.TrimEndSearch) & " (" & LVHistory.SelectedItems.Count.ToString("N0") & ")"
    End Sub
    Private Sub CMIQueue_Click(sender As Object, e As EventArgs) Handles CMIQueue.Click
        Queue()
    End Sub
    Private Sub CMIAddToPlaylist_Click(sender As Object, e As EventArgs) Handles CMIAddToPlaylist.Click
        AddToPlaylist()
    End Sub
    Private Sub LVHistory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVHistory.SelectedIndexChanged
        ShowCounts()
    End Sub
    Private Sub RadBtnMostPlayed_Click(sender As Object, e As EventArgs) Handles RadBtnMostPlayed.Click
        CurrentView = HistoryView.MostPlayed
        PutViewData()
    End Sub
    Private Sub RadBtnRecentlyPlayed_Click(sender As Object, e As EventArgs) Handles RadBtnRecentlyPlayed.Click
        CurrentView = HistoryView.RecentlyPlayed
        PutViewData()
    End Sub
    Private Sub RadBtnFavorites_Click(sender As Object, e As EventArgs) Handles RadBtnFavorites.Click
        CurrentView = HistoryView.Favorites
        PutViewData()
    End Sub
    Private Sub TxtBoxMaxRecords_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxMaxRecords.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            TxtBoxMaxRecords_Validated(sender, EventArgs.Empty)
        End If
    End Sub
    Private Sub TxtBoxMaxRecords_Validated(sender As Object, e As EventArgs) Handles TxtBoxMaxRecords.Validated
        Try
            CurrentViewMaxRecords = CInt(TxtBoxMaxRecords.Text)
        Catch
            CurrentViewMaxRecords = 0
            TxtBoxMaxRecords.Text = "All"
        End Try
        SetShowAll()
        PutViewData()
    End Sub

    'Procedures
    Private Async Function GetDataAsync() As Task(Of List(Of App.SongView))

        'Show loading message
        Text &= " - LOADING..."
        GrpBoxHistory.Enabled = False

        'Run the heavy work off the UI thread
        Dim views = Await Task.Run(Function()
                                       Dim list As New List(Of App.SongView)
                                       For Each s As App.Song In App.History
                                           list.Add(New App.SongView(s))
                                       Next
                                       Return list
                                   End Function)

        'Now we're back on the UI thread after Await
        Text = Text.TrimEnd(" - LOADING...".ToCharArray)
        GrpBoxHistory.Enabled = True

        Return views
    End Function
    Private Sub PutData()
        TxtBoxSessionPlayedSongs.Text = App.HistoryTotalPlayedSongsThisSession.ToString("N0")
        TxtBoxTotalPlayedSongs.Text = App.HistoryTotalPlayedSongs.ToString("N0")
        Dim TotalDuration As TimeSpan = TimeSpan.Zero
        For Each v As App.SongView In views
            TotalDuration += (v.Duration * v.Data.PlayCount)
        Next
        TxtBoxTotalDuration.Text = $"{CInt(TotalDuration.TotalDays)}d {TotalDuration.Hours:D2}:{TotalDuration.Minutes:D2}:{TotalDuration.Seconds:D2}"
        Dim mostPlayed = views.OrderByDescending(Function(v) v.Data.PlayCount).ThenByDescending(Function(v) v.Data.LastPlayed).FirstOrDefault()
        If mostPlayed Is Nothing Then
            TxtBoxMostPlayedSong.Text = "No songs played yet."
        Else
            TxtBoxMostPlayedSong.Text = $"{mostPlayed.Title} - {mostPlayed.Artist} ({mostPlayed.Data.PlayCount} plays)"
        End If
    End Sub
    Private Sub PutViewData()
        LVHistory.BeginUpdate()
        LVHistory.Items.Clear()
        Dim sortedViews As IEnumerable(Of App.SongView) = Nothing
        Select Case CurrentView
            Case HistoryView.MostPlayed
                sortedViews = views.OrderByDescending(Function(v) v.Data.PlayCount).ThenByDescending(Function(v) v.Data.LastPlayed)
            Case HistoryView.RecentlyPlayed
                sortedViews = views.OrderByDescending(Function(v) v.Data.LastPlayed)
            Case HistoryView.Favorites
                sortedViews = views.OrderByDescending(Function(v) v.Data.Rating).ThenByDescending(Function(v) v.Data.LastPlayed)
        End Select
        If CurrentViewMaxRecords > 0 Then
            sortedViews = sortedViews.Take(CurrentViewMaxRecords)
        End If
        For Each v As App.SongView In sortedViews
            Dim lvi As ListViewItem
            If App.IsUrl(v.Data.Path) Then
                lvi = New ListViewItem(v.Data.Path)
            Else
                lvi = New ListViewItem(v.Title)
            End If
            lvi.SubItems.Add(v.Artist)
            lvi.SubItems.Add(v.Album)
            lvi.SubItems.Add(v.Genre)
            lvi.SubItems.Add(v.Data.PlayCount.ToString("N0"))
            lvi.SubItems.Add(v.Data.LastPlayed.ToString("g"))
            lvi.SubItems.Add(v.Data.Path)
            LVHistory.Items.Add(lvi)
        Next
        LVHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        LVHistory.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.HeaderSize)
        LVHistory.Columns(4).TextAlign = HorizontalAlignment.Center
        LVHistory.EndUpdate()
        ShowCounts()
    End Sub
    Private Sub Queue(Optional queueall As Boolean = False)
        Dim queuelist = If(queueall, LVHistory.Items.Cast(Of ListViewItem).ToList(), LVHistory.SelectedItems.Cast(Of ListViewItem).ToList())
        If queuelist.Count > 0 Then
            Dim pathindex = LVHistory.Columns("Path").Index
            For Each lvi As ListViewItem In queuelist
                Player.QueuePath(lvi.SubItems(pathindex).Text)
            Next
        End If
    End Sub
    Private Sub AddToPlaylist(Optional addall As Boolean = False)
        Dim addlist = If(addall, LVHistory.Items.Cast(Of ListViewItem).ToList(), LVHistory.SelectedItems.Cast(Of ListViewItem).ToList())
        If addlist.Count > 0 Then
            Dim pathindex = LVHistory.Columns("Path").Index
            Dim paths As New List(Of String)
            For Each lvi As ListViewItem In addlist
                paths.Add(lvi.SubItems(pathindex).Text)
            Next
            Player.AddToPlaylist(paths)
        End If
    End Sub
    Private Sub SetShowAll()
        If CurrentViewMaxRecords = 0 Then
            BtnShowAll.Enabled = False
        Else
            BtnShowAll.Enabled = True
        End If
    End Sub
    Private Sub ShowCounts()
        LblHistoryViewCount.Text = LVHistory.Items.Count.ToString("N0") & If(LVHistory.Items.Count = 1, " song", " songs")
        LblHistoryViewCount.Text &= ", " & LVHistory.SelectedItems.Count.ToString("N0") & " selected"
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
    Private Sub SetAccentColor()
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            BackColor = c
        End If
        ResumeLayout()
        Debug.Print("Help Accent Color Set")
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If Not App.CurrentTheme.IsAccent Then
            BackColor = App.CurrentTheme.BackColor
        End If
        LblMostPlayedSong.ForeColor = App.CurrentTheme.TextColor
        TxtBoxMostPlayedSong.BackColor = App.CurrentTheme.BackColor
        TxtBoxMostPlayedSong.ForeColor = App.CurrentTheme.TextColor
        LblSessionPlayedSongs.ForeColor = App.CurrentTheme.TextColor
        TxtBoxSessionPlayedSongs.BackColor = App.CurrentTheme.BackColor
        TxtBoxSessionPlayedSongs.ForeColor = App.CurrentTheme.TextColor
        LblTotalPlayedSongs.ForeColor = App.CurrentTheme.TextColor
        TxtBoxTotalPlayedSongs.BackColor = App.CurrentTheme.BackColor
        TxtBoxTotalPlayedSongs.ForeColor = App.CurrentTheme.TextColor
        LblTotalDuration.ForeColor = App.CurrentTheme.TextColor
        TxtBoxTotalDuration.BackColor = App.CurrentTheme.BackColor
        TxtBoxTotalDuration.ForeColor = App.CurrentTheme.TextColor
        LVHistory.BackColor = App.CurrentTheme.BackColor
        LVHistory.ForeColor = App.CurrentTheme.TextColor
        GrpBoxHistory.ForeColor = App.CurrentTheme.TextColor
        RadBtnMostPlayed.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnMostPlayed.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnRecentlyPlayed.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnRecentlyPlayed.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnFavorites.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnFavorites.ForeColor = App.CurrentTheme.ButtonTextColor
        LblMaxRecords.ForeColor = App.CurrentTheme.TextColor
        TxtBoxMaxRecords.BackColor = App.CurrentTheme.BackColor
        TxtBoxMaxRecords.ForeColor = App.CurrentTheme.TextColor
        BtnShowAll.BackColor = App.CurrentTheme.ButtonBackColor
        BtnShowAll.ForeColor = App.CurrentTheme.ButtonTextColor
        LblHistoryViewCount.ForeColor = App.CurrentTheme.TextColor
        ResumeLayout()
        Debug.Print("Help Theme Set")
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor()
        SetTheme()
    End Sub

End Class
