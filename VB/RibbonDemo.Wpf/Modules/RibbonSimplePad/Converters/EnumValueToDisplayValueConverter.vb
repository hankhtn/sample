Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Data

Namespace RibbonDemo
    Public Class EnumValueToDisplayValueConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return DependencyProperty.UnsetValue
            End If
            Dim customAttributes As IEnumerable(Of Attribute) = value.GetType().GetField(value.ToString()).GetCustomAttributes(False).Cast(Of Attribute)()
            Dim displayAttribute As DisplayAttribute = TryCast(customAttributes.FirstOrDefault(Function(attribute) TypeOf attribute Is DisplayAttribute), DisplayAttribute)
            Return If(displayAttribute Is Nothing, System.Enum.GetName(value.GetType(), value), displayAttribute.Name)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
