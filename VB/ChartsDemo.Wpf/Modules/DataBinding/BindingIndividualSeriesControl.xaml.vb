Imports System.Collections.ObjectModel
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/DataBinding/BindingIndividualSeriesControl.xaml"), CodeFile("Modules/DataBinding/BindingIndividualSeriesControl.xaml.(cs)")>
    Partial Public Class BindingIndividualSeriesControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            series.ToolTipPointPattern = "X = {A}" & ControlChars.Lf & "Y = {V}"
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub

    End Class
    Public Class PointCollection
        Inherits ObservableCollection(Of Point)

    End Class
End Namespace
