<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlayerEditTitle
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlayerEditTitle))
        TxtBoxTitle = New TextBox()
        BtnOK = New Button()
        LblTitle = New Label()
        CMEditTitle = New Skye.UI.TextBoxContextMenu()
        SuspendLayout()
        ' 
        ' TxtBoxTitle
        ' 
        TxtBoxTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBoxTitle.ContextMenuStrip = CMEditTitle
        TxtBoxTitle.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxTitle.Location = New Point(12, 28)
        TxtBoxTitle.Name = "TxtBoxTitle"
        TxtBoxTitle.ShortcutsEnabled = False
        TxtBoxTitle.Size = New Size(463, 29)
        TxtBoxTitle.TabIndex = 11
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
        ' LblTitle
        ' 
        LblTitle.AutoSize = True
        LblTitle.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblTitle.Location = New Point(12, 8)
        LblTitle.Name = "LblTitle"
        LblTitle.Size = New Size(39, 21)
        LblTitle.TabIndex = 13
        LblTitle.Text = "Title"
        ' 
        ' CMEditTitle
        ' 
        CMEditTitle.Name = "CMEditTitle"
        CMEditTitle.Size = New Size(138, 176)
        ' 
        ' PlayerEditTitle
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(487, 147)
        Controls.Add(TxtBoxTitle)
        Controls.Add(BtnOK)
        Controls.Add(LblTitle)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "PlayerEditTitle"
        StartPosition = FormStartPosition.CenterParent
        Text = "Edit Title"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TxtBoxTitle As TextBox
    Friend WithEvents BtnOK As Button
    Friend WithEvents LblTitle As Label
    Friend WithEvents CMEditTitle As Skye.UI.TextBoxContextMenu
End Class
