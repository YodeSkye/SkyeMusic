
Imports System.Net.Http
Imports Newtonsoft.Json.Linq

Public Class Directory

    ' Declarations
    Private radioBrowser As RadioBrowserSource

    ' Form Events
    Private Sub Directory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        radioBrowser = New RadioBrowserSource
        LoadSources()
        SetStatusLabelEmptyText()
        CMStations.Font = CurrentTheme.BaseFont
        CMIPlay.Font = New Font(CurrentTheme.BaseFont, FontStyle.Bold)
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
    Private Async Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
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
    Private Sub CMIPlay_Click(sender As Object, e As EventArgs) Handles CMIPlay.Click

    End Sub
    Private Sub CMIAddToPlaylist_Click(sender As Object, e As EventArgs) Handles CMIAddToPlaylist.Click
        If LVStations.SelectedItems.Count = 0 Then Return
        Player.AddToPlaylistFromDirectory(LVStations.SelectedItems(0).Tag.ToString)
    End Sub
    Private Sub CMICopyStreamURL_Click(sender As Object, e As EventArgs) Handles CMICopyStreamURL.Click

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
    Private Sub SetStatusLabelEmptyText()
        StatusLabel.Text = "No stations to display. Select a source from the left to begin."
    End Sub

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
