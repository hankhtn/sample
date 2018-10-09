Imports System
Imports System.Windows
Imports System.Windows.Media.Animation
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System.Data

Namespace ChartsDemo
    <CodeFile("Modules/PointLineSeries/Bubble3DControl.xaml"), CodeFile("Modules/PointLineSeries/Bubble3DControl.xaml.(cs)")>
    Partial Public Class Bubble3DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub chart_CustomDrawSeriesPoint(ByVal sender As Object, ByVal e As CustomDrawSeriesPointEventArgs)
            e.LegendText = (DirectCast(CType(e.SeriesPoint.Tag, DataRowView).Row("Title"), String)).Replace(ControlChars.Lf, " ")
        End Sub
        Private Sub Bubble3DDemo_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim sizeAnimation = TryCast(Series.TryFindResource("SizeAnimationStoryboard"), Storyboard)
            If sizeAnimation IsNot Nothing Then
                sizeAnimation.Begin(Series)
            End If
        End Sub
        Private Sub Storyboard_Completed(ByVal sender As Object, ByVal e As EventArgs)
            Series.MaxSize = 2.2
            Series.MinSize = 0.4
        End Sub
    End Class
End Namespace
