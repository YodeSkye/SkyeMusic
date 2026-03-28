
Friend Class Startup
    Private Sub Startup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Visible = False
        App.FrmPlayer = New Player()
        My.Application.ApplicationContext.MainForm = App.FrmPlayer
        App.FrmPlayer.Show()
        Close()
    End Sub
End Class
