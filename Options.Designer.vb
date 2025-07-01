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
        GrBoxPlaylistFormatting = New GroupBox()
        TxtBoxPlaylistVideoIdentifier = New TextBox()
        Labelcsy2 = New Components.LabelCSY()
        Labelcsy3 = New Components.LabelCSY()
        Labelcsy4 = New Components.LabelCSY()
        GrBoxLibrarySearchFolders = New GroupBox()
        BtnLibrarySearchFoldersAdd = New Button()
        CkBoxLibrarySearchSubFolders = New CheckBox()
        LBLibrarySearchFolders = New ListBox()
        CMLibrarySearchFolders = New ContextMenuStrip(components)
        CMILibrarySearchFoldersAdd = New ToolStripMenuItem()
        CMILibrarySearchFoldersRemove = New ToolStripMenuItem()
        CkBoxSaveWindowMetrics = New CheckBox()
        CkBoxSuspendOnSessionChange = New CheckBox()
        TxtBoxHelperApp1Name = New TextBox()
        TxtBoxHelperApp1Path = New TextBox()
        BtnHelperApp1 = New Button()
        BtnHelperApp2 = New Button()
        TxtBoxHelperApp2Path = New TextBox()
        TxtBoxHelperApp2Name = New TextBox()
        CMTxtBox = New Components.TextBoxContextMenu()
        LblSongPlayMode = New Components.LabelCSY()
        LblHelperApp1Name = New Components.LabelCSY()
        LblHelperApp1Path = New Components.LabelCSY()
        LblHelperApp2Name = New Components.LabelCSY()
        LblHelperApp2Path = New Components.LabelCSY()
        CoBoxPlaylistDefaultAction = New ComboBox()
        CoBoxPlaylistSearchAction = New ComboBox()
        LblDefaultPlaylistAction = New Components.LabelCSY()
        LblPlaylistSearchAction = New Components.LabelCSY()
        CoBoxTheme = New ComboBox()
        LblTheme = New Components.LabelCSY()
        TipOptions = New ToolTip(components)
        GrBoxTime.SuspendLayout()
        GrBoxPlaylistFormatting.SuspendLayout()
        GrBoxLibrarySearchFolders.SuspendLayout()
        CMLibrarySearchFolders.SuspendLayout()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(389, 396)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 100
        TipOptions.SetToolTip(BtnOK, "Save & Close Window")
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' RadBtnElapsed
        ' 
        RadBtnElapsed.AutoSize = True
        RadBtnElapsed.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        RadBtnElapsed.Location = New Point(6, 22)
        RadBtnElapsed.Name = "RadBtnElapsed"
        RadBtnElapsed.Size = New Size(145, 21)
        RadBtnElapsed.TabIndex = 1
        RadBtnElapsed.TabStop = True
        RadBtnElapsed.Text = "Show Elapsed Time"
        RadBtnElapsed.UseVisualStyleBackColor = True
        ' 
        ' RadBtnRemaining
        ' 
        RadBtnRemaining.AutoSize = True
        RadBtnRemaining.BackColor = SystemColors.Control
        RadBtnRemaining.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        RadBtnRemaining.Location = New Point(6, 47)
        RadBtnRemaining.Name = "RadBtnRemaining"
        RadBtnRemaining.Size = New Size(164, 21)
        RadBtnRemaining.TabIndex = 2
        RadBtnRemaining.TabStop = True
        RadBtnRemaining.Text = "Show Remaining Time"
        RadBtnRemaining.UseVisualStyleBackColor = False
        ' 
        ' GrBoxTime
        ' 
        GrBoxTime.BackColor = SystemColors.Control
        GrBoxTime.Controls.Add(RadBtnElapsed)
        GrBoxTime.Controls.Add(RadBtnRemaining)
        GrBoxTime.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        GrBoxTime.Location = New Point(12, 12)
        GrBoxTime.Name = "GrBoxTime"
        GrBoxTime.Size = New Size(176, 76)
        GrBoxTime.TabIndex = 0
        GrBoxTime.TabStop = False
        GrBoxTime.Text = "Song Position Display"
        ' 
        ' CoBoxPlayMode
        ' 
        CoBoxPlayMode.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlayMode.FlatStyle = FlatStyle.Flat
        CoBoxPlayMode.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CoBoxPlayMode.FormattingEnabled = True
        CoBoxPlayMode.Location = New Point(12, 127)
        CoBoxPlayMode.Name = "CoBoxPlayMode"
        CoBoxPlayMode.Size = New Size(151, 25)
        CoBoxPlayMode.TabIndex = 3
        ' 
        ' CoBoxPlaylistTitleFormat
        ' 
        CoBoxPlaylistTitleFormat.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlaylistTitleFormat.FlatStyle = FlatStyle.Flat
        CoBoxPlaylistTitleFormat.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CoBoxPlaylistTitleFormat.FormattingEnabled = True
        CoBoxPlaylistTitleFormat.Location = New Point(6, 39)
        CoBoxPlaylistTitleFormat.Name = "CoBoxPlaylistTitleFormat"
        CoBoxPlaylistTitleFormat.Size = New Size(167, 25)
        CoBoxPlaylistTitleFormat.TabIndex = 12
        ' 
        ' TxtBoxPlaylistTitleSeparator
        ' 
        TxtBoxPlaylistTitleSeparator.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TxtBoxPlaylistTitleSeparator.Location = New Point(6, 104)
        TxtBoxPlaylistTitleSeparator.Name = "TxtBoxPlaylistTitleSeparator"
        TxtBoxPlaylistTitleSeparator.ShortcutsEnabled = False
        TxtBoxPlaylistTitleSeparator.Size = New Size(167, 25)
        TxtBoxPlaylistTitleSeparator.TabIndex = 14
        ' 
        ' CkBoxPlaylistRemoveSpaces
        ' 
        CkBoxPlaylistRemoveSpaces.AutoSize = True
        CkBoxPlaylistRemoveSpaces.FlatStyle = FlatStyle.Flat
        CkBoxPlaylistRemoveSpaces.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CkBoxPlaylistRemoveSpaces.Location = New Point(7, 67)
        CkBoxPlaylistRemoveSpaces.Name = "CkBoxPlaylistRemoveSpaces"
        CkBoxPlaylistRemoveSpaces.Size = New Size(118, 21)
        CkBoxPlaylistRemoveSpaces.TabIndex = 13
        CkBoxPlaylistRemoveSpaces.Text = "Remove Spaces"
        CkBoxPlaylistRemoveSpaces.UseVisualStyleBackColor = True
        ' 
        ' GrBoxPlaylistFormatting
        ' 
        GrBoxPlaylistFormatting.BackColor = SystemColors.Control
        GrBoxPlaylistFormatting.Controls.Add(CkBoxPlaylistRemoveSpaces)
        GrBoxPlaylistFormatting.Controls.Add(TxtBoxPlaylistVideoIdentifier)
        GrBoxPlaylistFormatting.Controls.Add(CoBoxPlaylistTitleFormat)
        GrBoxPlaylistFormatting.Controls.Add(TxtBoxPlaylistTitleSeparator)
        GrBoxPlaylistFormatting.Controls.Add(Labelcsy2)
        GrBoxPlaylistFormatting.Controls.Add(Labelcsy3)
        GrBoxPlaylistFormatting.Controls.Add(Labelcsy4)
        GrBoxPlaylistFormatting.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        GrBoxPlaylistFormatting.Location = New Point(194, 12)
        GrBoxPlaylistFormatting.Name = "GrBoxPlaylistFormatting"
        GrBoxPlaylistFormatting.Size = New Size(179, 178)
        GrBoxPlaylistFormatting.TabIndex = 13
        GrBoxPlaylistFormatting.TabStop = False
        GrBoxPlaylistFormatting.Text = "Playlist Formatting"
        ' 
        ' TxtBoxPlaylistVideoIdentifier
        ' 
        TxtBoxPlaylistVideoIdentifier.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TxtBoxPlaylistVideoIdentifier.Location = New Point(6, 147)
        TxtBoxPlaylistVideoIdentifier.Name = "TxtBoxPlaylistVideoIdentifier"
        TxtBoxPlaylistVideoIdentifier.ShortcutsEnabled = False
        TxtBoxPlaylistVideoIdentifier.Size = New Size(167, 25)
        TxtBoxPlaylistVideoIdentifier.TabIndex = 15
        ' 
        ' Labelcsy2
        ' 
        Labelcsy2.AutoSize = True
        Labelcsy2.CopyOnDoubleClick = False
        Labelcsy2.Location = New Point(6, 23)
        Labelcsy2.Name = "Labelcsy2"
        Labelcsy2.Size = New Size(77, 17)
        Labelcsy2.TabIndex = 132
        Labelcsy2.Text = "Title Format"
        ' 
        ' Labelcsy3
        ' 
        Labelcsy3.AutoSize = True
        Labelcsy3.CopyOnDoubleClick = False
        Labelcsy3.Location = New Point(7, 88)
        Labelcsy3.Name = "Labelcsy3"
        Labelcsy3.Size = New Size(94, 17)
        Labelcsy3.TabIndex = 132
        Labelcsy3.Text = "Title Separator"
        ' 
        ' Labelcsy4
        ' 
        Labelcsy4.AutoSize = True
        Labelcsy4.CopyOnDoubleClick = False
        Labelcsy4.Location = New Point(6, 131)
        Labelcsy4.Name = "Labelcsy4"
        Labelcsy4.Size = New Size(97, 17)
        Labelcsy4.TabIndex = 133
        Labelcsy4.Text = "Video Identifier"
        ' 
        ' GrBoxLibrarySearchFolders
        ' 
        GrBoxLibrarySearchFolders.BackColor = SystemColors.Control
        GrBoxLibrarySearchFolders.Controls.Add(BtnLibrarySearchFoldersAdd)
        GrBoxLibrarySearchFolders.Controls.Add(CkBoxLibrarySearchSubFolders)
        GrBoxLibrarySearchFolders.Controls.Add(LBLibrarySearchFolders)
        GrBoxLibrarySearchFolders.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        GrBoxLibrarySearchFolders.Location = New Point(379, 12)
        GrBoxLibrarySearchFolders.Name = "GrBoxLibrarySearchFolders"
        GrBoxLibrarySearchFolders.Size = New Size(450, 171)
        GrBoxLibrarySearchFolders.TabIndex = 20
        GrBoxLibrarySearchFolders.TabStop = False
        GrBoxLibrarySearchFolders.Text = "Library Search Folders"
        ' 
        ' BtnLibrarySearchFoldersAdd
        ' 
        BtnLibrarySearchFoldersAdd.Image = My.Resources.Resources.ImageAdd16
        BtnLibrarySearchFoldersAdd.Location = New Point(412, 134)
        BtnLibrarySearchFoldersAdd.Name = "BtnLibrarySearchFoldersAdd"
        BtnLibrarySearchFoldersAdd.Size = New Size(32, 32)
        BtnLibrarySearchFoldersAdd.TabIndex = 24
        TipOptions.SetToolTip(BtnLibrarySearchFoldersAdd, "Add Folder")
        BtnLibrarySearchFoldersAdd.UseVisualStyleBackColor = True
        ' 
        ' CkBoxLibrarySearchSubFolders
        ' 
        CkBoxLibrarySearchSubFolders.AutoSize = True
        CkBoxLibrarySearchSubFolders.BackColor = SystemColors.Control
        CkBoxLibrarySearchSubFolders.FlatStyle = FlatStyle.Flat
        CkBoxLibrarySearchSubFolders.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CkBoxLibrarySearchSubFolders.Location = New Point(6, 138)
        CkBoxLibrarySearchSubFolders.Name = "CkBoxLibrarySearchSubFolders"
        CkBoxLibrarySearchSubFolders.Size = New Size(141, 21)
        CkBoxLibrarySearchSubFolders.TabIndex = 23
        CkBoxLibrarySearchSubFolders.Text = "Search Sub-Folders"
        CkBoxLibrarySearchSubFolders.UseVisualStyleBackColor = False
        ' 
        ' LBLibrarySearchFolders
        ' 
        LBLibrarySearchFolders.BackColor = SystemColors.Window
        LBLibrarySearchFolders.ContextMenuStrip = CMLibrarySearchFolders
        LBLibrarySearchFolders.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LBLibrarySearchFolders.FormattingEnabled = True
        LBLibrarySearchFolders.ItemHeight = 17
        LBLibrarySearchFolders.Location = New Point(6, 22)
        LBLibrarySearchFolders.Name = "LBLibrarySearchFolders"
        LBLibrarySearchFolders.Size = New Size(438, 106)
        LBLibrarySearchFolders.Sorted = True
        LBLibrarySearchFolders.TabIndex = 20
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
        CkBoxSaveWindowMetrics.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        CkBoxSaveWindowMetrics.Location = New Point(12, 294)
        CkBoxSaveWindowMetrics.Name = "CkBoxSaveWindowMetrics"
        CkBoxSaveWindowMetrics.Size = New Size(156, 21)
        CkBoxSaveWindowMetrics.TabIndex = 8
        CkBoxSaveWindowMetrics.Text = "Save Window Metrics"
        CkBoxSaveWindowMetrics.UseVisualStyleBackColor = True
        ' 
        ' CkBoxSuspendOnSessionChange
        ' 
        CkBoxSuspendOnSessionChange.AutoSize = True
        CkBoxSuspendOnSessionChange.FlatStyle = FlatStyle.Flat
        CkBoxSuspendOnSessionChange.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        CkBoxSuspendOnSessionChange.Location = New Point(12, 315)
        CkBoxSuspendOnSessionChange.Name = "CkBoxSuspendOnSessionChange"
        CkBoxSuspendOnSessionChange.Size = New Size(208, 21)
        CkBoxSuspendOnSessionChange.TabIndex = 9
        CkBoxSuspendOnSessionChange.Text = "Minimize App On Screen Lock"
        CkBoxSuspendOnSessionChange.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxHelperApp1Name
        ' 
        TxtBoxHelperApp1Name.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        TxtBoxHelperApp1Name.Location = New Point(379, 216)
        TxtBoxHelperApp1Name.Name = "TxtBoxHelperApp1Name"
        TxtBoxHelperApp1Name.ShortcutsEnabled = False
        TxtBoxHelperApp1Name.Size = New Size(156, 25)
        TxtBoxHelperApp1Name.TabIndex = 30
        ' 
        ' TxtBoxHelperApp1Path
        ' 
        TxtBoxHelperApp1Path.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        TxtBoxHelperApp1Path.ForeColor = Color.White
        TxtBoxHelperApp1Path.Location = New Point(379, 258)
        TxtBoxHelperApp1Path.Name = "TxtBoxHelperApp1Path"
        TxtBoxHelperApp1Path.ShortcutsEnabled = False
        TxtBoxHelperApp1Path.Size = New Size(420, 25)
        TxtBoxHelperApp1Path.TabIndex = 31
        ' 
        ' BtnHelperApp1
        ' 
        BtnHelperApp1.Image = My.Resources.Resources.ImageGetPath16
        BtnHelperApp1.Location = New Point(799, 254)
        BtnHelperApp1.Name = "BtnHelperApp1"
        BtnHelperApp1.Size = New Size(32, 32)
        BtnHelperApp1.TabIndex = 32
        TipOptions.SetToolTip(BtnHelperApp1, "Select Helper App")
        BtnHelperApp1.UseVisualStyleBackColor = True
        ' 
        ' BtnHelperApp2
        ' 
        BtnHelperApp2.Image = My.Resources.Resources.ImageGetPath16
        BtnHelperApp2.Location = New Point(799, 351)
        BtnHelperApp2.Name = "BtnHelperApp2"
        BtnHelperApp2.Size = New Size(32, 32)
        BtnHelperApp2.TabIndex = 37
        TipOptions.SetToolTip(BtnHelperApp2, "Select Helper App")
        BtnHelperApp2.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxHelperApp2Path
        ' 
        TxtBoxHelperApp2Path.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        TxtBoxHelperApp2Path.Location = New Point(379, 355)
        TxtBoxHelperApp2Path.Name = "TxtBoxHelperApp2Path"
        TxtBoxHelperApp2Path.ShortcutsEnabled = False
        TxtBoxHelperApp2Path.Size = New Size(420, 25)
        TxtBoxHelperApp2Path.TabIndex = 36
        ' 
        ' TxtBoxHelperApp2Name
        ' 
        TxtBoxHelperApp2Name.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        TxtBoxHelperApp2Name.Location = New Point(379, 314)
        TxtBoxHelperApp2Name.Name = "TxtBoxHelperApp2Name"
        TxtBoxHelperApp2Name.ShortcutsEnabled = False
        TxtBoxHelperApp2Name.Size = New Size(156, 25)
        TxtBoxHelperApp2Name.TabIndex = 35
        ' 
        ' CMTxtBox
        ' 
        CMTxtBox.Name = "CMTxtBox"
        CMTxtBox.Size = New Size(123, 148)
        ' 
        ' LblSongPlayMode
        ' 
        LblSongPlayMode.AutoSize = True
        LblSongPlayMode.CopyOnDoubleClick = False
        LblSongPlayMode.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblSongPlayMode.Location = New Point(12, 109)
        LblSongPlayMode.Name = "LblSongPlayMode"
        LblSongPlayMode.Size = New Size(104, 17)
        LblSongPlayMode.TabIndex = 131
        LblSongPlayMode.Text = "Song Play Mode"
        ' 
        ' LblHelperApp1Name
        ' 
        LblHelperApp1Name.AutoSize = True
        LblHelperApp1Name.CopyOnDoubleClick = False
        LblHelperApp1Name.Font = New Font("Segoe UI", 9.75F)
        LblHelperApp1Name.Location = New Point(379, 198)
        LblHelperApp1Name.Name = "LblHelperApp1Name"
        LblHelperApp1Name.Size = New Size(141, 17)
        LblHelperApp1Name.TabIndex = 132
        LblHelperApp1Name.Text = "Name of Helper App 1"
        ' 
        ' LblHelperApp1Path
        ' 
        LblHelperApp1Path.AutoSize = True
        LblHelperApp1Path.CopyOnDoubleClick = False
        LblHelperApp1Path.Font = New Font("Segoe UI", 9.75F)
        LblHelperApp1Path.Location = New Point(379, 240)
        LblHelperApp1Path.Name = "LblHelperApp1Path"
        LblHelperApp1Path.Size = New Size(131, 17)
        LblHelperApp1Path.TabIndex = 133
        LblHelperApp1Path.Text = "Path to Helper App 1"
        ' 
        ' LblHelperApp2Name
        ' 
        LblHelperApp2Name.AutoSize = True
        LblHelperApp2Name.CopyOnDoubleClick = False
        LblHelperApp2Name.Font = New Font("Segoe UI", 9.75F)
        LblHelperApp2Name.Location = New Point(379, 296)
        LblHelperApp2Name.Name = "LblHelperApp2Name"
        LblHelperApp2Name.Size = New Size(141, 17)
        LblHelperApp2Name.TabIndex = 134
        LblHelperApp2Name.Text = "Name of Helper App 2"
        ' 
        ' LblHelperApp2Path
        ' 
        LblHelperApp2Path.AutoSize = True
        LblHelperApp2Path.CopyOnDoubleClick = False
        LblHelperApp2Path.Font = New Font("Segoe UI", 9.75F)
        LblHelperApp2Path.Location = New Point(379, 337)
        LblHelperApp2Path.Name = "LblHelperApp2Path"
        LblHelperApp2Path.Size = New Size(131, 17)
        LblHelperApp2Path.TabIndex = 135
        LblHelperApp2Path.Text = "Path to Helper App 2"
        ' 
        ' CoBoxPlaylistDefaultAction
        ' 
        CoBoxPlaylistDefaultAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlaylistDefaultAction.FlatStyle = FlatStyle.Flat
        CoBoxPlaylistDefaultAction.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CoBoxPlaylistDefaultAction.FormattingEnabled = True
        CoBoxPlaylistDefaultAction.Location = New Point(12, 216)
        CoBoxPlaylistDefaultAction.Name = "CoBoxPlaylistDefaultAction"
        CoBoxPlaylistDefaultAction.Size = New Size(151, 25)
        CoBoxPlaylistDefaultAction.TabIndex = 5
        ' 
        ' CoBoxPlaylistSearchAction
        ' 
        CoBoxPlaylistSearchAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPlaylistSearchAction.FlatStyle = FlatStyle.Flat
        CoBoxPlaylistSearchAction.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CoBoxPlaylistSearchAction.FormattingEnabled = True
        CoBoxPlaylistSearchAction.Location = New Point(12, 264)
        CoBoxPlaylistSearchAction.Name = "CoBoxPlaylistSearchAction"
        CoBoxPlaylistSearchAction.Size = New Size(151, 25)
        CoBoxPlaylistSearchAction.TabIndex = 6
        ' 
        ' LblDefaultPlaylistAction
        ' 
        LblDefaultPlaylistAction.AutoSize = True
        LblDefaultPlaylistAction.CopyOnDoubleClick = False
        LblDefaultPlaylistAction.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblDefaultPlaylistAction.Location = New Point(12, 198)
        LblDefaultPlaylistAction.Name = "LblDefaultPlaylistAction"
        LblDefaultPlaylistAction.Size = New Size(132, 17)
        LblDefaultPlaylistAction.TabIndex = 138
        LblDefaultPlaylistAction.Text = "Default Playlist Action"
        ' 
        ' LblPlaylistSearchAction
        ' 
        LblPlaylistSearchAction.AutoSize = True
        LblPlaylistSearchAction.CopyOnDoubleClick = False
        LblPlaylistSearchAction.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblPlaylistSearchAction.Location = New Point(12, 246)
        LblPlaylistSearchAction.Name = "LblPlaylistSearchAction"
        LblPlaylistSearchAction.Size = New Size(130, 17)
        LblPlaylistSearchAction.TabIndex = 139
        LblPlaylistSearchAction.Text = "Playlist Search Action"
        ' 
        ' CoBoxTheme
        ' 
        CoBoxTheme.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxTheme.FlatStyle = FlatStyle.Flat
        CoBoxTheme.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CoBoxTheme.FormattingEnabled = True
        CoBoxTheme.Location = New Point(12, 355)
        CoBoxTheme.Name = "CoBoxTheme"
        CoBoxTheme.Size = New Size(151, 25)
        CoBoxTheme.TabIndex = 10
        ' 
        ' LblTheme
        ' 
        LblTheme.AutoSize = True
        LblTheme.CopyOnDoubleClick = False
        LblTheme.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblTheme.Location = New Point(12, 337)
        LblTheme.Name = "LblTheme"
        LblTheme.Size = New Size(47, 17)
        LblTheme.TabIndex = 141
        LblTheme.Text = "Theme"
        ' 
        ' Options
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(842, 472)
        Controls.Add(CoBoxTheme)
        Controls.Add(LblTheme)
        Controls.Add(LblPlaylistSearchAction)
        Controls.Add(CoBoxPlaylistSearchAction)
        Controls.Add(CoBoxPlaylistDefaultAction)
        Controls.Add(BtnHelperApp2)
        Controls.Add(TxtBoxHelperApp2Path)
        Controls.Add(TxtBoxHelperApp2Name)
        Controls.Add(BtnHelperApp1)
        Controls.Add(TxtBoxHelperApp1Path)
        Controls.Add(TxtBoxHelperApp1Name)
        Controls.Add(CkBoxSuspendOnSessionChange)
        Controls.Add(CkBoxSaveWindowMetrics)
        Controls.Add(GrBoxLibrarySearchFolders)
        Controls.Add(GrBoxPlaylistFormatting)
        Controls.Add(CoBoxPlayMode)
        Controls.Add(GrBoxTime)
        Controls.Add(BtnOK)
        Controls.Add(LblSongPlayMode)
        Controls.Add(LblHelperApp2Path)
        Controls.Add(LblHelperApp2Name)
        Controls.Add(LblHelperApp1Path)
        Controls.Add(LblHelperApp1Name)
        Controls.Add(LblDefaultPlaylistAction)
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
        GrBoxPlaylistFormatting.ResumeLayout(False)
        GrBoxPlaylistFormatting.PerformLayout()
        GrBoxLibrarySearchFolders.ResumeLayout(False)
        GrBoxLibrarySearchFolders.PerformLayout()
        CMLibrarySearchFolders.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents RadBtnElapsed As RadioButton
    Friend WithEvents RadBtnRemaining As RadioButton
    Friend WithEvents GrBoxTime As GroupBox
    Friend WithEvents CoBoxPlayMode As ComboBox
    Friend WithEvents CoBoxPlaylistTitleFormat As ComboBox
    Friend WithEvents TxtBoxPlaylistTitleSeparator As TextBox
    Friend WithEvents CkBoxPlaylistRemoveSpaces As CheckBox
    Friend WithEvents GrBoxPlaylistFormatting As GroupBox
    Friend WithEvents TxtBoxPlaylistVideoIdentifier As TextBox
    Friend WithEvents GrBoxLibrarySearchFolders As GroupBox
    Friend WithEvents LBLibrarySearchFolders As ListBox
    Friend WithEvents CkBoxLibrarySearchSubFolders As CheckBox
    Friend WithEvents BtnLibrarySearchFoldersAdd As Button
    Friend WithEvents CMLibrarySearchFolders As ContextMenuStrip
    Friend WithEvents CMILibrarySearchFoldersAdd As ToolStripMenuItem
    Friend WithEvents CMILibrarySearchFoldersRemove As ToolStripMenuItem
    Friend WithEvents CkBoxSaveWindowMetrics As CheckBox
    Friend WithEvents CkBoxSuspendOnSessionChange As CheckBox
    Friend WithEvents TxtBoxHelperApp1Name As TextBox
    Friend WithEvents TxtBoxHelperApp1Path As TextBox
    Friend WithEvents BtnHelperApp1 As Button
    Friend WithEvents BtnHelperApp2 As Button
    Friend WithEvents TxtBoxHelperApp2Path As TextBox
    Friend WithEvents TxtBoxHelperApp2Name As TextBox
    Friend WithEvents CMTxtBox As My.Components.TextBoxContextMenu
    Friend WithEvents LblSongPlayMode As My.Components.LabelCSY
    Friend WithEvents Labelcsy2 As My.Components.LabelCSY
    Friend WithEvents Labelcsy3 As My.Components.LabelCSY
    Friend WithEvents Labelcsy4 As My.Components.LabelCSY
    Friend WithEvents LblHelperApp1Name As My.Components.LabelCSY
    Friend WithEvents LblHelperApp1Path As My.Components.LabelCSY
    Friend WithEvents LblHelperApp2Name As My.Components.LabelCSY
    Friend WithEvents LblHelperApp2Path As My.Components.LabelCSY
    Friend WithEvents CoBoxPlaylistDefaultAction As ComboBox
    Friend WithEvents CoBoxPlaylistSearchAction As ComboBox
    Friend WithEvents LblDefaultPlaylistAction As My.Components.LabelCSY
    Friend WithEvents LblPlaylistSearchAction As My.Components.LabelCSY
    Friend WithEvents CoBoxTheme As ComboBox
    Friend WithEvents LblTheme As Components.LabelCSY
    Friend WithEvents TipOptions As ToolTip
End Class
