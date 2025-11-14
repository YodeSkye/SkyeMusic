<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsRainbowBar
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        TBGain = New Syncfusion.Windows.Forms.Tools.TrackBarEx(10, 1000)
        LblBarCount = New Skye.UI.Label()
        TxtBoxBarCount = New TextBox()
        LblGain = New Skye.UI.Label()
        SuspendLayout()
        ' 
        ' TBGain
        ' 
        TBGain.BackColor = Color.Transparent
        TBGain.BeforeTouchSize = New Size(20, 290)
        TBGain.Location = New Point(141, 26)
        TBGain.Margin = New Padding(4)
        TBGain.Name = "TBGain"
        TBGain.Orientation = Orientation.Vertical
        TBGain.Size = New Size(20, 290)
        TBGain.SmallChange = 10
        TBGain.TabIndex = 0
        TBGain.TimerInterval = 100
        TBGain.Value = 10
        ' 
        ' LblBarCount
        ' 
        LblBarCount.Location = New Point(13, 0)
        LblBarCount.Margin = New Padding(4, 0, 4, 0)
        LblBarCount.Name = "LblBarCount"
        LblBarCount.Size = New Size(85, 32)
        LblBarCount.TabIndex = 1
        LblBarCount.Text = "Bar Count"
        LblBarCount.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TxtBoxBarCount
        ' 
        TxtBoxBarCount.Location = New Point(13, 29)
        TxtBoxBarCount.Margin = New Padding(4)
        TxtBoxBarCount.Name = "TxtBoxBarCount"
        TxtBoxBarCount.Size = New Size(85, 29)
        TxtBoxBarCount.TabIndex = 2
        TxtBoxBarCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblGain
        ' 
        LblGain.Location = New Point(131, 5)
        LblGain.Name = "LblGain"
        LblGain.Size = New Size(43, 23)
        LblGain.TabIndex = 3
        LblGain.Text = "Gain"
        LblGain.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' OptionsRainbowBar
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(TxtBoxBarCount)
        Controls.Add(LblBarCount)
        Controls.Add(TBGain)
        Controls.Add(LblGain)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsRainbowBar"
        Size = New Size(800, 330)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TBGain As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblBarCount As Skye.UI.Label
    Friend WithEvents TxtBoxBarCount As TextBox
    Friend WithEvents LblGain As Skye.UI.Label

End Class
