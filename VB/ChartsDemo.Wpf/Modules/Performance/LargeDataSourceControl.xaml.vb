Imports System
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/Performance/LargeDataSourceControl.xaml"), CodeFile("Modules/Performance/LargeDataSourceControl.xaml.(cs)")>
    Partial Public Class LargeDataSourceControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            LoadPoints()
        End Sub
        Private Sub LoadPoints()
            Dim pointCount As Integer = CInt((cbPointCount.SelectedItem))
            Dim value As Double = 0
            Dim random As New Random()
            series.Points.BeginInit()
            series.Points.Clear()
            For i As Integer = 0 To pointCount - 1
                series.Points.Add(New SeriesPoint(CDbl(i), value))
                value += (random.NextDouble() * 10.0 - 5.0)
                If value > 1200 Then
                    value -= 500
                End If
                If value < -1200 Then
                    value += 500
                End If
            Next i
            series.Points.EndInit()
            axisX.VisualRange.SetMinMaxValues(3 * pointCount \ 8, 5 * pointCount \ 8)
            axisX.WholeRange.SetMinMaxValues(0, pointCount)
        End Sub
        Private Sub ExecuteIdle(ByVal operation As Action)
            Dispatcher.BeginInvoke(operation, DispatcherPriority.Background)
        End Sub
        Private Sub HideLoadingPanel()
            loadingDecorator.IsSplashScreenShown = False
        End Sub
        Private Sub ShowLoadingPanel()
            loadingDecorator.IsSplashScreenShown = True
        End Sub
        Private Sub cbPointCount_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ShowLoadingPanel()
            ExecuteIdle(AddressOf LoadPoints)
            ExecuteIdle(AddressOf HideLoadingPanel)
        End Sub
    End Class
End Namespace
