Imports System
Imports System.Windows
Imports System.Windows.Threading
Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Gauges

Namespace GaugesDemo
    Partial Public Class LinearRanges
        Inherits GaugesDemoModule

        Private Const cloudyIndex As Integer = 0
        Private Const snowyIndex As Integer = 1
        Private Const rainyIndex As Integer = 2
        Private Const sunnyIndex As Integer = 3

        Private pressure As PressureState
        Private temperature As TemperatureState = TemperatureState.High

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub LowRangeIndicatorEnter(ByVal sender As Object, ByVal e As IndicatorEnterEventArgs)
            pressure = PressureState.Low
            UpdateWeatherState()
        End Sub
        Private Sub LowRangeIndicatorLeave(ByVal sender As Object, ByVal e As IndicatorLeaveEventArgs)
            Dim range As LinearScaleRange = TryCast(sender, LinearScaleRange)
            If range IsNot Nothing AndAlso e.NewValue < range.StartValueAbsolute Then
                pressure = PressureState.Undefined
                UpdateWeatherState()
            End If
        End Sub
        Private Sub NormalRangeIndicatorEnter(ByVal sender As Object, ByVal e As IndicatorEnterEventArgs)
            pressure = PressureState.Normal
            UpdateWeatherState()
        End Sub
        Private Sub NormalRangeIndicatorLeave(ByVal sender As Object, ByVal e As IndicatorLeaveEventArgs)
            Dim range As LinearScaleRange = TryCast(sender, LinearScaleRange)
            If range IsNot Nothing Then
                pressure = If(e.NewValue < range.StartValueAbsolute, PressureState.Low, PressureState.High)
                UpdateWeatherState()
            End If
        End Sub
        Private Sub HighRangeIndicatorEnter(ByVal sender As Object, ByVal e As IndicatorEnterEventArgs)
            pressure = PressureState.High
            UpdateWeatherState()
        End Sub
        Private Sub HighRangeIndicatorLeave(ByVal sender As Object, ByVal e As IndicatorLeaveEventArgs)
            Dim range As LinearScaleRange = TryCast(sender, LinearScaleRange)
            If range IsNot Nothing Then
                pressure = If(e.NewValue < range.StartValueAbsolute, PressureState.Normal, PressureState.Undefined)
                UpdateWeatherState()
            End If
        End Sub
        Private Sub HighTemperatureIndicatorEnter(ByVal sender As Object, ByVal e As IndicatorEnterEventArgs)
            temperature = TemperatureState.High
            UpdateWeatherState()
        End Sub
        Private Sub HighTemperatureIndicatorLeave(ByVal sender As Object, ByVal e As IndicatorLeaveEventArgs)
            temperature = TemperatureState.Low
            UpdateWeatherState()
        End Sub
        Private Sub UpdateWeatherState()
            Select Case pressure
                Case PressureState.Low
                    stateIndicator.StateIndex = If(temperature = TemperatureState.Low, snowyIndex, rainyIndex)
                Case PressureState.Normal
                    stateIndicator.StateIndex = cloudyIndex
                Case PressureState.High
                    stateIndicator.StateIndex = sunnyIndex
                Case Else
                    stateIndicator.StateIndex = -1
            End Select
        End Sub
    End Class
End Namespace
