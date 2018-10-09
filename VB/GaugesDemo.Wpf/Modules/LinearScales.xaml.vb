Imports System
Imports System.Windows.Threading
Imports DevExpress.Mvvm

Namespace GaugesDemo
    Partial Public Class LinearScales
        Inherits GaugesDemoModule

        Public Sub New()
            InitializeComponent()
            Dim dataGenerator As New EqualizerDataGenerator()
            gauge.DataContext = dataGenerator
        End Sub
    End Class

    Public Class EqualizerDataGenerator
        Inherits BindableBase

        Public Property Frequency32() As Double
            Get
                Return GetProperty(Function() Frequency32)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Frequency32, value)
            End Set
        End Property
        Public Property Frequency64() As Double
            Get
                Return GetProperty(Function() Frequency64)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Frequency64, value)
            End Set
        End Property
        Public Property Frequency125() As Double
            Get
                Return GetProperty(Function() Frequency125)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Frequency125, value)
            End Set
        End Property
        Public Property Frequency250() As Double
            Get
                Return GetProperty(Function() Frequency250)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Frequency250, value)
            End Set
        End Property
        Public Property Frequency500() As Double
            Get
                Return GetProperty(Function() Frequency500)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Frequency500, value)
            End Set
        End Property
        Public Property Frequency1K() As Double
            Get
                Return GetProperty(Function() Frequency1K)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Frequency1K, value)
            End Set
        End Property
        Public Property Frequency2K() As Double
            Get
                Return GetProperty(Function() Frequency2K)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Frequency2K, value)
            End Set
        End Property
        Public Property Frequency4K() As Double
            Get
                Return GetProperty(Function() Frequency4K)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Frequency4K, value)
            End Set
        End Property
        Public Property Frequency8K() As Double
            Get
                Return GetProperty(Function() Frequency8K)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Frequency8K, value)
            End Set
        End Property
        Public Property Frequency16K() As Double
            Get
                Return GetProperty(Function() Frequency16K)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Frequency16K, value)
            End Set
        End Property

        Private Const MaxValue As Integer = 100
        Private Const UpdateIneterval As Integer = 500

        Private ReadOnly random As New Random()
        Private ReadOnly timer As New DispatcherTimer()

        Public Sub New()
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Interval = New TimeSpan(0, 0, 0, 0, UpdateIneterval)
            timer.Start()
        End Sub
        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            Frequency32 = random.Next(MaxValue)
            Frequency64 = random.Next(MaxValue)
            Frequency125 = random.Next(MaxValue)
            Frequency250 = random.Next(MaxValue)
            Frequency500 = random.Next(MaxValue)
            Frequency1K = random.Next(MaxValue)
            Frequency2K = random.Next(MaxValue)
            Frequency4K = random.Next(MaxValue)
            Frequency8K = random.Next(MaxValue)
            Frequency16K = random.Next(MaxValue)
        End Sub
    End Class
End Namespace
