<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ArtViewer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        PicBox = New PictureBox()
        CType(PicBox, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PicBox
        ' 
        PicBox.Dock = DockStyle.Fill
        PicBox.Location = New Point(0, 0)
        PicBox.Name = "PicBox"
        PicBox.Size = New Size(257, 190)
        PicBox.SizeMode = PictureBoxSizeMode.Zoom
        PicBox.TabIndex = 0
        PicBox.TabStop = False
        ' 
        ' ArtViewer
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(257, 190)
        ControlBox = False
        Controls.Add(PicBox)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.None
        Margin = New Padding(4)
        MaximizeBox = False
        MinimizeBox = False
        Name = "ArtViewer"
        ShowIcon = False
        ShowInTaskbar = False
        SizeGripStyle = SizeGripStyle.Hide
        StartPosition = FormStartPosition.Manual
        Text = "PictureBox"
        CType(PicBox, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents PicBox As System.Windows.Forms.PictureBox
End Class
