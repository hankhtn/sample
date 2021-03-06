Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace DevExpress.DevAV
    Public Class GridHeightConverter
        Inherits MarkupExtension
        Implements IMultiValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        Public Property SummaryDistance() As Double

        Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim commonHeight = DirectCast(values(0), Double)
            Dim panelHeight = DirectCast(values(1), Double)
            Dim height = commonHeight - panelHeight - SummaryDistance
            Return If(height < 0, 0, height)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
