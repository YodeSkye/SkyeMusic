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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CompanionClients))
        LVClients = New Skye.UI.ListViewEX()
        DeviceName = New ColumnHeader()
        IPAddress = New ColumnHeader()
        ConnectedAt = New ColumnHeader()
        LastMessage = New ColumnHeader()
        CMClients = New ContextMenuStrip(components)
        CMIDisconnect = New ToolStripMenuItem()
        CMICopyDeviceName = New ToolStripMenuItem()
        CMICopyIP = New ToolStripMenuItem()
        CMIRefresh = New ToolStripMenuItem()
        TimerRefresh = New Timer(components)
        CMClients.SuspendLayout()
        SuspendLayout()
        ' 
        ' LVClients
        ' 
        LVClients.AllowColumnReorder = True
        LVClients.Columns.AddRange(New ColumnHeader() {DeviceName, IPAddress, ConnectedAt, LastMessage})
        LVClients.ContextMenuStrip = CMClients
        LVClients.Dock = DockStyle.Fill
        LVClients.EditableColumns = CType(resources.GetObject("LVClients.EditableColumns"), List(Of Boolean))
        LVClients.FullRowSelect = True
        LVClients.HeaderStyle = ColumnHeaderStyle.Nonclickable
        LVClients.InsertionLineColor = Color.Teal
        LVClients.Location = New Point(0, 0)
        LVClients.MultiSelect = False
        LVClients.Name = "LVClients"
        LVClients.OwnerDraw = True
        LVClients.Size = New Size(659, 168)
        LVClients.TabIndex = 0
        LVClients.UseCompatibleStateImageBehavior = False
        LVClients.View = View.Details
        ' 
        ' DeviceName
        ' 
        DeviceName.Text = "Device Name"
        DeviceName.Width = 175
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
        ' CMClients
        ' 
        CMClients.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CMClients.Items.AddRange(New ToolStripItem() {CMIDisconnect, CMICopyDeviceName, CMICopyIP, CMIRefresh})
        CMClients.Name = "CMClients"
        CMClients.Size = New Size(188, 92)
        ' 
        ' CMIDisconnect
        ' 
        CMIDisconnect.Image = My.Resources.Resources.ImageNetwork16
        CMIDisconnect.Name = "CMIDisconnect"
        CMIDisconnect.Size = New Size(187, 22)
        CMIDisconnect.Text = "Disconnect"
        ' 
        ' CMICopyDeviceName
        ' 
        CMICopyDeviceName.Image = My.Resources.Resources.ImageCopy16
        CMICopyDeviceName.Name = "CMICopyDeviceName"
        CMICopyDeviceName.Size = New Size(187, 22)
        CMICopyDeviceName.Text = "Copy Device Name"
        ' 
        ' CMICopyIP
        ' 
        CMICopyIP.Image = My.Resources.Resources.ImageCopy16
        CMICopyIP.Name = "CMICopyIP"
        CMICopyIP.Size = New Size(187, 22)
        CMICopyIP.Text = "Copy IP Address"
        ' 
        ' CMIRefresh
        ' 
        CMIRefresh.Image = My.Resources.Resources.ImageRefresh16
        CMIRefresh.Name = "CMIRefresh"
        CMIRefresh.Size = New Size(187, 22)
        CMIRefresh.Text = "Refresh"
        ' 
        ' TimerRefresh
        ' 
        TimerRefresh.Enabled = True
        TimerRefresh.Interval = 3000
        ' 
        ' CompanionClients
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(659, 168)
        Controls.Add(LVClients)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4)
        MaximizeBox = False
        Name = "CompanionClients"
        StartPosition = FormStartPosition.CenterParent
        Text = "Companion Clients"
        CMClients.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents LVClients As Skye.UI.ListViewEX
    Friend WithEvents DeviceName As ColumnHeader
    Friend WithEvents IPAddress As ColumnHeader
    Friend WithEvents ConnectedAt As ColumnHeader
    Friend WithEvents LastMessage As ColumnHeader
    Friend WithEvents CMClients As ContextMenuStrip
    Friend WithEvents TimerRefresh As Timer
    Friend WithEvents CMIDisconnect As ToolStripMenuItem
    Friend WithEvents CMICopyDeviceName As ToolStripMenuItem
    Friend WithEvents CMICopyIP As ToolStripMenuItem
    Friend WithEvents CMIRefresh As ToolStripMenuItem
End Class
