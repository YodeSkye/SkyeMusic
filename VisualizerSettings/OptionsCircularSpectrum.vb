
Public Class OptionsCircularSpectrum
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
    Private Sub ChkBoxFill_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxFill.CheckedChanged
        App.Visualizers.CircularSpectrumFill = ChkBoxFill.Checked
    End Sub
    Private Sub ChkBoxAllowMiniMode_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxAllowMiniMode.CheckedChanged
        App.Visualizers.CircularSpectrumAllowMiniMode = ChkBoxAllowMiniMode.Checked
    End Sub
    Private Sub CoBoxWeightingMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxWeightingMode.SelectedIndexChanged
        App.Visualizers.CircularSpectrumWeightingMode = CType(CoBoxWeightingMode.SelectedIndex, App.VisualizerSettings.CircularSpectrumWeightingModes)
    End Sub
    Private Sub TBGain_ValueChanged(sender As Object, e As EventArgs) Handles TBGain.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.CircularSpectrumGain = CSng(TBGain.Value / 10)
    End Sub
    Private Sub TBSmoothing_ValueChanged(sender As Object, e As EventArgs) Handles TBSmoothing.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.CircularSpectrumSmoothing = CSng(TBSmoothing.Value / 100)
    End Sub
    Private Sub TBLineWidth_ValueChanged(sender As Object, e As EventArgs) Handles TBLineWidth.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.CircularSpectrumLineWidth = TBLineWidth.Value
    End Sub
    Private Sub TBRadiusFactor_ValueChanged(sender As Object, e As EventArgs) Handles TBRadiusFactor.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.CircularSpectrumRadiusFactor = CSng(TBRadiusFactor.Value / 10)
    End Sub
    Private Sub BtnClassic_Click(sender As Object, e As EventArgs) Handles BtnClassic.Click
        App.Visualizers.CircularSpectrumWeightingMode = App.VisualizerSettings.CircularSpectrumWeightingModes.Raw
        App.Visualizers.CircularSpectrumGain = 6.0F
        App.Visualizers.CircularSpectrumSmoothing = 0.3F
        App.Visualizers.CircularSpectrumLineWidth = 1
        ShowSettings()
    End Sub
    Private Sub BtnBassBoost_Click(sender As Object, e As EventArgs) Handles BtnBassBoost.Click
        App.Visualizers.CircularSpectrumWeightingMode = App.VisualizerSettings.CircularSpectrumWeightingModes.BassHeavy
        App.Visualizers.CircularSpectrumGain = 8.0F
        App.Visualizers.CircularSpectrumSmoothing = 0.25F
        App.Visualizers.CircularSpectrumLineWidth = 2
        ShowSettings()
    End Sub
    Private Sub BtnTrebleGlow_Click(sender As Object, e As EventArgs) Handles BtnTrebleGlow.Click
        App.Visualizers.CircularSpectrumWeightingMode = App.VisualizerSettings.CircularSpectrumWeightingModes.TrebleBright
        App.Visualizers.CircularSpectrumGain = 7.0F
        App.Visualizers.CircularSpectrumSmoothing = 0.2F
        App.Visualizers.CircularSpectrumLineWidth = 1
        ShowSettings()
    End Sub
    Private Sub BtnWarmAnalog_Click(sender As Object, e As EventArgs) Handles BtnWarmAnalog.Click
        App.Visualizers.CircularSpectrumWeightingMode = App.VisualizerSettings.CircularSpectrumWeightingModes.Warm
        App.Visualizers.CircularSpectrumGain = 5.0F
        App.Visualizers.CircularSpectrumSmoothing = 0.4F
        App.Visualizers.CircularSpectrumLineWidth = 2
        ShowSettings()
    End Sub
    Private Sub BtnVShapeEnergy_Click(sender As Object, e As EventArgs) Handles BtnVShapeEnergy.Click
        App.Visualizers.CircularSpectrumWeightingMode = App.VisualizerSettings.CircularSpectrumWeightingModes.VShape
        App.Visualizers.CircularSpectrumGain = 9.0F
        App.Visualizers.CircularSpectrumSmoothing = 0.2F
        App.Visualizers.CircularSpectrumLineWidth = 2
        ShowSettings()
    End Sub
    Private Sub BtnSmoothMid_Click(sender As Object, e As EventArgs) Handles BtnSmoothMid.Click
        App.Visualizers.CircularSpectrumWeightingMode = App.VisualizerSettings.CircularSpectrumWeightingModes.MidFocus
        App.Visualizers.CircularSpectrumGain = 6.0F
        App.Visualizers.CircularSpectrumSmoothing = 0.5F
        App.Visualizers.CircularSpectrumLineWidth = 1
        ShowSettings()
    End Sub
    Private Sub BtnPosterBold_Click(sender As Object, e As EventArgs) Handles BtnPosterBold.Click
        App.Visualizers.CircularSpectrumWeightingMode = App.VisualizerSettings.CircularSpectrumWeightingModes.Balanced
        App.Visualizers.CircularSpectrumGain = 10.0F
        App.Visualizers.CircularSpectrumSmoothing = 0.3F
        App.Visualizers.CircularSpectrumLineWidth = 3
        ShowSettings()
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        ChkBoxFill.Checked = App.Visualizers.CircularSpectrumFill
        ChkBoxAllowMiniMode.Checked = App.Visualizers.CircularSpectrumAllowMiniMode
        CoBoxWeightingMode.Items.Clear()
        CoBoxWeightingMode.Items.Add(App.VisualizerSettings.CircularSpectrumWeightingModes.Balanced)
        CoBoxWeightingMode.Items.Add(App.VisualizerSettings.CircularSpectrumWeightingModes.BassHeavy)
        CoBoxWeightingMode.Items.Add(App.VisualizerSettings.CircularSpectrumWeightingModes.TrebleBright)
        CoBoxWeightingMode.Items.Add(App.VisualizerSettings.CircularSpectrumWeightingModes.Raw)
        CoBoxWeightingMode.Items.Add(App.VisualizerSettings.CircularSpectrumWeightingModes.Warm)
        CoBoxWeightingMode.Items.Add(App.VisualizerSettings.CircularSpectrumWeightingModes.VShape)
        CoBoxWeightingMode.Items.Add(App.VisualizerSettings.CircularSpectrumWeightingModes.MidFocus)
        CoBoxWeightingMode.SelectedItem = App.Visualizers.CircularSpectrumWeightingMode
        TBGain.Value = CInt(App.Visualizers.CircularSpectrumGain * 10)
        TBSmoothing.Value = CInt(App.Visualizers.CircularSpectrumSmoothing * 100)
        TBLineWidth.Value = App.Visualizers.CircularSpectrumLineWidth
        TBRadiusFactor.Value = CInt(App.Visualizers.CircularSpectrumRadiusFactor * 10)
        IsInitializing = False
    End Sub
    Private Sub SetAccentColor() Implements App.IAccentable.SetAccentColor
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            ChkBoxFill.BackColor = c
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
        ChkBoxFill.ForeColor = forecolor
        ChkBoxAllowMiniMode.ForeColor = forecolor
        CoBoxWeightingMode.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxWeightingMode.ForeColor = App.CurrentTheme.TextColor
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
        TBRadiusFactor.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBRadiusFactor.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBRadiusFactor.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBRadiusFactor.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBRadiusFactor.TrackBarGradientEnd = App.CurrentTheme.TextColor
        LblPresets.ForeColor = forecolor
        LblWeightingMode.ForeColor = forecolor
        LblGain.ForeColor = forecolor
        LblSmoothing.ForeColor = forecolor
        LblLineWidth.ForeColor = forecolor
        LblRadiusFactor.ForeColor = forecolor
        BtnBassBoost.BackColor = App.CurrentTheme.ButtonBackColor
        BtnBassBoost.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnClassic.BackColor = App.CurrentTheme.ButtonBackColor
        BtnClassic.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnPosterBold.BackColor = App.CurrentTheme.ButtonBackColor
        BtnPosterBold.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnSmoothMid.BackColor = App.CurrentTheme.ButtonBackColor
        BtnSmoothMid.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnTrebleGlow.BackColor = App.CurrentTheme.ButtonBackColor
        BtnTrebleGlow.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnVShapeEnergy.BackColor = App.CurrentTheme.ButtonBackColor
        BtnVShapeEnergy.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnWarmAnalog.BackColor = App.CurrentTheme.ButtonBackColor
        BtnWarmAnalog.ForeColor = App.CurrentTheme.ButtonTextColor
        TipCircularSpectrum.BackColor = App.CurrentTheme.BackColor
        TipCircularSpectrum.ForeColor = App.CurrentTheme.TextColor
        TipCircularSpectrum.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
    End Sub

End Class
