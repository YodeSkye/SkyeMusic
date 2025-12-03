
Public Class TagEditor

    ' Declarations
    Private _paths As String()
    Private Property HasChanged As Boolean = False

    ' Form Events
    Public Sub New(paths As String())

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

    ' Methods
    Private Sub GetTags()
        Dim tlfile As TagLib.File
        Dim multiMessage As String = "< Keep Original >"
        Dim artistText As String = String.Empty
        Dim titleText As String = String.Empty
        Dim albumText As String = String.Empty

        For Each path In _paths
            Try
                tlfile = TagLib.File.Create(path)
            Catch ex As Exception
                WriteToLog("TagLib Error while Editing Tag, Cannot read from file: " + path + Chr(13) + ex.Message)
                tlfile = Nothing
            End Try
            If tlfile Is Nothing Then
                Dim errorMsg As String = "< Error Reading Tag" & If(_paths.Count = 1, String.Empty, "s") & " >"
                TxtBoxArtist.Text = errorMsg
                TxtBoxArtist.ReadOnly = True
                TxtBoxTitle.Text = errorMsg
                TxtBoxTitle.ReadOnly = True
                TxtBoxAlbum.Text = errorMsg
                TxtBoxAlbum.ReadOnly = True
                Exit For
            Else
                'Artist
                If String.IsNullOrWhiteSpace(artistText) Then
                    artistText = tlfile.Tag.FirstPerformer
                Else
                    If Not String.Equals(artistText, tlfile.Tag.FirstPerformer, StringComparison.Ordinal) Then
                        artistText = multiMessage
                    End If
                End If
                TxtBoxArtist.Text = artistText
                'Title
                'Album
            End If
        Next

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
        ResumeLayout()
        'Debug.Print("Tag Editor Theme Set")
    End Sub

End Class
