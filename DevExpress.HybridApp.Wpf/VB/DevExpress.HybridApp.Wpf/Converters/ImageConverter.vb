Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Windows.Data
Imports DevExpress.Utils

Namespace DevExpress.DevAV.Converters
    Public Class ImageConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing Then
                Dim uri = AssemblyHelper.GetResourceUri(GetType(ImageConverter).Assembly, value.ToString())
                Return New System.Windows.Media.Imaging.BitmapImage(uri)
            End If
            Return Nothing
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
