<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Options
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Options))
        BtnOK = New Button()
        RadBtnElapsed = New RadioButton()
        RadBtnRemaining = New RadioButton()
        GrBoxTime = New GroupBox()
        CoBoxPlayMode = New ComboBox()
        CoBoxPlaylistTitleFormat = New ComboBox()
        TxtBoxPlaylistTitleSeparator = New TextBox()
        CMTxtBox = New Skye.UI.TextBoxContextMenu()
        CkBoxPlaylistRemoveSpaces = New CheckBox()
        TxtBoxPlaylistVideoIdentifier = New TextBox()
        LblTitleFormat = New Skye.UI.Label()
        LblTitleSeparator = New Skye.UI.Label()
        LblVideoIdentifier = New Skye.UI.Label()
        BtnLibrarySearchFoldersAdd = New Button()
        CkBoxLibrarySearchSubFolders = New CheckBox()
        LBLibrarySearchFolders = New ListBox()
        CMLibrarySearchFolders = New ContextMenuStrip(components)
        CMILibrarySearchFoldersAdd = New ToolStripMenuItem()
        CMILibrarySearchFoldersRemove = New ToolStripMenuItem()
        CkBoxSaveWindowMetrics = New CheckBox()
        CkBoxSuspendOnSessionChange = New CheckBox()
        BtnHelperApp1 = New Button()
        BtnHelperApp2 = New Button()
        LblSongPlayMode = New Skye.UI.Label()
        LblHelperApp1Name = New Skye.UI.Label()
        CoBoxPlaylistDefaultAction = New ComboBox()
        CoBoxPlaylistSearchAction = New ComboBox()
        LblDefaultPlaylistAction = New Skye.UI.Label()
        LblPlaylistSearchAction = New Skye.UI.Label()
        CoBoxTheme = New ComboBox()
        LblTheme = New Skye.UI.Label()
        TxtBoxHistoryUpdateInterval = New TextBox()
        LblPlaylistFormatting = New Label()
        TxtBoxHistoryAutoSaveInterval = New TextBox()
        BtnHistorySaveNow = New Button()
        BtnHistoryPrune = New Button()
        TxtBoxHelperApp2Path = New TextBox()
        TxtBoxHelperApp2Name = New TextBox()
        TxtBoxHelperApp1Path = New TextBox()
        TxtBoxHelperApp1Name = New TextBox()
        TxtBoxRandomHistoryUpdateInterval = New TextBox()
        BtnPrunePlaylist = New Button()
        LblHistoryUpdateInterval2 = New Skye.UI.Label()
        LblHistoryUpdateInterval1 = New Skye.UI.Label()
        LblHistoryAutoSaveInterval1 = New Skye.UI.Label()
        LblHistoryAutoSaveInterval2 = New Skye.UI.Label()
        TCOptions = New Syncfusion.Windows.Forms.Tools.TabControlAdv()
        TPApp = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        TxtBoxCompanionServerPort = New Skye.UI.NumericTextBox()
        CkBoxEnableCompanionServer = New CheckBox()
        CkBoxShowTrayIcon = New CheckBox()
        CkBoxMinimizeToTray = New CheckBox()
        LblHelperApp2Path = New Skye.UI.Label()
        LblHelperApp2Name = New Skye.UI.Label()
        LblHelperApp1Path = New Skye.UI.Label()
        LblCompanionServerPort = New Skye.UI.Label()
        TPPlayer = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        GrBoxShowNowPlayingToast = New GroupBox()
        RadBtnNPTBottomRight = New RadioButton()
        RadBtnNPTBottomCenter = New RadioButton()
        RadBtnNPTBottomLeft = New RadioButton()
        RadBtnNPTMiddleRight = New RadioButton()
        RadBtnNPTMiddleCenter = New RadioButton()
        RadBtnNPTMiddleLeft = New RadioButton()
        RadBtnNPTTopRight = New RadioButton()
        RadBtnNPTTopCenter = New RadioButton()
        RadBtnNPTTopLeft = New RadioButton()
        CkBoxShowNowPlayingToast = New CheckBox()
        LblRandomHistoryUpdateInterval1 = New Skye.UI.Label()
        LblRandomHistoryUpdateInterval2 = New Skye.UI.Label()
        TPVisualizers = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        PanelVisualizers = New Panel()
        CoBoxVisualizers = New ComboBox()
        LblVisualizers = New Skye.UI.Label()
        TPPlaylist = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        TxtBoxStatusMessageDisplayTime = New TextBox()
        lblStatusMessageDisplayTime1 = New Skye.UI.Label()
        lblStatusMessageDisplayTime2 = New Skye.UI.Label()
        TPLibrary = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        CkBoxWatchFoldersUpdatePlaylist = New CheckBox()
        CkBoxWatchFoldersUpdateLibrary = New CheckBox()
        CkBoxWatchFolders = New CheckBox()
        LblLibrarySearchFolders = New Label()
        TipOptions = New Skye.UI.ToolTipEX(components)
        TipError = New Skye.UI.ToolTipEX(components)
        GrBoxTime.SuspendLayout()
        CMLibrarySearchFolders.SuspendLayout()
        CType(TCOptions, ComponentModel.ISupportInitialize).BeginInit()
        TCOptions.SuspendLayout()
        TPApp.SuspendLayout()
        TPPlayer.SuspendLayout()
        GrBoxShowNowPlayingToast.SuspendLayout()
        TPVisualizers.SuspendLayout()
        TPPlaylist.SuspendLayout()
        TPLibrary.SuspendLayout()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        TipError.SetImage(BtnOK, Nothing)
        BtnOK.Image = My.Resources.Resources.ImageOK
        TipOptions.SetImage(BtnOK, Nothing)
        BtnOK.Location = New Point(389, 483)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 100
        TipOptions.SetText(BtnOK, "Save & Close Window")
        TipError.SetText(BtnOK, Nothing)
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' RadBtnElapsed
        ' 
        RadBtnElapsed.AutoSize = True
        RadBtnElapsed.Checked = True
        RadBtnElapsed.Font = New Font("Segoe UI", 12F)
        TipOptions.SetImage(RadBtnElapsed, Nothing)
        TipError.SetImage(RadBtnElapsed, Nothing)
        RadBtnElapsed.Location = New Point(6, 22)
        RadBtnElapsed.Name = "RadBtnElapsed"
        RadBtnElapsed.Size = New Size(162, 25)
        RadBtnElapsed.TabIndex = 1
        RadBtnElapsed.TabStop = True
        TipError.SetText(RadBtnElapsed, Nothing)
        TipOptions.SetText(RadBtnElapsed, "Show time that has passed since the song started playing.")
        RadBtnElapsed.Text = "Show Elapsed Time"
        RadBtnElapsed.UseVisualStyleBackColor = True
        ' 
        ' RadBtnRemaining
        ' 
        RadBtnRemaining.AutoSize = True
        RadBtnRemaining.BackColor = SystemColors.Control
        RadBtnRemaining.Font = New Font("Segoe UI", 12F)
        TipOptions.SetImage(RadBtnRemaining, Nothing)
        TipError.SetImage(RadBtnRemaining, Nothing)
        RadBtnRemaining.Location = New Point(6, 51)
        RadBtnRemaining.Name = "RadBtnRemaining"
        RadBtnRemaining.Size = New Size(184, 25)
        RadBtnRemaining.TabIndex = 2
        TipError.SetText(RadBtnRemaining, Nothing)
        TipOptions.SetText(RadBtnRemaining, "Show time left before the song is finished playing.")
        RadBtnRemaining.Text = "Show Remaining Time"
        RadBtnRemaining.UseVisualStyleBackColor = False
        ' 
        ' GrBoxTime
        ' 
        GrBoxTime.BackColor = SystemColors.Control
        GrBoxTime.Controls.Add(RadBtnElapsed)
        GrBoxTime.Controls.Add(RadBtnRemaining)
        GrBoxTime.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipError.SetImage(GrBoxTime, Nothing)
        TipOptions.SetImage(GrBoxTime, Nothing)
        GrBoxTime.Location = New Point(14, 14)
        GrBoxTime.Name = "GrBoxTime"
        GrBoxTime.Size = New Size(197, 89)
        GrBoxTime.TabIndex = 0
        GrBoxTime.TabStop = False
        TipError.SetText(GrBoxTime, Nothing)
        TipOptions.SetText(GrBoxTime, "Choose the way to view the play time of the song." & vbCrLf & "This may also be achieved by clicking the Play Time in the Player.")
        GrBoxTime.Text = "Song Position Display"
        ' 
        ' CoBoxPlayMode
        ' 
        CoBoxPlayMode.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlayMode.FlatStyle = FlatStyle.Flat
        CoBoxPlayMode.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CoBoxPlayMode.FormattingEnabled = True
        TipOptions.SetImage(CoBoxPlayMode, Nothing)
        TipError.SetImage(CoBoxPlayMode, Nothing)
        CoBoxPlayMode.Location = New Point(14, 158)
        CoBoxPlayMode.Name = "CoBoxPlayMode"
        CoBoxPlayMode.Size = New Size(168, 29)
        CoBoxPlayMode.TabIndex = 3
        TipOptions.SetText(CoBoxPlayMode, "Song Play Mode" & vbCrLf & "  Play Once = Play the song then stop." & vbCrLf & "  Repeat = Play the same song over & over." & vbCrLf & "  Play Next = Play the next or previous song in the Playlist." & vbCrLf & "  Shuffle = Play a random song next.")
        TipError.SetText(CoBoxPlayMode, Nothing)
        ' 
        ' CoBoxPlaylistTitleFormat
        ' 
        CoBoxPlaylistTitleFormat.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlaylistTitleFormat.FlatStyle = FlatStyle.Flat
        CoBoxPlaylistTitleFormat.Font = New Font("Segoe UI", 12F)
        CoBoxPlaylistTitleFormat.FormattingEnabled = True
        TipOptions.SetImage(CoBoxPlaylistTitleFormat, Nothing)
        TipError.SetImage(CoBoxPlaylistTitleFormat, Nothing)
        CoBoxPlaylistTitleFormat.Location = New Point(19, 59)
        CoBoxPlaylistTitleFormat.Name = "CoBoxPlaylistTitleFormat"
        CoBoxPlaylistTitleFormat.Size = New Size(230, 29)
        CoBoxPlaylistTitleFormat.TabIndex = 10
        TipOptions.SetText(CoBoxPlaylistTitleFormat, "Choose how to generally format the song title in the Playlist")
        TipError.SetText(CoBoxPlaylistTitleFormat, Nothing)
        ' 
        ' TxtBoxPlaylistTitleSeparator
        ' 
        TxtBoxPlaylistTitleSeparator.ContextMenuStrip = CMTxtBox
        TxtBoxPlaylistTitleSeparator.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(TxtBoxPlaylistTitleSeparator, Nothing)
        TipOptions.SetImage(TxtBoxPlaylistTitleSeparator, Nothing)
        TxtBoxPlaylistTitleSeparator.Location = New Point(18, 138)
        TxtBoxPlaylistTitleSeparator.Name = "TxtBoxPlaylistTitleSeparator"
        TxtBoxPlaylistTitleSeparator.ShortcutsEnabled = False
        TxtBoxPlaylistTitleSeparator.Size = New Size(137, 29)
        TxtBoxPlaylistTitleSeparator.TabIndex = 18
        TipOptions.SetText(TxtBoxPlaylistTitleSeparator, "Enter a separator to separate different song title elements.")
        TipError.SetText(TxtBoxPlaylistTitleSeparator, Nothing)
        ' 
        ' CMTxtBox
        ' 
        CMTxtBox.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipOptions.SetImage(CMTxtBox, Nothing)
        TipError.SetImage(CMTxtBox, Nothing)
        CMTxtBox.Name = "CMTxtBox"
        CMTxtBox.Size = New Size(149, 176)
        TipError.SetText(CMTxtBox, Nothing)
        TipOptions.SetText(CMTxtBox, Nothing)
        ' 
        ' CkBoxPlaylistRemoveSpaces
        ' 
        CkBoxPlaylistRemoveSpaces.AutoSize = True
        CkBoxPlaylistRemoveSpaces.FlatStyle = FlatStyle.Flat
        CkBoxPlaylistRemoveSpaces.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipOptions.SetImage(CkBoxPlaylistRemoveSpaces, Nothing)
        TipError.SetImage(CkBoxPlaylistRemoveSpaces, Nothing)
        CkBoxPlaylistRemoveSpaces.Location = New Point(20, 86)
        CkBoxPlaylistRemoveSpaces.Name = "CkBoxPlaylistRemoveSpaces"
        CkBoxPlaylistRemoveSpaces.Size = New Size(135, 25)
        CkBoxPlaylistRemoveSpaces.TabIndex = 14
        TipError.SetText(CkBoxPlaylistRemoveSpaces, Nothing)
        TipOptions.SetText(CkBoxPlaylistRemoveSpaces, "Select to remove spaces from each element of the song title.")
        CkBoxPlaylistRemoveSpaces.Text = "Remove Spaces"
        CkBoxPlaylistRemoveSpaces.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxPlaylistVideoIdentifier
        ' 
        TxtBoxPlaylistVideoIdentifier.ContextMenuStrip = CMTxtBox
        TxtBoxPlaylistVideoIdentifier.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(TxtBoxPlaylistVideoIdentifier, Nothing)
        TipOptions.SetImage(TxtBoxPlaylistVideoIdentifier, Nothing)
        TxtBoxPlaylistVideoIdentifier.Location = New Point(18, 192)
        TxtBoxPlaylistVideoIdentifier.Name = "TxtBoxPlaylistVideoIdentifier"
        TxtBoxPlaylistVideoIdentifier.ShortcutsEnabled = False
        TxtBoxPlaylistVideoIdentifier.Size = New Size(137, 29)
        TxtBoxPlaylistVideoIdentifier.TabIndex = 22
        TipOptions.SetText(TxtBoxPlaylistVideoIdentifier, "Enter how to identify videos in the Playlist." & vbCrLf & "The identifier will be added to the end of the title.")
        TipError.SetText(TxtBoxPlaylistVideoIdentifier, Nothing)
        ' 
        ' LblTitleFormat
        ' 
        LblTitleFormat.AutoSize = True
        TipError.SetImage(LblTitleFormat, Nothing)
        TipOptions.SetImage(LblTitleFormat, Nothing)
        LblTitleFormat.Location = New Point(19, 38)
        LblTitleFormat.Name = "LblTitleFormat"
        LblTitleFormat.Size = New Size(93, 21)
        LblTitleFormat.TabIndex = 132
        TipOptions.SetText(LblTitleFormat, Nothing)
        LblTitleFormat.Text = "Title Format"
        TipError.SetText(LblTitleFormat, Nothing)
        ' 
        ' LblTitleSeparator
        ' 
        LblTitleSeparator.AutoSize = True
        TipError.SetImage(LblTitleSeparator, Nothing)
        TipOptions.SetImage(LblTitleSeparator, Nothing)
        LblTitleSeparator.Location = New Point(19, 117)
        LblTitleSeparator.Name = "LblTitleSeparator"
        LblTitleSeparator.Size = New Size(111, 21)
        LblTitleSeparator.TabIndex = 132
        TipOptions.SetText(LblTitleSeparator, Nothing)
        LblTitleSeparator.Text = "Title Separator"
        TipError.SetText(LblTitleSeparator, Nothing)
        ' 
        ' LblVideoIdentifier
        ' 
        LblVideoIdentifier.AutoSize = True
        TipError.SetImage(LblVideoIdentifier, Nothing)
        TipOptions.SetImage(LblVideoIdentifier, Nothing)
        LblVideoIdentifier.Location = New Point(18, 173)
        LblVideoIdentifier.Name = "LblVideoIdentifier"
        LblVideoIdentifier.Size = New Size(116, 21)
        LblVideoIdentifier.TabIndex = 133
        TipOptions.SetText(LblVideoIdentifier, Nothing)
        LblVideoIdentifier.Text = "Video Identifier"
        TipError.SetText(LblVideoIdentifier, Nothing)
        ' 
        ' BtnLibrarySearchFoldersAdd
        ' 
        TipError.SetImage(BtnLibrarySearchFoldersAdd, Nothing)
        BtnLibrarySearchFoldersAdd.Image = My.Resources.Resources.ImageAdd16
        TipOptions.SetImage(BtnLibrarySearchFoldersAdd, Nothing)
        BtnLibrarySearchFoldersAdd.Location = New Point(643, 147)
        BtnLibrarySearchFoldersAdd.Name = "BtnLibrarySearchFoldersAdd"
        BtnLibrarySearchFoldersAdd.Size = New Size(32, 31)
        BtnLibrarySearchFoldersAdd.TabIndex = 150
        TipOptions.SetText(BtnLibrarySearchFoldersAdd, "Add folder to list.")
        TipError.SetText(BtnLibrarySearchFoldersAdd, Nothing)
        BtnLibrarySearchFoldersAdd.UseVisualStyleBackColor = True
        ' 
        ' CkBoxLibrarySearchSubFolders
        ' 
        CkBoxLibrarySearchSubFolders.AutoSize = True
        CkBoxLibrarySearchSubFolders.BackColor = SystemColors.Control
        CkBoxLibrarySearchSubFolders.FlatStyle = FlatStyle.Flat
        CkBoxLibrarySearchSubFolders.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipOptions.SetImage(CkBoxLibrarySearchSubFolders, Nothing)
        TipError.SetImage(CkBoxLibrarySearchSubFolders, Nothing)
        CkBoxLibrarySearchSubFolders.Location = New Point(140, 144)
        CkBoxLibrarySearchSubFolders.Name = "CkBoxLibrarySearchSubFolders"
        CkBoxLibrarySearchSubFolders.Size = New Size(161, 25)
        CkBoxLibrarySearchSubFolders.TabIndex = 81
        TipError.SetText(CkBoxLibrarySearchSubFolders, Nothing)
        TipOptions.SetText(CkBoxLibrarySearchSubFolders, "Check this to search all sub-folders of the  folders in your list. Un-Check it to only search the folders listed.")
        CkBoxLibrarySearchSubFolders.Text = "Search Sub-Folders"
        CkBoxLibrarySearchSubFolders.UseVisualStyleBackColor = False
        ' 
        ' LBLibrarySearchFolders
        ' 
        LBLibrarySearchFolders.BackColor = SystemColors.Window
        LBLibrarySearchFolders.ContextMenuStrip = CMLibrarySearchFolders
        LBLibrarySearchFolders.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LBLibrarySearchFolders.FormattingEnabled = True
        TipError.SetImage(LBLibrarySearchFolders, Nothing)
        TipOptions.SetImage(LBLibrarySearchFolders, Nothing)
        LBLibrarySearchFolders.Location = New Point(137, 39)
        LBLibrarySearchFolders.Name = "LBLibrarySearchFolders"
        LBLibrarySearchFolders.Size = New Size(541, 88)
        LBLibrarySearchFolders.Sorted = True
        LBLibrarySearchFolders.TabIndex = 80
        TipOptions.SetText(LBLibrarySearchFolders, "Add your music folders here, the Library uses this list to find your music." & vbCrLf & "Right-Click for options.")
        TipError.SetText(LBLibrarySearchFolders, Nothing)
        ' 
        ' CMLibrarySearchFolders
        ' 
        CMLibrarySearchFolders.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipOptions.SetImage(CMLibrarySearchFolders, Nothing)
        TipError.SetImage(CMLibrarySearchFolders, Nothing)
        CMLibrarySearchFolders.Items.AddRange(New ToolStripItem() {CMILibrarySearchFoldersAdd, CMILibrarySearchFoldersRemove})
        CMLibrarySearchFolders.Name = "CMLibrarySearchFolders"
        CMLibrarySearchFolders.Size = New Size(165, 48)
        TipOptions.SetText(CMLibrarySearchFolders, Nothing)
        TipError.SetText(CMLibrarySearchFolders, Nothing)
        ' 
        ' CMILibrarySearchFoldersAdd
        ' 
        CMILibrarySearchFoldersAdd.Image = My.Resources.Resources.ImageAdd16
        CMILibrarySearchFoldersAdd.Name = "CMILibrarySearchFoldersAdd"
        CMILibrarySearchFoldersAdd.Size = New Size(164, 22)
        CMILibrarySearchFoldersAdd.Text = "Add Folder"
        ' 
        ' CMILibrarySearchFoldersRemove
        ' 
        CMILibrarySearchFoldersRemove.Image = My.Resources.Resources.ImageClearRemoveDelete16
        CMILibrarySearchFoldersRemove.Name = "CMILibrarySearchFoldersRemove"
        CMILibrarySearchFoldersRemove.Size = New Size(164, 22)
        CMILibrarySearchFoldersRemove.Text = "Remove Folder"
        ' 
        ' CkBoxSaveWindowMetrics
        ' 
        CkBoxSaveWindowMetrics.AutoSize = True
        CkBoxSaveWindowMetrics.FlatStyle = FlatStyle.Flat
        CkBoxSaveWindowMetrics.Font = New Font("Segoe UI", 12F)
        TipOptions.SetImage(CkBoxSaveWindowMetrics, Nothing)
        TipError.SetImage(CkBoxSaveWindowMetrics, Nothing)
        CkBoxSaveWindowMetrics.Location = New Point(13, 181)
        CkBoxSaveWindowMetrics.Name = "CkBoxSaveWindowMetrics"
        CkBoxSaveWindowMetrics.Size = New Size(176, 25)
        CkBoxSaveWindowMetrics.TabIndex = 20
        TipError.SetText(CkBoxSaveWindowMetrics, Nothing)
        TipOptions.SetText(CkBoxSaveWindowMetrics, "Auto save window sizes and locations.")
        CkBoxSaveWindowMetrics.Text = "Save Window Metrics"
        CkBoxSaveWindowMetrics.UseVisualStyleBackColor = True
        ' 
        ' CkBoxSuspendOnSessionChange
        ' 
        CkBoxSuspendOnSessionChange.AutoSize = True
        CkBoxSuspendOnSessionChange.FlatStyle = FlatStyle.Flat
        CkBoxSuspendOnSessionChange.Font = New Font("Segoe UI", 12F)
        TipOptions.SetImage(CkBoxSuspendOnSessionChange, Nothing)
        TipError.SetImage(CkBoxSuspendOnSessionChange, Nothing)
        CkBoxSuspendOnSessionChange.Location = New Point(13, 203)
        CkBoxSuspendOnSessionChange.Name = "CkBoxSuspendOnSessionChange"
        CkBoxSuspendOnSessionChange.Size = New Size(234, 25)
        CkBoxSuspendOnSessionChange.TabIndex = 22
        TipError.SetText(CkBoxSuspendOnSessionChange, Nothing)
        TipOptions.SetText(CkBoxSuspendOnSessionChange, "Stop play and Minimize the window if the screen is locked or screensaver is activated.")
        CkBoxSuspendOnSessionChange.Text = "Minimize App On Screen Lock"
        CkBoxSuspendOnSessionChange.UseVisualStyleBackColor = True
        ' 
        ' BtnHelperApp1
        ' 
        TipError.SetImage(BtnHelperApp1, Nothing)
        BtnHelperApp1.Image = My.Resources.Resources.ImageGetPath16
        TipOptions.SetImage(BtnHelperApp1, Nothing)
        BtnHelperApp1.Location = New Point(773, 81)
        BtnHelperApp1.Name = "BtnHelperApp1"
        BtnHelperApp1.Size = New Size(32, 31)
        BtnHelperApp1.TabIndex = 64
        TipOptions.SetText(BtnHelperApp1, "Select a Helper App.")
        TipError.SetText(BtnHelperApp1, Nothing)
        BtnHelperApp1.UseVisualStyleBackColor = True
        ' 
        ' BtnHelperApp2
        ' 
        TipError.SetImage(BtnHelperApp2, Nothing)
        BtnHelperApp2.Image = My.Resources.Resources.ImageGetPath16
        TipOptions.SetImage(BtnHelperApp2, Nothing)
        BtnHelperApp2.Location = New Point(773, 199)
        BtnHelperApp2.Name = "BtnHelperApp2"
        BtnHelperApp2.Size = New Size(32, 31)
        BtnHelperApp2.TabIndex = 70
        TipOptions.SetText(BtnHelperApp2, "Select a Helper App.")
        TipError.SetText(BtnHelperApp2, Nothing)
        BtnHelperApp2.UseVisualStyleBackColor = True
        ' 
        ' LblSongPlayMode
        ' 
        LblSongPlayMode.AutoSize = True
        LblSongPlayMode.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblSongPlayMode, Nothing)
        TipOptions.SetImage(LblSongPlayMode, Nothing)
        LblSongPlayMode.Location = New Point(14, 136)
        LblSongPlayMode.Name = "LblSongPlayMode"
        LblSongPlayMode.Size = New Size(123, 21)
        LblSongPlayMode.TabIndex = 131
        TipOptions.SetText(LblSongPlayMode, Nothing)
        LblSongPlayMode.Text = "Song Play Mode"
        TipError.SetText(LblSongPlayMode, Nothing)
        ' 
        ' LblHelperApp1Name
        ' 
        LblHelperApp1Name.AutoSize = True
        LblHelperApp1Name.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblHelperApp1Name, Nothing)
        TipOptions.SetImage(LblHelperApp1Name, Nothing)
        LblHelperApp1Name.Location = New Point(353, 10)
        LblHelperApp1Name.Name = "LblHelperApp1Name"
        LblHelperApp1Name.Size = New Size(165, 21)
        LblHelperApp1Name.TabIndex = 132
        TipOptions.SetText(LblHelperApp1Name, Nothing)
        LblHelperApp1Name.Text = "Name of Helper App 1"
        TipError.SetText(LblHelperApp1Name, Nothing)
        ' 
        ' CoBoxPlaylistDefaultAction
        ' 
        CoBoxPlaylistDefaultAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlaylistDefaultAction.FlatStyle = FlatStyle.Flat
        CoBoxPlaylistDefaultAction.Font = New Font("Segoe UI", 12F)
        CoBoxPlaylistDefaultAction.FormattingEnabled = True
        TipOptions.SetImage(CoBoxPlaylistDefaultAction, Nothing)
        TipError.SetImage(CoBoxPlaylistDefaultAction, Nothing)
        CoBoxPlaylistDefaultAction.Location = New Point(10, 282)
        CoBoxPlaylistDefaultAction.Name = "CoBoxPlaylistDefaultAction"
        CoBoxPlaylistDefaultAction.Size = New Size(178, 29)
        CoBoxPlaylistDefaultAction.TabIndex = 250
        TipOptions.SetText(CoBoxPlaylistDefaultAction, "Choose what happens when you double-click a song in the Playlist or the Library.")
        TipError.SetText(CoBoxPlaylistDefaultAction, Nothing)
        ' 
        ' CoBoxPlaylistSearchAction
        ' 
        CoBoxPlaylistSearchAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlaylistSearchAction.FlatStyle = FlatStyle.Flat
        CoBoxPlaylistSearchAction.Font = New Font("Segoe UI", 12F)
        CoBoxPlaylistSearchAction.FormattingEnabled = True
        TipOptions.SetImage(CoBoxPlaylistSearchAction, Nothing)
        TipError.SetImage(CoBoxPlaylistSearchAction, Nothing)
        CoBoxPlaylistSearchAction.Location = New Point(10, 371)
        CoBoxPlaylistSearchAction.Name = "CoBoxPlaylistSearchAction"
        CoBoxPlaylistSearchAction.Size = New Size(178, 29)
        CoBoxPlaylistSearchAction.TabIndex = 255
        TipOptions.SetText(CoBoxPlaylistSearchAction, "Choose what happens when you select an item in the search box.")
        TipError.SetText(CoBoxPlaylistSearchAction, Nothing)
        ' 
        ' LblDefaultPlaylistAction
        ' 
        LblDefaultPlaylistAction.AutoSize = True
        LblDefaultPlaylistAction.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblDefaultPlaylistAction, Nothing)
        TipOptions.SetImage(LblDefaultPlaylistAction, Nothing)
        LblDefaultPlaylistAction.Location = New Point(10, 260)
        LblDefaultPlaylistAction.Name = "LblDefaultPlaylistAction"
        LblDefaultPlaylistAction.Size = New Size(161, 21)
        LblDefaultPlaylistAction.TabIndex = 138
        TipOptions.SetText(LblDefaultPlaylistAction, Nothing)
        LblDefaultPlaylistAction.Text = "Default Playlist Action"
        TipError.SetText(LblDefaultPlaylistAction, Nothing)
        ' 
        ' LblPlaylistSearchAction
        ' 
        LblPlaylistSearchAction.AutoSize = True
        LblPlaylistSearchAction.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblPlaylistSearchAction, Nothing)
        TipOptions.SetImage(LblPlaylistSearchAction, Nothing)
        LblPlaylistSearchAction.Location = New Point(10, 349)
        LblPlaylistSearchAction.Name = "LblPlaylistSearchAction"
        LblPlaylistSearchAction.Size = New Size(158, 21)
        LblPlaylistSearchAction.TabIndex = 139
        TipOptions.SetText(LblPlaylistSearchAction, Nothing)
        LblPlaylistSearchAction.Text = "Playlist Search Action"
        TipError.SetText(LblPlaylistSearchAction, Nothing)
        ' 
        ' CoBoxTheme
        ' 
        CoBoxTheme.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxTheme.FlatStyle = FlatStyle.Flat
        CoBoxTheme.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CoBoxTheme.FormattingEnabled = True
        TipOptions.SetImage(CoBoxTheme, Nothing)
        TipError.SetImage(CoBoxTheme, Nothing)
        CoBoxTheme.Location = New Point(13, 31)
        CoBoxTheme.Name = "CoBoxTheme"
        CoBoxTheme.Size = New Size(196, 33)
        CoBoxTheme.TabIndex = 10
        TipOptions.SetText(CoBoxTheme, "Choose a color theme.")
        TipError.SetText(CoBoxTheme, Nothing)
        ' 
        ' LblTheme
        ' 
        LblTheme.AutoSize = True
        LblTheme.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipError.SetImage(LblTheme, Nothing)
        TipOptions.SetImage(LblTheme, Nothing)
        LblTheme.Location = New Point(13, 12)
        LblTheme.Name = "LblTheme"
        LblTheme.Size = New Size(57, 21)
        LblTheme.TabIndex = 141
        TipOptions.SetText(LblTheme, Nothing)
        LblTheme.Text = "Theme"
        TipError.SetText(LblTheme, Nothing)
        ' 
        ' TxtBoxHistoryUpdateInterval
        ' 
        TxtBoxHistoryUpdateInterval.ContextMenuStrip = CMTxtBox
        TxtBoxHistoryUpdateInterval.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipError.SetImage(TxtBoxHistoryUpdateInterval, Nothing)
        TipOptions.SetImage(TxtBoxHistoryUpdateInterval, Nothing)
        TxtBoxHistoryUpdateInterval.Location = New Point(269, 229)
        TxtBoxHistoryUpdateInterval.Name = "TxtBoxHistoryUpdateInterval"
        TxtBoxHistoryUpdateInterval.ShortcutsEnabled = False
        TxtBoxHistoryUpdateInterval.Size = New Size(44, 29)
        TxtBoxHistoryUpdateInterval.TabIndex = 148
        TipOptions.SetText(TxtBoxHistoryUpdateInterval, "Update Song History after 1-60 seconds, or 0 for immediate update.")
        TipError.SetText(TxtBoxHistoryUpdateInterval, Nothing)
        TxtBoxHistoryUpdateInterval.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblPlaylistFormatting
        ' 
        LblPlaylistFormatting.AutoSize = True
        LblPlaylistFormatting.Font = New Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        TipError.SetImage(LblPlaylistFormatting, Nothing)
        TipOptions.SetImage(LblPlaylistFormatting, Nothing)
        LblPlaylistFormatting.Location = New Point(10, 10)
        LblPlaylistFormatting.Name = "LblPlaylistFormatting"
        LblPlaylistFormatting.Size = New Size(140, 21)
        LblPlaylistFormatting.TabIndex = 140
        TipError.SetText(LblPlaylistFormatting, Nothing)
        LblPlaylistFormatting.Text = "Playlist Formatting"
        TipOptions.SetText(LblPlaylistFormatting, "Choose the formatting options for listing songs in the Playlist.")
        ' 
        ' TxtBoxHistoryAutoSaveInterval
        ' 
        TxtBoxHistoryAutoSaveInterval.ContextMenuStrip = CMTxtBox
        TxtBoxHistoryAutoSaveInterval.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipError.SetImage(TxtBoxHistoryAutoSaveInterval, Nothing)
        TipOptions.SetImage(TxtBoxHistoryAutoSaveInterval, Nothing)
        TxtBoxHistoryAutoSaveInterval.Location = New Point(148, 285)
        TxtBoxHistoryAutoSaveInterval.Name = "TxtBoxHistoryAutoSaveInterval"
        TxtBoxHistoryAutoSaveInterval.ShortcutsEnabled = False
        TxtBoxHistoryAutoSaveInterval.Size = New Size(61, 29)
        TxtBoxHistoryAutoSaveInterval.TabIndex = 42
        TipOptions.SetText(TxtBoxHistoryAutoSaveInterval, "Auto save the song history every 1-1440 minutes.")
        TipError.SetText(TxtBoxHistoryAutoSaveInterval, Nothing)
        TxtBoxHistoryAutoSaveInterval.TextAlign = HorizontalAlignment.Center
        ' 
        ' BtnHistorySaveNow
        ' 
        TipError.SetImage(BtnHistorySaveNow, Nothing)
        BtnHistorySaveNow.Image = My.Resources.Resources.ImageSave32
        TipOptions.SetImage(BtnHistorySaveNow, My.Resources.Resources.ImageSave32)
        BtnHistorySaveNow.ImageAlign = ContentAlignment.MiddleLeft
        BtnHistorySaveNow.Location = New Point(267, 277)
        BtnHistorySaveNow.Name = "BtnHistorySaveNow"
        BtnHistorySaveNow.Size = New Size(120, 40)
        BtnHistorySaveNow.TabIndex = 44
        TipOptions.SetText(BtnHistorySaveNow, "Save the song history now.")
        TipError.SetText(BtnHistorySaveNow, Nothing)
        BtnHistorySaveNow.Text = "Save Now"
        BtnHistorySaveNow.TextAlign = ContentAlignment.MiddleRight
        BtnHistorySaveNow.UseVisualStyleBackColor = True
        ' 
        ' BtnHistoryPrune
        ' 
        TipError.SetImage(BtnHistoryPrune, Nothing)
        BtnHistoryPrune.Image = My.Resources.Resources.ImagePrune32
        TipOptions.SetImage(BtnHistoryPrune, My.Resources.Resources.ImagePrune32)
        BtnHistoryPrune.ImageAlign = ContentAlignment.MiddleLeft
        BtnHistoryPrune.Location = New Point(587, 277)
        BtnHistoryPrune.Name = "BtnHistoryPrune"
        BtnHistoryPrune.Size = New Size(218, 40)
        BtnHistoryPrune.TabIndex = 80
        TipOptions.SetText(BtnHistoryPrune, resources.GetString("BtnHistoryPrune.Text"))
        TipError.SetText(BtnHistoryPrune, Nothing)
        BtnHistoryPrune.Text = "Prune History"
        BtnHistoryPrune.TextAlign = ContentAlignment.MiddleRight
        BtnHistoryPrune.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxHelperApp2Path
        ' 
        TxtBoxHelperApp2Path.ContextMenuStrip = CMTxtBox
        TxtBoxHelperApp2Path.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(TxtBoxHelperApp2Path, Nothing)
        TipOptions.SetImage(TxtBoxHelperApp2Path, Nothing)
        TxtBoxHelperApp2Path.Location = New Point(353, 200)
        TxtBoxHelperApp2Path.Name = "TxtBoxHelperApp2Path"
        TxtBoxHelperApp2Path.ShortcutsEnabled = False
        TxtBoxHelperApp2Path.Size = New Size(420, 29)
        TxtBoxHelperApp2Path.TabIndex = 68
        TipOptions.SetText(TxtBoxHelperApp2Path, "Enter or Select the path to your Helper App.")
        TipError.SetText(TxtBoxHelperApp2Path, Nothing)
        ' 
        ' TxtBoxHelperApp2Name
        ' 
        TxtBoxHelperApp2Name.ContextMenuStrip = CMTxtBox
        TxtBoxHelperApp2Name.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(TxtBoxHelperApp2Name, Nothing)
        TipOptions.SetImage(TxtBoxHelperApp2Name, Nothing)
        TxtBoxHelperApp2Name.Location = New Point(353, 150)
        TxtBoxHelperApp2Name.Name = "TxtBoxHelperApp2Name"
        TxtBoxHelperApp2Name.ShortcutsEnabled = False
        TxtBoxHelperApp2Name.Size = New Size(202, 29)
        TxtBoxHelperApp2Name.TabIndex = 66
        TipOptions.SetText(TxtBoxHelperApp2Name, "Enter a name for your Helper App.")
        TipError.SetText(TxtBoxHelperApp2Name, Nothing)
        ' 
        ' TxtBoxHelperApp1Path
        ' 
        TxtBoxHelperApp1Path.ContextMenuStrip = CMTxtBox
        TxtBoxHelperApp1Path.Font = New Font("Segoe UI", 12F)
        TxtBoxHelperApp1Path.ForeColor = Color.White
        TipError.SetImage(TxtBoxHelperApp1Path, Nothing)
        TipOptions.SetImage(TxtBoxHelperApp1Path, Nothing)
        TxtBoxHelperApp1Path.Location = New Point(353, 82)
        TxtBoxHelperApp1Path.Name = "TxtBoxHelperApp1Path"
        TxtBoxHelperApp1Path.ShortcutsEnabled = False
        TxtBoxHelperApp1Path.Size = New Size(420, 29)
        TxtBoxHelperApp1Path.TabIndex = 62
        TipOptions.SetText(TxtBoxHelperApp1Path, "Enter or Select the path to your Helper App.")
        TipError.SetText(TxtBoxHelperApp1Path, Nothing)
        ' 
        ' TxtBoxHelperApp1Name
        ' 
        TxtBoxHelperApp1Name.ContextMenuStrip = CMTxtBox
        TxtBoxHelperApp1Name.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(TxtBoxHelperApp1Name, Nothing)
        TipOptions.SetImage(TxtBoxHelperApp1Name, Nothing)
        TxtBoxHelperApp1Name.Location = New Point(353, 31)
        TxtBoxHelperApp1Name.Name = "TxtBoxHelperApp1Name"
        TxtBoxHelperApp1Name.ShortcutsEnabled = False
        TxtBoxHelperApp1Name.Size = New Size(202, 29)
        TxtBoxHelperApp1Name.TabIndex = 60
        TipOptions.SetText(TxtBoxHelperApp1Name, "Enter a name for your Helper App.")
        TipError.SetText(TxtBoxHelperApp1Name, Nothing)
        ' 
        ' TxtBoxRandomHistoryUpdateInterval
        ' 
        TxtBoxRandomHistoryUpdateInterval.ContextMenuStrip = CMTxtBox
        TxtBoxRandomHistoryUpdateInterval.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipError.SetImage(TxtBoxRandomHistoryUpdateInterval, Nothing)
        TipOptions.SetImage(TxtBoxRandomHistoryUpdateInterval, Nothing)
        TxtBoxRandomHistoryUpdateInterval.Location = New Point(355, 293)
        TxtBoxRandomHistoryUpdateInterval.Name = "TxtBoxRandomHistoryUpdateInterval"
        TxtBoxRandomHistoryUpdateInterval.ShortcutsEnabled = False
        TxtBoxRandomHistoryUpdateInterval.Size = New Size(44, 29)
        TxtBoxRandomHistoryUpdateInterval.TabIndex = 151
        TipOptions.SetText(TxtBoxRandomHistoryUpdateInterval, resources.GetString("TxtBoxRandomHistoryUpdateInterval.Text"))
        TipError.SetText(TxtBoxRandomHistoryUpdateInterval, Nothing)
        TxtBoxRandomHistoryUpdateInterval.TextAlign = HorizontalAlignment.Center
        ' 
        ' BtnPrunePlaylist
        ' 
        TipError.SetImage(BtnPrunePlaylist, Nothing)
        BtnPrunePlaylist.Image = My.Resources.Resources.ImagePrune32
        TipOptions.SetImage(BtnPrunePlaylist, Nothing)
        BtnPrunePlaylist.ImageAlign = ContentAlignment.MiddleLeft
        BtnPrunePlaylist.Location = New Point(583, 360)
        BtnPrunePlaylist.Name = "BtnPrunePlaylist"
        BtnPrunePlaylist.Size = New Size(218, 40)
        BtnPrunePlaylist.TabIndex = 1000
        TipOptions.SetText(BtnPrunePlaylist, "Prune the Playlist." & vbCrLf & "This will remove any playlist entries that cannot be found in storage, while preserving streams." & vbCrLf & "The total number of playlist entries is given in parentheses.")
        TipError.SetText(BtnPrunePlaylist, Nothing)
        BtnPrunePlaylist.Text = "Prune Playlist"
        BtnPrunePlaylist.TextAlign = ContentAlignment.MiddleRight
        BtnPrunePlaylist.UseVisualStyleBackColor = True
        ' 
        ' LblHistoryUpdateInterval2
        ' 
        LblHistoryUpdateInterval2.AutoSize = True
        LblHistoryUpdateInterval2.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblHistoryUpdateInterval2, Nothing)
        TipOptions.SetImage(LblHistoryUpdateInterval2, Nothing)
        LblHistoryUpdateInterval2.Location = New Point(311, 232)
        LblHistoryUpdateInterval2.Name = "LblHistoryUpdateInterval2"
        LblHistoryUpdateInterval2.Size = New Size(68, 21)
        LblHistoryUpdateInterval2.TabIndex = 150
        TipOptions.SetText(LblHistoryUpdateInterval2, Nothing)
        LblHistoryUpdateInterval2.Text = "Seconds"
        TipError.SetText(LblHistoryUpdateInterval2, Nothing)
        LblHistoryUpdateInterval2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblHistoryUpdateInterval1
        ' 
        LblHistoryUpdateInterval1.AutoSize = True
        LblHistoryUpdateInterval1.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblHistoryUpdateInterval1, Nothing)
        TipOptions.SetImage(LblHistoryUpdateInterval1, Nothing)
        LblHistoryUpdateInterval1.Location = New Point(14, 233)
        LblHistoryUpdateInterval1.Name = "LblHistoryUpdateInterval1"
        LblHistoryUpdateInterval1.Size = New Size(259, 21)
        LblHistoryUpdateInterval1.TabIndex = 149
        TipOptions.SetText(LblHistoryUpdateInterval1, Nothing)
        LblHistoryUpdateInterval1.Text = "Update History After Song Plays For"
        TipError.SetText(LblHistoryUpdateInterval1, Nothing)
        LblHistoryUpdateInterval1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblHistoryAutoSaveInterval1
        ' 
        LblHistoryAutoSaveInterval1.AutoSize = True
        LblHistoryAutoSaveInterval1.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblHistoryAutoSaveInterval1, Nothing)
        TipOptions.SetImage(LblHistoryAutoSaveInterval1, Nothing)
        LblHistoryAutoSaveInterval1.Location = New Point(13, 288)
        LblHistoryAutoSaveInterval1.Name = "LblHistoryAutoSaveInterval1"
        LblHistoryAutoSaveInterval1.Size = New Size(139, 21)
        LblHistoryAutoSaveInterval1.TabIndex = 143
        TipOptions.SetText(LblHistoryAutoSaveInterval1, Nothing)
        LblHistoryAutoSaveInterval1.Text = "Save History Every"
        TipError.SetText(LblHistoryAutoSaveInterval1, Nothing)
        LblHistoryAutoSaveInterval1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblHistoryAutoSaveInterval2
        ' 
        LblHistoryAutoSaveInterval2.AutoSize = True
        LblHistoryAutoSaveInterval2.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblHistoryAutoSaveInterval2, Nothing)
        TipOptions.SetImage(LblHistoryAutoSaveInterval2, Nothing)
        LblHistoryAutoSaveInterval2.Location = New Point(206, 288)
        LblHistoryAutoSaveInterval2.Name = "LblHistoryAutoSaveInterval2"
        LblHistoryAutoSaveInterval2.Size = New Size(66, 21)
        LblHistoryAutoSaveInterval2.TabIndex = 144
        TipOptions.SetText(LblHistoryAutoSaveInterval2, Nothing)
        LblHistoryAutoSaveInterval2.Text = "Minutes"
        TipError.SetText(LblHistoryAutoSaveInterval2, Nothing)
        LblHistoryAutoSaveInterval2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TCOptions
        ' 
        TCOptions.ActiveTabFont = New Font("Segoe UI", 14.25F, FontStyle.Bold)
        TCOptions.BeforeTouchSize = New Size(818, 457)
        TCOptions.Controls.Add(TPApp)
        TCOptions.Controls.Add(TPPlayer)
        TCOptions.Controls.Add(TPVisualizers)
        TCOptions.Controls.Add(TPPlaylist)
        TCOptions.Controls.Add(TPLibrary)
        TCOptions.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipError.SetImage(TCOptions, Nothing)
        TipOptions.SetImage(TCOptions, Nothing)
        TCOptions.Location = New Point(12, 12)
        TCOptions.Name = "TCOptions"
        TCOptions.Size = New Size(818, 457)
        TCOptions.TabIndex = 148
        TCOptions.TabStop = False
        TCOptions.TabStyle = GetType(Syncfusion.Windows.Forms.Tools.TabRendererWhidbey)
        TipOptions.SetText(TCOptions, Nothing)
        TipError.SetText(TCOptions, Nothing)
        TCOptions.ThemeName = "TabRendererWhidbey"
        ' 
        ' TPApp
        ' 
        TPApp.BorderStyle = BorderStyle.Fixed3D
        TPApp.Controls.Add(TxtBoxCompanionServerPort)
        TPApp.Controls.Add(CkBoxEnableCompanionServer)
        TPApp.Controls.Add(CkBoxShowTrayIcon)
        TPApp.Controls.Add(CkBoxMinimizeToTray)
        TPApp.Controls.Add(CkBoxSaveWindowMetrics)
        TPApp.Controls.Add(BtnHelperApp2)
        TPApp.Controls.Add(TxtBoxHelperApp2Path)
        TPApp.Controls.Add(CkBoxSuspendOnSessionChange)
        TPApp.Controls.Add(TxtBoxHelperApp2Name)
        TPApp.Controls.Add(BtnHelperApp1)
        TPApp.Controls.Add(TxtBoxHelperApp1Path)
        TPApp.Controls.Add(TxtBoxHelperApp1Name)
        TPApp.Controls.Add(CoBoxTheme)
        TPApp.Controls.Add(BtnHistoryPrune)
        TPApp.Controls.Add(LblHelperApp2Path)
        TPApp.Controls.Add(TxtBoxHistoryAutoSaveInterval)
        TPApp.Controls.Add(LblHelperApp2Name)
        TPApp.Controls.Add(BtnHistorySaveNow)
        TPApp.Controls.Add(LblHelperApp1Path)
        TPApp.Controls.Add(LblHelperApp1Name)
        TPApp.Controls.Add(LblHistoryAutoSaveInterval1)
        TPApp.Controls.Add(LblHistoryAutoSaveInterval2)
        TPApp.Controls.Add(LblTheme)
        TPApp.Controls.Add(LblCompanionServerPort)
        TipError.SetImage(TPApp, Nothing)
        TipOptions.SetImage(TPApp, Nothing)
        TPApp.Image = Nothing
        TPApp.ImageSize = New Size(16, 16)
        TPApp.Location = New Point(1, 42)
        TPApp.Name = "TPApp"
        TPApp.ShowCloseButton = True
        TPApp.Size = New Size(815, 413)
        TPApp.TabIndex = 1
        TipError.SetText(TPApp, Nothing)
        TipOptions.SetText(TPApp, Nothing)
        TPApp.Text = " App "
        TPApp.ThemesEnabled = False
        ' 
        ' TxtBoxCompanionServerPort
        ' 
        TipError.SetImage(TxtBoxCompanionServerPort, Nothing)
        TipOptions.SetImage(TxtBoxCompanionServerPort, Nothing)
        TxtBoxCompanionServerPort.Location = New Point(744, 374)
        TxtBoxCompanionServerPort.Name = "TxtBoxCompanionServerPort"
        TxtBoxCompanionServerPort.ShortcutsEnabled = False
        TxtBoxCompanionServerPort.Size = New Size(61, 29)
        TxtBoxCompanionServerPort.TabIndex = 99
        TipError.SetText(TxtBoxCompanionServerPort, Nothing)
        TipOptions.SetText(TxtBoxCompanionServerPort, "Companion App Server Port Number")
        TxtBoxCompanionServerPort.TextAlign = HorizontalAlignment.Center
        ' 
        ' CkBoxEnableCompanionServer
        ' 
        CkBoxEnableCompanionServer.AutoSize = True
        CkBoxEnableCompanionServer.FlatStyle = FlatStyle.Flat
        CkBoxEnableCompanionServer.Font = New Font("Segoe UI", 12F)
        TipOptions.SetImage(CkBoxEnableCompanionServer, Nothing)
        TipError.SetImage(CkBoxEnableCompanionServer, Nothing)
        CkBoxEnableCompanionServer.Location = New Point(496, 376)
        CkBoxEnableCompanionServer.Name = "CkBoxEnableCompanionServer"
        CkBoxEnableCompanionServer.Size = New Size(206, 25)
        CkBoxEnableCompanionServer.TabIndex = 98
        TipError.SetText(CkBoxEnableCompanionServer, Nothing)
        TipOptions.SetText(CkBoxEnableCompanionServer, "Set In Code")
        CkBoxEnableCompanionServer.Text = "Enable Companion Server"
        CkBoxEnableCompanionServer.UseVisualStyleBackColor = True
        ' 
        ' CkBoxShowTrayIcon
        ' 
        CkBoxShowTrayIcon.AutoSize = True
        CkBoxShowTrayIcon.FlatStyle = FlatStyle.Flat
        CkBoxShowTrayIcon.Font = New Font("Segoe UI", 12F)
        TipOptions.SetImage(CkBoxShowTrayIcon, Nothing)
        TipError.SetImage(CkBoxShowTrayIcon, Nothing)
        CkBoxShowTrayIcon.Location = New Point(13, 105)
        CkBoxShowTrayIcon.Name = "CkBoxShowTrayIcon"
        CkBoxShowTrayIcon.Size = New Size(197, 25)
        CkBoxShowTrayIcon.TabIndex = 145
        TipError.SetText(CkBoxShowTrayIcon, Nothing)
        TipOptions.SetText(CkBoxShowTrayIcon, "Auto save window sizes and locations.")
        CkBoxShowTrayIcon.Text = "Show Skye Music In Tray"
        CkBoxShowTrayIcon.UseVisualStyleBackColor = True
        ' 
        ' CkBoxMinimizeToTray
        ' 
        CkBoxMinimizeToTray.AutoSize = True
        CkBoxMinimizeToTray.FlatStyle = FlatStyle.Flat
        CkBoxMinimizeToTray.Font = New Font("Segoe UI", 12F)
        TipOptions.SetImage(CkBoxMinimizeToTray, Nothing)
        TipError.SetImage(CkBoxMinimizeToTray, Nothing)
        CkBoxMinimizeToTray.Location = New Point(13, 127)
        CkBoxMinimizeToTray.Name = "CkBoxMinimizeToTray"
        CkBoxMinimizeToTray.Size = New Size(189, 25)
        CkBoxMinimizeToTray.TabIndex = 146
        TipError.SetText(CkBoxMinimizeToTray, Nothing)
        TipOptions.SetText(CkBoxMinimizeToTray, "Stop play and Minimize the window if the screen is locked or screensaver is activated.")
        CkBoxMinimizeToTray.Text = "Minimize Player To Tray"
        CkBoxMinimizeToTray.UseVisualStyleBackColor = True
        ' 
        ' LblHelperApp2Path
        ' 
        LblHelperApp2Path.AutoSize = True
        LblHelperApp2Path.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblHelperApp2Path, Nothing)
        TipOptions.SetImage(LblHelperApp2Path, Nothing)
        LblHelperApp2Path.Location = New Point(353, 179)
        LblHelperApp2Path.Name = "LblHelperApp2Path"
        LblHelperApp2Path.Size = New Size(153, 21)
        LblHelperApp2Path.TabIndex = 135
        TipOptions.SetText(LblHelperApp2Path, Nothing)
        LblHelperApp2Path.Text = "Path to Helper App 2"
        TipError.SetText(LblHelperApp2Path, Nothing)
        ' 
        ' LblHelperApp2Name
        ' 
        LblHelperApp2Name.AutoSize = True
        LblHelperApp2Name.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblHelperApp2Name, Nothing)
        TipOptions.SetImage(LblHelperApp2Name, Nothing)
        LblHelperApp2Name.Location = New Point(353, 129)
        LblHelperApp2Name.Name = "LblHelperApp2Name"
        LblHelperApp2Name.Size = New Size(165, 21)
        LblHelperApp2Name.TabIndex = 134
        TipOptions.SetText(LblHelperApp2Name, Nothing)
        LblHelperApp2Name.Text = "Name of Helper App 2"
        TipError.SetText(LblHelperApp2Name, Nothing)
        ' 
        ' LblHelperApp1Path
        ' 
        LblHelperApp1Path.AutoSize = True
        LblHelperApp1Path.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblHelperApp1Path, Nothing)
        TipOptions.SetImage(LblHelperApp1Path, Nothing)
        LblHelperApp1Path.Location = New Point(353, 60)
        LblHelperApp1Path.Name = "LblHelperApp1Path"
        LblHelperApp1Path.Size = New Size(153, 21)
        LblHelperApp1Path.TabIndex = 133
        TipOptions.SetText(LblHelperApp1Path, Nothing)
        LblHelperApp1Path.Text = "Path to Helper App 1"
        TipError.SetText(LblHelperApp1Path, Nothing)
        ' 
        ' LblCompanionServerPort
        ' 
        LblCompanionServerPort.AutoSize = True
        LblCompanionServerPort.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblCompanionServerPort, Nothing)
        TipOptions.SetImage(LblCompanionServerPort, Nothing)
        LblCompanionServerPort.Location = New Point(709, 378)
        LblCompanionServerPort.Name = "LblCompanionServerPort"
        LblCompanionServerPort.Size = New Size(38, 21)
        LblCompanionServerPort.TabIndex = 148
        TipOptions.SetText(LblCompanionServerPort, Nothing)
        LblCompanionServerPort.Text = "Port"
        TipError.SetText(LblCompanionServerPort, Nothing)
        LblCompanionServerPort.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TPPlayer
        ' 
        TPPlayer.BorderStyle = BorderStyle.Fixed3D
        TPPlayer.Controls.Add(GrBoxShowNowPlayingToast)
        TPPlayer.Controls.Add(CkBoxShowNowPlayingToast)
        TPPlayer.Controls.Add(TxtBoxRandomHistoryUpdateInterval)
        TPPlayer.Controls.Add(TxtBoxHistoryUpdateInterval)
        TPPlayer.Controls.Add(LblHistoryUpdateInterval2)
        TPPlayer.Controls.Add(GrBoxTime)
        TPPlayer.Controls.Add(CoBoxPlayMode)
        TPPlayer.Controls.Add(LblHistoryUpdateInterval1)
        TPPlayer.Controls.Add(LblSongPlayMode)
        TPPlayer.Controls.Add(LblRandomHistoryUpdateInterval1)
        TPPlayer.Controls.Add(LblRandomHistoryUpdateInterval2)
        TipError.SetImage(TPPlayer, Nothing)
        TipOptions.SetImage(TPPlayer, Nothing)
        TPPlayer.Image = Nothing
        TPPlayer.ImageSize = New Size(16, 16)
        TPPlayer.Location = New Point(1, 42)
        TPPlayer.Name = "TPPlayer"
        TPPlayer.ShowCloseButton = True
        TPPlayer.Size = New Size(815, 413)
        TPPlayer.TabIndex = 2
        TipError.SetText(TPPlayer, Nothing)
        TipOptions.SetText(TPPlayer, Nothing)
        TPPlayer.Text = " Player "
        TPPlayer.ThemesEnabled = False
        ' 
        ' GrBoxShowNowPlayingToast
        ' 
        GrBoxShowNowPlayingToast.Controls.Add(RadBtnNPTBottomRight)
        GrBoxShowNowPlayingToast.Controls.Add(RadBtnNPTBottomCenter)
        GrBoxShowNowPlayingToast.Controls.Add(RadBtnNPTBottomLeft)
        GrBoxShowNowPlayingToast.Controls.Add(RadBtnNPTMiddleRight)
        GrBoxShowNowPlayingToast.Controls.Add(RadBtnNPTMiddleCenter)
        GrBoxShowNowPlayingToast.Controls.Add(RadBtnNPTMiddleLeft)
        GrBoxShowNowPlayingToast.Controls.Add(RadBtnNPTTopRight)
        GrBoxShowNowPlayingToast.Controls.Add(RadBtnNPTTopCenter)
        GrBoxShowNowPlayingToast.Controls.Add(RadBtnNPTTopLeft)
        GrBoxShowNowPlayingToast.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipError.SetImage(GrBoxShowNowPlayingToast, Nothing)
        TipOptions.SetImage(GrBoxShowNowPlayingToast, Nothing)
        GrBoxShowNowPlayingToast.Location = New Point(606, 35)
        GrBoxShowNowPlayingToast.Name = "GrBoxShowNowPlayingToast"
        GrBoxShowNowPlayingToast.RightToLeft = RightToLeft.Yes
        GrBoxShowNowPlayingToast.Size = New Size(196, 111)
        GrBoxShowNowPlayingToast.TabIndex = 155
        GrBoxShowNowPlayingToast.TabStop = False
        TipError.SetText(GrBoxShowNowPlayingToast, Nothing)
        TipOptions.SetText(GrBoxShowNowPlayingToast, "Select the Location for the Now Playing Toast.")
        GrBoxShowNowPlayingToast.Text = "Location"
        ' 
        ' RadBtnNPTBottomRight
        ' 
        RadBtnNPTBottomRight.AutoSize = True
        TipOptions.SetImage(RadBtnNPTBottomRight, Nothing)
        TipError.SetImage(RadBtnNPTBottomRight, Nothing)
        RadBtnNPTBottomRight.Location = New Point(171, 87)
        RadBtnNPTBottomRight.Name = "RadBtnNPTBottomRight"
        RadBtnNPTBottomRight.Size = New Size(14, 13)
        RadBtnNPTBottomRight.TabIndex = 8
        RadBtnNPTBottomRight.TabStop = True
        TipError.SetText(RadBtnNPTBottomRight, Nothing)
        TipOptions.SetText(RadBtnNPTBottomRight, Nothing)
        RadBtnNPTBottomRight.UseVisualStyleBackColor = True
        ' 
        ' RadBtnNPTBottomCenter
        ' 
        RadBtnNPTBottomCenter.AutoSize = True
        TipOptions.SetImage(RadBtnNPTBottomCenter, Nothing)
        TipError.SetImage(RadBtnNPTBottomCenter, Nothing)
        RadBtnNPTBottomCenter.Location = New Point(89, 87)
        RadBtnNPTBottomCenter.Name = "RadBtnNPTBottomCenter"
        RadBtnNPTBottomCenter.Size = New Size(14, 13)
        RadBtnNPTBottomCenter.TabIndex = 7
        RadBtnNPTBottomCenter.TabStop = True
        TipError.SetText(RadBtnNPTBottomCenter, Nothing)
        TipOptions.SetText(RadBtnNPTBottomCenter, Nothing)
        RadBtnNPTBottomCenter.UseVisualStyleBackColor = True
        ' 
        ' RadBtnNPTBottomLeft
        ' 
        RadBtnNPTBottomLeft.AutoSize = True
        TipOptions.SetImage(RadBtnNPTBottomLeft, Nothing)
        TipError.SetImage(RadBtnNPTBottomLeft, Nothing)
        RadBtnNPTBottomLeft.Location = New Point(10, 87)
        RadBtnNPTBottomLeft.Name = "RadBtnNPTBottomLeft"
        RadBtnNPTBottomLeft.Size = New Size(14, 13)
        RadBtnNPTBottomLeft.TabIndex = 6
        RadBtnNPTBottomLeft.TabStop = True
        TipError.SetText(RadBtnNPTBottomLeft, Nothing)
        TipOptions.SetText(RadBtnNPTBottomLeft, Nothing)
        RadBtnNPTBottomLeft.UseVisualStyleBackColor = True
        ' 
        ' RadBtnNPTMiddleRight
        ' 
        RadBtnNPTMiddleRight.AutoSize = True
        TipOptions.SetImage(RadBtnNPTMiddleRight, Nothing)
        TipError.SetImage(RadBtnNPTMiddleRight, Nothing)
        RadBtnNPTMiddleRight.Location = New Point(171, 55)
        RadBtnNPTMiddleRight.Name = "RadBtnNPTMiddleRight"
        RadBtnNPTMiddleRight.Size = New Size(14, 13)
        RadBtnNPTMiddleRight.TabIndex = 5
        RadBtnNPTMiddleRight.TabStop = True
        TipError.SetText(RadBtnNPTMiddleRight, Nothing)
        TipOptions.SetText(RadBtnNPTMiddleRight, Nothing)
        RadBtnNPTMiddleRight.UseVisualStyleBackColor = True
        ' 
        ' RadBtnNPTMiddleCenter
        ' 
        RadBtnNPTMiddleCenter.AutoSize = True
        TipOptions.SetImage(RadBtnNPTMiddleCenter, Nothing)
        TipError.SetImage(RadBtnNPTMiddleCenter, Nothing)
        RadBtnNPTMiddleCenter.Location = New Point(89, 55)
        RadBtnNPTMiddleCenter.Name = "RadBtnNPTMiddleCenter"
        RadBtnNPTMiddleCenter.Size = New Size(14, 13)
        RadBtnNPTMiddleCenter.TabIndex = 4
        RadBtnNPTMiddleCenter.TabStop = True
        TipError.SetText(RadBtnNPTMiddleCenter, Nothing)
        TipOptions.SetText(RadBtnNPTMiddleCenter, Nothing)
        RadBtnNPTMiddleCenter.UseVisualStyleBackColor = True
        ' 
        ' RadBtnNPTMiddleLeft
        ' 
        RadBtnNPTMiddleLeft.AutoSize = True
        TipOptions.SetImage(RadBtnNPTMiddleLeft, Nothing)
        TipError.SetImage(RadBtnNPTMiddleLeft, Nothing)
        RadBtnNPTMiddleLeft.Location = New Point(10, 55)
        RadBtnNPTMiddleLeft.Name = "RadBtnNPTMiddleLeft"
        RadBtnNPTMiddleLeft.Size = New Size(14, 13)
        RadBtnNPTMiddleLeft.TabIndex = 3
        RadBtnNPTMiddleLeft.TabStop = True
        TipError.SetText(RadBtnNPTMiddleLeft, Nothing)
        TipOptions.SetText(RadBtnNPTMiddleLeft, Nothing)
        RadBtnNPTMiddleLeft.UseVisualStyleBackColor = True
        ' 
        ' RadBtnNPTTopRight
        ' 
        RadBtnNPTTopRight.AutoSize = True
        TipOptions.SetImage(RadBtnNPTTopRight, Nothing)
        TipError.SetImage(RadBtnNPTTopRight, Nothing)
        RadBtnNPTTopRight.Location = New Point(171, 24)
        RadBtnNPTTopRight.Name = "RadBtnNPTTopRight"
        RadBtnNPTTopRight.Size = New Size(14, 13)
        RadBtnNPTTopRight.TabIndex = 2
        RadBtnNPTTopRight.TabStop = True
        TipError.SetText(RadBtnNPTTopRight, Nothing)
        TipOptions.SetText(RadBtnNPTTopRight, Nothing)
        RadBtnNPTTopRight.UseVisualStyleBackColor = True
        ' 
        ' RadBtnNPTTopCenter
        ' 
        RadBtnNPTTopCenter.AutoSize = True
        TipOptions.SetImage(RadBtnNPTTopCenter, Nothing)
        TipError.SetImage(RadBtnNPTTopCenter, Nothing)
        RadBtnNPTTopCenter.Location = New Point(89, 24)
        RadBtnNPTTopCenter.Name = "RadBtnNPTTopCenter"
        RadBtnNPTTopCenter.Size = New Size(14, 13)
        RadBtnNPTTopCenter.TabIndex = 1
        RadBtnNPTTopCenter.TabStop = True
        TipError.SetText(RadBtnNPTTopCenter, Nothing)
        TipOptions.SetText(RadBtnNPTTopCenter, Nothing)
        RadBtnNPTTopCenter.UseVisualStyleBackColor = True
        ' 
        ' RadBtnNPTTopLeft
        ' 
        RadBtnNPTTopLeft.AutoSize = True
        TipOptions.SetImage(RadBtnNPTTopLeft, Nothing)
        TipError.SetImage(RadBtnNPTTopLeft, Nothing)
        RadBtnNPTTopLeft.Location = New Point(10, 24)
        RadBtnNPTTopLeft.Name = "RadBtnNPTTopLeft"
        RadBtnNPTTopLeft.Size = New Size(14, 13)
        RadBtnNPTTopLeft.TabIndex = 0
        RadBtnNPTTopLeft.TabStop = True
        TipError.SetText(RadBtnNPTTopLeft, Nothing)
        TipOptions.SetText(RadBtnNPTTopLeft, Nothing)
        RadBtnNPTTopLeft.UseVisualStyleBackColor = True
        ' 
        ' CkBoxShowNowPlayingToast
        ' 
        CkBoxShowNowPlayingToast.AutoSize = True
        CkBoxShowNowPlayingToast.FlatStyle = FlatStyle.Flat
        CkBoxShowNowPlayingToast.Font = New Font("Segoe UI", 12F)
        TipOptions.SetImage(CkBoxShowNowPlayingToast, Nothing)
        TipError.SetImage(CkBoxShowNowPlayingToast, Nothing)
        CkBoxShowNowPlayingToast.Location = New Point(606, 10)
        CkBoxShowNowPlayingToast.Name = "CkBoxShowNowPlayingToast"
        CkBoxShowNowPlayingToast.Size = New Size(196, 25)
        CkBoxShowNowPlayingToast.TabIndex = 154
        TipError.SetText(CkBoxShowNowPlayingToast, Nothing)
        TipOptions.SetText(CkBoxShowNowPlayingToast, "Show Now Playing Toast in the Specified Location.")
        CkBoxShowNowPlayingToast.Text = "Show Now Playing Toast"
        CkBoxShowNowPlayingToast.UseVisualStyleBackColor = True
        ' 
        ' LblRandomHistoryUpdateInterval1
        ' 
        LblRandomHistoryUpdateInterval1.AutoSize = True
        LblRandomHistoryUpdateInterval1.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblRandomHistoryUpdateInterval1, Nothing)
        TipOptions.SetImage(LblRandomHistoryUpdateInterval1, Nothing)
        LblRandomHistoryUpdateInterval1.Location = New Point(14, 296)
        LblRandomHistoryUpdateInterval1.Name = "LblRandomHistoryUpdateInterval1"
        LblRandomHistoryUpdateInterval1.Size = New Size(345, 21)
        LblRandomHistoryUpdateInterval1.TabIndex = 152
        TipOptions.SetText(LblRandomHistoryUpdateInterval1, Nothing)
        LblRandomHistoryUpdateInterval1.Text = "Update Shuffle Play History After Song Plays For"
        TipError.SetText(LblRandomHistoryUpdateInterval1, Nothing)
        LblRandomHistoryUpdateInterval1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblRandomHistoryUpdateInterval2
        ' 
        LblRandomHistoryUpdateInterval2.AutoSize = True
        LblRandomHistoryUpdateInterval2.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(LblRandomHistoryUpdateInterval2, Nothing)
        TipOptions.SetImage(LblRandomHistoryUpdateInterval2, Nothing)
        LblRandomHistoryUpdateInterval2.Location = New Point(397, 296)
        LblRandomHistoryUpdateInterval2.Name = "LblRandomHistoryUpdateInterval2"
        LblRandomHistoryUpdateInterval2.Size = New Size(68, 21)
        LblRandomHistoryUpdateInterval2.TabIndex = 153
        TipOptions.SetText(LblRandomHistoryUpdateInterval2, Nothing)
        LblRandomHistoryUpdateInterval2.Text = "Seconds"
        TipError.SetText(LblRandomHistoryUpdateInterval2, Nothing)
        LblRandomHistoryUpdateInterval2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TPVisualizers
        ' 
        TPVisualizers.Controls.Add(PanelVisualizers)
        TPVisualizers.Controls.Add(CoBoxVisualizers)
        TPVisualizers.Controls.Add(LblVisualizers)
        TipError.SetImage(TPVisualizers, Nothing)
        TipOptions.SetImage(TPVisualizers, Nothing)
        TPVisualizers.Image = Nothing
        TPVisualizers.ImageSize = New Size(16, 16)
        TPVisualizers.Location = New Point(1, 42)
        TPVisualizers.Name = "TPVisualizers"
        TPVisualizers.ShowCloseButton = True
        TPVisualizers.Size = New Size(815, 413)
        TPVisualizers.TabIndex = 5
        TipError.SetText(TPVisualizers, Nothing)
        TipOptions.SetText(TPVisualizers, Nothing)
        TPVisualizers.Text = " Visualizers "
        TPVisualizers.ThemesEnabled = False
        ' 
        ' PanelVisualizers
        ' 
        PanelVisualizers.Dock = DockStyle.Bottom
        TipOptions.SetImage(PanelVisualizers, Nothing)
        TipError.SetImage(PanelVisualizers, Nothing)
        PanelVisualizers.Location = New Point(0, 72)
        PanelVisualizers.Name = "PanelVisualizers"
        PanelVisualizers.Size = New Size(815, 341)
        PanelVisualizers.TabIndex = 1
        TipError.SetText(PanelVisualizers, Nothing)
        TipOptions.SetText(PanelVisualizers, Nothing)
        ' 
        ' CoBoxVisualizers
        ' 
        CoBoxVisualizers.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        CoBoxVisualizers.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxVisualizers.FlatStyle = FlatStyle.Flat
        CoBoxVisualizers.FormattingEnabled = True
        TipOptions.SetImage(CoBoxVisualizers, Nothing)
        TipError.SetImage(CoBoxVisualizers, Nothing)
        CoBoxVisualizers.Location = New Point(13, 29)
        CoBoxVisualizers.Name = "CoBoxVisualizers"
        CoBoxVisualizers.Size = New Size(249, 29)
        CoBoxVisualizers.TabIndex = 0
        TipOptions.SetText(CoBoxVisualizers, "Choose a Visualizer.")
        TipError.SetText(CoBoxVisualizers, Nothing)
        ' 
        ' LblVisualizers
        ' 
        LblVisualizers.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TipError.SetImage(LblVisualizers, Nothing)
        TipOptions.SetImage(LblVisualizers, Nothing)
        LblVisualizers.Location = New Point(13, 7)
        LblVisualizers.Name = "LblVisualizers"
        LblVisualizers.Size = New Size(249, 23)
        LblVisualizers.TabIndex = 2
        TipOptions.SetText(LblVisualizers, Nothing)
        LblVisualizers.Text = "Visualizer"
        TipError.SetText(LblVisualizers, Nothing)
        LblVisualizers.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TPPlaylist
        ' 
        TPPlaylist.BorderStyle = BorderStyle.Fixed3D
        TPPlaylist.Controls.Add(TxtBoxStatusMessageDisplayTime)
        TPPlaylist.Controls.Add(lblStatusMessageDisplayTime1)
        TPPlaylist.Controls.Add(BtnPrunePlaylist)
        TPPlaylist.Controls.Add(LblPlaylistFormatting)
        TPPlaylist.Controls.Add(TxtBoxPlaylistVideoIdentifier)
        TPPlaylist.Controls.Add(CoBoxPlaylistTitleFormat)
        TPPlaylist.Controls.Add(TxtBoxPlaylistTitleSeparator)
        TPPlaylist.Controls.Add(CoBoxPlaylistDefaultAction)
        TPPlaylist.Controls.Add(LblTitleFormat)
        TPPlaylist.Controls.Add(LblPlaylistSearchAction)
        TPPlaylist.Controls.Add(LblTitleSeparator)
        TPPlaylist.Controls.Add(CoBoxPlaylistSearchAction)
        TPPlaylist.Controls.Add(LblVideoIdentifier)
        TPPlaylist.Controls.Add(CkBoxPlaylistRemoveSpaces)
        TPPlaylist.Controls.Add(LblDefaultPlaylistAction)
        TPPlaylist.Controls.Add(lblStatusMessageDisplayTime2)
        TipError.SetImage(TPPlaylist, Nothing)
        TipOptions.SetImage(TPPlaylist, Nothing)
        TPPlaylist.Image = Nothing
        TPPlaylist.ImageSize = New Size(16, 16)
        TPPlaylist.Location = New Point(1, 42)
        TPPlaylist.Name = "TPPlaylist"
        TPPlaylist.ShowCloseButton = True
        TPPlaylist.Size = New Size(815, 413)
        TPPlaylist.TabIndex = 3
        TipError.SetText(TPPlaylist, Nothing)
        TipOptions.SetText(TPPlaylist, Nothing)
        TPPlaylist.Text = " Playlist "
        TPPlaylist.ThemesEnabled = False
        ' 
        ' TxtBoxStatusMessageDisplayTime
        ' 
        TxtBoxStatusMessageDisplayTime.ContextMenuStrip = CMTxtBox
        TxtBoxStatusMessageDisplayTime.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipError.SetImage(TxtBoxStatusMessageDisplayTime, Nothing)
        TipOptions.SetImage(TxtBoxStatusMessageDisplayTime, Nothing)
        TxtBoxStatusMessageDisplayTime.Location = New Point(697, 10)
        TxtBoxStatusMessageDisplayTime.Name = "TxtBoxStatusMessageDisplayTime"
        TxtBoxStatusMessageDisplayTime.ShortcutsEnabled = False
        TxtBoxStatusMessageDisplayTime.Size = New Size(44, 29)
        TxtBoxStatusMessageDisplayTime.TabIndex = 400
        TipOptions.SetText(TxtBoxStatusMessageDisplayTime, "Show Status Messages below the Playlist for 1-60 seconds, 0 to disable.")
        TipError.SetText(TxtBoxStatusMessageDisplayTime, Nothing)
        TxtBoxStatusMessageDisplayTime.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblStatusMessageDisplayTime1
        ' 
        lblStatusMessageDisplayTime1.AutoSize = True
        lblStatusMessageDisplayTime1.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(lblStatusMessageDisplayTime1, Nothing)
        TipOptions.SetImage(lblStatusMessageDisplayTime1, Nothing)
        lblStatusMessageDisplayTime1.Location = New Point(509, 14)
        lblStatusMessageDisplayTime1.Name = "lblStatusMessageDisplayTime1"
        lblStatusMessageDisplayTime1.Size = New Size(195, 21)
        lblStatusMessageDisplayTime1.TabIndex = 258
        TipOptions.SetText(lblStatusMessageDisplayTime1, Nothing)
        lblStatusMessageDisplayTime1.Text = "Show Status Messages for "
        TipError.SetText(lblStatusMessageDisplayTime1, Nothing)
        lblStatusMessageDisplayTime1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblStatusMessageDisplayTime2
        ' 
        lblStatusMessageDisplayTime2.AutoSize = True
        lblStatusMessageDisplayTime2.Font = New Font("Segoe UI", 12F)
        TipError.SetImage(lblStatusMessageDisplayTime2, Nothing)
        TipOptions.SetImage(lblStatusMessageDisplayTime2, Nothing)
        lblStatusMessageDisplayTime2.Location = New Point(739, 14)
        lblStatusMessageDisplayTime2.Name = "lblStatusMessageDisplayTime2"
        lblStatusMessageDisplayTime2.Size = New Size(68, 21)
        lblStatusMessageDisplayTime2.TabIndex = 259
        TipOptions.SetText(lblStatusMessageDisplayTime2, Nothing)
        lblStatusMessageDisplayTime2.Text = "Seconds"
        TipError.SetText(lblStatusMessageDisplayTime2, Nothing)
        lblStatusMessageDisplayTime2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TPLibrary
        ' 
        TPLibrary.BorderStyle = BorderStyle.Fixed3D
        TPLibrary.Controls.Add(CkBoxWatchFoldersUpdatePlaylist)
        TPLibrary.Controls.Add(CkBoxWatchFoldersUpdateLibrary)
        TPLibrary.Controls.Add(CkBoxWatchFolders)
        TPLibrary.Controls.Add(BtnLibrarySearchFoldersAdd)
        TPLibrary.Controls.Add(LBLibrarySearchFolders)
        TPLibrary.Controls.Add(LblLibrarySearchFolders)
        TPLibrary.Controls.Add(CkBoxLibrarySearchSubFolders)
        TipError.SetImage(TPLibrary, Nothing)
        TipOptions.SetImage(TPLibrary, Nothing)
        TPLibrary.Image = Nothing
        TPLibrary.ImageSize = New Size(16, 16)
        TPLibrary.Location = New Point(1, 42)
        TPLibrary.Name = "TPLibrary"
        TPLibrary.ShowCloseButton = True
        TPLibrary.Size = New Size(815, 413)
        TPLibrary.TabIndex = 4
        TipError.SetText(TPLibrary, Nothing)
        TipOptions.SetText(TPLibrary, Nothing)
        TPLibrary.Text = " Library "
        TPLibrary.ThemesEnabled = False
        ' 
        ' CkBoxWatchFoldersUpdatePlaylist
        ' 
        CkBoxWatchFoldersUpdatePlaylist.AutoSize = True
        CkBoxWatchFoldersUpdatePlaylist.BackColor = SystemColors.Control
        CkBoxWatchFoldersUpdatePlaylist.FlatStyle = FlatStyle.Flat
        CkBoxWatchFoldersUpdatePlaylist.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipOptions.SetImage(CkBoxWatchFoldersUpdatePlaylist, Nothing)
        TipError.SetImage(CkBoxWatchFoldersUpdatePlaylist, Nothing)
        CkBoxWatchFoldersUpdatePlaylist.Location = New Point(151, 221)
        CkBoxWatchFoldersUpdatePlaylist.Name = "CkBoxWatchFoldersUpdatePlaylist"
        CkBoxWatchFoldersUpdatePlaylist.Size = New Size(168, 25)
        CkBoxWatchFoldersUpdatePlaylist.TabIndex = 94
        TipError.SetText(CkBoxWatchFoldersUpdatePlaylist, Nothing)
        TipOptions.SetText(CkBoxWatchFoldersUpdatePlaylist, resources.GetString("CkBoxWatchFoldersUpdatePlaylist.Text"))
        CkBoxWatchFoldersUpdatePlaylist.Text = "Auto-Update Playlist"
        CkBoxWatchFoldersUpdatePlaylist.UseVisualStyleBackColor = False
        ' 
        ' CkBoxWatchFoldersUpdateLibrary
        ' 
        CkBoxWatchFoldersUpdateLibrary.AutoSize = True
        CkBoxWatchFoldersUpdateLibrary.BackColor = SystemColors.Control
        CkBoxWatchFoldersUpdateLibrary.FlatStyle = FlatStyle.Flat
        CkBoxWatchFoldersUpdateLibrary.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipOptions.SetImage(CkBoxWatchFoldersUpdateLibrary, Nothing)
        TipError.SetImage(CkBoxWatchFoldersUpdateLibrary, Nothing)
        CkBoxWatchFoldersUpdateLibrary.Location = New Point(151, 198)
        CkBoxWatchFoldersUpdateLibrary.Name = "CkBoxWatchFoldersUpdateLibrary"
        CkBoxWatchFoldersUpdateLibrary.Size = New Size(168, 25)
        CkBoxWatchFoldersUpdateLibrary.TabIndex = 92
        TipError.SetText(CkBoxWatchFoldersUpdateLibrary, Nothing)
        TipOptions.SetText(CkBoxWatchFoldersUpdateLibrary, "Automatically update the Library when files are added, renamed, deleted, or changed.")
        CkBoxWatchFoldersUpdateLibrary.Text = "Auto-Update Library"
        CkBoxWatchFoldersUpdateLibrary.UseVisualStyleBackColor = False
        ' 
        ' CkBoxWatchFolders
        ' 
        CkBoxWatchFolders.AutoSize = True
        CkBoxWatchFolders.BackColor = SystemColors.Control
        CkBoxWatchFolders.FlatStyle = FlatStyle.Flat
        CkBoxWatchFolders.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipOptions.SetImage(CkBoxWatchFolders, Nothing)
        TipError.SetImage(CkBoxWatchFolders, Nothing)
        CkBoxWatchFolders.Location = New Point(140, 175)
        CkBoxWatchFolders.Name = "CkBoxWatchFolders"
        CkBoxWatchFolders.Size = New Size(215, 25)
        CkBoxWatchFolders.TabIndex = 90
        TipError.SetText(CkBoxWatchFolders, Nothing)
        TipOptions.SetText(CkBoxWatchFolders, "Monitor Library Search Folders for changes and notify the user when changes occur.")
        CkBoxWatchFolders.Text = "Watch Folders For Changes"
        CkBoxWatchFolders.UseVisualStyleBackColor = False
        ' 
        ' LblLibrarySearchFolders
        ' 
        LblLibrarySearchFolders.AutoSize = True
        TipError.SetImage(LblLibrarySearchFolders, Nothing)
        TipOptions.SetImage(LblLibrarySearchFolders, Nothing)
        LblLibrarySearchFolders.Location = New Point(136, 18)
        LblLibrarySearchFolders.Name = "LblLibrarySearchFolders"
        LblLibrarySearchFolders.Size = New Size(165, 21)
        LblLibrarySearchFolders.TabIndex = 83
        TipError.SetText(LblLibrarySearchFolders, Nothing)
        LblLibrarySearchFolders.Text = "Library Search Folders"
        TipOptions.SetText(LblLibrarySearchFolders, Nothing)
        ' 
        ' TipOptions
        ' 
        TipOptions.FadeInRate = 25
        TipOptions.FadeOutRate = 25
        TipOptions.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipOptions.HideDelay = 1000
        TipOptions.ShadowAlpha = 0
        TipOptions.ShadowThickness = 0
        TipOptions.ShowDelay = 1000
        ' 
        ' TipError
        ' 
        TipError.FadeInRate = 25
        TipError.FadeOutRate = 25
        TipError.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipError.HideDelay = 5000
        TipError.ShadowAlpha = 0
        TipError.ShadowThickness = 0
        TipError.ShowDelay = 1000
        ' 
        ' Options
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(842, 559)
        Controls.Add(TCOptions)
        Controls.Add(BtnOK)
        FormBorderStyle = FormBorderStyle.Fixed3D
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipError.SetImage(Me, Nothing)
        TipOptions.SetImage(Me, Nothing)
        KeyPreview = True
        MaximizeBox = False
        MinimizeBox = False
        Name = "Options"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        TipOptions.SetText(Me, Nothing)
        TipError.SetText(Me, Nothing)
        Text = "Options"
        GrBoxTime.ResumeLayout(False)
        GrBoxTime.PerformLayout()
        CMLibrarySearchFolders.ResumeLayout(False)
        CType(TCOptions, ComponentModel.ISupportInitialize).EndInit()
        TCOptions.ResumeLayout(False)
        TPApp.ResumeLayout(False)
        TPApp.PerformLayout()
        TPPlayer.ResumeLayout(False)
        TPPlayer.PerformLayout()
        GrBoxShowNowPlayingToast.ResumeLayout(False)
        GrBoxShowNowPlayingToast.PerformLayout()
        TPVisualizers.ResumeLayout(False)
        TPPlaylist.ResumeLayout(False)
        TPPlaylist.PerformLayout()
        TPLibrary.ResumeLayout(False)
        TPLibrary.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents RadBtnElapsed As RadioButton
    Friend WithEvents RadBtnRemaining As RadioButton
    Friend WithEvents GrBoxTime As GroupBox
    Friend WithEvents CoBoxPlayMode As ComboBox
    Friend WithEvents CoBoxPlaylistTitleFormat As ComboBox
    Friend WithEvents TxtBoxPlaylistTitleSeparator As TextBox
    Friend WithEvents CkBoxPlaylistRemoveSpaces As CheckBox
    Friend WithEvents TxtBoxPlaylistVideoIdentifier As TextBox
    Friend WithEvents LBLibrarySearchFolders As ListBox
    Friend WithEvents CkBoxLibrarySearchSubFolders As CheckBox
    Friend WithEvents BtnLibrarySearchFoldersAdd As Button
    Friend WithEvents CMLibrarySearchFolders As ContextMenuStrip
    Friend WithEvents CMILibrarySearchFoldersAdd As ToolStripMenuItem
    Friend WithEvents CMILibrarySearchFoldersRemove As ToolStripMenuItem
    Friend WithEvents CkBoxSaveWindowMetrics As CheckBox
    Friend WithEvents CkBoxSuspendOnSessionChange As CheckBox
    Friend WithEvents BtnHelperApp1 As Button
    Friend WithEvents BtnHelperApp2 As Button
    Friend WithEvents CMTxtBox As Skye.UI.TextBoxContextMenu
    Friend WithEvents LblSongPlayMode As Skye.UI.Label
    Friend WithEvents LblTitleFormat As Skye.UI.Label
    Friend WithEvents LblTitleSeparator As Skye.UI.Label
    Friend WithEvents LblVideoIdentifier As Skye.UI.Label
    Friend WithEvents LblHelperApp1Name As Skye.UI.Label
    Friend WithEvents CoBoxPlaylistDefaultAction As ComboBox
    Friend WithEvents CoBoxPlaylistSearchAction As ComboBox
    Friend WithEvents LblDefaultPlaylistAction As Skye.UI.Label
    Friend WithEvents LblPlaylistSearchAction As Skye.UI.Label
    Friend WithEvents CoBoxTheme As ComboBox
    Friend WithEvents LblTheme As Skye.UI.Label
    Friend WithEvents TxtBoxHistoryAutoSaveInterval As TextBox
    Friend WithEvents LblHistoryAutoSaveInterval1 As Skye.UI.Label
    Friend WithEvents LblHistoryAutoSaveInterval2 As Skye.UI.Label
    Friend WithEvents BtnHistorySaveNow As Button
    Friend WithEvents BtnHistoryPrune As Button
    Friend WithEvents TCOptions As Syncfusion.Windows.Forms.Tools.TabControlAdv
    Friend WithEvents TPApp As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents TPPlayer As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents TPPlaylist As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents TPLibrary As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents TxtBoxHistoryUpdateInterval As TextBox
    Friend WithEvents LblHistoryUpdateInterval2 As Skye.UI.Label
    Friend WithEvents LblHistoryUpdateInterval1 As Skye.UI.Label
    Friend WithEvents LblLibrarySearchFolders As Label
    Friend WithEvents LblPlaylistFormatting As Label
    Friend WithEvents TxtBoxHelperApp2Path As TextBox
    Friend WithEvents TxtBoxHelperApp2Name As TextBox
    Friend WithEvents TxtBoxHelperApp1Path As TextBox
    Friend WithEvents TxtBoxHelperApp1Name As TextBox
    Friend WithEvents LblHelperApp2Path As Skye.UI.Label
    Friend WithEvents LblHelperApp2Name As Skye.UI.Label
    Friend WithEvents LblHelperApp1Path As Skye.UI.Label
    Friend WithEvents TxtBoxRandomHistoryUpdateInterval As TextBox
    Friend WithEvents LblRandomHistoryUpdateInterval2 As Skye.UI.Label
    Friend WithEvents LblRandomHistoryUpdateInterval1 As Skye.UI.Label
    Friend WithEvents BtnPrunePlaylist As Button
    Friend WithEvents TipOptions As Skye.UI.ToolTipEX
    Friend WithEvents CkBoxWatchFoldersUpdatePlaylist As CheckBox
    Friend WithEvents CkBoxWatchFoldersUpdateLibrary As CheckBox
    Friend WithEvents CkBoxWatchFolders As CheckBox
    Friend WithEvents lblStatusMessageDisplayTime2 As Skye.UI.Label
    Friend WithEvents TxtBoxStatusMessageDisplayTime As TextBox
    Friend WithEvents lblStatusMessageDisplayTime1 As Skye.UI.Label
    Friend WithEvents TPVisualizers As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents LblVisualizers As Skye.UI.Label
    Friend WithEvents PanelVisualizers As Panel
    Friend WithEvents CoBoxVisualizers As ComboBox
    Friend WithEvents CkBoxShowTrayIcon As CheckBox
    Friend WithEvents CkBoxMinimizeToTray As CheckBox
    Friend WithEvents CkBoxShowNowPlayingToast As CheckBox
    Friend WithEvents GrBoxShowNowPlayingToast As GroupBox
    Friend WithEvents RadBtnNPTTopCenter As RadioButton
    Friend WithEvents RadBtnNPTTopLeft As RadioButton
    Friend WithEvents RadBtnNPTTopRight As RadioButton
    Friend WithEvents RadBtnNPTBottomRight As RadioButton
    Friend WithEvents RadBtnNPTBottomCenter As RadioButton
    Friend WithEvents RadBtnNPTBottomLeft As RadioButton
    Friend WithEvents RadBtnNPTMiddleRight As RadioButton
    Friend WithEvents RadBtnNPTMiddleCenter As RadioButton
    Friend WithEvents RadBtnNPTMiddleLeft As RadioButton
    Friend WithEvents TipError As Skye.UI.ToolTipEX
    Friend WithEvents CkBoxEnableCompanionServer As CheckBox
    Friend WithEvents LblCompanionServerPort As Skye.UI.Label
    Friend WithEvents TxtBoxCompanionServerPort As Skye.UI.NumericTextBox
End Class
