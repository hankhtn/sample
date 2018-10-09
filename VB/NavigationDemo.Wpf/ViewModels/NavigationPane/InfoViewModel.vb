Imports DevExpress.Mvvm

Namespace NavigationDemo
    Public Class InfoViewModel
        Implements ISupportParameter

        Public Overridable Property Parameter() As Object

        #Region "ISupportParameter"
        Private Property ISupportParameter_Parameter() As Object Implements ISupportParameter.Parameter
            Get
                Return Parameter
            End Get
            Set(ByVal value As Object)
                Parameter = value
            End Set
        End Property
        #End Region
    End Class
End Namespace
