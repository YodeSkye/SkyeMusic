
Public Class OptionsWaveform
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
    Private Sub ChkBoxShowPeaks_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxWaveformFill.CheckedChanged
        App.Settings.Visualizers.WaveformFill = ChkBoxWaveformFill.Checked
    End Sub
    Private Sub ChkBoxAllowMiniMode_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxAllowMiniMode.CheckedChanged
        App.Settings.Visualizers.WaveformAllowMiniMode = ChkBoxAllowMiniMode.Checked
    End Sub

    ' Methods
    Private Sub ShowSettings()
        IsInitializing = True
        ChkBoxWaveformFill.Checked = App.Settings.Visualizers.WaveformFill
        ChkBoxAllowMiniMode.Checked = App.Settings.Visualizers.WaveformAllowMiniMode
        IsInitializing = False
    End Sub
    Private Sub SetAccentColor() Implements App.IAccentable.SetAccentColor
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            ChkBoxWaveformFill.BackColor = c
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
        ChkBoxWaveformFill.ForeColor = forecolor
        ChkBoxAllowMiniMode.ForeColor = forecolor
        TipWaveform.BackColor = App.CurrentTheme.BackColor
        TipWaveform.ForeColor = App.CurrentTheme.TextColor
        TipWaveform.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
    End Sub

End Class
