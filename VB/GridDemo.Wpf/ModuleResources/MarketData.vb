Imports DevExpress.Mvvm
Imports System

Namespace GridDemo
    Public Class MarketData
        Inherits BindableBase

        Private ReadOnly Shared rnd As New Random()
        Private Const Max As Double = 950
        Private Const Min As Double = 350

        Public Sub New(ByVal name As String)
            Ticker = name
            Open = NextRnd() * (Max - Min) + Min
            DayVal = Open
            UpdateInternal(Open)
        End Sub
        Private privateTicker As String
        Public Property Ticker() As String
            Get
                Return privateTicker
            End Get
            Private Set(ByVal value As String)
                privateTicker = value
            End Set
        End Property
        Private privateLast As Double
        Public Property Last() As Double
            Get
                Return privateLast
            End Get
            Private Set(ByVal value As Double)
                privateLast = value
            End Set
        End Property
        Private privateChgPercent As Double
        Public Property ChgPercent() As Double
            Get
                Return privateChgPercent
            End Get
            Private Set(ByVal value As Double)
                privateChgPercent = value
            End Set
        End Property
        Private privateOpen As Double
        Public Property Open() As Double
            Get
                Return privateOpen
            End Get
            Private Set(ByVal value As Double)
                privateOpen = value
            End Set
        End Property
        Private privateHigh As Double
        Public Property High() As Double
            Get
                Return privateHigh
            End Get
            Private Set(ByVal value As Double)
                privateHigh = value
            End Set
        End Property
        Private privateLow As Double
        Public Property Low() As Double
            Get
                Return privateLow
            End Get
            Private Set(ByVal value As Double)
                privateLow = value
            End Set
        End Property
        Private privateDayVal As Double
        Public Property DayVal() As Double
            Get
                Return privateDayVal
            End Get
            Private Set(ByVal value As Double)
                privateDayVal = value
            End Set
        End Property

        Public Sub Update()
            Dim value As Double = DayVal - (Max - Min) * 0.05 + NextRnd() * (Max - Min) * 0.1
            If value <= Min Then
                value = Min
            End If
            If value >= Max Then
                value = Max
            End If
            UpdateInternal(value)
        End Sub
        Private Sub UpdateInternal(ByVal value As Double)
            Last = DayVal
            DayVal = value
            ChgPercent = (DayVal - Last) * 100.0 / DayVal
            High = Math.Max(Open, Math.Max(DayVal, Last))
            Low = Math.Min(Open, Math.Min(DayVal, Last))
            RaisePropertyChanged("")
        End Sub
        Private Shared Function NextRnd() As Double
            Dim value As Double = 0
            For i As Integer = 0 To 4
                value += rnd.NextDouble()
            Next i
            Return value / 5
        End Function
    End Class
End Namespace
