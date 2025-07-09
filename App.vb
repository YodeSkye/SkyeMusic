
Imports Microsoft.Win32

Namespace My
    Friend Module App

        'Declarations
        Friend Enum PlaylistTitleFormats 'When modifying, update Options form.
            UseFilename
            Song
            SongGenre
            SongArtist
            SongArtistAlbum
            SongAlbumArtist
            SongArtistGenre
            SongGenreArtist
            SongArtistAlbumGenre
            SongAlbumArtistGenre
            SongGenreArtistAlbum
            ArtistSong
            ArtistSongAlbum
            ArtistAlbumSong
            ArtistGenreSong
            ArtistSongGenre
            ArtistSongAlbumGenre
            ArtistGenreSongAlbum
            AlbumSongArtist
            AlbumArtistSong
            AlbumGenreSongArtist
            AlbumGenreArtistSong
            AlbumSongArtistGenre
            AlbumArtistSongGenre
            GenreSong
            GenreSongArtist
            GenreArtistSong
            GenreAlbumSongArtist
            GenreAlbumArtistSong
            GenreSongArtistAlbum
            GenreSongAlbumArtist
        End Enum
        Friend Enum PlayModes
            None
            Repeat
            Linear
            Random
        End Enum
        Friend Enum PlaylistActions
            Play
            Queue
            SelectOnly
        End Enum
        Friend Enum Themes
            Accent
            Light
            Dark
            Pink
            Red
        End Enum
        Friend Structure ThemeProperties
            Dim BackColor As Color
            Dim TextColor As Color
            Dim ControlBackColor As Color
            Dim ButtonBackColor As Color
            Dim ButtonTextColor As Color
            Dim AccentTextColor As Color
            Dim InactiveTitleBarColor As Color
            Dim InactiveSearchTextColor As Color
            Dim PlayerPlay As Image
            Dim PlayerPause As Image
            Dim PlayerStop As Image
            Dim PlayerNext As Image
            Dim PlayerPrevious As Image
            Dim PlayerFastForward As Image
            Dim PlayerFastReverse As Image
        End Structure
        Friend Enum FormatFileSizeUnits
            Auto
            Bytes
            KiloBytes
            MegaBytes
            GigaBytes
        End Enum
        Private Structure HotKey
            Dim WinID As Integer
            Dim Description As String
            Dim Key As Keys
            Dim KeyCode As Byte
            Dim KeyMod As Byte
            ReadOnly Property KeyText As String
                Get
                    Dim kc As New System.Windows.Forms.KeysConverter
                    KeyText = kc.ConvertToString(Key)
                    kc = Nothing
                End Get
            End Property
            Sub New(id As Integer, description As String, key As Keys, keycode As Byte, keymod As Byte)
                Me.WinID = id
                Me.Description = description
                Me.Key = key
                Me.KeyCode = keycode
                Me.KeyMod = keymod
            End Sub
        End Structure
        Friend ExtensionDictionary As New Dictionary(Of String, String) 'ExtensionDictionary is a dictionary that maps file extensions to their respective media types.
        Friend AudioExtensionDictionary As New Dictionary(Of String, String) 'AudioExtensionDictionary is a dictionary that maps audio file extensions to their respective media types.
        Friend VideoExtensionDictionary As New Dictionary(Of String, String) 'ExtensionDictionary is a dictionary that maps file extensions to their respective media types.
        Friend FRMLibrary As Library 'FRMLibrary is the main library window that displays the media library.
        Friend FRMLog As Log 'FRMLog is the log window that displays application logs.
        Friend AdjustScreenBoundsNormalWindow As Byte = 8 'AdjustScreenBoundsNormalWindow is the number of pixels to adjust the screen bounds for normal windows.
        Friend AdjustScreenBoundsDialogWindow As Byte = 10 'AdjustScreenBoundsDialogWindow is the number of pixels to adjust the screen bounds for dialog windows.
        Private HotKeys As New Collections.Generic.List(Of HotKey) 'HotKeys is a list of hotkeys used in the application for global media control.
        Private HotKeyPlay As New HotKey(0, "Global Play/Pause", Keys.MediaPlayPause, WinAPI.VK_MEDIA_PLAY_PAUSE, 0) 'HotKeyPlay is a hotkey for global play/pause functionality.
        Private HotKeyStop As New HotKey(1, "Global Stop", Keys.MediaStop, WinAPI.VK_MEDIA_STOP, 0) 'HotKeyStop is a hotkey for global stop functionality.
        Private HotKeyNext As New HotKey(2, "Global Next Track", Keys.MediaNextTrack, WinAPI.VK_MEDIA_NEXT_TRACK, 0) 'HotKeyNext is a hotkey for global next track functionality.
        Private HotKeyPrevious As New HotKey(3, "Global Previous Track", Keys.MediaPreviousTrack, WinAPI.VK_MEDIA_PREV_TRACK, 0) 'HotKeyPrevious is a hotkey for global previous track functionality.
        Private WithEvents ScreenSaverWatcher As New Timer 'ScreenSaverWatcher is a timer that checks the state of the screensaver, sets the ScreenSaverActive flag, and acts accordingly.
        Private ScreenSaverActive As Boolean = False 'ScreenSaverActive is a flag that indicates whether the screensaver is currently active.
        Private ScreenLocked As Boolean = False 'ScreenLocked is a flag that indicates whether the screen is currently locked.
        Friend ReadOnly AttributionMicrosoft As String = "https://www.microsoft.com"
        Friend ReadOnly AttributionSyncFusion As String = "https://www.syncfusion.com/"
        Friend ReadOnly AttributionIcons8 As String = "https://icons8.com/"
        Friend ReadOnly UserPath As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\Skye\" 'UserPath is the base path for user-specific files.
#If DEBUG Then
        Private ReadOnly LogPath As String = My.Computer.FileSystem.SpecialDirectories.Temp + "\" + My.Application.Info.ProductName + "LogDEV.txt" 'LogPath is the path to the log file.
        Private ReadOnly RegPath As String = "Software\\" + My.Application.Info.ProductName + "DEV" 'RegPath is the path to the registry key where application settings are stored.
        Friend ReadOnly PlaylistPath As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\Skye\" + My.Application.Info.ProductName + "PlaylistDEV.xml" 'PlayerPath is the path to the playlist XML file.
        Friend ReadOnly LibraryPath As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\Skye\" + My.Application.Info.ProductName + "LibraryDEV.xml" 'LibraryPath is the path to the media library XML file.
#Else
        Private ReadOnly LogPath As String = My.Computer.FileSystem.SpecialDirectories.Temp + "\" + My.Application.Info.ProductName + "Log.txt" 'LogPath is the path to the log file.
        Private ReadOnly RegPath As String = "Software\\" + My.Application.Info.ProductName 'RegPath is the path to the registry key where application settings are stored.
        Friend ReadOnly PlaylistPath As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\Skye\" + My.Application.Info.ProductName + "Playlist.xml" 'PlayerPath is the path to the playlist XML file.
        Friend ReadOnly LibraryPath As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\Skye\" + My.Application.Info.ProductName + "Library.xml" 'LibraryPath is the path to the media library XML file.
#End If

        'Themes
        Friend CurrentTheme As ThemeProperties 'Holds the current theme settings of the application.
        Friend ReadOnly AccentTheme As New ThemeProperties With { 'Used by Splash Screen
            .BackColor = Color.FromArgb(255, 35, 35, 35),
            .TextColor = Color.DodgerBlue,
            .ControlBackColor = Color.FromArgb(255, 35, 35, 35),
            .ButtonBackColor = Color.SlateGray,
            .ButtonTextColor = Color.White,
            .AccentTextColor = Color.White,
            .InactiveTitleBarColor = Color.FromArgb(255, 243, 243, 243),
            .InactiveSearchTextColor = SystemColors.InactiveCaption,
            .PlayerPlay = Resources.ImagePlayerAccentPlay,
            .PlayerPause = Resources.ImagePlayerAccentPause,
            .PlayerStop = Resources.ImagePlayerAccentStop,
            .PlayerNext = Resources.ImagePlayerAccentNext,
            .PlayerPrevious = Resources.ImagePlayerAccentPrevious,
            .PlayerFastForward = Resources.ImagePlayerAccentFastForward,
            .PlayerFastReverse = Resources.ImagePlayerAccentFastReverse}
        Private ReadOnly LightTheme As New ThemeProperties With {
            .BackColor = SystemColors.Control,
            .TextColor = Color.Black,
            .ControlBackColor = SystemColors.Window,
            .ButtonBackColor = SystemColors.Window,
            .ButtonTextColor = Color.Black,
            .AccentTextColor = Color.White,
            .InactiveTitleBarColor = Color.FromArgb(255, 243, 243, 243),
            .InactiveSearchTextColor = SystemColors.InactiveCaption,
            .PlayerPlay = Resources.ImagePlayerBlackPlay,
            .PlayerPause = Resources.ImagePlayerBlackPause,
            .PlayerStop = Resources.ImagePlayerBlackStop,
            .PlayerNext = Resources.ImagePlayerBlackNext,
            .PlayerPrevious = Resources.ImagePlayerBlackPrevious,
            .PlayerFastForward = Resources.ImagePlayerBlackFastForward,
            .PlayerFastReverse = Resources.ImagePlayerBlackFastReverse}
        Private ReadOnly DarkTheme As New ThemeProperties With {
            .BackColor = Color.FromArgb(255, 35, 35, 35),
            .TextColor = Color.White,
            .ControlBackColor = Color.FromArgb(255, 35, 35, 35),
            .ButtonBackColor = Color.SlateGray,
            .ButtonTextColor = Color.White,
            .AccentTextColor = Color.White,
            .InactiveTitleBarColor = Color.FromArgb(255, 243, 243, 243),
            .InactiveSearchTextColor = SystemColors.InactiveCaption,
            .PlayerPlay = Resources.ImagePlayerWhitePlay,
            .PlayerPause = Resources.ImagePlayerWhitePause,
            .PlayerStop = Resources.ImagePlayerWhiteStop,
            .PlayerNext = Resources.ImagePlayerWhiteNext,
            .PlayerPrevious = Resources.ImagePlayerWhitePrevious,
            .PlayerFastForward = Resources.ImagePlayerWhiteFastForward,
            .PlayerFastReverse = Resources.ImagePlayerWhiteFastReverse}
        Private ReadOnly PinkTheme As New ThemeProperties With {
            .BackColor = Color.Pink,
            .TextColor = Color.DeepPink,
            .ControlBackColor = Color.Pink,
            .ButtonBackColor = Color.HotPink,
            .ButtonTextColor = Color.White,
            .AccentTextColor = Color.White,
            .InactiveTitleBarColor = Color.FromArgb(255, 243, 243, 243),
            .InactiveSearchTextColor = Color.Gray,
            .PlayerPlay = Resources.ImagePlayerWhitePlay,
            .PlayerPause = Resources.ImagePlayerWhitePause,
            .PlayerStop = Resources.ImagePlayerWhiteStop,
            .PlayerNext = Resources.ImagePlayerWhiteNext,
            .PlayerPrevious = Resources.ImagePlayerWhitePrevious,
            .PlayerFastForward = Resources.ImagePlayerWhiteFastForward,
            .PlayerFastReverse = Resources.ImagePlayerWhiteFastReverse}
        Private ReadOnly RedTheme As New ThemeProperties With {
            .BackColor = Color.FromArgb(255, 128, 13, 13),
            .TextColor = Color.AntiqueWhite,
            .ControlBackColor = Color.IndianRed,
            .ButtonBackColor = Color.FromArgb(255, 200, 20, 20),
            .ButtonTextColor = Color.AntiqueWhite,
            .AccentTextColor = Color.White,
            .InactiveTitleBarColor = Color.FromArgb(255, 243, 243, 243),
            .InactiveSearchTextColor = Color.Gray,
            .PlayerPlay = Resources.ImagePlayerWhitePlay,
            .PlayerPause = Resources.ImagePlayerWhitePause,
            .PlayerStop = Resources.ImagePlayerWhiteStop,
            .PlayerNext = Resources.ImagePlayerWhiteNext,
            .PlayerPrevious = Resources.ImagePlayerWhitePrevious,
            .PlayerFastForward = Resources.ImagePlayerWhiteFastForward,
            .PlayerFastReverse = Resources.ImagePlayerWhiteFastReverse}

        'Saved Settings
        Friend PlayerPositionShowElapsed As Boolean = True
        Friend PlayMode As PlayModes = PlayModes.Random
        Friend PlaylistTitleFormat As PlaylistTitleFormats = PlaylistTitleFormats.ArtistSong
        Friend PlaylistTitleRemoveSpaces As Boolean = False
        Friend PlaylistTitleSeparator As String = " "
        Friend PlaylistVideoIdentifier As String = String.Empty
        Friend PlaylistDefaultAction As PlaylistActions = PlaylistActions.Play
        Friend PlaylistSearchAction As PlaylistActions = PlaylistActions.Play
        Friend LibrarySearchFolders As New Collections.Generic.List(Of String)
        Friend LibrarySearchSubFolders As Boolean = True
        Friend SuspendOnSessionChange As Boolean = True 'Flag that indicates whether the application should suspend playback and minimize when the session changes (e.g., screen saver starts, screen locks).
        Friend SaveWindowMetrics As Boolean = False 'Flag that indicates whether to save and restore window positions and sizes.
        Friend Theme As Themes = Themes.Accent 'The current theme of the application.
        Friend PlayerLocation As New Point(-AdjustScreenBoundsNormalWindow - 1, -1)
        Friend PlayerSize As New Size(-1, -1)
        Friend LibraryLocation As New Point(-AdjustScreenBoundsNormalWindow - 1, -1)
        Friend LibrarySize As New Size(-1, -1)
        Friend LogLocation As New Point(-AdjustScreenBoundsNormalWindow - 1, -1)
        Friend LogSize As New Size(-1, -1)
        Friend HelperApp1Name As String = "SkyeTag"
        Friend HelperApp1Path As String = "C:\Program Files\SkyeApps\SkyeTag.exe"
        Friend HelperApp2Name As String = "MP3Tag"
        Friend HelperApp2Path As String = "C:\Program Files\Mp3tag\Mp3tag.exe"

        'Handlers
        Private Sub ScreenSaverWatcherTick(ByVal sender As Object, ByVal e As EventArgs) Handles ScreenSaverWatcher.Tick
            Static ssStatus As Boolean
            WinAPI.SystemParametersInfo(WinAPI.SPI_GETSCREENSAVERRUNNING, 0, ssStatus, 0)
            Select Case ssStatus
                Case True
                    If Not ScreenSaverActive Then
                        ScreenSaverActive = True
                        Debug.Print("ScreenSaver Activated @ " & Now.ToString)
                        WriteToLog("ScreenSaver Activated @ " + Now.ToString)
                        SessionSuspended()
                    End If
                Case False
                    If ScreenSaverActive Then
                        ScreenSaverActive = False
                        Debug.Print("ScreenSaver Deactivated @ " & Now)
                        WriteToLog("ScreenSaver Deactivated @ " + Now)
                    End If
            End Select
        End Sub
        Private Sub SessionSwitchHandler(sender As Object, e As Microsoft.Win32.SessionSwitchEventArgs)
            Select Case e.Reason
                Case Microsoft.Win32.SessionSwitchReason.SessionLock
                    ScreenLocked = True
                    Debug.Print("Screen Locked @ " & Now)
                    WriteToLog("Screen Locked @ " & Now)
                    SessionSuspended()
                Case Microsoft.Win32.SessionSwitchReason.SessionUnlock
                    ScreenLocked = False
                    Debug.Print("Screen Unlocked @ " & Now)
                    WriteToLog("Screen Unlocked @ " & Now)
            End Select
        End Sub

        'Procedures
        Friend Function GenerateLogTime(starttime As TimeSpan, stoptime As TimeSpan, Optional fractionalseconds As Boolean = True) As String
            Dim time As TimeSpan
            If starttime > stoptime Then
                time = stoptime + (New TimeSpan(24, 0, 0) - starttime)
            Else
                time = stoptime - starttime
            End If
            If fractionalseconds Then
                Return New Date(time.Ticks).ToString("HH:mm:ss.ffff")
            Else
                Return New Date(time.Ticks).ToString("HH:mm:ss")
            End If
        End Function
        Friend Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
            ' by making Generator static, we preserve the same instance '
            ' (i.e., do not create new instances with the same seed over and over) '
            ' between calls '
            Static Generator As System.Random = New System.Random()
            Return Generator.Next(Min, Max)
        End Function
        Friend Function GetAccentColor() As Color
            Dim c As Color
            Dim regkey As RegistryKey
            Dim regvalue As Integer
            regkey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\DWM")
            regvalue = regkey.GetValue("AccentColor")
            If regvalue = Nothing Then
                c = App.CurrentTheme.BackColor
            Else
                c = Color.FromArgb(255, WinAPI.GetRValue(regvalue), WinAPI.GetGValue(regvalue), WinAPI.GetBValue(regvalue))
            End If
            regkey.Close()
            regkey.Dispose()
            GetAccentColor = c

            '
            'API Method
            'Dim params As WinAPI.DWMCOLORIZATIONPARAMS
            'WinAPI.DwmGetColorizationParameters(params)
            'c = Color.FromArgb(255, App.GetRValue(params.ColorizationColor), App.GetGValue(params.ColorizationColor), App.GetBValue(params.ColorizationColor))
            '

        End Function
        Friend Function GetCurrentThemeProperties() As ThemeProperties
            Debug.Print(Color.IndianRed.R.ToString + ", " + Color.IndianRed.G.ToString + ", " + Color.IndianRed.B.ToString)
            Select Case Theme
                Case Themes.Accent
                    Return AccentTheme
                Case Themes.Light
                    Return LightTheme
                Case Themes.Dark
                    Return DarkTheme
                Case Themes.Pink
                    Return PinkTheme
                Case Themes.Red
                    Return RedTheme
                Case Else
                    Return AccentTheme
            End Select
        End Function
        Friend Function GenerateEllipsis(ByRef g As Graphics, s As String, f As System.Drawing.Font, width As Integer) As String
            Dim ellipsistext As String = s
            If width >= 0 Then
                Do While g.MeasureString(ellipsistext, f).Width > width
                    ellipsistext = ellipsistext.Substring(0, ellipsistext.Length - 1)
                Loop
            End If
            If ellipsistext = s Then
                Return s
            Else
                If ellipsistext.Length <= 2 Then
                    Return "..."
                Else
                    Return ellipsistext.Substring(0, ellipsistext.Length - 2) + "..."
                End If
            End If
        End Function
        Friend Function FormatFileSize(filesizeinbytes As Long, unit As FormatFileSizeUnits, Optional decimalDigits As Integer = 2, Optional omitThousandSeparators As Boolean = False) As String 'Converts a number of bytes into Kbytes, Megabytes, or Gigabytes
            'Simple Error Checking
            If filesizeinbytes <= 0 Then Return "0 B"
            'Auto-Select Best Units Of Measure
            If unit = FormatFileSizeUnits.Auto Then
                Select Case filesizeinbytes
                    Case Is < 1023
                        unit = FormatFileSizeUnits.Bytes
                        decimalDigits = 0
                    Case Is < 1024 * 1023 : unit = FormatFileSizeUnits.KiloBytes
                    Case Is < 1048576 * 1023 : unit = FormatFileSizeUnits.MegaBytes
                    Case Else : unit = FormatFileSizeUnits.GigaBytes
                End Select
            End If
            'Evaluate The Decimal Value
            Dim value As Decimal
            Dim suffix As String = ""
            Select Case unit
                Case FormatFileSizeUnits.Bytes
                    value = CDec(filesizeinbytes)
                    suffix = " B"
                Case FormatFileSizeUnits.KiloBytes
                    value = CDec(filesizeinbytes / 1024)
                    suffix = " KB"
                Case FormatFileSizeUnits.MegaBytes
                    value = CDec(filesizeinbytes / 1048576)
                    suffix = " MB"
                Case FormatFileSizeUnits.GigaBytes
                    value = CDec(filesizeinbytes / 1073741824)
                    suffix = " GB"
            End Select
            'Get The String Representation
            Dim format As String
            If omitThousandSeparators Then
                format = "F" & decimalDigits.ToString
            Else
                format = "N" & decimalDigits.ToString
            End If
            Return value.ToString(format) & suffix
        End Function
        Friend Sub Initialize()
            WriteToLog(My.Application.Info.ProductName + " Started")
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzkzMzQwMUAzMzMwMmUzMDJlMzAzYjMzMzAzYmorMHVJSHVxLy9PM25TUGYrMURsLzhuY3BCK0k0QjZ4L3hJOTcvQ1dQcjQ9")
            GetOptions()
            CurrentTheme = GetCurrentThemeProperties()

            'Setup Dictionaries
#Region "            Audio Types"
            ExtensionDictionary.Add(".aa", "")
            AudioExtensionDictionary.Add(".aa", "")
            ExtensionDictionary.Add(".aax", "")
            AudioExtensionDictionary.Add(".aax", "")
            ExtensionDictionary.Add(".aac", "")
            AudioExtensionDictionary.Add(".aac", "")
            ExtensionDictionary.Add(".aiff", "")
            AudioExtensionDictionary.Add(".aiff", "")
            ExtensionDictionary.Add(".ape", "")
            AudioExtensionDictionary.Add(".ape", "")
            ExtensionDictionary.Add(".dsf", "")
            AudioExtensionDictionary.Add(".dsf", "")
            ExtensionDictionary.Add(".flac", "FLAC")
            AudioExtensionDictionary.Add(".flac", "FLAC")
            ExtensionDictionary.Add(".m4a", "MP4")
            AudioExtensionDictionary.Add(".m4a", "MP4")
            ExtensionDictionary.Add(".m4b", "MP4")
            AudioExtensionDictionary.Add(".m4b", "MP4")
            ExtensionDictionary.Add(".m4p", "MP4")
            AudioExtensionDictionary.Add(".m4p", "MP4")
            ExtensionDictionary.Add(".mp3", "MP3")
            AudioExtensionDictionary.Add(".mp3", "MP3")
            ExtensionDictionary.Add(".mpc", "")
            AudioExtensionDictionary.Add(".mpc", "")
            ExtensionDictionary.Add(".mpp", "")
            AudioExtensionDictionary.Add(".mpp", "")
            ExtensionDictionary.Add(".ogg", "OGG Audio")
            AudioExtensionDictionary.Add(".ogg", "OGG Audio")
            ExtensionDictionary.Add(".oga", "OGG Audio")
            AudioExtensionDictionary.Add(".oga", "OGG Audio")
            ExtensionDictionary.Add(".wav", "Wave File")
            AudioExtensionDictionary.Add(".wav", "Wave File")
            ExtensionDictionary.Add(".wma", "Windows Media")
            AudioExtensionDictionary.Add(".wma", "Windows Media")
            ExtensionDictionary.Add(".wv", "")
            AudioExtensionDictionary.Add(".wv", "")
            ExtensionDictionary.Add(".webm", "")
            AudioExtensionDictionary.Add(".webm", "")
#End Region
#Region "            Video Types"
            ExtensionDictionary.Add(".mkv", "Matroska")
            VideoExtensionDictionary.Add(".mkv", "Matroska")
            ExtensionDictionary.Add(".ogv", "OGG Video")
            VideoExtensionDictionary.Add(".ogv", "OGG Video")
            ExtensionDictionary.Add(".avi", "AVI Video")
            VideoExtensionDictionary.Add(".avi", "AVI Video")
            ExtensionDictionary.Add(".wmv", "Windows Media")
            VideoExtensionDictionary.Add(".wmv", "Windows Media")
            ExtensionDictionary.Add(".asf", "ASF")
            VideoExtensionDictionary.Add(".asf", "ASF")
            ExtensionDictionary.Add(".mp4", "MP4")
            VideoExtensionDictionary.Add(".mp4", "MP4")
            'ExtensionDictionary.Add(".m4p", "MP4")
            VideoExtensionDictionary.Add(".m4p", "MP4")
            ExtensionDictionary.Add(".m4v", "MP4")
            VideoExtensionDictionary.Add(".m4v", "MP4")
            ExtensionDictionary.Add(".mpeg", "MPEG")
            VideoExtensionDictionary.Add(".mpeg", "MPEG")
            ExtensionDictionary.Add(".mpg", "MPEG")
            VideoExtensionDictionary.Add(".mpg", "MPEG")
            ExtensionDictionary.Add(".mpe", "MPEG")
            VideoExtensionDictionary.Add(".mpe", "MPEG")
            ExtensionDictionary.Add(".mpv", "MPEG")
            VideoExtensionDictionary.Add(".mpv", "MPEG")
            ExtensionDictionary.Add(".m2v", "MPEG")
            VideoExtensionDictionary.Add(".m2v", "MPEG")
            'Video Types Not Supported By TagLib-Sharp
            ExtensionDictionary.Add(".flv", "Flash Video")
            VideoExtensionDictionary.Add(".flv", "Flash Video")
#End Region

            FRMLibrary = New Library
            GenerateHotKeyList()
            RegisterHotKeys()
            ScreenSaverWatcher.Interval = 1000
            ScreenSaverWatcher.Start()
            AddHandler Microsoft.Win32.SystemEvents.SessionSwitch, AddressOf SessionSwitchHandler 'SessionSwitchHandler is a handler for session switch events, sets the ScreenLocked flag, and acts accordingly.
        End Sub
        Friend Sub Finalize()
            UnRegisterHotKeys()
            If FRMLog IsNot Nothing AndAlso FRMLog.Visible Then FRMLog.Close()
            If FRMLibrary.Visible Then FRMLibrary.Close()
            FRMLibrary.Dispose()
            SaveOptions()
            My.App.WriteToLog(My.Application.Info.ProductName + " Closed")
        End Sub
        Friend Sub SaveOptions()
            Try
                Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
                Dim RegKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegPath, True)
                Dim RegSubKey As Microsoft.Win32.RegistryKey
                RegKey.SetValue("PlayerPositionShowElapsed", App.PlayerPositionShowElapsed.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlayMode", App.PlayMode.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlaylistTitleFormat", App.PlaylistTitleFormat.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlaylistTitleRemoveSpaces", App.PlaylistTitleRemoveSpaces.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlaylistTitleSeparator", App.PlaylistTitleSeparator, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlaylistVideoIdentifier", App.PlaylistVideoIdentifier, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlaylistDefaultAction", App.PlaylistDefaultAction.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlaylistSearchAction", App.PlaylistSearchAction.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("Theme", App.Theme.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("SuspendOnSessionChange", App.SuspendOnSessionChange.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("SaveWindowMetrics", App.SaveWindowMetrics.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlayerLocationX", App.PlayerLocation.X.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlayerLocationY", App.PlayerLocation.Y.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlayerSizeX", App.PlayerSize.Width.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("PlayerSizeY", App.PlayerSize.Height.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("LibraryLocationX", App.LibraryLocation.X.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("LibraryLocationY", App.LibraryLocation.Y.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("LibrarySizeX", App.LibrarySize.Width.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("LibrarySizeY", App.LibrarySize.Height.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("LogLocationX", App.LogLocation.X.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("LogLocationY", App.LogLocation.Y.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("LogSizeX", App.LogSize.Width.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("LogSizeY", App.LogSize.Height.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("HelperApp1Name", App.HelperApp1Name, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("HelperApp1Path", App.HelperApp1Path, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("HelperApp2Name", App.HelperApp2Name, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("HelperApp2Path", App.HelperApp2Path, Microsoft.Win32.RegistryValueKind.String)
                RegSubKey = RegKey.OpenSubKey("LibraryWatchFolders", True)
                For Each s As String In RegSubKey.GetValueNames : RegSubKey.DeleteValue(s) : Next
                For Each s As String In LibrarySearchFolders : RegSubKey.SetValue("Folder" + Str(LibrarySearchFolders.IndexOf(s) + 1).Trim, s, Microsoft.Win32.RegistryValueKind.String) : Next
                RegSubKey.Close()
                RegKey.SetValue("LibrarySearchSubFolders", LibrarySearchSubFolders.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.Flush()
                RegSubKey.Dispose()
                RegKey.Close()
                RegKey.Dispose()
                App.WriteToLog("Options Saved (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
            Catch ex As Exception
                WriteToLog("Error Saving Options" + vbCr + ex.Message)
            End Try
        End Sub
        Private Sub GetOptions()
            Try
                Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
                Dim RegKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegPath)
                Dim RegSubKey As Microsoft.Win32.RegistryKey
                Select Case RegKey.GetValue("PlayerPositionShowElapsed", "True").ToString
                    Case "False", "0" : PlayerPositionShowElapsed = False
                    Case Else : PlayerPositionShowElapsed = True
                End Select
                Try : App.PlayMode = CType([Enum].Parse(GetType(App.PlayModes), RegKey.GetValue("PlayMode", App.PlayModes.Random.ToString).ToString), PlayModes)
                Catch : App.PlayMode = App.PlayModes.Random
                End Try
                Try : App.PlaylistTitleFormat = CType([Enum].Parse(GetType(App.PlaylistTitleFormats), RegKey.GetValue("PlaylistTitleFormat", App.PlaylistTitleFormats.ArtistSong.ToString).ToString), PlaylistTitleFormats)
                Catch : App.PlaylistTitleFormat = App.PlaylistTitleFormats.ArtistSong
                End Try
                Select Case RegKey.GetValue("PlaylistTitleRemoveSpaces", "False").ToString
                    Case "True", "1" : App.PlaylistTitleRemoveSpaces = True
                    Case Else : App.PlaylistTitleRemoveSpaces = False
                End Select
                App.PlaylistTitleSeparator = RegKey.GetValue("PlaylistTitleSeparator", " ").ToString
                App.PlaylistVideoIdentifier = RegKey.GetValue("PlaylistVideoIdentifier", String.Empty).ToString
                Try : App.PlaylistDefaultAction = CType([Enum].Parse(GetType(App.PlaylistActions), RegKey.GetValue("PlaylistDefaultAction", App.PlaylistActions.Play.ToString).ToString), App.PlaylistActions)
                Catch : App.PlaylistDefaultAction = App.PlaylistActions.Play
                End Try
                Try : App.PlaylistSearchAction = CType([Enum].Parse(GetType(App.PlaylistActions), RegKey.GetValue("PlaylistSearchAction", App.PlaylistActions.Play.ToString).ToString), App.PlaylistActions)
                Catch : App.PlaylistSearchAction = App.PlaylistActions.Play
                End Try
                Try : App.Theme = CType([Enum].Parse(GetType(App.Themes), RegKey.GetValue("Theme", App.Themes.Accent.ToString).ToString), App.Themes)
                Catch : App.Theme = App.Themes.Accent
                End Try
                Select Case RegKey.GetValue("SuspendOnSessionChange", "True").ToString
                    Case "False", "0" : App.SuspendOnSessionChange = False
                    Case Else : App.SuspendOnSessionChange = True
                End Select
                Select Case RegKey.GetValue("SaveWindowMetrics", "False").ToString
                    Case "True", "1" : App.SaveWindowMetrics = True
                    Case Else : App.SaveWindowMetrics = False
                End Select
                App.PlayerLocation.X = CInt(Val(RegKey.GetValue("PlayerLocationX", (-AdjustScreenBoundsNormalWindow - 1).ToString)))
                App.PlayerLocation.Y = CInt(Val(RegKey.GetValue("PlayerLocationY", (-1).ToString)))
                App.PlayerSize.Width = CInt(Val(RegKey.GetValue("PlayerSizeX", (-1).ToString)))
                App.PlayerSize.Height = CInt(Val(RegKey.GetValue("PlayerSizeY", (-1).ToString)))
                App.LibraryLocation.X = CInt(Val(RegKey.GetValue("LibraryLocationX", (-AdjustScreenBoundsNormalWindow - 1).ToString)))
                App.LibraryLocation.Y = CInt(Val(RegKey.GetValue("LibraryLocationY", (-1).ToString)))
                App.LibrarySize.Width = CInt(Val(RegKey.GetValue("LibrarySizeX", (-1).ToString)))
                App.LibrarySize.Height = CInt(Val(RegKey.GetValue("LibrarySizeY", (-1).ToString)))
                App.LogLocation.X = CInt(Val(RegKey.GetValue("LogLocationX", (-AdjustScreenBoundsNormalWindow - 1).ToString)))
                App.LogLocation.Y = CInt(Val(RegKey.GetValue("LogLocationY", (-1).ToString)))
                App.LogSize.Width = CInt(Val(RegKey.GetValue("LogSizeX", (-1).ToString)))
                App.LogSize.Height = CInt(Val(RegKey.GetValue("LogSizeY", (-1).ToString)))
                App.HelperApp1Name = RegKey.GetValue("HelperApp1Name", "SkyeTag").ToString
                App.HelperApp1Path = RegKey.GetValue("HelperApp1Path", "C:\Program Files\SkyeApps\SkyeTag.exe").ToString
                App.HelperApp2Name = RegKey.GetValue("HelperApp2Name", "MP3Tag").ToString
                App.HelperApp2Path = RegKey.GetValue("HelperApp2Path", "C:\Program Files\Mp3tag\Mp3tag.exe").ToString
                LibrarySearchFolders.Clear()
                RegSubKey = RegKey.CreateSubKey("LibraryWatchFolders")
                For Each s As String In RegSubKey.GetValueNames : LibrarySearchFolders.Add(RegSubKey.GetValue(s).ToString) : Next
                RegSubKey.Close()
                Select Case RegKey.GetValue("LibrarySearchSubFolders", "True").ToString
                    Case "False", "0" : LibrarySearchSubFolders = False
                    Case Else : LibrarySearchSubFolders = True
                End Select
                RegSubKey.Dispose()
                RegKey.Close()
                RegKey.Dispose()
                App.WriteToLog("Options Loaded (" + App.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
            Catch ex As Exception
                WriteToLog("Error Loading Options" + vbCr + ex.Message)
            End Try
        End Sub
        Friend Sub WriteToLog(logtext As String)
            Static fInfo As IO.FileInfo
            Try
                fInfo = New IO.FileInfo(LogPath)
                If fInfo.Exists AndAlso fInfo.Length >= 1000000 Then IO.File.Move(LogPath, LogPath.Insert(LogPath.Length - 4, "Backup@" + My.Computer.Clock.LocalTime.ToString("yyyyMMdd") + "@" + My.Computer.Clock.LocalTime.ToString("HHmmss")))
                IO.File.AppendAllText(LogPath, IIf(String.IsNullOrEmpty(logtext), String.Empty, My.Computer.Clock.LocalTime.ToString("yyyy/MM/dd") + " @ " + My.Computer.Clock.LocalTime.ToString("HH:mm:ss") + " --> " + logtext + Chr(13)).ToString)
                'My.Debug.ShowMessage("WriteToLog", IIf(String.IsNullOrEmpty(logtext), String.Empty, logtext + IIf(showfilename, " (" + IIf(String.IsNullOrEmpty(tagPath), sNoFile, tagPath).ToString + ")", String.Empty).ToString).ToString)
            Catch
            Finally : fInfo = Nothing
            End Try
        End Sub
        Friend Sub ShowOptions()
            Options.ShowDialog()
        End Sub
        Friend Sub ShowLibrary()
            If FRMLibrary.WindowState = FormWindowState.Minimized Then
                FRMLibrary.WindowState = FormWindowState.Normal
            ElseIf FRMLibrary.Visible Then
                FRMLibrary.BringToFront()
            Else
                FRMLibrary.Show()
            End If
        End Sub
        Friend Sub ShowHelp()
            Help.ShowDialog()
        End Sub
        Friend Sub ShowLog(Optional refresh As Boolean = False)
            If refresh AndAlso FRMLog IsNot Nothing Then
                FRMLog.BringToFront()
                FRMLog.Focus()
            Else
                FRMLog = New Log
                FRMLog.Show()
            End If
            Dim logtext As String = String.Empty
            Dim lines As Integer = 0
            FRMLog.RTBLog.Clear()
            Try
                logtext = IO.File.ReadAllText(LogPath)
            Catch
            Finally
            End Try
            FRMLog.RTBLog.AppendText(logtext)
            FRMLog.LBLLogInfo.Text = LogPath
            If FRMLog.RTBLog.Lines.Count > 0 AndAlso FRMLog.RTBLog.Lines(0).Length > 0 Then lines = FRMLog.RTBLog.GetLineFromCharIndex(FRMLog.RTBLog.Text.Length)
            FRMLog.LBLLogInfo.Text += " (" + lines.ToString + IIf(lines = 1, " Line", " Lines").ToString + ")"
            If lines > 0 Then
                FRMLog.BTNDeleteLog.Visible = True
                FRMLog.BTNRefreshLog.Visible = True
                FRMLog.RTBLog.ScrollToCaret()
            Else
                FRMLog.BTNDeleteLog.Visible = False
            End If
            FRMLog.RTBLog.ReadOnly = True
            FRMLog.BTNOK.Select()
        End Sub
        Friend Sub DeleteLog()
            If IO.File.Exists(LogPath) Then IO.File.Delete(LogPath)
            ShowLog(True)
        End Sub
        Friend Sub ShowAbout()
            About.ShowDialog()
        End Sub
        Friend Sub HelperApp1(filename As String)
            If filename IsNot String.Empty Then
                If IO.File.Exists(filename) Then
                    Dim pInfo As New Diagnostics.ProcessStartInfo
                    pInfo.UseShellExecute = False
                    pInfo.FileName = HelperApp1Path
                    pInfo.Arguments = """" + filename + """"
                    Try
                        Diagnostics.Process.Start(pInfo)
                        WriteToLog(HelperApp1Name + " Opened (" + filename + ")")
                    Catch ex As Exception
                        WriteToLog("Cannot Open " + HelperApp1Name + " (" + pInfo.FileName + " " + pInfo.Arguments + ")" + vbCr + ex.Message)
                    Finally : pInfo = Nothing
                    End Try
                End If
            End If
        End Sub
        Friend Sub HelperApp2(filename As String)
            If filename IsNot String.Empty Then
                If IO.File.Exists(filename) Then
                    Dim pInfo As New Diagnostics.ProcessStartInfo
                    pInfo.UseShellExecute = False
                    pInfo.FileName = HelperApp2Path
                    pInfo.Arguments = """" + filename + """"
                    Try
                        Diagnostics.Process.Start(pInfo)
                        WriteToLog(HelperApp2Name + " Opened (" + filename + ")")
                    Catch ex As Exception
                        WriteToLog("Cannot Open " + HelperApp2Name + " (" + pInfo.FileName + " " + pInfo.Arguments + ")" + vbCr + ex.Message)
                    Finally : pInfo = Nothing
                    End Try
                End If
            End If
        End Sub
        Friend Sub OpenFileLocation(filename As String)
            Dim psi As New Diagnostics.ProcessStartInfo("EXPLORER.EXE")
            psi.Arguments = "/SELECT," + """" + filename + """"
            Try
                Diagnostics.Process.Start(psi)
                WriteToLog("File Location Opened (" + filename + ")")
            Catch ex As Exception
                App.WriteToLog("Error Opening File Location (" + filename + ")" + vbCr + ex.Message)
            Finally
                psi = Nothing
            End Try
        End Sub
        Friend Sub PlayWithWindows(filename As String)
            If filename IsNot String.Empty Then
                If IO.File.Exists(filename) Then
                    Dim pInfo As New Diagnostics.ProcessStartInfo
                    pInfo.UseShellExecute = True
                    pInfo.FileName = filename
                    Try
                        Diagnostics.Process.Start(pInfo)
                        WriteToLog("Played In Windows (" + filename + ")")
                    Catch ex As Exception
                        WriteToLog("Cannot Play In Windows (" + pInfo.FileName + ")" + vbCr + ex.Message)
                    Finally : pInfo = Nothing
                    End Try
                End If
            End If
        End Sub
        Private Sub GenerateHotKeyList()
            HotKeys.Clear()
            HotKeys.Add(HotKeyPlay)
            HotKeys.Add(HotKeyStop)
            HotKeys.Add(HotKeyNext)
            HotKeys.Add(HotKeyPrevious)
        End Sub
        Private Sub RegisterHotKeys()
            Dim status As Boolean
            For Each key As HotKey In HotKeys
                If Not key.Key = Keys.None Then
                    status = My.WinAPI.RegisterHotKey(Player.Handle, key.WinID, key.KeyMod, key.KeyCode)
                    Debug.Print("HotKey '" + key.Description + " (" + key.WinID.ToString + ") (" + key.Key.ToString + ") (" + key.KeyCode.ToString + " mod " + key.KeyMod.ToString + ")' " + IIf(status, "Successfully Registered", "Failed To Register").ToString)
                    WriteToLog("HotKey '" + key.Description + " (" + key.WinID.ToString + ") (" + key.Key.ToString + ") (" + key.KeyCode.ToString + " mod " + key.KeyMod.ToString + ")' " + IIf(status, "Successfully Registered", "Failed To Register").ToString)
                End If
            Next
        End Sub
        Private Sub UnRegisterHotKeys()
            Dim status As Boolean
            For Each key As HotKey In HotKeys
                If Not key.Key = Keys.None Then
                    status = My.WinAPI.UnregisterHotKey(Player.Handle, key.WinID)
                    Debug.Print("HotKey '" + key.Description + " (" + key.WinID.ToString + ")' " + IIf(status, "Successfully UNRegistered", "Failed To UNRegister").ToString)
                    WriteToLog("HotKey '" + key.Description + " (" + key.WinID.ToString + ")' " + IIf(status, "Successfully UNRegistered", "Failed To UNRegister").ToString)
                End If
            Next
        End Sub
        Friend Sub PerformHotKeyAction(hotkey As Integer)
            Select Case hotkey
                Case HotKeyPlay.WinID
                    Player.TogglePlay()
                Case HotKeyStop.WinID
                    Player.StopPlay()
                Case HotKeyNext.WinID
                    Player.PlayNext()
                Case HotKeyPrevious.WinID
                    Player.PlayPrevious()
            End Select
        End Sub
        Private Sub SessionSuspended() 'SessionSuspended is called when the screensaver is activated or the screen is locked.
            If ScreenLocked OrElse ScreenSaverActive Then Player.Suspend()
        End Sub

    End Module
    Friend Class ListViewItemStringComparer
        Implements IComparer
        Private col As Integer
        Private sort As SortOrder
        Public Sub New(column As Integer, sortorder As SortOrder)
            col = column
            sort = sortorder
        End Sub
        Public Function Compare(x As Object,
                 y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim returnVal As Integer = -1
            returnVal = String.Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
            If sort = SortOrder.Descending Then
                returnVal *= -1
            End If
            Return returnVal
        End Function
    End Class
    Public Class MessageFilterPlayerIgnoreFullscreenMouseClick

        Implements IMessageFilter
        Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
            If Player.Fullscreen AndAlso (m.Msg = WinAPI.WM_LBUTTONDOWN OrElse m.Msg = WinAPI.WM_LBUTTONUP OrElse m.Msg = WinAPI.WM_LBUTTONDBLCLK OrElse m.Msg = WinAPI.WM_RBUTTONDOWN OrElse m.Msg = WinAPI.WM_RBUTTONUP) Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class

End Namespace