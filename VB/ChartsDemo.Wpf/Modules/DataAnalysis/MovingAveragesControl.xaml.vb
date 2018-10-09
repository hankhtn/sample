Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors

Namespace ChartsDemo
    <CodeFile("Modules/DataAnalysis/MovingAveragesControl.xaml"), CodeFile("Modules/DataAnalysis/MovingAveragesControl.xaml.(cs)")>
    Partial Public Class MovingAveragesControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class
End Namespace
