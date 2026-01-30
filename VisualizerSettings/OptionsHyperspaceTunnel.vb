
Public Class OptionsHyperspaceTunnel
    Implements App.IAccentable

    ' Declarations
    Private IsInitializing As Boolean = True

    ' Form Events
    Private Sub OptionsRainbowBar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetAccentColor()
        SetTheme()
        ShowSettings()
    End Sub

    ' Control Events
    Private Sub ChkBoxAllowMiniMode_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxAllowMiniMode.CheckedChanged
        App.Settings.Visualizers.HyperspaceTunnelAllowMiniMode = ChkBoxAllowMiniMode.Checked
    End Sub
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
        App.Settings.Visualizers.HyperspaceTunnelParticleCount = CInt(TxtBoxParticleCount.Text)
        App.Settings.Visualizers.HyperspaceTunnelParticleCount = Math.Max(100, Math.Min(5000, App.Settings.Visualizers.HyperspaceTunnelParticleCount))
        TxtBoxParticleCount.Text = App.Settings.Visualizers.HyperspaceTunnelParticleCount.ToString
        Player.VisualizerHost.SetHyperspaceTunnelParticleCount(App.Settings.Visualizers.HyperspaceTunnelParticleCount)
    End Sub
    Private Sub TBSwirlSpeedBase_ValueChanged(sender As Object, e As EventArgs) Handles TBSwirlSpeedBase.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.HyperspaceTunnelSwirlSpeedBase = CDbl(TBSwirlSpeedBase.Value / 100)
    End Sub
    Private Sub TBSwirlSpeedAudioFactor_ValueChanged(sender As Object, e As EventArgs) Handles TBSwirlSpeedAudioFactor.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.HyperspaceTunnelSwirlSpeedAudioFactor = CDbl(TBSwirlSpeedAudioFactor.Value / 100)
    End Sub
    Private Sub TBParticleSpeedBase_ValueChanged(sender As Object, e As EventArgs) Handles TBParticleSpeedBase.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.HyperspaceTunnelParticleSpeedBase = CDbl(TBParticleSpeedBase.Value / 10)
    End Sub
    Private Sub TBParticleSpeedAudioFactor_ValueChanged(sender As Object, e As EventArgs) Handles TBParticleSpeedAudioFactor.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.HyperspaceTunnelParticleSpeedAudioFactor = CDbl(TBParticleSpeedAudioFactor.Value)
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        ChkBoxAllowMiniMode.Checked = App.Settings.Visualizers.HyperspaceTunnelAllowMiniMode
        TxtBoxParticleCount.Text = App.Settings.Visualizers.HyperspaceTunnelParticleCount.ToString
        TBSwirlSpeedBase.Value = CInt(App.Settings.Visualizers.HyperspaceTunnelSwirlSpeedBase * 100)
        TBSwirlSpeedAudioFactor.Value = CInt(App.Settings.Visualizers.HyperspaceTunnelSwirlSpeedAudioFactor * 100)
        TBParticleSpeedBase.Value = CInt(App.Settings.Visualizers.HyperspaceTunnelParticleSpeedBase * 10)
        TBParticleSpeedAudioFactor.Value = CInt(App.Settings.Visualizers.HyperspaceTunnelParticleSpeedAudioFactor)
        IsInitializing = False
    End Sub
    Private Sub SetAccentColor() Implements App.IAccentable.SetAccentColor
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            ChkBoxAllowMiniMode.BackColor = c
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
        ChkBoxAllowMiniMode.ForeColor = forecolor
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
