
Friend Class Startup
    Private Sub Startup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WindowState = FormWindowState.Minimized
        ShowInTaskbar = False
        App.FrmPlayer = New Player()
        App.FrmPlayer.Show()
    End Sub
    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        Hide()
    End Sub
End Class
