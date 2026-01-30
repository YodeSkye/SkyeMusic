
Imports Microsoft.Win32

Public Class OptionsParticleNebula
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
    Private Sub CoBoxActivePalettePreset_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxActivePalettePreset.SelectedIndexChanged
        App.Settings.Visualizers.ParticleNebulaActivePalettePreset = CType(CoBoxActivePalettePreset.SelectedIndex, App.ParticleNebulaPalettePresets)
        App.ParticleNebulaActivePalette = App.ParticleNebulaGetPalette(App.Settings.Visualizers.ParticleNebulaActivePalettePreset)
    End Sub
    Private Sub ChkBoxFadeTrails_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxFadeTrails.CheckedChanged
        App.Settings.Visualizers.ParticleNebulaFadeTrails = ChkBoxFadeTrails.Checked
    End Sub
    Private Sub ChkBoxRainbowColors_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxRainbowColors.CheckedChanged
        App.Settings.Visualizers.ParticleNebulaRainbowColors = ChkBoxRainbowColors.Checked
    End Sub
    Private Sub ChkBoxShowBloom_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxShowBloom.CheckedChanged
        App.Settings.Visualizers.ParticleNebulaShowBloom = ChkBoxShowBloom.Checked
    End Sub
    Private Sub ChkBoxShowTrails_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxShowTrails.CheckedChanged
        App.Settings.Visualizers.ParticleNebulaShowTrails = ChkBoxShowTrails.Checked
    End Sub
    Private Sub ChkBoxAllowMiniMode_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxAllowMiniMode.CheckedChanged
        App.Settings.Visualizers.ParticleNebulaAllowMiniMode = ChkBoxAllowMiniMode.Checked
    End Sub
    Private Sub TBBloomIntensity_ValueChanged(sender As Object, e As EventArgs) Handles TBBloomIntensity.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ParticleNebulaBloomIntensity = CSng(TBBloomIntensity.Value / 10)
    End Sub
    Private Sub TBBloomRadius_ValueChanged(sender As Object, e As EventArgs) Handles TBBloomRadius.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ParticleNebulaBloomRadius = TBBloomRadius.Value
    End Sub
    Private Sub TBFadeRate_ValueChanged(sender As Object, e As EventArgs) Handles TBFadeRate.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ParticleNebulaFadeRate = CSng(TBFadeRate.Value / 10000)
    End Sub
    Private Sub TBSizeScale_ValueChanged(sender As Object, e As EventArgs) Handles TBSizeScale.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ParticleNebulaSizeScale = TBSizeScale.Value
    End Sub
    Private Sub TBSpawnMultiplier_ValueChanged(sender As Object, e As EventArgs) Handles TBSpawnMultiplier.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ParticleNebulaSpawnMultiplier = CSng(TBSpawnMultiplier.Value / 10)
    End Sub
    Private Sub TBSwirlBias_ValueChanged(sender As Object, e As EventArgs) Handles TBSwirlBias.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ParticleNebulaSwirlBias = CSng((TBSwirlBias.Value / 100) - 1)
    End Sub
    Private Sub TBSwirlStrength_ValueChanged(sender As Object, e As EventArgs) Handles TBSwirlStrength.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ParticleNebulaSwirlStrength = CSng(TBSwirlStrength.Value / 100)
    End Sub
    Private Sub TBTrailAlpha_ValueChanged(sender As Object, e As EventArgs) Handles TBTrailAlpha.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ParticleNebulaTrailAlpha = CSng(TBTrailAlpha.Value / 100)
    End Sub
    Private Sub TBVelocityScale_ValueChanged(sender As Object, e As EventArgs) Handles TBVelocityScale.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ParticleNebulaVelocityScale = TBVelocityScale.Value
    End Sub
    Private Sub BtnDefaults_Click(sender As Object, e As EventArgs) Handles BtnDefaults.Click
        App.Settings.Visualizers.ParticleNebulaActivePalettePreset = App.ParticleNebulaPalettePresets.Cosmic
        App.Settings.Visualizers.ParticleNebulaBloomIntensity = 0.5F
        App.Settings.Visualizers.ParticleNebulaBloomRadius = 2
        App.Settings.Visualizers.ParticleNebulaFadeRate = 0.005F
        App.Settings.Visualizers.ParticleNebulaFadeTrails = False
        App.Settings.Visualizers.ParticleNebulaRainbowColors = False
        App.Settings.Visualizers.ParticleNebulaShowBloom = False
        App.Settings.Visualizers.ParticleNebulaShowTrails = False
        App.Settings.Visualizers.ParticleNebulaSizeScale = 20
        App.Settings.Visualizers.ParticleNebulaSpawnMultiplier = 2.0F
        App.Settings.Visualizers.ParticleNebulaSwirlBias = 0.0F
        App.Settings.Visualizers.ParticleNebulaSwirlStrength = 0.15F
        App.Settings.Visualizers.ParticleNebulaTrailAlpha = 0.5F
        App.Settings.Visualizers.ParticleNebulaVelocityScale = 50
        ShowSettings()
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        CoBoxActivePalettePreset.Items.Clear()
        CoBoxActivePalettePreset.Items.Add(App.ParticleNebulaPalettePresets.Cosmic)
        CoBoxActivePalettePreset.Items.Add(App.ParticleNebulaPalettePresets.Firestorm)
        CoBoxActivePalettePreset.Items.Add(App.ParticleNebulaPalettePresets.Oceanic)
        CoBoxActivePalettePreset.Items.Add(App.ParticleNebulaPalettePresets.Aurora)
        CoBoxActivePalettePreset.Items.Add(App.ParticleNebulaPalettePresets.MonochromeGlow)
        CoBoxActivePalettePreset.SelectedItem = App.Settings.Visualizers.ParticleNebulaActivePalettePreset
        TBBloomIntensity.Value = CInt(App.Settings.Visualizers.ParticleNebulaBloomIntensity * 10)
        TBBloomRadius.Value = App.Settings.Visualizers.ParticleNebulaBloomRadius
        TBFadeRate.Value = CInt(App.Settings.Visualizers.ParticleNebulaFadeRate * 10000)
        ChkBoxFadeTrails.Checked = App.Settings.Visualizers.ParticleNebulaFadeTrails
        ChkBoxRainbowColors.Checked = App.Settings.Visualizers.ParticleNebulaRainbowColors
        ChkBoxShowBloom.Checked = App.Settings.Visualizers.ParticleNebulaShowBloom
        ChkBoxShowTrails.Checked = App.Settings.Visualizers.ParticleNebulaShowTrails
        ChkBoxAllowMiniMode.Checked = App.Settings.Visualizers.ParticleNebulaAllowMiniMode
        TBSizeScale.Value = App.Settings.Visualizers.ParticleNebulaSizeScale
        TBSpawnMultiplier.Value = CInt(App.Settings.Visualizers.ParticleNebulaSpawnMultiplier * 10)
        TBSwirlBias.Value = CInt((App.Settings.Visualizers.ParticleNebulaSwirlBias + 1) * 100)
        TBSwirlStrength.Value = CInt(App.Settings.Visualizers.ParticleNebulaSwirlStrength * 100)
        TBTrailAlpha.Value = CInt(App.Settings.Visualizers.ParticleNebulaTrailAlpha * 100)
        TBVelocityScale.Value = App.Settings.Visualizers.ParticleNebulaVelocityScale
        IsInitializing = False
    End Sub
    Private Sub SetAccentColor() Implements App.IAccentable.SetAccentColor
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            ChkBoxFadeTrails.BackColor = c
            ChkBoxRainbowColors.BackColor = c
            ChkBoxShowBloom.BackColor = c
            ChkBoxShowTrails.BackColor = c
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
        ChkBoxFadeTrails.ForeColor = forecolor
        ChkBoxRainbowColors.ForeColor = forecolor
        ChkBoxShowBloom.ForeColor = forecolor
        ChkBoxShowTrails.ForeColor = forecolor
        ChkBoxAllowMiniMode.ForeColor = forecolor
        CoBoxActivePalettePreset.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxActivePalettePreset.ForeColor = App.CurrentTheme.TextColor
        TBBloomIntensity.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBBloomIntensity.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBBloomIntensity.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBBloomIntensity.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBBloomIntensity.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBBloomRadius.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBBloomRadius.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBBloomRadius.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBBloomRadius.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBBloomRadius.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBFadeRate.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBFadeRate.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBFadeRate.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBFadeRate.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBFadeRate.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBSizeScale.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBSizeScale.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBSizeScale.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBSizeScale.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBSizeScale.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBSpawnMultiplier.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBSpawnMultiplier.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBSpawnMultiplier.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBSpawnMultiplier.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBSpawnMultiplier.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBSwirlBias.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBSwirlBias.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBSwirlBias.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBSwirlBias.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBSwirlBias.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBSwirlStrength.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBSwirlStrength.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBSwirlStrength.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBSwirlStrength.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBSwirlStrength.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBTrailAlpha.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBTrailAlpha.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBTrailAlpha.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBTrailAlpha.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBTrailAlpha.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBVelocityScale.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBVelocityScale.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBVelocityScale.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBVelocityScale.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBVelocityScale.TrackBarGradientEnd = App.CurrentTheme.TextColor
        LblActivePalettePreset.ForeColor = forecolor
        LblBloomIntensity.ForeColor = forecolor
        LblBloomRadius.ForeColor = forecolor
        LblFadeRate.ForeColor = forecolor
        LblSizeScale.ForeColor = forecolor
        LblSpawnMultiplier.ForeColor = forecolor
        LblSwirlBias.ForeColor = forecolor
        LblSwirlStrength.ForeColor = forecolor
        LblTrailAlpha.ForeColor = forecolor
        LblVelocityScale.ForeColor = forecolor
        BtnDefaults.BackColor = App.CurrentTheme.ButtonBackColor
        BtnDefaults.ForeColor = App.CurrentTheme.ButtonTextColor
        TipParticleNebula.BackColor = App.CurrentTheme.BackColor
        TipParticleNebula.ForeColor = App.CurrentTheme.TextColor
        TipParticleNebula.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
    End Sub

End Class
