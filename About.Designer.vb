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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        BtnOK = New Button()
        LblAbout = New Label()
        LLblMicrosoft = New LinkLabel()
        LLblSyncFusion = New LinkLabel()
        LLblIcons8 = New LinkLabel()
        LLblTagLibSharp = New LinkLabel()
        LblVersion = New Components.LabelCSY()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(160, 385)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 0
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' LblAbout
        ' 
        LblAbout.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LblAbout.BackColor = Color.Transparent
        LblAbout.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblAbout.Location = New Point(12, 9)
        LblAbout.Name = "LblAbout"
        LblAbout.Size = New Size(360, 203)
        LblAbout.TabIndex = 2
        LblAbout.Text = "About"
        LblAbout.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LLblMicrosoft
        ' 
        LLblMicrosoft.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblMicrosoft.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LLblMicrosoft.Image = My.Resources.Resources.ImageAttributionMicrosoft16
        LLblMicrosoft.ImageAlign = ContentAlignment.MiddleLeft
        LLblMicrosoft.LinkBehavior = LinkBehavior.HoverUnderline
        LLblMicrosoft.Location = New Point(12, 259)
        LLblMicrosoft.Name = "LLblMicrosoft"
        LLblMicrosoft.Size = New Size(94, 23)
        LLblMicrosoft.TabIndex = 3
        LLblMicrosoft.TabStop = True
        LLblMicrosoft.Text = "Microsoft"
        LLblMicrosoft.TextAlign = ContentAlignment.TopRight
        ' 
        ' LLblSyncFusion
        ' 
        LLblSyncFusion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblSyncFusion.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LLblSyncFusion.Image = My.Resources.Resources.ImageAttributionSyncFusion24
        LLblSyncFusion.LinkBehavior = LinkBehavior.HoverUnderline
        LLblSyncFusion.Location = New Point(115, 258)
        LLblSyncFusion.Name = "LLblSyncFusion"
        LLblSyncFusion.Size = New Size(107, 23)
        LLblSyncFusion.TabIndex = 4
        LLblSyncFusion.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LLblIcons8
        ' 
        LLblIcons8.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblIcons8.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LLblIcons8.Image = My.Resources.Resources.ImageAttributionIcons816
        LLblIcons8.ImageAlign = ContentAlignment.MiddleLeft
        LLblIcons8.LinkBehavior = LinkBehavior.HoverUnderline
        LLblIcons8.Location = New Point(302, 260)
        LLblIcons8.Name = "LLblIcons8"
        LLblIcons8.Size = New Size(70, 23)
        LLblIcons8.TabIndex = 5
        LLblIcons8.TabStop = True
        LLblIcons8.Text = "Icons8"
        LLblIcons8.TextAlign = ContentAlignment.TopRight
        ' 
        ' LLblTagLibSharp
        ' 
        LLblTagLibSharp.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LLblTagLibSharp.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LLblTagLibSharp.ImageAlign = ContentAlignment.MiddleLeft
        LLblTagLibSharp.LinkBehavior = LinkBehavior.HoverUnderline
        LLblTagLibSharp.Location = New Point(227, 260)
        LLblTagLibSharp.Name = "LLblTagLibSharp"
        LLblTagLibSharp.Size = New Size(67, 23)
        LLblTagLibSharp.TabIndex = 6
        LLblTagLibSharp.TabStop = True
        LLblTagLibSharp.Text = "TagLib#"
        LLblTagLibSharp.TextAlign = ContentAlignment.TopCenter
        ' 
        ' LblVersion
        ' 
        LblVersion.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblVersion.Location = New Point(12, 347)
        LblVersion.Name = "LblVersion"
        LblVersion.Size = New Size(360, 23)
        LblVersion.TabIndex = 7
        LblVersion.Text = "Labelcsy1"
        LblVersion.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' About
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(384, 461)
        Controls.Add(LblVersion)
        Controls.Add(LLblTagLibSharp)
        Controls.Add(LLblIcons8)
        Controls.Add(LLblSyncFusion)
        Controls.Add(LLblMicrosoft)
        Controls.Add(LblAbout)
        Controls.Add(BtnOK)
        FormBorderStyle = FormBorderStyle.Fixed3D
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        MaximizeBox = False
        MinimizeBox = False
        Name = "About"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents LblAbout As Label
    Friend WithEvents LLblMicrosoft As LinkLabel
    Friend WithEvents LLblSyncFusion As LinkLabel
    Friend WithEvents LLblIcons8 As LinkLabel
    Friend WithEvents LLblTagLibSharp As LinkLabel
    Friend WithEvents LblVersion As Components.LabelCSY
End Class
