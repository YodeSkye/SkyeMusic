
Imports System.Net.Http
Imports Newtonsoft.Json.Linq

Public Class Directory

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private sortColumn As Integer = -1
    Private sortOrder As SortOrder = SortOrder.Ascending
    Private radioBrowser As RadioBrowserSource
    Private soma As SomaFMSource

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
        ILSources.Images.Add(My.Resources.ImageRadioBrowser96)
        ILSources.Images.Add(My.Resources.ImageSomaFM96)
        ILSources.Images.Add(My.Resources.ImageRadioParadise96)
        ILSources.Images.Add(My.Resources.ImageFavorites96)
        ILSources.Images.Add(My.Resources.ImageAdd96)
        ILSources.Images.Add(My.Resources.ImageImport96)
        radioBrowser = New RadioBrowserSource
        soma = New SomaFMSource
        LoadSources()
        SetStatusLabelEmptyText()
        CMStations.Font = CurrentTheme.BaseFont
        CMIPlay.Font = New Font(CurrentTheme.BaseFont, FontStyle.Bold)
#If DEBUG Then
        'If App.SaveWindowMetrics AndAlso App.DirectorySize.Height >= 0 Then Me.Size = App.DirectorySize
        'If App.SaveWindowMetrics AndAlso App.DirectoryLocation.Y >= 0 Then Me.Location = App.DirectoryLocation
#Else
        If App.SaveWindowMetrics AndAlso App.DirectorySize.Height >= 0 Then Me.Size = App.DirectorySize
        If App.SaveWindowMetrics AndAlso App.DirectoryLocation.Y >= 0 Then Me.Location = App.DirectoryLocation
#End If
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
            App.DirectoryLocation = Location
        End If
    End Sub
    Private Sub Directory_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, PanelSearch.MouseUp, StatusStripDirectory.MouseUp
        mMove = False
    End Sub
    Private Sub Directory_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.DirectoryLocation = Location
        End If
    End Sub
    Private Sub Directory_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.DirectorySize = Size
        End If
    End Sub

    ' Control Events
    Private Async Sub LVSources_MouseDown(sender As Object, e As MouseEventArgs) Handles LVSources.MouseDown
        ' Find the item under the mouse
        Dim info = LVSources.HitTest(e.Location)
        Dim item = info.Item
        If item Is Nothing Then Return

        ' Ensure it becomes selected (for visual feedback)
        item.Selected = True

        Dim selectedSource As String = item.Text

        LVStations.Items.Clear()
        StatusLabel.Text = $"Loading {selectedSource}…"

        Select Case selectedSource
            Case "Radio Browser"
                TxtBoxSearch.PlaceholderText = "< Top Stations >"
                Dim results = Await radioBrowser.GetDefaultStationsAsync()
                PopulateStations(results)
                StatusLabel.Text = $"Loaded {results.Count} stations of thousands."
            Case "SomaFM"
                TxtBoxSearch.PlaceholderText = "< All SomaFM Channels >"
                Dim results = Await soma.GetStationsAsync()
                PopulateStations(results)
                StatusLabel.Text = $"Loaded {results.Count} SomaFM channels."
            Case "Radio Paradise"
                TxtBoxSearch.PlaceholderText = "< All Radio Paradise Channels >"
                StatusLabel.Text = "Source not implemented yet."
            Case "Favorites"
                TxtBoxSearch.PlaceholderText = "< Your Favorite Stations >"
                StatusLabel.Text = "Source not implemented yet."
            Case "Add Stream To Playlist"
                TxtBoxSearch.PlaceholderText = String.Empty
                StatusLabel.Text = String.Empty
                Player.OpenURL()
            Case "Import Playlist"
                TxtBoxSearch.PlaceholderText = String.Empty
                StatusLabel.Text = String.Empty
                Player.OpenPlaylist()
            Case Else
                StatusLabel.Text = "Source not implemented yet."
        End Select
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

        Dim item = LVStations.SelectedItems(0)
        Dim url = Await GetPlayableURL(item)

        If String.IsNullOrWhiteSpace(url) Then
            StatusLabel.Text = "No playable streams found."
            Return
        End If

        Player.PlayFromDirectory(url)
    End Sub
    Private Async Sub CMIPlay_Click(sender As Object, e As EventArgs) Handles CMIPlay.Click
        If LVStations.SelectedItems.Count = 0 Then Return

        Dim item = LVStations.SelectedItems(0)
        Dim url = Await GetPlayableURL(item)

        If String.IsNullOrWhiteSpace(url) Then
            StatusLabel.Text = "No playable streams found."
            Return
        End If

        Player.PlayFromDirectory(url)
    End Sub
    Private Async Sub CMIAddToPlaylist_Click(sender As Object, e As EventArgs) Handles CMIAddToPlaylist.Click
        If LVStations.SelectedItems.Count = 0 Then Return

        Dim item = LVStations.SelectedItems(0)
        Dim url = Await GetPlayableURL(item)

        If String.IsNullOrWhiteSpace(url) Then
            StatusLabel.Text = "No playable streams found."
            Return
        End If

        Player.AddToPlaylistFromDirectory(url)
    End Sub
    Private Sub CMICopyStreamURL_Click(sender As Object, e As EventArgs) Handles CMICopyStreamURL.Click
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
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Search()
    End Sub
    Private Sub TxtBoxSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the 'ding' sound
            Search()
        End If
    End Sub
    Private Sub LVStations_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles LVStations.ColumnClick
        ' Remove arrow from previous sort column
        If sortColumn >= 0 AndAlso sortColumn < LVStations.Columns.Count Then
            Dim oldHeader = LVStations.Columns(sortColumn).Text
            LVStations.Columns(sortColumn).Text = oldHeader.Replace(" ▲", "").Replace(" ▼", "")
        End If

        ' Determine if clicked column is already the sort column
        If e.Column = sortColumn Then
            ' Reverse the current sort direction
            If sortOrder = SortOrder.Ascending Then
                sortOrder = SortOrder.Descending
            Else
                sortOrder = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending
            sortColumn = e.Column
            sortOrder = SortOrder.Ascending
        End If

        ' Add arrow to current sort column
        Dim currentHeader = LVStations.Columns(sortColumn).Text.Replace(" ▲", "").Replace(" ▼", "")
        If sortOrder = SortOrder.Ascending Then
            LVStations.Columns(sortColumn).Text = currentHeader & " ▲"
        Else
            LVStations.Columns(sortColumn).Text = currentHeader & " ▼"
        End If

        ' Set the ListViewItemSorter with the new sort options
        LVStations.ListViewItemSorter = New ListViewItemComparer(sortColumn, sortOrder)

        ' Call the sort method to manually sort
        LVStations.Sort()
    End Sub

    ' Methods
    Private Sub LoadSources()
        LVSources.Items.Clear()

        LVSources.Items.Add("Radio Browser", 0)
        LVSources.Items.Add("SomaFM", 1)
        LVSources.Items.Add("Radio Paradise", 2)
        LVSources.Items.Add("Favorites", 3)
        LVSources.Items.Add("Add Stream To Playlist", 4)
        LVSources.Items.Add("Import Playlist", 5)

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

            ElseIf s.PlaylistUrls IsNot Nothing AndAlso s.PlaylistUrls.Count > 0 Then
                ' Playlist with multiple streams
                displayUrl = "Playlist"
            Else
                displayUrl = "No Stream Available"
            End If

            item.SubItems.Add(displayUrl)

            ' More column (▾ only when multiple streams exist)
            Dim moreText As String = ""
            If s.PlaylistUrls IsNot Nothing AndAlso s.PlaylistUrls.Count > 1 Then
                moreText = "▾"
            End If
            item.SubItems.Add(moreText)

            item.Tag = s
            LVStations.Items.Add(item)
        Next

        AutoSizeColumn(LVStations.Columns("ColName"), 300)
        AutoSizeColumn(LVStations.Columns("ColTags"), 200)
        AutoSizeColumn(LVStations.Columns("ColCountry"))
        AutoSizeColumn(LVStations.Columns("ColStatus"))
        AutoSizeColumn(LVStations.Columns("ColURL"))

        LVStations.EndUpdate()
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
            Case "Add Stream To Playlist"
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
            Dim options = Await ExplodeAllPlaylists(entry)

            If options.Count = 0 Then
                Return Nothing
            End If

            ' Auto-select first stream
            entry.Url = options(0).Url
            item.SubItems(urlColIndex).Text = entry.Url

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
        Dim options = Await ExplodeAllPlaylists(entry)
        If options.Count = 0 Then Return

        Dim menu As New ContextMenuStrip With {
            .Font = CurrentTheme.SubBaseFont,
            .ShowImageMargin = False
        }
        App.ThemeMenu(menu)

        For Each opt In options
            Dim shortUrl = TruncateUrl(opt.Url, 50)
            Dim label = $"{opt.Bitrate} {opt.Format}   {shortUrl}"

            Dim mi As New ToolStripMenuItem(label)
            mi.Tag = opt.Url

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
    Private Sub AutoSizeColumn(col As ColumnHeader, Optional maxWidth As Integer = 0)
        LVStations.AutoResizeColumn(col.Index, ColumnHeaderAutoResizeStyle.ColumnContent)
        Dim contentWidth = col.Width

        LVStations.AutoResizeColumn(col.Index, ColumnHeaderAutoResizeStyle.HeaderSize)
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
        sortColumn = -1
        sortOrder = SortOrder.Ascending
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

        ResumeLayout()
        'Debug.Print("Directory Theme Set")
    End Sub
    Friend Sub ReThemeMenus()
        App.ThemeMenu(CMStations)
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor()
        SetTheme()
        ReThemeMenus()
    End Sub

    ' ListView Sorter Class
    Public Class ListViewItemComparer
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

    ' Source Classes
    Public Class StreamEntry
        Public Property Name As String
        Public Property Tags As String
        Public Property Format As String
        Public Property Bitrate As Integer
        Public Property Country As String
        Public Property Status As String
        Public Property PlaylistUrls As List(Of String)
        Public Property Url As String
    End Class
    Public Class StreamOption
        Public Property Url As String
        Public Property Bitrate As Integer
        Public Property Format As String
    End Class
    Public Class RadioBrowserSource

        Private ReadOnly client As HttpClient

        Public Sub New()
            client = New HttpClient With {.BaseAddress = New Uri("https://de1.api.radio-browser.info/json/")}
        End Sub

        Public Async Function SearchAsync(query As String) As Task(Of List(Of StreamEntry))
            Dim url = $"stations/search?name={Uri.EscapeDataString(query)}"
            Dim json = Await client.GetStringAsync(url)
            Return ParseStations(json)
        End Function
        Public Async Function GetDefaultStationsAsync() As Task(Of List(Of StreamEntry))
            Dim json = Await client.GetStringAsync("stations/topclick/24")
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
    Public Class SomaFMSource

        Private ReadOnly client As HttpClient

        Public Sub New()
            client = New HttpClient With {.BaseAddress = New Uri("https://api.somafm.com/")}
        End Sub

        Public Async Function GetStationsAsync() As Task(Of List(Of StreamEntry))
            Try
                Dim json = Await client.GetStringAsync("channels.json")
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

End Class
