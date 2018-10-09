Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Windows.Markup
Imports System.Windows.Threading
Imports System.ComponentModel

Namespace ChartsDemo
    <CodeFile("Modules/DataRepresentation/EmptyPointsControl.xaml"), CodeFile("Modules/DataRepresentation/EmptyPointsControl.xaml.(cs)"), CodeFile("ViewModels/WebSiteData.(cs)"), CodeFile("Utils/SeriesInfo.(cs)")>
    Partial Public Class EmptyPointsControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
        Private Sub lbSeriesType_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dispatcher.BeginInvoke(New Action(AddressOf chart.Animate), DispatcherPriority.Background)
        End Sub
    End Class
End Namespace
