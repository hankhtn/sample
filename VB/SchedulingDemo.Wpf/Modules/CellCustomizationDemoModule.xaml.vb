Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.DemoBase

Namespace SchedulingDemo
    <NoAutogeneratedCodeFiles, CodeFiles("Modules/CellCustomizationDemoModule.xaml", "ViewModels/CellCustomizationDemoViewModel.(cs)")>
    Partial Public Class CellCustomizationDemoModule
        Inherits SchedulingDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class CellBackgroundConverter
        Implements IValueConverter

        Public Property LunchBackground() As Brush
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim range As DateTimeRange = DirectCast(value, DateTimeRange)
            If range.Start.TimeOfDay >= TimeSpan.FromHours(13) AndAlso range.End.TimeOfDay <= TimeSpan.FromHours(14) Then
                Return LunchBackground
            End If
            Return Nothing
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
