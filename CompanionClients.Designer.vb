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
        CMIClientsDisconnect = New ToolStripMenuItem()
        CMIClientsCopyDeviceName = New ToolStripMenuItem()
        CMIClientsCopyIP = New ToolStripMenuItem()
        CMIClientsRefresh = New ToolStripMenuItem()
        TimerRefresh = New Timer(components)
        TCClients = New Syncfusion.Windows.Forms.Tools.TabControlAdv()
        TPClients = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        TPListeners = New Syncfusion.Windows.Forms.Tools.TabPageAdv()
        LVListeners = New Skye.UI.ListViewEX()
        ColumnHeader2 = New ColumnHeader()
        ColumnHeader3 = New ColumnHeader()
        ColumnHeader5 = New ColumnHeader()
        ColumnHeader6 = New ColumnHeader()
        ColumnHeader7 = New ColumnHeader()
        CMListeners = New ContextMenuStrip(components)
        CMIListenersDisconnect = New ToolStripMenuItem()
        CMIListenersCopyIP = New ToolStripMenuItem()
        CMIListenersRefresh = New ToolStripMenuItem()
        CMClients.SuspendLayout()
        CType(TCClients, ComponentModel.ISupportInitialize).BeginInit()
        TCClients.SuspendLayout()
        TPClients.SuspendLayout()
        TPListeners.SuspendLayout()
        CMListeners.SuspendLayout()
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
        LVClients.Size = New Size(914, 394)
        LVClients.TabIndex = 0
        LVClients.UseCompatibleStateImageBehavior = False
        LVClients.View = View.Details
        ' 
        ' DeviceName
        ' 
        DeviceName.Text = "Device Name"
        DeviceName.Width = 310
        ' 
        ' IPAddress
        ' 
        IPAddress.Text = "IP"
        IPAddress.TextAlign = HorizontalAlignment.Center
        IPAddress.Width = 200
        ' 
        ' ConnectedAt
        ' 
        ConnectedAt.Text = "Connected At"
        ConnectedAt.TextAlign = HorizontalAlignment.Center
        ConnectedAt.Width = 200
        ' 
        ' LastMessage
        ' 
        LastMessage.Text = "Last Message"
        LastMessage.TextAlign = HorizontalAlignment.Center
        LastMessage.Width = 200
        ' 
        ' CMClients
        ' 
        CMClients.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CMClients.Items.AddRange(New ToolStripItem() {CMIClientsDisconnect, CMIClientsCopyDeviceName, CMIClientsCopyIP, CMIClientsRefresh})
        CMClients.Name = "CMClients"
        CMClients.Size = New Size(188, 92)
        ' 
        ' CMIClientsDisconnect
        ' 
        CMIClientsDisconnect.Image = My.Resources.Resources.ImageNetwork16
        CMIClientsDisconnect.Name = "CMIClientsDisconnect"
        CMIClientsDisconnect.Size = New Size(187, 22)
        CMIClientsDisconnect.Text = "Disconnect"
        ' 
        ' CMIClientsCopyDeviceName
        ' 
        CMIClientsCopyDeviceName.Image = My.Resources.Resources.ImageCopy16
        CMIClientsCopyDeviceName.Name = "CMIClientsCopyDeviceName"
        CMIClientsCopyDeviceName.Size = New Size(187, 22)
        CMIClientsCopyDeviceName.Text = "Copy Device Name"
        ' 
        ' CMIClientsCopyIP
        ' 
        CMIClientsCopyIP.Image = My.Resources.Resources.ImageCopy16
        CMIClientsCopyIP.Name = "CMIClientsCopyIP"
        CMIClientsCopyIP.Size = New Size(187, 22)
        CMIClientsCopyIP.Text = "Copy IP Address"
        ' 
        ' CMIClientsRefresh
        ' 
        CMIClientsRefresh.Image = My.Resources.Resources.ImageRefresh16
        CMIClientsRefresh.Name = "CMIClientsRefresh"
        CMIClientsRefresh.Size = New Size(187, 22)
        CMIClientsRefresh.Text = "Refresh"
        ' 
        ' TimerRefresh
        ' 
        TimerRefresh.Enabled = True
        TimerRefresh.Interval = 3000
        ' 
        ' TCClients
        ' 
        TCClients.ActiveTabFont = New Font("Segoe UI", 14.25F, FontStyle.Bold)
        TCClients.BeforeTouchSize = New Size(917, 438)
        TCClients.Controls.Add(TPClients)
        TCClients.Controls.Add(TPListeners)
        TCClients.Dock = DockStyle.Fill
        TCClients.Location = New Point(0, 0)
        TCClients.Name = "TCClients"
        TCClients.Size = New Size(917, 438)
        TCClients.TabIndex = 1
        TCClients.TabStop = False
        TCClients.TabStyle = GetType(Syncfusion.Windows.Forms.Tools.TabRendererWhidbey)
        TCClients.ThemeName = "TabRendererWhidbey"
        ' 
        ' TPClients
        ' 
        TPClients.Controls.Add(LVClients)
        TPClients.Image = Nothing
        TPClients.ImageSize = New Size(16, 16)
        TPClients.Location = New Point(1, 42)
        TPClients.Name = "TPClients"
        TPClients.ShowCloseButton = True
        TPClients.Size = New Size(914, 394)
        TPClients.TabIndex = 1
        TPClients.Text = " Clients "
        TPClients.ThemesEnabled = False
        ' 
        ' TPListeners
        ' 
        TPListeners.Controls.Add(LVListeners)
        TPListeners.Image = Nothing
        TPListeners.ImageSize = New Size(16, 16)
        TPListeners.Location = New Point(1, 42)
        TPListeners.Name = "TPListeners"
        TPListeners.ShowCloseButton = True
        TPListeners.Size = New Size(914, 394)
        TPListeners.TabIndex = 2
        TPListeners.Text = " Listeners "
        TPListeners.ThemesEnabled = False
        ' 
        ' LVListeners
        ' 
        LVListeners.AllowColumnReorder = True
        LVListeners.Columns.AddRange(New ColumnHeader() {ColumnHeader2, ColumnHeader3, ColumnHeader5, ColumnHeader6, ColumnHeader7})
        LVListeners.ContextMenuStrip = CMListeners
        LVListeners.Dock = DockStyle.Fill
        LVListeners.EditableColumns = CType(resources.GetObject("LVListeners.EditableColumns"), List(Of Boolean))
        LVListeners.FullRowSelect = True
        LVListeners.HeaderStyle = ColumnHeaderStyle.Nonclickable
        LVListeners.InsertionLineColor = Color.Teal
        LVListeners.Location = New Point(0, 0)
        LVListeners.MultiSelect = False
        LVListeners.Name = "LVListeners"
        LVListeners.OwnerDraw = True
        LVListeners.Size = New Size(914, 394)
        LVListeners.TabIndex = 1
        LVListeners.UseCompatibleStateImageBehavior = False
        LVListeners.View = View.Details
        ' 
        ' ColumnHeader2
        ' 
        ColumnHeader2.Text = "IP"
        ColumnHeader2.TextAlign = HorizontalAlignment.Center
        ColumnHeader2.Width = 190
        ' 
        ' ColumnHeader3
        ' 
        ColumnHeader3.Text = "Connected At"
        ColumnHeader3.TextAlign = HorizontalAlignment.Center
        ColumnHeader3.Width = 180
        ' 
        ' ColumnHeader5
        ' 
        ColumnHeader5.Text = "Duration"
        ColumnHeader5.TextAlign = HorizontalAlignment.Center
        ColumnHeader5.Width = 100
        ' 
        ' ColumnHeader6
        ' 
        ColumnHeader6.Text = "Data Streamed"
        ColumnHeader6.TextAlign = HorizontalAlignment.Center
        ColumnHeader6.Width = 140
        ' 
        ' ColumnHeader7
        ' 
        ColumnHeader7.Text = "User-Agent"
        ColumnHeader7.Width = 300
        ' 
        ' CMListeners
        ' 
        CMListeners.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CMListeners.Items.AddRange(New ToolStripItem() {CMIListenersDisconnect, CMIListenersCopyIP, CMIListenersRefresh})
        CMListeners.Name = "CMClients"
        CMListeners.Size = New Size(173, 70)
        ' 
        ' CMIListenersDisconnect
        ' 
        CMIListenersDisconnect.Image = My.Resources.Resources.ImageNetwork16
        CMIListenersDisconnect.Name = "CMIListenersDisconnect"
        CMIListenersDisconnect.Size = New Size(172, 22)
        CMIListenersDisconnect.Text = "Disconnect"
        ' 
        ' CMIListenersCopyIP
        ' 
        CMIListenersCopyIP.Image = My.Resources.Resources.ImageCopy16
        CMIListenersCopyIP.Name = "CMIListenersCopyIP"
        CMIListenersCopyIP.Size = New Size(172, 22)
        CMIListenersCopyIP.Text = "Copy IP Address"
        ' 
        ' CMIListenersRefresh
        ' 
        CMIListenersRefresh.Image = My.Resources.Resources.ImageRefresh16
        CMIListenersRefresh.Name = "CMIListenersRefresh"
        CMIListenersRefresh.Size = New Size(172, 22)
        CMIListenersRefresh.Text = "Refresh"
        ' 
        ' CompanionClients
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(917, 438)
        Controls.Add(TCClients)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        Margin = New Padding(4)
        MaximizeBox = False
        Name = "CompanionClients"
        StartPosition = FormStartPosition.Manual
        Text = "Companion Clients & Stream Listeners"
        CMClients.ResumeLayout(False)
        CType(TCClients, ComponentModel.ISupportInitialize).EndInit()
        TCClients.ResumeLayout(False)
        TPClients.ResumeLayout(False)
        TPListeners.ResumeLayout(False)
        CMListeners.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents LVClients As Skye.UI.ListViewEX
    Friend WithEvents DeviceName As ColumnHeader
    Friend WithEvents IPAddress As ColumnHeader
    Friend WithEvents ConnectedAt As ColumnHeader
    Friend WithEvents LastMessage As ColumnHeader
    Friend WithEvents CMClients As ContextMenuStrip
    Friend WithEvents TimerRefresh As Timer
    Friend WithEvents CMIClientsDisconnect As ToolStripMenuItem
    Friend WithEvents CMIClientsCopyDeviceName As ToolStripMenuItem
    Friend WithEvents CMIClientsCopyIP As ToolStripMenuItem
    Friend WithEvents CMIClientsRefresh As ToolStripMenuItem
    Friend WithEvents TCClients As Syncfusion.Windows.Forms.Tools.TabControlAdv
    Friend WithEvents TPClients As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents TPListeners As Syncfusion.Windows.Forms.Tools.TabPageAdv
    Friend WithEvents LVListeners As Skye.UI.ListViewEX
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents CMListeners As ContextMenuStrip
    Friend WithEvents CMIListenersDisconnect As ToolStripMenuItem
    Friend WithEvents CMIListenersCopyIP As ToolStripMenuItem
    Friend WithEvents CMIListenersRefresh As ToolStripMenuItem
End Class
