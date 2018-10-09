Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors

Namespace ChartsDemo
    <CodeFile("Modules/DataAnalysis/PriceIndicatorsControl.xaml"), CodeFile("Modules/DataAnalysis/PriceIndicatorsControl.xaml.(cs)")>
    Partial Public Class PriceIndicatorsControl
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
