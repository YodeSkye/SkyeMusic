
Imports SkyeMusic.Player

Public Class PlayerMini

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private ImagePlay As Image
    Private ImagePause As Image
    Private ImageStop As Image
    Private ImagePrevious As Image
    Private ImageNext As Image
    Private WithEvents MarqueeTimer As New Timer With {.Interval = 30}

    ' Form Events
    Private Sub PlayerMini_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DoubleBuffered = True
        SetAccentColor()
        SetTheme()
        Size = New Size(100, 144)
        Text = My.Application.Info.Title & " Mini Player"
        LblTitle.Text = Player.PlaylistCurrentText
        ResetMarquee()
        MarqueeTimer.Start()
#If DEBUG Then
        If App.SaveWindowMetrics AndAlso App.PlayerMiniLocation.Y >= 0 Then
            Location = App.PlayerMiniLocation
        Else
            Dim wa = Screen.PrimaryScreen.WorkingArea
            Left = wa.Right - Width
            Top = wa.Bottom - Height
        End If
#Else
        If App.SaveWindowMetrics AndAlso App.PlayerMiniLocation.Y >= 0 Then
            Location = App.PlayerMiniLocation
        Else
            Dim wa = Screen.PrimaryScreen.WorkingArea
            Left = wa.Right - Width
            Top = wa.Bottom - Height
        End If
#End If
        AddHandler Player.TitleChanged, AddressOf OnTitleChanged

        If Player.MiniPlayerVisualizer IsNot Nothing Then
            AttachMiniVisualizer(Player.MiniPlayerVisualizer)
            Player.MiniPlayerVisualizer.Start()
        End If

    End Sub
    Private Sub PlayerMini_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, PicBoxAlbumArt.MouseDown, LblTitle.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X, -e.Y)
            Else
                mOffset = New Point(-e.X - cSender.Left, -e.Y - cSender.Top)
            End If
        End If
    End Sub
    Private Sub PlayerMini_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, PicBoxAlbumArt.MouseMove, LblTitle.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
            PlayerMiniLocation = Location
        End If
    End Sub
    Private Sub PlayerMini_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, PicBoxAlbumArt.MouseUp, LblTitle.MouseUp
        mMove = False
    End Sub
    Private Sub PlayerMini_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.PlayerMiniLocation = Me.Location
        End If
    End Sub
    'Protected Overrides Sub OnResize(e As EventArgs)
    '    MyBase.OnResize(e)

    '    Dim radius As Integer = 20
    '    Dim diameter As Integer = radius * 2
    '    Dim rect As New Rectangle(0, 0, Width, Height)

    '    Dim path As New Drawing2D.GraphicsPath()
    '    ' Top-left corner
    '    path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
    '    ' Top-right corner
    '    path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
    '    ' Bottom-right corner
    '    path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
    '    ' Bottom-left corner
    '    path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
    '    path.CloseFigure()

    '    Region = New Region(path)
    'End Sub
    Private Sub PlayerMini_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick, PicBoxAlbumArt.DoubleClick
        App.SetMiniPlayer()
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            App.SetMiniPlayer()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.None

        ' Base border color from your theme
        Dim baseColor As Color = CurrentTheme.ButtonBackColor

        ' Create lighter + darker tones
        Dim light As Color = ControlPaint.Light(baseColor)
        Dim mid As Color = ControlPaint.LightLight(baseColor)
        Dim dark As Color = ControlPaint.Dark(baseColor)

        ' Outer border
        Using p As New Pen(light, 1)
            g.DrawRectangle(p, 0, 0, Me.Width - 1, Me.Height - 1)
        End Using

        ' mid border
        Using p As New Pen(mid, 1)
            g.DrawRectangle(p, 2, 2, Me.Width - 5, Me.Height - 5)
        End Using

        ' Inner border
        Using p As New Pen(dark, 1)
            g.DrawRectangle(p, 1, 1, Me.Width - 3, Me.Height - 3)
        End Using
    End Sub
    Private Sub PlayerMini_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Player.MiniPlayerVisualizer = Nothing
    End Sub

    ' Control Events
    Private Sub BtnPlay_Click(sender As Object, e As EventArgs) Handles BtnPlay.Click
        Player.TogglePlay()
        SetPlayState()
    End Sub
    Private Sub BtnStop_Click(sender As Object, e As EventArgs) Handles BtnStop.Click
        Player.StopPlay()
        SetPlayState()
    End Sub
    Private Sub BtnPrevious_Click(sender As Object, e As EventArgs) Handles BtnPrevious.Click
        Player.PlayPrevious()
        SetPlayState()
    End Sub
    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click
        Player.PlayNext()
        SetPlayState()
    End Sub

    ' Handlers
    Private Sub OnTitleChanged(newTitle As String)
        If IsDisposed Then Return
        LblTitle.Text = newTitle
        ResetMarquee()
    End Sub
    Private Sub MarqueeTimer_Tick(sender As Object, e As EventArgs) Handles MarqueeTimer.Tick
        LblTitle.Left -= 1 ' scroll speed

        ' When it scrolls off the left side, reset it
        If LblTitle.Right < 0 Then
            LblTitle.Left = PanelMarquee.Width
        End If
    End Sub

    ' Methods
    Friend Sub SetPlayState()
        Select Case Player.PlayState
            Case Player.PlayStates.Playing
                BtnPlay.Image = ImagePause
            Case Player.PlayStates.Paused, Player.PlayStates.Stopped
                BtnPlay.Image = ImagePlay
        End Select
    End Sub
    Friend Sub SetAlbumArt(img As Image)
        If img Is Nothing Then
            PicBoxAlbumArt.Image = Nothing
            PanelVisualizer.BringToFront()
        Else
            PicBoxAlbumArt.Image = App.ResizeImage(img, PicBoxAlbumArt.Width)
            PicBoxAlbumArt.BringToFront()
        End If
    End Sub
    Private Sub ResetMarquee()
        LblTitle.Left = PanelMarquee.Width
    End Sub
    Friend Sub AttachMiniVisualizer(v As Player.IVisualizer)
        Dim ctrl = v.DockedControl
        ctrl.Dock = DockStyle.Fill
        PanelVisualizer.Controls.Clear()
        PanelVisualizer.Controls.Add(ctrl)
    End Sub
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
            LblTitle.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblTitle.ForeColor = App.CurrentTheme.TextColor
        End If
        BtnPlay.BackColor = App.CurrentTheme.ButtonBackColor
        BtnStop.BackColor = App.CurrentTheme.ButtonBackColor
        BtnPrevious.BackColor = App.CurrentTheme.ButtonBackColor
        BtnNext.BackColor = App.CurrentTheme.ButtonBackColor
        ImagePlay = App.ResizeImage(App.TintIcon(My.Resources.ImageMiniPlayerPlay, CurrentTheme.ButtonTextColor), 16)
        ImagePause = App.ResizeImage(App.TintIcon(My.Resources.ImageMiniPlayerPause, CurrentTheme.ButtonTextColor), 16)
        ImageStop = App.ResizeImage(App.TintIcon(My.Resources.ImageMiniPlayerStop, CurrentTheme.ButtonTextColor), 16)
        ImagePrevious = App.ResizeImage(App.TintIcon(My.Resources.ImageMiniPlayerPrevious, CurrentTheme.ButtonTextColor), 16)
        ImageNext = App.ResizeImage(App.TintIcon(My.Resources.ImageMiniPlayerNext, CurrentTheme.ButtonTextColor), 16)
        SetPlayState()
        BtnStop.Image = ImageStop
        BtnPrevious.Image = ImagePrevious
        BtnNext.Image = ImageNext
        ResumeLayout()
        Debug.Print("About Theme Set")
    End Sub
    Friend Sub SetColors() 'Used By Options Form
        SetAccentColor()
        SetTheme()
    End Sub

End Class
