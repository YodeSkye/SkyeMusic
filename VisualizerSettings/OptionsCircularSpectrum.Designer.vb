<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsCircularSpectrum
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
        BtnTrebleGlow = New Button()
        BtnWarmAnalog = New Button()
        CoBoxWeightingMode = New ComboBox()
        TBRadiusFactor = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 5)
        TBLineWidth = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 5)
        TBSmoothing = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 100)
        BtnBassBoost = New Button()
        BtnPosterBold = New Button()
        BtnSmoothMid = New Button()
        BtnVShapeEnergy = New Button()
        ChkBoxFill = New CheckBox()
        LblPresets = New Skye.UI.Label()
        BtnClassic = New Button()
        TBGain = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 300)
        LblRadiusFactor = New Skye.UI.Label()
        LblLineWidth = New Skye.UI.Label()
        LblSmoothing = New Skye.UI.Label()
        LblGain = New Skye.UI.Label()
        LblWeightingMode = New Skye.UI.Label()
        TipCircularSpectrum = New Skye.UI.ToolTip(components)
        ChkBoxAllowMiniMode = New CheckBox()
        SuspendLayout()
        ' 
        ' BtnTrebleGlow
        ' 
        BtnTrebleGlow.Location = New Point(11, 161)
        BtnTrebleGlow.Name = "BtnTrebleGlow"
        BtnTrebleGlow.Size = New Size(147, 32)
        BtnTrebleGlow.TabIndex = 179
        BtnTrebleGlow.Text = "Treble Glow"
        TipCircularSpectrum.SetToolTipImage(BtnTrebleGlow, Nothing)
        BtnTrebleGlow.UseVisualStyleBackColor = True
        ' 
        ' BtnWarmAnalog
        ' 
        BtnWarmAnalog.Location = New Point(11, 193)
        BtnWarmAnalog.Name = "BtnWarmAnalog"
        BtnWarmAnalog.Size = New Size(147, 32)
        BtnWarmAnalog.TabIndex = 180
        BtnWarmAnalog.Text = "Warm Analog"
        TipCircularSpectrum.SetToolTipImage(BtnWarmAnalog, Nothing)
        BtnWarmAnalog.UseVisualStyleBackColor = True
        ' 
        ' CoBoxWeightingMode
        ' 
        CoBoxWeightingMode.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxWeightingMode.FlatStyle = FlatStyle.Flat
        CoBoxWeightingMode.FormattingEnabled = True
        CoBoxWeightingMode.Location = New Point(11, 30)
        CoBoxWeightingMode.Name = "CoBoxWeightingMode"
        CoBoxWeightingMode.Size = New Size(200, 29)
        CoBoxWeightingMode.TabIndex = 172
        TipCircularSpectrum.SetToolTip(CoBoxWeightingMode, "Frequency Emphasis Curve.")
        TipCircularSpectrum.SetToolTipImage(CoBoxWeightingMode, Nothing)
        ' 
        ' TBRadiusFactor
        ' 
        TBRadiusFactor.BackColor = Color.Transparent
        TBRadiusFactor.BeforeTouchSize = New Size(20, 290)
        TBRadiusFactor.LargeChange = 1
        TBRadiusFactor.Location = New Point(721, 32)
        TBRadiusFactor.Margin = New Padding(4)
        TBRadiusFactor.Name = "TBRadiusFactor"
        TBRadiusFactor.Orientation = Orientation.Vertical
        TBRadiusFactor.ShowFocusRect = False
        TBRadiusFactor.Size = New Size(20, 290)
        TBRadiusFactor.TabIndex = 165
        TBRadiusFactor.TabStop = False
        TBRadiusFactor.TimerInterval = 100
        TipCircularSpectrum.SetToolTip(TBRadiusFactor, "Proportion Of Control Size Used As Base Radius.")
        TipCircularSpectrum.SetToolTipImage(TBRadiusFactor, Nothing)
        TBRadiusFactor.Value = 5
        ' 
        ' TBLineWidth
        ' 
        TBLineWidth.BackColor = Color.Transparent
        TBLineWidth.BeforeTouchSize = New Size(20, 290)
        TBLineWidth.LargeChange = 1
        TBLineWidth.Location = New Point(609, 32)
        TBLineWidth.Margin = New Padding(4)
        TBLineWidth.Name = "TBLineWidth"
        TBLineWidth.Orientation = Orientation.Vertical
        TBLineWidth.ShowFocusRect = False
        TBLineWidth.Size = New Size(20, 290)
        TBLineWidth.TabIndex = 164
        TBLineWidth.TabStop = False
        TBLineWidth.TimerInterval = 100
        TipCircularSpectrum.SetToolTip(TBLineWidth, "Thickness Of Each Spoke Line.")
        TipCircularSpectrum.SetToolTipImage(TBLineWidth, Nothing)
        TBLineWidth.Value = 5
        ' 
        ' TBSmoothing
        ' 
        TBSmoothing.BackColor = Color.Transparent
        TBSmoothing.BeforeTouchSize = New Size(20, 290)
        TBSmoothing.LargeChange = 10
        TBSmoothing.Location = New Point(501, 32)
        TBSmoothing.Margin = New Padding(4)
        TBSmoothing.Name = "TBSmoothing"
        TBSmoothing.Orientation = Orientation.Vertical
        TBSmoothing.ShowFocusRect = False
        TBSmoothing.Size = New Size(20, 290)
        TBSmoothing.TabIndex = 163
        TBSmoothing.TabStop = False
        TBSmoothing.TimerInterval = 100
        TipCircularSpectrum.SetToolTip(TBSmoothing, "Blend Factor For Smoothing, 0 = No Smoothing.")
        TipCircularSpectrum.SetToolTipImage(TBSmoothing, Nothing)
        TBSmoothing.Value = 10
        ' 
        ' BtnBassBoost
        ' 
        BtnBassBoost.Location = New Point(11, 129)
        BtnBassBoost.Name = "BtnBassBoost"
        BtnBassBoost.Size = New Size(147, 32)
        BtnBassBoost.TabIndex = 175
        BtnBassBoost.Text = "Bass Boost"
        TipCircularSpectrum.SetToolTipImage(BtnBassBoost, Nothing)
        BtnBassBoost.UseVisualStyleBackColor = True
        ' 
        ' BtnPosterBold
        ' 
        BtnPosterBold.Location = New Point(11, 289)
        BtnPosterBold.Name = "BtnPosterBold"
        BtnPosterBold.Size = New Size(147, 32)
        BtnPosterBold.TabIndex = 178
        BtnPosterBold.Text = "Poster Bold"
        TipCircularSpectrum.SetToolTipImage(BtnPosterBold, Nothing)
        BtnPosterBold.UseVisualStyleBackColor = True
        ' 
        ' BtnSmoothMid
        ' 
        BtnSmoothMid.Location = New Point(11, 257)
        BtnSmoothMid.Name = "BtnSmoothMid"
        BtnSmoothMid.Size = New Size(147, 32)
        BtnSmoothMid.TabIndex = 177
        BtnSmoothMid.Text = "Smooth Mid"
        TipCircularSpectrum.SetToolTipImage(BtnSmoothMid, Nothing)
        BtnSmoothMid.UseVisualStyleBackColor = True
        ' 
        ' BtnVShapeEnergy
        ' 
        BtnVShapeEnergy.Location = New Point(11, 225)
        BtnVShapeEnergy.Name = "BtnVShapeEnergy"
        BtnVShapeEnergy.Size = New Size(147, 32)
        BtnVShapeEnergy.TabIndex = 176
        BtnVShapeEnergy.Text = "V-Shape Energy"
        TipCircularSpectrum.SetToolTipImage(BtnVShapeEnergy, Nothing)
        BtnVShapeEnergy.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxFill
        ' 
        ChkBoxFill.AutoSize = True
        ChkBoxFill.BackColor = Color.Transparent
        ChkBoxFill.FlatStyle = FlatStyle.Flat
        ChkBoxFill.Location = New Point(280, 32)
        ChkBoxFill.Name = "ChkBoxFill"
        ChkBoxFill.Size = New Size(46, 25)
        ChkBoxFill.TabIndex = 168
        ChkBoxFill.Text = "Fill"
        ChkBoxFill.TextAlign = ContentAlignment.MiddleCenter
        TipCircularSpectrum.SetToolTip(ChkBoxFill, "Whether To Fill the Area Under the Spectrum.")
        TipCircularSpectrum.SetToolTipImage(ChkBoxFill, Nothing)
        ChkBoxFill.UseVisualStyleBackColor = False
        ' 
        ' LblPresets
        ' 
        LblPresets.Font = New Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        LblPresets.Location = New Point(11, 75)
        LblPresets.Name = "LblPresets"
        LblPresets.Size = New Size(147, 23)
        LblPresets.TabIndex = 167
        LblPresets.Text = " Presets "
        LblPresets.TextAlign = ContentAlignment.MiddleCenter
        TipCircularSpectrum.SetToolTipImage(LblPresets, Nothing)
        ' 
        ' BtnClassic
        ' 
        BtnClassic.Location = New Point(10, 97)
        BtnClassic.Name = "BtnClassic"
        BtnClassic.Size = New Size(148, 32)
        BtnClassic.TabIndex = 174
        BtnClassic.Text = "Classic"
        TipCircularSpectrum.SetToolTipImage(BtnClassic, Nothing)
        BtnClassic.UseVisualStyleBackColor = True
        ' 
        ' TBGain
        ' 
        TBGain.BackColor = Color.Transparent
        TBGain.BeforeTouchSize = New Size(20, 290)
        TBGain.LargeChange = 25
        TBGain.Location = New Point(406, 32)
        TBGain.Margin = New Padding(4)
        TBGain.Name = "TBGain"
        TBGain.Orientation = Orientation.Vertical
        TBGain.ShowFocusRect = False
        TBGain.Size = New Size(20, 290)
        TBGain.TabIndex = 162
        TBGain.TabStop = False
        TBGain.TimerInterval = 100
        TipCircularSpectrum.SetToolTip(TBGain, "Gain Factor For Magnitudes.")
        TipCircularSpectrum.SetToolTipImage(TBGain, Nothing)
        TBGain.Value = 10
        ' 
        ' LblRadiusFactor
        ' 
        LblRadiusFactor.Location = New Point(677, 11)
        LblRadiusFactor.Name = "LblRadiusFactor"
        LblRadiusFactor.Size = New Size(109, 23)
        LblRadiusFactor.TabIndex = 171
        LblRadiusFactor.Text = "Radius Factor"
        LblRadiusFactor.TextAlign = ContentAlignment.MiddleCenter
        TipCircularSpectrum.SetToolTipImage(LblRadiusFactor, Nothing)
        ' 
        ' LblLineWidth
        ' 
        LblLineWidth.Location = New Point(553, 11)
        LblLineWidth.Name = "LblLineWidth"
        LblLineWidth.Size = New Size(132, 23)
        LblLineWidth.TabIndex = 170
        LblLineWidth.Text = "Line Width"
        LblLineWidth.TextAlign = ContentAlignment.MiddleCenter
        TipCircularSpectrum.SetToolTipImage(LblLineWidth, Nothing)
        ' 
        ' LblSmoothing
        ' 
        LblSmoothing.Location = New Point(458, 11)
        LblSmoothing.Name = "LblSmoothing"
        LblSmoothing.Size = New Size(106, 23)
        LblSmoothing.TabIndex = 169
        LblSmoothing.Text = "Smoothing"
        LblSmoothing.TextAlign = ContentAlignment.MiddleCenter
        TipCircularSpectrum.SetToolTipImage(LblSmoothing, Nothing)
        ' 
        ' LblGain
        ' 
        LblGain.Location = New Point(381, 11)
        LblGain.Name = "LblGain"
        LblGain.Size = New Size(71, 23)
        LblGain.TabIndex = 166
        LblGain.Text = "Gain"
        LblGain.TextAlign = ContentAlignment.MiddleCenter
        TipCircularSpectrum.SetToolTipImage(LblGain, Nothing)
        ' 
        ' LblWeightingMode
        ' 
        LblWeightingMode.Location = New Point(10, 7)
        LblWeightingMode.Name = "LblWeightingMode"
        LblWeightingMode.Size = New Size(162, 23)
        LblWeightingMode.TabIndex = 173
        LblWeightingMode.Text = "Weighting Mode"
        TipCircularSpectrum.SetToolTipImage(LblWeightingMode, Nothing)
        ' 
        ' TipCircularSpectrum
        ' 
        TipCircularSpectrum.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipCircularSpectrum.OwnerDraw = True
        ' 
        ' ChkBoxAllowMiniMode
        ' 
        ChkBoxAllowMiniMode.AutoSize = True
        ChkBoxAllowMiniMode.BackColor = Color.Transparent
        ChkBoxAllowMiniMode.FlatStyle = FlatStyle.Flat
        ChkBoxAllowMiniMode.Location = New Point(215, 293)
        ChkBoxAllowMiniMode.Name = "ChkBoxAllowMiniMode"
        ChkBoxAllowMiniMode.Size = New Size(140, 25)
        ChkBoxAllowMiniMode.TabIndex = 181
        ChkBoxAllowMiniMode.Text = "Allow MiniMode"
        ChkBoxAllowMiniMode.TextAlign = ContentAlignment.MiddleCenter
        TipCircularSpectrum.SetToolTip(ChkBoxAllowMiniMode, "Enables a compact, optimized version of this visualizer for the MiniPlayer’s smaller layout.")
        TipCircularSpectrum.SetToolTipImage(ChkBoxAllowMiniMode, Nothing)
        ChkBoxAllowMiniMode.UseVisualStyleBackColor = False
        ' 
        ' OptionsCircularSpectrum
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(ChkBoxAllowMiniMode)
        Controls.Add(BtnTrebleGlow)
        Controls.Add(BtnWarmAnalog)
        Controls.Add(CoBoxWeightingMode)
        Controls.Add(TBRadiusFactor)
        Controls.Add(TBLineWidth)
        Controls.Add(TBSmoothing)
        Controls.Add(BtnBassBoost)
        Controls.Add(BtnPosterBold)
        Controls.Add(BtnSmoothMid)
        Controls.Add(BtnVShapeEnergy)
        Controls.Add(ChkBoxFill)
        Controls.Add(LblPresets)
        Controls.Add(BtnClassic)
        Controls.Add(TBGain)
        Controls.Add(LblRadiusFactor)
        Controls.Add(LblLineWidth)
        Controls.Add(LblSmoothing)
        Controls.Add(LblGain)
        Controls.Add(LblWeightingMode)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsCircularSpectrum"
        Size = New Size(800, 330)
        TipCircularSpectrum.SetToolTipImage(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnTrebleGlow As Button
    Friend WithEvents BtnWarmAnalog As Button
    Friend WithEvents CoBoxWeightingMode As ComboBox
    Friend WithEvents TBRadiusFactor As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBLineWidth As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBSmoothing As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents BtnBassBoost As Button
    Friend WithEvents BtnPosterBold As Button
    Friend WithEvents BtnSmoothMid As Button
    Friend WithEvents BtnVShapeEnergy As Button
    Friend WithEvents ChkBoxFill As CheckBox
    Friend WithEvents LblPresets As Skye.UI.Label
    Friend WithEvents BtnClassic As Button
    Friend WithEvents TBGain As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblRadiusFactor As Skye.UI.Label
    Friend WithEvents LblLineWidth As Skye.UI.Label
    Friend WithEvents LblSmoothing As Skye.UI.Label
    Friend WithEvents LblGain As Skye.UI.Label
    Friend WithEvents LblWeightingMode As Skye.UI.Label
    Friend WithEvents TipCircularSpectrum As Skye.UI.ToolTip
    Friend WithEvents ChkBoxAllowMiniMode As CheckBox

End Class
