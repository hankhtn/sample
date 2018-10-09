Imports DevExpress.SalesDemo.Model
Imports System.Windows.Media.Imaging

Namespace DevExpress.SalesDemo.Wpf.Helpers
    Public NotInheritable Class ResourceImageHelper

        Private Sub New()
        End Sub

        Public Shared Function GetResourceImage(ByVal image As String) As BitmapImage
            Dim res As New BitmapImage()
            res.BeginInit()
            res.StreamSource = ModelAssemblyHelper.GetResourceStream(image)
            res.EndInit()
            res.Freeze()
            Return res
        End Function
    End Class
End Namespace
