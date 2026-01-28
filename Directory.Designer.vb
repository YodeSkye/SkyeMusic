<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Directory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Directory))
        PanelSearch = New Panel()
        BtnSearch = New Button()
        TxtBoxSearch = New TextBox()
        StatusStripDirectory = New StatusStrip()
        StatusLabel = New ToolStripStatusLabel()
        PanelDirectory = New Panel()
        LVSources = New Skye.UI.ListViewEX()
        Source = New ColumnHeader()
        ILSources = New ImageList(components)
        PanelDirectoryList = New Panel()
        LVStations = New Skye.UI.ListViewEX()
        ColStreamName = New ColumnHeader()
        ColTags = New ColumnHeader()
        ColFormat = New ColumnHeader()
        ColBitrate = New ColumnHeader()
        ColCountry = New ColumnHeader()
        ColStatus = New ColumnHeader()
        ColURL = New ColumnHeader()
        ColMore = New ColumnHeader()
        CMStations = New ContextMenuStrip(components)
        CMIPlay = New ToolStripMenuItem()
        CMIAddToPlaylist = New ToolStripMenuItem()
        CMIAddToFavorites = New ToolStripMenuItem()
        CMICopyStreamURL = New ToolStripMenuItem()
        CMIRemoveFromFavorites = New ToolStripMenuItem()
        PanelSearch.SuspendLayout()
        StatusStripDirectory.SuspendLayout()
        PanelDirectory.SuspendLayout()
        PanelDirectoryList.SuspendLayout()
        CMStations.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelSearch
        ' 
        PanelSearch.Controls.Add(BtnSearch)
        PanelSearch.Controls.Add(TxtBoxSearch)
        PanelSearch.Dock = DockStyle.Top
        PanelSearch.Location = New Point(0, 0)
        PanelSearch.Margin = New Padding(4)
        PanelSearch.Name = "PanelSearch"
        PanelSearch.Size = New Size(1029, 49)
        PanelSearch.TabIndex = 0
        ' 
        ' BtnSearch
        ' 
        BtnSearch.Location = New Point(285, 11)
        BtnSearch.Name = "BtnSearch"
        BtnSearch.Size = New Size(75, 30)
        BtnSearch.TabIndex = 1
        BtnSearch.Text = "Search"
        BtnSearch.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxSearch
        ' 
        TxtBoxSearch.Location = New Point(12, 12)
        TxtBoxSearch.Name = "TxtBoxSearch"
        TxtBoxSearch.Size = New Size(267, 29)
        TxtBoxSearch.TabIndex = 0
        ' 
        ' StatusStripDirectory
        ' 
        StatusStripDirectory.Items.AddRange(New ToolStripItem() {StatusLabel})
        StatusStripDirectory.Location = New Point(0, 608)
        StatusStripDirectory.Name = "StatusStripDirectory"
        StatusStripDirectory.Size = New Size(1029, 22)
        StatusStripDirectory.TabIndex = 2
        ' 
        ' StatusLabel
        ' 
        StatusLabel.Name = "StatusLabel"
        StatusLabel.Size = New Size(39, 17)
        StatusLabel.Text = "Status"
        ' 
        ' PanelDirectory
        ' 
        PanelDirectory.Controls.Add(LVSources)
        PanelDirectory.Dock = DockStyle.Left
        PanelDirectory.Location = New Point(0, 49)
        PanelDirectory.Name = "PanelDirectory"
        PanelDirectory.Size = New Size(95, 559)
        PanelDirectory.TabIndex = 5
        ' 
        ' LVSources
        ' 
        LVSources.BorderStyle = BorderStyle.FixedSingle
        LVSources.Columns.AddRange(New ColumnHeader() {Source})
        LVSources.Dock = DockStyle.Fill
        LVSources.FullRowSelect = True
        LVSources.HeaderStyle = ColumnHeaderStyle.Nonclickable
        LVSources.InsertionLineColor = Color.Teal
        LVSources.LargeImageList = ILSources
        LVSources.Location = New Point(0, 0)
        LVSources.MultiSelect = False
        LVSources.Name = "LVSources"
        LVSources.Scrollable = False
        LVSources.Size = New Size(95, 559)
        LVSources.TabIndex = 0
        LVSources.UseCompatibleStateImageBehavior = False
        ' 
        ' Source
        ' 
        Source.Text = "Source"
        Source.Width = 277
        ' 
        ' ILSources
        ' 
        ILSources.ColorDepth = ColorDepth.Depth32Bit
        ILSources.ImageSize = New Size(48, 48)
        ILSources.TransparentColor = Color.Transparent
        ' 
        ' PanelDirectoryList
        ' 
        PanelDirectoryList.Controls.Add(LVStations)
        PanelDirectoryList.Dock = DockStyle.Fill
        PanelDirectoryList.Location = New Point(95, 49)
        PanelDirectoryList.Name = "PanelDirectoryList"
        PanelDirectoryList.Size = New Size(934, 559)
        PanelDirectoryList.TabIndex = 6
        ' 
        ' LVStations
        ' 
        LVStations.BorderStyle = BorderStyle.FixedSingle
        LVStations.Columns.AddRange(New ColumnHeader() {ColStreamName, ColTags, ColFormat, ColBitrate, ColCountry, ColStatus, ColURL, ColMore})
        LVStations.ContextMenuStrip = CMStations
        LVStations.Dock = DockStyle.Fill
        LVStations.FullRowSelect = True
        LVStations.InsertionLineColor = Color.Teal
        LVStations.Location = New Point(0, 0)
        LVStations.Name = "LVStations"
        LVStations.Size = New Size(934, 559)
        LVStations.TabIndex = 0
        LVStations.UseCompatibleStateImageBehavior = False
        LVStations.View = View.Details
        ' 
        ' ColStreamName
        ' 
        ColStreamName.Text = "Name"
        ColStreamName.Width = 300
        ' 
        ' ColTags
        ' 
        ColTags.Text = "Tags"
        ColTags.Width = 150
        ' 
        ' ColFormat
        ' 
        ColFormat.Text = "Format"
        ColFormat.TextAlign = HorizontalAlignment.Center
        ColFormat.Width = 90
        ' 
        ' ColBitrate
        ' 
        ColBitrate.Text = "Bitrate"
        ColBitrate.TextAlign = HorizontalAlignment.Center
        ColBitrate.Width = 75
        ' 
        ' ColCountry
        ' 
        ColCountry.Text = "Country"
        ColCountry.TextAlign = HorizontalAlignment.Center
        ColCountry.Width = 75
        ' 
        ' ColStatus
        ' 
        ColStatus.Text = "Status"
        ColStatus.Width = 140
        ' 
        ' ColURL
        ' 
        ColURL.Text = "URL"
        ColURL.Width = 300
        ' 
        ' ColMore
        ' 
        ColMore.Text = ""
        ColMore.Width = 30
        ' 
        ' CMStations
        ' 
        CMStations.Items.AddRange(New ToolStripItem() {CMIPlay, CMIAddToPlaylist, CMIAddToFavorites, CMIRemoveFromFavorites, CMICopyStreamURL})
        CMStations.Name = "CMStations"
        CMStations.Size = New Size(199, 136)
        ' 
        ' CMIPlay
        ' 
        CMIPlay.Image = My.Resources.Resources.ImagePlay
        CMIPlay.Name = "CMIPlay"
        CMIPlay.Size = New Size(198, 22)
        CMIPlay.Text = "Play"
        ' 
        ' CMIAddToPlaylist
        ' 
        CMIAddToPlaylist.Image = My.Resources.Resources.ImageAdd16
        CMIAddToPlaylist.Name = "CMIAddToPlaylist"
        CMIAddToPlaylist.Size = New Size(198, 22)
        CMIAddToPlaylist.Text = "Add To Playlist"
        ' 
        ' CMIAddToFavorites
        ' 
        CMIAddToFavorites.Image = My.Resources.Resources.ImageFavorites16
        CMIAddToFavorites.Name = "CMIAddToFavorites"
        CMIAddToFavorites.Size = New Size(198, 22)
        CMIAddToFavorites.Text = "Add To Favorites"
        ' 
        ' CMICopyStreamURL
        ' 
        CMICopyStreamURL.Image = My.Resources.Resources.ImageCopy16
        CMICopyStreamURL.Name = "CMICopyStreamURL"
        CMICopyStreamURL.Size = New Size(198, 22)
        CMICopyStreamURL.Text = "Copy URL"
        ' 
        ' CMIRemoveFromFavorites
        ' 
        CMIRemoveFromFavorites.Image = My.Resources.Resources.ImageFavorites16
        CMIRemoveFromFavorites.Name = "CMIRemoveFromFavorites"
        CMIRemoveFromFavorites.Size = New Size(198, 22)
        CMIRemoveFromFavorites.Text = "Remove From Favorites"
        ' 
        ' Directory
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(1029, 630)
        Controls.Add(PanelDirectoryList)
        Controls.Add(PanelDirectory)
        Controls.Add(StatusStripDirectory)
        Controls.Add(PanelSearch)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4)
        MinimumSize = New Size(200, 200)
        Name = "Directory"
        SizeGripStyle = SizeGripStyle.Show
        StartPosition = FormStartPosition.CenterScreen
        Text = "Stream Directory"
        PanelSearch.ResumeLayout(False)
        PanelSearch.PerformLayout()
        StatusStripDirectory.ResumeLayout(False)
        StatusStripDirectory.PerformLayout()
        PanelDirectory.ResumeLayout(False)
        PanelDirectoryList.ResumeLayout(False)
        CMStations.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PanelSearch As Panel
    Friend WithEvents StatusStripDirectory As StatusStrip
    Friend WithEvents PanelDirectory As Panel
    Friend WithEvents PanelDirectoryList As Panel
    Friend WithEvents LVSources As Skye.UI.ListViewEX
    Friend WithEvents LVStations As Skye.UI.ListViewEX
    Friend WithEvents ColStreamName As ColumnHeader
    Friend WithEvents ColTags As ColumnHeader
    Friend WithEvents ColBitrate As ColumnHeader
    Friend WithEvents ColCountry As ColumnHeader
    Friend WithEvents ColStatus As ColumnHeader
    Friend WithEvents BtnSearch As Button
    Friend WithEvents TxtBoxSearch As TextBox
    Friend WithEvents Source As ColumnHeader
    Friend WithEvents CMStations As ContextMenuStrip
    Friend WithEvents CMIPlay As ToolStripMenuItem
    Friend WithEvents CMIAddToPlaylist As ToolStripMenuItem
    Friend WithEvents CMICopyStreamURL As ToolStripMenuItem
    Friend WithEvents StatusLabel As ToolStripStatusLabel
    Friend WithEvents ColURL As ColumnHeader
    Friend WithEvents ColFormat As ColumnHeader
    Friend WithEvents ColMore As ColumnHeader
    Friend WithEvents ILSources As ImageList
    Friend WithEvents CMIAddToFavorites As ToolStripMenuItem
    Friend WithEvents CMIRemoveFromFavorites As ToolStripMenuItem
End Class
