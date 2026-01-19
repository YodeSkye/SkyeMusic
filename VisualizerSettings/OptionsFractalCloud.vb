
Public Class OptionsFractalCloud
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
    Private Sub CoBoxPalette_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxPalette.SelectedIndexChanged
        App.Visualizers.FractalCloudPalette = CType(CoBoxPalette.SelectedIndex, App.VisualizerSettings.FractalCloudPalettes)
    End Sub
    Private Sub ChkBoxAllowMiniMode_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxAllowMiniMode.CheckedChanged
        App.Visualizers.FractalCloudAllowMiniMode = ChkBoxAllowMiniMode.Checked
    End Sub
    Private Sub TBSwirlSpeedBase_ValueChanged(sender As Object, e As EventArgs) Handles TBSwirlSpeedBase.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.FractalCloudSwirlSpeedBase = CDbl(TBSwirlSpeedBase.Value / 1000)
        Debug.Print("Fractal Cloud Swirl Speed Base set to " & App.Visualizers.FractalCloudSwirlSpeedBase.ToString)
    End Sub
    Private Sub TBPeakDecaySpeed_ValueChanged(sender As Object, e As EventArgs) Handles TBSwirlSpeedAudioFactor.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.FractalCloudSwirlSpeedAudioFactor = CDbl(TBSwirlSpeedAudioFactor.Value)
        Debug.Print("Fractal Cloud Swirl Speed Audio Factor set to " & App.Visualizers.FractalCloudSwirlSpeedAudioFactor.ToString)
    End Sub
    Private Sub TBPeakThickness_ValueChanged(sender As Object, e As EventArgs) Handles TBTimeIncrement.ValueChanged
        If IsInitializing Then Exit Sub
        App.Visualizers.FractalCloudTimeIncrement = CDbl(TBTimeIncrement.Value / 1000)
        Debug.Print("Fractal Cloud Time Increment set to " & App.Visualizers.FractalCloudTimeIncrement.ToString)
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        CoBoxPalette.Items.Clear()
        CoBoxPalette.Items.Add(App.VisualizerSettings.FractalCloudPalettes.Normal)
        CoBoxPalette.Items.Add(App.VisualizerSettings.FractalCloudPalettes.Firestorm)
        CoBoxPalette.Items.Add(App.VisualizerSettings.FractalCloudPalettes.Aurora)
        CoBoxPalette.Items.Add(App.VisualizerSettings.FractalCloudPalettes.CosmicRainbow)
        CoBoxPalette.SelectedItem = App.Visualizers.FractalCloudPalette
        ChkBoxAllowMiniMode.Checked = App.Visualizers.FractalCloudAllowMiniMode
        TBSwirlSpeedBase.Value = CInt(App.Visualizers.FractalCloudSwirlSpeedBase * 1000)
        TBSwirlSpeedAudioFactor.Value = CInt(App.Visualizers.FractalCloudSwirlSpeedAudioFactor)
        TBTimeIncrement.Value = CInt(App.Visualizers.FractalCloudTimeIncrement * 1000)
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
        'Debug.Print("Options Fractal Cloud Accent Color Set")
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
        TBTimeIncrement.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBTimeIncrement.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBTimeIncrement.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBTimeIncrement.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBTimeIncrement.TrackBarGradientEnd = App.CurrentTheme.TextColor
        LblPalette.ForeColor = forecolor
        LblSwirlSpeedBase.ForeColor = forecolor
        LblSwirlSpeedAudioFactor.ForeColor = forecolor
        LblTimeIncrement.ForeColor = forecolor
        CoBoxPalette.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxPalette.ForeColor = App.CurrentTheme.TextColor
        TipFractalCloud.BackColor = App.CurrentTheme.BackColor
        TipFractalCloud.ForeColor = App.CurrentTheme.TextColor
        TipFractalCloud.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
    End Sub

End Class
