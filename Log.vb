
Imports SkyeMusic.My
Public Class Log

    'Declarations
    Private LogSearchTitle As String

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
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
    Private Sub Log_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            If Left < 0 Then
                Left = -App.AdjustScreenBoundsNormalWindow
            ElseIf Right > My.Computer.Screen.WorkingArea.Width Then
                Left = My.Computer.Screen.WorkingArea.Width - Width + App.AdjustScreenBoundsNormalWindow
            End If
            If Top < App.AdjustScreenBoundsNormalWindow Then
                Top = 0
            ElseIf Bottom > My.Computer.Screen.WorkingArea.Height Then
                Top = My.Computer.Screen.WorkingArea.Height - Height + App.AdjustScreenBoundsNormalWindow
            End If
            App.LogLocation = Me.Location
        End If
    End Sub
    Private Sub Log_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.LogSize = Me.Size
        End If
    End Sub
    Private Sub Log_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub
    Private Sub Log_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        ToggleMaximized()
    End Sub

    'Control Events
    Private Sub RTBLog_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles RTBLog.PreviewKeyDown
        RTBCMLog.ShortcutKeys(sender, e)
    End Sub
    Private Sub RTBLog_MouseUp(sender As Object, e As MouseEventArgs) Handles RTBLog.MouseUp
        RTBCMLog.Display(sender, e)
    End Sub
    Private Sub LBLLogInfo_DoubleClick(sender As Object, e As EventArgs) Handles LBLLogInfo.DoubleClick
        ToggleMaximized()
    End Sub
    Private Sub BTNOK_Click(sender As Object, e As EventArgs) Handles BTNOK.Click
        Close()
    End Sub
    Private Sub BTNRefreshLog_Click(sender As Object, e As EventArgs) Handles BTNRefreshLog.Click
        App.ShowLog(True)
    End Sub
    Private Sub BTNDeleteLog_Click(sender As Object, e As EventArgs) Handles BTNDeleteLog.Click
        App.DeleteLog()
    End Sub
    Private Sub TxBxSearch_DoubleClick(sender As Object, e As EventArgs) Handles TxBxSearch.DoubleClick
        ToggleMaximized()
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
            Select Case App.Theme
                Case App.Themes.Accent
                    TxBxSearch.ForeColor = App.CurrentTheme.AccentTextColor
                Case Else
                    TxBxSearch.ForeColor = App.CurrentTheme.TextColor
            End Select
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
                Select Case App.Theme
                    Case App.Themes.Accent
                        TxBxSearch.ForeColor = App.CurrentTheme.AccentTextColor
                    Case Else
                        TxBxSearch.ForeColor = App.CurrentTheme.TextColor
                End Select
                RTBLog.Select(foundindex, 0)
                RTBLog.ScrollToCaret()
            End If
            Do Until foundindex < 0
                'Highlight Current Match
                RTBLog.SelectionStart = foundindex
                RTBLog.SelectionLength = TxBxSearch.Text.Length
                Select Case App.Theme
                    Case App.Themes.Accent, App.Themes.Light
                        RTBLog.SelectionBackColor = App.CurrentTheme.AccentTextColor
                    Case Else
                        RTBLog.SelectionBackColor = App.CurrentTheme.TextColor
                End Select
                RTBLog.SelectionColor = Color.Red
                'Try To Find Next Occurrence
                foundindex = searchtext.IndexOf(TxBxSearch.Text, foundindex + TxBxSearch.Text.Length, StringComparison.CurrentCultureIgnoreCase)
            Loop
            LblStatus.Visible = False
        End If
    End Sub

    'Procedures
    Private Sub SetAccentColor(Optional AsTheme As Boolean = False)
        Static c As Color
        If Not AsTheme Then SuspendLayout()
        If App.Theme = App.Themes.Accent Then
            c = App.GetAccentColor()
            BackColor = c
            TxBxSearch.BackColor = c
        End If
        If Not AsTheme Then ResumeLayout()
        Debug.Print("Log Accent Color Set")
    End Sub
    Friend Sub SetTheme()
        SuspendLayout()
        If App.Theme = App.Themes.Accent Then
            SetAccentColor(True)
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
        If TxBxSearch.Text = LogSearchTitle Then TxBxSearch.ForeColor = App.CurrentTheme.InactiveSearchTextColor
        ResumeLayout()
        Debug.Print("Log Theme Set")
    End Sub
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
    End Sub


End Class