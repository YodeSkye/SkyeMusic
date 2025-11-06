
Imports System.ComponentModel

Public Class DevTools

    'Declarations
    Private lastHistorySortColumn As String = ""
    Private lastHistorySortAscending As Boolean = True

    'Form Events
    Private Sub DevTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title & " " & Text
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
    End Sub
    Private Sub BtnPlaysRefresh_Click(sender As Object, e As EventArgs) Handles BtnPlaysRefresh.Click
        GetPlaysData()
    End Sub
    Private Sub BtnPlaysDeleteSelected_Click(sender As Object, e As EventArgs) Handles BtnPlaysDeleteSelected.Click
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

End Class
