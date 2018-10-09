Imports System
Imports System.Windows.Data

Namespace CommonDemo.Helpers
    Public Class WindowShowTitleAndIconConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
            Return DirectCast(values(0), Boolean)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Return New Object() { DirectCast(value, Boolean), DirectCast(value, Boolean) }
        End Function
    End Class
End Namespace
