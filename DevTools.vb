
Public Class DevTools

    'Form Events
    Private Sub DevTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title & " " & Text
        GetHistoryData()
        GetPlaysData()
    End Sub
    Private Sub DevTools_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        'Force layout update
        DGVPlays.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

        'Resize form
        'Size = New Size(DGVPlays.PreferredSize.Width, Height)

    End Sub

    'Control Events
    Private Sub BtnHistoryRefresh_Click(sender As Object, e As EventArgs) Handles BtnHistoryRefresh.Click
        GetHistoryData()
    End Sub
    Private Sub BtnHistoryDeleteSelected_Click(sender As Object, e As EventArgs) Handles BtnHistoryDeleteSelected.Click

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

    End Sub
    Private Sub GetPlaysData()
        DGVPlays.DataSource = App.GetPlayHistoryTable()
        LblPlaysCounts.Text = DGVPlays.Rows.Count.ToString & " Rows"
    End Sub

End Class
