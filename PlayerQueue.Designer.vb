<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlayerQueue
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlayerQueue))
        LVQueue = New ListView()
        ColumnHeader1 = New ColumnHeader()
        ColumnHeader2 = New ColumnHeader()
        CMQueue = New ContextMenuStrip(components)
        CMIRemove = New ToolStripMenuItem()
        BtnOK = New Button()
        CMQueue.SuspendLayout()
        SuspendLayout()
        ' 
        ' LVQueue
        ' 
        LVQueue.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LVQueue.Columns.AddRange(New ColumnHeader() {ColumnHeader1, ColumnHeader2})
        LVQueue.ContextMenuStrip = CMQueue
        LVQueue.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LVQueue.FullRowSelect = True
        LVQueue.HeaderStyle = ColumnHeaderStyle.Nonclickable
        LVQueue.Location = New Point(12, 12)
        LVQueue.Name = "LVQueue"
        LVQueue.OwnerDraw = True
        LVQueue.Size = New Size(760, 158)
        LVQueue.TabIndex = 0
        LVQueue.UseCompatibleStateImageBehavior = False
        LVQueue.View = View.Details
        ' 
        ' ColumnHeader1
        ' 
        ColumnHeader1.Text = "Title"
        ColumnHeader1.Width = 350
        ' 
        ' ColumnHeader2
        ' 
        ColumnHeader2.Text = "Path"
        ColumnHeader2.Width = 600
        ' 
        ' CMQueue
        ' 
        CMQueue.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CMQueue.Items.AddRange(New ToolStripItem() {CMIRemove})
        CMQueue.Name = "CMQueue"
        CMQueue.Size = New Size(181, 52)
        ' 
        ' CMIRemove
        ' 
        CMIRemove.Image = My.Resources.Resources.ImageClearRemoveDelete16
        CMIRemove.Name = "CMIRemove"
        CMIRemove.Size = New Size(180, 26)
        CMIRemove.Text = "Remove"
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(360, 185)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 9
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' PlayerQueue
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(784, 261)
        Controls.Add(BtnOK)
        Controls.Add(LVQueue)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MinimumSize = New Size(800, 300)
        Name = "PlayerQueue"
        StartPosition = FormStartPosition.CenterParent
        Text = "Queue"
        CMQueue.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents LVQueue As ListView
    Friend WithEvents BtnOK As Button
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents CMQueue As ContextMenuStrip
    Friend WithEvents CMIRemove As ToolStripMenuItem
End Class
