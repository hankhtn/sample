Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace RibbonDemo
    Friend Class CateroryColorEnableConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim walker = TryCast(value, ThemeTreeWalker)
            If walker IsNot Nothing AndAlso (walker.ThemeName = Theme.Office2016ColorfulSEName OrElse walker.ThemeName = Theme.Office2016DarkGraySEName OrElse walker.ThemeName = Theme.Office2016BlackSEName) Then
                Return False
            End If
            Return True
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
