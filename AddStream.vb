Public Class AddStream

    'Declarations
    Friend NewStream As Player.PlaylistItemType

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            If m.Msg = My.WinAPI.WM_SYSCOMMAND AndAlso CInt(m.WParam) = My.WinAPI.SC_CLOSE Then
                DialogResult = DialogResult.Cancel
            End If
        Catch ex As Exception
            My.App.WriteToLog("AddStream WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub AddStream_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetTheme()
        TxtBoxStreamPath.Text = NewStream.Path
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
    Private Sub TxtBox_MouseUp(sender As Object, e As MouseEventArgs) Handles TxtBoxStreamTitle.MouseUp, TxtBoxStreamPath.MouseUp
        CMAddStream.Display(DirectCast(sender, TextBox), e)
    End Sub
    Private Sub TxtBox_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxStreamTitle.PreviewKeyDown, TxtBoxStreamPath.PreviewKeyDown
        CMAddStream.ShortcutKeys(DirectCast(sender, TextBox), e)
    End Sub

    'Procedures
    Private Sub SetAccentColor()
        Static c As Color
        c = App.GetAccentColor()
        If App.Theme = App.Themes.Accent Then
            BackColor = c
        End If
        Debug.Print("Player Accent Color Set")
    End Sub
    Private Sub SetTheme()
        If App.Theme = App.Themes.Accent Then
            SetAccentColor()
        Else
            BackColor = App.CurrentTheme.BackColor
        End If
        LblStreamTitle.ForeColor = App.CurrentTheme.TextColor
        LblStreamPath.ForeColor = App.CurrentTheme.TextColor
        TxtBoxStreamTitle.BackColor = App.CurrentTheme.BackColor
        TxtBoxStreamTitle.ForeColor = App.CurrentTheme.TextColor
        TxtBoxStreamPath.BackColor = App.CurrentTheme.BackColor
        TxtBoxStreamPath.ForeColor = App.CurrentTheme.TextColor
    End Sub

End Class