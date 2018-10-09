Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Threading
Imports System.Windows.Media
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Gauges
Imports DevExpress.Mvvm

Namespace GaugesDemo
    Partial Public Class CustomSymbolMapping
        Inherits GaugesDemoModule

        Public Sub New()
            Dim dataGenerator As New TrafficLightDataGenerator()
            InitializeComponent()
            trafficLightsGrid.DataContext = dataGenerator
        End Sub
    End Class
    Public Enum TrafficLightStates
        RedLightEnabled
        GreenRightLightBlinking
        YellowRedLightEnabled
        GreenLeftLightEnabeld
        GreenLeftLightBlinking
        YellowLightEnabled
    End Enum

    Public Class TrafficLightDataGenerator
        Inherits BindableBase

        Public Property ExpectationTime() As String
            Get
                Return GetProperty(Function() ExpectationTime)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() ExpectationTime, value)
            End Set
        End Property
        Public Property IsTimerGreen() As Boolean
            Get
                Return GetProperty(Function() IsTimerGreen)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Function() IsTimerGreen, value)
            End Set
        End Property
        Public Property IsRedSegmentEnabled() As Boolean
            Get
                Return GetProperty(Function() IsRedSegmentEnabled)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Function() IsRedSegmentEnabled, value)
            End Set
        End Property
        Public Property IsYellowSegmentEnabled() As Boolean
            Get
                Return GetProperty(Function() IsYellowSegmentEnabled)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Function() IsYellowSegmentEnabled, value)
            End Set
        End Property
        Public Property IsGreenLeftSegmentEnabled() As Boolean
            Get
                Return GetProperty(Function() IsGreenLeftSegmentEnabled)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Function() IsGreenLeftSegmentEnabled, value)
            End Set
        End Property
        Public Property IsGreenRightSegmentEnabled() As Boolean
            Get
                Return GetProperty(Function() IsGreenRightSegmentEnabled)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Function() IsGreenRightSegmentEnabled, value)
            End Set
        End Property
        Public Property IsGangerRedSegmentEnabled() As Boolean
            Get
                Return GetProperty(Function() IsGangerRedSegmentEnabled)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Function() IsGangerRedSegmentEnabled, value)
            End Set
        End Property
        Public Property IsGangerGreenSegmentEnabled() As Boolean
            Get
                Return GetProperty(Function() IsGangerGreenSegmentEnabled)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Function() IsGangerGreenSegmentEnabled, value)
            End Set
        End Property

        Private Const gangerGreenLightCycles As Integer = 2
        Private Const gangerRedLightCycles As Integer = 4
        Private Const changeTicksCount As Integer = 3
        Private Const startCycles As Integer = 3

        Private Shared redSegmentStates() As Boolean = { True, True, True, False, False, False }
        Private Shared yellowSegmentStates() As Boolean = { False, False, True, False, False, True }
        Private Shared greenLeftSegmentStates() As Boolean = { False, False, False, True, True, False }
        Private Shared greenRightSegmentStates() As Boolean = { True, True, False, False, False, False }
        Private Shared greenLeftBlinkingStates() As Boolean = { False, False, False, False, True, False }
        Private Shared greenRightBlinkingStates() As Boolean = { False, True, False, False, False, False }
        Private Shared gangerGreenSegmentStates() As Boolean = { False, False, False, True, True, False }
        Private Shared gangerRedSegmentStates() As Boolean = { True, True, True, False, False, True }
        Private Shared gangerGreenBlinkingStates() As Boolean = { False, False, False, False, True, False }
        Private Shared blinkingStates() As Boolean = { False, True, False, False, True, False }


        Private blinkingTimer As New DispatcherTimer()
        Private timer As New DispatcherTimer()
        Private currentState As TrafficLightStates = TrafficLightStates.RedLightEnabled
        Private expectationTicksCount As Integer
        Private currentChangeTicksCount As Integer
        Private isGreenLeftLightBlinking As Boolean = False
        Private isGreenRightLightBlinking As Boolean = False
        Private isGangerGreenLightBlinking As Boolean = False

        Public Sub New()
            ExpectationTime = ""
            IsTimerGreen = False
            IsRedSegmentEnabled = True
            IsYellowSegmentEnabled = False
            IsGreenLeftSegmentEnabled = False
            IsGreenRightSegmentEnabled = True
            IsGangerRedSegmentEnabled = True
            IsGangerGreenSegmentEnabled = False

            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Interval = New TimeSpan(0, 0, 1)
            expectationTicksCount = startCycles * changeTicksCount
            AddHandler blinkingTimer.Tick, AddressOf OnBlinkingTimedEvent
            blinkingTimer.Interval = New TimeSpan(0, 0, 0, 0, 300)
            currentChangeTicksCount = changeTicksCount
            timer.Start()
        End Sub
        Private Sub ChangeTrafficLite()
            If currentState = TrafficLightStates.GreenLeftLightBlinking Then
                expectationTicksCount = gangerRedLightCycles * changeTicksCount
            End If
            If currentState = TrafficLightStates.YellowRedLightEnabled Then
                expectationTicksCount = gangerGreenLightCycles * changeTicksCount
            End If
            If currentState = TrafficLightStates.YellowLightEnabled Then
                currentState = TrafficLightStates.RedLightEnabled
            Else
                currentState = CType(CInt(currentState) + 1, TrafficLightStates)
            End If
            IsGreenLeftSegmentEnabled = greenLeftSegmentStates(CInt(currentState))
            IsGreenRightSegmentEnabled = greenRightSegmentStates(CInt(currentState))
            IsYellowSegmentEnabled = yellowSegmentStates(CInt(currentState))
            IsRedSegmentEnabled = redSegmentStates(CInt(currentState))
            IsGangerGreenSegmentEnabled = gangerGreenSegmentStates(CInt(currentState))
            IsGangerRedSegmentEnabled = gangerRedSegmentStates(CInt(currentState))
            IsTimerGreen = Not IsGangerRedSegmentEnabled
            isGreenLeftLightBlinking = greenLeftBlinkingStates(CInt(currentState))
            isGreenRightLightBlinking = greenRightBlinkingStates(CInt(currentState))
            isGangerGreenLightBlinking = gangerGreenBlinkingStates(CInt(currentState))
            If blinkingStates(CInt(currentState)) Then
                blinkingTimer.Start()
            Else
                blinkingTimer.Stop()
            End If
        End Sub
        Private Sub ChangeExpectationTime()
            expectationTicksCount -= 1
            ExpectationTime = expectationTicksCount.ToString()
        End Sub
        Private Sub ChangeBlinkingState()
            If isGangerGreenLightBlinking Then
                IsGangerGreenSegmentEnabled = Not IsGangerGreenSegmentEnabled
            End If
            If isGreenLeftLightBlinking Then
                IsGreenLeftSegmentEnabled = Not IsGreenLeftSegmentEnabled
            End If
            If isGreenRightLightBlinking Then
                IsGreenRightSegmentEnabled = Not IsGreenRightSegmentEnabled
            End If
        End Sub
        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            If currentChangeTicksCount = 0 Then
                currentChangeTicksCount = changeTicksCount
                ChangeTrafficLite()
            End If
            currentChangeTicksCount -= 1
            ChangeExpectationTime()
        End Sub
        Private Sub OnBlinkingTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            ChangeBlinkingState()
        End Sub
    End Class

    Public Class SegmentsStatesProvider
        Private converter As New StatesMaskConverter()

        Public ReadOnly Property RoundSegmentsMappingMask() As StatesMask
            Get
                Return CType(converter.ConvertFromString("0 0 0 0 1 1 1 1 1 0 0 0 0" & ControlChars.CrLf & _
"                                                                                                            0 0 1 1 1 1 1 1 1 1 1 0 0" & ControlChars.CrLf & _
"                                                                                                            0 1 1 1 1 1 1 1 1 1 1 1 0" & ControlChars.CrLf & _
"                                                                                                            0 1 1 1 1 1 1 1 1 1 1 1 0" & ControlChars.CrLf & _
"                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1" & ControlChars.CrLf & _
"                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1" & ControlChars.CrLf & _
"                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1" & ControlChars.CrLf & _
"                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1" & ControlChars.CrLf & _
"                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1" & ControlChars.CrLf & _
"                                                                                                            0 1 1 1 1 1 1 1 1 1 1 1 0" & ControlChars.CrLf & _
"                                                                                                            0 1 1 1 1 1 1 1 1 1 1 1 0" & ControlChars.CrLf & _
"                                                                                                            0 0 1 1 1 1 1 1 1 1 1 0 0" & ControlChars.CrLf & _
"                                                                                                            0 0 0 0 1 1 1 1 1 0 0 0 0"), StatesMask)
            End Get
        End Property
        Public ReadOnly Property ArrowSegmentsMappingMask() As StatesMask
            Get
                Return CType(converter.ConvertFromString("0 0 0 0 0 0 0 0 0 0 0 0 0" & ControlChars.CrLf & _
"                                                                                                            0 0 0 0 0 0 0 1 0 0 0 0 0" & ControlChars.CrLf & _
"                                                                                                            0 0 0 0 0 0 0 1 1 0 0 0 0" & ControlChars.CrLf & _
"                                                                                                            0 0 0 0 0 0 0 1 1 1 0 0 0" & ControlChars.CrLf & _
"                                                                                                            1 1 1 1 1 1 1 1 1 1 1 0 0" & ControlChars.CrLf & _
"                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 0" & ControlChars.CrLf & _
"                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1" & ControlChars.CrLf & _
"                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 0" & ControlChars.CrLf & _
"                                                                                                            1 1 1 1 1 1 1 1 1 1 1 0 0" & ControlChars.CrLf & _
"                                                                                                            0 0 0 0 0 0 0 1 1 1 0 0 0" & ControlChars.CrLf & _
"                                                                                                            0 0 0 0 0 0 0 1 1 0 0 0 0" & ControlChars.CrLf & _
"                                                                                                            0 0 0 0 0 0 0 1 0 0 0 0 0" & ControlChars.CrLf & _
"                                                                                                            0 0 0 0 0 0 0 0 0 0 0 0 0"), StatesMask)
            End Get
        End Property
    End Class
End Namespace
