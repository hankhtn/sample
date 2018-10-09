Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/AreaSeries/FullStackedSplineArea2DControl.xaml"), CodeFile("Modules/AreaSeries/FullStackedSplineArea2DControl.xaml.(cs)")>
    Partial Public Class FullStackedSplineArea2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            For Each series As AreaFullStackedSeries2D In chart.Diagram.Series
                series.CrosshairLabelPattern = "Architecture: {S}" & ControlChars.Lf & "Amount: {V:0.00}"
            Next series
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
        Private Sub chbPercent_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If chart IsNot Nothing Then
                For Each series As AreaFullStackedSeries2D In CType(chart.Diagram, XYDiagram2D).Series
                    series.Label.TextPattern = "{VP:P2}"
                Next series
            End If
        End Sub
        Private Sub chbPercent_UnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If chart IsNot Nothing Then
                For Each series As AreaFullStackedSeries2D In CType(chart.Diagram, XYDiagram2D).Series
                    series.Label.TextPattern = "${V}M"
                Next series
            End If
        End Sub
    End Class
End Namespace
