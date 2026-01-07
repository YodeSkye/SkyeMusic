
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Threading
Imports MetaBrainz.MusicBrainz.Interfaces
Imports MetaBrainz.MusicBrainz.Interfaces.Entities
Imports MetaBrainz.MusicBrainz.Interfaces.Searches

Public Class TagEditorOnline

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private FrmArtViewer As ArtViewer
    Private NetClient As New System.Net.Http.HttpClient
    Private MBQuery As New MetaBrainz.MusicBrainz.Query(NetClient)
    Private MBArt As New MetaBrainz.MusicBrainz.CoverArt.CoverArt(NetClient)
    Private MBImageFront As MetaBrainz.MusicBrainz.CoverArt.CoverArtImage = Nothing
    Private MBImageBack As MetaBrainz.MusicBrainz.CoverArt.CoverArtImage = Nothing
    Private query As String = String.Empty
    Private selectedpic As TagLib.IPicture = Nothing
    Friend ReadOnly Property NewPic As TagLib.IPicture
        Get
            Return selectedpic
        End Get
    End Property

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
                Case Skye.WinAPI.WM_SYSCOMMAND
                    If m.WParam.ToInt32 = Skye.WinAPI.SC_CLOSE Then
                        DialogResult = DialogResult.Cancel
                    End If
            End Select
        Catch ex As Exception
            My.App.WriteToLog("TagEditorOnline WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub Frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title & " " & Text
        SetAccentColor()
        SetTheme()
        TxtBoxSearchPhrase.ContextMenuStrip = App.DummyMenu
    End Sub
    Private Sub Frm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        query = BuildQuery(App.FrmTagEditor.ArtistText, App.FrmTagEditor.AlbumText)
        TxtBoxSearchPhrase.Text = query
        Search()
    End Sub
    Private Sub Frm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MBImageFront?.Dispose()
        MBImageBack?.Dispose()
        MBArt?.Dispose()
        MBQuery?.Dispose()
        NetClient?.Dispose()
    End Sub
    Private Sub Frm_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, PicBoxArt.MouseDown, PicBoxFrontThumb.MouseDown, PicBoxBackThumb.MouseDown, LblDimBack.MouseDown, LblStatus.MouseDown
        Select Case e.Button
            Case MouseButtons.Left
                If WindowState = FormWindowState.Normal Then
                    mMove = True
                    Dim cSender = DirectCast(sender, Control)
                    If cSender Is PicBoxArt OrElse cSender Is PicBoxFrontThumb OrElse cSender Is PicBoxBackThumb OrElse cSender Is LblDimFront OrElse cSender Is LblDimBack OrElse cSender Is LblStatus Then
                        mOffset = New Point(-e.X - cSender.Left - SystemInformation.FrameBorderSize.Width - 4, -e.Y - cSender.Top - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
                    Else
                        mOffset = New Point(-e.X - SystemInformation.FrameBorderSize.Width - 4, -e.Y - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
                    End If
                End If
            Case MouseButtons.Right
                Dim cSender = TryCast(sender, PictureBox)
                If cSender IsNot Nothing AndAlso cSender.Image IsNot Nothing Then
                    FrmArtViewer = New ArtViewer(cSender.Image, MousePosition) With {.Owner = Me}
                    FrmArtViewer.Show()
                End If
        End Select
    End Sub
    Private Sub Frm_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, PicBoxArt.MouseMove, PicBoxFrontThumb.MouseMove, PicBoxBackThumb.MouseMove, LblDimBack.MouseMove, LblStatus.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub Frm_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, PicBoxArt.MouseUp, PicBoxFrontThumb.MouseUp, PicBoxBackThumb.MouseUp, LblDimBack.MouseUp, LblStatus.MouseUp
        mMove = False
        If e.Button = MouseButtons.Right Then
            FrmArtViewer?.Close()
        End If
    End Sub
    Private Sub Frm_Move(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Move
        If Not mMove AndAlso Me.WindowState = FormWindowState.Normal Then CheckMove(Me.Location)
    End Sub
    Private Sub Frm_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick, PicBoxArt.DoubleClick
        ToggleMaximized()
    End Sub

    'Control Events
    Private Sub TxtBoxSearchPhrase_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtBoxSearchPhrase.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            e.Handled = True
            Validate()
        End If
    End Sub
    Private Sub TxtBoxSearchPhrase_Validated(sender As Object, e As EventArgs) Handles TxtBoxSearchPhrase.Validated
        LVIDs.Focus()
        If Not query = TxtBoxSearchPhrase.Text.Trim Then
            query = TxtBoxSearchPhrase.Text.Trim
            Search()
        End If
    End Sub
    Private Async Sub LVIDs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVIDs.SelectedIndexChanged
        If LVIDs.SelectedItems.Count > 0 Then
            LVIDs.Enabled = False
            LblStatus.Visible = True
            LblDimFront.Text = String.Empty
            LblDimBack.Text = String.Empty
            PicBoxArt.Image = Nothing
            PicBoxFrontThumb.Image = Nothing
            PicBoxBackThumb.Image = Nothing
            Refresh()

            Try
                MBImageFront = Await MBArt.FetchFrontAsync(Guid.Parse(LVIDs.SelectedItems(0).Tag.ToString))
                PicBoxArt.Image = New Bitmap(MBImageFront.Data)
                PicBoxFrontThumb.Image = New Bitmap(MBImageFront.Data)
                Using ms As New MemoryStream
                    PicBoxArt.Image.Save(ms, ImageFormat.Jpeg)
                    selectedpic = New TagLib.Picture(ms.ToArray())
                End Using
                LblDimFront.Text = PicBoxFrontThumb.Image.Width.ToString + " x " + PicBoxFrontThumb.Image.Height.ToString
            Catch ex As Exception
                Debug.WriteLine("Fetch Front Error: " & ex.GetType().FullName & ": " & ex.Message)
            End Try

            Try
                MBImageBack = Await MBArt.FetchBackAsync(Guid.Parse(LVIDs.SelectedItems(0).Tag.ToString))
                PicBoxBackThumb.Image = New Bitmap(MBImageBack.Data)
                LblDimBack.Text = PicBoxBackThumb.Image.Width.ToString + " x " + PicBoxBackThumb.Image.Height.ToString
            Catch ex As Exception
                Debug.WriteLine("Fetch Back Error: " & ex.GetType().FullName & ": " & ex.Message)
            End Try

            LblStatus.Visible = False
            LVIDs.Enabled = True
            LVIDs.Select()
        End If
    End Sub
    Private Sub LVIDs_SizeChanged(sender As Object, e As EventArgs) Handles LVIDs.SizeChanged
        LVIDs.Columns(0).Width = LVIDs.Width - SystemInformation.VerticalScrollBarWidth - 4
    End Sub
    Private Sub PicBoxFrontThumb_Click(sender As Object, e As EventArgs) Handles PicBoxFrontThumb.Click
        If MBImageFront IsNot Nothing Then
            PicBoxArt.Image = New Bitmap(MBImageFront.Data)
            Using ms As New MemoryStream
                PicBoxArt.Image.Save(ms, ImageFormat.Jpeg)
                selectedpic = New TagLib.Picture(ms.ToArray())
            End Using
        End If
    End Sub
    Private Sub PicBoxBackThumb_Click(sender As Object, e As EventArgs) Handles PicBoxBackThumb.Click
        If MBImageBack IsNot Nothing Then
            PicBoxArt.Image = New Bitmap(MBImageBack.Data)
            Using ms As New MemoryStream
                PicBoxArt.Image.Save(ms, ImageFormat.Jpeg)
                selectedpic = New TagLib.Picture(ms.ToArray())
            End Using
        End If
    End Sub
    Private Sub BtnSaveImage_Click(sender As Object, e As EventArgs) Handles BtnSaveImage.Click
        Dim frmSave As New TagEditorOnlineSave
        frmSave.PicBoxThumb.Image = PicBoxArt.Image
        frmSave.ShowDialog()
        If frmSave.DialogResult = DialogResult.OK Then
            If Not String.IsNullOrWhiteSpace(frmSave.GetFilename) Then
                If PicBoxArt.Image IsNot Nothing Then
                    Dim ext As String = Path.GetExtension(frmSave.GetFilename).ToLowerInvariant()
                    Try
                        Select Case ext
                            Case ".jpg", ".jpeg"
                                PicBoxArt.Image.Save(frmSave.GetFilename, ImageFormat.Jpeg)
                            Case ".png"
                                PicBoxArt.Image.Save(frmSave.GetFilename, ImageFormat.Png)
                            Case ".bmp"
                                PicBoxArt.Image.Save(frmSave.GetFilename, ImageFormat.Bmp)
                        End Select
                        App.WriteToLog("Saved online cover art to " + frmSave.GetFilename)
                    Catch ex As Exception
                        App.WriteToLog("Error saving online cover art to " + frmSave.GetFilename + vbCr + ex.Message)
                    End Try
                End If
            End If
        End If
        frmSave.Dispose()
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    'Procedures
    Private Async Sub Search()
        Await SearchAsync()
    End Sub
    Private Async Function SearchAsync() As Task
        If String.IsNullOrWhiteSpace(query) Then Return

        'Reset UI
        LblSearchPhrase.Text = "Searching..."
        LblSearchPhrase.Font = New Font(LblSearchPhrase.Font, FontStyle.Bold)
        LblSearchPhrase.ForeColor = Color.Red
        LVIDs.Items.Clear()
        LVIDs.Enabled = False
        LblDimFront.Text = String.Empty
        LblDimBack.Text = String.Empty
        PicBoxArt.Image = Nothing
        PicBoxFrontThumb.Image = Nothing
        PicBoxBackThumb.Image = Nothing

        Dim items As New List(Of ListViewItem)()

        Try
            'New API: streaming search
            Dim results As IStreamingQueryResults(Of ISearchResult(Of IRelease)) = MBQuery.FindAllReleases(query)

            'Explicit async enumerator
            Dim enumerator As IAsyncEnumerator(Of ISearchResult(Of IRelease)) = results.GetAsyncEnumerator(CancellationToken.None)

            While Await enumerator.MoveNextAsync()
                Dim result As ISearchResult(Of IRelease) = enumerator.Current
                Dim release As IRelease = result.Item
                Dim lvitem As New ListViewItem() With {
                    .Tag = release.Id.ToString(),
                    .Text = release.ArtistCredit(0).Artist.Name & ", " & release.Title}
                items.Add(lvitem)
            End While

            Await enumerator.DisposeAsync()

            'Update UI in one shot
            LVIDs.Items.AddRange(items.ToArray())
            LVIDs.Enabled = True

            'Update status label
            LblSearchPhrase.Text = "Search Phrase:"
            LblSearchPhrase.Font = New Font(LblSearchPhrase.Font, FontStyle.Regular)
            SetTheme()

        Catch ex As Exception
            Debug.WriteLine("Search Error: " & ex.Message)
            LVIDs.Enabled = True
        End Try
    End Function
    Private Function BuildQuery(artist As String, album As String) As String
        Dim parts As New List(Of String)
        If Not String.IsNullOrWhiteSpace(artist) Then
            parts.Add("artist:""" & artist & """")
        End If
        If Not String.IsNullOrWhiteSpace(album) Then
            parts.Add("release:""" & album & """")
        End If
        If parts.Count = 0 Then
            Return String.Empty
        End If
        Return String.Join(" AND ", parts)
    End Function
    Private Sub ToggleMaximized()
        Select Case WindowState
            Case FormWindowState.Normal
                WindowState = FormWindowState.Maximized
            Case FormWindowState.Maximized, FormWindowState.Minimized
                WindowState = FormWindowState.Normal
        End Select
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Height + App.AdjustScreenBoundsDialogWindow
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
            LblSearchPhrase.ForeColor = App.CurrentTheme.AccentTextColor
            LblDimFront.ForeColor = App.CurrentTheme.AccentTextColor
            LblDimBack.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblSearchPhrase.ForeColor = App.CurrentTheme.TextColor
            LblDimFront.ForeColor = App.CurrentTheme.TextColor
            LblDimBack.ForeColor = App.CurrentTheme.TextColor
        End If
        TxtBoxSearchPhrase.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxSearchPhrase.ForeColor = App.CurrentTheme.TextColor
        LVIDs.BackColor = App.CurrentTheme.ControlBackColor
        LVIDs.ForeColor = App.CurrentTheme.TextColor
        BtnSaveImage.BackColor = App.CurrentTheme.ButtonBackColor
        BtnSaveImage.ForeColor = App.CurrentTheme.TextColor
        BtnOK.BackColor = App.CurrentTheme.ButtonBackColor
        BtnOK.ForeColor = App.CurrentTheme.TextColor
        tipInfo.BackColor = App.CurrentTheme.BackColor
        tipInfo.ForeColor = App.CurrentTheme.TextColor
        tipInfo.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
        'Debug.Print("Tag Editor Theme Set")
    End Sub

End Class
