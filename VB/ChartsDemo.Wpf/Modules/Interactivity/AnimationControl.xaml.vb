Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System.Windows.Markup
Imports System.Windows.Threading
Imports System.Linq
Imports System.ComponentModel
Imports System.Collections
Imports DevExpress.Mvvm.POCO

Namespace ChartsDemo
    <CodeFile("Modules/Interactivity/AnimationControl.xaml"), CodeFile("Modules/Interactivity/AnimationControl.xaml.(cs)"), CodeFile("ViewModels/DataPointSources.(cs)"), CodeFile("Utils/AnimationHelper.(cs)"), CodeFile("Utils/SeriesInfo.(cs)")>
    Partial Public Class AnimationControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            Dispatcher.BeginInvoke(New Action(AddressOf OnSeriesTemplateChanged), DispatcherPriority.Background)
        End Sub

        Private Sub SeriesTemplateChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dispatcher.BeginInvoke(New Action(AddressOf OnSeriesTemplateChanged), DispatcherPriority.Background)
        End Sub
        Private Sub OnSeriesTemplateChanged()
            Dim firstSeries = chart.Diagram.Series.FirstOrDefault()
            If firstSeries Is Nothing Then
                Return
            End If
            If TypeOf firstSeries Is RangeBarOverlappedSeries2D Then
                CType(chart.Diagram.Series.Last(), RangeBarOverlappedSeries2D).BarWidth = 0.2
            End If
            If TypeOf firstSeries Is ISupportStackedGroup Then
                For i As Integer = 0 To chart.Diagram.Series.Count - 1
                    DirectCast(chart.Diagram.Series(i), ISupportStackedGroup).StackedGroup = i Mod 2
                Next i
            End If
            AnimationHelper.InitializeAnimationListBoxEdit(lbPointAnimation, firstSeries.GetPredefinedPointAnimationKinds(), AnimationHelper.GetDefaultPointAnimationType(firstSeries))
            AnimationHelper.InitializeAnimationListBoxEdit(lbSeriesAnimation, firstSeries.GetPredefinedSeriesAnimationKinds(), AnimationHelper.GetDefaultSeriesAnimationType(firstSeries))
            chart.Animate()
        End Sub
        Private Sub AnimationKindChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If chart.Diagram Is Nothing Then
                Return
            End If
            For i As Integer = 0 To chart.Diagram.Series.Count - 1
                Dim series = chart.Diagram.Series(i)
                Dim seriesAnimation = AnimationHelper.CreateSeriesAnimation(CType(lbSeriesAnimation.SelectedItem, AnimationKind), i)
                Dim pointAnimation = AnimationHelper.CreatePointAnimation(CType(lbPointAnimation.SelectedItem, AnimationKind), seriesAnimation, i)
                series.SetSeriesAnimation(seriesAnimation)
                series.SetPointAnimation(pointAnimation)
            Next i
            chart.Animate()
        End Sub
    End Class
End Namespace
