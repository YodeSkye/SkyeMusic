
Imports System.Net.Http
Imports Newtonsoft.Json.Linq

Public Class Directory

    ' Declarations
    Private radioBrowser As RadioBrowserSource

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
