
Imports System.Net.Http
Imports Newtonsoft.Json.Linq

Public Class Directory

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private radioBrowser As RadioBrowserSource
    Private sortColumn As Integer = -1
    Private sortOrder As SortOrder = SortOrder.Ascending

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
        radioBrowser = New RadioBrowserSource
        LoadSources()
        SetStatusLabelEmptyText()
        CMStations.Font = CurrentTheme.BaseFont
        CMIPlay.Font = New Font(CurrentTheme.BaseFont, FontStyle.Bold)
#If DEBUG Then
        If App.SaveWindowMetrics AndAlso App.DirectorySize.Height >= 0 Then Me.Size = App.DirectorySize
        If App.SaveWindowMetrics AndAlso App.DirectoryLocation.Y >= 0 Then Me.Location = App.DirectoryLocation
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
    Private Async Sub LVSources_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVSources.SelectedIndexChanged
        If LVSources.SelectedItems.Count = 0 Then Return

        Dim selectedSource As String = LVSources.SelectedItems(0).Text

        LVStations.Items.Clear()
        StatusLabel.Text = $"Loading {selectedSource}…"

        Select Case selectedSource
            Case "RadioBrowser"
                Dim results = Await radioBrowser.GetDefaultStationsAsync()
                PopulateStations(results)
                StatusLabel.Text = $"Loaded {results.Count} stations."
            Case Else
                StatusLabel.Text = "Source not implemented yet."
        End Select
    End Sub
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Search()
    End Sub
    Private Sub LVStations_DoubleClick(sender As Object, e As EventArgs) Handles LVStations.DoubleClick
        If LVStations.SelectedItems.Count = 0 Then Return
        Player.PlayFromDirectory(LVStations.SelectedItems(0).Tag.ToString)
    End Sub
    Private Sub CMIPlay_Click(sender As Object, e As EventArgs) Handles CMIPlay.Click
        If LVStations.SelectedItems.Count = 0 Then Return
        Player.PlayFromDirectory(LVStations.SelectedItems(0).Tag.ToString)
    End Sub
    Private Sub CMIAddToPlaylist_Click(sender As Object, e As EventArgs) Handles CMIAddToPlaylist.Click
        If LVStations.SelectedItems.Count = 0 Then Return
        Player.AddToPlaylistFromDirectory(LVStations.SelectedItems(0).Tag.ToString)
    End Sub
    Private Sub CMICopyStreamURL_Click(sender As Object, e As EventArgs) Handles CMICopyStreamURL.Click
        If LVStations.SelectedItems.Count = 0 Then Return
        Clipboard.SetText(LVStations.SelectedItems(0).Tag.ToString)
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

        LVSources.Items.Add(New ListViewItem("RadioBrowser"))
        ' Future:
        ' LVSources.Items.Add(New ListViewItem("Icecast"))
        ' LVSources.Items.Add(New ListViewItem("SHOUTcast"))
        ' LVSources.Items.Add(New ListViewItem("Live365"))
        ' LVSources.Items.Add(New ListViewItem("Favorites"))

        StatusLabel.Text = "Select a source to begin."
    End Sub
    Private Sub PopulateStations(list As List(Of StreamEntry))
        LVStations.BeginUpdate()
        LVStations.Items.Clear()

        ClearStationsSortState()

        For Each s In list
            Dim item As New ListViewItem(s.Name)
            item.SubItems.Add(s.Tags)
            item.SubItems.Add(s.Bitrate.ToString())
            item.SubItems.Add(s.Country)
            item.SubItems.Add(s.Status)
            item.SubItems.Add(s.Url)
            item.Tag = s.Url
            LVStations.Items.Add(item)
        Next

        LVStations.EndUpdate()
    End Sub
    Private Async Sub SearchRadioBrowser(query As String)
        Await Task.Delay(300)

        ' TODO: Replace with actual API call
        StatusLabel.Text = $"No results for '{query}'."
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
            Case "RadioBrowser"
                Dim results = Await radioBrowser.SearchAsync(query)
                PopulateStations(results)
                If results.Count = 0 Then
                    StatusLabel.Text = $"No results for '{query}'."
                Else
                    StatusLabel.Text = $"Found {results.Count} stations."
                End If
            Case Else
                StatusLabel.Text = "Search not implemented for this source."
        End Select
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
        Debug.Print("History Accent Color Set")
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
        Debug.Print("History Theme Set")
    End Sub
    Friend Sub ReThemeMenus()
        App.ThemeMenu(CMStations)
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor()
        SetTheme()
    End Sub

    ' ListView Sorter Class
    Public Class ListViewItemComparer
        Implements IComparer

        Private col As Integer
        Private order As SortOrder

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
        Public Property Url As String
        Public Property Tags As String
        Public Property Bitrate As Integer
        Public Property Country As String
        Public Property Status As String
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
                list.Add(New StreamEntry With {
                    .Name = item("name")?.ToString(),
                    .Url = item("url")?.ToString(),
                    .Tags = item("tags")?.ToString(),
                    .Bitrate = If(Integer.TryParse(item("bitrate")?.ToString(), Nothing), CInt(item("bitrate")), 0),
                    .Country = item("country")?.ToString(),
                    .Status = item("status")?.ToString()
                })
            Next

            Return list
        End Function

    End Class

End Class
