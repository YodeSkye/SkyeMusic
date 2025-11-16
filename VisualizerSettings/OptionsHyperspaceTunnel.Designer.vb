<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsHyperspaceTunnel
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
        LblParticleSpeedAudioFactor = New Skye.UI.Label()
        TBParticleSpeedAudioFactor = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 200)
        LblParticleSpeedBase = New Skye.UI.Label()
        LblSwirlSpeedAudioFactor = New Skye.UI.Label()
        TBParticleSpeedBase = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 20)
        TBSwirlSpeedAudioFactor = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 20)
        TxtBoxParticleCount = New TextBox()
        LblParticleCount = New Skye.UI.Label()
        TBSwirlSpeedBase = New Syncfusion.Windows.Forms.Tools.TrackBarEx(10, 1000)
        LblSwirlSpeedBase = New Skye.UI.Label()
        TipHyperspaceTunnel = New Skye.UI.ToolTip(components)
        SuspendLayout()
        ' 
        ' LblParticleSpeedAudioFactor
        ' 
        LblParticleSpeedAudioFactor.Location = New Point(598, 12)
        LblParticleSpeedAudioFactor.Name = "LblParticleSpeedAudioFactor"
        LblParticleSpeedAudioFactor.Size = New Size(199, 23)
        LblParticleSpeedAudioFactor.TabIndex = 20
        LblParticleSpeedAudioFactor.Text = "Particle Speed Audio Factor"
        LblParticleSpeedAudioFactor.TextAlign = ContentAlignment.MiddleCenter
        TipHyperspaceTunnel.SetToolTipImage(LblParticleSpeedAudioFactor, Nothing)
        ' 
        ' TBParticleSpeedAudioFactor
        ' 
        TBParticleSpeedAudioFactor.BackColor = Color.Transparent
        TBParticleSpeedAudioFactor.BeforeTouchSize = New Size(20, 290)
        TBParticleSpeedAudioFactor.Location = New Point(687, 32)
        TBParticleSpeedAudioFactor.Name = "TBParticleSpeedAudioFactor"
        TBParticleSpeedAudioFactor.Orientation = Orientation.Vertical
        TBParticleSpeedAudioFactor.Size = New Size(20, 290)
        TBParticleSpeedAudioFactor.SmallChange = 5
        TBParticleSpeedAudioFactor.TabIndex = 19
        TBParticleSpeedAudioFactor.Text = "TrackBarEx2"
        TBParticleSpeedAudioFactor.TimerInterval = 100
        TipHyperspaceTunnel.SetToolTipImage(TBParticleSpeedAudioFactor, Nothing)
        TBParticleSpeedAudioFactor.Value = 5
        ' 
        ' LblParticleSpeedBase
        ' 
        LblParticleSpeedBase.Location = New Point(445, 11)
        LblParticleSpeedBase.Name = "LblParticleSpeedBase"
        LblParticleSpeedBase.Size = New Size(147, 23)
        LblParticleSpeedBase.TabIndex = 18
        LblParticleSpeedBase.Text = "Particle Speed Base"
        LblParticleSpeedBase.TextAlign = ContentAlignment.MiddleCenter
        TipHyperspaceTunnel.SetToolTipImage(LblParticleSpeedBase, Nothing)
        ' 
        ' LblSwirlSpeedAudioFactor
        ' 
        LblSwirlSpeedAudioFactor.Location = New Point(256, 11)
        LblSwirlSpeedAudioFactor.Name = "LblSwirlSpeedAudioFactor"
        LblSwirlSpeedAudioFactor.Size = New Size(186, 23)
        LblSwirlSpeedAudioFactor.TabIndex = 17
        LblSwirlSpeedAudioFactor.Text = "Swirl Speed Audio Factor"
        LblSwirlSpeedAudioFactor.TextAlign = ContentAlignment.MiddleCenter
        TipHyperspaceTunnel.SetToolTipImage(LblSwirlSpeedAudioFactor, Nothing)
        ' 
        ' TBParticleSpeedBase
        ' 
        TBParticleSpeedBase.BackColor = Color.Transparent
        TBParticleSpeedBase.BeforeTouchSize = New Size(20, 290)
        TBParticleSpeedBase.Location = New Point(508, 35)
        TBParticleSpeedBase.Name = "TBParticleSpeedBase"
        TBParticleSpeedBase.Orientation = Orientation.Vertical
        TBParticleSpeedBase.Size = New Size(20, 290)
        TBParticleSpeedBase.TabIndex = 16
        TBParticleSpeedBase.Text = "TrackBarEx2"
        TBParticleSpeedBase.TimerInterval = 100
        TipHyperspaceTunnel.SetToolTipImage(TBParticleSpeedBase, Nothing)
        TBParticleSpeedBase.Value = 5
        ' 
        ' TBSwirlSpeedAudioFactor
        ' 
        TBSwirlSpeedAudioFactor.BackColor = Color.Transparent
        TBSwirlSpeedAudioFactor.BeforeTouchSize = New Size(20, 290)
        TBSwirlSpeedAudioFactor.Location = New Point(339, 32)
        TBSwirlSpeedAudioFactor.Name = "TBSwirlSpeedAudioFactor"
        TBSwirlSpeedAudioFactor.Orientation = Orientation.Vertical
        TBSwirlSpeedAudioFactor.Size = New Size(20, 290)
        TBSwirlSpeedAudioFactor.TabIndex = 15
        TBSwirlSpeedAudioFactor.Text = "TrackBarEx1"
        TBSwirlSpeedAudioFactor.TimerInterval = 100
        TipHyperspaceTunnel.SetToolTipImage(TBSwirlSpeedAudioFactor, Nothing)
        TBSwirlSpeedAudioFactor.Value = 5
        ' 
        ' TxtBoxParticleCount
        ' 
        TxtBoxParticleCount.Location = New Point(13, 165)
        TxtBoxParticleCount.Margin = New Padding(4)
        TxtBoxParticleCount.Name = "TxtBoxParticleCount"
        TxtBoxParticleCount.Size = New Size(109, 29)
        TxtBoxParticleCount.TabIndex = 13
        TxtBoxParticleCount.TextAlign = HorizontalAlignment.Center
        TipHyperspaceTunnel.SetToolTipImage(TxtBoxParticleCount, Nothing)
        ' 
        ' LblParticleCount
        ' 
        LblParticleCount.Location = New Point(14, 136)
        LblParticleCount.Margin = New Padding(4, 0, 4, 0)
        LblParticleCount.Name = "LblParticleCount"
        LblParticleCount.Size = New Size(109, 32)
        LblParticleCount.TabIndex = 12
        LblParticleCount.Text = "Particle Count"
        LblParticleCount.TextAlign = ContentAlignment.MiddleCenter
        TipHyperspaceTunnel.SetToolTipImage(LblParticleCount, Nothing)
        ' 
        ' TBSwirlSpeedBase
        ' 
        TBSwirlSpeedBase.BackColor = Color.Transparent
        TBSwirlSpeedBase.BeforeTouchSize = New Size(20, 290)
        TBSwirlSpeedBase.Location = New Point(176, 32)
        TBSwirlSpeedBase.Margin = New Padding(4)
        TBSwirlSpeedBase.Name = "TBSwirlSpeedBase"
        TBSwirlSpeedBase.Orientation = Orientation.Vertical
        TBSwirlSpeedBase.Size = New Size(20, 290)
        TBSwirlSpeedBase.SmallChange = 10
        TBSwirlSpeedBase.TabIndex = 11
        TBSwirlSpeedBase.TimerInterval = 100
        TipHyperspaceTunnel.SetToolTipImage(TBSwirlSpeedBase, Nothing)
        TBSwirlSpeedBase.Value = 10
        ' 
        ' LblSwirlSpeedBase
        ' 
        LblSwirlSpeedBase.Location = New Point(120, 11)
        LblSwirlSpeedBase.Name = "LblSwirlSpeedBase"
        LblSwirlSpeedBase.Size = New Size(132, 23)
        LblSwirlSpeedBase.TabIndex = 14
        LblSwirlSpeedBase.Text = "Swirl Speed Base"
        LblSwirlSpeedBase.TextAlign = ContentAlignment.MiddleCenter
        TipHyperspaceTunnel.SetToolTipImage(LblSwirlSpeedBase, Nothing)
        ' 
        ' TipHyperspaceTunnel
        ' 
        TipHyperspaceTunnel.Font = New Font("Segoe UI", 10F)
        TipHyperspaceTunnel.OwnerDraw = True
        ' 
        ' OptionsHyperspaceTunnel
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(LblParticleSpeedAudioFactor)
        Controls.Add(TBParticleSpeedAudioFactor)
        Controls.Add(LblParticleSpeedBase)
        Controls.Add(LblSwirlSpeedAudioFactor)
        Controls.Add(TBParticleSpeedBase)
        Controls.Add(TBSwirlSpeedAudioFactor)
        Controls.Add(TxtBoxParticleCount)
        Controls.Add(LblParticleCount)
        Controls.Add(TBSwirlSpeedBase)
        Controls.Add(LblSwirlSpeedBase)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsHyperspaceTunnel"
        Size = New Size(800, 330)
        TipHyperspaceTunnel.SetToolTipImage(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblParticleSpeedAudioFactor As Skye.UI.Label
    Friend WithEvents TBParticleSpeedAudioFactor As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblParticleSpeedBase As Skye.UI.Label
    Friend WithEvents LblSwirlSpeedAudioFactor As Skye.UI.Label
    Friend WithEvents TBParticleSpeedBase As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBSwirlSpeedAudioFactor As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TxtBoxParticleCount As TextBox
    Friend WithEvents LblParticleCount As Skye.UI.Label
    Friend WithEvents TBSwirlSpeedBase As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblSwirlSpeedBase As Skye.UI.Label
    Friend WithEvents TipHyperspaceTunnel As Skye.UI.ToolTip

End Class
