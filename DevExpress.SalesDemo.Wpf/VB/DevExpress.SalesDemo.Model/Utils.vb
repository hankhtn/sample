Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text

Namespace DevExpress.SalesDemo.Model
    Public NotInheritable Class DateTimeUtils

        Private Sub New()
        End Sub

        Public Shared Function GetDayRange(ByVal [date] As Date) As DateTimeRange
            Dim startOfDate As New Date([date].Year, [date].Month, [date].Day)
            Dim endOfToday As Date = startOfDate.AddDays(1).AddTicks(-1)
            Return New DateTimeRange(startOfDate, endOfToday)
        End Function
        Public Shared Function GetTodayRange() As DateTimeRange
            Return GetDayRange(Date.Now)
        End Function
        Public Shared Function GetYesterdayRange() As DateTimeRange
            Return GetDayRange(Date.Now.AddDays(-1))
        End Function
        Public Shared Function GetLastWeekRange() As DateTimeRange
            Dim firstDay As DayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek
            Dim today As Date = Date.Today
            Dim startOfWeek As Date = today
            Do While startOfWeek.DayOfWeek <> firstDay
                startOfWeek = startOfWeek.AddDays(-1)
            Loop
            Dim endOfWeek As Date = startOfWeek.AddDays(7).AddTicks(-1)
            Return New DateTimeRange(startOfWeek, endOfWeek)
        End Function
        Public Shared Function GetMonthRange(ByVal [date] As Date) As DateTimeRange
            Dim startOfMonth As New Date([date].Year, [date].Month, 1)
            Dim daysInCurrentMonth As Integer = DateTime.DaysInMonth([date].Year, [date].Month)
            Dim endOfMonth As Date = startOfMonth.AddDays(daysInCurrentMonth).AddTicks(-1)
            Return New DateTimeRange(startOfMonth, endOfMonth)
        End Function
        Public Shared Function GetThidMonthRange() As DateTimeRange
            Return GetMonthRange(Date.Now)
        End Function
        Public Shared Function GetLastMonthRange() As DateTimeRange
            Return GetMonthRange(Date.Now.AddMonths(-1))
        End Function
        Public Shared Function GetYtdRange() As DateTimeRange
            Dim today As Date = Date.Today
            Dim startOfYear As New Date(today.Year, 1, 1)
            Dim endOfYear As Date = today ' startOfYear.AddYears(1).AddTicks(-1);
            Return New DateTimeRange(startOfYear, endOfYear)
        End Function
        Public Shared Function GetOneYearRange() As DateTimeRange
            Return New DateTimeRange(Date.Today.AddYears(-1), Date.Today)
        End Function

        Public Shared Function GetYearRange(ByVal [date] As Date) As DateTimeRange
            Dim startOfYear As New Date([date].Year, 1, 1)
            Dim endOfYear As Date = startOfYear.AddYears(1).AddTicks(-1)
            Return New DateTimeRange(startOfYear, endOfYear)
        End Function
        Public Shared Function GetLastYearRange() As DateTimeRange
            Return GetYearRange(Date.Today.AddYears(-1))
        End Function
        Public Shared Function GetLastYear() As Integer
            Return Date.Today.AddYears(-1).Year
        End Function
        Public Shared Function GetCurrentYear() As Object
            Return Date.Now.Year
        End Function

        Public Shared Function IsCurrentYear(ByVal [date] As Date) As Boolean
            Dim now As Date = Date.Now
            Return now.Year = [date].Year
        End Function
        Public Shared Function IsCurrentMonth(ByVal [date] As Date) As Boolean
            Dim now As Date = Date.Now
            Return IsCurrentYear([date]) AndAlso now.Month = [date].Month
        End Function
        Public Shared Function IsToday(ByVal [date] As Date) As Boolean
            Dim now As Date = Date.Now
            Return IsCurrentMonth([date]) AndAlso now.Day = [date].Day
        End Function
    End Class

    Public Structure DateTimeRange

        Private start_Renamed As Date

        Private end_Renamed As Date

        Public ReadOnly Property Start() As Date
            Get
                Return start_Renamed
            End Get
        End Property
        Public ReadOnly Property [End]() As Date
            Get
                Return end_Renamed
            End Get
        End Property

        Public Sub New(ByVal start As Date, ByVal [end] As Date)
            Me.start_Renamed = start
            Me.end_Renamed = [end]
        End Sub
    End Structure

    Public Structure DecimalRange

        Private start_Renamed As Decimal

        Private end_Renamed As Decimal

        Public ReadOnly Property Start() As Decimal
            Get
                Return start_Renamed
            End Get
        End Property
        Public ReadOnly Property [End]() As Decimal
            Get
                Return end_Renamed
            End Get
        End Property

        Public Sub New(ByVal start As Decimal, ByVal [end] As Decimal)
            Me.start_Renamed = start
            Me.end_Renamed = [end]
        End Sub
    End Structure

    Public NotInheritable Class SalesRangeProvider

        Private Sub New()
        End Sub

        Public Shared Function GetBadSalesRange() As DecimalRange
            Return New DecimalRange(0, 200000000D)
        End Function
        Public Shared Function GetNormalSalesRange() As DecimalRange
            Return New DecimalRange(0, 400000000D)
        End Function
        Public Shared Function GetGoodSalesRange() As DecimalRange
            Return New DecimalRange(0, 600000000D)
        End Function
    End Class
End Namespace
