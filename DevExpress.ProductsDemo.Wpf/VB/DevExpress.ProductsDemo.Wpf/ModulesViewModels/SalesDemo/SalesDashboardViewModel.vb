Imports DevExpress.Xpf.Charts
Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Media.Imaging
Imports DevExpress.SalesDemo.Model

Namespace ProductsDemo
    Public Class SalesDashboardViewModel
        Inherits NavigationModule

        Private Const SalesPerformanceArgumentDataMember As String = "StartOfPeriod"
        Private Const SalesPerformanceValueDataMember As String = "TotalCost"
        Private Const FirstPartOfFiscalYearHeder As String = "YEAR "
        Private Const SalesVolumeForDayFormatString As String = "$#,#,0"
        Private Const SalesVolumeForWeekFormatString As String = "$#,#,0"
        Private Const SalesVolumeForMonthFormatString As String = "$#,#,0"
        Private Const SalesVolumeForYearFormatString As String = "$#,#,0"
        Private Const FirstPartOfSalesForecastDashboardGroupHeader As String = "SALES FORECAST "


        Private selectedDay_Renamed As Date = Date.Now

        Private selectedMonth_Renamed As Date = Date.Now

        Private salesBySector_Renamed As IEnumerable(Of SalesGroup)

        Private ytdSalesVolume_Renamed As Decimal

        Private ytdSalesForecast_Renamed As Decimal

        Private lastYearSalesVolume_Renamed As Decimal

        Private annualSalesFirstRangeEnd_Renamed As Decimal

        Private annualSalesSecondRangeEnd_Renamed As Decimal

        Private annualSalesThirdRangeEnd_Renamed As Decimal
        Private lastYearHeder As String

        Private salesForecastDashboardGroupHeader_Renamed As String

        Private dailySalesPerformanceViewModel_Renamed As SalesPerformanceViewModel

        Private monthlySalesPerformanceViewModel_Renamed As SalesPerformanceViewModel

        Public Property SelectedDay() As Date
            Get
                Return selectedDay_Renamed
            End Get
            Private Set(ByVal value As Date)
                SetProperty(selectedDay_Renamed, value, "SelectedDay", AddressOf OnSelectedDayChanged)
            End Set
        End Property
        Public Property SelectedMonth() As Date
            Get
                Return selectedMonth_Renamed
            End Get
            Private Set(ByVal value As Date)
                SetProperty(selectedMonth_Renamed, value, "SelectedMonth", AddressOf OnSelectedMonthChanged)
            End Set
        End Property
        Public Property YTDSalesVolume() As Decimal
            Get
                Return ytdSalesVolume_Renamed
            End Get

            Set(ByVal value As Decimal)
                SetProperty(ytdSalesVolume_Renamed, value, "YTDSalesVolume")
            End Set
        End Property
        Public Property YTDSalesForecast() As Decimal
            Get
                Return ytdSalesForecast_Renamed
            End Get
            Private Set(ByVal value As Decimal)
                SetProperty(ytdSalesForecast_Renamed, value, "YTDSalesForecast")
            End Set
        End Property
        Public Property LastYearSalesVolume() As Decimal
            Get
                Return lastYearSalesVolume_Renamed
            End Get
            Private Set(ByVal value As Decimal)
                SetProperty(lastYearSalesVolume_Renamed, value, "LastYearSalesVolume")
            End Set
        End Property
        Public Property AnnualSalesFirstRangeEnd() As Decimal
            Get
                Return annualSalesFirstRangeEnd_Renamed
            End Get
            Private Set(ByVal value As Decimal)
                SetProperty(annualSalesFirstRangeEnd_Renamed, value, "AnnualSalesFirstRangeEnd")
            End Set
        End Property
        Public Property AnnualSalesSecondRangeEnd() As Decimal
            Get
                Return annualSalesSecondRangeEnd_Renamed
            End Get
            Private Set(ByVal value As Decimal)
                SetProperty(annualSalesSecondRangeEnd_Renamed, value, "AnnualSalesSecondRangeEnd")
            End Set
        End Property
        Public Property AnnualSalesThirdRangeEnd() As Decimal
            Get
                Return annualSalesThirdRangeEnd_Renamed
            End Get
            Private Set(ByVal value As Decimal)
                SetProperty(annualSalesThirdRangeEnd_Renamed, value, "AnnualSalesThirdRangeEnd")
            End Set
        End Property
        Public Property LastYearHeader() As String
            Get
                Return lastYearHeder
            End Get
            Private Set(ByVal value As String)
                SetProperty(lastYearHeder, value, "LastYearHeader")
            End Set
        End Property
        Public Property SalesForecastDashboardGroupHeader() As String
            Get
                Return salesForecastDashboardGroupHeader_Renamed
            End Get
            Private Set(ByVal value As String)
                SetProperty(salesForecastDashboardGroupHeader_Renamed, value, "SalesForecastDashboardGroupHeader")
            End Set
        End Property
        Public Property SalesBySector() As IEnumerable(Of SalesGroup)
            Get
                Return salesBySector_Renamed
            End Get
            Private Set(ByVal value As IEnumerable(Of SalesGroup))
                SetProperty(salesBySector_Renamed, value, "SalesBySector")
            End Set
        End Property
        Public Property DailySalesPerformanceViewModel() As SalesPerformanceViewModel
            Get
                Return dailySalesPerformanceViewModel_Renamed
            End Get
            Private Set(ByVal value As SalesPerformanceViewModel)
                SetProperty(dailySalesPerformanceViewModel_Renamed, value, "DailySalesPerformanceViewModel")
            End Set
        End Property
        Public Property MonthlySalesPerformanceViewModel() As SalesPerformanceViewModel
            Get
                Return monthlySalesPerformanceViewModel_Renamed
            End Get
            Private Set(ByVal value As SalesPerformanceViewModel)
                SetProperty(monthlySalesPerformanceViewModel_Renamed, value, "MonthlySalesPerformanceViewModel")
            End Set
        End Property
        Public Overrides ReadOnly Property Caption() As String
            Get
                Return "Sales"
            End Get
        End Property
        Public Overrides ReadOnly Property Description() As String
            Get
                Return "Revenue" & Environment.NewLine & "Snapshots"
            End Get
        End Property
        Public Overrides ReadOnly Property Glyph() As BitmapImage
            Get
                Return ResourceImageHelper.GetResourceImage("Sales.png")
            End Get
        End Property

        Protected Overrides Sub SaveAndClearData()
            SaveAndClearPropertyValue(lastYearSalesVolume_Renamed, "LastYearSalesVolume", 0.0D)
            SaveAndClearPropertyValue(ytdSalesVolume_Renamed, "YTDSalesVolume", 0.0D)
            SaveAndClearPropertyValue(ytdSalesForecast_Renamed, "YTDSalesForecast", 0.0D)
            SaveAndClearPropertyValue(salesBySector_Renamed, "SalesBySector")
            SavePropertyValue(Of Object)(DailySalesPerformanceViewModel.ChartDataSource, "DailySalesPerformanceViewModel.ChartDataSource")
            DailySalesPerformanceViewModel.ChartDataSource = Nothing
            SavePropertyValue(Of Object)(MonthlySalesPerformanceViewModel.ChartDataSource, "MonthlySalesPerformanceViewModel.ChartDataSource")
            MonthlySalesPerformanceViewModel.ChartDataSource = Nothing
        End Sub
        Protected Overrides Sub RestoreData()
            RestorePropertyValue(lastYearSalesVolume_Renamed, "LastYearSalesVolume", True)
            RestorePropertyValue(ytdSalesVolume_Renamed, "YTDSalesVolume", True)
            RestorePropertyValue(ytdSalesForecast_Renamed, "YTDSalesForecast", True)
            RestorePropertyValue(salesBySector_Renamed, "SalesBySector", True)
            Dim dailySalesPerformanceViewModelChartDataSource As Object = Nothing
            RestorePropertyValue(Of Object)(dailySalesPerformanceViewModelChartDataSource, "DailySalesPerformanceViewModel.ChartDataSource", False)
            DailySalesPerformanceViewModel.ChartDataSource = dailySalesPerformanceViewModelChartDataSource
            Dim monthlySalesPerformanceViewModelChartDataSource As Object = Nothing
            RestorePropertyValue(Of Object)(monthlySalesPerformanceViewModelChartDataSource, "MonthlySalesPerformanceViewModel.ChartDataSource", False)
            MonthlySalesPerformanceViewModel.ChartDataSource = monthlySalesPerformanceViewModelChartDataSource
        End Sub
        Protected Overrides Sub InitializeData()
            Dim currentYearRange As DateTimeRange = DateTimeUtils.GetYtdRange()
            YTDSalesVolume = DataProvider.GetTotalSalesByRange(currentYearRange.Start, currentYearRange.End).TotalCost
            YTDSalesForecast = SalesForecastMaker.GetYtdForecast(YTDSalesVolume)
            Dim lastYearRange As DateTimeRange = DateTimeUtils.GetLastYearRange()
            LastYearSalesVolume = DataProvider.GetTotalSalesByRange(lastYearRange.Start, lastYearRange.End).TotalCost
            LastYearHeader = FirstPartOfFiscalYearHeder & DateTimeUtils.GetLastYear()
            SalesForecastDashboardGroupHeader = FirstPartOfSalesForecastDashboardGroupHeader & "(" & DateTimeUtils.GetCurrentYear().ToString() & ")"
            SalesBySector = DataProvider.GetSalesBySector(New Date(), Date.Now, GroupingPeriod.All)
            Dim badSalesRange As DecimalRange = SalesRangeProvider.GetBadSalesRange()
            Dim normalSalesRange As DecimalRange = SalesRangeProvider.GetNormalSalesRange()
            Dim goodSalesRange As DecimalRange = SalesRangeProvider.GetGoodSalesRange()
            AnnualSalesFirstRangeEnd = badSalesRange.End
            AnnualSalesSecondRangeEnd = normalSalesRange.End
            AnnualSalesThirdRangeEnd = goodSalesRange.End
            InitializeDailySalesPerformanceViewModel()
            InitializeMonthlySalesPerformanceViewModel()
        End Sub
        Private Sub InitializeDailySalesPerformanceViewModel()
            DailySalesPerformanceViewModel = New SalesPerformanceViewModel() With {.Mode = SalesPerformanceViewMode.Daily, .AreaSeriesVisible = True, .ArgumentDataMember = SalesPerformanceArgumentDataMember, .ValueDataMember = SalesPerformanceValueDataMember, .DateTimeMeasureUnit = DateTimeMeasureUnit.Hour, .DateTimeGridAlignment = DateTimeGridAlignment.Hour, .AxisXGridSpacing = 2, .AxisXMinorCount = 1, .AxisXLabelFormatString = "t", .AreaSeriesCrosshairLabelPattern = "{A:t}:  ${V:###,###,###}"}
            Dim todayRange As DateTimeRange = DateTimeUtils.GetDayRange(Date.Now)
            Dim todaySalesGroup As SalesGroup = DataProvider.GetTotalSalesByRange(todayRange.Start, todayRange.End)
            DailySalesPerformanceViewModel.FirstSalesVolume = todaySalesGroup.TotalCost.ToString(SalesVolumeForDayFormatString)
            Dim yesterdayRange As DateTimeRange = DateTimeUtils.GetDayRange(Date.Now.AddDays(-1))
            Dim yesterdaySalesGroup As SalesGroup = DataProvider.GetTotalSalesByRange(yesterdayRange.Start, yesterdayRange.End)
            DailySalesPerformanceViewModel.SecondSalesVolume = yesterdaySalesGroup.TotalCost.ToString(SalesVolumeForDayFormatString)
            Dim lastWeekRange As DateTimeRange = DateTimeUtils.GetLastWeekRange()
            Dim lastWeekSalesGroup As SalesGroup = DataProvider.GetTotalSalesByRange(lastWeekRange.Start, lastWeekRange.End)
            DailySalesPerformanceViewModel.ThirdSalesVolume = lastWeekSalesGroup.TotalCost.ToString(SalesVolumeForWeekFormatString)
            AddHandler DailySalesPerformanceViewModel.PropertyChanged, AddressOf OnDailySalesPerformanceViewModelPropertyChanged
            RequestDataForDaylySalesPerformanceChart()
        End Sub
        Private Sub InitializeMonthlySalesPerformanceViewModel()
            MonthlySalesPerformanceViewModel = New SalesPerformanceViewModel() With {.Mode = SalesPerformanceViewMode.Monthly, .AreaSeriesVisible = True, .ArgumentDataMember = SalesPerformanceArgumentDataMember, .ValueDataMember = SalesPerformanceValueDataMember, .DateTimeMeasureUnit = DateTimeMeasureUnit.Day, .DateTimeGridAlignment = DateTimeGridAlignment.Day, .AxisXGridSpacing = 3, .AxisXMinorCount = 2, .AxisXLabelFormatString = " d", .AreaSeriesCrosshairLabelPattern = "{A:d}:  ${V:###,###,###}"}
            Dim thisMonthRange As DateTimeRange = DateTimeUtils.GetMonthRange(Date.Now)
            Dim thisMonthSalesGroup As SalesGroup = DataProvider.GetTotalSalesByRange(thisMonthRange.Start, thisMonthRange.End)
            MonthlySalesPerformanceViewModel.FirstSalesVolume = thisMonthSalesGroup.TotalCost.ToString(SalesVolumeForMonthFormatString)
            Dim lastMonthRange As DateTimeRange = DateTimeUtils.GetMonthRange(Date.Now.AddMonths(-1))
            Dim lastMonthSalesGroup As SalesGroup = DataProvider.GetTotalSalesByRange(lastMonthRange.Start, lastMonthRange.End)
            MonthlySalesPerformanceViewModel.SecondSalesVolume = lastMonthSalesGroup.TotalCost.ToString(SalesVolumeForMonthFormatString)
            MonthlySalesPerformanceViewModel.ThirdSalesVolume = YTDSalesVolume.ToString(SalesVolumeForYearFormatString)
            AddHandler MonthlySalesPerformanceViewModel.PropertyChanged, AddressOf OnMonthlySalesPerformanceViewModelPropertyChanged
            RequestDataForMonthlySalesPerformanceChart()
        End Sub
        Private Sub RequestDataForDaylySalesPerformanceChart()
            Dim start As New Date(SelectedDay.Year, SelectedDay.Month, SelectedDay.Day)
            Dim [end] As Date = start.AddDays(1)
            DailySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(start, [end], GroupingPeriod.Hour)
        End Sub
        Private Sub RequestDataForMonthlySalesPerformanceChart()
            Dim start As New Date(SelectedMonth.Year, SelectedMonth.Month, 1)
            Dim daysInMonth As Integer = DateTime.DaysInMonth(SelectedMonth.Year, SelectedMonth.Month)
            Dim [end] As New Date(SelectedMonth.Year, SelectedMonth.Month, daysInMonth)
            MonthlySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(start, [end], GroupingPeriod.Day)
        End Sub
        Private Sub OnDailySalesPerformanceViewModelPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            If e.PropertyName = "SelectedDate" Then
                SelectedDay = DailySalesPerformanceViewModel.SelectedDate
            End If
        End Sub
        Private Sub OnMonthlySalesPerformanceViewModelPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            If e.PropertyName = "SelectedDate" Then
                SelectedMonth = MonthlySalesPerformanceViewModel.SelectedDate
            End If
        End Sub
        Private Sub OnSelectedDayChanged()
            Dim day As DateTimeRange = DateTimeUtils.GetDayRange(SelectedDay)
            DailySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(day.Start, day.End, GroupingPeriod.Hour)
        End Sub
        Private Sub OnSelectedMonthChanged()
            Dim month As DateTimeRange = DateTimeUtils.GetMonthRange(SelectedMonth)
            MonthlySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(month.Start, month.End, GroupingPeriod.Day)
        End Sub
    End Class
End Namespace
