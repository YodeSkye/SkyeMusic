Imports Microsoft.VisualBasic.ApplicationServices

Namespace My

    Partial Friend Class MyApplication

        Friend appsplash As New Splash

        Protected Overrides Sub OnCreateSplashScreen()
#If Not DEBUG Then
            appsplash.ShowSplash(Resources.ImageAppRed32, "Skye Music", "")
#End If
        End Sub
        Protected Overrides Function OnStartup(e As ApplicationServices.StartupEventArgs) As Boolean
            If e.Cancel Then : Return False
            Else
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High
                My.App.InitializeAppPreStartup()
                Return True
            End If
        End Function

    End Class

End Namespace
