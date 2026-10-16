<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ConfirmationDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConfirmationDialog))
        BtnOK = New Button()
        LblPrompt = New Label()
        BtnCancel = New Button()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(175, 65)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 12
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' LblPrompt
        ' 
        LblPrompt.AutoSize = True
        LblPrompt.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblPrompt.Location = New Point(10, 6)
        LblPrompt.MaximumSize = New Size(500, 0)
        LblPrompt.Name = "LblPrompt"
        LblPrompt.Size = New Size(76, 21)
        LblPrompt.TabIndex = 13
        LblPrompt.Text = "*Prompt*"
        ' 
        ' BtnCancel
        ' 
        BtnCancel.Anchor = AnchorStyles.Bottom
        BtnCancel.Image = My.Resources.Resources.ImageCancel64
        BtnCancel.Location = New Point(245, 65)
        BtnCancel.Name = "BtnCancel"
        BtnCancel.Size = New Size(64, 64)
        BtnCancel.TabIndex = 14
        BtnCancel.UseVisualStyleBackColor = True
        ' 
        ' ConfirmationDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(484, 141)
        Controls.Add(BtnCancel)
        Controls.Add(BtnOK)
        Controls.Add(LblPrompt)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MinimumSize = New Size(500, 180)
        Name = "ConfirmationDialog"
        StartPosition = FormStartPosition.CenterParent
        Text = "*Title*"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents BtnOK As Button
    Friend WithEvents LblPrompt As Label
    Friend WithEvents BtnCancel As Button
End Class
