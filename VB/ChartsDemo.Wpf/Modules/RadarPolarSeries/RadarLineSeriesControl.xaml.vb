Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace ChartsDemo
    <CodeFile("Modules/RadarPolarSeries/RadarLineSeriesControl.xaml"), CodeFile("Modules/RadarPolarSeries/RadarLineSeriesControl.xaml.(cs)"), CodeFile("ViewModels/WindRoseData.(cs)")>
    Partial Public Class RadarLineSeriesControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            series.ToolTipPointPattern = "Direction: {A}" & ControlChars.Lf & "Speed: {V}"
        End Sub
    End Class
End Namespace
