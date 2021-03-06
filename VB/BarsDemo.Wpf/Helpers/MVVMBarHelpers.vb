Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media

Namespace BarsDemo
    Public NotInheritable Class DXImageHelper

        Private Sub New()
        End Sub

        Public Shared Function GetDXImage(ByVal dxImageName As String) As ImageSource
            Dim extension As New DXImageExtension()
            extension.Image = TryCast((New DXImageConverter()).ConvertFromString(dxImageName), DXImageInfo)
            Return TryCast(extension.ProvideValue(Nothing), ImageSource)
        End Function
    End Class
    Public Class CommandTemplateSelector
        Inherits DataTemplateSelector

        Public Property SubItemTemplate() As DataTemplate
        Public Property ItemTemplate() As DataTemplate
        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            If item IsNot Nothing AndAlso TypeOf item Is BarButtonInfo Then
                If TypeOf item Is GroupBarButtonInfo Then
                    Return SubItemTemplate
                Else
                    Return ItemTemplate
                End If
            End If
            Return Nothing
        End Function
    End Class
    Public Class DXImageConverterExtension
        Inherits MarkupExtension
        Implements IValueConverter

        Public Sub New()
        End Sub
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return DXImageHelper.GetDXImage(DirectCast(value, String))
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
