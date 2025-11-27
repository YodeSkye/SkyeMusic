<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsFractalJulia
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
        BtnBassquake = New Button()
        BtnCrystalGrid = New Button()
        BtnFirestorm = New Button()
        BtnCoralReef = New Button()
        BtnGalaxyBloom = New Button()
        BtnTreblePulse = New Button()
        LblPresets = New Skye.UI.Label()
        BtnCalmOcean = New Button()
        TBBassInfluence = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 250)
        TBBaseCX = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 200)
        LblBassInfluence = New Skye.UI.Label()
        LblBaseCX = New Skye.UI.Label()
        TBBaseCY = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 200)
        LblBaseCY = New Skye.UI.Label()
        TBMidInfluence = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 250)
        LblMidInfluence = New Skye.UI.Label()
        TBMaxIterations = New Syncfusion.Windows.Forms.Tools.TrackBarEx(10, 250)
        LblMaxIterations = New Skye.UI.Label()
        TipFractalJulia = New Skye.UI.ToolTip(components)
        SuspendLayout()
        ' 
        ' BtnBassquake
        ' 
        BtnBassquake.Location = New Point(4, 158)
        BtnBassquake.Name = "BtnBassquake"
        BtnBassquake.Size = New Size(132, 32)
        BtnBassquake.TabIndex = 169
        BtnBassquake.Text = "Bassquake"
        TipFractalJulia.SetToolTipImage(BtnBassquake, Nothing)
        BtnBassquake.UseVisualStyleBackColor = True
        ' 
        ' BtnCrystalGrid
        ' 
        BtnCrystalGrid.Location = New Point(4, 126)
        BtnCrystalGrid.Name = "BtnCrystalGrid"
        BtnCrystalGrid.Size = New Size(132, 32)
        BtnCrystalGrid.TabIndex = 168
        BtnCrystalGrid.Text = "Crystal Grid"
        TipFractalJulia.SetToolTipImage(BtnCrystalGrid, Nothing)
        BtnCrystalGrid.UseVisualStyleBackColor = True
        ' 
        ' BtnFirestorm
        ' 
        BtnFirestorm.Location = New Point(4, 94)
        BtnFirestorm.Name = "BtnFirestorm"
        BtnFirestorm.Size = New Size(132, 32)
        BtnFirestorm.TabIndex = 164
        BtnFirestorm.Text = "Firestorm"
        TipFractalJulia.SetToolTipImage(BtnFirestorm, Nothing)
        BtnFirestorm.UseVisualStyleBackColor = True
        ' 
        ' BtnCoralReef
        ' 
        BtnCoralReef.Location = New Point(4, 254)
        BtnCoralReef.Name = "BtnCoralReef"
        BtnCoralReef.Size = New Size(132, 32)
        BtnCoralReef.TabIndex = 167
        BtnCoralReef.Text = "Coral Reef"
        TipFractalJulia.SetToolTipImage(BtnCoralReef, Nothing)
        BtnCoralReef.UseVisualStyleBackColor = True
        ' 
        ' BtnGalaxyBloom
        ' 
        BtnGalaxyBloom.Location = New Point(4, 222)
        BtnGalaxyBloom.Name = "BtnGalaxyBloom"
        BtnGalaxyBloom.Size = New Size(132, 32)
        BtnGalaxyBloom.TabIndex = 166
        BtnGalaxyBloom.Text = "Galaxy Bloom"
        TipFractalJulia.SetToolTipImage(BtnGalaxyBloom, Nothing)
        BtnGalaxyBloom.UseVisualStyleBackColor = True
        ' 
        ' BtnTreblePulse
        ' 
        BtnTreblePulse.Location = New Point(4, 190)
        BtnTreblePulse.Name = "BtnTreblePulse"
        BtnTreblePulse.Size = New Size(132, 32)
        BtnTreblePulse.TabIndex = 165
        BtnTreblePulse.Text = "Treble Pulse"
        TipFractalJulia.SetToolTipImage(BtnTreblePulse, Nothing)
        BtnTreblePulse.UseVisualStyleBackColor = True
        ' 
        ' LblPresets
        ' 
        LblPresets.Font = New Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        LblPresets.Location = New Point(4, 40)
        LblPresets.Name = "LblPresets"
        LblPresets.Size = New Size(132, 23)
        LblPresets.TabIndex = 162
        LblPresets.Text = " Presets "
        LblPresets.TextAlign = ContentAlignment.MiddleCenter
        TipFractalJulia.SetToolTipImage(LblPresets, Nothing)
        ' 
        ' BtnCalmOcean
        ' 
        BtnCalmOcean.Location = New Point(3, 62)
        BtnCalmOcean.Name = "BtnCalmOcean"
        BtnCalmOcean.Size = New Size(133, 32)
        BtnCalmOcean.TabIndex = 163
        BtnCalmOcean.Text = "Calm Ocean"
        TipFractalJulia.SetToolTipImage(BtnCalmOcean, Nothing)
        BtnCalmOcean.UseVisualStyleBackColor = True
        ' 
        ' TBBassInfluence
        ' 
        TBBassInfluence.BackColor = Color.Transparent
        TBBassInfluence.BeforeTouchSize = New Size(20, 290)
        TBBassInfluence.LargeChange = 25
        TBBassInfluence.Location = New Point(373, 31)
        TBBassInfluence.Margin = New Padding(4)
        TBBassInfluence.Name = "TBBassInfluence"
        TBBassInfluence.Orientation = Orientation.Vertical
        TBBassInfluence.ShowFocusRect = False
        TBBassInfluence.Size = New Size(20, 290)
        TBBassInfluence.TabIndex = 171
        TBBassInfluence.TabStop = False
        TBBassInfluence.TimerInterval = 100
        TipFractalJulia.SetToolTip(TBBassInfluence, "How much the low‑frequency audio band shifts the real part (cx)." & vbCrLf & "Strong bass makes the fractal " & ChrW(8220) & "wobble" & ChrW(8221) & " horizontally.")
        TipFractalJulia.SetToolTipImage(TBBassInfluence, Nothing)
        TBBassInfluence.Value = 10
        ' 
        ' TBBaseCX
        ' 
        TBBaseCX.BackColor = Color.Transparent
        TBBaseCX.BeforeTouchSize = New Size(20, 290)
        TBBaseCX.LargeChange = 10
        TBBaseCX.Location = New Point(285, 31)
        TBBaseCX.Margin = New Padding(4)
        TBBaseCX.Name = "TBBaseCX"
        TBBaseCX.Orientation = Orientation.Vertical
        TBBaseCX.ShowFocusRect = False
        TBBaseCX.Size = New Size(20, 290)
        TBBaseCX.TabIndex = 170
        TBBaseCX.TabStop = False
        TBBaseCX.TimerInterval = 100
        TipFractalJulia.SetToolTip(TBBaseCX, "The fixed real part of the Julia constant." & vbCrLf & "This anchors the fractal’s overall shape.")
        TipFractalJulia.SetToolTipImage(TBBaseCX, Nothing)
        TBBaseCX.Value = 10
        ' 
        ' LblBassInfluence
        ' 
        LblBassInfluence.Location = New Point(319, 10)
        LblBassInfluence.Name = "LblBassInfluence"
        LblBassInfluence.Size = New Size(128, 23)
        LblBassInfluence.TabIndex = 173
        LblBassInfluence.Text = "Bass Influence"
        LblBassInfluence.TextAlign = ContentAlignment.MiddleCenter
        TipFractalJulia.SetToolTipImage(LblBassInfluence, Nothing)
        ' 
        ' LblBaseCX
        ' 
        LblBaseCX.Location = New Point(260, 10)
        LblBaseCX.Name = "LblBaseCX"
        LblBaseCX.Size = New Size(71, 23)
        LblBaseCX.TabIndex = 172
        LblBaseCX.Text = "Base CX"
        LblBaseCX.TextAlign = ContentAlignment.MiddleCenter
        TipFractalJulia.SetToolTipImage(LblBaseCX, Nothing)
        ' 
        ' TBBaseCY
        ' 
        TBBaseCY.BackColor = Color.Transparent
        TBBaseCY.BeforeTouchSize = New Size(20, 290)
        TBBaseCY.LargeChange = 10
        TBBaseCY.Location = New Point(500, 31)
        TBBaseCY.Margin = New Padding(4)
        TBBaseCY.Name = "TBBaseCY"
        TBBaseCY.Orientation = Orientation.Vertical
        TBBaseCY.ShowFocusRect = False
        TBBaseCY.Size = New Size(20, 290)
        TBBaseCY.TabIndex = 174
        TBBaseCY.TabStop = False
        TBBaseCY.TimerInterval = 100
        TipFractalJulia.SetToolTip(TBBaseCY, "The fixed imaginary part of the Julia constant." & vbCrLf & "This sets the fractal’s vertical symmetry and complexity.")
        TipFractalJulia.SetToolTipImage(TBBaseCY, Nothing)
        TBBaseCY.Value = 10
        ' 
        ' LblBaseCY
        ' 
        LblBaseCY.Location = New Point(475, 10)
        LblBaseCY.Name = "LblBaseCY"
        LblBaseCY.Size = New Size(71, 23)
        LblBaseCY.TabIndex = 176
        LblBaseCY.Text = "Base CY"
        LblBaseCY.TextAlign = ContentAlignment.MiddleCenter
        TipFractalJulia.SetToolTipImage(LblBaseCY, Nothing)
        ' 
        ' TBMidInfluence
        ' 
        TBMidInfluence.BackColor = Color.Transparent
        TBMidInfluence.BeforeTouchSize = New Size(20, 290)
        TBMidInfluence.LargeChange = 25
        TBMidInfluence.Location = New Point(588, 31)
        TBMidInfluence.Margin = New Padding(4)
        TBMidInfluence.Name = "TBMidInfluence"
        TBMidInfluence.Orientation = Orientation.Vertical
        TBMidInfluence.ShowFocusRect = False
        TBMidInfluence.Size = New Size(20, 290)
        TBMidInfluence.TabIndex = 175
        TBMidInfluence.TabStop = False
        TBMidInfluence.TimerInterval = 100
        TipFractalJulia.SetToolTip(TBMidInfluence, "How much the mid‑frequency audio band shifts the imaginary part (cy)." & vbCrLf & "Strong mids make the fractal " & ChrW(8220) & "stretch" & ChrW(8221) & " vertically.")
        TipFractalJulia.SetToolTipImage(TBMidInfluence, Nothing)
        TBMidInfluence.Value = 10
        ' 
        ' LblMidInfluence
        ' 
        LblMidInfluence.Location = New Point(534, 10)
        LblMidInfluence.Name = "LblMidInfluence"
        LblMidInfluence.Size = New Size(128, 23)
        LblMidInfluence.TabIndex = 177
        LblMidInfluence.Text = "Mid Influence"
        LblMidInfluence.TextAlign = ContentAlignment.MiddleCenter
        TipFractalJulia.SetToolTipImage(LblMidInfluence, Nothing)
        ' 
        ' TBMaxIterations
        ' 
        TBMaxIterations.BackColor = Color.Transparent
        TBMaxIterations.BeforeTouchSize = New Size(20, 290)
        TBMaxIterations.LargeChange = 25
        TBMaxIterations.Location = New Point(724, 31)
        TBMaxIterations.Margin = New Padding(4)
        TBMaxIterations.Name = "TBMaxIterations"
        TBMaxIterations.Orientation = Orientation.Vertical
        TBMaxIterations.ShowFocusRect = False
        TBMaxIterations.Size = New Size(20, 290)
        TBMaxIterations.TabIndex = 178
        TBMaxIterations.TabStop = False
        TBMaxIterations.TimerInterval = 100
        TipFractalJulia.SetToolTip(TBMaxIterations, "Controls fractal detail: higher values = sharper, slower; lower values = simpler, faster.")
        TipFractalJulia.SetToolTipImage(TBMaxIterations, Nothing)
        TBMaxIterations.Value = 10
        ' 
        ' LblMaxIterations
        ' 
        LblMaxIterations.Location = New Point(670, 10)
        LblMaxIterations.Name = "LblMaxIterations"
        LblMaxIterations.Size = New Size(128, 23)
        LblMaxIterations.TabIndex = 179
        LblMaxIterations.Text = "Max Iterations"
        LblMaxIterations.TextAlign = ContentAlignment.MiddleCenter
        TipFractalJulia.SetToolTipImage(LblMaxIterations, Nothing)
        ' 
        ' TipFractalJulia
        ' 
        TipFractalJulia.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipFractalJulia.OwnerDraw = True
        ' 
        ' OptionsFractalJulia
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(TBMaxIterations)
        Controls.Add(LblMaxIterations)
        Controls.Add(TBBaseCY)
        Controls.Add(LblBaseCY)
        Controls.Add(TBMidInfluence)
        Controls.Add(TBBaseCX)
        Controls.Add(LblBaseCX)
        Controls.Add(BtnBassquake)
        Controls.Add(BtnCrystalGrid)
        Controls.Add(BtnFirestorm)
        Controls.Add(BtnCoralReef)
        Controls.Add(BtnGalaxyBloom)
        Controls.Add(BtnTreblePulse)
        Controls.Add(LblPresets)
        Controls.Add(BtnCalmOcean)
        Controls.Add(TBBassInfluence)
        Controls.Add(LblBassInfluence)
        Controls.Add(LblMidInfluence)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsFractalJulia"
        Size = New Size(800, 330)
        TipFractalJulia.SetToolTipImage(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnBassquake As Button
    Friend WithEvents BtnCrystalGrid As Button
    Friend WithEvents BtnFirestorm As Button
    Friend WithEvents BtnCoralReef As Button
    Friend WithEvents BtnGalaxyBloom As Button
    Friend WithEvents BtnTreblePulse As Button
    Friend WithEvents LblPresets As Skye.UI.Label
    Friend WithEvents BtnCalmOcean As Button
    Friend WithEvents TBBassInfluence As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBBaseCX As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblBassInfluence As Skye.UI.Label
    Friend WithEvents LblBaseCX As Skye.UI.Label
    Friend WithEvents TBBaseCY As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblBaseCY As Skye.UI.Label
    Friend WithEvents TBMidInfluence As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblMidInfluence As Skye.UI.Label
    Friend WithEvents TBMaxIterations As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblMaxIterations As Skye.UI.Label
    Friend WithEvents TipFractalJulia As Skye.UI.ToolTip

End Class
