
Public Class PlayerMini

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point

    ' Form Events
    Private Sub PlayerMini_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DoubleBuffered = True
        Size = New Size(100, 100)
        SetAccentColor()
        SetTheme()
        Text = My.Application.Info.Title & " Mini Player"
#If DEBUG Then
        'If App.SaveWindowMetrics AndAlso App.PlayerMiniLocation.Y >= 0 Then
        '    Location = App.PlayerMiniLocation
        'Else
        Dim wa = Screen.PrimaryScreen.WorkingArea
        Left = wa.Right - Width
        Top = wa.Bottom - Height
        'End If
#Else
        If App.SaveWindowMetrics AndAlso App.PlayerMiniLocation.Y >= 0 Then
            Location = App.PlayerMiniLocation
        Else
            Dim wa = Screen.PrimaryScreen.WorkingArea
            Left = wa.Right - Width
            Top = wa.Bottom - Height
        End If
#End If
    End Sub
    Private Sub PlayerMini_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X, -e.Y)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FrameBorderSize.Width - 4, -e.Y - cSender.Top - SystemInformation.FrameBorderSize.Height - 4)
            End If
        End If
    End Sub
    Private Sub PlayerMini_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
            App.PlayerMiniLocation = Me.Location
        End If
    End Sub
    Private Sub PlayerMini_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        mMove = False
    End Sub
    Private Sub PlayerMini_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.PlayerMiniLocation = Me.Location
        End If
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        Dim radius As Integer = 20
        Dim diameter As Integer = radius * 2
        Dim rect As New Rectangle(0, 0, Width, Height)

        Dim path As New Drawing2D.GraphicsPath()
        ' Top-left corner
        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
        ' Top-right corner
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
        ' Bottom-right corner
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
        ' Bottom-left corner
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
        path.CloseFigure()

        Region = New Region(path)
    End Sub

    ' Control Events
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        App.SetMiniPlayer()
    End Sub

    ' Methods
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Width
        If location.Y + Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Height
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left
        If location.Y < My.Computer.Screen.WorkingArea.Top Then location.Y = My.Computer.Screen.WorkingArea.Top
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
            'LblAbout.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            'LblAbout.ForeColor = App.CurrentTheme.TextColor
        End If
        BtnClose.BackColor = App.CurrentTheme.ButtonBackColor
        'TipAbout.BackColor = App.CurrentTheme.BackColor
        'TipAbout.ForeColor = App.CurrentTheme.TextColor
        'TipAbout.BorderColor = App.CurrentTheme.ButtonBackColor

        Dim baseIcon As Image = My.Resources.ImageMiniPlayerClose
        Dim tinted As Image = App.TintIcon(baseIcon, CurrentTheme.ButtonTextColor)
        Dim finalIcon As Image = App.ResizeImage(tinted, 16)
        BtnClose.Image = finalIcon

        ResumeLayout()
        Debug.Print("About Theme Set")
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor()
        SetTheme()
    End Sub

End Class
