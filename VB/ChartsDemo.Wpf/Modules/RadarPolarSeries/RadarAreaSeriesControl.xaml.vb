Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/RadarPolarSeries/RadarAreaSeriesControl.xaml"), CodeFile("Modules/RadarPolarSeries/RadarAreaSeriesControl.xaml.(cs)"), CodeFile("ViewModels/WindRoseData.(cs)")>
    Partial Public Class RadarAreaSeriesControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            series.ToolTipPointPattern = "Direction: {A}" & ControlChars.Lf & "Speed: {V}"
        End Sub
    End Class
End Namespace
