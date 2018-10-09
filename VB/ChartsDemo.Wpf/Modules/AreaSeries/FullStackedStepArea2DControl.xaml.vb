Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Linq
Imports System.Xml.Serialization
Imports System.IO
Imports System.Text

Namespace ChartsDemo
    <CodeFile("Modules/AreaSeries/FullStackedStepArea2DControl.xaml"), CodeFile("Modules/AreaSeries/FullStackedStepArea2DControl.xaml.(cs)"), CodeFile("ViewModels/CommentsData.(cs)")>
    Partial Public Class FullStackedStepArea2DControl
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
