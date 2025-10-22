<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Player
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Player))
        BtnPlay = New Button()
        CMPlaylist = New ContextMenuStrip(components)
        CMIPlay = New ToolStripMenuItem()
        CMIQueue = New ToolStripMenuItem()
        CMIPlayWithWindows = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        CMIPlaylistAdd = New ToolStripMenuItem()
        CMIPlaylistRemove = New ToolStripMenuItem()
        CMIClearPlaylist = New ToolStripMenuItem()
        CMIEditTitle = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        CMIShowCurrent = New ToolStripMenuItem()
        CMIRating = New ToolStripMenuItem()
        CMRatings = New ContextMenuStrip(components)
        CMIRating5Stars = New ToolStripMenuItem()
        CMIRating4Stars = New ToolStripMenuItem()
        CMIRating3Stars = New ToolStripMenuItem()
        CMIRating2Stars = New ToolStripMenuItem()
        CMIRating1Star = New ToolStripMenuItem()
        CMIViewInLibrary = New ToolStripMenuItem()
        ToolStripSeparator6 = New ToolStripSeparator()
        CMIHelperApp1 = New ToolStripMenuItem()
        CMIHelperApp2 = New ToolStripMenuItem()
        CMIOpenLocation = New ToolStripMenuItem()
        TSSeparatorExternalTools = New ToolStripSeparator()
        CMICopyTitle = New ToolStripMenuItem()
        CMICopyFileName = New ToolStripMenuItem()
        CMICopyFilePath = New ToolStripMenuItem()
        TimerPosition = New Timer(components)
        BtnReverse = New Button()
        MenuPlayer = New MenuStrip()
        MIFile = New ToolStripMenuItem()
        MIOpen = New ToolStripMenuItem()
        MIOpenURL = New ToolStripMenuItem()
        MIOpenPlaylist = New ToolStripMenuItem()
        MISavePlaylist = New ToolStripMenuItem()
        ToolStripSeparator4 = New ToolStripSeparator()
        MIExit = New ToolStripMenuItem()
        MIView = New ToolStripMenuItem()
        MIFullscreen = New ToolStripMenuItem()
        MIViewQueue = New ToolStripMenuItem()
        ToolStripSeparator5 = New ToolStripSeparator()
        MIOptions = New ToolStripMenuItem()
        MIVisualizer = New ToolStripMenuItem()
        MILyrics = New ToolStripMenuItem()
        MIPlayMode = New ToolStripMenuItem()
        MILibrary = New ToolStripMenuItem()
        MIAbout = New ToolStripMenuItem()
        MIShowHelp = New ToolStripMenuItem()
        MIShowLog = New ToolStripMenuItem()
        MIShowAbout = New ToolStripMenuItem()
        TimerPlayNext = New Timer(components)
        BtnForward = New Button()
        BtnStop = New Button()
        BtnMute = New Button()
        BtnNext = New Button()
        PEXLeft = New Skye.UI.ProgressEX()
        PEXRight = New Skye.UI.ProgressEX()
        PicBoxAlbumArt = New PictureBox()
        LblAlbumArtSelect = New Label()
        TxtBoxPlaylistSearch = New TextBox()
        ListBoxPlaylistSearch = New ListBox()
        PanelMedia = New Panel()
        BtnPrevious = New Button()
        LVPlaylist = New Skye.UI.ListViewEX()
        PicBoxVisualizer = New PictureBox()
        TxtBoxLyrics = New TextBox()
        CMLyrics = New Skye.UI.TextBoxContextMenu()
        LblPlaylistCount = New Skye.UI.Label()
        LblDuration = New Skye.UI.Label()
        LblPosition = New Skye.UI.Label()
        TrackBarPosition = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 10)
        TimerMeter = New Timer(components)
        TimerVisualizer = New Timer(components)
        TipPlayer = New Skye.UI.ToolTipEX(components)
        VLCViewer = New LibVLCSharp.WinForms.VideoView()
        TipPlaylist = New Skye.UI.ToolTipEX(components)
        TipWatcherNotification = New Skye.UI.ToolTipEX(components)
        TimerStatus = New Timer(components)
        CMPlaylist.SuspendLayout()
        CMRatings.SuspendLayout()
        MenuPlayer.SuspendLayout()
        CType(PicBoxAlbumArt, ComponentModel.ISupportInitialize).BeginInit()
        CType(PicBoxVisualizer, ComponentModel.ISupportInitialize).BeginInit()
        CType(VLCViewer, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' BtnPlay
        ' 
        BtnPlay.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnPlay.BackColor = Color.Transparent
        BtnPlay.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TipPlayer.SetImage(BtnPlay, Nothing)
        TipPlaylist.SetImage(BtnPlay, Nothing)
        TipWatcherNotification.SetImage(BtnPlay, Nothing)
        BtnPlay.Location = New Point(12, 399)
        BtnPlay.Name = "BtnPlay"
        BtnPlay.Size = New Size(50, 50)
        BtnPlay.TabIndex = 1
        BtnPlay.TabStop = False
        BtnPlay.UseVisualStyleBackColor = False
        ' 
        ' CMPlaylist
        ' 
        CMPlaylist.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipPlaylist.SetImage(CMPlaylist, Nothing)
        TipPlayer.SetImage(CMPlaylist, Nothing)
        TipWatcherNotification.SetImage(CMPlaylist, Nothing)
        CMPlaylist.Items.AddRange(New ToolStripItem() {CMIPlay, CMIQueue, CMIPlayWithWindows, ToolStripSeparator3, CMIPlaylistAdd, CMIPlaylistRemove, CMIClearPlaylist, CMIEditTitle, ToolStripSeparator1, CMIShowCurrent, CMIRating, CMIViewInLibrary, ToolStripSeparator6, CMIHelperApp1, CMIHelperApp2, CMIOpenLocation, TSSeparatorExternalTools, CMICopyTitle, CMICopyFileName, CMICopyFilePath})
        CMPlaylist.Name = "CMPlaylist"
        CMPlaylist.Size = New Size(217, 444)
        ' 
        ' CMIPlay
        ' 
        CMIPlay.Image = My.Resources.Resources.ImagePlay
        CMIPlay.Name = "CMIPlay"
        CMIPlay.Size = New Size(216, 26)
        CMIPlay.Text = "Play"
        ' 
        ' CMIQueue
        ' 
        CMIQueue.Image = My.Resources.Resources.ImagePlay
        CMIQueue.Name = "CMIQueue"
        CMIQueue.Size = New Size(216, 26)
        CMIQueue.Text = "Queue"
        ' 
        ' CMIPlayWithWindows
        ' 
        CMIPlayWithWindows.Image = My.Resources.Resources.ImageWindows16
        CMIPlayWithWindows.Name = "CMIPlayWithWindows"
        CMIPlayWithWindows.Size = New Size(216, 26)
        CMIPlayWithWindows.Text = "Play In Windows"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(213, 6)
        ' 
        ' CMIPlaylistAdd
        ' 
        CMIPlaylistAdd.Image = My.Resources.Resources.ImageAdd16
        CMIPlaylistAdd.Name = "CMIPlaylistAdd"
        CMIPlaylistAdd.Size = New Size(216, 26)
        CMIPlaylistAdd.Text = "Add"
        ' 
        ' CMIPlaylistRemove
        ' 
        CMIPlaylistRemove.Image = My.Resources.Resources.ImageClearRemoveDelete16
        CMIPlaylistRemove.Name = "CMIPlaylistRemove"
        CMIPlaylistRemove.Size = New Size(216, 26)
        CMIPlaylistRemove.Text = "Remove"
        ' 
        ' CMIClearPlaylist
        ' 
        CMIClearPlaylist.Image = My.Resources.Resources.ImageClearRemoveDelete16
        CMIClearPlaylist.Name = "CMIClearPlaylist"
        CMIClearPlaylist.Size = New Size(216, 26)
        CMIClearPlaylist.Text = "Clear Playlist"
        ' 
        ' CMIEditTitle
        ' 
        CMIEditTitle.Image = My.Resources.Resources.ImageEdit16
        CMIEditTitle.Name = "CMIEditTitle"
        CMIEditTitle.Size = New Size(216, 26)
        CMIEditTitle.Text = "Edit Title"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(213, 6)
        ' 
        ' CMIShowCurrent
        ' 
        CMIShowCurrent.Image = My.Resources.Resources.ImageAcceptOKGoStart
        CMIShowCurrent.Name = "CMIShowCurrent"
        CMIShowCurrent.Size = New Size(216, 26)
        CMIShowCurrent.Text = "Show Current Song"
        ' 
        ' CMIRating
        ' 
        CMIRating.DropDown = CMRatings
        CMIRating.Image = My.Resources.Resources.ImageRating16
        CMIRating.Name = "CMIRating"
        CMIRating.Size = New Size(216, 26)
        CMIRating.Text = "Rating"
        ' 
        ' CMRatings
        ' 
        CMRatings.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipPlaylist.SetImage(CMRatings, Nothing)
        TipPlayer.SetImage(CMRatings, Nothing)
        TipWatcherNotification.SetImage(CMRatings, Nothing)
        CMRatings.Items.AddRange(New ToolStripItem() {CMIRating5Stars, CMIRating4Stars, CMIRating3Stars, CMIRating2Stars, CMIRating1Star})
        CMRatings.Name = "CMRatings"
        CMRatings.OwnerItem = CMIRating
        CMRatings.ShowImageMargin = False
        CMRatings.Size = New Size(121, 134)
        ' 
        ' CMIRating5Stars
        ' 
        CMIRating5Stars.Name = "CMIRating5Stars"
        CMIRating5Stars.Size = New Size(120, 26)
        CMIRating5Stars.Text = "★★★★★"
        ' 
        ' CMIRating4Stars
        ' 
        CMIRating4Stars.Name = "CMIRating4Stars"
        CMIRating4Stars.Size = New Size(120, 26)
        CMIRating4Stars.Text = "★★★★"
        ' 
        ' CMIRating3Stars
        ' 
        CMIRating3Stars.Name = "CMIRating3Stars"
        CMIRating3Stars.Size = New Size(120, 26)
        CMIRating3Stars.Text = "★★★"
        ' 
        ' CMIRating2Stars
        ' 
        CMIRating2Stars.Name = "CMIRating2Stars"
        CMIRating2Stars.Size = New Size(120, 26)
        CMIRating2Stars.Text = "★★"
        ' 
        ' CMIRating1Star
        ' 
        CMIRating1Star.Name = "CMIRating1Star"
        CMIRating1Star.Size = New Size(120, 26)
        CMIRating1Star.Text = "★"
        ' 
        ' CMIViewInLibrary
        ' 
        CMIViewInLibrary.Image = My.Resources.Resources.ImageLibrary16
        CMIViewInLibrary.Name = "CMIViewInLibrary"
        CMIViewInLibrary.Size = New Size(216, 26)
        CMIViewInLibrary.Text = "View In Library"
        ' 
        ' ToolStripSeparator6
        ' 
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New Size(213, 6)
        ' 
        ' CMIHelperApp1
        ' 
        CMIHelperApp1.Image = My.Resources.Resources.ImageSkyeTag
        CMIHelperApp1.Name = "CMIHelperApp1"
        CMIHelperApp1.Size = New Size(216, 26)
        ' 
        ' CMIHelperApp2
        ' 
        CMIHelperApp2.Image = My.Resources.Resources.ImageMP3Tag16
        CMIHelperApp2.Name = "CMIHelperApp2"
        CMIHelperApp2.Size = New Size(216, 26)
        ' 
        ' CMIOpenLocation
        ' 
        CMIOpenLocation.Image = My.Resources.Resources.ImageOpen16
        CMIOpenLocation.Name = "CMIOpenLocation"
        CMIOpenLocation.Size = New Size(216, 26)
        CMIOpenLocation.Text = "Open File Location"
        ' 
        ' TSSeparatorExternalTools
        ' 
        TSSeparatorExternalTools.Name = "TSSeparatorExternalTools"
        TSSeparatorExternalTools.Size = New Size(213, 6)
        ' 
        ' CMICopyTitle
        ' 
        CMICopyTitle.Image = My.Resources.Resources.ImageCopy16
        CMICopyTitle.Name = "CMICopyTitle"
        CMICopyTitle.Size = New Size(216, 26)
        CMICopyTitle.Text = "Copy Title"
        ' 
        ' CMICopyFileName
        ' 
        CMICopyFileName.Image = My.Resources.Resources.ImageCopy16
        CMICopyFileName.Name = "CMICopyFileName"
        CMICopyFileName.Size = New Size(216, 26)
        CMICopyFileName.Text = "Copy File Name"
        ' 
        ' CMICopyFilePath
        ' 
        CMICopyFilePath.Image = My.Resources.Resources.ImageCopy16
        CMICopyFilePath.Name = "CMICopyFilePath"
        CMICopyFilePath.Size = New Size(216, 26)
        CMICopyFilePath.Text = "Copy File Path"
        ' 
        ' TimerPosition
        ' 
        TimerPosition.Enabled = True
        TimerPosition.Interval = 1000
        ' 
        ' BtnReverse
        ' 
        BtnReverse.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnReverse.BackColor = Color.Transparent
        TipPlayer.SetImage(BtnReverse, Nothing)
        TipPlaylist.SetImage(BtnReverse, Nothing)
        TipWatcherNotification.SetImage(BtnReverse, Nothing)
        BtnReverse.Location = New Point(124, 399)
        BtnReverse.Name = "BtnReverse"
        BtnReverse.Size = New Size(50, 50)
        BtnReverse.TabIndex = 6
        BtnReverse.TabStop = False
        BtnReverse.UseVisualStyleBackColor = False
        ' 
        ' MenuPlayer
        ' 
        MenuPlayer.BackColor = Color.Black
        MenuPlayer.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipPlaylist.SetImage(MenuPlayer, Nothing)
        TipPlayer.SetImage(MenuPlayer, Nothing)
        TipWatcherNotification.SetImage(MenuPlayer, Nothing)
        MenuPlayer.Items.AddRange(New ToolStripItem() {MIFile, MIView, MIVisualizer, MILyrics, MIPlayMode, MILibrary, MIAbout})
        MenuPlayer.Location = New Point(0, 0)
        MenuPlayer.Name = "MenuPlayer"
        MenuPlayer.Size = New Size(984, 25)
        MenuPlayer.TabIndex = 12
        MenuPlayer.Text = "MenuStrip1"
        ' 
        ' MIFile
        ' 
        MIFile.DropDownItems.AddRange(New ToolStripItem() {MIOpen, MIOpenURL, MIOpenPlaylist, MISavePlaylist, ToolStripSeparator4, MIExit})
        MIFile.ForeColor = SystemColors.HighlightText
        MIFile.Image = My.Resources.Resources.ImageOpen16
        MIFile.Name = "MIFile"
        MIFile.Size = New Size(55, 21)
        MIFile.Text = "File"
        ' 
        ' MIOpen
        ' 
        MIOpen.Image = My.Resources.Resources.ImageOpen16
        MIOpen.Name = "MIOpen"
        MIOpen.Size = New Size(173, 22)
        MIOpen.Text = "Open"
        ' 
        ' MIOpenURL
        ' 
        MIOpenURL.Image = My.Resources.Resources.ImageGlobe
        MIOpenURL.Name = "MIOpenURL"
        MIOpenURL.Size = New Size(173, 22)
        MIOpenURL.Text = "Open URL"
        ' 
        ' MIOpenPlaylist
        ' 
        MIOpenPlaylist.Image = My.Resources.Resources.ImageImport16
        MIOpenPlaylist.Name = "MIOpenPlaylist"
        MIOpenPlaylist.Size = New Size(173, 22)
        MIOpenPlaylist.Text = "Open Playlist..."
        ' 
        ' MISavePlaylist
        ' 
        MISavePlaylist.Image = My.Resources.Resources.ImageExport16
        MISavePlaylist.Name = "MISavePlaylist"
        MISavePlaylist.Size = New Size(173, 22)
        MISavePlaylist.Text = "Save Playlist As..."
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(170, 6)
        ' 
        ' MIExit
        ' 
        MIExit.Image = My.Resources.Resources.ImageExit
        MIExit.Name = "MIExit"
        MIExit.Size = New Size(173, 22)
        MIExit.Text = "Exit"
        ' 
        ' MIView
        ' 
        MIView.DropDownItems.AddRange(New ToolStripItem() {MIFullscreen, MIViewQueue, ToolStripSeparator5, MIOptions})
        MIView.ForeColor = SystemColors.HighlightText
        MIView.Image = My.Resources.Resources.ImageView
        MIView.Name = "MIView"
        MIView.Size = New Size(63, 21)
        MIView.Text = "View"
        ' 
        ' MIFullscreen
        ' 
        MIFullscreen.Image = My.Resources.Resources.ImageFullscreen16
        MIFullscreen.Name = "MIFullscreen"
        MIFullscreen.Size = New Size(138, 22)
        MIFullscreen.Text = "Full Screen"
        ' 
        ' MIViewQueue
        ' 
        MIViewQueue.Image = My.Resources.Resources.ImagePlay
        MIViewQueue.Name = "MIViewQueue"
        MIViewQueue.Size = New Size(138, 22)
        MIViewQueue.Text = "Queue"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(135, 6)
        ' 
        ' MIOptions
        ' 
        MIOptions.Image = My.Resources.Resources.ImageSettings16
        MIOptions.Name = "MIOptions"
        MIOptions.Size = New Size(138, 22)
        MIOptions.Text = "Options"
        ' 
        ' MIVisualizer
        ' 
        MIVisualizer.Image = My.Resources.Resources.ImageVisualizer16
        MIVisualizer.Name = "MIVisualizer"
        MIVisualizer.Size = New Size(28, 21)
        ' 
        ' MILyrics
        ' 
        MILyrics.BackColor = Color.Transparent
        MILyrics.Image = My.Resources.Resources.ImageLyrics
        MILyrics.Name = "MILyrics"
        MILyrics.Size = New Size(28, 21)
        MILyrics.Visible = False
        ' 
        ' MIPlayMode
        ' 
        MIPlayMode.ForeColor = SystemColors.HighlightText
        MIPlayMode.Name = "MIPlayMode"
        MIPlayMode.Size = New Size(82, 21)
        MIPlayMode.Text = "Play Mode"
        ' 
        ' MILibrary
        ' 
        MILibrary.ForeColor = SystemColors.HighlightText
        MILibrary.Image = My.Resources.Resources.ImageLibrary16
        MILibrary.Name = "MILibrary"
        MILibrary.Size = New Size(76, 21)
        MILibrary.Text = "Library"
        ' 
        ' MIAbout
        ' 
        MIAbout.DropDownItems.AddRange(New ToolStripItem() {MIShowHelp, MIShowLog, MIShowAbout})
        MIAbout.ForeColor = SystemColors.HighlightText
        MIAbout.Image = My.Resources.Resources.ImageAbout16
        MIAbout.Name = "MIAbout"
        MIAbout.Size = New Size(71, 21)
        MIAbout.Text = "About"
        ' 
        ' MIShowHelp
        ' 
        MIShowHelp.Image = My.Resources.Resources.ImageHelp16
        MIShowHelp.Name = "MIShowHelp"
        MIShowHelp.Size = New Size(111, 22)
        MIShowHelp.Text = "Help"
        ' 
        ' MIShowLog
        ' 
        MIShowLog.Image = My.Resources.Resources.ImageLog16
        MIShowLog.Name = "MIShowLog"
        MIShowLog.Size = New Size(111, 22)
        MIShowLog.Text = "Log"
        ' 
        ' MIShowAbout
        ' 
        MIShowAbout.Image = My.Resources.Resources.ImageAbout16
        MIShowAbout.Name = "MIShowAbout"
        MIShowAbout.Size = New Size(111, 22)
        MIShowAbout.Text = "About"
        ' 
        ' TimerPlayNext
        ' 
        TimerPlayNext.Interval = 250
        ' 
        ' BtnForward
        ' 
        BtnForward.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnForward.BackColor = Color.Transparent
        TipPlayer.SetImage(BtnForward, Nothing)
        TipPlaylist.SetImage(BtnForward, Nothing)
        TipWatcherNotification.SetImage(BtnForward, Nothing)
        BtnForward.Location = New Point(180, 399)
        BtnForward.Name = "BtnForward"
        BtnForward.Size = New Size(50, 50)
        BtnForward.TabIndex = 7
        BtnForward.TabStop = False
        BtnForward.UseVisualStyleBackColor = False
        ' 
        ' BtnStop
        ' 
        BtnStop.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnStop.BackColor = Color.Transparent
        BtnStop.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TipPlayer.SetImage(BtnStop, Nothing)
        TipPlaylist.SetImage(BtnStop, Nothing)
        TipWatcherNotification.SetImage(BtnStop, Nothing)
        BtnStop.Location = New Point(68, 399)
        BtnStop.Name = "BtnStop"
        BtnStop.Size = New Size(50, 50)
        BtnStop.TabIndex = 4
        BtnStop.TabStop = False
        BtnStop.UseVisualStyleBackColor = False
        ' 
        ' BtnMute
        ' 
        BtnMute.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnMute.BackColor = Color.Transparent
        BtnMute.Image = My.Resources.Resources.ImagePlayerSound
        TipPlayer.SetImage(BtnMute, Nothing)
        TipPlaylist.SetImage(BtnMute, Nothing)
        TipWatcherNotification.SetImage(BtnMute, Nothing)
        BtnMute.Location = New Point(348, 399)
        BtnMute.Name = "BtnMute"
        BtnMute.Size = New Size(50, 50)
        BtnMute.TabIndex = 11
        BtnMute.TabStop = False
        BtnMute.UseVisualStyleBackColor = False
        ' 
        ' BtnNext
        ' 
        BtnNext.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnNext.BackColor = Color.Transparent
        TipPlayer.SetImage(BtnNext, Nothing)
        TipPlaylist.SetImage(BtnNext, Nothing)
        TipWatcherNotification.SetImage(BtnNext, Nothing)
        BtnNext.Location = New Point(292, 399)
        BtnNext.Name = "BtnNext"
        BtnNext.Size = New Size(50, 50)
        BtnNext.TabIndex = 9
        BtnNext.TabStop = False
        BtnNext.UseVisualStyleBackColor = False
        ' 
        ' PEXLeft
        ' 
        PEXLeft.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        PEXLeft.BackColor = Color.Transparent
        PEXLeft.DrawingColor = Color.DodgerBlue
        PEXLeft.DrawingColorMode = Skye.UI.ProgressEX.colorDrawModes.Smooth
        TipPlaylist.SetImage(PEXLeft, Nothing)
        TipPlayer.SetImage(PEXLeft, Nothing)
        TipWatcherNotification.SetImage(PEXLeft, Nothing)
        PEXLeft.Location = New Point(12, 352)
        PEXLeft.MaximumSize = New Size(Integer.MaxValue, 40)
        PEXLeft.MinimumSize = New Size(50, 5)
        PEXLeft.Name = "PEXLeft"
        PEXLeft.PercentageMode = Skye.UI.ProgressEX.percentageDrawModes.None
        PEXLeft.Size = New Size(385, 5)
        PEXLeft.Step = 1
        PEXLeft.TabIndex = 17
        PEXLeft.TabStop = False
        ' 
        ' PEXRight
        ' 
        PEXRight.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        PEXRight.BackColor = Color.Transparent
        PEXRight.DrawingColor = Color.DodgerBlue
        PEXRight.DrawingColorMode = Skye.UI.ProgressEX.colorDrawModes.Smooth
        TipPlaylist.SetImage(PEXRight, Nothing)
        TipPlayer.SetImage(PEXRight, Nothing)
        TipWatcherNotification.SetImage(PEXRight, Nothing)
        PEXRight.Location = New Point(12, 389)
        PEXRight.MaximumSize = New Size(Integer.MaxValue, 40)
        PEXRight.MinimumSize = New Size(50, 5)
        PEXRight.Name = "PEXRight"
        PEXRight.PercentageMode = Skye.UI.ProgressEX.percentageDrawModes.None
        PEXRight.Size = New Size(385, 5)
        PEXRight.Step = 1
        PEXRight.TabIndex = 18
        PEXRight.TabStop = False
        ' 
        ' PicBoxAlbumArt
        ' 
        PicBoxAlbumArt.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TipPlayer.SetImage(PicBoxAlbumArt, Nothing)
        TipPlaylist.SetImage(PicBoxAlbumArt, Nothing)
        TipWatcherNotification.SetImage(PicBoxAlbumArt, Nothing)
        PicBoxAlbumArt.Location = New Point(1, 27)
        PicBoxAlbumArt.Name = "PicBoxAlbumArt"
        PicBoxAlbumArt.Size = New Size(407, 294)
        PicBoxAlbumArt.SizeMode = PictureBoxSizeMode.Zoom
        PicBoxAlbumArt.TabIndex = 20
        PicBoxAlbumArt.TabStop = False
        PicBoxAlbumArt.Visible = False
        ' 
        ' LblAlbumArtSelect
        ' 
        LblAlbumArtSelect.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LblAlbumArtSelect.BackColor = Color.Transparent
        TipWatcherNotification.SetImage(LblAlbumArtSelect, Nothing)
        LblAlbumArtSelect.Image = My.Resources.Resources.ImageAlbumArtSelect
        TipPlayer.SetImage(LblAlbumArtSelect, Nothing)
        TipPlaylist.SetImage(LblAlbumArtSelect, Nothing)
        LblAlbumArtSelect.Location = New Point(98, 321)
        LblAlbumArtSelect.Name = "LblAlbumArtSelect"
        LblAlbumArtSelect.Size = New Size(214, 32)
        LblAlbumArtSelect.TabIndex = 15
        LblAlbumArtSelect.Visible = False
        ' 
        ' TxtBoxPlaylistSearch
        ' 
        TxtBoxPlaylistSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TxtBoxPlaylistSearch.BackColor = Color.Black
        TxtBoxPlaylistSearch.BorderStyle = BorderStyle.None
        TxtBoxPlaylistSearch.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxPlaylistSearch.ForeColor = SystemColors.InactiveCaption
        TipPlaylist.SetImage(TxtBoxPlaylistSearch, Nothing)
        TipPlayer.SetImage(TxtBoxPlaylistSearch, Nothing)
        TipWatcherNotification.SetImage(TxtBoxPlaylistSearch, Nothing)
        TxtBoxPlaylistSearch.Location = New Point(414, 5)
        TxtBoxPlaylistSearch.Name = "TxtBoxPlaylistSearch"
        TxtBoxPlaylistSearch.ShortcutsEnabled = False
        TxtBoxPlaylistSearch.Size = New Size(150, 18)
        TxtBoxPlaylistSearch.TabIndex = 22
        TxtBoxPlaylistSearch.TabStop = False
        TxtBoxPlaylistSearch.Text = "Search Playlist"
        ' 
        ' ListBoxPlaylistSearch
        ' 
        ListBoxPlaylistSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ListBoxPlaylistSearch.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ListBoxPlaylistSearch.FormattingEnabled = True
        TipPlaylist.SetImage(ListBoxPlaylistSearch, Nothing)
        TipPlayer.SetImage(ListBoxPlaylistSearch, Nothing)
        TipWatcherNotification.SetImage(ListBoxPlaylistSearch, Nothing)
        ListBoxPlaylistSearch.Location = New Point(410, 25)
        ListBoxPlaylistSearch.Name = "ListBoxPlaylistSearch"
        ListBoxPlaylistSearch.Size = New Size(574, 88)
        ListBoxPlaylistSearch.TabIndex = 25
        ListBoxPlaylistSearch.Visible = False
        ' 
        ' PanelMedia
        ' 
        PanelMedia.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TipWatcherNotification.SetImage(PanelMedia, Nothing)
        TipPlayer.SetImage(PanelMedia, Nothing)
        TipPlaylist.SetImage(PanelMedia, Nothing)
        PanelMedia.Location = New Point(1, 27)
        PanelMedia.Name = "PanelMedia"
        PanelMedia.Size = New Size(407, 294)
        PanelMedia.TabIndex = 30
        PanelMedia.Visible = False
        ' 
        ' BtnPrevious
        ' 
        BtnPrevious.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnPrevious.BackColor = Color.Transparent
        TipPlayer.SetImage(BtnPrevious, Nothing)
        TipPlaylist.SetImage(BtnPrevious, Nothing)
        TipWatcherNotification.SetImage(BtnPrevious, Nothing)
        BtnPrevious.Location = New Point(236, 399)
        BtnPrevious.Name = "BtnPrevious"
        BtnPrevious.Size = New Size(50, 50)
        BtnPrevious.TabIndex = 8
        BtnPrevious.TabStop = False
        BtnPrevious.UseVisualStyleBackColor = False
        ' 
        ' LVPlaylist
        ' 
        LVPlaylist.AllowColumnReorder = True
        LVPlaylist.AllowDrop = True
        LVPlaylist.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        LVPlaylist.ContextMenuStrip = CMPlaylist
        LVPlaylist.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipPlaylist.SetImage(LVPlaylist, Nothing)
        TipPlayer.SetImage(LVPlaylist, Nothing)
        TipWatcherNotification.SetImage(LVPlaylist, Nothing)
        LVPlaylist.InsertionLineColor = Color.Teal
        LVPlaylist.Location = New Point(410, 27)
        LVPlaylist.Name = "LVPlaylist"
        LVPlaylist.OwnerDraw = True
        LVPlaylist.Size = New Size(574, 409)
        LVPlaylist.TabIndex = 0
        LVPlaylist.UseCompatibleStateImageBehavior = False
        LVPlaylist.View = View.Details
        ' 
        ' PicBoxVisualizer
        ' 
        PicBoxVisualizer.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TipPlayer.SetImage(PicBoxVisualizer, Nothing)
        TipPlaylist.SetImage(PicBoxVisualizer, Nothing)
        TipWatcherNotification.SetImage(PicBoxVisualizer, Nothing)
        PicBoxVisualizer.Location = New Point(1, 27)
        PicBoxVisualizer.Name = "PicBoxVisualizer"
        PicBoxVisualizer.Size = New Size(407, 294)
        PicBoxVisualizer.TabIndex = 31
        PicBoxVisualizer.TabStop = False
        PicBoxVisualizer.Visible = False
        ' 
        ' TxtBoxLyrics
        ' 
        TxtBoxLyrics.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBoxLyrics.BorderStyle = BorderStyle.None
        TxtBoxLyrics.ContextMenuStrip = CMLyrics
        TxtBoxLyrics.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TxtBoxLyrics.ForeColor = Color.White
        TipPlaylist.SetImage(TxtBoxLyrics, Nothing)
        TipPlayer.SetImage(TxtBoxLyrics, Nothing)
        TipWatcherNotification.SetImage(TxtBoxLyrics, Nothing)
        TxtBoxLyrics.Location = New Point(1, 27)
        TxtBoxLyrics.Multiline = True
        TxtBoxLyrics.Name = "TxtBoxLyrics"
        TxtBoxLyrics.ReadOnly = True
        TxtBoxLyrics.ScrollBars = ScrollBars.Vertical
        TxtBoxLyrics.ShortcutsEnabled = False
        TxtBoxLyrics.Size = New Size(407, 294)
        TxtBoxLyrics.TabIndex = 36
        TxtBoxLyrics.TextAlign = HorizontalAlignment.Center
        TxtBoxLyrics.Visible = False
        ' 
        ' CMLyrics
        ' 
        CMLyrics.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipPlaylist.SetImage(CMLyrics, Nothing)
        TipPlayer.SetImage(CMLyrics, Nothing)
        TipWatcherNotification.SetImage(CMLyrics, Nothing)
        CMLyrics.Name = "CMLyrics"
        CMLyrics.Size = New Size(149, 176)
        ' 
        ' LblPlaylistCount
        ' 
        LblPlaylistCount.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        LblPlaylistCount.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TipWatcherNotification.SetImage(LblPlaylistCount, Nothing)
        TipPlayer.SetImage(LblPlaylistCount, Nothing)
        TipPlaylist.SetImage(LblPlaylistCount, Nothing)
        LblPlaylistCount.Location = New Point(410, 436)
        LblPlaylistCount.Name = "LblPlaylistCount"
        LblPlaylistCount.Size = New Size(572, 22)
        LblPlaylistCount.TabIndex = 37
        LblPlaylistCount.TextAlign = ContentAlignment.TopCenter
        ' 
        ' LblDuration
        ' 
        LblDuration.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        LblDuration.BackColor = Color.Transparent
        LblDuration.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipWatcherNotification.SetImage(LblDuration, Nothing)
        TipPlayer.SetImage(LblDuration, Nothing)
        TipPlaylist.SetImage(LblDuration, Nothing)
        LblDuration.Location = New Point(318, 327)
        LblDuration.Name = "LblDuration"
        LblDuration.Size = New Size(80, 25)
        LblDuration.TabIndex = 15
        LblDuration.TextAlign = ContentAlignment.TopCenter
        ' 
        ' LblPosition
        ' 
        LblPosition.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LblPosition.BackColor = Color.Transparent
        LblPosition.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipWatcherNotification.SetImage(LblPosition, Nothing)
        TipPlayer.SetImage(LblPosition, Nothing)
        TipPlaylist.SetImage(LblPosition, Nothing)
        LblPosition.Location = New Point(12, 327)
        LblPosition.Name = "LblPosition"
        LblPosition.Size = New Size(80, 25)
        LblPosition.TabIndex = 14
        LblPosition.TextAlign = ContentAlignment.TopCenter
        ' 
        ' TrackBarPosition
        ' 
        TrackBarPosition.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TrackBarPosition.AutoSize = False
        TrackBarPosition.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(45))
        TrackBarPosition.BeforeTouchSize = New Size(385, 20)
        TrackBarPosition.ChannelHeight = 8
        TrackBarPosition.DecreaseButtonSize = New Size(0, 0)
        TrackBarPosition.Enabled = False
        TipPlaylist.SetImage(TrackBarPosition, Nothing)
        TipPlayer.SetImage(TrackBarPosition, Nothing)
        TipWatcherNotification.SetImage(TrackBarPosition, Nothing)
        TrackBarPosition.IncreaseButtonSize = New Size(0, 0)
        TrackBarPosition.Location = New Point(13, 360)
        TrackBarPosition.Name = "TrackBarPosition"
        TrackBarPosition.ShowButtons = False
        TrackBarPosition.ShowFocusRect = False
        TrackBarPosition.Size = New Size(385, 20)
        TrackBarPosition.SliderSize = New Size(15, 25)
        TrackBarPosition.TabIndex = 20
        TrackBarPosition.TabStop = False
        TrackBarPosition.ThemeName = "Default"
        TrackBarPosition.TimerInterval = 100
        TrackBarPosition.Value = 0
        ' 
        ' TimerMeter
        ' 
        TimerMeter.Enabled = True
        TimerMeter.Interval = 75
        ' 
        ' TimerVisualizer
        ' 
        TimerVisualizer.Interval = 75
        ' 
        ' TipPlayer
        ' 
        TipPlayer.FadeInRate = 25
        TipPlayer.FadeOutRate = 25
        TipPlayer.Font = New Font("Segoe UI", 12F)
        TipPlayer.HideDelay = 1000
        TipPlayer.ShadowAlpha = 200
        TipPlayer.ShowDelay = 1000
        ' 
        ' VLCViewer
        ' 
        VLCViewer.BackColor = Color.Black
        TipPlaylist.SetImage(VLCViewer, Nothing)
        TipPlayer.SetImage(VLCViewer, Nothing)
        TipWatcherNotification.SetImage(VLCViewer, Nothing)
        VLCViewer.Location = New Point(1, 27)
        VLCViewer.MediaPlayer = Nothing
        VLCViewer.Name = "VLCViewer"
        VLCViewer.Size = New Size(173, 214)
        VLCViewer.TabIndex = 38
        VLCViewer.Text = "VideoView1"
        VLCViewer.Visible = False
        ' 
        ' TipPlaylist
        ' 
        TipPlaylist.FadeInRate = 25
        TipPlaylist.FadeOutRate = 25
        TipPlaylist.Font = New Font("Segoe UI", 12F)
        TipPlaylist.HideDelay = 1000000
        TipPlaylist.ShadowAlpha = 200
        TipPlaylist.ShowDelay = 1000
        ' 
        ' TipWatcherNotification
        ' 
        TipWatcherNotification.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipWatcherNotification.HideDelay = 7500
        TipWatcherNotification.ShadowAlpha = 200
        TipWatcherNotification.ShowDelay = 1000
        ' 
        ' TimerStatus
        ' 
        TimerStatus.Interval = 8000
        ' 
        ' Player
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(984, 461)
        Controls.Add(VLCViewer)
        Controls.Add(LblPosition)
        Controls.Add(LblDuration)
        Controls.Add(PicBoxAlbumArt)
        Controls.Add(PicBoxVisualizer)
        Controls.Add(ListBoxPlaylistSearch)
        Controls.Add(LVPlaylist)
        Controls.Add(BtnPrevious)
        Controls.Add(TxtBoxPlaylistSearch)
        Controls.Add(BtnNext)
        Controls.Add(BtnMute)
        Controls.Add(BtnStop)
        Controls.Add(BtnForward)
        Controls.Add(BtnReverse)
        Controls.Add(BtnPlay)
        Controls.Add(PEXLeft)
        Controls.Add(PEXRight)
        Controls.Add(MenuPlayer)
        Controls.Add(PanelMedia)
        Controls.Add(TxtBoxLyrics)
        Controls.Add(LblPlaylistCount)
        Controls.Add(TrackBarPosition)
        Controls.Add(LblAlbumArtSelect)
        ForeColor = SystemColors.HighlightText
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipWatcherNotification.SetImage(Me, Nothing)
        TipPlayer.SetImage(Me, Nothing)
        TipPlaylist.SetImage(Me, Nothing)
        KeyPreview = True
        MinimumSize = New Size(1000, 500)
        Name = "Player"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Player"
        TopMost = True
        CMPlaylist.ResumeLayout(False)
        CMRatings.ResumeLayout(False)
        MenuPlayer.ResumeLayout(False)
        MenuPlayer.PerformLayout()
        CType(PicBoxAlbumArt, ComponentModel.ISupportInitialize).EndInit()
        CType(PicBoxVisualizer, ComponentModel.ISupportInitialize).EndInit()
        CType(VLCViewer, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnPlay As Button
    Friend WithEvents TimerPosition As Timer
    Friend WithEvents BtnReverse As Button
    Friend WithEvents MenuPlayer As MenuStrip
    Friend WithEvents MIAbout As ToolStripMenuItem
    Friend WithEvents MIShowHelp As ToolStripMenuItem
    Friend WithEvents MIShowLog As ToolStripMenuItem
    Friend WithEvents MIShowAbout As ToolStripMenuItem
    Friend WithEvents CMPlaylist As ContextMenuStrip
    Friend WithEvents CMIPlaylistRemove As ToolStripMenuItem
    Friend WithEvents CMIPlaylistAdd As ToolStripMenuItem
    Friend WithEvents MIFile As ToolStripMenuItem
    Friend WithEvents MIOpen As ToolStripMenuItem
    Friend WithEvents MIView As ToolStripMenuItem
    Friend WithEvents MIOptions As ToolStripMenuItem
    Friend WithEvents TimerPlayNext As Timer
    Friend WithEvents BtnForward As Button
    Friend WithEvents BtnStop As Button
    Friend WithEvents BtnMute As Button
    Friend WithEvents BtnNext As Button
    Friend WithEvents PEXLeft As Skye.UI.ProgressEX
    Friend WithEvents PEXRight As Skye.UI.ProgressEX
    Friend WithEvents PicBoxAlbumArt As PictureBox
    Friend WithEvents LblAlbumArtSelect As Label
    Friend WithEvents TxtBoxPlaylistSearch As TextBox
    Friend WithEvents ListBoxPlaylistSearch As ListBox
    Friend WithEvents PanelMedia As Panel
    Friend WithEvents MIFullscreen As ToolStripMenuItem
    Friend WithEvents MILibrary As ToolStripMenuItem
    Friend WithEvents BtnPrevious As Button
    Friend WithEvents CMIHelperApp1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents MIExit As ToolStripMenuItem
    Friend WithEvents CMIOpenLocation As ToolStripMenuItem
    Friend WithEvents CMIHelperApp2 As ToolStripMenuItem
    Friend WithEvents TSSeparatorExternalTools As ToolStripSeparator
    Friend WithEvents CMICopyTitle As ToolStripMenuItem
    Friend WithEvents CMICopyFileName As ToolStripMenuItem
    Friend WithEvents CMICopyFilePath As ToolStripMenuItem
    Friend WithEvents CMIPlay As ToolStripMenuItem
    Friend WithEvents CMIPlayWithWindows As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents LVPlaylist As Skye.UI.ListViewEX
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents TimerMeter As Timer
    Friend WithEvents CMIShowCurrent As ToolStripMenuItem
    Friend WithEvents CMIViewInLibrary As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents CMIClearPlaylist As ToolStripMenuItem
    Friend WithEvents TimerVisualizer As Timer
    Friend WithEvents PicBoxVisualizer As PictureBox
    Friend WithEvents MIPlayMode As ToolStripMenuItem
    Friend WithEvents MILyrics As ToolStripMenuItem
    Friend WithEvents TxtBoxLyrics As TextBox
    Friend WithEvents Labelcsy1 As Skye.UI.Label
    Friend WithEvents LblPlaylistCount As Skye.UI.Label
    Friend WithEvents MIVisualizer As ToolStripMenuItem
    Friend WithEvents LblDuration As Skye.UI.Label
    Friend WithEvents LblPosition As Skye.UI.Label
    Friend WithEvents CMLyrics As Skye.UI.TextBoxContextMenu
    Friend WithEvents CMIQueue As ToolStripMenuItem
    Friend WithEvents MIOpenURL As ToolStripMenuItem
    Friend WithEvents TrackBarPosition As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents CMIRating As ToolStripMenuItem
    Friend WithEvents CMRatings As ContextMenuStrip
    Friend WithEvents CMIRating5Stars As ToolStripMenuItem
    Friend WithEvents CMIRating4Stars As ToolStripMenuItem
    Friend WithEvents CMIRating3Stars As ToolStripMenuItem
    Friend WithEvents CMIRating2Stars As ToolStripMenuItem
    Friend WithEvents CMIRating1Star As ToolStripMenuItem
    Friend WithEvents CMIEditTitle As ToolStripMenuItem
    Friend WithEvents MIViewQueue As ToolStripMenuItem
    Friend WithEvents TipPlayer As Skye.UI.ToolTipEX
    Friend WithEvents TipPlaylist As Skye.UI.ToolTipEX
    Friend WithEvents TipWatcherNotification As Skye.UI.ToolTipEX
    Friend WithEvents TimerStatus As Timer
    Friend WithEvents MIOpenPlaylist As ToolStripMenuItem
    Friend WithEvents MISavePlaylist As ToolStripMenuItem
    Friend WithEvents VLCViewer As LibVLCSharp.WinForms.VideoView
End Class
