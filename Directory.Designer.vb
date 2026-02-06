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
        StatusProgressBar = New ToolStripProgressBar()
        PanelSources = New Panel()
        LVSources = New Skye.UI.ListViewEX()
        Source = New ColumnHeader()
        ILSources = New ImageList(components)
        PanelStreams = New Panel()
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
        CMIStreamPlay = New ToolStripMenuItem()
        CMIStreamAddToPlaylist = New ToolStripMenuItem()
        CMIStreamAddToFavorites = New ToolStripMenuItem()
        CMIStreamRemoveFromFavorites = New ToolStripMenuItem()
        CMIStreamCopyStreamURL = New ToolStripMenuItem()
        LVEpisodes = New ListView()
        ColEpisodesTitle = New ColumnHeader()
        ColEpisodesDuration = New ColumnHeader()
        ColEpisodesReleaseDate = New ColumnHeader()
        ColEpisodesDescription = New ColumnHeader()
        ColEpisodesURL = New ColumnHeader()
        CMEpisodes = New ContextMenuStrip(components)
        CMIEpisodePlay = New ToolStripMenuItem()
        CMIEpisodeAddToPlaylist = New ToolStripMenuItem()
        CMIEpisodeDownload = New ToolStripMenuItem()
        CMIEpisodeAddToFavorites = New ToolStripMenuItem()
        CMIEpisodeRemoveFromFavorites = New ToolStripMenuItem()
        CMIEpisodeCopyURL = New ToolStripMenuItem()
        LVPodcasts = New ListView()
        ColPodcastsArtwork = New ColumnHeader()
        ColPodcastsTitle = New ColumnHeader()
        ColPodcastsAuthor = New ColumnHeader()
        ColPodcastsGenre = New ColumnHeader()
        ColPodcastsURL = New ColumnHeader()
        CMPodcasts = New ContextMenuStrip(components)
        CMIPodcastsAddToFavorites = New ToolStripMenuItem()
        CMIPodcastsRemoveFromFavorites = New ToolStripMenuItem()
        ILPodcasts = New ImageList(components)
        SplitContainerPodcasts = New SplitContainer()
        PanelSearch.SuspendLayout()
        StatusStripDirectory.SuspendLayout()
        PanelSources.SuspendLayout()
        PanelStreams.SuspendLayout()
        CMStations.SuspendLayout()
        CMEpisodes.SuspendLayout()
        CMPodcasts.SuspendLayout()
        CType(SplitContainerPodcasts, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainerPodcasts.Panel1.SuspendLayout()
        SplitContainerPodcasts.Panel2.SuspendLayout()
        SplitContainerPodcasts.SuspendLayout()
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
        PanelSearch.Size = New Size(1184, 49)
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
        StatusStripDirectory.Items.AddRange(New ToolStripItem() {StatusLabel, StatusProgressBar})
        StatusStripDirectory.Location = New Point(0, 699)
        StatusStripDirectory.Name = "StatusStripDirectory"
        StatusStripDirectory.Size = New Size(1184, 22)
        StatusStripDirectory.TabIndex = 2
        ' 
        ' StatusLabel
        ' 
        StatusLabel.Name = "StatusLabel"
        StatusLabel.Size = New Size(39, 17)
        StatusLabel.Text = "Status"
        ' 
        ' StatusProgressBar
        ' 
        StatusProgressBar.Name = "StatusProgressBar"
        StatusProgressBar.Size = New Size(100, 16)
        StatusProgressBar.Visible = False
        ' 
        ' PanelSources
        ' 
        PanelSources.Controls.Add(LVSources)
        PanelSources.Dock = DockStyle.Left
        PanelSources.Location = New Point(0, 49)
        PanelSources.Name = "PanelSources"
        PanelSources.Size = New Size(190, 650)
        PanelSources.TabIndex = 5
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
        LVSources.Size = New Size(190, 650)
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
        ' PanelStreams
        ' 
        PanelStreams.Controls.Add(LVStations)
        PanelStreams.Dock = DockStyle.Fill
        PanelStreams.Location = New Point(190, 49)
        PanelStreams.Name = "PanelStreams"
        PanelStreams.Size = New Size(994, 650)
        PanelStreams.TabIndex = 6
        PanelStreams.Visible = False
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
        LVStations.MultiSelect = False
        LVStations.Name = "LVStations"
        LVStations.Size = New Size(994, 650)
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
        CMStations.Items.AddRange(New ToolStripItem() {CMIStreamPlay, CMIStreamAddToPlaylist, CMIStreamAddToFavorites, CMIStreamRemoveFromFavorites, CMIStreamCopyStreamURL})
        CMStations.Name = "CMStations"
        CMStations.Size = New Size(199, 114)
        ' 
        ' CMIStreamPlay
        ' 
        CMIStreamPlay.Image = My.Resources.Resources.ImagePlay
        CMIStreamPlay.Name = "CMIStreamPlay"
        CMIStreamPlay.Size = New Size(198, 22)
        CMIStreamPlay.Text = "Play"
        ' 
        ' CMIStreamAddToPlaylist
        ' 
        CMIStreamAddToPlaylist.Image = My.Resources.Resources.ImageAdd16
        CMIStreamAddToPlaylist.Name = "CMIStreamAddToPlaylist"
        CMIStreamAddToPlaylist.Size = New Size(198, 22)
        CMIStreamAddToPlaylist.Text = "Add To Playlist"
        ' 
        ' CMIStreamAddToFavorites
        ' 
        CMIStreamAddToFavorites.Image = My.Resources.Resources.ImageFavorites16
        CMIStreamAddToFavorites.Name = "CMIStreamAddToFavorites"
        CMIStreamAddToFavorites.Size = New Size(198, 22)
        CMIStreamAddToFavorites.Text = "Add To Favorites"
        ' 
        ' CMIStreamRemoveFromFavorites
        ' 
        CMIStreamRemoveFromFavorites.Image = My.Resources.Resources.ImageFavorites16
        CMIStreamRemoveFromFavorites.Name = "CMIStreamRemoveFromFavorites"
        CMIStreamRemoveFromFavorites.Size = New Size(198, 22)
        CMIStreamRemoveFromFavorites.Text = "Remove From Favorites"
        ' 
        ' CMIStreamCopyStreamURL
        ' 
        CMIStreamCopyStreamURL.Image = My.Resources.Resources.ImageCopy16
        CMIStreamCopyStreamURL.Name = "CMIStreamCopyStreamURL"
        CMIStreamCopyStreamURL.Size = New Size(198, 22)
        CMIStreamCopyStreamURL.Text = "Copy URL"
        ' 
        ' LVEpisodes
        ' 
        LVEpisodes.Columns.AddRange(New ColumnHeader() {ColEpisodesTitle, ColEpisodesDuration, ColEpisodesReleaseDate, ColEpisodesDescription, ColEpisodesURL})
        LVEpisodes.ContextMenuStrip = CMEpisodes
        LVEpisodes.Dock = DockStyle.Fill
        LVEpisodes.FullRowSelect = True
        LVEpisodes.Location = New Point(0, 0)
        LVEpisodes.MultiSelect = False
        LVEpisodes.Name = "LVEpisodes"
        LVEpisodes.Size = New Size(994, 441)
        LVEpisodes.TabIndex = 1
        LVEpisodes.UseCompatibleStateImageBehavior = False
        LVEpisodes.View = View.Details
        ' 
        ' ColEpisodesTitle
        ' 
        ColEpisodesTitle.Text = "Episode Title"
        ColEpisodesTitle.Width = 400
        ' 
        ' ColEpisodesDuration
        ' 
        ColEpisodesDuration.Text = "Duration"
        ColEpisodesDuration.Width = 75
        ' 
        ' ColEpisodesReleaseDate
        ' 
        ColEpisodesReleaseDate.Text = "Release Date"
        ColEpisodesReleaseDate.Width = 110
        ' 
        ' ColEpisodesDescription
        ' 
        ColEpisodesDescription.Text = "Description"
        ColEpisodesDescription.Width = 400
        ' 
        ' ColEpisodesURL
        ' 
        ColEpisodesURL.Text = "URL"
        ColEpisodesURL.Width = 400
        ' 
        ' CMEpisodes
        ' 
        CMEpisodes.Items.AddRange(New ToolStripItem() {CMIEpisodePlay, CMIEpisodeAddToPlaylist, CMIEpisodeDownload, CMIEpisodeAddToFavorites, CMIEpisodeRemoveFromFavorites, CMIEpisodeCopyURL})
        CMEpisodes.Name = "CMEpisodes"
        CMEpisodes.Size = New Size(199, 136)
        ' 
        ' CMIEpisodePlay
        ' 
        CMIEpisodePlay.Image = My.Resources.Resources.ImagePlay
        CMIEpisodePlay.Name = "CMIEpisodePlay"
        CMIEpisodePlay.Size = New Size(198, 22)
        CMIEpisodePlay.Text = "Play"
        ' 
        ' CMIEpisodeAddToPlaylist
        ' 
        CMIEpisodeAddToPlaylist.Image = My.Resources.Resources.ImageAdd16
        CMIEpisodeAddToPlaylist.Name = "CMIEpisodeAddToPlaylist"
        CMIEpisodeAddToPlaylist.Size = New Size(198, 22)
        CMIEpisodeAddToPlaylist.Text = "Add To Playlist"
        ' 
        ' CMIEpisodeDownload
        ' 
        CMIEpisodeDownload.Image = My.Resources.Resources.ImageSave32
        CMIEpisodeDownload.Name = "CMIEpisodeDownload"
        CMIEpisodeDownload.Size = New Size(198, 22)
        CMIEpisodeDownload.Text = "Download"
        ' 
        ' CMIEpisodeAddToFavorites
        ' 
        CMIEpisodeAddToFavorites.Image = My.Resources.Resources.ImageFavorites16
        CMIEpisodeAddToFavorites.Name = "CMIEpisodeAddToFavorites"
        CMIEpisodeAddToFavorites.Size = New Size(198, 22)
        CMIEpisodeAddToFavorites.Text = "Add To Favorites"
        ' 
        ' CMIEpisodeRemoveFromFavorites
        ' 
        CMIEpisodeRemoveFromFavorites.Image = My.Resources.Resources.ImageFavorites16
        CMIEpisodeRemoveFromFavorites.Name = "CMIEpisodeRemoveFromFavorites"
        CMIEpisodeRemoveFromFavorites.Size = New Size(198, 22)
        CMIEpisodeRemoveFromFavorites.Text = "Remove From Favorites"
        ' 
        ' CMIEpisodeCopyURL
        ' 
        CMIEpisodeCopyURL.Image = My.Resources.Resources.ImageCopy16
        CMIEpisodeCopyURL.Name = "CMIEpisodeCopyURL"
        CMIEpisodeCopyURL.Size = New Size(198, 22)
        CMIEpisodeCopyURL.Text = "Copy URL"
        ' 
        ' LVPodcasts
        ' 
        LVPodcasts.Columns.AddRange(New ColumnHeader() {ColPodcastsArtwork, ColPodcastsTitle, ColPodcastsAuthor, ColPodcastsGenre, ColPodcastsURL})
        LVPodcasts.ContextMenuStrip = CMPodcasts
        LVPodcasts.Dock = DockStyle.Fill
        LVPodcasts.FullRowSelect = True
        LVPodcasts.Location = New Point(0, 0)
        LVPodcasts.MultiSelect = False
        LVPodcasts.Name = "LVPodcasts"
        LVPodcasts.Size = New Size(994, 205)
        LVPodcasts.SmallImageList = ILPodcasts
        LVPodcasts.TabIndex = 0
        LVPodcasts.UseCompatibleStateImageBehavior = False
        LVPodcasts.View = View.Details
        ' 
        ' ColPodcastsArtwork
        ' 
        ColPodcastsArtwork.Text = ""
        ColPodcastsArtwork.Width = 32
        ' 
        ' ColPodcastsTitle
        ' 
        ColPodcastsTitle.Text = "Podcast Title"
        ColPodcastsTitle.Width = 400
        ' 
        ' ColPodcastsAuthor
        ' 
        ColPodcastsAuthor.Text = "Author"
        ColPodcastsAuthor.Width = 200
        ' 
        ' ColPodcastsGenre
        ' 
        ColPodcastsGenre.Text = "Genre"
        ColPodcastsGenre.Width = 150
        ' 
        ' ColPodcastsURL
        ' 
        ColPodcastsURL.Text = "Feed URL"
        ColPodcastsURL.Width = 270
        ' 
        ' CMPodcasts
        ' 
        CMPodcasts.Items.AddRange(New ToolStripItem() {CMIPodcastsAddToFavorites, CMIPodcastsRemoveFromFavorites})
        CMPodcasts.Name = "CMPodcasts"
        CMPodcasts.Size = New Size(199, 48)
        ' 
        ' CMIPodcastsAddToFavorites
        ' 
        CMIPodcastsAddToFavorites.Image = My.Resources.Resources.ImageFavorites16
        CMIPodcastsAddToFavorites.Name = "CMIPodcastsAddToFavorites"
        CMIPodcastsAddToFavorites.Size = New Size(198, 22)
        CMIPodcastsAddToFavorites.Text = "Add To Favorites"
        ' 
        ' CMIPodcastsRemoveFromFavorites
        ' 
        CMIPodcastsRemoveFromFavorites.Image = My.Resources.Resources.ImageFavorites16
        CMIPodcastsRemoveFromFavorites.Name = "CMIPodcastsRemoveFromFavorites"
        CMIPodcastsRemoveFromFavorites.Size = New Size(198, 22)
        CMIPodcastsRemoveFromFavorites.Text = "Remove From Favorites"
        ' 
        ' ILPodcasts
        ' 
        ILPodcasts.ColorDepth = ColorDepth.Depth32Bit
        ILPodcasts.ImageSize = New Size(24, 24)
        ILPodcasts.TransparentColor = Color.Transparent
        ' 
        ' SplitContainerPodcasts
        ' 
        SplitContainerPodcasts.Dock = DockStyle.Fill
        SplitContainerPodcasts.Location = New Point(190, 49)
        SplitContainerPodcasts.Name = "SplitContainerPodcasts"
        SplitContainerPodcasts.Orientation = Orientation.Horizontal
        ' 
        ' SplitContainerPodcasts.Panel1
        ' 
        SplitContainerPodcasts.Panel1.Controls.Add(LVPodcasts)
        ' 
        ' SplitContainerPodcasts.Panel2
        ' 
        SplitContainerPodcasts.Panel2.Controls.Add(LVEpisodes)
        SplitContainerPodcasts.Size = New Size(994, 650)
        SplitContainerPodcasts.SplitterDistance = 205
        SplitContainerPodcasts.TabIndex = 7
        SplitContainerPodcasts.Visible = False
        ' 
        ' Directory
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(1184, 721)
        Controls.Add(SplitContainerPodcasts)
        Controls.Add(PanelStreams)
        Controls.Add(PanelSources)
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
        PanelSources.ResumeLayout(False)
        PanelStreams.ResumeLayout(False)
        CMStations.ResumeLayout(False)
        CMEpisodes.ResumeLayout(False)
        CMPodcasts.ResumeLayout(False)
        SplitContainerPodcasts.Panel1.ResumeLayout(False)
        SplitContainerPodcasts.Panel2.ResumeLayout(False)
        CType(SplitContainerPodcasts, ComponentModel.ISupportInitialize).EndInit()
        SplitContainerPodcasts.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PanelSearch As Panel
    Friend WithEvents StatusStripDirectory As StatusStrip
    Friend WithEvents PanelSources As Panel
    Friend WithEvents PanelStreams As Panel
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
    Friend WithEvents CMIStreamPlay As ToolStripMenuItem
    Friend WithEvents CMIStreamAddToPlaylist As ToolStripMenuItem
    Friend WithEvents CMIStreamCopyStreamURL As ToolStripMenuItem
    Friend WithEvents StatusLabel As ToolStripStatusLabel
    Friend WithEvents ColURL As ColumnHeader
    Friend WithEvents ColFormat As ColumnHeader
    Friend WithEvents ColMore As ColumnHeader
    Friend WithEvents ILSources As ImageList
    Friend WithEvents CMIStreamAddToFavorites As ToolStripMenuItem
    Friend WithEvents CMIStreamRemoveFromFavorites As ToolStripMenuItem
    Friend WithEvents LVPodcasts As ListView
    Friend WithEvents LVEpisodes As ListView
    Friend WithEvents CMEpisodes As ContextMenuStrip
    Friend WithEvents CMIEpisodePlay As ToolStripMenuItem
    Friend WithEvents CMIEpisodeDownload As ToolStripMenuItem
    Friend WithEvents CMIEpisodeAddToPlaylist As ToolStripMenuItem
    Friend WithEvents CMIEpisodeAddToFavorites As ToolStripMenuItem
    Friend WithEvents CMIEpisodeRemoveFromFavorites As ToolStripMenuItem
    Friend WithEvents CMIEpisodeCopyURL As ToolStripMenuItem
    Friend WithEvents ColPodcastsArtwork As ColumnHeader
    Friend WithEvents ColPodcastsTitle As ColumnHeader
    Friend WithEvents ColPodcastsAuthor As ColumnHeader
    Friend WithEvents ColPodcastsGenre As ColumnHeader
    Friend WithEvents ColPodcastsURL As ColumnHeader
    Friend WithEvents ColEpisodesTitle As ColumnHeader
    Friend WithEvents ColEpisodesDuration As ColumnHeader
    Friend WithEvents ColEpisodesReleaseDate As ColumnHeader
    Friend WithEvents ColEpisodesDescription As ColumnHeader
    Friend WithEvents ColEpisodesURL As ColumnHeader
    Friend WithEvents ILPodcasts As ImageList
    Friend WithEvents CMPodcasts As ContextMenuStrip
    Friend WithEvents CMIPodcastsAddToFavorites As ToolStripMenuItem
    Friend WithEvents CMIPodcastsRemoveFromFavorites As ToolStripMenuItem
    Friend WithEvents StatusProgressBar As ToolStripProgressBar
    Friend WithEvents SplitContainerPodcasts As SplitContainer
End Class
