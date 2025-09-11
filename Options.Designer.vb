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
        TipOptions = New ToolTip(components)
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
        LblHelperApp2Path = New Skye.UI.Label()
        LblHelperApp2Name = New Skye.UI.Label()
        LblHelperApp1Path = New Skye.UI.Label()
        TPPlayer = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        LblRandomHistoryUpdateInterval1 = New Skye.UI.Label()
        LblRandomHistoryUpdateInterval2 = New Skye.UI.Label()
        TPPlaylist = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        TPLibrary = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        LblLibrarySearchFolders = New Label()
        GrBoxTime.SuspendLayout()
        CMLibrarySearchFolders.SuspendLayout()
        CType(TCOptions, ComponentModel.ISupportInitialize).BeginInit()
        TCOptions.SuspendLayout()
        TPApp.SuspendLayout()
        TPPlayer.SuspendLayout()
        TPPlaylist.SuspendLayout()
        TPLibrary.SuspendLayout()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(389, 483)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 100
        TipOptions.SetToolTip(BtnOK, "Save & Close Window")
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' RadBtnElapsed
        ' 
        RadBtnElapsed.AutoSize = True
        RadBtnElapsed.Checked = True
        RadBtnElapsed.Font = New Font("Segoe UI", 12F)
        RadBtnElapsed.Location = New Point(6, 22)
        RadBtnElapsed.Name = "RadBtnElapsed"
        RadBtnElapsed.Size = New Size(162, 25)
        RadBtnElapsed.TabIndex = 1
        RadBtnElapsed.TabStop = True
        RadBtnElapsed.Text = "Show Elapsed Time"
        TipOptions.SetToolTip(RadBtnElapsed, "Show time that has passed since the song started playing.")
        RadBtnElapsed.UseVisualStyleBackColor = True
        ' 
        ' RadBtnRemaining
        ' 
        RadBtnRemaining.AutoSize = True
        RadBtnRemaining.BackColor = SystemColors.Control
        RadBtnRemaining.Font = New Font("Segoe UI", 12F)
        RadBtnRemaining.Location = New Point(6, 51)
        RadBtnRemaining.Name = "RadBtnRemaining"
        RadBtnRemaining.Size = New Size(184, 25)
        RadBtnRemaining.TabIndex = 2
        RadBtnRemaining.Text = "Show Remaining Time"
        TipOptions.SetToolTip(RadBtnRemaining, "Show time left before the song is finished playing.")
        RadBtnRemaining.UseVisualStyleBackColor = False
        ' 
        ' GrBoxTime
        ' 
        GrBoxTime.BackColor = SystemColors.Control
        GrBoxTime.Controls.Add(RadBtnElapsed)
        GrBoxTime.Controls.Add(RadBtnRemaining)
        GrBoxTime.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        GrBoxTime.Location = New Point(14, 14)
        GrBoxTime.Name = "GrBoxTime"
        GrBoxTime.Size = New Size(197, 89)
        GrBoxTime.TabIndex = 0
        GrBoxTime.TabStop = False
        GrBoxTime.Text = "Song Position Display"
        TipOptions.SetToolTip(GrBoxTime, "Choose the way to view the play time of the song." & vbCrLf & "This may also be achieved by clicking the Play Time in the Player.")
        ' 
        ' CoBoxPlayMode
        ' 
        CoBoxPlayMode.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlayMode.FlatStyle = FlatStyle.Flat
        CoBoxPlayMode.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CoBoxPlayMode.FormattingEnabled = True
        CoBoxPlayMode.Location = New Point(14, 158)
        CoBoxPlayMode.Name = "CoBoxPlayMode"
        CoBoxPlayMode.Size = New Size(168, 29)
        CoBoxPlayMode.TabIndex = 3
        TipOptions.SetToolTip(CoBoxPlayMode, "Song Play Mode" & vbCrLf & "  Play Once = Play the song then stop." & vbCrLf & "  Repeat = Play the same song over & over." & vbCrLf & "  Play Next = Play the next or previous song in the Playlist." & vbCrLf & "  Shuffle = Play a random song next.")
        ' 
        ' CoBoxPlaylistTitleFormat
        ' 
        CoBoxPlaylistTitleFormat.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlaylistTitleFormat.FlatStyle = FlatStyle.Flat
        CoBoxPlaylistTitleFormat.Font = New Font("Segoe UI", 12F)
        CoBoxPlaylistTitleFormat.FormattingEnabled = True
        CoBoxPlaylistTitleFormat.Location = New Point(19, 59)
        CoBoxPlaylistTitleFormat.Name = "CoBoxPlaylistTitleFormat"
        CoBoxPlaylistTitleFormat.Size = New Size(230, 29)
        CoBoxPlaylistTitleFormat.TabIndex = 10
        TipOptions.SetToolTip(CoBoxPlaylistTitleFormat, "Choose how to generally format the song title in the Playlist")
        ' 
        ' TxtBoxPlaylistTitleSeparator
        ' 
        TxtBoxPlaylistTitleSeparator.ContextMenuStrip = CMTxtBox
        TxtBoxPlaylistTitleSeparator.Font = New Font("Segoe UI", 12F)
        TxtBoxPlaylistTitleSeparator.Location = New Point(18, 138)
        TxtBoxPlaylistTitleSeparator.Name = "TxtBoxPlaylistTitleSeparator"
        TxtBoxPlaylistTitleSeparator.ShortcutsEnabled = False
        TxtBoxPlaylistTitleSeparator.Size = New Size(137, 29)
        TxtBoxPlaylistTitleSeparator.TabIndex = 18
        TipOptions.SetToolTip(TxtBoxPlaylistTitleSeparator, "Enter a separator to separate different song title elements.")
        ' 
        ' CMTxtBox
        ' 
        CMTxtBox.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CMTxtBox.Name = "CMTxtBox"
        CMTxtBox.Size = New Size(181, 198)
        ' 
        ' CkBoxPlaylistRemoveSpaces
        ' 
        CkBoxPlaylistRemoveSpaces.AutoSize = True
        CkBoxPlaylistRemoveSpaces.FlatStyle = FlatStyle.Flat
        CkBoxPlaylistRemoveSpaces.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CkBoxPlaylistRemoveSpaces.Location = New Point(20, 86)
        CkBoxPlaylistRemoveSpaces.Name = "CkBoxPlaylistRemoveSpaces"
        CkBoxPlaylistRemoveSpaces.Size = New Size(135, 25)
        CkBoxPlaylistRemoveSpaces.TabIndex = 14
        CkBoxPlaylistRemoveSpaces.Text = "Remove Spaces"
        TipOptions.SetToolTip(CkBoxPlaylistRemoveSpaces, "Select to remove spaces from each element of the song title.")
        CkBoxPlaylistRemoveSpaces.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxPlaylistVideoIdentifier
        ' 
        TxtBoxPlaylistVideoIdentifier.ContextMenuStrip = CMTxtBox
        TxtBoxPlaylistVideoIdentifier.Font = New Font("Segoe UI", 12F)
        TxtBoxPlaylistVideoIdentifier.Location = New Point(18, 192)
        TxtBoxPlaylistVideoIdentifier.Name = "TxtBoxPlaylistVideoIdentifier"
        TxtBoxPlaylistVideoIdentifier.ShortcutsEnabled = False
        TxtBoxPlaylistVideoIdentifier.Size = New Size(137, 29)
        TxtBoxPlaylistVideoIdentifier.TabIndex = 22
        TipOptions.SetToolTip(TxtBoxPlaylistVideoIdentifier, "Enter how to identify videos in the Playlist." & vbCrLf & "The identifier will be added to the end of the title.")
        ' 
        ' LblTitleFormat
        ' 
        LblTitleFormat.AutoSize = True
        LblTitleFormat.Location = New Point(19, 38)
        LblTitleFormat.Name = "LblTitleFormat"
        LblTitleFormat.Size = New Size(93, 21)
        LblTitleFormat.TabIndex = 132
        LblTitleFormat.Text = "Title Format"
        ' 
        ' LblTitleSeparator
        ' 
        LblTitleSeparator.AutoSize = True
        LblTitleSeparator.Location = New Point(19, 117)
        LblTitleSeparator.Name = "LblTitleSeparator"
        LblTitleSeparator.Size = New Size(111, 21)
        LblTitleSeparator.TabIndex = 132
        LblTitleSeparator.Text = "Title Separator"
        ' 
        ' LblVideoIdentifier
        ' 
        LblVideoIdentifier.AutoSize = True
        LblVideoIdentifier.Location = New Point(18, 173)
        LblVideoIdentifier.Name = "LblVideoIdentifier"
        LblVideoIdentifier.Size = New Size(116, 21)
        LblVideoIdentifier.TabIndex = 133
        LblVideoIdentifier.Text = "Video Identifier"
        ' 
        ' BtnLibrarySearchFoldersAdd
        ' 
        BtnLibrarySearchFoldersAdd.Image = My.Resources.Resources.ImageAdd16
        BtnLibrarySearchFoldersAdd.Location = New Point(643, 147)
        BtnLibrarySearchFoldersAdd.Name = "BtnLibrarySearchFoldersAdd"
        BtnLibrarySearchFoldersAdd.Size = New Size(32, 31)
        BtnLibrarySearchFoldersAdd.TabIndex = 82
        TipOptions.SetToolTip(BtnLibrarySearchFoldersAdd, "Add folder to list.")
        BtnLibrarySearchFoldersAdd.UseVisualStyleBackColor = True
        ' 
        ' CkBoxLibrarySearchSubFolders
        ' 
        CkBoxLibrarySearchSubFolders.AutoSize = True
        CkBoxLibrarySearchSubFolders.BackColor = SystemColors.Control
        CkBoxLibrarySearchSubFolders.FlatStyle = FlatStyle.Flat
        CkBoxLibrarySearchSubFolders.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CkBoxLibrarySearchSubFolders.Location = New Point(140, 144)
        CkBoxLibrarySearchSubFolders.Name = "CkBoxLibrarySearchSubFolders"
        CkBoxLibrarySearchSubFolders.Size = New Size(161, 25)
        CkBoxLibrarySearchSubFolders.TabIndex = 81
        CkBoxLibrarySearchSubFolders.Text = "Search Sub-Folders"
        TipOptions.SetToolTip(CkBoxLibrarySearchSubFolders, "Check this to search all sub-folders of the  folders in your list. Un-Check it to only search the folders listed.")
        CkBoxLibrarySearchSubFolders.UseVisualStyleBackColor = False
        ' 
        ' LBLibrarySearchFolders
        ' 
        LBLibrarySearchFolders.BackColor = SystemColors.Window
        LBLibrarySearchFolders.ContextMenuStrip = CMLibrarySearchFolders
        LBLibrarySearchFolders.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LBLibrarySearchFolders.FormattingEnabled = True
        LBLibrarySearchFolders.Location = New Point(137, 39)
        LBLibrarySearchFolders.Name = "LBLibrarySearchFolders"
        LBLibrarySearchFolders.Size = New Size(541, 88)
        LBLibrarySearchFolders.Sorted = True
        LBLibrarySearchFolders.TabIndex = 80
        TipOptions.SetToolTip(LBLibrarySearchFolders, "Add your music folders here, the Library uses this list to find your music." & vbCrLf & "Right-Click for options.")
        ' 
        ' CMLibrarySearchFolders
        ' 
        CMLibrarySearchFolders.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CMLibrarySearchFolders.Items.AddRange(New ToolStripItem() {CMILibrarySearchFoldersAdd, CMILibrarySearchFoldersRemove})
        CMLibrarySearchFolders.Name = "CMLibrarySearchFolders"
        CMLibrarySearchFolders.Size = New Size(165, 48)
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
        CkBoxSaveWindowMetrics.Location = New Point(13, 181)
        CkBoxSaveWindowMetrics.Name = "CkBoxSaveWindowMetrics"
        CkBoxSaveWindowMetrics.Size = New Size(176, 25)
        CkBoxSaveWindowMetrics.TabIndex = 20
        CkBoxSaveWindowMetrics.Text = "Save Window Metrics"
        TipOptions.SetToolTip(CkBoxSaveWindowMetrics, "Auto save window sizes and locations.")
        CkBoxSaveWindowMetrics.UseVisualStyleBackColor = True
        ' 
        ' CkBoxSuspendOnSessionChange
        ' 
        CkBoxSuspendOnSessionChange.AutoSize = True
        CkBoxSuspendOnSessionChange.FlatStyle = FlatStyle.Flat
        CkBoxSuspendOnSessionChange.Font = New Font("Segoe UI", 12F)
        CkBoxSuspendOnSessionChange.Location = New Point(13, 203)
        CkBoxSuspendOnSessionChange.Name = "CkBoxSuspendOnSessionChange"
        CkBoxSuspendOnSessionChange.Size = New Size(234, 25)
        CkBoxSuspendOnSessionChange.TabIndex = 22
        CkBoxSuspendOnSessionChange.Text = "Minimize App On Screen Lock"
        TipOptions.SetToolTip(CkBoxSuspendOnSessionChange, "Stop play and Minimize the window if the screen is locked or screensaver is activated.")
        CkBoxSuspendOnSessionChange.UseVisualStyleBackColor = True
        ' 
        ' BtnHelperApp1
        ' 
        BtnHelperApp1.Image = My.Resources.Resources.ImageGetPath16
        BtnHelperApp1.Location = New Point(773, 81)
        BtnHelperApp1.Name = "BtnHelperApp1"
        BtnHelperApp1.Size = New Size(32, 31)
        BtnHelperApp1.TabIndex = 64
        TipOptions.SetToolTip(BtnHelperApp1, "Select a Helper App.")
        BtnHelperApp1.UseVisualStyleBackColor = True
        ' 
        ' BtnHelperApp2
        ' 
        BtnHelperApp2.Image = My.Resources.Resources.ImageGetPath16
        BtnHelperApp2.Location = New Point(773, 199)
        BtnHelperApp2.Name = "BtnHelperApp2"
        BtnHelperApp2.Size = New Size(32, 31)
        BtnHelperApp2.TabIndex = 70
        TipOptions.SetToolTip(BtnHelperApp2, "Select a Helper App.")
        BtnHelperApp2.UseVisualStyleBackColor = True
        ' 
        ' LblSongPlayMode
        ' 
        LblSongPlayMode.AutoSize = True
        LblSongPlayMode.Font = New Font("Segoe UI", 12F)
        LblSongPlayMode.Location = New Point(14, 136)
        LblSongPlayMode.Name = "LblSongPlayMode"
        LblSongPlayMode.Size = New Size(123, 21)
        LblSongPlayMode.TabIndex = 131
        LblSongPlayMode.Text = "Song Play Mode"
        ' 
        ' LblHelperApp1Name
        ' 
        LblHelperApp1Name.AutoSize = True
        LblHelperApp1Name.Font = New Font("Segoe UI", 12F)
        LblHelperApp1Name.Location = New Point(353, 10)
        LblHelperApp1Name.Name = "LblHelperApp1Name"
        LblHelperApp1Name.Size = New Size(165, 21)
        LblHelperApp1Name.TabIndex = 132
        LblHelperApp1Name.Text = "Name of Helper App 1"
        ' 
        ' CoBoxPlaylistDefaultAction
        ' 
        CoBoxPlaylistDefaultAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlaylistDefaultAction.FlatStyle = FlatStyle.Flat
        CoBoxPlaylistDefaultAction.Font = New Font("Segoe UI", 12F)
        CoBoxPlaylistDefaultAction.FormattingEnabled = True
        CoBoxPlaylistDefaultAction.Location = New Point(10, 282)
        CoBoxPlaylistDefaultAction.Name = "CoBoxPlaylistDefaultAction"
        CoBoxPlaylistDefaultAction.Size = New Size(178, 29)
        CoBoxPlaylistDefaultAction.TabIndex = 250
        TipOptions.SetToolTip(CoBoxPlaylistDefaultAction, "Choose what happens when you double-click a song in the playlist.")
        ' 
        ' CoBoxPlaylistSearchAction
        ' 
        CoBoxPlaylistSearchAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlaylistSearchAction.FlatStyle = FlatStyle.Flat
        CoBoxPlaylistSearchAction.Font = New Font("Segoe UI", 12F)
        CoBoxPlaylistSearchAction.FormattingEnabled = True
        CoBoxPlaylistSearchAction.Location = New Point(10, 371)
        CoBoxPlaylistSearchAction.Name = "CoBoxPlaylistSearchAction"
        CoBoxPlaylistSearchAction.Size = New Size(178, 29)
        CoBoxPlaylistSearchAction.TabIndex = 255
        TipOptions.SetToolTip(CoBoxPlaylistSearchAction, "Choose what happens when you select an item in the search box.")
        ' 
        ' LblDefaultPlaylistAction
        ' 
        LblDefaultPlaylistAction.AutoSize = True
        LblDefaultPlaylistAction.Font = New Font("Segoe UI", 12F)
        LblDefaultPlaylistAction.Location = New Point(10, 260)
        LblDefaultPlaylistAction.Name = "LblDefaultPlaylistAction"
        LblDefaultPlaylistAction.Size = New Size(161, 21)
        LblDefaultPlaylistAction.TabIndex = 138
        LblDefaultPlaylistAction.Text = "Default Playlist Action"
        ' 
        ' LblPlaylistSearchAction
        ' 
        LblPlaylistSearchAction.AutoSize = True
        LblPlaylistSearchAction.Font = New Font("Segoe UI", 12F)
        LblPlaylistSearchAction.Location = New Point(10, 349)
        LblPlaylistSearchAction.Name = "LblPlaylistSearchAction"
        LblPlaylistSearchAction.Size = New Size(158, 21)
        LblPlaylistSearchAction.TabIndex = 139
        LblPlaylistSearchAction.Text = "Playlist Search Action"
        ' 
        ' CoBoxTheme
        ' 
        CoBoxTheme.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxTheme.FlatStyle = FlatStyle.Flat
        CoBoxTheme.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CoBoxTheme.FormattingEnabled = True
        CoBoxTheme.Location = New Point(13, 31)
        CoBoxTheme.Name = "CoBoxTheme"
        CoBoxTheme.Size = New Size(196, 33)
        CoBoxTheme.TabIndex = 10
        TipOptions.SetToolTip(CoBoxTheme, "Choose a color theme." & vbCrLf & "Accent Theme will color the app based on the Windows accent color.")
        ' 
        ' LblTheme
        ' 
        LblTheme.AutoSize = True
        LblTheme.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblTheme.Location = New Point(13, 12)
        LblTheme.Name = "LblTheme"
        LblTheme.Size = New Size(57, 21)
        LblTheme.TabIndex = 141
        LblTheme.Text = "Theme"
        ' 
        ' TipOptions
        ' 
        TipOptions.OwnerDraw = True
        ' 
        ' TxtBoxHistoryUpdateInterval
        ' 
        TxtBoxHistoryUpdateInterval.ContextMenuStrip = CMTxtBox
        TxtBoxHistoryUpdateInterval.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxHistoryUpdateInterval.Location = New Point(269, 229)
        TxtBoxHistoryUpdateInterval.Name = "TxtBoxHistoryUpdateInterval"
        TxtBoxHistoryUpdateInterval.ShortcutsEnabled = False
        TxtBoxHistoryUpdateInterval.Size = New Size(44, 29)
        TxtBoxHistoryUpdateInterval.TabIndex = 148
        TxtBoxHistoryUpdateInterval.TextAlign = HorizontalAlignment.Center
        TipOptions.SetToolTip(TxtBoxHistoryUpdateInterval, "Update Song History after 1-60 seconds, or 0 for immediate update.")
        ' 
        ' LblPlaylistFormatting
        ' 
        LblPlaylistFormatting.AutoSize = True
        LblPlaylistFormatting.Font = New Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        LblPlaylistFormatting.Location = New Point(10, 10)
        LblPlaylistFormatting.Name = "LblPlaylistFormatting"
        LblPlaylistFormatting.Size = New Size(140, 21)
        LblPlaylistFormatting.TabIndex = 140
        LblPlaylistFormatting.Text = "Playlist Formatting"
        TipOptions.SetToolTip(LblPlaylistFormatting, "Choose the formatting options for listing songs in the Playlist.")
        ' 
        ' TxtBoxHistoryAutoSaveInterval
        ' 
        TxtBoxHistoryAutoSaveInterval.ContextMenuStrip = CMTxtBox
        TxtBoxHistoryAutoSaveInterval.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxHistoryAutoSaveInterval.Location = New Point(148, 301)
        TxtBoxHistoryAutoSaveInterval.Name = "TxtBoxHistoryAutoSaveInterval"
        TxtBoxHistoryAutoSaveInterval.ShortcutsEnabled = False
        TxtBoxHistoryAutoSaveInterval.Size = New Size(61, 29)
        TxtBoxHistoryAutoSaveInterval.TabIndex = 42
        TxtBoxHistoryAutoSaveInterval.TextAlign = HorizontalAlignment.Center
        TipOptions.SetToolTip(TxtBoxHistoryAutoSaveInterval, "Auto save the song history every 1-1440 minutes.")
        ' 
        ' BtnHistorySaveNow
        ' 
        BtnHistorySaveNow.Image = My.Resources.Resources.ImageSave32
        BtnHistorySaveNow.ImageAlign = ContentAlignment.MiddleLeft
        BtnHistorySaveNow.Location = New Point(267, 293)
        BtnHistorySaveNow.Name = "BtnHistorySaveNow"
        BtnHistorySaveNow.Size = New Size(120, 40)
        BtnHistorySaveNow.TabIndex = 44
        BtnHistorySaveNow.Text = "Save Now"
        BtnHistorySaveNow.TextAlign = ContentAlignment.MiddleRight
        TipOptions.SetToolTip(BtnHistorySaveNow, "Save the song history now.")
        BtnHistorySaveNow.UseVisualStyleBackColor = True
        ' 
        ' BtnHistoryPrune
        ' 
        BtnHistoryPrune.Image = My.Resources.Resources.ImagePrune32
        BtnHistoryPrune.ImageAlign = ContentAlignment.MiddleLeft
        BtnHistoryPrune.Location = New Point(587, 293)
        BtnHistoryPrune.Name = "BtnHistoryPrune"
        BtnHistoryPrune.Size = New Size(218, 40)
        BtnHistoryPrune.TabIndex = 90
        BtnHistoryPrune.Text = "Prune History"
        BtnHistoryPrune.TextAlign = ContentAlignment.MiddleRight
        TipOptions.SetToolTip(BtnHistoryPrune, resources.GetString("BtnHistoryPrune.ToolTip"))
        BtnHistoryPrune.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxHelperApp2Path
        ' 
        TxtBoxHelperApp2Path.ContextMenuStrip = CMTxtBox
        TxtBoxHelperApp2Path.Font = New Font("Segoe UI", 12F)
        TxtBoxHelperApp2Path.Location = New Point(353, 200)
        TxtBoxHelperApp2Path.Name = "TxtBoxHelperApp2Path"
        TxtBoxHelperApp2Path.ShortcutsEnabled = False
        TxtBoxHelperApp2Path.Size = New Size(420, 29)
        TxtBoxHelperApp2Path.TabIndex = 68
        TipOptions.SetToolTip(TxtBoxHelperApp2Path, "Enter or Select the path to your Helper App.")
        ' 
        ' TxtBoxHelperApp2Name
        ' 
        TxtBoxHelperApp2Name.ContextMenuStrip = CMTxtBox
        TxtBoxHelperApp2Name.Font = New Font("Segoe UI", 12F)
        TxtBoxHelperApp2Name.Location = New Point(353, 150)
        TxtBoxHelperApp2Name.Name = "TxtBoxHelperApp2Name"
        TxtBoxHelperApp2Name.ShortcutsEnabled = False
        TxtBoxHelperApp2Name.Size = New Size(202, 29)
        TxtBoxHelperApp2Name.TabIndex = 66
        TipOptions.SetToolTip(TxtBoxHelperApp2Name, "Enter a name for your Helper App.")
        ' 
        ' TxtBoxHelperApp1Path
        ' 
        TxtBoxHelperApp1Path.ContextMenuStrip = CMTxtBox
        TxtBoxHelperApp1Path.Font = New Font("Segoe UI", 12F)
        TxtBoxHelperApp1Path.ForeColor = Color.White
        TxtBoxHelperApp1Path.Location = New Point(353, 82)
        TxtBoxHelperApp1Path.Name = "TxtBoxHelperApp1Path"
        TxtBoxHelperApp1Path.ShortcutsEnabled = False
        TxtBoxHelperApp1Path.Size = New Size(420, 29)
        TxtBoxHelperApp1Path.TabIndex = 62
        TipOptions.SetToolTip(TxtBoxHelperApp1Path, "Enter or Select the path to your Helper App.")
        ' 
        ' TxtBoxHelperApp1Name
        ' 
        TxtBoxHelperApp1Name.ContextMenuStrip = CMTxtBox
        TxtBoxHelperApp1Name.Font = New Font("Segoe UI", 12F)
        TxtBoxHelperApp1Name.Location = New Point(353, 31)
        TxtBoxHelperApp1Name.Name = "TxtBoxHelperApp1Name"
        TxtBoxHelperApp1Name.ShortcutsEnabled = False
        TxtBoxHelperApp1Name.Size = New Size(202, 29)
        TxtBoxHelperApp1Name.TabIndex = 60
        TipOptions.SetToolTip(TxtBoxHelperApp1Name, "Enter a name for your Helper App.")
        ' 
        ' TxtBoxRandomHistoryUpdateInterval
        ' 
        TxtBoxRandomHistoryUpdateInterval.ContextMenuStrip = CMTxtBox
        TxtBoxRandomHistoryUpdateInterval.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxRandomHistoryUpdateInterval.Location = New Point(355, 293)
        TxtBoxRandomHistoryUpdateInterval.Name = "TxtBoxRandomHistoryUpdateInterval"
        TxtBoxRandomHistoryUpdateInterval.ShortcutsEnabled = False
        TxtBoxRandomHistoryUpdateInterval.Size = New Size(44, 29)
        TxtBoxRandomHistoryUpdateInterval.TabIndex = 151
        TxtBoxRandomHistoryUpdateInterval.TextAlign = HorizontalAlignment.Center
        TipOptions.SetToolTip(TxtBoxRandomHistoryUpdateInterval, resources.GetString("TxtBoxRandomHistoryUpdateInterval.ToolTip"))
        ' 
        ' BtnPrunePlaylist
        ' 
        BtnPrunePlaylist.Image = My.Resources.Resources.ImagePrune32
        BtnPrunePlaylist.ImageAlign = ContentAlignment.MiddleLeft
        BtnPrunePlaylist.Location = New Point(583, 360)
        BtnPrunePlaylist.Name = "BtnPrunePlaylist"
        BtnPrunePlaylist.Size = New Size(218, 40)
        BtnPrunePlaylist.TabIndex = 256
        BtnPrunePlaylist.Text = "Prune Playlist"
        BtnPrunePlaylist.TextAlign = ContentAlignment.MiddleRight
        TipOptions.SetToolTip(BtnPrunePlaylist, "Prune the Playlist." & vbCrLf & "This will remove any playlist entries that cannot be found in storage, while preserving streams." & vbCrLf & "The total number of playlist entries is given in parentheses.")
        BtnPrunePlaylist.UseVisualStyleBackColor = True
        ' 
        ' LblHistoryUpdateInterval2
        ' 
        LblHistoryUpdateInterval2.AutoSize = True
        LblHistoryUpdateInterval2.Font = New Font("Segoe UI", 12F)
        LblHistoryUpdateInterval2.Location = New Point(311, 232)
        LblHistoryUpdateInterval2.Name = "LblHistoryUpdateInterval2"
        LblHistoryUpdateInterval2.Size = New Size(68, 21)
        LblHistoryUpdateInterval2.TabIndex = 150
        LblHistoryUpdateInterval2.Text = "Seconds"
        LblHistoryUpdateInterval2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblHistoryUpdateInterval1
        ' 
        LblHistoryUpdateInterval1.AutoSize = True
        LblHistoryUpdateInterval1.Font = New Font("Segoe UI", 12F)
        LblHistoryUpdateInterval1.Location = New Point(14, 233)
        LblHistoryUpdateInterval1.Name = "LblHistoryUpdateInterval1"
        LblHistoryUpdateInterval1.Size = New Size(259, 21)
        LblHistoryUpdateInterval1.TabIndex = 149
        LblHistoryUpdateInterval1.Text = "Update History After Song Plays For"
        LblHistoryUpdateInterval1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblHistoryAutoSaveInterval1
        ' 
        LblHistoryAutoSaveInterval1.AutoSize = True
        LblHistoryAutoSaveInterval1.Font = New Font("Segoe UI", 12F)
        LblHistoryAutoSaveInterval1.Location = New Point(13, 304)
        LblHistoryAutoSaveInterval1.Name = "LblHistoryAutoSaveInterval1"
        LblHistoryAutoSaveInterval1.Size = New Size(139, 21)
        LblHistoryAutoSaveInterval1.TabIndex = 143
        LblHistoryAutoSaveInterval1.Text = "Save History Every"
        LblHistoryAutoSaveInterval1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblHistoryAutoSaveInterval2
        ' 
        LblHistoryAutoSaveInterval2.AutoSize = True
        LblHistoryAutoSaveInterval2.Font = New Font("Segoe UI", 12F)
        LblHistoryAutoSaveInterval2.Location = New Point(206, 304)
        LblHistoryAutoSaveInterval2.Name = "LblHistoryAutoSaveInterval2"
        LblHistoryAutoSaveInterval2.Size = New Size(66, 21)
        LblHistoryAutoSaveInterval2.TabIndex = 144
        LblHistoryAutoSaveInterval2.Text = "Minutes"
        LblHistoryAutoSaveInterval2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TCOptions
        ' 
        TCOptions.ActiveTabFont = New Font("Segoe UI", 14.25F, FontStyle.Bold)
        TCOptions.BeforeTouchSize = New Size(818, 457)
        TCOptions.Controls.Add(TPApp)
        TCOptions.Controls.Add(TPPlayer)
        TCOptions.Controls.Add(TPPlaylist)
        TCOptions.Controls.Add(TPLibrary)
        TCOptions.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TCOptions.Location = New Point(12, 12)
        TCOptions.Name = "TCOptions"
        TCOptions.Size = New Size(818, 457)
        TCOptions.TabIndex = 148
        TCOptions.TabStop = False
        TCOptions.TabStyle = GetType(Syncfusion.Windows.Forms.Tools.TabRendererWhidbey)
        TCOptions.ThemeName = "TabRendererWhidbey"
        ' 
        ' TPApp
        ' 
        TPApp.BorderStyle = BorderStyle.Fixed3D
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
        TPApp.Image = Nothing
        TPApp.ImageSize = New Size(16, 16)
        TPApp.Location = New Point(1, 42)
        TPApp.Name = "TPApp"
        TPApp.ShowCloseButton = True
        TPApp.Size = New Size(815, 413)
        TPApp.TabIndex = 1
        TPApp.Text = " App "
        TPApp.ThemesEnabled = False
        ' 
        ' LblHelperApp2Path
        ' 
        LblHelperApp2Path.AutoSize = True
        LblHelperApp2Path.Font = New Font("Segoe UI", 12F)
        LblHelperApp2Path.Location = New Point(353, 179)
        LblHelperApp2Path.Name = "LblHelperApp2Path"
        LblHelperApp2Path.Size = New Size(153, 21)
        LblHelperApp2Path.TabIndex = 135
        LblHelperApp2Path.Text = "Path to Helper App 2"
        ' 
        ' LblHelperApp2Name
        ' 
        LblHelperApp2Name.AutoSize = True
        LblHelperApp2Name.Font = New Font("Segoe UI", 12F)
        LblHelperApp2Name.Location = New Point(353, 129)
        LblHelperApp2Name.Name = "LblHelperApp2Name"
        LblHelperApp2Name.Size = New Size(165, 21)
        LblHelperApp2Name.TabIndex = 134
        LblHelperApp2Name.Text = "Name of Helper App 2"
        ' 
        ' LblHelperApp1Path
        ' 
        LblHelperApp1Path.AutoSize = True
        LblHelperApp1Path.Font = New Font("Segoe UI", 12F)
        LblHelperApp1Path.Location = New Point(353, 60)
        LblHelperApp1Path.Name = "LblHelperApp1Path"
        LblHelperApp1Path.Size = New Size(153, 21)
        LblHelperApp1Path.TabIndex = 133
        LblHelperApp1Path.Text = "Path to Helper App 1"
        ' 
        ' TPPlayer
        ' 
        TPPlayer.BorderStyle = BorderStyle.Fixed3D
        TPPlayer.Controls.Add(TxtBoxRandomHistoryUpdateInterval)
        TPPlayer.Controls.Add(TxtBoxHistoryUpdateInterval)
        TPPlayer.Controls.Add(LblHistoryUpdateInterval2)
        TPPlayer.Controls.Add(GrBoxTime)
        TPPlayer.Controls.Add(CoBoxPlayMode)
        TPPlayer.Controls.Add(LblHistoryUpdateInterval1)
        TPPlayer.Controls.Add(LblSongPlayMode)
        TPPlayer.Controls.Add(LblRandomHistoryUpdateInterval1)
        TPPlayer.Controls.Add(LblRandomHistoryUpdateInterval2)
        TPPlayer.Image = Nothing
        TPPlayer.ImageSize = New Size(16, 16)
        TPPlayer.Location = New Point(1, 42)
        TPPlayer.Name = "TPPlayer"
        TPPlayer.ShowCloseButton = True
        TPPlayer.Size = New Size(815, 413)
        TPPlayer.TabIndex = 2
        TPPlayer.Text = " Player "
        TPPlayer.ThemesEnabled = False
        ' 
        ' LblRandomHistoryUpdateInterval1
        ' 
        LblRandomHistoryUpdateInterval1.AutoSize = True
        LblRandomHistoryUpdateInterval1.Font = New Font("Segoe UI", 12F)
        LblRandomHistoryUpdateInterval1.Location = New Point(14, 296)
        LblRandomHistoryUpdateInterval1.Name = "LblRandomHistoryUpdateInterval1"
        LblRandomHistoryUpdateInterval1.Size = New Size(345, 21)
        LblRandomHistoryUpdateInterval1.TabIndex = 152
        LblRandomHistoryUpdateInterval1.Text = "Update Shuffle Play History After Song Plays For"
        LblRandomHistoryUpdateInterval1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblRandomHistoryUpdateInterval2
        ' 
        LblRandomHistoryUpdateInterval2.AutoSize = True
        LblRandomHistoryUpdateInterval2.Font = New Font("Segoe UI", 12F)
        LblRandomHistoryUpdateInterval2.Location = New Point(397, 296)
        LblRandomHistoryUpdateInterval2.Name = "LblRandomHistoryUpdateInterval2"
        LblRandomHistoryUpdateInterval2.Size = New Size(68, 21)
        LblRandomHistoryUpdateInterval2.TabIndex = 153
        LblRandomHistoryUpdateInterval2.Text = "Seconds"
        LblRandomHistoryUpdateInterval2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TPPlaylist
        ' 
        TPPlaylist.BorderStyle = BorderStyle.Fixed3D
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
        TPPlaylist.Image = Nothing
        TPPlaylist.ImageSize = New Size(16, 16)
        TPPlaylist.Location = New Point(1, 42)
        TPPlaylist.Name = "TPPlaylist"
        TPPlaylist.ShowCloseButton = True
        TPPlaylist.Size = New Size(815, 413)
        TPPlaylist.TabIndex = 3
        TPPlaylist.Text = " Playlist "
        TPPlaylist.ThemesEnabled = False
        ' 
        ' TPLibrary
        ' 
        TPLibrary.BorderStyle = BorderStyle.Fixed3D
        TPLibrary.Controls.Add(BtnLibrarySearchFoldersAdd)
        TPLibrary.Controls.Add(LBLibrarySearchFolders)
        TPLibrary.Controls.Add(LblLibrarySearchFolders)
        TPLibrary.Controls.Add(CkBoxLibrarySearchSubFolders)
        TPLibrary.Image = Nothing
        TPLibrary.ImageSize = New Size(16, 16)
        TPLibrary.Location = New Point(1, 42)
        TPLibrary.Name = "TPLibrary"
        TPLibrary.ShowCloseButton = True
        TPLibrary.Size = New Size(815, 413)
        TPLibrary.TabIndex = 4
        TPLibrary.Text = " Library "
        TPLibrary.ThemesEnabled = False
        ' 
        ' LblLibrarySearchFolders
        ' 
        LblLibrarySearchFolders.AutoSize = True
        LblLibrarySearchFolders.Location = New Point(136, 18)
        LblLibrarySearchFolders.Name = "LblLibrarySearchFolders"
        LblLibrarySearchFolders.Size = New Size(165, 21)
        LblLibrarySearchFolders.TabIndex = 83
        LblLibrarySearchFolders.Text = "Library Search Folders"
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
        KeyPreview = True
        MaximizeBox = False
        MinimizeBox = False
        Name = "Options"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
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
    Friend WithEvents TipOptions As ToolTip
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
End Class
