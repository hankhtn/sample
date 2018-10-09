Imports System
Imports System.Windows.Data
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/FunnelSeries/Funnel2DControl.xaml"), CodeFile("Modules/FunnelSeries/Funnel2DControl.xaml.(cs)")>
    Partial Public Class Funnel2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
