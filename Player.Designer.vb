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
        ToolStripSeparator1 = New ToolStripSeparator()
        CMIShowCurrent = New ToolStripMenuItem()
        CMIViewInLibrary = New ToolStripMenuItem()
        ToolStripSeparator6 = New ToolStripSeparator()
        CMIHelperApp1 = New ToolStripMenuItem()
        CMIHelperApp2 = New ToolStripMenuItem()
        CMIOpenLocation = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        CMICopyTitle = New ToolStripMenuItem()
        CMICopyFileName = New ToolStripMenuItem()
        CMICopyFilePath = New ToolStripMenuItem()
        TimerPosition = New Timer(components)
        AxPlayer = New AxWMPLib.AxWindowsMediaPlayer()
        BtnReverse = New Button()
        MenuPlayer = New MenuStrip()
        MIFile = New ToolStripMenuItem()
        MIOpen = New ToolStripMenuItem()
        MIOpenURL = New ToolStripMenuItem()
        ToolStripSeparator4 = New ToolStripSeparator()
        MIExit = New ToolStripMenuItem()
        MIView = New ToolStripMenuItem()
        MIFullscreen = New ToolStripMenuItem()
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
        PEXLeft = New Components.ProgressEX()
        PEXRight = New Components.ProgressEX()
        PicBoxAlbumArt = New PictureBox()
        LblAlbumArtSelect = New Label()
        TxtBoxPlaylistSearch = New TextBox()
        ListBoxPlaylistSearch = New ListBox()
        PanelMedia = New Panel()
        BtnPrevious = New Button()
        TipPlayer = New ToolTip(components)
        LVPlaylist = New Components.ListViewEX()
        TimerMeter = New Timer(components)
        TimerVisualizer = New Timer(components)
        PicBoxVisualizer = New PictureBox()
        TxtBoxLyrics = New TextBox()
        LblPlaylistCount = New Components.LabelCSY()
        LblDuration = New Components.LabelCSY()
        LblPosition = New Components.LabelCSY()
        CMLyrics = New Components.TextBoxContextMenu()
        TrackBarPosition = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 10)
        CMPlaylist.SuspendLayout()
        CType(AxPlayer, ComponentModel.ISupportInitialize).BeginInit()
        MenuPlayer.SuspendLayout()
        CType(PicBoxAlbumArt, ComponentModel.ISupportInitialize).BeginInit()
        CType(PicBoxVisualizer, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' BtnPlay
        ' 
        BtnPlay.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnPlay.BackColor = Color.Transparent
        BtnPlay.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnPlay.Location = New Point(12, 399)
        BtnPlay.Name = "BtnPlay"
        BtnPlay.Size = New Size(50, 50)
        BtnPlay.TabIndex = 1
        BtnPlay.TabStop = False
        BtnPlay.UseVisualStyleBackColor = False
        ' 
        ' CMPlaylist
        ' 
        CMPlaylist.Items.AddRange(New ToolStripItem() {CMIPlay, CMIQueue, CMIPlayWithWindows, ToolStripSeparator3, CMIPlaylistAdd, CMIPlaylistRemove, CMIClearPlaylist, ToolStripSeparator1, CMIShowCurrent, CMIViewInLibrary, ToolStripSeparator6, CMIHelperApp1, CMIHelperApp2, CMIOpenLocation, ToolStripSeparator2, CMICopyTitle, CMICopyFileName, CMICopyFilePath})
        CMPlaylist.Name = "CMPlaylist"
        CMPlaylist.Size = New Size(177, 336)
        ' 
        ' CMIPlay
        ' 
        CMIPlay.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CMIPlay.Image = My.Resources.Resources.ImagePlay
        CMIPlay.Name = "CMIPlay"
        CMIPlay.Size = New Size(176, 22)
        CMIPlay.Text = "Play"
        ' 
        ' CMIQueue
        ' 
        CMIQueue.Image = My.Resources.Resources.ImagePlay
        CMIQueue.Name = "CMIQueue"
        CMIQueue.Size = New Size(176, 22)
        CMIQueue.Text = "Queue"
        ' 
        ' CMIPlayWithWindows
        ' 
        CMIPlayWithWindows.Image = My.Resources.Resources.ImageWindows16
        CMIPlayWithWindows.Name = "CMIPlayWithWindows"
        CMIPlayWithWindows.Size = New Size(176, 22)
        CMIPlayWithWindows.Text = "Play In Windows"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(173, 6)
        ' 
        ' CMIPlaylistAdd
        ' 
        CMIPlaylistAdd.Image = My.Resources.Resources.ImageAdd16
        CMIPlaylistAdd.Name = "CMIPlaylistAdd"
        CMIPlaylistAdd.Size = New Size(176, 22)
        CMIPlaylistAdd.Text = "Add"
        ' 
        ' CMIPlaylistRemove
        ' 
        CMIPlaylistRemove.Image = My.Resources.Resources.ImageClearRemoveDelete16
        CMIPlaylistRemove.Name = "CMIPlaylistRemove"
        CMIPlaylistRemove.Size = New Size(176, 22)
        CMIPlaylistRemove.Text = "Remove"
        ' 
        ' CMIClearPlaylist
        ' 
        CMIClearPlaylist.Image = My.Resources.Resources.ImageClearRemoveDelete16
        CMIClearPlaylist.Name = "CMIClearPlaylist"
        CMIClearPlaylist.Size = New Size(176, 22)
        CMIClearPlaylist.Text = "Clear Playlist"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(173, 6)
        ' 
        ' CMIShowCurrent
        ' 
        CMIShowCurrent.Image = My.Resources.Resources.ImageAcceptOKGoStart
        CMIShowCurrent.Name = "CMIShowCurrent"
        CMIShowCurrent.Size = New Size(176, 22)
        CMIShowCurrent.Text = "Show Current Song"
        ' 
        ' CMIViewInLibrary
        ' 
        CMIViewInLibrary.Image = My.Resources.Resources.ImageLibrary16
        CMIViewInLibrary.Name = "CMIViewInLibrary"
        CMIViewInLibrary.Size = New Size(176, 22)
        CMIViewInLibrary.Text = "View In Library"
        ' 
        ' ToolStripSeparator6
        ' 
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New Size(173, 6)
        ' 
        ' CMIHelperApp1
        ' 
        CMIHelperApp1.Image = My.Resources.Resources.ImageSkyeTag
        CMIHelperApp1.Name = "CMIHelperApp1"
        CMIHelperApp1.Size = New Size(176, 22)
        ' 
        ' CMIHelperApp2
        ' 
        CMIHelperApp2.Image = My.Resources.Resources.ImageMP3Tag16
        CMIHelperApp2.Name = "CMIHelperApp2"
        CMIHelperApp2.Size = New Size(176, 22)
        ' 
        ' CMIOpenLocation
        ' 
        CMIOpenLocation.Image = My.Resources.Resources.ImageOpen16
        CMIOpenLocation.Name = "CMIOpenLocation"
        CMIOpenLocation.Size = New Size(176, 22)
        CMIOpenLocation.Text = "Open File Location"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(173, 6)
        ' 
        ' CMICopyTitle
        ' 
        CMICopyTitle.Image = My.Resources.Resources.ImageCopy16
        CMICopyTitle.Name = "CMICopyTitle"
        CMICopyTitle.Size = New Size(176, 22)
        CMICopyTitle.Text = "Copy Title"
        ' 
        ' CMICopyFileName
        ' 
        CMICopyFileName.Image = My.Resources.Resources.ImageCopy16
        CMICopyFileName.Name = "CMICopyFileName"
        CMICopyFileName.Size = New Size(176, 22)
        CMICopyFileName.Text = "Copy File Name"
        ' 
        ' CMICopyFilePath
        ' 
        CMICopyFilePath.Image = My.Resources.Resources.ImageCopy16
        CMICopyFilePath.Name = "CMICopyFilePath"
        CMICopyFilePath.Size = New Size(176, 22)
        CMICopyFilePath.Text = "Copy File Path"
        ' 
        ' TimerPosition
        ' 
        TimerPosition.Enabled = True
        TimerPosition.Interval = 10
        ' 
        ' AxPlayer
        ' 
        AxPlayer.Anchor = AnchorStyles.None
        AxPlayer.Enabled = True
        AxPlayer.Location = New Point(1, 27)
        AxPlayer.Name = "AxPlayer"
        AxPlayer.OcxState = CType(resources.GetObject("AxPlayer.OcxState"), AxHost.State)
        AxPlayer.Size = New Size(173, 214)
        AxPlayer.TabIndex = 35
        ' 
        ' BtnReverse
        ' 
        BtnReverse.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnReverse.BackColor = Color.Transparent
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
        MenuPlayer.Items.AddRange(New ToolStripItem() {MIFile, MIView, MIVisualizer, MILyrics, MIPlayMode, MILibrary, MIAbout})
        MenuPlayer.Location = New Point(0, 0)
        MenuPlayer.Name = "MenuPlayer"
        MenuPlayer.Size = New Size(984, 24)
        MenuPlayer.TabIndex = 12
        MenuPlayer.Text = "MenuStrip1"
        ' 
        ' MIFile
        ' 
        MIFile.DropDownItems.AddRange(New ToolStripItem() {MIOpen, MIOpenURL, ToolStripSeparator4, MIExit})
        MIFile.ForeColor = SystemColors.HighlightText
        MIFile.Image = My.Resources.Resources.ImageOpen16
        MIFile.Name = "MIFile"
        MIFile.Size = New Size(53, 20)
        MIFile.Text = "File"
        ' 
        ' MIOpen
        ' 
        MIOpen.Image = My.Resources.Resources.ImageOpen16
        MIOpen.Name = "MIOpen"
        MIOpen.Size = New Size(213, 22)
        MIOpen.Text = "Open"
        ' 
        ' MIOpenURL
        ' 
        MIOpenURL.Image = My.Resources.Resources.ImageGlobe
        MIOpenURL.Name = "MIOpenURL"
        MIOpenURL.Size = New Size(213, 22)
        MIOpenURL.Text = "Open URL From Clipboard"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(210, 6)
        ' 
        ' MIExit
        ' 
        MIExit.Image = My.Resources.Resources.ImageExit
        MIExit.Name = "MIExit"
        MIExit.Size = New Size(213, 22)
        MIExit.Text = "Exit"
        ' 
        ' MIView
        ' 
        MIView.DropDownItems.AddRange(New ToolStripItem() {MIFullscreen, ToolStripSeparator5, MIOptions})
        MIView.ForeColor = SystemColors.HighlightText
        MIView.Image = My.Resources.Resources.ImageView
        MIView.Name = "MIView"
        MIView.Size = New Size(60, 20)
        MIView.Text = "View"
        ' 
        ' MIFullscreen
        ' 
        MIFullscreen.Image = My.Resources.Resources.ImageFullscreen16
        MIFullscreen.Name = "MIFullscreen"
        MIFullscreen.Size = New Size(131, 22)
        MIFullscreen.Text = "Full Screen"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(128, 6)
        ' 
        ' MIOptions
        ' 
        MIOptions.Image = My.Resources.Resources.ImageSettings16
        MIOptions.Name = "MIOptions"
        MIOptions.Size = New Size(131, 22)
        MIOptions.Text = "Options"
        ' 
        ' MIVisualizer
        ' 
        MIVisualizer.Image = My.Resources.Resources.ImageVisualizer16
        MIVisualizer.Name = "MIVisualizer"
        MIVisualizer.Size = New Size(28, 20)
        ' 
        ' MILyrics
        ' 
        MILyrics.BackColor = Color.Transparent
        MILyrics.Image = My.Resources.Resources.ImageLyrics
        MILyrics.Name = "MILyrics"
        MILyrics.Size = New Size(28, 20)
        MILyrics.Visible = False
        ' 
        ' MIPlayMode
        ' 
        MIPlayMode.ForeColor = SystemColors.HighlightText
        MIPlayMode.Name = "MIPlayMode"
        MIPlayMode.Size = New Size(75, 20)
        MIPlayMode.Text = "Play Mode"
        ' 
        ' MILibrary
        ' 
        MILibrary.ForeColor = SystemColors.HighlightText
        MILibrary.Image = My.Resources.Resources.ImageLibrary16
        MILibrary.Name = "MILibrary"
        MILibrary.Size = New Size(71, 20)
        MILibrary.Text = "Library"
        ' 
        ' MIAbout
        ' 
        MIAbout.DropDownItems.AddRange(New ToolStripItem() {MIShowHelp, MIShowLog, MIShowAbout})
        MIAbout.ForeColor = SystemColors.HighlightText
        MIAbout.Image = My.Resources.Resources.ImageAbout16
        MIAbout.Name = "MIAbout"
        MIAbout.Size = New Size(68, 20)
        MIAbout.Text = "About"
        ' 
        ' MIShowHelp
        ' 
        MIShowHelp.Image = My.Resources.Resources.ImageHelp16
        MIShowHelp.Name = "MIShowHelp"
        MIShowHelp.Size = New Size(107, 22)
        MIShowHelp.Text = "Help"
        ' 
        ' MIShowLog
        ' 
        MIShowLog.Image = My.Resources.Resources.ImageLog16
        MIShowLog.Name = "MIShowLog"
        MIShowLog.Size = New Size(107, 22)
        MIShowLog.Text = "Log"
        ' 
        ' MIShowAbout
        ' 
        MIShowAbout.Image = My.Resources.Resources.ImageAbout16
        MIShowAbout.Name = "MIShowAbout"
        MIShowAbout.Size = New Size(107, 22)
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
        PEXLeft.DrawingColorMode = My.Components.ProgressEX.colorDrawModes.Smooth
        PEXLeft.Location = New Point(12, 352)
        PEXLeft.Maximum = 100
        PEXLeft.Minimum = 0
        PEXLeft.Name = "PEXLeft"
        PEXLeft.PercentageMode = My.Components.ProgressEX.percentageDrawModes.None
        PEXLeft.Size = New Size(385, 5)
        PEXLeft.Step = 1
        PEXLeft.TabIndex = 17
        PEXLeft.TabStop = False
        PEXLeft.Value = 0
        ' 
        ' PEXRight
        ' 
        PEXRight.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        PEXRight.BackColor = Color.Transparent
        PEXRight.DrawingColor = Color.DodgerBlue
        PEXRight.DrawingColorMode = My.Components.ProgressEX.colorDrawModes.Smooth
        PEXRight.Location = New Point(12, 389)
        PEXRight.Maximum = 100
        PEXRight.Minimum = 0
        PEXRight.Name = "PEXRight"
        PEXRight.PercentageMode = My.Components.ProgressEX.percentageDrawModes.None
        PEXRight.Size = New Size(385, 5)
        PEXRight.Step = 1
        PEXRight.TabIndex = 18
        PEXRight.TabStop = False
        PEXRight.Value = 0
        ' 
        ' PicBoxAlbumArt
        ' 
        PicBoxAlbumArt.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
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
        LblAlbumArtSelect.Image = My.Resources.Resources.ImageAlbumArtSelect
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
        TxtBoxPlaylistSearch.ForeColor = SystemColors.InactiveCaption
        TxtBoxPlaylistSearch.Location = New Point(412, 5)
        TxtBoxPlaylistSearch.Name = "TxtBoxPlaylistSearch"
        TxtBoxPlaylistSearch.ShortcutsEnabled = False
        TxtBoxPlaylistSearch.Size = New Size(150, 16)
        TxtBoxPlaylistSearch.TabIndex = 22
        TxtBoxPlaylistSearch.TabStop = False
        TxtBoxPlaylistSearch.Text = "Search Playlist"
        ' 
        ' ListBoxPlaylistSearch
        ' 
        ListBoxPlaylistSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ListBoxPlaylistSearch.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ListBoxPlaylistSearch.FormattingEnabled = True
        ListBoxPlaylistSearch.ItemHeight = 21
        ListBoxPlaylistSearch.Location = New Point(410, 25)
        ListBoxPlaylistSearch.Name = "ListBoxPlaylistSearch"
        ListBoxPlaylistSearch.Size = New Size(574, 88)
        ListBoxPlaylistSearch.TabIndex = 25
        ListBoxPlaylistSearch.Visible = False
        ' 
        ' PanelMedia
        ' 
        PanelMedia.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
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
        BtnPrevious.Location = New Point(236, 399)
        BtnPrevious.Name = "BtnPrevious"
        BtnPrevious.Size = New Size(50, 50)
        BtnPrevious.TabIndex = 8
        BtnPrevious.TabStop = False
        BtnPrevious.UseVisualStyleBackColor = False
        ' 
        ' TipPlayer
        ' 
        TipPlayer.AutomaticDelay = 1000
        TipPlayer.AutoPopDelay = 5000
        TipPlayer.InitialDelay = 1000
        TipPlayer.OwnerDraw = True
        TipPlayer.ReshowDelay = 1000
        ' 
        ' LVPlaylist
        ' 
        LVPlaylist.AllowDrop = True
        LVPlaylist.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        LVPlaylist.ContextMenuStrip = CMPlaylist
        LVPlaylist.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LVPlaylist.InsertionLineColor = Color.Teal
        LVPlaylist.LineAfter = -1
        LVPlaylist.LineBefore = -1
        LVPlaylist.Location = New Point(410, 27)
        LVPlaylist.Name = "LVPlaylist"
        LVPlaylist.OwnerDraw = True
        LVPlaylist.Size = New Size(574, 409)
        LVPlaylist.TabIndex = 0
        LVPlaylist.UseCompatibleStateImageBehavior = False
        LVPlaylist.View = View.Details
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
        ' PicBoxVisualizer
        ' 
        PicBoxVisualizer.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
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
        TxtBoxLyrics.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TxtBoxLyrics.ForeColor = Color.White
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
        ' LblPlaylistCount
        ' 
        LblPlaylistCount.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        LblPlaylistCount.CopyOnDoubleClick = False
        LblPlaylistCount.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
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
        LblDuration.CopyOnDoubleClick = False
        LblDuration.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
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
        LblPosition.CopyOnDoubleClick = False
        LblPosition.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblPosition.Location = New Point(12, 327)
        LblPosition.Name = "LblPosition"
        LblPosition.Size = New Size(80, 25)
        LblPosition.TabIndex = 14
        LblPosition.TextAlign = ContentAlignment.TopCenter
        ' 
        ' CMLyrics
        ' 
        CMLyrics.Name = "CMLyrics"
        CMLyrics.Size = New Size(123, 148)
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
        ' Player
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(984, 461)
        Controls.Add(LblPosition)
        Controls.Add(LblDuration)
        Controls.Add(AxPlayer)
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
        KeyPreview = True
        MinimumSize = New Size(1000, 500)
        Name = "Player"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Player"
        TopMost = True
        CMPlaylist.ResumeLayout(False)
        CType(AxPlayer, ComponentModel.ISupportInitialize).EndInit()
        MenuPlayer.ResumeLayout(False)
        MenuPlayer.PerformLayout()
        CType(PicBoxAlbumArt, ComponentModel.ISupportInitialize).EndInit()
        CType(PicBoxVisualizer, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnPlay As Button
    Friend WithEvents TimerPosition As Timer
    Friend WithEvents AxPlayer As AxWMPLib.AxWindowsMediaPlayer
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
    Friend WithEvents PEXLeft As My.Components.ProgressEX
    Friend WithEvents PEXRight As My.Components.ProgressEX
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
    Friend WithEvents TipPlayer As ToolTip
    Friend WithEvents CMIOpenLocation As ToolStripMenuItem
    Friend WithEvents CMIHelperApp2 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents CMICopyTitle As ToolStripMenuItem
    Friend WithEvents CMICopyFileName As ToolStripMenuItem
    Friend WithEvents CMICopyFilePath As ToolStripMenuItem
    Friend WithEvents CMIPlay As ToolStripMenuItem
    Friend WithEvents CMIPlayWithWindows As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents LVPlaylist As My.Components.ListViewEX
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
    Friend WithEvents Labelcsy1 As My.Components.LabelCSY
    Friend WithEvents LblPlaylistCount As My.Components.LabelCSY
    Friend WithEvents MIVisualizer As ToolStripMenuItem
    Friend WithEvents LblDuration As My.Components.LabelCSY
    Friend WithEvents LblPosition As My.Components.LabelCSY
    Friend WithEvents CMLyrics As My.Components.TextBoxContextMenu
    Friend WithEvents CMIQueue As ToolStripMenuItem
    Friend WithEvents MIOpenURL As ToolStripMenuItem
    Friend WithEvents TrackBarPosition As Syncfusion.Windows.Forms.Tools.TrackBarEx
End Class
