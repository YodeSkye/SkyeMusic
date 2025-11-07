
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms.DataVisualization.Charting
Imports WordCloudSharp

Public Class History

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private PanelLoading As New Panel()
    Private PBLoading As New Skye.UI.ProgressEX
    Private Enum HistoryView
        MostPlayed
        MostPlayedArtists
        RecentlyPlayed
        Favorites
    End Enum
    Private CurrentView As HistoryView = HistoryView.MostPlayed
    Private Enum ChartView
        Genres
        GenrePolar
        GenrePareto
        Artists
        ArtistWordCloud
    End Enum
    Private CurrentChartView As ChartView = ChartView.Genres
    Private CurrentViewMaxRecords As Integer = CInt(App.HistoryViewMaxRecords)
    Private views As List(Of App.SongView)
    Public Class MostPlayedArtistsReport
        Public Property Artist As String
        Public Property PlayCount As Integer
        Public Property LastPlayed As Date
    End Class

    'Form Events
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
    Private Sub History_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title + " History & Statistics"
        SetAccentColor()
        SetTheme()
#If DEBUG Then
        'If App.SaveWindowMetrics AndAlso App.HistoryLocation.Y >= 0 Then Me.Location = App.HistoryLocation
        'If App.SaveWindowMetrics AndAlso App.HistorySize.Height >= 0 Then Me.Size = App.HistorySize
#Else
        If App.SaveWindowMetrics AndAlso App.HistoryLocation.Y >= 0 Then Me.Location = App.HistoryLocation
        If App.SaveWindowMetrics AndAlso App.HistorySize.Height >= 0 Then Me.Size = App.HistorySize
#End If
        PanelLoading.BackColor = App.CurrentTheme.BackColor
        PanelLoading.Parent = Me
        PanelLoading.Dock = DockStyle.Fill
        PanelLoading.BringToFront()
        PBLoading.Minimum = 0
        PBLoading.Maximum = App.History.Count
        PBLoading.Value = 0
        PBLoading.DrawingColor = App.CurrentTheme.BackColor
        PBLoading.ForeColor = App.CurrentTheme.TextColor
        PBLoading.Parent = PanelLoading
        PBLoading.Left = (PanelLoading.ClientSize.Width - PBLoading.Width) \ 2
        PBLoading.Top = (PanelLoading.ClientSize.Height - PBLoading.Height) \ 2
    End Sub
    Private Sub History_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Text &= " - LOADING..."
        Enabled = False
        Application.DoEvents()

        views = GetData()
        PutData()
        PutViewData()

        Select Case CurrentView
            Case HistoryView.MostPlayed
                RadBtnMostPlayed.Checked = True
            Case HistoryView.RecentlyPlayed
                RadBtnRecentlyPlayed.Checked = True
            Case HistoryView.Favorites
                RadBtnFavorites.Checked = True
        End Select
        Select Case App.HistoryViewMaxRecords
            Case 0
                TxtBoxMaxRecords.Text = "All"
            Case Else
                TxtBoxMaxRecords.Text = App.HistoryViewMaxRecords.ToString
        End Select
        Select Case CurrentChartView
            Case ChartView.Genres
                RadBtnGenres.Checked = True
            Case ChartView.GenrePolar
                RadBtnGenrePolar.Checked = True
            Case ChartView.Artists
                RadBtnArtists.Checked = True
        End Select
        SetShowAll()
        LVHistory.BringToFront()

        BtnOK.Focus()

        Text = Text.TrimEnd(" - LOADING...".ToCharArray)
        PanelLoading.Visible = False
        Enabled = True
    End Sub
    Private Sub History_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If CurrentViewMaxRecords <> App.HistoryViewMaxRecords Then
            App.HistoryViewMaxRecords = CUShort(CurrentViewMaxRecords)
            App.SaveOptions()
        End If
        App.FRMHistory.Dispose()
        App.FRMHistory = Nothing
    End Sub
    Private Sub History_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, LblMostPlayedSong.MouseDown, LblSessionPlayedSongs.MouseDown, LblTotalDuration.MouseDown, LblTotalPlayedSongs.MouseDown, GrpBoxHistory.MouseDown
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
    Private Sub History_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, LblMostPlayedSong.MouseMove, LblSessionPlayedSongs.MouseMove, LblTotalDuration.MouseMove, LblTotalPlayedSongs.MouseMove, GrpBoxHistory.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub History_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, LblMostPlayedSong.MouseUp, LblSessionPlayedSongs.MouseUp, LblTotalDuration.MouseUp, LblTotalPlayedSongs.MouseUp, GrpBoxHistory.MouseUp
        mMove = False
    End Sub
    Private Sub History_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.HistoryLocation = Location
        End If
    End Sub
    Private Sub History_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.HistorySize = Me.Size
        End If
    End Sub
    Private Sub History_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub

    'Control Events
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub
    Private Sub BtnShowAll_Click(sender As Object, e As EventArgs) Handles BtnShowAll.Click
        CurrentViewMaxRecords = 0
        TxtBoxMaxRecords.Text = "All"
        SetShowAll()
        PutViewData()
    End Sub
    Private Sub BtnCharts_Click(sender As Object, e As EventArgs) Handles BtnCharts.Click
        PanelCharts.BringToFront()
        GrpBoxCharts.Visible = True
        GrpBoxCharts.BringToFront()
        GrpBoxHistory.Visible = False
        LblHistoryViewCount.Visible = False
        PutChartData()
    End Sub
    Private Sub BtnLists_Click(sender As Object, e As EventArgs) Handles BtnLists.Click
        PanelCharts.SendToBack()
        GrpBoxCharts.Visible = False
        GrpBoxHistory.Visible = True
        GrpBoxHistory.BringToFront()
        ShowCounts()
    End Sub
    Private Sub BtnQueueAll_Click(sender As Object, e As EventArgs) Handles BtnQueueAll.Click
        Queue(True)
    End Sub
    Private Sub BtnAddAllToPlaylist_Click(sender As Object, e As EventArgs) Handles BtnAddAllToPlaylist.Click
        AddToPlaylist(True)
    End Sub
    Private Sub CMHistoryView_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMHistoryView.Opening
        Select Case CurrentView
            Case HistoryView.MostPlayed, HistoryView.RecentlyPlayed, HistoryView.Favorites
                CMIQueue.Enabled = True
                CMIAddToPlaylist.Enabled = True
                CMIQueue.Text = CMIQueue.Text.TrimEnd(App.TrimEndSearch) & " (" & LVHistory.SelectedItems.Count.ToString("N0") & ")"
                CMIAddToPlaylist.Text = CMIAddToPlaylist.Text.TrimEnd(App.TrimEndSearch) & " (" & LVHistory.SelectedItems.Count.ToString("N0") & ")"
            Case HistoryView.MostPlayedArtists
                CMIQueue.Enabled = False
                CMIAddToPlaylist.Enabled = False
                CMIQueue.Text = CMIQueue.Text.TrimEnd(App.TrimEndSearch)
                CMIAddToPlaylist.Text = CMIAddToPlaylist.Text.TrimEnd(App.TrimEndSearch)
        End Select
    End Sub
    Private Sub CMIQueue_Click(sender As Object, e As EventArgs) Handles CMIQueue.Click
        Queue()
    End Sub
    Private Sub CMIAddToPlaylist_Click(sender As Object, e As EventArgs) Handles CMIAddToPlaylist.Click
        AddToPlaylist()
    End Sub
    Private Sub LVHistory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVHistory.SelectedIndexChanged
        ShowCounts()
    End Sub
    Private Sub RadBtnMostPlayed_Click(sender As Object, e As EventArgs) Handles RadBtnMostPlayed.Click
        CurrentView = HistoryView.MostPlayed
        PutViewData()
    End Sub
    Private Sub RadBtnMostPlayedArtists_Click(sender As Object, e As EventArgs) Handles RadBtnMostPlayedArtists.Click
        CurrentView = HistoryView.MostPlayedArtists
        PutViewData()
    End Sub
    Private Sub RadBtnRecentlyPlayed_Click(sender As Object, e As EventArgs) Handles RadBtnRecentlyPlayed.Click
        CurrentView = HistoryView.RecentlyPlayed
        PutViewData()
    End Sub
    Private Sub RadBtnFavorites_Click(sender As Object, e As EventArgs) Handles RadBtnFavorites.Click
        CurrentView = HistoryView.Favorites
        PutViewData()
    End Sub
    Private Sub RadBtnGenres_Click(sender As Object, e As EventArgs) Handles RadBtnGenres.Click
        CurrentChartView = ChartView.Genres
        PutChartData()
    End Sub
    Private Sub RadBtnGenrePolar_Click(sender As Object, e As EventArgs) Handles RadBtnGenrePolar.Click
        CurrentChartView = ChartView.GenrePolar
        PutChartData()
    End Sub
    Private Sub RadBtnGenrePareto_Click(sender As Object, e As EventArgs) Handles RadBtnGenrePareto.Click
        CurrentChartView = ChartView.GenrePareto
        PutChartData()
    End Sub
    Private Sub RadBtnArtists_Click(sender As Object, e As EventArgs) Handles RadBtnArtists.Click
        CurrentChartView = ChartView.Artists
        PutChartData()
    End Sub
    Private Sub RadBtnArtistWordCloud_Click(sender As Object, e As EventArgs) Handles RadBtnArtistWordCloud.Click
        CurrentChartView = ChartView.ArtistWordCloud
        PutChartData()
    End Sub
    Private Sub TxtBoxMaxRecords_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxMaxRecords.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            TxtBoxMaxRecords_Validated(sender, EventArgs.Empty)
        End If
    End Sub
    Private Sub TxtBoxMaxRecords_Validated(sender As Object, e As EventArgs) Handles TxtBoxMaxRecords.Validated
        Try
            CurrentViewMaxRecords = CInt(TxtBoxMaxRecords.Text)
        Catch
            CurrentViewMaxRecords = 0
            TxtBoxMaxRecords.Text = "All"
        End Try
        SetShowAll()
        PutViewData()
    End Sub

    'Procedures
    Private Function GetData() As List(Of App.SongView)
        Dim list As New List(Of App.SongView)

        For Each s As App.Song In App.History
            list.Add(New App.SongView(s))
            PBLoading.Value += 1
            Application.DoEvents()
        Next

        Return list
    End Function
    Private Sub PutData()

        Dim sessionStats = App.GetSessionStats
        TxtBoxSessionPlayedSongs.Text = sessionStats.Count.ToString("N0")
        TxtBoxSessionPlayedDuration.Text = $"{CInt(sessionStats.Duration.TotalDays)}d {sessionStats.Duration.Hours:D2}:{sessionStats.Duration.Minutes:D2}:{sessionStats.Duration.Seconds:D2}"

        Dim lifetimeStats = App.GetLifetimeStats
        TxtBoxTotalPlayedSongs.Text = lifetimeStats.Count.ToString("N0")
        TxtBoxTotalDuration.Text = $"{CInt(lifetimeStats.Duration.TotalDays)}d {lifetimeStats.Duration.Hours:D2}:{lifetimeStats.Duration.Minutes:D2}:{lifetimeStats.Duration.Seconds:D2}"

        Dim mostPlayed = views.OrderByDescending(Function(v) v.Data.PlayCount).ThenByDescending(Function(v) v.Data.LastPlayed).FirstOrDefault()
        If mostPlayed Is Nothing Then
            TxtBoxMostPlayedSong.Text = "No songs played yet."
        Else
            TxtBoxMostPlayedSong.Text = $"{mostPlayed.Title} - {mostPlayed.Artist} ({mostPlayed.Data.PlayCount} plays)"
        End If

    End Sub
    Private Sub PutViewData()
        LVHistory.BeginUpdate()
        LVHistory.Items.Clear()
        Dim sortedViews As IEnumerable(Of App.SongView) = Nothing
        Select Case CurrentView
            Case HistoryView.MostPlayed
                ConfigureColumns()
                sortedViews = views.OrderByDescending(Function(v) v.Data.PlayCount).ThenByDescending(Function(v) v.Data.LastPlayed)
                If CurrentViewMaxRecords > 0 Then
                    sortedViews = sortedViews.Take(CurrentViewMaxRecords)
                End If
                For Each v As App.SongView In sortedViews
                    Dim lvi As ListViewItem
                    If App.IsUrl(v.Data.Path) Then
                        lvi = New ListViewItem(v.Data.Path)
                    Else
                        lvi = New ListViewItem(v.Title)
                    End If
                    lvi.SubItems.Add(v.Artist)
                    lvi.SubItems.Add(v.Album)
                    lvi.SubItems.Add(v.Genre)
                    lvi.SubItems.Add(v.Data.PlayCount.ToString("N0"))
                    lvi.SubItems.Add(v.Data.LastPlayed.ToString("g"))
                    lvi.SubItems.Add(v.Data.Path)
                    LVHistory.Items.Add(lvi)
                Next
                LVHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                LVHistory.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.HeaderSize)
                LVHistory.Columns(4).TextAlign = HorizontalAlignment.Center
                LVHistory.EndUpdate()
                ShowCounts()
            Case HistoryView.MostPlayedArtists
                ConfigureColumns()

                Dim artistGroups As IEnumerable(Of MostPlayedArtistsReport) =
                    views.GroupBy(Function(v)
                                      Dim artistName As String = v.Artist
                                      If String.IsNullOrWhiteSpace(v.Artist) Then
                                          Return "Video"
                                      Else
                                          Return v.Artist
                                      End If
                                  End Function) _
                         .Select(Function(g) New MostPlayedArtistsReport With {
                             .Artist = g.Key,
                             .PlayCount = g.Sum(Function(v) v.Data.PlayCount),
                             .LastPlayed = g.Max(Function(v) v.Data.LastPlayed)
                         }) _
                         .OrderByDescending(Function(x) x.PlayCount) _
                         .ThenByDescending(Function(x) x.LastPlayed)

                If CurrentViewMaxRecords > 0 Then
                    artistGroups = artistGroups.Take(CurrentViewMaxRecords)
                End If

                For Each g As MostPlayedArtistsReport In artistGroups
                    Dim lvi As New ListViewItem(g.Artist)
                    lvi.SubItems.Add(g.PlayCount.ToString("N0"))
                    lvi.SubItems.Add(g.LastPlayed.ToString("g"))
                    LVHistory.Items.Add(lvi)
                Next

                LVHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                LVHistory.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize)
                LVHistory.Columns(1).TextAlign = HorizontalAlignment.Center

                LVHistory.EndUpdate()
                ShowCounts()
            Case HistoryView.RecentlyPlayed
                ConfigureColumns()
                sortedViews = views.OrderByDescending(Function(v) v.Data.LastPlayed)
                If CurrentViewMaxRecords > 0 Then
                    sortedViews = sortedViews.Take(CurrentViewMaxRecords)
                End If
                For Each v As App.SongView In sortedViews
                    Dim lvi As ListViewItem
                    If App.IsUrl(v.Data.Path) Then
                        lvi = New ListViewItem(v.Data.Path)
                    Else
                        lvi = New ListViewItem(v.Title)
                    End If
                    lvi.SubItems.Add(v.Artist)
                    lvi.SubItems.Add(v.Album)
                    lvi.SubItems.Add(v.Genre)
                    lvi.SubItems.Add(v.Data.PlayCount.ToString("N0"))
                    lvi.SubItems.Add(v.Data.LastPlayed.ToString("g"))
                    lvi.SubItems.Add(v.Data.Path)
                    LVHistory.Items.Add(lvi)
                Next
                LVHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                LVHistory.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.HeaderSize)
                LVHistory.Columns(4).TextAlign = HorizontalAlignment.Center
                LVHistory.EndUpdate()
                ShowCounts()
            Case HistoryView.Favorites
                ConfigureColumns()
                sortedViews = views.OrderByDescending(Function(v) v.Data.Rating).ThenByDescending(Function(v) v.Data.LastPlayed)
                If CurrentViewMaxRecords > 0 Then
                    sortedViews = sortedViews.Take(CurrentViewMaxRecords)
                End If
                For Each v As App.SongView In sortedViews
                    Dim lvi As ListViewItem
                    If App.IsUrl(v.Data.Path) Then
                        lvi = New ListViewItem(v.Data.Path)
                    Else
                        lvi = New ListViewItem(v.Title)
                    End If
                    lvi.SubItems.Add(v.Artist)
                    lvi.SubItems.Add(v.Album)
                    lvi.SubItems.Add(v.Genre)
                    lvi.SubItems.Add(v.Data.PlayCount.ToString("N0"))
                    lvi.SubItems.Add(v.Data.LastPlayed.ToString("g"))
                    lvi.SubItems.Add(v.Data.Path)
                    LVHistory.Items.Add(lvi)
                Next
                LVHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                LVHistory.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.HeaderSize)
                LVHistory.Columns(4).TextAlign = HorizontalAlignment.Center
                LVHistory.EndUpdate()
                ShowCounts()
        End Select
    End Sub
    Private Sub PutChartData()
        Select Case CurrentChartView
            Case ChartView.Genres
                Dim genreCounts As New Dictionary(Of String, Integer)
                Dim genreIndex = LVHistory.Columns("Genre").Index

                'Count genres from history list
                For Each lvi As ListViewItem In LVHistory.Items
                    Dim genre = lvi.SubItems(genreIndex).Text
                    If Not String.IsNullOrEmpty(genre) Then
                        If Not genreCounts.ContainsKey(genre) Then
                            genreCounts(genre) = 0
                        End If
                        genreCounts(genre) += 1
                    End If
                Next

                'Create chart
                Dim chart As New Chart With {.Dock = DockStyle.Fill}
                Dim area As New ChartArea()
                chart.ChartAreas.Add(area)

                Dim series As New Series("Genres") With {.ChartType = SeriesChartType.Pie}

                For Each kvp In genreCounts
                    series.Points.AddXY(kvp.Key, kvp.Value)
                Next

                chart.Series.Add(series)
                chart.BackColor = App.CurrentTheme.BackColor
                chart.ChartAreas(0).BackColor = App.CurrentTheme.BackColor

                'Show in a panel or form
                PanelCharts.Controls.Clear()
                PanelCharts.Controls.Add(chart)
            Case ChartView.GenrePolar
                'Count genres from history list
                Dim genreCounts As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
                Dim genreIndex = LVHistory.Columns("Genre").Index

                For Each lvi As ListViewItem In LVHistory.Items
                    Dim genre = lvi.SubItems(genreIndex).Text.Trim()
                    If Not String.IsNullOrEmpty(genre) Then
                        If Not genreCounts.ContainsKey(genre) Then
                            genreCounts(genre) = 0
                        End If
                        genreCounts(genre) += 1
                    End If
                Next

                'Create chart
                Dim chart As New Chart With {.Dock = DockStyle.Fill}
                Dim area As New ChartArea("PolarArea")
                chart.ChartAreas.Add(area)
                With chart.ChartAreas(0).AxisY.MajorGrid
                    .LineColor = App.CurrentTheme.TextColor   'or any Color you like
                    .LineDashStyle = ChartDashStyle.Solid     'optional: Solid, Dash, Dot, etc.
                    .LineWidth = 1                            'thickness of the circles
                End With
                With chart.ChartAreas(0).AxisX
                    .LineColor = App.CurrentTheme.TextColor             'horizontal axis line
                    .MajorGrid.LineColor = App.CurrentTheme.TextColor   'radial spokes
                    .LineWidth = 2
                End With
                With chart.ChartAreas(0).AxisY
                    .LineColor = App.CurrentTheme.TextColor             'vertical axis line (the radius line)
                    .MajorGrid.LineColor = App.CurrentTheme.TextColor   'concentric circles
                    .LineWidth = 2
                End With

                'Configure axis for polar/radar style
                With area
                    .AxisX.Interval = 1
                    .AxisX.LabelStyle.ForeColor = App.CurrentTheme.TextColor
                    .AxisY.LabelStyle.ForeColor = App.CurrentTheme.TextColor
                    .BackColor = App.CurrentTheme.BackColor
                End With

                'Create radar series
                Dim series As New Series("Genres") With {
                    .ChartType = SeriesChartType.Radar,
                    .BorderWidth = 4,
                    .Color = Color.FromArgb(100, App.CurrentTheme.ButtonBackColor), 'fill color
                    .BackSecondaryColor = App.CurrentTheme.ButtonTextColor,         'outline color
                    .BackGradientStyle = GradientStyle.DiagonalRight,
                    .IsValueShownAsLabel = True,
                    .LabelForeColor = App.CurrentTheme.TextColor}

                'Add each genre as a spoke
                Dim i As Integer = 0
                For Each kvp In genreCounts.OrderByDescending(Function(k) k.Value)
                    Dim idx As Integer = series.Points.AddXY(i, kvp.Value) ' numeric X
                    series.Points(idx).AxisLabel = kvp.Key                  ' label on spoke
                    i += 1
                Next

                chart.Series.Clear()
                chart.Series.Add(series)

                'Apply theme
                chart.BackColor = App.CurrentTheme.BackColor

                'Show chart
                PanelCharts.Controls.Clear()
                PanelCharts.Controls.Add(chart)
            Case ChartView.GenrePareto
                'Count plays per genre
                Dim genreCounts As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
                Dim genreIndex = LVHistory.Columns("Genre").Index

                For Each lvi As ListViewItem In LVHistory.Items
                    Dim genre = lvi.SubItems(genreIndex).Text.Trim()
                    If Not String.IsNullOrEmpty(genre) Then
                        If Not genreCounts.ContainsKey(genre) Then
                            genreCounts(genre) = 0
                        End If
                        genreCounts(genre) += 1
                    End If
                Next

                'Sort by descending play count
                Dim sorted = genreCounts.OrderByDescending(Function(k) k.Value).ToList()
                Dim totalPlays = sorted.Sum(Function(k) k.Value)

                'Create chart
                Dim chart As New Chart With {.Dock = DockStyle.Fill}
                Dim area As New ChartArea("ParetoArea")
                chart.ChartAreas.Add(area)
                With chart.ChartAreas(0).AxisX
                    .Interval = 1
                    .MajorGrid.Enabled = False
                    .MinorTickMark.LineColor = App.CurrentTheme.TextColor
                    .MajorTickMark.LineColor = App.CurrentTheme.TextColor
                    .LineColor = App.CurrentTheme.TextColor
                    .LineDashStyle = ChartDashStyle.Solid
                    .LabelStyle.IsStaggered = True
                End With
                With chart.ChartAreas(0).AxisY
                    .MinorTickMark.LineColor = App.CurrentTheme.TextColor
                    .MajorTickMark.LineColor = App.CurrentTheme.TextColor
                    .LineColor = App.CurrentTheme.TextColor
                    .LineDashStyle = ChartDashStyle.Solid
                End With
                With chart.ChartAreas(0).AxisY2
                    .Title = "Cumulative %"
                    .Minimum = 0
                    .Maximum = 100
                    .MajorGrid.Enabled = True
                    .MajorGrid.LineColor = App.CurrentTheme.TextColor
                    .MajorGrid.LineDashStyle = ChartDashStyle.Solid
                    .MinorTickMark.LineColor = App.CurrentTheme.TextColor
                    .MajorTickMark.LineColor = App.CurrentTheme.TextColor
                    .TitleForeColor = App.CurrentTheme.TextColor
                    .LineColor = App.CurrentTheme.TextColor
                    .LabelStyle.ForeColor = App.CurrentTheme.TextColor
                End With

                'Configure axes
                With area
                    .AxisX.Interval = 1
                    .AxisX.LabelStyle.Angle = -45
                    .AxisX.LabelStyle.ForeColor = App.CurrentTheme.TextColor
                    .AxisY.LabelStyle.ForeColor = App.CurrentTheme.TextColor
                    .AxisY2.LabelStyle.ForeColor = App.CurrentTheme.TextColor
                    .BackColor = App.CurrentTheme.BackColor
                End With

                'Bar series (play counts)
                Dim barSeries As New Series("Plays") With {
                    .ChartType = SeriesChartType.Column,
                    .Color = App.CurrentTheme.ButtonBackColor,
                    .IsValueShownAsLabel = True,
                    .LabelForeColor = App.CurrentTheme.TextColor,
                    .YAxisType = AxisType.Primary
                }

                'Line series (cumulative %)
                Dim lineSeries As New Series("Cumulative %") With {
                    .ChartType = SeriesChartType.Line,
                    .BorderWidth = 2,
                    .Color = App.CurrentTheme.TextColor,
                    .YAxisType = AxisType.Secondary
                }

                Debug.WriteLine(String.Join(", ", genreCounts.Keys))

                'Populate both series
                Dim runningTotal As Integer = 0
                Dim i As Integer = 0
                For Each kvp In sorted
                    Dim genre = kvp.Key
                    Dim plays = kvp.Value
                    runningTotal += plays
                    Dim cumulativePct = (runningTotal / totalPlays) * 100

                    'Add with numeric X
                    Dim barIdx = barSeries.Points.AddXY(i, plays)
                    barSeries.Points(barIdx).AxisLabel = genre

                    Dim lineIdx = lineSeries.Points.AddXY(i, cumulativePct)
                    lineSeries.Points(lineIdx).AxisLabel = genre

                    i += 1
                Next

                chart.Series.Add(barSeries)
                chart.Series.Add(lineSeries)

                'Optional: add 80% reference line
                Dim strip As New StripLine()
                strip.IntervalOffset = 80
                strip.StripWidth = 0
                strip.BorderColor = App.CurrentTheme.ButtonBackColor
                strip.BorderDashStyle = ChartDashStyle.Dash
                chart.ChartAreas(0).AxisY2.StripLines.Add(strip)

                'Apply theme
                chart.BackColor = App.CurrentTheme.BackColor

                'Show chart
                PanelCharts.Controls.Clear()
                PanelCharts.Controls.Add(chart)
            Case ChartView.Artists
                'Dictionary to hold artist → play count
                'Use case-insensitive comparer so "Avril Lavigne" and "avril lavigne" are treated the same
                Dim artistCounts As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

                'Find the index of the Artist column in the ListView
                Dim artistIndex = LVHistory.Columns("Artist").Index

                'Count artists from history list
                For Each lvi As ListViewItem In LVHistory.Items
                    Dim artist = lvi.SubItems(artistIndex).Text.Trim() ' Trim to remove stray spaces
                    If Not String.IsNullOrEmpty(artist) Then
                        If Not artistCounts.ContainsKey(artist) Then
                            artistCounts(artist) = 0
                        End If
                        artistCounts(artist) += 1
                    End If
                Next

                'Create a new chart and chart area
                Dim chart As New Chart With {.Dock = DockStyle.Fill}
                Dim area As New ChartArea("MainArea")
                chart.ChartAreas.Add(area)

                'Configure the series for a vertical bar chart
                Dim series As New Series("Artists") With {
                    .ChartType = SeriesChartType.Column,
                    .IsValueShownAsLabel = True, ' Show play counts above bars
                    .LabelForeColor = App.CurrentTheme.TextColor}

                'Sort artists by play count descending before plotting
                Dim sortedArtists = artistCounts.OrderByDescending(Function(kvp) kvp.Value)

                'Add each artist as a separate bar
                Dim i As Integer = 0
                For Each kvp In sortedArtists
                    'Add a point with numeric X (i) and Y = play count
                    Dim idx As Integer = series.Points.AddXY(i, kvp.Value)
                    'Set the label under the bar to the artist name
                    series.Points(idx).AxisLabel = kvp.Key
                    i += 1
                Next

                'Clear any existing series and add our new one
                chart.Series.Clear()
                chart.Series.Add(series)

                'Configure X axis so every artist label shows
                With chart.ChartAreas(0).AxisX
                    .Interval = 1
                    .LabelStyle.IsStaggered = False
                    .MajorGrid.Enabled = False
                End With

                'Apply theme colors
                chart.BackColor = App.CurrentTheme.BackColor
                chart.ChartAreas(0).BackColor = App.CurrentTheme.BackColor
                chart.ChartAreas(0).AxisY.LabelStyle.ForeColor = App.CurrentTheme.TextColor
                chart.ChartAreas(0).AxisX.LabelStyle.ForeColor = App.CurrentTheme.TextColor

                'Show chart in the panel
                PanelCharts.Controls.Clear()
                PanelCharts.Controls.Add(chart)
            Case ChartView.ArtistWordCloud
                'Dictionary to hold artist → play count
                Dim artistCounts As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

                'Find the index of the Artist column in the ListView
                Dim artistIndex = LVHistory.Columns("Artist").Index

                'Count artists from history list
                For Each lvi As ListViewItem In LVHistory.Items
                    Dim artist = lvi.SubItems(artistIndex).Text.Trim()
                    If Not String.IsNullOrEmpty(artist) Then
                        If Not artistCounts.ContainsKey(artist) Then
                            artistCounts(artist) = 0
                        End If
                        artistCounts(artist) += 1
                    End If
                Next

                'Convert dictionary to arrays for WordCloud.NET
                Dim words = artistCounts.Keys.ToArray()
                Dim frequencies = artistCounts.Values.ToArray()

                'Create the word cloud generator
                Dim wc As New WordCloud(
                    width:=PanelCharts.ClientSize.Width,
                    height:=PanelCharts.ClientSize.Height,
                    useRank:=False,
                    fontColor:=App.CurrentTheme.TextColor,     'single color for all words
                    maxFontSize:=60,                           'cap size
                    fontStep:=2,                               'spacing between sizes
                    mask:=Nothing,
                    allowVerical:=False,                       'spelled as in lib
                    fontname:="Segoe UI"                       'font family
                )

                'Generate the image
                Dim bmp As Image = wc.Draw(words, frequencies)

                'Show in a PictureBox (or directly in your panel)
                Dim pb As New PictureBox With {
                    .Dock = DockStyle.Fill,
                    .Image = bmp,
                    .SizeMode = PictureBoxSizeMode.Zoom,
                    .BackColor = App.CurrentTheme.BackColor
                }

                PanelCharts.Controls.Clear()
                PanelCharts.Controls.Add(pb)
        End Select
    End Sub
    Private Sub Queue(Optional queueall As Boolean = False)
        Dim queuelist = If(queueall, LVHistory.Items.Cast(Of ListViewItem).ToList(), LVHistory.SelectedItems.Cast(Of ListViewItem).ToList())
        If queuelist.Count > 0 Then
            Dim pathindex = LVHistory.Columns("Path").Index
            For Each lvi As ListViewItem In queuelist
                Player.QueuePath(lvi.SubItems(pathindex).Text)
            Next
        End If
    End Sub
    Private Sub AddToPlaylist(Optional addall As Boolean = False)
        Dim addlist = If(addall, LVHistory.Items.Cast(Of ListViewItem).ToList(), LVHistory.SelectedItems.Cast(Of ListViewItem).ToList())
        If addlist.Count > 0 Then
            Dim pathindex = LVHistory.Columns("Path").Index
            Dim paths As New List(Of String)
            For Each lvi As ListViewItem In addlist
                paths.Add(lvi.SubItems(pathindex).Text)
            Next
            Player.AddToPlaylist(paths)
        End If
    End Sub
    Private Sub SetShowAll()
        If CurrentViewMaxRecords = 0 Then
            BtnShowAll.Enabled = False
        Else
            BtnShowAll.Enabled = True
        End If
    End Sub
    Private Sub ShowCounts()
        LblHistoryViewCount.Visible = True
        LblHistoryViewCount.Text = LVHistory.Items.Count.ToString("N0") & If(LVHistory.Items.Count = 1, " song", " songs")
        LblHistoryViewCount.Text &= ", " & LVHistory.SelectedItems.Count.ToString("N0") & " selected"
        BtnQueueAll.Enabled = False
        BtnAddAllToPlaylist.Enabled = False
        Select Case CurrentView
            Case HistoryView.MostPlayed, HistoryView.RecentlyPlayed, HistoryView.Favorites
                BtnQueueAll.Enabled = True
                BtnAddAllToPlaylist.Enabled = True
        End Select
    End Sub
    Private Sub ConfigureColumns()
        Dim header As ColumnHeader
        LVHistory.Columns.Clear()
        Select Case CurrentView
            Case HistoryView.MostPlayed, HistoryView.RecentlyPlayed, HistoryView.Favorites
                'Define 7 Columns
                header = New ColumnHeader()
                header.Name = "Title"
                header.Text = "Title"
                header.Width = 200
                LVHistory.Columns.Add(header)
                header = New ColumnHeader()
                header.Name = "Artist"
                header.Text = "Artist"
                header.Width = 200
                LVHistory.Columns.Add(header)
                header = New ColumnHeader()
                header.Name = "Album"
                header.Text = "Album"
                header.Width = 200
                LVHistory.Columns.Add(header)
                header = New ColumnHeader()
                header.Name = "Genre"
                header.Text = "Genre"
                header.Width = 100
                LVHistory.Columns.Add(header)
                header = New ColumnHeader()
                header.Name = "PlayCount"
                header.Text = "Plays"
                header.Width = 50
                LVHistory.Columns.Add(header)
                header = New ColumnHeader()
                header.Name = "LastPlayed"
                header.Text = "Last Played"
                header.Width = 200
                LVHistory.Columns.Add(header)
                header = New ColumnHeader()
                header.Name = "Path"
                header.Text = "Path"
                header.Width = 400
                LVHistory.Columns.Add(header)
            Case HistoryView.MostPlayedArtists
                'Define 7 Columns
                header = New ColumnHeader()
                header.Name = "Artist"
                header.Text = "Artist"
                header.Width = 200
                LVHistory.Columns.Add(header)
                header = New ColumnHeader()
                header.Name = "PlayCount"
                header.Text = "Count"
                header.Width = 200
                LVHistory.Columns.Add(header)
                header = New ColumnHeader()
                header.Name = "LastPlayed"
                header.Text = "Last Played"
                header.Width = 200
                LVHistory.Columns.Add(header)
        End Select
    End Sub
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
        End If
        ResumeLayout()
        Debug.Print("History Accent Color Set")
    End Sub
    Private Sub SetTheme()
        SuspendLayout()

        If Not App.CurrentTheme.IsAccent Then
            BackColor = App.CurrentTheme.BackColor
        End If

        LblMostPlayedSong.ForeColor = App.CurrentTheme.TextColor
        TxtBoxMostPlayedSong.BackColor = App.CurrentTheme.BackColor
        TxtBoxMostPlayedSong.ForeColor = App.CurrentTheme.TextColor
        LblSessionPlayedSongs.ForeColor = App.CurrentTheme.TextColor
        TxtBoxSessionPlayedSongs.BackColor = App.CurrentTheme.BackColor
        TxtBoxSessionPlayedSongs.ForeColor = App.CurrentTheme.TextColor
        LblSessionPlayedDuration.ForeColor = App.CurrentTheme.TextColor
        TxtBoxSessionPlayedDuration.BackColor = App.CurrentTheme.BackColor
        TxtBoxSessionPlayedDuration.ForeColor = App.CurrentTheme.TextColor
        LblTotalPlayedSongs.ForeColor = App.CurrentTheme.TextColor
        TxtBoxTotalPlayedSongs.BackColor = App.CurrentTheme.BackColor
        TxtBoxTotalPlayedSongs.ForeColor = App.CurrentTheme.TextColor
        LblTotalDuration.ForeColor = App.CurrentTheme.TextColor
        TxtBoxTotalDuration.BackColor = App.CurrentTheme.BackColor
        TxtBoxTotalDuration.ForeColor = App.CurrentTheme.TextColor
        LVHistory.BackColor = App.CurrentTheme.BackColor
        LVHistory.ForeColor = App.CurrentTheme.TextColor
        GrpBoxHistory.ForeColor = App.CurrentTheme.TextColor
        RadBtnMostPlayed.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnMostPlayed.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnMostPlayedArtists.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnMostPlayedArtists.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnRecentlyPlayed.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnRecentlyPlayed.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnFavorites.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnFavorites.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnGenres.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnGenres.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnGenrePolar.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnGenrePolar.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnGenrePareto.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnGenrePareto.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnArtists.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnArtists.ForeColor = App.CurrentTheme.ButtonTextColor
        RadBtnArtistWordCloud.BackColor = App.CurrentTheme.ButtonBackColor
        RadBtnArtistWordCloud.ForeColor = App.CurrentTheme.ButtonTextColor
        LblMaxRecords.ForeColor = App.CurrentTheme.TextColor
        TxtBoxMaxRecords.BackColor = App.CurrentTheme.BackColor
        TxtBoxMaxRecords.ForeColor = App.CurrentTheme.TextColor
        BtnShowAll.BackColor = App.CurrentTheme.ButtonBackColor
        BtnShowAll.ForeColor = App.CurrentTheme.ButtonTextColor
        LblHistoryViewCount.ForeColor = App.CurrentTheme.TextColor
        BtnCharts.BackColor = App.CurrentTheme.ButtonBackColor
        BtnCharts.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnLists.BackColor = App.CurrentTheme.ButtonBackColor
        BtnLists.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnQueueAll.BackColor = App.CurrentTheme.ButtonBackColor
        BtnQueueAll.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnAddAllToPlaylist.BackColor = App.CurrentTheme.ButtonBackColor
        BtnAddAllToPlaylist.ForeColor = App.CurrentTheme.ButtonTextColor

        ResumeLayout()
        Debug.Print("History Theme Set")
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor()
        SetTheme()
        If GrpBoxCharts.Visible Then
            PutChartData()
        End If
    End Sub

End Class
