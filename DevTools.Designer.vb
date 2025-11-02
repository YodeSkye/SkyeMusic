<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DevTools
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
        DGVPlays = New DataGridView()
        PanelControls = New Panel()
        BtnRefreshData = New Button()
        BtnDeleteSelected = New Button()
        LblCount = New Label()
        PanelDGV = New Panel()
        CType(DGVPlays, ComponentModel.ISupportInitialize).BeginInit()
        PanelControls.SuspendLayout()
        PanelDGV.SuspendLayout()
        SuspendLayout()
        ' 
        ' DGVPlays
        ' 
        DGVPlays.AllowUserToAddRows = False
        DGVPlays.AllowUserToDeleteRows = False
        DGVPlays.AllowUserToOrderColumns = True
        DGVPlays.AllowUserToResizeColumns = False
        DGVPlays.AllowUserToResizeRows = False
        DGVPlays.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DGVPlays.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVPlays.Dock = DockStyle.Fill
        DGVPlays.Location = New Point(0, 0)
        DGVPlays.Margin = New Padding(4)
        DGVPlays.Name = "DGVPlays"
        DGVPlays.ReadOnly = True
        DGVPlays.Size = New Size(1029, 571)
        DGVPlays.TabIndex = 0
        ' 
        ' PanelControls
        ' 
        PanelControls.Controls.Add(BtnRefreshData)
        PanelControls.Controls.Add(BtnDeleteSelected)
        PanelControls.Controls.Add(LblCount)
        PanelControls.Dock = DockStyle.Bottom
        PanelControls.Location = New Point(0, 571)
        PanelControls.Margin = New Padding(4)
        PanelControls.Name = "PanelControls"
        PanelControls.Size = New Size(1029, 59)
        PanelControls.TabIndex = 1
        ' 
        ' BtnRefreshData
        ' 
        BtnRefreshData.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnRefreshData.Location = New Point(238, 15)
        BtnRefreshData.Margin = New Padding(4)
        BtnRefreshData.Name = "BtnRefreshData"
        BtnRefreshData.Size = New Size(138, 32)
        BtnRefreshData.TabIndex = 2
        BtnRefreshData.Text = "Refresh Data"
        BtnRefreshData.UseVisualStyleBackColor = True
        ' 
        ' BtnDeleteSelected
        ' 
        BtnDeleteSelected.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnDeleteSelected.Location = New Point(876, 15)
        BtnDeleteSelected.Margin = New Padding(4)
        BtnDeleteSelected.Name = "BtnDeleteSelected"
        BtnDeleteSelected.Size = New Size(138, 32)
        BtnDeleteSelected.TabIndex = 1
        BtnDeleteSelected.Text = "Delete Selected"
        BtnDeleteSelected.UseVisualStyleBackColor = True
        ' 
        ' LblCount
        ' 
        LblCount.AutoSize = True
        LblCount.Location = New Point(15, 18)
        LblCount.Margin = New Padding(4, 0, 4, 0)
        LblCount.Name = "LblCount"
        LblCount.Size = New Size(52, 21)
        LblCount.TabIndex = 0
        LblCount.Text = "Count"
        ' 
        ' PanelDGV
        ' 
        PanelDGV.Controls.Add(DGVPlays)
        PanelDGV.Dock = DockStyle.Fill
        PanelDGV.Location = New Point(0, 0)
        PanelDGV.Name = "PanelDGV"
        PanelDGV.Size = New Size(1029, 571)
        PanelDGV.TabIndex = 2
        ' 
        ' DevTools
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1029, 630)
        Controls.Add(PanelDGV)
        Controls.Add(PanelControls)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "DevTools"
        StartPosition = FormStartPosition.CenterScreen
        Text = "DevTools"
        CType(DGVPlays, ComponentModel.ISupportInitialize).EndInit()
        PanelControls.ResumeLayout(False)
        PanelControls.PerformLayout()
        PanelDGV.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents DGVPlays As DataGridView
    Friend WithEvents PanelControls As Panel
    Friend WithEvents BtnDeleteSelected As Button
    Friend WithEvents LblCount As Label
    Friend WithEvents BtnRefreshData As Button
    Friend WithEvents PanelDGV As Panel
End Class
