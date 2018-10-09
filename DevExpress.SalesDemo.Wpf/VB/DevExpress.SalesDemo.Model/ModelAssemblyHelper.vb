Imports System.IO
Imports System.Reflection

Namespace DevExpress.SalesDemo.Model
    Public NotInheritable Class ModelAssemblyHelper

        Private Sub New()
        End Sub


        Private Shared modelAssembly_Renamed As System.Reflection.Assembly = Nothing
        Public Shared ReadOnly Property ModelAssembly() As System.Reflection.Assembly
            Get
                If modelAssembly_Renamed Is Nothing Then
                    modelAssembly_Renamed = GetType(ModelAssemblyHelper).Assembly
                End If
                Return modelAssembly_Renamed
            End Get
        End Property
        Private Shared resourceNames() As String = Nothing
        Public Shared Function GetResourcePath(ByVal resourceName As String) As String
            If resourceNames Is Nothing Then
                resourceNames = ModelAssembly.GetManifestResourceNames()
            End If
            For Each name As String In resourceNames
                If name.EndsWith(resourceName) Then
                    Return name
                End If
            Next name
            Return Nothing
        End Function
        Public Shared Function GetResourceStream(ByVal resourceName As String) As Stream
            Return ModelAssembly.GetManifestResourceStream(GetResourcePath(resourceName))
        End Function
    End Class
End Namespace
