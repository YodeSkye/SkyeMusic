
Imports Skye.UI.Log
Imports Syncfusion.Windows.Forms.Tools

Public Class Log

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private CurrentAccentColor As Color 'Current Windows Accent Color
    Private LogViewerColors As New Skye.UI.Log.LogViewerControl.LogViewerColors
    Private DeleteLogConfirm As Boolean = False
    Private WithEvents TimerDeleteLog As New Timer

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
    Private Sub Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title + " Log"
        TimerDeleteLog.Interval = 5000
        SetAccentColor()
        SetTheme()
        ReThemeMenus()
#If DEBUG Then
        'If App.SaveWindowMetrics AndAlso App.LogSize.Height >= 0 Then Me.Size = App.LogSize
        'If App.SaveWindowMetrics AndAlso App.LogLocation.Y >= 0 Then Me.Location = App.LogLocation
#Else
        If App.Settings.SaveWindowMetrics AndAlso App.Settings.LogSize.Height >= 0 Then Me.Size = App.Settings.LogSize
        If App.Settings.SaveWindowMetrics AndAlso App.Settings.LogLocation.Y >= 0 Then Me.Location = App.Settings.LogLocation
#End If
    End Sub
    Private Sub Log_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        App.FrmLog.Dispose()
        App.FrmLog = Nothing
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
    End Sub
    Private Sub Log_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, LBLLogInfo.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
            App.Settings.LogLocation = Me.Location
        End If
    End Sub
    Private Sub Log_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, LBLLogInfo.MouseUp
        mMove = False
    End Sub
    Private Sub Log_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.Settings.LogLocation = Me.Location
        End If
    End Sub
    Private Sub Log_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.Settings.LogSize = Me.Size
        End If
    End Sub
    Private Sub Log_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Close()
    End Sub
    Private Sub Log_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        ToggleMaximized()
    End Sub

    ' Control Events
    Private Sub RTBLog_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs)
        RTBCMLog.ShortcutKeys(CType(sender, RichTextBox), e)
    End Sub
    Private Sub LBLLogInfo_DoubleClick(sender As Object, e As EventArgs) Handles LBLLogInfo.DoubleClick
        App.OpenFileLocation(Skye.Common.Log.LogFilePath)
    End Sub
    Private Sub BTNOK_Click(sender As Object, e As EventArgs) Handles BTNOK.Click
        Close()
    End Sub
    Private Sub BTNRefreshLog_Click(sender As Object, e As EventArgs)
        SetDeleteLogConfirm(True)
        ShowLog(True)
    End Sub
    Private Sub BTNDeleteLog_Click(sender As Object, e As EventArgs) Handles BTNDeleteLog.Click
        If DeleteLogConfirm Then
            App.DeleteLog()
        End If
        SetDeleteLogConfirm()
    End Sub

    ' Handlers
    Private Sub TimerDeleteLog_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerDeleteLog.Tick
        SetDeleteLogConfirm()
    End Sub

    ' Methods
    Private Sub ToggleMaximized()
        Select Case WindowState
            Case FormWindowState.Normal, FormWindowState.Minimized
                WindowState = FormWindowState.Maximized
            Case FormWindowState.Maximized
                WindowState = FormWindowState.Normal
        End Select
    End Sub
    Private Sub SetDeleteLogConfirm(Optional forcereset As Boolean = False)
        If DeleteLogConfirm Or forcereset Then
            TimerDeleteLog.Stop()
            DeleteLogConfirm = False
            Me.BTNDeleteLog.BackColor = App.CurrentTheme.ButtonBackColor
            TipLog.Hide(BTNDeleteLog)
        Else
            DeleteLogConfirm = True
            Me.BTNDeleteLog.BackColor = Color.Red
            TipLog.Show("Are You Sure?", Me, PointToClient(MousePosition))
            TimerDeleteLog.Start()
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
                LogViewerColors.TextBoxBack = CurrentAccentColor
            End If
            ResumeLayout()
            'Debug.Print("Log Accent Color Set")
            Skye.WinAPI.RedrawWindow(Me.Handle, IntPtr.Zero, IntPtr.Zero, Skye.WinAPI.RDW_INVALIDATE Or Skye.WinAPI.RDW_ERASE Or Skye.WinAPI.RDW_FRAME Or Skye.WinAPI.RDW_ALLCHILDREN Or Skye.WinAPI.RDW_UPDATENOW)
            'Debug.Print("Log Repainted")
        End If
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            LBLLogInfo.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LogViewerColors.TextBoxBack = App.CurrentTheme.BackColor
            LBLLogInfo.ForeColor = App.CurrentTheme.TextColor
        End If
        BTNDeleteLog.BackColor = App.CurrentTheme.ButtonBackColor
        BTNDeleteLog.ForeColor = App.CurrentTheme.TextColor
        TipLog.BackColor = App.CurrentTheme.BackColor
        TipLog.ForeColor = App.CurrentTheme.TextColor
        TipLog.BorderColor = App.CurrentTheme.ButtonBackColor
        LogViewerColors.Back = App.CurrentTheme.BackColor
        LogViewerColors.Fore = App.CurrentTheme.TextColor
        LogViewerColors.TextBoxBack = App.CurrentTheme.BackColor
        LogViewerColors.TextBoxFore = App.CurrentTheme.TextColor
        LogViewerColors.ButtonBack = App.CurrentTheme.ButtonBackColor
        LogViewerColors.ButtonFore = App.CurrentTheme.TextColor
        LogViewerColors.TooltipBack = App.CurrentTheme.BackColor
        LogViewerColors.TooltipFore = App.CurrentTheme.TextColor
        LogViewerColors.TooltipBorder = App.CurrentTheme.ButtonBackColor
        LogViewer.ApplyColors(LogViewerColors)
        ResumeLayout()
        'Debug.Print("Log Theme Set")
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor(True)
        SetTheme()
    End Sub
    Friend Sub ReThemeMenus()
        App.ThemeMenu(RTBCMLog)
    End Sub

End Class
