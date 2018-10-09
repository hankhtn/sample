Imports DevExpress.Xpf.Charts
Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Linq
Imports DevExpress.SalesDemo.Model

Namespace ProductsDemo.Modules
    Public Class PivotGridViewModel
        Inherits ViewModelBase





        Private rangeStart_Renamed, rangeEnd_Renamed, selectedRangeStart_Renamed, selectedRangeEnd_Renamed As Date



        Private filteredSource_Renamed As IEnumerable(Of SalesGroup), dataSource_Renamed As IEnumerable(Of SalesGroup), filteredSourceByDate_Renamed As IEnumerable(Of SalesGroup)

        Private tilesData_Renamed As IEnumerable(Of TileData)
        Private dataProvider As IDataProvider = DevExpress.SalesDemo.Model.DataSource.GetDataProvider()

        Public Sub New()
            Dim range As DateTimeRange = DateTimeUtils.GetOneYearRange()
            RangeStart = range.Start
            SelectedRangeStart = RangeStart
            RangeEnd = range.End
            SelectedRangeEnd = RangeEnd
            DataSource = dataProvider.GetSales(RangeStart, RangeEnd, GroupingPeriod.Day)
            UpdateTiles()
            OnSelectedRangeChanged()
        End Sub

        Private Sub UpdateTiles()
            Dim tiles As New List(Of TileData)()
            Dim ytd As DateTimeRange = DateTimeUtils.GetYtdRange()
            Dim ytdPrev As New DateTimeRange(ytd.Start.AddYears(-1), ytd.End.AddYears(-1))
            Dim ytdSales As SalesGroup = dataProvider.GetTotalSalesByRange(ytd.Start, ytd.End)
            Dim ytdSalesPrev As SalesGroup = dataProvider.GetTotalSalesByRange(ytdPrev.Start, ytdPrev.End)
            Dim percents As Decimal = (ytdSales.TotalCost - ytdSalesPrev.TotalCost) / ytdSalesPrev.TotalCost
            tiles.Add(New TileData("Revenues", "YTD GROWTH", String.Format("{1}{0:P1}", Math.Abs(percents),If(percents < 0, "-", "+"))))
            tiles.Add(New TileData("Unit Sales", "YTD", ytdSales.Units.ToString("n0")))
            tiles.Add(New TileData("Direct Sales", "YTD", ytdSales.TotalCost.ToString("$#,##0,,M", CultureInfo.InvariantCulture)))
            Dim sector As SalesGroup = dataProvider.GetSalesBySector(ytd.Start, ytd.End, GroupingPeriod.All).OrderByDescending(Function(q) q.TotalCost).FirstOrDefault()
            If sector IsNot Nothing Then
                tiles.Add(New TileData("Best Sector YTD", sector.GroupName.ToUpper(), sector.TotalCost.ToString("$#,##0,,M", CultureInfo.InvariantCulture)))
            End If
            TilesData = tiles
        End Sub

        Public Property RangeStart() As Date
            Get
                Return rangeStart_Renamed
            End Get
            Private Set(ByVal value As Date)
                SetProperty(rangeStart_Renamed, value, Function() RangeStart)
            End Set
        End Property

        Public Property RangeEnd() As Date
            Get
                Return rangeEnd_Renamed
            End Get
            Private Set(ByVal value As Date)
                SetProperty(rangeEnd_Renamed, value, Function() RangeEnd)
            End Set
        End Property

        Public Property SelectedRangeStart() As Date
            Get
                Return selectedRangeStart_Renamed
            End Get
            Set(ByVal value As Date)
                SetProperty(selectedRangeStart_Renamed, value, Function() SelectedRangeStart, AddressOf OnSelectedRangeChanged)
            End Set
        End Property

        Public Property SelectedRangeEnd() As Date
            Get
                Return selectedRangeEnd_Renamed
            End Get
            Set(ByVal value As Date)
                SetProperty(selectedRangeEnd_Renamed, value, Function() SelectedRangeEnd, AddressOf OnSelectedRangeChanged)
            End Set
        End Property

        Public Property DataSource() As IEnumerable(Of SalesGroup)
            Get
                Return dataSource_Renamed
            End Get
            Private Set(ByVal value As IEnumerable(Of SalesGroup))
                SetProperty(dataSource_Renamed, value, "DataSource")
            End Set
        End Property

        Public Property FilteredSource() As IEnumerable(Of SalesGroup)
            Get
                Return filteredSource_Renamed
            End Get
            Private Set(ByVal value As IEnumerable(Of SalesGroup))
                SetProperty(filteredSource_Renamed, value, "FilteredSource")
            End Set
        End Property

        Public Property FilteredSourceByDate() As IEnumerable(Of SalesGroup)
            Get
                Return filteredSourceByDate_Renamed
            End Get
            Private Set(ByVal value As IEnumerable(Of SalesGroup))
                SetProperty(filteredSourceByDate_Renamed, value, "FilteredSourceByDate")
            End Set
        End Property

        Public Property TilesData() As IEnumerable(Of TileData)
            Get
                Return tilesData_Renamed
            End Get
            Private Set(ByVal value As IEnumerable(Of TileData))
                SetProperty(tilesData_Renamed, value, "TilesData")
            End Set
        End Property

        Private Sub OnSelectedRangeChanged()
            If SelectedRangeEnd < SelectedRangeStart Then
                Return
            End If
            FilteredSourceByDate = dataProvider.GetSalesByProduct(SelectedRangeStart, SelectedRangeEnd, GroupingPeriod.Day)
            FilteredSource = dataProvider.GetSalesByProduct(SelectedRangeStart, SelectedRangeEnd, GroupingPeriod.All)
        End Sub
    End Class

    Public Class TileData
        Inherits ViewModelBase



        Private svalue, subText_Renamed, mainText_Renamed As String

        Public Sub New(ByVal mainText As String, ByVal subText As String, ByVal value As String)
            Me.mainText_Renamed = mainText
            Me.subText_Renamed = subText
            Me.svalue = value
        End Sub

        Public Property MainText() As String
            Get
                Return mainText_Renamed
            End Get
            Private Set(ByVal value As String)
                SetProperty(mainText_Renamed, value, "MainText")
            End Set
        End Property


        Public Property SubText() As String
            Get
                Return subText_Renamed
            End Get
            Private Set(ByVal value As String)
                SetProperty(subText_Renamed, value, "SubText")
            End Set
        End Property

        Public Property Value() As String
            Get
                Return svalue
            End Get
            Private Set(ByVal value As String)
                SetProperty(svalue, value, "Value")
            End Set
        End Property
    End Class
End Namespace
