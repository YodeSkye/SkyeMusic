<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsFractalCloud
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
        LblSwirlSpeedAudioFactor = New Skye.UI.Label()
        TBTimeIncrement = New Syncfusion.Windows.Forms.Tools.TrackBarEx(5, 100)
        TBSwirlSpeedAudioFactor = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 30)
        TBSwirlSpeedBase = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 50)
        LblSwirlSpeedBase = New Skye.UI.Label()
        CoBoxPalette = New ComboBox()
        LblTimeIncrement = New Skye.UI.Label()
        LblPalette = New Skye.UI.Label()
        TipFractalCloud = New Skye.UI.ToolTip(components)
        ChkBoxAllowMiniMode = New CheckBox()
        SuspendLayout()
        ' 
        ' LblSwirlSpeedAudioFactor
        ' 
        LblSwirlSpeedAudioFactor.Location = New Point(445, 4)
        LblSwirlSpeedAudioFactor.Name = "LblSwirlSpeedAudioFactor"
        LblSwirlSpeedAudioFactor.Size = New Size(183, 23)
        LblSwirlSpeedAudioFactor.TabIndex = 13
        LblSwirlSpeedAudioFactor.Text = "Swirl Speed Audio Factor"
        LblSwirlSpeedAudioFactor.TextAlign = ContentAlignment.MiddleCenter
        TipFractalCloud.SetToolTipImage(LblSwirlSpeedAudioFactor, Nothing)
        ' 
        ' TBTimeIncrement
        ' 
        TBTimeIncrement.BackColor = Color.Transparent
        TBTimeIncrement.BeforeTouchSize = New Size(20, 290)
        TBTimeIncrement.Location = New Point(699, 28)
        TBTimeIncrement.Name = "TBTimeIncrement"
        TBTimeIncrement.Orientation = Orientation.Vertical
        TBTimeIncrement.ShowFocusRect = False
        TBTimeIncrement.Size = New Size(20, 290)
        TBTimeIncrement.TabIndex = 12
        TBTimeIncrement.Text = "TrackBarEx2"
        TBTimeIncrement.TimerInterval = 100
        TipFractalCloud.SetToolTip(TBTimeIncrement, "Animation Speed of the Fractal Cloud.")
        TipFractalCloud.SetToolTipImage(TBTimeIncrement, Nothing)
        TBTimeIncrement.Value = 5
        ' 
        ' TBSwirlSpeedAudioFactor
        ' 
        TBSwirlSpeedAudioFactor.BackColor = Color.Transparent
        TBSwirlSpeedAudioFactor.BeforeTouchSize = New Size(20, 290)
        TBSwirlSpeedAudioFactor.Location = New Point(526, 25)
        TBSwirlSpeedAudioFactor.Name = "TBSwirlSpeedAudioFactor"
        TBSwirlSpeedAudioFactor.Orientation = Orientation.Vertical
        TBSwirlSpeedAudioFactor.ShowFocusRect = False
        TBSwirlSpeedAudioFactor.Size = New Size(20, 290)
        TBSwirlSpeedAudioFactor.TabIndex = 11
        TBSwirlSpeedAudioFactor.Text = "TrackBarEx1"
        TBSwirlSpeedAudioFactor.TimerInterval = 100
        TipFractalCloud.SetToolTip(TBSwirlSpeedAudioFactor, "How Much Audio Affects the Swirl Speed.")
        TipFractalCloud.SetToolTipImage(TBSwirlSpeedAudioFactor, Nothing)
        TBSwirlSpeedAudioFactor.Value = 5
        ' 
        ' TBSwirlSpeedBase
        ' 
        TBSwirlSpeedBase.BackColor = Color.Transparent
        TBSwirlSpeedBase.BeforeTouchSize = New Size(20, 290)
        TBSwirlSpeedBase.Location = New Point(352, 25)
        TBSwirlSpeedBase.Margin = New Padding(4)
        TBSwirlSpeedBase.Name = "TBSwirlSpeedBase"
        TBSwirlSpeedBase.Orientation = Orientation.Vertical
        TBSwirlSpeedBase.ShowFocusRect = False
        TBSwirlSpeedBase.Size = New Size(20, 290)
        TBSwirlSpeedBase.TabIndex = 9
        TBSwirlSpeedBase.TimerInterval = 100
        TipFractalCloud.SetToolTip(TBSwirlSpeedBase, "Base Speed of the Swirl Rotation.")
        TipFractalCloud.SetToolTipImage(TBSwirlSpeedBase, Nothing)
        TBSwirlSpeedBase.Value = 10
        ' 
        ' LblSwirlSpeedBase
        ' 
        LblSwirlSpeedBase.Location = New Point(297, 4)
        LblSwirlSpeedBase.Name = "LblSwirlSpeedBase"
        LblSwirlSpeedBase.Size = New Size(131, 23)
        LblSwirlSpeedBase.TabIndex = 10
        LblSwirlSpeedBase.Text = "Swirl Speed Base"
        LblSwirlSpeedBase.TextAlign = ContentAlignment.MiddleCenter
        TipFractalCloud.SetToolTipImage(LblSwirlSpeedBase, Nothing)
        ' 
        ' CoBoxPalette
        ' 
        CoBoxPalette.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxPalette.FlatStyle = FlatStyle.Flat
        CoBoxPalette.FormattingEnabled = True
        CoBoxPalette.Location = New Point(13, 63)
        CoBoxPalette.Name = "CoBoxPalette"
        CoBoxPalette.Size = New Size(249, 29)
        CoBoxPalette.TabIndex = 15
        TipFractalCloud.SetToolTip(CoBoxPalette, "The Color Palette used for the Fractal Cloud.")
        TipFractalCloud.SetToolTipImage(CoBoxPalette, Nothing)
        ' 
        ' LblTimeIncrement
        ' 
        LblTimeIncrement.Location = New Point(645, 4)
        LblTimeIncrement.Name = "LblTimeIncrement"
        LblTimeIncrement.Size = New Size(129, 23)
        LblTimeIncrement.TabIndex = 16
        LblTimeIncrement.Text = "Time Increment"
        LblTimeIncrement.TextAlign = ContentAlignment.MiddleCenter
        TipFractalCloud.SetToolTipImage(LblTimeIncrement, Nothing)
        ' 
        ' LblPalette
        ' 
        LblPalette.Location = New Point(12, 37)
        LblPalette.Name = "LblPalette"
        LblPalette.Size = New Size(56, 23)
        LblPalette.TabIndex = 17
        LblPalette.Text = "Palette"
        TipFractalCloud.SetToolTipImage(LblPalette, Nothing)
        ' 
        ' TipFractalCloud
        ' 
        TipFractalCloud.Font = New Font("Segoe UI", 10F)
        TipFractalCloud.OwnerDraw = True
        ' 
        ' ChkBoxAllowMiniMode
        ' 
        ChkBoxAllowMiniMode.AutoSize = True
        ChkBoxAllowMiniMode.BackColor = Color.Transparent
        ChkBoxAllowMiniMode.FlatStyle = FlatStyle.Flat
        ChkBoxAllowMiniMode.Location = New Point(13, 108)
        ChkBoxAllowMiniMode.Name = "ChkBoxAllowMiniMode"
        ChkBoxAllowMiniMode.Size = New Size(140, 25)
        ChkBoxAllowMiniMode.TabIndex = 18
        ChkBoxAllowMiniMode.Text = "Allow MiniMode"
        TipFractalCloud.SetToolTip(ChkBoxAllowMiniMode, "Enables a compact, optimized version of this visualizer for the MiniPlayer’s smaller layout.")
        TipFractalCloud.SetToolTipImage(ChkBoxAllowMiniMode, Nothing)
        ChkBoxAllowMiniMode.UseVisualStyleBackColor = False
        ' 
        ' OptionsFractalCloud
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(ChkBoxAllowMiniMode)
        Controls.Add(LblPalette)
        Controls.Add(LblTimeIncrement)
        Controls.Add(CoBoxPalette)
        Controls.Add(LblSwirlSpeedAudioFactor)
        Controls.Add(TBTimeIncrement)
        Controls.Add(TBSwirlSpeedAudioFactor)
        Controls.Add(TBSwirlSpeedBase)
        Controls.Add(LblSwirlSpeedBase)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsFractalCloud"
        Size = New Size(800, 330)
        TipFractalCloud.SetToolTipImage(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblSwirlSpeedAudioFactor As Skye.UI.Label
    Friend WithEvents TBTimeIncrement As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBSwirlSpeedAudioFactor As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBSwirlSpeedBase As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LblSwirlSpeedBase As Skye.UI.Label
    Friend WithEvents CoBoxPalette As ComboBox
    Friend WithEvents LblTimeIncrement As Skye.UI.Label
    Friend WithEvents LblPalette As Skye.UI.Label
    Friend WithEvents TipFractalCloud As Skye.UI.ToolTip
    Friend WithEvents ChkBoxAllowMiniMode As CheckBox

End Class
