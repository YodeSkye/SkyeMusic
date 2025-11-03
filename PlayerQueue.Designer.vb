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
        CMQueue = New ContextMenuStrip(components)
        CMIMoveTop = New ToolStripMenuItem()
        CMIMoveUp = New ToolStripMenuItem()
        CMIMoveDown = New ToolStripMenuItem()
        CMIMoveBottom = New ToolStripMenuItem()
        CMIRemove = New ToolStripMenuItem()
        BtnOK = New Button()
        BtnPrune = New Button()
        TipQueue = New Skye.UI.ToolTipEX(components)
        LVQueue = New Skye.UI.ListViewEX()
        ColumnHeader3 = New ColumnHeader()
        ColumnHeader4 = New ColumnHeader()
        CMQueue.SuspendLayout()
        SuspendLayout()
        ' 
        ' CMQueue
        ' 
        CMQueue.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipQueue.SetImage(CMQueue, Nothing)
        CMQueue.Items.AddRange(New ToolStripItem() {CMIMoveTop, CMIMoveUp, CMIMoveDown, CMIMoveBottom, CMIRemove})
        CMQueue.Name = "CMQueue"
        CMQueue.Size = New Size(194, 134)
        ' 
        ' CMIMoveTop
        ' 
        CMIMoveTop.Image = My.Resources.Resources.ImageMoveTop
        CMIMoveTop.Name = "CMIMoveTop"
        CMIMoveTop.Size = New Size(193, 26)
        CMIMoveTop.Text = "Move To Top"
        ' 
        ' CMIMoveUp
        ' 
        CMIMoveUp.Image = My.Resources.Resources.ImageMoveUp
        CMIMoveUp.Name = "CMIMoveUp"
        CMIMoveUp.Size = New Size(193, 26)
        CMIMoveUp.Text = "Move Up"
        ' 
        ' CMIMoveDown
        ' 
        CMIMoveDown.Image = My.Resources.Resources.ImageMoveDown
        CMIMoveDown.Name = "CMIMoveDown"
        CMIMoveDown.Size = New Size(193, 26)
        CMIMoveDown.Text = "Move Down"
        ' 
        ' CMIMoveBottom
        ' 
        CMIMoveBottom.Image = My.Resources.Resources.ImageMoveBottom
        CMIMoveBottom.Name = "CMIMoveBottom"
        CMIMoveBottom.Size = New Size(193, 26)
        CMIMoveBottom.Text = "Move To Bottom"
        ' 
        ' CMIRemove
        ' 
        CMIRemove.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CMIRemove.Image = My.Resources.Resources.ImageClearRemoveDelete16
        CMIRemove.Name = "CMIRemove"
        CMIRemove.Size = New Size(193, 26)
        CMIRemove.Text = "Remove"
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        TipQueue.SetImage(BtnOK, Nothing)
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(360, 185)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 9
        TipQueue.SetText(BtnOK, "Close Window")
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' BtnPrune
        ' 
        BtnPrune.Anchor = AnchorStyles.Bottom
        TipQueue.SetImage(BtnPrune, My.Resources.Resources.ImagePrune32)
        BtnPrune.Image = My.Resources.Resources.ImagePrune32
        BtnPrune.Location = New Point(724, 201)
        BtnPrune.Name = "BtnPrune"
        BtnPrune.Size = New Size(48, 48)
        BtnPrune.TabIndex = 10
        TipQueue.SetText(BtnPrune, "Prune Queue by removing any item not found in the Playlist")
        BtnPrune.UseVisualStyleBackColor = True
        ' 
        ' TipQueue
        ' 
        TipQueue.FadeInRate = 25
        TipQueue.FadeOutRate = 25
        TipQueue.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipQueue.HideDelay = 1000
        TipQueue.ShadowAlpha = 200
        TipQueue.ShowDelay = 1000
        ' 
        ' LVQueue
        ' 
        LVQueue.AllowDrop = True
        LVQueue.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LVQueue.Columns.AddRange(New ColumnHeader() {ColumnHeader3, ColumnHeader4})
        LVQueue.ContextMenuStrip = CMQueue
        LVQueue.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LVQueue.FullRowSelect = True
        LVQueue.HeaderStyle = ColumnHeaderStyle.Nonclickable
        TipQueue.SetImage(LVQueue, Nothing)
        LVQueue.InsertionLineColor = Color.Teal
        LVQueue.Location = New Point(12, 12)
        LVQueue.Name = "LVQueue"
        LVQueue.OwnerDraw = True
        LVQueue.Size = New Size(760, 158)
        LVQueue.TabIndex = 0
        LVQueue.UseCompatibleStateImageBehavior = False
        LVQueue.View = View.Details
        ' 
        ' ColumnHeader3
        ' 
        ColumnHeader3.Text = "Title"
        ColumnHeader3.Width = 350
        ' 
        ' ColumnHeader4
        ' 
        ColumnHeader4.Text = "Path"
        ColumnHeader4.Width = 600
        ' 
        ' PlayerQueue
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(784, 261)
        Controls.Add(LVQueue)
        Controls.Add(BtnPrune)
        Controls.Add(BtnOK)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipQueue.SetImage(Me, Nothing)
        KeyPreview = True
        MinimumSize = New Size(800, 300)
        Name = "PlayerQueue"
        StartPosition = FormStartPosition.CenterParent
        Text = "Queue"
        CMQueue.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents CMQueue As ContextMenuStrip
    Friend WithEvents CMIRemove As ToolStripMenuItem
    Friend WithEvents BtnPrune As Button
    Friend WithEvents TipQueue As Skye.UI.ToolTipEX
    Friend WithEvents LVQueue As Skye.UI.ListViewEX
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents CMIMoveTop As ToolStripMenuItem
    Friend WithEvents CMIMoveUp As ToolStripMenuItem
    Friend WithEvents CMIMoveDown As ToolStripMenuItem
    Friend WithEvents CMIMoveBottom As ToolStripMenuItem
End Class
