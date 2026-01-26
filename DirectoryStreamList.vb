
Imports SkyeMusic.Directory

Public Class DirectoryStreamList

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Friend ReadOnly Property SelectedUrl As String
        Get
            If LVStreams.SelectedItems.Count = 0 Then Return Nothing
            Return LVStreams.SelectedItems(0).Tag.ToString()
        End Get
    End Property

    ' Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            App.WriteToLog("Help WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Public Sub New(options As List(Of StreamOption))
        InitializeComponent()

        SetAccentColor()
        SetTheme()
        ReThemeMenus()
        CMStreamList.Font = CurrentTheme.SubBaseFont

        For Each o In options
            Dim item As New ListViewItem(o.Format)
            item.SubItems.Add(o.Bitrate.ToString())
            item.SubItems.Add(o.Url)
            item.Tag = o.Url
            LVStreams.Items.Add(item)
        Next
    End Sub
    Private Sub DirectoryStreamList_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FixedFrameBorderSize.Width - 4, -e.Y - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FixedFrameBorderSize.Width - 4, -e.Y - cSender.Top - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            End If
        End If
    End Sub
    Private Sub DirectoryStreamList_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub DirectoryStreamList_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        mMove = False
    End Sub
    Private Sub DirectoryStreamList_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub

    ' Control Events
    Private Sub LVStreams_DoubleClick(sender As Object, e As EventArgs) Handles LVStreams.DoubleClick
        If LVStreams.SelectedItems.Count > 0 Then
            DialogResult = DialogResult.OK
            Close()
        Else
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If LVStreams.SelectedItems.Count > 0 Then
            DialogResult = DialogResult.OK
            Close()
        Else
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub
    Private Sub CMICopy_Click(sender As Object, e As EventArgs) Handles CMICopy.Click
        If LVStreams.SelectedItems.Count > 0 Then
            Clipboard.SetText(LVStreams.SelectedItems(0).SubItems(2).Text)
        End If
    End Sub

    ' Methods
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsNormalWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsNormalWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsNormalWindow
        If location.Y < App.AdjustScreenBoundsNormalWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
    Private Sub SetAccentColor()
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            BackColor = c
        End If
        ResumeLayout()
        Debug.Print("History Accent Color Set")
    End Sub
    Private Sub SetTheme()
        SuspendLayout()

        If App.CurrentTheme.IsAccent Then
        Else
            BackColor = App.CurrentTheme.BackColor
        End If
        BtnOK.BackColor = App.CurrentTheme.ButtonBackColor
        BtnOK.ForeColor = App.CurrentTheme.ButtonTextColor
        LVStreams.BackColor = App.CurrentTheme.BackColor
        LVStreams.ForeColor = App.CurrentTheme.TextColor

        ResumeLayout()
        Debug.Print("History Theme Set")
    End Sub
    Friend Sub ReThemeMenus()
        App.ThemeMenu(CMStreamList)
    End Sub

End Class
