Imports DevExpress.Xpf.Charts
Imports DevExpress.Mvvm
Imports System
Imports System.Windows.Input
Imports DevExpress.SalesDemo.Model

Namespace ProductsDemo
    Public Enum SalesPerformanceViewMode
        Daily
        Monthly
    End Enum

    Public Class SalesPerformanceViewModel
        Inherits ViewModelBase


        Private mode_Renamed As SalesPerformanceViewMode

        Private selectedDate_Renamed As Date = Date.Now

        Private chartDataSource_Renamed As Object

        Private selectedDateString_Renamed As String

        Private argumentDataMember_Renamed As String

        Private valueDataMember_Renamed As String

        Private secondButtonText_Renamed As String

        Private thirdButtonText_Renamed As String

        Private firstSalesVolumeHeader_Renamed As String

        Private secondSalesVolumeHeader_Renamed As String

        Private thirdSalesVolumeHeader_Renamed As String

        Private firstSalesVolume_Renamed As String

        Private secondSalesVolume_Renamed As String

        Private thirdSalesVolume_Renamed As String

        Private axisXLabelFormatString_Renamed As String

        Private areaSeriesCrosshairLabelPattern_Renamed As String

        Private areaSeriesVisible_Renamed As Boolean

        Private barSeriesVisible_Renamed As Boolean

        Private chartSideMarginsEnabled_Renamed As Boolean

        Private dateTimeMeasureUnit_Renamed As DateTimeMeasureUnit

        Private dateTimeGridAlignment_Renamed As DateTimeGridAlignment
        Private gridSpacing As Double

        Private axisXMinorCount_Renamed As Integer

        Private timeForwardCommand_Renamed As ICommand

        Private timeBackwardCommand_Renamed As ICommand

        Private setCurrentTimePeriodCommand_Renamed As ICommand

        Private setLastTimePeriodCommand_Renamed As ICommand

        Public Property Mode() As SalesPerformanceViewMode
            Get
                Return mode_Renamed
            End Get
            Set(ByVal value As SalesPerformanceViewMode)
                SetProperty(mode_Renamed, value, "Mode", AddressOf OnModePropertyChanged)
            End Set
        End Property
        Public Property SelectedDate() As Date
            Get
                Return selectedDate_Renamed
            End Get
            Set(ByVal value As Date)
                SetProperty(selectedDate_Renamed, value, "SelectedDate", AddressOf OnSelectedDatePropertyChanged)
            End Set
        End Property
        Public Property SelectedDateString() As String
            Get
                Return selectedDateString_Renamed
            End Get
            Private Set(ByVal value As String)
                SetProperty(selectedDateString_Renamed, value, "SelectedDateString")
            End Set
        End Property
        Public Property ChartDataSource() As Object
            Get
                Return chartDataSource_Renamed
            End Get
            Set(ByVal value As Object)
                SetProperty(chartDataSource_Renamed, value, "ChartDataSource")
            End Set
        End Property
        Public Property ArgumentDataMember() As String
            Get
                Return argumentDataMember_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(argumentDataMember_Renamed, value, "ArgumentDataMember")
            End Set
        End Property
        Public Property ValueDataMember() As String
            Get
                Return valueDataMember_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(valueDataMember_Renamed, value, "ValueDataMember")
            End Set
        End Property
        Public Property SecondButtonText() As String
            Get
                Return secondButtonText_Renamed
            End Get
            Private Set(ByVal value As String)
                SetProperty(secondButtonText_Renamed, value, "SecondButtonText")
            End Set
        End Property
        Public Property ThirdButtonText() As String
            Get
                Return thirdButtonText_Renamed
            End Get
            Private Set(ByVal value As String)
                SetProperty(thirdButtonText_Renamed, value, "ThirdButtonText")
            End Set
        End Property
        Public Property FirstSalesVolumeHeader() As String
            Get
                Return firstSalesVolumeHeader_Renamed
            End Get
            Private Set(ByVal value As String)
                SetProperty(firstSalesVolumeHeader_Renamed, value, "FirstSalesVolumeHeader")
            End Set
        End Property
        Public Property SecondSalesVolumeHeader() As String
            Get
                Return secondSalesVolumeHeader_Renamed
            End Get
            Private Set(ByVal value As String)
                SetProperty(secondSalesVolumeHeader_Renamed, value, "SecondSalesVolumeHeader")
            End Set
        End Property
        Public Property ThirdSalesVolumeHeader() As String
            Get
                Return thirdSalesVolumeHeader_Renamed
            End Get
            Private Set(ByVal value As String)
                SetProperty(thirdSalesVolumeHeader_Renamed, value, "ThirdSalesVolumeHeader")
            End Set
        End Property
        Public Property FirstSalesVolume() As String
            Get
                Return firstSalesVolume_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(firstSalesVolume_Renamed, value, "FirstSalesVolume")
            End Set
        End Property
        Public Property SecondSalesVolume() As String
            Get
                Return secondSalesVolume_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(secondSalesVolume_Renamed, value, "SecondSalesVolume")
            End Set
        End Property
        Public Property ThirdSalesVolume() As String
            Get
                Return thirdSalesVolume_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(thirdSalesVolume_Renamed, value, "ThirdSalesVolume")
            End Set
        End Property
        Public Property AxisXLabelFormatString() As String
            Get
                Return axisXLabelFormatString_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(axisXLabelFormatString_Renamed, value, "AxisXLabelFormatString")
            End Set
        End Property
        Public Property AreaSeriesCrosshairLabelPattern() As String
            Get
                Return areaSeriesCrosshairLabelPattern_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(areaSeriesCrosshairLabelPattern_Renamed, value, "AreaSeriesCrosshairLabelPattern")
            End Set
        End Property
        Public Property AreaSeriesVisible() As Boolean
            Get
                Return areaSeriesVisible_Renamed
            End Get
            Set(ByVal value As Boolean)
                SetProperty(areaSeriesVisible_Renamed, value, "AreaSeriesVisible", AddressOf OnSeriesVisibilityChanged)
            End Set
        End Property
        Public Property BarSeriesVisible() As Boolean
            Get
                Return barSeriesVisible_Renamed
            End Get
            Set(ByVal value As Boolean)
                SetProperty(barSeriesVisible_Renamed, value, "BarSeriesVisible", AddressOf OnSeriesVisibilityChanged)
            End Set
        End Property
        Public Property ChartSideMarginsEnabled() As Boolean
            Get
                Return chartSideMarginsEnabled_Renamed
            End Get
            Set(ByVal value As Boolean)
                SetProperty(chartSideMarginsEnabled_Renamed, value, "ChartSideMarginsEnabled")
            End Set
        End Property
        Public Property DateTimeMeasureUnit() As DateTimeMeasureUnit
            Get
                Return dateTimeMeasureUnit_Renamed
            End Get
            Set(ByVal value As DateTimeMeasureUnit)
                SetProperty(dateTimeMeasureUnit_Renamed, value, "DateTimeMeasureUnit")
            End Set
        End Property
        Public Property DateTimeGridAlignment() As DateTimeGridAlignment
            Get
                Return dateTimeGridAlignment_Renamed
            End Get
            Set(ByVal value As DateTimeGridAlignment)
                SetProperty(dateTimeGridAlignment_Renamed, value, "DateTimeGridAlignment")
            End Set
        End Property
        Public Property AxisXGridSpacing() As Double
            Get
                Return gridSpacing
            End Get
            Set(ByVal value As Double)
                SetProperty(gridSpacing, value, "AxisXGridSpacing")
            End Set
        End Property
        Public Property AxisXMinorCount() As Integer
            Get
                Return axisXMinorCount_Renamed
            End Get
            Set(ByVal value As Integer)
                SetProperty(axisXMinorCount_Renamed, value, "AxisXMinorCount")
            End Set
        End Property
        Public Property TimeForwardCommand() As ICommand
            Get
                Return timeForwardCommand_Renamed
            End Get
            Private Set(ByVal value As ICommand)
                SetProperty(timeForwardCommand_Renamed, value, "TimeForwardCommand")
            End Set
        End Property
        Public Property TimeBackwardCommand() As ICommand
            Get
                Return timeBackwardCommand_Renamed
            End Get
            Private Set(ByVal value As ICommand)
                SetProperty(timeBackwardCommand_Renamed, value, "TimeBackwardCommand")
            End Set
        End Property
        Public Property SetCurrentTimePeriodCommand() As ICommand
            Get
                Return setCurrentTimePeriodCommand_Renamed
            End Get
            Set(ByVal value As ICommand)
                SetProperty(setCurrentTimePeriodCommand_Renamed, value, "SetCurrentTimePeriodCommand")
            End Set
        End Property
        Public Property SetLastTimePeriodCommand() As ICommand
            Get
                Return setLastTimePeriodCommand_Renamed
            End Get
            Set(ByVal value As ICommand)
                SetProperty(setLastTimePeriodCommand_Renamed, value, "SetLastTimePeriodCommand")
            End Set
        End Property

        Public Sub New()
            OnModePropertyChanged()
            TimeForwardCommand = New DelegateCommand(AddressOf OnTimeForwardCommandExecuted, AddressOf CanExecuteTimeForward)
            TimeBackwardCommand = New DelegateCommand(AddressOf OnTimeBackwardCommandExecuted)
            SetCurrentTimePeriodCommand = New DelegateCommand(AddressOf OnSetCurrentTimePeriodCommandExecuted)
            SetLastTimePeriodCommand = New DelegateCommand(AddressOf OnSetLastTimePeriodCommandExecuted)
        End Sub

        Private Sub OnModePropertyChanged()
            SecondButtonText = If(Mode = SalesPerformanceViewMode.Daily, "Yesterday", "Last Month")
            ThirdButtonText = If(Mode = SalesPerformanceViewMode.Daily, "Today", "This Month")
            FirstSalesVolumeHeader = If(Mode = SalesPerformanceViewMode.Daily, "TODAY", "THIS MONTH")
            SecondSalesVolumeHeader = If(Mode = SalesPerformanceViewMode.Daily, "YESTERDAY", "LAST MONTH")
            ThirdSalesVolumeHeader = If(Mode = SalesPerformanceViewMode.Daily, "LAST WEEK", "YTD")
            OnSelectedDatePropertyChanged()
        End Sub
        Private Sub OnSelectedDatePropertyChanged()
            Dim fomatString As String = If(Mode = SalesPerformanceViewMode.Daily, "d", "MMMM, yyyy")
            SelectedDateString = SelectedDate.ToString(fomatString)
        End Sub
        Private Sub OnSeriesVisibilityChanged()
            If AreaSeriesVisible AndAlso (Not BarSeriesVisible) Then
                ChartSideMarginsEnabled = False
            Else
                ChartSideMarginsEnabled = True
            End If
        End Sub
        Private Function IsCurrentMonth(ByVal [date] As Date) As Boolean
            Dim now As Date = Date.Now
            Return now.Year = [date].Year AndAlso now.Month = [date].Month
        End Function
        Private Function IsToday(ByVal [date] As Date) As Boolean
            Dim now As Date = Date.Now
            Return now.Year = [date].Year AndAlso now.Month = [date].Month AndAlso now.Day = [date].Day
        End Function
        Private Function CanExecuteTimeForward() As Boolean
            If Mode = SalesPerformanceViewMode.Daily Then
                Return Not IsToday(SelectedDate)
            Else
                Return Not IsCurrentMonth(SelectedDate)
            End If
        End Function
        Private Sub OnTimeForwardCommandExecuted()
            If Mode = SalesPerformanceViewMode.Daily Then
                SelectedDate = SelectedDate.AddDays(1)
            Else
                SelectedDate = SelectedDate.AddMonths(1)
            End If
        End Sub
        Private Sub OnTimeBackwardCommandExecuted()
            If Mode = SalesPerformanceViewMode.Daily Then
                SelectedDate = SelectedDate.AddDays(-1)
            Else
                SelectedDate = SelectedDate.AddMonths(-1)
            End If
        End Sub
        Private Sub OnSetCurrentTimePeriodCommandExecuted()
            SelectedDate = Date.Now
        End Sub
        Private Sub OnSetLastTimePeriodCommandExecuted()
            If Mode = SalesPerformanceViewMode.Daily Then
                SelectedDate = Date.Now.AddDays(-1)
            Else
                SelectedDate = Date.Now.AddMonths(-1)
            End If
        End Sub

        Protected Overrides Sub OnInitializeInDesignMode()
            MyBase.OnInitializeInDesignMode()
            OnModePropertyChanged()
            SelectedDate = Date.Now
            Dim dataProvider As IDataProvider = New SampleDataProvider()
            FirstSalesVolume = "1.1M"
            SecondSalesVolume = "2.2M"
            ThirdSalesVolume = "12.3M"
            ValueDataMember = "TotalCost"
            ArgumentDataMember = "StartOfPeriod"
            ChartDataSource = dataProvider.GetSales(Date.Today.AddMonths(-1), Date.Today, GroupingPeriod.Day)
            AreaSeriesVisible = True
        End Sub
    End Class
End Namespace
