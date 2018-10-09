Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Namespace RibbonDemo
    Public Class EnumToIndexConverter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return DependencyProperty.UnsetValue
            End If
            Return DirectCast(value, Integer)
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return System.Enum.ToObject(targetType, DirectCast(value, Integer))
        End Function
        #End Region
    End Class
    Public Class ObjectsEqualityConverter
        Implements IValueConverter

        Public Property Inverse() As Boolean

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return value
            End If
            Dim result = String.Equals(value.ToString(), parameter)
            Return If(Inverse, (Not result), result)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(DirectCast(value, Boolean), parameter, Nothing)
        End Function
    End Class
End Namespace
