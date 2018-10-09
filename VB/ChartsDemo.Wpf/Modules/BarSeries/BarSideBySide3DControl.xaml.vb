Imports System
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/BarSeries/BarSideBySide3DControl.xaml"), CodeFile("Modules/BarSeries/BarSideBySide3DControl.xaml.(cs)")>
    Partial Public Class BarSideBySide3DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
