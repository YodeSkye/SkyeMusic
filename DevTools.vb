
Public Class DevTools

    'Form Events
    Private Sub DevTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = My.Application.Info.Title & " " & Me.Text
        GetPlaysData()
    End Sub
    Private Sub DevTools_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        'Force layout update
        DGVPlays.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

        'Resize form
        Me.Size = New Size(DGVPlays.PreferredSize.Width, Me.Height)

    End Sub

    'Control Events
    Private Sub BtnRefreshData_Click(sender As Object, e As EventArgs) Handles BtnRefreshData.Click
        GetPlaysData()
    End Sub
    Private Sub BtnDeleteSelected_Click(sender As Object, e As EventArgs) Handles BtnDeleteSelected.Click
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
                LblCount.Text = $"{failedCount} record(s) failed to delete."
            Else
                LblCount.Text = "Selected records deleted."
            End If
        End If
    End Sub

    'Procedures
    Private Sub GetPlaysData()
        DGVPlays.DataSource = App.GetPlayHistoryTable()
        LblCount.Text = DGVPlays.Rows.Count.ToString & " Rows"
    End Sub

End Class
