Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/AreaSeries/StackedStepArea2DControl.xaml"), CodeFile("Modules/AreaSeries/StackedStepArea2DControl.xaml.(cs)"), CodeFile("ViewModels/CommentsData.(cs)")>
    Partial Public Class StackedStepArea2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class
End Namespace
