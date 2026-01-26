<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DirectoryStreamList
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DirectoryStreamList))
        LVStreams = New Skye.UI.ListViewEX()
        Format = New ColumnHeader()
        Bitrate = New ColumnHeader()
        URL = New ColumnHeader()
        BtnOK = New Button()
        CMStreamList = New ContextMenuStrip(components)
        CMICopy = New ToolStripMenuItem()
        CMStreamList.SuspendLayout()
        SuspendLayout()
        ' 
        ' LVStreams
        ' 
        LVStreams.Columns.AddRange(New ColumnHeader() {Format, Bitrate, URL})
        LVStreams.ContextMenuStrip = CMStreamList
        LVStreams.Dock = DockStyle.Top
        LVStreams.FullRowSelect = True
        LVStreams.HeaderStyle = ColumnHeaderStyle.Nonclickable
        LVStreams.InsertionLineColor = Color.Teal
        LVStreams.Location = New Point(0, 0)
        LVStreams.MultiSelect = False
        LVStreams.Name = "LVStreams"
        LVStreams.Size = New Size(904, 200)
        LVStreams.TabIndex = 1
        LVStreams.UseCompatibleStateImageBehavior = False
        LVStreams.View = View.Details
        ' 
        ' Format
        ' 
        Format.Text = "Format"
        Format.Width = 90
        ' 
        ' Bitrate
        ' 
        Bitrate.Text = "Bitrate"
        Bitrate.Width = 90
        ' 
        ' URL
        ' 
        URL.Text = "URL"
        URL.Width = 400
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(420, 214)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 2
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' CMStreamList
        ' 
        CMStreamList.Items.AddRange(New ToolStripItem() {CMICopy})
        CMStreamList.Name = "CMStreamList"
        CMStreamList.Size = New Size(127, 26)
        ' 
        ' CMICopy
        ' 
        CMICopy.Image = My.Resources.Resources.ImageCopy16
        CMICopy.Name = "CMICopy"
        CMICopy.Size = New Size(126, 22)
        CMICopy.Text = "Copy URL"
        ' 
        ' DirectoryStreamList
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(904, 290)
        Controls.Add(BtnOK)
        Controls.Add(LVStreams)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DirectoryStreamList"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Select a stream for this station..."
        CMStreamList.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents LVStreams As Skye.UI.ListViewEX
    Friend WithEvents BtnOK As Button
    Friend WithEvents Format As ColumnHeader
    Friend WithEvents Bitrate As ColumnHeader
    Friend WithEvents URL As ColumnHeader
    Friend WithEvents CMStreamList As ContextMenuStrip
    Friend WithEvents CMICopy As ToolStripMenuItem
End Class
