
Imports SkyeMusic.My
Imports System.ComponentModel

Public Class ConfirmationDialog

    'Declarations

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_SYSCOMMAND
                    If CInt(m.WParam) = Skye.WinAPI.SC_CLOSE Then DialogResult = DialogResult.Cancel
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            Skye.Common.Log.Write("EditTitle WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Public Overloads Shared Function ShowDialog(title As String, prompt As String) As DialogResult
        Using frm As New ConfirmationDialog
            frm.Text = title
            frm.LblPrompt.Text = prompt
            If frm.ShowDialog() = DialogResult.OK Then
                Return DialogResult.OK
            Else
                Return DialogResult.Cancel
            End If
        End Using
    End Function
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim minHeaderAndButtonsHeight As Integer = 84 ' Combined height of header, margins, and bottom buttons
        Me.Height = Math.Max(180, LblPrompt.Height + minHeaderAndButtonsHeight)

        SetAccentColor()
        SetTheme()

        App.EnableFormDragging(Me)
    End Sub
    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        App.DisableFormDragging(Me)
    End Sub
    Private Sub Form_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        CheckMove(Location)
    End Sub

    'Control Events
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        OK()
    End Sub
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Cancel()
    End Sub
    Private Sub TxtBoxTitle_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            OK()
        End If
    End Sub

    'Procedures
    Private Sub OK()
        DialogResult = DialogResult.OK
        Close()
    End Sub
    Private Sub Cancel()
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
    Private Sub SetAccentColor()
        Static c As Color
        c = App.GetAccentColor()
        If App.CurrentTheme.IsAccent Then
            BackColor = c
        End If
    End Sub
    Private Sub SetTheme()
        If App.CurrentTheme.IsAccent Then
            LblPrompt.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblPrompt.ForeColor = App.CurrentTheme.TextColor
        End If
    End Sub

End Class
