
Public Class Directory

    ' Form Events
    Private Sub Directory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSources()
        SetStatusLabelEmptyText()
    End Sub

    ' Control Events
    Private Sub LVSources_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVSources.SelectedIndexChanged
        If LVSources.SelectedItems.Count = 0 Then Return

        Dim selectedSource As String = LVSources.SelectedItems(0).Text

        LVStations.Items.Clear()
        StatusLabel.Text = $"Loading {selectedSource}…"

        Select Case selectedSource
            Case "RadioBrowser"
                LoadRadioBrowserDefault()
            Case Else
                StatusLabel.Text = "Source not implemented yet."
        End Select
    End Sub
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
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
                SearchRadioBrowser(query)
            Case Else
                StatusLabel.Text = "Search not implemented for this source."
        End Select
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
    Private Async Sub LoadRadioBrowserDefault()
        ' Simulate loading delay
        Await Task.Delay(300)

        ' TODO: Replace with actual API call
        StatusLabel.Text = "Ready. Use search to find stations."
    End Sub
    Private Async Sub SearchRadioBrowser(query As String)
        Await Task.Delay(300)

        ' TODO: Replace with actual API call
        StatusLabel.Text = $"No results for '{query}'."
    End Sub
    Private Sub SetStatusLabelEmptyText()
        StatusLabel.Text = "No stations to display. Select a source from the left to begin."
    End Sub
End Class
