
Public Class Splash
    Private Sub Splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BackColor = App.CrimsonEmberTheme.BackColor
        LblSkye.ForeColor = App.CrimsonEmberTheme.TextColor
        LblMusic.ForeColor = App.CrimsonEmberTheme.TextColor
    End Sub

End Class