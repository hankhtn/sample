Imports System
Imports System.Windows.Data
Imports System.Windows.Media

Namespace PrintingDemo
    Public Class BoolToSolidColorBrush1Converter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim boolValue As Boolean = DirectCast(value, Boolean)
            If boolValue Then
                Return New SolidColorBrush(Color.FromArgb(&HFF, &H7B, &H99, &HC9))
            Else
                Return New SolidColorBrush(Color.FromArgb(&HFF, &HBB, &H8A, &H92))
            End If
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        #End Region
    End Class
End Namespace
