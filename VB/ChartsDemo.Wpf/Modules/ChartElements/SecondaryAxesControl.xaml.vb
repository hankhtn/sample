Imports System
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/ChartElements/SecondaryAxesControl.xaml"), CodeFile("Modules/ChartElements/SecondaryAxesControl.xaml.(cs)")>
    Partial Public Class SecondaryAxesControl
        Inherits ChartsDemoModule

        Private Const SecondaryPostfixX As String = " - Secondary Axis X"
        Private Const SecondaryPostfixY As String = " - Secondary Axis Y"
        Private Const PrimaryPostfixX As String = " - Primary Axis X"
        Private Const PrimaryPostfixY As String = " - Primary Axis Y"

        Private firstRun As Boolean = True

        Private ReadOnly Property FirstSeries() As XYSeries2D
            Get
                Return If(chart.Diagram.Series.Count > 0, CType(chart.Diagram.Series(0), XYSeries2D), Nothing)
            End Get
        End Property
        Private ReadOnly Property SecondSeries() As XYSeries2D
            Get
                Return If(chart.Diagram.Series.Count > 1, CType(chart.Diagram.Series(1), XYSeries2D), Nothing)
            End Get
        End Property
        Private ReadOnly Property AxisX() As AxisX2D
            Get
                Return CType(chart.Diagram, XYDiagram2D).AxisX
            End Get
        End Property
        Private ReadOnly Property AxisY() As AxisY2D
            Get
                Return CType(chart.Diagram, XYDiagram2D).AxisY
            End Get
        End Property
        Private ReadOnly Property SecondaryAxisX() As SecondaryAxisX2D
            Get
                Return CType(chart.Diagram, XYDiagram2D).SecondaryAxesX(0)
            End Get
        End Property
        Private ReadOnly Property SecondaryAxisY() As SecondaryAxisY2D
            Get
                Return CType(chart.Diagram, XYDiagram2D).SecondaryAxesY(0)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            CreateSeries(New LineSeries2D(), New LineSeries2D())
            SecondaryAxisX.Title.Content = SecondSeries.DisplayName & SecondaryPostfixX
            SecondaryAxisY.Title.Content = SecondSeries.DisplayName & SecondaryPostfixY
            lbChartType.SelectedIndex = 0
            lbSeries2AxisX.SelectedIndex = 0
            lbSeries2AxisY.SelectedIndex = 1
        End Sub
        Private Sub SecondaryAxesControl_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
            firstRun = False
        End Sub
        Private Sub PrepareSeries1(ByVal series As Series)
            series.DisplayName = "Series 1"
            series.Label = New SeriesLabel()
            series.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAroundPoint
            If chbVisible.IsChecked.HasValue Then
                series.LabelsVisibility = chbVisible.IsChecked.Value
            End If
            series.Points.Add(New SeriesPoint("A", 40))
            series.Points.Add(New SeriesPoint("B", 30))
            series.Points.Add(New SeriesPoint("C", 25))
            series.Points.Add(New SeriesPoint("D", 22.5))
            series.Points.Add(New SeriesPoint("E", 21.25))
        End Sub
        Private Sub PrepareSeries2(ByVal series As Series)
            series.DisplayName = "Series 2"
            series.Label = New SeriesLabel()
            series.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAroundPoint
            If chbVisible.IsChecked.HasValue Then
                series.LabelsVisibility = chbVisible.IsChecked.Value
            End If
            series.Points.Add(New SeriesPoint("A", 1700))
            series.Points.Add(New SeriesPoint("B", 900))
            series.Points.Add(New SeriesPoint("C", 500))
            series.Points.Add(New SeriesPoint("D", 300))
            series.Points.Add(New SeriesPoint("E", 200))
            series.Points.Add(New SeriesPoint("F", 150))
            series.Points.Add(New SeriesPoint("G", 125))
        End Sub
        Private Sub CreateSeries(ByVal series1 As XYSeries2D, ByVal series2 As XYSeries2D)
            series1.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState
            series2.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState
            chart.Diagram.Series.Clear()
            chart.Diagram.Series.Add(series1)
            PrepareSeries1(series1)
            chart.Diagram.Series.Add(series2)
            PrepareSeries2(series2)
            If lbSeries2AxisX.SelectedIndex = 1 Then
                SecondSeries.AxisX = SecondaryAxisX
            End If
            If lbSeries2AxisY.SelectedIndex = 1 Then
                SecondSeries.AxisY = SecondaryAxisY
            End If
        End Sub
        Private Sub lbChartType_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If chart IsNot Nothing Then
                If lbChartType.SelectedIndex = 0 Then
                    Dim lineSeries1 As New LineSeries2D()
                    Dim lineSeries2 As New LineSeries2D()
                    lineSeries1.SeriesAnimation = New Line2DUnwrapVerticallyAnimation()
                    lineSeries1.PointAnimation = New Marker2DSlideFromTopAnimation()
                    lineSeries2.SeriesAnimation = New Line2DUnwrapVerticallyAnimation() With {.BeginTime = TimeSpan.FromMilliseconds(400)}
                    lineSeries2.PointAnimation = New Marker2DSlideFromTopAnimation() With {.BeginTime = TimeSpan.FromMilliseconds(400)}
                    CreateSeries(lineSeries1, lineSeries2)
                Else
                    Dim barSeries1 As New BarSideBySideSeries2D()
                    Dim barSeries2 As New BarSideBySideSeries2D()
                    barSeries1.PointAnimation = New Bar2DSlideFromBottomAnimation() With {.PointDelay = TimeSpan.FromMilliseconds(200)}
                    barSeries2.PointAnimation = New Bar2DSlideFromBottomAnimation() With {.PointDelay = TimeSpan.FromMilliseconds(200), .BeginTime = TimeSpan.FromMilliseconds(100)}
                    CreateSeries(barSeries1, barSeries2)
                End If
                If Not firstRun Then
                    chart.Animate()
                End If
            End If
        End Sub
        Private Sub lbSeries2AxisX_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If chart IsNot Nothing AndAlso SecondSeries IsNot Nothing Then
                If lbSeries2AxisX.SelectedIndex = 0 Then
                    SecondSeries.AxisX = Nothing
                    SecondaryAxisX.Visible = False
                    AxisX.Title.Content = FirstSeries.DisplayName & ", " & SecondSeries.DisplayName & PrimaryPostfixX
                Else
                    SecondSeries.AxisX = SecondaryAxisX
                    SecondaryAxisX.Visible = True
                    AxisX.Title.Content = FirstSeries.DisplayName & PrimaryPostfixX
                End If
            End If
        End Sub
        Private Sub lbSeries2AxisY_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If chart IsNot Nothing AndAlso SecondSeries IsNot Nothing Then
                If lbSeries2AxisY.SelectedIndex = 0 Then
                    SecondSeries.AxisY = Nothing
                    SecondaryAxisY.Visible = False
                    AxisY.Title.Content = FirstSeries.DisplayName & ", " & SecondSeries.DisplayName & PrimaryPostfixY
                Else
                    SecondSeries.AxisY = SecondaryAxisY
                    SecondaryAxisY.Visible = True
                    AxisY.Title.Content = FirstSeries.DisplayName & PrimaryPostfixY
                End If
            End If
        End Sub
        Private Sub chbVisible_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each series As Series In chart.Diagram.Series
                series.LabelsVisibility = True
            Next series
        End Sub
        Private Sub chbVisible_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each series As Series In chart.Diagram.Series
                series.LabelsVisibility = False
            Next series
        End Sub
    End Class
End Namespace
