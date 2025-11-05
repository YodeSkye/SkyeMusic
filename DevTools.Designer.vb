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
        PanelPlaysControls = New Panel()
        BtnPlaysRefresh = New Button()
        BtnPlaysDeleteSelected = New Button()
        LblPlaysCounts = New Label()
        PanelDGVPlays = New Panel()
        TCDevTools = New TabControl()
        TPHistory = New TabPage()
        PanelHistoryControls = New Panel()
        BtnHistoryRefresh = New Button()
        BtnHistoryDeleteSelected = New Button()
        LblHistoryCounts = New Label()
        PanelHistory = New Panel()
        DGVHistory = New DataGridView()
        TPPlays = New TabPage()
        CType(DGVPlays, ComponentModel.ISupportInitialize).BeginInit()
        PanelPlaysControls.SuspendLayout()
        PanelDGVPlays.SuspendLayout()
        TCDevTools.SuspendLayout()
        TPHistory.SuspendLayout()
        PanelHistoryControls.SuspendLayout()
        PanelHistory.SuspendLayout()
        CType(DGVHistory, ComponentModel.ISupportInitialize).BeginInit()
        TPPlays.SuspendLayout()
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
        DGVPlays.Size = New Size(1015, 531)
        DGVPlays.TabIndex = 0
        ' 
        ' PanelPlaysControls
        ' 
        PanelPlaysControls.Controls.Add(BtnPlaysRefresh)
        PanelPlaysControls.Controls.Add(BtnPlaysDeleteSelected)
        PanelPlaysControls.Controls.Add(LblPlaysCounts)
        PanelPlaysControls.Dock = DockStyle.Bottom
        PanelPlaysControls.Location = New Point(0, 531)
        PanelPlaysControls.Margin = New Padding(4)
        PanelPlaysControls.Name = "PanelPlaysControls"
        PanelPlaysControls.Size = New Size(1015, 59)
        PanelPlaysControls.TabIndex = 1
        ' 
        ' BtnPlaysRefresh
        ' 
        BtnPlaysRefresh.Anchor = AnchorStyles.Top
        BtnPlaysRefresh.Location = New Point(385, 15)
        BtnPlaysRefresh.Margin = New Padding(4)
        BtnPlaysRefresh.Name = "BtnPlaysRefresh"
        BtnPlaysRefresh.Size = New Size(138, 32)
        BtnPlaysRefresh.TabIndex = 2
        BtnPlaysRefresh.Text = "Refresh Data"
        BtnPlaysRefresh.UseVisualStyleBackColor = True
        ' 
        ' BtnPlaysDeleteSelected
        ' 
        BtnPlaysDeleteSelected.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnPlaysDeleteSelected.Location = New Point(862, 15)
        BtnPlaysDeleteSelected.Margin = New Padding(4)
        BtnPlaysDeleteSelected.Name = "BtnPlaysDeleteSelected"
        BtnPlaysDeleteSelected.Size = New Size(138, 32)
        BtnPlaysDeleteSelected.TabIndex = 1
        BtnPlaysDeleteSelected.Text = "Delete Selected"
        BtnPlaysDeleteSelected.UseVisualStyleBackColor = True
        ' 
        ' LblPlaysCounts
        ' 
        LblPlaysCounts.AutoSize = True
        LblPlaysCounts.Location = New Point(15, 18)
        LblPlaysCounts.Margin = New Padding(4, 0, 4, 0)
        LblPlaysCounts.Name = "LblPlaysCounts"
        LblPlaysCounts.Size = New Size(92, 21)
        LblPlaysCounts.TabIndex = 0
        LblPlaysCounts.Text = "Plays Count"
        ' 
        ' PanelDGVPlays
        ' 
        PanelDGVPlays.Controls.Add(DGVPlays)
        PanelDGVPlays.Controls.Add(PanelPlaysControls)
        PanelDGVPlays.Dock = DockStyle.Fill
        PanelDGVPlays.Location = New Point(3, 3)
        PanelDGVPlays.Name = "PanelDGVPlays"
        PanelDGVPlays.Size = New Size(1015, 590)
        PanelDGVPlays.TabIndex = 2
        ' 
        ' TCDevTools
        ' 
        TCDevTools.Controls.Add(TPHistory)
        TCDevTools.Controls.Add(TPPlays)
        TCDevTools.Dock = DockStyle.Fill
        TCDevTools.Location = New Point(0, 0)
        TCDevTools.Name = "TCDevTools"
        TCDevTools.SelectedIndex = 0
        TCDevTools.Size = New Size(1029, 630)
        TCDevTools.TabIndex = 1
        ' 
        ' TPHistory
        ' 
        TPHistory.Controls.Add(PanelHistoryControls)
        TPHistory.Controls.Add(PanelHistory)
        TPHistory.Location = New Point(4, 30)
        TPHistory.Name = "TPHistory"
        TPHistory.Padding = New Padding(3)
        TPHistory.Size = New Size(1021, 596)
        TPHistory.TabIndex = 0
        TPHistory.Text = "History"
        TPHistory.UseVisualStyleBackColor = True
        ' 
        ' PanelHistoryControls
        ' 
        PanelHistoryControls.Controls.Add(BtnHistoryRefresh)
        PanelHistoryControls.Controls.Add(BtnHistoryDeleteSelected)
        PanelHistoryControls.Controls.Add(LblHistoryCounts)
        PanelHistoryControls.Dock = DockStyle.Bottom
        PanelHistoryControls.Location = New Point(3, 536)
        PanelHistoryControls.Name = "PanelHistoryControls"
        PanelHistoryControls.Size = New Size(1015, 57)
        PanelHistoryControls.TabIndex = 1
        ' 
        ' BtnHistoryRefresh
        ' 
        BtnHistoryRefresh.Anchor = AnchorStyles.Top
        BtnHistoryRefresh.Location = New Point(394, 13)
        BtnHistoryRefresh.Margin = New Padding(4)
        BtnHistoryRefresh.Name = "BtnHistoryRefresh"
        BtnHistoryRefresh.Size = New Size(138, 32)
        BtnHistoryRefresh.TabIndex = 5
        BtnHistoryRefresh.Text = "Refresh Data"
        BtnHistoryRefresh.UseVisualStyleBackColor = True
        ' 
        ' BtnHistoryDeleteSelected
        ' 
        BtnHistoryDeleteSelected.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnHistoryDeleteSelected.Location = New Point(871, 13)
        BtnHistoryDeleteSelected.Margin = New Padding(4)
        BtnHistoryDeleteSelected.Name = "BtnHistoryDeleteSelected"
        BtnHistoryDeleteSelected.Size = New Size(138, 32)
        BtnHistoryDeleteSelected.TabIndex = 4
        BtnHistoryDeleteSelected.Text = "Delete Selected"
        BtnHistoryDeleteSelected.UseVisualStyleBackColor = True
        ' 
        ' LblHistoryCounts
        ' 
        LblHistoryCounts.AutoSize = True
        LblHistoryCounts.Location = New Point(24, 16)
        LblHistoryCounts.Margin = New Padding(4, 0, 4, 0)
        LblHistoryCounts.Name = "LblHistoryCounts"
        LblHistoryCounts.Size = New Size(106, 21)
        LblHistoryCounts.TabIndex = 3
        LblHistoryCounts.Text = "History Count"
        ' 
        ' PanelHistory
        ' 
        PanelHistory.Controls.Add(DGVHistory)
        PanelHistory.Dock = DockStyle.Fill
        PanelHistory.Location = New Point(3, 3)
        PanelHistory.Name = "PanelHistory"
        PanelHistory.Size = New Size(1015, 590)
        PanelHistory.TabIndex = 0
        ' 
        ' DGVHistory
        ' 
        DGVHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVHistory.Dock = DockStyle.Fill
        DGVHistory.Location = New Point(0, 0)
        DGVHistory.Name = "DGVHistory"
        DGVHistory.Size = New Size(1015, 590)
        DGVHistory.TabIndex = 0
        ' 
        ' TPPlays
        ' 
        TPPlays.Controls.Add(PanelDGVPlays)
        TPPlays.Location = New Point(4, 30)
        TPPlays.Name = "TPPlays"
        TPPlays.Padding = New Padding(3)
        TPPlays.Size = New Size(1021, 596)
        TPPlays.TabIndex = 1
        TPPlays.Text = "Plays"
        TPPlays.UseVisualStyleBackColor = True
        ' 
        ' DevTools
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1029, 630)
        Controls.Add(TCDevTools)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "DevTools"
        StartPosition = FormStartPosition.CenterScreen
        Text = "DevTools"
        CType(DGVPlays, ComponentModel.ISupportInitialize).EndInit()
        PanelPlaysControls.ResumeLayout(False)
        PanelPlaysControls.PerformLayout()
        PanelDGVPlays.ResumeLayout(False)
        TCDevTools.ResumeLayout(False)
        TPHistory.ResumeLayout(False)
        PanelHistoryControls.ResumeLayout(False)
        PanelHistoryControls.PerformLayout()
        PanelHistory.ResumeLayout(False)
        CType(DGVHistory, ComponentModel.ISupportInitialize).EndInit()
        TPPlays.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents DGVPlays As DataGridView
    Friend WithEvents PanelPlaysControls As Panel
    Friend WithEvents BtnPlaysDeleteSelected As Button
    Friend WithEvents LblPlaysCounts As Label
    Friend WithEvents BtnPlaysRefresh As Button
    Friend WithEvents PanelDGVPlays As Panel
    Friend WithEvents TCDevTools As TabControl
    Friend WithEvents TPHistory As TabPage
    Friend WithEvents TPPlays As TabPage
    Friend WithEvents PanelHistoryControls As Panel
    Friend WithEvents PanelHistory As Panel
    Friend WithEvents DGVHistory As DataGridView
    Friend WithEvents BtnHistoryRefresh As Button
    Friend WithEvents BtnHistoryDeleteSelected As Button
    Friend WithEvents LblHistoryCounts As Label
End Class
