Imports System
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media.Animation
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Mvvm.UI.Interactivity

Namespace ChartsDemo
    Public Class PieChartSelectionBehavior
        Inherits Behavior(Of ChartControl)

        #Region "DependencyProperties"

        Public Shared ReadOnly ExpandAnimationProperty As DependencyProperty = DependencyProperty.Register("ExpandAnimation", GetType(DoubleAnimation), GetType(PieChartSelectionBehavior), New PropertyMetadata(Nothing))
        Public Shared ReadOnly CollapseAnimationProperty As DependencyProperty = DependencyProperty.Register("CollapseAnimation", GetType(DoubleAnimation), GetType(PieChartSelectionBehavior), New PropertyMetadata(Nothing))

        Public Property ExpandAnimation() As DoubleAnimation
            Get
                Return CType(GetValue(ExpandAnimationProperty), DoubleAnimation)
            End Get
            Set(ByVal value As DoubleAnimation)
                SetValue(ExpandAnimationProperty, value)
            End Set
        End Property
        Public Property CollapseAnimation() As DoubleAnimation
            Get
                Return CType(GetValue(CollapseAnimationProperty), DoubleAnimation)
            End Get
            Set(ByVal value As DoubleAnimation)
                SetValue(CollapseAnimationProperty, value)
            End Set
        End Property

        #End Region
        Private Const clickDelta As Integer = 200

        Private ReadOnly Property Chart() As ChartControl
            Get
                Return AssociatedObject
            End Get
        End Property
        Private mouseDownTime As Date

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler Chart.MouseDown, AddressOf Chart_MouseDown
            AddHandler Chart.MouseUp, AddressOf Chart_MouseUp
        End Sub

        Private Sub Chart_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            mouseDownTime = Date.Now
        End Sub
        Private Sub Chart_MouseUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim hitInfo As ChartHitInfo = Chart.CalcHitInfo(e.GetPosition(Chart))
            If hitInfo Is Nothing OrElse hitInfo.SeriesPoint Is Nothing OrElse (Not IsClick(Date.Now)) Then
                Return
            End If
            Dim distance As Double = PieSeries.GetExplodedDistance(hitInfo.SeriesPoint)
            Dim storyBoard As New Storyboard()
            Dim animation = If(distance > 0, CollapseAnimation, ExpandAnimation)
            storyBoard.Children.Add(animation)
            Storyboard.SetTarget(animation, hitInfo.SeriesPoint)
            Storyboard.SetTargetProperty(animation, New PropertyPath(PieSeries.ExplodedDistanceProperty))
            storyBoard.Begin()
        End Sub

        Private Function IsClick(ByVal mouseUpTime As Date) As Boolean
            Return (mouseUpTime.Subtract(mouseDownTime)).TotalMilliseconds < clickDelta
        End Function
    End Class
End Namespace
