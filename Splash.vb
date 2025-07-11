
Public Class Splash
    Private Sub Splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BackColor = App.RedTheme.BackColor
        LblSkye.ForeColor = App.RedTheme.TextColor
        LblMusic.ForeColor = App.RedTheme.TextColor
    End Sub

End Class