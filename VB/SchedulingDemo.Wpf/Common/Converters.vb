Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Scheduling

Namespace SchedulingDemo
   Public Class SportGroupToImageSourceConverter
       Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return SportChannelsData.SportGroups(DirectCast(value, Integer)).Image
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return value
        End Function
   End Class

    Public Class CommandBarStyleToBooleanConverter
        Inherits ObjectToObjectConverter

        Public Sub New()
            Map.Add(New MapItem() With {.Target = True, .Source = CommandBarStyle.Ribbon})
            Map.Add(New MapItem() With {.Target = False, .Source = CommandBarStyle.Empty})
        End Sub
    End Class

    Public Class SportGroupToImageSourceConverterExtension
        Inherits MarkupExtension

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return New SportGroupToImageSourceConverter()
        End Function
    End Class

    Public Class CommandBarStyleToBooleanConverterExtension
        Inherits MarkupExtension

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return New CommandBarStyleToBooleanConverter()
        End Function
    End Class
End Namespace
