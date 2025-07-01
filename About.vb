
Imports SkyeMusic.My

Public Class About

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            App.WriteToLog("About WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub AboutLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        SetTheme()
        Text = "About " + My.Application.Info.Title
        LblAbout.Text = My.Application.Info.Description
        LblVersion.Text = My.Application.Info.Version.ToString
    End Sub
    Private Sub AboutMove(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            If Left < 0 Then
                Left = -App.AdjustScreenBoundsDialogWindow
            ElseIf Right > My.Computer.Screen.WorkingArea.Width Then
                Left = My.Computer.Screen.WorkingArea.Width - Width + App.AdjustScreenBoundsDialogWindow
            End If
            If Top < App.AdjustScreenBoundsDialogWindow Then
                Top = 0
            ElseIf Bottom > My.Computer.Screen.WorkingArea.Height Then
                Top = My.Computer.Screen.WorkingArea.Height - Height + App.AdjustScreenBoundsDialogWindow
            End If
        End If
    End Sub
    Private Sub AboutKeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub

    'Control Events
    Private Sub BtnOKClick(sender As Object, e As EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub

    'Procedures
    Private Sub SetAccentColor(Optional AsTheme As Boolean = False)
        Static c As Color
        If Not AsTheme Then SuspendLayout()
        If App.Theme = App.Themes.Accent Then
            c = App.GetAccentColor()
            BackColor = c
        End If
        If Not AsTheme Then ResumeLayout()
        Debug.Print("About Accent Color Set")
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If App.Theme = App.Themes.Accent Then
            SetAccentColor(True)
            LblAbout.ForeColor = App.CurrentTheme.AccentTextColor
            LblVersion.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblAbout.ForeColor = App.CurrentTheme.TextColor
            LblVersion.ForeColor = App.CurrentTheme.TextColor
        End If
        ResumeLayout()
        Debug.Print("About Theme Set")
    End Sub

End Class