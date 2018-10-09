Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Windows.Threading

Namespace ChartsDemo
    Public Class FinancialDataViewModel
        Private Const InitialUpdatesCount As Integer = 2500
        Private Const MaxPointsCount As Integer = 1000
        Private Const Interval As Double = 150
        Private ReadOnly timer As New DispatcherTimer()
        Private currentDate As Date

        Private privateDataSource As ObservableCollection(Of TraderPoint)
        Public Property DataSource() As ObservableCollection(Of TraderPoint)
            Get
                Return privateDataSource
            End Get
            Private Set(ByVal value As ObservableCollection(Of TraderPoint))
                privateDataSource = value
            End Set
        End Property
        Private privateWorkdays As ObservableCollection(Of Date)
        Public Property Workdays() As ObservableCollection(Of Date)
            Get
                Return privateWorkdays
            End Get
            Private Set(ByVal value As ObservableCollection(Of Date))
                privateWorkdays = value
            End Set
        End Property
        Public Overridable Property CurrentPoint() As TraderPoint
        Public Overridable Property IsTimerEnabled() As Boolean

        Public Sub New()
            Workdays = New ObservableCollection(Of Date)()
            SetCurrentDate(FinancialDataGenerator.GetStartDate())
            DataSource = New ObservableCollection(Of TraderPoint) From {FinancialDataGenerator.CreatePoint(currentDate, FinancialDataGenerator.StartPrice)}
            For i As Integer = 0 To InitialUpdatesCount - 1
                UpdateData()
            Next i
            IsTimerEnabled = True
            timer.Interval = TimeSpan.FromMilliseconds(Interval)
            AddHandler timer.Tick, Sub(d, e) UpdateData()
        End Sub

        Private Sub UpdateData()
            SetCurrentDate(FinancialDataGenerator.GetNextDate(currentDate))

            Dim prevPoint = DataSource.Last()
            If currentDate.Subtract(prevPoint.Date) >= FinancialDataGenerator.GeneratePointPeriod Then
                DataSource.Add(FinancialDataGenerator.CreatePoint(currentDate, prevPoint.PriceClose))
                If DataSource.Count > MaxPointsCount Then
                    DataSource.RemoveAt(0)
                End If
            Else
                DataSource(DataSource.Count - 1) = FinancialDataGenerator.UpdatePoint(prevPoint)
            End If

            CurrentPoint = DataSource.Last()
        End Sub
        Private Sub SetCurrentDate(ByVal newDate As Date)
            If currentDate.Date <> newDate.Date Then
                Workdays.Add(newDate)
            End If
            currentDate = newDate
        End Sub

        #Region "POCO"
        Public Sub ToggleIsTimerEnabled()
            IsTimerEnabled = Not IsTimerEnabled
        End Sub
        Protected Sub OnIsTimerEnabledChanged()
            timer.IsEnabled = IsTimerEnabled
        End Sub
        #End Region
    End Class
    Public NotInheritable Class FinancialDataGenerator

        Private Sub New()
        End Sub
        #Region "Fields"

        Public Const StartPrice As Double = 500.0
        Private Const MinPrice As Double = 50.0
        Private Const WorkStart As Double = 9.0
        Private Const WorkPeriod As Double = 9.0

        Public Shared ReadOnly GeneratePointPeriod As TimeSpan = TimeSpan.FromMinutes(5)
        Private Shared ReadOnly random As New Random(1)
        Private Shared ReadOnly UpdatePointPeriod As TimeSpan = TimeSpan.FromSeconds(30)

        #End Region
        #Region "Methods"

        Public Shared Function GetStartDate() As Date
            Return GetNextWorkday(Date.Today)
        End Function
        Public Shared Function GetNextDate(ByVal [date] As Date) As Date
            If [date].Add(UpdatePointPeriod) > [date].Date.AddHours(WorkStart + WorkPeriod) Then
                Return GetNextWorkday([date])
            End If
            Return [date].Add(UpdatePointPeriod)
        End Function
        Private Shared Function GetNextWorkday(ByVal [date] As Date) As Date
            Dim addDays = If([date].DayOfWeek = DayOfWeek.Friday, 3, If([date].DayOfWeek = DayOfWeek.Saturday, 2, 1))
            Return [date].Date.AddDays(addDays).AddHours(WorkStart)
        End Function

        Public Shared Function CreatePoint(ByVal argument As Date, ByVal prevClose As Double) As TraderPoint
            Dim open As Double = prevClose
            Dim close As Double = CorrectPrice(open + GetPriceDelta())
            Dim low As Double = Math.Min(open, close)
            Dim high As Double = Math.Max(open, close)
            Return New TraderPoint(argument, low, high, open, close, random.NextDouble() * 100)
        End Function
        Public Shared Function UpdatePoint(ByVal point As TraderPoint) As TraderPoint
            Dim open As Double = point.PriceOpen
            Dim close As Double = CorrectPrice(point.PriceClose + GetPriceDelta())
            Dim low As Double = Math.Min(point.PriceLow, close)
            Dim high As Double = Math.Max(point.PriceHigh, close)
            Return New TraderPoint(point.Date, low, high, open, close, point.Value + random.NextDouble() * 100)
        End Function
        Private Shared Function CorrectPrice(ByVal price As Double) As Double
            If price <= MinPrice Then
                Return 2 * MinPrice - price
            End If
            Return price
        End Function
        Private Shared Function GetPriceDelta() As Double
            Return (random.NextDouble() - 0.5) * 40
        End Function

        #End Region
    End Class
    Public Structure TraderPoint

        Private ReadOnly date_Renamed As Date

        Private ReadOnly priceLow_Renamed As Double

        Private ReadOnly priceHigh_Renamed As Double

        Private ReadOnly priceOpen_Renamed As Double

        Private ReadOnly priceClose_Renamed As Double

        Private ReadOnly value_Renamed As Double

        Public ReadOnly Property [Date]() As Date
            Get
                Return date_Renamed
            End Get
        End Property
        Public ReadOnly Property PriceLow() As Double
            Get
                Return priceLow_Renamed
            End Get
        End Property
        Public ReadOnly Property PriceHigh() As Double
            Get
                Return priceHigh_Renamed
            End Get
        End Property
        Public ReadOnly Property PriceOpen() As Double
            Get
                Return priceOpen_Renamed
            End Get
        End Property
        Public ReadOnly Property PriceClose() As Double
            Get
                Return priceClose_Renamed
            End Get
        End Property
        Public ReadOnly Property Value() As Double
            Get
                Return value_Renamed
            End Get
        End Property

        Public Sub New(ByVal [date] As Date, ByVal priceLow As Double, ByVal priceHigh As Double, ByVal priceOpen As Double, ByVal priceClose As Double, ByVal value As Double)
            Me.date_Renamed = [date]
            Me.priceLow_Renamed = priceLow
            Me.priceHigh_Renamed = priceHigh
            Me.priceOpen_Renamed = priceOpen
            Me.priceClose_Renamed = priceClose
            Me.value_Renamed = value
        End Sub
    End Structure
End Namespace
