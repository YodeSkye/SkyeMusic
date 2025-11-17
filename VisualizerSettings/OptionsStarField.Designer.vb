<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsStarField
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
        LblMaxStarSize = New Skye.UI.Label()
        LblAudioSpeedFactor = New Skye.UI.Label()
        TBMaxStarSize = New Syncfusion.Windows.Forms.Tools.TrackBarEx(2, 12)
        TBAudioSpeedFactor = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 100)
        TxtBoxStarCount = New TextBox()
        LblStarCount = New Skye.UI.Label()
        TBBaseSpeed = New Syncfusion.Windows.Forms.Tools.TrackBarEx(10, 50)
        LblBaseSpeed = New Skye.UI.Label()
        LblPresets = New Skye.UI.Label()
        BtnCalmDrift = New Button()
        TipStarField = New Skye.UI.ToolTip(components)
        BtnClassicArcade = New Button()
        BtnHyperWarp = New Button()
        BtnNebulaDust = New Button()
        BtnSilentSpace = New Button()
        SuspendLayout()
        ' 
        ' LblMaxStarSize
        ' 
        LblMaxStarSize.Location = New Point(613, 8)
        LblMaxStarSize.Name = "LblMaxStarSize"
        LblMaxStarSize.Size = New Size(147, 23)
        LblMaxStarSize.TabIndex = 26
        LblMaxStarSize.Text = "Max Star Size"
        LblMaxStarSize.TextAlign = ContentAlignment.MiddleCenter
        TipStarField.SetToolTipImage(LblMaxStarSize, Nothing)
        ' 
        ' LblAudioSpeedFactor
        ' 
        LblAudioSpeedFactor.Location = New Point(449, 8)
        LblAudioSpeedFactor.Name = "LblAudioSpeedFactor"
        LblAudioSpeedFactor.Size = New Size(186, 23)
        LblAudioSpeedFactor.TabIndex = 25
        LblAudioSpeedFactor.Text = "Audio Speed Factor"
        LblAudioSpeedFactor.TextAlign = ContentAlignment.MiddleCenter
        TipStarField.SetToolTipImage(LblAudioSpeedFactor, Nothing)
        ' 
        ' TBMaxStarSize
        ' 
        TBMaxStarSize.BackColor = Color.Transparent
        TBMaxStarSize.BeforeTouchSize = New Size(20, 290)
        TBMaxStarSize.LargeChange = 2
        TBMaxStarSize.Location = New Point(676, 29)
        TBMaxStarSize.Name = "TBMaxStarSize"
        TBMaxStarSize.Orientation = Orientation.Vertical
        TBMaxStarSize.ShowFocusRect = False
        TBMaxStarSize.Size = New Size(20, 290)
        TBMaxStarSize.TabIndex = 24
        TBMaxStarSize.TabStop = False
        TBMaxStarSize.Text = "TrackBarEx2"
        TBMaxStarSize.TimerInterval = 100
        TipStarField.SetToolTip(TBMaxStarSize, "Maximum Size of Stars.")
        TipStarField.SetToolTipImage(TBMaxStarSize, Nothing)
        TBMaxStarSize.Value = 10
        ' 
        ' TBAudioSpeedFactor
        ' 
        TBAudioSpeedFactor.BackColor = Color.Transparent
        TBAudioSpeedFactor.BeforeTouchSize = New Size(20, 290)
        TBAudioSpeedFactor.Location = New Point(532, 29)
        TBAudioSpeedFactor.Name = "TBAudioSpeedFactor"
        TBAudioSpeedFactor.Orientation = Orientation.Vertical
        TBAudioSpeedFactor.ShowFocusRect = False
        TBAudioSpeedFactor.Size = New Size(20, 290)
        TBAudioSpeedFactor.TabIndex = 23
        TBAudioSpeedFactor.TabStop = False
        TBAudioSpeedFactor.Text = "TrackBarEx1"
        TBAudioSpeedFactor.TimerInterval = 100
        TipStarField.SetToolTip(TBAudioSpeedFactor, "How Much the Audio Level Boosts Star Speed.")
        TipStarField.SetToolTipImage(TBAudioSpeedFactor, Nothing)
        TBAudioSpeedFactor.Value = 5
        ' 
        ' TxtBoxStarCount
        ' 
        TxtBoxStarCount.Location = New Point(13, 32)
        TxtBoxStarCount.Margin = New Padding(4)
        TxtBoxStarCount.Name = "TxtBoxStarCount"
        TxtBoxStarCount.Size = New Size(86, 29)
        TxtBoxStarCount.TabIndex = 21
        TxtBoxStarCount.TextAlign = HorizontalAlignment.Center
        TipStarField.SetToolTip(TxtBoxStarCount, "Number of Stars.")
        TipStarField.SetToolTipImage(TxtBoxStarCount, Nothing)
        ' 
        ' LblStarCount
        ' 
        LblStarCount.Location = New Point(14, 3)
        LblStarCount.Margin = New Padding(4, 0, 4, 0)
        LblStarCount.Name = "LblStarCount"
        LblStarCount.Size = New Size(85, 32)
        LblStarCount.TabIndex = 20
        LblStarCount.Text = "Star Count"
        LblStarCount.TextAlign = ContentAlignment.MiddleCenter
        TipStarField.SetToolTipImage(LblStarCount, Nothing)
        ' 
        ' TBBaseSpeed
        ' 
        TBBaseSpeed.BackColor = Color.Transparent
        TBBaseSpeed.BeforeTouchSize = New Size(20, 290)
        TBBaseSpeed.Location = New Point(390, 29)
        TBBaseSpeed.Margin = New Padding(4)
        TBBaseSpeed.Name = "TBBaseSpeed"
        TBBaseSpeed.Orientation = Orientation.Vertical
        TBBaseSpeed.ShowFocusRect = False
        TBBaseSpeed.Size = New Size(20, 290)
        TBBaseSpeed.TabIndex = 19
        TBBaseSpeed.TabStop = False
        TBBaseSpeed.TimerInterval = 100
        TipStarField.SetToolTip(TBBaseSpeed, "Minimum Star Movement Speed.")
        TipStarField.SetToolTipImage(TBBaseSpeed, Nothing)
        TBBaseSpeed.Value = 10
        ' 
        ' LblBaseSpeed
        ' 
        LblBaseSpeed.Location = New Point(334, 8)
        LblBaseSpeed.Name = "LblBaseSpeed"
        LblBaseSpeed.Size = New Size(132, 23)
        LblBaseSpeed.TabIndex = 22
        LblBaseSpeed.Text = "Base Speed"
        LblBaseSpeed.TextAlign = ContentAlignment.MiddleCenter
        TipStarField.SetToolTipImage(LblBaseSpeed, Nothing)
        ' 
        ' LblPresets
        ' 
        LblPresets.Font = New Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        LblPresets.Location = New Point(14, 109)
        LblPresets.Name = "LblPresets"
        LblPresets.Size = New Size(117, 23)
        LblPresets.TabIndex = 28
        LblPresets.Text = " Presets "
        LblPresets.TextAlign = ContentAlignment.MiddleCenter
        TipStarField.SetToolTipImage(LblPresets, Nothing)
        ' 
        ' BtnCalmDrift
        ' 
        BtnCalmDrift.Location = New Point(13, 135)
        BtnCalmDrift.Name = "BtnCalmDrift"
        BtnCalmDrift.Size = New Size(118, 32)
        BtnCalmDrift.TabIndex = 27
        BtnCalmDrift.Text = "Calm Drift"
        TipStarField.SetToolTipImage(BtnCalmDrift, Nothing)
        BtnCalmDrift.UseVisualStyleBackColor = True
        ' 
        ' TipStarField
        ' 
        TipStarField.Font = New Font("Segoe UI", 10F)
        TipStarField.OwnerDraw = True
        ' 
        ' BtnClassicArcade
        ' 
        BtnClassicArcade.Location = New Point(14, 173)
        BtnClassicArcade.Name = "BtnClassicArcade"
        BtnClassicArcade.Size = New Size(117, 32)
        BtnClassicArcade.TabIndex = 29
        BtnClassicArcade.Text = "Classic Arcade"
        TipStarField.SetToolTipImage(BtnClassicArcade, Nothing)
        BtnClassicArcade.UseVisualStyleBackColor = True
        ' 
        ' BtnHyperWarp
        ' 
        BtnHyperWarp.Location = New Point(14, 211)
        BtnHyperWarp.Name = "BtnHyperWarp"
        BtnHyperWarp.Size = New Size(117, 32)
        BtnHyperWarp.TabIndex = 30
        BtnHyperWarp.Text = "Hyper Warp"
        TipStarField.SetToolTipImage(BtnHyperWarp, Nothing)
        BtnHyperWarp.UseVisualStyleBackColor = True
        ' 
        ' BtnNebulaDust
        ' 
        BtnNebulaDust.Location = New Point(14, 249)
        BtnNebulaDust.Name = "BtnNebulaDust"
        BtnNebulaDust.Size = New Size(117, 32)
        BtnNebulaDust.TabIndex = 31
        BtnNebulaDust.Text = "Nebula Dust"
        TipStarField.SetToolTipImage(BtnNebulaDust, Nothing)
        BtnNebulaDust.UseVisualStyleBackColor = True
        ' 
        ' BtnSilentSpace
        ' 
        BtnSilentSpace.Location = New Point(14, 287)
        BtnSilentSpace.Name = "BtnSilentSpace"
        BtnSilentSpace.Size = New Size(117, 32)
        BtnSilentSpace.TabIndex = 32
        BtnSilentSpace.Text = "Silent Space"
        TipStarField.SetToolTipImage(BtnSilentSpace, Nothing)
        BtnSilentSpace.UseVisualStyleBackColor = True
        ' 
        ' OptionsStarField
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(BtnSilentSpace)
        Controls.Add(BtnNebulaDust)
        Controls.Add(BtnHyperWarp)
        Controls.Add(BtnClassicArcade)
        Controls.Add(LblPresets)
        Controls.Add(BtnCalmDrift)
        Controls.Add(TBMaxStarSize)
        Controls.Add(TBAudioSpeedFactor)
        Controls.Add(TxtBoxStarCount)
        Controls.Add(LblStarCount)
        Controls.Add(TBBaseSpeed)
        Controls.Add(LblAudioSpeedFactor)
        Controls.Add(LblMaxStarSize)
        Controls.Add(LblBaseSpeed)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsStarField"
        Size = New Size(800, 330)
        TipStarField.SetToolTipImage(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblMaxStarSize As Skye.UI.Label
    Friend WithEvents LblAudioSpeedFactor As Skye.UI.Label
    Friend WithEvents TBMaxStarSize As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBAudioSpeedFactor As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TxtBoxStarCount As TextBox
    Friend WithEvents LblStarCount As Skye.UI.Label
    Friend WithEvents TBBaseSpeed As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblBaseSpeed As Skye.UI.Label
    Friend WithEvents LblPresets As Skye.UI.Label
    Friend WithEvents BtnCalmDrift As Button
    Friend WithEvents TipStarField As Skye.UI.ToolTip
    Friend WithEvents BtnClassicArcade As Button
    Friend WithEvents BtnHyperWarp As Button
    Friend WithEvents BtnNebulaDust As Button
    Friend WithEvents BtnSilentSpace As Button

End Class
