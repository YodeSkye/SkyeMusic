
Imports System.IO
Imports SkyeMusic.My

Public Class Library

    'Declarations
    Private Enum LibraryGroupMode
        None
        Genre
        Album
        Artist
        Year
    End Enum
    Private Structure LibraryGroup
        Dim Name As String
        Dim Index As Int16
    End Structure
    Public Structure LibraryItemType
        Dim Artist As String
        Dim Title As String
        Dim Album As String
        Dim Genre As String
        Dim Year As String
        Dim Track As String
        Dim Tracks As String
        Dim Duration As String
        Dim AV As String
        Dim Artists As String
        Dim Comments As String
        Dim FilePath As String
        Dim HasAlbumArt As Boolean
    End Structure
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private LibraryImageList As New ImageList
    Private AlbumArtIndex As Byte = 0
    Private PicBoxAlbumArtSmallSize As Size
    Private PicBoxAlbumArtLargeSize As Size
    Private PicBoxAlbumArtSuperSize As Size
    Private PicBoxAlbumArtStartLocation As Point
    Private LibrarySearchTitle As String
    Private LibrarySearchItems As New List(Of ListViewItem)
    Private LibraryArtistSort As SortOrder = SortOrder.None
    Private LibraryTitleSort As SortOrder = SortOrder.None
    Private LibraryAlbumSort As SortOrder = SortOrder.None
    Private LibraryGenreSort As SortOrder = SortOrder.None
    Private LibraryYearSort As SortOrder = SortOrder.None
    Private LibraryDurationSort As SortOrder = SortOrder.None
    Private LibraryArtistsSort As SortOrder = SortOrder.None
    Private LibraryCommentsSort As SortOrder = SortOrder.None
    Private LibraryFilenameSort As SortOrder = SortOrder.None
    Private LibraryGroupBy As LibraryGroupMode = LibraryGroupMode.None
    Private LibraryGroups As New Collections.Generic.List(Of LibraryGroup)
    Private IsTextBoxLibrarySearch As Boolean = False

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            App.WriteToLog("About WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub Library_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Define 12 Columns
        Dim header As ColumnHeader
        header = New ColumnHeader()
        header.Name = "Artist"
        header.Text = "Artist"
        header.Width = 200
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Title"
        header.Text = "Title"
        header.Width = 200
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Album"
        header.Text = "Album"
        header.Width = 200
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Genre"
        header.Text = "Genre"
        header.Width = 100
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Year"
        header.Text = "Year"
        header.Width = 50
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Track"
        header.Text = "T"
        header.Width = 25
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Tracks"
        header.Text = "Ts"
        header.Width = 25
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Duration"
        header.Text = "Duration"
        header.Width = 67
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "AV"
        header.Text = "AV"
        header.Width = 27
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Artists"
        header.Text = "Contributing Artists"
        header.Width = 200
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "Comments"
        header.Text = "Comments"
        header.Width = 200
        LVLibrary.Columns.Add(header)
        header = New ColumnHeader()
        header.Name = "FilePath"
        header.Text = "File Path"
        header.Width = 200
        LVLibrary.Columns.Add(header)
        header = Nothing
        LibrarySearchTitle = TxbxLibrarySearch.Text
        SetTheme()
        LibraryImageList.Images.Add("AlbumArt", Resources.ImageAlbumArtSelect)
        LVLibrary.SmallImageList = LibraryImageList
        PicBoxAlbumArtSmallSize = PicBoxAlbumArt.Size
        PicBoxAlbumArtLargeSize = New Size(400, 400)
        PicBoxAlbumArtSuperSize = New Size(800, 800)
        RadBtnGroupByNone.TabStop = False
        TipLibrary.SetToolTip(LblAlbumArtSelect, "Show Next Album Art")
        LblHistory.Text = String.Empty
        LblExtTitle.Text = String.Empty
        LblExtFileInfo.Text = String.Empty
        LblExtProperties.Text = String.Empty
        LblExtType.Text = String.Empty
        LoadLibrary()
#If DEBUG Then
        'If App.SaveWindowMetrics AndAlso App.LibraryLocation.Y >= 0 Then Me.Location = App.LibraryLocation
        'If App.SaveWindowMetrics AndAlso App.LibrarySize.Height >= 0 Then Me.Size = App.LibrarySize
#Else
        If App.SaveWindowMetrics AndAlso App.LibraryLocation.Y >= 0 Then Me.Location = App.LibraryLocation
        If App.SaveWindowMetrics AndAlso App.LibrarySize.Height >= 0 Then Me.Size = App.LibrarySize
#End If
    End Sub
    Private Sub Library_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        SaveLibrary()
        Hide()
    End Sub
    Private Sub Library_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, LblLibraryCounts.MouseDown, LblExtTitle.MouseDown, LblExtFileInfo.MouseDown, LblExtProperties.MouseDown, LblExtType.MouseDown, GrpBoxGroupBy.MouseDown
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
    Private Sub Library_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, LblLibraryCounts.MouseMove, LblExtTitle.MouseMove, LblExtFileInfo.MouseMove, LblExtProperties.MouseMove, LblExtType.MouseMove, GrpBoxGroupBy.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
            App.LibraryLocation = Location
        End If
    End Sub
    Private Sub Library_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, LblLibraryCounts.MouseUp, LblExtTitle.MouseUp, LblExtFileInfo.MouseUp, LblExtProperties.MouseUp, LblExtType.MouseUp, GrpBoxGroupBy.MouseUp
        mMove = False
    End Sub
    Private Sub Library_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.LibraryLocation = Location
        End If
    End Sub
    Private Sub Library_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.LibrarySize = Me.Size
        End If
    End Sub
    Private Sub Library_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Not IsTextBoxLibrarySearch And e.KeyCode = Keys.Escape Then Close()
        IsTextBoxLibrarySearch = False
    End Sub
    Private Sub Library_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        ToggleMaximized()
    End Sub

    'Control Events
    Private Sub LVLibrary_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles LVLibrary.DrawColumnHeader
        Dim b As Rectangle = e.Bounds
        'Draw Background
        Using brush As Brush = New SolidBrush(App.CurrentTheme.BackColor)
            e.Graphics.FillRectangle(brush, b)
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
        TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, b, App.CurrentTheme.TextColor, TextFormatFlags.VerticalCenter)
    End Sub
    Private Sub LVLibrary_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles LVLibrary.DrawItem
        If e.Item.Selected = False Then
            e.DrawDefault = True
        End If
    End Sub
    Private Sub LVLibrary_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles LVLibrary.DrawSubItem
        Static b As Rectangle
        If e.Item.Selected = True Then
            b = e.Bounds
            If e.ColumnIndex = 0 AndAlso LVLibrary.Columns(0).DisplayIndex <> 0 Then
                Dim i As Integer
                For i = 0 To LVLibrary.Columns.Count - 1
                    If LVLibrary.Columns(i).DisplayIndex = LVLibrary.Columns(0).DisplayIndex - 1 Then Exit For
                Next
                b = New Rectangle(e.Item.SubItems(i).Bounds.Right, e.Item.SubItems(i).Bounds.Y, LVLibrary.Columns(0).Width, e.Item.SubItems(i).Bounds.Height)
            End If
            e.Graphics.FillRectangle(New SolidBrush(App.CurrentTheme.TextColor), b)
            If e.ColumnIndex = 0 AndAlso LVLibrary.Columns("Artist").Index = 0 Then
                If Not String.IsNullOrEmpty(e.Item.ImageKey) Then e.Graphics.DrawImage(Resources.ImageAlbumArtSelect, New Rectangle(New Point(b.Location.X + 4, b.Location.Y + 2), New Size(16, 16)))
                TextRenderer.DrawText(e.Graphics, App.GenerateEllipsis(e.Graphics, e.SubItem.Text, New Font(e.Item.Font, FontStyle.Bold), e.Bounds.Size.Width - 18), New Font(e.Item.Font, FontStyle.Bold), New Point(b.Left + 18, b.Top + 2), App.CurrentTheme.BackColor, TextFormatFlags.NoPrefix Or TextFormatFlags.EndEllipsis)
            Else
                TextRenderer.DrawText(e.Graphics, App.GenerateEllipsis(e.Graphics, e.SubItem.Text, New Font(e.Item.Font, FontStyle.Bold), e.Bounds.Size.Width), New Font(e.Item.Font, FontStyle.Bold), New Point(b.Left + 2, b.Top + 2), App.CurrentTheme.BackColor, TextFormatFlags.NoPrefix Or TextFormatFlags.EndEllipsis)
            End If
        Else
            e.DrawDefault = True
        End If
    End Sub
    Private Sub LVLibrary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVLibrary.SelectedIndexChanged
        If LVLibrary.SelectedItems.Count > 0 Then
            SetLibraryCountText()
            AlbumArtIndex = 0
            ShowAlbumArt()
            SetExtInfo()
        Else
            ResetExtInfo()
        End If
    End Sub
    Private Sub LVLibrary_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles LVLibrary.ColumnClick
        SetLibraryTitles()
        If LVLibrary.Items.Count = 0 Then
            ClearLibrarySorts()
        Else
            Select Case e.Column
                Case LVLibrary.Columns("Artist").Index
                    Select Case LibraryArtistSort
                        Case SortOrder.Ascending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            ClearLibrarySorts()
                            LibraryArtistSort = SortOrder.Descending
                            LVLibrary.Columns(e.Column).Text += " ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            ClearLibrarySorts()
                            LibraryArtistSort = SortOrder.Ascending
                            LVLibrary.Columns(e.Column).Text += " ↑"
                    End Select
                Case LVLibrary.Columns("Title").Index
                    Select Case LibraryTitleSort
                        Case SortOrder.Ascending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            ClearLibrarySorts()
                            LibraryTitleSort = SortOrder.Descending
                            LVLibrary.Columns(e.Column).Text += " ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            ClearLibrarySorts()
                            LibraryTitleSort = SortOrder.Ascending
                            LVLibrary.Columns(e.Column).Text += " ↑"
                    End Select
                Case LVLibrary.Columns("Album").Index
                    Select Case LibraryAlbumSort
                        Case SortOrder.Ascending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            ClearLibrarySorts()
                            LibraryAlbumSort = SortOrder.Descending
                            LVLibrary.Columns(e.Column).Text += " ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            ClearLibrarySorts()
                            LibraryAlbumSort = SortOrder.Ascending
                            LVLibrary.Columns(e.Column).Text += " ↑"
                    End Select
                Case LVLibrary.Columns("Genre").Index
                    Select Case LibraryGenreSort
                        Case SortOrder.Ascending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            ClearLibrarySorts()
                            LibraryGenreSort = SortOrder.Descending
                            LVLibrary.Columns(e.Column).Text += " ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            ClearLibrarySorts()
                            LibraryGenreSort = SortOrder.Ascending
                            LVLibrary.Columns(e.Column).Text += " ↑"
                    End Select
                Case LVLibrary.Columns("Year").Index
                    Select Case LibraryYearSort
                        Case SortOrder.Ascending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            ClearLibrarySorts()
                            LibraryYearSort = SortOrder.Descending
                            LVLibrary.Columns(e.Column).Text += " ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            ClearLibrarySorts()
                            LibraryYearSort = SortOrder.Ascending
                            LVLibrary.Columns(e.Column).Text += " ↑"
                    End Select
                Case LVLibrary.Columns("Duration").Index
                    Select Case LibraryDurationSort
                        Case SortOrder.Ascending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            ClearLibrarySorts()
                            LibraryDurationSort = SortOrder.Descending
                            LVLibrary.Columns(e.Column).Text += " ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            ClearLibrarySorts()
                            LibraryDurationSort = SortOrder.Ascending
                            LVLibrary.Columns(e.Column).Text += " ↑"
                    End Select
                Case LVLibrary.Columns("Artists").Index
                    Select Case LibraryArtistsSort
                        Case SortOrder.Ascending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            ClearLibrarySorts()
                            LibraryArtistsSort = SortOrder.Descending
                            LVLibrary.Columns(e.Column).Text += " ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            ClearLibrarySorts()
                            LibraryArtistsSort = SortOrder.Ascending
                            LVLibrary.Columns(e.Column).Text += " ↑"
                    End Select
                Case LVLibrary.Columns("Comments").Index
                    Select Case LibraryCommentsSort
                        Case SortOrder.Ascending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            ClearLibrarySorts()
                            LibraryCommentsSort = SortOrder.Descending
                            LVLibrary.Columns(e.Column).Text += " ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            ClearLibrarySorts()
                            LibraryCommentsSort = SortOrder.Ascending
                            LVLibrary.Columns(e.Column).Text += " ↑"
                    End Select
                Case LVLibrary.Columns("FilePath").Index
                    Select Case LibraryFilenameSort
                        Case SortOrder.Ascending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Descending)
                            ClearLibrarySorts()
                            LibraryFilenameSort = SortOrder.Descending
                            LVLibrary.Columns(e.Column).Text += " ↓"
                        Case SortOrder.None, SortOrder.Descending
                            LVLibrary.ListViewItemSorter = New My.ListViewItemStringComparer(e.Column, SortOrder.Ascending)
                            ClearLibrarySorts()
                            LibraryFilenameSort = SortOrder.Ascending
                            LVLibrary.Columns(e.Column).Text += " ↑"
                    End Select
            End Select
        End If
    End Sub
    Private Sub LVLibrary_DoubleClick(sender As Object, e As EventArgs) Handles LVLibrary.DoubleClick
        Play()
    End Sub
    Private Sub CMLibraryOpening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMLibrary.Opening
        CMIHelperApp1.Text = "Open with " + App.HelperApp1Name
        CMIHelperApp2.Text = "Open with " + App.HelperApp2Name
        If LVLibrary.Groups.Count = 0 Then
            CMIAddGroupToPlaylist.Visible = False
            CMICollapseGroup.Visible = False
            CMIExpandAllGroups.Visible = False
            CMISeparatorGroupBy.Visible = False
        Else
            CMIAddGroupToPlaylist.Visible = True
            CMICollapseGroup.Visible = True
            CMIExpandAllGroups.Visible = True
            CMISeparatorGroupBy.Visible = True
            CMIExpandAllGroups.Enabled = False
            For Each g As ListViewGroup In LVLibrary.Groups
                If g.CollapsedState = ListViewGroupCollapsedState.Collapsed Then
                    CMIExpandAllGroups.Enabled = True
                    Exit For
                End If
            Next
        End If
        If LVLibrary.SelectedItems.Count = 0 Then
            CMICopyTitle.ToolTipText = String.Empty
            CMICopyFileName.ToolTipText = String.Empty
            CMICopyFilePath.ToolTipText = String.Empty
            CMIHelperApp1.Visible = False
            CMIHelperApp2.Visible = False
        Else
            CMICopyTitle.ToolTipText = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Title").Index).Text
            CMICopyFileName.ToolTipText = IO.Path.GetFileNameWithoutExtension(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)
            CMICopyFilePath.ToolTipText = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text
            If File.Exists(App.HelperApp1Path) Then
                CMIHelperApp1.Visible = True
            Else
                CMIHelperApp1.Visible = False
            End If
            If File.Exists(App.HelperApp2Path) Then
                CMIHelperApp2.Visible = True
            Else
                CMIHelperApp2.Visible = False
            End If
        End If
    End Sub
    Private Sub CMIPlayClick(sender As Object, e As EventArgs) Handles CMIPlay.Click
        Play()
    End Sub
    Private Sub CMIPlayWithWindows_Click(sender As Object, e As EventArgs) Handles CMIPlayWithWindows.Click
        If LVLibrary.SelectedItems.Count > 0 Then App.PlayWithWindows(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)
    End Sub
    Private Sub CMIAddToPlaylistClick(sender As Object, e As EventArgs) Handles CMIAddToPlaylist.Click
        Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
        LblStatus.Text = "Adding Selected Songs to Playlist..."
        LblStatus.Visible = True
        LblStatus.Refresh()
        Player.LVPlaylist.Visible = False
        Player.LVPlaylist.SuspendLayout()
        For Each item As ListViewItem In LVLibrary.SelectedItems
            AddToPlaylist(item)
        Next
        Player.LVPlaylist.ResumeLayout()
        Player.LVPlaylist.Visible = True
        LblStatus.Visible = False
        App.WriteToLog("Selected Library Added to Playlist (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
    End Sub
    Private Sub CMIAddAllToPlaylistClick(sender As Object, e As EventArgs) Handles CMIAddAllToPlaylist.Click
        Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
        LblStatus.Text = "Adding All Songs to Playlist..."
        LblStatus.Visible = True
        LblStatus.Refresh()
        Player.LVPlaylist.Visible = False
        Player.LVPlaylist.SuspendLayout()
        For Each item As ListViewItem In LVLibrary.Items
            AddToPlaylist(item)
        Next
        Player.LVPlaylist.ResumeLayout()
        Player.LVPlaylist.Visible = True
        LblStatus.Visible = False
        App.WriteToLog("Full Library Added to Playlist (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
    End Sub
    Private Sub CMIAddGroupToPlaylist_Click(sender As Object, e As EventArgs) Handles CMIAddGroupToPlaylist.Click

        'Initialize
        Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
        LblStatus.Text = "Adding Group to Playlist..."
        LblStatus.Visible = True
        LblStatus.Refresh()
        Player.LVPlaylist.Visible = False
        Player.LVPlaylist.SuspendLayout()

        'Get group name from selected list view item
        Dim groupname As String
        Select Case LibraryGroupBy
            Case LibraryGroupMode.Album
                groupname = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Album").Index).Text
            Case LibraryGroupMode.Artist
                groupname = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Artist").Index).Text
            Case LibraryGroupMode.Genre
                groupname = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Genre").Index).Text
            Case LibraryGroupMode.Year
                groupname = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Year").Index).Text
            Case Else
                groupname = String.Empty
        End Select

        'Get group index
        Dim groupindex As Int16 = GetLibraryGroupIndex(groupname)

        'Add to Playlist
        For Each item As ListViewItem In LVLibrary.Groups(groupindex).Items
            AddToPlaylist(item)
        Next

        'Finalize
        Player.LVPlaylist.ResumeLayout()
        Player.LVPlaylist.Visible = True
        LblStatus.Visible = False
        App.WriteToLog(LibraryGroupBy.ToString + " Library Added to Playlist (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")

    End Sub
    Private Sub CMICollapseGroup_Click(sender As Object, e As EventArgs) Handles CMICollapseGroup.Click

        'Get group name from selected list view item
        Dim groupname As String
        Select Case LibraryGroupBy
            Case LibraryGroupMode.Album
                groupname = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Album").Index).Text
            Case LibraryGroupMode.Artist
                groupname = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Artist").Index).Text
            Case LibraryGroupMode.Genre
                groupname = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Genre").Index).Text
            Case LibraryGroupMode.Year
                groupname = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Year").Index).Text
            Case Else
                groupname = String.Empty
        End Select

        'Get group index
        Dim groupindex As Int16 = GetLibraryGroupIndex(groupname)

        'Collapse group
        If Not groupindex = -1 Then
            LVLibrary.Groups(groupindex).CollapsedState = ListViewGroupCollapsedState.Collapsed
        End If

    End Sub
    Private Sub CMIExpandAllGroups_Click(sender As Object, e As EventArgs) Handles CMIExpandAllGroups.Click
        For Each g As ListViewGroup In LVLibrary.Groups
            g.CollapsedState = ListViewGroupCollapsedState.Expanded
        Next
    End Sub
    Private Sub CMIHelperApp1Click(sender As Object, e As EventArgs) Handles CMIHelperApp1.Click
        App.HelperApp1(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)
    End Sub
    Private Sub CMIHelperApp2Click(sender As Object, e As EventArgs) Handles CMIHelperApp2.Click
        App.HelperApp2(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)
    End Sub
    Private Sub CMIOpenLocationClick(sender As Object, e As EventArgs) Handles CMIOpenLocation.Click
        If LVLibrary.SelectedItems.Count > 0 Then App.OpenFileLocation(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)
    End Sub
    Private Sub CMICopyTitleClick(sender As Object, e As EventArgs) Handles CMICopyTitle.Click
        If LVLibrary.SelectedItems.Count > 0 Then Clipboard.SetText(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Title").Index).Text)
    End Sub
    Private Sub CMICopyFilenameClick(sender As Object, e As EventArgs) Handles CMICopyFileName.Click
        If LVLibrary.SelectedItems.Count > 0 Then Clipboard.SetText(IO.Path.GetFileNameWithoutExtension(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text))
    End Sub
    Private Sub CMICopyFilePathClick(sender As Object, e As EventArgs) Handles CMICopyFilePath.Click
        If LVLibrary.SelectedItems.Count > 0 Then Clipboard.SetText(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)
    End Sub
    Private Sub PicBoxAlbumArtMouseDown(sender As Object, e As MouseEventArgs) Handles PicBoxAlbumArt.MouseDown
        If PicBoxAlbumArt.Visible AndAlso e.Button = MouseButtons.Left Then
            PicBoxAlbumArtStartLocation = PicBoxAlbumArt.Location
            If Me.Width > PicBoxAlbumArtSuperSize.Width AndAlso Me.Height > PicBoxAlbumArtSuperSize.Height Then
                PicBoxAlbumArt.Top = PicBoxAlbumArt.Bottom - PicBoxAlbumArtSuperSize.Height
                PicBoxAlbumArt.Size = PicBoxAlbumArtSuperSize
            Else
                PicBoxAlbumArt.Top = PicBoxAlbumArt.Bottom - PicBoxAlbumArtLargeSize.Height
                PicBoxAlbumArt.Size = PicBoxAlbumArtLargeSize
            End If
        End If
    End Sub
    Private Sub PicBoxAlbumArtMouseUp(sender As Object, e As MouseEventArgs) Handles PicBoxAlbumArt.MouseUp
        If PicBoxAlbumArt.Visible AndAlso e.Button = MouseButtons.Left Then
            PicBoxAlbumArt.Size = PicBoxAlbumArtSmallSize
            PicBoxAlbumArt.Location = PicBoxAlbumArtStartLocation
        End If
    End Sub
    Private Sub LblAlbumArtSelectClick(sender As Object, e As EventArgs) Handles LblAlbumArtSelect.Click
        AlbumArtIndex += CType(1, Byte)
        ShowAlbumArt()
    End Sub
    Private Sub LblLibraryCounts_DoubleClick(sender As Object, e As EventArgs) Handles LblLibraryCounts.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub LblExtInfo_DoubleClick(sender As Object, e As EventArgs) Handles LblExtTitle.DoubleClick, LblExtFileInfo.DoubleClick, LblExtProperties.DoubleClick, LblExtType.DoubleClick, LblHistory.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub LblExtInfo_Resize(sender As Object, e As EventArgs) Handles LblExtTitle.Resize, LblExtFileInfo.Resize, LblExtProperties.Resize, LblExtType.Resize, LblHistory.Resize
        SetExtInfo()
    End Sub
    Private Sub TxbxLibrarySearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxbxLibrarySearch.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Escape) Then e.Handled = True
    End Sub
    Private Sub TxbxLibrarySearch_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxbxLibrarySearch.PreviewKeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                ResetTxtBoxLibrarySearch()
                LVLibrary.Focus()
                IsTextBoxLibrarySearch = True
        End Select
    End Sub
    Private Sub TxbxLibrarySearchEnter(sender As Object, e As EventArgs) Handles TxbxLibrarySearch.Enter
        If TxbxLibrarySearch.Text = LibrarySearchTitle Then
            TxbxLibrarySearch.ResetText()
            Select Case App.Theme
                Case App.Themes.Accent
                    TxbxLibrarySearch.ForeColor = App.CurrentTheme.AccentTextColor
                Case Else
                    TxbxLibrarySearch.ForeColor = App.CurrentTheme.TextColor
            End Select
        End If
    End Sub
    Private Sub TxbxLibrarySearchLeave(sender As Object, e As EventArgs) Handles TxbxLibrarySearch.Leave
        If TxbxLibrarySearch.Text = String.Empty Or Not LBXLibrarySearch.Focused Then
            ResetTxtBoxLibrarySearch()
        End If
    End Sub
    Private Sub TxbxLibrarySearchTextChanged(sender As Object, e As EventArgs) Handles TxbxLibrarySearch.TextChanged
        If TxbxLibrarySearch.Text Is String.Empty And LBXLibrarySearch.Visible Then
            LBXLibrarySearch.Visible = False
            LibrarySearchItems.Clear()
        ElseIf TxbxLibrarySearch.Text IsNot String.Empty And TxbxLibrarySearch.Text IsNot LibrarySearchTitle Then
            LibrarySearchItems.Clear()
            For Each item As ListViewItem In LVLibrary.Items
                If item.SubItems(LVLibrary.Columns("Artist").Index).Text.ToLower.Contains(TxbxLibrarySearch.Text.ToLower) Then
                    LibrarySearchItems.Add(item)
                ElseIf item.SubItems(LVLibrary.Columns("Title").Index).Text.ToLower.Contains(TxbxLibrarySearch.Text.ToLower) Then
                    LibrarySearchItems.Add(item)
                ElseIf item.SubItems(LVLibrary.Columns("Album").Index).Text.ToLower.Contains(TxbxLibrarySearch.Text.ToLower) Then
                    LibrarySearchItems.Add(item)
                ElseIf item.SubItems(LVLibrary.Columns("Genre").Index).Text.ToLower.Contains(TxbxLibrarySearch.Text.ToLower) Then
                    LibrarySearchItems.Add(item)
                ElseIf item.SubItems(LVLibrary.Columns("Year").Index).Text.ToLower.Contains(TxbxLibrarySearch.Text.ToLower) Then
                    LibrarySearchItems.Add(item)
                ElseIf item.SubItems(LVLibrary.Columns("Artists").Index).Text.ToLower.Contains(TxbxLibrarySearch.Text.ToLower) Then
                    LibrarySearchItems.Add(item)
                ElseIf item.SubItems(LVLibrary.Columns("Comments").Index).Text.ToLower.Contains(TxbxLibrarySearch.Text.ToLower) Then
                    LibrarySearchItems.Add(item)
                ElseIf item.SubItems(LVLibrary.Columns("FilePath").Index).Text.ToLower.Contains(TxbxLibrarySearch.Text.ToLower) Then
                    LibrarySearchItems.Add(item)
                End If
            Next
            LBXLibrarySearch.Items.Clear()
            If LibrarySearchItems.Count > 0 Then
                For Each item As ListViewItem In LibrarySearchItems
                    LBXLibrarySearch.Items.Add((item.SubItems(LVLibrary.Columns("Artist").Index).Text + ", " + item.SubItems(LVLibrary.Columns("Title").Index).Text + ", " + item.SubItems(LVLibrary.Columns("Album").Index).Text).TrimStart(", ".ToCharArray).TrimEnd(", ".ToCharArray))
                Next
                LBXLibrarySearch.Visible = True
            End If
        End If
    End Sub
    Private Sub LBXLibrarySearchSelectedIndexChanged(sender As Object, e As EventArgs) Handles LBXLibrarySearch.SelectedIndexChanged
        If LBXLibrarySearch.SelectedItems.Count = 1 Then
            LVLibrary.EnsureVisible(LibrarySearchItems.Item(LBXLibrarySearch.SelectedIndex).Index)
            LVLibrary.SelectedIndices.Clear()
            LVLibrary.SelectedIndices.Add(LibrarySearchItems.Item(LBXLibrarySearch.SelectedIndex).Index)
            ResetTxtBoxLibrarySearch()
            LBXLibrarySearch.Items.Clear()
            LVLibrary.Select()
        End If
    End Sub
    Private Sub BtnSearchFoldersClick(sender As Object, e As EventArgs) Handles BtnSearchFolders.Click
        RadBtnGroupByNone.Checked = True
        SearchFolders()
        LVLibrary.Focus()
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub
    Private Sub RadBtnGroupByAlbum_CheckedChanged(sender As Object, e As EventArgs) Handles RadBtnGroupByAlbum.CheckedChanged
        If RadBtnGroupByAlbum.Checked Then
            LibraryGroupBy = LibraryGroupMode.Album
            SetGroups()
        End If
        LVLibrary.Focus()
        RadBtnGroupByAlbum.TabStop = False
    End Sub
    Private Sub RadBtnGroupByArtist_CheckedChanged(sender As Object, e As EventArgs) Handles RadBtnGroupByArtist.CheckedChanged
        If RadBtnGroupByArtist.Checked Then
            LibraryGroupBy = LibraryGroupMode.Artist
            SetGroups()
        End If
        LVLibrary.Focus()
        RadBtnGroupByArtist.TabStop = False
    End Sub
    Private Sub RadBtnGroupByGenre_CheckedChanged(sender As Object, e As EventArgs) Handles RadBtnGroupByGenre.CheckedChanged
        If RadBtnGroupByGenre.Checked Then
            LibraryGroupBy = LibraryGroupMode.Genre
            SetGroups()
        End If
        LVLibrary.Focus()
        RadBtnGroupByGenre.TabStop = False
    End Sub
    Private Sub RadBtnGroupByYear_CheckedChanged(sender As Object, e As EventArgs) Handles RadBtnGroupByYear.CheckedChanged
        If RadBtnGroupByYear.Checked Then
            LibraryGroupBy = LibraryGroupMode.Year
            SetGroups()
        End If
        LVLibrary.Focus()
        RadBtnGroupByYear.TabStop = False
    End Sub
    Private Sub RadBtnGroupByNone_CheckedChanged(sender As Object, e As EventArgs) Handles RadBtnGroupByNone.CheckedChanged
        If RadBtnGroupByNone.Checked Then
            LibraryGroupBy = LibraryGroupMode.None
            ClearGroups()
        End If
        LVLibrary.Focus()
        RadBtnGroupByNone.TabStop = False
    End Sub
    Private Sub GrpBoxGroupBy_DoubleClick(sender As System.Object, e As System.EventArgs) Handles GrpBoxGroupBy.DoubleClick
        ToggleMaximized()
    End Sub

    'Procedures
    Friend Overloads Sub Show(filename As String)
        LVLibrary.SelectedItems.Clear()
        Show()
        Dim item As ListViewItem = LVLibrary.FindItemWithText(filename, True, 0)
        If item IsNot Nothing Then
            item.Selected = True
            item.EnsureVisible()
        End If
        BringToFront()
    End Sub
    Private Function FormatPlaylistTitle(ByRef item As ListViewItem) As String
        FormatPlaylistTitle = ""
        Select Case App.PlaylistTitleFormat
            Case App.PlaylistTitleFormats.UseFilename
                FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
            Case App.PlaylistTitleFormats.Song
                If item.SubItems(LVLibrary.Columns("Title").Index).Text Is String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                    End If
                End If
            Case App.PlaylistTitleFormats.SongGenre
                If item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.SongArtist
                If item.SubItems(LVLibrary.Columns("Artist").Index).Text Is String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.SongArtistAlbum
                If item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.SongAlbumArtist
                If item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.SongArtistGenre
                If item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.SongGenreArtist
                If item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.SongArtistAlbumGenre
                If item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.SongAlbumArtistGenre
                If item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.SongGenreArtistAlbum
                If item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.ArtistSong
                If item.SubItems(LVLibrary.Columns("Artist").Index).Text Is String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.ArtistSongAlbum
                If item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.ArtistAlbumSong
                If item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.ArtistGenreSong
                If item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.ArtistSongGenre
                If item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.ArtistSongAlbumGenre
                If item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.ArtistGenreSongAlbum
                If item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.AlbumSongArtist
                If item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.AlbumArtistSong
                If item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.AlbumGenreSongArtist
                If item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.AlbumGenreArtistSong
                If item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.AlbumSongArtistGenre
                If item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.AlbumArtistSongGenre
                If item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.GenreSong
                If item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.GenreSongArtist
                If item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.GenreArtistSong
                If item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.GenreAlbumSongArtist
                If item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.GenreAlbumArtistSong
                If item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.GenreSongArtistAlbum
                If item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                End If
            Case App.PlaylistTitleFormats.GenreSongAlbumArtist
                If item.SubItems(LVLibrary.Columns("Genre").Index).Text = String.Empty Then
                    FormatPlaylistTitle = IO.Path.GetFileNameWithoutExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
                Else
                    If App.PlaylistTitleRemoveSpaces Then
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text.Replace(" ", "")
                    Else
                        FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Genre").Index).Text
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Title").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Title").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Album").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Album").Index).Text
                        End If
                    End If
                    If Not item.SubItems(LVLibrary.Columns("Artist").Index).Text = String.Empty Then
                        FormatPlaylistTitle += App.PlaylistTitleSeparator
                        If App.PlaylistTitleRemoveSpaces Then
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text.Replace(" ", "")
                        Else
                            FormatPlaylistTitle += item.SubItems(LVLibrary.Columns("Artist").Index).Text
                        End If
                    End If
                End If
        End Select
        If App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(item.SubItems(LVLibrary.Columns("FilePath").Index).Text)) Then
            FormatPlaylistTitle += App.PlaylistVideoIdentifier
        End If
    End Function
    Private Sub SearchFolders()
        If App.LibrarySearchFolders.Count > 0 Then
            Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
            Dim files As New Collections.Generic.List(Of String)
            Dim tlfile As TagLib.File
            Dim item As New ListViewItem()
            Dim col As New Collections.Generic.List(Of ListViewItem)
            LVLibrary.Items.Clear()
            ShowAlbumArt()
            ResetExtInfo()
            LVLibrary.Refresh()
            LVLibrary.Visible = False
            LblStatus.Text = "Searching your Folders for Media Files..."
            LblStatus.Visible = True
            LblStatus.Refresh()
            ClearHistoryInLibraryFlag()
            For Each folder As String In App.LibrarySearchFolders
                Try
                    If App.LibrarySearchSubFolders Then
                        files.AddRange(IO.Directory.GetFiles(folder, "*", IO.SearchOption.AllDirectories))
                    Else
                        files.AddRange(IO.Directory.GetFiles(folder, "*", IO.SearchOption.TopDirectoryOnly))
                    End If
                Catch ex As Exception
                    WriteToLog("Error while Searching Folder: " + folder + Chr(13) + ex.Message)
                End Try
                For Each file As String In files
                    If App.ExtensionDictionary.ContainsKey(Path.GetExtension(file)) Then
                        item = New ListViewItem
                        'Keep this in sync with number of columns set in Load Event
                        item.SubItems.Add(String.Empty)
                        item.SubItems.Add(String.Empty)
                        item.SubItems.Add(String.Empty)
                        item.SubItems.Add(String.Empty)
                        item.SubItems.Add(String.Empty)
                        item.SubItems.Add(String.Empty)
                        item.SubItems.Add(String.Empty)
                        item.SubItems.Add(String.Empty)
                        item.SubItems.Add(String.Empty)
                        item.SubItems.Add(String.Empty)
                        item.SubItems.Add(String.Empty)
                        Try
                            tlfile = TagLib.File.Create(file)
                        Catch ex As Exception
                            WriteToLog("TagLib Error while Searching Folders, Cannot read from file: " + file + Chr(13) + ex.Message)
                            tlfile = Nothing
                        End Try
                        If tlfile Is Nothing Then
                            item.SubItems(LVLibrary.Columns("Title").Index).Text = IO.Path.GetFileNameWithoutExtension(file)
                        Else
                            'Artist
                            item.SubItems(LVLibrary.Columns("Artist").Index).Text = tlfile.Tag.FirstPerformer
                            'Title
                            If tlfile.Tag.Title = String.Empty Then
                                item.SubItems(LVLibrary.Columns("Title").Index).Text = IO.Path.GetFileNameWithoutExtension(file)
                            Else
                                item.SubItems(LVLibrary.Columns("Title").Index).Text = tlfile.Tag.Title
                            End If
                            'Album
                            item.SubItems(LVLibrary.Columns("Album").Index).Text = tlfile.Tag.Album
                            'Genre
                            item.SubItems(LVLibrary.Columns("Genre").Index).Text = tlfile.Tag.FirstGenre
                            'Year
                            If tlfile.Tag.Year <> 0 Then
                                item.SubItems(LVLibrary.Columns("Year").Index).Text = tlfile.Tag.Year.ToString
                            End If
                            'Track
                            If tlfile.Tag.Track > 0 Then item.SubItems(LVLibrary.Columns("Track").Index).Text = tlfile.Tag.Track.ToString
                            'Tracks
                            If tlfile.Tag.TrackCount > 0 Then item.SubItems(LVLibrary.Columns("Tracks").Index).Text = tlfile.Tag.TrackCount.ToString
                            'Duration
                            If tlfile.Properties IsNot Nothing AndAlso tlfile.Properties.Duration <> TimeSpan.Zero Then
                                item.SubItems(LVLibrary.Columns("Duration").Index).Text = tlfile.Properties.Duration.ToString("hh\:mm\:ss")
                            End If
                            'Artists
                            If tlfile.Tag.Performers IsNot Nothing Then
                                If tlfile.Tag.Performers.Count > 1 Then
                                    For index = 1 To tlfile.Tag.Performers.Count - 1
                                        item.SubItems(LVLibrary.Columns("Artists").Index).Text += tlfile.Tag.Performers(index) + ", "
                                    Next
                                    item.SubItems(LVLibrary.Columns("Artists").Index).Text = item.SubItems(LVLibrary.Columns("Artists").Index).Text.TrimEnd(", ".ToCharArray)
                                End If
                            End If
                            'Comments
                            item.SubItems(LVLibrary.Columns("Comments").Index).Text = tlfile.Tag.Comment
                            'Album Art Identifier
                            If tlfile.Tag.Pictures.Count > 0 Then item.ImageKey = "AlbumArt"
                        End If
                        'Filename
                        item.SubItems(LVLibrary.Columns("FilePath").Index).Text = file
                        If App.AudioExtensionDictionary.ContainsKey(Path.GetExtension(file)) Then
                            item.SubItems(LVLibrary.Columns("AV").Index).Text = "A"
                        Else
                            item.SubItems(LVLibrary.Columns("AV").Index).Text = "V"
                        End If
                        col.Add(item)
                        AddToHistoryFromLibrary(file)
                    End If
                Next
                files.Clear()
            Next
            LVLibrary.Items.AddRange(col.ToArray)
            col.Clear()
            item = Nothing
            tlfile = Nothing
            LblStatus.Visible = False
            LVLibrary.Visible = True
            SetLibraryCountText()
            App.WriteToLog("Folders Searched (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
        End If
    End Sub
    Private Sub Play()
        If LVLibrary.SelectedItems.Count > 0 Then
            Player.PlayFromLibrary(FormatPlaylistTitle(LVLibrary.SelectedItems(0)), LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)
        End If
    End Sub
    Private Sub AddToPlaylist(item As ListViewItem)
        Player.AddToPlaylistFromLibrary(FormatPlaylistTitle(item), item.SubItems(LVLibrary.Columns("FilePath").Index).Text)
        item = Nothing
    End Sub
    Private Sub SaveLibrary()
        If LVLibrary.Items.Count = 0 Then
            If My.Computer.FileSystem.FileExists(App.LibraryPath) Then My.Computer.FileSystem.DeleteFile(App.LibraryPath)
        Else
            Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
            Dim items As New Collections.Generic.List(Of LibraryItemType)
            For Each libraryitem As ListViewItem In LVLibrary.Items
                Dim newitem As New LibraryItemType
                newitem.Artist = libraryitem.SubItems(LVLibrary.Columns("Artist").Index).Text
                newitem.Title = libraryitem.SubItems(LVLibrary.Columns("Title").Index).Text
                newitem.Album = libraryitem.SubItems(LVLibrary.Columns("Album").Index).Text
                newitem.Genre = libraryitem.SubItems(LVLibrary.Columns("Genre").Index).Text
                newitem.Year = libraryitem.SubItems(LVLibrary.Columns("Year").Index).Text
                newitem.Track = libraryitem.SubItems(LVLibrary.Columns("Track").Index).Text
                newitem.Tracks = libraryitem.SubItems(LVLibrary.Columns("Tracks").Index).Text
                newitem.Duration = libraryitem.SubItems(LVLibrary.Columns("Duration").Index).Text
                newitem.AV = libraryitem.SubItems(LVLibrary.Columns("AV").Index).Text
                newitem.Artists = libraryitem.SubItems(LVLibrary.Columns("Artists").Index).Text
                newitem.Comments = libraryitem.SubItems(LVLibrary.Columns("Comments").Index).Text
                newitem.FilePath = libraryitem.SubItems(LVLibrary.Columns("FilePath").Index).Text
                If libraryitem.ImageKey = "AlbumArt" Then
                    newitem.HasAlbumArt = True
                Else
                    newitem.HasAlbumArt = False
                End If
                items.Add(newitem)
                newitem = Nothing
            Next
            Dim writer As New System.Xml.Serialization.XmlSerializer(GetType(Collections.Generic.List(Of LibraryItemType)))
            If Not My.Computer.FileSystem.DirectoryExists(App.UserPath) Then
                My.Computer.FileSystem.CreateDirectory(App.UserPath)
            End If
            Dim file As New System.IO.StreamWriter(App.LibraryPath)
            writer.Serialize(file, items)
            file.Close()
            file.Dispose()
            writer = Nothing
            items.Clear()
            items = Nothing
            App.WriteToLog("Library Saved (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
        End If
    End Sub
    Private Sub LoadLibrary()
        If My.Computer.FileSystem.FileExists(App.LibraryPath) Then
            Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
            Dim reader As New System.Xml.Serialization.XmlSerializer(GetType(Collections.Generic.List(Of LibraryItemType)))
            Dim file As New System.IO.StreamReader(App.LibraryPath)
            Dim items As Collections.Generic.List(Of LibraryItemType)
            Try
                items = DirectCast(reader.Deserialize(file), Collections.Generic.List(Of LibraryItemType))
                For Each item As LibraryItemType In items
                    Dim lvitem As New ListViewItem
                    'Keep this in sync with number of columns set in Load Event
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems.Add(String.Empty)
                    lvitem.SubItems(LVLibrary.Columns("Artist").Index).Text = item.Artist
                    lvitem.SubItems(LVLibrary.Columns("Title").Index).Text = item.Title
                    lvitem.SubItems(LVLibrary.Columns("Album").Index).Text = item.Album
                    lvitem.SubItems(LVLibrary.Columns("Genre").Index).Text = item.Genre
                    lvitem.SubItems(LVLibrary.Columns("Year").Index).Text = item.Year
                    lvitem.SubItems(LVLibrary.Columns("Track").Index).Text = item.Track
                    lvitem.SubItems(LVLibrary.Columns("Tracks").Index).Text = item.Tracks
                    lvitem.SubItems(LVLibrary.Columns("Duration").Index).Text = item.Duration
                    lvitem.SubItems(LVLibrary.Columns("AV").Index).Text = item.AV
                    lvitem.SubItems(LVLibrary.Columns("Artists").Index).Text = item.Artists
                    lvitem.SubItems(LVLibrary.Columns("Comments").Index).Text = item.Comments
                    lvitem.SubItems(LVLibrary.Columns("FilePath").Index).Text = item.FilePath
                    If item.HasAlbumArt Then lvitem.ImageKey = "AlbumArt"
                    LVLibrary.Items.Add(lvitem)
                    lvitem = Nothing
                Next
            Catch
                items = Nothing
            End Try
            file.Close()
            file.Dispose()
            reader = Nothing
            If items Is Nothing Then
                App.WriteToLog("Library Not Loaded: File not valid (" + App.LibraryPath + ")")
            Else
                items.Clear()
                items = Nothing
                App.WriteToLog("Library Loaded (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
            End If
        Else
            App.WriteToLog("Library Not Loaded: File does not exist")
        End If
        SetLibraryCountText()
    End Sub
    Private Sub ShowAlbumArt()
        If LVLibrary.SelectedItems.Count > 0 Then
            Dim tlfile As TagLib.File
            Try
                tlfile = TagLib.File.Create(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)
            Catch ex As Exception
                WriteToLog("TagLib Error while Showing Album Art, Cannot read from file: " + LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text + Chr(13) + ex.Message)
                tlfile = Nothing
            End Try
            If tlfile Is Nothing Then
                PicBoxAlbumArt.Visible = False
                LblAlbumArtSelect.Visible = False
            Else
                If tlfile.Tag.Pictures.Length = 0 Then
                    PicBoxAlbumArt.Visible = False
                    LblAlbumArtSelect.Visible = False
                Else
                    If AlbumArtIndex + 1 > tlfile.Tag.Pictures.Count Then AlbumArtIndex = 0
                    Dim ms As New IO.MemoryStream(tlfile.Tag.Pictures(AlbumArtIndex).Data.Data)
                    Try
                        PicBoxAlbumArt.Image = Image.FromStream(ms)
                        PicBoxAlbumArt.Visible = True
                    Catch ex As Exception
                        WriteToLog("Error Loading Album Art for " + LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text + vbCr + ex.Message)
                        PicBoxAlbumArt.Visible = False
                        LblAlbumArtSelect.Visible = False
                    End Try
                    ms.Dispose()
                    ms = Nothing
                    If tlfile.Tag.Pictures.Count > 1 Then : LblAlbumArtSelect.Visible = True
                    Else : LblAlbumArtSelect.Visible = False
                    End If
                End If
                tlfile = Nothing
            End If
        Else
            PicBoxAlbumArt.Visible = False
            LblAlbumArtSelect.Visible = False
        End If
    End Sub
    Private Sub SetLibraryCountText()
        Dim count As Integer = 0
        LblLibraryCounts.ResetText()
        LblLibraryCounts.Text = LVLibrary.Items.Count.ToString
        If LVLibrary.Items.Count = 1 Then
            LblLibraryCounts.Text += " Song, "
        Else
            LblLibraryCounts.Text += " Songs, "
        End If
        For Each item As ListViewItem In LVLibrary.Items
            If item.SubItems(LVLibrary.Columns("AV").Index).Text = "A" Then count += 1
        Next
        LblLibraryCounts.Text += count.ToString + " Audio, "
        LblLibraryCounts.Text += (LVLibrary.Items.Count - count).ToString + " Video, "
        LblLibraryCounts.Text += LVLibrary.SelectedItems.Count.ToString + " Selected"
    End Sub
    Private Sub ResetTxtBoxLibrarySearch()
        LBXLibrarySearch.Visible = False
        TxbxLibrarySearch.Text = LibrarySearchTitle
        TxbxLibrarySearch.ForeColor = App.CurrentTheme.InactiveSearchTextColor
        LibrarySearchItems.Clear()
    End Sub
    Private Sub ClearLibrarySorts()
        LibraryArtistSort = SortOrder.None
        LibraryTitleSort = SortOrder.None
        LibraryAlbumSort = SortOrder.None
        LibraryGenreSort = SortOrder.None
        LibraryYearSort = SortOrder.None
        LibraryDurationSort = SortOrder.None
        LibraryArtistsSort = SortOrder.None
        LibraryCommentsSort = SortOrder.None
        LibraryFilenameSort = SortOrder.None
    End Sub
    Private Sub SetLibraryTitles()
        LVLibrary.Columns(0).Text = LVLibrary.Columns(0).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(1).Text = LVLibrary.Columns(1).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(2).Text = LVLibrary.Columns(2).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(3).Text = LVLibrary.Columns(3).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(4).Text = LVLibrary.Columns(4).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(5).Text = LVLibrary.Columns(5).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(6).Text = LVLibrary.Columns(6).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(7).Text = LVLibrary.Columns(7).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(8).Text = LVLibrary.Columns(8).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(9).Text = LVLibrary.Columns(9).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(10).Text = LVLibrary.Columns(10).Text.TrimEnd(" ↑↓".ToCharArray)
        LVLibrary.Columns(11).Text = LVLibrary.Columns(11).Text.TrimEnd(" ↑↓".ToCharArray)
    End Sub
    Private Sub SetGroups()

        'Initialize
        Debug.Print("SetGroups")
        Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
        LVLibrary.Visible = False
        LibraryGroups.Clear()
        LVLibrary.Groups.Clear()
        LVLibrary.SelectedItems.Clear()
        ShowAlbumArt()
        ResetExtInfo()

        'Create list of groups
        For Each item As ListViewItem In LVLibrary.Items
            Dim pGroup As String
            Dim aGroup As LibraryGroup
            Select Case LibraryGroupBy
                Case LibraryGroupMode.Album
                    pGroup = item.SubItems(LVLibrary.Columns("Album").Index).Text
                Case LibraryGroupMode.Artist
                    pGroup = item.SubItems(LVLibrary.Columns("Artist").Index).Text
                Case LibraryGroupMode.Genre
                    pGroup = item.SubItems(LVLibrary.Columns("Genre").Index).Text
                Case LibraryGroupMode.Year
                    pGroup = item.SubItems(LVLibrary.Columns("Year").Index).Text
                Case Else
                    pGroup = String.Empty
            End Select
            If GetLibraryGroupIndex(pGroup) = -1 Then
                aGroup = New LibraryGroup
                aGroup.Name = pGroup
                aGroup.Index = CShort(LibraryGroups.Count)
                LibraryGroups.Add(aGroup)
            End If
        Next

        'Create listview groups
        For Each group As LibraryGroup In LibraryGroups
            Dim groupname As String
            If String.IsNullOrEmpty(group.Name) Then
                Select Case LibraryGroupBy
                    Case LibraryGroupMode.Album
                        groupname = "Unknown Album"
                    Case LibraryGroupMode.Artist
                        groupname = "Unknown Artist"
                    Case LibraryGroupMode.Genre
                        groupname = "No Genre"
                    Case LibraryGroupMode.Year
                        groupname = "Unknown Year"
                    Case Else
                        groupname = "Default"
                End Select
            Else
                groupname = group.Name
            End If
            LVLibrary.Groups.Add(group.Index.ToString, groupname)
        Next
        '
        'Assign each listview item to a group
        For Each item As ListViewItem In LVLibrary.Items
            Dim aGroup As String
            Select Case LibraryGroupBy
                Case LibraryGroupMode.Album
                    aGroup = item.SubItems(LVLibrary.Columns("Album").Index).Text
                Case LibraryGroupMode.Artist
                    aGroup = item.SubItems(LVLibrary.Columns("Artist").Index).Text
                Case LibraryGroupMode.Genre
                    aGroup = item.SubItems(LVLibrary.Columns("Genre").Index).Text
                Case LibraryGroupMode.Year
                    aGroup = item.SubItems(LVLibrary.Columns("Year").Index).Text
                Case Else
                    aGroup = String.Empty
            End Select
            item.Group = LVLibrary.Groups(GetLibraryGroupIndex(aGroup))
        Next

        'Display totals for each group
        For Each group As ListViewGroup In LVLibrary.Groups
            group.Header += " (" + group.Items.Count.ToString + ")"
        Next

        'Finalize
        LVLibrary.Visible = True
        App.WriteToLog("Songs Grouped By " + LibraryGroupBy.ToString + " (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")

    End Sub
    Private Function GetLibraryGroupIndex(groupname As String) As Int16
        For Each group As LibraryGroup In LibraryGroups
            If group.Name = groupname Then Return group.Index
        Next
        Return -1
    End Function
    Private Sub ClearGroups()
        'Debug.Print("ClearGroups")
        LibraryGroups.Clear()
        LVLibrary.Groups.Clear()
    End Sub
    Private Sub SetExtInfo()
        If LVLibrary.SelectedItems.Count > 0 Then
            Dim h As String = App.History.Find(Function(p) p.Path = (LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)).ToStringFull
            LblHistory.Text = GenerateEllipsis(LblHistory.CreateGraphics(), h, LblHistory.Font, LblHistory.Size.Width)
            If LblHistory.Text = h Then
                TipLibrary.SetToolTip(LblHistory, Nothing)
            Else
                TipLibrary.SetToolTip(LblHistory, h)
            End If
            LblExtTitle.Text = LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("Title").Index).Text
            Dim fInfo As IO.FileInfo
            fInfo = New IO.FileInfo(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)
            LblExtFileInfo.Text = fInfo.Extension.TrimStart(CChar(".")).ToUpper
            LblExtFileInfo.Text += " " + App.FormatFileSize(fInfo.Length, My.App.FormatFileSizeUnits.Auto, 2, False)
            fInfo = Nothing
            Dim tlFile As TagLib.File = Nothing
            Try
                tlFile = TagLib.File.Create(LVLibrary.SelectedItems(0).SubItems(LVLibrary.Columns("FilePath").Index).Text)
            Catch
            Finally
                If tlFile IsNot Nothing Then
                    Select Case tlFile.Properties.MediaTypes
                        Case TagLib.MediaTypes.Audio
                            LblExtProperties.Text = tlFile.Properties.AudioBitrate.ToString + " Kbps, " + tlFile.Properties.AudioSampleRate.ToString + " Hz, "
                            Select Case tlFile.Properties.AudioChannels
                                Case 1 : LblExtProperties.Text += "Mono"
                                Case 2 : LblExtProperties.Text += "Stereo"
                                Case Else : LblExtProperties.Text += tlFile.Properties.AudioChannels.ToString + " channels"
                            End Select
                        Case TagLib.MediaTypes.Video Or TagLib.MediaTypes.Audio
                            LblExtProperties.Text = tlFile.Properties.VideoWidth.ToString + "x" + tlFile.Properties.VideoHeight.ToString + ", Audio: "
                            LblExtProperties.Text += tlFile.Properties.AudioBitrate.ToString + " Kbps, " + tlFile.Properties.AudioSampleRate.ToString + " Hz, "
                            Select Case tlFile.Properties.AudioChannels
                                Case 1 : LblExtProperties.Text += "Mono"
                                Case 2 : LblExtProperties.Text += "Stereo"
                                Case Else : LblExtProperties.Text += tlFile.Properties.AudioChannels.ToString + " channels"
                            End Select
                    End Select
                    h = tlFile.Properties.MediaTypes.ToString + " (" + tlFile.Properties.Description + ")"
                    LblExtType.Text = GenerateEllipsis(LblExtType.CreateGraphics(), h, LblExtType.Font, LblExtType.Size.Width)
                    If LblExtType.Text = h Then
                        TipLibrary.SetToolTip(LblExtType, Nothing)
                    Else
                        TipLibrary.SetToolTip(LblExtType, h)
                    End If
                    tlFile.Dispose()
                    tlFile = Nothing
                End If
            End Try
        End If
    End Sub
    Private Sub ResetExtInfo()
        LblHistory.Text = String.Empty
        LblHistory.Refresh()
        LblExtTitle.Text = String.Empty
        LblExtTitle.Refresh()
        LblExtFileInfo.Text = String.Empty
        LblExtFileInfo.Refresh()
        LblExtProperties.Text = String.Empty
        LblExtProperties.Refresh()
        LblExtType.Text = String.Empty
        LblExtType.Refresh()
    End Sub
    Private Sub ToggleMaximized()
        Select Case WindowState
            Case FormWindowState.Normal, FormWindowState.Minimized
                WindowState = FormWindowState.Maximized
            Case FormWindowState.Maximized
                WindowState = FormWindowState.Normal
        End Select
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
        If App.Theme = App.Themes.Accent Then
            c = App.GetAccentColor()
            BackColor = c
            TxbxLibrarySearch.BackColor = c
        End If
        If Not AsTheme Then ResumeLayout()
        Debug.Print("Library Accent Color Set")
    End Sub
    Friend Sub SetTheme()
        Static forecolor As Color
        SuspendLayout()
        If App.Theme = App.Themes.Accent Then
            SetAccentColor(True)
            forecolor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            TxbxLibrarySearch.BackColor = App.CurrentTheme.BackColor
            forecolor = App.CurrentTheme.TextColor
        End If
        LVLibrary.BackColor = App.CurrentTheme.BackColor
        LVLibrary.ForeColor = App.CurrentTheme.TextColor
        LBXLibrarySearch.BackColor = App.CurrentTheme.BackColor
        LBXLibrarySearch.ForeColor = App.CurrentTheme.TextColor
        BtnSearchFolders.BackColor = App.CurrentTheme.ButtonBackColor
        BtnSearchFolders.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnGroupByAlbum.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnGroupByAlbum.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnGroupByArtist.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnGroupByArtist.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnGroupByGenre.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnGroupByGenre.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnGroupByYear.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnGroupByYear.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnGroupByNone.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnGroupByNone.ForeColor = App.CurrentTheme.ButtonTextColor
        If TxbxLibrarySearch.Text = LibrarySearchTitle Then TxbxLibrarySearch.ForeColor = App.CurrentTheme.InactiveSearchTextColor
        LblLibraryCounts.ForeColor = forecolor
        LblHistory.ForeColor = forecolor
        LblExtFileInfo.ForeColor = forecolor
        LblExtProperties.ForeColor = forecolor
        LblExtTitle.ForeColor = forecolor
        LblExtType.ForeColor = forecolor
        GrpBoxGroupBy.ForeColor = forecolor
        ResumeLayout()
        Debug.Print("Library Theme Set")
    End Sub

End Class
