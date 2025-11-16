
Public Class OptionsHyperspaceTunnel

    ' Declarations
    Private IsInitializing As Boolean = True

    ' Form Events
    Private Sub OptionsRainbowBar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetAccentColor()
        SetTheme()
        ShowSettings()
    End Sub

    ' Control Events
    Private Sub TxtBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxParticleCount.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            Validate()
        End If
    End Sub
    Private Sub TxtBoxNumbersOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TxtBoxParticleCount.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then e.Handled = True
    End Sub
    Private Sub TxtBoxBarCount_Validated(sender As Object, e As EventArgs) Handles TxtBoxParticleCount.Validated
        App.Visualizers.HyperspaceTunnelParticleCount = CInt(TxtBoxParticleCount.Text)
        App.Visualizers.HyperspaceTunnelParticleCount = Math.Max(100, Math.Min(5000, App.Visualizers.HyperspaceTunnelParticleCount))
        TxtBoxParticleCount.Text = App.Visualizers.HyperspaceTunnelParticleCount.ToString
        Player.VisualizerHost.SetHyperspaceTunnelParticleCount(App.Visualizers.HyperspaceTunnelParticleCount)
    End Sub
    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        TxtBoxParticleCount.Text = App.Visualizers.HyperspaceTunnelParticleCount.ToString
        'ChkBoxShowPeaks.Checked = App.Visualizers.RainbowBarShowPeaks
        'TBGain.Value = CInt(App.Visualizers.RainbowBarGain)
        'TBPeakDecaySpeed.Value = App.Visualizers.RainbowBarPeakDecaySpeed
        'TBPeakThickness.Value = App.Visualizers.RainbowBarPeakThickness
        'TBPeakThreshold.Value = App.Visualizers.RainbowBarPeakThreshold
        'TBHueCycleSpeed.Value = CInt(App.Visualizers.RainbowBarHueCycleSpeed * 10)
        IsInitializing = False
    End Sub
    Private Sub SetAccentColor()
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
        End If
        ResumeLayout()
        'Debug.Print("Options Hyperspace Tunnel Accent Color Set")
    End Sub
    Private Sub SetTheme()
        Static forecolor As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            forecolor = App.CurrentTheme.AccentTextColor
        Else
            forecolor = App.CurrentTheme.TextColor
        End If
        'TBGain.ButtonColor = App.CurrentTheme.ButtonBackColor
        'TBGain.HighlightedButtonColor = App.CurrentTheme.TextColor
        'TBGain.PushedButtonEndColor = App.CurrentTheme.TextColor
        'TBGain.TrackBarGradientStart = App.CurrentTheme.BackColor
        'TBGain.TrackBarGradientEnd = App.CurrentTheme.TextColor
        LblParticleCount.ForeColor = forecolor
        LblSwirlSpeedBase.ForeColor = forecolor
        LblSwirlSpeedAudioFactor.ForeColor = forecolor
        LblParticleSpeedBase.ForeColor = forecolor
        LblParticleSpeedAudioFactor.ForeColor = forecolor
        TxtBoxParticleCount.BackColor = App.CurrentTheme.BackColor
        TxtBoxParticleCount.ForeColor = App.CurrentTheme.TextColor
        TipHyperspaceTunnel.BackColor = App.CurrentTheme.BackColor
        TipHyperspaceTunnel.ForeColor = App.CurrentTheme.TextColor
        TipHyperspaceTunnel.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
    End Sub

End Class
