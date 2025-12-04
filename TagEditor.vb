
Public Class TagEditor

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Drawing.Point
    Private _paths As List(Of String)
    Private _haschanged As Boolean = False
    Private _libraryneedsupdated As Boolean = False
    Private Property HasChanged As Boolean
        Get
            Return _haschanged
        End Get
        Set(value As Boolean)
            _haschanged = value
            BtnSave.Enabled = value
        End Set
    End Property
    Dim multiMessage As String = "< Keep Original >"
    Dim oArtist As String = String.Empty
    Dim oTitle As String = String.Empty
    Dim oAlbum As String = String.Empty
    Dim oGenre As String = String.Empty
    Dim oYear As String = String.Empty
    Dim oTracks As String = String.Empty
    Dim oComments As String = String.Empty

    ' Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            App.WriteToLog("TagEditor WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Public Sub New(paths As List(Of String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _paths = paths
        Text = My.Application.Info.Title & " " & Text
        SetAccentColor()
        SetTheme()
        For Each s As String In TagLib.Genres.Audio
            CoBoxGenre.Items.Add(s)
        Next
        For Each s As String In TagLib.Genres.Video
            If Not CoBoxGenre.Items.Contains(s) Then CoBoxGenre.Items.Add(s)
        Next
    End Sub
    Private Sub TagEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetTags()
    End Sub
    Private Sub TagEditor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If _libraryneedsupdated Then DialogResult = DialogResult.OK
    End Sub
    Private Sub TagEditor_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
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
    Private Sub TagEditor_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub TagEditor_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        mMove = False
    End Sub
    Private Sub TagEditor_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub

    ' Control Events
    Private Sub TxtBox_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtBoxAlbum.KeyUp, TxtBoxArtist.KeyUp, TxtBoxTitle.KeyUp, TxtBoxYear.KeyUp, TxtBoxTracks.KeyUp, TxtBoxComments.KeyUp, TxtBoxGenre.KeyUp
        Select Case e.KeyCode
            Case Keys.Enter
                e.Handled = True
                Dim tb = TryCast(sender, TextBox)
                If tb IsNot Nothing Then tb.SelectAll()
            Case Keys.Tab
                e.Handled = True
            Case Else
                HasChanged = SetSave()
                Dim tb = TryCast(sender, TextBox)
                If tb Is TxtBoxArtist Then
                    If HasChanged Then
                        LblArtist.Font = New Font(LblArtist.Font, FontStyle.Bold)
                    Else
                        LblArtist.Font = New Font(LblArtist.Font, FontStyle.Regular)
                    End If
                    btnArtistKeepOriginal.Enabled = HasChanged
                ElseIf tb Is TxtBoxTitle Then
                    If HasChanged Then
                        LblTitle.Font = New Font(LblTitle.Font, FontStyle.Bold)
                    Else
                        LblTitle.Font = New Font(LblTitle.Font, FontStyle.Regular)
                    End If
                    BtnTitleKeepOriginal.Enabled = HasChanged
                ElseIf tb Is TxtBoxAlbum Then
                    If HasChanged Then
                        LbLAlbum.Font = New Font(LbLAlbum.Font, FontStyle.Bold)
                    Else
                        LbLAlbum.Font = New Font(LbLAlbum.Font, FontStyle.Regular)
                    End If
                    BtnAlbumKeepOriginal.Enabled = HasChanged
                End If
        End Select
    End Sub
    Private Sub TxtBox_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxAlbum.PreviewKeyDown, TxtBoxTitle.PreviewKeyDown, TxtBoxArtist.PreviewKeyDown, TxtBoxYear.PreviewKeyDown, TxtBoxTracks.PreviewKeyDown, TxtBoxComments.PreviewKeyDown, TxtBoxGenre.PreviewKeyDown
        CMBasic.ShortcutKeys(TryCast(sender, TextBox), e)
    End Sub
    Private Sub TxtBoxNumbersOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TxtBoxYear.KeyPress, TxtBoxTracks.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then e.Handled = True
    End Sub
    Private Sub CoBoxGenre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxGenre.SelectedIndexChanged
        TxtBoxGenre.Text = CoBoxGenre.SelectedItem.ToString
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If HasChanged Then SaveTags()
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub
    Private Sub btnArtistKeepOriginal_Click(sender As Object, e As EventArgs) Handles btnArtistKeepOriginal.Click
        TxtBoxArtist.Text = oArtist
        LblArtist.Font = New Font(LblArtist.Font, FontStyle.Regular)
        btnArtistKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxArtist.Focus()
        TxtBoxArtist.SelectAll()
    End Sub
    Private Sub BtnTitleKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnTitleKeepOriginal.Click
        TxtBoxTitle.Text = oTitle
        LblTitle.Font = New Font(LblArtist.Font, FontStyle.Regular)
        BtnTitleKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxTitle.Focus()
        TxtBoxTitle.SelectAll()
    End Sub
    Private Sub BtnAlbumKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnAlbumKeepOriginal.Click
        TxtBoxAlbum.Text = oAlbum
        LbLAlbum.Font = New Font(LblArtist.Font, FontStyle.Regular)
        BtnAlbumKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxAlbum.Focus()
        TxtBoxAlbum.SelectAll()
    End Sub

    ' Methods
    Private Sub GetTags()
        If _paths.Count > 0 Then
            Dim tlfile As TagLib.File = Nothing
            Dim removelist As New List(Of String)

            For Each path In _paths
                Try
                    tlfile = TagLib.File.Create(path)
                Catch ex As Exception
                    WriteToLog("TagLib Error while Editing Tag, Cannot read from file: " + path + Chr(13) + ex.Message)
                    removelist.Add(path)
                    tlfile = Nothing
                End Try
                If tlfile IsNot Nothing Then
                    ' Artist
                    If String.IsNullOrWhiteSpace(oArtist) Then
                        oArtist = tlfile.Tag.FirstPerformer
                    Else
                        If Not String.Equals(oArtist, tlfile.Tag.FirstPerformer, StringComparison.Ordinal) Then
                            oArtist = multiMessage
                        End If
                    End If
                    ' Title
                    If String.IsNullOrWhiteSpace(oTitle) Then
                        oTitle = tlfile.Tag.Title
                    Else
                        If Not String.Equals(oTitle, tlfile.Tag.Title, StringComparison.Ordinal) Then
                            oTitle = multiMessage
                        End If
                    End If
                    ' Album
                    If String.IsNullOrWhiteSpace(oAlbum) Then
                        oAlbum = tlfile.Tag.Album
                    Else
                        If Not String.Equals(oAlbum, tlfile.Tag.Album, StringComparison.Ordinal) Then
                            oAlbum = multiMessage
                        End If
                    End If
                    ' Genre
                    If String.IsNullOrWhiteSpace(oGenre) Then
                        oGenre = tlfile.Tag.FirstGenre
                    Else
                        If Not String.Equals(oGenre, tlfile.Tag.FirstGenre, StringComparison.Ordinal) Then
                            oGenre = multiMessage
                        End If
                    End If
                    ' Year
                    If String.IsNullOrWhiteSpace(oYear) Then
                        oYear = tlfile.Tag.Year.ToString
                    Else
                        If Not String.Equals(oYear, tlfile.Tag.Year.ToString, StringComparison.Ordinal) Then
                            oYear = multiMessage
                        End If
                    End If
                    ' Tracks
                    If String.IsNullOrWhiteSpace(oTracks) Then
                        oTracks = tlfile.Tag.TrackCount.ToString
                    Else
                        If Not String.Equals(oTracks, tlfile.Tag.TrackCount.ToString, StringComparison.Ordinal) Then
                            oTracks = multiMessage
                        End If
                    End If
                    ' Comments
                    If String.IsNullOrWhiteSpace(oComments) Then
                        oComments = tlfile.Tag.Comment
                    Else
                        If Not String.Equals(oComments, tlfile.Tag.Comment, StringComparison.Ordinal) Then
                            oComments = multiMessage
                        End If
                    End If
                End If
                tlfile.Dispose()
            Next
            _paths = _paths.Except(removelist).ToList
            If _paths.Count = 0 Then
                Close()
            Else
                TxtBoxArtist.Text = oArtist
                TxtBoxTitle.Text = oTitle
                TxtBoxAlbum.Text = oAlbum
                TxtBoxGenre.Text = oGenre
                TxtBoxYear.Text = oYear
                TxtBoxTracks.Text = oTracks
                TxtBoxComments.Text = oComments
                If _paths.Count = 1 Then
                    Text &= " - " & IO.Path.GetFileNameWithoutExtension(_paths(0))
                Else
                    Text &= " < Multiple Songs > (" & _paths.Count.ToString & ")"
                End If
            End If

        End If
    End Sub
    Private Sub SaveTags()
        If Not HasChanged Then Exit Sub

        For Each path In _paths
            Try
                Using tlfile As TagLib.File = TagLib.File.Create(path)
                    If TxtBoxArtist.Text <> multiMessage AndAlso oArtist <> TxtBoxArtist.Text Then
                        If tlfile.Tag.Performers IsNot Nothing AndAlso tlfile.Tag.Performers.Count > 1 Then
                            Dim existingPerformers As String() = tlfile.Tag.Performers
                            Dim newPerformers As New List(Of String) From {
                                TxtBoxArtist.Text}
                            For i As Integer = 1 To existingPerformers.Length - 1
                                newPerformers.Add(existingPerformers(i))
                            Next
                            tlfile.Tag.Performers = newPerformers.ToArray()
                        Else
                            tlfile.Tag.Performers = New String() {TxtBoxArtist.Text}
                        End If
                    End If
                    If TxtBoxTitle.Text <> multiMessage AndAlso oTitle <> TxtBoxTitle.Text Then
                        tlfile.Tag.Title = TxtBoxTitle.Text
                    End If
                    If TxtBoxAlbum.Text <> multiMessage AndAlso oAlbum <> TxtBoxAlbum.Text Then
                        tlfile.Tag.Album = TxtBoxAlbum.Text
                    End If
                    tlfile.Save()
                End Using
            Catch ex As Exception
                WriteToLog("TagLib Error while Saving Tag, Cannot write to file: " + path + Chr(13) + ex.Message)
            End Try
        Next
        _libraryneedsupdated = True
        ' Reset state
        HasChanged = False
        LblArtist.Font = New Font(LblArtist.Font, FontStyle.Regular)
        LblTitle.Font = New Font(LblTitle.Font, FontStyle.Regular)
        LbLAlbum.Font = New Font(LbLAlbum.Font, FontStyle.Regular)
        btnArtistKeepOriginal.Enabled = False
        BtnTitleKeepOriginal.Enabled = False
        BtnAlbumKeepOriginal.Enabled = False

        TipStatus.ShowTooltipAtCursor("Tag" & If(_paths.Count > 1, "s", String.Empty) & " Saved Successfully", My.Resources.ImageOK)
    End Sub
    Private Function SetSave() As Boolean
        If oArtist = TxtBoxArtist.Text AndAlso oTitle = TxtBoxTitle.Text AndAlso oAlbum = TxtBoxAlbum.Text Then
            Return False
        Else
            Return True
        End If
    End Function
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
        'Debug.Print("Tag Editor Accent Color Set")
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            LblArtist.ForeColor = App.CurrentTheme.AccentTextColor
            LblTitle.ForeColor = App.CurrentTheme.AccentTextColor
            LbLAlbum.ForeColor = App.CurrentTheme.AccentTextColor
            LblGenre.ForeColor = App.CurrentTheme.AccentTextColor
            LblYear.ForeColor = App.CurrentTheme.AccentTextColor
            LblTracks.ForeColor = App.CurrentTheme.AccentTextColor
            LblComments.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblArtist.ForeColor = App.CurrentTheme.TextColor
            LblTitle.ForeColor = App.CurrentTheme.TextColor
            LbLAlbum.ForeColor = App.CurrentTheme.TextColor
            LblGenre.ForeColor = App.CurrentTheme.TextColor
            LblYear.ForeColor = App.CurrentTheme.TextColor
            LblTracks.ForeColor = App.CurrentTheme.TextColor
            LblComments.ForeColor = App.CurrentTheme.TextColor
        End If
        TxtBoxArtist.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxArtist.ForeColor = App.CurrentTheme.TextColor
        TxtBoxTitle.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxTitle.ForeColor = App.CurrentTheme.TextColor
        TxtBoxAlbum.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxAlbum.ForeColor = App.CurrentTheme.TextColor
        TxtBoxGenre.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxGenre.ForeColor = App.CurrentTheme.TextColor
        CoBoxGenre.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxGenre.ForeColor = App.CurrentTheme.TextColor
        TxtBoxYear.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxYear.ForeColor = App.CurrentTheme.TextColor
        TxtBoxTracks.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxTracks.ForeColor = App.CurrentTheme.TextColor
        TxtBoxComments.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxComments.ForeColor = App.CurrentTheme.TextColor
        BtnOK.BackColor = App.CurrentTheme.ButtonBackColor
        BtnOK.ForeColor = App.CurrentTheme.TextColor
        BtnSave.BackColor = App.CurrentTheme.ButtonBackColor
        BtnSave.ForeColor = App.CurrentTheme.TextColor
        btnArtistKeepOriginal.BackColor = App.CurrentTheme.ButtonBackColor
        btnArtistKeepOriginal.ForeColor = App.CurrentTheme.TextColor
        BtnTitleKeepOriginal.BackColor = App.CurrentTheme.ButtonBackColor
        BtnTitleKeepOriginal.ForeColor = App.CurrentTheme.TextColor
        BtnAlbumKeepOriginal.BackColor = App.CurrentTheme.ButtonBackColor
        BtnAlbumKeepOriginal.ForeColor = App.CurrentTheme.TextColor
        BtnGenreKeepOriginal.BackColor = App.CurrentTheme.ButtonBackColor
        BtnGenreKeepOriginal.ForeColor = App.CurrentTheme.TextColor
        BtnYearKeepOriginal.BackColor = App.CurrentTheme.ButtonBackColor
        BtnYearKeepOriginal.ForeColor = App.CurrentTheme.TextColor
        BtnTracksKeepOriginal.BackColor = App.CurrentTheme.ButtonBackColor
        BtnTracksKeepOriginal.ForeColor = App.CurrentTheme.TextColor
        BtnCommentsKeepOriginal.BackColor = App.CurrentTheme.ButtonBackColor
        BtnCommentsKeepOriginal.ForeColor = App.CurrentTheme.TextColor
        TipInfo.BackColor = App.CurrentTheme.BackColor
        TipInfo.ForeColor = App.CurrentTheme.TextColor
        TipInfo.BorderColor = App.CurrentTheme.ButtonBackColor
        TipStatus.BackColor = App.CurrentTheme.BackColor
        TipStatus.ForeColor = App.CurrentTheme.TextColor
        TipStatus.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
        'Debug.Print("Tag Editor Theme Set")
    End Sub

End Class
