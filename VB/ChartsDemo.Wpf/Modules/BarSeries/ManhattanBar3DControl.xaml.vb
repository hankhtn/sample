Imports System
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/BarSeries/ManhattanBar3DControl.xaml"), CodeFile("Modules/BarSeries/ManhattanBar3DControl.xaml.(cs)")>
    Partial Public Class ManhattanBar3DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
