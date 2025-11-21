<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsOscilloscope
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
        CoBoxChannelMode = New ComboBox()
        TBFadeAlpha = New Syncfusion.Windows.Forms.Tools.TrackBarEx(16, 128)
        TBLineWidth = New Syncfusion.Windows.Forms.Tools.TrackBarEx(5, 100)
        TBSmoothing = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 100)
        BtnSoftGlow = New Button()
        BtnMinimal = New Button()
        BtnNeonPulse = New Button()
        BtnLiquid = New Button()
        ChkBoxGlow = New CheckBox()
        LblPresets = New Skye.UI.Label()
        BtnClean = New Button()
        TBGain = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 80)
        LblFadeAlpha = New Skye.UI.Label()
        LblLineWidth = New Skye.UI.Label()
        LblSmoothing = New Skye.UI.Label()
        LblGain = New Skye.UI.Label()
        LblChannelMode = New Skye.UI.Label()
        TipOscilloscope = New Skye.UI.ToolTip(components)
        BtnDreamy = New Button()
        BtnPunchy = New Button()
        SuspendLayout()
        ' 
        ' CoBoxChannelMode
        ' 
        CoBoxChannelMode.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxChannelMode.FlatStyle = FlatStyle.Flat
        CoBoxChannelMode.FormattingEnabled = True
        CoBoxChannelMode.Location = New Point(9, 33)
        CoBoxChannelMode.Name = "CoBoxChannelMode"
        CoBoxChannelMode.Size = New Size(178, 29)
        CoBoxChannelMode.TabIndex = 153
        TipOscilloscope.SetToolTip(CoBoxChannelMode, "Channel Mode for the Oscilloscope.")
        TipOscilloscope.SetToolTipImage(CoBoxChannelMode, Nothing)
        ' 
        ' TBFadeAlpha
        ' 
        TBFadeAlpha.BackColor = Color.Transparent
        TBFadeAlpha.BeforeTouchSize = New Size(20, 290)
        TBFadeAlpha.LargeChange = 10
        TBFadeAlpha.Location = New Point(705, 33)
        TBFadeAlpha.Margin = New Padding(4)
        TBFadeAlpha.Name = "TBFadeAlpha"
        TBFadeAlpha.Orientation = Orientation.Vertical
        TBFadeAlpha.ShowFocusRect = False
        TBFadeAlpha.Size = New Size(20, 290)
        TBFadeAlpha.TabIndex = 144
        TBFadeAlpha.TabStop = False
        TBFadeAlpha.TimerInterval = 100
        TipOscilloscope.SetToolTip(TBFadeAlpha, "Alpha Value for the Glow Effect. Higher Values = Less After Glow.")
        TipOscilloscope.SetToolTipImage(TBFadeAlpha, Nothing)
        TBFadeAlpha.Value = 16
        ' 
        ' TBLineWidth
        ' 
        TBLineWidth.BackColor = Color.Transparent
        TBLineWidth.BeforeTouchSize = New Size(20, 290)
        TBLineWidth.LargeChange = 10
        TBLineWidth.Location = New Point(595, 33)
        TBLineWidth.Margin = New Padding(4)
        TBLineWidth.Name = "TBLineWidth"
        TBLineWidth.Orientation = Orientation.Vertical
        TBLineWidth.ShowFocusRect = False
        TBLineWidth.Size = New Size(20, 290)
        TBLineWidth.TabIndex = 143
        TBLineWidth.TabStop = False
        TBLineWidth.TimerInterval = 100
        TipOscilloscope.SetToolTip(TBLineWidth, "Width of the Oscilloscope Line.")
        TipOscilloscope.SetToolTipImage(TBLineWidth, Nothing)
        TBLineWidth.Value = 10
        ' 
        ' TBSmoothing
        ' 
        TBSmoothing.BackColor = Color.Transparent
        TBSmoothing.BeforeTouchSize = New Size(20, 290)
        TBSmoothing.LargeChange = 10
        TBSmoothing.Location = New Point(487, 33)
        TBSmoothing.Margin = New Padding(4)
        TBSmoothing.Name = "TBSmoothing"
        TBSmoothing.Orientation = Orientation.Vertical
        TBSmoothing.ShowFocusRect = False
        TBSmoothing.Size = New Size(20, 290)
        TBSmoothing.TabIndex = 142
        TBSmoothing.TabStop = False
        TBSmoothing.TimerInterval = 100
        TipOscilloscope.SetToolTip(TBSmoothing, "Smoothing Factor for the Oscilloscope Data.")
        TipOscilloscope.SetToolTipImage(TBSmoothing, Nothing)
        TBSmoothing.Value = 10
        ' 
        ' BtnSoftGlow
        ' 
        BtnSoftGlow.Location = New Point(9, 130)
        BtnSoftGlow.Name = "BtnSoftGlow"
        BtnSoftGlow.Size = New Size(112, 32)
        BtnSoftGlow.TabIndex = 156
        BtnSoftGlow.Text = "Soft Glow"
        TipOscilloscope.SetToolTipImage(BtnSoftGlow, Nothing)
        BtnSoftGlow.UseVisualStyleBackColor = True
        ' 
        ' BtnMinimal
        ' 
        BtnMinimal.Location = New Point(9, 290)
        BtnMinimal.Name = "BtnMinimal"
        BtnMinimal.Size = New Size(112, 32)
        BtnMinimal.TabIndex = 159
        BtnMinimal.Text = "Minimal"
        TipOscilloscope.SetToolTipImage(BtnMinimal, Nothing)
        BtnMinimal.UseVisualStyleBackColor = True
        ' 
        ' BtnNeonPulse
        ' 
        BtnNeonPulse.Location = New Point(9, 258)
        BtnNeonPulse.Name = "BtnNeonPulse"
        BtnNeonPulse.Size = New Size(112, 32)
        BtnNeonPulse.TabIndex = 158
        BtnNeonPulse.Text = "Neon Pulse"
        TipOscilloscope.SetToolTipImage(BtnNeonPulse, Nothing)
        BtnNeonPulse.UseVisualStyleBackColor = True
        ' 
        ' BtnLiquid
        ' 
        BtnLiquid.Location = New Point(9, 226)
        BtnLiquid.Name = "BtnLiquid"
        BtnLiquid.Size = New Size(112, 32)
        BtnLiquid.TabIndex = 157
        BtnLiquid.Text = "Liquid"
        TipOscilloscope.SetToolTipImage(BtnLiquid, Nothing)
        BtnLiquid.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxGlow
        ' 
        ChkBoxGlow.AutoSize = True
        ChkBoxGlow.BackColor = Color.Transparent
        ChkBoxGlow.FlatStyle = FlatStyle.Flat
        ChkBoxGlow.Location = New Point(224, 35)
        ChkBoxGlow.Name = "ChkBoxGlow"
        ChkBoxGlow.Size = New Size(112, 25)
        ChkBoxGlow.TabIndex = 149
        ChkBoxGlow.Text = "Enable Glow"
        ChkBoxGlow.TextAlign = ContentAlignment.MiddleCenter
        TipOscilloscope.SetToolTip(ChkBoxGlow, "Whether To Enable Glow Effect on the Oscilloscope Line.")
        TipOscilloscope.SetToolTipImage(ChkBoxGlow, Nothing)
        ChkBoxGlow.UseVisualStyleBackColor = False
        ' 
        ' LblPresets
        ' 
        LblPresets.Font = New Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        LblPresets.Location = New Point(9, 76)
        LblPresets.Name = "LblPresets"
        LblPresets.Size = New Size(112, 23)
        LblPresets.TabIndex = 148
        LblPresets.Text = " Presets "
        LblPresets.TextAlign = ContentAlignment.MiddleCenter
        TipOscilloscope.SetToolTipImage(LblPresets, Nothing)
        ' 
        ' BtnClean
        ' 
        BtnClean.Location = New Point(8, 98)
        BtnClean.Name = "BtnClean"
        BtnClean.Size = New Size(113, 32)
        BtnClean.TabIndex = 155
        BtnClean.Text = "Clean"
        TipOscilloscope.SetToolTipImage(BtnClean, Nothing)
        BtnClean.UseVisualStyleBackColor = True
        ' 
        ' TBGain
        ' 
        TBGain.BackColor = Color.Transparent
        TBGain.BeforeTouchSize = New Size(20, 290)
        TBGain.LargeChange = 10
        TBGain.Location = New Point(392, 33)
        TBGain.Margin = New Padding(4)
        TBGain.Name = "TBGain"
        TBGain.Orientation = Orientation.Vertical
        TBGain.ShowFocusRect = False
        TBGain.Size = New Size(20, 290)
        TBGain.TabIndex = 141
        TBGain.TabStop = False
        TBGain.TimerInterval = 100
        TipOscilloscope.SetToolTip(TBGain, "Gain Multiplier for the Oscilloscope Data.")
        TipOscilloscope.SetToolTipImage(TBGain, Nothing)
        TBGain.Value = 10
        ' 
        ' LblFadeAlpha
        ' 
        LblFadeAlpha.Location = New Point(666, 12)
        LblFadeAlpha.Name = "LblFadeAlpha"
        LblFadeAlpha.Size = New Size(99, 23)
        LblFadeAlpha.TabIndex = 152
        LblFadeAlpha.Text = "Fade Alpha"
        LblFadeAlpha.TextAlign = ContentAlignment.MiddleCenter
        TipOscilloscope.SetToolTipImage(LblFadeAlpha, Nothing)
        ' 
        ' LblLineWidth
        ' 
        LblLineWidth.Location = New Point(539, 12)
        LblLineWidth.Name = "LblLineWidth"
        LblLineWidth.Size = New Size(132, 23)
        LblLineWidth.TabIndex = 151
        LblLineWidth.Text = "Line Width"
        LblLineWidth.TextAlign = ContentAlignment.MiddleCenter
        TipOscilloscope.SetToolTipImage(LblLineWidth, Nothing)
        ' 
        ' LblSmoothing
        ' 
        LblSmoothing.Location = New Point(444, 12)
        LblSmoothing.Name = "LblSmoothing"
        LblSmoothing.Size = New Size(106, 23)
        LblSmoothing.TabIndex = 150
        LblSmoothing.Text = "Smoothing"
        LblSmoothing.TextAlign = ContentAlignment.MiddleCenter
        TipOscilloscope.SetToolTipImage(LblSmoothing, Nothing)
        ' 
        ' LblGain
        ' 
        LblGain.Location = New Point(367, 12)
        LblGain.Name = "LblGain"
        LblGain.Size = New Size(71, 23)
        LblGain.TabIndex = 147
        LblGain.Text = "Gain"
        LblGain.TextAlign = ContentAlignment.MiddleCenter
        TipOscilloscope.SetToolTipImage(LblGain, Nothing)
        ' 
        ' LblChannelMode
        ' 
        LblChannelMode.Location = New Point(8, 10)
        LblChannelMode.Name = "LblChannelMode"
        LblChannelMode.Size = New Size(162, 23)
        LblChannelMode.TabIndex = 154
        LblChannelMode.Text = "Channel Mode"
        TipOscilloscope.SetToolTipImage(LblChannelMode, Nothing)
        ' 
        ' TipOscilloscope
        ' 
        TipOscilloscope.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipOscilloscope.OwnerDraw = True
        ' 
        ' BtnDreamy
        ' 
        BtnDreamy.Location = New Point(9, 162)
        BtnDreamy.Name = "BtnDreamy"
        BtnDreamy.Size = New Size(112, 32)
        BtnDreamy.TabIndex = 160
        BtnDreamy.Text = "Dreamy"
        TipOscilloscope.SetToolTipImage(BtnDreamy, Nothing)
        BtnDreamy.UseVisualStyleBackColor = True
        ' 
        ' BtnPunchy
        ' 
        BtnPunchy.Location = New Point(9, 194)
        BtnPunchy.Name = "BtnPunchy"
        BtnPunchy.Size = New Size(112, 32)
        BtnPunchy.TabIndex = 161
        BtnPunchy.Text = "Punchy"
        TipOscilloscope.SetToolTipImage(BtnPunchy, Nothing)
        BtnPunchy.UseVisualStyleBackColor = True
        ' 
        ' OptionsOscilloscope
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(BtnPunchy)
        Controls.Add(BtnDreamy)
        Controls.Add(CoBoxChannelMode)
        Controls.Add(TBFadeAlpha)
        Controls.Add(TBLineWidth)
        Controls.Add(TBSmoothing)
        Controls.Add(BtnSoftGlow)
        Controls.Add(BtnMinimal)
        Controls.Add(BtnNeonPulse)
        Controls.Add(BtnLiquid)
        Controls.Add(ChkBoxGlow)
        Controls.Add(LblPresets)
        Controls.Add(BtnClean)
        Controls.Add(TBGain)
        Controls.Add(LblFadeAlpha)
        Controls.Add(LblLineWidth)
        Controls.Add(LblSmoothing)
        Controls.Add(LblGain)
        Controls.Add(LblChannelMode)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsOscilloscope"
        Size = New Size(800, 330)
        TipOscilloscope.SetToolTipImage(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents CoBoxChannelMode As ComboBox
    Friend WithEvents TipOscilloscope As Skye.UI.ToolTip
    Friend WithEvents TBFadeAlpha As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBLineWidth As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBSmoothing As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents BtnSoftGlow As Button
    Friend WithEvents BtnMinimal As Button
    Friend WithEvents BtnNeonPulse As Button
    Friend WithEvents BtnLiquid As Button
    Friend WithEvents ChkBoxGlow As CheckBox
    Friend WithEvents LblPresets As Skye.UI.Label
    Friend WithEvents BtnClean As Button
    Friend WithEvents TBGain As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblFadeAlpha As Skye.UI.Label
    Friend WithEvents LblLineWidth As Skye.UI.Label
    Friend WithEvents LblSmoothing As Skye.UI.Label
    Friend WithEvents LblGain As Skye.UI.Label
    Friend WithEvents LblChannelMode As Skye.UI.Label
    Friend WithEvents BtnDreamy As Button
    Friend WithEvents BtnPunchy As Button

End Class
