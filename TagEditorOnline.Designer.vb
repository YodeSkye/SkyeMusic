<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TagEditorOnline
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TagEditorOnline))
        LVIDs = New ListView()
        ColumnHeader1 = New ColumnHeader()
        PicBoxArt = New System.Windows.Forms.PictureBox()
        BtnOK = New Button()
        PicBoxBackThumb = New System.Windows.Forms.PictureBox()
        PicBoxFrontThumb = New System.Windows.Forms.PictureBox()
        LblStatus = New Label()
        TxtBoxSearchPhrase = New TextBox()
        LblDimFront = New Skye.UI.Label()
        LblDimBack = New Skye.UI.Label()
        LblSearchPhrase = New Skye.UI.Label()
        tipInfo = New Skye.UI.ToolTipEX(components)
        BtnSaveImage = New Button()
        CType(PicBoxArt, ComponentModel.ISupportInitialize).BeginInit()
        CType(PicBoxBackThumb, ComponentModel.ISupportInitialize).BeginInit()
        CType(PicBoxFrontThumb, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' LVIDs
        ' 
        LVIDs.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LVIDs.Columns.AddRange(New ColumnHeader() {ColumnHeader1})
        LVIDs.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        LVIDs.HeaderStyle = ColumnHeaderStyle.None
        tipInfo.SetImage(LVIDs, Nothing)
        LVIDs.Location = New Point(12, 57)
        LVIDs.Name = "LVIDs"
        LVIDs.Size = New Size(388, 208)
        LVIDs.TabIndex = 0
        tipInfo.SetText(LVIDs, "Select a MusicBrainz ID")
        LVIDs.UseCompatibleStateImageBehavior = False
        LVIDs.View = View.Details
        ' 
        ' ColumnHeader1
        ' 
        ColumnHeader1.Text = ""
        ColumnHeader1.Width = 540
        ' 
        ' PicBoxArt
        ' 
        PicBoxArt.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        PicBoxArt.BorderStyle = BorderStyle.Fixed3D
        tipInfo.SetImage(PicBoxArt, Nothing)
        PicBoxArt.Location = New Point(12, 271)
        PicBoxArt.Name = "PicBoxArt"
        PicBoxArt.Size = New Size(600, 600)
        PicBoxArt.SizeMode = PictureBoxSizeMode.Zoom
        PicBoxArt.TabIndex = 2
        PicBoxArt.TabStop = False
        tipInfo.SetText(PicBoxArt, "Selected Image")
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnOK.Image = My.Resources.Resources.ImageOK
        tipInfo.SetImage(BtnOK, Nothing)
        BtnOK.Location = New Point(548, 201)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 30
        tipInfo.SetText(BtnOK, "Insert Image as Artwork")
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' PicBoxBackThumb
        ' 
        PicBoxBackThumb.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        tipInfo.SetImage(PicBoxBackThumb, Nothing)
        PicBoxBackThumb.Location = New Point(512, 57)
        PicBoxBackThumb.Name = "PicBoxBackThumb"
        PicBoxBackThumb.Size = New Size(100, 113)
        PicBoxBackThumb.SizeMode = PictureBoxSizeMode.Zoom
        PicBoxBackThumb.TabIndex = 4
        PicBoxBackThumb.TabStop = False
        tipInfo.SetText(PicBoxBackThumb, "Back Cover Thumbnail, Click to Select")
        ' 
        ' PicBoxFrontThumb
        ' 
        PicBoxFrontThumb.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        tipInfo.SetImage(PicBoxFrontThumb, Nothing)
        PicBoxFrontThumb.Location = New Point(406, 57)
        PicBoxFrontThumb.Name = "PicBoxFrontThumb"
        PicBoxFrontThumb.Size = New Size(100, 113)
        PicBoxFrontThumb.SizeMode = PictureBoxSizeMode.Zoom
        PicBoxFrontThumb.TabIndex = 5
        PicBoxFrontThumb.TabStop = False
        tipInfo.SetText(PicBoxFrontThumb, "Front Cover Thumbnail, Click to Select")
        ' 
        ' LblStatus
        ' 
        LblStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblStatus.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblStatus.ForeColor = Color.Red
        tipInfo.SetImage(LblStatus, Nothing)
        LblStatus.Location = New Point(406, 38)
        LblStatus.Name = "LblStatus"
        LblStatus.Size = New Size(206, 19)
        LblStatus.TabIndex = 8
        LblStatus.Text = "Downloading Art..."
        tipInfo.SetText(LblStatus, Nothing)
        LblStatus.TextAlign = ContentAlignment.MiddleCenter
        LblStatus.Visible = False
        ' 
        ' TxtBoxSearchPhrase
        ' 
        TxtBoxSearchPhrase.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBoxSearchPhrase.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        tipInfo.SetImage(TxtBoxSearchPhrase, Nothing)
        TxtBoxSearchPhrase.Location = New Point(12, 26)
        TxtBoxSearchPhrase.Name = "TxtBoxSearchPhrase"
        TxtBoxSearchPhrase.Size = New Size(388, 25)
        TxtBoxSearchPhrase.TabIndex = 100
        tipInfo.SetText(TxtBoxSearchPhrase, Nothing)
        ' 
        ' LblDimFront
        ' 
        LblDimFront.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblDimFront.Font = New Font("Segoe UI", 9.75F)
        tipInfo.SetImage(LblDimFront, Nothing)
        LblDimFront.Location = New Point(406, 168)
        LblDimFront.Name = "LblDimFront"
        LblDimFront.Size = New Size(100, 23)
        LblDimFront.TabIndex = 101
        LblDimFront.Text = "W x H"
        tipInfo.SetText(LblDimFront, Nothing)
        LblDimFront.TextAlign = ContentAlignment.TopCenter
        ' 
        ' LblDimBack
        ' 
        LblDimBack.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblDimBack.Font = New Font("Segoe UI", 9.75F)
        tipInfo.SetImage(LblDimBack, Nothing)
        LblDimBack.Location = New Point(512, 168)
        LblDimBack.Name = "LblDimBack"
        LblDimBack.Size = New Size(100, 23)
        LblDimBack.TabIndex = 102
        LblDimBack.Text = "W x H"
        tipInfo.SetText(LblDimBack, Nothing)
        LblDimBack.TextAlign = ContentAlignment.TopCenter
        ' 
        ' LblSearchPhrase
        ' 
        LblSearchPhrase.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tipInfo.SetImage(LblSearchPhrase, Nothing)
        LblSearchPhrase.Location = New Point(12, 5)
        LblSearchPhrase.Name = "LblSearchPhrase"
        LblSearchPhrase.Size = New Size(390, 26)
        LblSearchPhrase.TabIndex = 103
        LblSearchPhrase.Text = "Search Phrase:"
        tipInfo.SetText(LblSearchPhrase, Nothing)
        LblSearchPhrase.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tipInfo
        ' 
        tipInfo.BackColor = Color.White
        tipInfo.BorderColor = Color.Gainsboro
        tipInfo.FadeInRate = 25
        tipInfo.FadeOutRate = 25
        tipInfo.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        tipInfo.ShowBorder = False
        tipInfo.ShowDelay = 100
        ' 
        ' BtnSaveImage
        ' 
        BtnSaveImage.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnSaveImage.Image = My.Resources.Resources.ImageSave32
        tipInfo.SetImage(BtnSaveImage, Nothing)
        BtnSaveImage.Location = New Point(406, 217)
        BtnSaveImage.Name = "BtnSaveImage"
        BtnSaveImage.Size = New Size(48, 48)
        BtnSaveImage.TabIndex = 104
        tipInfo.SetText(BtnSaveImage, "Save Selected Image To File")
        BtnSaveImage.UseVisualStyleBackColor = True
        ' 
        ' TagEditorOnline
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(624, 881)
        Controls.Add(BtnSaveImage)
        Controls.Add(TxtBoxSearchPhrase)
        Controls.Add(PicBoxFrontThumb)
        Controls.Add(PicBoxBackThumb)
        Controls.Add(BtnOK)
        Controls.Add(PicBoxArt)
        Controls.Add(LVIDs)
        Controls.Add(LblDimFront)
        Controls.Add(LblDimBack)
        Controls.Add(LblSearchPhrase)
        Controls.Add(LblStatus)
        Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.Fixed3D
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        tipInfo.SetImage(Me, Nothing)
        MinimumSize = New Size(500, 561)
        Name = "TagEditorOnline"
        StartPosition = FormStartPosition.CenterParent
        tipInfo.SetText(Me, Nothing)
        Text = "MusicBrainz Cover Art"
        CType(PicBoxArt, ComponentModel.ISupportInitialize).EndInit()
        CType(PicBoxBackThumb, ComponentModel.ISupportInitialize).EndInit()
        CType(PicBoxFrontThumb, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LVIDs As ListView
    Friend WithEvents PicBoxArt As System.Windows.Forms.PictureBox
    Friend WithEvents BtnOK As Button
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents PicBoxBackThumb As System.Windows.Forms.PictureBox
    Friend WithEvents PicBoxFrontThumb As System.Windows.Forms.PictureBox
    Friend WithEvents LblStatus As Label
    Friend WithEvents TxtBoxSearchPhrase As TextBox
    Friend WithEvents LblDimFront As Skye.UI.Label
    Friend WithEvents LblDimBack As Skye.UI.Label
    Friend WithEvents LblSearchPhrase As Skye.UI.Label
    Friend WithEvents tipInfo As Skye.UI.ToolTipEX
    Friend WithEvents BtnSaveImage As Button
End Class
