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
        CMBasic = New Skye.UI.TextBoxContextMenu()
        LblArtist = New Skye.UI.Label()
        TxtBoxTitle = New TextBox()
        LblTitle = New Skye.UI.Label()
        LbLAlbum = New Skye.UI.Label()
        TxtBoxAlbum = New TextBox()
        BtnSave = New Button()
        BtnOK = New Button()
        TipInfo = New Skye.UI.ToolTipEX(components)
        btnArtistKeepOriginal = New Button()
        BtnTitleKeepOriginal = New Button()
        BtnAlbumKeepOriginal = New Button()
        CoBoxGenre = New ComboBox()
        LblGenre = New Skye.UI.Label()
        TxtBoxComments = New TextBox()
        TxtBoxTracks = New TextBox()
        TxtBoxYear = New TextBox()
        LblYear = New Skye.UI.Label()
        LblTracks = New Skye.UI.Label()
        LblComments = New Skye.UI.Label()
        BtnGenreKeepOriginal = New Button()
        BtnYearKeepOriginal = New Button()
        BtnTracksKeepOriginal = New Button()
        BtnCommentsKeepOriginal = New Button()
        TxtBoxGenre = New TextBox()
        BtnTrackKeepOriginal = New Button()
        TxtBoxTrack = New TextBox()
        LblTrack = New Skye.UI.Label()
        TxtBoxArtDescription = New TextBox()
        CoBoxArtType = New ComboBox()
        PicBoxArt = New PictureBox()
        BtnArtRight = New Button()
        BtnArtLeft = New Button()
        BtnArtKeepOriginal = New Button()
        BtnArtRemove = New Button()
        BtnArtNewFromClipboard = New Button()
        BtnArtNewFromFile = New Button()
        BtnArtNewFromOnline = New Button()
        LblArt = New Skye.UI.Label()
        TipStatus = New Skye.UI.ToolTipEX(components)
        CType(PicBoxArt, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TxtBoxArtist
        ' 
        TxtBoxArtist.ContextMenuStrip = CMBasic
        TipStatus.SetImage(TxtBoxArtist, Nothing)
        TipInfo.SetImage(TxtBoxArtist, Nothing)
        TxtBoxArtist.Location = New Point(12, 28)
        TxtBoxArtist.Name = "TxtBoxArtist"
        TxtBoxArtist.ShortcutsEnabled = False
        TxtBoxArtist.Size = New Size(380, 29)
        TxtBoxArtist.TabIndex = 100
        TipInfo.SetText(TxtBoxArtist, Nothing)
        TipStatus.SetText(TxtBoxArtist, Nothing)
        ' 
        ' CMBasic
        ' 
        TipInfo.SetImage(CMBasic, Nothing)
        TipStatus.SetImage(CMBasic, Nothing)
        CMBasic.Name = "CMBasic"
        CMBasic.ShowExtendedTools = True
        CMBasic.Size = New Size(138, 176)
        TipStatus.SetText(CMBasic, Nothing)
        TipInfo.SetText(CMBasic, Nothing)
        ' 
        ' LblArtist
        ' 
        TipStatus.SetImage(LblArtist, Nothing)
        TipInfo.SetImage(LblArtist, Nothing)
        LblArtist.Location = New Point(12, 9)
        LblArtist.Name = "LblArtist"
        LblArtist.Size = New Size(100, 23)
        LblArtist.TabIndex = 1
        TipInfo.SetText(LblArtist, Nothing)
        LblArtist.Text = "Artist"
        TipStatus.SetText(LblArtist, Nothing)
        ' 
        ' TxtBoxTitle
        ' 
        TxtBoxTitle.ContextMenuStrip = CMBasic
        TipStatus.SetImage(TxtBoxTitle, Nothing)
        TipInfo.SetImage(TxtBoxTitle, Nothing)
        TxtBoxTitle.Location = New Point(12, 75)
        TxtBoxTitle.Name = "TxtBoxTitle"
        TxtBoxTitle.ShortcutsEnabled = False
        TxtBoxTitle.Size = New Size(380, 29)
        TxtBoxTitle.TabIndex = 200
        TipInfo.SetText(TxtBoxTitle, Nothing)
        TipStatus.SetText(TxtBoxTitle, Nothing)
        ' 
        ' LblTitle
        ' 
        TipStatus.SetImage(LblTitle, Nothing)
        TipInfo.SetImage(LblTitle, Nothing)
        LblTitle.Location = New Point(12, 56)
        LblTitle.Name = "LblTitle"
        LblTitle.Size = New Size(100, 23)
        LblTitle.TabIndex = 3
        TipInfo.SetText(LblTitle, Nothing)
        LblTitle.Text = "Title"
        TipStatus.SetText(LblTitle, Nothing)
        ' 
        ' LbLAlbum
        ' 
        TipStatus.SetImage(LbLAlbum, Nothing)
        TipInfo.SetImage(LbLAlbum, Nothing)
        LbLAlbum.Location = New Point(12, 103)
        LbLAlbum.Name = "LbLAlbum"
        LbLAlbum.Size = New Size(100, 23)
        LbLAlbum.TabIndex = 5
        TipInfo.SetText(LbLAlbum, Nothing)
        LbLAlbum.Text = "Album"
        TipStatus.SetText(LbLAlbum, Nothing)
        ' 
        ' TxtBoxAlbum
        ' 
        TxtBoxAlbum.ContextMenuStrip = CMBasic
        TipStatus.SetImage(TxtBoxAlbum, Nothing)
        TipInfo.SetImage(TxtBoxAlbum, Nothing)
        TxtBoxAlbum.Location = New Point(12, 122)
        TxtBoxAlbum.Name = "TxtBoxAlbum"
        TxtBoxAlbum.ShortcutsEnabled = False
        TxtBoxAlbum.Size = New Size(380, 29)
        TxtBoxAlbum.TabIndex = 300
        TipInfo.SetText(TxtBoxAlbum, Nothing)
        TipStatus.SetText(TxtBoxAlbum, Nothing)
        ' 
        ' BtnSave
        ' 
        BtnSave.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnSave.Enabled = False
        TipStatus.SetImage(BtnSave, Nothing)
        BtnSave.Image = My.Resources.Resources.ImageSave32
        TipInfo.SetImage(BtnSave, Nothing)
        BtnSave.Location = New Point(12, 415)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New Size(48, 48)
        BtnSave.TabIndex = 5000
        TipInfo.SetText(BtnSave, "Save Tag(s)" & vbCrLf & "If you selected multiple songs in the Library, this will save the information to Each of those songs." & vbCrLf & "There is No Undo.")
        TipStatus.SetText(BtnSave, Nothing)
        BtnSave.UseVisualStyleBackColor = True
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        TipStatus.SetImage(BtnOK, Nothing)
        BtnOK.Image = My.Resources.Resources.ImageOK
        TipInfo.SetImage(BtnOK, Nothing)
        BtnOK.Location = New Point(190, 399)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 5100
        TipInfo.SetText(BtnOK, "Close Window" & vbCrLf & "(Without Saving)")
        TipStatus.SetText(BtnOK, Nothing)
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' TipInfo
        ' 
        TipInfo.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ' 
        ' btnArtistKeepOriginal
        ' 
        btnArtistKeepOriginal.Enabled = False
        TipStatus.SetImage(btnArtistKeepOriginal, Nothing)
        btnArtistKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipInfo.SetImage(btnArtistKeepOriginal, Nothing)
        btnArtistKeepOriginal.Location = New Point(393, 26)
        btnArtistKeepOriginal.Name = "btnArtistKeepOriginal"
        btnArtistKeepOriginal.Size = New Size(32, 32)
        btnArtistKeepOriginal.TabIndex = 0
        btnArtistKeepOriginal.TabStop = False
        TipInfo.SetText(btnArtistKeepOriginal, "Undo")
        TipStatus.SetText(btnArtistKeepOriginal, Nothing)
        btnArtistKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' BtnTitleKeepOriginal
        ' 
        BtnTitleKeepOriginal.Enabled = False
        TipStatus.SetImage(BtnTitleKeepOriginal, Nothing)
        BtnTitleKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipInfo.SetImage(BtnTitleKeepOriginal, Nothing)
        BtnTitleKeepOriginal.Location = New Point(393, 73)
        BtnTitleKeepOriginal.Name = "BtnTitleKeepOriginal"
        BtnTitleKeepOriginal.Size = New Size(32, 32)
        BtnTitleKeepOriginal.TabIndex = 0
        BtnTitleKeepOriginal.TabStop = False
        TipInfo.SetText(BtnTitleKeepOriginal, "Undo")
        TipStatus.SetText(BtnTitleKeepOriginal, Nothing)
        BtnTitleKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' BtnAlbumKeepOriginal
        ' 
        BtnAlbumKeepOriginal.Enabled = False
        TipStatus.SetImage(BtnAlbumKeepOriginal, Nothing)
        BtnAlbumKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipInfo.SetImage(BtnAlbumKeepOriginal, Nothing)
        BtnAlbumKeepOriginal.Location = New Point(393, 120)
        BtnAlbumKeepOriginal.Name = "BtnAlbumKeepOriginal"
        BtnAlbumKeepOriginal.Size = New Size(32, 32)
        BtnAlbumKeepOriginal.TabIndex = 0
        BtnAlbumKeepOriginal.TabStop = False
        TipInfo.SetText(BtnAlbumKeepOriginal, "Undo")
        TipStatus.SetText(BtnAlbumKeepOriginal, Nothing)
        BtnAlbumKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' CoBoxGenre
        ' 
        CoBoxGenre.ContextMenuStrip = CMBasic
        CoBoxGenre.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxGenre.FormattingEnabled = True
        TipInfo.SetImage(CoBoxGenre, Nothing)
        TipStatus.SetImage(CoBoxGenre, Nothing)
        CoBoxGenre.Location = New Point(12, 169)
        CoBoxGenre.Name = "CoBoxGenre"
        CoBoxGenre.Size = New Size(380, 29)
        CoBoxGenre.Sorted = True
        CoBoxGenre.TabIndex = 5300
        CoBoxGenre.TabStop = False
        TipInfo.SetText(CoBoxGenre, Nothing)
        TipStatus.SetText(CoBoxGenre, Nothing)
        ' 
        ' LblGenre
        ' 
        TipStatus.SetImage(LblGenre, Nothing)
        TipInfo.SetImage(LblGenre, Nothing)
        LblGenre.Location = New Point(12, 150)
        LblGenre.Name = "LblGenre"
        LblGenre.Size = New Size(100, 23)
        LblGenre.TabIndex = 5102
        TipInfo.SetText(LblGenre, Nothing)
        LblGenre.Text = "Genre"
        TipStatus.SetText(LblGenre, Nothing)
        ' 
        ' TxtBoxComments
        ' 
        TxtBoxComments.ContextMenuStrip = CMBasic
        TipStatus.SetImage(TxtBoxComments, Nothing)
        TipInfo.SetImage(TxtBoxComments, Nothing)
        TxtBoxComments.Location = New Point(12, 309)
        TxtBoxComments.Name = "TxtBoxComments"
        TxtBoxComments.ShortcutsEnabled = False
        TxtBoxComments.Size = New Size(380, 29)
        TxtBoxComments.TabIndex = 800
        TipInfo.SetText(TxtBoxComments, Nothing)
        TipStatus.SetText(TxtBoxComments, Nothing)
        ' 
        ' TxtBoxTracks
        ' 
        TxtBoxTracks.ContextMenuStrip = CMBasic
        TipStatus.SetImage(TxtBoxTracks, Nothing)
        TipInfo.SetImage(TxtBoxTracks, Nothing)
        TxtBoxTracks.Location = New Point(242, 263)
        TxtBoxTracks.Name = "TxtBoxTracks"
        TxtBoxTracks.ShortcutsEnabled = False
        TxtBoxTracks.Size = New Size(150, 29)
        TxtBoxTracks.TabIndex = 700
        TipInfo.SetText(TxtBoxTracks, Nothing)
        TipStatus.SetText(TxtBoxTracks, Nothing)
        ' 
        ' TxtBoxYear
        ' 
        TxtBoxYear.ContextMenuStrip = CMBasic
        TipStatus.SetImage(TxtBoxYear, Nothing)
        TipInfo.SetImage(TxtBoxYear, Nothing)
        TxtBoxYear.Location = New Point(12, 216)
        TxtBoxYear.Name = "TxtBoxYear"
        TxtBoxYear.ShortcutsEnabled = False
        TxtBoxYear.Size = New Size(150, 29)
        TxtBoxYear.TabIndex = 500
        TipInfo.SetText(TxtBoxYear, Nothing)
        TipStatus.SetText(TxtBoxYear, Nothing)
        ' 
        ' LblYear
        ' 
        TipStatus.SetImage(LblYear, Nothing)
        TipInfo.SetImage(LblYear, Nothing)
        LblYear.Location = New Point(12, 197)
        LblYear.Name = "LblYear"
        LblYear.Size = New Size(100, 23)
        LblYear.TabIndex = 5103
        TipInfo.SetText(LblYear, Nothing)
        LblYear.Text = "Year"
        TipStatus.SetText(LblYear, Nothing)
        ' 
        ' LblTracks
        ' 
        TipStatus.SetImage(LblTracks, Nothing)
        TipInfo.SetImage(LblTracks, Nothing)
        LblTracks.Location = New Point(242, 244)
        LblTracks.Name = "LblTracks"
        LblTracks.Size = New Size(100, 23)
        LblTracks.TabIndex = 5104
        TipInfo.SetText(LblTracks, Nothing)
        LblTracks.Text = "# of Tracks"
        TipStatus.SetText(LblTracks, Nothing)
        ' 
        ' LblComments
        ' 
        TipStatus.SetImage(LblComments, Nothing)
        TipInfo.SetImage(LblComments, Nothing)
        LblComments.Location = New Point(12, 290)
        LblComments.Name = "LblComments"
        LblComments.Size = New Size(100, 23)
        LblComments.TabIndex = 5105
        TipInfo.SetText(LblComments, Nothing)
        LblComments.Text = "Comments"
        TipStatus.SetText(LblComments, Nothing)
        ' 
        ' BtnGenreKeepOriginal
        ' 
        BtnGenreKeepOriginal.Enabled = False
        TipStatus.SetImage(BtnGenreKeepOriginal, Nothing)
        BtnGenreKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipInfo.SetImage(BtnGenreKeepOriginal, Nothing)
        BtnGenreKeepOriginal.Location = New Point(393, 167)
        BtnGenreKeepOriginal.Name = "BtnGenreKeepOriginal"
        BtnGenreKeepOriginal.Size = New Size(32, 32)
        BtnGenreKeepOriginal.TabIndex = 5109
        BtnGenreKeepOriginal.TabStop = False
        TipInfo.SetText(BtnGenreKeepOriginal, "Undo")
        TipStatus.SetText(BtnGenreKeepOriginal, Nothing)
        BtnGenreKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' BtnYearKeepOriginal
        ' 
        BtnYearKeepOriginal.Enabled = False
        TipStatus.SetImage(BtnYearKeepOriginal, Nothing)
        BtnYearKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipInfo.SetImage(BtnYearKeepOriginal, Nothing)
        BtnYearKeepOriginal.Location = New Point(162, 214)
        BtnYearKeepOriginal.Name = "BtnYearKeepOriginal"
        BtnYearKeepOriginal.Size = New Size(32, 32)
        BtnYearKeepOriginal.TabIndex = 5110
        BtnYearKeepOriginal.TabStop = False
        TipInfo.SetText(BtnYearKeepOriginal, "Undo")
        TipStatus.SetText(BtnYearKeepOriginal, Nothing)
        BtnYearKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' BtnTracksKeepOriginal
        ' 
        BtnTracksKeepOriginal.Enabled = False
        TipStatus.SetImage(BtnTracksKeepOriginal, Nothing)
        BtnTracksKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipInfo.SetImage(BtnTracksKeepOriginal, Nothing)
        BtnTracksKeepOriginal.Location = New Point(393, 261)
        BtnTracksKeepOriginal.Name = "BtnTracksKeepOriginal"
        BtnTracksKeepOriginal.Size = New Size(32, 32)
        BtnTracksKeepOriginal.TabIndex = 5111
        BtnTracksKeepOriginal.TabStop = False
        TipInfo.SetText(BtnTracksKeepOriginal, "Undo")
        TipStatus.SetText(BtnTracksKeepOriginal, Nothing)
        BtnTracksKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' BtnCommentsKeepOriginal
        ' 
        BtnCommentsKeepOriginal.Enabled = False
        TipStatus.SetImage(BtnCommentsKeepOriginal, Nothing)
        BtnCommentsKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipInfo.SetImage(BtnCommentsKeepOriginal, Nothing)
        BtnCommentsKeepOriginal.Location = New Point(393, 307)
        BtnCommentsKeepOriginal.Name = "BtnCommentsKeepOriginal"
        BtnCommentsKeepOriginal.Size = New Size(32, 32)
        BtnCommentsKeepOriginal.TabIndex = 5112
        BtnCommentsKeepOriginal.TabStop = False
        TipInfo.SetText(BtnCommentsKeepOriginal, "Undo")
        TipStatus.SetText(BtnCommentsKeepOriginal, Nothing)
        BtnCommentsKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxGenre
        ' 
        TxtBoxGenre.ContextMenuStrip = CMBasic
        TipStatus.SetImage(TxtBoxGenre, Nothing)
        TipInfo.SetImage(TxtBoxGenre, Nothing)
        TxtBoxGenre.Location = New Point(12, 169)
        TxtBoxGenre.Name = "TxtBoxGenre"
        TxtBoxGenre.ShortcutsEnabled = False
        TxtBoxGenre.Size = New Size(363, 29)
        TxtBoxGenre.TabIndex = 400
        TipInfo.SetText(TxtBoxGenre, Nothing)
        TipStatus.SetText(TxtBoxGenre, Nothing)
        ' 
        ' BtnTrackKeepOriginal
        ' 
        BtnTrackKeepOriginal.Enabled = False
        TipStatus.SetImage(BtnTrackKeepOriginal, Nothing)
        BtnTrackKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipInfo.SetImage(BtnTrackKeepOriginal, Nothing)
        BtnTrackKeepOriginal.Location = New Point(162, 261)
        BtnTrackKeepOriginal.Name = "BtnTrackKeepOriginal"
        BtnTrackKeepOriginal.Size = New Size(32, 32)
        BtnTrackKeepOriginal.TabIndex = 5116
        BtnTrackKeepOriginal.TabStop = False
        TipInfo.SetText(BtnTrackKeepOriginal, "Undo")
        TipStatus.SetText(BtnTrackKeepOriginal, Nothing)
        BtnTrackKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxTrack
        ' 
        TxtBoxTrack.ContextMenuStrip = CMBasic
        TipStatus.SetImage(TxtBoxTrack, Nothing)
        TipInfo.SetImage(TxtBoxTrack, Nothing)
        TxtBoxTrack.Location = New Point(12, 263)
        TxtBoxTrack.Name = "TxtBoxTrack"
        TxtBoxTrack.ShortcutsEnabled = False
        TxtBoxTrack.Size = New Size(150, 29)
        TxtBoxTrack.TabIndex = 600
        TipInfo.SetText(TxtBoxTrack, Nothing)
        TipStatus.SetText(TxtBoxTrack, Nothing)
        ' 
        ' LblTrack
        ' 
        TipStatus.SetImage(LblTrack, Nothing)
        TipInfo.SetImage(LblTrack, Nothing)
        LblTrack.Location = New Point(12, 244)
        LblTrack.Name = "LblTrack"
        LblTrack.Size = New Size(100, 23)
        LblTrack.TabIndex = 5114
        TipInfo.SetText(LblTrack, Nothing)
        LblTrack.Text = "Track #"
        TipStatus.SetText(LblTrack, Nothing)
        ' 
        ' TxtBoxArtDescription
        ' 
        TxtBoxArtDescription.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TxtBoxArtDescription.ContextMenuStrip = CMBasic
        TipStatus.SetImage(TxtBoxArtDescription, Nothing)
        TipInfo.SetImage(TxtBoxArtDescription, Nothing)
        TxtBoxArtDescription.Location = New Point(445, 28)
        TxtBoxArtDescription.Name = "TxtBoxArtDescription"
        TxtBoxArtDescription.ShortcutsEnabled = False
        TxtBoxArtDescription.Size = New Size(200, 29)
        TxtBoxArtDescription.TabIndex = 5500
        TxtBoxArtDescription.TabStop = False
        TipInfo.SetText(TxtBoxArtDescription, "Description of Artwork")
        TipStatus.SetText(TxtBoxArtDescription, Nothing)
        ' 
        ' CoBoxArtType
        ' 
        CoBoxArtType.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        CoBoxArtType.FormattingEnabled = True
        TipInfo.SetImage(CoBoxArtType, Nothing)
        TipStatus.SetImage(CoBoxArtType, Nothing)
        CoBoxArtType.Location = New Point(651, 28)
        CoBoxArtType.Name = "CoBoxArtType"
        CoBoxArtType.Size = New Size(170, 29)
        CoBoxArtType.Sorted = True
        CoBoxArtType.TabIndex = 5555
        CoBoxArtType.TabStop = False
        TipInfo.SetText(CoBoxArtType, "Type of Artwork")
        TipStatus.SetText(CoBoxArtType, Nothing)
        ' 
        ' PicBoxArt
        ' 
        PicBoxArt.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        PicBoxArt.BorderStyle = BorderStyle.Fixed3D
        TipStatus.SetImage(PicBoxArt, Nothing)
        TipInfo.SetImage(PicBoxArt, Nothing)
        PicBoxArt.Location = New Point(446, 58)
        PicBoxArt.Name = "PicBoxArt"
        PicBoxArt.Size = New Size(375, 375)
        PicBoxArt.SizeMode = PictureBoxSizeMode.Zoom
        PicBoxArt.TabIndex = 5121
        PicBoxArt.TabStop = False
        TipStatus.SetText(PicBoxArt, Nothing)
        TipInfo.SetText(PicBoxArt, "Size")
        ' 
        ' BtnArtRight
        ' 
        BtnArtRight.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnArtRight.Enabled = False
        TipStatus.SetImage(BtnArtRight, Nothing)
        BtnArtRight.Image = My.Resources.Resources.ImageMoveRight
        TipInfo.SetImage(BtnArtRight, Nothing)
        BtnArtRight.Location = New Point(679, 433)
        BtnArtRight.Name = "BtnArtRight"
        BtnArtRight.Size = New Size(32, 32)
        BtnArtRight.TabIndex = 5123
        BtnArtRight.TabStop = False
        TipInfo.SetText(BtnArtRight, "Next Image")
        TipStatus.SetText(BtnArtRight, Nothing)
        BtnArtRight.UseVisualStyleBackColor = True
        ' 
        ' BtnArtLeft
        ' 
        BtnArtLeft.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnArtLeft.Enabled = False
        TipStatus.SetImage(BtnArtLeft, Nothing)
        BtnArtLeft.Image = My.Resources.Resources.ImageMoveLeft
        TipInfo.SetImage(BtnArtLeft, Nothing)
        BtnArtLeft.Location = New Point(648, 433)
        BtnArtLeft.Name = "BtnArtLeft"
        BtnArtLeft.Size = New Size(32, 32)
        BtnArtLeft.TabIndex = 5122
        BtnArtLeft.TabStop = False
        TipInfo.SetText(BtnArtLeft, "Previous Image")
        TipStatus.SetText(BtnArtLeft, Nothing)
        BtnArtLeft.UseVisualStyleBackColor = True
        ' 
        ' BtnArtKeepOriginal
        ' 
        BtnArtKeepOriginal.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnArtKeepOriginal.Enabled = False
        TipStatus.SetImage(BtnArtKeepOriginal, Nothing)
        BtnArtKeepOriginal.Image = My.Resources.Resources.ImageUndo
        TipInfo.SetImage(BtnArtKeepOriginal, Nothing)
        BtnArtKeepOriginal.Location = New Point(790, 433)
        BtnArtKeepOriginal.Name = "BtnArtKeepOriginal"
        BtnArtKeepOriginal.Size = New Size(32, 32)
        BtnArtKeepOriginal.TabIndex = 5124
        BtnArtKeepOriginal.TabStop = False
        TipInfo.SetText(BtnArtKeepOriginal, "Undo")
        TipStatus.SetText(BtnArtKeepOriginal, Nothing)
        BtnArtKeepOriginal.UseVisualStyleBackColor = True
        ' 
        ' BtnArtRemove
        ' 
        BtnArtRemove.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnArtRemove.Enabled = False
        TipStatus.SetImage(BtnArtRemove, Nothing)
        BtnArtRemove.Image = My.Resources.Resources.ImageClearRemoveDelete16
        TipInfo.SetImage(BtnArtRemove, Nothing)
        BtnArtRemove.Location = New Point(543, 433)
        BtnArtRemove.Name = "BtnArtRemove"
        BtnArtRemove.Size = New Size(32, 32)
        BtnArtRemove.TabIndex = 5126
        BtnArtRemove.TabStop = False
        TipInfo.SetText(BtnArtRemove, "Remove Image")
        TipStatus.SetText(BtnArtRemove, Nothing)
        BtnArtRemove.UseVisualStyleBackColor = True
        ' 
        ' BtnArtNewFromClipboard
        ' 
        BtnArtNewFromClipboard.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TipStatus.SetImage(BtnArtNewFromClipboard, Nothing)
        BtnArtNewFromClipboard.Image = My.Resources.Resources.ImageEditPaste16
        TipInfo.SetImage(BtnArtNewFromClipboard, Nothing)
        BtnArtNewFromClipboard.Location = New Point(444, 433)
        BtnArtNewFromClipboard.Name = "BtnArtNewFromClipboard"
        BtnArtNewFromClipboard.Size = New Size(32, 32)
        BtnArtNewFromClipboard.TabIndex = 5125
        BtnArtNewFromClipboard.TabStop = False
        TipInfo.SetText(BtnArtNewFromClipboard, "Add Image From Clipboard" & vbCrLf & "LeftClick = Insert At Current Image" & vbCrLf & "RightClick = Insert Last")
        TipStatus.SetText(BtnArtNewFromClipboard, Nothing)
        BtnArtNewFromClipboard.UseVisualStyleBackColor = True
        ' 
        ' BtnArtNewFromFile
        ' 
        BtnArtNewFromFile.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TipStatus.SetImage(BtnArtNewFromFile, Nothing)
        BtnArtNewFromFile.Image = My.Resources.Resources.ImageGetPath16
        TipInfo.SetImage(BtnArtNewFromFile, Nothing)
        BtnArtNewFromFile.Location = New Point(475, 433)
        BtnArtNewFromFile.Name = "BtnArtNewFromFile"
        BtnArtNewFromFile.Size = New Size(32, 32)
        BtnArtNewFromFile.TabIndex = 5127
        BtnArtNewFromFile.TabStop = False
        TipInfo.SetText(BtnArtNewFromFile, "Add Image From File" & vbCrLf & "LeftClick = Insert At Current Image" & vbCrLf & "RightClick = Insert Last")
        TipStatus.SetText(BtnArtNewFromFile, Nothing)
        BtnArtNewFromFile.UseVisualStyleBackColor = True
        ' 
        ' BtnArtNewFromOnline
        ' 
        BtnArtNewFromOnline.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TipStatus.SetImage(BtnArtNewFromOnline, Nothing)
        BtnArtNewFromOnline.Image = My.Resources.Resources.ImageOnline16
        TipInfo.SetImage(BtnArtNewFromOnline, Nothing)
        BtnArtNewFromOnline.Location = New Point(506, 433)
        BtnArtNewFromOnline.Name = "BtnArtNewFromOnline"
        BtnArtNewFromOnline.Size = New Size(32, 32)
        BtnArtNewFromOnline.TabIndex = 5128
        BtnArtNewFromOnline.TabStop = False
        TipInfo.SetText(BtnArtNewFromOnline, "Add Image From Online" & vbCrLf & "LeftClick = Insert At Current Image" & vbCrLf & "RightClick = Insert Last")
        TipStatus.SetText(BtnArtNewFromOnline, Nothing)
        BtnArtNewFromOnline.UseVisualStyleBackColor = True
        ' 
        ' LblArt
        ' 
        TipStatus.SetImage(LblArt, Nothing)
        TipInfo.SetImage(LblArt, Nothing)
        LblArt.Location = New Point(446, 9)
        LblArt.Name = "LblArt"
        LblArt.Size = New Size(375, 23)
        LblArt.TabIndex = 5556
        TipInfo.SetText(LblArt, Nothing)
        LblArt.Text = "Artwork"
        TipStatus.SetText(LblArt, Nothing)
        ' 
        ' TipStatus
        ' 
        TipStatus.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipStatus.HideDelay = 5000
        ' 
        ' TagEditor
        ' 
        AllowDrop = True
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(833, 474)
        Controls.Add(BtnArtNewFromOnline)
        Controls.Add(BtnArtNewFromFile)
        Controls.Add(BtnArtRemove)
        Controls.Add(BtnArtNewFromClipboard)
        Controls.Add(BtnArtKeepOriginal)
        Controls.Add(BtnArtRight)
        Controls.Add(BtnArtLeft)
        Controls.Add(PicBoxArt)
        Controls.Add(CoBoxArtType)
        Controls.Add(TxtBoxArtDescription)
        Controls.Add(BtnTrackKeepOriginal)
        Controls.Add(TxtBoxTrack)
        Controls.Add(TxtBoxGenre)
        Controls.Add(BtnCommentsKeepOriginal)
        Controls.Add(BtnTracksKeepOriginal)
        Controls.Add(BtnYearKeepOriginal)
        Controls.Add(BtnGenreKeepOriginal)
        Controls.Add(CoBoxGenre)
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
        Controls.Add(LblGenre)
        Controls.Add(TxtBoxComments)
        Controls.Add(TxtBoxTracks)
        Controls.Add(TxtBoxYear)
        Controls.Add(LblYear)
        Controls.Add(LblTracks)
        Controls.Add(LblComments)
        Controls.Add(LblTrack)
        Controls.Add(LblArt)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipStatus.SetImage(Me, Nothing)
        TipInfo.SetImage(Me, Nothing)
        Margin = New Padding(4)
        MaximizeBox = False
        Name = "TagEditor"
        SizeGripStyle = SizeGripStyle.Hide
        StartPosition = FormStartPosition.CenterParent
        TipInfo.SetText(Me, Nothing)
        TipStatus.SetText(Me, Nothing)
        Text = "Tag Editor"
        CType(PicBoxArt, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents LblArtist As Skye.UI.Label
    Friend WithEvents TxtBoxTitle As TextBox
    Friend WithEvents LblTitle As Skye.UI.Label
    Friend WithEvents LbLAlbum As Skye.UI.Label
    Friend WithEvents TxtBoxAlbum As TextBox
    Friend WithEvents BtnSave As Button
    Friend WithEvents BtnOK As Button
    Friend WithEvents TipInfo As Skye.UI.ToolTipEX
    Friend WithEvents btnArtistKeepOriginal As Button
    Friend WithEvents BtnTitleKeepOriginal As Button
    Friend WithEvents BtnAlbumKeepOriginal As Button
    Friend WithEvents CMBasic As Skye.UI.TextBoxContextMenu
    Friend WithEvents TipStatus As Skye.UI.ToolTipEX
    Friend WithEvents CoBoxGenre As ComboBox
    Friend WithEvents LblGenre As Skye.UI.Label
    Friend WithEvents TxtBoxComments As TextBox
    Friend WithEvents TxtBoxTracks As TextBox
    Friend WithEvents TxtBoxYear As TextBox
    Friend WithEvents LblYear As Skye.UI.Label
    Friend WithEvents LblTracks As Skye.UI.Label
    Friend WithEvents LblComments As Skye.UI.Label
    Friend WithEvents BtnGenreKeepOriginal As Button
    Friend WithEvents BtnYearKeepOriginal As Button
    Friend WithEvents BtnTracksKeepOriginal As Button
    Friend WithEvents BtnCommentsKeepOriginal As Button
    Friend WithEvents TxtBoxGenre As TextBox
    Friend WithEvents BtnTrackKeepOriginal As Button
    Friend WithEvents TxtBoxTrack As TextBox
    Friend WithEvents LblTrack As Skye.UI.Label
    Friend WithEvents TxtBoxArtDescription As TextBox
    Friend WithEvents CoBoxArtType As ComboBox
    Friend WithEvents PicBoxArt As PictureBox
    Friend WithEvents BtnArtRight As Button
    Friend WithEvents BtnArtLeft As Button
    Friend WithEvents BtnArtKeepOriginal As Button
    Friend WithEvents BtnArtRemove As Button
    Friend WithEvents BtnArtNewFromClipboard As Button
    Friend WithEvents BtnArtNewFromFile As Button
    Friend WithEvents BtnArtNewFromOnline As Button
    Friend WithEvents LblArt As Skye.UI.Label
    Friend WithEvents TxtBoxArtist As TextBox
End Class
