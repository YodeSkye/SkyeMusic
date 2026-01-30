
Public Class OptionsStarField
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
        App.Settings.Visualizers.StarFieldAllowMiniMode = ChkBoxAllowMiniMode.Checked
    End Sub
    Private Sub TxtBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxStarCount.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            Validate()
        End If
    End Sub
    Private Sub TxtBoxNumbersOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TxtBoxStarCount.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then e.Handled = True
    End Sub
    Private Sub TxtBoxStarCount_Validated(sender As Object, e As EventArgs) Handles TxtBoxStarCount.Validated
        App.Settings.Visualizers.StarFieldStarCount = CInt(TxtBoxStarCount.Text)
        App.Settings.Visualizers.StarFieldStarCount = Math.Max(100, Math.Min(2000, App.Settings.Visualizers.StarFieldStarCount))
        TxtBoxStarCount.Text = App.Settings.Visualizers.StarFieldStarCount.ToString
        Player.VisualizerHost.SetStarFieldStarCount(App.Settings.Visualizers.StarFieldStarCount)
    End Sub
    Private Sub TBBaseSpeed_ValueChanged(sender As Object, e As EventArgs) Handles TBBaseSpeed.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.StarFieldBaseSpeed = CSng(TBBaseSpeed.Value / 10)
    End Sub
    Private Sub TBAudioSpeedFactor_ValueChanged(sender As Object, e As EventArgs) Handles TBAudioSpeedFactor.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.StarFieldAudioSpeedFactor = TBAudioSpeedFactor.Value
    End Sub
    Private Sub TBMaxStarSize_ValueChanged(sender As Object, e As EventArgs) Handles TBMaxStarSize.ValueChanged
        If IsInitializing Then Exit Sub
        App.Settings.Visualizers.StarFieldMaxStarSize = TBMaxStarSize.Value
    End Sub
    Private Sub BtnCalmDrift_Click(sender As Object, e As EventArgs) Handles BtnCalmDrift.Click
        App.Settings.Visualizers.StarFieldStarCount = 300
        App.Settings.Visualizers.StarFieldBaseSpeed = 1.0F
        App.Settings.Visualizers.StarFieldAudioSpeedFactor = 5
        App.Settings.Visualizers.StarFieldMaxStarSize = 3
        ShowSettings()
    End Sub
    Private Sub BtnClassicArcade_Click(sender As Object, e As EventArgs) Handles BtnClassicArcade.Click
        App.Settings.Visualizers.StarFieldStarCount = 500
        App.Settings.Visualizers.StarFieldBaseSpeed = 2.0F
        App.Settings.Visualizers.StarFieldAudioSpeedFactor = 35
        App.Settings.Visualizers.StarFieldMaxStarSize = 6
        ShowSettings()
    End Sub
    Private Sub BtnHyperWarp_Click(sender As Object, e As EventArgs) Handles BtnHyperWarp.Click
        App.Settings.Visualizers.StarFieldStarCount = 1000
        App.Settings.Visualizers.StarFieldBaseSpeed = 3.0F
        App.Settings.Visualizers.StarFieldAudioSpeedFactor = 75
        App.Settings.Visualizers.StarFieldMaxStarSize = 8
        ShowSettings()
    End Sub
    Private Sub BtnNebulaDust_Click(sender As Object, e As EventArgs) Handles BtnNebulaDust.Click
        App.Settings.Visualizers.StarFieldStarCount = 800
        App.Settings.Visualizers.StarFieldBaseSpeed = 1.5F
        App.Settings.Visualizers.StarFieldAudioSpeedFactor = 1
        App.Settings.Visualizers.StarFieldMaxStarSize = 3
        ShowSettings()
    End Sub
    Private Sub BtnSilentSpace_Click(sender As Object, e As EventArgs) Handles BtnSilentSpace.Click
        App.Settings.Visualizers.StarFieldStarCount = 200
        App.Settings.Visualizers.StarFieldBaseSpeed = 1.0F
        App.Settings.Visualizers.StarFieldAudioSpeedFactor = 0
        App.Settings.Visualizers.StarFieldMaxStarSize = 2
        ShowSettings()
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        ChkBoxAllowMiniMode.Checked = App.Settings.Visualizers.StarFieldAllowMiniMode
        TxtBoxStarCount.Text = App.Settings.Visualizers.StarFieldStarCount.ToString
        TBBaseSpeed.Value = CInt(App.Settings.Visualizers.StarFieldBaseSpeed * 10)
        TBAudioSpeedFactor.Value = App.Settings.Visualizers.StarFieldAudioSpeedFactor
        TBMaxStarSize.Value = App.Settings.Visualizers.StarFieldMaxStarSize
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
        TBBaseSpeed.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBBaseSpeed.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBBaseSpeed.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBBaseSpeed.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBBaseSpeed.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBAudioSpeedFactor.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBAudioSpeedFactor.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBAudioSpeedFactor.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBAudioSpeedFactor.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBAudioSpeedFactor.TrackBarGradientEnd = App.CurrentTheme.TextColor
        TBMaxStarSize.ButtonColor = App.CurrentTheme.ButtonBackColor
        TBMaxStarSize.HighlightedButtonColor = App.CurrentTheme.TextColor
        TBMaxStarSize.PushedButtonEndColor = App.CurrentTheme.TextColor
        TBMaxStarSize.TrackBarGradientStart = App.CurrentTheme.BackColor
        TBMaxStarSize.TrackBarGradientEnd = App.CurrentTheme.TextColor
        LblStarCount.ForeColor = forecolor
        LblBaseSpeed.ForeColor = forecolor
        LblAudioSpeedFactor.ForeColor = forecolor
        LblMaxStarSize.ForeColor = forecolor
        LblPresets.ForeColor = forecolor
        TxtBoxStarCount.BackColor = App.CurrentTheme.BackColor
        TxtBoxStarCount.ForeColor = App.CurrentTheme.TextColor
        BtnCalmDrift.BackColor = App.CurrentTheme.ButtonBackColor
        BtnCalmDrift.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnClassicArcade.BackColor = App.CurrentTheme.ButtonBackColor
        BtnClassicArcade.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnHyperWarp.BackColor = App.CurrentTheme.ButtonBackColor
        BtnHyperWarp.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnNebulaDust.BackColor = App.CurrentTheme.ButtonBackColor
        BtnNebulaDust.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnSilentSpace.BackColor = App.CurrentTheme.ButtonBackColor
        BtnSilentSpace.ForeColor = App.CurrentTheme.ButtonTextColor
        TipStarField.BackColor = App.CurrentTheme.BackColor
        TipStarField.ForeColor = App.CurrentTheme.TextColor
        TipStarField.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
    End Sub

End Class
