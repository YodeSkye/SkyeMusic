Public Class AddStream

    'Declarations
    Friend NewStream As Player.PlaylistItemType

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            If m.Msg = My.WinAPI.WM_SYSCOMMAND AndAlso CInt(m.WParam) = My.WinAPI.SC_CLOSE Then
                DialogResult = DialogResult.Cancel
            End If
        Catch ex As Exception
            My.App.WriteToLog("AddStream WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub AddStream_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtBoxStreamPath.Text = NewStream.Path
    End Sub

    'Control Events
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        NewStream.Title = TxtBoxStreamTitle.Text
        NewStream.Path = TxtBoxStreamPath.Text
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class