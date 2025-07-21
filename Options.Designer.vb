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
        CkBoxPlaylistRemoveSpaces = New CheckBox()
        TxtBoxPlaylistVideoIdentifier = New TextBox()
        LblTitleFormat = New Components.LabelCSY()
        LblTitleSeparator = New Components.LabelCSY()
        LblVideoIdentifier = New Components.LabelCSY()
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
        CMTxtBox = New Components.TextBoxContextMenu()
        LblSongPlayMode = New Components.LabelCSY()
        LblHelperApp1Name = New Components.LabelCSY()
        CoBoxPlaylistDefaultAction = New ComboBox()
        CoBoxPlaylistSearchAction = New ComboBox()
        LblDefaultPlaylistAction = New Components.LabelCSY()
        LblPlaylistSearchAction = New Components.LabelCSY()
        CoBoxTheme = New ComboBox()
        LblTheme = New Components.LabelCSY()
        TipOptions = New ToolTip(components)
        TxtBoxHistoryAutoSaveInterval = New TextBox()
        LblHistoryAutoSaveInterval1 = New Components.LabelCSY()
        LblHistoryAutoSaveInterval2 = New Components.LabelCSY()
        BtnHistorySaveNow = New Button()
        BtnHistoryPrune = New Button()
        TCOptions = New Syncfusion.Windows.Forms.Tools.TabControlAdv()
        TPApp = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        TxtBoxHelperApp2Path = New TextBox()
        TxtBoxHelperApp2Name = New TextBox()
        TxtBoxHelperApp1Path = New TextBox()
        TxtBoxHelperApp1Name = New TextBox()
        LblHelperApp2Path = New Components.LabelCSY()
        LblHelperApp2Name = New Components.LabelCSY()
        LblHelperApp1Path = New Components.LabelCSY()
        TPPlayer = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        TxtBoxHistoryUpdateInterval = New TextBox()
        LblHistoryUpdateInterval2 = New Components.LabelCSY()
        LblHistoryUpdateInterval1 = New Components.LabelCSY()
        TPPlaylist = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        LblPlaylistFormatting = New Label()
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
        ' 
        ' CoBoxPlaylistTitleFormat
        ' 
        CoBoxPlaylistTitleFormat.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlaylistTitleFormat.FlatStyle = FlatStyle.Flat
        CoBoxPlaylistTitleFormat.Font = New Font("Segoe UI", 12F)
        CoBoxPlaylistTitleFormat.FormattingEnabled = True
        CoBoxPlaylistTitleFormat.Location = New Point(19, 58)
        CoBoxPlaylistTitleFormat.Name = "CoBoxPlaylistTitleFormat"
        CoBoxPlaylistTitleFormat.Size = New Size(230, 29)
        CoBoxPlaylistTitleFormat.TabIndex = 10
        ' 
        ' TxtBoxPlaylistTitleSeparator
        ' 
        TxtBoxPlaylistTitleSeparator.Font = New Font("Segoe UI", 12F)
        TxtBoxPlaylistTitleSeparator.Location = New Point(18, 138)
        TxtBoxPlaylistTitleSeparator.Name = "TxtBoxPlaylistTitleSeparator"
        TxtBoxPlaylistTitleSeparator.ShortcutsEnabled = False
        TxtBoxPlaylistTitleSeparator.Size = New Size(137, 29)
        TxtBoxPlaylistTitleSeparator.TabIndex = 18
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
        CkBoxPlaylistRemoveSpaces.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxPlaylistVideoIdentifier
        ' 
        TxtBoxPlaylistVideoIdentifier.Font = New Font("Segoe UI", 12F)
        TxtBoxPlaylistVideoIdentifier.Location = New Point(18, 192)
        TxtBoxPlaylistVideoIdentifier.Name = "TxtBoxPlaylistVideoIdentifier"
        TxtBoxPlaylistVideoIdentifier.ShortcutsEnabled = False
        TxtBoxPlaylistVideoIdentifier.Size = New Size(137, 29)
        TxtBoxPlaylistVideoIdentifier.TabIndex = 22
        ' 
        ' LblTitleFormat
        ' 
        LblTitleFormat.AutoSize = True
        LblTitleFormat.CopyOnDoubleClick = False
        LblTitleFormat.Location = New Point(19, 38)
        LblTitleFormat.Name = "LblTitleFormat"
        LblTitleFormat.Size = New Size(93, 21)
        LblTitleFormat.TabIndex = 132
        LblTitleFormat.Text = "Title Format"
        ' 
        ' LblTitleSeparator
        ' 
        LblTitleSeparator.AutoSize = True
        LblTitleSeparator.CopyOnDoubleClick = False
        LblTitleSeparator.Location = New Point(19, 117)
        LblTitleSeparator.Name = "LblTitleSeparator"
        LblTitleSeparator.Size = New Size(111, 21)
        LblTitleSeparator.TabIndex = 132
        LblTitleSeparator.Text = "Title Separator"
        ' 
        ' LblVideoIdentifier
        ' 
        LblVideoIdentifier.AutoSize = True
        LblVideoIdentifier.CopyOnDoubleClick = False
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
        BtnLibrarySearchFoldersAdd.Size = New Size(32, 32)
        BtnLibrarySearchFoldersAdd.TabIndex = 82
        TipOptions.SetToolTip(BtnLibrarySearchFoldersAdd, "Add Folder")
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
        CkBoxLibrarySearchSubFolders.UseVisualStyleBackColor = False
        ' 
        ' LBLibrarySearchFolders
        ' 
        LBLibrarySearchFolders.BackColor = SystemColors.Window
        LBLibrarySearchFolders.ContextMenuStrip = CMLibrarySearchFolders
        LBLibrarySearchFolders.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LBLibrarySearchFolders.FormattingEnabled = True
        LBLibrarySearchFolders.ItemHeight = 25
        LBLibrarySearchFolders.Location = New Point(137, 39)
        LBLibrarySearchFolders.Name = "LBLibrarySearchFolders"
        LBLibrarySearchFolders.Size = New Size(541, 104)
        LBLibrarySearchFolders.Sorted = True
        LBLibrarySearchFolders.TabIndex = 80
        ' 
        ' CMLibrarySearchFolders
        ' 
        CMLibrarySearchFolders.Items.AddRange(New ToolStripItem() {CMILibrarySearchFoldersAdd, CMILibrarySearchFoldersRemove})
        CMLibrarySearchFolders.Name = "CMLibrarySearchFolders"
        CMLibrarySearchFolders.Size = New Size(154, 48)
        ' 
        ' CMILibrarySearchFoldersAdd
        ' 
        CMILibrarySearchFoldersAdd.Image = My.Resources.Resources.ImageAdd16
        CMILibrarySearchFoldersAdd.Name = "CMILibrarySearchFoldersAdd"
        CMILibrarySearchFoldersAdd.Size = New Size(153, 22)
        CMILibrarySearchFoldersAdd.Text = "Add Folder"
        ' 
        ' CMILibrarySearchFoldersRemove
        ' 
        CMILibrarySearchFoldersRemove.Image = My.Resources.Resources.ImageClearRemoveDelete16
        CMILibrarySearchFoldersRemove.Name = "CMILibrarySearchFoldersRemove"
        CMILibrarySearchFoldersRemove.Size = New Size(153, 22)
        CMILibrarySearchFoldersRemove.Text = "Remove Folder"
        ' 
        ' CkBoxSaveWindowMetrics
        ' 
        CkBoxSaveWindowMetrics.AutoSize = True
        CkBoxSaveWindowMetrics.FlatStyle = FlatStyle.Flat
        CkBoxSaveWindowMetrics.Font = New Font("Segoe UI", 12F)
        CkBoxSaveWindowMetrics.Location = New Point(13, 182)
        CkBoxSaveWindowMetrics.Name = "CkBoxSaveWindowMetrics"
        CkBoxSaveWindowMetrics.Size = New Size(176, 25)
        CkBoxSaveWindowMetrics.TabIndex = 20
        CkBoxSaveWindowMetrics.Text = "Save Window Metrics"
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
        CkBoxSuspendOnSessionChange.UseVisualStyleBackColor = True
        ' 
        ' BtnHelperApp1
        ' 
        BtnHelperApp1.Image = My.Resources.Resources.ImageGetPath16
        BtnHelperApp1.Location = New Point(773, 81)
        BtnHelperApp1.Name = "BtnHelperApp1"
        BtnHelperApp1.Size = New Size(32, 32)
        BtnHelperApp1.TabIndex = 64
        TipOptions.SetToolTip(BtnHelperApp1, "Select Helper App")
        BtnHelperApp1.UseVisualStyleBackColor = True
        ' 
        ' BtnHelperApp2
        ' 
        BtnHelperApp2.Image = My.Resources.Resources.ImageGetPath16
        BtnHelperApp2.Location = New Point(773, 199)
        BtnHelperApp2.Name = "BtnHelperApp2"
        BtnHelperApp2.Size = New Size(32, 32)
        BtnHelperApp2.TabIndex = 70
        TipOptions.SetToolTip(BtnHelperApp2, "Select Helper App")
        BtnHelperApp2.UseVisualStyleBackColor = True
        ' 
        ' CMTxtBox
        ' 
        CMTxtBox.Name = "CMTxtBox"
        CMTxtBox.ShowExtendedTools = False
        CMTxtBox.Size = New Size(138, 176)
        ' 
        ' LblSongPlayMode
        ' 
        LblSongPlayMode.AutoSize = True
        LblSongPlayMode.CopyOnDoubleClick = False
        LblSongPlayMode.Font = New Font("Segoe UI", 12F)
        LblSongPlayMode.Location = New Point(14, 137)
        LblSongPlayMode.Name = "LblSongPlayMode"
        LblSongPlayMode.Size = New Size(123, 21)
        LblSongPlayMode.TabIndex = 131
        LblSongPlayMode.Text = "Song Play Mode"
        ' 
        ' LblHelperApp1Name
        ' 
        LblHelperApp1Name.AutoSize = True
        LblHelperApp1Name.CopyOnDoubleClick = False
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
        ' 
        ' LblDefaultPlaylistAction
        ' 
        LblDefaultPlaylistAction.AutoSize = True
        LblDefaultPlaylistAction.CopyOnDoubleClick = False
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
        LblPlaylistSearchAction.CopyOnDoubleClick = False
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
        CoBoxTheme.Location = New Point(13, 32)
        CoBoxTheme.Name = "CoBoxTheme"
        CoBoxTheme.Size = New Size(196, 33)
        CoBoxTheme.TabIndex = 10
        ' 
        ' LblTheme
        ' 
        LblTheme.AutoSize = True
        LblTheme.CopyOnDoubleClick = False
        LblTheme.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblTheme.Location = New Point(13, 12)
        LblTheme.Name = "LblTheme"
        LblTheme.Size = New Size(57, 21)
        LblTheme.TabIndex = 141
        LblTheme.Text = "Theme"
        ' 
        ' TxtBoxHistoryAutoSaveInterval
        ' 
        TxtBoxHistoryAutoSaveInterval.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxHistoryAutoSaveInterval.Location = New Point(148, 301)
        TxtBoxHistoryAutoSaveInterval.Name = "TxtBoxHistoryAutoSaveInterval"
        TxtBoxHistoryAutoSaveInterval.ShortcutsEnabled = False
        TxtBoxHistoryAutoSaveInterval.Size = New Size(61, 29)
        TxtBoxHistoryAutoSaveInterval.TabIndex = 42
        TxtBoxHistoryAutoSaveInterval.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblHistoryAutoSaveInterval1
        ' 
        LblHistoryAutoSaveInterval1.AutoSize = True
        LblHistoryAutoSaveInterval1.CopyOnDoubleClick = False
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
        LblHistoryAutoSaveInterval2.CopyOnDoubleClick = False
        LblHistoryAutoSaveInterval2.Font = New Font("Segoe UI", 12F)
        LblHistoryAutoSaveInterval2.Location = New Point(206, 304)
        LblHistoryAutoSaveInterval2.Name = "LblHistoryAutoSaveInterval2"
        LblHistoryAutoSaveInterval2.Size = New Size(66, 21)
        LblHistoryAutoSaveInterval2.TabIndex = 144
        LblHistoryAutoSaveInterval2.Text = "Minutes"
        LblHistoryAutoSaveInterval2.TextAlign = ContentAlignment.MiddleRight
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
        BtnHistoryPrune.UseVisualStyleBackColor = True
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
        ' TxtBoxHelperApp2Path
        ' 
        TxtBoxHelperApp2Path.Font = New Font("Segoe UI", 12F)
        TxtBoxHelperApp2Path.Location = New Point(353, 200)
        TxtBoxHelperApp2Path.Name = "TxtBoxHelperApp2Path"
        TxtBoxHelperApp2Path.ShortcutsEnabled = False
        TxtBoxHelperApp2Path.Size = New Size(420, 29)
        TxtBoxHelperApp2Path.TabIndex = 68
        ' 
        ' TxtBoxHelperApp2Name
        ' 
        TxtBoxHelperApp2Name.Font = New Font("Segoe UI", 12F)
        TxtBoxHelperApp2Name.Location = New Point(353, 150)
        TxtBoxHelperApp2Name.Name = "TxtBoxHelperApp2Name"
        TxtBoxHelperApp2Name.ShortcutsEnabled = False
        TxtBoxHelperApp2Name.Size = New Size(202, 29)
        TxtBoxHelperApp2Name.TabIndex = 66
        ' 
        ' TxtBoxHelperApp1Path
        ' 
        TxtBoxHelperApp1Path.Font = New Font("Segoe UI", 12F)
        TxtBoxHelperApp1Path.ForeColor = Color.White
        TxtBoxHelperApp1Path.Location = New Point(353, 82)
        TxtBoxHelperApp1Path.Name = "TxtBoxHelperApp1Path"
        TxtBoxHelperApp1Path.ShortcutsEnabled = False
        TxtBoxHelperApp1Path.Size = New Size(420, 29)
        TxtBoxHelperApp1Path.TabIndex = 62
        ' 
        ' TxtBoxHelperApp1Name
        ' 
        TxtBoxHelperApp1Name.Font = New Font("Segoe UI", 12F)
        TxtBoxHelperApp1Name.Location = New Point(353, 31)
        TxtBoxHelperApp1Name.Name = "TxtBoxHelperApp1Name"
        TxtBoxHelperApp1Name.ShortcutsEnabled = False
        TxtBoxHelperApp1Name.Size = New Size(202, 29)
        TxtBoxHelperApp1Name.TabIndex = 60
        ' 
        ' LblHelperApp2Path
        ' 
        LblHelperApp2Path.AutoSize = True
        LblHelperApp2Path.CopyOnDoubleClick = False
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
        LblHelperApp2Name.CopyOnDoubleClick = False
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
        LblHelperApp1Path.CopyOnDoubleClick = False
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
        TPPlayer.Controls.Add(TxtBoxHistoryUpdateInterval)
        TPPlayer.Controls.Add(LblHistoryUpdateInterval2)
        TPPlayer.Controls.Add(GrBoxTime)
        TPPlayer.Controls.Add(CoBoxPlayMode)
        TPPlayer.Controls.Add(LblHistoryUpdateInterval1)
        TPPlayer.Controls.Add(LblSongPlayMode)
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
        ' TxtBoxHistoryUpdateInterval
        ' 
        TxtBoxHistoryUpdateInterval.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxHistoryUpdateInterval.Location = New Point(269, 229)
        TxtBoxHistoryUpdateInterval.Name = "TxtBoxHistoryUpdateInterval"
        TxtBoxHistoryUpdateInterval.ShortcutsEnabled = False
        TxtBoxHistoryUpdateInterval.Size = New Size(44, 29)
        TxtBoxHistoryUpdateInterval.TabIndex = 148
        TxtBoxHistoryUpdateInterval.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblHistoryUpdateInterval2
        ' 
        LblHistoryUpdateInterval2.AutoSize = True
        LblHistoryUpdateInterval2.CopyOnDoubleClick = False
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
        LblHistoryUpdateInterval1.CopyOnDoubleClick = False
        LblHistoryUpdateInterval1.Font = New Font("Segoe UI", 12F)
        LblHistoryUpdateInterval1.Location = New Point(14, 233)
        LblHistoryUpdateInterval1.Name = "LblHistoryUpdateInterval1"
        LblHistoryUpdateInterval1.Size = New Size(259, 21)
        LblHistoryUpdateInterval1.TabIndex = 149
        LblHistoryUpdateInterval1.Text = "Update History After Song Plays For"
        LblHistoryUpdateInterval1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TPPlaylist
        ' 
        TPPlaylist.BorderStyle = BorderStyle.Fixed3D
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
        ' LblPlaylistFormatting
        ' 
        LblPlaylistFormatting.AutoSize = True
        LblPlaylistFormatting.Font = New Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        LblPlaylistFormatting.Location = New Point(10, 10)
        LblPlaylistFormatting.Name = "LblPlaylistFormatting"
        LblPlaylistFormatting.Size = New Size(140, 21)
        LblPlaylistFormatting.TabIndex = 140
        LblPlaylistFormatting.Text = "Playlist Formatting"
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
    Friend WithEvents CMTxtBox As My.Components.TextBoxContextMenu
    Friend WithEvents LblSongPlayMode As My.Components.LabelCSY
    Friend WithEvents LblTitleFormat As My.Components.LabelCSY
    Friend WithEvents LblTitleSeparator As My.Components.LabelCSY
    Friend WithEvents LblVideoIdentifier As My.Components.LabelCSY
    Friend WithEvents LblHelperApp1Name As My.Components.LabelCSY
    Friend WithEvents CoBoxPlaylistDefaultAction As ComboBox
    Friend WithEvents CoBoxPlaylistSearchAction As ComboBox
    Friend WithEvents LblDefaultPlaylistAction As My.Components.LabelCSY
    Friend WithEvents LblPlaylistSearchAction As My.Components.LabelCSY
    Friend WithEvents CoBoxTheme As ComboBox
    Friend WithEvents LblTheme As Components.LabelCSY
    Friend WithEvents TipOptions As ToolTip
    Friend WithEvents TxtBoxHistoryAutoSaveInterval As TextBox
    Friend WithEvents LblHistoryAutoSaveInterval1 As Components.LabelCSY
    Friend WithEvents LblHistoryAutoSaveInterval2 As Components.LabelCSY
    Friend WithEvents BtnHistorySaveNow As Button
    Friend WithEvents BtnHistoryPrune As Button
    Friend WithEvents TCOptions As Syncfusion.Windows.Forms.Tools.TabControlAdv
    Friend WithEvents TPApp As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents TPPlayer As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents TPPlaylist As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents TPLibrary As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents TxtBoxHistoryUpdateInterval As TextBox
    Friend WithEvents LblHistoryUpdateInterval2 As Components.LabelCSY
    Friend WithEvents LblHistoryUpdateInterval1 As Components.LabelCSY
    Friend WithEvents LblLibrarySearchFolders As Label
    Friend WithEvents LblPlaylistFormatting As Label
    Friend WithEvents TxtBoxHelperApp2Path As TextBox
    Friend WithEvents TxtBoxHelperApp2Name As TextBox
    Friend WithEvents TxtBoxHelperApp1Path As TextBox
    Friend WithEvents TxtBoxHelperApp1Name As TextBox
    Friend WithEvents LblHelperApp2Path As Components.LabelCSY
    Friend WithEvents LblHelperApp2Name As Components.LabelCSY
    Friend WithEvents LblHelperApp1Path As Components.LabelCSY
End Class
