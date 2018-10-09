Imports DevExpress.Mvvm

' This class demonstrates the code that will be generated by the POCO mechanism at runtime

Namespace MVVMDemo.POCOViewModels
    Public Class ServicesViewModel_RuntimeGeneratedCode
        Inherits ServicesViewModel
        Implements ISupportServices

        Private _ServiceContainer As IServiceContainer = Nothing
        Private ReadOnly Property ISupportServices_ServiceContainer() As IServiceContainer Implements ISupportServices.ServiceContainer
            Get
                If _ServiceContainer Is Nothing Then
                    _ServiceContainer = New ServiceContainer(Me)
                End If
                Return _ServiceContainer
            End Get
        End Property
    End Class
End Namespace
