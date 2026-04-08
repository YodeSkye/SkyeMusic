
Public Class CompanionClients

    ' Form Events
    Public Sub New()
        InitializeComponent()
        RefreshClientList()
    End Sub

    ' Control Events
    Private Sub CMIDisconnect_Click(sender As Object, e As EventArgs) Handles CMIDisconnect.Click
        Dim info = GetSelectedClient()
        If info Is Nothing Then Exit Sub

        App.CompanionControlServer.DisconnectClient(info)
        RefreshClientList()

    End Sub
    Private Sub CMICopyDeviceName_Click(sender As Object, e As EventArgs) Handles CMICopyDeviceName.Click
        Dim info = GetSelectedClient()
        If info Is Nothing Then Exit Sub

        Clipboard.SetText(info.Name)

    End Sub
    Private Sub CMICopyIP_Click(sender As Object, e As EventArgs) Handles CMICopyIP.Click
        Dim info = GetSelectedClient()
        If info Is Nothing Then Exit Sub

        Clipboard.SetText(info.IP)

    End Sub
    Private Sub CMIRefresh_Click(sender As Object, e As EventArgs) Handles CMIRefresh.Click
        RefreshClientList()
    End Sub

    ' Handlers
    Private Sub TimerRefresh_Tick(sender As Object, e As EventArgs) Handles TimerRefresh.Tick
        If CMClients.Visible = False Then RefreshClientList()
    End Sub

    ' Methods
    Private Sub RefreshClientList()
        LVClients.Items.Clear()
        Dim clients = App.CompanionControlServer.GetClients()
        For Each info In clients
            Dim item As New ListViewItem(info.Name)
            item.SubItems.Add(info.IP)
            item.SubItems.Add(info.ConnectedAt.ToString("g"))
            item.SubItems.Add(info.LastMessageAt.ToString("g"))
            item.Tag = info
            LVClients.Items.Add(item)
        Next
        Debug.WriteLine($"Refreshed client list: {clients.Count} clients found.")
    End Sub
    Private Function GetSelectedClient() As CompanionClientInfo
        If LVClients.SelectedItems.Count = 0 Then Return Nothing
        Return DirectCast(LVClients.SelectedItems(0).Tag, CompanionClientInfo)
    End Function

End Class
