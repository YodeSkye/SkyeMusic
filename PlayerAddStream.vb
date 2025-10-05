
Imports SkyeMusic.My

Public Class PlayerAddStream

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Friend NewStream As Player.PlaylistItemType

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
            My.App.WriteToLog("AddStream WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub AddStream_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetAccentColor()
        SetTheme()
        TxtBoxStreamPath.Text = NewStream.Path
    End Sub
    Private Sub Options_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, LblStreamTitle.MouseDown, LblStreamPath.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FixedFrameBorderSize.Width - 5, -e.Y - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 5)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FixedFrameBorderSize.Width - 5, -e.Y - cSender.Top - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 5)
            End If
        End If
        cSender = Nothing
    End Sub
    Private Sub Options_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, LblStreamTitle.MouseMove, LblStreamPath.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub Options_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, LblStreamTitle.MouseUp, LblStreamPath.MouseUp
        mMove = False
    End Sub
    Private Sub Options_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub

    'Control Events
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If String.IsNullOrEmpty(TxtBoxStreamTitle.Text) Then
            LblStreamTitle.ForeColor = Color.Red
            TxtBoxStreamTitle.Text = "Please Enter A Title Here"
            TxtBoxStreamTitle.Select()
            TxtBoxStreamTitle.Select(0, TxtBoxStreamTitle.Text.Length)
        ElseIf String.IsNullOrEmpty(TxtBoxStreamPath.Text) Then
            LblStreamPath.ForeColor = Color.Red
            TxtBoxStreamPath.Text = "Please Enter A URL Path Here"
            TxtBoxStreamPath.Select()
            TxtBoxStreamPath.Select(0, TxtBoxStreamPath.Text.Length)
        Else
            NewStream.Title = TxtBoxStreamTitle.Text
            NewStream.Path = TxtBoxStreamPath.Text
            DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub
    Private Sub TxtBox_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxStreamTitle.PreviewKeyDown, TxtBoxStreamPath.PreviewKeyDown
        CMAddStream.ShortcutKeys(DirectCast(sender, TextBox), e)
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
        c = App.GetAccentColor()
        If App.CurrentTheme.IsAccent Then
            BackColor = c
        End If
        Debug.Print("Player Accent Color Set")
    End Sub
    Private Sub SetTheme()
        If App.CurrentTheme.IsAccent Then
            LblStreamTitle.ForeColor = App.CurrentTheme.AccentTextColor
            LblStreamPath.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblStreamTitle.ForeColor = App.CurrentTheme.TextColor
            LblStreamPath.ForeColor = App.CurrentTheme.TextColor
        End If
        TxtBoxStreamTitle.BackColor = App.CurrentTheme.BackColor
        TxtBoxStreamTitle.ForeColor = App.CurrentTheme.TextColor
        TxtBoxStreamPath.BackColor = App.CurrentTheme.BackColor
        TxtBoxStreamPath.ForeColor = App.CurrentTheme.TextColor
    End Sub

End Class
