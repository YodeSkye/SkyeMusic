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
        TxtBoxTotalDuration = New TextBox()
        LblTotalDuration = New Skye.UI.Label()
        TxtBoxMostPlayedSong = New TextBox()
        LblMostPlayedSong = New Skye.UI.Label()
        LVHistory = New Skye.UI.ListViewEX()
        GrpBoxHistory = New GroupBox()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(473, 524)
        BtnOK.Margin = New Padding(4)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(82, 90)
        BtnOK.TabIndex = 0
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' LblTotalPlayedSongs
        ' 
        LblTotalPlayedSongs.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblTotalPlayedSongs.BackColor = Color.Transparent
        LblTotalPlayedSongs.Font = New Font("Segoe UI", 12F)
        LblTotalPlayedSongs.Location = New Point(733, 47)
        LblTotalPlayedSongs.Margin = New Padding(4, 0, 4, 0)
        LblTotalPlayedSongs.Name = "LblTotalPlayedSongs"
        LblTotalPlayedSongs.Size = New Size(180, 32)
        LblTotalPlayedSongs.TabIndex = 1
        LblTotalPlayedSongs.Text = "Total Played Songs"
        LblTotalPlayedSongs.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TxtBoxTotalPlayedSongs
        ' 
        TxtBoxTotalPlayedSongs.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TxtBoxTotalPlayedSongs.Font = New Font("Segoe UI", 12F)
        TxtBoxTotalPlayedSongs.Location = New Point(910, 50)
        TxtBoxTotalPlayedSongs.Margin = New Padding(4)
        TxtBoxTotalPlayedSongs.Name = "TxtBoxTotalPlayedSongs"
        TxtBoxTotalPlayedSongs.ReadOnly = True
        TxtBoxTotalPlayedSongs.Size = New Size(106, 29)
        TxtBoxTotalPlayedSongs.TabIndex = 2
        TxtBoxTotalPlayedSongs.TabStop = False
        TxtBoxTotalPlayedSongs.TextAlign = HorizontalAlignment.Center
        ' 
        ' TxtBoxSessionPlayedSongs
        ' 
        TxtBoxSessionPlayedSongs.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TxtBoxSessionPlayedSongs.Font = New Font("Segoe UI", 12F)
        TxtBoxSessionPlayedSongs.Location = New Point(910, 13)
        TxtBoxSessionPlayedSongs.Margin = New Padding(4)
        TxtBoxSessionPlayedSongs.Name = "TxtBoxSessionPlayedSongs"
        TxtBoxSessionPlayedSongs.ReadOnly = True
        TxtBoxSessionPlayedSongs.Size = New Size(106, 29)
        TxtBoxSessionPlayedSongs.TabIndex = 4
        TxtBoxSessionPlayedSongs.TabStop = False
        TxtBoxSessionPlayedSongs.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblSessionPlayedSongs
        ' 
        LblSessionPlayedSongs.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblSessionPlayedSongs.BackColor = Color.Transparent
        LblSessionPlayedSongs.Font = New Font("Segoe UI", 12F)
        LblSessionPlayedSongs.Location = New Point(697, 11)
        LblSessionPlayedSongs.Margin = New Padding(4, 0, 4, 0)
        LblSessionPlayedSongs.Name = "LblSessionPlayedSongs"
        LblSessionPlayedSongs.Size = New Size(216, 32)
        LblSessionPlayedSongs.TabIndex = 3
        LblSessionPlayedSongs.Text = "Session Played Songs"
        LblSessionPlayedSongs.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TxtBoxTotalDuration
        ' 
        TxtBoxTotalDuration.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TxtBoxTotalDuration.Font = New Font("Segoe UI", 12F)
        TxtBoxTotalDuration.Location = New Point(910, 87)
        TxtBoxTotalDuration.Margin = New Padding(4)
        TxtBoxTotalDuration.Name = "TxtBoxTotalDuration"
        TxtBoxTotalDuration.ReadOnly = True
        TxtBoxTotalDuration.Size = New Size(106, 29)
        TxtBoxTotalDuration.TabIndex = 5
        TxtBoxTotalDuration.TabStop = False
        TxtBoxTotalDuration.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblTotalDuration
        ' 
        LblTotalDuration.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LblTotalDuration.BackColor = Color.Transparent
        LblTotalDuration.Font = New Font("Segoe UI", 12F)
        LblTotalDuration.Location = New Point(733, 84)
        LblTotalDuration.Margin = New Padding(4, 0, 4, 0)
        LblTotalDuration.Name = "LblTotalDuration"
        LblTotalDuration.Size = New Size(180, 32)
        LblTotalDuration.TabIndex = 6
        LblTotalDuration.Text = "Total Duration"
        LblTotalDuration.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TxtBoxMostPlayedSong
        ' 
        TxtBoxMostPlayedSong.Font = New Font("Segoe UI", 12F)
        TxtBoxMostPlayedSong.Location = New Point(13, 50)
        TxtBoxMostPlayedSong.Margin = New Padding(4)
        TxtBoxMostPlayedSong.Name = "TxtBoxMostPlayedSong"
        TxtBoxMostPlayedSong.ReadOnly = True
        TxtBoxMostPlayedSong.Size = New Size(575, 29)
        TxtBoxMostPlayedSong.TabIndex = 7
        TxtBoxMostPlayedSong.TabStop = False
        TxtBoxMostPlayedSong.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblMostPlayedSong
        ' 
        LblMostPlayedSong.BackColor = Color.Transparent
        LblMostPlayedSong.Font = New Font("Segoe UI", 12F)
        LblMostPlayedSong.Location = New Point(13, 21)
        LblMostPlayedSong.Margin = New Padding(4, 0, 4, 0)
        LblMostPlayedSong.Name = "LblMostPlayedSong"
        LblMostPlayedSong.Size = New Size(576, 32)
        LblMostPlayedSong.TabIndex = 8
        LblMostPlayedSong.Text = "Most Played Song"
        LblMostPlayedSong.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LVHistory
        ' 
        LVHistory.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LVHistory.InsertionLineColor = Color.Teal
        LVHistory.Location = New Point(13, 124)
        LVHistory.Margin = New Padding(4)
        LVHistory.Name = "LVHistory"
        LVHistory.Size = New Size(805, 336)
        LVHistory.TabIndex = 9
        LVHistory.UseCompatibleStateImageBehavior = False
        ' 
        ' GrpBoxHistory
        ' 
        GrpBoxHistory.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        GrpBoxHistory.BackColor = Color.Transparent
        GrpBoxHistory.Location = New Point(826, 184)
        GrpBoxHistory.Margin = New Padding(4)
        GrpBoxHistory.Name = "GrpBoxHistory"
        GrpBoxHistory.Padding = New Padding(4)
        GrpBoxHistory.Size = New Size(190, 217)
        GrpBoxHistory.TabIndex = 10
        GrpBoxHistory.TabStop = False
        ' 
        ' History
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1029, 630)
        Controls.Add(GrpBoxHistory)
        Controls.Add(LVHistory)
        Controls.Add(TxtBoxMostPlayedSong)
        Controls.Add(LblMostPlayedSong)
        Controls.Add(TxtBoxTotalDuration)
        Controls.Add(TxtBoxSessionPlayedSongs)
        Controls.Add(LblSessionPlayedSongs)
        Controls.Add(TxtBoxTotalPlayedSongs)
        Controls.Add(BtnOK)
        Controls.Add(LblTotalPlayedSongs)
        Controls.Add(LblTotalDuration)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4)
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
    Friend WithEvents TxtBoxTotalDuration As TextBox
    Friend WithEvents LblTotalDuration As Skye.UI.Label
    Friend WithEvents LVHistory As Skye.UI.ListViewEX
    Friend WithEvents GrpBoxHistory As GroupBox
    Private WithEvents TxtBoxMostPlayedSong As TextBox
    Private WithEvents LblMostPlayedSong As Skye.UI.Label
End Class
