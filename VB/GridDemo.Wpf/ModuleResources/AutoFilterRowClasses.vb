Imports System
Imports System.Windows
Imports System.Windows.Data

Namespace GridDemo
    Public Class AutoFilterConditionVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim strValue As String = DirectCast(value, String)
            Return If(strValue = "ToId" OrElse strValue = "HasAttachment" OrElse strValue = "Sent", Visibility.Collapsed, Visibility.Visible)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
