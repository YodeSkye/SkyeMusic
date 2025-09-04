
Imports SkyeMusic.My

Public Class PlayerEditTitle

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Friend NewTitle As String

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            If m.Msg = My.WinAPI.WM_SYSCOMMAND AndAlso CInt(m.WParam) = My.WinAPI.SC_CLOSE Then
                DialogResult = DialogResult.Cancel
            End If
        Catch ex As Exception
            My.App.WriteToLog("EditTitle WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub AddStream_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetTheme()
        TxtBoxTitle.Text = NewTitle
    End Sub
    Private Sub Options_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, LblTitle.MouseDown
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
    Private Sub Options_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, LblTitle.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub Options_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, LblTitle.MouseUp
        mMove = False
    End Sub
    Private Sub Options_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub

    'Control Events
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If String.IsNullOrEmpty(TxtBoxTitle.Text) Then
            LblTitle.ForeColor = Color.Red
            TxtBoxTitle.Text = "Please Enter A Title Here"
            TxtBoxTitle.Select()
            TxtBoxTitle.Select(0, TxtBoxTitle.Text.Length)
        Else
            NewTitle = TxtBoxTitle.Text
            DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub
    Private Sub TxtBox_MouseUp(sender As Object, e As MouseEventArgs) Handles TxtBoxTitle.MouseUp
        CMEditTitle.Display(DirectCast(sender, TextBox), e)
    End Sub
    Private Sub TxtBox_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxTitle.PreviewKeyDown
        CMEditTitle.ShortcutKeys(DirectCast(sender, TextBox), e)
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
            SetAccentColor()
        Else
            BackColor = App.CurrentTheme.BackColor
        End If
        LblTitle.ForeColor = App.CurrentTheme.TextColor
        TxtBoxTitle.BackColor = App.CurrentTheme.BackColor
        TxtBoxTitle.ForeColor = App.CurrentTheme.TextColor
    End Sub

End Class