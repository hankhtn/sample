Imports System
Imports System.Windows
Imports System.Windows.Data

Namespace EditorsDemo.ModuleResources
    Public Class ResourceKeyThemeConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim temp As DevExpress.Xpf.Grid.Themes.GridRowThemeKeyExtension = DirectCast(values(1), DevExpress.Xpf.Grid.Themes.GridRowThemeKeyExtension)
            temp.ThemeName = If(values(2) IsNot Nothing AndAlso values(2).ToString()<> "DeepBlue", values(2).ToString(), Nothing)
            Return DirectCast(values(0), FrameworkElement).FindResource(temp)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class EnumBooleanConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim parameterString As String = TryCast(parameter, String)
            If parameterString Is Nothing Then
                Return DependencyProperty.UnsetValue
            End If
            If System.Enum.IsDefined(value.GetType(), value) = False Then
                Return DependencyProperty.UnsetValue
            End If
            Dim parameterValue As Object = System.Enum.Parse(value.GetType(), parameterString)
            Return parameterValue.Equals(value)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim parameterString As String = TryCast(parameter, String)
            If parameterString Is Nothing Then
                Return DependencyProperty.UnsetValue
            End If
            Return System.Enum.Parse(targetType, parameterString)
        End Function
    End Class

    Public Class BoolToIndexConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return DirectCast(value, Integer) <> -1
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(DirectCast(value, Boolean), 0, -1)
        End Function
    End Class

    Public Class IncrementInt
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return DirectCast(value, Integer) + 20
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
