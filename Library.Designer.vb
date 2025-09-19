<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Library
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Library))
        CMLibrary = New ContextMenuStrip(components)
        CMIPlay = New ToolStripMenuItem()
        CMIQueue = New ToolStripMenuItem()
        CMIPlayWithWindows = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        CMIAddToPlaylist = New ToolStripMenuItem()
        CMIAddAllToPlaylist = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        CMIAddGroupToPlaylist = New ToolStripMenuItem()
        CMICollapseGroup = New ToolStripMenuItem()
        CMIExpandAllGroups = New ToolStripMenuItem()
        CMISeparatorGroupBy = New ToolStripSeparator()
        CMIHelperApp1 = New ToolStripMenuItem()
        CMIHelperApp2 = New ToolStripMenuItem()
        CMIOpenLocation = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        CMICopyTitle = New ToolStripMenuItem()
        CMICopyFileName = New ToolStripMenuItem()
        CMICopyFilePath = New ToolStripMenuItem()
        BtnSearchFolders = New Button()
        BtnOK = New Button()
        TxbxLibrarySearch = New TextBox()
        LBXLibrarySearch = New ListBox()
        PicBoxAlbumArt = New PictureBox()
        LblAlbumArtSelect = New Label()
        TipLibrary = New Skye.UI.ToolTip(components)
        RadBtnGroupByGenre = New RadioButton()
        GrpBoxGroupBy = New GroupBox()
        RadBtnGroupByType = New RadioButton()
        RadBtnGroupByYear = New RadioButton()
        RadBtnGroupByArtist = New RadioButton()
        RadBtnGroupByAlbum = New RadioButton()
        RadBtnGroupByNone = New RadioButton()
        LblExtTitle = New Skye.UI.Label()
        LblExtFileInfo = New Skye.UI.Label()
        LblExtProperties = New Skye.UI.Label()
        LblExtType = New Skye.UI.Label()
        LblLibraryCounts = New Skye.UI.Label()
        LblStatus = New Skye.UI.Label()
        LVLibrary = New ListView()
        LblHistory = New Skye.UI.Label()
        CMLibrary.SuspendLayout()
        CType(PicBoxAlbumArt, ComponentModel.ISupportInitialize).BeginInit()
        GrpBoxGroupBy.SuspendLayout()
        SuspendLayout()
        ' 
        ' CMLibrary
        ' 
        CMLibrary.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CMLibrary.Items.AddRange(New ToolStripItem() {CMIPlay, CMIQueue, CMIPlayWithWindows, ToolStripSeparator3, CMIAddToPlaylist, CMIAddAllToPlaylist, ToolStripSeparator1, CMIAddGroupToPlaylist, CMICollapseGroup, CMIExpandAllGroups, CMISeparatorGroupBy, CMIHelperApp1, CMIHelperApp2, CMIOpenLocation, ToolStripSeparator2, CMICopyTitle, CMICopyFileName, CMICopyFilePath})
        CMLibrary.Name = "CMLibrary"
        CMLibrary.Size = New Size(229, 392)
        TipLibrary.SetToolTipImage(CMLibrary, Nothing)
        ' 
        ' CMIPlay
        ' 
        CMIPlay.Image = My.Resources.Resources.ImagePlay
        CMIPlay.Name = "CMIPlay"
        CMIPlay.Size = New Size(228, 26)
        CMIPlay.Text = "Play"
        ' 
        ' CMIQueue
        ' 
        CMIQueue.Image = My.Resources.Resources.ImagePlay
        CMIQueue.Name = "CMIQueue"
        CMIQueue.Size = New Size(228, 26)
        CMIQueue.Text = "Queue"
        ' 
        ' CMIPlayWithWindows
        ' 
        CMIPlayWithWindows.Image = My.Resources.Resources.ImageWindows16
        CMIPlayWithWindows.Name = "CMIPlayWithWindows"
        CMIPlayWithWindows.Size = New Size(228, 26)
        CMIPlayWithWindows.Text = "Play In Windows"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(225, 6)
        ' 
        ' CMIAddToPlaylist
        ' 
        CMIAddToPlaylist.Image = My.Resources.Resources.ImageAdd16
        CMIAddToPlaylist.Name = "CMIAddToPlaylist"
        CMIAddToPlaylist.Size = New Size(228, 26)
        CMIAddToPlaylist.Text = "Add To Playlist"
        ' 
        ' CMIAddAllToPlaylist
        ' 
        CMIAddAllToPlaylist.Image = My.Resources.Resources.ImageAddAll16
        CMIAddAllToPlaylist.Name = "CMIAddAllToPlaylist"
        CMIAddAllToPlaylist.Size = New Size(228, 26)
        CMIAddAllToPlaylist.Text = "Add ALL To Playlist"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(225, 6)
        ' 
        ' CMIAddGroupToPlaylist
        ' 
        CMIAddGroupToPlaylist.Image = My.Resources.Resources.ImageAdd16
        CMIAddGroupToPlaylist.Name = "CMIAddGroupToPlaylist"
        CMIAddGroupToPlaylist.Size = New Size(228, 26)
        CMIAddGroupToPlaylist.Text = "Add Group To Playlist"
        ' 
        ' CMICollapseGroup
        ' 
        CMICollapseGroup.Image = My.Resources.Resources.ImageCollapse16
        CMICollapseGroup.Name = "CMICollapseGroup"
        CMICollapseGroup.Size = New Size(228, 26)
        CMICollapseGroup.Text = "Collapse Group"
        ' 
        ' CMIExpandAllGroups
        ' 
        CMIExpandAllGroups.Image = My.Resources.Resources.ImageExpandAll16
        CMIExpandAllGroups.Name = "CMIExpandAllGroups"
        CMIExpandAllGroups.Size = New Size(228, 26)
        CMIExpandAllGroups.Text = "Expand All"
        ' 
        ' CMISeparatorGroupBy
        ' 
        CMISeparatorGroupBy.Name = "CMISeparatorGroupBy"
        CMISeparatorGroupBy.Size = New Size(225, 6)
        ' 
        ' CMIHelperApp1
        ' 
        CMIHelperApp1.Image = My.Resources.Resources.ImageSkyeTag
        CMIHelperApp1.Name = "CMIHelperApp1"
        CMIHelperApp1.Size = New Size(228, 26)
        ' 
        ' CMIHelperApp2
        ' 
        CMIHelperApp2.Image = My.Resources.Resources.ImageMP3Tag16
        CMIHelperApp2.Name = "CMIHelperApp2"
        CMIHelperApp2.Size = New Size(228, 26)
        ' 
        ' CMIOpenLocation
        ' 
        CMIOpenLocation.Image = My.Resources.Resources.ImageOpen16
        CMIOpenLocation.Name = "CMIOpenLocation"
        CMIOpenLocation.Size = New Size(228, 26)
        CMIOpenLocation.Text = "Open File Location"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(225, 6)
        ' 
        ' CMICopyTitle
        ' 
        CMICopyTitle.Image = My.Resources.Resources.ImageCopy16
        CMICopyTitle.Name = "CMICopyTitle"
        CMICopyTitle.Size = New Size(228, 26)
        CMICopyTitle.Text = "Copy Title"
        ' 
        ' CMICopyFileName
        ' 
        CMICopyFileName.Image = My.Resources.Resources.ImageCopy16
        CMICopyFileName.Name = "CMICopyFileName"
        CMICopyFileName.Size = New Size(228, 26)
        CMICopyFileName.Text = "Copy File Name"
        ' 
        ' CMICopyFilePath
        ' 
        CMICopyFilePath.Image = My.Resources.Resources.ImageCopy16
        CMICopyFilePath.Name = "CMICopyFilePath"
        CMICopyFilePath.Size = New Size(228, 26)
        CMICopyFilePath.Text = "Copy File Path"
        ' 
        ' BtnSearchFolders
        ' 
        BtnSearchFolders.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnSearchFolders.Image = My.Resources.Resources.ImageSearchFolder
        BtnSearchFolders.ImageAlign = ContentAlignment.MiddleLeft
        BtnSearchFolders.Location = New Point(841, 394)
        BtnSearchFolders.Name = "BtnSearchFolders"
        BtnSearchFolders.Size = New Size(131, 36)
        BtnSearchFolders.TabIndex = 1
        BtnSearchFolders.TabStop = False
        BtnSearchFolders.Text = "Search Folders"
        BtnSearchFolders.TextAlign = ContentAlignment.MiddleRight
        TipLibrary.SetToolTipImage(BtnSearchFolders, Nothing)
        BtnSearchFolders.UseVisualStyleBackColor = True
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(883, 436)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 73)
        BtnOK.TabIndex = 3
        BtnOK.TabStop = False
        TipLibrary.SetToolTipImage(BtnOK, Nothing)
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' TxbxLibrarySearch
        ' 
        TxbxLibrarySearch.BorderStyle = BorderStyle.None
        TxbxLibrarySearch.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxbxLibrarySearch.ForeColor = SystemColors.InactiveCaption
        TxbxLibrarySearch.Location = New Point(14, 19)
        TxbxLibrarySearch.Name = "TxbxLibrarySearch"
        TxbxLibrarySearch.ShortcutsEnabled = False
        TxbxLibrarySearch.Size = New Size(153, 18)
        TxbxLibrarySearch.TabIndex = 4
        TxbxLibrarySearch.Text = "Search Library"
        TipLibrary.SetToolTipImage(TxbxLibrarySearch, Nothing)
        ' 
        ' LBXLibrarySearch
        ' 
        LBXLibrarySearch.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LBXLibrarySearch.FormattingEnabled = True
        LBXLibrarySearch.Location = New Point(12, 42)
        LBXLibrarySearch.Name = "LBXLibrarySearch"
        LBXLibrarySearch.Size = New Size(491, 88)
        LBXLibrarySearch.TabIndex = 5
        TipLibrary.SetToolTipImage(LBXLibrarySearch, Nothing)
        LBXLibrarySearch.Visible = False
        ' 
        ' PicBoxAlbumArt
        ' 
        PicBoxAlbumArt.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        PicBoxAlbumArt.Location = New Point(12, 394)
        PicBoxAlbumArt.Name = "PicBoxAlbumArt"
        PicBoxAlbumArt.Size = New Size(101, 114)
        PicBoxAlbumArt.SizeMode = PictureBoxSizeMode.Zoom
        PicBoxAlbumArt.TabIndex = 7
        PicBoxAlbumArt.TabStop = False
        TipLibrary.SetToolTipImage(PicBoxAlbumArt, Nothing)
        PicBoxAlbumArt.Visible = False
        ' 
        ' LblAlbumArtSelect
        ' 
        LblAlbumArtSelect.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LblAlbumArtSelect.Image = My.Resources.Resources.ImageAlbumArtSelect
        LblAlbumArtSelect.Location = New Point(115, 436)
        LblAlbumArtSelect.Name = "LblAlbumArtSelect"
        LblAlbumArtSelect.Size = New Size(32, 36)
        LblAlbumArtSelect.TabIndex = 16
        TipLibrary.SetToolTipImage(LblAlbumArtSelect, Nothing)
        LblAlbumArtSelect.Visible = False
        ' 
        ' TipLibrary
        ' 
        TipLibrary.BackColor = SystemColors.Control
        TipLibrary.BorderColor = SystemColors.Window
        TipLibrary.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipLibrary.ForeColor = SystemColors.WindowText
        TipLibrary.OwnerDraw = True
        ' 
        ' RadBtnGroupByGenre
        ' 
        RadBtnGroupByGenre.Appearance = Appearance.Button
        RadBtnGroupByGenre.Font = New Font("Segoe UI", 9.75F)
        RadBtnGroupByGenre.ForeColor = SystemColors.ControlText
        RadBtnGroupByGenre.Location = New Point(124, 25)
        RadBtnGroupByGenre.Name = "RadBtnGroupByGenre"
        RadBtnGroupByGenre.Size = New Size(53, 28)
        RadBtnGroupByGenre.TabIndex = 21
        RadBtnGroupByGenre.Text = "Genre"
        RadBtnGroupByGenre.TextAlign = ContentAlignment.MiddleCenter
        TipLibrary.SetToolTipImage(RadBtnGroupByGenre, Nothing)
        RadBtnGroupByGenre.UseVisualStyleBackColor = True
        ' 
        ' GrpBoxGroupBy
        ' 
        GrpBoxGroupBy.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        GrpBoxGroupBy.Controls.Add(RadBtnGroupByType)
        GrpBoxGroupBy.Controls.Add(RadBtnGroupByYear)
        GrpBoxGroupBy.Controls.Add(RadBtnGroupByArtist)
        GrpBoxGroupBy.Controls.Add(RadBtnGroupByAlbum)
        GrpBoxGroupBy.Controls.Add(RadBtnGroupByNone)
        GrpBoxGroupBy.Controls.Add(RadBtnGroupByGenre)
        GrpBoxGroupBy.Location = New Point(589, 402)
        GrpBoxGroupBy.Name = "GrpBoxGroupBy"
        GrpBoxGroupBy.Size = New Size(242, 97)
        GrpBoxGroupBy.TabIndex = 18
        GrpBoxGroupBy.TabStop = False
        GrpBoxGroupBy.Text = "Group By"
        TipLibrary.SetToolTipImage(GrpBoxGroupBy, Nothing)
        ' 
        ' RadBtnGroupByType
        ' 
        RadBtnGroupByType.Appearance = Appearance.Button
        RadBtnGroupByType.Font = New Font("Segoe UI", 9.75F)
        RadBtnGroupByType.ForeColor = SystemColors.ControlText
        RadBtnGroupByType.Location = New Point(6, 60)
        RadBtnGroupByType.Name = "RadBtnGroupByType"
        RadBtnGroupByType.Size = New Size(53, 28)
        RadBtnGroupByType.TabIndex = 24
        RadBtnGroupByType.Text = "Type"
        RadBtnGroupByType.TextAlign = ContentAlignment.MiddleCenter
        TipLibrary.SetToolTipImage(RadBtnGroupByType, Nothing)
        RadBtnGroupByType.UseVisualStyleBackColor = True
        ' 
        ' RadBtnGroupByYear
        ' 
        RadBtnGroupByYear.Appearance = Appearance.Button
        RadBtnGroupByYear.Font = New Font("Segoe UI", 9.75F)
        RadBtnGroupByYear.ForeColor = SystemColors.ControlText
        RadBtnGroupByYear.Location = New Point(183, 25)
        RadBtnGroupByYear.Name = "RadBtnGroupByYear"
        RadBtnGroupByYear.Size = New Size(53, 28)
        RadBtnGroupByYear.TabIndex = 23
        RadBtnGroupByYear.Text = "Year"
        RadBtnGroupByYear.TextAlign = ContentAlignment.MiddleCenter
        TipLibrary.SetToolTipImage(RadBtnGroupByYear, Nothing)
        RadBtnGroupByYear.UseVisualStyleBackColor = True
        ' 
        ' RadBtnGroupByArtist
        ' 
        RadBtnGroupByArtist.Appearance = Appearance.Button
        RadBtnGroupByArtist.Font = New Font("Segoe UI", 9.75F)
        RadBtnGroupByArtist.ForeColor = SystemColors.ControlText
        RadBtnGroupByArtist.Location = New Point(6, 25)
        RadBtnGroupByArtist.Name = "RadBtnGroupByArtist"
        RadBtnGroupByArtist.Size = New Size(53, 28)
        RadBtnGroupByArtist.TabIndex = 20
        RadBtnGroupByArtist.Text = "Artist"
        RadBtnGroupByArtist.TextAlign = ContentAlignment.MiddleCenter
        TipLibrary.SetToolTipImage(RadBtnGroupByArtist, Nothing)
        RadBtnGroupByArtist.UseVisualStyleBackColor = True
        ' 
        ' RadBtnGroupByAlbum
        ' 
        RadBtnGroupByAlbum.Appearance = Appearance.Button
        RadBtnGroupByAlbum.Font = New Font("Segoe UI", 9.75F)
        RadBtnGroupByAlbum.ForeColor = SystemColors.ControlText
        RadBtnGroupByAlbum.Location = New Point(65, 25)
        RadBtnGroupByAlbum.Name = "RadBtnGroupByAlbum"
        RadBtnGroupByAlbum.Size = New Size(53, 28)
        RadBtnGroupByAlbum.TabIndex = 19
        RadBtnGroupByAlbum.Text = "Album"
        RadBtnGroupByAlbum.TextAlign = ContentAlignment.MiddleCenter
        TipLibrary.SetToolTipImage(RadBtnGroupByAlbum, Nothing)
        RadBtnGroupByAlbum.UseVisualStyleBackColor = True
        ' 
        ' RadBtnGroupByNone
        ' 
        RadBtnGroupByNone.Appearance = Appearance.Button
        RadBtnGroupByNone.Checked = True
        RadBtnGroupByNone.ForeColor = SystemColors.ControlText
        RadBtnGroupByNone.Location = New Point(65, 60)
        RadBtnGroupByNone.Name = "RadBtnGroupByNone"
        RadBtnGroupByNone.Size = New Size(171, 28)
        RadBtnGroupByNone.TabIndex = 22
        RadBtnGroupByNone.TabStop = True
        RadBtnGroupByNone.Text = "No Grouping"
        RadBtnGroupByNone.TextAlign = ContentAlignment.MiddleCenter
        TipLibrary.SetToolTipImage(RadBtnGroupByNone, Nothing)
        RadBtnGroupByNone.UseVisualStyleBackColor = True
        ' 
        ' LblExtTitle
        ' 
        LblExtTitle.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LblExtTitle.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblExtTitle.Location = New Point(147, 420)
        LblExtTitle.Name = "LblExtTitle"
        LblExtTitle.Size = New Size(436, 20)
        LblExtTitle.TabIndex = 19
        LblExtTitle.Text = "Title"
        LblExtTitle.TextAlign = ContentAlignment.MiddleLeft
        TipLibrary.SetToolTipImage(LblExtTitle, Nothing)
        ' 
        ' LblExtFileInfo
        ' 
        LblExtFileInfo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LblExtFileInfo.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblExtFileInfo.Location = New Point(147, 441)
        LblExtFileInfo.Name = "LblExtFileInfo"
        LblExtFileInfo.Size = New Size(436, 20)
        LblExtFileInfo.TabIndex = 20
        LblExtFileInfo.Text = "File Info"
        LblExtFileInfo.TextAlign = ContentAlignment.MiddleLeft
        TipLibrary.SetToolTipImage(LblExtFileInfo, Nothing)
        ' 
        ' LblExtProperties
        ' 
        LblExtProperties.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LblExtProperties.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblExtProperties.Location = New Point(147, 461)
        LblExtProperties.Name = "LblExtProperties"
        LblExtProperties.Size = New Size(436, 20)
        LblExtProperties.TabIndex = 21
        LblExtProperties.Text = "Properties"
        LblExtProperties.TextAlign = ContentAlignment.MiddleLeft
        TipLibrary.SetToolTipImage(LblExtProperties, Nothing)
        ' 
        ' LblExtType
        ' 
        LblExtType.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LblExtType.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblExtType.Location = New Point(147, 482)
        LblExtType.Name = "LblExtType"
        LblExtType.Size = New Size(436, 20)
        LblExtType.TabIndex = 22
        LblExtType.Text = "Type"
        LblExtType.TextAlign = ContentAlignment.MiddleLeft
        TipLibrary.SetToolTipImage(LblExtType, Nothing)
        ' 
        ' LblLibraryCounts
        ' 
        LblLibraryCounts.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblLibraryCounts.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblLibraryCounts.Location = New Point(549, 12)
        LblLibraryCounts.Name = "LblLibraryCounts"
        LblLibraryCounts.Size = New Size(423, 28)
        LblLibraryCounts.TabIndex = 23
        LblLibraryCounts.TextAlign = ContentAlignment.MiddleRight
        TipLibrary.SetToolTipImage(LblLibraryCounts, Nothing)
        ' 
        ' LblStatus
        ' 
        LblStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblStatus.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblStatus.ForeColor = Color.Red
        LblStatus.Location = New Point(549, 12)
        LblStatus.Name = "LblStatus"
        LblStatus.Size = New Size(423, 28)
        LblStatus.TabIndex = 24
        LblStatus.Text = "Status"
        LblStatus.TextAlign = ContentAlignment.MiddleRight
        TipLibrary.SetToolTipImage(LblStatus, Nothing)
        LblStatus.Visible = False
        ' 
        ' LVLibrary
        ' 
        LVLibrary.AllowColumnReorder = True
        LVLibrary.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LVLibrary.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LVLibrary.FullRowSelect = True
        LVLibrary.Location = New Point(12, 42)
        LVLibrary.Name = "LVLibrary"
        LVLibrary.OwnerDraw = True
        LVLibrary.Size = New Size(960, 339)
        LVLibrary.TabIndex = 0
        TipLibrary.SetToolTipImage(LVLibrary, Nothing)
        LVLibrary.UseCompatibleStateImageBehavior = False
        LVLibrary.View = View.Details
        ' 
        ' LblHistory
        ' 
        LblHistory.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LblHistory.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblHistory.Location = New Point(147, 400)
        LblHistory.Name = "LblHistory"
        LblHistory.Size = New Size(436, 20)
        LblHistory.TabIndex = 25
        LblHistory.Text = "History"
        LblHistory.TextAlign = ContentAlignment.MiddleLeft
        TipLibrary.SetToolTipImage(LblHistory, Nothing)
        ' 
        ' Library
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(984, 522)
        ContextMenuStrip = CMLibrary
        Controls.Add(PicBoxAlbumArt)
        Controls.Add(LblStatus)
        Controls.Add(LblLibraryCounts)
        Controls.Add(LBXLibrarySearch)
        Controls.Add(BtnOK)
        Controls.Add(BtnSearchFolders)
        Controls.Add(LblAlbumArtSelect)
        Controls.Add(GrpBoxGroupBy)
        Controls.Add(TxbxLibrarySearch)
        Controls.Add(LblExtType)
        Controls.Add(LblExtProperties)
        Controls.Add(LblExtFileInfo)
        Controls.Add(LblExtTitle)
        Controls.Add(LVLibrary)
        Controls.Add(LblHistory)
        Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        MinimumSize = New Size(1000, 561)
        Name = "Library"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Library"
        TipLibrary.SetToolTipImage(Me, Nothing)
        CMLibrary.ResumeLayout(False)
        CType(PicBoxAlbumArt, ComponentModel.ISupportInitialize).EndInit()
        GrpBoxGroupBy.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents BtnSearchFolders As Button
    Friend WithEvents BtnOK As Button
    Friend WithEvents TxbxLibrarySearch As TextBox
    Friend WithEvents LBXLibrarySearch As ListBox
    Friend WithEvents PicBoxAlbumArt As PictureBox
    Friend WithEvents LblAlbumArtSelect As Label
    Friend WithEvents CMLibrary As ContextMenuStrip
    Friend WithEvents CMIHelperApp1 As ToolStripMenuItem
    Friend WithEvents CMIAddToPlaylist As ToolStripMenuItem
    Friend WithEvents CMIAddAllToPlaylist As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TipLibrary As Skye.UI.ToolTip
    Friend WithEvents CMIOpenLocation As ToolStripMenuItem
    Friend WithEvents CMIHelperApp2 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents CMICopyTitle As ToolStripMenuItem
    Friend WithEvents CMICopyFileName As ToolStripMenuItem
    Friend WithEvents CMICopyFilePath As ToolStripMenuItem
    Friend WithEvents CMIPlay As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents CMIPlayWithWindows As ToolStripMenuItem
    Friend WithEvents RadBtnGroupByGenre As RadioButton
    Friend WithEvents GrpBoxGroupBy As GroupBox
    Friend WithEvents RadBtnGroupByNone As RadioButton
    Friend WithEvents RadBtnGroupByArtist As RadioButton
    Friend WithEvents RadBtnGroupByAlbum As RadioButton
    Friend WithEvents CMIAddGroupToPlaylist As ToolStripMenuItem
    Friend WithEvents CMICollapseGroup As ToolStripMenuItem
    Friend WithEvents CMIExpandAllGroups As ToolStripMenuItem
    Friend WithEvents CMISeparatorGroupBy As ToolStripSeparator
    Friend WithEvents LblExtTitle As Skye.UI.Label
    Friend WithEvents LblExtFileInfo As Skye.UI.Label
    Friend WithEvents LblExtProperties As Skye.UI.Label
    Friend WithEvents LblExtType As Skye.UI.Label
    Friend WithEvents LblLibraryCounts As Skye.UI.Label
    Friend WithEvents LblStatus As Skye.UI.Label
    Friend WithEvents LVLibrary As ListView
    Friend WithEvents LblHistory As Skye.UI.Label
    Friend WithEvents RadBtnGroupByYear As RadioButton
    Friend WithEvents CMIQueue As ToolStripMenuItem
    Friend WithEvents RadBtnGroupByType As RadioButton
End Class
