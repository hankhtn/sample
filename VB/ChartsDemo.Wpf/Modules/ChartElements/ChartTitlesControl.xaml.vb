Imports System
Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Xpf.Charts
Imports System.Diagnostics
Imports System.Windows.Documents
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/ChartElements/ChartTitlesControl.xaml"), CodeFile("Modules/ChartElements/ChartTitlesControl.xaml.(cs)")>
    Partial Public Class ChartTitlesControl
        Inherits ChartsDemoModule

        Private Const pointsCount As Integer = 40

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            CreatePoints(chart.Diagram.Series(0))
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            chart.Animate()
        End Sub
        Private Sub CreatePoints(ByVal series As Series)
            Dim random As New Random()
            For i As Integer = 0 To pointsCount - 1
                series.Points.Add(New SeriesPoint(i, random.NextDouble() + 1))
            Next i
        End Sub
        Private Sub Hyperlink_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim source As Hyperlink = TryCast(sender, Hyperlink)
            If source IsNot Nothing Then
                Process.Start(source.NavigateUri.ToString())
            End If
        End Sub
    End Class
End Namespace
