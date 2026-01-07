<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class History
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(History))
        BtnOK = New Button()
        LblTotalPlayedSongs = New Skye.UI.Label()
        TxtBoxTotalPlayedSongs = New TextBox()
        TxtBoxSessionPlayedSongs = New TextBox()
        LblSessionPlayedSongs = New Skye.UI.Label()
        TxtBoxTotalDuration = New TextBox()
        LblTotalDuration = New Skye.UI.Label()
        TxtBoxMostPlayedSong = New TextBox()
        LblMostPlayedSong = New Skye.UI.Label()
        LVHistory = New Skye.UI.ListViewEX()
        CMHistoryView = New ContextMenuStrip(components)
        CMIQueue = New ToolStripMenuItem()
        CMIQueueAll = New ToolStripMenuItem()
        CMIAddToPlaylist = New ToolStripMenuItem()
        CMIAddAllToPlaylist = New ToolStripMenuItem()
        GrpBoxHistory = New GroupBox()
        RadBtnForgottenSongs = New RadioButton()
        RadBtnMostPlayedArtists = New RadioButton()
        BtnCharts = New Button()
        RadBtnFavorites = New RadioButton()
        RadBtnRecentlyPlayed = New RadioButton()
        RadBtnMostPlayed = New RadioButton()
        TxtBoxMaxRecords = New TextBox()
        BtnShowAll = New Button()
        LblMaxRecords = New Label()
        GrpBoxCharts = New GroupBox()
        RadBtnRatingVsPlays = New RadioButton()
        RadBtnPeakHours = New RadioButton()
        RadBtnStreaks = New RadioButton()
        RadBtnArtistWordCloud = New RadioButton()
        RadBtnGenrePareto = New RadioButton()
        RadBtnGenrePolar = New RadioButton()
        BtnLists = New Button()
        RadBtnArtists = New RadioButton()
        RadBtnGenres = New RadioButton()
        LblHistoryViewCount = New Label()
        PanelCharts = New Panel()
        TxtBoxSessionPlayedDuration = New TextBox()
        LblSessionPlayedDuration = New Skye.UI.Label()
        CMHistoryView.SuspendLayout()
        GrpBoxHistory.SuspendLayout()
        GrpBoxCharts.SuspendLayout()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(482, 556)
        BtnOK.Margin = New Padding(4)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 0
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' LblTotalPlayedSongs
        ' 
        LblTotalPlayedSongs.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblTotalPlayedSongs.BackColor = Color.Transparent
        LblTotalPlayedSongs.Font = New Font("Segoe UI", 12F)
        LblTotalPlayedSongs.Location = New Point(719, 10)
        LblTotalPlayedSongs.Margin = New Padding(4, 0, 4, 0)
        LblTotalPlayedSongs.Name = "LblTotalPlayedSongs"
        LblTotalPlayedSongs.Size = New Size(180, 32)
        LblTotalPlayedSongs.TabIndex = 1
        LblTotalPlayedSongs.Text = "Total Played Songs"
        LblTotalPlayedSongs.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TxtBoxTotalPlayedSongs
        ' 
        TxtBoxTotalPlayedSongs.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TxtBoxTotalPlayedSongs.Font = New Font("Segoe UI", 12F)
        TxtBoxTotalPlayedSongs.Location = New Point(896, 13)
        TxtBoxTotalPlayedSongs.Margin = New Padding(4)
        TxtBoxTotalPlayedSongs.Name = "TxtBoxTotalPlayedSongs"
        TxtBoxTotalPlayedSongs.ReadOnly = True
        TxtBoxTotalPlayedSongs.ShortcutsEnabled = False
        TxtBoxTotalPlayedSongs.Size = New Size(120, 29)
        TxtBoxTotalPlayedSongs.TabIndex = 2
        TxtBoxTotalPlayedSongs.TabStop = False
        TxtBoxTotalPlayedSongs.TextAlign = HorizontalAlignment.Center
        ' 
        ' TxtBoxSessionPlayedSongs
        ' 
        TxtBoxSessionPlayedSongs.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TxtBoxSessionPlayedSongs.Font = New Font("Segoe UI", 12F)
        TxtBoxSessionPlayedSongs.Location = New Point(635, 13)
        TxtBoxSessionPlayedSongs.Margin = New Padding(4)
        TxtBoxSessionPlayedSongs.Name = "TxtBoxSessionPlayedSongs"
        TxtBoxSessionPlayedSongs.ReadOnly = True
        TxtBoxSessionPlayedSongs.ShortcutsEnabled = False
        TxtBoxSessionPlayedSongs.Size = New Size(120, 29)
        TxtBoxSessionPlayedSongs.TabIndex = 4
        TxtBoxSessionPlayedSongs.TabStop = False
        TxtBoxSessionPlayedSongs.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblSessionPlayedSongs
        ' 
        LblSessionPlayedSongs.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblSessionPlayedSongs.BackColor = Color.Transparent
        LblSessionPlayedSongs.Font = New Font("Segoe UI", 12F)
        LblSessionPlayedSongs.Location = New Point(476, 11)
        LblSessionPlayedSongs.Margin = New Padding(4, 0, 4, 0)
        LblSessionPlayedSongs.Name = "LblSessionPlayedSongs"
        LblSessionPlayedSongs.Size = New Size(162, 32)
        LblSessionPlayedSongs.TabIndex = 3
        LblSessionPlayedSongs.Text = "Session Played Songs"
        LblSessionPlayedSongs.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TxtBoxTotalDuration
        ' 
        TxtBoxTotalDuration.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TxtBoxTotalDuration.Font = New Font("Segoe UI", 12F)
        TxtBoxTotalDuration.Location = New Point(896, 50)
        TxtBoxTotalDuration.Margin = New Padding(4)
        TxtBoxTotalDuration.Name = "TxtBoxTotalDuration"
        TxtBoxTotalDuration.ReadOnly = True
        TxtBoxTotalDuration.ShortcutsEnabled = False
        TxtBoxTotalDuration.Size = New Size(120, 29)
        TxtBoxTotalDuration.TabIndex = 5
        TxtBoxTotalDuration.TabStop = False
        TxtBoxTotalDuration.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblTotalDuration
        ' 
        LblTotalDuration.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblTotalDuration.BackColor = Color.Transparent
        LblTotalDuration.Font = New Font("Segoe UI", 12F)
        LblTotalDuration.Location = New Point(719, 47)
        LblTotalDuration.Margin = New Padding(4, 0, 4, 0)
        LblTotalDuration.Name = "LblTotalDuration"
        LblTotalDuration.Size = New Size(180, 32)
        LblTotalDuration.TabIndex = 6
        LblTotalDuration.Text = "Total Duration"
        LblTotalDuration.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TxtBoxMostPlayedSong
        ' 
        TxtBoxMostPlayedSong.Font = New Font("Segoe UI", 12F)
        TxtBoxMostPlayedSong.Location = New Point(13, 50)
        TxtBoxMostPlayedSong.Margin = New Padding(4)
        TxtBoxMostPlayedSong.Name = "TxtBoxMostPlayedSong"
        TxtBoxMostPlayedSong.ReadOnly = True
        TxtBoxMostPlayedSong.ShortcutsEnabled = False
        TxtBoxMostPlayedSong.Size = New Size(455, 29)
        TxtBoxMostPlayedSong.TabIndex = 7
        TxtBoxMostPlayedSong.TabStop = False
        TxtBoxMostPlayedSong.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblMostPlayedSong
        ' 
        LblMostPlayedSong.BackColor = Color.Transparent
        LblMostPlayedSong.Font = New Font("Segoe UI", 12F)
        LblMostPlayedSong.Location = New Point(13, 21)
        LblMostPlayedSong.Margin = New Padding(4, 0, 4, 0)
        LblMostPlayedSong.Name = "LblMostPlayedSong"
        LblMostPlayedSong.Size = New Size(456, 32)
        LblMostPlayedSong.TabIndex = 8
        LblMostPlayedSong.Text = "Most Played Song"
        LblMostPlayedSong.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LVHistory
        ' 
        LVHistory.AllowColumnReorder = True
        LVHistory.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LVHistory.ContextMenuStrip = CMHistoryView
        LVHistory.FullRowSelect = True
        LVHistory.HeaderStyle = ColumnHeaderStyle.Nonclickable
        LVHistory.InsertionLineColor = Color.Teal
        LVHistory.Location = New Point(13, 87)
        LVHistory.Margin = New Padding(4)
        LVHistory.Name = "LVHistory"
        LVHistory.Size = New Size(805, 434)
        LVHistory.TabIndex = 0
        LVHistory.UseCompatibleStateImageBehavior = False
        LVHistory.View = View.Details
        ' 
        ' CMHistoryView
        ' 
        CMHistoryView.Items.AddRange(New ToolStripItem() {CMIQueue, CMIQueueAll, CMIAddToPlaylist, CMIAddAllToPlaylist})
        CMHistoryView.Name = "CMHistoryView"
        CMHistoryView.Size = New Size(170, 92)
        ' 
        ' CMIQueue
        ' 
        CMIQueue.Image = My.Resources.Resources.ImagePlay
        CMIQueue.Name = "CMIQueue"
        CMIQueue.Size = New Size(169, 22)
        CMIQueue.Text = "Queue"
        ' 
        ' CMIQueueAll
        ' 
        CMIQueueAll.Image = My.Resources.Resources.ImagePlay
        CMIQueueAll.Name = "CMIQueueAll"
        CMIQueueAll.Size = New Size(169, 22)
        CMIQueueAll.Text = "Queue All"
        ' 
        ' CMIAddToPlaylist
        ' 
        CMIAddToPlaylist.Image = My.Resources.Resources.ImageAdd16
        CMIAddToPlaylist.Name = "CMIAddToPlaylist"
        CMIAddToPlaylist.Size = New Size(169, 22)
        CMIAddToPlaylist.Text = "Add To Playlist"
        ' 
        ' CMIAddAllToPlaylist
        ' 
        CMIAddAllToPlaylist.Image = My.Resources.Resources.ImageAddAll16
        CMIAddAllToPlaylist.Name = "CMIAddAllToPlaylist"
        CMIAddAllToPlaylist.Size = New Size(169, 22)
        CMIAddAllToPlaylist.Text = "Add All To Playlist"
        ' 
        ' GrpBoxHistory
        ' 
        GrpBoxHistory.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        GrpBoxHistory.BackColor = Color.Transparent
        GrpBoxHistory.Controls.Add(RadBtnForgottenSongs)
        GrpBoxHistory.Controls.Add(RadBtnMostPlayedArtists)
        GrpBoxHistory.Controls.Add(BtnCharts)
        GrpBoxHistory.Controls.Add(RadBtnFavorites)
        GrpBoxHistory.Controls.Add(RadBtnRecentlyPlayed)
        GrpBoxHistory.Controls.Add(RadBtnMostPlayed)
        GrpBoxHistory.Location = New Point(826, 87)
        GrpBoxHistory.Margin = New Padding(4)
        GrpBoxHistory.Name = "GrpBoxHistory"
        GrpBoxHistory.Padding = New Padding(4)
        GrpBoxHistory.Size = New Size(190, 426)
        GrpBoxHistory.TabIndex = 0
        GrpBoxHistory.TabStop = False
        ' 
        ' RadBtnForgottenSongs
        ' 
        RadBtnForgottenSongs.Location = New Point(7, 112)
        RadBtnForgottenSongs.Name = "RadBtnForgottenSongs"
        RadBtnForgottenSongs.Size = New Size(176, 32)
        RadBtnForgottenSongs.TabIndex = 8
        RadBtnForgottenSongs.Text = "Forgotten Songs"
        RadBtnForgottenSongs.TextAlign = ContentAlignment.MiddleCenter
        RadBtnForgottenSongs.UseVisualStyleBackColor = True
        ' 
        ' RadBtnMostPlayedArtists
        ' 
        RadBtnMostPlayedArtists.Location = New Point(7, 51)
        RadBtnMostPlayedArtists.Name = "RadBtnMostPlayedArtists"
        RadBtnMostPlayedArtists.Size = New Size(176, 32)
        RadBtnMostPlayedArtists.TabIndex = 7
        RadBtnMostPlayedArtists.Text = "Most Played Artists"
        RadBtnMostPlayedArtists.TextAlign = ContentAlignment.MiddleCenter
        RadBtnMostPlayedArtists.UseVisualStyleBackColor = True
        ' 
        ' BtnCharts
        ' 
        BtnCharts.Anchor = AnchorStyles.Bottom
        BtnCharts.Location = New Point(7, 387)
        BtnCharts.Name = "BtnCharts"
        BtnCharts.Size = New Size(176, 32)
        BtnCharts.TabIndex = 6
        BtnCharts.TabStop = False
        BtnCharts.Text = "Charts"
        BtnCharts.UseVisualStyleBackColor = True
        ' 
        ' RadBtnFavorites
        ' 
        RadBtnFavorites.Location = New Point(7, 143)
        RadBtnFavorites.Name = "RadBtnFavorites"
        RadBtnFavorites.Size = New Size(176, 32)
        RadBtnFavorites.TabIndex = 2
        RadBtnFavorites.Text = "Favorites"
        RadBtnFavorites.TextAlign = ContentAlignment.MiddleCenter
        RadBtnFavorites.UseVisualStyleBackColor = True
        ' 
        ' RadBtnRecentlyPlayed
        ' 
        RadBtnRecentlyPlayed.Location = New Point(7, 82)
        RadBtnRecentlyPlayed.Name = "RadBtnRecentlyPlayed"
        RadBtnRecentlyPlayed.Size = New Size(176, 32)
        RadBtnRecentlyPlayed.TabIndex = 1
        RadBtnRecentlyPlayed.Text = "Recently Played"
        RadBtnRecentlyPlayed.TextAlign = ContentAlignment.MiddleCenter
        RadBtnRecentlyPlayed.UseVisualStyleBackColor = True
        ' 
        ' RadBtnMostPlayed
        ' 
        RadBtnMostPlayed.Location = New Point(7, 20)
        RadBtnMostPlayed.Name = "RadBtnMostPlayed"
        RadBtnMostPlayed.Size = New Size(176, 32)
        RadBtnMostPlayed.TabIndex = 0
        RadBtnMostPlayed.Text = "Most Played Song"
        RadBtnMostPlayed.TextAlign = ContentAlignment.MiddleCenter
        RadBtnMostPlayed.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxMaxRecords
        ' 
        TxtBoxMaxRecords.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        TxtBoxMaxRecords.Location = New Point(12, 592)
        TxtBoxMaxRecords.Name = "TxtBoxMaxRecords"
        TxtBoxMaxRecords.ShortcutsEnabled = False
        TxtBoxMaxRecords.Size = New Size(88, 29)
        TxtBoxMaxRecords.TabIndex = 4
        TxtBoxMaxRecords.TabStop = False
        TxtBoxMaxRecords.TextAlign = HorizontalAlignment.Center
        ' 
        ' BtnShowAll
        ' 
        BtnShowAll.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnShowAll.Location = New Point(98, 590)
        BtnShowAll.Name = "BtnShowAll"
        BtnShowAll.Size = New Size(88, 32)
        BtnShowAll.TabIndex = 3
        BtnShowAll.TabStop = False
        BtnShowAll.Text = "Show All"
        BtnShowAll.UseVisualStyleBackColor = True
        ' 
        ' LblMaxRecords
        ' 
        LblMaxRecords.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        LblMaxRecords.Location = New Point(7, 567)
        LblMaxRecords.Name = "LblMaxRecords"
        LblMaxRecords.Size = New Size(99, 25)
        LblMaxRecords.TabIndex = 5
        LblMaxRecords.Text = "Max Records"
        LblMaxRecords.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' GrpBoxCharts
        ' 
        GrpBoxCharts.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        GrpBoxCharts.BackColor = Color.Transparent
        GrpBoxCharts.Controls.Add(RadBtnRatingVsPlays)
        GrpBoxCharts.Controls.Add(RadBtnPeakHours)
        GrpBoxCharts.Controls.Add(RadBtnStreaks)
        GrpBoxCharts.Controls.Add(RadBtnArtistWordCloud)
        GrpBoxCharts.Controls.Add(RadBtnGenrePareto)
        GrpBoxCharts.Controls.Add(RadBtnGenrePolar)
        GrpBoxCharts.Controls.Add(BtnLists)
        GrpBoxCharts.Controls.Add(RadBtnArtists)
        GrpBoxCharts.Controls.Add(RadBtnGenres)
        GrpBoxCharts.Location = New Point(826, 87)
        GrpBoxCharts.Margin = New Padding(4)
        GrpBoxCharts.Name = "GrpBoxCharts"
        GrpBoxCharts.Padding = New Padding(4)
        GrpBoxCharts.Size = New Size(190, 426)
        GrpBoxCharts.TabIndex = 0
        GrpBoxCharts.TabStop = False
        ' 
        ' RadBtnRatingVsPlays
        ' 
        RadBtnRatingVsPlays.Location = New Point(7, 232)
        RadBtnRatingVsPlays.Name = "RadBtnRatingVsPlays"
        RadBtnRatingVsPlays.Size = New Size(176, 32)
        RadBtnRatingVsPlays.TabIndex = 12
        RadBtnRatingVsPlays.Text = "Rating Vs Plays"
        RadBtnRatingVsPlays.TextAlign = ContentAlignment.MiddleCenter
        RadBtnRatingVsPlays.UseVisualStyleBackColor = True
        ' 
        ' RadBtnPeakHours
        ' 
        RadBtnPeakHours.Location = New Point(7, 203)
        RadBtnPeakHours.Name = "RadBtnPeakHours"
        RadBtnPeakHours.Size = New Size(176, 32)
        RadBtnPeakHours.TabIndex = 11
        RadBtnPeakHours.Text = "Peak Hours"
        RadBtnPeakHours.TextAlign = ContentAlignment.MiddleCenter
        RadBtnPeakHours.UseVisualStyleBackColor = True
        ' 
        ' RadBtnStreaks
        ' 
        RadBtnStreaks.Location = New Point(7, 174)
        RadBtnStreaks.Name = "RadBtnStreaks"
        RadBtnStreaks.Size = New Size(176, 32)
        RadBtnStreaks.TabIndex = 10
        RadBtnStreaks.Text = "Longest Streaks"
        RadBtnStreaks.TextAlign = ContentAlignment.MiddleCenter
        RadBtnStreaks.UseVisualStyleBackColor = True
        ' 
        ' RadBtnArtistWordCloud
        ' 
        RadBtnArtistWordCloud.Location = New Point(7, 144)
        RadBtnArtistWordCloud.Name = "RadBtnArtistWordCloud"
        RadBtnArtistWordCloud.Size = New Size(176, 32)
        RadBtnArtistWordCloud.TabIndex = 9
        RadBtnArtistWordCloud.Text = "Artist Word Cloud"
        RadBtnArtistWordCloud.TextAlign = ContentAlignment.MiddleCenter
        RadBtnArtistWordCloud.UseVisualStyleBackColor = True
        ' 
        ' RadBtnGenrePareto
        ' 
        RadBtnGenrePareto.Location = New Point(7, 84)
        RadBtnGenrePareto.Name = "RadBtnGenrePareto"
        RadBtnGenrePareto.Size = New Size(176, 32)
        RadBtnGenrePareto.TabIndex = 8
        RadBtnGenrePareto.Text = "Genre Pareto"
        RadBtnGenrePareto.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' RadBtnGenrePolar
        ' 
        RadBtnGenrePolar.Location = New Point(7, 52)
        RadBtnGenrePolar.Name = "RadBtnGenrePolar"
        RadBtnGenrePolar.Size = New Size(176, 32)
        RadBtnGenrePolar.TabIndex = 7
        RadBtnGenrePolar.Text = "Genre Polar"
        RadBtnGenrePolar.TextAlign = ContentAlignment.MiddleCenter
        RadBtnGenrePolar.UseVisualStyleBackColor = True
        ' 
        ' BtnLists
        ' 
        BtnLists.Anchor = AnchorStyles.Bottom
        BtnLists.Location = New Point(7, 387)
        BtnLists.Name = "BtnLists"
        BtnLists.Size = New Size(176, 32)
        BtnLists.TabIndex = 6
        BtnLists.TabStop = False
        BtnLists.Text = "Lists"
        BtnLists.UseVisualStyleBackColor = True
        ' 
        ' RadBtnArtists
        ' 
        RadBtnArtists.Location = New Point(7, 115)
        RadBtnArtists.Name = "RadBtnArtists"
        RadBtnArtists.Size = New Size(176, 32)
        RadBtnArtists.TabIndex = 1
        RadBtnArtists.Text = "Artists"
        RadBtnArtists.TextAlign = ContentAlignment.MiddleCenter
        RadBtnArtists.UseVisualStyleBackColor = True
        ' 
        ' RadBtnGenres
        ' 
        RadBtnGenres.Location = New Point(7, 20)
        RadBtnGenres.Name = "RadBtnGenres"
        RadBtnGenres.Size = New Size(176, 32)
        RadBtnGenres.TabIndex = 0
        RadBtnGenres.Text = "Genres"
        RadBtnGenres.TextAlign = ContentAlignment.MiddleCenter
        RadBtnGenres.UseVisualStyleBackColor = True
        ' 
        ' LblHistoryViewCount
        ' 
        LblHistoryViewCount.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LblHistoryViewCount.AutoSize = True
        LblHistoryViewCount.Location = New Point(12, 524)
        LblHistoryViewCount.Name = "LblHistoryViewCount"
        LblHistoryViewCount.Size = New Size(112, 21)
        LblHistoryViewCount.TabIndex = 10
        LblHistoryViewCount.Text = "Listview Count"
        LblHistoryViewCount.Visible = False
        ' 
        ' PanelCharts
        ' 
        PanelCharts.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        PanelCharts.Location = New Point(13, 86)
        PanelCharts.Name = "PanelCharts"
        PanelCharts.Size = New Size(805, 435)
        PanelCharts.TabIndex = 13
        ' 
        ' TxtBoxSessionPlayedDuration
        ' 
        TxtBoxSessionPlayedDuration.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TxtBoxSessionPlayedDuration.Font = New Font("Segoe UI", 12F)
        TxtBoxSessionPlayedDuration.Location = New Point(635, 50)
        TxtBoxSessionPlayedDuration.Margin = New Padding(4)
        TxtBoxSessionPlayedDuration.Name = "TxtBoxSessionPlayedDuration"
        TxtBoxSessionPlayedDuration.ReadOnly = True
        TxtBoxSessionPlayedDuration.ShortcutsEnabled = False
        TxtBoxSessionPlayedDuration.Size = New Size(120, 29)
        TxtBoxSessionPlayedDuration.TabIndex = 14
        TxtBoxSessionPlayedDuration.TabStop = False
        TxtBoxSessionPlayedDuration.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblSessionPlayedDuration
        ' 
        LblSessionPlayedDuration.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblSessionPlayedDuration.BackColor = Color.Transparent
        LblSessionPlayedDuration.Font = New Font("Segoe UI", 12F)
        LblSessionPlayedDuration.Location = New Point(527, 47)
        LblSessionPlayedDuration.Margin = New Padding(4, 0, 4, 0)
        LblSessionPlayedDuration.Name = "LblSessionPlayedDuration"
        LblSessionPlayedDuration.Size = New Size(111, 32)
        LblSessionPlayedDuration.TabIndex = 15
        LblSessionPlayedDuration.Text = "Total Duration"
        LblSessionPlayedDuration.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' History
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1029, 633)
        Controls.Add(GrpBoxHistory)
        Controls.Add(TxtBoxMaxRecords)
        Controls.Add(BtnShowAll)
        Controls.Add(TxtBoxSessionPlayedDuration)
        Controls.Add(LblSessionPlayedDuration)
        Controls.Add(LblMaxRecords)
        Controls.Add(TxtBoxMostPlayedSong)
        Controls.Add(LblMostPlayedSong)
        Controls.Add(TxtBoxTotalDuration)
        Controls.Add(TxtBoxSessionPlayedSongs)
        Controls.Add(LblSessionPlayedSongs)
        Controls.Add(TxtBoxTotalPlayedSongs)
        Controls.Add(BtnOK)
        Controls.Add(LblTotalPlayedSongs)
        Controls.Add(LblTotalDuration)
        Controls.Add(LVHistory)
        Controls.Add(LblHistoryViewCount)
        Controls.Add(PanelCharts)
        Controls.Add(GrpBoxCharts)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        Margin = New Padding(4)
        MinimumSize = New Size(900, 400)
        Name = "History"
        StartPosition = FormStartPosition.CenterScreen
        Text = "History & Statistics"
        CMHistoryView.ResumeLayout(False)
        GrpBoxHistory.ResumeLayout(False)
        GrpBoxCharts.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents LblTotalPlayedSongs As Skye.UI.Label
    Friend WithEvents TxtBoxTotalPlayedSongs As TextBox
    Friend WithEvents TxtBoxSessionPlayedSongs As TextBox
    Friend WithEvents LblSessionPlayedSongs As Skye.UI.Label
    Friend WithEvents TxtBoxTotalDuration As TextBox
    Friend WithEvents LblTotalDuration As Skye.UI.Label
    Friend WithEvents LVHistory As Skye.UI.ListViewEX
    Friend WithEvents GrpBoxHistory As GroupBox
    Private WithEvents TxtBoxMostPlayedSong As TextBox
    Private WithEvents LblMostPlayedSong As Skye.UI.Label
    Friend WithEvents RadBtnFavorites As RadioButton
    Friend WithEvents RadBtnRecentlyPlayed As RadioButton
    Friend WithEvents RadBtnMostPlayed As RadioButton
    Friend WithEvents LblHistoryViewCount As Label
    Friend WithEvents CMHistoryView As ContextMenuStrip
    Friend WithEvents CMIQueue As ToolStripMenuItem
    Friend WithEvents CMIAddToPlaylist As ToolStripMenuItem
    Friend WithEvents BtnCharts As Button
    Friend WithEvents PanelCharts As Panel
    Friend WithEvents GrpBoxCharts As GroupBox
    Friend WithEvents BtnLists As Button
    Friend WithEvents RadBtnArtists As RadioButton
    Friend WithEvents RadBtnGenres As RadioButton
    Friend WithEvents TxtBoxMaxRecords As TextBox
    Friend WithEvents BtnShowAll As Button
    Friend WithEvents LblMaxRecords As Label
    Friend WithEvents RadBtnGenrePolar As RadioButton
    Friend WithEvents RadBtnGenrePareto As RadioButton
    Friend WithEvents RadBtnArtistWordCloud As RadioButton
    Friend WithEvents TxtBoxSessionPlayedDuration As TextBox
    Friend WithEvents LblSessionPlayedDuration As Skye.UI.Label
    Friend WithEvents RadBtnMostPlayedArtists As RadioButton
    Friend WithEvents RadBtnStreaks As RadioButton
    Friend WithEvents CMIQueueAll As ToolStripMenuItem
    Friend WithEvents CMIAddAllToPlaylist As ToolStripMenuItem
    Friend WithEvents RadBtnPeakHours As RadioButton
    Friend WithEvents RadBtnRatingVsPlays As RadioButton
    Friend WithEvents RadBtnForgottenSongs As RadioButton
End Class
