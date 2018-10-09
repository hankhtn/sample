Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Windows

Namespace ChartsDemo
    <CodeFile("Modules/PointLineSeries/PointScatter2DControl.xaml"), CodeFile("Modules/PointLineSeries/PointScatter2DControl.xaml.(cs)")>
    Partial Public Class PointScatter2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class
End Namespace
