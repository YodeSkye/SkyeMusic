
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
        App.Settings.Visualizers.CircularSpectrumFill = ChkBoxFill.Checked
    End Sub
    Private Sub ChkBoxAllowMiniMode_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxAllowMiniMode.CheckedChanged
        App.Settings.Visualizers.CircularSpectrumAllowMiniMode = ChkBoxAllowMiniMode.Checked
    End Sub
    Private Sub CoBoxWeightingMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxWeightingMode.SelectedIndexChanged
        App.Settings.Visualizers.CircularSpectrumWeightingMode = CType(CoBoxWeightingMode.SelectedIndex, App.CircularSpectrumWeightingModes)
    End Sub
    Private Sub TBGain_ValueChanged(sender As Object, e As EventArgs) Handles TBGain.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.CircularSpectrumGain = CSng(TBGain.Value / 10)
    End Sub
    Private Sub TBSmoothing_ValueChanged(sender As Object, e As EventArgs) Handles TBSmoothing.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.CircularSpectrumSmoothing = CSng(TBSmoothing.Value / 100)
    End Sub
    Private Sub TBLineWidth_ValueChanged(sender As Object, e As EventArgs) Handles TBLineWidth.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.CircularSpectrumLineWidth = TBLineWidth.Value
    End Sub
    Private Sub TBRadiusFactor_ValueChanged(sender As Object, e As EventArgs) Handles TBRadiusFactor.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.CircularSpectrumRadiusFactor = CSng(TBRadiusFactor.Value / 10)
    End Sub
    Private Sub BtnClassic_Click(sender As Object, e As EventArgs) Handles BtnClassic.Click
        App.Settings.Visualizers.CircularSpectrumWeightingMode = App.CircularSpectrumWeightingModes.Raw
        App.Settings.Visualizers.CircularSpectrumGain = 6.0F
        App.Settings.Visualizers.CircularSpectrumSmoothing = 0.3F
        App.Settings.Visualizers.CircularSpectrumLineWidth = 1
        ShowSettings()
    End Sub
    Private Sub BtnBassBoost_Click(sender As Object, e As EventArgs) Handles BtnBassBoost.Click
        App.Settings.Visualizers.CircularSpectrumWeightingMode = App.CircularSpectrumWeightingModes.BassHeavy
        App.Settings.Visualizers.CircularSpectrumGain = 8.0F
        App.Settings.Visualizers.CircularSpectrumSmoothing = 0.25F
        App.Settings.Visualizers.CircularSpectrumLineWidth = 2
        ShowSettings()
    End Sub
    Private Sub BtnTrebleGlow_Click(sender As Object, e As EventArgs) Handles BtnTrebleGlow.Click
        App.Settings.Visualizers.CircularSpectrumWeightingMode = App.CircularSpectrumWeightingModes.TrebleBright
        App.Settings.Visualizers.CircularSpectrumGain = 7.0F
        App.Settings.Visualizers.CircularSpectrumSmoothing = 0.2F
        App.Settings.Visualizers.CircularSpectrumLineWidth = 1
        ShowSettings()
    End Sub
    Private Sub BtnWarmAnalog_Click(sender As Object, e As EventArgs) Handles BtnWarmAnalog.Click
        App.Settings.Visualizers.CircularSpectrumWeightingMode = App.CircularSpectrumWeightingModes.Warm
        App.Settings.Visualizers.CircularSpectrumGain = 5.0F
        App.Settings.Visualizers.CircularSpectrumSmoothing = 0.4F
        App.Settings.Visualizers.CircularSpectrumLineWidth = 2
        ShowSettings()
    End Sub
    Private Sub BtnVShapeEnergy_Click(sender As Object, e As EventArgs) Handles BtnVShapeEnergy.Click
        App.Settings.Visualizers.CircularSpectrumWeightingMode = App.CircularSpectrumWeightingModes.VShape
        App.Settings.Visualizers.CircularSpectrumGain = 9.0F
        App.Settings.Visualizers.CircularSpectrumSmoothing = 0.2F
        App.Settings.Visualizers.CircularSpectrumLineWidth = 2
        ShowSettings()
    End Sub
    Private Sub BtnSmoothMid_Click(sender As Object, e As EventArgs) Handles BtnSmoothMid.Click
        App.Settings.Visualizers.CircularSpectrumWeightingMode = App.CircularSpectrumWeightingModes.MidFocus
        App.Settings.Visualizers.CircularSpectrumGain = 6.0F
        App.Settings.Visualizers.CircularSpectrumSmoothing = 0.5F
        App.Settings.Visualizers.CircularSpectrumLineWidth = 1
        ShowSettings()
    End Sub
    Private Sub BtnPosterBold_Click(sender As Object, e As EventArgs) Handles BtnPosterBold.Click
        App.Settings.Visualizers.CircularSpectrumWeightingMode = App.CircularSpectrumWeightingModes.Balanced
        App.Settings.Visualizers.CircularSpectrumGain = 10.0F
        App.Settings.Visualizers.CircularSpectrumSmoothing = 0.3F
        App.Settings.Visualizers.CircularSpectrumLineWidth = 3
        ShowSettings()
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        ChkBoxFill.Checked = App.Settings.Visualizers.CircularSpectrumFill
        ChkBoxAllowMiniMode.Checked = App.Settings.Visualizers.CircularSpectrumAllowMiniMode
        CoBoxWeightingMode.Items.Clear()
        CoBoxWeightingMode.Items.Add(App.CircularSpectrumWeightingModes.Balanced)
        CoBoxWeightingMode.Items.Add(App.CircularSpectrumWeightingModes.BassHeavy)
        CoBoxWeightingMode.Items.Add(App.CircularSpectrumWeightingModes.TrebleBright)
        CoBoxWeightingMode.Items.Add(App.CircularSpectrumWeightingModes.Raw)
        CoBoxWeightingMode.Items.Add(App.CircularSpectrumWeightingModes.Warm)
        CoBoxWeightingMode.Items.Add(App.CircularSpectrumWeightingModes.VShape)
        CoBoxWeightingMode.Items.Add(App.CircularSpectrumWeightingModes.MidFocus)
        CoBoxWeightingMode.SelectedItem = App.Settings.Visualizers.CircularSpectrumWeightingMode
        TBGain.Value = CInt(App.Settings.Visualizers.CircularSpectrumGain * 10)
        TBSmoothing.Value = CInt(App.Settings.Visualizers.CircularSpectrumSmoothing * 100)
        TBLineWidth.Value = App.Settings.Visualizers.CircularSpectrumLineWidth
        TBRadiusFactor.Value = CInt(App.Settings.Visualizers.CircularSpectrumRadiusFactor * 10)
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
