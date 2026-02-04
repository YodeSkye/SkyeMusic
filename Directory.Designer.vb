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
        PanelPodcasts = New Panel()
        LVEpisodes = New ListView()
        ColumnEpisodesTitle = New ColumnHeader()
        ColumnEpisodesDuration = New ColumnHeader()
        ColumnEpisodesReleaseDate = New ColumnHeader()
        ColumnEpisodesDescription = New ColumnHeader()
        ColumnEpisodesURL = New ColumnHeader()
        CMEpisodes = New ContextMenuStrip(components)
        CMIEpisodePlay = New ToolStripMenuItem()
        CMIEpisodeAddToPlaylist = New ToolStripMenuItem()
        CMIEpisodeDownload = New ToolStripMenuItem()
        CMIEpisodeAddToFavorites = New ToolStripMenuItem()
        CMIEpisodeRemoveFromFavorites = New ToolStripMenuItem()
        CMIEpisodeCopyURL = New ToolStripMenuItem()
        LVPodcasts = New ListView()
        ColumnPodcastsArtwork = New ColumnHeader()
        ColumnPodcastsTitle = New ColumnHeader()
        ColumnPodcastsAuthor = New ColumnHeader()
        ColumnPodcastsGenre = New ColumnHeader()
        ColumnPodcastsURL = New ColumnHeader()
        ILPodcasts = New ImageList(components)
        PanelSearch.SuspendLayout()
        StatusStripDirectory.SuspendLayout()
        PanelSources.SuspendLayout()
        PanelStreams.SuspendLayout()
        CMStations.SuspendLayout()
        PanelPodcasts.SuspendLayout()
        CMEpisodes.SuspendLayout()
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
        StatusStripDirectory.Items.AddRange(New ToolStripItem() {StatusLabel})
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
        ' PanelSources
        ' 
        PanelSources.Controls.Add(LVSources)
        PanelSources.Dock = DockStyle.Left
        PanelSources.Location = New Point(0, 49)
        PanelSources.Name = "PanelSources"
        PanelSources.Size = New Size(95, 650)
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
        LVSources.Size = New Size(95, 650)
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
        PanelStreams.Location = New Point(95, 49)
        PanelStreams.Name = "PanelStreams"
        PanelStreams.Size = New Size(1089, 650)
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
        LVStations.Name = "LVStations"
        LVStations.Size = New Size(1089, 650)
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
        ' PanelPodcasts
        ' 
        PanelPodcasts.Controls.Add(LVEpisodes)
        PanelPodcasts.Controls.Add(LVPodcasts)
        PanelPodcasts.Dock = DockStyle.Fill
        PanelPodcasts.Location = New Point(95, 49)
        PanelPodcasts.Name = "PanelPodcasts"
        PanelPodcasts.Size = New Size(1089, 650)
        PanelPodcasts.TabIndex = 7
        PanelPodcasts.Visible = False
        ' 
        ' LVEpisodes
        ' 
        LVEpisodes.Columns.AddRange(New ColumnHeader() {ColumnEpisodesTitle, ColumnEpisodesDuration, ColumnEpisodesReleaseDate, ColumnEpisodesDescription, ColumnEpisodesURL})
        LVEpisodes.ContextMenuStrip = CMEpisodes
        LVEpisodes.Dock = DockStyle.Fill
        LVEpisodes.FullRowSelect = True
        LVEpisodes.Location = New Point(0, 250)
        LVEpisodes.Name = "LVEpisodes"
        LVEpisodes.Size = New Size(1089, 400)
        LVEpisodes.TabIndex = 1
        LVEpisodes.UseCompatibleStateImageBehavior = False
        LVEpisodes.View = View.Details
        ' 
        ' ColumnEpisodesTitle
        ' 
        ColumnEpisodesTitle.Text = "Episode Title"
        ColumnEpisodesTitle.Width = 400
        ' 
        ' ColumnEpisodesDuration
        ' 
        ColumnEpisodesDuration.Text = "Duration"
        ColumnEpisodesDuration.Width = 75
        ' 
        ' ColumnEpisodesReleaseDate
        ' 
        ColumnEpisodesReleaseDate.Text = "Release Date"
        ColumnEpisodesReleaseDate.Width = 110
        ' 
        ' ColumnEpisodesDescription
        ' 
        ColumnEpisodesDescription.Text = "Description"
        ColumnEpisodesDescription.Width = 400
        ' 
        ' ColumnEpisodesURL
        ' 
        ColumnEpisodesURL.Text = "URL"
        ColumnEpisodesURL.Width = 400
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
        LVPodcasts.Columns.AddRange(New ColumnHeader() {ColumnPodcastsArtwork, ColumnPodcastsTitle, ColumnPodcastsAuthor, ColumnPodcastsGenre, ColumnPodcastsURL})
        LVPodcasts.Dock = DockStyle.Top
        LVPodcasts.FullRowSelect = True
        LVPodcasts.Location = New Point(0, 0)
        LVPodcasts.Name = "LVPodcasts"
        LVPodcasts.Size = New Size(1089, 250)
        LVPodcasts.SmallImageList = ILPodcasts
        LVPodcasts.TabIndex = 0
        LVPodcasts.UseCompatibleStateImageBehavior = False
        LVPodcasts.View = View.Details
        ' 
        ' ColumnPodcastsArtwork
        ' 
        ColumnPodcastsArtwork.Text = ""
        ColumnPodcastsArtwork.Width = 32
        ' 
        ' ColumnPodcastsTitle
        ' 
        ColumnPodcastsTitle.Text = "Podcast Title"
        ColumnPodcastsTitle.Width = 400
        ' 
        ' ColumnPodcastsAuthor
        ' 
        ColumnPodcastsAuthor.Text = "Author"
        ColumnPodcastsAuthor.Width = 200
        ' 
        ' ColumnPodcastsGenre
        ' 
        ColumnPodcastsGenre.Text = "Genre"
        ColumnPodcastsGenre.Width = 150
        ' 
        ' ColumnPodcastsURL
        ' 
        ColumnPodcastsURL.Text = "Feed URL"
        ColumnPodcastsURL.Width = 270
        ' 
        ' ILPodcasts
        ' 
        ILPodcasts.ColorDepth = ColorDepth.Depth32Bit
        ILPodcasts.ImageSize = New Size(24, 24)
        ILPodcasts.TransparentColor = Color.Transparent
        ' 
        ' Directory
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(1184, 721)
        Controls.Add(PanelPodcasts)
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
        PanelPodcasts.ResumeLayout(False)
        CMEpisodes.ResumeLayout(False)
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
    Friend WithEvents PanelPodcasts As Panel
    Friend WithEvents LVPodcasts As ListView
    Friend WithEvents LVEpisodes As ListView
    Friend WithEvents CMEpisodes As ContextMenuStrip
    Friend WithEvents CMIEpisodePlay As ToolStripMenuItem
    Friend WithEvents CMIEpisodeDownload As ToolStripMenuItem
    Friend WithEvents CMIEpisodeAddToPlaylist As ToolStripMenuItem
    Friend WithEvents CMIEpisodeAddToFavorites As ToolStripMenuItem
    Friend WithEvents CMIEpisodeRemoveFromFavorites As ToolStripMenuItem
    Friend WithEvents CMIEpisodeCopyURL As ToolStripMenuItem
    Friend WithEvents ColumnPodcastsArtwork As ColumnHeader
    Friend WithEvents ColumnPodcastsTitle As ColumnHeader
    Friend WithEvents ColumnPodcastsAuthor As ColumnHeader
    Friend WithEvents ColumnPodcastsGenre As ColumnHeader
    Friend WithEvents ColumnPodcastsURL As ColumnHeader
    Friend WithEvents ColumnEpisodesTitle As ColumnHeader
    Friend WithEvents ColumnEpisodesDuration As ColumnHeader
    Friend WithEvents ColumnEpisodesReleaseDate As ColumnHeader
    Friend WithEvents ColumnEpisodesDescription As ColumnHeader
    Friend WithEvents ColumnEpisodesURL As ColumnHeader
    Friend WithEvents ILPodcasts As ImageList
End Class
