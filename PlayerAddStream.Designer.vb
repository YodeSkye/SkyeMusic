<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlayerAddStream
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlayerAddStream))
        BtnOK = New Button()
        TxtBoxStreamTitle = New TextBox()
        TxtBoxStreamPath = New TextBox()
        LblStreamTitle = New Label()
        LblStreamPath = New Label()
        CMAddStream = New Components.TextBoxContextMenu()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(211, 116)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 8
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxStreamTitle
        ' 
        TxtBoxStreamTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBoxStreamTitle.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxStreamTitle.Location = New Point(12, 22)
        TxtBoxStreamTitle.Name = "TxtBoxStreamTitle"
        TxtBoxStreamTitle.ShortcutsEnabled = False
        TxtBoxStreamTitle.Size = New Size(463, 29)
        TxtBoxStreamTitle.TabIndex = 4
        ' 
        ' TxtBoxStreamPath
        ' 
        TxtBoxStreamPath.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBoxStreamPath.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxStreamPath.Location = New Point(12, 72)
        TxtBoxStreamPath.Name = "TxtBoxStreamPath"
        TxtBoxStreamPath.ShortcutsEnabled = False
        TxtBoxStreamPath.Size = New Size(463, 29)
        TxtBoxStreamPath.TabIndex = 5
        ' 
        ' LblStreamTitle
        ' 
        LblStreamTitle.AutoSize = True
        LblStreamTitle.Location = New Point(12, 8)
        LblStreamTitle.Name = "LblStreamTitle"
        LblStreamTitle.Size = New Size(70, 15)
        LblStreamTitle.TabIndex = 9
        LblStreamTitle.Text = "Stream Title"
        ' 
        ' LblStreamPath
        ' 
        LblStreamPath.AutoSize = True
        LblStreamPath.Location = New Point(12, 58)
        LblStreamPath.Name = "LblStreamPath"
        LblStreamPath.Size = New Size(71, 15)
        LblStreamPath.TabIndex = 10
        LblStreamPath.Text = "Stream Path"
        ' 
        ' CMAddStream
        ' 
        CMAddStream.Name = "CMAddStream"
        CMAddStream.ShowExtendedTools = True
        CMAddStream.Size = New Size(181, 198)
        ' 
        ' AddStream
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(487, 192)
        Controls.Add(TxtBoxStreamPath)
        Controls.Add(TxtBoxStreamTitle)
        Controls.Add(BtnOK)
        Controls.Add(LblStreamPath)
        Controls.Add(LblStreamTitle)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "AddStream"
        StartPosition = FormStartPosition.CenterParent
        Text = "Add Stream"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents TxtBoxStreamTitle As TextBox
    Friend WithEvents TxtBoxStreamPath As TextBox
    Friend WithEvents LblStreamTitle As Label
    Friend WithEvents LblStreamPath As Label
    Friend WithEvents CMAddStream As Components.TextBoxContextMenu
End Class
