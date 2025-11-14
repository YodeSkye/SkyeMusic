
Public Class OptionsRainbowBar

    Private IsInitializing As Boolean = True

    ' Form Events
    Private Sub OptionsRainbowBar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowSettings()
    End Sub

    ' Control Events
    Private Sub TxtBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxBarCount.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            Validate()
        End If
    End Sub
    Private Sub TxtBoxNumbersOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TxtBoxBarCount.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then e.Handled = True
    End Sub
    Private Sub TxtBoxBarCount_Validated(sender As Object, e As EventArgs) Handles TxtBoxBarCount.Validated
        App.Visualizers.RainbowBarCount = CInt(TxtBoxBarCount.Text)
        If App.Visualizers.RainbowBarCount < 8 Then
            App.Visualizers.RainbowBarCount = 8
        ElseIf App.Visualizers.RainbowBarCount > 128 Then
            App.Visualizers.RainbowBarCount = 128
        End If
        TxtBoxBarCount.Text = App.Visualizers.RainbowBarCount.ToString
        'Debug.Print("Rainbow Bar Count set to " & App.Visualizers.RainbowBarCount.ToString)
    End Sub
    Private Sub TBGain_ValueChanged(sender As Object, e As EventArgs) Handles TBGain.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.RainbowBarGain = TBGain.Value
        'Debug.Print("Rainbow Bar Gain set to " & App.Visualizers.RainbowBarGain.ToString)
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        TxtBoxBarCount.Text = App.Visualizers.RainbowBarCount.ToString
        TBGain.Value = CInt(App.Visualizers.RainbowBarGain)
        IsInitializing = False
    End Sub

End Class
