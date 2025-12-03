<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TagEditor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TagEditor))
        TxtBoxArtist = New TextBox()
        LblArtist = New Skye.UI.Label()
        TxtBoxTitle = New TextBox()
        LblTitle = New Skye.UI.Label()
        LbLAlbum = New Skye.UI.Label()
        TxtBoxAlbum = New TextBox()
        SuspendLayout()
        ' 
        ' TxtBoxArtist
        ' 
        TxtBoxArtist.Location = New Point(12, 28)
        TxtBoxArtist.Name = "TxtBoxArtist"
        TxtBoxArtist.Size = New Size(274, 29)
        TxtBoxArtist.TabIndex = 0
        ' 
        ' LblArtist
        ' 
        LblArtist.Location = New Point(12, 9)
        LblArtist.Name = "LblArtist"
        LblArtist.Size = New Size(100, 23)
        LblArtist.TabIndex = 1
        LblArtist.Text = "Artist"
        ' 
        ' TxtBoxTitle
        ' 
        TxtBoxTitle.Location = New Point(12, 75)
        TxtBoxTitle.Name = "TxtBoxTitle"
        TxtBoxTitle.Size = New Size(274, 29)
        TxtBoxTitle.TabIndex = 2
        ' 
        ' LblTitle
        ' 
        LblTitle.Location = New Point(12, 56)
        LblTitle.Name = "LblTitle"
        LblTitle.Size = New Size(100, 23)
        LblTitle.TabIndex = 3
        LblTitle.Text = "Title"
        ' 
        ' LbLAlbum
        ' 
        LbLAlbum.Location = New Point(12, 103)
        LbLAlbum.Name = "LbLAlbum"
        LbLAlbum.Size = New Size(100, 23)
        LbLAlbum.TabIndex = 5
        LbLAlbum.Text = "Album"
        ' 
        ' TxtBoxAlbum
        ' 
        TxtBoxAlbum.Location = New Point(12, 122)
        TxtBoxAlbum.Name = "TxtBoxAlbum"
        TxtBoxAlbum.Size = New Size(274, 29)
        TxtBoxAlbum.TabIndex = 4
        ' 
        ' TagEditor
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(851, 451)
        Controls.Add(TxtBoxAlbum)
        Controls.Add(TxtBoxTitle)
        Controls.Add(TxtBoxArtist)
        Controls.Add(LblArtist)
        Controls.Add(LblTitle)
        Controls.Add(LbLAlbum)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4)
        Name = "TagEditor"
        StartPosition = FormStartPosition.CenterParent
        Text = "Tag Editor"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TxtBoxArtist As TextBox
    Friend WithEvents LblArtist As Skye.UI.Label
    Friend WithEvents TxtBoxTitle As TextBox
    Friend WithEvents LblTitle As Skye.UI.Label
    Friend WithEvents LbLAlbum As Skye.UI.Label
    Friend WithEvents TxtBoxAlbum As TextBox
End Class
