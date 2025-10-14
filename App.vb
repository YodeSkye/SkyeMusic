
Imports System.IO
Imports Microsoft.Win32

Namespace My

    Public Module App

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
            BlueAccent
            PinkAccent
            Light
            Dark
            DarkPink
            Pink
            Red
        End Enum
        Friend Enum FormatFileSizeUnits
            Auto
            Bytes
            KiloBytes
            MegaBytes
            GigaBytes
        End Enum
        Public Structure Song

            Public Path As String 'Path to the media.
            Public InLibrary As Boolean 'InLibrary indicates whether the song is part of the library.
            Public IsStream As Boolean 'IsStream indicates whether the song is a stream.
            Public PlayCount As UShort 'PlayCount is the number of times the song has been played.
            Public Added As DateTime 'DateAdded is the date and time when the song was added to the History.
            Public FirstPlayed As DateTime 'FirstPlayed is the date and time when the song was first played.
            Public LastPlayed As DateTime 'LastPlayed is the date and time when the song was last played.
            Public Rating As Byte 'Rating is the rating of the song, from 0 to 5.

            Public Overrides Function ToString() As String
                Dim s As String = String.Empty
                If IsStream Then s += "Stream, "
                If Rating > 0 Then s += New String("★"c, Rating) + ", "
                s += PlayCount.ToString()
                If PlayCount = 1 Then
                    s += " Play"
                Else
                    s += " Plays"
                End If
                If Not LastPlayed = Nothing Then s += ", Last Played on " + LastPlayed.ToString()
                Return s
            End Function
            Public Function ToStringFull() As String
                Dim s As String = String.Empty
                If IsStream Then s += "Stream, "
                If Rating > 0 Then s += New String("★"c, Rating) + ", "
                s += PlayCount.ToString()
                If PlayCount = 1 Then
                    s += " Play"
                Else
                    s += " Plays"
                End If
                If Not LastPlayed = Nothing Then s += ", Last Played " + LastPlayed.ToString()
                If Not FirstPlayed = Nothing Then s += ", First Played " + FirstPlayed.ToString()
                If Not Added = Nothing Then s += ", Added " + Added.ToString()
                Return s
            End Function

        End Structure
        Friend Structure ThemeProperties
            Public IsAccent As Boolean
            Public BackColor As Color
            Public TextColor As Color
            Public ControlBackColor As Color
            Public ButtonBackColor As Color
            Public ButtonTextColor As Color
            Public AccentTextColor As Color
            Public InactiveTitleBarColor As Color
            Public InactiveSearchTextColor As Color
            Public PlayerPlay As Image
            Public PlayerPause As Image
            Public PlayerStop As Image
            Public PlayerNext As Image
            Public PlayerPrevious As Image
            Public PlayerFastForward As Image
            Public PlayerFastReverse As Image
        End Structure
        Private Structure HotKey
            Public WinID As Integer
            Public Description As String
            Public Key As Keys
            Public KeyCode As Byte
            Public KeyMod As Byte
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
        Private HotKeyPlay As New HotKey(0, "Global Play/Pause", Keys.MediaPlayPause, Skye.WinAPI.VK_MEDIA_PLAY_PAUSE, 0) 'HotKeyPlay is a hotkey for global play/pause functionality.
        Private HotKeyStop As New HotKey(1, "Global Stop", Keys.MediaStop, Skye.WinAPI.VK_MEDIA_STOP, 0) 'HotKeyStop is a hotkey for global stop functionality.
        Private HotKeyNext As New HotKey(2, "Global Next Track", Keys.MediaNextTrack, Skye.WinAPI.VK_MEDIA_NEXT_TRACK, 0) 'HotKeyNext is a hotkey for global next track functionality.
        Private HotKeyPrevious As New HotKey(3, "Global Previous Track", Keys.MediaPreviousTrack, Skye.WinAPI.VK_MEDIA_PREV_TRACK, 0) 'HotKeyPrevious is a hotkey for global previous track functionality.
        Friend History As New Collections.Generic.List(Of Song) 'History is a list that stores the history of songs and streams in the Library and Playlist.
        Private HistoryChanged As Boolean = False 'Tracks if history has been changed.
        Private WithEvents timerHistoryAutoSave As New Timer 'HistoryAutoSaveTimer is a timer that automatically saves the history at regular intervals.
        Private WithEvents timerHistoryUpdate As New Timer 'HistoryUpdate is a timer that allows for a delay in the updating of the Play Count.
        Private WithEvents timerRandomHistoryUpdate As New Timer 'RandomHistoryUpdate is a timer that allows for a delay in the adding of a song to the random history.
        Private WithEvents timerScreenSaverWatcher As New Timer 'ScreenSaverWatcher is a timer that checks the state of the screensaver, sets the ScreenSaverActive flag, and acts accordingly.
        Private ScreenSaverActive As Boolean = False 'ScreenSaverActive is a flag that indicates whether the screensaver is currently active.
        Private ScreenLocked As Boolean = False 'ScreenLocked is a flag that indicates whether the screen is currently locked.
        Private Watchers As New List(Of System.IO.FileSystemWatcher) 'Watchers is a set of file system watchers that monitors changes in the library folders.
        Private WithEvents WatcherWorkTimer As New Timers.Timer(1000) 'WatcherWorkTimer is a timer that debounces file system watcher events to prevent multiple rapid events from being processed.
        Private WatcherWorkList As New Collections.Generic.List(Of String) 'WatcherWorkList is a list of files that have been changed, created, deleted, or renamed by the file system watchers.
        Friend ReadOnly TrimEndSearch() As Char = {CChar(" "), CChar("("), CChar(")"), CChar("0"), CChar("1"), CChar("2"), CChar("3"), CChar("4"), CChar("5"), CChar("6"), CChar("7"), CChar("8"), CChar("9")} 'TrimEndSearch is a string used to trim whitespace characters from the end of strings.
        Friend ReadOnly AttributionMicrosoft As String = "https://www.microsoft.com" 'AttributionMicrosoft is the URL for Microsoft, which provides various APIs and libraries used in the application.
        Friend ReadOnly AttributionSyncFusion As String = "https://www.syncfusion.com/" 'AttributionSyncFusion is the URL for Syncfusion, which provides UI controls and libraries used in the application.
        Friend ReadOnly AttributionTagLibSharp As String = "https://github.com/mono/taglib-sharp" 'AttributionTagLibSharp is the URL for TagLib# library, which is used for reading and writing metadata in media files.
        Friend ReadOnly AttributionIcons8 As String = "https://icons8.com/" 'AttributionIcons8 is the URL for Icons8, which provides icons used in the application.
        Friend ReadOnly UserPath As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\Skye\" 'UserPath is the base path for user-specific files.
#If DEBUG Then
        Friend ReadOnly LogPath As String = My.Computer.FileSystem.SpecialDirectories.Temp + "\" + My.Application.Info.ProductName + "LogDEV.txt" 'LogPath is the path to the log file.
        Private ReadOnly RegPath As String = "Software\\" + My.Application.Info.ProductName + "DEV" 'RegPath is the path to the registry key where application settings are stored.
        Friend ReadOnly PlaylistPath As String = UserPath + My.Application.Info.ProductName + "PlaylistDEV.xml" 'PlayerPath is the path to the playlist XML file.
        Friend ReadOnly LibraryPath As String = UserPath + My.Application.Info.ProductName + "LibraryDEV.xml" 'LibraryPath is the path to the media library XML file.
        Private ReadOnly HistoryPath As String = UserPath + My.Application.Info.ProductName + "HistoryDEV.xml" 'HistoryPath is the path to the media history XML file.
#Else
        Friend ReadOnly LogPath As String = My.Computer.FileSystem.SpecialDirectories.Temp + "\" + My.Application.Info.ProductName + "Log.txt" 'LogPath is the path to the log file.
        Private ReadOnly RegPath As String = "Software\\" + My.Application.Info.ProductName 'RegPath is the path to the registry key where application settings are stored.
        Friend ReadOnly PlaylistPath As String = UserPath + My.Application.Info.ProductName + "Playlist.xml" 'PlayerPath is the path to the playlist XML file.
        Friend ReadOnly LibraryPath As String = UserPath + My.Application.Info.ProductName + "Library.xml" 'LibraryPath is the path to the media library XML file.
        Private ReadOnly HistoryPath As String = UserPath + My.Application.Info.ProductName + "History.xml" 'HistoryPath is the path to the media history XML file.
#End If

        'Themes
        Friend CurrentTheme As ThemeProperties 'Holds the current theme settings of the application.
        Private ReadOnly BlueAccentTheme As New ThemeProperties With { 'Used by Splash Screen
            .IsAccent = True,
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
        Private ReadOnly PinkAccentTheme As New ThemeProperties With {
            .IsAccent = True,
            .BackColor = Color.FromArgb(255, 35, 35, 35),
            .TextColor = Color.DeepPink,
            .ControlBackColor = Color.FromArgb(255, 35, 35, 35),
            .ButtonBackColor = Color.DeepPink,
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
        Private ReadOnly LightTheme As New ThemeProperties With {
            .IsAccent = False,
            .BackColor = Color.FromArgb(255, 241, 240, 240), 'SystemColors.Control = 240,240,240, set to avoid transparency issues in ToolTipEX
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
            .IsAccent = False,
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
        Private ReadOnly DarkPinkTheme As New ThemeProperties With {
            .IsAccent = False,
            .BackColor = Color.FromArgb(255, 35, 35, 35),
            .TextColor = Color.DeepPink,
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
            .IsAccent = False,
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
        Friend ReadOnly RedTheme As New ThemeProperties With { 'Used by Splash Screen
            .IsAccent = False,
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
        Friend RandomHistoryUpdateInterval As Byte = 5 '0-60 'Interval in seconds to add the currently playing song to the shuffle play history.
        Friend HistoryUpdateInterval As Byte = 5 '0-60 'Interval in seconds to update the play count of the currently playing song.
        Friend HistoryAutoSaveInterval As UShort = 5 '1-1440 'Interval in minutes to automatically save the history.
        Friend WatcherEnabled As Boolean = False 'Flag that indicates whether to watch for changes in the library folders.
        Friend WatcherUpdateLibrary As Boolean = False 'Flag that indicates whether to automatically update the library when changes are detected in the file system.
        Friend WatcherUpdatePlaylist As Boolean = False 'Flag that indicates whether to automatically update the playlist when changes are detected in the file system.
        Friend Theme As Themes = Themes.Red 'The current theme of the application.
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

        'Interfaces
        Private Interface IPlaylistIOFormat
            ReadOnly Property Name As String
            ReadOnly Property FileExtension As String
            Function Import(path As String) As List(Of Player.PlaylistItemType)
            Sub Export(path As String, items As IEnumerable(Of Player.PlaylistItemType))
        End Interface
        Private MustInherit Class PlaylistIOFormatM3UBase
            Implements IPlaylistIOFormat

            Public MustOverride ReadOnly Property Name As String Implements IPlaylistIOFormat.Name
            Public MustOverride ReadOnly Property FileExtension As String Implements IPlaylistIOFormat.FileExtension

            Public Function Import(path As String) As List(Of Player.PlaylistItemType) Implements IPlaylistIOFormat.Import
                Dim items As New List(Of Player.PlaylistItemType)
                Dim pendingTitle As String = Nothing

                For Each line In IO.File.ReadAllLines(path, System.Text.Encoding.UTF8)
                    If String.IsNullOrWhiteSpace(line) Then Continue For

                    If line.StartsWith("#EXTINF", StringComparison.OrdinalIgnoreCase) Then
                        ' Format: #EXTINF:duration,Title
                        Dim parts = line.Split(","c, 2)
                        If parts.Length = 2 Then
                            pendingTitle = parts(1).Trim()
                        End If

                    ElseIf Not line.StartsWith("#") Then
                        ' This is a file path
                        Dim title = If(pendingTitle, IO.Path.GetFileNameWithoutExtension(line))
                        items.Add(New Player.PlaylistItemType With {.Path = line, .Title = title})
                        pendingTitle = Nothing
                    End If
                Next

                Return items
            End Function
            Public Sub Export(path As String, items As IEnumerable(Of Player.PlaylistItemType)) Implements IPlaylistIOFormat.Export
                Using writer As New IO.StreamWriter(path, False, System.Text.Encoding.UTF8)
                    writer.WriteLine("#EXTM3U")
                    For Each item In items
                        ' Write EXTINF with -1 duration (unknown) and title
                        writer.WriteLine($"#EXTINF:-1,{item.Title}")
                        writer.WriteLine(item.Path)
                    Next
                End Using
            End Sub

        End Class
        Private Class PlaylistIOFormatM3U
            Inherits PlaylistIOFormatM3UBase
            Public Overrides ReadOnly Property Name As String = "M3U Playlist"
            Public Overrides ReadOnly Property FileExtension As String = ".m3u"
        End Class
        Private Class PlaylistIOFormatM3U8
            Inherits PlaylistIOFormatM3UBase
            Public Overrides ReadOnly Property Name As String = "M3U8 Playlist"
            Public Overrides ReadOnly Property FileExtension As String = ".m3u8"
        End Class
        Private Class PlaylistIOFormatPLS
            Implements IPlaylistIOFormat

            Public ReadOnly Property Name As String Implements IPlaylistIOFormat.Name
                Get
                    Return "PLS Playlist"
                End Get
            End Property
            Public ReadOnly Property FileExtension As String Implements IPlaylistIOFormat.FileExtension
                Get
                    Return ".pls"
                End Get
            End Property

            Public Function Import(path As String) As List(Of Player.PlaylistItemType) Implements IPlaylistIOFormat.Import
                Dim items As List(Of Player.PlaylistItemType) = New List(Of Player.PlaylistItemType)

                'Dictionary to hold grouped entries
                Dim files As New Dictionary(Of Integer, String)
                Dim titles As New Dictionary(Of Integer, String)

                For Each line In IO.File.ReadAllLines(path, System.Text.Encoding.UTF8)
                    If line.StartsWith("File", StringComparison.OrdinalIgnoreCase) Then
                        Dim idx = Integer.Parse(line.Substring(4, line.IndexOf("="c) - 4))
                        files(idx) = line.Substring(line.IndexOf("="c) + 1).Trim()
                    ElseIf line.StartsWith("Title", StringComparison.OrdinalIgnoreCase) Then
                        Dim idx = Integer.Parse(line.Substring(5, line.IndexOf("="c) - 5))
                        titles(idx) = line.Substring(line.IndexOf("="c) + 1).Trim()
                    End If
                Next

                'Build items in order
                For Each kvp In files.OrderBy(Function(f) f.Key)
                    Dim idx = kvp.Key
                    Dim pathVal = kvp.Value
                    Dim titleVal As String = Nothing
                    titles.TryGetValue(idx, titleVal)
                    If String.IsNullOrEmpty(titleVal) Then
                        titleVal = IO.Path.GetFileNameWithoutExtension(pathVal)
                    End If
                    items.Add(New Player.PlaylistItemType With {.Path = pathVal, .Title = titleVal})
                Next

                Return items
            End Function
            Public Sub Export(path As String, items As IEnumerable(Of Player.PlaylistItemType)) Implements IPlaylistIOFormat.Export
                Using writer As New IO.StreamWriter(path, False, System.Text.Encoding.UTF8)
                    writer.WriteLine("[playlist]")
                    Dim index As Integer = 1
                    For Each item In items
                        writer.WriteLine($"File{index}={item.Path}")
                        writer.WriteLine($"Title{index}={item.Title}")
                        writer.WriteLine($"Length{index}=-1") ' Unknown length
                        index += 1
                    Next
                    writer.WriteLine($"NumberOfEntries={index - 1}")
                    writer.WriteLine("Version=2")
                End Using
            End Sub

        End Class
        Private Class PlaylistIOFormatXSPF
            Implements IPlaylistIOFormat

            Public ReadOnly Property Name As String Implements IPlaylistIOFormat.Name
                Get
                    Return "XSPF Playlist"
                End Get
            End Property
            Public ReadOnly Property FileExtension As String Implements IPlaylistIOFormat.FileExtension
                Get
                    Return ".xspf"
                End Get
            End Property

            Public Function Import(path As String) As List(Of Player.PlaylistItemType) Implements IPlaylistIOFormat.Import
                Dim items As New List(Of Player.PlaylistItemType)
                Dim doc As XDocument = XDocument.Load(path)
                Dim ns As XNamespace = "http://xspf.org/ns/0/"

                For Each track In doc.Descendants(ns + "track")
                    Dim loc = track.Element(ns + "location")?.Value
                    Dim title = track.Element(ns + "title")?.Value

                    If Not String.IsNullOrEmpty(loc) Then
                        ' Convert URI back to local path if possible
                        Dim pathVal As String = loc
                        If pathVal.StartsWith("file://", StringComparison.OrdinalIgnoreCase) Then
                            pathVal = New Uri(pathVal).LocalPath
                        End If

                        If String.IsNullOrEmpty(title) Then
                            title = IO.Path.GetFileNameWithoutExtension(pathVal)
                        End If

                        items.Add(New Player.PlaylistItemType With {.Path = pathVal, .Title = title})
                    End If
                Next

                Return items
            End Function
            Public Sub Export(path As String, items As IEnumerable(Of Player.PlaylistItemType)) Implements IPlaylistIOFormat.Export
                Dim ns As XNamespace = "http://xspf.org/ns/0/"
                Dim doc As New XDocument(
            New XElement(ns + "playlist",
                New XAttribute("version", "1"),
                New XAttribute(XNamespace.Xmlns + "xspf", ns),
                New XElement(ns + "trackList",
                    From item In items
                    Select New XElement(ns + "track",
                        New XElement(ns + "location", New Uri(item.Path).AbsoluteUri),
                        New XElement(ns + "title", item.Title)
                            )
                        )
                    )
                )
                doc.Save(path)
            End Sub

        End Class
        Private Class PlaylistIOFormatWPL
            Implements IPlaylistIOFormat

            Public ReadOnly Property Name As String Implements IPlaylistIOFormat.Name
                Get
                    Return "Windows Media Player Playlist"
                End Get
            End Property
            Public ReadOnly Property FileExtension As String Implements IPlaylistIOFormat.FileExtension
                Get
                    Return ".wpl"
                End Get
            End Property

            Public Function Import(path As String) As List(Of Player.PlaylistItemType) Implements IPlaylistIOFormat.Import
                Dim items As New List(Of Player.PlaylistItemType)
                Dim doc As XDocument = XDocument.Load(path)

                'WPL uses no namespace, so we can just query elements directly
                For Each mediaelement In doc.Descendants("media")
                    Dim src = mediaelement.Attribute("src")?.Value
                    If Not String.IsNullOrEmpty(src) Then
                        Dim title = IO.Path.GetFileNameWithoutExtension(src)
                        items.Add(New Player.PlaylistItemType With {.Path = src, .Title = title})
                    End If
                Next

                Return items
            End Function
            Public Sub Export(path As String, items As IEnumerable(Of Player.PlaylistItemType)) Implements IPlaylistIOFormat.Export
                Dim doc As New XDocument(
                    New XElement("smil",
                    New XElement("head",
                    New XElement("meta",
                    New XAttribute("name", "Generator"),
                    New XAttribute("content", "SkyeMusic")
                    ),
                    New XElement("title", "Playlist Export")
                    ),
                    New XElement("body",
                    New XElement("seq",
                        From item In items
                        Select New XElement("media",
                            New XAttribute("src", item.Path)
                                    )
                                )
                            )
                        )
                    )
                doc.Save(path)
            End Sub
        End Class
        Private Class PlaylistIOFormatASX
            Implements IPlaylistIOFormat

            Public ReadOnly Property Name As String Implements IPlaylistIOFormat.Name
                Get
                    Return "ASX Playlist"
                End Get
            End Property
            Public ReadOnly Property FileExtension As String Implements IPlaylistIOFormat.FileExtension
                Get
                    Return ".asx"
                End Get
            End Property

            Public Function Import(path As String) As List(Of Player.PlaylistItemType) Implements IPlaylistIOFormat.Import
                Dim items As New List(Of Player.PlaylistItemType)
                Dim doc As XDocument = XDocument.Load(path)

                For Each entry In doc.Descendants("entry")
                    Dim href = entry.Element("ref")?.Attribute("href")?.Value
                    Dim title = entry.Element("title")?.Value
                    If Not String.IsNullOrEmpty(href) Then
                        If String.IsNullOrEmpty(title) Then
                            title = IO.Path.GetFileNameWithoutExtension(href)
                        End If
                        items.Add(New Player.PlaylistItemType With {.Path = href, .Title = title})
                    End If
                Next

                Return items
            End Function
            Public Sub Export(path As String, items As IEnumerable(Of Player.PlaylistItemType)) Implements IPlaylistIOFormat.Export
                Dim doc As New XDocument(
                    New XElement("asx",
                    New XAttribute("version", "3.0"),
                    From item In items
                    Select New XElement("entry",
                        New XElement("title", item.Title),
                        New XElement("ref", New XAttribute("href", item.Path))
                            )
                        )
                    )
                doc.Save(path)
            End Sub
        End Class
        Private Class PlaylistIOFormatJSON
            Implements IPlaylistIOFormat

            Public ReadOnly Property Name As String Implements IPlaylistIOFormat.Name
                Get
                    Return "JSON Playlist"
                End Get
            End Property
            Public ReadOnly Property FileExtension As String Implements IPlaylistIOFormat.FileExtension
                Get
                    Return ".json"
                End Get
            End Property

            Public Function Import(path As String) As List(Of Player.PlaylistItemType) Implements IPlaylistIOFormat.Import
                Dim items As New List(Of Player.PlaylistItemType)
                Dim json = IO.File.ReadAllText(path, System.Text.Encoding.UTF8)
                Dim options As New System.Text.Json.JsonSerializerOptions With {
                    .PropertyNameCaseInsensitive = True,
                    .IncludeFields = True}
                Dim parsed = System.Text.Json.JsonSerializer.Deserialize(Of List(Of Player.PlaylistItemType))(json, options)
                If parsed Is Nothing Then
                    Debug.WriteLine("JSON Import: parsed is Nothing")
                Else
                    Debug.WriteLine($"JSON Import: parsed.Count = {parsed.Count}")
                    For Each it In parsed
                        Debug.WriteLine($" -> Path={it.Path}, Title={it.Title}")
                    Next
                    items.AddRange(parsed)
                End If
                Return items
            End Function
            Public Sub Export(path As String, items As IEnumerable(Of Player.PlaylistItemType)) Implements IPlaylistIOFormat.Export
                Dim options As New System.Text.Json.JsonSerializerOptions With {
                    .WriteIndented = True,
                    .IncludeFields = True}
                Dim json = System.Text.Json.JsonSerializer.Serialize(items, options)
                IO.File.WriteAllText(path, json, System.Text.Encoding.UTF8)
            End Sub

        End Class
        Friend Class PlaylistIO
            Private Shared ReadOnly Formats As New List(Of IPlaylistIOFormat) From {
                New PlaylistIOFormatM3U(),
                New PlaylistIOFormatM3U8(),
                New PlaylistIOFormatPLS,
                New PlaylistIOFormatXSPF(),
                New PlaylistIOFormatWPL(),
                New PlaylistIOFormatASX(),
                New PlaylistIOFormatJSON()}       'Add new formats here later
            Public Shared Function Import(path As String) As List(Of Player.PlaylistItemType)
                Dim fmt = Formats.FirstOrDefault(Function(f) path.EndsWith(f.FileExtension, StringComparison.OrdinalIgnoreCase))
                If fmt Is Nothing Then
                    WriteToLog("Playlist Import: Unknown Playlist Format")
                    Return New List(Of Player.PlaylistItemType)
                End If
                Try
                    Return fmt.Import(path)
                Catch ex As Exception
                    WriteToLog($"{fmt.Name} Import Failed ({If(String.IsNullOrEmpty(path), "<no path provided>", path)}): {ex.Message}")
                    Return New List(Of Player.PlaylistItemType)
                End Try
            End Function
            Public Shared Function Export(path As String, items As IEnumerable(Of Player.PlaylistItemType)) As Boolean
                Dim fmt = Formats.FirstOrDefault(Function(f) path.EndsWith(f.FileExtension, StringComparison.OrdinalIgnoreCase))
                If fmt Is Nothing Then
                    WriteToLog("Playlist Export: Unknown Playlist Format")
                    Return False
                End If
                Try
                    fmt.Export(path, items)
                    Return True
                Catch ex As Exception
                    WriteToLog($"{fmt.Name} Export Failed ({If(String.IsNullOrEmpty(path), "<no path provided>", path)}): {ex.Message}")
                    Return False
                End Try
            End Function
        End Class

        'App Handlers
        Private Sub timerScreenSaverWatcher_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timerScreenSaverWatcher.Tick
            Static ssStatus As Boolean
            Skye.WinAPI.SystemParametersInfo(Skye.WinAPI.SPI_GETSCREENSAVERRUNNING, 0, ssStatus, 0)
            Select Case ssStatus
                Case True
                    If Not ScreenSaverActive Then
                        ScreenSaverActive = True
                        Debug.Print("ScreenSaver Activated @ " + Now.ToString)
                        WriteToLog("ScreenSaver Activated @ " + Now.ToString)
                        SessionSuspended()
                    End If
                Case False
                    If ScreenSaverActive Then
                        ScreenSaverActive = False
                        Debug.Print("ScreenSaver Deactivated @ " + Now.ToString)
                        WriteToLog("ScreenSaver Deactivated @ " + Now.ToString)
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
        Private Sub watcher_Created(sender As Object, e As FileSystemEventArgs)
            QueueWatcherWork(e.FullPath)
            Debug.Print("File Created: " + e.FullPath)
        End Sub
        Private Sub watcher_Renamed(sender As Object, e As RenamedEventArgs)
            QueueWatcherWork(e.OldFullPath)
            QueueWatcherWork(e.FullPath)
            Debug.Print("File Renamed: " + e.OldFullPath + " to " + e.FullPath)
        End Sub
        Private Sub watcher_Deleted(sender As Object, e As FileSystemEventArgs)
            QueueWatcherWork(e.FullPath)
            Debug.Print("File Deleted: " + e.FullPath)
        End Sub
        Private Sub watcher_Changed(sender As Object, e As FileSystemEventArgs)
            QueueWatcherWork(e.FullPath)
            Debug.Print("File Changed: " + e.FullPath)
        End Sub
        Private Sub WatcherWorkTimer_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles WatcherWorkTimer.Elapsed

            Dim workItems As List(Of String)
            SyncLock WatcherWorkList
                workItems = WatcherWorkList.ToList()
                WatcherWorkList.Clear()
            End SyncLock

            WatcherDoWork(workItems)

        End Sub

        'App Procedures
        Friend Sub Initialize()
            WriteToLog(My.Application.Info.ProductName + " Started")

            Dim TracePath As String = IO.Path.Combine(Application.Info.DirectoryPath, "skyemusic_trace.log")
            Trace.Listeners.Add(New RollingTraceListener(TracePath))
            Trace.AutoFlush = True
            Trace.WriteLine("=== SkyeMusic Started ===")

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance) 'Allows use of Windows-1252 character encoding, needed for Components context menu Proper Case function.
            LicenseKey.RegisterSyncfusionLicense()

            GetOptions()
            GetDebugOptions()
            CurrentTheme = GetCurrentThemeProperties()
            LoadHistory()

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
            FRMLibrary.Opacity = 0 'This is done to initialize the form on startup, but keep it hidden from the user, to prevent null reference errors when the FileSystemWatcher fires and the user hasn't opened the form yet.
            FRMLibrary.Show()
            FRMLibrary.Hide()
            FRMLibrary.Opacity = 1

            GenerateHotKeyList()
            RegisterHotKeys()
            SetHistoryAutoSaveTimer()
            timerScreenSaverWatcher.Interval = 1000
            timerScreenSaverWatcher.Start()
            AddHandler Microsoft.Win32.SystemEvents.SessionSwitch, AddressOf SessionSwitchHandler 'SessionSwitchHandler is a handler for session switch events, sets the ScreenLocked flag, and acts accordingly.

            WatcherWorkTimer.AutoReset = False
            SetWatchers()

        End Sub
        Friend Sub Finalize()
            UnRegisterHotKeys()
            If FRMLog IsNot Nothing AndAlso FRMLog.Visible Then FRMLog.Close()
            If FRMLibrary.Visible Then FRMLibrary.Close()
            FRMLibrary.Dispose()
            SaveHistory()
            SaveOptions()
            SetWatchers(True) 'Dispose watchers
            My.App.WriteToLog(My.Application.Info.ProductName + " Closed")
        End Sub
        Friend Sub SaveHistory()
            If History.Count = 0 Then
                If My.Computer.FileSystem.FileExists(HistoryPath) Then My.Computer.FileSystem.DeleteFile(HistoryPath)
            Else
                Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
                Dim writer As New System.Xml.Serialization.XmlSerializer(GetType(Collections.Generic.List(Of App.Song)))
                If Not My.Computer.FileSystem.DirectoryExists(App.UserPath) Then
                    My.Computer.FileSystem.CreateDirectory(App.UserPath)
                End If
                Dim file As New System.IO.StreamWriter(HistoryPath)
                writer.Serialize(file, History)
                file.Close()
                file.Dispose()
                writer = Nothing
                Debug.Print("History Saved")
                App.WriteToLog("History Saved (" + Skye.Common.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
            End If
        End Sub
        Private Sub LoadHistory()
            If My.Computer.FileSystem.FileExists(HistoryPath) Then
                Dim starttime As TimeSpan = My.Computer.Clock.LocalTime.TimeOfDay
                Dim reader As New System.Xml.Serialization.XmlSerializer(GetType(Collections.Generic.List(Of Song)))
                Dim file As New IO.FileStream(App.HistoryPath, IO.FileMode.Open)
                Try
                    History = DirectCast(reader.Deserialize(file), Collections.Generic.List(Of Song))
                Catch
                    History = Nothing
                End Try
                file.Close()
                file.Dispose()
                reader = Nothing
                If History Is Nothing Then
                    App.WriteToLog("History Not Loaded: File not valid (" + HistoryPath + ")")
                    History = New Collections.Generic.List(Of Song) 'Initialize an empty history if the file is not valid
                Else
                    App.WriteToLog("History Loaded (" + Skye.Common.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
                End If
            Else
                App.WriteToLog("History Not Loaded: File does not exist")
            End If
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
                RegKey.SetValue("RandomHistoryUpdateInterval", App.RandomHistoryUpdateInterval.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("HistoryUpdateInterval", App.HistoryUpdateInterval.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("HistoryAutoSaveInterval", App.HistoryAutoSaveInterval.ToString, Microsoft.Win32.RegistryValueKind.String)
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
                RegKey.SetValue("WatcherEnabled", WatcherEnabled.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("WatcherUpdateLibrary", WatcherUpdateLibrary.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.SetValue("WatcherUpdatePlaylist", WatcherUpdatePlaylist.ToString, Microsoft.Win32.RegistryValueKind.String)
                RegKey.Flush()
                RegSubKey.Dispose()
                RegKey.Close()
                RegKey.Dispose()
                App.WriteToLog("Options Saved (" + Skye.Common.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
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
                Try : App.Theme = CType([Enum].Parse(GetType(App.Themes), RegKey.GetValue("Theme", App.Themes.Red.ToString).ToString), App.Themes)
                Catch : App.Theme = App.Themes.Red
                End Try
                Select Case RegKey.GetValue("SuspendOnSessionChange", "True").ToString
                    Case "False", "0" : App.SuspendOnSessionChange = False
                    Case Else : App.SuspendOnSessionChange = True
                End Select
                Select Case RegKey.GetValue("SaveWindowMetrics", "False").ToString
                    Case "True", "1" : App.SaveWindowMetrics = True
                    Case Else : App.SaveWindowMetrics = False
                End Select
                RandomHistoryUpdateInterval = CByte(Val(RegKey.GetValue("RandomHistoryUpdateInterval", 5.ToString)))
                If RandomHistoryUpdateInterval > 60 Then
                    RandomHistoryUpdateInterval = 60 'Limit the interval to a maximum of 60 seconds
                End If
                HistoryUpdateInterval = CByte(Val(RegKey.GetValue("HistoryUpdateInterval", 5.ToString)))
                If HistoryUpdateInterval > 60 Then
                    HistoryUpdateInterval = 60 'Limit the interval to a maximum of 60 seconds
                End If
                HistoryAutoSaveInterval = CUShort(Val(RegKey.GetValue("HistoryAutoSaveInterval", 5.ToString)))
                If HistoryAutoSaveInterval < 1 Then
                    App.HistoryAutoSaveInterval = 1
                ElseIf HistoryAutoSaveInterval > 1440 Then
                    HistoryAutoSaveInterval = 1440 'Limit the interval to a maximum of 1440 minutes (24 hours)
                End If
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
                Select Case RegKey.GetValue("WatcherEnabled", "False").ToString
                    Case "True", "1" : WatcherEnabled = True
                    Case Else : WatcherEnabled = False
                End Select
                Select Case RegKey.GetValue("WatcherUpdateLibrary", "False").ToString
                    Case "True", "1" : WatcherUpdateLibrary = True
                    Case Else : WatcherUpdateLibrary = False
                End Select
                Select Case RegKey.GetValue("WatcherUpdatePlaylist", "False").ToString
                    Case "True", "1" : WatcherUpdatePlaylist = True
                    Case Else : WatcherUpdatePlaylist = False
                End Select
                RegSubKey.Dispose()
                RegKey.Close()
                RegKey.Dispose()
                App.WriteToLog("Options Loaded (" + Skye.Common.GenerateLogTime(starttime, My.Computer.Clock.LocalTime.TimeOfDay, True) + ")")
            Catch ex As Exception
                WriteToLog("Error Loading Options" + vbCr + ex.Message)
            End Try
        End Sub
        Private Sub GetDebugOptions()
#If DEBUG Then
            'WatcherEnabled = True
            'WatcherUpdateLibrary = True
            'WatcherUpdatePlaylist = True
#End If
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
                    status = Skye.WinAPI.RegisterHotKey(Player.Handle, key.WinID, key.KeyMod, key.KeyCode)
                    Debug.Print("HotKey '" + key.Description + " (" + key.WinID.ToString + ") (" + key.Key.ToString + ") (" + key.KeyCode.ToString + " mod " + key.KeyMod.ToString + ")' " + IIf(status, "Successfully Registered", "Failed To Register").ToString)
                    WriteToLog("HotKey '" + key.Description + " (" + key.WinID.ToString + ") (" + key.Key.ToString + ") (" + key.KeyCode.ToString + " mod " + key.KeyMod.ToString + ")' " + IIf(status, "Successfully Registered", "Failed To Register").ToString)
                End If
            Next
        End Sub
        Private Sub UnRegisterHotKeys()
            Dim status As Boolean
            For Each key As HotKey In HotKeys
                If Not key.Key = Keys.None Then
                    status = Skye.WinAPI.UnregisterHotKey(Player.Handle, key.WinID)
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
        Friend Sub WriteToLog(logtext As String)
            Static fInfo As IO.FileInfo
            Try
                fInfo = New IO.FileInfo(LogPath)
                If fInfo.Exists AndAlso fInfo.Length >= 1000000 Then IO.File.Move(LogPath, LogPath.Insert(LogPath.Length - 4, "Backup@" + My.Computer.Clock.LocalTime.ToString("yyyyMMdd") + "@" + My.Computer.Clock.LocalTime.ToString("HHmmss")))
                'IO.File.AppendAllText(LogPath, IIf(String.IsNullOrEmpty(logtext), String.Empty, My.Computer.Clock.LocalTime.ToString("yyyy/MM/dd") + " @ " + My.Computer.Clock.LocalTime.ToString("HH:mm:ss") + " --> " + logtext + Chr(13)).ToString)
                If Not String.IsNullOrEmpty(logtext) Then IO.File.AppendAllText(LogPath, My.Computer.Clock.LocalTime.ToString("yyyy/MM/dd") + " @ " + My.Computer.Clock.LocalTime.ToString("HH:mm:ss") + " --> " + logtext + Chr(13))
            Catch
            Finally : fInfo = Nothing
            End Try
        End Sub
        Friend Sub SetWatchers(Optional forcestop As Boolean = False)

            'Clear existing watchers
            For Each watcher In Watchers
                watcher.EnableRaisingEvents = False
                watcher.Dispose()
                App.WriteToLog("Watcher Stopped: " & watcher.Path)
                Debug.WriteLine("Watcher Stopped: " & watcher.Path)
            Next
            Watchers.Clear()

            'Set new watchers
            If WatcherEnabled AndAlso LibrarySearchFolders.Count > 0 AndAlso Not forcestop Then
                For Each folder In LibrarySearchFolders
                    Dim watcher As New FileSystemWatcher(folder)
                    watcher.IncludeSubdirectories = LibrarySearchSubFolders
                    watcher.NotifyFilter = NotifyFilters.FileName Or NotifyFilters.Size Or NotifyFilters.LastWrite
                    AddHandler watcher.Created, AddressOf watcher_Created
                    AddHandler watcher.Renamed, AddressOf watcher_Renamed
                    AddHandler watcher.Deleted, AddressOf watcher_Deleted
                    AddHandler watcher.Changed, AddressOf watcher_Changed
                    Watchers.Add(watcher)
                    watcher.EnableRaisingEvents = True
                    App.WriteToLog("Watching Folder: " & folder)
                    Debug.WriteLine("Watching Folder: " & folder)
                Next
            End If

        End Sub
        Private Sub QueueWatcherWork(path As String)

            SyncLock WatcherWorkList
                If Not WatcherWorkList.Contains(path) Then
                    WatcherWorkList.Add(path)
                End If
            End SyncLock

            WatcherWorkTimer.Stop()
            WatcherWorkTimer.Start()

        End Sub
        Private Sub WatcherDoWork(paths As List(Of String))
            If FRMLibrary.InvokeRequired Then
                FRMLibrary.Invoke(Sub() FRMLibrary.DoWatcherWork(paths))
            Else
                FRMLibrary.DoWatcherWork(paths)
            End If

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
            'FRMLog.RTBLog.ReadOnly = True
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

        'History Handlers
        Private Sub timerHistoryUpdate_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timerHistoryUpdate.Tick
            timerHistoryUpdate.Stop()
            UpdateHistory()
        End Sub
        Private Sub timerRandomHistoryUpdate_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timerRandomHistoryUpdate.Tick
            timerRandomHistoryUpdate.Stop()
            UpdateRandomHistory()
        End Sub
        Private Sub timerHistoryAutoSave_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timerHistoryAutoSave.Tick
            If HistoryChanged Then
                HistoryChanged = False
                SaveHistory()
            End If
        End Sub

        'History Procedures
        Friend Sub AddToHistoryFromPlaylist(songorstream As String, Optional stream As Boolean = False)
            'Check if in the history already
            Dim existingindex As Integer = History.FindIndex(Function(p) p.Path.Equals(songorstream, StringComparison.OrdinalIgnoreCase))
            If existingindex < 0 Then
                'If not in the history, add it
                Dim newsong As New Song With {
                    .Path = songorstream,
                    .InLibrary = False,
                    .IsStream = stream,
                    .PlayCount = 0,
                    .Added = DateTime.Now,
                    .FirstPlayed = Nothing,
                    .LastPlayed = Nothing,
                    .Rating = 0}
                History.Add(newsong)
                HistoryChanged = True
                Debug.Print("Added " + songorstream + " to history")
            End If
        End Sub
        Friend Sub AddToHistoryFromLibrary(songorstream As String)
            'Check if in the history already
            Dim existingindex As Integer = History.FindIndex(Function(p) p.Path.Equals(songorstream, StringComparison.OrdinalIgnoreCase))
            If existingindex >= 0 Then
                'If it is in the history, update the InLibrary flag if necessary
                If Not History(existingindex).InLibrary Then
                    Dim existingsong As Song = History(existingindex)
                    existingsong.InLibrary = True
                    History(existingindex) = existingsong
                    'Debug.Print("Updated InLibrary flag for " + songorstream)
                End If
            Else
                'If not in the history, add it with InLibrary flag set to True
                Dim newsong As New Song With {
                    .Path = songorstream,
                    .InLibrary = True,
                    .IsStream = False,
                    .PlayCount = 0,
                    .Added = DateTime.Now,
                    .FirstPlayed = Nothing,
                    .LastPlayed = Nothing,
                    .Rating = 0}
                History.Add(newsong)
                'Debug.Print("Added " + songorstream + " to history with InLibrary flag set")
            End If
            HistoryChanged = True
        End Sub
        Friend Sub ClearHistoryInLibraryFlag()
            If History.Count > 0 Then
                'Clear the InLibrary flag for all songs in the history
                For index As Integer = 0 To History.Count - 1
                    If History(index).InLibrary Then
                        Dim song As Song = History(index)
                        song.InLibrary = False
                        History(index) = song
                    End If
                Next
                Debug.Print("Cleared History InLibrary Flag")
            End If
        End Sub
        Friend Sub PruneHistory()

            Debug.Print("Pruning History..." + History.Count.ToString + " total history items...")

            'Find songs that are not in the library and don't exist
            Dim prunelist As Collections.Generic.List(Of Song) = History.FindAll(Function(p) Not p.InLibrary AndAlso Not My.Computer.FileSystem.FileExists(p.Path))
            Debug.Print("Pruning History..." + prunelist.Count.ToString + " items found so far...")

            'Find streams that are not in the playlist
            Dim streamlist As Collections.Generic.List(Of Song) = prunelist.FindAll(Function(p) p.IsStream)
            Debug.Print("Pruning Streams..." + streamlist.Count.ToString + " streams found so far...")
            For Each s As Song In streamlist
                If s.IsStream AndAlso Player.LVPlaylist.FindItemWithText(s.Path, True, 0) IsNot Nothing Then
                    Debug.Print(s.Path + " found in playlist")
                    prunelist.Remove(s)
                End If
            Next

            'Prune History
            For Each s As Song In prunelist
                History.Remove(s)
            Next
            Debug.Print("History Pruned (" + prunelist.Count.ToString + ")")
            Debug.Print("Pruning History Complete..." + History.Count.ToString + " total history items.")
            WriteToLog("History Pruned (" + prunelist.Count.ToString + ")")
            streamlist = Nothing
            prunelist = Nothing
        End Sub
        Friend Sub UpdateHistory(songorstream As String)
            timerHistoryUpdate.Stop()
            If HistoryUpdateInterval = 0 Then
                timerHistoryUpdate.Tag = songorstream
                UpdateHistory()
                Return
            Else
                timerHistoryUpdate.Interval = HistoryUpdateInterval * 1000
                timerHistoryUpdate.Tag = songorstream
                timerHistoryUpdate.Start()
            End If
        End Sub
        Private Sub UpdateHistory()
            Dim songorstream As String = CStr(timerHistoryUpdate.Tag)
            Dim existingindex As Integer = History.FindIndex(Function(p) p.Path.Equals(songorstream, StringComparison.OrdinalIgnoreCase))
            If existingindex >= 0 Then
                Dim existingsong As Song = History(existingindex)
                existingsong.PlayCount += CUShort(1)
                If existingsong.FirstPlayed = Nothing Then existingsong.FirstPlayed = DateTime.Now
                existingsong.LastPlayed = DateTime.Now
                History(existingindex) = existingsong
                HistoryChanged = True
                Debug.Print("Updated PlayCount for " + songorstream + " to " + existingsong.PlayCount.ToString)
                WriteToLog("History Updated " + songorstream + " (" + existingsong.PlayCount.ToString + If(existingsong.PlayCount = 1, " Play", " Plays") + ")")
                Player.UpdateHistoryInPlaylist(songorstream)
            Else
                Debug.Print("Song not found in history: " + songorstream)
            End If
        End Sub
        Friend Sub UpdateRandomHistory(songorstream As String)
            timerRandomHistoryUpdate.Stop()
            If RandomHistoryUpdateInterval = 0 Then
                timerRandomHistoryUpdate.Tag = songorstream
                UpdateRandomHistory()
                Return
            Else
                timerRandomHistoryUpdate.Interval = RandomHistoryUpdateInterval * 1000
                timerRandomHistoryUpdate.Tag = songorstream
                timerRandomHistoryUpdate.Start()
            End If
        End Sub
        Private Sub UpdateRandomHistory()
            Dim songorstream As String = CStr(timerRandomHistoryUpdate.Tag)
            Player.AddToRandomHistory(songorstream)
        End Sub
        Friend Sub StopHistoryUpdates()
            timerHistoryUpdate.Stop()
            timerRandomHistoryUpdate.Stop()
            Debug.Print("History Update Timers Stopped")
        End Sub
        Friend Sub SetHistoryAutoSaveTimer()
            timerHistoryAutoSave.Stop()
            timerHistoryAutoSave.Interval = App.HistoryAutoSaveInterval * 60 * 1000 'Convert minutes to milliseconds
            timerHistoryAutoSave.Start()
            Debug.Print("History AutoSave Timer Set to " & App.HistoryAutoSaveInterval.ToString & " minutes")
        End Sub

        'Functions
        Private Function FormatPlaylistTitleCore(filePath As String) As String 'Core routine: all formatting logic lives here
            FormatPlaylistTitleCore = String.Empty
            Dim tlfile As TagLib.File = Nothing
            If App.PlaylistTitleFormat <> App.PlaylistTitleFormats.UseFilename Then
                Try
                    tlfile = TagLib.File.Create(filePath)
                Catch ex As Exception
                    WriteToLog("TagLib Error while Formatting Playlist Title, Cannot read from file: " + filePath + Chr(13) + ex.Message)
                    tlfile = Nothing
                End Try
            End If
            If tlfile Is Nothing Then
                FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
            Else
                Select Case App.PlaylistTitleFormat
                    Case App.PlaylistTitleFormats.Song
                        If tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Title
                            End If
                        End If
                    Case App.PlaylistTitleFormats.SongGenre
                        If tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Title
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.SongArtist
                        If tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Title
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.SongArtistAlbum
                        If tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Title
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.SongAlbumArtist
                        If tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Title
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.SongArtistGenre
                        If tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Title
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.SongGenreArtist
                        If tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Title
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.SongArtistAlbumGenre
                        If tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Title
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.SongAlbumArtistGenre
                        If tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Title
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.SongGenreArtistAlbum
                        If tlfile.Tag.Title = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Title
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.ArtistSong
                        If tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.ArtistSongAlbum
                        If tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.ArtistAlbumSong
                        If tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.ArtistGenreSong
                        If tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.ArtistSongGenre
                        If tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.ArtistSongAlbumGenre
                        If tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.ArtistGenreSongAlbum
                        If tlfile.Tag.FirstPerformer = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.AlbumSongArtist
                        If tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Album
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.AlbumArtistSong
                        If tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Album
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.AlbumGenreSongArtist
                        If tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Album
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.AlbumGenreArtistSong
                        If tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Album
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.AlbumSongArtistGenre
                        If tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Album
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.AlbumArtistSongGenre
                        If tlfile.Tag.Album = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.Album
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.FirstGenre = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.GenreSong
                        If tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.GenreSongArtist
                        If tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.GenreArtistSong
                        If tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.GenreAlbumSongArtist
                        If tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.GenreAlbumArtistSong
                        If tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.GenreSongArtistAlbum
                        If tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                        End If
                    Case App.PlaylistTitleFormats.GenreSongAlbumArtist
                        If tlfile.Tag.FirstGenre = String.Empty Then
                            FormatPlaylistTitleCore = IO.Path.GetFileNameWithoutExtension(filePath)
                        Else
                            If App.PlaylistTitleRemoveSpaces Then
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre.Replace(" ", "")
                            Else
                                FormatPlaylistTitleCore += tlfile.Tag.FirstGenre
                            End If
                            If Not tlfile.Tag.Title = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Title.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Title
                                End If
                            End If
                            If Not tlfile.Tag.Album = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.Album.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.Album
                                End If
                            End If
                            If Not tlfile.Tag.FirstPerformer = String.Empty Then
                                FormatPlaylistTitleCore += App.PlaylistTitleSeparator
                                If App.PlaylistTitleRemoveSpaces Then
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer.Replace(" ", "")
                                Else
                                    FormatPlaylistTitleCore += tlfile.Tag.FirstPerformer
                                End If
                            End If
                        End If
                End Select
                tlfile = Nothing
            End If
            If App.VideoExtensionDictionary.ContainsKey(Path.GetExtension(filePath)) Then
                FormatPlaylistTitleCore += PlaylistVideoIdentifier
            End If
        End Function
        ''' <summary>
        ''' Called from Library with a ListViewItem
        ''' </summary>
        ''' <param name="item">Library listview item type</param>
        ''' <returns></returns>
        Friend Function FormatPlaylistTitle(item As ListViewItem) As String
            Return FormatPlaylistTitleCore(item.SubItems(FRMLibrary.LVLibrary.Columns("FilePath").Index).Text)
        End Function
        ''' <summary>
        ''' Called from Player with a raw filename
        ''' </summary>
        ''' <param name="filename">File Path of the target song.</param>
        ''' <returns></returns>
        Friend Function FormatPlaylistTitle(filename As String) As String
            Return FormatPlaylistTitleCore(filename)
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
        Friend Function GetAccentColor() As Color
            Dim c As Color
            Dim regkey As RegistryKey
            Dim regvalue As Integer
            regkey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\DWM")
            regvalue = CInt(regkey.GetValue("AccentColor"))
            If regvalue = Nothing Then
                c = App.CurrentTheme.BackColor
            Else
                c = Color.FromArgb(255, Skye.WinAPI.GetRValue(regvalue), Skye.WinAPI.GetGValue(regvalue), Skye.WinAPI.GetBValue(regvalue))
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
            Select Case Theme
                Case Themes.BlueAccent
                    Return BlueAccentTheme
                Case Themes.PinkAccent
                    Return PinkAccentTheme
                Case Themes.Light
                    Return LightTheme
                Case Themes.Dark
                    Return DarkTheme
                Case Themes.DarkPink
                    Return DarkPinkTheme
                Case Themes.Pink
                    Return PinkTheme
                Case Themes.Red
                    Return RedTheme
                Case Else
                    Return RedTheme
            End Select
        End Function

    End Module

    Friend Class RollingTraceListener
        Inherits TextWriterTraceListener

        Private ReadOnly maxSize As Long = 1024 * 1024 ' 1 MB
        Private ReadOnly logPath As String

        Public Sub New(path As String)
            MyBase.New(path)
            logPath = path
        End Sub

        Public Overrides Sub WriteLine(message As String)
            RotateIfNeeded()
            Dim stamped As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | {message}"
            MyBase.WriteLine(stamped)
            Me.Writer.Flush()
        End Sub
        Private Sub RotateIfNeeded()
            Try
                If File.Exists(logPath) AndAlso New FileInfo(logPath).Length > maxSize Then
                    Dim backupPath As String = Path.ChangeExtension(logPath, ".old")
                    If File.Exists(backupPath) Then File.Delete(backupPath)
                    File.Move(logPath, backupPath)
                    'Reset writer to new file
                    Me.Writer.Close()
                    Me.Writer = New StreamWriter(logPath, append:=False)
                End If
            Catch
                'Fail silently — logging should never crash the app
            End Try
        End Sub

    End Class

    Friend Class ListViewItemStringComparer
        Implements IComparer

        Private col As Integer
        Private sort As SortOrder

        Public Sub New(column As Integer, sortorder As SortOrder)
            col = column
            sort = sortorder
        End Sub
        Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim returnVal As Integer
            returnVal = String.Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
            If sort = SortOrder.Descending Then returnVal *= -1
            Return returnVal
        End Function

    End Class
    Friend Class ListViewItemNumberComparer
        Implements IComparer

        Private col As Integer
        Private sort As SortOrder

        Public Sub New(column As Integer, sortorder As SortOrder)
            col = column
            sort = sortorder
        End Sub
        Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim returnval As Integer
            Dim valx As Integer
            Dim valy As Integer
            If Integer.TryParse(CType(x, ListViewItem).SubItems(col).Text, valx) AndAlso Integer.TryParse(CType(y, ListViewItem).SubItems(col).Text, valy) Then
                returnval = valx.CompareTo(valy)
                If sort = SortOrder.Descending Then
                    Return -returnval
                Else
                    Return returnval
                End If
            Else
                returnval = String.Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
                If sort = SortOrder.Descending Then
                    Return -returnval
                Else
                    Return returnval
                End If
            End If
        End Function

    End Class
    Friend Class ListViewItemDateComparer
        Implements IComparer

        Private col As Integer
        Private sort As SortOrder

        Public Sub New(column As Integer, sortorder As SortOrder)
            col = column
            sort = sortorder
        End Sub
        Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim returnval As Integer
            Dim datex As DateTime
            Dim datey As DateTime
            If DateTime.TryParse(CType(x, ListViewItem).SubItems(col).Text, datex) AndAlso DateTime.TryParse(CType(y, ListViewItem).SubItems(col).Text, datey) Then
                returnval = datex.CompareTo(datey)
                If sort = SortOrder.Descending Then
                    Return -returnval
                Else
                    Return returnval
                End If
            Else
                returnval = String.Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
                If sort = SortOrder.Descending Then
                    Return -returnval
                Else
                    Return returnval
                End If
            End If
        End Function

    End Class

    Public Class MessageFilterPlayerIgnoreFullscreenMouseClick

        Implements IMessageFilter
        Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
            If Player.Fullscreen AndAlso (m.Msg = Skye.WinAPI.WM_LBUTTONDOWN OrElse m.Msg = Skye.WinAPI.WM_LBUTTONUP OrElse m.Msg = Skye.WinAPI.WM_LBUTTONDBLCLK OrElse m.Msg = Skye.WinAPI.WM_RBUTTONDOWN OrElse m.Msg = Skye.WinAPI.WM_RBUTTONUP) Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class


End Namespace
