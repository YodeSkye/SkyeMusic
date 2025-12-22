
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window
Imports SkyeMusic.My
Public Class About

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
            App.WriteToLog("About WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub AboutLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        SetAccentColor()
        SetTheme()
        Text = "About " + My.Application.Info.Title
        LblAbout.Text = My.Application.Info.Description
        LblVersion.Text = "v" + My.Application.Info.Version.Major.ToString + "." + My.Application.Info.Version.Minor.ToString + "." + My.Application.Info.Version.Build.ToString
        LLblVLCSharp.Image = App.ResizeImage(My.Resources.ImageAttributionVLCSharp, 16)
        LLblNAudio.Image = App.ResizeImage(My.Resources.ImageAttributionNAudio, 16)
        BtnOK.Select()
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
    Private Sub BtnChangeLog_Click(sender As Object, e As EventArgs) Handles BtnChangeLog.Click
        App.ShowChangeLog()
    End Sub
    Private Sub LblVersion_DoubleClick(sender As Object, e As EventArgs) Handles LblVersion.DoubleClick
        Dim classname As New System.Text.StringBuilder(256)
        Dim length As Integer = Skye.WinAPI.GetClassName(Player.Handle, classname, classname.Capacity)
        If length > 0 Then
            My.Computer.Clipboard.SetText(classname.ToString.TrimEnd(Chr(0), CChar(" "))) 'Remove null terminator & trailing spaces from class name
            Debug.Print(classname.ToString.TrimEnd(Chr(0), CChar(" ")))
        End If
    End Sub
    Private Sub LLblMicrosoft_MouseEnter(sender As Object, e As EventArgs) Handles LLblMicrosoft.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblMicrosoft_MouseLeave(sender As Object, e As EventArgs) Handles LLblMicrosoft.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblMicrosoft_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblMicrosoft.MouseClick
        OpenLink(App.AttributionMicrosoft)
    End Sub
    Private Sub LLblVLCSharp_MouseEnter(sender As Object, e As EventArgs) Handles LLblVLCSharp.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblVLCSharp_MouseLeave(sender As Object, e As EventArgs) Handles LLblVLCSharp.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblVLCSharp_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblVLCSharp.MouseClick
        OpenLink(App.AttributionVLC)
    End Sub
    Private Sub LLblNAudio_MouseEnter(sender As Object, e As EventArgs) Handles LLblNAudio.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblNAudio_MouseLeave(sender As Object, e As EventArgs) Handles LLblNAudio.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblNAudio_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblNAudio.MouseClick
        OpenLink(App.AttributionNAudio)
    End Sub
    Private Sub LLblSQLite_MouseEnter(sender As Object, e As EventArgs) Handles LLblSQLite.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblSQLite_MouseLeave(sender As Object, e As EventArgs) Handles LLblSQLite.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblSQLite_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblSQLite.MouseClick
        OpenLink(App.AttributionSQLite)
    End Sub
    Private Sub LLblSyncFusion_MouseEnter(sender As Object, e As EventArgs) Handles LLblSyncFusion.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblSyncFusion_MouseLeave(sender As Object, e As EventArgs) Handles LLblSyncFusion.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblSyncFusion_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblSyncFusion.MouseClick
        OpenLink(App.AttributionSyncFusion)
    End Sub
    Private Sub LLblTagLibSharp_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblTagLibSharp.MouseClick
        OpenLink(App.AttributionTagLibSharp)
    End Sub
    Private Sub LLblMusicBrainz_MouseEnter(sender As Object, e As EventArgs) Handles LLblMusicBrainz.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblMusicBrainz_MouseLeave(sender As Object, e As EventArgs) Handles LLblMusicBrainz.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblMusicBrainz_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblMusicBrainz.MouseClick
        OpenLink(App.AttributionMusicBrainz)
    End Sub
    Private Sub LLblIcons8_MouseEnter(sender As Object, e As EventArgs) Handles LLblIcons8.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblIcons8_MouseLeave(sender As Object, e As EventArgs) Handles LLblIcons8.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblIcons8_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblIcons8.MouseClick
        OpenLink(App.AttributionIcons8)
    End Sub

    'Procedures
    Public Sub OpenLink(target As String)
        Dim pInfo As New Diagnostics.ProcessStartInfo With {
        .UseShellExecute = True,
        .FileName = target}
        Try
            Diagnostics.Process.Start(pInfo)
        Catch ex As Exception
            WriteToLog("Cannot Open " & target & vbCr & ex.Message)
        End Try
    End Sub
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
        Debug.Print("About Accent Color Set")
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            LblAbout.ForeColor = App.CurrentTheme.AccentTextColor
            LblVersion.ForeColor = App.CurrentTheme.AccentTextColor
            LLblMicrosoft.LinkColor = App.CurrentTheme.AccentTextColor
            LLblVLCSharp.LinkColor = App.CurrentTheme.AccentTextColor
            LLblNAudio.LinkColor = App.CurrentTheme.AccentTextColor
            LLblSyncFusion.LinkColor = App.CurrentTheme.AccentTextColor
            LLblTagLibSharp.LinkColor = App.CurrentTheme.AccentTextColor
            LLblIcons8.LinkColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblAbout.ForeColor = App.CurrentTheme.TextColor
            LblVersion.ForeColor = App.CurrentTheme.TextColor
            LLblMicrosoft.LinkColor = App.CurrentTheme.TextColor
            LLblVLCSharp.LinkColor = App.CurrentTheme.TextColor
            LLblNAudio.LinkColor = App.CurrentTheme.TextColor
            LLblSyncFusion.LinkColor = App.CurrentTheme.TextColor
            LLblTagLibSharp.LinkColor = App.CurrentTheme.TextColor
            LLblIcons8.LinkColor = App.CurrentTheme.TextColor
        End If
        LLblMicrosoft.ActiveLinkColor = App.CurrentTheme.ButtonBackColor
        LLblVLCSharp.ActiveLinkColor = App.CurrentTheme.ButtonBackColor
        LLblNAudio.ActiveLinkColor = App.CurrentTheme.ButtonBackColor
        LLblSyncFusion.ActiveLinkColor = App.CurrentTheme.ButtonBackColor
        LLblTagLibSharp.ActiveLinkColor = App.CurrentTheme.ButtonBackColor
        LLblIcons8.ActiveLinkColor = App.CurrentTheme.ButtonBackColor
        BtnChangeLog.BackColor = App.CurrentTheme.ButtonBackColor
        TipAbout.BackColor = App.CurrentTheme.BackColor
        TipAbout.ForeColor = App.CurrentTheme.TextColor
        TipAbout.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
        Debug.Print("About Theme Set")
    End Sub

End Class
