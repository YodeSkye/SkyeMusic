<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsWaveform
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
        ChkBoxWaveformFill = New CheckBox()
        TipWaveform = New Skye.UI.ToolTip(components)
        ChkBoxAllowMiniMode = New CheckBox()
        SuspendLayout()
        ' 
        ' ChkBoxWaveformFill
        ' 
        ChkBoxWaveformFill.AutoSize = True
        ChkBoxWaveformFill.BackColor = Color.Transparent
        ChkBoxWaveformFill.CheckAlign = ContentAlignment.BottomCenter
        ChkBoxWaveformFill.FlatStyle = FlatStyle.Flat
        ChkBoxWaveformFill.Location = New Point(345, 24)
        ChkBoxWaveformFill.Name = "ChkBoxWaveformFill"
        ChkBoxWaveformFill.RightToLeft = RightToLeft.Yes
        ChkBoxWaveformFill.Size = New Size(110, 36)
        ChkBoxWaveformFill.TabIndex = 5
        ChkBoxWaveformFill.Text = "Fill Waveform"
        TipWaveform.SetToolTip(ChkBoxWaveformFill, "Fill Area Under Waveform")
        TipWaveform.SetToolTipImage(ChkBoxWaveformFill, Nothing)
        ChkBoxWaveformFill.UseVisualStyleBackColor = False
        ' 
        ' TipWaveform
        ' 
        TipWaveform.Font = New Font("Segoe UI", 10F)
        TipWaveform.OwnerDraw = True
        ' 
        ' ChkBoxAllowMiniMode
        ' 
        ChkBoxAllowMiniMode.AutoSize = True
        ChkBoxAllowMiniMode.BackColor = Color.Transparent
        ChkBoxAllowMiniMode.CheckAlign = ContentAlignment.BottomCenter
        ChkBoxAllowMiniMode.FlatStyle = FlatStyle.Flat
        ChkBoxAllowMiniMode.Location = New Point(336, 86)
        ChkBoxAllowMiniMode.Name = "ChkBoxAllowMiniMode"
        ChkBoxAllowMiniMode.RightToLeft = RightToLeft.Yes
        ChkBoxAllowMiniMode.Size = New Size(128, 36)
        ChkBoxAllowMiniMode.TabIndex = 6
        ChkBoxAllowMiniMode.Text = "Allow MiniMode"
        TipWaveform.SetToolTip(ChkBoxAllowMiniMode, "Enables a compact, optimized version of this visualizer for the MiniPlayer’s smaller layout.")
        TipWaveform.SetToolTipImage(ChkBoxAllowMiniMode, Nothing)
        ChkBoxAllowMiniMode.UseVisualStyleBackColor = False
        ' 
        ' OptionsWaveform
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(ChkBoxAllowMiniMode)
        Controls.Add(ChkBoxWaveformFill)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "OptionsWaveform"
        Size = New Size(800, 330)
        TipWaveform.SetToolTipImage(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ChkBoxWaveformFill As CheckBox
    Friend WithEvents TipWaveform As Skye.UI.ToolTip
    Friend WithEvents ChkBoxAllowMiniMode As CheckBox

End Class
