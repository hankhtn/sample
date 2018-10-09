Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Linq
Imports System

Namespace ChartsDemo
    <CodeFile("Modules/DataAnalysis/BasicFinancialIndicatorsControl.xaml"), CodeFile("Modules/DataAnalysis/BasicFinancialIndicatorsControl.xaml.(cs)")>
    Partial Public Class BasicFinancialIndicatorsControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub

        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
        Private Sub ComboBoxEdit_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dispatcher.BeginInvoke(New Action(Sub()
            For Each indicator In series.Indicators
                indicator.Animate()
            Next indicator
            End Sub))
        End Sub
    End Class
End Namespace
