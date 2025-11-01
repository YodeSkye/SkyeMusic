
Imports SkyeMusic.My

Public Class Log

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private CurrentAccentColor As Color 'Current Windows Accent Color
    Private LogSearchTitle As String
    Private DeleteLogConfirm As Boolean = False
    Private WithEvents timerDeleteLog As New Timer

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            App.WriteToLog("Log WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title + " Log"
        LogSearchTitle = TxBxSearch.Text
        timerDeleteLog.Interval = 5000
        SetAccentColor()
        SetTheme()
#If DEBUG Then
        'If App.SaveWindowMetrics AndAlso App.LogLocation.Y >= 0 Then Me.Location = App.LogLocation
        'If App.SaveWindowMetrics AndAlso App.LogSize.Height >= 0 Then Me.Size = App.LogSize
#Else
        If App.SaveWindowMetrics AndAlso App.LogLocation.Y >= 0 Then Me.Location = App.LogLocation
        If App.SaveWindowMetrics AndAlso App.LogSize.Height >= 0 Then Me.Size = App.LogSize
#End If
    End Sub
    Private Sub Log_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        App.FRMLog.Dispose()
        App.FRMLog = Nothing
    End Sub
    Private Sub Log_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, LBLLogInfo.MouseDown
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
        cSender = Nothing
    End Sub
    Private Sub Log_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, LBLLogInfo.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
            App.LogLocation = Me.Location
        End If
    End Sub
    Private Sub Log_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, LBLLogInfo.MouseUp
        mMove = False
    End Sub
    Private Sub Log_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.LogLocation = Me.Location
        End If
    End Sub
    Private Sub Log_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.LogSize = Me.Size
        End If
    End Sub
    Private Sub Log_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Not TxBxSearch.Focused Then
            If e.KeyData = Keys.Escape Then Me.Close()
        End If
    End Sub
    Private Sub Log_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        ToggleMaximized()
    End Sub

    'Control Events
    Private Sub RTBLog_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles RTBLog.PreviewKeyDown
        RTBCMLog.ShortcutKeys(CType(sender, System.Windows.Forms.RichTextBox), e)
    End Sub
    Private Sub LBLLogInfo_DoubleClick(sender As Object, e As EventArgs) Handles LBLLogInfo.DoubleClick
        App.OpenFileLocation(App.LogPath)
    End Sub
    Private Sub BTNOK_Click(sender As Object, e As EventArgs) Handles BTNOK.Click
        Close()
    End Sub
    Private Sub BTNRefreshLog_Click(sender As Object, e As EventArgs) Handles BTNRefreshLog.Click
        SetDeleteLogConfirm(True)
        App.ShowLog(True)
    End Sub
    Private Sub BTNDeleteLog_Click(sender As Object, e As EventArgs) Handles BTNDeleteLog.Click
        If DeleteLogConfirm Then
            App.DeleteLog()
        End If
        SetDeleteLogConfirm()
    End Sub
    Private Sub TxBxSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxBxSearch.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Escape)
                ResetRTBLogFind()
                RTBLog.Focus()
                e.Handled = True
        End Select
    End Sub
    Private Sub TxBxSearch_Enter(sender As Object, e As EventArgs) Handles TxBxSearch.Enter
        If TxBxSearch.Text = LogSearchTitle Then TxBxSearch.ResetText()
    End Sub
    Private Sub TxBxSearch_Leave(sender As Object, e As EventArgs) Handles TxBxSearch.Leave
        TxBxSearch.Text = LogSearchTitle
        TxBxSearch.ForeColor = App.CurrentTheme.InactiveSearchTextColor
    End Sub
    Private Sub TxBxSearch_TextChanged(sender As Object, e As EventArgs) Handles TxBxSearch.TextChanged
        If TxBxSearch.Text Is String.Empty Or RTBLog.Focused Then
            ResetRTBLogFind()
        ElseIf TxBxSearch.Text.Length <= 4 Then
            If App.CurrentTheme.IsAccent Then
                TxBxSearch.ForeColor = App.CurrentTheme.AccentTextColor
            Else
                TxBxSearch.ForeColor = App.CurrentTheme.TextColor
            End If
            ResetRTBLogFind()
            ElseIf Not TxBxSearch.Text = LogSearchTitle AndAlso TxBxSearch.Text.Length > 4 AndAlso IsHandleCreated Then
                Debug.Print("Searching Log...")
            LblStatus.Visible = True
            LblStatus.Refresh()
            Dim foundindex As Integer
            Dim searchtext As String = RTBLog.Text
            ResetRTBLogFind()
            'Try To Find First Occurrence
            foundindex = searchtext.IndexOf(TxBxSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase)
            If foundindex < 0 Then
                TxBxSearch.ForeColor = Color.Red
            Else
                If App.CurrentTheme.IsAccent Then
                    TxBxSearch.ForeColor = App.CurrentTheme.AccentTextColor
                Else
                    TxBxSearch.ForeColor = App.CurrentTheme.TextColor
                End If
                RTBLog.Select(foundindex, 0)
                    RTBLog.ScrollToCaret()
                End If
                Do Until foundindex < 0
                'Highlight Current Match
                RTBLog.SelectionStart = foundindex
                RTBLog.SelectionLength = TxBxSearch.Text.Length
                If App.CurrentTheme.IsAccent Then
                    RTBLog.SelectionBackColor = App.CurrentTheme.AccentTextColor
                Else
                    RTBLog.SelectionBackColor = App.CurrentTheme.TextColor
                End If
                RTBLog.SelectionColor = App.CurrentTheme.BackColor
                'Try To Find Next Occurrence
                foundindex = searchtext.IndexOf(TxBxSearch.Text, foundindex + TxBxSearch.Text.Length, StringComparison.CurrentCultureIgnoreCase)
            Loop
            LblStatus.Visible = False
        End If
    End Sub

    'Handlers
    Private Sub timerDeleteLog_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timerDeleteLog.Tick
        SetDeleteLogConfirm()
    End Sub

    'Procedures
    Private Sub ToggleMaximized()
        Select Case WindowState
            Case FormWindowState.Normal, FormWindowState.Minimized
                WindowState = FormWindowState.Maximized
            Case FormWindowState.Maximized
                WindowState = FormWindowState.Normal
        End Select
    End Sub
    Private Sub ResetRTBLogFind()
        RTBLog.SelectAll()
        RTBLog.SelectionBackColor = App.CurrentTheme.BackColor
        RTBLog.SelectionColor = App.CurrentTheme.TextColor
        RTBLog.DeselectAll()
        RTBLog.SelectionStart = RTBLog.TextLength
        RTBLog.SelectionLength = 0
        RTBLog.ScrollToCaret()
    End Sub
    Private Sub SetDeleteLogConfirm(Optional forcereset As Boolean = False)
        If DeleteLogConfirm Or forcereset Then
            timerDeleteLog.Stop()
            DeleteLogConfirm = False
            Me.BTNDeleteLog.BackColor = App.CurrentTheme.ButtonBackColor
            TipLog.HideDelay = 500
            TipLog.HideTooltip()
        Else
            DeleteLogConfirm = True
            Me.BTNDeleteLog.BackColor = Color.Red
            TipLog.HideDelay = 5000
            TipLog.ShowTooltipAtCursor("Are You Sure?")
            timerDeleteLog.Start()
        End If
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
    Private Sub SetAccentColor(Optional force As Boolean = False)
        Dim accent As Color = App.GetAccentColor()
        If CurrentAccentColor <> accent OrElse force Then
            CurrentAccentColor = accent
            SuspendLayout()
            If App.CurrentTheme.IsAccent Then
                BackColor = CurrentAccentColor
                TxBxSearch.BackColor = CurrentAccentColor
            End If
            ResumeLayout()
            Debug.Print("Log Accent Color Set")
            Skye.WinAPI.RedrawWindow(Me.Handle, IntPtr.Zero, IntPtr.Zero, Skye.WinAPI.RDW_INVALIDATE Or Skye.WinAPI.RDW_ERASE Or Skye.WinAPI.RDW_FRAME Or Skye.WinAPI.RDW_ALLCHILDREN Or Skye.WinAPI.RDW_UPDATENOW)
            Debug.Print("Log Repainted")
        End If
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            LBLLogInfo.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            TxBxSearch.BackColor = App.CurrentTheme.BackColor
            LBLLogInfo.ForeColor = App.CurrentTheme.TextColor
        End If
        RTBLog.BackColor = App.CurrentTheme.BackColor
        RTBLog.ForeColor = App.CurrentTheme.TextColor
        BTNDeleteLog.BackColor = App.CurrentTheme.ButtonBackColor
        BTNDeleteLog.ForeColor = App.CurrentTheme.TextColor
        BTNRefreshLog.BackColor = App.CurrentTheme.ButtonBackColor
        BTNRefreshLog.ForeColor = App.CurrentTheme.TextColor
        TipLog.BackColor = App.CurrentTheme.BackColor
        TipLog.ForeColor = App.CurrentTheme.TextColor
        TipLog.BorderColor = App.CurrentTheme.ButtonBackColor
        If TxBxSearch.Text = LogSearchTitle Then TxBxSearch.ForeColor = App.CurrentTheme.InactiveSearchTextColor
        ResumeLayout()
        Debug.Print("Log Theme Set")
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor(True)
        SetTheme()
    End Sub

End Class
