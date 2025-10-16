<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangeLog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangeLog))
        BtnOK = New Button()
        RTBoxChangeLog = New RichTextBox()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(368, 374)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 2
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' RTBoxChangeLog
        ' 
        RTBoxChangeLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        RTBoxChangeLog.Location = New Point(12, 12)
        RTBoxChangeLog.Name = "RTBoxChangeLog"
        RTBoxChangeLog.ShortcutsEnabled = False
        RTBoxChangeLog.ShowSelectionMargin = True
        RTBoxChangeLog.Size = New Size(776, 356)
        RTBoxChangeLog.TabIndex = 3
        RTBoxChangeLog.Text = ""
        ' 
        ' ChangeLog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(RTBoxChangeLog)
        Controls.Add(BtnOK)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MinimizeBox = False
        Name = "ChangeLog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "What's New in Skye Music"
        ResumeLayout(False)
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents RTBoxChangeLog As RichTextBox
End Class
