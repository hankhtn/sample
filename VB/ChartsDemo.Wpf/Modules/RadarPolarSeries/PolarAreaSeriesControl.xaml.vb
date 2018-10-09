Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic
Imports System.Linq

Namespace ChartsDemo
    <CodeFile("Modules/RadarPolarSeries/PolarAreaSeriesControl.xaml"), CodeFile("Modules/RadarPolarSeries/PolarAreaSeriesControl.xaml.(cs)"), CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")>
    Partial Public Class PolarAreaSeriesControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
