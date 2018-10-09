Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.Editors
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq

Namespace ChartsDemo
    Public NotInheritable Class AnimationHelper

        Private Sub New()
        End Sub

        Public Shared Function CreateSeriesAnimation(ByVal animationKind As AnimationKind, ByVal seriesIndex As Integer) As SeriesAnimationBase
            If animationKind Is Nothing OrElse animationKind.Type Is Nothing Then
                Return Nothing
            End If
            Dim seriesAnimation = DirectCast(Activator.CreateInstance(animationKind.Type), SeriesAnimationBase)
            If animationKind.Name = "None" Then
                seriesAnimation.Duration = New TimeSpan(0)
                seriesAnimation.BeginTime = New TimeSpan(0)
            Else
                seriesAnimation.BeginTime = TimeSpan.FromMilliseconds(Math.Round(seriesAnimation.Duration.TotalMilliseconds / 4.0) * seriesIndex)
            End If
            Return seriesAnimation
        End Function
        Public Shared Function CreatePointAnimation(ByVal animationKind As AnimationKind, ByVal seriesAnimation As SeriesAnimationBase, ByVal seriesIndex As Integer) As SeriesPointAnimationBase
            If animationKind Is Nothing OrElse animationKind.Type Is Nothing Then
                Return Nothing
            End If
            Dim pointAnimation = DirectCast(Activator.CreateInstance(animationKind.Type), SeriesPointAnimationBase)
            If animationKind.Name = "None" Then
                pointAnimation.Duration = New TimeSpan(0)
                pointAnimation.PointDelay = New TimeSpan(0)
                pointAnimation.BeginTime = New TimeSpan(0)
            ElseIf seriesAnimation IsNot Nothing Then
                pointAnimation.BeginTime = seriesAnimation.BeginTime
            Else
                pointAnimation.BeginTime = TimeSpan.FromMilliseconds(pointAnimation.PointDelay.TotalMilliseconds * seriesIndex)
            End If
            Return pointAnimation
        End Function

        Public Shared Sub InitializeAnimationListBoxEdit(ByVal listBoxEdit As ListBoxEdit, ByVal animationKinds As IEnumerable(Of AnimationKind), ByVal defaultAnimationType As Type)
            Dim allAnimationKinds As New List(Of AnimationKind)()
            Dim noneAnimation As AnimationKind = GetNoneAnimation(animationKinds)
            allAnimationKinds.Add(noneAnimation)
            allAnimationKinds.AddRange(animationKinds)
            listBoxEdit.ClearValue(ListBoxEdit.SelectedItemProperty)
            listBoxEdit.ItemsSource = allAnimationKinds
            listBoxEdit.SelectedItem = If(animationKinds.Where(Function(x) x.Type = defaultAnimationType).FirstOrDefault(), noneAnimation)
        End Sub
        Private Shared Function GetNoneAnimation(ByVal animationKinds As IEnumerable(Of AnimationKind)) As AnimationKind
            If animationKinds Is Nothing OrElse (Not animationKinds.Any()) Then
                Return New AnimationKind(Nothing, "None")
            End If
            Return New AnimationKind(animationKinds.First().Type, "None")
        End Function

        Public Shared Function GetDefaultPointAnimationType(ByVal series As Series) As Type
            If TypeOf series Is BarSideBySideSeries2D OrElse TypeOf series Is RangeBarSeries2D Then
                Return GetType(Bar2DGrowUpAnimation)
            End If
            If TypeOf series Is BarStackedSeries2D Then
                Return GetType(Bar2DDropInAnimation)
            End If
            If TypeOf series Is PointSeries2D Then
                Return GetType(Marker2DSlideFromLeftAnimation)
            End If
            If TypeOf series Is BubbleSeries2D Then
                Return GetType(Marker2DWidenAnimation)
            End If
            If TypeOf series Is LineSeries2D OrElse TypeOf series Is AreaSeries2D OrElse TypeOf series Is RangeAreaSeries2D Then
                Return GetType(Marker2DFadeInAnimation)
            End If
            If TypeOf series Is AreaStackedSeries2D Then
                Return GetType(AreaStacked2DFadeInAnimation)
            End If
            If TypeOf series Is FinancialSeries2D Then
                Return GetType(Stock2DSlideFromTopAnimation)
            End If
            If TypeOf series Is PieSeries2D Then
                Return GetType(Pie2DGrowUpAnimation)
            End If
            If TypeOf series Is FunnelSeries2D Then
                Return GetType(Funnel2DWidenAnimation)
            End If
            If TypeOf series Is CircularSeriesBase2D Then
                Return GetType(CircularMarkerSlideToCenterAnimation)
            End If
            Return Nothing
        End Function
        Public Shared Function GetDefaultSeriesAnimationType(ByVal series As Series) As Type
            If TypeOf series Is LineSeries2D Then
                Return GetType(Line2DStretchFromNearAnimation)
            End If
            If TypeOf series Is AreaSeries2D OrElse TypeOf series Is RangeAreaSeries2D Then
                Return GetType(Area2DStretchFromNearAnimation)
            End If
            If TypeOf series Is AreaStackedSeries2D Then
                Return GetType(Area2DDropFromFarAnimation)
            End If
            If TypeOf series Is CircularAreaSeries2D Then
                Return GetType(CircularAreaZoomInAnimation)
            End If
            If TypeOf series Is CircularLineSeries2D Then
                Return GetType(CircularLineZoomInAnimation)
            End If
            If TypeOf series Is CircularRangeAreaSeries2D Then
                Return GetType(CircularAreaZoomInAnimation)
            End If
            Return Nothing
        End Function
    End Class
End Namespace
