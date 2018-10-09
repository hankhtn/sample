Imports System.ComponentModel
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic

Namespace ChartsDemo
    <CodeFile("Modules/PieDonutSeries/NestedDonut2DControl.xaml"), CodeFile("Modules/PieDonutSeries/NestedDonut2DControl.xaml.(cs)")>
    Partial Public Class NestedDonut2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub ChartControlBoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim seriesCollection = chart.Diagram.Series
            If seriesCollection.Count > 0 Then
                For Each series As NestedDonutSeries2D In seriesCollection
                    series.ShowInLegend = False
                    Dim population As AgePopulation = TryCast(series.Points(0).Tag, AgePopulation)
                    If population IsNot Nothing Then
                        series.Group = population.Name
                    End If
                Next series
                seriesCollection(0).ShowInLegend = True
            End If
        End Sub
    End Class
End Namespace
