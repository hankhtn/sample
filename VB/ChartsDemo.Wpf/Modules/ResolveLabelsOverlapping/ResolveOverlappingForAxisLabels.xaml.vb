Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/ResolveLabelsOverlapping/ResolveOverlappingForAxisLabels.xaml"), CodeFile("Modules/ResolveLabelsOverlapping/ResolveOverlappingForAxisLabels.xaml.(cs)"), CodeFile("ViewModels/HumidityData.(cs)")>
    Partial Public Class ResolveOverlappingForAxisLabels
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
