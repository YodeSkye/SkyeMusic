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
        TipLibrary = New ToolTip(components)
        RadBtnGroupByGenre = New RadioButton()
        GrpBoxGroupBy = New GroupBox()
        RadBtnGroupByArtist = New RadioButton()
        RadBtnGroupByAlbum = New RadioButton()
        RadBtnGroupByNone = New RadioButton()
        LblExtTitle = New Components.LabelCSY()
        LblExtFileInfo = New Components.LabelCSY()
        LblExtProperties = New Components.LabelCSY()
        LblExtType = New Components.LabelCSY()
        LblLibraryCounts = New Components.LabelCSY()
        LblStatus = New Components.LabelCSY()
        LVLibrary = New ListView()
        CMLibrary.SuspendLayout()
        CType(PicBoxAlbumArt, ComponentModel.ISupportInitialize).BeginInit()
        GrpBoxGroupBy.SuspendLayout()
        SuspendLayout()
        ' 
        ' CMLibrary
        ' 
        CMLibrary.Items.AddRange(New ToolStripItem() {CMIPlay, CMIPlayWithWindows, ToolStripSeparator3, CMIAddToPlaylist, CMIAddAllToPlaylist, ToolStripSeparator1, CMIAddGroupToPlaylist, CMICollapseGroup, CMIExpandAllGroups, CMISeparatorGroupBy, CMIHelperApp1, CMIHelperApp2, CMIOpenLocation, ToolStripSeparator2, CMICopyTitle, CMICopyFileName, CMICopyFilePath})
        CMLibrary.Name = "CMLibrary"
        CMLibrary.Size = New Size(189, 314)
        ' 
        ' CMIPlay
        ' 
        CMIPlay.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CMIPlay.Image = My.Resources.Resources.ImagePlay
        CMIPlay.Name = "CMIPlay"
        CMIPlay.Size = New Size(188, 22)
        CMIPlay.Text = "Play"
        ' 
        ' CMIPlayWithWindows
        ' 
        CMIPlayWithWindows.Image = My.Resources.Resources.ImageWindows16
        CMIPlayWithWindows.Name = "CMIPlayWithWindows"
        CMIPlayWithWindows.Size = New Size(188, 22)
        CMIPlayWithWindows.Text = "Play In Windows"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(185, 6)
        ' 
        ' CMIAddToPlaylist
        ' 
        CMIAddToPlaylist.Image = My.Resources.Resources.ImageAdd16
        CMIAddToPlaylist.Name = "CMIAddToPlaylist"
        CMIAddToPlaylist.Size = New Size(188, 22)
        CMIAddToPlaylist.Text = "Add To Playlist"
        ' 
        ' CMIAddAllToPlaylist
        ' 
        CMIAddAllToPlaylist.Image = My.Resources.Resources.ImageAddAll16
        CMIAddAllToPlaylist.Name = "CMIAddAllToPlaylist"
        CMIAddAllToPlaylist.Size = New Size(188, 22)
        CMIAddAllToPlaylist.Text = "Add ALL To Playlist"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(185, 6)
        ' 
        ' CMIAddGroupToPlaylist
        ' 
        CMIAddGroupToPlaylist.Image = My.Resources.Resources.ImageAdd16
        CMIAddGroupToPlaylist.Name = "CMIAddGroupToPlaylist"
        CMIAddGroupToPlaylist.Size = New Size(188, 22)
        CMIAddGroupToPlaylist.Text = "Add Group To Playlist"
        ' 
        ' CMICollapseGroup
        ' 
        CMICollapseGroup.Image = My.Resources.Resources.ImageCollapse16
        CMICollapseGroup.Name = "CMICollapseGroup"
        CMICollapseGroup.Size = New Size(188, 22)
        CMICollapseGroup.Text = "Collapse Group"
        ' 
        ' CMIExpandAllGroups
        ' 
        CMIExpandAllGroups.Image = My.Resources.Resources.ImageExpandAll16
        CMIExpandAllGroups.Name = "CMIExpandAllGroups"
        CMIExpandAllGroups.Size = New Size(188, 22)
        CMIExpandAllGroups.Text = "Expand All"
        ' 
        ' CMISeparatorGroupBy
        ' 
        CMISeparatorGroupBy.Name = "CMISeparatorGroupBy"
        CMISeparatorGroupBy.Size = New Size(185, 6)
        ' 
        ' CMIHelperApp1
        ' 
        CMIHelperApp1.Image = My.Resources.Resources.ImageSkyeTag
        CMIHelperApp1.Name = "CMIHelperApp1"
        CMIHelperApp1.Size = New Size(188, 22)
        ' 
        ' CMIHelperApp2
        ' 
        CMIHelperApp2.Image = My.Resources.Resources.ImageMP3Tag16
        CMIHelperApp2.Name = "CMIHelperApp2"
        CMIHelperApp2.Size = New Size(188, 22)
        ' 
        ' CMIOpenLocation
        ' 
        CMIOpenLocation.Image = My.Resources.Resources.ImageOpen16
        CMIOpenLocation.Name = "CMIOpenLocation"
        CMIOpenLocation.Size = New Size(188, 22)
        CMIOpenLocation.Text = "Open File Location"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(185, 6)
        ' 
        ' CMICopyTitle
        ' 
        CMICopyTitle.Image = My.Resources.Resources.ImageCopy16
        CMICopyTitle.Name = "CMICopyTitle"
        CMICopyTitle.Size = New Size(188, 22)
        CMICopyTitle.Text = "Copy Title"
        ' 
        ' CMICopyFileName
        ' 
        CMICopyFileName.Image = My.Resources.Resources.ImageCopy16
        CMICopyFileName.Name = "CMICopyFileName"
        CMICopyFileName.Size = New Size(188, 22)
        CMICopyFileName.Text = "Copy File Name"
        ' 
        ' CMICopyFilePath
        ' 
        CMICopyFilePath.Image = My.Resources.Resources.ImageCopy16
        CMICopyFilePath.Name = "CMICopyFilePath"
        CMICopyFilePath.Size = New Size(188, 22)
        CMICopyFilePath.Text = "Copy File Path"
        ' 
        ' BtnSearchFolders
        ' 
        BtnSearchFolders.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnSearchFolders.Image = My.Resources.Resources.ImageSearchFolder
        BtnSearchFolders.ImageAlign = ContentAlignment.MiddleLeft
        BtnSearchFolders.Location = New Point(858, 348)
        BtnSearchFolders.Name = "BtnSearchFolders"
        BtnSearchFolders.Size = New Size(114, 32)
        BtnSearchFolders.TabIndex = 1
        BtnSearchFolders.TabStop = False
        BtnSearchFolders.Text = "Search Folders"
        BtnSearchFolders.TextAlign = ContentAlignment.MiddleRight
        BtnSearchFolders.UseVisualStyleBackColor = True
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(883, 385)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 3
        BtnOK.TabStop = False
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' TxbxLibrarySearch
        ' 
        TxbxLibrarySearch.BorderStyle = BorderStyle.None
        TxbxLibrarySearch.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxbxLibrarySearch.ForeColor = SystemColors.InactiveCaption
        TxbxLibrarySearch.Location = New Point(14, 17)
        TxbxLibrarySearch.Name = "TxbxLibrarySearch"
        TxbxLibrarySearch.Size = New Size(153, 18)
        TxbxLibrarySearch.TabIndex = 4
        TxbxLibrarySearch.Text = "Search Library"
        ' 
        ' LBXLibrarySearch
        ' 
        LBXLibrarySearch.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LBXLibrarySearch.FormattingEnabled = True
        LBXLibrarySearch.ItemHeight = 21
        LBXLibrarySearch.Location = New Point(12, 37)
        LBXLibrarySearch.Name = "LBXLibrarySearch"
        LBXLibrarySearch.Size = New Size(491, 88)
        LBXLibrarySearch.TabIndex = 5
        LBXLibrarySearch.Visible = False
        ' 
        ' PicBoxAlbumArt
        ' 
        PicBoxAlbumArt.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        PicBoxAlbumArt.Location = New Point(12, 348)
        PicBoxAlbumArt.Name = "PicBoxAlbumArt"
        PicBoxAlbumArt.Size = New Size(101, 101)
        PicBoxAlbumArt.SizeMode = PictureBoxSizeMode.Zoom
        PicBoxAlbumArt.TabIndex = 7
        PicBoxAlbumArt.TabStop = False
        PicBoxAlbumArt.Visible = False
        ' 
        ' LblAlbumArtSelect
        ' 
        LblAlbumArtSelect.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LblAlbumArtSelect.Image = My.Resources.Resources.ImageAlbumArtSelect
        LblAlbumArtSelect.Location = New Point(115, 385)
        LblAlbumArtSelect.Name = "LblAlbumArtSelect"
        LblAlbumArtSelect.Size = New Size(32, 32)
        LblAlbumArtSelect.TabIndex = 16
        LblAlbumArtSelect.Visible = False
        ' 
        ' TipLibrary
        ' 
        TipLibrary.AutoPopDelay = 5000
        TipLibrary.InitialDelay = 1000
        TipLibrary.IsBalloon = True
        TipLibrary.ReshowDelay = 1000
        ' 
        ' RadBtnGroupByGenre
        ' 
        RadBtnGroupByGenre.Appearance = Appearance.Button
        RadBtnGroupByGenre.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        RadBtnGroupByGenre.ForeColor = SystemColors.ControlText
        RadBtnGroupByGenre.Location = New Point(124, 22)
        RadBtnGroupByGenre.Name = "RadBtnGroupByGenre"
        RadBtnGroupByGenre.Size = New Size(53, 25)
        RadBtnGroupByGenre.TabIndex = 21
        RadBtnGroupByGenre.Text = "Genre"
        RadBtnGroupByGenre.TextAlign = ContentAlignment.MiddleCenter
        RadBtnGroupByGenre.UseVisualStyleBackColor = True
        ' 
        ' GrpBoxGroupBy
        ' 
        GrpBoxGroupBy.Anchor = AnchorStyles.Bottom
        GrpBoxGroupBy.Controls.Add(RadBtnGroupByArtist)
        GrpBoxGroupBy.Controls.Add(RadBtnGroupByAlbum)
        GrpBoxGroupBy.Controls.Add(RadBtnGroupByNone)
        GrpBoxGroupBy.Controls.Add(RadBtnGroupByGenre)
        GrpBoxGroupBy.Location = New Point(665, 355)
        GrpBoxGroupBy.Name = "GrpBoxGroupBy"
        GrpBoxGroupBy.Size = New Size(184, 86)
        GrpBoxGroupBy.TabIndex = 18
        GrpBoxGroupBy.TabStop = False
        GrpBoxGroupBy.Text = "Group By"
        ' 
        ' RadBtnGroupByArtist
        ' 
        RadBtnGroupByArtist.Appearance = Appearance.Button
        RadBtnGroupByArtist.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        RadBtnGroupByArtist.ForeColor = SystemColors.ControlText
        RadBtnGroupByArtist.Location = New Point(65, 22)
        RadBtnGroupByArtist.Name = "RadBtnGroupByArtist"
        RadBtnGroupByArtist.Size = New Size(53, 25)
        RadBtnGroupByArtist.TabIndex = 20
        RadBtnGroupByArtist.Text = "Artist"
        RadBtnGroupByArtist.TextAlign = ContentAlignment.MiddleCenter
        RadBtnGroupByArtist.UseVisualStyleBackColor = True
        ' 
        ' RadBtnGroupByAlbum
        ' 
        RadBtnGroupByAlbum.Appearance = Appearance.Button
        RadBtnGroupByAlbum.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        RadBtnGroupByAlbum.ForeColor = SystemColors.ControlText
        RadBtnGroupByAlbum.Location = New Point(6, 22)
        RadBtnGroupByAlbum.Name = "RadBtnGroupByAlbum"
        RadBtnGroupByAlbum.Size = New Size(53, 25)
        RadBtnGroupByAlbum.TabIndex = 19
        RadBtnGroupByAlbum.Text = "Album"
        RadBtnGroupByAlbum.TextAlign = ContentAlignment.MiddleCenter
        RadBtnGroupByAlbum.UseVisualStyleBackColor = True
        ' 
        ' RadBtnGroupByNone
        ' 
        RadBtnGroupByNone.Appearance = Appearance.Button
        RadBtnGroupByNone.Checked = True
        RadBtnGroupByNone.ForeColor = SystemColors.ControlText
        RadBtnGroupByNone.Location = New Point(6, 53)
        RadBtnGroupByNone.Name = "RadBtnGroupByNone"
        RadBtnGroupByNone.Size = New Size(171, 25)
        RadBtnGroupByNone.TabIndex = 22
        RadBtnGroupByNone.TabStop = True
        RadBtnGroupByNone.Text = "No Grouping"
        RadBtnGroupByNone.TextAlign = ContentAlignment.MiddleCenter
        RadBtnGroupByNone.UseVisualStyleBackColor = True
        ' 
        ' LblExtTitle
        ' 
        LblExtTitle.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LblExtTitle.CopyOnDoubleClick = False
        LblExtTitle.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblExtTitle.Location = New Point(147, 365)
        LblExtTitle.Name = "LblExtTitle"
        LblExtTitle.Size = New Size(512, 16)
        LblExtTitle.TabIndex = 19
        LblExtTitle.Text = "Title"
        LblExtTitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LblExtFileInfo
        ' 
        LblExtFileInfo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LblExtFileInfo.CopyOnDoubleClick = False
        LblExtFileInfo.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblExtFileInfo.Location = New Point(147, 382)
        LblExtFileInfo.Name = "LblExtFileInfo"
        LblExtFileInfo.Size = New Size(512, 16)
        LblExtFileInfo.TabIndex = 20
        LblExtFileInfo.Text = "LblExtFileInfo"
        LblExtFileInfo.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LblExtProperties
        ' 
        LblExtProperties.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LblExtProperties.CopyOnDoubleClick = False
        LblExtProperties.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblExtProperties.Location = New Point(147, 399)
        LblExtProperties.Name = "LblExtProperties"
        LblExtProperties.Size = New Size(512, 16)
        LblExtProperties.TabIndex = 21
        LblExtProperties.Text = "Properties"
        LblExtProperties.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LblExtType
        ' 
        LblExtType.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LblExtType.CopyOnDoubleClick = False
        LblExtType.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblExtType.Location = New Point(147, 415)
        LblExtType.Name = "LblExtType"
        LblExtType.Size = New Size(512, 16)
        LblExtType.TabIndex = 22
        LblExtType.Text = "Type"
        LblExtType.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LblLibraryCounts
        ' 
        LblLibraryCounts.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblLibraryCounts.CopyOnDoubleClick = False
        LblLibraryCounts.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblLibraryCounts.Location = New Point(549, 11)
        LblLibraryCounts.Name = "LblLibraryCounts"
        LblLibraryCounts.Size = New Size(423, 25)
        LblLibraryCounts.TabIndex = 23
        LblLibraryCounts.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblStatus
        ' 
        LblStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblStatus.CopyOnDoubleClick = False
        LblStatus.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblStatus.ForeColor = Color.Red
        LblStatus.Location = New Point(549, 11)
        LblStatus.Name = "LblStatus"
        LblStatus.Size = New Size(423, 25)
        LblStatus.TabIndex = 24
        LblStatus.Text = "Status"
        LblStatus.TextAlign = ContentAlignment.MiddleRight
        LblStatus.Visible = False
        ' 
        ' LVLibrary
        ' 
        LVLibrary.AllowColumnReorder = True
        LVLibrary.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LVLibrary.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LVLibrary.FullRowSelect = True
        LVLibrary.Location = New Point(12, 37)
        LVLibrary.Name = "LVLibrary"
        LVLibrary.OwnerDraw = True
        LVLibrary.Size = New Size(960, 300)
        LVLibrary.TabIndex = 0
        LVLibrary.UseCompatibleStateImageBehavior = False
        LVLibrary.View = View.Details
        ' 
        ' Library
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(984, 461)
        Controls.Add(LblStatus)
        Controls.Add(LblLibraryCounts)
        Controls.Add(PicBoxAlbumArt)
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
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        MinimumSize = New Size(1000, 500)
        Name = "Library"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Library"
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
    Friend WithEvents TipLibrary As ToolTip
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
    Friend WithEvents LblExtTitle As My.Components.LabelCSY
    Friend WithEvents LblExtFileInfo As My.Components.LabelCSY
    Friend WithEvents LblExtProperties As My.Components.LabelCSY
    Friend WithEvents LblExtType As My.Components.LabelCSY
    Friend WithEvents LblLibraryCounts As My.Components.LabelCSY
    Friend WithEvents LblStatus As My.Components.LabelCSY
    Friend WithEvents LVLibrary As ListView
End Class
