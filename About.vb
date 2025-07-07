
Imports SkyeMusic.My

Public Class About

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point

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
    Private Sub About_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, LblAbout.MouseDown, LblVersion.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FixedFrameBorderSize.Width - 7, -e.Y - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 7)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FixedFrameBorderSize.Width - 7, -e.Y - cSender.Top - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 7)
            End If
        End If
        cSender = Nothing
    End Sub
    Private Sub About_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, LblAbout.MouseMove, LblVersion.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub About_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, LblAbout.MouseUp, LblVersion.MouseUp
        mMove = False
    End Sub
    Private Sub About_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
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
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
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