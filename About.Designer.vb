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
        LblVersion = New Label()
        LblAbout = New Label()
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
        ' LblVersion
        ' 
        LblVersion.BackColor = Color.Transparent
        LblVersion.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblVersion.Location = New Point(12, 328)
        LblVersion.Name = "LblVersion"
        LblVersion.Size = New Size(360, 23)
        LblVersion.TabIndex = 1
        LblVersion.Text = "Version"
        LblVersion.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblAbout
        ' 
        LblAbout.BackColor = Color.Transparent
        LblAbout.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblAbout.Location = New Point(12, 9)
        LblAbout.Name = "LblAbout"
        LblAbout.Size = New Size(360, 296)
        LblAbout.TabIndex = 2
        LblAbout.Text = "About"
        LblAbout.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' About
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(384, 461)
        Controls.Add(LblAbout)
        Controls.Add(LblVersion)
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
    Friend WithEvents LblVersion As Label
    Friend WithEvents LblAbout As Label
End Class
