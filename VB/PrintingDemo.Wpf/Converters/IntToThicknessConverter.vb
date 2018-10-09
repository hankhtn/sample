Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Data

Namespace PrintingDemo
    Public Class IntToThicknessConverter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim thickness As Thickness = DirectCast(value, Thickness)
            Return thickness.Bottom
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim doubleValue As Double = System.Convert.ToDouble(value)
            Return New Thickness(doubleValue, 0, 0, doubleValue)
        End Function

        #End Region
    End Class

    Public Class ThicknessConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
           Dim thickness As Thickness = DirectCast(value, Thickness)
            If parameter IsNot Nothing AndAlso parameter.Equals("right") Then
               Return New Thickness(thickness.Bottom, 0, thickness.Bottom, thickness.Bottom)
            End If
            Return thickness
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

End Namespace
