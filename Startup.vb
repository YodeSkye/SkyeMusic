
'Friend Class Startup
'    Private Sub Startup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        WindowState = FormWindowState.Minimized
'        ShowInTaskbar = False
'        App.FrmPlayer = New Player()
'        App.FrmPlayer.Show()
'    End Sub
'    Protected Overrides Sub OnShown(e As EventArgs)
'        MyBase.OnShown(e)
'        Hide()
'    End Sub
'End Class

Imports System.Windows.Forms

Friend Class Startup

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            ' WS_EX_TOOLWINDOW (0x80) hides the window completely from Alt+Tab
            cp.ExStyle = cp.ExStyle Or &H80
            Return cp
        End Get
    End Property
    Public Sub New()
        ' Initialize designer components
        InitializeComponent()

        ' Prevent flash on screen
        Me.Opacity = 0
        Me.ShowInTaskbar = False
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New Drawing.Point(-2000, -2000) ' Push off-screen just in case
    End Sub
    Private Sub Startup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Spin up the real player form
        App.FrmPlayer = New Player()
        App.FrmPlayer.Show()
    End Sub

End Class
