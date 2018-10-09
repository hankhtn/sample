Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/AreaSeries/FullStackedArea3DControl.xaml"), CodeFile("Modules/AreaSeries/FullStackedArea3DControl.xaml.(cs)")>
    Partial Public Class FullStackedArea3DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub chbVisible_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If chart IsNot Nothing Then
                For Each series As AreaStackedSeries3D In CType(chart.Diagram, XYDiagram3D).Series
                    series.LabelsVisibility = True
                Next series
                chbPercent.IsEnabled = True
            End If
        End Sub
        Private Sub chbVisible_UnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If chart IsNot Nothing Then
                For Each series As AreaStackedSeries3D In CType(chart.Diagram, XYDiagram3D).Series
                    series.LabelsVisibility = False
                Next series
                chbPercent.IsEnabled = False
            End If
        End Sub
        Private Sub chbPercent_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If chart IsNot Nothing Then
                For Each series As AreaFullStackedSeries3D In CType(chart.Diagram, XYDiagram3D).Series
                    series.Label.TextPattern = "{VP:P2}"
                Next series
            End If
        End Sub
        Private Sub chbPercent_UnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If chart IsNot Nothing Then
                For Each series As AreaFullStackedSeries3D In CType(chart.Diagram, XYDiagram3D).Series
                    series.Label.TextPattern = "${V}M"
                Next series
            End If
        End Sub
    End Class
End Namespace
