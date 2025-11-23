<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsParticleNebula
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
        CoBoxActivePalettePreset = New ComboBox()
        BtnDefaults = New Button()
        ChkBoxRainbowColors = New CheckBox()
        TBSpawnMultiplier = New Syncfusion.Windows.Forms.Tools.TrackBarEx(5, 100)
        LblSpawnMultiplier = New Skye.UI.Label()
        LblActivePalettePreset = New Skye.UI.Label()
        ChkBoxShowTrails = New CheckBox()
        ChkBoxFadeTrails = New CheckBox()
        ChkBoxShowBloom = New CheckBox()
        TBVelocityScale = New Syncfusion.Windows.Forms.Tools.TrackBarEx(10, 200)
        LblVelocityScale = New Skye.UI.Label()
        TBFadeRate = New Syncfusion.Windows.Forms.Tools.TrackBarEx(10, 200)
        TBSizeScale = New Syncfusion.Windows.Forms.Tools.TrackBarEx(5, 50)
        LblSizeScale = New Skye.UI.Label()
        LblFadeRate = New Skye.UI.Label()
        TBSwirlBias = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 200)
        TBSwirlStrength = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 50)
        LblSwirlStrength = New Skye.UI.Label()
        LblSwirlBias = New Skye.UI.Label()
        TBBloomRadius = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 5)
        TBBloomIntensity = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 20)
        LblBloomIntensity = New Skye.UI.Label()
        LblBloomRadius = New Skye.UI.Label()
        TBTrailAlpha = New Syncfusion.Windows.Forms.Tools.TrackBarEx(10, 100)
        LblTrailAlpha = New Skye.UI.Label()
        TipParticleNebula = New Skye.UI.ToolTip(components)
        SuspendLayout()
        ' 
        ' CoBoxActivePalettePreset
        ' 
        CoBoxActivePalettePreset.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxActivePalettePreset.FlatStyle = FlatStyle.Flat
        CoBoxActivePalettePreset.Location = New Point(8, 29)
        CoBoxActivePalettePreset.Name = "CoBoxActivePalettePreset"
        CoBoxActivePalettePreset.Size = New Size(200, 29)
        CoBoxActivePalettePreset.TabIndex = 10
        TipParticleNebula.SetToolTip(CoBoxActivePalettePreset, "Selected Color Palette.")
        TipParticleNebula.SetToolTipImage(CoBoxActivePalettePreset, Nothing)
        ' 
        ' BtnDefaults
        ' 
        BtnDefaults.Location = New Point(8, 291)
        BtnDefaults.Name = "BtnDefaults"
        BtnDefaults.Size = New Size(90, 32)
        BtnDefaults.TabIndex = 60
        BtnDefaults.Text = "Defaults"
        TipParticleNebula.SetToolTip(BtnDefaults, "Reset to Defaults")
        TipParticleNebula.SetToolTipImage(BtnDefaults, Nothing)
        BtnDefaults.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxRainbowColors
        ' 
        ChkBoxRainbowColors.AutoSize = True
        ChkBoxRainbowColors.BackColor = Color.Transparent
        ChkBoxRainbowColors.FlatStyle = FlatStyle.Flat
        ChkBoxRainbowColors.Location = New Point(8, 64)
        ChkBoxRainbowColors.Name = "ChkBoxRainbowColors"
        ChkBoxRainbowColors.Size = New Size(136, 25)
        ChkBoxRainbowColors.TabIndex = 20
        ChkBoxRainbowColors.Text = "Rainbow Colors"
        ChkBoxRainbowColors.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTip(ChkBoxRainbowColors, "Use Rainbow Coloring Instead of Palette.")
        TipParticleNebula.SetToolTipImage(ChkBoxRainbowColors, Nothing)
        ChkBoxRainbowColors.UseVisualStyleBackColor = False
        ' 
        ' TBSpawnMultiplier
        ' 
        TBSpawnMultiplier.BackColor = Color.Transparent
        TBSpawnMultiplier.BeforeTouchSize = New Size(20, 280)
        TBSpawnMultiplier.LargeChange = 10
        TBSpawnMultiplier.Location = New Point(261, 46)
        TBSpawnMultiplier.Margin = New Padding(4)
        TBSpawnMultiplier.Name = "TBSpawnMultiplier"
        TBSpawnMultiplier.Orientation = Orientation.Vertical
        TBSpawnMultiplier.ShowFocusRect = False
        TBSpawnMultiplier.Size = New Size(20, 280)
        TBSpawnMultiplier.TabIndex = 179
        TBSpawnMultiplier.TabStop = False
        TBSpawnMultiplier.TimerInterval = 100
        TipParticleNebula.SetToolTip(TBSpawnMultiplier, "Nebula Density. Higher = More Particles.")
        TipParticleNebula.SetToolTipImage(TBSpawnMultiplier, Nothing)
        TBSpawnMultiplier.Value = 10
        ' 
        ' LblSpawnMultiplier
        ' 
        LblSpawnMultiplier.Location = New Point(230, -3)
        LblSpawnMultiplier.Name = "LblSpawnMultiplier"
        LblSpawnMultiplier.Size = New Size(83, 56)
        LblSpawnMultiplier.TabIndex = 180
        LblSpawnMultiplier.Text = "Spawn" & vbCrLf & "Multiplier"
        LblSpawnMultiplier.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTipImage(LblSpawnMultiplier, Nothing)
        ' 
        ' LblActivePalettePreset
        ' 
        LblActivePalettePreset.Location = New Point(7, 6)
        LblActivePalettePreset.Name = "LblActivePalettePreset"
        LblActivePalettePreset.Size = New Size(162, 23)
        LblActivePalettePreset.TabIndex = 183
        LblActivePalettePreset.Text = "Palette"
        TipParticleNebula.SetToolTipImage(LblActivePalettePreset, Nothing)
        ' 
        ' ChkBoxShowTrails
        ' 
        ChkBoxShowTrails.AutoSize = True
        ChkBoxShowTrails.BackColor = Color.Transparent
        ChkBoxShowTrails.FlatStyle = FlatStyle.Flat
        ChkBoxShowTrails.Location = New Point(8, 109)
        ChkBoxShowTrails.Name = "ChkBoxShowTrails"
        ChkBoxShowTrails.Size = New Size(105, 25)
        ChkBoxShowTrails.TabIndex = 30
        ChkBoxShowTrails.Text = "Show Trails"
        ChkBoxShowTrails.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTip(ChkBoxShowTrails, "Whether to Draw Particle Trails.")
        TipParticleNebula.SetToolTipImage(ChkBoxShowTrails, Nothing)
        ChkBoxShowTrails.UseVisualStyleBackColor = False
        ' 
        ' ChkBoxFadeTrails
        ' 
        ChkBoxFadeTrails.AutoSize = True
        ChkBoxFadeTrails.BackColor = Color.Transparent
        ChkBoxFadeTrails.FlatStyle = FlatStyle.Flat
        ChkBoxFadeTrails.Location = New Point(8, 140)
        ChkBoxFadeTrails.Name = "ChkBoxFadeTrails"
        ChkBoxFadeTrails.Size = New Size(98, 25)
        ChkBoxFadeTrails.TabIndex = 40
        ChkBoxFadeTrails.Text = "Fade Trails"
        ChkBoxFadeTrails.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTip(ChkBoxFadeTrails, "Whether Trail Segments Fade Out.")
        TipParticleNebula.SetToolTipImage(ChkBoxFadeTrails, Nothing)
        ChkBoxFadeTrails.UseVisualStyleBackColor = False
        ' 
        ' ChkBoxShowBloom
        ' 
        ChkBoxShowBloom.AutoSize = True
        ChkBoxShowBloom.BackColor = Color.Transparent
        ChkBoxShowBloom.FlatStyle = FlatStyle.Flat
        ChkBoxShowBloom.Location = New Point(8, 185)
        ChkBoxShowBloom.Name = "ChkBoxShowBloom"
        ChkBoxShowBloom.Size = New Size(114, 25)
        ChkBoxShowBloom.TabIndex = 50
        ChkBoxShowBloom.Text = "Show Bloom"
        ChkBoxShowBloom.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTip(ChkBoxShowBloom, "Whether to Draw Bloom Effect.")
        TipParticleNebula.SetToolTipImage(ChkBoxShowBloom, Nothing)
        ChkBoxShowBloom.UseVisualStyleBackColor = False
        ' 
        ' TBVelocityScale
        ' 
        TBVelocityScale.BackColor = Color.Transparent
        TBVelocityScale.BeforeTouchSize = New Size(20, 280)
        TBVelocityScale.LargeChange = 25
        TBVelocityScale.Location = New Point(328, 46)
        TBVelocityScale.Margin = New Padding(4)
        TBVelocityScale.Name = "TBVelocityScale"
        TBVelocityScale.Orientation = Orientation.Vertical
        TBVelocityScale.ShowFocusRect = False
        TBVelocityScale.Size = New Size(20, 280)
        TBVelocityScale.TabIndex = 188
        TBVelocityScale.TabStop = False
        TBVelocityScale.TimerInterval = 100
        TipParticleNebula.SetToolTip(TBVelocityScale, "Speed of Particle Movement.")
        TipParticleNebula.SetToolTipImage(TBVelocityScale, Nothing)
        TBVelocityScale.Value = 10
        ' 
        ' LblVelocityScale
        ' 
        LblVelocityScale.Location = New Point(308, -3)
        LblVelocityScale.Name = "LblVelocityScale"
        LblVelocityScale.Size = New Size(62, 56)
        LblVelocityScale.TabIndex = 189
        LblVelocityScale.Text = "Speed" & vbCrLf & "Scale"
        LblVelocityScale.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTipImage(LblVelocityScale, Nothing)
        ' 
        ' TBFadeRate
        ' 
        TBFadeRate.BackColor = Color.Transparent
        TBFadeRate.BeforeTouchSize = New Size(20, 280)
        TBFadeRate.LargeChange = 25
        TBFadeRate.Location = New Point(424, 46)
        TBFadeRate.Margin = New Padding(4)
        TBFadeRate.Name = "TBFadeRate"
        TBFadeRate.Orientation = Orientation.Vertical
        TBFadeRate.ShowFocusRect = False
        TBFadeRate.Size = New Size(20, 280)
        TBFadeRate.TabIndex = 192
        TBFadeRate.TabStop = False
        TBFadeRate.TimerInterval = 100
        TipParticleNebula.SetToolTip(TBFadeRate, "How Quickly Particles Fade Out.")
        TipParticleNebula.SetToolTipImage(TBFadeRate, Nothing)
        TBFadeRate.Value = 10
        ' 
        ' TBSizeScale
        ' 
        TBSizeScale.BackColor = Color.Transparent
        TBSizeScale.BeforeTouchSize = New Size(20, 280)
        TBSizeScale.Location = New Point(377, 46)
        TBSizeScale.Margin = New Padding(4)
        TBSizeScale.Name = "TBSizeScale"
        TBSizeScale.Orientation = Orientation.Vertical
        TBSizeScale.ShowFocusRect = False
        TBSizeScale.Size = New Size(20, 280)
        TBSizeScale.TabIndex = 190
        TBSizeScale.TabStop = False
        TBSizeScale.TimerInterval = 100
        TipParticleNebula.SetToolTip(TBSizeScale, "Size of Particles. Higher = Bigger Particles.")
        TipParticleNebula.SetToolTipImage(TBSizeScale, Nothing)
        TBSizeScale.Value = 10
        ' 
        ' LblSizeScale
        ' 
        LblSizeScale.Location = New Point(363, -3)
        LblSizeScale.Name = "LblSizeScale"
        LblSizeScale.Size = New Size(50, 56)
        LblSizeScale.TabIndex = 191
        LblSizeScale.Text = "Size" & vbCrLf & "Scale"
        LblSizeScale.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTipImage(LblSizeScale, Nothing)
        ' 
        ' LblFadeRate
        ' 
        LblFadeRate.Location = New Point(411, -3)
        LblFadeRate.Name = "LblFadeRate"
        LblFadeRate.Size = New Size(47, 56)
        LblFadeRate.TabIndex = 193
        LblFadeRate.Text = "Fade" & vbCrLf & "Rate"
        LblFadeRate.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTipImage(LblFadeRate, Nothing)
        ' 
        ' TBSwirlBias
        ' 
        TBSwirlBias.BackColor = Color.Transparent
        TBSwirlBias.BeforeTouchSize = New Size(20, 280)
        TBSwirlBias.LargeChange = 25
        TBSwirlBias.Location = New Point(539, 46)
        TBSwirlBias.Margin = New Padding(4)
        TBSwirlBias.Name = "TBSwirlBias"
        TBSwirlBias.Orientation = Orientation.Vertical
        TBSwirlBias.ShowFocusRect = False
        TBSwirlBias.Size = New Size(20, 280)
        TBSwirlBias.TabIndex = 196
        TBSwirlBias.TabStop = False
        TBSwirlBias.TimerInterval = 100
        TipParticleNebula.SetToolTip(TBSwirlBias, "Directional Bias. -1 = mostly CCW, 0 = Balanced, +1 = mostly CW")
        TipParticleNebula.SetToolTipImage(TBSwirlBias, Nothing)
        TBSwirlBias.Value = 10
        ' 
        ' TBSwirlStrength
        ' 
        TBSwirlStrength.BackColor = Color.Transparent
        TBSwirlStrength.BeforeTouchSize = New Size(20, 280)
        TBSwirlStrength.Location = New Point(479, 46)
        TBSwirlStrength.Margin = New Padding(4)
        TBSwirlStrength.Name = "TBSwirlStrength"
        TBSwirlStrength.Orientation = Orientation.Vertical
        TBSwirlStrength.ShowFocusRect = False
        TBSwirlStrength.Size = New Size(20, 280)
        TBSwirlStrength.TabIndex = 194
        TBSwirlStrength.TabStop = False
        TBSwirlStrength.TimerInterval = 100
        TipParticleNebula.SetToolTip(TBSwirlStrength, "How Strongly Particles Swirl.")
        TipParticleNebula.SetToolTipImage(TBSwirlStrength, Nothing)
        TBSwirlStrength.Value = 10
        ' 
        ' LblSwirlStrength
        ' 
        LblSwirlStrength.Location = New Point(455, -3)
        LblSwirlStrength.Name = "LblSwirlStrength"
        LblSwirlStrength.Size = New Size(69, 56)
        LblSwirlStrength.TabIndex = 195
        LblSwirlStrength.Text = "Swirl" & vbCrLf & "Strength"
        LblSwirlStrength.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTipImage(LblSwirlStrength, Nothing)
        ' 
        ' LblSwirlBias
        ' 
        LblSwirlBias.Location = New Point(526, -3)
        LblSwirlBias.Name = "LblSwirlBias"
        LblSwirlBias.Size = New Size(47, 56)
        LblSwirlBias.TabIndex = 197
        LblSwirlBias.Text = "Swirl" & vbCrLf & "Bias"
        LblSwirlBias.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTipImage(LblSwirlBias, Nothing)
        ' 
        ' TBBloomRadius
        ' 
        TBBloomRadius.BackColor = Color.Transparent
        TBBloomRadius.BeforeTouchSize = New Size(20, 280)
        TBBloomRadius.LargeChange = 1
        TBBloomRadius.Location = New Point(755, 46)
        TBBloomRadius.Margin = New Padding(4)
        TBBloomRadius.Name = "TBBloomRadius"
        TBBloomRadius.Orientation = Orientation.Vertical
        TBBloomRadius.ShowFocusRect = False
        TBBloomRadius.Size = New Size(20, 280)
        TBBloomRadius.TabIndex = 202
        TBBloomRadius.TabStop = False
        TBBloomRadius.TimerInterval = 100
        TipParticleNebula.SetToolTip(TBBloomRadius, "How Many Extra Bloom Rings to Draw.")
        TipParticleNebula.SetToolTipImage(TBBloomRadius, Nothing)
        TBBloomRadius.Value = 5
        ' 
        ' TBBloomIntensity
        ' 
        TBBloomIntensity.BackColor = Color.Transparent
        TBBloomIntensity.BeforeTouchSize = New Size(20, 280)
        TBBloomIntensity.LargeChange = 1
        TBBloomIntensity.Location = New Point(686, 46)
        TBBloomIntensity.Margin = New Padding(4)
        TBBloomIntensity.Name = "TBBloomIntensity"
        TBBloomIntensity.Orientation = Orientation.Vertical
        TBBloomIntensity.ShowFocusRect = False
        TBBloomIntensity.Size = New Size(20, 280)
        TBBloomIntensity.TabIndex = 200
        TBBloomIntensity.TabStop = False
        TBBloomIntensity.TimerInterval = 100
        TipParticleNebula.SetToolTip(TBBloomIntensity, "Bloom Brightness Multiplier.")
        TipParticleNebula.SetToolTipImage(TBBloomIntensity, Nothing)
        TBBloomIntensity.Value = 10
        ' 
        ' LblBloomIntensity
        ' 
        LblBloomIntensity.Location = New Point(662, -3)
        LblBloomIntensity.Name = "LblBloomIntensity"
        LblBloomIntensity.Size = New Size(69, 56)
        LblBloomIntensity.TabIndex = 201
        LblBloomIntensity.Text = "Bloom" & vbCrLf & "Intensity"
        LblBloomIntensity.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTipImage(LblBloomIntensity, Nothing)
        ' 
        ' LblBloomRadius
        ' 
        LblBloomRadius.Location = New Point(734, -3)
        LblBloomRadius.Name = "LblBloomRadius"
        LblBloomRadius.Size = New Size(63, 56)
        LblBloomRadius.TabIndex = 203
        LblBloomRadius.Text = "Bloom" & vbCrLf & "Radius"
        LblBloomRadius.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTipImage(LblBloomRadius, Nothing)
        ' 
        ' TBTrailAlpha
        ' 
        TBTrailAlpha.BackColor = Color.Transparent
        TBTrailAlpha.BeforeTouchSize = New Size(20, 280)
        TBTrailAlpha.LargeChange = 10
        TBTrailAlpha.Location = New Point(610, 46)
        TBTrailAlpha.Margin = New Padding(4)
        TBTrailAlpha.Name = "TBTrailAlpha"
        TBTrailAlpha.Orientation = Orientation.Vertical
        TBTrailAlpha.ShowFocusRect = False
        TBTrailAlpha.Size = New Size(20, 280)
        TBTrailAlpha.TabIndex = 198
        TBTrailAlpha.TabStop = False
        TBTrailAlpha.TimerInterval = 100
        TipParticleNebula.SetToolTip(TBTrailAlpha, "Brightness of Trail Segments.")
        TipParticleNebula.SetToolTipImage(TBTrailAlpha, Nothing)
        TBTrailAlpha.Value = 10
        ' 
        ' LblTrailAlpha
        ' 
        LblTrailAlpha.Location = New Point(592, -3)
        LblTrailAlpha.Name = "LblTrailAlpha"
        LblTrailAlpha.Size = New Size(57, 56)
        LblTrailAlpha.TabIndex = 199
        LblTrailAlpha.Text = "Trail" & vbCrLf & "Alpha"
        LblTrailAlpha.TextAlign = ContentAlignment.MiddleCenter
        TipParticleNebula.SetToolTipImage(LblTrailAlpha, Nothing)
        ' 
        ' TipParticleNebula
        ' 
        TipParticleNebula.Font = New Font("Segoe UI", 10F)
        TipParticleNebula.OwnerDraw = True
        ' 
        ' OptionsParticleNebula
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(TBBloomRadius)
        Controls.Add(TBBloomIntensity)
        Controls.Add(LblBloomIntensity)
        Controls.Add(LblBloomRadius)
        Controls.Add(TBTrailAlpha)
        Controls.Add(LblTrailAlpha)
        Controls.Add(TBSwirlBias)
        Controls.Add(TBSwirlStrength)
        Controls.Add(LblSwirlStrength)
        Controls.Add(LblSwirlBias)
        Controls.Add(TBFadeRate)
        Controls.Add(TBSizeScale)
        Controls.Add(LblSizeScale)
        Controls.Add(LblFadeRate)
        Controls.Add(TBVelocityScale)
        Controls.Add(ChkBoxShowBloom)
        Controls.Add(ChkBoxFadeTrails)
        Controls.Add(ChkBoxShowTrails)
        Controls.Add(CoBoxActivePalettePreset)
        Controls.Add(BtnDefaults)
        Controls.Add(ChkBoxRainbowColors)
        Controls.Add(TBSpawnMultiplier)
        Controls.Add(LblActivePalettePreset)
        Controls.Add(LblSpawnMultiplier)
        Controls.Add(LblVelocityScale)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsParticleNebula"
        Size = New Size(800, 330)
        TipParticleNebula.SetToolTipImage(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents CoBoxActivePalettePreset As ComboBox
    Friend WithEvents BtnDefaults As Button
    Friend WithEvents ChkBoxRainbowColors As CheckBox
    Friend WithEvents TBSpawnMultiplier As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblSpawnMultiplier As Skye.UI.Label
    Friend WithEvents LblActivePalettePreset As Skye.UI.Label
    Friend WithEvents ChkBoxShowTrails As CheckBox
    Friend WithEvents ChkBoxFadeTrails As CheckBox
    Friend WithEvents ChkBoxShowBloom As CheckBox
    Friend WithEvents TBVelocityScale As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblVelocityScale As Skye.UI.Label
    Friend WithEvents TBFadeRate As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBSizeScale As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblSizeScale As Skye.UI.Label
    Friend WithEvents LblFadeRate As Skye.UI.Label
    Friend WithEvents TBSwirlBias As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBSwirlStrength As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblSwirlStrength As Skye.UI.Label
    Friend WithEvents LblSwirlBias As Skye.UI.Label
    Friend WithEvents TBBloomRadius As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBBloomIntensity As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblBloomIntensity As Skye.UI.Label
    Friend WithEvents LblBloomRadius As Skye.UI.Label
    Friend WithEvents TBTrailAlpha As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblTrailAlpha As Skye.UI.Label
    Friend WithEvents TipParticleNebula As Skye.UI.ToolTip

End Class
