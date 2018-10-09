Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media.Imaging

Namespace ProductsDemo
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
    Public Class ResourceImageConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim image As String = If(TryCast(parameter, String), TryCast(value, String))
            If image Is Nothing Then
                Throw New NullReferenceException("parameter")
            End If
            Return ResourceImageHelper.GetResourceImage(image)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
