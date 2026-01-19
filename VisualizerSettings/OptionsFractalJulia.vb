
Public Class OptionsFractalJulia
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
    Private Sub ChkBoxAllowMiniMode_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxAllowMiniMode.CheckedChanged
        App.Visualizers.JuliaFractalAllowMiniMode = ChkBoxAllowMiniMode.Checked
    End Sub
    Private Sub TBBaseCX_ValueChanged(sender As Object, e As EventArgs) Handles TBBaseCX.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.JuliaFractalBaseCX = CSng(TBBaseCX.Value / 100) - 1
    End Sub
    Private Sub TBBaseCY_ValueChanged(sender As Object, e As EventArgs) Handles TBBaseCY.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.JuliaFractalBaseCY = CSng(TBBaseCY.Value / 100) - 1
    End Sub
    Private Sub TBBassInfluence_ValueChanged(sender As Object, e As EventArgs) Handles TBBassInfluence.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.JuliaFractalBassInfluence = CSng(TBBassInfluence.Value / 10)
    End Sub
    Private Sub TBMidInfluence_ValueChanged(sender As Object, e As EventArgs) Handles TBMidInfluence.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.JuliaFractalMidInfluence = CSng(TBMidInfluence.Value / 10)
    End Sub
    Private Sub TBMaxIterations_ValueChanged(sender As Object, e As EventArgs) Handles TBMaxIterations.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.JuliaFractalMaxIterations = TBMaxIterations.Value
    End Sub
    Private Sub BtnCalmOcean_Click(sender As Object, e As EventArgs) Handles BtnCalmOcean.Click
        App.Visualizers.JuliaFractalBaseCX = -0.7F
        App.Visualizers.JuliaFractalBaseCY = 0.27015F
        App.Visualizers.JuliaFractalBassInfluence = 0.2F
        App.Visualizers.JuliaFractalMidInfluence = 0.5F
        App.Visualizers.JuliaFractalMaxIterations = 85
        ShowSettings()
    End Sub
    Private Sub BtnFirestorm_Click(sender As Object, e As EventArgs) Handles BtnFirestorm.Click
        App.Visualizers.JuliaFractalBaseCX = -0.5F
        App.Visualizers.JuliaFractalBaseCY = 0.3F
        App.Visualizers.JuliaFractalBassInfluence = 0.5F
        App.Visualizers.JuliaFractalMidInfluence = 2.0F
        App.Visualizers.JuliaFractalMaxIterations = 150
        ShowSettings()
    End Sub
    Private Sub BtnCrystalGrid_Click(sender As Object, e As EventArgs) Handles BtnCrystalGrid.Click
        App.Visualizers.JuliaFractalBaseCX = 0.355F
        App.Visualizers.JuliaFractalBaseCY = 0.355F
        App.Visualizers.JuliaFractalBassInfluence = 0.1F
        App.Visualizers.JuliaFractalMidInfluence = 0.1F
        App.Visualizers.JuliaFractalMaxIterations = 250
        ShowSettings()
    End Sub
    Private Sub BtnBassquake_Click(sender As Object, e As EventArgs) Handles BtnBassquake.Click
        App.Visualizers.JuliaFractalBaseCX = -0.8F
        App.Visualizers.JuliaFractalBaseCY = 0.156F
        App.Visualizers.JuliaFractalBassInfluence = 1.0F
        App.Visualizers.JuliaFractalMidInfluence = 0.2F
        App.Visualizers.JuliaFractalMaxIterations = 100
        ShowSettings()
    End Sub
    Private Sub BtnTreblePulse_Click(sender As Object, e As EventArgs) Handles BtnTreblePulse.Click
        App.Visualizers.JuliaFractalBaseCX = -0.7F
        App.Visualizers.JuliaFractalBaseCY = 0.2F
        App.Visualizers.JuliaFractalBassInfluence = 0.2F
        App.Visualizers.JuliaFractalMidInfluence = 0.2F
        App.Visualizers.JuliaFractalMaxIterations = 75
        ShowSettings()
    End Sub
    Private Sub BtnGalaxyBloom_Click(sender As Object, e As EventArgs) Handles BtnGalaxyBloom.Click
        App.Visualizers.JuliaFractalBaseCX = -0.4F
        App.Visualizers.JuliaFractalBaseCY = 0.6F
        App.Visualizers.JuliaFractalBassInfluence = 0.3F
        App.Visualizers.JuliaFractalMidInfluence = 1.5F
        App.Visualizers.JuliaFractalMaxIterations = 200
        ShowSettings()
    End Sub
    Private Sub BtnCoralReef_Click(sender As Object, e As EventArgs) Handles BtnCoralReef.Click
        App.Visualizers.JuliaFractalBaseCX = 0.285F
        App.Visualizers.JuliaFractalBaseCY = 0.01F
        App.Visualizers.JuliaFractalBassInfluence = 0.4F
        App.Visualizers.JuliaFractalMidInfluence = 0.8F
        App.Visualizers.JuliaFractalMaxIterations = 120
        ShowSettings()
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        ChkBoxAllowMiniMode.Checked = App.Visualizers.JuliaFractalAllowMiniMode
        TBBaseCX.Value = CInt((App.Visualizers.JuliaFractalBaseCX + 1) * 100)
        TBBassInfluence.Value = CInt(App.Visualizers.JuliaFractalBassInfluence * 10)
        TBBaseCY.Value = CInt((App.Visualizers.JuliaFractalBaseCY + 1) * 100)
        TBMidInfluence.Value = CInt(App.Visualizers.JuliaFractalMidInfluence * 10)
        TBMaxIterations.Value = App.Visualizers.JuliaFractalMaxIterations
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
        TBBaseCX.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBBaseCX.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBBaseCX.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBBaseCX.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBBaseCX.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBBaseCY.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBBaseCY.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBBaseCY.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBBaseCY.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBBaseCY.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBBassInfluence.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBBassInfluence.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBBassInfluence.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBBassInfluence.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBBassInfluence.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBMaxIterations.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBMaxIterations.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBMaxIterations.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBMaxIterations.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBMaxIterations.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBMidInfluence.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBMidInfluence.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBMidInfluence.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBMidInfluence.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBMidInfluence.TrackBarGradientEnd = App.CurrentTheme.TextColor
        LblPresets.ForeColor = forecolor
        LblBaseCX.ForeColor = forecolor
        LblBaseCY.ForeColor = forecolor
        LblBassInfluence.ForeColor = forecolor
        LblMaxIterations.ForeColor = forecolor
        LblMidInfluence.ForeColor = forecolor
        BtnBassquake.BackColor = App.CurrentTheme.ButtonBackColor
        BtnBassquake.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnCalmOcean.BackColor = App.CurrentTheme.ButtonBackColor
        BtnCalmOcean.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnCoralReef.BackColor = App.CurrentTheme.ButtonBackColor
        BtnCoralReef.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnCrystalGrid.BackColor = App.CurrentTheme.ButtonBackColor
        BtnCrystalGrid.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnFirestorm.BackColor = App.CurrentTheme.ButtonBackColor
        BtnFirestorm.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnGalaxyBloom.BackColor = App.CurrentTheme.ButtonBackColor
        BtnGalaxyBloom.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnTreblePulse.BackColor = App.CurrentTheme.ButtonBackColor
        BtnTreblePulse.ForeColor = App.CurrentTheme.ButtonTextColor
        TipFractalJulia.BackColor = App.CurrentTheme.BackColor
        TipFractalJulia.ForeColor = App.CurrentTheme.TextColor
        TipFractalJulia.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
    End Sub

End Class
