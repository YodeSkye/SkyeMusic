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
        PanelDirectoryList = New Panel()
        LVStations = New Skye.UI.ListViewEX()
        StreamName = New ColumnHeader()
        Tags = New ColumnHeader()
        Format = New ColumnHeader()
        Bitrate = New ColumnHeader()
        Country = New ColumnHeader()
        Status = New ColumnHeader()
        URL = New ColumnHeader()
        CMStations = New ContextMenuStrip(components)
        CMIPlay = New ToolStripMenuItem()
        CMIAddToPlaylist = New ToolStripMenuItem()
        CMICopyStreamURL = New ToolStripMenuItem()
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
        PanelDirectory.Size = New Size(279, 559)
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
        LVSources.Location = New Point(0, 0)
        LVSources.MultiSelect = False
        LVSources.Name = "LVSources"
        LVSources.Size = New Size(279, 559)
        LVSources.TabIndex = 0
        LVSources.UseCompatibleStateImageBehavior = False
        LVSources.View = View.Details
        ' 
        ' Source
        ' 
        Source.Text = "Source"
        Source.Width = 277
        ' 
        ' PanelDirectoryList
        ' 
        PanelDirectoryList.Controls.Add(LVStations)
        PanelDirectoryList.Dock = DockStyle.Fill
        PanelDirectoryList.Location = New Point(279, 49)
        PanelDirectoryList.Name = "PanelDirectoryList"
        PanelDirectoryList.Size = New Size(750, 559)
        PanelDirectoryList.TabIndex = 6
        ' 
        ' LVStations
        ' 
        LVStations.BorderStyle = BorderStyle.FixedSingle
        LVStations.Columns.AddRange(New ColumnHeader() {StreamName, Tags, Format, Bitrate, Country, Status, URL})
        LVStations.ContextMenuStrip = CMStations
        LVStations.Dock = DockStyle.Fill
        LVStations.FullRowSelect = True
        LVStations.InsertionLineColor = Color.Teal
        LVStations.Location = New Point(0, 0)
        LVStations.Name = "LVStations"
        LVStations.Size = New Size(750, 559)
        LVStations.TabIndex = 0
        LVStations.UseCompatibleStateImageBehavior = False
        LVStations.View = View.Details
        ' 
        ' StreamName
        ' 
        StreamName.Text = "Name"
        StreamName.Width = 300
        ' 
        ' Tags
        ' 
        Tags.Text = "Tags"
        Tags.Width = 150
        ' 
        ' Format
        ' 
        Format.Text = "Format"
        Format.TextAlign = HorizontalAlignment.Center
        Format.Width = 90
        ' 
        ' Bitrate
        ' 
        Bitrate.Text = "Bitrate"
        Bitrate.TextAlign = HorizontalAlignment.Center
        Bitrate.Width = 75
        ' 
        ' Country
        ' 
        Country.Text = "Country"
        Country.TextAlign = HorizontalAlignment.Center
        Country.Width = 75
        ' 
        ' Status
        ' 
        Status.Text = "Status"
        Status.Width = 140
        ' 
        ' URL
        ' 
        URL.Text = "URL"
        URL.Width = 300
        ' 
        ' CMStations
        ' 
        CMStations.Items.AddRange(New ToolStripItem() {CMIPlay, CMIAddToPlaylist, CMICopyStreamURL})
        CMStations.Name = "CMStations"
        CMStations.Size = New Size(153, 70)
        ' 
        ' CMIPlay
        ' 
        CMIPlay.Image = My.Resources.Resources.ImagePlay
        CMIPlay.Name = "CMIPlay"
        CMIPlay.Size = New Size(152, 22)
        CMIPlay.Text = "Play"
        ' 
        ' CMIAddToPlaylist
        ' 
        CMIAddToPlaylist.Image = My.Resources.Resources.ImageAdd16
        CMIAddToPlaylist.Name = "CMIAddToPlaylist"
        CMIAddToPlaylist.Size = New Size(152, 22)
        CMIAddToPlaylist.Text = "Add To Playlist"
        ' 
        ' CMICopyStreamURL
        ' 
        CMICopyStreamURL.Image = My.Resources.Resources.ImageCopy16
        CMICopyStreamURL.Name = "CMICopyStreamURL"
        CMICopyStreamURL.Size = New Size(152, 22)
        CMICopyStreamURL.Text = "Copy URL"
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
    Friend WithEvents StreamName As ColumnHeader
    Friend WithEvents Tags As ColumnHeader
    Friend WithEvents Bitrate As ColumnHeader
    Friend WithEvents Country As ColumnHeader
    Friend WithEvents Status As ColumnHeader
    Friend WithEvents BtnSearch As Button
    Friend WithEvents TxtBoxSearch As TextBox
    Friend WithEvents Source As ColumnHeader
    Friend WithEvents CMStations As ContextMenuStrip
    Friend WithEvents CMIPlay As ToolStripMenuItem
    Friend WithEvents CMIAddToPlaylist As ToolStripMenuItem
    Friend WithEvents CMICopyStreamURL As ToolStripMenuItem
    Friend WithEvents StatusLabel As ToolStripStatusLabel
    Friend WithEvents URL As ColumnHeader
    Friend WithEvents Format As ColumnHeader
End Class
