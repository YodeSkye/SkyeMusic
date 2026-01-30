
Public Class OptionsClassicSpectrumAnalyzer
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
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount = CInt(TxtBoxBarCount.Text)
        If App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount < 16 Then
            App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount = 16
        ElseIf App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount > 96 Then
            App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount = 96
        End If
        TxtBoxBarCount.Text = App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount.ToString
    End Sub
    Private Sub ChkBoxShowPeaks_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxShowPeaks.CheckedChanged
        App.Settings.Visualizers.ClassicSpectrumAnalyzerShowPeaks = ChkBoxShowPeaks.Checked
    End Sub
    Private Sub ChkBoxAllowMiniMode_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxAllowMiniMode.CheckedChanged
        App.Settings.Visualizers.ClassicSpectrumAnalyzerAllowMiniMode = ChkBoxAllowMiniMode.Checked
    End Sub
    Private Sub CoBoxBandMappingMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxBandMappingMode.SelectedIndexChanged
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBandMappingMode = CType(CoBoxBandMappingMode.SelectedIndex, App.ClassicSpectrumAnalyzerBandMappingModes)
    End Sub
    Private Sub TBGain_ValueChanged(sender As Object, e As EventArgs) Handles TBGain.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ClassicSpectrumAnalyzerGain = CSng(TBGain.Value / 10)
    End Sub
    Private Sub TBSmoothing_ValueChanged(sender As Object, e As EventArgs) Handles TBSmoothing.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ClassicSpectrumAnalyzerSmoothing = CSng(TBSmoothing.Value / 100)
    End Sub
    Private Sub TBPeakDecay_ValueChanged(sender As Object, e As EventArgs) Handles TBPeakDecay.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakDecay = TBPeakDecay.Value
    End Sub
    Private Sub TBPeakHoldFrames_ValueChanged(sender As Object, e As EventArgs) Handles TBPeakHoldFrames.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakHoldFrames = TBPeakHoldFrames.Value
    End Sub
    Private Sub BtnClassicWinamp_Click(sender As Object, e As EventArgs) Handles BtnClassicWinamp.Click
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount = 32
        App.Settings.Visualizers.ClassicSpectrumAnalyzerGain = 3.0F
        App.Settings.Visualizers.ClassicSpectrumAnalyzerSmoothing = 0.7F
        App.Settings.Visualizers.ClassicSpectrumAnalyzerShowPeaks = True
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakDecay = 2
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakHoldFrames = 20
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBandMappingMode = App.ClassicSpectrumAnalyzerBandMappingModes.Logarithmic
        ShowSettings()
    End Sub
    Private Sub BtnAmbientFlow_Click(sender As Object, e As EventArgs) Handles BtnAmbientFlow.Click
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount = 24
        App.Settings.Visualizers.ClassicSpectrumAnalyzerGain = 2.5F
        App.Settings.Visualizers.ClassicSpectrumAnalyzerSmoothing = 0.9F
        App.Settings.Visualizers.ClassicSpectrumAnalyzerShowPeaks = True
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakDecay = 1
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakHoldFrames = 30
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBandMappingMode = App.ClassicSpectrumAnalyzerBandMappingModes.Logarithmic
        ShowSettings()
    End Sub
    Private Sub BtnLiveDJ_Click(sender As Object, e As EventArgs) Handles BtnLiveDJ.Click
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount = 48
        App.Settings.Visualizers.ClassicSpectrumAnalyzerGain = 4.5F
        App.Settings.Visualizers.ClassicSpectrumAnalyzerSmoothing = 0.3F
        App.Settings.Visualizers.ClassicSpectrumAnalyzerShowPeaks = True
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakDecay = 4
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakHoldFrames = 10
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBandMappingMode = App.ClassicSpectrumAnalyzerBandMappingModes.Linear
        ShowSettings()
    End Sub
    Private Sub BtnRetroArcade_Click(sender As Object, e As EventArgs) Handles BtnRetroArcade.Click
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount = 32
        App.Settings.Visualizers.ClassicSpectrumAnalyzerGain = 5.0F
        App.Settings.Visualizers.ClassicSpectrumAnalyzerSmoothing = 0.5F
        App.Settings.Visualizers.ClassicSpectrumAnalyzerShowPeaks = True
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakDecay = 2
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakHoldFrames = 25
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBandMappingMode = App.ClassicSpectrumAnalyzerBandMappingModes.Logarithmic
        ShowSettings()
    End Sub
    Private Sub BtnTechnicalAnalyzer_Click(sender As Object, e As EventArgs) Handles BtnTechnicalAnalyzer.Click
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount = 64
        App.Settings.Visualizers.ClassicSpectrumAnalyzerGain = 2.0F
        App.Settings.Visualizers.ClassicSpectrumAnalyzerSmoothing = 0.0F
        App.Settings.Visualizers.ClassicSpectrumAnalyzerShowPeaks = True
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakDecay = 5
        App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakHoldFrames = 0
        App.Settings.Visualizers.ClassicSpectrumAnalyzerBandMappingMode = App.ClassicSpectrumAnalyzerBandMappingModes.Linear
        ShowSettings()
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        TxtBoxBarCount.Text = App.Settings.Visualizers.ClassicSpectrumAnalyzerBarCount.ToString()
        ChkBoxShowPeaks.Checked = App.Settings.Visualizers.ClassicSpectrumAnalyzerShowPeaks
        ChkBoxAllowMiniMode.Checked = App.Settings.Visualizers.ClassicSpectrumAnalyzerAllowMiniMode
        CoBoxBandMappingMode.Items.Clear()
        CoBoxBandMappingMode.Items.Add(App.ClassicSpectrumAnalyzerBandMappingModes.Linear)
        CoBoxBandMappingMode.Items.Add(App.ClassicSpectrumAnalyzerBandMappingModes.Logarithmic)
        CoBoxBandMappingMode.SelectedItem = App.Settings.Visualizers.ClassicSpectrumAnalyzerBandMappingMode
        TBGain.Value = CInt(App.Settings.Visualizers.ClassicSpectrumAnalyzerGain * 10)
        TBSmoothing.Value = CInt(App.Settings.Visualizers.ClassicSpectrumAnalyzerSmoothing * 100)
        TBPeakDecay.Value = CInt(App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakDecay)
        TBPeakHoldFrames.Value = CInt(App.Settings.Visualizers.ClassicSpectrumAnalyzerPeakHoldFrames)
        IsInitializing = False
    End Sub
    Private Sub SetAccentColor() Implements App.IAccentable.SetAccentColor
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            ChkBoxShowPeaks.BackColor = c
            ChkBoxAllowMiniMode.BackColor = c
        End If
        ResumeLayout()
        'Debug.Print("Options Waveform Accent Color Set")
    End Sub
    Private Sub SetTheme()
        Static forecolor As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            forecolor = App.CurrentTheme.AccentTextColor
        Else
            forecolor = App.CurrentTheme.TextColor
        End If
        ChkBoxShowPeaks.ForeColor = forecolor
        ChkBoxAllowMiniMode.ForeColor = forecolor
        CoBoxBandMappingMode.BackColor = App.CurrentTheme.ControlBackColor
        CoBoxBandMappingMode.ForeColor = App.CurrentTheme.TextColor
        TxtBoxBarCount.BackColor = App.CurrentTheme.BackColor
        TxtBoxBarCount.ForeColor = App.CurrentTheme.TextColor
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
        TBPeakDecay.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBPeakDecay.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBPeakDecay.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBPeakDecay.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBPeakDecay.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBPeakHoldFrames.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBPeakHoldFrames.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBPeakHoldFrames.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBPeakHoldFrames.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBPeakHoldFrames.TrackBarGradientEnd = App.CurrentTheme.TextColor
        LblPresets.ForeColor = forecolor
        LblBarCount.ForeColor = forecolor
        LblBandMappingMode.ForeColor = forecolor
        LblGain.ForeColor = forecolor
        LblSmoothing.ForeColor = forecolor
        LblPeakDecay.ForeColor = forecolor
        LblPeakHoldFrames.ForeColor = forecolor
        BtnClassicWinamp.BackColor = App.CurrentTheme.ButtonBackColor
        BtnClassicWinamp.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnAmbientFlow.BackColor = App.CurrentTheme.ButtonBackColor
        BtnAmbientFlow.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnLiveDJ.BackColor = App.CurrentTheme.ButtonBackColor
        BtnLiveDJ.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnRetroArcade.BackColor = App.CurrentTheme.ButtonBackColor
        BtnRetroArcade.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnTechnicalAnalyzer.BackColor = App.CurrentTheme.ButtonBackColor
        BtnTechnicalAnalyzer.ForeColor = App.CurrentTheme.ButtonTextColor
        TipClassicSpectrumAnalyzer.BackColor = App.CurrentTheme.BackColor
        TipClassicSpectrumAnalyzer.ForeColor = App.CurrentTheme.TextColor
        TipClassicSpectrumAnalyzer.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
    End Sub

End Class
