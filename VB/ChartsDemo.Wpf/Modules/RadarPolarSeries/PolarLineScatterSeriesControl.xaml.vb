Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/RadarPolarSeries/PolarLineScatterSeriesControl.xaml"), CodeFile("Modules/RadarPolarSeries/PolarLineScatterSeriesControl.xaml.(cs)"), CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")>
    Partial Public Class PolarLineScatterSeriesControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
