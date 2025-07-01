Imports SkyeMusic.My

Public Class Help

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            App.WriteToLog("Help WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub HelpLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title + " Help"
        RTxBxHelp.Rtf = My.Resources.HelpRT
        SetTheme()
    End Sub
    Private Sub HelpMove(sender As Object, e As EventArgs) Handles MyBase.Move
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
    Private Sub HelpKeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub

    'Control Events
    Private Sub BtnOKClick(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub

    'Procedures
    Private Sub SetAccentColor(Optional AsTheme As Boolean = False)
        Static c As Color
        If Not AsTheme Then SuspendLayout()
        If App.Theme = App.Themes.Accent Then
            c = App.GetAccentColor()
            BackColor = c
            RTxBxHelp.BackColor = c
        End If
        If Not AsTheme Then ResumeLayout()
        Debug.Print("Help Accent Color Set")
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If App.Theme = App.Themes.Accent Then
            SetAccentColor(True)
        Else
            BackColor = App.CurrentTheme.BackColor
            RTxBxHelp.BackColor = App.CurrentTheme.BackColor
        End If
        RTxBxHelp.SelectAll()
        Select Case App.Theme
            Case App.Themes.Accent
                RTxBxHelp.SelectionColor = App.CurrentTheme.AccentTextColor
            Case Else
                RTxBxHelp.SelectionColor = App.CurrentTheme.TextColor
        End Select
        RTxBxHelp.DeselectAll()
        ResumeLayout()
        Debug.Print("Help Theme Set")
    End Sub

End Class