Imports DevExpress.Xpf.Charts
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media

Namespace PropertyGridDemo
    Public Class StaticPropertiesExtension
        Inherits MarkupExtension

        Public Property Type() As Type
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Type.GetProperties(System.Reflection.BindingFlags.Static Or System.Reflection.BindingFlags.Public).Select(Function(x) x.GetValue(Nothing, Nothing))
        End Function
    End Class
    Public Class ColorToBrushConverterExtension
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is Color) Then
                Return Nothing
            End If
            Return New SolidColorBrush(DirectCast(value, Color))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
    Public Class Bar2DKindToModelConverterExtension
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim bar2DKind As Bar2DKind = TryCast(value, Bar2DKind)
            If bar2DKind IsNot Nothing Then
                Return Activator.CreateInstance(bar2DKind.Type)
            End If
            Return value
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
