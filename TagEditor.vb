Imports System.IO

Public Class TagEditor

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As System.Drawing.Point
    Private FrmArtViewer As ArtViewer
    Private _paths As List(Of String)
    Private _haschanged As Boolean = False
    Private _libraryneedsupdated As Boolean = False
    Private _playlistneedsupdated As Boolean = False
    Private artindex As Integer = 0
    Private aggregateconflict As Boolean = False
    Private oText As String
    Private multiMessage As String = "< Keep Original >"
    Private oArtist As String
    Private oTitle As String
    Private oAlbum As String
    Private oGenre As String
    Private oYear As String
    Private oTrack As String
    Private oTracks As String
    Private oComments As String
    Private oArt As New List(Of TagLib.IPicture)
    Private nArt As New List(Of TagLib.IPicture)
    Private oLyrics As String
    Private LyricsFileCountLRC As Integer
    Private LyricsFileCountTXT As Integer
    Private Property HasChanged As Boolean
        Get
            Return _haschanged
        End Get
        Set(value As Boolean)
            _haschanged = value
            BtnSave.Enabled = value
        End Set
    End Property
    Friend ReadOnly Property ArtistText As String
        Get
            Return TxtBoxArtist.Text.Trim()
        End Get
    End Property
    Friend ReadOnly Property AlbumText As String
        Get
            Return TxtBoxAlbum.Text.Trim()
        End Get
    End Property

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
        InitializeComponent()

        ' Initialize Locals
        _paths = paths
        Text = My.Application.Info.Title & " " & Text
        oText = Text
        SetAccentColor()
        SetTheme()
        App.ThemeMenu(CMBasic)
        For Each s As String In TagLib.Genres.Audio
            CoBoxGenre.Items.Add(s)
        Next
        For Each s As String In TagLib.Genres.Video
            If Not CoBoxGenre.Items.Contains(s) Then CoBoxGenre.Items.Add(s)
        Next
        CoBoxArtType.DataSource = [Enum].GetValues(GetType(TagLib.PictureType))
        CoBoxArtType.ContextMenuStrip = New ContextMenuStrip() ' Disable right-click context menu

    End Sub
    Private Sub TagEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetTags()
    End Sub
    Private Sub TagEditor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentForm Is Library And _libraryneedsupdated Then
            DialogResult = DialogResult.OK
        ElseIf ParentForm Is Player And _playlistneedsupdated Then
            DialogResult = DialogResult.OK
        Else
            DialogResult = DialogResult.Cancel
        End If
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
    Private Sub TxtBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxArtist.KeyDown, TxtBoxAlbum.KeyDown, TxtBoxTitle.KeyDown, TxtBoxYear.KeyDown, TxtBoxTracks.KeyDown, TxtBoxComments.KeyDown, TxtBoxGenre.KeyDown, TxtBoxTrack.KeyDown, TxtBoxLyrics.KeyDown
        Dim s = TryCast(sender, System.Windows.Forms.TextBox)
        If s IsNot Nothing Then
            If s.Text = multiMessage Then s.Clear()
        End If
    End Sub
    Private Sub TxtBox_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtBoxAlbum.KeyUp, TxtBoxArtist.KeyUp, TxtBoxTitle.KeyUp, TxtBoxYear.KeyUp, TxtBoxTracks.KeyUp, TxtBoxComments.KeyUp, TxtBoxGenre.KeyUp, TxtBoxTrack.KeyUp, TxtBoxLyrics.KeyUp
        Select Case e.KeyCode
            Case Keys.Enter
                e.Handled = True
                Dim tb = TryCast(sender, System.Windows.Forms.TextBox)
                If tb IsNot Nothing Then tb.SelectAll()
            Case Keys.Tab
                e.Handled = True
            Case Else
                HasChanged = SetSave()
                If oArtist = TxtBoxArtist.Text Then
                    LblArtist.Font = New Font(LblArtist.Font, FontStyle.Regular)
                    btnArtistKeepOriginal.Enabled = False
                Else
                    LblArtist.Font = New Font(LblArtist.Font, FontStyle.Bold)
                    btnArtistKeepOriginal.Enabled = True
                End If
                If oTitle = TxtBoxTitle.Text Then
                    LblTitle.Font = New Font(LblTitle.Font, FontStyle.Regular)
                    BtnTitleKeepOriginal.Enabled = False
                Else
                    LblTitle.Font = New Font(LblTitle.Font, FontStyle.Bold)
                    BtnTitleKeepOriginal.Enabled = True
                End If
                If oAlbum = TxtBoxAlbum.Text Then
                    LbLAlbum.Font = New Font(LbLAlbum.Font, FontStyle.Regular)
                    BtnAlbumKeepOriginal.Enabled = False
                Else
                    LbLAlbum.Font = New Font(LbLAlbum.Font, FontStyle.Bold)
                    BtnAlbumKeepOriginal.Enabled = True
                End If
                If oGenre = TxtBoxGenre.Text Then
                    LblGenre.Font = New Font(LblGenre.Font, FontStyle.Regular)
                    BtnGenreKeepOriginal.Enabled = False
                Else
                    LblGenre.Font = New Font(LblGenre.Font, FontStyle.Bold)
                    BtnGenreKeepOriginal.Enabled = True
                End If
                If oYear = TxtBoxYear.Text Then
                    LblYear.Font = New Font(LblYear.Font, FontStyle.Regular)
                    BtnYearKeepOriginal.Enabled = False
                Else
                    LblYear.Font = New Font(LblYear.Font, FontStyle.Bold)
                    BtnYearKeepOriginal.Enabled = True
                End If
                If oTrack = TxtBoxTrack.Text Then
                    LblTrack.Font = New Font(LblTrack.Font, FontStyle.Regular)
                    BtnTrackKeepOriginal.Enabled = False
                Else
                    LblTrack.Font = New Font(LblTrack.Font, FontStyle.Bold)
                    BtnTrackKeepOriginal.Enabled = True
                End If
                If oTracks = TxtBoxTracks.Text Then
                    LblTracks.Font = New Font(LblTracks.Font, FontStyle.Regular)
                    BtnTracksKeepOriginal.Enabled = False
                Else
                    LblTracks.Font = New Font(LblTracks.Font, FontStyle.Bold)
                    BtnTracksKeepOriginal.Enabled = True
                End If
                If oComments = TxtBoxComments.Text Then
                    LblComments.Font = New Font(LblComments.Font, FontStyle.Regular)
                    BtnCommentsKeepOriginal.Enabled = False
                Else
                    LblComments.Font = New Font(LblComments.Font, FontStyle.Bold)
                    BtnCommentsKeepOriginal.Enabled = True
                End If
                If oLyrics = TxtBoxLyrics.Text Then
                    LblLyrics.Font = New Font(LblLyrics.Font, FontStyle.Regular)
                    BtnLyricsKeepOriginal.Enabled = False
                Else
                    LblLyrics.Font = New Font(LblLyrics.Font, FontStyle.Bold)
                    BtnLyricsKeepOriginal.Enabled = True
                End If
        End Select
    End Sub
    Private Sub TxtBox_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxAlbum.PreviewKeyDown, TxtBoxTitle.PreviewKeyDown, TxtBoxArtist.PreviewKeyDown, TxtBoxYear.PreviewKeyDown, TxtBoxTracks.PreviewKeyDown, TxtBoxComments.PreviewKeyDown, TxtBoxGenre.PreviewKeyDown, TxtBoxTrack.PreviewKeyDown, TxtBoxArtDescription.PreviewKeyDown, TxtBoxLyrics.PreviewKeyDown
        CMBasic.ShortcutKeys(TryCast(sender, TextBox), e)
    End Sub
    Private Sub TxtBoxNumbersOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TxtBoxYear.KeyPress, TxtBoxTracks.KeyPress, TxtBoxTrack.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then e.Handled = True
    End Sub
    Private Sub TxtBoxNumbersOnly_TextChanged(sender As Object, e As EventArgs) Handles TxtBoxTrack.TextChanged, TxtBoxYear.TextChanged, TxtBoxTracks.TextChanged
        Dim s = TryCast(sender, TextBox)
        If s Is Nothing OrElse s.Text = multiMessage Then Exit Sub
        Dim result As UInteger
        If Not s.Text = multiMessage Then
            If UInteger.TryParse(s.Text, result) Then
                s.ForeColor = App.CurrentTheme.TextColor
            Else
                s.ForeColor = Color.Red
            End If
        End If
    End Sub
    Private Sub TxtBoxArtDescription_TextChanged(sender As Object, e As EventArgs) Handles TxtBoxArtDescription.TextChanged
        If artindex >= 0 AndAlso artindex < nArt.Count Then
            nArt(artindex).Description = TxtBoxArtDescription.Text
            HasChanged = SetSave()
            If PicturesEqual(nArt, oArt) Then
                BtnArtKeepOriginal.Enabled = False
                LblArt.Font = New Font(LblArtist.Font, FontStyle.Regular)
            Else
                BtnArtKeepOriginal.Enabled = True
                LblArt.Font = New Font(LblArtist.Font, FontStyle.Bold)
            End If
        End If
    End Sub
    Private Sub CoBoxArtType_SelectedValueChanged(sender As Object, e As EventArgs) Handles CoBoxArtType.SelectedValueChanged
        If artindex >= 0 AndAlso artindex < nArt.Count Then
            nArt(artindex).Type = CType(CoBoxArtType.SelectedItem, TagLib.PictureType)
            HasChanged = SetSave()
            If PicturesEqual(nArt, oArt) Then
                BtnArtKeepOriginal.Enabled = False
                LblArt.Font = New Font(LblArtist.Font, FontStyle.Regular)
            Else
                BtnArtKeepOriginal.Enabled = True
                LblArt.Font = New Font(LblArtist.Font, FontStyle.Bold)
            End If
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
    Private Sub CoBoxArtType_KeyDown(sender As Object, e As KeyEventArgs) Handles CoBoxArtType.KeyDown
        If e.KeyCode = Keys.Delete Then
            e.SuppressKeyPress = True   ' stops the delete action
            e.Handled = True
        End If
    End Sub
    Private Sub CoBoxArtType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CoBoxArtType.KeyPress
        e.Handled = True
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If HasChanged Then
            TipInfo.ShowTooltip(BtnSave)
            TipInfo.HideTooltip()
            SaveTags()
        End If
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub
    Private Sub BtnArtistKeepOriginal_Click(sender As Object, e As EventArgs) Handles btnArtistKeepOriginal.Click
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
        TxtBoxYear.ForeColor = App.CurrentTheme.TextColor
        LblYear.Font = New Font(LblYear.Font, FontStyle.Regular)
        BtnYearKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxYear.Focus()
        TxtBoxYear.SelectAll()
    End Sub
    Private Sub BtnTrackKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnTrackKeepOriginal.Click
        TxtBoxTrack.Text = oTrack
        TxtBoxTrack.ForeColor = App.CurrentTheme.TextColor
        LblTrack.Font = New Font(LblTrack.Font, FontStyle.Regular)
        BtnTrackKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxTrack.Focus()
        TxtBoxTrack.SelectAll()
    End Sub
    Private Sub BtnTracksKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnTracksKeepOriginal.Click
        TxtBoxTracks.Text = oTracks
        TxtBoxTracks.ForeColor = CurrentTheme.TextColor
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
    Private Sub BtnLyricsKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnLyricsKeepOriginal.Click
        TxtBoxLyrics.Text = oLyrics
        LblLyrics.Font = New Font(LblLyrics.Font, FontStyle.Regular)
        BtnLyricsKeepOriginal.Enabled = False
        HasChanged = SetSave()
        TxtBoxLyrics.Focus()
        TxtBoxLyrics.SelectAll()
    End Sub
    Private Sub BtnArtLeft_Click(sender As Object, e As EventArgs) Handles BtnArtLeft.Click
        artindex -= 1
        If artindex < 0 Then artindex = 0
        ShowImages()
    End Sub
    Private Sub BtnArtRight_Click(sender As Object, e As EventArgs) Handles BtnArtRight.Click
        artindex += 1
        If artindex > nArt.Count - 1 Then artindex = nArt.Count - 1
        ShowImages()
    End Sub
    Private Sub BtnArtNewFromClipboard_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnArtNewFromClipboard.MouseDown
        If Computer.Clipboard.ContainsImage Then
            Dim img = Clipboard.GetImage

            ' Convert to byte array
            Using ms As New MemoryStream
                ' Save as JPEG or PNG depending on your preference
                img.Save(ms, Imaging.ImageFormat.Jpeg)
                Dim bytes = ms.ToArray

                ' Wrap in TagLib.Picture
                Dim pic As New TagLib.Picture(New TagLib.ByteVector(bytes)) With {
                    .Description = Nothing,
                    .Type = TagLib.PictureType.Other}

                ' Add to your list
                If e.Button = MouseButtons.Right Then
                    nArt.Add(pic)
                Else
                    nArt.Insert(artindex, pic)
                End If
            End Using
            ShowImages()
            TipInfo.HideTooltip()
            TipStatus.ShowTooltipAtCursor("Image Added From Clipboard", SystemIcons.Information.ToBitmap)

            If PicturesEqual(nArt, oArt) Then
                BtnArtKeepOriginal.Enabled = False
                LblArt.Font = New Font(LblArtist.Font, FontStyle.Regular)
            Else
                BtnArtKeepOriginal.Enabled = True
                LblArt.Font = New Font(LblArtist.Font, FontStyle.Bold)
            End If
        Else
            TipInfo.HideTooltip()
            TipStatus.ShowTooltipAtCursor("No Image Found on Clipboard", SystemIcons.Error.ToBitmap)
        End If
    End Sub
    Private Sub BtnArtNewFromFile_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnArtNewFromFile.MouseDown
        Using ofd As New OpenFileDialog
            ofd.Title = "Select an Image File"
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            If ofd.ShowDialog = DialogResult.OK Then
                Try
                    Dim img = Image.FromFile(ofd.FileName)

                    ' Convert to byte array
                    Using ms As New MemoryStream

                        ' Save as JPEG or PNG depending on your preference
                        img.Save(ms, Imaging.ImageFormat.Jpeg)
                        Dim bytes = ms.ToArray

                        ' Wrap in TagLib.Picture
                        Dim pic As New TagLib.Picture(New TagLib.ByteVector(bytes)) With {
                            .Description = Nothing,
                            .Type = TagLib.PictureType.Other}

                        ' Add to your list
                        If e.Button = MouseButtons.Right Then
                            nArt.Add(pic)
                        Else
                            nArt.Insert(artindex, pic)
                        End If
                    End Using
                    ShowImages()
                    TipInfo.HideTooltip()
                    TipStatus.ShowTooltipAtCursor("Image File Added", SystemIcons.Information.ToBitmap)

                    If PicturesEqual(nArt, oArt) Then
                        BtnArtKeepOriginal.Enabled = False
                        LblArt.Font = New Font(LblArtist.Font, FontStyle.Regular)
                    Else
                        BtnArtKeepOriginal.Enabled = True
                        LblArt.Font = New Font(LblArtist.Font, FontStyle.Bold)
                    End If
                Catch ex As Exception
                    TipInfo.HideTooltip()
                    App.WriteToLog("Tag Editor Error Loading Image From File: " & ex.Message)
                    TipStatus.ShowTooltipAtCursor("Error loading image from file.", SystemIcons.Error.ToBitmap)
                End Try
            End If
        End Using
    End Sub
    Private Sub BtnArtNewFromOnline_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnArtNewFromOnline.MouseDown
        Dim newpic As TagLib.IPicture = Nothing
        Dim FrmTagEditorOnline As New TagEditorOnline
        FrmTagEditorOnline.ShowDialog()
        If FrmTagEditorOnline.DialogResult = DialogResult.OK Then
            newpic = FrmTagEditorOnline.NewPic
        End If
        FrmTagEditorOnline.Dispose()
        If newpic IsNot Nothing Then
            If e.Button = MouseButtons.Right Then
                nArt.Add(newpic)
            Else
                nArt.Insert(artindex, newpic)
            End If
            ShowImages()
            If PicturesEqual(nArt, oArt) Then
                BtnArtKeepOriginal.Enabled = False
                LblArt.Font = New Font(LblArtist.Font, FontStyle.Regular)
            Else
                BtnArtKeepOriginal.Enabled = True
                LblArt.Font = New Font(LblArtist.Font, FontStyle.Bold)
            End If
        End If
    End Sub
    Private Sub BtnExport_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnArtExport.MouseDown
        Select Case e.Button
            Case MouseButtons.Left
                If artindex >= 0 AndAlso artindex < nArt.Count Then
                    Dim img As Image
                    Using ms As New MemoryStream(nArt(artindex).Data.Data)
                        img = Image.FromStream(ms)
                    End Using
                    Clipboard.SetImage(img)
                    img.Dispose()
                    TipStatus.ShowTooltipAtCursor("Image Copied To Clipboard", SystemIcons.Information.ToBitmap)
                End If
            Case MouseButtons.Right
                If artindex >= 0 AndAlso artindex < nArt.Count Then
                    Using sfd As New SaveFileDialog
                        sfd.Title = "Export Artwork Image"
                        sfd.Filter = "JPEG Image|*.jpg;*.jpeg|PNG Image|*.png|Bitmap Image|*.bmp|GIF Image|*.gif"
                        sfd.FileName = "artwork"
                        If sfd.ShowDialog = DialogResult.OK Then
                            Try
                                Dim img As Image
                                Using ms As New MemoryStream(nArt(artindex).Data.Data)
                                    ' Create a copy of the image to break the stream dependency
                                    Using tempImg = Image.FromStream(ms)
                                        img = New Bitmap(tempImg)
                                    End Using
                                End Using
                                Using img
                                    Select Case Path.GetExtension(sfd.FileName).ToLowerInvariant
                                        Case ".jpg", ".jpeg"
                                            img.Save(sfd.FileName, Imaging.ImageFormat.Jpeg)
                                        Case ".png"
                                            img.Save(sfd.FileName, Imaging.ImageFormat.Png)
                                        Case ".bmp"
                                            img.Save(sfd.FileName, Imaging.ImageFormat.Bmp)
                                        Case ".gif"
                                            img.Save(sfd.FileName, Imaging.ImageFormat.Gif)
                                    End Select
                                End Using
                                TipStatus.ShowTooltipAtCursor("Image Exported Successfully", SystemIcons.Information.ToBitmap)
                            Catch ex As Exception
                                App.WriteToLog("Tag Editor Error Exporting Artwork Image: " & ex.Message)
                                TipStatus.ShowTooltipAtCursor("Error Exporting Image", SystemIcons.Error.ToBitmap)
                            End Try
                        End If
                    End Using
                End If
        End Select
    End Sub
    Private Sub BtnArtRemove_Click(sender As Object, e As EventArgs) Handles BtnArtRemove.Click
        If nArt.Count > 0 Then
            nArt.RemoveAt(artindex)
            ShowImages()
            HasChanged = SetSave()
        End If
        If PicturesEqual(nArt, oArt) Then
            BtnArtKeepOriginal.Enabled = False
            LblArt.Font = New Font(LblArtist.Font, FontStyle.Regular)
        Else
            BtnArtKeepOriginal.Enabled = True
            LblArt.Font = New Font(LblArtist.Font, FontStyle.Bold)
        End If
    End Sub
    Private Sub BtnArtKeepOriginal_Click(sender As Object, e As EventArgs) Handles BtnArtKeepOriginal.Click
        nArt = ClonePictures(oArt)
        artindex = 0
        ShowImages()
        BtnArtKeepOriginal.Enabled = False
        HasChanged = SetSave()
    End Sub
    Private Sub PicBoxArt_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        ' Check if the drag data contains file paths
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files = CType(e.Data.GetData(DataFormats.FileDrop), String())
            ' Check if at least one file is an image
            Dim hasImage = False
            For Each file In files
                Dim ext = Path.GetExtension(file).ToLowerInvariant
                If ext = ".jpg" OrElse ext = ".jpeg" OrElse ext = ".png" OrElse ext = ".bmp" OrElse ext = ".gif" Then
                    hasImage = True
                    Exit For
                End If
            Next
            If hasImage Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.None
            End If
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub PicBoxArt_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files = CType(e.Data.GetData(DataFormats.FileDrop), String())
            Dim addedCount = 0

            For Each file In files
                Dim ext = Path.GetExtension(file).ToLowerInvariant
                If ext = ".jpg" OrElse ext = ".jpeg" OrElse ext = ".png" OrElse ext = ".bmp" OrElse ext = ".gif" Then
                    Try
                        Dim img = Image.FromFile(file)

                        ' Convert to byte array
                        Using ms As New MemoryStream
                            img.Save(ms, Imaging.ImageFormat.Jpeg)
                            Dim bytes = ms.ToArray

                            ' Wrap in TagLib.Picture
                            Dim pic As New TagLib.Picture(New TagLib.ByteVector(bytes)) With {
                                .Description = Nothing,
                                .Type = TagLib.PictureType.Other}

                            ' Add to list at current index
                            nArt.Insert(artindex, pic)
                            addedCount += 1
                        End Using
                    Catch ex As Exception
                        WriteToLog("Tag Editor Error loading image from drag and drop: " & file & vbCrLf & ex.Message)
                    End Try
                End If
            Next

            If addedCount > 0 Then
                ShowImages()
                HasChanged = SetSave()
                If PicturesEqual(nArt, oArt) Then
                    BtnArtKeepOriginal.Enabled = False
                    LblArt.Font = New Font(LblArtist.Font, FontStyle.Regular)
                Else
                    BtnArtKeepOriginal.Enabled = True
                    LblArt.Font = New Font(LblArtist.Font, FontStyle.Bold)
                End If
                TipStatus.ShowTooltipAtCursor(If(addedCount = 1, "Image added", addedCount.ToString & " images added"), Resources.ImageOK)
            End If
        End If
    End Sub
    Private Sub PicBoxArt_MouseDown(sender As Object, e As MouseEventArgs) Handles PicBoxArt.MouseDown
        If e.Button = MouseButtons.Right Then
            FrmArtViewer = New ArtViewer(PicBoxArt.Image, MousePosition) With {.Owner = Me}
            FrmArtViewer.Show()
        End If
    End Sub
    Private Sub PicBoxArt_MouseUp(sender As Object, e As MouseEventArgs) Handles PicBoxArt.MouseUp
        If e.Button = MouseButtons.Right Then
            FrmArtViewer?.Close()
        End If
    End Sub

    ' Methods
    Private Sub GetTags()
        If _paths.Count > 0 Then
            Dim tlfile As TagLib.File
            Dim removelist As New List(Of String)
            oArtist = Nothing
            oTitle = Nothing
            oAlbum = Nothing
            oGenre = Nothing
            oYear = Nothing
            oTrack = Nothing
            oTracks = Nothing
            oComments = Nothing
            oLyrics = Nothing
            LyricsFileCountLRC = 0
            LyricsFileCountTXT = 0

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
                    ' Artwork
                    oArt.Clear()
                    aggregateconflict = False
                    If _paths.Count = 1 Then
                        ' Single file → just show all its pictures
                        Dim pics = tlfile.Tag.Pictures
                        Dim images As New List(Of Image)

                        For Each pic In pics
                            Using ms As New MemoryStream(pic.Data.Data)
                                images.Add(Image.FromStream(ms))
                            End Using
                            oArt.Add(pic)
                        Next

                    Else
                        ' Multiple files → run aggregation
                        Dim pics As List(Of TagLib.IPicture) = AggregatePictures(_paths, multiMessage)
                        If pics.Count > 0 Then oArt = pics
                    End If
                    nArt = ClonePictures(oArt)
                    ' Lyrics
                    Dim lyrics As String = If(String.IsNullOrWhiteSpace(tlfile.Tag.Lyrics), String.Empty, tlfile.Tag.Lyrics)
                    If oLyrics Is Nothing Then
                        oLyrics = lyrics
                    Else
                        If Not String.Equals(oLyrics, lyrics, StringComparison.Ordinal) Then
                            oLyrics = multiMessage
                        End If
                    End If

                    ' Check for Lyric Files
                    Dim lrcPath = IO.Path.ChangeExtension(path, ".lrc")
                    Dim txtPath = IO.Path.ChangeExtension(path, ".txt")
                    If IO.File.Exists(lrcPath) Then LyricsFileCountLRC += 1
                    If IO.File.Exists(txtPath) Then LyricsFileCountTXT += 1
                    If LyricsFileCountLRC = 0 Then
                        ChkBoxHasSyncedLyricsFile.CheckState = CheckState.Unchecked
                    ElseIf LyricsFileCountLRC = _paths.Count Then
                        ChkBoxHasSyncedLyricsFile.CheckState = CheckState.Checked
                    Else
                        ChkBoxHasSyncedLyricsFile.CheckState = CheckState.Indeterminate
                    End If
                    If LyricsFileCountTXT = 0 Then
                        ChkBoxHasPlainTextFile.CheckState = CheckState.Unchecked
                    ElseIf LyricsFileCountTXT = _paths.Count Then
                        ChkBoxHasPlainTextFile.CheckState = CheckState.Checked
                    Else
                        ChkBoxHasPlainTextFile.CheckState = CheckState.Indeterminate
                    End If

                    tlfile.Dispose()
                End If
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
                TxtBoxLyrics.Text = oLyrics
                ShowImages()
                If _paths.Count = 1 Then
                    Text = oText & " - " & IO.Path.GetFileNameWithoutExtension(_paths(0))
                Else
                    Text = oText & " < Multiple Songs > (" & _paths.Count.ToString & ")"
                End If
            End If

        End If
    End Sub
    Private Sub SaveTags()
        If Not HasChanged Then Exit Sub

        Dim failedPaths As New List(Of String)
        Dim savedCount As Integer = 0

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
                    If Not PicturesEqual(nArt, oArt) Then tlfile.Tag.Pictures = nArt.ToArray()
                    If TxtBoxLyrics.Text <> multiMessage AndAlso oLyrics <> TxtBoxLyrics.Text Then
                        tlfile.Tag.Lyrics = TxtBoxLyrics.Text
                    End If
                    tlfile.Save()
                End Using
                savedCount += 1
            Catch ioEx As IO.IOException ' File is locked or in use
                failedPaths.Add(path)
                WriteToLog("File In Use, Cannot Save Tag: " & path & vbCrLf & ioEx.Message)
            Catch unauthEx As UnauthorizedAccessException ' No permission or locked
                failedPaths.Add(path)
                WriteToLog("Access Denied While Saving Tag: " & path & vbCrLf & unauthEx.Message)
            Catch ex As Exception ' Generic fallback
                failedPaths.Add(path)
                WriteToLog("TagLib Error While Saving Tag, Cannot Write To File: " & path & vbCrLf & ex.Message)
            End Try
        Next

        ' Only update library and reset UI if at least one successful save
        If failedPaths.Count = 0 Then
            If Not App.Settings.WatcherUpdateLibrary Then _libraryneedsupdated = True
            If Not App.Settings.WatcherUpdatePlaylist Then _playlistneedsupdated = True

            ' Reset state once after all attempts
            HasChanged = False
            LblArtist.Font = New Font(LblArtist.Font, FontStyle.Regular)
            LblTitle.Font = New Font(LblTitle.Font, FontStyle.Regular)
            LbLAlbum.Font = New Font(LbLAlbum.Font, FontStyle.Regular)
            LblGenre.Font = New Font(LblGenre.Font, FontStyle.Regular)
            LblYear.Font = New Font(LblYear.Font, FontStyle.Regular)
            LblTrack.Font = New Font(LblTrack.Font, FontStyle.Regular)
            LblTracks.Font = New Font(LblTracks.Font, FontStyle.Regular)
            LblComments.Font = New Font(LblComments.Font, FontStyle.Regular)
            LblArt.Font = New Font(LblArt.Font, FontStyle.Regular)
            LblLyrics.Font = New Font(LblLyrics.Font, FontStyle.Regular)
            btnArtistKeepOriginal.Enabled = False
            BtnTitleKeepOriginal.Enabled = False
            BtnAlbumKeepOriginal.Enabled = False
            BtnGenreKeepOriginal.Enabled = False
            BtnYearKeepOriginal.Enabled = False
            BtnTrackKeepOriginal.Enabled = False
            BtnTracksKeepOriginal.Enabled = False
            BtnCommentsKeepOriginal.Enabled = False
            BtnArtKeepOriginal.Enabled = False
            BtnLyricsKeepOriginal.Enabled = False

            GetTags()
            TipStatus.ShowTooltipAtCursor("Tag" & If(savedCount > 1, "s", String.Empty) & " Saved Successfully (" & savedCount.ToString() & ")", My.Resources.ImageOK)
        Else
            Dim errormessage As String = If(failedPaths.Count = 1, "1 file failed to save. Check log.", failedPaths.Count.ToString() & " files failed to save. Check log.")
            TipStatus.ShowTooltipAtCursor(errormessage, SystemIcons.Error.ToBitmap)
        End If
    End Sub
    Private Sub ShowImages()
        If nArt.Count = 0 Then
            If aggregateconflict Then
                ' Mixed Images
                PicBoxArt.Image = My.Resources.ImageMixedImages
                TipInfo.SetText(PicBoxArt, "Mixed Artwork")
            Else
                ' No Images at all
                PicBoxArt.Image = My.Resources.ImageNoImages
                TipInfo.SetText(PicBoxArt, "No Artwork")
            End If
            TxtBoxArtDescription.Enabled = False
            CoBoxArtType.Enabled = False
            BtnArtExport.Enabled = False
            BtnArtRemove.Enabled = False
        Else
            Dim Image As Image
            If artindex > nArt.Count - 1 Then artindex = 0
            'Debug.Print("Showing Art Index: " & artindex.ToString & " of " & nArt.Count.ToString)

            Using ms As New MemoryStream(nArt(artindex).Data.Data)
                Image = Image.FromStream(ms)
            End Using
            PicBoxArt.Image = Image
            TxtBoxArtDescription.Text = nArt(artindex).Description
            CoBoxArtType.SelectedItem = nArt(artindex).Type
            TipInfo.SetText(PicBoxArt, Image.Width.ToString & "x" & Image.Height.ToString & vbCr & Skye.Common.FormatFileSize(nArt(artindex).Data.Count, Skye.Common.FormatFileSizeUnits.Auto).ToString)

            BtnArtLeft.Enabled = Not artindex = 0
            BtnArtRight.Enabled = Not artindex >= nArt.Count - 1
            BtnArtExport.Enabled = True
            BtnArtRemove.Enabled = True
            TxtBoxArtDescription.Enabled = True
            CoBoxArtType.Enabled = True
        End If
    End Sub
    Private Function SetSave() As Boolean
        If oArtist = TxtBoxArtist.Text AndAlso oTitle = TxtBoxTitle.Text AndAlso oAlbum = TxtBoxAlbum.Text _
            AndAlso oGenre = TxtBoxGenre.Text AndAlso oYear = TxtBoxYear.Text AndAlso oTrack = TxtBoxTrack.Text _
            AndAlso oTracks = TxtBoxTracks.Text AndAlso oComments = TxtBoxComments.Text AndAlso oLyrics = TxtBoxLyrics.Text _
            AndAlso PicturesEqual(nArt, oArt) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function PicturesEqual(listA As List(Of TagLib.IPicture),
                               listB As List(Of TagLib.IPicture)) As Boolean
        ' First check count
        If listA.Count <> listB.Count Then Return False

        ' Compare each picture's raw data
        For i As Integer = 0 To listA.Count - 1
            Dim picA = listA(i)
            Dim picB = listB(i)

            ' Compare type if relevant
            If picA.Type <> picB.Type Then Return False

            ' Compare description directly (Nothing vs "")
            If picA.Description <> picB.Description Then Return False

            ' Compare lengths first for speed
            If picA.Data.Count <> picB.Data.Count Then Return False

            ' Compare actual bytes
            If Not picA.Data.Data.SequenceEqual(picB.Data.Data) Then
                Return False
            End If
        Next

        Return True
    End Function
    Private Function AggregatePictures(paths As IEnumerable(Of String), multiMessage As String) As List(Of TagLib.IPicture)
        Dim basePics As List(Of TagLib.IPicture) = Nothing

        For Each path In paths
            Try
                Using tlfile As TagLib.File = TagLib.File.Create(path)
                    Dim pics = tlfile.Tag.Pictures.ToList()

                    If basePics Is Nothing Then
                        ' First file’s pictures become the baseline
                        basePics = pics
                    Else
                        ' Compare count first
                        If pics.Count <> basePics.Count Then
                            aggregateconflict = True
                        Else
                            ' Compare type

                            ' Compare each picture’s raw data
                            For i As Integer = 0 To pics.Count - 1
                                If pics(i).Type <> basePics(i).Type Then
                                    aggregateconflict = True
                                    Exit For
                                End If

                                ' Compare description (Nothing vs "" counts as different)
                                If pics(i).Description <> basePics(i).Description Then
                                    aggregateconflict = True
                                    Exit For
                                End If

                                If Not pics(i).Data.Data.SequenceEqual(basePics(i).Data.Data) Then
                                    aggregateconflict = True
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End Using
            Catch ex As Exception
                WriteToLog("TagLib Error while reading pictures in AggregatePictures: " & path & vbCrLf & ex.Message)
            End Try
        Next

        ' Decide what to return
        If aggregateconflict OrElse basePics Is Nothing Then
            ' Mixed or no art → return empty list (or placeholder)
            Return New List(Of TagLib.IPicture)
        Else
            Return basePics
        End If
    End Function
    Private Function ClonePictures(src As List(Of TagLib.IPicture)) As List(Of TagLib.IPicture)
        Dim result As New List(Of TagLib.IPicture)
        For Each pic In src
            Dim clone As New TagLib.Picture(pic.Data) With {
                .Type = pic.Type,
                .Description = pic.Description}
            result.Add(clone)
        Next
        Return result
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
            ChkBoxHasPlainTextFile.BackColor = c
            ChkBoxHasSyncedLyricsFile.BackColor = c
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
            LblArt.ForeColor = App.CurrentTheme.AccentTextColor
            LblLyrics.ForeColor = App.CurrentTheme.AccentTextColor
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
            LblArt.ForeColor = App.CurrentTheme.TextColor
            LblLyrics.ForeColor = App.CurrentTheme.TextColor
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
        TxtBoxArtDescription.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxArtDescription.ForeColor = App.CurrentTheme.TextColor
        CoBoxArtType.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxArtType.ForeColor = App.CurrentTheme.TextColor
        TxtBoxLyrics.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxLyrics.ForeColor = App.CurrentTheme.TextColor
        ChkBoxHasPlainTextFile.ForeColor = App.CurrentTheme.ButtonTextColor
        ChkBoxHasSyncedLyricsFile.ForeColor = App.CurrentTheme.ButtonTextColor
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
        BtnArtNewFromClipboard.BackColor = App.CurrentTheme.ButtonBackColor
        BtnArtNewFromClipboard.ForeColor = App.CurrentTheme.TextColor
        BtnArtNewFromFile.BackColor = App.CurrentTheme.ButtonBackColor
        BtnArtNewFromFile.ForeColor = App.CurrentTheme.TextColor
        BtnArtNewFromOnline.BackColor = App.CurrentTheme.ButtonBackColor
        BtnArtNewFromOnline.ForeColor = App.CurrentTheme.TextColor
        BtnArtExport.BackColor = App.CurrentTheme.ButtonBackColor
        BtnArtExport.ForeColor = App.CurrentTheme.TextColor
        BtnArtRemove.BackColor = App.CurrentTheme.ButtonBackColor
        BtnArtRemove.ForeColor = App.CurrentTheme.TextColor
        BtnArtLeft.BackColor = App.CurrentTheme.ButtonBackColor
        BtnArtLeft.ForeColor = App.CurrentTheme.TextColor
        BtnArtRight.BackColor = App.CurrentTheme.ButtonBackColor
        BtnArtRight.ForeColor = App.CurrentTheme.TextColor
        BtnArtKeepOriginal.BackColor = App.CurrentTheme.ButtonBackColor
        BtnArtKeepOriginal.ForeColor = App.CurrentTheme.TextColor
        BtnLyricsKeepOriginal.BackColor = App.CurrentTheme.ButtonBackColor
        BtnLyricsKeepOriginal.ForeColor = App.CurrentTheme.ButtonTextColor
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
