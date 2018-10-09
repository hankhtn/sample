Imports DevExpress.Mvvm

Namespace MVVMDemo.Services.DocumentManager
    Public Class DetailViewModel
        Implements ISupportParameter

        Public Overridable Property Person() As PersonInfo

        Private _Parameter As Object
        Private Property ISupportParameter_Parameter() As Object Implements ISupportParameter.Parameter
            Get
                Return _Parameter
            End Get
            Set(ByVal value As Object)
                _Parameter = value
                Person = DirectCast(_Parameter, PersonInfo)
            End Set
        End Property
    End Class
End Namespace
