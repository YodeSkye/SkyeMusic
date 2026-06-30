<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Help
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Help))
        BtnOK = New Button()
        RTxBxHelp = New Skye.UI.RichTextBox()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(368, 374)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 1
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' RTxBxHelp
        ' 
        RTxBxHelp.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        RTxBxHelp.Location = New Point(12, 12)
        RTxBxHelp.Name = "RTxBxHelp"
        RTxBxHelp.ReadOnly = True
        RTxBxHelp.ShortcutsEnabled = False
        RTxBxHelp.ShowSelectionMargin = True
        RTxBxHelp.Size = New Size(776, 345)
        RTxBxHelp.TabIndex = 3
        RTxBxHelp.Text = ""
        ' 
        ' Help
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(RTxBxHelp)
        Controls.Add(BtnOK)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        MinimumSize = New Size(400, 300)
        Name = "Help"
        StartPosition = FormStartPosition.CenterScreen
        ResumeLayout(False)
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents RTxBxHelp As Skye.UI.RichTextBox
End Class
