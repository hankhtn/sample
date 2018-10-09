Imports DevExpress.Mvvm
Imports System
Imports System.Windows.Input
Imports System.Windows.Threading

Namespace GaugesDemo
    Public Class CarDashboardViewModel
        Private ReadOnly timer As New DispatcherTimer()
        Private ReadOnly timerInitialAnimation As New DispatcherTimer()
        Private ReadOnly timerUpdateDateTime As New DispatcherTimer()
        Private ReadOnly Property GearInteval() As Double
            Get
                Return 0.8 * (MaxSpeed / GearCount)
            End Get
        End Property
        Private Sub OnTimedEventInitialAnimation(ByVal source As Object, ByVal e As EventArgs)
            timerInitialAnimation.Stop()
            Speed = 0
            TachometerValue = 0
            timer.Start()
        End Sub
        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            Update(timer.Interval.TotalSeconds)
        End Sub
        Private Sub Update(ByVal deltaTime As Double)
            UpdateSpeed(10 * deltaTime)
            Gear = CInt(Math.Min(GearCount, Math.Ceiling((Speed / GearInteval))))
            TachometerValue = If(Gear > 0, TachometerMaxValue * (Speed - GearInteval * (Gear - 1)) / GearInteval, 0)
            TachometerValue = Math.Max(0, Math.Min(TachometerMaxValue, TachometerValue))
            FuelLevel -= TachometerValue / TachometerMaxValue / 1000
            If ((TachometerMaxValue / 2) < TachometerValue) OrElse (CurrentEngineTemperature < NormalEngineTemperature) Then
                CurrentEngineTemperature += TachometerValue / TachometerMaxValue / 2.5
            Else
                CurrentEngineTemperature -= (TachometerMaxValue - TachometerValue) / TachometerMaxValue / 2.5
            End If
            CurrentEngineTemperature = Math.Min(MaxEngineTemperature, CurrentEngineTemperature)
        End Sub
        Private Sub UpdateSpeed(ByVal delta As Double)
            If IsAcceleratePressed Then
                Speed += delta
            Else
                If IsBrakePressed Then
                    Speed -= delta
                End If
            End If
            Speed = Math.Max(0, Math.Min(MaxSpeed, Speed))
        End Sub
        Public Sub Start()
            timer.Stop()
            Speed = MaxSpeed
            TachometerValue = TachometerMaxValue
            timerInitialAnimation.Start()
        End Sub
        Private Sub updateTimerAndDate(ByVal source As Object, ByVal e As EventArgs)
            CurrentTime = Date.Now.ToShortTimeString()
            CurrentDate = Date.Now.ToShortDateString()
        End Sub
        Public Sub New()
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Interval = TimeSpan.FromMilliseconds(500)
            AddHandler timerInitialAnimation.Tick, AddressOf OnTimedEventInitialAnimation
            timerInitialAnimation.Interval = TimeSpan.FromMilliseconds(800)
            CurrentTime = Date.Now.ToShortTimeString()
            CurrentDate = Date.Now.ToShortDateString()
            timerUpdateDateTime.Interval = New TimeSpan(0, 0, 1)
            AddHandler timerUpdateDateTime.Tick, AddressOf updateTimerAndDate
            timerUpdateDateTime.Start()
            IsAcceleratePressed = False
            IsBrakePressed = False
            MaxSpeed = 120.0
            Speed = 0
            NormalEngineTemperature = 85
            MaxEngineTemperature = 130
            CurrentEngineTemperature = 20
            TachometerMaxValue = 8000
            TachometerValue = 900
            GearCount = 6
            Gear = 0
            FuelLevel = 0.75
            CurrentTime = ""
            CurrentDate = ""
        End Sub
        Public Overridable Property IsAcceleratePressed() As Boolean
        Public Overridable Property IsBrakePressed() As Boolean
        Public Overridable Property MaxSpeed() As Double
        Public Overridable Property Speed() As Double
        Public Overridable Property NormalEngineTemperature() As Double
        Public Overridable Property MaxEngineTemperature() As Double
        Public Overridable Property CurrentEngineTemperature() As Double
        Public Overridable Property TachometerMaxValue() As Double
        Public Overridable Property TachometerValue() As Double
        Public Overridable Property GearCount() As Integer
        Public Overridable Property Gear() As Integer
        Public Overridable Property FuelLevel() As Double
        Public Overridable Property CurrentTime() As String
        Public Overridable Property CurrentDate() As String
    End Class
End Namespace
