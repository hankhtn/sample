Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.ComponentModel

Namespace ControlsDemo
    Partial Public Class Moon
        Inherits UserControl
        Implements INotifyPropertyChanged

        Public Shared ReadOnly DateProperty As DependencyProperty = DependencyProperty.Register("Date", GetType(Date), GetType(Moon), New PropertyMetadata(Date.Now, New PropertyChangedCallback(AddressOf OnDateChanged)))
        Public Property [Date]() As Date
            Get
                Return DirectCast(GetValue(DateProperty), Date)
            End Get
            Set(ByVal value As Date)
                SetValue(DateProperty, value)
            End Set
        End Property
        Public ReadOnly Property Phase() As Double
            Get
                Return Calculator.Phase
            End Get
        End Property
        Public ReadOnly Property PhaseName() As String
            Get
                Return If(Calculator IsNot Nothing, Calculator.PhaseName, "")
            End Get
        End Property
        Private ReadOnly Property PhaseType() As MoonPhaseType
            Get
                Return Calculator.PhaseType
            End Get
        End Property
        Private Property Calculator() As MoonPhaseCalculator

        Public Sub New()
            InitializeComponent()
            OnDateChangedCore()
            AddHandler SizeChanged, AddressOf OnSizeChanged
        End Sub

        Protected Shared Sub OnDateChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, Moon).OnDateChangedCore()
        End Sub
        Protected Overridable Sub OnDateChangedCore()
            Calculator = New MoonPhaseCalculator([Date])
            UpdatePhase()
            RaisePropertyChangedEvent()
        End Sub
        Protected Overridable Sub OnSizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            UpdatePhase()
        End Sub
        Protected Overridable Sub UpdatePhase()
            If ActualWidth = 0 OrElse ActualHeight = 0 Then
                Return
            End If
            Dim upPoint As New Point(ActualWidth / 2, 2)
            Dim downPoint As New Point(ActualWidth / 2, ActualHeight - 2)
            Figure.StartPoint = upPoint
            UpBottomArc.Point = downPoint
            BottomUpArc.Point = upPoint
            Dim xRadius As Double = ActualWidth / 2 - 5
            Dim yRadius As Double = ActualHeight / 2 - 5
            Dim k As Double = Math.Abs(2 * Math.Abs(Phase) - 1)
            Dim upBottomK As Double = If(PhaseType < MoonPhaseType.WaningGibbous, k, 1)
            Dim bottomUpK As Double = If(PhaseType > MoonPhaseType.Full, k, 1)
            UpBottomArc.Size = New Size(xRadius * upBottomK, yRadius)
            BottomUpArc.Size = New Size(xRadius * bottomUpK, yRadius)
            UpBottomArc.SweepDirection = GetDirection((PhaseType = MoonPhaseType.WaxingGibbous OrElse PhaseType = MoonPhaseType.Full))
            BottomUpArc.SweepDirection = GetDirection(PhaseType = MoonPhaseType.WaningGibbous)
        End Sub
        Protected Shared Function GetVisibility(ByVal isVisible As Boolean) As Visibility
            Return If(isVisible, Visibility.Visible, Visibility.Collapsed)
        End Function
        Private Function GetDirection(ByVal isCounter As Boolean) As SweepDirection
            Return If(isCounter, SweepDirection.Counterclockwise, SweepDirection.Clockwise)
        End Function
        #Region "INotifyPropertyChanged Members"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Protected Sub RaisePropertyChangedEvent()
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("PhaseName"))
        End Sub
        #End Region
    End Class

    Public Enum MoonPhaseType
        [New]
        WaxingCrescent
        FirstQuarter
        WaxingGibbous
        Full
        WaningGibbous
        LastQuarter
        WaningCrescent
    End Enum

    Public Class MoonPhaseCalculator
        Private Property BeginPhase() As Double
        Private Property EndPhase() As Double
        Public Property PhaseType() As MoonPhaseType
        Public ReadOnly Property PhaseName() As String
            Get
                Return PhaseToName(PhaseType)
            End Get
        End Property
        Public ReadOnly Property Phase() As Double
            Get
                Select Case PhaseType
                    Case MoonPhaseType.[New]
                        Return 0.0
                    Case MoonPhaseType.Full
                        Return 1.0
                    Case MoonPhaseType.FirstQuarter
                        Return 0.5
                    Case MoonPhaseType.LastQuarter
                        Return -0.5
                    Case Else
                        Return (BeginPhase + EndPhase) / 2
                End Select
            End Get
        End Property
        Public Sub New(ByVal [date] As Date)
            Dim beginDateTime As New Date([date].Year, [date].Month, [date].Day, 0, 0, 0)
            Dim endDateTime As New Date([date].Year, [date].Month, [date].Day, 23, 59, 59)
            BeginPhase = MoonCalculator.GetPhase(beginDateTime)
            EndPhase = MoonCalculator.GetPhase(endDateTime)
            PhaseType = CalcPhaseType()
        End Sub
        Private Function CalcPhaseType() As MoonPhaseType
            If BeginPhase <= 0.0 AndAlso EndPhase >= 0.0 Then
                Return MoonPhaseType.[New]
            End If
            If BeginPhase >= 0.0 AndAlso EndPhase <= 0.0 Then
                Return MoonPhaseType.Full
            End If
            If BeginPhase <= 0.5 AndAlso EndPhase >= 0.5 Then
                Return MoonPhaseType.FirstQuarter
            End If
            If BeginPhase <= -0.5 AndAlso EndPhase >= -0.5 Then
                Return MoonPhaseType.LastQuarter
            End If
            If BeginPhase > 0.0 AndAlso EndPhase < 0.5 Then
                Return MoonPhaseType.WaxingCrescent
            End If
            If BeginPhase > 0.5 AndAlso EndPhase < 1.0 Then
                Return MoonPhaseType.WaxingGibbous
            End If
            If BeginPhase > -1.0 AndAlso EndPhase < -0.5 Then
                Return MoonPhaseType.WaningGibbous
            End If
            Return MoonPhaseType.WaningCrescent
        End Function
        Private Function PhaseToName(ByVal phase As MoonPhaseType) As String
            Dim source As String = phase.ToString()
            Dim arr() As Char = source.ToCharArray()
            Dim result As String = arr(0).ToString()
            For i As Integer = 1 To source.Length - 1
                Dim c As Char = arr(i)
                If Char.IsUpper(c) Then
                    result &= " "
                End If
                result &= c.ToString()
            Next i
            Return result
        End Function
    End Class
End Namespace
