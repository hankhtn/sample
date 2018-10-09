Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace CommonDemo.TabControl.WebBrowser
    Public Class MainViewModel
        Public Sub LaunchDemo()
            Me.GetService(Of IWindowService)().Show("WebBrowserView", Nothing, Me)
        End Sub
    End Class
End Namespace
