<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class History
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(History))
        BtnOK = New Button()
        LblTotalPlayedSongs = New Skye.UI.Label()
        TxtBoxTotalPlayedSongs = New TextBox()
        TxtBoxSessionPlayedSongs = New TextBox()
        LblSessionPlayedSongs = New Skye.UI.Label()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(368, 374)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 0
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' LblTotalPlayedSongs
        ' 
        LblTotalPlayedSongs.BackColor = Color.Transparent
        LblTotalPlayedSongs.Font = New Font("Segoe UI", 12F)
        LblTotalPlayedSongs.Location = New Point(568, 15)
        LblTotalPlayedSongs.Name = "LblTotalPlayedSongs"
        LblTotalPlayedSongs.Size = New Size(140, 23)
        LblTotalPlayedSongs.TabIndex = 1
        LblTotalPlayedSongs.Text = "Total Played Songs"
        LblTotalPlayedSongs.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TxtBoxTotalPlayedSongs
        ' 
        TxtBoxTotalPlayedSongs.Font = New Font("Segoe UI", 12F)
        TxtBoxTotalPlayedSongs.Location = New Point(705, 12)
        TxtBoxTotalPlayedSongs.Name = "TxtBoxTotalPlayedSongs"
        TxtBoxTotalPlayedSongs.Size = New Size(83, 29)
        TxtBoxTotalPlayedSongs.TabIndex = 2
        TxtBoxTotalPlayedSongs.TextAlign = HorizontalAlignment.Center
        ' 
        ' TxtBoxSessionPlayedSongs
        ' 
        TxtBoxSessionPlayedSongs.Font = New Font("Segoe UI", 12F)
        TxtBoxSessionPlayedSongs.Location = New Point(705, 47)
        TxtBoxSessionPlayedSongs.Name = "TxtBoxSessionPlayedSongs"
        TxtBoxSessionPlayedSongs.Size = New Size(83, 29)
        TxtBoxSessionPlayedSongs.TabIndex = 4
        TxtBoxSessionPlayedSongs.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblSessionPlayedSongs
        ' 
        LblSessionPlayedSongs.BackColor = Color.Transparent
        LblSessionPlayedSongs.Font = New Font("Segoe UI", 12F)
        LblSessionPlayedSongs.Location = New Point(540, 49)
        LblSessionPlayedSongs.Name = "LblSessionPlayedSongs"
        LblSessionPlayedSongs.Size = New Size(168, 23)
        LblSessionPlayedSongs.TabIndex = 3
        LblSessionPlayedSongs.Text = "Session Played Songs"
        LblSessionPlayedSongs.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' History
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(TxtBoxSessionPlayedSongs)
        Controls.Add(LblSessionPlayedSongs)
        Controls.Add(TxtBoxTotalPlayedSongs)
        Controls.Add(BtnOK)
        Controls.Add(LblTotalPlayedSongs)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "History"
        StartPosition = FormStartPosition.CenterScreen
        Text = "History & Statistics"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents LblTotalPlayedSongs As Skye.UI.Label
    Friend WithEvents TxtBoxTotalPlayedSongs As TextBox
    Friend WithEvents TxtBoxSessionPlayedSongs As TextBox
    Friend WithEvents LblSessionPlayedSongs As Skye.UI.Label
End Class
