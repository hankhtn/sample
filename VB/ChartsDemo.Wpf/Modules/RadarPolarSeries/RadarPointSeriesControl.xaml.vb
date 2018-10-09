Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/RadarPolarSeries/RadarPointSeriesControl.xaml"), CodeFile("Modules/RadarPolarSeries/RadarPointSeriesControl.xaml.(cs)"), CodeFile("ViewModels/TemperatureData.(cs)")>
    Partial Public Class RadarPointSeriesControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
