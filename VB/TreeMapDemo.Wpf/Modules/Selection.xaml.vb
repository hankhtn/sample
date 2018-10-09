Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Media
Imports System.Xml.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map
Imports DevExpress.Xpf.TreeMap
Imports System.Windows

Namespace TreeMapDemo
    Partial Public Class Selection
        Inherits TreeMapDemoModule

        Private viewModel As New DashboardViewModel()

        Public Sub New()
            InitializeComponent()
            DataContext = ViewModelSource.Create(Function() New DashboardViewModel())
        End Sub

        Private Sub ShapefileLoader_ShapesLoaded(ByVal sender As Object, ByVal e As ShapesLoadedEventArgs)
            CType(DataContext, DashboardViewModel).SetMapItems(e.Shapes)
        End Sub

        Private Sub OnMapSizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs)
            map.EnableZooming = True
            map.ZoomToFitLayerItems()
            map.EnableZooming = False
        End Sub
    End Class

    Friend Class CountriesInfoDataReader
        Private Shared Function LoadStatistic(ByVal exportOfGoodsDynamic As XElement) As List(Of GDPStatisticByYear)
            Dim statistic As New List(Of GDPStatisticByYear)()
            For Each exportOfGoodsDynamicItem As XElement In exportOfGoodsDynamic.Elements("GDPByYear")
                Dim year As Integer = Integer.Parse(exportOfGoodsDynamicItem.Element("Year").Value)
                Dim exportOfGoodsPercent As Double = Double.Parse(exportOfGoodsDynamicItem.Element("GDP").Value, CultureInfo.InvariantCulture)
                Dim popDynamicItem As New GDPStatisticByYear(year, exportOfGoodsPercent)
                statistic.Add(popDynamicItem)
            Next exportOfGoodsDynamicItem
            Return statistic
        End Function

        Public Shared Function Load() As List(Of CountryStatisticInfo)
            Dim data As New List(Of CountryStatisticInfo)()
            Try
                Dim Top10LargestCountries_xml As XDocument = DataLoader.LoadXDocumentFromResources("/Data/CountriesGDPByYears.xml")
                For Each countryInfo As XElement In Top10LargestCountries_xml.Root.Elements("CountryInfo")
                    Dim name As String = countryInfo.Element("Name").Value
                    Dim gdp As String = countryInfo.Element("GDP").Value
                    Dim continent As String = countryInfo.Element("Continent").Value
                    Dim statistic As List(Of GDPStatisticByYear) = LoadStatistic(countryInfo.Element("Statistic"))
                    Dim countryInfoInstance As New CountryStatisticInfo(name, continent, Convert.ToDouble(gdp), statistic)
                    data.Add(countryInfoInstance)
                Next countryInfo
            Catch
            End Try
            Return data
        End Function
    End Class

    <POCOViewModel>
    Public Class DashboardViewModel
        Inherits ViewModelBase

        Public Overridable Property CountriesData() As List(Of CountryStatisticInfo)
        Public Overridable Property SelectedCountry() As CountryStatisticInfo
        Public Overridable Property ChartTitle() As String
        Public Overridable Property MapFileUri() As Uri

        Protected Sub OnSelectedCountryChnged()

        End Sub

        Public Sub New()
            MapFileUri = DataLoader.GetResourceUri("/Data/CountriesGDP.shp")
            CountriesData = CountriesInfoDataReader.Load()
        End Sub

        Public Sub SetMapItems(ByVal layerMapItemCollection As IList(Of MapItem))
            For Each item As MapItem In layerMapItemCollection
                Dim shapeName As String = CStr(item.Attributes("NAME").Value)
                If shapeName = "Others" Then
                    item.Visible = False
                End If
                Dim countryInfo As CountryStatisticInfo = CountriesData.Find(Function(info) info.Name = shapeName)
                If countryInfo IsNot Nothing Then
                    countryInfo.Shape = item
                    item.Attributes.Add(New MapItemAttribute() With {.Name = "CountryInfo", .Type = GetType(CountryStatisticInfo), .Value = countryInfo})
                End If
            Next item
            SelectedCountry = CountriesData(25)
        End Sub
    End Class

    Public Class CountryStatisticInfo

        Private ReadOnly name_Renamed As String

        Private ReadOnly continent_Renamed As String
        Private ReadOnly statistic As List(Of GDPStatisticByYear)

        Private ReadOnly gdp_Renamed As Double

        Private shape_Renamed As WeakReference

        Public ReadOnly Property Name() As String
            Get
                Return name_Renamed
            End Get
        End Property
        Public ReadOnly Property Continent() As String
            Get
                Return continent_Renamed
            End Get
        End Property
        Public ReadOnly Property GDPDynamic() As List(Of GDPStatisticByYear)
            Get
                Return statistic
            End Get
        End Property
        Public ReadOnly Property GDP() As Double
            Get
                Return gdp_Renamed
            End Get
        End Property
        Public Property Shape() As MapItem
            Get
                Return If(shape_Renamed IsNot Nothing, DirectCast(shape_Renamed.Target, MapItem), Nothing)
            End Get
            Set(ByVal value As MapItem)
                If value Is Nothing Then
                    shape_Renamed = Nothing
                Else
                    If shape_Renamed Is Nothing OrElse shape_Renamed.Target IsNot value Then
                        shape_Renamed = New WeakReference(value)
                    End If
                End If
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal continent As String, ByVal gdp As Double, ByVal statistic As List(Of GDPStatisticByYear))
            Me.name_Renamed = name
            Me.continent_Renamed = continent
            Me.statistic = statistic
            Me.gdp_Renamed = gdp
        End Sub
    End Class

    Public Class GDPStatisticByYear

        Private year_Renamed As Integer

        Private gdp_Renamed As Double

        Public ReadOnly Property Year() As Integer
            Get
                Return year_Renamed
            End Get
        End Property
        Public ReadOnly Property GDP() As Double
            Get
                Return gdp_Renamed
            End Get
        End Property

        Public Sub New(ByVal year As Integer, ByVal gdp As Double)
            Me.year_Renamed = year
            Me.gdp_Renamed = gdp
        End Sub
    End Class

    Public Class SelectedMapItemConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Dim country As CountryStatisticInfo = DirectCast(value, CountryStatisticInfo)
            Return country.Shape
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim selectedShape As MapItem = DirectCast(value, MapItem)
            Return selectedShape.Attributes("CountryInfo").Value
        End Function
    End Class

    Public Class ChartPaletteToMapColorsConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim chartColors As PaletteBase = DirectCast(value, PaletteBase)
            Dim mapColors As New DevExpress.Xpf.Map.ColorCollection()
            Dim rangeStops As DoubleCollection = DirectCast(parameter, DoubleCollection)
            Dim colorsCount As Integer = CInt((rangeStops(rangeStops.Count - 1) - rangeStops(0))) + 1
            For i As Integer = 0 To colorsCount - 1
                mapColors.Add(chartColors(i))
            Next i
            Return mapColors
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Friend Class ColorsMultiConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim chartPalette As New DevExpress.Xpf.Charts.CustomPalette()
            Dim palette As PaletteBase = If(values(0) Is Nothing, Nothing, DirectCast(values(0), PaletteBase))
            Dim countryInfo As CountryStatisticInfo = If(values(0) Is Nothing, Nothing, DirectCast(values(1), CountryStatisticInfo))
            If countryInfo IsNot Nothing AndAlso countryInfo.Shape IsNot Nothing Then
                Dim index As Integer = Int16.Parse(countryInfo.Shape.Attributes("MAP_COLOR").Value.ToString())
                chartPalette.Colors.Add(palette(index))
            End If
            Return chartPalette
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
End Namespace
