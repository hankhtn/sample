Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/DataRepresentation/LogarithmicScaleControl.xaml"), CodeFile("Modules/DataRepresentation/LogarithmicScaleControl.xaml.(cs)"), CodeFile("ViewModels/RegionPopulationData.(cs)")>
    Partial Public Class LogarithmicScaleControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub AnimateChart(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class
End Namespace
