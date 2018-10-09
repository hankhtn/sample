Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors

Namespace ChartsDemo
    <CodeFile("Modules/DataAnalysis/OscillatorIndicatorsControl.xaml"), CodeFile("Modules/DataAnalysis/OscillatorIndicatorsControl.xaml.(cs)")>
    Partial Public Class OscillatorIndicatorsControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub

        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
        Private Sub cbeIndicatorKind_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each indicator As Indicator In series.Indicators
                indicator.Animate()
            Next indicator
        End Sub
    End Class
End Namespace
