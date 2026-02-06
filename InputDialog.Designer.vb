<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InputDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InputDialog))
        TxtBoxInput = New TextBox()
        CMEditTitle = New Skye.UI.TextBoxContextMenu()
        BtnOK = New Button()
        LblPrompt = New Label()
        SuspendLayout()
        ' 
        ' TxtBoxInput
        ' 
        TxtBoxInput.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBoxInput.ContextMenuStrip = CMEditTitle
        TxtBoxInput.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxInput.Location = New Point(12, 28)
        TxtBoxInput.Name = "TxtBoxInput"
        TxtBoxInput.ShortcutsEnabled = False
        TxtBoxInput.Size = New Size(463, 29)
        TxtBoxInput.TabIndex = 11
        ' 
        ' CMEditTitle
        ' 
        CMEditTitle.Name = "CMEditTitle"
        CMEditTitle.Size = New Size(138, 176)
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(211, 71)
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
        LblPrompt.Name = "LblPrompt"
        LblPrompt.Size = New Size(76, 21)
        LblPrompt.TabIndex = 13
        LblPrompt.Text = "*Prompt*"
        ' 
        ' InputDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(487, 147)
        Controls.Add(TxtBoxInput)
        Controls.Add(BtnOK)
        Controls.Add(LblPrompt)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "InputDialog"
        StartPosition = FormStartPosition.CenterParent
        Text = "*Title*"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TxtBoxInput As TextBox
    Friend WithEvents BtnOK As Button
    Friend WithEvents LblPrompt As Label
    Friend WithEvents CMEditTitle As Skye.UI.TextBoxContextMenu
End Class
