<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TagEditorOnlineSave
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
        GroupBox1 = New GroupBox()
        RadBtnImageFormatBMP = New RadioButton()
        RadBtnImageFormatPNG = New RadioButton()
        RadBtnImageFormatJPG = New RadioButton()
        TxtBoxFilename = New TextBox()
        TxtBoxLocation = New TextBox()
        BtnLocation = New Button()
        BtnSave = New Button()
        LblFilename = New Skye.UI.Label()
        LblLocation = New Skye.UI.Label()
        PicBoxThumb = New PictureBox()
        tipInfo = New Skye.UI.ToolTipEX(components)
        GroupBox1.SuspendLayout()
        CType(PicBoxThumb, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(RadBtnImageFormatBMP)
        GroupBox1.Controls.Add(RadBtnImageFormatPNG)
        GroupBox1.Controls.Add(RadBtnImageFormatJPG)
        GroupBox1.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tipInfo.SetImage(GroupBox1, Nothing)
        GroupBox1.Location = New Point(15, 132)
        GroupBox1.Margin = New Padding(4)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(4)
        GroupBox1.Size = New Size(107, 148)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Format"
        tipInfo.SetText(GroupBox1, "Select the File Format")
        ' 
        ' RadBtnImageFormatBMP
        ' 
        RadBtnImageFormatBMP.AutoSize = True
        RadBtnImageFormatBMP.Font = New Font("Segoe UI", 12F)
        tipInfo.SetImage(RadBtnImageFormatBMP, Nothing)
        RadBtnImageFormatBMP.Location = New Point(22, 101)
        RadBtnImageFormatBMP.Margin = New Padding(4)
        RadBtnImageFormatBMP.Name = "RadBtnImageFormatBMP"
        RadBtnImageFormatBMP.Size = New Size(60, 25)
        RadBtnImageFormatBMP.TabIndex = 2
        RadBtnImageFormatBMP.TabStop = True
        tipInfo.SetText(RadBtnImageFormatBMP, Nothing)
        RadBtnImageFormatBMP.Text = "BMP"
        RadBtnImageFormatBMP.UseVisualStyleBackColor = True
        ' 
        ' RadBtnImageFormatPNG
        ' 
        RadBtnImageFormatPNG.AutoSize = True
        RadBtnImageFormatPNG.Font = New Font("Segoe UI", 12F)
        tipInfo.SetImage(RadBtnImageFormatPNG, Nothing)
        RadBtnImageFormatPNG.Location = New Point(22, 65)
        RadBtnImageFormatPNG.Margin = New Padding(4)
        RadBtnImageFormatPNG.Name = "RadBtnImageFormatPNG"
        RadBtnImageFormatPNG.Size = New Size(60, 25)
        RadBtnImageFormatPNG.TabIndex = 1
        RadBtnImageFormatPNG.TabStop = True
        tipInfo.SetText(RadBtnImageFormatPNG, Nothing)
        RadBtnImageFormatPNG.Text = "PNG"
        RadBtnImageFormatPNG.UseVisualStyleBackColor = True
        ' 
        ' RadBtnImageFormatJPG
        ' 
        RadBtnImageFormatJPG.AutoSize = True
        RadBtnImageFormatJPG.Font = New Font("Segoe UI", 12F)
        tipInfo.SetImage(RadBtnImageFormatJPG, Nothing)
        RadBtnImageFormatJPG.Location = New Point(22, 31)
        RadBtnImageFormatJPG.Margin = New Padding(4)
        RadBtnImageFormatJPG.Name = "RadBtnImageFormatJPG"
        RadBtnImageFormatJPG.Size = New Size(62, 25)
        RadBtnImageFormatJPG.TabIndex = 0
        RadBtnImageFormatJPG.TabStop = True
        tipInfo.SetText(RadBtnImageFormatJPG, Nothing)
        RadBtnImageFormatJPG.Text = "JPEG"
        RadBtnImageFormatJPG.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxFilename
        ' 
        TxtBoxFilename.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        tipInfo.SetImage(TxtBoxFilename, Nothing)
        TxtBoxFilename.Location = New Point(15, 33)
        TxtBoxFilename.Margin = New Padding(4)
        TxtBoxFilename.Name = "TxtBoxFilename"
        TxtBoxFilename.Size = New Size(223, 29)
        TxtBoxFilename.TabIndex = 1
        tipInfo.SetText(TxtBoxFilename, Nothing)
        ' 
        ' TxtBoxLocation
        ' 
        TxtBoxLocation.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        tipInfo.SetImage(TxtBoxLocation, Nothing)
        TxtBoxLocation.Location = New Point(15, 90)
        TxtBoxLocation.Margin = New Padding(4)
        TxtBoxLocation.Name = "TxtBoxLocation"
        TxtBoxLocation.Size = New Size(379, 29)
        TxtBoxLocation.TabIndex = 3
        tipInfo.SetText(TxtBoxLocation, Nothing)
        ' 
        ' BtnLocation
        ' 
        BtnLocation.Image = Resources.Resources.imageSelectFolder16
        tipInfo.SetImage(BtnLocation, Nothing)
        BtnLocation.Location = New Point(394, 84)
        BtnLocation.Margin = New Padding(4)
        BtnLocation.Name = "BtnLocation"
        BtnLocation.Size = New Size(41, 40)
        BtnLocation.TabIndex = 5
        tipInfo.SetText(BtnLocation, "Select a File Location")
        BtnLocation.UseVisualStyleBackColor = True
        ' 
        ' BtnSave
        ' 
        BtnSave.Image = Resources.Resources.ImageSave32
        tipInfo.SetImage(BtnSave, Nothing)
        BtnSave.ImageAlign = ContentAlignment.MiddleLeft
        BtnSave.Location = New Point(14, 288)
        BtnSave.Margin = New Padding(4)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New Size(108, 40)
        BtnSave.TabIndex = 6
        tipInfo.SetText(BtnSave, "Save Image with the Selected Properties")
        BtnSave.Text = "Save"
        BtnSave.TextAlign = ContentAlignment.MiddleRight
        BtnSave.UseVisualStyleBackColor = True
        ' 
        ' LblFilename
        ' 
        LblFilename.AutoSize = True
        tipInfo.SetImage(LblFilename, Nothing)
        LblFilename.Location = New Point(15, 11)
        LblFilename.Margin = New Padding(4, 0, 4, 0)
        LblFilename.Name = "LblFilename"
        LblFilename.Size = New Size(213, 21)
        LblFilename.TabIndex = 7
        LblFilename.Text = "Filename (Without Extension)"
        tipInfo.SetText(LblFilename, Nothing)
        ' 
        ' LblLocation
        ' 
        LblLocation.AutoSize = True
        tipInfo.SetImage(LblLocation, Nothing)
        LblLocation.Location = New Point(15, 68)
        LblLocation.Margin = New Padding(4, 0, 4, 0)
        LblLocation.Name = "LblLocation"
        LblLocation.Size = New Size(69, 21)
        LblLocation.TabIndex = 8
        LblLocation.Text = "Location"
        tipInfo.SetText(LblLocation, Nothing)
        ' 
        ' PicBoxThumb
        ' 
        tipInfo.SetImage(PicBoxThumb, Nothing)
        PicBoxThumb.Location = New Point(240, 136)
        PicBoxThumb.Margin = New Padding(4)
        PicBoxThumb.Name = "PicBoxThumb"
        PicBoxThumb.Size = New Size(197, 189)
        PicBoxThumb.SizeMode = PictureBoxSizeMode.Zoom
        PicBoxThumb.TabIndex = 9
        PicBoxThumb.TabStop = False
        tipInfo.SetText(PicBoxThumb, "Thumbnail of Image to Save")
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
        ' TagEditorOnlineSave
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(453, 340)
        Controls.Add(PicBoxThumb)
        Controls.Add(BtnSave)
        Controls.Add(BtnLocation)
        Controls.Add(TxtBoxLocation)
        Controls.Add(TxtBoxFilename)
        Controls.Add(GroupBox1)
        Controls.Add(LblFilename)
        Controls.Add(LblLocation)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedDialog
        tipInfo.SetImage(Me, Nothing)
        Margin = New Padding(4)
        Name = "TagEditorOnlineSave"
        StartPosition = FormStartPosition.CenterParent
        tipInfo.SetText(Me, Nothing)
        Text = "Save Selected Image"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(PicBoxThumb, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadBtnImageFormatBMP As RadioButton
    Friend WithEvents RadBtnImageFormatPNG As RadioButton
    Friend WithEvents RadBtnImageFormatJPG As RadioButton
    Friend WithEvents TxtBoxFilename As TextBox
    Friend WithEvents TxtBoxLocation As TextBox
    Friend WithEvents BtnLocation As Button
    Friend WithEvents BtnSave As Button
    Friend WithEvents LblFilename As Skye.UI.Label
    Friend WithEvents LblLocation As Skye.UI.Label
    Friend WithEvents PicBoxThumb As System.Windows.Forms.PictureBox
    Friend WithEvents tipInfo As Skye.UI.ToolTipEX
End Class
