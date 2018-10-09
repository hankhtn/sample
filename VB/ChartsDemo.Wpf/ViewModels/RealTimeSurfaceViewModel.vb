Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Threading
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo
    Public Class RealTimeSurfaceViewModel
        Private Const ValueFactor As Double = 0.5
        Private Const Interval As Integer = 40

        Private ReadOnly dataGenerator As DataGenerator
        Private ReadOnly timer As DispatcherTimer
        Private inProcess As Boolean = True

        Public Overridable Property Values() As IEnumerable(Of Double)
        Public Overridable Property FillStyle() As FillStyleBase
        Public Overridable Property Size() As Integer
        Public Overridable Property IsTimerEnabled() As Boolean
        Public Overridable Property ArgumentsX() As IEnumerable(Of Object)
        Public Overridable Property ArgumentsY() As IEnumerable(Of Object)
        Public Overridable Property MinValue() As Double
        Public Overridable Property MaxValue() As Double

        Public Sub New()
            Me.dataGenerator = New DataGenerator()
            Me.timer = CreateTimer()
            IsTimerEnabled = True
            Size = 50
        End Sub

        Public Sub DisableProcess()
            inProcess = timer.IsEnabled
            IsTimerEnabled = False
        End Sub
        Public Sub RestoreProcess()
            IsTimerEnabled = inProcess
        End Sub
        Public Sub ToggleIsTimerEnabled()
            IsTimerEnabled = Not IsTimerEnabled
        End Sub

        Protected Sub OnIsTimerEnabledChanged()
            timer.IsEnabled = IsTimerEnabled
        End Sub
        Protected Sub OnSizeChanged()
            Dim oldIsEnabled = timer.IsEnabled
            timer.IsEnabled = False

            dataGenerator.Size = Size + 1
            MinValue = -dataGenerator.Size \ 3 - 1.5
            MaxValue = dataGenerator.Size \ 3 + 1.5
            UpdateDataSource()
            UpdateValues()

            timer.IsEnabled = oldIsEnabled
        End Sub

        Private Function CreateTimer() As DispatcherTimer
            Dim timer As New DispatcherTimer()
            timer.Interval = TimeSpan.FromMilliseconds(Interval)
            AddHandler timer.Tick, Sub(_d, _e) UpdateValues()
            Return timer
        End Function
        Private Function CreateGradientFillStyle() As FillStyleBase
            Dim style As New GradientFillStyle()
            style.ColorStops.Add(New ColorStop() With {.Offset = New Unit(MaxValue)})
            style.ColorStops.Add(New ColorStop() With {.Offset = New Unit(0)})
            style.ColorStops.Add(New ColorStop() With {.Offset = New Unit(MinValue)})
            Return style
        End Function
        Private Sub UpdateDataSource()
            Dim arguments As IEnumerable(Of Object) = dataGenerator.GenerateArguments()
            ArgumentsX = arguments
            ArgumentsY = arguments
            dataGenerator.RecreateElevations()
            FillStyle = CreateGradientFillStyle()
        End Sub
        Private Sub UpdateValues()
            Values = dataGenerator.GenerateValues()
        End Sub
    End Class
End Namespace
