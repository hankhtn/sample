Imports System
Imports System.Windows
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Threading
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.Gauges

Namespace GaugesDemo
    Partial Public Class Interactivity
        Inherits GaugesDemoModule

        Private Const deltaDelay As Double = 0.2
        Private Const toolTipOffset As Integer = 15

        Private offsetData As Double = -1
        Private selectedNeedle As ArcScaleNeedle = Nothing
        Private timer As New DispatcherTimer()

        Private Property HorizontallPosition() As Double
            Get
                Return (horizontallAxisRange.ActualMinValueInternal + horizontallAxisRange.ActualMaxValueInternal) / 2
            End Get
            Set(ByVal value As Double)
                horizontallAxisRange.SetMinMaxValues(value - 0.5 * HorizontallSensitivity, value + 0.5 * HorizontallSensitivity)
            End Set
        End Property
        Private Property HorizontallSensitivity() As Double
            Get
                Return horizontallAxisRange.ActualMaxValueInternal - horizontallAxisRange.ActualMinValueInternal
            End Get
            Set(ByVal value As Double)
                horizontallAxisRange.SetMinMaxValues(HorizontallPosition - 0.5 * value, HorizontallPosition + 0.5 * value)
            End Set
        End Property
        Private Property VerticalPosition() As Double
            Get
                Return (verticalAxisRange.ActualMinValueInternal + verticalAxisRange.ActualMaxValueInternal) / 2
            End Get
            Set(ByVal value As Double)
                verticalAxisRange.SetMinMaxValues(value - 0.5 * VerticalSensitivity, value + 0.5 * VerticalSensitivity)
            End Set
        End Property
        Private Property VerticalSensitivity() As Double
            Get
                Return verticalAxisRange.ActualMaxValueInternal - verticalAxisRange.ActualMinValueInternal
            End Get
            Set(ByVal value As Double)
                verticalAxisRange.SetMinMaxValues(VerticalPosition - 0.5 * value, VerticalPosition + 0.5 * value)
            End Set
        End Property
        Private Property TriggerLavel() As Double
            Get
                Return Convert.ToDouble(сonstantLine.Value)
            End Get
            Set(ByVal value As Double)
                сonstantLine.Value = value
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            CreateOscilloscopeGrid()
            UpdateData()
            timer.Interval = New TimeSpan(0, 0, 0, 0, 50)
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Start()
        End Sub
        Private Sub VerticalSensitivityNeedle_ValueChanged(ByVal sender As Object, ByVal e As ValueChangedEventArgs)
            If verticalAxisRange IsNot Nothing AndAlso Math.Abs(VerticalSensitivity - e.NewValue) >= deltaDelay Then
                VerticalSensitivity = e.NewValue
                UpdateData()
            End If
        End Sub
        Private Sub HorizontallSensitivityNeedle_ValueChanged(ByVal sender As Object, ByVal e As ValueChangedEventArgs)
            If horizontallAxisRange IsNot Nothing AndAlso Math.Abs(HorizontallSensitivity - e.NewValue) >= deltaDelay Then
                HorizontallSensitivity = e.NewValue
                UpdateData()
            End If
        End Sub
        Private Sub VerticalPositionNeedle_ValueChanged(ByVal sender As Object, ByVal e As ValueChangedEventArgs)
            If verticalAxisRange IsNot Nothing AndAlso Math.Abs(VerticalPosition - e.NewValue) >= deltaDelay Then
                VerticalPosition = e.NewValue
                UpdateData()
            End If
        End Sub
        Private Sub HorizontallPosition_ValueChanged(ByVal sender As Object, ByVal e As ValueChangedEventArgs)
            If horizontallAxisRange IsNot Nothing AndAlso Math.Abs(HorizontallPosition - e.NewValue) >= deltaDelay Then
                HorizontallPosition = e.NewValue
                UpdateData()
            End If
        End Sub
        Private Sub ReferenceVoltageNeedle_ValueChanged(ByVal sender As Object, ByVal e As ValueChangedEventArgs)
            If сonstantLine IsNot Nothing AndAlso Math.Abs(TriggerLavel - e.NewValue) >= deltaDelay Then
                TriggerLavel = e.NewValue
                UpdateData()
            End If
        End Sub
        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            UpdateData()
        End Sub
        Private Sub CircularGaugeControl_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim gauge As CircularGaugeControl = DirectCast(sender, CircularGaugeControl)
            Dim currentSelectedNeedle As ArcScaleNeedle = If(selectedNeedle IsNot Nothing, selectedNeedle, gauge.CalcHitInfo(e.GetPosition(gauge)).Needle)
            If currentSelectedNeedle IsNot Nothing Then
                ShowTooltip(currentSelectedNeedle, Me, e.GetPosition(Me))
            Else
                HideTooltip()
            End If
        End Sub
        Private Sub CircularGaugeControl_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim gauge As CircularGaugeControl = TryCast(sender, CircularGaugeControl)
            If gauge IsNot Nothing Then
                Dim hitInfo As CircularGaugeHitInfo = gauge.CalcHitInfo(e.GetPosition(gauge))
                If hitInfo.InNeedle Then
                    selectedNeedle = hitInfo.Needle
                End If
            End If
        End Sub
        Private Sub CircularGaugeControl_MouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            selectedNeedle = Nothing
            HideTooltip()
        End Sub
        Private Sub CircularGaugeControl_MouseLeave(ByVal sender As Object, ByVal e As MouseEventArgs)
            selectedNeedle = Nothing
            HideTooltip()
        End Sub
        Private Sub HideTooltip()
            ttContent.Text = ""
            needleTooltip.IsOpen = False
            Cursor = Cursors.Arrow
        End Sub
        Private Sub ShowTooltip(ByVal needle As ArcScaleNeedle, ByVal placementTarget As UIElement, ByVal position As Point)
            ttContent.Text = String.Format("Value = {0:F2}", needle.Value)
            needleTooltip.Placement = PlacementMode.RelativePoint
            needleTooltip.PlacementTarget = placementTarget
            needleTooltip.HorizontalOffset = position.X + toolTipOffset
            needleTooltip.VerticalOffset = position.Y + toolTipOffset
            needleTooltip.IsOpen = True
            Cursor = Cursors.Hand
        End Sub
        Private Sub UpdateData()
            If enabledSignalUpCheckEdit IsNot Nothing Then
                If Math.Abs(TriggerLavel) <= 1 Then
                    offsetData = (If(enabledSignalUpCheckEdit.IsChecked.Value, (0), (1)))
                Else
                    offsetData += 0.5
                    If offsetData > 1 Then
                        offsetData = -1
                    End If
                    If offsetData < -1 Then
                        offsetData = 1
                    End If
                End If
                lineStepSeries2D.Points.BeginInit()
                lineStepSeries2D.Points.Clear()
                For i As Integer = -25 To 24
                    Dim yValue As Double = Math.Abs(i Mod 2) * 2 - 1
                    Dim seriesPoint As New SeriesPoint(i + offsetData, yValue)
                    lineStepSeries2D.Points.Add(seriesPoint)
                Next i
                lineStepSeries2D.Points.EndInit()
            End If
        End Sub
        Private Sub CreateOscilloscopeGrid()
            Dim majorConstantLineBrush As New SolidColorBrush(Color.FromArgb(&H80, &H4B,&HC7, &HB9))
            Dim minorConstantLineBrush As New SolidColorBrush(Color.FromArgb(&H29, &H4B, &HC7, &HB9))
            For i As Double = 0.25 To 3 Step 0.25
                Dim constantLineX As New ConstantLine()
                Dim constantLineY As New ConstantLine()
                constantLineY.Value = i
                constantLineX.Value = constantLineY.Value
                constantLineY.Brush = If((i / 0.25) Mod 2 = 0, majorConstantLineBrush, minorConstantLineBrush)
                constantLineX.Brush = constantLineY.Brush
                gridAxisX.ConstantLinesBehind.Add(constantLineX)
                gridAxisY.ConstantLinesBehind.Add(constantLineY)
            Next i
        End Sub
    End Class
End Namespace
