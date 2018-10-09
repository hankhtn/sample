Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/DataAnalysis/TrendIndicatorsControl.xaml"), CodeFile("Modules/DataAnalysis/TrendIndicatorsControl.xaml.(cs)")>
    Partial Public Class TrendIndicatorsControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            chart.Diagram.Series(0).DataSource = CsvReader.ReadFinancialData("USDJPYDaily.csv")
        End Sub

        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class
End Namespace
