<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Splash
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
        LblSkye = New Label()
        LblMusic = New Label()
        SuspendLayout()
        ' 
        ' LblSkye
        ' 
        LblSkye.AutoSize = True
        LblSkye.Font = New Font("Segoe UI", 36F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        LblSkye.ForeColor = Color.DodgerBlue
        LblSkye.Location = New Point(20, 40)
        LblSkye.Name = "LblSkye"
        LblSkye.Size = New Size(132, 65)
        LblSkye.TabIndex = 0
        LblSkye.Text = "Skye"
        LblSkye.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblMusic
        ' 
        LblMusic.AutoSize = True
        LblMusic.Font = New Font("Segoe UI Semibold", 27.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblMusic.ForeColor = SystemColors.Window
        LblMusic.Location = New Point(134, 51)
        LblMusic.Name = "LblMusic"
        LblMusic.Size = New Size(121, 50)
        LblMusic.TabIndex = 1
        LblMusic.Text = "Music"
        LblMusic.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Splash
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Desktop
        ClientSize = New Size(276, 153)
        ControlBox = False
        Controls.Add(LblMusic)
        Controls.Add(LblSkye)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        MaximizeBox = False
        MinimizeBox = False
        Name = "Splash"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        TopMost = True
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblSkye As Label
    Friend WithEvents LblMusic As Label
End Class
