Imports DevExpress.Mvvm

Namespace MVVMDemo.ViewModelsInteraction
    Public Class DetailViewModel
        Inherits ViewModelBase

        Public Property DetailInfo() As String
            Get
                Return GetProperty(Function() DetailInfo)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() DetailInfo, value)
            End Set
        End Property

        Protected Overrides Sub OnParameterChanged(ByVal parameter As Object)
            MyBase.OnParameterChanged(parameter)
            DetailInfo = String.Format("Detail for {0} item", parameter)
        End Sub
    End Class
End Namespace
