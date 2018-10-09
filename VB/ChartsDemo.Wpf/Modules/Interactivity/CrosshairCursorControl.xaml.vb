Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System.Windows

Namespace ChartsDemo
    <CodeFile("Modules/Interactivity/CrosshairCursorControl.xaml"), CodeFile("Modules/Interactivity/CrosshairCursorControl.xaml.(cs)")>
    Partial Public Class CrosshairCursorControl
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
