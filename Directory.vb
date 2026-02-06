
Imports System.IO
Imports System.Net.Http
Imports NAudio.FileFormats
Imports NAudio.Utils
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Directory

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private sortColumnStations As Integer = -1
    Private sortOrderStations As SortOrder = SortOrder.Ascending
    Private sortColumnPodcasts As Integer = -1
    Private sortOrderPodcasts As SortOrder = SortOrder.Ascending
    Private sortColumnEpisodes As Integer = -1
    Private sortOrderEpisodes As SortOrder = SortOrder.Ascending
    Private suppressSelection As Boolean = False
    Private radioBrowser As RadioBrowserSource
    Private soma As SomaFMSource
    Private radioParadise As RadioParadiseSource
    Private Favorites As List(Of FavoriteEntry)

    ' Form Events
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
    Private Sub Directory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetAccentColor()
        SetTheme()
        ReThemeMenus()

        ' Assign stable names to columns
        If LVStations.Columns.Count >= 8 Then
            LVStations.Columns(0).Name = "ColName"
            LVStations.Columns(1).Name = "ColTags"
            LVStations.Columns(2).Name = "ColFormat"
            LVStations.Columns(3).Name = "ColBitrate"
            LVStations.Columns(4).Name = "ColCountry"
            LVStations.Columns(5).Name = "ColStatus"
            LVStations.Columns(6).Name = "ColURL"
            LVStations.Columns(7).Name = "ColMore"
        End If
        If LVPodcasts.Columns.Count >= 5 Then
            LVPodcasts.Columns(0).Name = "ColPodcastsArtwork"
            LVPodcasts.Columns(1).Name = "ColPodcastsTitle"
            LVPodcasts.Columns(2).Name = "ColPodcastsAuthor"
            LVPodcasts.Columns(3).Name = "ColPodcastsGenre"
            LVPodcasts.Columns(4).Name = "ColPodcastsURL"
        End If
        If LVEpisodes.Columns.Count >= 5 Then
            LVEpisodes.Columns(0).Name = "ColEpisodesTitle"
            LVEpisodes.Columns(1).Name = "ColEpisodesDuration"
            LVEpisodes.Columns(2).Name = "ColEpisodesReleaseDate"
            LVEpisodes.Columns(3).Name = "ColEpisodesDescription"
            LVEpisodes.Columns(4).Name = "ColEpisodesURL"
        End If
        ILSources.Images.Add(My.Resources.ImageRadioBrowser96)
        ILSources.Images.Add(My.Resources.ImageSomaFM96)
        ILSources.Images.Add(My.Resources.ImageRadioParadise96)
        ILSources.Images.Add(My.Resources.ImageApplePodcasts96)
        ILSources.Images.Add(My.Resources.ImageFavorites96)
        ILSources.Images.Add(My.Resources.ImageAdd96)
        ILSources.Images.Add(My.Resources.ImageImport96)

        radioBrowser = New RadioBrowserSource
        soma = New SomaFMSource
        radioParadise = New RadioParadiseSource
        LoadSources()

        SetStatusLabelEmptyText()
        CMStations.Font = CurrentTheme.BaseFont
        CMPodcasts.Font = CurrentTheme.BaseFont
        CMEpisodes.Font = CurrentTheme.BaseFont
        CMIStreamPlay.Font = New Font(CurrentTheme.BaseFont, FontStyle.Bold)
        CMIEpisodePlay.Font = New Font(CurrentTheme.BaseFont, FontStyle.Bold)

#If DEBUG Then
        'If App.SaveWindowMetrics AndAlso App.DirectorySize.Height >= 0 Then Me.Size = App.DirectorySize
        'If App.SaveWindowMetrics AndAlso App.DirectoryLocation.Y >= 0 Then Me.Location = App.DirectoryLocation
#Else
        If App.Settings.SaveWindowMetrics AndAlso App.Settings.DirectorySize.Height >= 0 Then Me.Size = App.Settings.DirectorySize
        If App.Settings.SaveWindowMetrics AndAlso App.Settings.DirectoryLocation.Y >= 0 Then Me.Location = App.Settings.DirectoryLocation
#End If
        If App.DirectoryLastSelectedSource >= 0 Then
            LVSources.Items(App.DirectoryLastSelectedSource).Selected = True
        End If

    End Sub
    Private Sub Directory_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If LVSources.SelectedItems.Count > 0 Then
            App.DirectoryLastSelectedSource = LVSources.SelectedItems(0).Index
            LVSources.Focus()
        Else
            App.DirectoryLastSelectedSource = -1
        End If
    End Sub
    Private Sub Directory_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, PanelSearch.MouseDown, StatusStripDirectory.MouseDown
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
    Private Sub Directory_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, PanelSearch.MouseMove, StatusStripDirectory.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
            App.Settings.DirectoryLocation = Location
        End If
    End Sub
    Private Sub Directory_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, PanelSearch.MouseUp, StatusStripDirectory.MouseUp
        mMove = False
    End Sub
    Private Sub Directory_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.Settings.DirectoryLocation = Location
        End If
    End Sub
    Private Sub Directory_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.Settings.DirectorySize = Size
        End If
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        ' Enter = Play
        If keyData = Keys.Enter Then
            If LVStations.Focused AndAlso LVStations.SelectedItems.Count > 0 Then
                CMIStreamPlay.PerformClick()
                Return True
            ElseIf TxtBoxSearch.Focused Then
                Search()
                Return True
            End If
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    ' Control Events
    Private Sub LVSources_MouseDown(sender As Object, e As MouseEventArgs) Handles LVSources.MouseDown
        ' Find the item under the mouse
        suppressSelection = True
        Dim info = LVSources.HitTest(e.Location)
        Dim item = info.Item
        If item Is Nothing Then Return

        ' Ensure it becomes selected (for visual feedback)

        item.Selected = True
        Dim selectedSource As String = item.Text

        SetSearch(selectedSource)
        suppressSelection = False
    End Sub
    Private Sub LVSources_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVSources.SelectedIndexChanged
        If suppressSelection OrElse LVSources.SelectedItems.Count = 0 Then Return
        Dim selectedSource As String = LVSources.SelectedItems(0).Text
        If selectedSource = "Add Stream To Playlist" OrElse selectedSource = "Import Playlist" Then Return
        SetSearch(LVSources.SelectedItems(0).Text)
    End Sub
    Private Sub LVStations_MouseDown(sender As Object, e As MouseEventArgs) Handles LVStations.MouseDown
        If e.Button = MouseButtons.Right Then Return ' Handled by ContextMenu
        Dim info = LVStations.HitTest(e.Location)
        If info.Item Is Nothing Then Return
        Dim clickedColumn = info.Item.SubItems.IndexOf(info.SubItem)

        If LVStations.Columns(clickedColumn).Name = "ColMore" Then ShowStreamMenu(info.Item, e.Location)

    End Sub
    Private Async Sub LVStations_DoubleClick(sender As Object, e As EventArgs) Handles LVStations.DoubleClick
        If LVStations.SelectedItems.Count = 0 Then Return

        Dim entry As StreamEntry = CType(LVStations.SelectedItems(0).Tag, StreamEntry)

        ' NEW: Handle podcast favorites
        If entry.Format = "PodcastFeed" Then
            Await OpenPodcastFavorite(entry)
            Return
        End If

        ' Existing radio logic
        Dim item = LVStations.SelectedItems(0)
        Dim title As String = item.Text
        Dim url = Await GetPlayableURL(item)

        If String.IsNullOrWhiteSpace(url) Then
            StatusLabel.Text = "No playable streams found."
            Return
        End If

        Player.PlayFromDirectory(title, url)
    End Sub
    Private Sub LVStations_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles LVStations.ColumnClick
        ' Remove arrow from previous sort column
        If sortColumnStations >= 0 AndAlso sortColumnStations < LVStations.Columns.Count Then
            Dim oldHeader = LVStations.Columns(sortColumnStations).Text
            LVStations.Columns(sortColumnStations).Text = oldHeader.Replace(" ▲", "").Replace(" ▼", "")
        End If

        ' Determine if clicked column is already the sort column
        If e.Column = sortColumnStations Then
            ' Reverse the current sort direction
            If sortOrderStations = SortOrder.Ascending Then
                sortOrderStations = SortOrder.Descending
            Else
                sortOrderStations = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending
            sortColumnStations = e.Column
            sortOrderStations = SortOrder.Ascending
        End If

        ' Add arrow to current sort column
        Dim currentHeader = LVStations.Columns(sortColumnStations).Text.Replace(" ▲", "").Replace(" ▼", "")
        If sortOrderStations = SortOrder.Ascending Then
            LVStations.Columns(sortColumnStations).Text = currentHeader & " ▲"
        Else
            LVStations.Columns(sortColumnStations).Text = currentHeader & " ▼"
        End If

        ' Set the ListViewItemSorter with the new sort options
        LVStations.ListViewItemSorter = New ListViewItemComparer(sortColumnStations, sortOrderStations)

        ' Call the sort method to manually sort
        LVStations.Sort()
    End Sub
    Private Async Sub LVPodcasts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVPodcasts.SelectedIndexChanged
        If LVPodcasts.SelectedItems.Count = 0 Then Return

        StatusLabel.Text = "Loading podcast episodes…"
        Dim feedUrl = CStr(LVPodcasts.SelectedItems(0).Tag)
        Await LoadPodcastEpisodes(feedUrl)
    End Sub
    Private Sub LVPodcasts_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles LVPodcasts.ColumnClick
        ' Remove arrow from previous sort column
        If sortColumnPodcasts >= 0 AndAlso sortColumnPodcasts < LVPodcasts.Columns.Count Then
            Dim oldHeader = LVPodcasts.Columns(sortColumnPodcasts).Text
            LVPodcasts.Columns(sortColumnPodcasts).Text = oldHeader.Replace(" ▲", "").Replace(" ▼", "")
        End If

        ' Determine if clicked column is already the sort column
        If e.Column = sortColumnPodcasts Then
            ' Reverse the current sort direction
            If sortOrderPodcasts = SortOrder.Ascending Then
                sortOrderPodcasts = SortOrder.Descending
            Else
                sortOrderPodcasts = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending
            sortColumnPodcasts = e.Column
            sortOrderPodcasts = SortOrder.Ascending
        End If

        ' Add arrow to current sort column
        Dim currentHeader = LVPodcasts.Columns(sortColumnPodcasts).Text.Replace(" ▲", "").Replace(" ▼", "")
        If sortOrderPodcasts = SortOrder.Ascending Then
            LVPodcasts.Columns(sortColumnPodcasts).Text = currentHeader & " ▲"
        Else
            LVPodcasts.Columns(sortColumnPodcasts).Text = currentHeader & " ▼"
        End If

        ' Set the ListViewItemSorter with the new sort options
        LVPodcasts.ListViewItemSorter = New ListViewItemComparer(sortColumnPodcasts, sortOrderPodcasts)

        ' Call the sort method to manually sort
        LVPodcasts.Sort()
    End Sub
    Private Sub LVEpisodes_DoubleClick(sender As Object, e As EventArgs) Handles LVEpisodes.DoubleClick
        If LVEpisodes.SelectedItems.Count = 0 Then Return
        Dim item = LVEpisodes.SelectedItems(0)
        Dim title = item.Text
        Dim url = CStr(item.Tag)
        Player.PlayFromDirectory(title, url)
    End Sub
    Private Sub LVEpisodes_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles LVEpisodes.ColumnClick
        ' Remove arrow from previous sort column
        If sortColumnEpisodes >= 0 AndAlso sortColumnEpisodes < LVEpisodes.Columns.Count Then
            Dim oldHeader = LVEpisodes.Columns(sortColumnEpisodes).Text
            LVEpisodes.Columns(sortColumnEpisodes).Text = oldHeader.Replace(" ▲", "").Replace(" ▼", "")
        End If

        ' Determine if clicked column is already the sort column
        If e.Column = sortColumnEpisodes Then
            ' Reverse the current sort direction
            If sortOrderEpisodes = SortOrder.Ascending Then
                sortOrderEpisodes = SortOrder.Descending
            Else
                sortOrderEpisodes = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending
            sortColumnEpisodes = e.Column
            sortOrderEpisodes = SortOrder.Ascending
        End If

        ' Add arrow to current sort column
        Dim currentHeader = LVEpisodes.Columns(sortColumnEpisodes).Text.Replace(" ▲", "").Replace(" ▼", "")
        If sortOrderEpisodes = SortOrder.Ascending Then
            LVEpisodes.Columns(sortColumnEpisodes).Text = currentHeader & " ▲"
        Else
            LVEpisodes.Columns(sortColumnEpisodes).Text = currentHeader & " ▼"
        End If

        ' Set the ListViewItemSorter with the new sort options
        LVEpisodes.ListViewItemSorter = New ListViewItemComparer(sortColumnEpisodes, sortOrderEpisodes)

        ' Call the sort method to manually sort
        LVEpisodes.Sort()
    End Sub
    Private Sub CMStations_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMStations.Opening
        If LVStations.SelectedItems.Count = 0 Then
            e.Cancel = True
            Return
        End If

        Dim item = LVStations.SelectedItems(0)
        Dim url = CType(item.Tag, StreamEntry).Url
        If IsURLFavorited(url) Then
            CMIStreamRemoveFromFavorites.Visible = True
            CMIStreamAddToFavorites.Visible = False
        Else
            CMIStreamRemoveFromFavorites.Visible = False
            CMIStreamAddToFavorites.Visible = True
        End If

    End Sub
    Private Async Sub CMIPlay_Click(sender As Object, e As EventArgs) Handles CMIStreamPlay.Click
        If LVStations.SelectedItems.Count = 0 Then Return
        Dim item = LVStations.SelectedItems(0)
        Dim title As String = LVStations.SelectedItems(0).Text
        Dim url = Await GetPlayableURL(item)

        If String.IsNullOrWhiteSpace(url) Then
            StatusLabel.Text = "No playable streams found."
            Return
        End If

        Player.PlayFromDirectory(title, url)
    End Sub
    Private Async Sub CMIAddToPlaylist_Click(sender As Object, e As EventArgs) Handles CMIStreamAddToPlaylist.Click
        If LVStations.SelectedItems.Count = 0 Then Return
        Dim item = LVStations.SelectedItems(0)
        Dim title As String = LVStations.SelectedItems(0).Text
        Dim url = Await GetPlayableURL(item)

        If String.IsNullOrWhiteSpace(url) Then
            StatusLabel.Text = "No playable streams found."
            Return
        End If

        Player.AddToPlaylistFromDirectory(title, url)
    End Sub
    Private Async Sub CMIAddToFavorites_Click(sender As Object, e As EventArgs) Handles CMIStreamAddToFavorites.Click
        If LVStations.SelectedItems.Count = 0 Then Exit Sub

        Dim entry As StreamEntry = CType(LVStations.SelectedItems(0).Tag, StreamEntry)
        Dim sourceName As String = LVSources.SelectedItems(0).Text
        Dim item As ListViewItem = LVStations.SelectedItems(0)
        Dim url = Await GetPlayableURL(item)

        Dim fav As New FavoriteEntry With {
            .Name = entry.Name,
            .Url = url,
            .Format = entry.Format,
            .Bitrate = entry.Bitrate,
            .Source = sourceName
        }

        Dim added = AddFavorite(fav)
        If added Then
            StatusLabel.Text = "Added to Favorites."
        Else
            StatusLabel.Text = "This stream is already in Favorites."
        End If

    End Sub
    Private Sub CMIRemoveFromFavorites_Click(sender As Object, e As EventArgs) Handles CMIStreamRemoveFromFavorites.Click
        If LVStations.SelectedItems.Count = 0 Then Exit Sub

        Dim removedCount As Integer = 0

        ' Collect URLs first so we don't modify the ListView while iterating
        Dim urls As New List(Of String)
        For Each item As ListViewItem In LVStations.SelectedItems
            urls.Add(item.SubItems(6).Text)
        Next

        ' Remove each favorite
        For Each url In urls
            If RemoveFavorite(url) Then
                removedCount += 1
            End If
        Next

        ' Status message
        If removedCount = 0 Then
            StatusLabel.Text = "No favorites were removed."
        ElseIf removedCount = 1 Then
            StatusLabel.Text = "Removed 1 favorite."
        Else
            StatusLabel.Text = $"Removed {removedCount} favorites."
        End If

        ' Refresh Favorites view
        SetSearch("Favorites")
    End Sub
    Private Sub CMICopyStreamURL_Click(sender As Object, e As EventArgs) Handles CMIStreamCopyStreamURL.Click
        If LVStations.SelectedItems.Count = 0 Then Return
        Dim entry As StreamEntry = CType(LVStations.SelectedItems(0).Tag, StreamEntry)
        Dim urlToCopy As String = Nothing

        If Not String.IsNullOrWhiteSpace(entry.Url) Then
            urlToCopy = entry.Url
        ElseIf entry.PlaylistUrls IsNot Nothing AndAlso entry.PlaylistUrls.Count > 0 Then
            urlToCopy = entry.PlaylistUrls(0)
        End If

        If String.IsNullOrWhiteSpace(urlToCopy) Then
            StatusLabel.Text = "This station has no stream URL to copy."
            Return
        End If
        Clipboard.SetText(urlToCopy)
        StatusLabel.Text = "Stream URL copied."
    End Sub
    Private Sub CMPodcasts_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMPodcasts.Opening
        If LVPodcasts.SelectedItems.Count = 0 Then
            e.Cancel = True
            Return
        End If

        Dim item = LVPodcasts.SelectedItems(0)
        Dim url = CStr(item.Tag)
        If IsURLFavorited(url) Then
            CMIPodcastsRemoveFromFavorites.Visible = True
            CMIPodcastsAddToFavorites.Visible = False
        Else
            CMIPodcastsRemoveFromFavorites.Visible = False
            CMIPodcastsAddToFavorites.Visible = True
        End If

    End Sub
    Private Sub CMIPodcastsAddToFavorites_Click(sender As Object, e As EventArgs) Handles CMIPodcastsAddToFavorites.Click
        AddPodcastToFavorites()
    End Sub
    Private Sub CMIPodcastsRemoveFromFavorites_Click(sender As Object, e As EventArgs) Handles CMIPodcastsRemoveFromFavorites.Click
        If LVPodcasts.SelectedItems.Count = 0 Then Exit Sub

        Dim url As String = LVPodcasts.SelectedItems(0).SubItems(4).Text
        Dim removed = RemoveFavorite(url)

        If removed Then
            StatusLabel.Text = "Removed from Favorites."
        Else
            StatusLabel.Text = "Favorite not removed."
        End If
    End Sub
    Private Sub CMEpisodes_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMEpisodes.Opening
        If LVEpisodes.SelectedItems.Count = 0 Then
            e.Cancel = True
            Return
        End If

        Dim item = LVEpisodes.SelectedItems(0)
        Dim url = CStr(item.Tag)
        If IsURLFavorited(url) Then
            CMIEpisodeRemoveFromFavorites.Visible = True
            CMIEpisodeAddToFavorites.Visible = False
        Else
            CMIEpisodeRemoveFromFavorites.Visible = False
            CMIEpisodeAddToFavorites.Visible = True
        End If

    End Sub
    Private Sub CMIEpisodePlay_Click(sender As Object, e As EventArgs) Handles CMIEpisodePlay.Click
        If LVEpisodes.SelectedItems.Count = 0 Then Return
        Dim item = LVEpisodes.SelectedItems(0)
        Dim title = item.Text
        Dim url = CStr(item.Tag)
        Player.PlayFromDirectory(title, url)
    End Sub
    Private Sub CMIEpisodeAddToPlaylist_Click(sender As Object, e As EventArgs) Handles CMIEpisodeAddToPlaylist.Click
        If LVEpisodes.SelectedItems.Count = 0 Then Return
        Dim item = LVEpisodes.SelectedItems(0)
        Dim title = item.Text
        Dim url = CStr(item.Tag)
        Player.AddToPlaylistFromDirectory(title, url)
    End Sub
    Private Async Sub CMIEpisodeDownload_Click(sender As Object, e As EventArgs) Handles CMIEpisodeDownload.Click
        If LVEpisodes.SelectedItems.Count = 0 Then Return

        Dim item = LVEpisodes.SelectedItems(0)
        Dim title = item.Text
        Dim url = CStr(item.Tag)

        ' Get podcast name from LVPodcasts selection
        Dim podcastName As String = "Podcast"
        If LVPodcasts.SelectedItems.Count > 0 Then
            podcastName = LVPodcasts.SelectedItems(0).SubItems(1).Text
        End If

        StatusLabel.Text = "Downloading episode…"

        Dim savedPath = Await DownloadEpisodeAsync(podcastName, title, url)

        If savedPath Is Nothing Then
            StatusLabel.Text = "Episode Download failed."
        Else
            StatusLabel.Text = $"Episode Saved to: {savedPath}"
        End If
    End Sub
    Private Sub CMIEpisodeAddToFavorites_Click(sender As Object, e As EventArgs) Handles CMIEpisodeAddToFavorites.Click
        AddEpisodeToFavorites()
    End Sub
    Private Sub CMIEpisodeRemoveFromFavorites_Click(sender As Object, e As EventArgs) Handles CMIEpisodeRemoveFromFavorites.Click
        If LVEpisodes.SelectedItems.Count = 0 Then Exit Sub

        Dim url As String = LVEpisodes.SelectedItems(0).SubItems(4).Text
        Dim removed = RemoveFavorite(url)

        If removed Then
            StatusLabel.Text = "Removed from Favorites."
        Else
            StatusLabel.Text = "Favorite not removed."
        End If
    End Sub
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Search()
    End Sub

    ' Methods
    Private Sub LoadSources()
        LVSources.Items.Clear()

        LVSources.Items.Add("Radio Browser", 0)
        LVSources.Items.Add("SomaFM", 1)
        LVSources.Items.Add("Radio Paradise", 2)
        LVSources.Items.Add("Apple Podcasts", 3)
        LVSources.Items.Add("Favorites", 4)
        LVSources.Items.Add("Add Stream To Playlist", 5)
        LVSources.Items.Add("Import Playlist", 6)

        StatusLabel.Text = "Select a source to begin."
    End Sub
    Private Sub PopulateStations(list As List(Of StreamEntry))
        LVStations.BeginUpdate()
        LVStations.Items.Clear()

        ClearStationsSortState()

        For Each s In list

            Dim item As New ListViewItem(s.Name)
            item.SubItems.Add(s.Tags)
            item.SubItems.Add(s.Format)
            item.SubItems.Add(s.Bitrate.ToString())
            item.SubItems.Add(s.Country)
            item.SubItems.Add(s.Status)

            ' Decide what to show in the URL column
            Dim displayUrl As String

            If Not String.IsNullOrWhiteSpace(s.Url) Then
                ' Direct stream OR resolved playlist
                displayUrl = s.Url
            ElseIf (s.PlaylistUrls IsNot Nothing AndAlso s.PlaylistUrls.Count > 1) OrElse (s.PlaylistOptions IsNot Nothing AndAlso s.PlaylistOptions.Count > 1) Then
                ' Playlist with multiple streams
                displayUrl = "Playlist"
            Else
                displayUrl = "No Stream Available"
            End If

            item.SubItems.Add(displayUrl)

            ' More column (▾ only when multiple streams exist)
            Dim moreText As String = ""
            If (s.PlaylistUrls IsNot Nothing AndAlso s.PlaylistUrls.Count > 1) OrElse (s.PlaylistOptions IsNot Nothing AndAlso s.PlaylistOptions.Count > 1) Then
                moreText = "▾"
            End If
            item.SubItems.Add(moreText)

            item.Tag = s
            LVStations.Items.Add(item)
        Next

        AutoSizeStationsColumn(LVStations.Columns("ColName"), 300)
        AutoSizeStationsColumn(LVStations.Columns("ColTags"), 200)
        AutoSizeStationsColumn(LVStations.Columns("ColCountry"))
        AutoSizeStationsColumn(LVStations.Columns("ColStatus"))
        AutoSizeStationsColumn(LVStations.Columns("ColURL"))

        LVStations.EndUpdate()
    End Sub
    Private Async Function PopulatePodcasts(results As JArray) As Task
        LVPodcasts.BeginUpdate()
        LVPodcasts.Items.Clear()
        ILPodcasts.Images.Clear()

        Dim index As Integer = 0

        For Each p In results
            Dim title = CStr(p("collectionName"))
            Dim author = CStr(p("artistName"))
            Dim genre = If(p("primaryGenreName")?.ToString(), "")
            Dim feedUrl = CStr(p("feedUrl"))
            Dim artworkUrl = CStr(p("artworkUrl100"))

            ' Download artwork
            Dim img As Image = Nothing
            Try
                Dim bytes = Await App.Http.GetByteArrayAsync(artworkUrl)
                Using ms As New MemoryStream(bytes)
                    img = Image.FromStream(ms)
                End Using
            Catch
            End Try

            If img IsNot Nothing Then
                Dim resized = ResizeImage(img, ILPodcasts.ImageSize)
                ILPodcasts.Images.Add(resized)
            Else
                ILPodcasts.Images.Add(My.Resources.ImageApplePodcasts96)
            End If

            Dim item As New ListViewItem("")
            item.ImageIndex = index
            item.SubItems.Add(title)
            item.SubItems.Add(author)
            item.SubItems.Add(genre)
            item.SubItems.Add(feedUrl)
            item.Tag = feedUrl

            LVPodcasts.Items.Add(item)
            index += 1
        Next

        For Each col As ColumnHeader In LVPodcasts.Columns
            Debug.WriteLine($"Podcast Column: {col.Name}")
        Next
        AutoSizePodcastsColumn(LVPodcasts.Columns("ColPodcastsTitle"), 800)
        AutoSizePodcastsColumn(LVPodcasts.Columns("ColPodcastsAuthor"), 400)
        AutoSizePodcastsColumn(LVPodcasts.Columns("ColPodcastsGenre"))
        AutoSizePodcastsColumn(LVPodcasts.Columns("ColPodcastsURL"))

        LVPodcasts.EndUpdate()

        Return
    End Function
    Private Async Sub SetSearch(source As String)
        LVStations.Items.Clear()
        LVStations.MultiSelect = False
        LVPodcasts.Items.Clear()
        LVEpisodes.Items.Clear()
        StatusLabel.Text = $"Loading {source}…"
        TxtBoxSearch.Text = String.Empty
        Select Case source
            Case "Radio Browser"
                TxtBoxSearch.PlaceholderText = "< Top Stations >"
                SetPanels(source)
                Dim results = Await radioBrowser.GetDefaultStationsAsync()
                PopulateStations(results)
                StatusLabel.Text = $"Loaded {results.Count} stations of thousands."
            Case "SomaFM"
                TxtBoxSearch.PlaceholderText = "< All SomaFM Channels >"
                SetPanels(source)
                Dim results = Await soma.GetStationsAsync()
                PopulateStations(results)
                StatusLabel.Text = $"Loaded {results.Count} SomaFM channels."
            Case "Radio Paradise"
                TxtBoxSearch.PlaceholderText = "< All Radio Paradise Channels >"
                SetPanels(source)
                Dim results = radioParadise.GetStations()
                PopulateStations(results)
                StatusLabel.Text = $"Loaded {results.Count} Radio Paradise channels."
            Case "Apple Podcasts"
                TxtBoxSearch.PlaceholderText = "< Apple Podcasts >"
                SetPanels(source)
                StatusLabel.Text = $"Search for Apple Podcasts."
            Case "Favorites"
                TxtBoxSearch.PlaceholderText = "< Your Favorites >"

                ' Load favorites as StreamEntry objects
                Dim favs = GetFavoritesAsStreamEntries()

                SetPanels(source)
                LVStations.MultiSelect = True
                PopulateStations(favs)
                StatusLabel.Text = $"Loaded {Favorites.Count} favorite stations."
            Case "Add Stream To Playlist"
                TxtBoxSearch.PlaceholderText = String.Empty
                SetPanels(source)
                StatusLabel.Text = "Select a source to begin."
                Player.OpenURL()
            Case "Import Playlist"
                TxtBoxSearch.PlaceholderText = String.Empty
                SetPanels(source)
                StatusLabel.Text = "Select a source to begin."
                Player.OpenPlaylist()
            Case Else
                TxtBoxSearch.PlaceholderText = String.Empty
                SetPanels(source)
                StatusLabel.Text = "Source not implemented yet."
        End Select
    End Sub
    Private Async Sub Search()
        Dim query As String = TxtBoxSearch.Text.Trim()

        If String.IsNullOrWhiteSpace(query) Then
            StatusLabel.Text = "Enter a search term."
            Return
        End If

        If LVSources.SelectedItems.Count = 0 Then
            StatusLabel.Text = "Select a source first."
            Return
        End If

        Dim selectedSource As String = LVSources.SelectedItems(0).Text
        StatusLabel.Text = $"Searching {selectedSource} for '{query}'…"

        LVStations.Items.Clear()

        Select Case selectedSource
            Case "Radio Browser"
                Dim results = Await radioBrowser.SearchAsync(query)
                PopulateStations(results)
                If results.Count = 0 Then
                    StatusLabel.Text = $"No results for '{query}'."
                Else
                    StatusLabel.Text = $"Found {results.Count} stations."
                End If
            Case "SomaFM"
                ' Load all channels
                Dim all = Await soma.GetStationsAsync()

                ' Local filtering
                Dim results = all.Where(Function(s)
                                            Return (s.Name IsNot Nothing AndAlso
                                            s.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) _
                                            OrElse (s.Tags IsNot Nothing AndAlso
                                            s.Tags.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                                        End Function).ToList()

                PopulateStations(results)

                If results.Count = 0 Then
                    StatusLabel.Text = $"No SomaFM results for '{query}'."
                Else
                    StatusLabel.Text = $"Found {results.Count} SomaFM channels."
                End If
            Case "Radio Paradise"
                ' Load all channels
                Dim all = radioParadise.GetStations

                ' Local filtering
                Dim results = all.Where(Function(s)
                                            Return (s.Name IsNot Nothing AndAlso
                                            s.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) _
                                            OrElse (s.Tags IsNot Nothing AndAlso
                                            s.Tags.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                                        End Function).ToList()

                PopulateStations(results)

                If results.Count = 0 Then
                    StatusLabel.Text = $"No Radio Paradise results for '{query}'."
                Else
                    StatusLabel.Text = $"Found {results.Count} Radio Paradise channels."
                End If
            Case "Apple Podcasts"
                Dim results = Await SearchApplePodcasts(query)
                Await PopulatePodcasts(results)
                StatusLabel.Text = $"Found {results.Count} podcasts."
            Case "Favorites"
                ' Load all channels
                Dim all = GetFavoritesAsStreamEntries()

                ' Local filtering
                Dim results = all.Where(Function(s)
                                            Return (s.Name IsNot Nothing AndAlso
                                            s.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) _
                                            OrElse (s.Tags IsNot Nothing AndAlso
                                            s.Tags.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                                        End Function).ToList()

                PopulateStations(results)

                If results.Count = 0 Then
                    StatusLabel.Text = $"No Favorites matching '{query}'."
                Else
                    StatusLabel.Text = $"Found {results.Count} Favorites."
                End If
            Case "Add Stream To Playlist", "Import Playlist"
                StatusLabel.Text = "Not Searchable."
            Case Else
                StatusLabel.Text = "Search not implemented for this source."
        End Select
    End Sub
    Private Async Function GetPlayableURL(item As ListViewItem) As Task(Of String)
        Dim entry As StreamEntry = CType(item.Tag, StreamEntry)

        ' If already resolved, return it
        If Not String.IsNullOrWhiteSpace(entry.Url) AndAlso item.SubItems(6).Text <> "Playlist" Then
            Return entry.Url
        End If

        ' If URL column says Playlist, explode now
        Dim urlColIndex = LVStations.Columns("ColURL").Index
        If item.SubItems(urlColIndex).Text = "Playlist" Then
            Dim options As List(Of StreamOption)
            If entry.PlaylistOptions IsNot Nothing AndAlso entry.PlaylistOptions.Count > 0 Then
                options = entry.PlaylistOptions
            Else
                options = Await ExplodeAllPlaylists(entry)
            End If
            If options.Count = 0 Then
                Return Nothing
            End If

            ' Auto-select first stream
            entry.Url = options(0).Url
            item.SubItems(urlColIndex).Text = entry.Url

            ' Set Format and Bitrate
            entry.Format = options(0).Format
            entry.Bitrate = options(0).Bitrate
            Dim formatCol = LVStations.Columns("ColFormat").Index
            Dim bitrateCol = LVStations.Columns("ColBitrate").Index
            item.SubItems(formatCol).Text = entry.Format
            item.SubItems(bitrateCol).Text = entry.Bitrate.ToString()

            Return entry.Url
        End If

        ' Fallback
        Return entry.Url
    End Function
    Private Async Function ExplodeAllPlaylists(entry As StreamEntry) As Task(Of List(Of StreamOption))
        Dim results As New List(Of StreamOption)

        ' If RadioBrowser gives a direct URL, handle that too
        If entry.PlaylistUrls Is Nothing OrElse entry.PlaylistUrls.Count = 0 Then
            ' Single direct stream
            results.Add(New StreamOption With {
            .Url = entry.Url,
            .Bitrate = entry.Bitrate,
            .Format = entry.Format
        })
            Return results
        End If

        ' Handle playlist URLs (SomaFM or RadioBrowser)
        For Each playlistUrl In entry.PlaylistUrls
            Try
                Dim text = Await App.Http.GetStringAsync(playlistUrl)
                Dim urls = ExtractUrlsFromPlaylistText(text)

                For Each u In urls
                    results.Add(New StreamOption With {
                    .Url = u,
                    .Bitrate = ExtractBitrateFromUrl(u),
                    .Format = ExtractFormatFromUrl(u)
                })
                Next

            Catch ex As Exception
                App.WriteToLog("Playlist explode failed: " & ex.ToString())
            End Try
        Next

        Return results
    End Function
    Private Function ExtractBitrateFromUrl(url As String) As Integer
        Dim m = System.Text.RegularExpressions.Regex.Match(url, "(\d{2,3})")
        If m.Success Then
            Return CInt(m.Value)
        End If
        Return 0
    End Function
    Private Function ExtractFormatFromUrl(url As String) As String
        Dim u = url.ToLower()

        If u.Contains("aacp") Then Return "AAC+"
        If u.Contains("aac") Then Return "AAC"
        If u.Contains("mp3") Then Return "MP3"
        If u.Contains("ogg") OrElse u.Contains("opus") Then Return "OGG"
        If u.Contains("m3u8") Then Return "HLS"

        Return "Unknown"
    End Function
    Private Function ExtractUrlsFromPlaylistText(text As String) As List(Of String)
        Dim urls As New List(Of String)

        For Each rawLine In text.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
            Dim line = rawLine.Trim()

            ' Skip comments (#EXTM3U, #EXTINF, etc.)
            If line.StartsWith("#") Then Continue For

            ' --- .pls format: File1=URL ---
            If line.StartsWith("File", StringComparison.OrdinalIgnoreCase) Then
                Dim parts = line.Split("="c)
                If parts.Length = 2 AndAlso parts(1).Trim().StartsWith("http", StringComparison.OrdinalIgnoreCase) Then
                    urls.Add(parts(1).Trim())
                End If
                Continue For
            End If

            ' --- .m3u / .m3u8 format: raw URL lines ---
            If line.StartsWith("http", StringComparison.OrdinalIgnoreCase) Then
                urls.Add(line)
                Continue For
            End If

            ' Some .m3u files have URLs after whitespace or metadata
            If line.Contains("http://") OrElse line.Contains("https://") Then
                Dim idx = line.IndexOf("http", StringComparison.OrdinalIgnoreCase)
                If idx >= 0 Then
                    urls.Add(line.Substring(idx).Trim())
                End If
            End If
        Next

        Return urls
    End Function
    Private Async Sub ShowStreamMenu(item As ListViewItem, clickPoint As Point)
        Dim entry As StreamEntry = CType(item.Tag, StreamEntry)

        ' Explode playlists
        Dim options As List(Of StreamOption)
        If entry.PlaylistOptions Is Nothing Then
            options = Await ExplodeAllPlaylists(entry)
        Else
            options = entry.PlaylistOptions
        End If
        If options.Count <= 1 Then Return

        Dim menu As New ContextMenuStrip With {
            .Font = CurrentTheme.SubBaseFont,
            .ShowImageMargin = False
        }
        App.ThemeMenu(menu)

        For Each opt In options
            Dim shortUrl = TruncateUrl(opt.Url, 50)
            Dim label = $"{opt.Bitrate} {opt.Format}   {shortUrl}"

            Dim mi As New ToolStripMenuItem(label) With {.Tag = opt.Url}

            AddHandler mi.Click,
            Sub()
                entry.Url = opt.Url

                Dim urlCol = LVStations.Columns("ColURL").Index
                item.SubItems(urlCol).Text = entry.Url
            End Sub

            menu.Items.Add(mi)
        Next

        menu.Show(LVStations, clickPoint)
    End Sub
    Private Function TruncateUrl(url As String, maxLength As Integer) As String
        If url.Length <= maxLength Then Return url

        Dim keep = (maxLength - 5) \ 2
        Dim startPart = url.Substring(0, keep)
        Dim endPart = url.Substring(url.Length - keep)

        Return $"{startPart}...{endPart}"
    End Function
    Private Function ResizeImage(img As Image, size As Size) As Image
        Dim bmp As New Bitmap(size.Width, size.Height)
        Using g = Graphics.FromImage(bmp)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.DrawImage(img, 0, 0, size.Width, size.Height)
        End Using
        Return bmp
    End Function
    Private Function IsURLFavorited(url As String) As Boolean
        If Favorites Is Nothing Then LoadFavorites()
        Return Favorites.Any(Function(f) f.Url = url)
    End Function
    Private Sub SetPanels(source As String)
        Select Case source
            Case "Radio Browser"
                PanelStreams.Enabled = True
                PanelStreams.Visible = True
                PanelPodcasts.Enabled = False
                PanelStreams.BringToFront()
            Case "SomaFM"
                PanelStreams.Enabled = True
                PanelStreams.Visible = True
                PanelPodcasts.Enabled = False
                PanelStreams.BringToFront()
            Case "Radio Paradise"
                PanelStreams.Enabled = True
                PanelStreams.Visible = True
                PanelPodcasts.Enabled = False
                PanelStreams.BringToFront()
            Case "Apple Podcasts"
                PanelStreams.Enabled = False
                PanelPodcasts.Enabled = True
                PanelPodcasts.Visible = True
                PanelPodcasts.BringToFront()
            Case "Favorites"
                PanelStreams.Enabled = True
                PanelStreams.Visible = True
                PanelPodcasts.Enabled = False
                PanelStreams.BringToFront()
            Case "Add Stream To Playlist"
                PanelStreams.Visible = False
                PanelStreams.Enabled = False
                PanelPodcasts.Visible = False
                PanelPodcasts.Enabled = False
            Case "Import Playlist"
                PanelStreams.Visible = False
                PanelStreams.Enabled = False
                PanelPodcasts.Visible = False
                PanelPodcasts.Enabled = False
            Case Else
                PanelStreams.Visible = False
                PanelStreams.Enabled = False
                PanelPodcasts.Visible = False
                PanelPodcasts.Enabled = False
        End Select
    End Sub
    Private Sub AutoSizeStationsColumn(col As ColumnHeader, Optional maxWidth As Integer = 0)
        LVStations.AutoResizeColumn(col.Index, ColumnHeaderAutoResizeStyle.ColumnContent)
        Dim contentWidth = col.Width

        LVStations.AutoResizeColumn(col.Index, ColumnHeaderAutoResizeStyle.HeaderSize)
        Dim headerWidth = col.Width

        Dim finalWidth = Math.Max(contentWidth, headerWidth)
        If maxWidth > 0 Then finalWidth = Math.Min(maxWidth, finalWidth)

        col.Width = finalWidth
    End Sub
    Private Sub AutoSizePodcastsColumn(col As ColumnHeader, Optional maxWidth As Integer = 0)
        LVPodcasts.AutoResizeColumn(col.Index, ColumnHeaderAutoResizeStyle.ColumnContent)
        Dim contentWidth = col.Width

        LVPodcasts.AutoResizeColumn(col.Index, ColumnHeaderAutoResizeStyle.HeaderSize)
        Dim headerWidth = col.Width

        Dim finalWidth = Math.Max(contentWidth, headerWidth)
        If maxWidth > 0 Then finalWidth = Math.Min(maxWidth, finalWidth)

        col.Width = finalWidth
    End Sub
    Private Sub AutoSizeEpisodesColumn(col As ColumnHeader, Optional maxWidth As Integer = 0)
        LVEpisodes.AutoResizeColumn(col.Index, ColumnHeaderAutoResizeStyle.ColumnContent)
        Dim contentWidth = col.Width

        LVEpisodes.AutoResizeColumn(col.Index, ColumnHeaderAutoResizeStyle.HeaderSize)
        Dim headerWidth = col.Width

        Dim finalWidth = Math.Max(contentWidth, headerWidth)
        If maxWidth > 0 Then finalWidth = Math.Min(maxWidth, finalWidth)

        col.Width = finalWidth
    End Sub
    Private Sub SetStatusLabelEmptyText()
        StatusLabel.Text = "No stations to display. Select a source from the left to begin."
    End Sub
    Private Sub ClearStationsSortState()
        ' Clear sort arrows from all columns
        For i As Integer = 0 To LVStations.Columns.Count - 1
            Dim header = LVStations.Columns(i).Text
            LVStations.Columns(i).Text = header.Replace(" ▲", "").Replace(" ▼", "")
        Next

        ' Reset sort state
        sortColumnStations = -1
        sortOrderStations = SortOrder.Ascending
        LVStations.ListViewItemSorter = Nothing
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsNormalWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsNormalWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsNormalWindow
        If location.Y < App.AdjustScreenBoundsNormalWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
    Private Sub SetAccentColor()
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            BackColor = c
            StatusStripDirectory.BackColor = c
        End If
        ResumeLayout()
        'Debug.Print("Directory Accent Color Set")
    End Sub
    Private Sub SetTheme()
        SuspendLayout()

        If App.CurrentTheme.IsAccent Then
            StatusStripDirectory.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            StatusStripDirectory.BackColor = App.CurrentTheme.BackColor
            StatusStripDirectory.ForeColor = App.CurrentTheme.TextColor
        End If
        TxtBoxSearch.BackColor = App.CurrentTheme.BackColor
        TxtBoxSearch.ForeColor = App.CurrentTheme.TextColor
        BtnSearch.BackColor = App.CurrentTheme.ButtonBackColor
        BtnSearch.ForeColor = App.CurrentTheme.ButtonTextColor
        LVSources.BackColor = App.CurrentTheme.BackColor
        LVSources.ForeColor = App.CurrentTheme.TextColor
        LVStations.BackColor = App.CurrentTheme.BackColor
        LVStations.ForeColor = App.CurrentTheme.TextColor
        LVPodcasts.BackColor = App.CurrentTheme.BackColor
        LVPodcasts.ForeColor = App.CurrentTheme.TextColor
        LVEpisodes.BackColor = App.CurrentTheme.BackColor
        LVEpisodes.ForeColor = App.CurrentTheme.TextColor

        ResumeLayout()
        'Debug.Print("Directory Theme Set")
    End Sub
    Friend Sub ReThemeMenus()
        App.ThemeMenu(CMStations)
        App.ThemeMenu(CMPodcasts)
        App.ThemeMenu(CMEpisodes)
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor()
        SetTheme()
        ReThemeMenus()
    End Sub

    ' Favorites
    Private Sub LoadFavorites()
        Try
            If IO.File.Exists(App.DirectoryFavoritesPath) Then
                Dim json = IO.File.ReadAllText(App.DirectoryFavoritesPath)
                Favorites = JsonConvert.DeserializeObject(Of List(Of FavoriteEntry))(json)
            Else
                Favorites = New List(Of FavoriteEntry)
            End If
        Catch
            Favorites = New List(Of FavoriteEntry)
            WriteToLog("Failed to load Directory Favorites.")
        End Try

        ' --- AUTOCLEAN ---
        If Favorites Is Nothing Then
            Favorites = New List(Of FavoriteEntry)
            Exit Sub
        End If
        ' Remove nulls and entries with no URL
        Favorites = Favorites.
        Where(Function(f) f IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(f.Url)).ToList()
        ' Remove duplicates by URL
        Favorites = Favorites.GroupBy(Function(f) f.Url.ToLower()).Select(Function(g) g.First()).ToList()
        ' Save cleaned list if anything changed
        SaveFavorites()

    End Sub
    Private Sub SaveFavorites()
        Try
            Dim json = JsonConvert.SerializeObject(Favorites, Formatting.Indented)
            IO.File.WriteAllText(DirectoryFavoritesPath, json)
        Catch
            WriteToLog("Failed to save Directory Favorites.")
        End Try
    End Sub
    Private Function AddFavorite(fav As FavoriteEntry) As Boolean
        If Favorites Is Nothing Then LoadFavorites()

        ' Deduplicate by URL
        If Favorites.Any(Function(f)
                             Return f IsNot Nothing AndAlso
                            f.Url IsNot Nothing AndAlso
                            fav.Url IsNot Nothing AndAlso
                            f.Url.Equals(fav.Url, StringComparison.OrdinalIgnoreCase)
                         End Function) Then
            Return False   ' Already exists
        End If

        Favorites.Add(fav)
        SaveFavorites()
        Return True
    End Function
    Private Sub AddPodcastToFavorites()
        If LVPodcasts.SelectedItems.Count = 0 Then Exit Sub

        Dim item = LVPodcasts.SelectedItems(0)
        Dim title = item.SubItems(1).Text
        Dim feedUrl = CStr(item.Tag)

        Dim fav As New FavoriteEntry With {
        .Name = title,
        .Url = feedUrl,
        .Format = "PodcastFeed",
        .Bitrate = 0,
        .Source = "Apple Podcasts"
    }

        Dim added = AddFavorite(fav)
        StatusLabel.Text = If(added, "Podcast added to Favorites.", "Already in Favorites.")
    End Sub
    Private Sub AddEpisodeToFavorites()
        If LVEpisodes.SelectedItems.Count = 0 Then Exit Sub

        Dim item = LVEpisodes.SelectedItems(0)
        Dim title = item.Text
        Dim url = CStr(item.Tag)

        Dim fav As New FavoriteEntry With {
            .Name = title,
            .Url = url,
            .Format = "Podcast",
            .Bitrate = 0,
            .Source = "Apple Podcasts"
        }

        Dim added = AddFavorite(fav)
        StatusLabel.Text = If(added, "Episode added to Favorites.", "Already in Favorites.")
    End Sub
    Private Function RemoveFavorite(url As String) As Boolean
        If Favorites Is Nothing Then LoadFavorites()

        Dim removed = Favorites.RemoveAll(
            Function(f)
                Return f IsNot Nothing AndAlso
                       f.Url IsNot Nothing AndAlso
                       f.Url.Equals(url, StringComparison.OrdinalIgnoreCase)
            End Function)
        If removed > 0 Then
            SaveFavorites()
            Return True
        End If

        Return False
    End Function
    Private Function GetFavoritesAsStreamEntries() As List(Of StreamEntry)
        Dim list As New List(Of StreamEntry)

        If Favorites Is Nothing Then LoadFavorites()

        For Each fav In Favorites

            ' Detect podcast feed favorites
            If fav.Source = "Apple Podcasts" AndAlso fav.Format = "PodcastFeed" Then

                ' Podcast feed favorite
                Dim p As New StreamEntry With {
                .Name = fav.Name,
                .Url = fav.Url,                 ' This is the RSS feed URL
                .Format = "PodcastFeed",        ' Tells Directory this is a podcast
                .Bitrate = 0,
                .Tags = "Podcast",              ' Display tag
                .Country = "",
                .Status = "Favorite"
            }

                list.Add(p)
                Continue For
            End If

            ' Normal radio favorite
            Dim s As New StreamEntry With {
            .Name = fav.Name,
            .Url = fav.Url,
            .Format = fav.Format,
            .Bitrate = fav.Bitrate,
            .Tags = fav.Source,                ' Source label
            .Country = "",
            .Status = "Favorite"
        }

            list.Add(s)
        Next

        Return list
    End Function
    Private Async Function OpenPodcastFavorite(entry As StreamEntry) As Task
        ' Switch UI to podcast mode
        SetPanels("Apple Podcasts")
        PanelPodcasts.Visible = True
        PanelStreams.Visible = False

        ' Clear lists
        LVPodcasts.Items.Clear()
        LVEpisodes.Items.Clear()

        ' Add a single podcast entry to LVPodcasts
        Dim item As New ListViewItem("")
        item.SubItems.Add(entry.Name)
        item.SubItems.Add("Favorite Podcast")
        item.SubItems.Add("Podcast")
        item.SubItems.Add(entry.Url)
        item.Tag = entry.Url

        LVPodcasts.Items.Add(item)

        ' Auto-select it
        item.Selected = True

        ' Load episodes
        StatusLabel.Text = "Loading podcast episodes…"
        Await LoadPodcastEpisodes(entry.Url)
    End Function

    ' ListView Sorter Class
    Private Class ListViewItemComparer
        Implements IComparer

        Private ReadOnly col As Integer
        Private ReadOnly order As SortOrder

        Public Sub New(column As Integer, sortOrder As SortOrder)
            col = column
            order = sortOrder
        End Sub

        Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
            Dim itemX As ListViewItem = CType(x, ListViewItem)
            Dim itemY As ListViewItem = CType(y, ListViewItem)

            Dim compareResult As Integer

            ' Handle numeric comparison for Bitrate column (index 2)
            If col = 2 Then
                Dim numX, numY As Integer
                If Integer.TryParse(itemX.SubItems(col).Text, numX) AndAlso Integer.TryParse(itemY.SubItems(col).Text, numY) Then
                    compareResult = numX.CompareTo(numY)
                Else
                    compareResult = String.Compare(itemX.SubItems(col).Text, itemY.SubItems(col).Text)
                End If
            Else
                ' String comparison for all other columns
                compareResult = String.Compare(itemX.SubItems(col).Text, itemY.SubItems(col).Text)
            End If

            ' Apply sort order
            If order = SortOrder.Descending Then
                compareResult = -compareResult
            End If

            Return compareResult
        End Function
    End Class

    ' Stream Source Classes
    Private Class StreamEntry
        Public Property Name As String
        Public Property Tags As String
        Public Property Format As String
        Public Property Bitrate As Integer
        Public Property Country As String
        Public Property Status As String
        Public Property PlaylistUrls As List(Of String)
        Public Property PlaylistOptions As List(Of StreamOption)
        Public Property Url As String
    End Class
    Private Class StreamOption
        Public Property Url As String
        Public Property Bitrate As Integer
        Public Property Format As String
    End Class
    Private Class FavoriteEntry
        Public Property Name As String
        Public Property Url As String
        Public Property Format As String
        Public Property Bitrate As Integer
        Public Property Source As String   ' "RadioBrowser", "SomaFM", "Radio Paradise", etc.
    End Class
    Private Class RadioBrowserSource

        Public Async Function SearchAsync(query As String) As Task(Of List(Of StreamEntry))
            Dim url = $"https://de1.api.radio-browser.info/json/stations/search?name={Uri.EscapeDataString(query)}"
            Dim json = Await App.Http.GetStringAsync(url)
            Return ParseStations(json)
        End Function
        Public Async Function GetDefaultStationsAsync() As Task(Of List(Of StreamEntry))
            Dim json = Await App.Http.GetStringAsync("https://de1.api.radio-browser.info/json/stations/topclick/24")
            Return ParseStations(json)
        End Function
        Private Function ParseStations(json As String) As List(Of StreamEntry)
            Dim arr = JArray.Parse(json)
            Dim list As New List(Of StreamEntry)

            For Each item In arr

                Dim entry As New StreamEntry With {
                    .Name = item("name")?.ToString(),
                    .Tags = item("tags")?.ToString(),
                    .Bitrate = If(Integer.TryParse(item("bitrate")?.ToString(), Nothing), CInt(item("bitrate")), 0),
                    .Country = item("country")?.ToString(),
                    .Status = item("status")?.ToString(),
                    .PlaylistUrls = New List(Of String)
                }

                ' Prefer url_resolved
                Dim rawUrl As String = item("url_resolved")?.ToString()
                If String.IsNullOrWhiteSpace(rawUrl) Then
                    rawUrl = item("url")?.ToString()
                End If

                ' Try urlcache as fallback
                If String.IsNullOrWhiteSpace(rawUrl) Then
                    rawUrl = item("urlcache")?.ToString()
                End If

                ' If still blank, skip station
                If String.IsNullOrWhiteSpace(rawUrl) Then
                    Continue For
                End If

                ' Detect playlist URLs
                If rawUrl.EndsWith(".pls", StringComparison.OrdinalIgnoreCase) OrElse rawUrl.EndsWith(".m3u", StringComparison.OrdinalIgnoreCase) Then
                    entry.PlaylistUrls.Add(rawUrl)
                    entry.Url = Nothing
                Else
                    entry.Url = rawUrl
                End If

                list.Add(entry)
            Next

            Return list
        End Function

    End Class
    Private Class SomaFMSource

        Public Async Function GetStationsAsync() As Task(Of List(Of StreamEntry))
            Try
                Dim json = Await App.Http.GetStringAsync("https://api.somafm.com/channels.json")
                Return ParseStations(json)
            Catch ex As Exception
                App.WriteToLog("SomaFM Error: " & ex.ToString())
                Return New List(Of StreamEntry)
            End Try
        End Function
        Private Function ParseStations(json As String) As List(Of StreamEntry)
            Dim root = JObject.Parse(json)
            Dim arr = root("channels")
            Dim list As New List(Of StreamEntry)

            For Each item In arr
                Dim title As String = item("title")?.ToString()
                Dim genre As String = item("genre")?.ToString()

                Dim playlists = item("playlists")
                If playlists Is Nothing OrElse playlists.Count = 0 Then Continue For

                ' Create ONE entry per station
                Dim entry As New StreamEntry With {
                    .Name = title,
                    .Tags = genre,
                    .Bitrate = 0,
                    .Country = "USA",
                    .Status = "OK",
                    .Format = "Multiple",
                    .PlaylistUrls = New List(Of String)
                }

                ' Add all playlist URLs to the entry
                For Each p In playlists
                    Dim url As String = p("url")?.ToString()
                    If Not String.IsNullOrWhiteSpace(url) Then
                        entry.PlaylistUrls.Add(url)
                    End If
                Next

                list.Add(entry)
            Next

            Return list
        End Function

    End Class
    Private Class RadioParadiseSource

        Public Function GetStations() As List(Of Directory.StreamEntry)
            Dim list As New List(Of Directory.StreamEntry) From {
                New Directory.StreamEntry With {
                    .Name = "Radio Paradise – Main Mix",
                    .Tags = "Radio Paradise",
                    .Format = "Multiple",
                    .Country = "USA",
                    .Status = "OK",
                    .Url = Nothing,
                    .PlaylistOptions = New List(Of Directory.StreamOption) From {
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/aac-320", .Format = "AAC", .Bitrate = 320},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/mp3-192", .Format = "MP3", .Bitrate = 192},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/ogg-192m", .Format = "OGG", .Bitrate = 192},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/flacm (Not Supported Yet)", .Format = "FLAC", .Bitrate = 0}
                    }
                },
                New Directory.StreamEntry With {
                    .Name = "Radio Paradise – Mellow Mix",
                    .Tags = "Radio Paradise",
                    .Format = "Multiple",
                    .Country = "USA",
                    .Status = "OK",
                    .Url = Nothing,
                    .PlaylistOptions = New List(Of Directory.StreamOption) From {
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/mellow-320", .Format = "AAC", .Bitrate = 320},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/mellow-192", .Format = "MP3", .Bitrate = 192},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/mellow-flacm (Not Supported Yet)", .Format = "FLAC", .Bitrate = 0}
                    }
                },
                New Directory.StreamEntry With {
                    .Name = "Radio Paradise – Rock Mix",
                    .Tags = "Radio Paradise",
                    .Format = "Multiple",
                    .Country = "USA",
                    .Status = "OK",
                    .Url = Nothing,
                    .PlaylistOptions = New List(Of Directory.StreamOption) From {
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/rock-320", .Format = "AAC", .Bitrate = 320},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/rock-192", .Format = "MP3", .Bitrate = 192},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/rock-flacm (Not Supported Yet)", .Format = "FLAC", .Bitrate = 0}
                    }
                },
                New Directory.StreamEntry With {
                    .Name = "Radio Paradise – Global Mix",
                    .Tags = "Radio Paradise",
                    .Format = "Multiple",
                    .Country = "USA",
                    .Status = "OK",
                    .Url = Nothing,
                    .PlaylistOptions = New List(Of Directory.StreamOption) From {
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/global-320", .Format = "AAC", .Bitrate = 320},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/global-192", .Format = "MP3", .Bitrate = 192},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/global-flacm (Not Supported Yet)", .Format = "FLAC", .Bitrate = 0}
                    }
                },
                 New Directory.StreamEntry With {
                    .Name = "Radio Paradise – Beyond...",
                    .Tags = "Radio Paradise",
                    .Format = "Multiple",
                    .Country = "USA",
                    .Status = "OK",
                    .Url = Nothing,
                    .PlaylistOptions = New List(Of Directory.StreamOption) From {
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/beyond-320", .Format = "AAC", .Bitrate = 320},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/beyond-192", .Format = "MP3", .Bitrate = 192},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/beyond-flacm (Not Supported Yet)", .Format = "FLAC", .Bitrate = 0}
                    }
                },
                 New Directory.StreamEntry With {
                    .Name = "Radio Paradise – Serenity",
                    .Tags = "Radio Paradise",
                    .Format = "AAC",
                    .Bitrate = 64,
                    .Country = "USA",
                    .Status = "OK",
                    .Url = "http://stream.radioparadise.com/serenity"
                },
                New Directory.StreamEntry With {
                    .Name = "Radio Paradise – Radio 2050",
                    .Tags = "Radio Paradise",
                    .Format = "Multiple",
                    .Country = "USA",
                    .Status = "OK",
                    .Url = Nothing,
                    .PlaylistOptions = New List(Of Directory.StreamOption) From {
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/radio2050-320", .Format = "AAC", .Bitrate = 320},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/radio2050-192", .Format = "MP3", .Bitrate = 192},
                        New Directory.StreamOption With {.Url = "http://stream.radioparadise.com/radio2050-flacm (Not Supported Yet)", .Format = "FLAC", .Bitrate = 0}
                    }
                }
            }
            Return list
        End Function

    End Class

    ' Podcasts
    Private Async Function SearchApplePodcasts(query As String) As Task(Of JArray)
        Dim url = $"https://itunes.apple.com/search?media=podcast&term={Uri.EscapeDataString(query)}"
        Dim json = Await App.Http.GetStringAsync(url)
        Dim obj = JObject.Parse(json)
        Return CType(obj("results"), JArray)
    End Function
    Private Async Function LoadPodcastEpisodes(feedUrl As String) As Task
        Try
            Dim nsItunes As XNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd"
            Dim xml = Await App.Http.GetStringAsync(feedUrl)
            Dim doc As XDocument = XDocument.Parse(xml)

            Dim items = doc...<item>

            LVEpisodes.BeginUpdate()
            LVEpisodes.Items.Clear()

            For Each ep In items
                Dim title = ep.<title>.Value

                ' Parse pubDate safely and format using system settings
                Dim pubDateRaw = ep.<pubDate>.Value
                Dim pubDate As String = ""
                Dim dt As Date

                If Date.TryParse(pubDateRaw, dt) Then
                    pubDate = dt.ToShortDateString()   ' ← system format
                End If

                Dim desc = ep.<description>.Value
                Dim duration = ep.Element(nsItunes + "duration")?.Value
                Dim enclosure = ep.<enclosure>.@url

                Dim item As New ListViewItem(title)
                item.SubItems.Add(duration)
                item.SubItems.Add(pubDate)
                item.SubItems.Add(desc)
                item.SubItems.Add(enclosure)
                item.Tag = enclosure

                LVEpisodes.Items.Add(item)
            Next

            AutoSizeEpisodesColumn(LVEpisodes.Columns("ColEpisodesTitle"), 800)
            AutoSizeEpisodesColumn(LVEpisodes.Columns("ColEpisodesDuration"))
            AutoSizeEpisodesColumn(LVEpisodes.Columns("ColEpisodesReleaseDate"))
            AutoSizeEpisodesColumn(LVEpisodes.Columns("ColEpisodesDescription"), 800)
            AutoSizeEpisodesColumn(LVEpisodes.Columns("ColEpisodesURL"))

            LVEpisodes.EndUpdate()
            StatusLabel.Text = $"Loaded {LVEpisodes.Items.Count} episodes."

        Catch ex As Exception
            StatusLabel.Text = "Failed to load podcast feed."
        End Try
    End Function
    Private Async Function DownloadEpisodeAsync(podcastName As String, episodeTitle As String, url As String) As Task(Of String)
        Try
            ' Sanitize names
            Dim safePodcast = String.Join("_", podcastName.Split(Path.GetInvalidFileNameChars()))
            Dim safeTitle = String.Join("_", episodeTitle.Split(Path.GetInvalidFileNameChars()))

            ' Strip query string
            Dim cleanUrl = url
            Dim qIndex = cleanUrl.IndexOf("?"c)
            If qIndex >= 0 Then cleanUrl = cleanUrl.Substring(0, qIndex)

            ' Determine extension
            Dim ext = Path.GetExtension(cleanUrl)
            If String.IsNullOrWhiteSpace(ext) Then ext = ".mp3"

            ' Build folder + file path
            Dim folder = Path.Combine(DownloadPath, "Podcasts", safePodcast)
            IO.Directory.CreateDirectory(folder)

            Dim filePath = Path.Combine(folder, safeTitle & ext)

            ' Prepare progress bar
            StatusProgressBar.Value = 0
            StatusProgressBar.Visible = True

            ' Stream download
            Using response = Await App.Http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead)
                response.EnsureSuccessStatusCode()

                Dim totalBytes = response.Content.Headers.ContentLength.GetValueOrDefault(-1)
                Dim canReportProgress = totalBytes > 0

                Using input = Await response.Content.ReadAsStreamAsync(),
                  output = File.Create(filePath)

                    Dim buffer(8191) As Byte
                    Dim bytesRead As Integer
                    Dim totalRead As Long = 0

                    Do
                        bytesRead = Await input.ReadAsync(buffer, 0, buffer.Length)
                        If bytesRead = 0 Then Exit Do

                        Await output.WriteAsync(buffer, 0, bytesRead)
                        totalRead += bytesRead

                        If canReportProgress Then
                            Dim pct = CInt((totalRead * 100L) / totalBytes)
                            StatusProgressBar.Value = Math.Min(100, pct)
                        End If
                    Loop
                End Using
            End Using

            StatusProgressBar.Visible = False
            Return filePath

        Catch ex As Exception
            StatusProgressBar.Visible = False
            App.WriteToLog("Podcast Episode Download Error:" & vbCrLf & ex.ToString())
            Return Nothing
        End Try
    End Function

End Class
