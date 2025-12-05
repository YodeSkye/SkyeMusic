
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
    Dim oArtist As String = Nothing
    Dim oTitle As String = Nothing
    Dim oAlbum As String = Nothing
    Dim oGenre As String = Nothing
    Dim oYear As String = Nothing
    Dim oTrack As String = Nothing
    Dim oTracks As String = Nothing
    Dim oComments As String = Nothing

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
    Private Sub TxtBox_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtBoxAlbum.KeyUp, TxtBoxArtist.KeyUp, TxtBoxTitle.KeyUp, TxtBoxYear.KeyUp, TxtBoxTracks.KeyUp, TxtBoxComments.KeyUp, TxtBoxGenre.KeyUp, TxtBoxTrack.KeyUp
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
                ElseIf tb Is TxtBoxGenre Then
                    If HasChanged Then
                        LblGenre.Font = New Font(LblGenre.Font, FontStyle.Bold)
                    Else
                        LblGenre.Font = New Font(LblGenre.Font, FontStyle.Regular)
                    End If
                    BtnGenreKeepOriginal.Enabled = HasChanged
                ElseIf tb Is TxtBoxYear Then
                    If HasChanged Then
                        LblYear.Font = New Font(LblYear.Font, FontStyle.Bold)
                    Else
                        LblYear.Font = New Font(LblYear.Font, FontStyle.Regular)
                    End If
                    BtnYearKeepOriginal.Enabled = HasChanged
                ElseIf tb Is TxtBoxTrack Then
                    If HasChanged Then
                        LblTrack.Font = New Font(LblTrack.Font, FontStyle.Bold)
                    Else
                        LblTrack.Font = New Font(LblTrack.Font, FontStyle.Regular)
                    End If
                    BtnTrackKeepOriginal.Enabled = HasChanged
                ElseIf tb Is TxtBoxTracks Then
                    If HasChanged Then
                        LblTracks.Font = New Font(LblTracks.Font, FontStyle.Bold)
                    Else
                        LblTracks.Font = New Font(LblTracks.Font, FontStyle.Regular)
                    End If
                    BtnTracksKeepOriginal.Enabled = HasChanged
                ElseIf tb Is TxtBoxComments Then
                    If HasChanged Then
                        LblComments.Font = New Font(LblComments.Font, FontStyle.Bold)
                    Else
                        LblComments.Font = New Font(LblComments.Font, FontStyle.Regular)
                    End If
                    BtnCommentsKeepOriginal.Enabled = HasChanged
                End If
        End Select
    End Sub
    Private Sub TxtBox_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxAlbum.PreviewKeyDown, TxtBoxTitle.PreviewKeyDown, TxtBoxArtist.PreviewKeyDown, TxtBoxYear.PreviewKeyDown, TxtBoxTracks.PreviewKeyDown, TxtBoxComments.PreviewKeyDown, TxtBoxGenre.PreviewKeyDown, TxtBoxTrack.PreviewKeyDown
        CMBasic.ShortcutKeys(TryCast(sender, TextBox), e)
    End Sub
    Private Sub TxtBoxNumbersOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TxtBoxYear.KeyPress, TxtBoxTracks.KeyPress, TxtBoxTrack.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then e.Handled = True
    End Sub
    Private Sub TxtBoxNumbersOnly_TextChanged(sender As Object, e As EventArgs) Handles TxtBoxTrack.TextChanged, TxtBoxYear.TextChanged, TxtBoxTracks.TextChanged
        Dim s = TryCast(sender, TextBox)
        If s Is Nothing OrElse s.Text = multiMessage Then Exit Sub
        Dim result As UInteger
        If UInteger.TryParse(s.Text, result) Then
            s.ForeColor = App.CurrentTheme.TextColor
        Else
            s.ForeColor = Color.Red
        End If
    End Sub
    Private Sub CoBoxGenre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxGenre.SelectedIndexChanged
        TxtBoxGenre.Text = CoBoxGenre.SelectedItem.ToString
        HasChanged = SetSave()
        If HasChanged Then
            LblGenre.Font = New Font(LblGenre.Font, FontStyle.Bold)
        Else
            LblGenre.Font = New Font(LblGenre.Font, FontStyle.Regular)
        End If
        BtnGenreKeepOriginal.Enabled = HasChanged
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
    Private Sub BtnGenreKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnGenreKeepOriginal.Click
        TxtBoxGenre.Text = oGenre
        LblGenre.Font = New Font(LblGenre.Font, FontStyle.Regular)
        BtnGenreKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxGenre.Focus()
        TxtBoxGenre.SelectAll()
    End Sub
    Private Sub BtnYearKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnYearKeepOriginal.Click
        TxtBoxYear.Text = oYear
        LblYear.Font = New Font(LblYear.Font, FontStyle.Regular)
        BtnYearKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxYear.Focus()
        TxtBoxYear.SelectAll()
    End Sub
    Private Sub BtnTrackKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnTrackKeepOriginal.Click
        TxtBoxTrack.Text = oTrack
        LblTrack.Font = New Font(LblTrack.Font, FontStyle.Regular)
        BtnTrackKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxTrack.Focus()
        TxtBoxTrack.SelectAll()
    End Sub
    Private Sub BtnTracksKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnTracksKeepOriginal.Click
        TxtBoxTracks.Text = oTracks
        LblTracks.Font = New Font(LblTracks.Font, FontStyle.Regular)
        BtnTracksKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxTracks.Focus()
        TxtBoxTracks.SelectAll()
    End Sub
    Private Sub BtnCommentsKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnCommentsKeepOriginal.Click
        TxtBoxComments.Text = oComments
        LblComments.Font = New Font(LblComments.Font, FontStyle.Regular)
        BtnCommentsKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxComments.Focus()
        TxtBoxComments.SelectAll()
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
                    Dim artist As String = If(String.IsNullOrWhiteSpace(tlfile.Tag.FirstPerformer), String.Empty, tlfile.Tag.FirstPerformer)
                    If oArtist Is Nothing Then
                        oArtist = artist
                    Else
                        If Not String.Equals(oArtist, artist, StringComparison.Ordinal) Then
                            oArtist = multiMessage
                        End If
                    End If
                    ' Title
                    Dim title As String = If(String.IsNullOrWhiteSpace(tlfile.Tag.Title), String.Empty, tlfile.Tag.Title)
                    If oTitle Is Nothing Then
                        oTitle = title
                    Else
                        If Not String.Equals(oTitle, title, StringComparison.Ordinal) Then
                            oTitle = multiMessage
                        End If
                    End If
                    ' Album
                    Dim album As String = If(String.IsNullOrWhiteSpace(tlfile.Tag.Album), String.Empty, tlfile.Tag.Album)
                    If oAlbum Is Nothing Then
                        oAlbum = album
                    Else
                        If Not String.Equals(oAlbum, album, StringComparison.Ordinal) Then
                            oAlbum = multiMessage
                        End If
                    End If
                    ' Genre
                    Dim genre As String = If(String.IsNullOrWhiteSpace(tlfile.Tag.FirstGenre), String.Empty, tlfile.Tag.FirstGenre)
                    If oGenre Is Nothing Then
                        oGenre = genre
                    Else
                        If Not String.Equals(oGenre, genre, StringComparison.Ordinal) Then
                            oGenre = multiMessage
                        End If
                    End If
                    ' Year
                    Dim year As String = If(tlfile.Tag.Year = 0, String.Empty, tlfile.Tag.Year.ToString)
                    If oYear Is Nothing Then
                        oYear = year
                    Else
                        If Not String.Equals(oYear, year, StringComparison.Ordinal) Then
                            oYear = multiMessage
                        End If
                    End If
                    ' Track
                    Dim track As String = If(tlfile.Tag.Track = 0, String.Empty, tlfile.Tag.Track.ToString)
                    If oTrack Is Nothing Then
                        oTrack = track
                    Else
                        If Not String.Equals(oTrack, track, StringComparison.Ordinal) Then
                            oTrack = multiMessage
                        End If
                    End If
                    ' Tracks
                    Dim tracks As String = If(tlfile.Tag.TrackCount = 0, String.Empty, tlfile.Tag.TrackCount.ToString)
                    If oTracks Is Nothing Then
                        oTracks = tracks
                    Else
                        If Not String.Equals(oTracks, tracks, StringComparison.Ordinal) Then
                            oTracks = multiMessage
                        End If
                    End If
                    ' Comments
                    Dim comments As String = If(String.IsNullOrWhiteSpace(tlfile.Tag.Comment), String.Empty, tlfile.Tag.Comment)
                    If oComments Is Nothing Then
                        oComments = comments
                    Else
                        If Not String.Equals(oComments, comments, StringComparison.Ordinal) Then
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
                TxtBoxTrack.Text = oTrack
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
                    If TxtBoxGenre.Text <> multiMessage AndAlso oGenre <> TxtBoxGenre.Text Then
                        If tlfile.Tag.Genres IsNot Nothing AndAlso tlfile.Tag.Genres.Count > 1 Then
                            Dim existingGenres As String() = tlfile.Tag.Genres
                            Dim newGenres As New List(Of String) From {
                                TxtBoxGenre.Text}
                            For i As Integer = 1 To existingGenres.Length - 1
                                newGenres.Add(existingGenres(i))
                            Next
                            tlfile.Tag.Genres = newGenres.ToArray()
                        Else
                            tlfile.Tag.Genres = New String() {TxtBoxGenre.Text}
                        End If
                    End If
                    If TxtBoxYear.Text <> multiMessage AndAlso oYear <> TxtBoxYear.Text Then
                        If TxtBoxYear.Text = String.Empty Then
                            tlfile.Tag.Year = 0
                        Else
                            tlfile.Tag.Year = Convert.ToUInt32(TxtBoxYear.Text)
                        End If
                    End If
                    If TxtBoxTrack.Text <> multiMessage AndAlso oTrack <> TxtBoxTrack.Text Then
                        If TxtBoxTrack.Text = String.Empty Then
                            tlfile.Tag.Track = 0
                        Else
                            tlfile.Tag.Track = Convert.ToUInt32(TxtBoxTrack.Text)
                        End If
                    End If
                    If TxtBoxTracks.Text <> multiMessage AndAlso oTracks <> TxtBoxTracks.Text Then
                        If TxtBoxTracks.Text = String.Empty Then
                            tlfile.Tag.TrackCount = 0
                        Else
                            tlfile.Tag.TrackCount = Convert.ToUInt32(TxtBoxTracks.Text)
                        End If
                    End If
                    If TxtBoxComments.Text <> multiMessage AndAlso oComments <> TxtBoxComments.Text Then
                        tlfile.Tag.Comment = TxtBoxComments.Text
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
        LblGenre.Font = New Font(LblGenre.Font, FontStyle.Regular)
        LblYear.Font = New Font(LblYear.Font, FontStyle.Regular)
        LblTrack.Font = New Font(LblTrack.Font, FontStyle.Regular)
        LblTracks.Font = New Font(LblTracks.Font, FontStyle.Regular)
        LblComments.Font = New Font(LblComments.Font, FontStyle.Regular)
        btnArtistKeepOriginal.Enabled = False
        BtnTitleKeepOriginal.Enabled = False
        BtnAlbumKeepOriginal.Enabled = False
        BtnGenreKeepOriginal.Enabled = False
        BtnYearKeepOriginal.Enabled = False
        BtnTrackKeepOriginal.Enabled = False
        BtnTracksKeepOriginal.Enabled = False
        BtnCommentsKeepOriginal.Enabled = False

        TipStatus.ShowTooltipAtCursor("Tag" & If(_paths.Count > 1, "s", String.Empty) & " Saved Successfully", My.Resources.ImageOK)
    End Sub
    Private Function SetSave() As Boolean
        If oArtist = TxtBoxArtist.Text AndAlso oTitle = TxtBoxTitle.Text AndAlso oAlbum = TxtBoxAlbum.Text _
            AndAlso oGenre = TxtBoxGenre.Text AndAlso oYear = TxtBoxYear.Text _
            AndAlso oTrack = TxtBoxTrack.Text AndAlso oTracks = TxtBoxTracks.Text AndAlso oComments = TxtBoxComments.Text Then
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
            LblTrack.ForeColor = App.CurrentTheme.AccentTextColor
            LblTracks.ForeColor = App.CurrentTheme.AccentTextColor
            LblComments.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblArtist.ForeColor = App.CurrentTheme.TextColor
            LblTitle.ForeColor = App.CurrentTheme.TextColor
            LbLAlbum.ForeColor = App.CurrentTheme.TextColor
            LblGenre.ForeColor = App.CurrentTheme.TextColor
            LblYear.ForeColor = App.CurrentTheme.TextColor
            LblTrack.ForeColor = App.CurrentTheme.TextColor
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
        TxtBoxTrack.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxTrack.ForeColor = App.CurrentTheme.TextColor
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
        BtnTrackKeepOriginal.BackColor = App.CurrentTheme.ButtonBackColor
        BtnTrackKeepOriginal.ForeColor = App.CurrentTheme.TextColor
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
