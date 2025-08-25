
Public Class PlayerQueue

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point

    'Form Events
    Private Sub PlayerQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetTheme()
        For Each s As String In Player.Queue
            Dim playlistlvi As ListViewItem = Player.LVPlaylist.FindItemWithText(s, True, 0)
            Dim lvi As New ListViewItem
            If playlistlvi Is Nothing Then
                lvi.SubItems(0).Text = "Not Found In Playlist"
                lvi.SubItems.Add(s)
            Else
                lvi.SubItems(0).Text = playlistlvi.SubItems(0).Text
                lvi.SubItems.Add(s)
            End If
            LVQueue.Items.Add(lvi)
        Next
        LVQueue.Columns(1).Width = -2
    End Sub
    Private Sub PlayerQueue_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
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
    Private Sub PlayerQueue_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub PlayerQueue_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        mMove = False
    End Sub
    Private Sub PlayerQueue_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub
    Private Sub PlayerQueue_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        LVQueue.Columns(1).Width = -2
    End Sub

    'Control Events
    Private Sub LVQueue_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles LVQueue.DrawColumnHeader
        Static b As Rectangle
        'Draw Background
        b = e.Bounds
        Using br As Brush = New SolidBrush(App.CurrentTheme.BackColor)
            e.Graphics.FillRectangle(br, b)
        End Using
        'Draw Borders
        b.Width -= 1
        b.Height -= 1
        e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, b)
        b.Width -= 1
        b.Height -= 1
        e.Graphics.DrawLine(SystemPens.ControlLightLight, b.X, b.Y, b.Right, b.Y)
        e.Graphics.DrawLine(SystemPens.ControlLightLight, b.X, b.Y, b.X, b.Bottom)
        e.Graphics.DrawLine(SystemPens.ControlDark, (b.X + 1), b.Bottom, b.Right, b.Bottom)
        e.Graphics.DrawLine(SystemPens.ControlDark, b.Right, (b.Y + 1), b.Right, b.Bottom)
        'Draw Text
        b = e.Bounds
        Dim width As Integer = TextRenderer.MeasureText(" ", e.Font).Width
        b = Rectangle.Inflate(e.Bounds, -2, 0)
        If e.Header.TextAlign = HorizontalAlignment.Center Then
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, b, App.CurrentTheme.TextColor, TextFormatFlags.HorizontalCenter)
        Else
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, b, App.CurrentTheme.TextColor, TextFormatFlags.VerticalCenter)
        End If
    End Sub
    Private Sub LVQueue_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles LVQueue.DrawItem
        e.DrawDefault = True
    End Sub
    Private Sub LVQueue_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles LVQueue.DrawSubItem
        e.DrawDefault = True
    End Sub
    Private Sub CMQueue_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMQueue.Opening
        If LVQueue.SelectedItems.Count = 0 Then
            CMIRemove.Text = CMIRemove.Text.TrimEnd(App.TrimEndSearch)
            CMIRemove.Enabled = False
        Else
            CMIRemove.Text = CMIRemove.Text.TrimEnd(App.TrimEndSearch) + " (" + LVQueue.SelectedItems.Count.ToString + ")"
            CMIRemove.Enabled = True
        End If
    End Sub
    Private Sub CMIRemove_Click(sender As Object, e As EventArgs) Handles CMIRemove.Click

    End Sub
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
        If App.Theme = App.Themes.Accent Then
            c = App.GetAccentColor()
            BackColor = c
            Debug.Print("Player Accent Color Set")
        End If
    End Sub
    Private Sub SetTheme()
        If App.Theme = App.Themes.Accent Then
            SetAccentColor()
        Else
            BackColor = App.CurrentTheme.BackColor
        End If
        LVQueue.BackColor = App.CurrentTheme.BackColor
        LVQueue.ForeColor = App.CurrentTheme.TextColor
    End Sub

End Class
