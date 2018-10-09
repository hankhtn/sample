Imports System
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media.Animation
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Mvvm.UI.Interactivity
Imports System.Windows.Data
Imports System.Globalization

Namespace ChartsDemo
    <CodeFile("Modules/PieDonutSeries/PieDonut2DControl.xaml"), CodeFile("Modules/PieDonutSeries/PieDonut2DControl.xaml.(cs)"), CodeFile("Utils/PieChartSelectionBehavior.(cs)"), CodeFile("Utils/PieChartRotationBehavior.(cs)")>
    Partial Public Class PieDonut2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            Series.ToolTipPointPattern = "{A}: {V:0.0}M km²"
        End Sub
        Private Sub Animate()
            If chart IsNot Nothing Then
                chart.Animate()
            End If
            If pieTotalLabel IsNot Nothing Then
                CType(pieTotalLabel.Resources("pieTotalLabelStoryboard"), Storyboard).Begin()
            End If
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Animate()
        End Sub
        Private Sub rblSweepDirection_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Animate()
        End Sub
        Private Sub chart_QueryChartCursor(ByVal sender As Object, ByVal e As QueryChartCursorEventArgs)
            Dim hitInfo As ChartHitInfo = chart.CalcHitInfo(e.Position)
            If hitInfo IsNot Nothing AndAlso hitInfo.SeriesPoint IsNot Nothing Then
                e.Cursor = Cursors.Hand
            End If
        End Sub
    End Class

    Public Class ShowTotalInToTitleVisibleConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim str As String = DirectCast(value, String)
            Return str = "Title"
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class

    Public Class ShowTotalInToPieTotalLabelVisibilityConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim str As String = DirectCast(value, String)
            If str = "Label" Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
End Namespace
