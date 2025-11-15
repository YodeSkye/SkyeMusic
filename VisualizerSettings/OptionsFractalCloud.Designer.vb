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
        LblSwirlSpeedAudioFactor = New Skye.UI.Label()
        TBTimeIncrement = New Syncfusion.Windows.Forms.Tools.TrackBarEx(5, 100)
        TBSwirlSpeedAudioFactor = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 30)
        TBSwirlSpeedBase = New Syncfusion.Windows.Forms.Tools.TrackBarEx(1, 50)
        LbSwirlSpeedBase = New Skye.UI.Label()
        CoBoxPalette = New ComboBox()
        LblTimeIncrement = New Skye.UI.Label()
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
        ' 
        ' TBTimeIncrement
        ' 
        TBTimeIncrement.BackColor = Color.Transparent
        TBTimeIncrement.BeforeTouchSize = New Size(20, 290)
        TBTimeIncrement.Location = New Point(699, 28)
        TBTimeIncrement.Name = "TBTimeIncrement"
        TBTimeIncrement.Orientation = Orientation.Vertical
        TBTimeIncrement.Size = New Size(20, 290)
        TBTimeIncrement.TabIndex = 12
        TBTimeIncrement.Text = "TrackBarEx2"
        TBTimeIncrement.TimerInterval = 100
        TBTimeIncrement.Value = 5
        ' 
        ' TBSwirlSpeedAudioFactor
        ' 
        TBSwirlSpeedAudioFactor.BackColor = Color.Transparent
        TBSwirlSpeedAudioFactor.BeforeTouchSize = New Size(20, 290)
        TBSwirlSpeedAudioFactor.Location = New Point(526, 25)
        TBSwirlSpeedAudioFactor.Name = "TBSwirlSpeedAudioFactor"
        TBSwirlSpeedAudioFactor.Orientation = Orientation.Vertical
        TBSwirlSpeedAudioFactor.Size = New Size(20, 290)
        TBSwirlSpeedAudioFactor.TabIndex = 11
        TBSwirlSpeedAudioFactor.Text = "TrackBarEx1"
        TBSwirlSpeedAudioFactor.TimerInterval = 100
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
        TBSwirlSpeedBase.Size = New Size(20, 290)
        TBSwirlSpeedBase.TabIndex = 9
        TBSwirlSpeedBase.TimerInterval = 100
        TBSwirlSpeedBase.Value = 10
        ' 
        ' LbSwirlSpeedBase
        ' 
        LbSwirlSpeedBase.Location = New Point(297, 4)
        LbSwirlSpeedBase.Name = "LbSwirlSpeedBase"
        LbSwirlSpeedBase.Size = New Size(131, 23)
        LbSwirlSpeedBase.TabIndex = 10
        LbSwirlSpeedBase.Text = "Swirl Speed Base"
        LbSwirlSpeedBase.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' CoBoxPalette
        ' 
        CoBoxPalette.FormattingEnabled = True
        CoBoxPalette.Location = New Point(13, 28)
        CoBoxPalette.Name = "CoBoxPalette"
        CoBoxPalette.Size = New Size(268, 29)
        CoBoxPalette.TabIndex = 15
        ' 
        ' LblTimeIncrement
        ' 
        LblTimeIncrement.Location = New Point(645, 4)
        LblTimeIncrement.Name = "LblTimeIncrement"
        LblTimeIncrement.Size = New Size(129, 23)
        LblTimeIncrement.TabIndex = 16
        LblTimeIncrement.Text = "Time Increment"
        LblTimeIncrement.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' OptionsFractalCloud
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(LblTimeIncrement)
        Controls.Add(CoBoxPalette)
        Controls.Add(LblSwirlSpeedAudioFactor)
        Controls.Add(TBTimeIncrement)
        Controls.Add(TBSwirlSpeedAudioFactor)
        Controls.Add(TBSwirlSpeedBase)
        Controls.Add(LbSwirlSpeedBase)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsFractalCloud"
        Size = New Size(800, 330)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblSwirlSpeedAudioFactor As Skye.UI.Label
    Friend WithEvents LblPeakDecaySpeed As Skye.UI.Label
    Friend WithEvents TBTimeIncrement As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBSwirlSpeedAudioFactor As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents TBSwirlSpeedBase As Syncfusion.Windows.Forms.Tools.TrackBarEx
    Friend WithEvents LbSwirlSpeedBase As Skye.UI.Label
    Friend WithEvents CoBoxPalette As ComboBox
    Friend WithEvents LblTimeIncrement As Skye.UI.Label

End Class
