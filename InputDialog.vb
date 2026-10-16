
Imports SkyeMusic.My
Imports System.ComponentModel

Public Class InputDialog

    'Declarations
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Private Property InputText As String

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
    Public Overloads Shared Function ShowDialog(title As String, prompt As String, Optional initialValue As String = "") As String
        Using frm As New InputDialog
            frm.Text = title
            frm.LblPrompt.Text = prompt
            frm.TxtBoxInput.Text = initialValue
            If frm.ShowDialog() = DialogResult.OK Then
                Return frm.InputText
            Else
                Return Nothing
            End If
        End Using
    End Function
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetAccentColor()
        SetTheme()
        App.ThemeMenu(CMEditTitle)
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
    Private Sub TxtBoxTitle_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxInput.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            OK()
        End If
    End Sub
    Private Sub TxtBox_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxInput.PreviewKeyDown
        CMEditTitle.ShortcutKeys(DirectCast(sender, TextBox), e)
    End Sub

    'Procedures
    Private Sub OK()
        If String.IsNullOrEmpty(TxtBoxInput.Text) Then
            LblPrompt.ForeColor = Color.Red
            'TxtBoxInput.Text = "Please Enter A Title Here"
            'TxtBoxInput.Select()
            'TxtBoxInput.Select(0, TxtBoxInput.Text.Length)
        Else
            InputText = TxtBoxInput.Text
            DialogResult = DialogResult.OK
            Close()
        End If
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
        'Debug.Print("Player Accent Color Set")
    End Sub
    Private Sub SetTheme()
        If App.CurrentTheme.IsAccent Then
            LblPrompt.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblPrompt.ForeColor = App.CurrentTheme.TextColor
        End If
        TxtBoxInput.BackColor = App.CurrentTheme.BackColor
        TxtBoxInput.ForeColor = App.CurrentTheme.TextColor
    End Sub

End Class
