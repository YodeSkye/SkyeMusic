
Imports System.Drawing
Imports System.Windows.Forms
Imports Skye.WinAPI

Friend Class Splash

    Private hWnd As IntPtr = IntPtr.Zero
    Private _logo As Image
    Private _appName As String
    Private _status As String
    Private ReadOnly TitleFont As New Font("Segoe UI", 30.0F, FontStyle.Bold)
    Private ReadOnly StatusFont As New Font("Segoe UI", 12.0F, FontStyle.Regular)
    Private Const WindowWidth As Integer = 450
    Private Const WindowHeight As Integer = 250
    Private Const LogoSize As Integer = 64

    ''' <summary>
    ''' Creates and renders the native splash screen window centered on the primary screen.
    ''' </summary>
    Friend Sub ShowSplash(logo As Image, appName As String, initialStatus As String)
        If hWnd = IntPtr.Zero Then CreateWindow()

        _logo = logo
        _appName = appName
        _status = initialStatus

        ' Center on primary screen
        Dim wa As Rectangle = Screen.PrimaryScreen.Bounds
        Dim x As Integer = wa.Left + (wa.Width - WindowWidth) \ 2
        Dim y As Integer = wa.Top + (wa.Height - WindowHeight) \ 2

        MoveWindow(hWnd, x, y, WindowWidth, WindowHeight, True)

        ' Display window topmost without taking input focus
        ShowWindow(hWnd, SW_SHOWNOACTIVATE)
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0,
                     SWP_NOMOVE Or SWP_NOSIZE Or SWP_NOACTIVATE Or SWP_SHOWWINDOW)

        UpdateWindow(hWnd)
        DrawContent()
    End Sub
    ''' <summary>
    ''' Dynamically updates loading progress text (e.g., "Loading settings...", "Initializing LibVLC...").
    ''' </summary>
    Friend Sub UpdateStatus(statusText As String)
        If hWnd = IntPtr.Zero Then Exit Sub
        _status = statusText
        DrawContent()
        ' Give the user's eyes just enough time to register the text step on high-speed CPUs
        System.Threading.Thread.Sleep(100)
    End Sub
    ''' <summary>
    ''' Immediately closes and destroys the native splash window without fading.
    ''' </summary>
    Friend Sub CloseSplash()
        If hWnd = IntPtr.Zero Then Exit Sub

        ' Hide and destroy native window handle
        ShowWindow(hWnd, SW_HIDE)
        DestroyWindow(hWnd)
        hWnd = IntPtr.Zero
    End Sub
    ''' <summary>
    ''' Fades out the native splash window and destroys its handle.
    ''' </summary>
    Friend Async Sub CloseSplashWithFade()
        If hWnd = IntPtr.Zero Then Exit Sub

        For a As Integer = 255 To 0 Step -15
            SetLayeredWindowAttributes(hWnd, 0, CByte(Math.Max(0, a)), LWA_ALPHA)
            Await Task.Delay(10)
        Next

        ShowWindow(hWnd, SW_HIDE)
        DestroyWindow(hWnd)
        hWnd = IntPtr.Zero
    End Sub

    Private Sub CreateWindow()
        If hWnd <> IntPtr.Zero Then Exit Sub

        ' WS_EX_LAYERED enables opacity/fading; WS_EX_TOOLWINDOW hides it from Taskbar
        Dim exStyle As Integer = WS_EX_TOPMOST Or WS_EX_TOOLWINDOW Or WS_EX_NOACTIVATE Or WS_EX_LAYERED
        Dim style As Integer = WS_POPUP

        ' Create native STATIC window class instance
        hWnd = CreateWindowEx(exStyle, "STATIC", String.Empty, style, 0, 0, WindowWidth, WindowHeight,
                              IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero)

        If hWnd = IntPtr.Zero Then
            Throw New Exception("Native splash window creation failed.")
        End If

        ' Strip borders and frame edge
        Dim s As Integer = GetWindowLong(hWnd, GWL_STYLE)
        SetWindowLong(hWnd, GWL_STYLE, s And Not WS_BORDER)

        Dim es As Integer = GetWindowLong(hWnd, GWL_EXSTYLE)
        SetWindowLong(hWnd, GWL_EXSTYLE, es And Not WS_EX_CLIENTEDGE)

        ' Full initial opacity
        SetLayeredWindowAttributes(hWnd, 0, 255, LWA_ALPHA)

        ApplyDwmAttributes()
    End Sub
    Private Sub DrawContent()
        If hWnd = IntPtr.Zero Then Exit Sub

        Dim BackColor As Color = App.CrimsonEmberTheme.BackColor
        Dim TextColor As Color = App.CrimsonEmberTheme.TextColor
        Dim SubTextColor As Color = App.CrimsonEmberTheme.TextColor

        Dim rc As RECT
        If Not GetClientRect(hWnd, rc) Then Exit Sub

        Dim w As Integer = rc.Right - rc.Left
        Dim h As Integer = rc.Bottom - rc.Top

        Dim hDC As IntPtr = GetDC(hWnd)
        If hDC = IntPtr.Zero Then Exit Sub

        Using g As Graphics = Graphics.FromHdc(hDC)
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

            ' Fill Background
            Using bg As New SolidBrush(BackColor)
                g.FillRectangle(bg, 0, 0, w, h)
            End Using

            ' Draw Logo Centered Top
            Dim logoWidth As Integer = _logo.Width
            Dim logoHeight As Integer = _logo.Height
            Dim logoX As Integer = (w - logoWidth) \ 2
            Dim logoY As Integer = 25

            If _logo IsNot Nothing Then
                Dim oldInterpolation = g.InterpolationMode
                Dim oldPixelOffset = g.PixelOffsetMode

                g.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half
                g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor

                ' Crop 2 pixels off outer perimeter
                Dim cropPixels As Integer = 2
                Dim srcRect As New Rectangle(
                    cropPixels,
                    cropPixels,
                    logoWidth - (cropPixels * 2),
                    logoHeight - (cropPixels * 2)
                )

                Dim destRect As New Rectangle(
                    logoX + cropPixels,
                    logoY + cropPixels,
                    srcRect.Width,
                    srcRect.Height
                )

                g.DrawImage(_logo, destRect, srcRect, GraphicsUnit.Pixel)

                g.InterpolationMode = oldInterpolation
                g.PixelOffsetMode = oldPixelOffset

                ' Draw Application Title directly under logo (10px gap)
                Dim titleY As Integer = logoY + logoHeight + 10
                Using br As New SolidBrush(TextColor)
                    Dim titleSize As SizeF = g.MeasureString(_appName, TitleFont)
                    Dim titleX As Single = (w - titleSize.Width) / 2
                    g.DrawString(_appName, TitleFont, br, titleX, titleY)
                End Using
            Else
                ' Fallback position if no logo is provided
                Dim titleY As Integer = 30
                Using br As New SolidBrush(TextColor)
                    Dim titleSize As SizeF = g.MeasureString(_appName, TitleFont)
                    Dim titleX As Single = (w - titleSize.Width) / 2
                    g.DrawString(_appName, TitleFont, br, titleX, titleY)
                End Using
            End If

            ' Draw Status Text Centered Bottom
            Dim statusY As Integer = h - 40
            Using brSub As New SolidBrush(SubTextColor)
                Dim statusSize As SizeF = g.MeasureString(_status, StatusFont)
                Dim statusX As Single = (w - statusSize.Width) / 2
                g.DrawString(_status, StatusFont, brSub, statusX, statusY)
            End Using
        End Using

        ReleaseDC(hWnd, hDC)
    End Sub
    Private Sub ApplyDwmAttributes()
        Const DWMWA_WINDOW_CORNER_PREFERENCE As Integer = 33
        Const DWMWCP_ROUND As Integer = 2
        Const DWMWA_USE_IMMERSIVE_DARK_MODE As Integer = 20

        ' Enable Windows 11 Rounded Corners
        Dim cornerPref As Integer = DWMWCP_ROUND
        DwmSetWindowAttribute(hWnd, DWMWA_WINDOW_CORNER_PREFERENCE, cornerPref, 4)

        ' Enable Dark Mode Frame Bordering
        Dim darkMode As Integer = 1
        DwmSetWindowAttribute(hWnd, DWMWA_USE_IMMERSIVE_DARK_MODE, darkMode, 4)
    End Sub

End Class
