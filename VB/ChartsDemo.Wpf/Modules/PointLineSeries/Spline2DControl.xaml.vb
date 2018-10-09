Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Linq

Namespace ChartsDemo
    <CodeFile("Modules/PointLineSeries/Spline2DControl.xaml"), CodeFile("Modules/PointLineSeries/Spline2DControl.xaml.(cs)")>
    Partial Public Class Spline2DControl
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
