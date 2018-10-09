Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors

Namespace ChartsDemo
    <CodeFile("Modules/AreaSeries/RangeArea2DControl.xaml"), CodeFile("Modules/AreaSeries/RangeArea2DControl.xaml.(cs)")>
    Partial Public Class RangeArea2DControl
        Inherits ChartsDemoModule

        Private Shared predefinedSizes() As String = { "8", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "30"}

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            cbeMarker1Size.Items.AddRange(predefinedSizes)
            cbeMarker2Size.Items.AddRange(predefinedSizes)
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class
End Namespace
