Imports System
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media.Animation
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/PieDonutSeries/PieDonut3DControl.xaml"), CodeFile("Modules/PieDonutSeries/PieDonut3DControl.xaml.(cs)"), CodeFile("Utils/PieChartSelectionBehavior.(cs)")>
    Partial Public Class PieDonut3DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            Series.ToolTipPointPattern = "{A}: {V:0.0}M km²"
        End Sub
        Private Sub chart_QueryChartCursor(ByVal sender As Object, ByVal e As QueryChartCursorEventArgs)
            Dim hitInfo As ChartHitInfo = chart.CalcHitInfo(e.Position)
            If hitInfo IsNot Nothing AndAlso hitInfo.SeriesPoint IsNot Nothing AndAlso Mouse.PrimaryDevice.LeftButton = MouseButtonState.Released Then
                e.Cursor = Cursors.Hand
            End If
        End Sub
    End Class
End Namespace
