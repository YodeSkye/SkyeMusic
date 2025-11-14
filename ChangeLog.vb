
Public Class ChangeLog

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            App.WriteToLog("ChangeLog WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub ChangeLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = "What's New In " & My.Application.Info.Title
        RTBoxChangeLog.Rtf = My.Resources.ChangeLogRT
        SetAccentColor()
        SetTheme()
    End Sub
    Private Sub ChangeLog_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Not About.Visible Then
            Player.Activate()
            Player.BringToFront()
        End If
    End Sub
    Private Sub ChangeLog_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FrameBorderSize.Width - 4, -e.Y - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FrameBorderSize.Width - 4, -e.Y - cSender.Top - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            End If
        End If
    End Sub
    Private Sub ChangeLog_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub ChangeLog_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        mMove = False
    End Sub
    Private Sub ChangeLog_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub
    Private Sub ChangeLogKey_Down(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub

    'Control Events
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub

    'Procedures
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
    Private Sub SetAccentColor()
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            BackColor = c
        End If
        ResumeLayout()
        Debug.Print("Help Accent Color Set")
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If Not App.CurrentTheme.IsAccent Then
            BackColor = App.CurrentTheme.BackColor
        End If
        ResumeLayout()
        Debug.Print("ChangeLog Theme Set")
    End Sub

End Class
