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
        TrackBarEx1 = New Syncfusion.Windows.Forms.Tools.TrackBarEx(0, 10)
        SuspendLayout()
        ' 
        ' TrackBarEx1
        ' 
        TrackBarEx1.BackColor = Color.Transparent
        TrackBarEx1.BeforeTouchSize = New Size(20, 250)
        TrackBarEx1.Location = New Point(202, 49)
        TrackBarEx1.Name = "TrackBarEx1"
        TrackBarEx1.Orientation = Orientation.Vertical
        TrackBarEx1.Size = New Size(20, 250)
        TrackBarEx1.TabIndex = 0
        TrackBarEx1.Text = "TrackBarEx1"
        TrackBarEx1.TimerInterval = 100
        TrackBarEx1.Value = 5
        ' 
        ' OptionsRainbowBar
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(TrackBarEx1)
        Name = "OptionsRainbowBar"
        Size = New Size(800, 330)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TrackBarEx1 As Syncfusion.Windows.Forms.Tools.TrackBarEx

End Class
