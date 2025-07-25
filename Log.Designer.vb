﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Log
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Log))
        RTBLog = New RichTextBox()
        RTBCMLog = New Components.RichTextBoxContextMenu()
        BTNOK = New Button()
        BTNDeleteLog = New Button()
        BTNRefreshLog = New Button()
        LBLLogInfo = New Components.LabelCSY()
        TxBxSearch = New TextBox()
        LblStatus = New Components.LabelCSY()
        TipLog = New ToolTip(components)
        SuspendLayout()
        ' 
        ' RTBLog
        ' 
        RTBLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        RTBLog.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        RTBLog.Location = New Point(12, 32)
        RTBLog.Name = "RTBLog"
        RTBLog.ShortcutsEnabled = False
        RTBLog.Size = New Size(776, 317)
        RTBLog.TabIndex = 0
        RTBLog.Text = ""
        RTBLog.WordWrap = False
        ' 
        ' RTBCMLog
        ' 
        RTBCMLog.Name = "RTBCMLog"
        RTBCMLog.Size = New Size(123, 148)
        ' 
        ' BTNOK
        ' 
        BTNOK.Anchor = AnchorStyles.Bottom
        BTNOK.Image = My.Resources.Resources.ImageOK
        BTNOK.Location = New Point(368, 383)
        BTNOK.Name = "BTNOK"
        BTNOK.Size = New Size(64, 64)
        BTNOK.TabIndex = 2
        TipLog.SetToolTip(BTNOK, "Close Window")
        BTNOK.UseVisualStyleBackColor = True
        ' 
        ' BTNDeleteLog
        ' 
        BTNDeleteLog.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BTNDeleteLog.Image = My.Resources.Resources.ImageDeleteLog32
        BTNDeleteLog.Location = New Point(12, 399)
        BTNDeleteLog.Name = "BTNDeleteLog"
        BTNDeleteLog.Size = New Size(48, 48)
        BTNDeleteLog.TabIndex = 3
        TipLog.SetToolTip(BTNDeleteLog, "Delete Log")
        BTNDeleteLog.UseVisualStyleBackColor = True
        ' 
        ' BTNRefreshLog
        ' 
        BTNRefreshLog.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BTNRefreshLog.Image = My.Resources.Resources.ImageRefreshLog32
        BTNRefreshLog.Location = New Point(740, 399)
        BTNRefreshLog.Name = "BTNRefreshLog"
        BTNRefreshLog.Size = New Size(48, 48)
        BTNRefreshLog.TabIndex = 4
        TipLog.SetToolTip(BTNRefreshLog, "Refresh Log")
        BTNRefreshLog.UseVisualStyleBackColor = True
        ' 
        ' LBLLogInfo
        ' 
        LBLLogInfo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LBLLogInfo.CopyOnDoubleClick = False
        LBLLogInfo.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LBLLogInfo.Location = New Point(12, 352)
        LBLLogInfo.Name = "LBLLogInfo"
        LBLLogInfo.Size = New Size(776, 23)
        LBLLogInfo.TabIndex = 5
        LBLLogInfo.Text = "Log Info"
        LBLLogInfo.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' TxBxSearch
        ' 
        TxBxSearch.BorderStyle = BorderStyle.None
        TxBxSearch.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxBxSearch.Location = New Point(14, 12)
        TxBxSearch.Name = "TxBxSearch"
        TxBxSearch.ShortcutsEnabled = False
        TxBxSearch.Size = New Size(175, 18)
        TxBxSearch.TabIndex = 6
        TxBxSearch.Text = "Search Log"
        ' 
        ' LblStatus
        ' 
        LblStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblStatus.AutoSize = True
        LblStatus.BackColor = Color.Transparent
        LblStatus.CopyOnDoubleClick = False
        LblStatus.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblStatus.ForeColor = Color.Red
        LblStatus.Location = New Point(657, 12)
        LblStatus.Name = "LblStatus"
        LblStatus.Size = New Size(128, 17)
        LblStatus.TabIndex = 7
        LblStatus.Text = "Searching The Log..."
        LblStatus.TextAlign = ContentAlignment.MiddleRight
        LblStatus.Visible = False
        ' 
        ' Log
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 459)
        Controls.Add(LblStatus)
        Controls.Add(TxBxSearch)
        Controls.Add(LBLLogInfo)
        Controls.Add(BTNRefreshLog)
        Controls.Add(BTNDeleteLog)
        Controls.Add(BTNOK)
        Controls.Add(RTBLog)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        MinimumSize = New Size(400, 300)
        Name = "Log"
        StartPosition = FormStartPosition.CenterScreen
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents RTBLog As RichTextBox
    Friend WithEvents BTNOK As Button
    Friend WithEvents BTNDeleteLog As Button
    Friend WithEvents BTNRefreshLog As Button
    Friend WithEvents RTBCMLog As My.Components.RichTextBoxContextMenu
    Friend WithEvents LBLLogInfo As My.Components.LabelCSY
    Friend WithEvents TxBxSearch As TextBox
    Friend WithEvents LblStatus As My.Components.LabelCSY
    Friend WithEvents TipLog As ToolTip
End Class
