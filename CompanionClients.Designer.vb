<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompanionClients
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CompanionClients))
        LVClients = New Skye.UI.ListViewEX()
        DeviceName = New ColumnHeader()
        IPAddress = New ColumnHeader()
        ConnectedAt = New ColumnHeader()
        LastMessage = New ColumnHeader()
        SuspendLayout()
        ' 
        ' LVClients
        ' 
        LVClients.Columns.AddRange(New ColumnHeader() {DeviceName, IPAddress, ConnectedAt, LastMessage})
        LVClients.Dock = DockStyle.Fill
        LVClients.EditableColumns = CType(resources.GetObject("LVClients.EditableColumns"), List(Of Boolean))
        LVClients.FullRowSelect = True
        LVClients.InsertionLineColor = Color.Teal
        LVClients.Location = New Point(0, 0)
        LVClients.Name = "LVClients"
        LVClients.Size = New Size(609, 168)
        LVClients.TabIndex = 0
        LVClients.UseCompatibleStateImageBehavior = False
        LVClients.View = View.Details
        ' 
        ' DeviceName
        ' 
        DeviceName.Text = "Device Name"
        DeviceName.Width = 125
        ' 
        ' IPAddress
        ' 
        IPAddress.Text = "IP"
        IPAddress.TextAlign = HorizontalAlignment.Center
        IPAddress.Width = 120
        ' 
        ' ConnectedAt
        ' 
        ConnectedAt.Text = "Connected At"
        ConnectedAt.TextAlign = HorizontalAlignment.Center
        ConnectedAt.Width = 180
        ' 
        ' LastMessage
        ' 
        LastMessage.Text = "Last Message"
        LastMessage.TextAlign = HorizontalAlignment.Center
        LastMessage.Width = 180
        ' 
        ' CompanionClients
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(609, 168)
        Controls.Add(LVClients)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4)
        Name = "CompanionClients"
        StartPosition = FormStartPosition.CenterParent
        Text = "Companion Clients"
        ResumeLayout(False)
    End Sub

    Friend WithEvents LVClients As Skye.UI.ListViewEX
    Friend WithEvents DeviceName As ColumnHeader
    Friend WithEvents IPAddress As ColumnHeader
    Friend WithEvents ConnectedAt As ColumnHeader
    Friend WithEvents LastMessage As ColumnHeader
End Class
