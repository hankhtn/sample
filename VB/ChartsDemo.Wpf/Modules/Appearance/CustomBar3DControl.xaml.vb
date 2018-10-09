Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media.Animation
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.ComponentModel
Imports DevExpress.Mvvm

Namespace ChartsDemo
    <CodeFile("Modules/Appearance/CustomBar3DControl.xaml"), CodeFile("Modules/Appearance/CustomBar3DControl.xaml.(cs)")>
    Partial Public Class CustomBar3DControl
        Inherits ChartsDemoModule

        #Region "Data"

        Public Shared ReadOnly Property PencilPoints() As List(Of NumericPoint)
            Get
                Return GetPencilPoints()
            End Get
        End Property
        Public Shared ReadOnly Property ChairPoints() As List(Of NumericPoint)
            Get
                Return GetChairPoints()
            End Get
        End Property

        Private Shared Function GetPencilPoints() As List(Of NumericPoint)
            Dim points = New List(Of NumericPoint)()
            points.Add(New NumericPoint(1, 65))
            points.Add(New NumericPoint(2, 78))
            points.Add(New NumericPoint(3, 95))
            points.Add(New NumericPoint(4, 110))
            points.Add(New NumericPoint(5, 108))
            points.Add(New NumericPoint(6, 52))
            points.Add(New NumericPoint(7, 46))
            Return points
        End Function
        Private Shared Function GetChairPoints() As List(Of NumericPoint)
            Dim points = New List(Of NumericPoint)()
            points.Add(New NumericPoint(1, 55))
            points.Add(New NumericPoint(2, 70))
            points.Add(New NumericPoint(3, 40))
            Return points
        End Function

        #End Region

        Private Const clickDelta As Integer = 200

        Private mouseDownTime As Integer
        Private seriesPointAnimationStoryboard As Storyboard

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            seriesPointAnimationStoryboard = TryCast(TryFindResource("SeriesPointAnimationStoryboard"), Storyboard)
            If seriesPointAnimationStoryboard IsNot Nothing AndAlso seriesPointAnimationStoryboard.Children.Count > 0 Then
                Storyboard.SetTarget(seriesPointAnimationStoryboard.Children(0), SeriesPointAnimationRecord)
            End If
        End Sub
        Private Function IsClick(ByVal mouseUpTime As Integer) As Boolean
            Return mouseUpTime - mouseDownTime < clickDelta
        End Function
        Private Sub chart_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            mouseDownTime = e.Timestamp
        End Sub
        Private Sub chart_MouseUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If seriesPointAnimationStoryboard Is Nothing OrElse SeriesPointAnimation Is Nothing OrElse (Not IsClick(e.Timestamp)) Then
                Return
            End If
            Dim hitInfo As ChartHitInfo = chart.CalcHitInfo(e.GetPosition(chart))
            If hitInfo IsNot Nothing AndAlso hitInfo.SeriesPoint IsNot Nothing Then
                SeriesPointAnimation.TargetSeriesPoint = hitInfo.SeriesPoint
                seriesPointAnimationStoryboard.Begin(chart)
            End If
        End Sub
        Private Sub chart_QueryChartCursor(ByVal sender As Object, ByVal e As QueryChartCursorEventArgs)
            Dim hitInfo As ChartHitInfo = chart.CalcHitInfo(e.Position)
            If hitInfo IsNot Nothing AndAlso hitInfo.SeriesPoint IsNot Nothing AndAlso Mouse.PrimaryDevice.LeftButton = MouseButtonState.Released Then
                e.Cursor = Cursors.Hand
            End If
        End Sub
    End Class

    Public Class CustomBarModelInfo
        Inherits BindableBase


        Private model_Renamed As CustomBar3DModel

        Private points_Renamed As List(Of NumericPoint)

        Public Property Model() As CustomBar3DModel
            Get
                Return model_Renamed
            End Get
            Set(ByVal value As CustomBar3DModel)
                SetProperty(model_Renamed, value, Function() Model)
            End Set
        End Property
        Public Property Points() As List(Of NumericPoint)
            Get
                Return points_Renamed
            End Get
            Set(ByVal value As List(Of NumericPoint))
                SetProperty(points_Renamed, value, Function() Points)
            End Set
        End Property
        Public Property BarWidth() As Double
            Get
                Return GetProperty(Function() BarWidth)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() BarWidth, value)
            End Set
        End Property
        Public Property SeriesPadding() As Double
            Get
                Return GetProperty(Function() SeriesPadding)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() SeriesPadding, value)
            End Set
        End Property
    End Class
End Namespace
