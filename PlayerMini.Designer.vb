<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlayerMini
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
        PicBoxAlbumArt = New PictureBox()
        BtnPlay = New Button()
        BtnStop = New Button()
        BtnPrevious = New Button()
        BtnNext = New Button()
        LblTitle = New Skye.UI.Label()
        PanelMarquee = New Panel()
        CType(PicBoxAlbumArt, ComponentModel.ISupportInitialize).BeginInit()
        PanelMarquee.SuspendLayout()
        SuspendLayout()
        ' 
        ' PicBoxAlbumArt
        ' 
        PicBoxAlbumArt.Location = New Point(3, 23)
        PicBoxAlbumArt.Name = "PicBoxAlbumArt"
        PicBoxAlbumArt.Size = New Size(94, 94)
        PicBoxAlbumArt.SizeMode = PictureBoxSizeMode.CenterImage
        PicBoxAlbumArt.TabIndex = 1
        PicBoxAlbumArt.TabStop = False
        ' 
        ' BtnPlay
        ' 
        BtnPlay.Location = New Point(30, 119)
        BtnPlay.Name = "BtnPlay"
        BtnPlay.Size = New Size(20, 20)
        BtnPlay.TabIndex = 2
        BtnPlay.UseVisualStyleBackColor = True
        ' 
        ' BtnStop
        ' 
        BtnStop.Location = New Point(50, 119)
        BtnStop.Name = "BtnStop"
        BtnStop.Size = New Size(20, 20)
        BtnStop.TabIndex = 3
        BtnStop.UseVisualStyleBackColor = True
        ' 
        ' BtnPrevious
        ' 
        BtnPrevious.Location = New Point(10, 119)
        BtnPrevious.Name = "BtnPrevious"
        BtnPrevious.Size = New Size(20, 20)
        BtnPrevious.TabIndex = 4
        BtnPrevious.UseVisualStyleBackColor = True
        ' 
        ' BtnNext
        ' 
        BtnNext.Location = New Point(70, 119)
        BtnNext.Name = "BtnNext"
        BtnNext.Size = New Size(20, 20)
        BtnNext.TabIndex = 5
        BtnNext.UseVisualStyleBackColor = True
        ' 
        ' LblTitle
        ' 
        LblTitle.AutoSize = True
        LblTitle.BackColor = Color.Transparent
        LblTitle.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LblTitle.Location = New Point(3, 1)
        LblTitle.Name = "LblTitle"
        LblTitle.Size = New Size(61, 17)
        LblTitle.TabIndex = 6
        LblTitle.Text = "Marquee"
        LblTitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' PanelMarquee
        ' 
        PanelMarquee.Controls.Add(LblTitle)
        PanelMarquee.Location = New Point(3, 3)
        PanelMarquee.Name = "PanelMarquee"
        PanelMarquee.Size = New Size(94, 22)
        PanelMarquee.TabIndex = 7
        ' 
        ' PlayerMini
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(100, 144)
        ControlBox = False
        Controls.Add(PicBoxAlbumArt)
        Controls.Add(PanelMarquee)
        Controls.Add(BtnNext)
        Controls.Add(BtnPrevious)
        Controls.Add(BtnStop)
        Controls.Add(BtnPlay)
        FormBorderStyle = FormBorderStyle.None
        MaximizeBox = False
        MinimizeBox = False
        Name = "PlayerMini"
        ShowIcon = False
        ShowInTaskbar = False
        SizeGripStyle = SizeGripStyle.Hide
        StartPosition = FormStartPosition.Manual
        TopMost = True
        CType(PicBoxAlbumArt, ComponentModel.ISupportInitialize).EndInit()
        PanelMarquee.ResumeLayout(False)
        PanelMarquee.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents PicBoxAlbumArt As PictureBox
    Friend WithEvents BtnPlay As Button
    Friend WithEvents BtnStop As Button
    Friend WithEvents BtnPrevious As Button
    Friend WithEvents BtnNext As Button
    Friend WithEvents LblTitle As Skye.UI.Label
    Friend WithEvents PanelMarquee As Panel
End Class
