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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TagEditor))
        TxtBoxArtist = New TextBox()
        LblArtist = New Skye.UI.Label()
        TxtBoxTitle = New TextBox()
        LblTitle = New Skye.UI.Label()
        LbLAlbum = New Skye.UI.Label()
        TxtBoxAlbum = New TextBox()
        BtnSave = New Button()
        BtnOK = New Button()
        TipTagEditor = New Skye.UI.ToolTipEX(components)
        btnArtistKeepOriginal = New Button()
        BtnTitleKeepOriginal = New Button()
        BtnAlbumKeepOriginal = New Button()
        SuspendLayout()
        ' 
        ' TxtBoxArtist
        ' 
        TipTagEditor.SetImage(TxtBoxArtist, Nothing)
        TxtBoxArtist.Location = New Point(12, 28)
        TxtBoxArtist.Name = "TxtBoxArtist"
        TxtBoxArtist.Size = New Size(380, 29)
        TxtBoxArtist.TabIndex = 100
        TipTagEditor.SetText(TxtBoxArtist, Nothing)
        ' 
        ' LblArtist
        ' 
        TipTagEditor.SetImage(LblArtist, Nothing)
        LblArtist.Location = New Point(12, 9)
        LblArtist.Name = "LblArtist"
        LblArtist.Size = New Size(100, 23)
        LblArtist.TabIndex = 1
        LblArtist.Text = "Artist"
        TipTagEditor.SetText(LblArtist, Nothing)
        ' 
        ' TxtBoxTitle
        ' 
        TipTagEditor.SetImage(TxtBoxTitle, Nothing)
        TxtBoxTitle.Location = New Point(12, 75)
        TxtBoxTitle.Name = "TxtBoxTitle"
        TxtBoxTitle.Size = New Size(380, 29)
        TxtBoxTitle.TabIndex = 200
        TipTagEditor.SetText(TxtBoxTitle, Nothing)
        ' 
        ' LblTitle
        ' 
        TipTagEditor.SetImage(LblTitle, Nothing)
        LblTitle.Location = New Point(12, 56)
        LblTitle.Name = "LblTitle"
        LblTitle.Size = New Size(100, 23)
        LblTitle.TabIndex = 3
        LblTitle.Text = "Title"
        TipTagEditor.SetText(LblTitle, Nothing)
        ' 
        ' LbLAlbum
        ' 
        TipTagEditor.SetImage(LbLAlbum, Nothing)
        LbLAlbum.Location = New Point(12, 103)
        LbLAlbum.Name = "LbLAlbum"
        LbLAlbum.Size = New Size(100, 23)
        LbLAlbum.TabIndex = 5
        LbLAlbum.Text = "Album"
        TipTagEditor.SetText(LbLAlbum, Nothing)
        ' 
        ' TxtBoxAlbum
        ' 
        TipTagEditor.SetImage(TxtBoxAlbum, Nothing)
        TxtBoxAlbum.Location = New Point(12, 122)
        TxtBoxAlbum.Name = "TxtBoxAlbum"
        TxtBoxAlbum.Size = New Size(380, 29)
        TxtBoxAlbum.TabIndex = 300
        TipTagEditor.SetText(TxtBoxAlbum, Nothing)
        ' 
        ' BtnSave
        ' 
        BtnSave.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnSave.Enabled = False
        BtnSave.Image = My.Resources.Resources.ImageSave32
        TipTagEditor.SetImage(BtnSave, Nothing)
        BtnSave.Location = New Point(12, 391)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New Size(48, 48)
        BtnSave.TabIndex = 5000
        TipTagEditor.SetText(BtnSave, "Save Tag(s)" & vbCrLf & "If you selected multiple songs in the Library, this will save the information to Each of those songs." & vbCrLf & "There is No Undo.")
        BtnSave.UseVisualStyleBackColor = True
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        TipTagEditor.SetImage(BtnOK, Nothing)
        BtnOK.Location = New Point(393, 375)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 5100
        TipTagEditor.SetText(BtnOK, "Close Window" & vbCrLf & "(Without Saving)")
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' TipTagEditor
        ' 
        TipTagEditor.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ' 
        ' btnArtistKeepOriginal
        ' 
        btnArtistKeepOriginal.Enabled = False
        btnArtistKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipTagEditor.SetImage(btnArtistKeepOriginal, Nothing)
        btnArtistKeepOriginal.Location = New Point(393, 26)
        btnArtistKeepOriginal.Name = "btnArtistKeepOriginal"
        btnArtistKeepOriginal.Size = New Size(32, 32)
        btnArtistKeepOriginal.TabIndex = 0
        btnArtistKeepOriginal.TabStop = False
        TipTagEditor.SetText(btnArtistKeepOriginal, "Undo")
        btnArtistKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' BtnTitleKeepOriginal
        ' 
        BtnTitleKeepOriginal.Enabled = False
        BtnTitleKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipTagEditor.SetImage(BtnTitleKeepOriginal, Nothing)
        BtnTitleKeepOriginal.Location = New Point(393, 73)
        BtnTitleKeepOriginal.Name = "BtnTitleKeepOriginal"
        BtnTitleKeepOriginal.Size = New Size(32, 32)
        BtnTitleKeepOriginal.TabIndex = 0
        BtnTitleKeepOriginal.TabStop = False
        TipTagEditor.SetText(BtnTitleKeepOriginal, "Undo")
        BtnTitleKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' BtnAlbumKeepOriginal
        ' 
        BtnAlbumKeepOriginal.Enabled = False
        BtnAlbumKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipTagEditor.SetImage(BtnAlbumKeepOriginal, Nothing)
        BtnAlbumKeepOriginal.Location = New Point(393, 120)
        BtnAlbumKeepOriginal.Name = "BtnAlbumKeepOriginal"
        BtnAlbumKeepOriginal.Size = New Size(32, 32)
        BtnAlbumKeepOriginal.TabIndex = 0
        BtnAlbumKeepOriginal.TabStop = False
        TipTagEditor.SetText(BtnAlbumKeepOriginal, "Undo")
        BtnAlbumKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' TagEditor
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(851, 451)
        Controls.Add(BtnAlbumKeepOriginal)
        Controls.Add(BtnTitleKeepOriginal)
        Controls.Add(btnArtistKeepOriginal)
        Controls.Add(BtnSave)
        Controls.Add(BtnOK)
        Controls.Add(TxtBoxAlbum)
        Controls.Add(TxtBoxTitle)
        Controls.Add(TxtBoxArtist)
        Controls.Add(LblArtist)
        Controls.Add(LblTitle)
        Controls.Add(LbLAlbum)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipTagEditor.SetImage(Me, Nothing)
        Margin = New Padding(4)
        MaximizeBox = False
        Name = "TagEditor"
        SizeGripStyle = SizeGripStyle.Hide
        StartPosition = FormStartPosition.CenterParent
        TipTagEditor.SetText(Me, Nothing)
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
    Friend WithEvents BtnSave As Button
    Friend WithEvents BtnOK As Button
    Friend WithEvents TipTagEditor As Skye.UI.ToolTipEX
    Friend WithEvents btnArtistKeepOriginal As Button
    Friend WithEvents BtnTitleKeepOriginal As Button
    Friend WithEvents BtnAlbumKeepOriginal As Button
End Class
