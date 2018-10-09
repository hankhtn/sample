Imports System
Imports System.Windows.Data

Namespace PrintingDemo
    Public Class ReplaceStringConverter
        Implements IValueConverter

        Public Property OldValue() As String
        Public Property NewValue() As String

        Public Sub New()
            OldValue = Environment.NewLine
            NewValue = " "
        End Sub

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return DirectCast(value, String).Replace(OldValue, NewValue)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
