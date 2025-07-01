
Imports System.Runtime.InteropServices

Namespace My
    Public Module WinAPI

        'Declarations

        'General
        Friend Const HWND_BROADCAST As Integer = 65535
        Friend Const WM_SYSCOMMAND As Integer = 274 '&H112
        Friend Const SC_MAXIMIZE As UShort = 61488 '&HF030
        Friend Const SC_MAXIMIZE_TBAR As UShort = 61490 '&HF032 'NOT A WINDOWS CONSTANT 'This value is passed to WndProc when DoubleClicking on the Title Bar to Maximize a window.
        Friend Const SC_RESTORE As UShort = 61728 '&HF120
        Friend Const SC_RESTORE_TBAR As UShort = 61730 '&HF122 'NOT A WINDOWS CONSTANT 'This value is passed to WndProc when DoubleClicking on the Title Bar to Restore a window.
        Friend Const SC_CLOSE As UShort = 61536 '&HF060
        Friend Const WM_ACTIVATE As UShort = &H6
        Friend Const WM_PAINT As Integer = &HF
        Friend Const WM_RIGHTCLICKTASKBAR As Integer = 787 '&H313 'UnDocumented Message passed when a user RightClicks on the app taskbar entry before the SystemMenu is displayed. Unless this message is fowarded, the SystemMenu won't display. Useful for replacing with custom menus or for when FormBorderStyle is set to None and the SystemMenu never displays. Just intercept and show your own custom menu. XP & earlier; after XP, ShiftRightClick is required.
        Friend Const WM_LBUTTONDOWN As Integer = &H201
        Friend Const WM_LBUTTONDBLCLK As Integer = &H203
        Friend Const WM_LBUTTONUP As Integer = &H202
        Friend Const WM_RBUTTONUP As Integer = &H205
        Friend Const WM_RBUTTONDOWN As Integer = &H204
        Friend Const WM_SIZE As Integer = &H5
        'Friend Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
        Friend Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        Friend Declare Auto Function PostMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
        Friend Declare Auto Function LockWorkStation Lib "user32.dll" () As Boolean
        Friend Declare Auto Function IsWow64Process Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByRef Wow64Process As Boolean) As Boolean

        'ClipBoard
        Friend Const WM_CHANGECBCHAIN As Integer = 781 '&H30D
        Friend Const WM_DRAWCLIPBOARD As Integer = 776 '&H308
        Friend Declare Auto Function SetClipboardViewer Lib "user32.dll" (ByVal hWndNewViewer As IntPtr) As IntPtr 'adds the specified window to the chain of clipboard viewers
        Friend Declare Auto Function ChangeClipboardChain Lib "user32.dll" (ByVal hWndRemove As IntPtr, ByVal hWndNewNext As IntPtr) As Boolean 'removes a specified window from the chain of clipboard viewers
        Friend Declare Auto Function GetClipboardSequenceNumber Lib "user32.dll" () As UInteger

        'Window Functions 'Get Information About & Change Attributes Of A Window
        'HideFormInTaskSwitcher, Customize Minimize/Maximize/Restore Functions
        'Usage In Forms: When Custom Maximizing(to properly set the maximize icon & window menu), simply set Form.WindowState to Normal when restoring: SetWindowLong(Me.Handle, GWL_STYLE, GetWindowLong(Me.Handle, GWL_STYLE) Or WS_MAXIMIZE)
        'Detecting if an App is in FullScreen Mode
        Friend Const GWL_STYLE As Integer = -16
        Friend Const GWL_EXSTYLE As Integer = -20
        Friend Const WS_EX_TOOLWINDOW As Integer = 128 '&H80
        Friend Const WS_MINIMIZEBOX As Integer = 131072 '&H20000 'Turn on the WS_MINIMIZEBOX style flag for borderless windows so you can minimize/restore from the taskbar.
        Friend Const WS_MAXIMIZE As Integer = 16777216 '&H1000000
        Friend Structure RECT
            Dim Left As Integer
            Dim Top As Integer
            Dim Right As Integer
            Dim Bottom As Integer
        End Structure
        Friend Declare Auto Function GetForegroundWindow Lib "user32.dll" () As IntPtr
        Friend Declare Auto Function SetForegroundWindow Lib "user32.dll" (ByVal hWnd As IntPtr) As Boolean
        Friend Declare Auto Function FindWindow Lib "user32.dll" (lpClassName As String, lpWindowName As String) As IntPtr
        Friend Declare Auto Function GetWindowThreadProcessId Lib "user32.dll" (hWnd As IntPtr, ByRef lpdwProcessId As UInteger) As UInteger
        Friend Declare Auto Function GetWindowText Lib "user32.dll" (hWnd As IntPtr, lpString As String, nMaxCount As Integer) As Integer
        Friend Declare Auto Function GetWindowRect Lib "user32.dll" (hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
        Friend Declare Auto Function GetWindowLong Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
        Friend Declare Auto Function SetWindowLong Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer

        'This function retrieves the current color of the specified display element.
        Friend Const COLOR_WINDOW As Integer = 5
        Friend Const COLOR_WINDOWTEXT As Integer = 8
        Friend Const COLOR_HIGHLIGHT As Integer = 13
        Friend Const COLOR_3DFACE As Integer = 15
        Friend Const COLOR_GRAYTEXT As Integer = 17
        Friend Const COLOR_HOTLIGHT As Integer = 26
        Friend Declare Auto Function GetSysColor Lib "user32.dll" (nIndex As Integer) As Integer

        'Keyboard Functions 'Synthesize a keystroke
        Friend Const VK_CAPITAL As Integer = &H14
        Friend Const VK_SCROLL As Integer = &H91
        Friend Const VK_NUMLOCK As Integer = &H90
        Friend Const VK_MEDIA_NEXT_TRACK As Integer = 176 '&B0
        Friend Const VK_MEDIA_PREV_TRACK As Integer = 177 '&B1
        Friend Const VK_MEDIA_STOP As Integer = 178 '&B2
        Friend Const VK_MEDIA_PLAY_PAUSE As Integer = 179 '&B3
        Friend Const KEYEVENTF_EXTENDEDKEY As Integer = &H1
        Friend Const KEYEVENTF_KEYUP As Integer = &H2
        Friend Declare Auto Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)

        'HotKeys
        Friend Const WM_HOTKEY As Integer = 786 '&H312
        Friend Const MOD_SHIFT As Integer = 4 '&H4
        Friend Const MOD_CONTROL As Integer = 2 '&H2
        Friend Const MOD_ALT As Integer = 1 '&H1
        Friend Const MOD_WIN As Integer = 8 '&H8
        Friend Declare Auto Function RegisterHotKey Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As UInteger, ByVal vk As UInteger) As Boolean
        Friend Declare Auto Function UnregisterHotKey Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal id As Integer) As Boolean

        'ScreenSaver
        Friend Enum EXECUTION_STATE As UInteger
            ES_AWAYMODE_REQUIRED = 64 '&H40
            ES_CONTINUOUS = 2147483648 '&H80000000
            ES_DISPLAY_REQUIRED = 2 '&H2
            ES_SYSTEM_REQUIRED = 1 '&H1
        End Enum
        Friend Const SPI_GETSCREENSAVERRUNNING As Integer = 114
        Friend Const SC_SCREENSAVE As UShort = 61760 '&HF140
        Friend Declare Auto Function SystemParametersInfo Lib "user32.dll" (ByVal uiAction As UInteger, ByVal uiParam As UInteger, ByRef pvParam As Boolean, ByVal fWinIni As UInteger) As Boolean
        Friend Declare Auto Function SetThreadExecutionState Lib "kernel32.dll" (ByVal esFlags As EXECUTION_STATE) As EXECUTION_STATE

        'AppCommand, For Catching Media Keys
        Friend Const WM_APPCOMMAND As Integer = &H319
        Friend Const APPCOMMAND_MEDIA_NEXTTRACK As Integer = 720896
        Friend Const APPCOMMAND_MEDIA_PREVIOUSTRACK As Integer = 786432
        Friend Const APPCOMMAND_MEDIA_STOP As Integer = 851968
        Friend Const APPCOMMAND_MEDIA_PLAY_PAUSE As Integer = 917504

        'Windows Accent Color
        Friend Const WM_DWMCOLORIZATIONCOLORCHANGED As Integer = &H320
        Friend Structure DWMCOLORIZATIONPARAMS
            Public ColorizationColor As UInteger
            Public ColorizationAfterglow As UInteger
            Public ColorizationColorBalance As UInteger
            Public ColorizationAfterglowBalance As UInteger
            Public ColorizationBlurBalance As UInteger
            Public ColorizationGlassReflectionIntensity As UInteger
            Public ColorizationOpaqueBlend As UInteger
        End Structure
        <DllImport("dwmapi.dll", EntryPoint:="#127", PreserveSig:=False)>
        Friend Sub DwmGetColorizationParameters(<Out> ByRef parameters As DWMCOLORIZATIONPARAMS)
        End Sub

        'GetApplicationIcon, & Other File Info
        Friend Const MAX_PATH As Integer = 260 '&H104
        Friend Const SHGFI_ICON As Integer = 256 '&H100 'Retrieve the handle to the icon that represents the file and the index of the icon within the system image list.
        Friend Const SHGFI_SMALLICON As Integer = 1 '&H1 '16x16, Modifies SHGFI_ICON
        Friend Const SHGFI_LARGEICON As Integer = 0 '&H0 '32x32, Modifies SHGFI_ICON
        Friend Const SHGFI_DISPLAYNAME As Integer = 512 '&H200 'Retrieve the display name for the file. The name is copied to the szDisplayName member of the structure specified in psfi.
        Friend Const SHGFI_TYPENAME As Integer = 1024 '&H400 'Retrieve the string that describes the file's type. The string is copied to the szTypeName member of the structure specified in psfi.
        Friend Structure SHFILEINFO
            Dim hIcon As IntPtr
            Dim iIcon As Integer
            Dim dwAttributes As Integer
            <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)> Dim szDisplayName As String
            <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=80)> Dim szTypeName As String
        End Structure
        Friend Declare Ansi Function SHGetFileInfo Lib "shell32.dll" (ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As SHFILEINFO, ByVal cbFileInfo As Integer, ByVal uFlags As Integer) As IntPtr
        Friend Declare Ansi Function DestroyIcon Lib "user32.dll" (ByVal hIcon As IntPtr) As Boolean

        'IdleTime, by getting the tick count of the last time the user provided input to the session.
        Friend Structure LASTINPUTINFO
            Dim cbSize As UInteger
            Dim dwTime As UInteger
        End Structure
        Friend Declare Auto Function GetLastInputInfo Lib "user32.dll" (ByRef plii As LASTINPUTINFO) As Boolean

        'Procedures
        ''' <summary>
        ''' To Remove A Borderless Window From Windows TaskSwitcher
        ''' </summary>
        ''' <param name="handle">Your Form's Handle</param>
        ''' <returns>True Is Success, Otherwise False</returns>
        Friend Function HideFormInTaskSwitcher(handle As IntPtr) As Boolean
            Try
                If SetWindowLong(handle, GWL_EXSTYLE, (GetWindowLong(handle, GWL_EXSTYLE) Or WS_EX_TOOLWINDOW)) = 0 Then : Return False
                Else : Return True
                End If
            Catch : Return False
            End Try
        End Function

        ''' <summary>
        ''' Gets the Associated Icon of a file or a folder.
        ''' </summary>
        ''' <param name="filepath">Fully Qualified Path</param>
        ''' <param name="getlargeicon">True = Get Large 32x32 Icon Using SHGFI_LARGEICON, False = Get Small 16x16 Icon Using SHGFI_SMALLICON, Default = False</param>
        ''' <returns>A single image Icon of the specified size associated with the file</returns>
        Friend Function GetApplicationIcon(filepath As String, Optional getlargeicon As Boolean = False) As Icon
            filepath = Microsoft.VisualBasic.Left(filepath, MAX_PATH)
            Dim windowsfileinfo As New SHFILEINFO
            Dim status As IntPtr = IntPtr.Zero
            Dim nIcon As Icon
            Try
                If getlargeicon Then : status = SHGetFileInfo(filepath, 0, windowsfileinfo, Runtime.InteropServices.Marshal.SizeOf(windowsfileinfo), SHGFI_ICON Or SHGFI_LARGEICON)
                Else : status = SHGetFileInfo(filepath, 0, windowsfileinfo, Runtime.InteropServices.Marshal.SizeOf(windowsfileinfo), SHGFI_ICON Or SHGFI_SMALLICON)
                End If
                If status = IntPtr.Zero Then : Throw New Exception
                Else
                    nIcon = Icon.FromHandle(windowsfileinfo.hIcon)
                    GetApplicationIcon = DirectCast(nIcon.Clone, Icon)
                    nIcon.Dispose()
                    If Not DestroyIcon(windowsfileinfo.hIcon) Then Throw New Exception
                End If
            Catch : GetApplicationIcon = Nothing
            Finally
                nIcon = Nothing
                windowsfileinfo = Nothing
            End Try
        End Function

        ''' <summary>
        ''' Gets The Session Idle Time
        ''' </summary>
        ''' <returns>The number of Ticks since the last user input.</returns>
        Friend Function GetIdleTime() As UInteger
            Dim lastInput As New LASTINPUTINFO
            lastInput.cbSize = CUInt(Runtime.InteropServices.Marshal.SizeOf(lastInput))
            GetLastInputInfo(lastInput)
            GetIdleTime = (CUInt(Environment.TickCount) - lastInput.dwTime)
            lastInput = Nothing
        End Function

        ''' <summary>
        ''' Functions to extract the color elements of a windows system color. Each returns an Red, Green, or Blue element value.
        ''' </summary>
        ''' <param name="color">A Windows system color integer.</param>
        ''' <returns>A Short containing the extracted color element value.</returns>
        Friend Function GetRValue(ByVal color As Long) As Short
            GetRValue = CShort(color And &HFF&)
        End Function
        Friend Function GetGValue(ByVal color As Long) As Short
            GetGValue = CShort((color \ &H100&) And &HFF&)
        End Function
        Friend Function GetBValue(ByVal color As Long) As Short
            GetBValue = CShort((color \ &H10000) And &HFF&)
        End Function

        ''' <summary>
        ''' Gets the Windows system color of a certain display element.
        ''' </summary>
        ''' <returns>A .NET Color.</returns>
        Friend Function GetSystemColor(nIndex As Integer) As Color
            Dim color As Integer = GetSysColor(nIndex)
            Return System.Drawing.Color.FromArgb(255, GetRValue(color), GetGValue(color), GetBValue(color))
        End Function

    End Module

End Namespace