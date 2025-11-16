
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
    Private Sub TBSwirlSpeedBase_ValueChanged(sender As Object, e As EventArgs) Handles TBSwirlSpeedBase.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.HyperspaceTunnelSwirlSpeedBase = CDbl(TBSwirlSpeedBase.Value / 100)
    End Sub
    Private Sub TBSwirlSpeedAudioFactor_ValueChanged(sender As Object, e As EventArgs) Handles TBSwirlSpeedAudioFactor.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.HyperspaceTunnelSwirlSpeedAudioFactor = CDbl(TBSwirlSpeedAudioFactor.Value / 100)
    End Sub
    Private Sub TBParticleSpeedBase_ValueChanged(sender As Object, e As EventArgs) Handles TBParticleSpeedBase.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.HyperspaceTunnelParticleSpeedBase = CDbl(TBParticleSpeedBase.Value / 10)
    End Sub
    Private Sub TBParticleSpeedAudioFactor_ValueChanged(sender As Object, e As EventArgs) Handles TBParticleSpeedAudioFactor.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.HyperspaceTunnelParticleSpeedAudioFactor = CDbl(TBParticleSpeedAudioFactor.Value)
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        TxtBoxParticleCount.Text = App.Visualizers.HyperspaceTunnelParticleCount.ToString
        TBSwirlSpeedBase.Value = CInt(App.Visualizers.HyperspaceTunnelSwirlSpeedBase * 100)
        TBSwirlSpeedAudioFactor.Value = CInt(App.Visualizers.HyperspaceTunnelSwirlSpeedAudioFactor * 100)
        TBParticleSpeedBase.Value = CInt(App.Visualizers.HyperspaceTunnelParticleSpeedBase * 10)
        TBParticleSpeedAudioFactor.Value = CInt(App.Visualizers.HyperspaceTunnelParticleSpeedAudioFactor)
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
        TBSwirlSpeedBase.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBSwirlSpeedBase.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBSwirlSpeedBase.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBSwirlSpeedBase.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBSwirlSpeedBase.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBSwirlSpeedAudioFactor.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBSwirlSpeedAudioFactor.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBSwirlSpeedAudioFactor.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBSwirlSpeedAudioFactor.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBSwirlSpeedAudioFactor.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBParticleSpeedBase.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBParticleSpeedBase.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBParticleSpeedBase.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBParticleSpeedBase.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBParticleSpeedBase.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBParticleSpeedAudioFactor.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBParticleSpeedAudioFactor.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBParticleSpeedAudioFactor.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBParticleSpeedAudioFactor.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBParticleSpeedAudioFactor.TrackBarGradientEnd = App.CurrentTheme.TextColor
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
