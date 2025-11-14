
Imports System.ComponentModel

Public Class DevTools

    'Declarations
    Private lastHistorySortColumn As String = ""
    Private lastHistorySortAscending As Boolean = True
    Private DeleteHistoryConfirm As Boolean = False
    Private DeletePlaysConfirm As Boolean = False
    Private WithEvents TimerDeleteHistory As New Timer
    Private WithEvents TimerDeletePlays As New Timer
    Private TipDevTools As Skye.UI.ToolTipEX

    'Form Events
    Private Sub DevTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title & " " & Text
        TimerDeleteHistory.Interval = 5000
        TimerDeletePlays.Interval = 5000
        GetHistoryData()
        GetPlaysData()
    End Sub

    'Control Events
    Private Sub DGVHistory_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGVHistory.ColumnHeaderMouseClick
        Dim col = DGVHistory.Columns(e.ColumnIndex)
        Dim propName = col.DataPropertyName

        'Toggle if same column clicked
        If lastHistorySortColumn = propName Then
            lastHistorySortAscending = Not lastHistorySortAscending
        Else
            lastHistorySortColumn = propName
            lastHistorySortAscending = True
        End If

        'Sort the list
        If lastHistorySortAscending Then
            App.History = App.History.OrderBy(Function(s) CallByName(s, propName, CallType.Get)).ToList()
            col.HeaderCell.SortGlyphDirection = SortOrder.Ascending
        Else
            App.History = App.History.OrderByDescending(Function(s) CallByName(s, propName, CallType.Get)).ToList()
            col.HeaderCell.SortGlyphDirection = SortOrder.Descending
        End If

        'Rebind
        GetHistoryData()
    End Sub
    Private Sub BtnHistoryRefresh_Click(sender As Object, e As EventArgs) Handles BtnHistoryRefresh.Click
        GetHistoryData()
    End Sub
    Private Sub BtnHistoryDeleteSelected_Click(sender As Object, e As EventArgs) Handles BtnHistoryDeleteSelected.Click
        If DeleteHistoryConfirm Then
            'Collect selected rows
            Dim rowsToDelete As New List(Of Song)
            For Each r As DataGridViewRow In DGVHistory.SelectedRows
                Dim song As Song = CType(r.DataBoundItem, Song)
                rowsToDelete.Add(song)
            Next

            'Remove them from History
            For Each s In rowsToDelete
                App.History.Remove(s)
            Next

            'Refresh the grid
            GetHistoryData()
        End If
        SetDeleteHistoryConfirm()
    End Sub
    Private Sub BtnPlaysRefresh_Click(sender As Object, e As EventArgs) Handles BtnPlaysRefresh.Click
        GetPlaysData()
    End Sub
    Private Sub BtnPlaysDeleteSelected_Click(sender As Object, e As EventArgs) Handles BtnPlaysDeleteSelected.Click
        If DeletePlaysConfirm Then
            If DGVPlays.SelectedRows.Count > 0 Then
                Dim idsToDelete As New List(Of Integer)

                'Collect all selected IDs first
                For Each row As DataGridViewRow In DGVPlays.SelectedRows
                    Dim id As Integer = Convert.ToInt32(row.Cells("Id").Value)
                    idsToDelete.Add(id)
                Next

                'Delete all after selection is stable
                Dim failedCount As Integer = 0
                For Each id In idsToDelete
                    If Not App.DeletePlayHistoryById(id) Then
                        failedCount += 1
                    End If
                Next

                'Refresh once
                GetPlaysData()

                'Feedback
                If failedCount > 0 Then
                    LblPlaysCounts.Text = $"{failedCount} record(s) failed to delete."
                Else
                    LblPlaysCounts.Text = "Selected records deleted."
                End If
            End If
        End If
        SetDeletePlaysConfirm()
    End Sub

    'Handlers
    Private Sub TimerDeleteHistory_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerDeleteHistory.Tick
        SetDeleteHistoryConfirm()
    End Sub
    Private Sub TimerDeletePlays_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerDeletePlays.Tick
        SetDeletePlaysConfirm()
    End Sub

    'Procedures
    Private Sub GetHistoryData()
        DGVHistory.DataSource = Nothing
        DGVHistory.AutoGenerateColumns = True
        DGVHistory.DataSource = App.History
        For Each col As DataGridViewColumn In DGVHistory.Columns
            col.SortMode = DataGridViewColumnSortMode.Automatic
        Next
        LblHistoryCounts.Text = DGVHistory.Rows.Count.ToString & " Rows"
    End Sub
    Private Sub GetPlaysData()
        DGVPlays.DataSource = App.GetPlayHistoryTable()
        LblPlaysCounts.Text = DGVPlays.Rows.Count.ToString & " Rows"
    End Sub
    Private Sub SetDeleteHistoryConfirm(Optional forcereset As Boolean = False)
        If DeleteHistoryConfirm Or forcereset Then
            TimerDeleteHistory.Stop()
            DeleteHistoryConfirm = False
            BtnHistoryDeleteSelected.ResetBackColor()
            If TipDevTools IsNot Nothing Then TipDevTools.HideTooltip()
        Else
            DeleteHistoryConfirm = True
            BtnHistoryDeleteSelected.BackColor = Color.Red
            SetToolTip()
            TipDevTools.HideDelay = 5000
            TipDevTools.ForeColor = Color.Red
            TipDevTools.ShowTooltipAtCursor("Are You Sure?")
            TimerDeleteHistory.Start()
        End If
    End Sub
    Private Sub SetDeletePlaysConfirm(Optional forcereset As Boolean = False)
        If DeletePlaysConfirm Or forcereset Then
            TimerDeletePlays.Stop()
            DeletePlaysConfirm = False
            BtnPlaysDeleteSelected.ResetBackColor()
            If TipDevTools IsNot Nothing Then TipDevTools.HideTooltip()
        Else
            DeletePlaysConfirm = True
            BtnPlaysDeleteSelected.BackColor = Color.Red
            SetToolTip()
            TipDevTools.HideDelay = 5000
            TipDevTools.ForeColor = Color.Red
            TipDevTools.ShowTooltipAtCursor("Are You Sure?")
            TimerDeletePlays.Start()
        End If
    End Sub
    Private Sub SetToolTip()
        TipDevTools = New Skye.UI.ToolTipEX With {
            .Font = New Font("Segoe UI", 14, FontStyle.Bold)}
    End Sub
End Class

'''Function to find duplicates in a List(of T)
'Dim dupes = App.History _
'        .GroupBy(Function(s) s.Path) _
'        .Where(Function(g) g.Count > 1) _
'        .SelectMany(Function(g) g) _
'        .ToList
'DGVHistory.DataSource = dupes