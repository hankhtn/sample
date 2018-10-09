Imports System.IO
Imports System.Reflection

Namespace ProductsDemo
    Public NotInheritable Class ModelAssemblyHelper

        Private Sub New()
        End Sub

        Private Shared assembly As System.Reflection.Assembly = Nothing
        Public Shared ReadOnly Property CurrentAssembly() As System.Reflection.Assembly
            Get
                If assembly Is Nothing Then
                    assembly = GetType(ModelAssemblyHelper).Assembly
                End If
                Return assembly
            End Get
        End Property
        Public Shared Function GetResourcePath(ByVal resourceName As String) As String
            Return "ProductsDemo.SalesDemoResources." & resourceName
        End Function
        Public Shared Function GetResourceStream(ByVal resourceName As String) As Stream
            Return CurrentAssembly.GetManifestResourceStream(GetResourcePath(resourceName))
        End Function
    End Class
End Namespace
