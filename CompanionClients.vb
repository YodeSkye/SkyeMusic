
Public Class CompanionClients

    ' Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            Skye.Common.Log.Write("Log WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub Frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title & " " & Text
        SetAccentColor()
        SetTheme()
        ReThemeMenus()
        RefreshLists()
    End Sub
    Private Sub Frm_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            CheckMove(Location)
        End If
    End Sub
    Private Sub Frm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.OemQuestion Then
            App.FrmPlayer.ShowNowPlayingToast(App.FrmPlayer.PlaylistCurrentText)
        ElseIf e.KeyData = Keys.Escape Then
            Close()
        End If
    End Sub

    ' Control Events
    Private Sub TCClients_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TCClients.SelectedIndexChanged
        'Debug.Print(TCClients.SelectedTab.Name)
        Select Case TCClients.SelectedTab.Name
            Case "TPClients"
                LVClients.Focus()
            Case "TPListeners"
                LVListeners.Focus()
        End Select
    End Sub
    Private Sub LVClients_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles LVClients.DrawColumnHeader
        Static b As Rectangle
        'Draw Background
        b = e.Bounds
        Using br As Brush = New SolidBrush(CurrentTheme.BackColor)
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
        e.Graphics.DrawLine(SystemPens.ControlDark, b.X + 1, b.Bottom, b.Right, b.Bottom)
        e.Graphics.DrawLine(SystemPens.ControlDark, b.Right, b.Y + 1, b.Right, b.Bottom)
        'Draw Text
        b = e.Bounds
        Dim width = TextRenderer.MeasureText(" ", CurrentTheme.SubBaseFont).Width
        b = Rectangle.Inflate(e.Bounds, -2, 0)
        If e.ColumnIndex = 0 Then
            TextRenderer.DrawText(e.Graphics, e.Header.Text, CurrentTheme.SubBaseFont, b, CurrentTheme.TextColor, TextFormatFlags.Left)
        Else
            TextRenderer.DrawText(e.Graphics, e.Header.Text, CurrentTheme.SubBaseFont, b, CurrentTheme.TextColor, TextFormatFlags.HorizontalCenter)
        End If
    End Sub
    Private Sub LVClients_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles LVClients.DrawItem
        e.DrawDefault = True
    End Sub
    Private Sub LVClients_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles LVClients.DrawSubItem
        e.DrawDefault = True
    End Sub
    Private Sub LVClients_ColumnWidthChanging(sender As Object, e As ColumnWidthChangingEventArgs) Handles LVClients.ColumnWidthChanging

        ' Block all resizing:
        e.Cancel = True
        e.NewWidth = LVClients.Columns(e.ColumnIndex).Width

        ' Or, if you only want to lock specific columns:
        'If e.ColumnIndex = 0 Then
        '    e.Cancel = True
        '    e.NewWidth = LVClients.Columns(0).Width
        'End If
    End Sub
    Private Sub CMClients_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMClients.Opening
        If LVClients.SelectedItems.Count = 0 Then e.Cancel = True
    End Sub
    Private Sub CMIClientsDisconnect_Click(sender As Object, e As EventArgs) Handles CMIClientsDisconnect.Click
        Dim info = GetSelectedClient()
        If info Is Nothing Then Exit Sub

        CompanionControlServer.DisconnectClient(info)
        RefreshLists()

    End Sub
    Private Sub CMIClientsCopyDeviceName_Click(sender As Object, e As EventArgs) Handles CMIClientsCopyDeviceName.Click
        Dim info = GetSelectedClient()
        If info Is Nothing Then Exit Sub

        Clipboard.SetText(info.Name)

    End Sub
    Private Sub CMIClientsCopyIP_Click(sender As Object, e As EventArgs) Handles CMIClientsCopyIP.Click
        Dim info = GetSelectedClient()
        If info Is Nothing Then Exit Sub

        Clipboard.SetText(info.IP)

    End Sub
    Private Sub CMIClientsRefresh_Click(sender As Object, e As EventArgs) Handles CMIClientsRefresh.Click
        RefreshLists()
    End Sub
    Private Sub LVListeners_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles LVListeners.DrawColumnHeader
        Static b As Rectangle
        'Draw Background
        b = e.Bounds
        Using br As Brush = New SolidBrush(CurrentTheme.BackColor)
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
        e.Graphics.DrawLine(SystemPens.ControlDark, b.X + 1, b.Bottom, b.Right, b.Bottom)
        e.Graphics.DrawLine(SystemPens.ControlDark, b.Right, b.Y + 1, b.Right, b.Bottom)
        'Draw Text
        b = e.Bounds
        Dim width = TextRenderer.MeasureText(" ", CurrentTheme.SubBaseFont).Width
        b = Rectangle.Inflate(e.Bounds, -2, 0)
        If e.ColumnIndex = 0 Then
            TextRenderer.DrawText(e.Graphics, e.Header.Text, CurrentTheme.SubBaseFont, b, CurrentTheme.TextColor, TextFormatFlags.Left)
        Else
            TextRenderer.DrawText(e.Graphics, e.Header.Text, CurrentTheme.SubBaseFont, b, CurrentTheme.TextColor, TextFormatFlags.HorizontalCenter)
        End If
    End Sub
    Private Sub LVListeners_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles LVListeners.DrawItem
        e.DrawDefault = True
    End Sub
    Private Sub LVListeners_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles LVListeners.DrawSubItem
        e.DrawDefault = True
    End Sub
    Private Sub LVListeners_ColumnWidthChanging(sender As Object, e As ColumnWidthChangingEventArgs) Handles LVListeners.ColumnWidthChanging
        ' Block all resizing:
        e.Cancel = True
        e.NewWidth = LVListeners.Columns(e.ColumnIndex).Width
    End Sub
    Private Sub CMListeners_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMListeners.Opening
        If LVListeners.SelectedItems.Count = 0 Then e.Cancel = True
    End Sub
    Private Sub CMIListenersDisconnect_Click(sender As Object, e As EventArgs) Handles CMIListenersDisconnect.Click
        Dim info = TryCast(LVListeners.SelectedItems(0).Tag, String)
        If String.IsNullOrWhiteSpace(info) Then Exit Sub

        LiveAudioServer.DisconnectClient(info)
        RefreshLists()

    End Sub
    Private Sub CMIListenersCopyIP_Click(sender As Object, e As EventArgs) Handles CMIListenersCopyIP.Click
        Dim info = TryCast(LVListeners.SelectedItems(0).Tag, String)
        If String.IsNullOrWhiteSpace(info) Then Exit Sub

        Clipboard.SetText(info)

    End Sub
    Private Sub CMIListenersRefresh_Click(sender As Object, e As EventArgs) Handles CMIListenersRefresh.Click
        RefreshLists()
    End Sub

    ' Handlers
    Private Sub TimerRefresh_Tick(sender As Object, e As EventArgs) Handles TimerRefresh.Tick
        If CMClients.Visible = False And CMListeners.Visible = False Then RefreshLists()
    End Sub

    ' Methods
    Private Sub RefreshLists()
        LVClients.Items.Clear()
        Dim clients = App.CompanionControlServer.GetClients()
        For Each info In clients
            Dim item As New ListViewItem(info.Name)
            item.SubItems.Add(info.IP)
            item.SubItems.Add(info.ConnectedAt.ToString("g"))
            item.SubItems.Add(info.LastMessageAt.ToString("g"))
            item.Tag = info
            LVClients.Items.Add(item)
        Next
        LVListeners.Items.Clear()
        Dim Listeners = LiveAudioServer.ActiveClients
        For Each listener In Listeners
            Dim item As New ListViewItem(listener.EndPoint)
            item.SubItems.Add(listener.ConnectedAt.ToString("g"))
            Dim duration As TimeSpan = DateTime.Now - listener.ConnectedAt
            Dim durationText As String = duration.ToString("hh\:mm\:ss")
            item.SubItems.Add(durationText)
            item.SubItems.Add(Skye.Common.FormatFileSize(listener.BytesStreamed, Skye.Common.FormatFileSizeUnits.KiloBytes))
            item.SubItems.Add(listener.UserAgent)
            item.Tag = listener.EndPoint
            LVListeners.Items.Add(item)
        Next
        Debug.WriteLine($"Refreshed client list: {clients.Count} clients found, {Listeners.Count} listeners found.")
    End Sub
    Private Function GetSelectedClient() As CompanionClientInfo
        If LVClients.SelectedItems.Count = 0 Then Return Nothing
        Return DirectCast(LVClients.SelectedItems(0).Tag, CompanionClientInfo)
    End Function
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
    Private Sub SetAccentColor(Optional force As Boolean = False)
        Dim accent As Color = App.GetAccentColor()
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            BackColor = accent
            TCClients.TabPanelBackColor = accent
        End If
        ResumeLayout()
    End Sub
    Private Sub SetTheme()
        Static forecolor As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            forecolor = App.CurrentTheme.AccentTextColor
        Else
            forecolor = App.CurrentTheme.TextColor
            BackColor = App.CurrentTheme.BackColor
            TCClients.TabPanelBackColor = App.CurrentTheme.BackColor
        End If
        TCClients.ActiveTabColor = App.CurrentTheme.ButtonBackColor
        TCClients.ActiveTabForeColor = App.CurrentTheme.ButtonTextColor
        TCClients.InactiveTabColor = App.CurrentTheme.ControlBackColor
        TCClients.InActiveTabForeColor = forecolor
        LVClients.BackColor = App.CurrentTheme.BackColor
        LVClients.ForeColor = App.CurrentTheme.TextColor
        LVListeners.BackColor = App.CurrentTheme.BackColor
        LVListeners.ForeColor = App.CurrentTheme.TextColor
        ResumeLayout()
    End Sub
    Friend Sub SetColors()
        SetAccentColor(True)
        SetTheme()
    End Sub
    Friend Sub ReThemeMenus()
        App.ThemeMenu(CMClients)
    End Sub

End Class
