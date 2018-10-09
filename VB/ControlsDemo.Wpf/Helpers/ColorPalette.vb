Imports System
Imports System.Collections.Generic
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media

Namespace ControlsDemo.Helpers
    Public Class ColorPalette
        Private ReadOnly Shared Palette As List(Of Color)
        Shared Sub New()
            Palette = New List(Of Color)() From {DirectCast(ColorConverter.ConvertFromString("#4668a5"), Color), DirectCast(ColorConverter.ConvertFromString("#a54671"), Color), DirectCast(ColorConverter.ConvertFromString("#49a4be"), Color), DirectCast(ColorConverter.ConvertFromString("#469ea5"), Color), DirectCast(ColorConverter.ConvertFromString("#5848a5"), Color), DirectCast(ColorConverter.ConvertFromString("#9462ae"), Color), DirectCast(ColorConverter.ConvertFromString("#fcc653"), Color)}
        End Sub
        Public Shared Function GetColor(ByVal number As Integer) As Color
            If number >= Palette.Count Then
                Dim coef As Integer = number \ (Palette.Count - 1)
                number -= coef * (Palette.Count - 1)
            End If
            Return Palette(CInt(Math.Max(0, number)))
        End Function
        Public Shared Function GetColor(ByVal number As Integer, ByVal opacity As Byte) As Color
            Dim res = GetColor(number)
            res.A = opacity
            Return res
        End Function
    End Class
    Public Class ColorPaletteConverter
        Implements IValueConverter

        Public Property Opacity() As Byte
        Public Sub New()
            Opacity = 255
        End Sub
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Integer Then
                Return ColorPalette.GetColor(DirectCast(value, Integer), Opacity)
            End If
            Return Colors.Transparent
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class ColorPaletteConverterExtension
        Inherits MarkupExtension

        Public Property Opacity() As Byte
        Public Sub New()
            Opacity = 255
        End Sub
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return New ColorPaletteConverter() With {.Opacity = Opacity}
        End Function
    End Class
End Namespace
