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
        PanelHistory = New Panel()
        DGVHistory = New DataGridView()
        PanelHistoryControls = New Panel()
        BtnHistoryRefresh = New Button()
        BtnHistoryDeleteSelected = New Button()
        LblHistoryCounts = New Label()
        TPPlays = New TabPage()
        CType(DGVPlays, ComponentModel.ISupportInitialize).BeginInit()
        PanelPlaysControls.SuspendLayout()
        PanelDGVPlays.SuspendLayout()
        TCDevTools.SuspendLayout()
        TPHistory.SuspendLayout()
        PanelHistory.SuspendLayout()
        CType(DGVHistory, ComponentModel.ISupportInitialize).BeginInit()
        PanelHistoryControls.SuspendLayout()
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
        DGVPlays.Size = New Size(1021, 543)
        DGVPlays.TabIndex = 0
        ' 
        ' PanelPlaysControls
        ' 
        PanelPlaysControls.Controls.Add(BtnPlaysRefresh)
        PanelPlaysControls.Controls.Add(BtnPlaysDeleteSelected)
        PanelPlaysControls.Controls.Add(LblPlaysCounts)
        PanelPlaysControls.Dock = DockStyle.Bottom
        PanelPlaysControls.Location = New Point(0, 543)
        PanelPlaysControls.Margin = New Padding(4)
        PanelPlaysControls.Name = "PanelPlaysControls"
        PanelPlaysControls.Size = New Size(1021, 59)
        PanelPlaysControls.TabIndex = 1
        ' 
        ' BtnPlaysRefresh
        ' 
        BtnPlaysRefresh.Anchor = AnchorStyles.Top
        BtnPlaysRefresh.Location = New Point(388, 15)
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
        BtnPlaysDeleteSelected.Location = New Point(868, 15)
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
        PanelDGVPlays.Location = New Point(0, 0)
        PanelDGVPlays.Name = "PanelDGVPlays"
        PanelDGVPlays.Size = New Size(1021, 602)
        PanelDGVPlays.TabIndex = 2
        ' 
        ' TCDevTools
        ' 
        TCDevTools.Controls.Add(TPHistory)
        TCDevTools.Controls.Add(TPPlays)
        TCDevTools.Dock = DockStyle.Fill
        TCDevTools.Location = New Point(0, 0)
        TCDevTools.Name = "TCDevTools"
        TCDevTools.Padding = New Point(0, 0)
        TCDevTools.SelectedIndex = 0
        TCDevTools.Size = New Size(1029, 630)
        TCDevTools.TabIndex = 1
        ' 
        ' TPHistory
        ' 
        TPHistory.Controls.Add(PanelHistory)
        TPHistory.Controls.Add(PanelHistoryControls)
        TPHistory.Location = New Point(4, 30)
        TPHistory.Name = "TPHistory"
        TPHistory.Size = New Size(1021, 596)
        TPHistory.TabIndex = 0
        TPHistory.Text = "History"
        TPHistory.UseVisualStyleBackColor = True
        ' 
        ' PanelHistory
        ' 
        PanelHistory.Controls.Add(DGVHistory)
        PanelHistory.Dock = DockStyle.Fill
        PanelHistory.Location = New Point(0, 0)
        PanelHistory.Name = "PanelHistory"
        PanelHistory.Size = New Size(1021, 539)
        PanelHistory.TabIndex = 0
        ' 
        ' DGVHistory
        ' 
        DGVHistory.AllowUserToAddRows = False
        DGVHistory.AllowUserToDeleteRows = False
        DGVHistory.AllowUserToOrderColumns = True
        DGVHistory.AllowUserToResizeColumns = False
        DGVHistory.AllowUserToResizeRows = False
        DGVHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DGVHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVHistory.Dock = DockStyle.Fill
        DGVHistory.Location = New Point(0, 0)
        DGVHistory.Margin = New Padding(4)
        DGVHistory.Name = "DGVHistory"
        DGVHistory.ReadOnly = True
        DGVHistory.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        DGVHistory.Size = New Size(1021, 539)
        DGVHistory.TabIndex = 1
        ' 
        ' PanelHistoryControls
        ' 
        PanelHistoryControls.Controls.Add(BtnHistoryRefresh)
        PanelHistoryControls.Controls.Add(BtnHistoryDeleteSelected)
        PanelHistoryControls.Controls.Add(LblHistoryCounts)
        PanelHistoryControls.Dock = DockStyle.Bottom
        PanelHistoryControls.Location = New Point(0, 539)
        PanelHistoryControls.Margin = New Padding(4)
        PanelHistoryControls.Name = "PanelHistoryControls"
        PanelHistoryControls.Size = New Size(1021, 57)
        PanelHistoryControls.TabIndex = 1
        ' 
        ' BtnHistoryRefresh
        ' 
        BtnHistoryRefresh.Anchor = AnchorStyles.Top
        BtnHistoryRefresh.Location = New Point(397, 13)
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
        BtnHistoryDeleteSelected.Location = New Point(877, 13)
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
        ' TPPlays
        ' 
        TPPlays.Controls.Add(PanelDGVPlays)
        TPPlays.Location = New Point(4, 24)
        TPPlays.Name = "TPPlays"
        TPPlays.Size = New Size(1021, 602)
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
        PanelHistory.ResumeLayout(False)
        CType(DGVHistory, ComponentModel.ISupportInitialize).EndInit()
        PanelHistoryControls.ResumeLayout(False)
        PanelHistoryControls.PerformLayout()
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
    Friend WithEvents BtnHistoryRefresh As Button
    Friend WithEvents BtnHistoryDeleteSelected As Button
    Friend WithEvents LblHistoryCounts As Label
    Friend WithEvents DGVHistory As DataGridView
End Class
