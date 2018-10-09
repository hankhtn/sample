Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System

Namespace ChartsDemo
    <CodeFile("Modules/Performance/RealtimeChartControl.xaml"), CodeFile("Modules/Performance/RealtimeChartControl.xaml.(cs)"), CodeFile("ViewModels/RealtimeViewModel.(cs)")>
    Partial Public Class RealtimeChartControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
