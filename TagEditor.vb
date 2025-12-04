
Public Class TagEditor

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private _paths As List(Of String)
    Private _haschanged As Boolean = False
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

    End Sub
    Private Sub TagEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetTags()
    End Sub
    Private Sub TagEditor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If HasChanged Then DialogResult = DialogResult.OK
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
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If HasChanged Then

        End If
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub
    Private Sub btnArtistKeepOriginal_Click(sender As Object, e As EventArgs) Handles btnArtistKeepOriginal.Click
        TxtBoxArtist.Text = oArtist
    End Sub
    Private Sub BtnTitleKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnTitleKeepOriginal.Click
        TxtBoxTitle.Text = oTitle
    End Sub
    Private Sub BtnAlbumKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnAlbumKeepOriginal.Click
        TxtBoxAlbum.Text = oAlbum
    End Sub

    ' Methods
    Private Sub GetTags()
        If _paths.Count > 0 Then
            Dim tlfile As TagLib.File
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
                End If
            Next
            _paths = _paths.Except(removelist).ToList
            If _paths.Count = 0 Then
                Close()
            Else
                TxtBoxArtist.Text = oArtist
                TxtBoxTitle.Text = oTitle
                TxtBoxAlbum.Text = oAlbum
                If _paths.Count = 1 Then
                    Text &= " - " & IO.Path.GetFileNameWithoutExtension(_paths(0))
                Else
                    Text &= " < Multiple Songs > (" & _paths.Count.ToString & ")"
                End If
            End If

            'For Each path In _paths
            '    Try
            '        tlfile = TagLib.File.Create(path)
            '    Catch ex As Exception
            '        WriteToLog("TagLib Error while Editing Tag, Cannot read from file: " + path + Chr(13) + ex.Message)
            '        tlfile = Nothing
            '    End Try
            '    If tlfile Is Nothing Then
            '        Dim errorMsg As String = "< Error Reading Tag" & If(_paths.Count = 1, String.Empty, "s") & " >"
            '        TxtBoxArtist.Text = errorMsg
            '        TxtBoxArtist.ReadOnly = True
            '        TxtBoxTitle.Text = errorMsg
            '        TxtBoxTitle.ReadOnly = True
            '        TxtBoxAlbum.Text = errorMsg
            '        TxtBoxAlbum.ReadOnly = True
            '    Else
            '        'Artist
            '        TxtBoxArtist.Text = tlfile.Tag.FirstPerformer
            '        'Title
            '        TxtBoxTitle.Text = tlfile.Tag.Title
            '        'Album
            '        TxtBoxAlbum.Text = tlfile.Tag.Album
            '        '    'Genre
            '        '    item.SubItems(LVLibrary.Columns("Genre").Index).Text = tlfile.Tag.FirstGenre
            '        '    'Year
            '        '    If tlfile.Tag.Year <> 0 Then
            '        '        item.SubItems(LVLibrary.Columns("Year").Index).Text = tlfile.Tag.Year.ToString
            '        '    End If
            '        '    'Track
            '        '    If tlfile.Tag.Track > 0 Then item.SubItems(LVLibrary.Columns("Track").Index).Text = tlfile.Tag.Track.ToString
            '        '    'Tracks
            '        '    If tlfile.Tag.TrackCount > 0 Then item.SubItems(LVLibrary.Columns("Tracks").Index).Text = tlfile.Tag.TrackCount.ToString
            '        '    'Duration
            '        '    If tlfile.Properties IsNot Nothing AndAlso tlfile.Properties.Duration <> TimeSpan.Zero Then
            '        '        item.SubItems(LVLibrary.Columns("Duration").Index).Text = tlfile.Properties.Duration.ToString("hh\:mm\:ss")
            '        '    End If
            '        '    'Artists
            '        '    If tlfile.Tag.Performers IsNot Nothing Then
            '        '        If tlfile.Tag.Performers.Count > 1 Then
            '        '            For index = 1 To tlfile.Tag.Performers.Count - 1
            '        '                item.SubItems(LVLibrary.Columns("Artists").Index).Text += tlfile.Tag.Performers(index) + ", "
            '        '            Next
            '        '            item.SubItems(LVLibrary.Columns("Artists").Index).Text = item.SubItems(LVLibrary.Columns("Artists").Index).Text.TrimEnd(", ".ToCharArray)
            '        '        End If
            '        '    End If
            '        '    'Comments
            '        '    item.SubItems(LVLibrary.Columns("Comments").Index).Text = tlfile.Tag.Comment
            '        '    'Album Art Identifier
            '        '    If tlfile.Tag.Pictures.Count > 0 Then item.ImageKey = "AlbumArt"
            '        '    tlfile.Dispose()
            '    End If
            'Next
        End If
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
        'Debug.Print("Tag Editor Accent Color Set")
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            LblArtist.ForeColor = App.CurrentTheme.AccentTextColor
            LblTitle.ForeColor = App.CurrentTheme.AccentTextColor
            LbLAlbum.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblArtist.ForeColor = App.CurrentTheme.TextColor
            LblTitle.ForeColor = App.CurrentTheme.TextColor
            LbLAlbum.ForeColor = App.CurrentTheme.TextColor
        End If
        TxtBoxArtist.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxArtist.ForeColor = App.CurrentTheme.TextColor
        TxtBoxTitle.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxTitle.ForeColor = App.CurrentTheme.TextColor
        TxtBoxAlbum.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxAlbum.ForeColor = App.CurrentTheme.TextColor
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
        TipTagEditor.BackColor = App.CurrentTheme.BackColor
        TipTagEditor.ForeColor = App.CurrentTheme.TextColor
        TipTagEditor.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
        'Debug.Print("Tag Editor Theme Set")
    End Sub

End Class
