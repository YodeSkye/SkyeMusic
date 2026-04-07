
Public Class CompanionClients

    Public Sub New()
        InitializeComponent()
        RefreshClientList()
    End Sub

    Private Sub RefreshClientList()
        LVClients.Items.Clear()

        Dim clients = App.CompanionControlServer.GetClients()

        For Each info In clients
            Dim item As New ListViewItem(info.Name)
            item.SubItems.Add(info.IP)
            item.SubItems.Add(info.ConnectedAt.ToString("g"))
            item.SubItems.Add(info.LastMessageAt.ToString("g"))
            LVClients.Items.Add(item)
        Next
    End Sub

End Class
