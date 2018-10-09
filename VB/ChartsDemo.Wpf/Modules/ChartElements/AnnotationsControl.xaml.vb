Imports System
Imports System.Windows
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Data
Imports System.Globalization
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Linq

Namespace ChartsDemo
    <CodeFile("Modules/ChartElements/AnnotationsControl.xaml"), CodeFile("Modules/ChartElements/AnnotationsControl.xaml.(cs)"), CodeFile("ViewModels/MarsTemperatureData.(cs)")>
    Partial Public Class AnnotationsControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub

        Private Sub BoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim min = splineSeries.Points.Aggregate(Function(seed, current)If(seed.Value < current.Value, seed, current))
            minimumPoint.SeriesPoint = min
            minimumAnnotation.Content = "Minimum: " & min.Value

            Dim max = splineSeries.Points.Aggregate(Function(seed, current)If(seed.Value > current.Value, seed, current))
            maximumPoint.SeriesPoint = max
            maximumAnnotation.Content = "Maximum: " & max.Value
        End Sub
    End Class
End Namespace
