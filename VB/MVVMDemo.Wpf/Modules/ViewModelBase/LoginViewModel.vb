Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace MVVMDemo.ViewModelBaseDemo
    Public Class LoginViewModel
        Inherits ViewModelBase

        Public Property UserName() As String
            Get
                Return GetProperty(Function() UserName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() UserName, value)
            End Set
        End Property
        Public Property Status() As String
            Get
                Return GetProperty(Function() Status)
            End Get
            Private Set(ByVal value As String)
                SetProperty(Function() Status, value)
            End Set
        End Property

        <Command>
        Public Sub Login()
            Status = "User: " & UserName
        End Sub
        Public Function CanLogin() As Boolean
            Return Not String.IsNullOrEmpty(UserName)
        End Function
    End Class
End Namespace
