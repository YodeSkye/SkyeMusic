
Imports Skye

Public Class FullScreen

    Private Sub FullScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WinAPI.HideFormInTaskSwitcher(Handle)
    End Sub

End Class
