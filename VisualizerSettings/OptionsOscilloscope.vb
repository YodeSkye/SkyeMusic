
Public Class OptionsOscilloscope
    Implements App.IAccentable

    ' Declarations
    Private IsInitializing As Boolean = True

    ' Form Events
    Private Sub OptionsWaveform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetAccentColor()
        SetTheme()
        ShowSettings()
    End Sub

    ' Control Events
    Private Sub ChkBoxGlow_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxGlow.CheckedChanged
        App.Settings.Visualizers.OscilloscopeEnableGlow = ChkBoxGlow.Checked
    End Sub
    Private Sub ChkBoxAllowMiniMode_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxAllowMiniMode.CheckedChanged
        App.Settings.Visualizers.OscilloscopeAllowMiniMode = ChkBoxAllowMiniMode.Checked
    End Sub
    Private Sub CoBoxBandMappingMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxChannelMode.SelectedIndexChanged
        App.Settings.Visualizers.OscilloscopeChannelMode = CType(CoBoxChannelMode.SelectedIndex, App.OscilloscopeChannelModes)
    End Sub
    Private Sub TBGain_ValueChanged(sender As Object, e As EventArgs) Handles TBGain.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.OscilloscopeGain = CSng(TBGain.Value / 10)
    End Sub
    Private Sub TBSmoothing_ValueChanged(sender As Object, e As EventArgs) Handles TBSmoothing.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.OscilloscopeSmoothing = CSng(TBSmoothing.Value / 100)
    End Sub
    Private Sub TBLineWidth_ValueChanged(sender As Object, e As EventArgs) Handles TBLineWidth.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.OscilloscopeLineWidth = CSng(TBLineWidth.Value / 10)
    End Sub
    Private Sub TBFadeAlpha_ValueChanged(sender As Object, e As EventArgs) Handles TBFadeAlpha.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.OscilloscopeFadeAlpha = TBFadeAlpha.Value
    End Sub
    Private Sub BtnClean_Click(sender As Object, e As EventArgs) Handles BtnClean.Click
        App.Settings.Visualizers.OscilloscopeChannelMode = App.OscilloscopeChannelModes.Mono
        App.Settings.Visualizers.OscilloscopeGain = 1.0F
        App.Settings.Visualizers.OscilloscopeSmoothing = 0.1F
        App.Settings.Visualizers.OscilloscopeLineWidth = 1.0F
        App.Settings.Visualizers.OscilloscopeEnableGlow = False
        App.Settings.Visualizers.OscilloscopeFadeAlpha = 48
        ShowSettings()
    End Sub
    Private Sub BtnSoftGlow_Click(sender As Object, e As EventArgs) Handles BtnSoftGlow.Click
        App.Settings.Visualizers.OscilloscopeChannelMode = App.OscilloscopeChannelModes.StereoLeft
        App.Settings.Visualizers.OscilloscopeGain = 1.2F
        App.Settings.Visualizers.OscilloscopeSmoothing = 0.3F
        App.Settings.Visualizers.OscilloscopeLineWidth = 1.5F
        App.Settings.Visualizers.OscilloscopeEnableGlow = True
        App.Settings.Visualizers.OscilloscopeFadeAlpha = 96
        ShowSettings()
    End Sub
    Private Sub BtnDreamy_Click(sender As Object, e As EventArgs) Handles BtnDreamy.Click
        App.Settings.Visualizers.OscilloscopeChannelMode = App.OscilloscopeChannelModes.StereoBoth
        App.Settings.Visualizers.OscilloscopeGain = 1.5F
        App.Settings.Visualizers.OscilloscopeSmoothing = 0.6F
        App.Settings.Visualizers.OscilloscopeLineWidth = 2.0F
        App.Settings.Visualizers.OscilloscopeEnableGlow = True
        App.Settings.Visualizers.OscilloscopeFadeAlpha = 32
        ShowSettings()
    End Sub
    Private Sub BtnPunchy_Click(sender As Object, e As EventArgs) Handles BtnPunchy.Click
        App.Settings.Visualizers.OscilloscopeChannelMode = App.OscilloscopeChannelModes.Mono
        App.Settings.Visualizers.OscilloscopeGain = 2.0F
        App.Settings.Visualizers.OscilloscopeSmoothing = 0.2F
        App.Settings.Visualizers.OscilloscopeLineWidth = 2.5F
        App.Settings.Visualizers.OscilloscopeEnableGlow = False
        App.Settings.Visualizers.OscilloscopeFadeAlpha = 48
        ShowSettings()
    End Sub
    Private Sub BtnLiquid_Click(sender As Object, e As EventArgs) Handles BtnLiquid.Click
        App.Settings.Visualizers.OscilloscopeChannelMode = App.OscilloscopeChannelModes.StereoRight
        App.Settings.Visualizers.OscilloscopeGain = 1.0F
        App.Settings.Visualizers.OscilloscopeSmoothing = 0.9F
        App.Settings.Visualizers.OscilloscopeLineWidth = 1.2F
        App.Settings.Visualizers.OscilloscopeEnableGlow = True
        App.Settings.Visualizers.OscilloscopeFadeAlpha = 64
        ShowSettings()
    End Sub
    Private Sub BtnNeonPulse_Click(sender As Object, e As EventArgs) Handles BtnNeonPulse.Click
        App.Settings.Visualizers.OscilloscopeChannelMode = App.OscilloscopeChannelModes.StereoBoth
        App.Settings.Visualizers.OscilloscopeGain = 1.8F
        App.Settings.Visualizers.OscilloscopeSmoothing = 0.4F
        App.Settings.Visualizers.OscilloscopeLineWidth = 2.0F
        App.Settings.Visualizers.OscilloscopeEnableGlow = True
        App.Settings.Visualizers.OscilloscopeFadeAlpha = 48
        ShowSettings()
    End Sub
    Private Sub BtnMinimal_Click(sender As Object, e As EventArgs) Handles BtnMinimal.Click
        App.Settings.Visualizers.OscilloscopeChannelMode = App.OscilloscopeChannelModes.Mono
        App.Settings.Visualizers.OscilloscopeGain = 0.8F
        App.Settings.Visualizers.OscilloscopeSmoothing = 0.2F
        App.Settings.Visualizers.OscilloscopeLineWidth = 0.8F
        App.Settings.Visualizers.OscilloscopeEnableGlow = False
        App.Settings.Visualizers.OscilloscopeFadeAlpha = 48
        ShowSettings()
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        ChkBoxGlow.Checked = App.Settings.Visualizers.OscilloscopeEnableGlow
        ChkBoxAllowMiniMode.Checked = App.Settings.Visualizers.OscilloscopeAllowMiniMode
        CoBoxChannelMode.Items.Clear()
        CoBoxChannelMode.Items.Add(App.OscilloscopeChannelModes.Mono)
        CoBoxChannelMode.Items.Add(App.OscilloscopeChannelModes.StereoLeft)
        CoBoxChannelMode.Items.Add(App.OscilloscopeChannelModes.StereoRight)
        CoBoxChannelMode.Items.Add(App.OscilloscopeChannelModes.StereoBoth)
        CoBoxChannelMode.SelectedItem = App.Settings.Visualizers.OscilloscopeChannelMode
        TBGain.Value = CInt(App.Settings.Visualizers.OscilloscopeGain * 10)
        TBSmoothing.Value = CInt(App.Settings.Visualizers.OscilloscopeSmoothing * 100)
        TBLineWidth.Value = CInt(App.Settings.Visualizers.OscilloscopeLineWidth * 10)
        TBFadeAlpha.Value = App.Settings.Visualizers.OscilloscopeFadeAlpha
        IsInitializing = False
    End Sub
    Private Sub SetAccentColor() Implements App.IAccentable.SetAccentColor
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            ChkBoxGlow.BackColor = c
            ChkBoxAllowMiniMode.BackColor = c
        End If
        ResumeLayout()
    End Sub
    Private Sub SetTheme()
        Static forecolor As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            forecolor = App.CurrentTheme.AccentTextColor
        Else
            forecolor = App.CurrentTheme.TextColor
        End If
        ChkBoxGlow.ForeColor = forecolor
        ChkBoxAllowMiniMode.ForeColor = forecolor
        CoBoxChannelMode.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxChannelMode.ForeColor = App.CurrentTheme.TextColor
        TBGain.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBGain.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBGain.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBGain.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBGain.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBSmoothing.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBSmoothing.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBSmoothing.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBSmoothing.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBSmoothing.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBLineWidth.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBLineWidth.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBLineWidth.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBLineWidth.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBLineWidth.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBFadeAlpha.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBFadeAlpha.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBFadeAlpha.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBFadeAlpha.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBFadeAlpha.TrackBarGradientEnd = App.CurrentTheme.TextColor
        LblPresets.ForeColor = forecolor
        LblChannelMode.ForeColor = forecolor
        LblGain.ForeColor = forecolor
        LblSmoothing.ForeColor = forecolor
        LblLineWidth.ForeColor = forecolor
        LblFadeAlpha.ForeColor = forecolor
        BtnClean.BackColor = App.CurrentTheme.ButtonBackColor
        BtnClean.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnDreamy.BackColor = App.CurrentTheme.ButtonBackColor
        BtnDreamy.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnLiquid.BackColor = App.CurrentTheme.ButtonBackColor
        BtnLiquid.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnMinimal.BackColor = App.CurrentTheme.ButtonBackColor
        BtnMinimal.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnNeonPulse.BackColor = App.CurrentTheme.ButtonBackColor
        BtnNeonPulse.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnPunchy.BackColor = App.CurrentTheme.ButtonBackColor
        BtnPunchy.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnSoftGlow.BackColor = App.CurrentTheme.ButtonBackColor
        BtnSoftGlow.ForeColor = App.CurrentTheme.ButtonTextColor
        TipOscilloscope.BackColor = App.CurrentTheme.BackColor
        TipOscilloscope.ForeColor = App.CurrentTheme.TextColor
        TipOscilloscope.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
    End Sub

End Class
