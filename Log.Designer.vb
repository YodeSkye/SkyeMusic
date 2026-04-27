<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        RTBCMLog = New Skye.UI.RichTextBoxContextMenu()
        BTNOK = New Button()
        BTNDeleteLog = New Button()
        LBLLogInfo = New Skye.UI.Label()
        LogViewer = New Skye.UI.Log.LogViewerControl()
        TipLogEX = New Skye.UI.ToolTipEX(components)
        SuspendLayout()
        ' 
        ' RTBCMLog
        ' 
        RTBCMLog.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipLogEX.SetImage(RTBCMLog, Nothing)
        RTBCMLog.Name = "RTBCMLog"
        RTBCMLog.Size = New Size(129, 148)
        TipLogEX.SetText(RTBCMLog, Nothing)
        ' 
        ' BTNOK
        ' 
        BTNOK.Anchor = AnchorStyles.Bottom
        BTNOK.Image = My.Resources.Resources.ImageOK
        TipLogEX.SetImage(BTNOK, My.Resources.Resources.ImageOK)
        BTNOK.Location = New Point(368, 383)
        BTNOK.Name = "BTNOK"
        BTNOK.Size = New Size(64, 64)
        BTNOK.TabIndex = 3
        TipLogEX.SetText(BTNOK, "Close Window")
        BTNOK.UseVisualStyleBackColor = True
        ' 
        ' BTNDeleteLog
        ' 
        BTNDeleteLog.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BTNDeleteLog.Image = My.Resources.Resources.ImageDeleteLog32
        TipLogEX.SetImage(BTNDeleteLog, My.Resources.Resources.ImageDeleteLog32)
        BTNDeleteLog.Location = New Point(12, 399)
        BTNDeleteLog.Name = "BTNDeleteLog"
        BTNDeleteLog.Size = New Size(48, 48)
        BTNDeleteLog.TabIndex = 2
        TipLogEX.SetText(BTNDeleteLog, "Delete Log")
        BTNDeleteLog.UseVisualStyleBackColor = True
        ' 
        ' LBLLogInfo
        ' 
        LBLLogInfo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LBLLogInfo.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TipLogEX.SetImage(LBLLogInfo, Nothing)
        LBLLogInfo.Location = New Point(12, 352)
        LBLLogInfo.Name = "LBLLogInfo"
        LBLLogInfo.Size = New Size(776, 23)
        LBLLogInfo.TabIndex = 5
        LBLLogInfo.Text = "Log Info"
        TipLogEX.SetText(LBLLogInfo, Nothing)
        LBLLogInfo.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' LogViewer
        ' 
        LogViewer.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LogViewer.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipLogEX.SetImage(LogViewer, Nothing)
        LogViewer.Location = New Point(0, 0)
        LogViewer.Margin = New Padding(6, 7, 6, 7)
        LogViewer.Name = "LogViewer"
        LogViewer.Size = New Size(800, 345)
        LogViewer.TabIndex = 6
        TipLogEX.SetText(LogViewer, Nothing)
        ' 
        ' TipLogEX
        ' 
        TipLogEX.FadeInRate = 25
        TipLogEX.FadeOutRate = 25
        TipLogEX.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipLogEX.HideDelay = 1000
        TipLogEX.ShadowAlpha = 0
        TipLogEX.ShadowThickness = 0
        TipLogEX.ShowDelay = 1000
        ' 
        ' Log
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 459)
        Controls.Add(LogViewer)
        Controls.Add(LBLLogInfo)
        Controls.Add(BTNDeleteLog)
        Controls.Add(BTNOK)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipLogEX.SetImage(Me, Nothing)
        KeyPreview = True
        MinimumSize = New Size(400, 300)
        Name = "Log"
        StartPosition = FormStartPosition.CenterScreen
        TipLogEX.SetText(Me, Nothing)
        ResumeLayout(False)
    End Sub
    Friend WithEvents BTNOK As Button
    Friend WithEvents BTNDeleteLog As Button
    Friend WithEvents RTBCMLog As Skye.UI.RichTextBoxContextMenu
    Friend WithEvents LBLLogInfo As Skye.UI.Label
    Friend WithEvents LogViewer As Skye.UI.Log.LogViewerControl
    Friend WithEvents TipLogEX As Skye.UI.ToolTipEX
End Class
