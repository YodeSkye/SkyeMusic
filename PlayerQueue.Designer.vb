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
        TipQueue = New Skye.UI.ToolTip(components)
        BtnPrune = New Button()
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
        TipQueue.SetToolTipImage(LVQueue, Nothing)
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
        CMQueue.Size = New Size(126, 26)
        TipQueue.SetToolTipImage(CMQueue, Nothing)
        ' 
        ' CMIRemove
        ' 
        CMIRemove.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CMIRemove.Image = My.Resources.Resources.ImageClearRemoveDelete16
        CMIRemove.Name = "CMIRemove"
        CMIRemove.Size = New Size(125, 22)
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
        TipQueue.SetToolTip(BtnOK, "Close Window")
        TipQueue.SetToolTipImage(BtnOK, Nothing)
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' TipQueue
        ' 
        TipQueue.BackColor = SystemColors.Control
        TipQueue.BorderColor = SystemColors.Window
        TipQueue.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipQueue.ForeColor = SystemColors.WindowText
        TipQueue.OwnerDraw = True
        ' 
        ' BtnPrune
        ' 
        BtnPrune.Anchor = AnchorStyles.Bottom
        BtnPrune.Image = My.Resources.Resources.ImagePrune32
        BtnPrune.Location = New Point(724, 201)
        BtnPrune.Name = "BtnPrune"
        BtnPrune.Size = New Size(48, 48)
        BtnPrune.TabIndex = 10
        TipQueue.SetToolTip(BtnPrune, "Prune Queue by removing any item not found in the Playlist")
        TipQueue.SetToolTipImage(BtnPrune, My.Resources.Resources.ImagePrune32)
        BtnPrune.UseVisualStyleBackColor = True
        ' 
        ' PlayerQueue
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(784, 261)
        Controls.Add(BtnPrune)
        Controls.Add(BtnOK)
        Controls.Add(LVQueue)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MinimumSize = New Size(800, 300)
        Name = "PlayerQueue"
        StartPosition = FormStartPosition.CenterParent
        Text = "Queue"
        TipQueue.SetToolTipImage(Me, Nothing)
        CMQueue.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents LVQueue As ListView
    Friend WithEvents BtnOK As Button
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents CMQueue As ContextMenuStrip
    Friend WithEvents CMIRemove As ToolStripMenuItem
    Friend WithEvents TipQueue As Skye.UI.ToolTip
    Friend WithEvents BtnPrune As Button
End Class
