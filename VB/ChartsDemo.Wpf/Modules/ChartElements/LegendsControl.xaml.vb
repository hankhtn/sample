Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/ChartElements/LegendsControl.xaml"), CodeFile("Modules/ChartElements/LegendsControl.xaml.(cs)"), CodeFile("ViewModels/PerformanceDataSource.(cs)")>
    Partial Public Class LegendsControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub

        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class

    Friend Enum LegendMode
        Common
        SeparateInsidePane
        SeparateOutsidePane
    End Enum
End Namespace
