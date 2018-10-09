Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System.Windows

Namespace ChartsDemo
    <CodeFile("Modules/RadarPolarSeries/RadarRangeAreaSeriesControl.xaml"), CodeFile("Modules/RadarPolarSeries/RadarRangeAreaSeriesControl.xaml.(cs)")>
    Partial Public Class RadarRangeAreaSeriesControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
