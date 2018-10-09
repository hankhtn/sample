Imports System.Data
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/PointLineSeries/Bubble2DControl.xaml"), CodeFile("Modules/PointLineSeries/Bubble2DControl.xaml.(cs)")>
    Partial Public Class Bubble2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
        Private Sub chart_CustomDrawSeriesPoint(ByVal sender As Object, ByVal e As CustomDrawSeriesPointEventArgs)
            e.LegendText = (DirectCast(CType(e.SeriesPoint.Tag, DataRowView).Row("Title"), String)).Replace(ControlChars.Lf, " ")
        End Sub
    End Class
End Namespace
