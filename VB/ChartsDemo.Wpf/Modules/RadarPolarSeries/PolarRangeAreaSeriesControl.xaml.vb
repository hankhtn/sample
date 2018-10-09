Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic
Imports DevExpress.Xpf.Editors

Namespace ChartsDemo
    <CodeFile("Modules/RadarPolarSeries/PolarRangeAreaSeriesControl.xaml"), CodeFile("Modules/RadarPolarSeries/PolarRangeAreaSeriesControl.xaml.(cs)"), CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")>
    Partial Public Class PolarRangeAreaSeriesControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
