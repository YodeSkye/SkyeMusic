
Imports System.IO
Imports SkyeMusic.My

Public Class Options

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private UIFolderBrowser As New FolderBrowserDialog
    Private uiFileBrowser As New OpenFileDialog

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            App.WriteToLog("Options WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub Options_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = "Options For " + My.Application.Info.Title
        SetTheme()

        'Settings
        Select Case App.PlayerPositionShowElapsed
            Case True : RadBtnElapsed.Checked = True
            Case False : RadBtnRemaining.Checked = True
        End Select
        CoBoxPlayMode.Items.Clear()
        CoBoxPlayMode.Items.Add("Play Once")
        CoBoxPlayMode.Items.Add("Repeat")
        CoBoxPlayMode.Items.Add("Play Next")
        CoBoxPlayMode.Items.Add("Shuffle")
        CoBoxPlayMode.SelectedIndex = App.PlayMode
        CoBoxPlaylistDefaultAction.Items.Clear()
        CoBoxPlaylistDefaultAction.Items.Add(App.PlaylistActions.Play.ToString)
        CoBoxPlaylistDefaultAction.Items.Add(App.PlaylistActions.Queue.ToString)
        CoBoxPlaylistDefaultAction.SelectedIndex = App.PlaylistDefaultAction
        CoBoxPlaylistSearchAction.Items.Clear()
        CoBoxPlaylistSearchAction.Items.Add(App.PlaylistActions.Play.ToString)
        CoBoxPlaylistSearchAction.Items.Add(App.PlaylistActions.Queue.ToString)
        CoBoxPlaylistSearchAction.Items.Add("Select Only")
        CoBoxPlaylistSearchAction.SelectedIndex = App.PlaylistSearchAction
        CoBoxTheme.Items.Clear()
        CoBoxTheme.Items.Add("Blue Accent")
        CoBoxTheme.Items.Add("Pink Accent")
        CoBoxTheme.Items.Add(App.Themes.Light.ToString)
        CoBoxTheme.Items.Add(App.Themes.Dark.ToString)
        CoBoxTheme.Items.Add(App.Themes.Pink.ToString)
        CoBoxTheme.Items.Add(App.Themes.Red.ToString)
        CoBoxTheme.SelectedIndex = App.Theme
        CoBoxPlaylistTitleFormat.Items.Clear()
        CoBoxPlaylistTitleFormat.Items.Add("Use FileName As Title")
        CoBoxPlaylistTitleFormat.Items.Add("Song")
        CoBoxPlaylistTitleFormat.Items.Add("Song Genre")
        CoBoxPlaylistTitleFormat.Items.Add("Song, Artist")
        CoBoxPlaylistTitleFormat.Items.Add("Song, Artist, Album")
        CoBoxPlaylistTitleFormat.Items.Add("Song, Album, Artist")
        CoBoxPlaylistTitleFormat.Items.Add("Song, Artist, Genre")
        CoBoxPlaylistTitleFormat.Items.Add("Song, Genre, Artist")
        CoBoxPlaylistTitleFormat.Items.Add("Song, Artist, Album, Genre")
        CoBoxPlaylistTitleFormat.Items.Add("Song, Album, Artist, Genre")
        CoBoxPlaylistTitleFormat.Items.Add("Song, Genre, Artist, Album")
        CoBoxPlaylistTitleFormat.Items.Add("Artist, Song")
        CoBoxPlaylistTitleFormat.Items.Add("Artist, Song, Album")
        CoBoxPlaylistTitleFormat.Items.Add("Artist, Album, Song")
        CoBoxPlaylistTitleFormat.Items.Add("Artist, Genre, Song")
        CoBoxPlaylistTitleFormat.Items.Add("Artist, Song, Genre")
        CoBoxPlaylistTitleFormat.Items.Add("Artist, Song, Album, Genre")
        CoBoxPlaylistTitleFormat.Items.Add("Artist, Genre, Song, Album")
        CoBoxPlaylistTitleFormat.Items.Add("Album, Song, Artist")
        CoBoxPlaylistTitleFormat.Items.Add("Album, Artist, Song")
        CoBoxPlaylistTitleFormat.Items.Add("Album, Genre, Song, Artist")
        CoBoxPlaylistTitleFormat.Items.Add("Album, Genre, Artist, Song")
        CoBoxPlaylistTitleFormat.Items.Add("Album, Song, Artist, Genre")
        CoBoxPlaylistTitleFormat.Items.Add("Album, Artist, Song, Genre")
        CoBoxPlaylistTitleFormat.Items.Add("Genre, Song")
        CoBoxPlaylistTitleFormat.Items.Add("Genre, Song, Artist")
        CoBoxPlaylistTitleFormat.Items.Add("Genre, Artist, Song")
        CoBoxPlaylistTitleFormat.Items.Add("Genre, Album, Song, Artist")
        CoBoxPlaylistTitleFormat.Items.Add("Genre, Album, Artist, Song")
        CoBoxPlaylistTitleFormat.Items.Add("Genre, Song, Artist, Album")
        CoBoxPlaylistTitleFormat.Items.Add("Genre, Song, Album, Artist")
        CoBoxPlaylistTitleFormat.SelectedIndex = App.PlaylistTitleFormat
        CkBoxPlaylistRemoveSpaces.Checked = App.PlaylistTitleRemoveSpaces
        TxtBoxPlaylistTitleSeparator.Text = App.PlaylistTitleSeparator
        TxtBoxPlaylistVideoIdentifier.Text = App.PlaylistVideoIdentifier
        LBLibrarySearchFolders.Items.Clear()
        For Each item As String In App.LibrarySearchFolders
            LBLibrarySearchFolders.Items.Add(item)
        Next
        CkBoxLibrarySearchSubFolders.Checked = App.LibrarySearchSubFolders
        CkBoxSaveWindowMetrics.Checked = App.SaveWindowMetrics
        CkBoxSuspendOnSessionChange.Checked = App.SuspendOnSessionChange
        TxtBoxHelperApp1Name.Text = App.HelperApp1Name
        TxtBoxHelperApp1Path.Text = App.HelperApp1Path
        If File.Exists(App.HelperApp1Path) Then
            TxtBoxHelperApp1Path.ForeColor = App.CurrentTheme.TextColor
        Else
            TxtBoxHelperApp1Path.ForeColor = Color.Red
        End If
        TxtBoxHelperApp2Name.Text = App.HelperApp2Name
        TxtBoxHelperApp2Path.Text = App.HelperApp2Path
        If File.Exists(App.HelperApp2Path) Then
            TxtBoxHelperApp2Path.ForeColor = App.CurrentTheme.TextColor
        Else
            TxtBoxHelperApp2Path.ForeColor = Color.Red
        End If
        TxtBoxRandomHistoryUpdateInterval.Text = App.RandomHistoryUpdateInterval.ToString
        TxtBoxHistoryUpdateInterval.Text = App.HistoryUpdateInterval.ToString
        TxtBoxHistoryAutoSaveInterval.Text = App.HistoryAutoSaveInterval.ToString
        SetPrunePlaylistButtonText()
        SetPruneHistoryButtonText()
    End Sub
    Private Sub Options_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        App.LibrarySearchFolders.Clear()
        For Each item In LBLibrarySearchFolders.Items
            App.LibrarySearchFolders.Add(item.ToString)
        Next
        App.SaveOptions()
        Player.ShowPlayMode()
    End Sub
    Private Sub Options_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, GrBoxTime.MouseDown, LblTitleFormat.MouseDown, LblTitleSeparator.MouseDown, LblVideoIdentifier.MouseDown, LblSongPlayMode.MouseDown, LblDefaultPlaylistAction.MouseDown, LblPlaylistSearchAction.MouseDown, LblTheme.MouseDown, LblHelperApp2Path.MouseDown, LblHelperApp2Name.MouseDown, LblHelperApp1Path.MouseDown, LblHelperApp1Name.MouseDown, TCOptions.MouseDown, TPApp.MouseDown, TPPlayer.MouseDown, TPPlaylist.MouseDown, TPLibrary.MouseDown, LblHistoryAutoSaveInterval1.MouseDown, LblHistoryAutoSaveInterval2.MouseDown, LblLibrarySearchFolders.MouseDown, LblHistoryUpdateInterval1.MouseDown, LblHistoryUpdateInterval2.MouseDown, LblPlaylistFormatting.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FixedFrameBorderSize.Width - 7, -e.Y - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 7)
            ElseIf cSender Is TCOptions Then
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FixedFrameBorderSize.Width - 7, -e.Y - cSender.Top - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 7)
            ElseIf cSender Is TPApp OrElse cSender Is TPLibrary OrElse cSender Is TPPlayer OrElse cSender Is TPPlaylist Then
                mOffset = New Point(-e.X - TCOptions.Left - cSender.Left - SystemInformation.FixedFrameBorderSize.Width - 9, -e.Y - TCOptions.Top - cSender.Top - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 9)
            Else
                mOffset = New Point(-e.X - TCOptions.Left - TPApp.Left - cSender.Left - SystemInformation.FixedFrameBorderSize.Width - 9, -e.Y - TCOptions.Top - TPApp.Top - cSender.Top - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 9)
            End If
        End If
        cSender = Nothing
    End Sub
    Private Sub Options_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, GrBoxTime.MouseMove, LblTitleFormat.MouseMove, LblTitleSeparator.MouseMove, LblVideoIdentifier.MouseMove, LblSongPlayMode.MouseMove, LblDefaultPlaylistAction.MouseMove, LblPlaylistSearchAction.MouseMove, LblTheme.MouseMove, LblHelperApp2Path.MouseMove, LblHelperApp2Name.MouseMove, LblHelperApp1Path.MouseMove, LblHelperApp1Name.MouseMove, TCOptions.MouseMove, TPApp.MouseMove, TPPlayer.MouseMove, TPPlaylist.MouseMove, TPLibrary.MouseMove, LblHistoryAutoSaveInterval1.MouseMove, LblHistoryAutoSaveInterval2.MouseMove, LblLibrarySearchFolders.MouseMove, LblHistoryUpdateInterval1.MouseMove, LblHistoryUpdateInterval2.MouseMove, LblPlaylistFormatting.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub Options_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, GrBoxTime.MouseUp, LblTitleFormat.MouseUp, LblTitleSeparator.MouseUp, LblVideoIdentifier.MouseUp, LblSongPlayMode.MouseUp, LblDefaultPlaylistAction.MouseUp, LblPlaylistSearchAction.MouseUp, LblTheme.MouseUp, LblHelperApp2Path.MouseUp, LblHelperApp2Name.MouseUp, LblHelperApp1Path.MouseUp, LblHelperApp1Name.MouseUp, TCOptions.MouseUp, TPApp.MouseUp, TPPlayer.MouseUp, TPPlaylist.MouseUp, TPLibrary.MouseUp, LblHistoryAutoSaveInterval1.MouseUp, LblHistoryAutoSaveInterval2.MouseUp, LblLibrarySearchFolders.MouseUp, LblHistoryUpdateInterval1.MouseUp, LblHistoryUpdateInterval2.MouseUp, LblPlaylistFormatting.MouseUp
        mMove = False
    End Sub
    Private Sub Options_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub
    Private Sub Options_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Close()
    End Sub

    'Control Events
    Private Sub TCOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TCOptions.SelectedIndexChanged
        Debug.Print(TCOptions.SelectedTab.Name)
        Select Case TCOptions.SelectedTab.Name
            Case "TPApp"
                CoBoxTheme.Focus()
            Case "TPPlayer"
                GrBoxTime.Focus()
            Case "TPPlaylist"
                CoBoxPlaylistTitleFormat.Focus()
            Case "TPLibrary"
                LBLibrarySearchFolders.Focus()
        End Select
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub
    Private Sub BtnPrunePlaylist_Click(sender As Object, e As EventArgs) Handles BtnPrunePlaylist.Click
        Player.PrunePlaylist()
        SetPrunePlaylistButtonText()
    End Sub
    Private Sub BtnHistoryPrune_Click(sender As Object, e As EventArgs) Handles BtnHistoryPrune.Click
        App.PruneHistory()
        SetPruneHistoryButtonText()
    End Sub
    Private Sub BtnHistorySaveNow_Click(sender As Object, e As EventArgs) Handles BtnHistorySaveNow.Click
        App.SaveHistory()
    End Sub
    Private Sub BtnLibrarySearchFoldersAdd_Click(sender As Object, e As EventArgs) Handles BtnLibrarySearchFoldersAdd.Click
        LibrarySearchFoldersAdd()
    End Sub
    Private Sub BtnHelperApp1_Click(sender As Object, e As EventArgs) Handles BtnHelperApp1.Click
        If String.IsNullOrEmpty(App.HelperApp1Path) Then
            uiFileBrowser.InitialDirectory = String.Empty
            uiFileBrowser.FileName = String.Empty
        Else
            Dim fInfo As New IO.FileInfo(App.HelperApp1Path)
            uiFileBrowser.InitialDirectory = fInfo.DirectoryName
            uiFileBrowser.FileName = fInfo.Name
            fInfo = Nothing
        End If
        Dim r As DialogResult = uiFileBrowser.ShowDialog(Me)
        If r = System.Windows.Forms.DialogResult.OK And Not String.IsNullOrEmpty(uiFileBrowser.FileName) Then
            TxtBoxHelperApp1Path.Text = uiFileBrowser.FileName
            TxtBoxHelperApp1Path.Focus()
            Validate()
        End If
    End Sub
    Private Sub BtnHelperApp2_Click(sender As Object, e As EventArgs) Handles BtnHelperApp2.Click
        If String.IsNullOrEmpty(App.HelperApp2Path) Then
            uiFileBrowser.InitialDirectory = String.Empty
            uiFileBrowser.FileName = String.Empty
        Else
            Dim fInfo As New IO.FileInfo(App.HelperApp2Path)
            uiFileBrowser.InitialDirectory = fInfo.DirectoryName
            uiFileBrowser.FileName = fInfo.Name
            fInfo = Nothing
        End If
        Dim r As DialogResult = uiFileBrowser.ShowDialog(Me)
        If r = System.Windows.Forms.DialogResult.OK And Not String.IsNullOrEmpty(uiFileBrowser.FileName) Then
            TxtBoxHelperApp2Path.Text = uiFileBrowser.FileName
            TxtBoxHelperApp2Path.Focus()
            Validate()
        End If
    End Sub
    Private Sub RadBtnElapsedClick(sender As Object, e As EventArgs) Handles RadBtnElapsed.Click
        App.PlayerPositionShowElapsed = True
        Player.SetTipPlayer()
    End Sub
    Private Sub RadBtnRemainingClick(sender As Object, e As EventArgs) Handles RadBtnRemaining.Click
        App.PlayerPositionShowElapsed = False
        Player.SetTipPlayer()
    End Sub
    Private Sub CoBoxPlayMode_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CoBoxPlayMode.SelectionChangeCommitted
        App.PlayMode = CType(CoBoxPlayMode.SelectedIndex, App.PlayModes)
        If App.PlayMode = PlayModes.Random Then Player.RandomHistoryClear()
        Player.SetTipPlayer()
    End Sub
    Private Sub CoBoxPlaylistDefaultAction_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CoBoxPlaylistDefaultAction.SelectionChangeCommitted
        App.PlaylistDefaultAction = CType(CoBoxPlaylistDefaultAction.SelectedIndex, App.PlaylistActions)
    End Sub
    Private Sub CoBoxPlaylistSearchAction_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CoBoxPlaylistSearchAction.SelectionChangeCommitted
        App.PlaylistSearchAction = CType(CoBoxPlaylistSearchAction.SelectedIndex, App.PlaylistActions)
    End Sub
    Private Sub CoBoxTheme_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CoBoxTheme.SelectionChangeCommitted
        App.Theme = CType(CoBoxTheme.SelectedIndex, App.Themes)
        Debug.Print("Theme set to " + App.Theme.ToString)
        App.CurrentTheme = App.GetCurrentThemeProperties
        SetTheme()
        If App.FRMLog IsNot Nothing Then
            App.FRMLog.SetTheme()
        End If
        App.FRMLibrary.SetTheme()
        Player.SetTheme()
    End Sub
    Private Sub CoBoxPlaylistTitleFormat_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CoBoxPlaylistTitleFormat.SelectionChangeCommitted
        App.PlaylistTitleFormat = CType(CoBoxPlaylistTitleFormat.SelectedIndex, App.PlaylistTitleFormats)
    End Sub
    Private Sub TxtBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxHelperApp1Path.KeyDown, TxtBoxHelperApp1Name.KeyDown, TxtBoxHelperApp2Name.KeyDown, TxtBoxHelperApp2Path.KeyDown, TxtBoxPlaylistTitleSeparator.KeyDown, TxtBoxPlaylistVideoIdentifier.KeyDown, TxtBoxHistoryAutoSaveInterval.KeyDown, TxtBoxHistoryUpdateInterval.KeyDown, TxtBoxRandomHistoryUpdateInterval.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            Validate()
        End If
    End Sub
    Private Sub TxtBoxNumbersOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TxtBoxHistoryAutoSaveInterval.KeyPress, TxtBoxHistoryUpdateInterval.KeyPress, TxtBoxRandomHistoryUpdateInterval.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then e.Handled = True
    End Sub
    Private Sub TxtBox_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxPlaylistTitleSeparator.PreviewKeyDown, TxtBoxPlaylistVideoIdentifier.PreviewKeyDown, MyBase.PreviewKeyDown, TxtBoxHelperApp1Path.PreviewKeyDown, TxtBoxHelperApp1Name.PreviewKeyDown, TxtBoxHelperApp2Name.PreviewKeyDown, TxtBoxHelperApp2Path.PreviewKeyDown, TxtBoxHistoryAutoSaveInterval.PreviewKeyDown, TxtBoxHistoryUpdateInterval.PreviewKeyDown, TxtBoxRandomHistoryUpdateInterval.PreviewKeyDown
        CMTxtBox.ShortcutKeys(CType(sender, TextBox), e)
    End Sub
    Private Sub TxtBoxPlaylistTitleSeparatorValidated(sender As Object, e As EventArgs) Handles TxtBoxPlaylistTitleSeparator.Validated
        PlaylistTitleSeparator = TxtBoxPlaylistTitleSeparator.Text
        TxtBoxPlaylistTitleSeparator.SelectAll()
    End Sub
    Private Sub TxtBoxPlaylistVideoIdentifierValidated(sender As Object, e As EventArgs) Handles TxtBoxPlaylistVideoIdentifier.Validated
        PlaylistVideoIdentifier = TxtBoxPlaylistVideoIdentifier.Text
        TxtBoxPlaylistVideoIdentifier.SelectAll()
    End Sub
    Private Sub TxtBoxHelperApp1NameValidated(sender As Object, e As EventArgs) Handles TxtBoxHelperApp1Name.Validated
        If Not HelperApp1Name = TxtBoxHelperApp1Name.Text Then
            HelperApp1Name = TxtBoxHelperApp1Name.Text
            TxtBoxHelperApp1Name.SelectAll()
            Debug.Print("TxtBoxHelperApp1NameValidated")
        End If
    End Sub
    Private Sub TxtBoxHelperApp1PathValidated(sender As Object, e As EventArgs) Handles TxtBoxHelperApp1Path.Validated
        If App.HelperApp1Path = TxtBoxHelperApp1Path.Text Then
            TxtBoxHelperApp1Path.ForeColor = App.CurrentTheme.TextColor
        Else
            If String.IsNullOrEmpty(TxtBoxHelperApp1Path.Text) OrElse My.Computer.FileSystem.FileExists(TxtBoxHelperApp1Path.Text) Then
                App.HelperApp1Path = TxtBoxHelperApp1Path.Text
                TxtBoxHelperApp1Path.ForeColor = App.CurrentTheme.TextColor
                TxtBoxHelperApp1Path.SelectAll()
                Debug.Print("TxtBoxHelperApp1PathValidated")
            Else
                TxtBoxHelperApp1Path.ForeColor = Color.Red
            End If
        End If
    End Sub
    Private Sub TxtBoxHelperApp2NameValidated(sender As Object, e As EventArgs) Handles TxtBoxHelperApp2Name.Validated
        If Not HelperApp2Name = TxtBoxHelperApp2Name.Text Then
            HelperApp2Name = TxtBoxHelperApp2Name.Text
            TxtBoxHelperApp2Name.SelectAll()
            Debug.Print("TxtBoxHelperApp2NameValidated")
        End If
    End Sub
    Private Sub TxtBoxHelperApp2PathValidated(sender As Object, e As EventArgs) Handles TxtBoxHelperApp2Path.Validated
        If App.HelperApp2Path = TxtBoxHelperApp2Path.Text Then
            TxtBoxHelperApp2Path.ForeColor = App.CurrentTheme.TextColor
        Else
            If String.IsNullOrEmpty(TxtBoxHelperApp2Path.Text) OrElse My.Computer.FileSystem.FileExists(TxtBoxHelperApp2Path.Text) Then
                App.HelperApp2Path = TxtBoxHelperApp2Path.Text
                TxtBoxHelperApp2Path.ForeColor = App.CurrentTheme.TextColor
                TxtBoxHelperApp2Path.SelectAll()
                Debug.Print("TxtBoxHelperApp2PathValidated")
            Else
                TxtBoxHelperApp2Path.ForeColor = Color.Red
            End If
        End If
    End Sub
    Private Sub TxtBoxRandomHistoryUpdateInterval_Validated(sender As Object, e As EventArgs) Handles TxtBoxRandomHistoryUpdateInterval.Validated
        If Not String.IsNullOrEmpty(TxtBoxRandomHistoryUpdateInterval.Text) Then
            Dim interval As Byte
            Try
                interval = CByte(Val(TxtBoxRandomHistoryUpdateInterval.Text))
                If interval > 60 Then
                    interval = 60
                End If
            Catch
                interval = 5
            End Try
            TxtBoxRandomHistoryUpdateInterval.Text = interval.ToString
            TxtBoxRandomHistoryUpdateInterval.SelectAll()
            Debug.Print("TxtBoxRandomHistoryUpdateInterval_Validated")
            App.RandomHistoryUpdateInterval = interval
        End If
    End Sub
    Private Sub TxtBoxHistoryUpdateInterval_Validated(sender As Object, e As EventArgs) Handles TxtBoxHistoryUpdateInterval.Validated
        If Not String.IsNullOrEmpty(TxtBoxHistoryUpdateInterval.Text) Then
            Dim interval As Byte
            Try
                interval = CByte(Val(TxtBoxHistoryUpdateInterval.Text))
                If interval > 60 Then
                    interval = 60
                End If
            Catch
                interval = 5
            End Try
            TxtBoxHistoryUpdateInterval.Text = interval.ToString
            TxtBoxHistoryUpdateInterval.SelectAll()
            Debug.Print("TxtBoxHistoryUpdateInterval_Validated")
            App.HistoryUpdateInterval = interval
        End If
    End Sub
    Private Sub TxtBoxHistoryAutoSaveInterval_Validated(sender As Object, e As EventArgs) Handles TxtBoxHistoryAutoSaveInterval.Validated
        If Not String.IsNullOrEmpty(TxtBoxHistoryAutoSaveInterval.Text) Then
            Dim interval As UShort
            Try
                interval = CUShort(Val(TxtBoxHistoryAutoSaveInterval.Text))
                If interval < 1 Then
                    interval = 1
                ElseIf interval > 1440 Then
                    interval = 1440
                End If
            Catch
                interval = 5
            End Try
            TxtBoxHistoryAutoSaveInterval.Text = interval.ToString
            TxtBoxHistoryAutoSaveInterval.SelectAll()
            Debug.Print("TxtBoxHistoryAutoSaveInterval_Validated")
            App.HistoryAutoSaveInterval = interval
            App.SetHistoryAutoSaveTimer()
        End If
    End Sub
    Private Sub CkBoxPlaylistRemoveSpacesClick(sender As Object, e As EventArgs) Handles CkBoxPlaylistRemoveSpaces.Click
        App.PlaylistTitleRemoveSpaces = Not App.PlaylistTitleRemoveSpaces
    End Sub
    Private Sub CkBoxSearchSubFoldersClick(sender As Object, e As EventArgs) Handles CkBoxLibrarySearchSubFolders.Click
        App.LibrarySearchSubFolders = Not App.LibrarySearchSubFolders
    End Sub
    Private Sub CkBoxSaveWindowMetricsClick(sender As Object, e As EventArgs) Handles CkBoxSaveWindowMetrics.Click
        App.SaveWindowMetrics = Not App.SaveWindowMetrics
    End Sub
    Private Sub CkBoxSuspendOnSessionChange_Click(sender As Object, e As EventArgs) Handles CkBoxSuspendOnSessionChange.Click
        App.SuspendOnSessionChange = Not App.SuspendOnSessionChange
    End Sub
    Private Sub LBLibrarySearchFoldersKeyDown(sender As Object, e As KeyEventArgs) Handles LBLibrarySearchFolders.KeyDown
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
                Case Keys.Escape
                Case Keys.End
                Case Keys.Up
                Case Keys.Left
                Case Keys.Right
                Case Keys.Space
                    'TogglePlay()
                    'e.SuppressKeyPress = True
                Case Keys.OemQuestion
                Case Keys.PageUp
                Case Keys.PageDown
                Case Keys.Home
                Case Keys.Delete
                    LibrarySearchFoldersRemove()
                Case Keys.Insert
            End Select
        End If

    End Sub
    Private Sub LBLibrarySearchFoldersMouseDown(sender As Object, e As MouseEventArgs) Handles LBLibrarySearchFolders.MouseDown
        If e.Button = MouseButtons.Right Then
            LBLibrarySearchFolders.SelectedIndex = LBLibrarySearchFolders.IndexFromPoint(e.X, e.Y)
        End If
    End Sub
    Private Sub CMLibrarySearchFolders_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMLibrarySearchFolders.Opening
        If LBLibrarySearchFolders.SelectedItems.Count = 0 Then
            CMILibrarySearchFoldersRemove.Enabled = False
        Else
            CMILibrarySearchFoldersRemove.Enabled = True
        End If
    End Sub
    Private Sub CMILibrarySearchFoldersAddClick(sender As Object, e As EventArgs) Handles CMILibrarySearchFoldersAdd.Click
        LibrarySearchFoldersAdd()
    End Sub
    Private Sub CMILibrarySearchFoldersRemoveClick(sender As Object, e As EventArgs) Handles CMILibrarySearchFoldersRemove.Click
        LibrarySearchFoldersRemove()
    End Sub

    'Procedures
    Private Sub LibrarySearchFoldersAdd()
        Dim r As DialogResult = UIFolderBrowser.ShowDialog(Me)
        If r = DialogResult.OK And Not UIFolderBrowser.SelectedPath = String.Empty And Not LBLibrarySearchFolders.Items.Contains(UIFolderBrowser.SelectedPath) Then
            LBLibrarySearchFolders.Items.Add(UIFolderBrowser.SelectedPath)
        End If
        LBLibrarySearchFolders.Focus()
    End Sub
    Private Sub LibrarySearchFoldersRemove()
        If LBLibrarySearchFolders.SelectedItems.Count = 1 Then
            LBLibrarySearchFolders.Items.RemoveAt(LBLibrarySearchFolders.SelectedIndex)
        End If
    End Sub
    Private Sub SetPrunePlaylistButtonText()
        BtnPrunePlaylist.Text = BtnPrunePlaylist.Text.TrimEnd(App.TrimEndSearch) + " (" + Player.LVPlaylist.Items.Count.ToString + ")"
        BtnPrunePlaylist.Enabled = (Player.LVPlaylist.Items.Count > 0)
    End Sub
    Private Sub SetPruneHistoryButtonText()
        BtnHistoryPrune.Text = BtnHistoryPrune.Text.TrimEnd(App.TrimEndSearch) + " (" + App.History.Count.ToString + ")"
        BtnHistoryPrune.Enabled = (App.History.Count > 0)
        BtnHistorySaveNow.Enabled = (App.History.Count > 0)
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
    Private Sub SetAccentColor(Optional AsTheme As Boolean = False)
        Static c As Color
        If Not AsTheme Then SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            BackColor = c
            GrBoxTime.BackColor = c
            RadBtnRemaining.BackColor = c
            CkBoxLibrarySearchSubFolders.BackColor = c
            TCOptions.TabPanelBackColor = c
        End If
        If Not AsTheme Then ResumeLayout()
        Debug.Print("Options Accent Color Set")
    End Sub
    Private Sub SetTheme()
        Static forecolor As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            SetAccentColor(True)
            forecolor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            GrBoxTime.BackColor = App.CurrentTheme.BackColor
            RadBtnRemaining.BackColor = App.CurrentTheme.BackColor
            CkBoxLibrarySearchSubFolders.BackColor = App.CurrentTheme.BackColor
            TCOptions.TabPanelBackColor = App.CurrentTheme.BackColor
            forecolor = App.CurrentTheme.TextColor
        End If
        TCOptions.ActiveTabColor = App.CurrentTheme.ButtonBackColor
        TCOptions.ActiveTabForeColor = App.CurrentTheme.ButtonTextColor
        TCOptions.InactiveTabColor = App.CurrentTheme.ControlBackColor
        TCOptions.InActiveTabForeColor = forecolor
        CoBoxPlayMode.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxPlayMode.ForeColor = App.CurrentTheme.TextColor
        CoBoxPlaylistDefaultAction.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxPlaylistDefaultAction.ForeColor = App.CurrentTheme.TextColor
        CoBoxPlaylistSearchAction.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxPlaylistSearchAction.ForeColor = App.CurrentTheme.TextColor
        CoBoxTheme.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxTheme.ForeColor = App.CurrentTheme.TextColor
        CoBoxPlaylistTitleFormat.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxPlaylistTitleFormat.ForeColor = App.CurrentTheme.TextColor
        TxtBoxPlaylistTitleSeparator.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxPlaylistTitleSeparator.ForeColor = App.CurrentTheme.TextColor
        TxtBoxPlaylistVideoIdentifier.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxPlaylistVideoIdentifier.ForeColor = App.CurrentTheme.TextColor
        LBLibrarySearchFolders.BackColor = App.CurrentTheme.ControlBackColor
        LBLibrarySearchFolders.ForeColor = App.CurrentTheme.TextColor
        TxtBoxHelperApp1Name.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxHelperApp1Name.ForeColor = App.CurrentTheme.TextColor
        TxtBoxHelperApp1Path.BackColor = App.CurrentTheme.ControlBackColor
        If Not TxtBoxHelperApp1Path.ForeColor = Color.Red Then TxtBoxHelperApp1Path.ForeColor = App.CurrentTheme.TextColor
        TxtBoxHelperApp2Name.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxHelperApp2Name.ForeColor = App.CurrentTheme.TextColor
        TxtBoxHelperApp2Path.BackColor = App.CurrentTheme.ControlBackColor
        If Not TxtBoxHelperApp2Path.ForeColor = Color.Red Then TxtBoxHelperApp2Path.ForeColor = App.CurrentTheme.TextColor
        BtnLibrarySearchFoldersAdd.BackColor = App.CurrentTheme.ButtonBackColor
        BtnHelperApp1.BackColor = App.CurrentTheme.ButtonBackColor
        BtnHelperApp2.BackColor = App.CurrentTheme.ButtonBackColor
        GrBoxTime.ForeColor = forecolor
        LblSongPlayMode.ForeColor = forecolor
        LblDefaultPlaylistAction.ForeColor = forecolor
        LblPlaylistSearchAction.ForeColor = forecolor
        LblTheme.ForeColor = forecolor
        LblLibrarySearchFolders.ForeColor = forecolor
        LblPlaylistFormatting.ForeColor = forecolor
        LblTitleFormat.ForeColor = forecolor
        LblTitleSeparator.ForeColor = forecolor
        LblVideoIdentifier.ForeColor = forecolor
        CkBoxSaveWindowMetrics.ForeColor = forecolor
        CkBoxSuspendOnSessionChange.ForeColor = forecolor
        CkBoxLibrarySearchSubFolders.ForeColor = forecolor
        CkBoxPlaylistRemoveSpaces.ForeColor = forecolor
        LblHelperApp1Name.ForeColor = forecolor
        LblHelperApp1Path.ForeColor = forecolor
        LblHelperApp2Name.ForeColor = forecolor
        LblHelperApp2Path.ForeColor = forecolor
        LblRandomHistoryUpdateInterval1.ForeColor = forecolor
        LblRandomHistoryUpdateInterval2.ForeColor = forecolor
        LblHistoryUpdateInterval1.ForeColor = forecolor
        LblHistoryUpdateInterval2.ForeColor = forecolor
        LblHistoryAutoSaveInterval1.ForeColor = forecolor
        LblHistoryAutoSaveInterval2.ForeColor = forecolor
        TxtBoxRandomHistoryUpdateInterval.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxRandomHistoryUpdateInterval.ForeColor = App.CurrentTheme.TextColor
        TxtBoxHistoryUpdateInterval.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxHistoryUpdateInterval.ForeColor = App.CurrentTheme.TextColor
        TxtBoxHistoryAutoSaveInterval.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxHistoryAutoSaveInterval.ForeColor = App.CurrentTheme.TextColor
        BtnPrunePlaylist.BackColor = App.CurrentTheme.ButtonBackColor
        BtnPrunePlaylist.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnHistorySaveNow.BackColor = App.CurrentTheme.ButtonBackColor
        BtnHistorySaveNow.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnHistoryPrune.BackColor = App.CurrentTheme.ButtonBackColor
        BtnHistoryPrune.ForeColor = App.CurrentTheme.ButtonTextColor
        TipOptions.BackColor = App.CurrentTheme.BackColor
        TipOptions.ForeColor = App.CurrentTheme.TextColor
        TipOptions.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
        Debug.Print("Options Theme Set")
    End Sub

End Class
