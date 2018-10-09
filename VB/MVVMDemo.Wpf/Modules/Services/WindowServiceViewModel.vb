Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Windows

Namespace MVVMDemo.Services
    Public Class WindowServiceViewModel
        Public Sub ShowRegistrationForm()
            Dim detailViewModel As WindowServiceDetailViewModel = WindowServiceDetailViewModel.Create()

            Dim service As IWindowService = Me.GetRequiredService(Of IWindowService)()
            service.Title = "Registration Form"
            service.Show(viewModel:= detailViewModel)
        End Sub
    End Class
End Namespace
