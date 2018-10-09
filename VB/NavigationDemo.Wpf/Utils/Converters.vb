Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media

Namespace NavigationDemo.Utils
    Public Class CollectionConverter(Of T)
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If value Is Nothing Then
                Return Enumerable.Empty(Of T)()
            End If
            Return DirectCast(value, List(Of Object)).Cast(Of T)().ToList()
        End Function
    End Class
    Public Class Int32CollectionConverter
        Inherits CollectionConverter(Of Integer)

    End Class
    Public Class Int64CollectionConverter
        Inherits CollectionConverter(Of Long)

    End Class
    Public Class DeltaValueToImageSourceConverter
        Inherits MarkupExtension
        Implements IMultiValueConverter

        Public Property NegativeImageSource() As ImageSource
        Public Property PositiveImageSource() As ImageSource

        Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim delta = DirectCast(values(0), Double)
            Dim deltaChange = DirectCast(values(1), Integer)
            Dim mode = DirectCast(values(2), StockIndicatorMode)
            If (mode = StockIndicatorMode.Delta AndAlso delta < 0) OrElse (mode = StockIndicatorMode.DeltaChange AndAlso deltaChange < 0) Then
                Return NegativeImageSource
            End If
            If (mode = StockIndicatorMode.Delta AndAlso delta > 0) OrElse (mode = StockIndicatorMode.DeltaChange AndAlso deltaChange > 0) Then
                Return PositiveImageSource
            End If
            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
    Public Class FilterStringConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim item = DirectCast(value, FilterItem)
            Return If(item Is Nothing, String.Empty, item.FilterString)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
    Public Class NavigationWidthConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        Public Property StartupWidth() As Double
        Private isExpanded As Boolean = True

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            isExpanded = DirectCast(value, Boolean)
            Return If(isExpanded, New GridLength(StartupWidth), GridLength.Auto)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            If isExpanded Then
                StartupWidth = DirectCast(value, GridLength).Value
            End If
            Return Binding.DoNothing
        End Function

    End Class
End Namespace
