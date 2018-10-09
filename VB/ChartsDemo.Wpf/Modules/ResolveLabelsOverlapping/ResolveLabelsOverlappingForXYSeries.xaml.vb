Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingForXYSeries.xaml"), CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingForXYSeries.xaml.(cs)"), CodeFile("ViewModels/MarsTemperatureData.(cs)")>
    Partial Public Class ResolveLabelsOverlappingForXYSeries
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class
End Namespace
