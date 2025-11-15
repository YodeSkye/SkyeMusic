
Public Class OptionsRainbowBar

    ' Declarations
    Private IsInitializing As Boolean = True

    ' Form Events
    Private Sub OptionsRainbowBar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetAccentColor()
        SetTheme()
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
    Private Sub ChkBoxShowPeaks_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxShowPeaks.CheckedChanged
        App.Visualizers.RainbowBarShowPeaks = ChkBoxShowPeaks.Checked
    End Sub
    Private Sub TBGain_ValueChanged(sender As Object, e As EventArgs) Handles TBGain.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.RainbowBarGain = TBGain.Value
        'Debug.Print("Rainbow Bar Gain set to " & App.Visualizers.RainbowBarGain.ToString)
    End Sub
    Private Sub TBPeakDecaySpeed_ValueChanged(sender As Object, e As EventArgs) Handles TBPeakDecaySpeed.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.RainbowBarPeakDecaySpeed = TBPeakDecaySpeed.Value
    End Sub
    Private Sub TBPeakThickness_ValueChanged(sender As Object, e As EventArgs) Handles TBPeakThickness.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.RainbowBarPeakThickness = TBPeakThickness.Value
    End Sub
    Private Sub TBPeakThreshold_ValueChanged(sender As Object, e As EventArgs) Handles TBPeakThreshold.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.RainbowBarPeakThreshold = TBPeakThreshold.Value
    End Sub
    Private Sub TBHueCycleSpeed_ValueChanged(sender As Object, e As EventArgs) Handles TBHueCycleSpeed.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.RainbowBarHueCycleSpeed = TBHueCycleSpeed.Value / 10.0F
    End Sub
    Private Sub BtnCalm_Click(sender As Object, e As EventArgs) Handles BtnCalm.Click
        App.Visualizers.RainbowBarCount = 24
        App.Visualizers.RainbowBarGain = 80.0F
        App.Visualizers.RainbowBarShowPeaks = False
        App.Visualizers.RainbowBarPeakDecaySpeed = 5
        App.Visualizers.RainbowBarPeakThickness = 4
        App.Visualizers.RainbowBarPeakThreshold = 60
        App.Visualizers.RainbowBarHueCycleSpeed = 0.5F
        ShowSettings()
    End Sub
    Private Sub BtnEnergetic_Click(sender As Object, e As EventArgs) Handles BtnEnergetic.Click
        App.Visualizers.RainbowBarCount = 32
        App.Visualizers.RainbowBarGain = 120.0F
        App.Visualizers.RainbowBarShowPeaks = True
        App.Visualizers.RainbowBarPeakDecaySpeed = 7
        App.Visualizers.RainbowBarPeakThickness = 6
        App.Visualizers.RainbowBarPeakThreshold = 50
        App.Visualizers.RainbowBarHueCycleSpeed = 2.0F
        ShowSettings()
    End Sub
    Private Sub BtnExtreme_Click(sender As Object, e As EventArgs) Handles BtnExtreme.Click
        App.Visualizers.RainbowBarCount = 64
        App.Visualizers.RainbowBarGain = 200.0F
        App.Visualizers.RainbowBarShowPeaks = True
        App.Visualizers.RainbowBarPeakDecaySpeed = 10
        App.Visualizers.RainbowBarPeakThickness = 8
        App.Visualizers.RainbowBarPeakThreshold = 40
        App.Visualizers.RainbowBarHueCycleSpeed = 5.0F
        ShowSettings()
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        TxtBoxBarCount.Text = App.Visualizers.RainbowBarCount.ToString
        ChkBoxShowPeaks.Checked = App.Visualizers.RainbowBarShowPeaks
        TBGain.Value = CInt(App.Visualizers.RainbowBarGain)
        TBPeakDecaySpeed.Value = App.Visualizers.RainbowBarPeakDecaySpeed
        TBPeakThickness.Value = App.Visualizers.RainbowBarPeakThickness
        TBPeakThreshold.Value = App.Visualizers.RainbowBarPeakThreshold
        TBHueCycleSpeed.Value = CInt(App.Visualizers.RainbowBarHueCycleSpeed * 10)
        IsInitializing = False
    End Sub
    Private Sub SetAccentColor()
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            ChkBoxShowPeaks.BackColor = c
        End If
        ResumeLayout()
        Debug.Print("Options Rainbow Bar Accent Color Set")
    End Sub
    Private Sub SetTheme()
        Static forecolor As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            forecolor = App.CurrentTheme.AccentTextColor
        Else
            forecolor = App.CurrentTheme.TextColor
        End If
        TBGain.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBGain.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBGain.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBGain.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBGain.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBPeakDecaySpeed.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBPeakDecaySpeed.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBPeakDecaySpeed.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBPeakDecaySpeed.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBPeakDecaySpeed.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBPeakThickness.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBPeakThickness.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBPeakThickness.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBPeakThickness.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBPeakThickness.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBPeakThreshold.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBPeakThreshold.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBPeakThreshold.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBPeakThreshold.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBPeakThreshold.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBHueCycleSpeed.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBHueCycleSpeed.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBHueCycleSpeed.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBHueCycleSpeed.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBHueCycleSpeed.TrackBarGradientEnd = App.CurrentTheme.TextColor
        LblBarCount.ForeColor = forecolor
        LblGain.ForeColor = forecolor
        LblHueCycleSpeed.ForeColor = forecolor
        LblPeakDecaySpeed.ForeColor = forecolor
        LblPeakThickness.ForeColor = forecolor
        LblPeakThreshold.ForeColor = forecolor
        LblPresets.ForeColor = forecolor
        ChkBoxShowPeaks.ForeColor = forecolor
        TxtBoxBarCount.BackColor = App.CurrentTheme.BackColor
        TxtBoxBarCount.ForeColor = App.CurrentTheme.TextColor
        BtnCalm.BackColor = App.CurrentTheme.ButtonBackColor
        BtnCalm.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnEnergetic.BackColor = App.CurrentTheme.ButtonBackColor
        BtnEnergetic.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnExtreme.BackColor = App.CurrentTheme.ButtonBackColor
        BtnExtreme.ForeColor = App.CurrentTheme.ButtonTextColor
        TipRainbowBar.BackColor = App.CurrentTheme.BackColor
        TipRainbowBar.ForeColor = App.CurrentTheme.TextColor
        TipRainbowBar.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
    End Sub

End Class
