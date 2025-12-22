<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        BtnOK = New Button()
        LblAbout = New Label()
        LLblMicrosoft = New LinkLabel()
        LLblSyncFusion = New LinkLabel()
        LLblIcons8 = New LinkLabel()
        LLblTagLibSharp = New LinkLabel()
        LblVersion = New Skye.UI.Label()
        BtnChangeLog = New Button()
        TipAbout = New Skye.UI.ToolTipEX(components)
        LLblVLCSharp = New LinkLabel()
        LLblNAudio = New LinkLabel()
        LLblSQLite = New LinkLabel()
        LLblMusicBrainz = New LinkLabel()
        LLblSponsorGitHub = New LinkLabel()
        LLblSponsorPayPal = New LinkLabel()
        LblSponsorMe = New Label()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        TipAbout.SetImage(BtnOK, Nothing)
        BtnOK.Location = New Point(160, 421)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 50
        TipAbout.SetText(BtnOK, "Close Window")
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' LblAbout
        ' 
        LblAbout.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LblAbout.BackColor = Color.Transparent
        LblAbout.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LblAbout, Nothing)
        LblAbout.Location = New Point(12, 9)
        LblAbout.Name = "LblAbout"
        LblAbout.Size = New Size(360, 77)
        LblAbout.TabIndex = 2
        LblAbout.Text = "About"
        TipAbout.SetText(LblAbout, Nothing)
        LblAbout.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LLblMicrosoft
        ' 
        LLblMicrosoft.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblMicrosoft.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LLblMicrosoft, Nothing)
        LLblMicrosoft.Image = My.Resources.Resources.ImageAttributionMicrosoft16
        LLblMicrosoft.ImageAlign = ContentAlignment.MiddleLeft
        LLblMicrosoft.LinkBehavior = LinkBehavior.HoverUnderline
        LLblMicrosoft.Location = New Point(12, 112)
        LLblMicrosoft.Name = "LLblMicrosoft"
        LLblMicrosoft.Size = New Size(94, 23)
        LLblMicrosoft.TabIndex = 3
        LLblMicrosoft.TabStop = True
        LLblMicrosoft.Text = "Microsoft"
        TipAbout.SetText(LLblMicrosoft, Nothing)
        LLblMicrosoft.TextAlign = ContentAlignment.TopRight
        ' 
        ' LLblSyncFusion
        ' 
        LLblSyncFusion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblSyncFusion.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LLblSyncFusion, Nothing)
        LLblSyncFusion.Image = My.Resources.Resources.ImageAttributionSyncFusion24
        LLblSyncFusion.LinkBehavior = LinkBehavior.HoverUnderline
        LLblSyncFusion.Location = New Point(218, 144)
        LLblSyncFusion.Name = "LLblSyncFusion"
        LLblSyncFusion.Size = New Size(107, 23)
        LLblSyncFusion.TabIndex = 4
        TipAbout.SetText(LLblSyncFusion, Nothing)
        LLblSyncFusion.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LLblIcons8
        ' 
        LLblIcons8.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblIcons8.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LLblIcons8, Nothing)
        LLblIcons8.Image = My.Resources.Resources.ImageAttributionIcons816
        LLblIcons8.ImageAlign = ContentAlignment.MiddleLeft
        LLblIcons8.LinkBehavior = LinkBehavior.HoverUnderline
        LLblIcons8.Location = New Point(288, 184)
        LLblIcons8.Name = "LLblIcons8"
        LLblIcons8.Size = New Size(70, 23)
        LLblIcons8.TabIndex = 7
        LLblIcons8.TabStop = True
        LLblIcons8.Text = "Icons8"
        TipAbout.SetText(LLblIcons8, Nothing)
        LLblIcons8.TextAlign = ContentAlignment.TopRight
        ' 
        ' LLblTagLibSharp
        ' 
        LLblTagLibSharp.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblTagLibSharp.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LLblTagLibSharp, Nothing)
        LLblTagLibSharp.ImageAlign = ContentAlignment.MiddleLeft
        LLblTagLibSharp.LinkBehavior = LinkBehavior.HoverUnderline
        LLblTagLibSharp.Location = New Point(27, 183)
        LLblTagLibSharp.Name = "LLblTagLibSharp"
        LLblTagLibSharp.Size = New Size(67, 23)
        LLblTagLibSharp.TabIndex = 6
        LLblTagLibSharp.TabStop = True
        LLblTagLibSharp.Text = "TagLib#"
        TipAbout.SetText(LLblTagLibSharp, Nothing)
        LLblTagLibSharp.TextAlign = ContentAlignment.TopCenter
        ' 
        ' LblVersion
        ' 
        LblVersion.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LblVersion, Nothing)
        LblVersion.Location = New Point(12, 364)
        LblVersion.Name = "LblVersion"
        LblVersion.Size = New Size(360, 23)
        LblVersion.TabIndex = 7
        LblVersion.Text = "Version"
        TipAbout.SetText(LblVersion, Nothing)
        LblVersion.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' BtnChangeLog
        ' 
        BtnChangeLog.Anchor = AnchorStyles.Bottom
        BtnChangeLog.Image = My.Resources.Resources.ImageChangeLog32
        TipAbout.SetImage(BtnChangeLog, Nothing)
        BtnChangeLog.Location = New Point(324, 437)
        BtnChangeLog.Name = "BtnChangeLog"
        BtnChangeLog.Size = New Size(48, 48)
        BtnChangeLog.TabIndex = 100
        TipAbout.SetText(BtnChangeLog, "What's New")
        BtnChangeLog.UseVisualStyleBackColor = True
        ' 
        ' TipAbout
        ' 
        TipAbout.FadeInRate = 25
        TipAbout.FadeOutRate = 25
        TipAbout.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.HideDelay = 1000
        TipAbout.ShadowAlpha = 200
        TipAbout.ShowDelay = 1000
        ' 
        ' LLblVLCSharp
        ' 
        LLblVLCSharp.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblVLCSharp.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LLblVLCSharp, Nothing)
        LLblVLCSharp.ImageAlign = ContentAlignment.MiddleLeft
        LLblVLCSharp.LinkBehavior = LinkBehavior.HoverUnderline
        LLblVLCSharp.Location = New Point(149, 112)
        LLblVLCSharp.Name = "LLblVLCSharp"
        LLblVLCSharp.Size = New Size(96, 23)
        LLblVLCSharp.TabIndex = 101
        LLblVLCSharp.TabStop = True
        LLblVLCSharp.Text = "VLCSharp"
        TipAbout.SetText(LLblVLCSharp, Nothing)
        LLblVLCSharp.TextAlign = ContentAlignment.TopRight
        ' 
        ' LLblNAudio
        ' 
        LLblNAudio.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblNAudio.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LLblNAudio, Nothing)
        LLblNAudio.ImageAlign = ContentAlignment.MiddleLeft
        LLblNAudio.LinkBehavior = LinkBehavior.HoverUnderline
        LLblNAudio.Location = New Point(293, 112)
        LLblNAudio.Name = "LLblNAudio"
        LLblNAudio.Size = New Size(79, 23)
        LLblNAudio.TabIndex = 102
        LLblNAudio.TabStop = True
        LLblNAudio.Text = "NAudio"
        TipAbout.SetText(LLblNAudio, Nothing)
        LLblNAudio.TextAlign = ContentAlignment.TopRight
        ' 
        ' LLblSQLite
        ' 
        LLblSQLite.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblSQLite.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LLblSQLite, Nothing)
        LLblSQLite.Image = My.Resources.Resources.ImageAttributionSQLite
        LLblSQLite.LinkBehavior = LinkBehavior.HoverUnderline
        LLblSQLite.Location = New Point(97, 142)
        LLblSQLite.Name = "LLblSQLite"
        LLblSQLite.Size = New Size(79, 27)
        LLblSQLite.TabIndex = 103
        TipAbout.SetText(LLblSQLite, Nothing)
        LLblSQLite.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LLblMusicBrainz
        ' 
        LLblMusicBrainz.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblMusicBrainz.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LLblMusicBrainz, Nothing)
        LLblMusicBrainz.Image = My.Resources.Resources.ImageAttributionMusicBrainz
        LLblMusicBrainz.LinkBehavior = LinkBehavior.HoverUnderline
        LLblMusicBrainz.Location = New Point(122, 176)
        LLblMusicBrainz.Name = "LLblMusicBrainz"
        LLblMusicBrainz.Size = New Size(138, 38)
        LLblMusicBrainz.TabIndex = 104
        TipAbout.SetText(LLblMusicBrainz, Nothing)
        LLblMusicBrainz.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LLblSponsorGitHub
        ' 
        LLblSponsorGitHub.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LLblSponsorGitHub, Nothing)
        LLblSponsorGitHub.ImageAlign = ContentAlignment.MiddleRight
        LLblSponsorGitHub.LinkBehavior = LinkBehavior.HoverUnderline
        LLblSponsorGitHub.Location = New Point(43, 291)
        LLblSponsorGitHub.Name = "LLblSponsorGitHub"
        LLblSponsorGitHub.Size = New Size(164, 30)
        LLblSponsorGitHub.TabIndex = 105
        LLblSponsorGitHub.TabStop = True
        LLblSponsorGitHub.Text = "GitHub Sponsors"
        TipAbout.SetText(LLblSponsorGitHub, Nothing)
        LLblSponsorGitHub.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LLblSponsorPayPal
        ' 
        LLblSponsorPayPal.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LLblSponsorPayPal, Nothing)
        LLblSponsorPayPal.ImageAlign = ContentAlignment.MiddleRight
        LLblSponsorPayPal.LinkBehavior = LinkBehavior.HoverUnderline
        LLblSponsorPayPal.Location = New Point(256, 291)
        LLblSponsorPayPal.Name = "LLblSponsorPayPal"
        LLblSponsorPayPal.Size = New Size(85, 30)
        LLblSponsorPayPal.TabIndex = 106
        LLblSponsorPayPal.TabStop = True
        LLblSponsorPayPal.Text = "PayPal"
        TipAbout.SetText(LLblSponsorPayPal, Nothing)
        LLblSponsorPayPal.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LblSponsorMe
        ' 
        LblSponsorMe.Font = New Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        TipAbout.SetImage(LblSponsorMe, Nothing)
        LblSponsorMe.Location = New Point(12, 266)
        LblSponsorMe.Name = "LblSponsorMe"
        LblSponsorMe.Size = New Size(360, 21)
        LblSponsorMe.TabIndex = 107
        LblSponsorMe.Text = "Support My Work!"
        TipAbout.SetText(LblSponsorMe, Nothing)
        LblSponsorMe.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' About
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(384, 497)
        Controls.Add(LblSponsorMe)
        Controls.Add(LLblSponsorPayPal)
        Controls.Add(LLblSponsorGitHub)
        Controls.Add(LLblMusicBrainz)
        Controls.Add(LLblSQLite)
        Controls.Add(LLblNAudio)
        Controls.Add(LLblVLCSharp)
        Controls.Add(BtnChangeLog)
        Controls.Add(LblVersion)
        Controls.Add(LLblTagLibSharp)
        Controls.Add(LLblIcons8)
        Controls.Add(LLblSyncFusion)
        Controls.Add(LLblMicrosoft)
        Controls.Add(LblAbout)
        Controls.Add(BtnOK)
        FormBorderStyle = FormBorderStyle.Fixed3D
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipAbout.SetImage(Me, Nothing)
        KeyPreview = True
        MaximizeBox = False
        MinimizeBox = False
        Name = "About"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        TipAbout.SetText(Me, Nothing)
        ResumeLayout(False)
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents LblAbout As Label
    Friend WithEvents LLblMicrosoft As LinkLabel
    Friend WithEvents LLblSyncFusion As LinkLabel
    Friend WithEvents LLblIcons8 As LinkLabel
    Friend WithEvents LLblTagLibSharp As LinkLabel
    Friend WithEvents LblVersion As Skye.UI.Label
    Friend WithEvents BtnChangeLog As Button
    Friend WithEvents TipAbout As Skye.UI.ToolTipEX
    Friend WithEvents LLblVLCSharp As LinkLabel
    Friend WithEvents LLblNAudio As LinkLabel
    Friend WithEvents LLblSQLite As LinkLabel
    Friend WithEvents LLblMusicBrainz As LinkLabel
    Friend WithEvents LLblSponsorGitHub As LinkLabel
    Friend WithEvents LLblSponsorPayPal As LinkLabel
    Friend WithEvents LblSponsorMe As Label
End Class
