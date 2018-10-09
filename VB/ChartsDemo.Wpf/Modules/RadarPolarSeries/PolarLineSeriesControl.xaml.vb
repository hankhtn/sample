Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/RadarPolarSeries/PolarLineSeriesControl.xaml"), CodeFile("Modules/RadarPolarSeries/PolarLineSeriesControl.xaml.(cs)"), CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")>
    Partial Public Class PolarLineSeriesControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace