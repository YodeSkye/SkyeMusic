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
        components = New ComponentModel.Container()
        TBGain = New Syncfusion.Windows.Forms.Tools.TrackBarEx(10, 1000)
        LblBarCount = New Skye.UI.Label()
        TxtBoxBarCount = New TextBox()
        LblGain = New Skye.UI.Label()
        ChkBoxShowPeaks = New CheckBox()
        TBPeakDecaySpeed = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 20)
        TBPeakThickness = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 20)
        LblPeakDecaySpeed = New Skye.UI.Label()
        LblPeakThickness = New Skye.UI.Label()
        TBPeakThreshold = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 200)
        LblPeakThreshold = New Skye.UI.Label()
        LblHueCycleSpeed = New Skye.UI.Label()
        TBHueCycleSpeed = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 200)
        BtnCalm = New Button()
        LblPresets = New Skye.UI.Label()
        BtnEnergetic = New Button()
        BtnExtreme = New Button()
        TipRainbowBar = New Skye.UI.ToolTip(components)
        LblPeakHoldFrames = New Skye.UI.Label()
        TBPeakHoldFrames = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 60)
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
        TBGain.ShowFocusRect = False
        TBGain.Size = New Size(20, 290)
        TBGain.SmallChange = 10
        TBGain.TabIndex = 0
        TBGain.TimerInterval = 100
        TipRainbowBar.SetToolTip(TBGain, "Controls Bar Height Sensitivity. Higher gain exaggerates quiet sounds, lower gain keeps bars smaller.")
        TipRainbowBar.SetToolTipImage(TBGain, Nothing)
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
        TipRainbowBar.SetToolTipImage(LblBarCount, Nothing)
        ' 
        ' TxtBoxBarCount
        ' 
        TxtBoxBarCount.Location = New Point(13, 29)
        TxtBoxBarCount.Margin = New Padding(4)
        TxtBoxBarCount.Name = "TxtBoxBarCount"
        TxtBoxBarCount.Size = New Size(85, 29)
        TxtBoxBarCount.TabIndex = 2
        TxtBoxBarCount.TextAlign = HorizontalAlignment.Center
        TipRainbowBar.SetToolTip(TxtBoxBarCount, "Number of Bars to Display.")
        TipRainbowBar.SetToolTipImage(TxtBoxBarCount, Nothing)
        ' 
        ' LblGain
        ' 
        LblGain.Location = New Point(131, 5)
        LblGain.Name = "LblGain"
        LblGain.Size = New Size(43, 23)
        LblGain.TabIndex = 3
        LblGain.Text = "Gain"
        LblGain.TextAlign = ContentAlignment.MiddleCenter
        TipRainbowBar.SetToolTipImage(LblGain, Nothing)
        ' 
        ' ChkBoxShowPeaks
        ' 
        ChkBoxShowPeaks.AutoSize = True
        ChkBoxShowPeaks.BackColor = Color.Transparent
        ChkBoxShowPeaks.CheckAlign = ContentAlignment.BottomCenter
        ChkBoxShowPeaks.FlatStyle = FlatStyle.Flat
        ChkBoxShowPeaks.Location = New Point(8, 65)
        ChkBoxShowPeaks.Name = "ChkBoxShowPeaks"
        ChkBoxShowPeaks.RightToLeft = RightToLeft.Yes
        ChkBoxShowPeaks.Size = New Size(96, 36)
        ChkBoxShowPeaks.TabIndex = 4
        ChkBoxShowPeaks.Text = "Show Peaks"
        TipRainbowBar.SetToolTip(ChkBoxShowPeaks, "Show Peak Indicators")
        TipRainbowBar.SetToolTipImage(ChkBoxShowPeaks, Nothing)
        ChkBoxShowPeaks.UseVisualStyleBackColor = False
        ' 
        ' TBPeakDecaySpeed
        ' 
        TBPeakDecaySpeed.BackColor = Color.Transparent
        TBPeakDecaySpeed.BeforeTouchSize = New Size(20, 290)
        TBPeakDecaySpeed.Location = New Point(228, 26)
        TBPeakDecaySpeed.Name = "TBPeakDecaySpeed"
        TBPeakDecaySpeed.Orientation = Orientation.Vertical
        TBPeakDecaySpeed.ShowFocusRect = False
        TBPeakDecaySpeed.Size = New Size(20, 290)
        TBPeakDecaySpeed.TabIndex = 5
        TBPeakDecaySpeed.Text = "TrackBarEx1"
        TBPeakDecaySpeed.TimerInterval = 100
        TipRainbowBar.SetToolTip(TBPeakDecaySpeed, "Peak falloff per frame in pixels.")
        TipRainbowBar.SetToolTipImage(TBPeakDecaySpeed, Nothing)
        TBPeakDecaySpeed.Value = 5
        ' 
        ' TBPeakThickness
        ' 
        TBPeakThickness.BackColor = Color.Transparent
        TBPeakThickness.BeforeTouchSize = New Size(20, 290)
        TBPeakThickness.Location = New Point(350, 29)
        TBPeakThickness.Name = "TBPeakThickness"
        TBPeakThickness.Orientation = Orientation.Vertical
        TBPeakThickness.ShowFocusRect = False
        TBPeakThickness.Size = New Size(20, 290)
        TBPeakThickness.TabIndex = 6
        TBPeakThickness.Text = "TrackBarEx2"
        TBPeakThickness.TimerInterval = 100
        TipRainbowBar.SetToolTip(TBPeakThickness, "Width of Peak Indicators.")
        TipRainbowBar.SetToolTipImage(TBPeakThickness, Nothing)
        TBPeakThickness.Value = 5
        ' 
        ' LblPeakDecaySpeed
        ' 
        LblPeakDecaySpeed.Location = New Point(169, 5)
        LblPeakDecaySpeed.Name = "LblPeakDecaySpeed"
        LblPeakDecaySpeed.Size = New Size(139, 23)
        LblPeakDecaySpeed.TabIndex = 7
        LblPeakDecaySpeed.Text = "Peak Decay Speed"
        LblPeakDecaySpeed.TextAlign = ContentAlignment.MiddleCenter
        TipRainbowBar.SetToolTipImage(LblPeakDecaySpeed, Nothing)
        ' 
        ' LblPeakThickness
        ' 
        LblPeakThickness.Location = New Point(301, 5)
        LblPeakThickness.Name = "LblPeakThickness"
        LblPeakThickness.Size = New Size(119, 23)
        LblPeakThickness.TabIndex = 8
        LblPeakThickness.Text = "Peak Thickness"
        LblPeakThickness.TextAlign = ContentAlignment.MiddleCenter
        TipRainbowBar.SetToolTipImage(LblPeakThickness, Nothing)
        ' 
        ' TBPeakThreshold
        ' 
        TBPeakThreshold.BackColor = Color.Transparent
        TBPeakThreshold.BeforeTouchSize = New Size(20, 290)
        TBPeakThreshold.Location = New Point(463, 26)
        TBPeakThreshold.Name = "TBPeakThreshold"
        TBPeakThreshold.Orientation = Orientation.Vertical
        TBPeakThreshold.ShowFocusRect = False
        TBPeakThreshold.Size = New Size(20, 290)
        TBPeakThreshold.SmallChange = 5
        TBPeakThreshold.TabIndex = 9
        TBPeakThreshold.Text = "TrackBarEx2"
        TBPeakThreshold.TimerInterval = 100
        TipRainbowBar.SetToolTip(TBPeakThreshold, "Pixels Above Bottom, to avoid flicker along bottom.")
        TipRainbowBar.SetToolTipImage(TBPeakThreshold, Nothing)
        TBPeakThreshold.Value = 5
        ' 
        ' LblPeakThreshold
        ' 
        LblPeakThreshold.Location = New Point(414, 5)
        LblPeakThreshold.Name = "LblPeakThreshold"
        LblPeakThreshold.Size = New Size(119, 23)
        LblPeakThreshold.TabIndex = 10
        LblPeakThreshold.Text = "Peak Threshold"
        LblPeakThreshold.TextAlign = ContentAlignment.MiddleCenter
        TipRainbowBar.SetToolTipImage(LblPeakThreshold, Nothing)
        ' 
        ' LblHueCycleSpeed
        ' 
        LblHueCycleSpeed.Location = New Point(664, 5)
        LblHueCycleSpeed.Name = "LblHueCycleSpeed"
        LblHueCycleSpeed.Size = New Size(131, 23)
        LblHueCycleSpeed.TabIndex = 12
        LblHueCycleSpeed.Text = "Hue Cycle Speed"
        LblHueCycleSpeed.TextAlign = ContentAlignment.MiddleCenter
        TipRainbowBar.SetToolTipImage(LblHueCycleSpeed, Nothing)
        ' 
        ' TBHueCycleSpeed
        ' 
        TBHueCycleSpeed.BackColor = Color.Transparent
        TBHueCycleSpeed.BeforeTouchSize = New Size(20, 290)
        TBHueCycleSpeed.Location = New Point(719, 29)
        TBHueCycleSpeed.Name = "TBHueCycleSpeed"
        TBHueCycleSpeed.Orientation = Orientation.Vertical
        TBHueCycleSpeed.ShowFocusRect = False
        TBHueCycleSpeed.Size = New Size(20, 290)
        TBHueCycleSpeed.SmallChange = 5
        TBHueCycleSpeed.TabIndex = 11
        TBHueCycleSpeed.Text = "TrackBarEx2"
        TBHueCycleSpeed.TimerInterval = 100
        TipRainbowBar.SetToolTip(TBHueCycleSpeed, "How Fast the Rainbow Shifts.")
        TipRainbowBar.SetToolTipImage(TBHueCycleSpeed, Nothing)
        TBHueCycleSpeed.Value = 5
        ' 
        ' BtnCalm
        ' 
        BtnCalm.Location = New Point(13, 208)
        BtnCalm.Name = "BtnCalm"
        BtnCalm.Size = New Size(85, 32)
        BtnCalm.TabIndex = 13
        BtnCalm.Text = "Calm"
        TipRainbowBar.SetToolTipImage(BtnCalm, Nothing)
        BtnCalm.UseVisualStyleBackColor = True
        ' 
        ' LblPresets
        ' 
        LblPresets.Location = New Point(13, 182)
        LblPresets.Name = "LblPresets"
        LblPresets.Size = New Size(85, 23)
        LblPresets.TabIndex = 14
        LblPresets.Text = "Presets"
        LblPresets.TextAlign = ContentAlignment.MiddleCenter
        TipRainbowBar.SetToolTipImage(LblPresets, Nothing)
        ' 
        ' BtnEnergetic
        ' 
        BtnEnergetic.Location = New Point(13, 246)
        BtnEnergetic.Name = "BtnEnergetic"
        BtnEnergetic.Size = New Size(85, 32)
        BtnEnergetic.TabIndex = 15
        BtnEnergetic.Text = "Energetic"
        TipRainbowBar.SetToolTipImage(BtnEnergetic, Nothing)
        BtnEnergetic.UseVisualStyleBackColor = True
        ' 
        ' BtnExtreme
        ' 
        BtnExtreme.Location = New Point(13, 284)
        BtnExtreme.Name = "BtnExtreme"
        BtnExtreme.Size = New Size(85, 32)
        BtnExtreme.TabIndex = 16
        BtnExtreme.Text = "Extreme"
        TipRainbowBar.SetToolTipImage(BtnExtreme, Nothing)
        BtnExtreme.UseVisualStyleBackColor = True
        ' 
        ' TipRainbowBar
        ' 
        TipRainbowBar.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipRainbowBar.OwnerDraw = True
        ' 
        ' LblPeakHoldFrames
        ' 
        LblPeakHoldFrames.Location = New Point(530, 5)
        LblPeakHoldFrames.Name = "LblPeakHoldFrames"
        LblPeakHoldFrames.Size = New Size(137, 23)
        LblPeakHoldFrames.TabIndex = 18
        LblPeakHoldFrames.Text = "Peak Hold Frames"
        LblPeakHoldFrames.TextAlign = ContentAlignment.MiddleCenter
        TipRainbowBar.SetToolTipImage(LblPeakHoldFrames, Nothing)
        ' 
        ' TBPeakHoldFrames
        ' 
        TBPeakHoldFrames.BackColor = Color.Transparent
        TBPeakHoldFrames.BeforeTouchSize = New Size(20, 290)
        TBPeakHoldFrames.Location = New Point(588, 26)
        TBPeakHoldFrames.Name = "TBPeakHoldFrames"
        TBPeakHoldFrames.Orientation = Orientation.Vertical
        TBPeakHoldFrames.ShowFocusRect = False
        TBPeakHoldFrames.Size = New Size(20, 290)
        TBPeakHoldFrames.TabIndex = 17
        TBPeakHoldFrames.Text = "TrackBarEx2"
        TBPeakHoldFrames.TimerInterval = 100
        TipRainbowBar.SetToolTip(TBPeakHoldFrames, "How long peaks " & ChrW(8220) & "stick" & ChrW(8221) & " before decaying." & vbCrLf & "At 30 FPS, 30 = ~1 second.")
        TipRainbowBar.SetToolTipImage(TBPeakHoldFrames, Nothing)
        TBPeakHoldFrames.Value = 5
        ' 
        ' OptionsRainbowBar
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(LblPeakHoldFrames)
        Controls.Add(TBPeakHoldFrames)
        Controls.Add(BtnExtreme)
        Controls.Add(BtnEnergetic)
        Controls.Add(LblPresets)
        Controls.Add(BtnCalm)
        Controls.Add(LblHueCycleSpeed)
        Controls.Add(TBHueCycleSpeed)
        Controls.Add(LblPeakThreshold)
        Controls.Add(TBPeakThreshold)
        Controls.Add(LblPeakThickness)
        Controls.Add(LblPeakDecaySpeed)
        Controls.Add(TBPeakThickness)
        Controls.Add(TBPeakDecaySpeed)
        Controls.Add(ChkBoxShowPeaks)
        Controls.Add(TxtBoxBarCount)
        Controls.Add(LblBarCount)
        Controls.Add(TBGain)
        Controls.Add(LblGain)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsRainbowBar"
        Size = New Size(800, 330)
        TipRainbowBar.SetToolTipImage(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TBGain As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblBarCount As Skye.UI.Label
    Friend WithEvents TxtBoxBarCount As TextBox
    Friend WithEvents LblGain As Skye.UI.Label
    Friend WithEvents ChkBoxShowPeaks As CheckBox
    Friend WithEvents TBPeakDecaySpeed As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBPeakThickness As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblPeakDecaySpeed As Skye.UI.Label
    Friend WithEvents LblPeakThickness As Skye.UI.Label
    Friend WithEvents TBPeakThreshold As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblPeakThreshold As Skye.UI.Label
    Friend WithEvents LblHueCycleSpeed As Skye.UI.Label
    Friend WithEvents TBHueCycleSpeed As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents BtnCalm As Button
    Friend WithEvents LblPresets As Skye.UI.Label
    Friend WithEvents BtnEnergetic As Button
    Friend WithEvents BtnExtreme As Button
    Friend WithEvents TipRainbowBar As Skye.UI.ToolTip
    Friend WithEvents LblPeakHoldFrames As Skye.UI.Label
    Friend WithEvents TBPeakHoldFrames As Syncfusion.Windows.Forms.Tools.TrackBarEx

End Class
