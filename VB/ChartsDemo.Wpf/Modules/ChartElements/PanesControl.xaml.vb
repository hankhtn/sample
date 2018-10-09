Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports System.IO
Imports System.Text

Namespace ChartsDemo
    <CodeFile("Modules/ChartElements/PanesControl.xaml"), CodeFile("Modules/ChartElements/PanesControl.xaml.(cs)"), CodeFile("ViewModels/WeatherData.(cs)")>
    Partial Public Class PanesControl
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
