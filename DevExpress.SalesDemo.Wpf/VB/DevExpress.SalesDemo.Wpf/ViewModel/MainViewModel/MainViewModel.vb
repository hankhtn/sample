Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Windows

Namespace DevExpress.SalesDemo.Wpf.ViewModel
    Public Class MainViewModel
        Public Shared Function Create() As MainViewModel
            Return ViewModelSource.Create(Function() New MainViewModel())
        End Function
        Protected Sub New()
            LoginViewModel = LoginViewModel.Create()
            NavigationViewModel = NavigationViewModel.Create()
        End Sub

        Public Overridable Property LoginViewModel() As LoginViewModel
        Public Overridable Property NavigationViewModel() As NavigationViewModel
        Protected Overridable ReadOnly Property DialogService() As IDialogService
            Get
                Return Nothing
            End Get
        End Property
        Public Sub ShowLoginView()
            Dim vm = LoginViewModel.Clone(LoginViewModel)
            Me.GetService(Of IDialogService)().ShowDialog(MessageButton.OK, ApplicationSettings.Default.MainWindowTitle, vm)
            LoginViewModel = vm
        End Sub
    End Class
End Namespace
