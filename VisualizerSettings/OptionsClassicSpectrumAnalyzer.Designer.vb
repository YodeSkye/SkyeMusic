<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsClassicSpectrumAnalyzer
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
        LblPresets = New Skye.UI.Label()
        BtnClassicWinamp = New Button()
        TxtBoxBarCount = New TextBox()
        LblBarCount = New Skye.UI.Label()
        TBGain = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 99)
        LblGain = New Skye.UI.Label()
        ChkBoxShowPeaks = New CheckBox()
        BtnAmbientFlow = New Button()
        BtnTechnicalAnalyzer = New Button()
        BtnRetroArcade = New Button()
        BtnLiveDJ = New Button()
        TBSmoothing = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 95)
        LblSmoothing = New Skye.UI.Label()
        TBPeakDecay = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 10)
        LblPeakDecay = New Skye.UI.Label()
        TBPeakHoldFrames = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 60)
        LblPeakHoldFrames = New Skye.UI.Label()
        LblBandMappingMode = New Skye.UI.Label()
        CoBoxBandMappingMode = New ComboBox()
        TipClassicSpectrumAnalyzer = New Skye.UI.ToolTip(components)
        SuspendLayout()
        ' 
        ' LblPresets
        ' 
        LblPresets.Font = New Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        LblPresets.Location = New Point(14, 137)
        LblPresets.Name = "LblPresets"
        LblPresets.Size = New Size(161, 23)
        LblPresets.TabIndex = 34
        LblPresets.Text = " Presets "
        LblPresets.TextAlign = ContentAlignment.MiddleCenter
        TipClassicSpectrumAnalyzer.SetToolTipImage(LblPresets, Nothing)
        ' 
        ' BtnClassicWinamp
        ' 
        BtnClassicWinamp.Location = New Point(13, 159)
        BtnClassicWinamp.Name = "BtnClassicWinamp"
        BtnClassicWinamp.Size = New Size(162, 32)
        BtnClassicWinamp.TabIndex = 100
        BtnClassicWinamp.Text = "Classic Winamp"
        TipClassicSpectrumAnalyzer.SetToolTipImage(BtnClassicWinamp, Nothing)
        BtnClassicWinamp.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxBarCount
        ' 
        TxtBoxBarCount.Location = New Point(14, 28)
        TxtBoxBarCount.Margin = New Padding(4)
        TxtBoxBarCount.Name = "TxtBoxBarCount"
        TxtBoxBarCount.Size = New Size(86, 29)
        TxtBoxBarCount.TabIndex = 31
        TxtBoxBarCount.TextAlign = HorizontalAlignment.Center
        TipClassicSpectrumAnalyzer.SetToolTipImage(TxtBoxBarCount, Nothing)
        ' 
        ' LblBarCount
        ' 
        LblBarCount.Location = New Point(14, 3)
        LblBarCount.Margin = New Padding(4, 0, 4, 0)
        LblBarCount.Name = "LblBarCount"
        LblBarCount.Size = New Size(85, 32)
        LblBarCount.TabIndex = 30
        LblBarCount.Text = "Bar Count"
        LblBarCount.TextAlign = ContentAlignment.MiddleCenter
        TipClassicSpectrumAnalyzer.SetToolTipImage(LblBarCount, Nothing)
        ' 
        ' TBGain
        ' 
        TBGain.BackColor = Color.Transparent
        TBGain.BeforeTouchSize = New Size(20, 290)
        TBGain.LargeChange = 8
        TBGain.Location = New Point(388, 29)
        TBGain.Margin = New Padding(4)
        TBGain.Name = "TBGain"
        TBGain.Orientation = Orientation.Vertical
        TBGain.ShowFocusRect = False
        TBGain.Size = New Size(20, 290)
        TBGain.TabIndex = 1
        TBGain.TabStop = False
        TBGain.TimerInterval = 100
        TipClassicSpectrumAnalyzer.SetToolTip(TBGain, "Gain multiplier for audio data.")
        TipClassicSpectrumAnalyzer.SetToolTipImage(TBGain, Nothing)
        TBGain.Value = 10
        ' 
        ' LblGain
        ' 
        LblGain.Location = New Point(363, 8)
        LblGain.Name = "LblGain"
        LblGain.Size = New Size(71, 23)
        LblGain.TabIndex = 32
        LblGain.Text = "Gain"
        LblGain.TextAlign = ContentAlignment.MiddleCenter
        TipClassicSpectrumAnalyzer.SetToolTipImage(LblGain, Nothing)
        ' 
        ' ChkBoxShowPeaks
        ' 
        ChkBoxShowPeaks.AutoSize = True
        ChkBoxShowPeaks.BackColor = Color.Transparent
        ChkBoxShowPeaks.FlatStyle = FlatStyle.Flat
        ChkBoxShowPeaks.Location = New Point(111, 30)
        ChkBoxShowPeaks.Name = "ChkBoxShowPeaks"
        ChkBoxShowPeaks.Size = New Size(112, 25)
        ChkBoxShowPeaks.TabIndex = 35
        ChkBoxShowPeaks.Text = " Show Peaks"
        ChkBoxShowPeaks.TextAlign = ContentAlignment.MiddleCenter
        TipClassicSpectrumAnalyzer.SetToolTip(ChkBoxShowPeaks, "Show Peak Values on Graph.")
        TipClassicSpectrumAnalyzer.SetToolTipImage(ChkBoxShowPeaks, Nothing)
        ChkBoxShowPeaks.UseVisualStyleBackColor = False
        ' 
        ' BtnAmbientFlow
        ' 
        BtnAmbientFlow.Location = New Point(14, 223)
        BtnAmbientFlow.Name = "BtnAmbientFlow"
        BtnAmbientFlow.Size = New Size(161, 32)
        BtnAmbientFlow.TabIndex = 120
        BtnAmbientFlow.Text = "Ambient Flow"
        TipClassicSpectrumAnalyzer.SetToolTipImage(BtnAmbientFlow, Nothing)
        BtnAmbientFlow.UseVisualStyleBackColor = True
        ' 
        ' BtnTechnicalAnalyzer
        ' 
        BtnTechnicalAnalyzer.Location = New Point(14, 255)
        BtnTechnicalAnalyzer.Name = "BtnTechnicalAnalyzer"
        BtnTechnicalAnalyzer.Size = New Size(161, 32)
        BtnTechnicalAnalyzer.TabIndex = 130
        BtnTechnicalAnalyzer.Text = "Technical Analyzer"
        TipClassicSpectrumAnalyzer.SetToolTipImage(BtnTechnicalAnalyzer, Nothing)
        BtnTechnicalAnalyzer.UseVisualStyleBackColor = True
        ' 
        ' BtnRetroArcade
        ' 
        BtnRetroArcade.Location = New Point(14, 287)
        BtnRetroArcade.Name = "BtnRetroArcade"
        BtnRetroArcade.Size = New Size(161, 32)
        BtnRetroArcade.TabIndex = 140
        BtnRetroArcade.Text = "Retro Arcade"
        TipClassicSpectrumAnalyzer.SetToolTipImage(BtnRetroArcade, Nothing)
        BtnRetroArcade.UseVisualStyleBackColor = True
        ' 
        ' BtnLiveDJ
        ' 
        BtnLiveDJ.Location = New Point(14, 191)
        BtnLiveDJ.Name = "BtnLiveDJ"
        BtnLiveDJ.Size = New Size(161, 32)
        BtnLiveDJ.TabIndex = 110
        BtnLiveDJ.Text = "Live DJ"
        TipClassicSpectrumAnalyzer.SetToolTipImage(BtnLiveDJ, Nothing)
        BtnLiveDJ.UseVisualStyleBackColor = True
        ' 
        ' TBSmoothing
        ' 
        TBSmoothing.BackColor = Color.Transparent
        TBSmoothing.BeforeTouchSize = New Size(20, 290)
        TBSmoothing.LargeChange = 10
        TBSmoothing.Location = New Point(475, 29)
        TBSmoothing.Margin = New Padding(4)
        TBSmoothing.Name = "TBSmoothing"
        TBSmoothing.Orientation = Orientation.Vertical
        TBSmoothing.ShowFocusRect = False
        TBSmoothing.Size = New Size(20, 290)
        TBSmoothing.TabIndex = 2
        TBSmoothing.TabStop = False
        TBSmoothing.TimerInterval = 100
        TipClassicSpectrumAnalyzer.SetToolTip(TBSmoothing, "Weight for previous frame vs new frame." & vbCrLf & "0 = instant response, 0.95 = very sluggish.")
        TipClassicSpectrumAnalyzer.SetToolTipImage(TBSmoothing, Nothing)
        TBSmoothing.Value = 10
        ' 
        ' LblSmoothing
        ' 
        LblSmoothing.Location = New Point(432, 8)
        LblSmoothing.Name = "LblSmoothing"
        LblSmoothing.Size = New Size(106, 23)
        LblSmoothing.TabIndex = 41
        LblSmoothing.Text = "Smoothing"
        LblSmoothing.TextAlign = ContentAlignment.MiddleCenter
        TipClassicSpectrumAnalyzer.SetToolTipImage(LblSmoothing, Nothing)
        ' 
        ' TBPeakDecay
        ' 
        TBPeakDecay.BackColor = Color.Transparent
        TBPeakDecay.BeforeTouchSize = New Size(20, 290)
        TBPeakDecay.LargeChange = 3
        TBPeakDecay.Location = New Point(584, 29)
        TBPeakDecay.Margin = New Padding(4)
        TBPeakDecay.Name = "TBPeakDecay"
        TBPeakDecay.Orientation = Orientation.Vertical
        TBPeakDecay.ShowFocusRect = False
        TBPeakDecay.Size = New Size(20, 290)
        TBPeakDecay.TabIndex = 3
        TBPeakDecay.TabStop = False
        TBPeakDecay.TimerInterval = 100
        TipClassicSpectrumAnalyzer.SetToolTip(TBPeakDecay, "Pixels per frame.")
        TipClassicSpectrumAnalyzer.SetToolTipImage(TBPeakDecay, Nothing)
        TBPeakDecay.Value = 10
        ' 
        ' LblPeakDecay
        ' 
        LblPeakDecay.Location = New Point(528, 8)
        LblPeakDecay.Name = "LblPeakDecay"
        LblPeakDecay.Size = New Size(132, 23)
        LblPeakDecay.TabIndex = 43
        LblPeakDecay.Text = "Peak Decay"
        LblPeakDecay.TextAlign = ContentAlignment.MiddleCenter
        TipClassicSpectrumAnalyzer.SetToolTipImage(LblPeakDecay, Nothing)
        ' 
        ' TBPeakHoldFrames
        ' 
        TBPeakHoldFrames.BackColor = Color.Transparent
        TBPeakHoldFrames.BeforeTouchSize = New Size(20, 290)
        TBPeakHoldFrames.LargeChange = 10
        TBPeakHoldFrames.Location = New Point(712, 29)
        TBPeakHoldFrames.Margin = New Padding(4)
        TBPeakHoldFrames.Name = "TBPeakHoldFrames"
        TBPeakHoldFrames.Orientation = Orientation.Vertical
        TBPeakHoldFrames.ShowFocusRect = False
        TBPeakHoldFrames.Size = New Size(20, 290)
        TBPeakHoldFrames.TabIndex = 4
        TBPeakHoldFrames.TabStop = False
        TBPeakHoldFrames.TimerInterval = 100
        TipClassicSpectrumAnalyzer.SetToolTip(TBPeakHoldFrames, "How long peaks " & ChrW(8220) & "stick" & ChrW(8221) & " before decaying." & vbCrLf & "At 30 FPS, 30 = ~1 second.")
        TipClassicSpectrumAnalyzer.SetToolTipImage(TBPeakHoldFrames, Nothing)
        TBPeakHoldFrames.Value = 10
        ' 
        ' LblPeakHoldFrames
        ' 
        LblPeakHoldFrames.Location = New Point(647, 8)
        LblPeakHoldFrames.Name = "LblPeakHoldFrames"
        LblPeakHoldFrames.Size = New Size(150, 23)
        LblPeakHoldFrames.TabIndex = 45
        LblPeakHoldFrames.Text = "Peak Hold Frames"
        LblPeakHoldFrames.TextAlign = ContentAlignment.MiddleCenter
        TipClassicSpectrumAnalyzer.SetToolTipImage(LblPeakHoldFrames, Nothing)
        ' 
        ' LblBandMappingMode
        ' 
        LblBandMappingMode.Location = New Point(13, 60)
        LblBandMappingMode.Name = "LblBandMappingMode"
        LblBandMappingMode.Size = New Size(162, 23)
        LblBandMappingMode.TabIndex = 47
        LblBandMappingMode.Text = "Band Mapping Mode"
        TipClassicSpectrumAnalyzer.SetToolTipImage(LblBandMappingMode, Nothing)
        ' 
        ' CoBoxBandMappingMode
        ' 
        CoBoxBandMappingMode.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxBandMappingMode.FlatStyle = FlatStyle.Flat
        CoBoxBandMappingMode.FormattingEnabled = True
        CoBoxBandMappingMode.Location = New Point(14, 83)
        CoBoxBandMappingMode.Name = "CoBoxBandMappingMode"
        CoBoxBandMappingMode.Size = New Size(249, 29)
        CoBoxBandMappingMode.TabIndex = 46
        TipClassicSpectrumAnalyzer.SetToolTip(CoBoxBandMappingMode, "Method used for mapping audio data to bars.")
        TipClassicSpectrumAnalyzer.SetToolTipImage(CoBoxBandMappingMode, Nothing)
        ' 
        ' TipClassicSpectrumAnalyzer
        ' 
        TipClassicSpectrumAnalyzer.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipClassicSpectrumAnalyzer.OwnerDraw = True
        ' 
        ' OptionsClassicSpectrumAnalyzer
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(CoBoxBandMappingMode)
        Controls.Add(TBPeakHoldFrames)
        Controls.Add(TBPeakDecay)
        Controls.Add(TBSmoothing)
        Controls.Add(BtnLiveDJ)
        Controls.Add(BtnRetroArcade)
        Controls.Add(BtnTechnicalAnalyzer)
        Controls.Add(BtnAmbientFlow)
        Controls.Add(ChkBoxShowPeaks)
        Controls.Add(LblPresets)
        Controls.Add(BtnClassicWinamp)
        Controls.Add(TxtBoxBarCount)
        Controls.Add(LblBarCount)
        Controls.Add(TBGain)
        Controls.Add(LblPeakHoldFrames)
        Controls.Add(LblPeakDecay)
        Controls.Add(LblSmoothing)
        Controls.Add(LblGain)
        Controls.Add(LblBandMappingMode)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsClassicSpectrumAnalyzer"
        Size = New Size(800, 330)
        TipClassicSpectrumAnalyzer.SetToolTipImage(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblPresets As Skye.UI.Label
    Friend WithEvents BtnClassicWinamp As Button
    Friend WithEvents TxtBoxBarCount As TextBox
    Friend WithEvents LblBarCount As Skye.UI.Label
    Friend WithEvents TBGain As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblGain As Skye.UI.Label
    Friend WithEvents ChkBoxShowPeaks As CheckBox
    Friend WithEvents BtnAmbientFlow As Button
    Friend WithEvents BtnTechnicalAnalyzer As Button
    Friend WithEvents BtnRetroArcade As Button
    Friend WithEvents BtnLiveDJ As Button
    Friend WithEvents TBSmoothing As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblSmoothing As Skye.UI.Label
    Friend WithEvents TBPeakDecay As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblPeakDecay As Skye.UI.Label
    Friend WithEvents TBPeakHoldFrames As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblPeakHoldFrames As Skye.UI.Label
    Friend WithEvents LblBandMappingMode As Skye.UI.Label
    Friend WithEvents CoBoxBandMappingMode As ComboBox
    Friend WithEvents TipClassicSpectrumAnalyzer As Skye.UI.ToolTip

End Class
