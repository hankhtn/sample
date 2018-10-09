Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Reflection
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Threading
Imports System.Xml.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Map

Namespace MapDemo
    Public Enum TemperatureScale
        Fahrenheit
        Celsius
    End Enum

    Public Class DemoValuesProvider
        Private Const key As String = DevExpress.Map.Native.DXBingKeyVerifier.BingKeyWpfMapDemo

        Public ReadOnly Property DevexpressBingKey() As String
            Get
                Return key
            End Get
        End Property
        Public ReadOnly Property BingMapKinds() As IEnumerable(Of BingMapKind)
            Get
                Return New BingMapKind() { BingMapKind.Area, BingMapKind.Road, BingMapKind.Hybrid }
            End Get
        End Property
        Public ReadOnly Property OSMBaseLayers() As IEnumerable(Of OpenStreetMapKind)
            Get
                Return New OpenStreetMapKind() { OpenStreetMapKind.Basic, OpenStreetMapKind.CycleMap, OpenStreetMapKind.Hot, OpenStreetMapKind.GrayScale, OpenStreetMapKind.Transport }
            End Get
        End Property
        Public ReadOnly Property OSMOverlayLayers() As IEnumerable(Of Object)
            Get
                Return New Object() { "None", OpenStreetMapKind.SeaMarks, OpenStreetMapKind.HikingRoutes, OpenStreetMapKind.CyclingRoutes, OpenStreetMapKind.PublicTransport }
            End Get
        End Property
        Public ReadOnly Property ShapeMapTypes() As IEnumerable(Of String)
            Get
                Return New String() { "GDP", "Population", "Political" }
            End Get
        End Property
        Public ReadOnly Property ShapefileMapTypes() As IEnumerable(Of String)
            Get
                Return New String() { "World", "Africa", "South America", "North America", "Australia", "Eurasia" }
            End Get
        End Property

        Public ReadOnly Property TemperatureUnit() As IEnumerable(Of TemperatureScale)
            Get
                Return New TemperatureScale() { TemperatureScale.Celsius, TemperatureScale.Fahrenheit }
            End Get
        End Property
        Public ReadOnly Property BubbleMarkerTypes() As IEnumerable(Of MarkerType)
            Get
                Return New MarkerType() { MarkerType.Circle, MarkerType.Cross, MarkerType.Diamond, MarkerType.Hexagon, MarkerType.InvertedTriangle, MarkerType.Triangle, MarkerType.Pentagon, MarkerType.Plus, MarkerType.Square, MarkerType.Star5, MarkerType.Star6, MarkerType.Star8 }
            End Get
        End Property
        Public ReadOnly Property ProjectionTypes() As IEnumerable(Of ProjectionBase)
            Get
                Return New ProjectionBase() {
                    New SphericalMercatorProjection(),
                    New EqualAreaProjection(),
                    New EquirectangularProjection(),
                    New EllipticalMercatorProjection(),
                    New MillerProjection(),
                    New EquidistantProjection(),
                    New LambertCylindricalEqualAreaProjection(),
                    New BraunStereographicProjection(),
                    New KavrayskiyProjection(),
                    New SinusoidalProjection(),
                    New EPSG4326Projection()
                }
            End Get
        End Property
    End Class

    Public NotInheritable Class DemoUtils

        Private Sub New()
        End Sub

        Public Shared Iterator Function FindMap(ByVal obj As DependencyObject) As IEnumerable(Of MapControl)
            If obj IsNot Nothing Then
                For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(obj) - 1
                    Dim child As DependencyObject = VisualTreeHelper.GetChild(obj, i)
                    If child IsNot Nothing AndAlso TypeOf child Is MapControl Then
                        Yield CType(child, MapControl)
                    End If
                    For Each childOfChild As MapControl In FindMap(child)
                        Yield childOfChild
                    Next childOfChild
                Next i
            End If
        End Function
    End Class

    Public NotInheritable Class DataLoader

        Private Sub New()
        End Sub

        Private Shared Function GetStream(ByVal fileName As String) As Stream
            fileName = "/MapDemo;component" & fileName
            Dim uri As New Uri(fileName, UriKind.RelativeOrAbsolute)
            Return Application.GetResourceStream(uri).Stream
        End Function

        Public Shared Function LoadXmlFromResources(ByVal fileName As String) As XDocument
            Try
                Return XDocument.Load(GetStream(fileName))
            Catch
                Return Nothing
            End Try
        End Function
        Public Shared Function LoadStreamFromResources(ByVal fileName As String) As Stream
            Try
                Return GetStream(fileName)
            Catch
                Return Nothing
            End Try
        End Function
    End Class

    Public Class DoubleToTimeSpanConvert
        Implements IValueConverter

        #Region "IValueConvector implementation"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim doubleValue As Double = 3600 * DirectCast(value, Double)
            Return New TimeSpan(0, 0, CInt((Math.Ceiling(doubleValue))))
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class
    Public Class SelectedLayerToVisibilityConverter
        Implements IValueConverter

        #Region "IValueConverter implementation"
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(TypeOf value Is String, Visibility.Collapsed, Visibility.Visible)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class
    Public Class SelectedLayerToKindConverter
        Implements IValueConverter

        #Region "IValueConverter implementation"
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(TypeOf value Is String, OpenStreetMapKind.SeaMarks, value)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class
    Public Class PlaneInfoToPathVisibilityConverter
        Implements IValueConverter

        #Region "IValueConverter implementation"
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return value Is parameter
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class RangeColor

        Private ReadOnly rangeMin_Renamed As Integer

        Private ReadOnly rangeMax_Renamed As Integer

        Private ReadOnly fill_Renamed As Color

        Public ReadOnly Property RangeMin() As Integer
            Get
                Return rangeMin_Renamed
            End Get
        End Property
        Public ReadOnly Property RangeMax() As Integer
            Get
                Return rangeMax_Renamed
            End Get
        End Property
        Public ReadOnly Property Fill() As Color
            Get
                Return fill_Renamed
            End Get
        End Property

        Public Sub New(ByVal rangeMin As Integer, ByVal rangeMax As Integer, ByVal fill As Color)
            Me.rangeMin_Renamed = rangeMin
            Me.rangeMax_Renamed = rangeMax
            Me.fill_Renamed = fill
        End Sub
    End Class

    Public Class ViewTypeToBoolConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Boolean) AndAlso TypeOf value Is ViewType AndAlso TypeOf parameter Is ViewType Then
                Return DirectCast(value, ViewType) = DirectCast(parameter, ViewType)
            End If
            Return False
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Boolean Then
                If targetType Is GetType(ViewType) Then
                    Return If(DirectCast(value, Boolean), ViewType.Gallery, ViewType.Map)
                End If
            End If
            Return Nothing
        End Function
    End Class

    Public Class ViewTypeToVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Visibility) AndAlso TypeOf value Is ViewType AndAlso TypeOf parameter Is ViewType Then
                Return If(DirectCast(value, ViewType) = DirectCast(parameter, ViewType), Visibility.Visible, Visibility.Hidden)
            End If
            Return Visibility.Hidden
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Visibility Then
                If targetType Is GetType(ViewType) Then
                    Return If(DirectCast(value, Visibility) = Visibility.Visible, ViewType.Gallery, ViewType.Map)
                End If
            End If
            Return Nothing
        End Function
    End Class
    Public Class CoordinateSystemTypeToVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Visibility) AndAlso TypeOf value Is CoordinateSystemType AndAlso TypeOf parameter Is CoordinateSystemType Then
                Return If(DirectCast(value, CoordinateSystemType) = DirectCast(parameter, CoordinateSystemType), Visibility.Visible, Visibility.Hidden)
            End If
            Return Visibility.Hidden
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Visibility Then
                If targetType Is GetType(CoordinateSystemType) Then
                    Return If(DirectCast(value, Visibility) = Visibility.Visible, CoordinateSystemType.Geo, CoordinateSystemType.Cartesian)
                End If
            End If
            Return Nothing
        End Function
    End Class
    Public Class DoubleToRenderTransforOffsetConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Double) AndAlso TypeOf value Is Double Then
                Dim doubleValue As Double = DirectCast(value, Double)
                If TypeOf parameter Is Double Then
                    Return doubleValue / DirectCast(parameter, Double)
                End If
                Return 0
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
    Public Class CoordinateSystemTypeToCoordinateSystemConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(MapCoordinateSystem) AndAlso TypeOf value Is CoordinateSystemType AndAlso DirectCast(value, CoordinateSystemType) = CoordinateSystemType.Cartesian Then
                Return New CartesianMapCoordinateSystem()
            End If
            Return New GeoMapCoordinateSystem()
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is MapCoordinateSystem AndAlso targetType Is GetType(CoordinateSystemType) Then
                Return If(TypeOf value Is GeoMapCoordinateSystem, CoordinateSystemType.Geo, CoordinateSystemType.Cartesian)
            End If
            Return Nothing
        End Function
    End Class
    Public Class ItemToTextConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is MapItem Then
                Dim path As MapPath = TryCast(value, MapPath)
                If path IsNot Nothing Then
                    Return HotelRoomTooltipHelper.CalculateTitle(path)
                End If
                Dim title As ShapeTitle = TryCast(value, ShapeTitle)
                If title IsNot Nothing Then
                    Return HotelRoomTooltipHelper.CalculateTitle(title.MapShape)
                End If
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
    Public Class ItemToImageSourceConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is MapItem Then
                Dim title As ShapeTitle = TryCast(value, ShapeTitle)
                Return HotelRoomTooltipHelper.GetItemImageSource(If(title IsNot Nothing, title.MapShape, DirectCast(value, MapItem)))
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
    Public Class ItemToImageVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is MapItem AndAlso targetType Is GetType(Visibility) Then
                Dim title As ShapeTitle = TryCast(value, ShapeTitle)
                Dim imageSource As String = HotelRoomTooltipHelper.GetItemImageSource(If(title IsNot Nothing, title.MapShape, DirectCast(value, MapItem)))
                Return If(String.IsNullOrWhiteSpace(imageSource), Visibility.Collapsed, Visibility.Visible)
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
    Public Class CountToMatrixTransformConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim itemsList As List(Of MapItem) = TryCast(value, List(Of MapItem))
            Dim count As Double = If(itemsList IsNot Nothing, itemsList.Count, 1.0)
            Dim scaleFactor As Double = Math.Log10(count / 5.0) * 0.02 + 0.05
            Dim offsetKoefX As Double = 318
            Dim offsetKoefY As Double = -455
            Return (New MatrixTransform(scaleFactor, 0, 0, scaleFactor, scaleFactor * offsetKoefX, scaleFactor * offsetKoefY)).Matrix
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
    Public Class CountToTextConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim itemsList As List(Of MapItem) = TryCast(value, List(Of MapItem))
            Dim count As Integer = If(itemsList IsNot Nothing, itemsList.Count, 1)
            Return String.Format("Cluster contains {0} item(s)", count)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
    Public Class MapTypeToVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Integer AndAlso targetType Is GetType(Visibility) AndAlso TypeOf parameter Is String Then
                Dim index As Integer = DirectCast(value, Integer)
                Dim mapType As String = DirectCast(parameter, String)
                If index = 1 AndAlso mapType = "population" Then
                    Return Visibility.Visible
                End If
                If index = 0 AndAlso mapType = "gdp" Then
                    Return Visibility.Visible
                End If
                Return Visibility.Collapsed
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
    Public Class ProviderNameToImageVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is ProviderName AndAlso targetType Is GetType(Visibility) Then
                Return If(DirectCast(value, ProviderName) = ProviderName.Bing, Visibility.Visible, Visibility.Collapsed)
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
    Public Class ProviderNameToCopyrightTextConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is ProviderName Then
                Dim providerName As ProviderName = DirectCast(value, ProviderName)
                If providerName = MapDemo.ProviderName.Bing Then
                    Return "Copyright © 2018 Microsoft and its suppliers. All rights reserved."
                End If
                If providerName = MapDemo.ProviderName.Osm Then
                    Return "© OpenStreetMap contributors"
                End If
                Return Nothing
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
    Public Class BoolToCircularScrollingConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean AndAlso targetType Is GetType(CircularScrollingMode) Then
                Return If(DirectCast(value, Boolean), CircularScrollingMode.TilesAndVectorItems, CircularScrollingMode.None)
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class

    Public Class ShapefileWorldResources
        Public ReadOnly Property CountriesFileUri() As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/Countries.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property
        Public ReadOnly Property AfricaFileUri() As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/Africa.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property
        Public ReadOnly Property SouthAmericaFileUri() As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/SouthAmerica.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property
        Public ReadOnly Property NorthAmericaFileUri() As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/NorthAmerica.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property
        Public ReadOnly Property AustraliaFileUri() As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/Australia.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property
        Public ReadOnly Property EurasiaFileUri() As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/Eurasia.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property

        Public Sub New()
        End Sub
    End Class

    Public Class PhotoGalleryResources
        Public ReadOnly Property CityInformationControlSource() As BitmapImage
            Get
                Return New BitmapImage(New Uri("/MapDemo;component/Images/PhotoGallery/CityInformationControl.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property
        Public ReadOnly Property LabelControlImageSource() As BitmapImage
            Get
                Return New BitmapImage(New Uri("/MapDemo;component/Images/PhotoGallery/Label.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property
        Public ReadOnly Property PlaceInfoControlPrevImageSource() As BitmapImage
            Get
                Return New BitmapImage(New Uri("/MapDemo;component/Images/PhotoGallery/PrevPlace.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property
        Public ReadOnly Property PlaceInfoControlNextImageSource() As BitmapImage
            Get
                Return New BitmapImage(New Uri("/MapDemo;component/Images/PhotoGallery/NextPlace.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public Sub New()
        End Sub
    End Class

    Public Class CityWeather
        Inherits BindableBase

        Private cityWeatherInfo As OpenWeatherMapService.CityWeatherInfo
        Private weatherCore As Weather
        Private weatherDescriptionsCore As List(Of WeatherDescription)

        Private weatherIconPath_Renamed As String = String.Empty

        Private forecast_Renamed As New ObservableCollection(Of CityWeather)()

        Private temperatureValueDataMember_Renamed As String = String.Empty

        Private temperatureString_Renamed As String = String.Empty

        Private crosshairLabelPattern_Renamed As String = String.Empty

        Public ReadOnly Property Day() As Date
            Get
                Return GetTime(cityWeatherInfo.Day)
            End Get
        End Property
        Public ReadOnly Property CityID() As Integer
            Get
                Return cityWeatherInfo.Id
            End Get
        End Property
        Public ReadOnly Property City() As String
            Get
                Return cityWeatherInfo.Name
            End Get
        End Property
        Public ReadOnly Property Longitude() As Double
            Get
                Return cityWeatherInfo.Coord.Longitude
            End Get
        End Property
        Public ReadOnly Property Latitude() As Double
            Get
                Return cityWeatherInfo.Coord.Latitude
            End Get
        End Property
        Public ReadOnly Property Weather() As Weather
            Get
                Return weatherCore
            End Get
        End Property
        Public ReadOnly Property WeatherDescriptions() As List(Of WeatherDescription)
            Get
                Return weatherDescriptionsCore
            End Get
        End Property
        Public Property ForecastTime() As Date

        Public Property WeatherIconPath() As String
            Get
                Return GetProperty(Function() weatherIconPath_Renamed)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() weatherIconPath_Renamed, value)
            End Set
        End Property
        Public Property Forecast() As ObservableCollection(Of CityWeather)
            Get
                Return GetProperty(Function() forecast_Renamed)
            End Get
            Set(ByVal value As ObservableCollection(Of CityWeather))
                SetProperty(Function() forecast_Renamed, value)
            End Set
        End Property
        Public Property TemperatureValueDataMember() As String
            Get
                Return GetProperty(Function() temperatureValueDataMember_Renamed)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() temperatureValueDataMember_Renamed, value)
            End Set
        End Property
        Public Property TemperatureString() As String
            Get
                Return GetProperty(Function() temperatureString_Renamed)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() temperatureString_Renamed, value)
            End Set
        End Property
        Public Property CrosshairLabelPattern() As String
            Get
                Return GetProperty(Function() crosshairLabelPattern_Renamed)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() crosshairLabelPattern_Renamed, value)
            End Set
        End Property

        Public Sub New(ByVal cityWeatherInfo As OpenWeatherMapService.CityWeatherInfo)
            Me.cityWeatherInfo = cityWeatherInfo
            Me.weatherCore = New Weather(cityWeatherInfo.Main)
            Me.weatherDescriptionsCore = New List(Of WeatherDescription)()
            For Each weatherDescription As OpenWeatherMapService.WeatherDescriptionInfo In cityWeatherInfo.Weather
                weatherDescriptionsCore.Add(New WeatherDescription(weatherDescription))
            Next weatherDescription
        End Sub

        Private Function GetTime(ByVal seconds As Long) As Date
            Dim dtDateTime As New Date(1970, 1, 1, 0, 0, 0, 0)
            Return dtDateTime.AddSeconds(Convert.ToDouble(seconds)).ToLocalTime()
        End Function
        Public Sub SetForecast(ByVal forecast As ObservableCollection(Of OpenWeatherMapService.CityWeatherInfo))
            Dim cityWeatherList As New ObservableCollection(Of CityWeather)()
            For Each cityWeatherInfo As OpenWeatherMapService.CityWeatherInfo In forecast
                cityWeatherList.Add(New CityWeather(cityWeatherInfo))
            Next cityWeatherInfo
            Me.Forecast = cityWeatherList
        End Sub
        Public Sub SetCurrentTemperatureType(ByVal temperatureScale As TemperatureScale)
            Select Case temperatureScale
                Case MapDemo.TemperatureScale.Fahrenheit
                    TemperatureValueDataMember = "Weather.FahrenheitTemperature"
                    TemperatureString = Weather.FahrenheitTemperatureString
                    CrosshairLabelPattern = "{A:g} : {V} °F"
                Case MapDemo.TemperatureScale.Celsius
                    TemperatureValueDataMember = "Weather.CelsiusTemperature"
                    TemperatureString = Weather.CelsiusTemperatureString
                    CrosshairLabelPattern = "{A:g} : {V} °C"
            End Select
        End Sub
    End Class
    Public Class Weather
        Private weatherInfo As OpenWeatherMapService.WeatherInfo

        Public ReadOnly Property CelsiusTemperature() As Integer
            Get
                Return CInt((weatherInfo.Temp))
            End Get
        End Property
        Public ReadOnly Property FahrenheitTemperature() As Integer
            Get
                Return CelsiusTemperature * 9 \ 5 + 32
            End Get
        End Property
        Public ReadOnly Property KelvinTemperature() As Integer
            Get
                Return CInt((weatherInfo.Temp))
            End Get
        End Property
        Public ReadOnly Property CelsiusTemperatureString() As String
            Get
                Return CelsiusTemperature.ToString("+#;-#;0") & " °C"
            End Get
        End Property
        Public ReadOnly Property FahrenheitTemperatureString() As String
            Get
                Return FahrenheitTemperature.ToString("+#;-#;0") & " °F"
            End Get
        End Property
        Public ReadOnly Property KelvinTemperatureString() As String
            Get
                Return weatherInfo.Temp.ToString("+#;-#;0") & " °K"
            End Get
        End Property

        Public Sub New(ByVal weatherInfo As OpenWeatherMapService.WeatherInfo)
            Me.weatherInfo = weatherInfo
        End Sub
    End Class
    Public Class WeatherDescription
        Private weatherDescriptionInfo As OpenWeatherMapService.WeatherDescriptionInfo

        Public ReadOnly Property IconName() As String
            Get
                Return weatherDescriptionInfo.Icon
            End Get
        End Property

        Public Sub New(ByVal weatherDescriptionInfo As OpenWeatherMapService.WeatherDescriptionInfo)
            Me.weatherDescriptionInfo = weatherDescriptionInfo
        End Sub
    End Class
    Public Class OpenWeatherMapService
        Inherits BindableBase

        #Region "classes for JSON parsing"

        <DataContract>
        Public Class ForecastInfo
            <DataMember>
            Public list As ObservableCollection(Of CityWeatherInfo)
        End Class
        <DataContract>
        Public Class WorldWeatherInfo
            <DataMember>
            Public list As ObservableCollection(Of CityWeatherInfo)
        End Class
        <DataContract>
        Public Class CityWeatherInfo
            <DataMember(Name := "id")>
            Public Property Id() As Integer
            <DataMember(Name := "name")>
            Public Property Name() As String
            <DataMember(Name := "coord")>
            Public Property Coord() As Coordinates
            <DataMember(Name := "main")>
            Public Property Main() As WeatherInfo
            <DataMember(Name := "weather")>
            Public Property Weather() As List(Of WeatherDescriptionInfo)
            <DataMember(Name := "wind")>
            Public Property Wind() As WindInfo
            <DataMember(Name := "dt")>
            Public Property Day() As Long
        End Class
        <DataContract>
        Public Class WeatherDescriptionInfo
            <DataMember(Name := "main")>
            Public Property Main() As String
            <DataMember(Name := "description")>
            Public Property Description() As String
            <DataMember(Name := "icon")>
            Public Property Icon() As String
        End Class
        <DataContract>
        Public Class WindInfo
            <DataMember(Name := "speed")>
            Public Property Speed() As Double
            <DataMember(Name := "deg")>
            Public Property Deg() As Double
        End Class
        <DataContract>
        Public Class WeatherInfo
            <DataMember(Name := "temp")>
            Public Property Temp() As Double
            <DataMember(Name := "pressure")>
            Public Property Pressure() As Double
            <DataMember(Name := "humidity")>
            Public Property Humidity() As Double
        End Class
        <DataContract>
        Public Class Coordinates

            <DataMember(Name := "lon")>
            Private lon1_Renamed As Double

            <DataMember(Name := "Lon")>
            Private lon2_Renamed As Double

            <DataMember(Name := "lat")>
            Private lat1_Renamed As Double

            <DataMember(Name := "Lat")>
            Private lat2_Renamed As Double

            #Region "warnings workarond"
            Protected WriteOnly Property Lat1() As Double
                Set(ByVal value As Double)
                    Me.lat1_Renamed = value
                End Set
            End Property
            Protected WriteOnly Property Lat2() As Double
                Set(ByVal value As Double)
                    Me.lat2_Renamed = value
                End Set
            End Property
            Protected WriteOnly Property Lon1() As Double
                Set(ByVal value As Double)
                    Me.lon1_Renamed = value
                End Set
            End Property
            Protected WriteOnly Property Lon2() As Double
                Set(ByVal value As Double)
                    Me.lon2_Renamed = value
                End Set
            End Property
            #End Region
            Public ReadOnly Property Longitude() As Double
                Get
                    Return If(lon1_Renamed <> 0, lon1_Renamed, lon2_Renamed)
                End Get
            End Property
            Public ReadOnly Property Latitude() As Double
                Get
                    Return If(lat1_Renamed <> 0, lat1_Renamed, lat2_Renamed)
                End Get
            End Property
        End Class

        #End Region

        Private Const OpenWeatherKey As String = "fcbff6dbed7bd7f295489daf4ffef3f1"

        Private temperatureScale As TemperatureScale = MapDemo.TemperatureScale.Celsius
        Private weatherLocker As New Object()
        Private capitals As New List(Of String)()

        Private weatherInCities_Renamed As New ObservableCollection(Of CityWeather)()

        Public Property WeatherInCities() As ObservableCollection(Of CityWeather)
            Get
                Return GetProperty(Function() weatherInCities_Renamed)
            End Get
            Set(ByVal value As ObservableCollection(Of CityWeather))
                SetProperty(Function() weatherInCities_Renamed, value)
            End Set
        End Property
        Public Property Forecast() As ObservableCollection(Of CityWeather)

        Private ReadOnly uiDispatcher As Dispatcher

        Public Sub New()
            LoadCapitalsFromXML()
            uiDispatcher = Dispatcher.CurrentDispatcher
        End Sub

        Private Sub weatherClient_OpenReadCompleted(ByVal sender As Object, ByVal e As OpenReadCompletedEventArgs)
            If e.Error Is Nothing Then
                Task.Factory.StartNew(Sub()
                    Dim dc As New DataContractJsonSerializer(GetType(WorldWeatherInfo))
                    Dim worldWeatherInfo As WorldWeatherInfo = DirectCast(dc.ReadObject(e.Result), WorldWeatherInfo)
                    Dim tempWeatherInCities As New ObservableCollection(Of CityWeather)()
                    For Each weatherInfo As CityWeatherInfo In worldWeatherInfo.list
                        Dim cityWeather As New CityWeather(weatherInfo)
                        Dim cityWithId As String = String.Format("{0};{1}", cityWeather.City, cityWeather.CityID)
                        If capitals.Contains(cityWeather.City) OrElse capitals.Contains(cityWithId) Then
                            If cityWeather.WeatherDescriptions IsNot Nothing AndAlso cityWeather.WeatherDescriptions.Count > 0 Then
                                cityWeather.WeatherIconPath = "http://openweathermap.org/img/w/" & cityWeather.WeatherDescriptions(0).IconName & ".png"
                            End If
                            tempWeatherInCities.Add(cityWeather)
                        End If
                    Next weatherInfo
                    SyncLock weatherLocker
                        WeatherInCities = tempWeatherInCities
                    End SyncLock
                    Me.uiDispatcher.Invoke(New Action(Sub() UpdateCurrentTemperatureType()))
                End Sub)
            End If
        End Sub
        Private Sub forecastClient_OpenReadCompleted(ByVal sender As Object, ByVal e As OpenReadCompletedEventArgs)
            If e.Error Is Nothing Then
                RemoveHandler DirectCast(sender, WebClient).OpenReadCompleted, AddressOf forecastClient_OpenReadCompleted
                Dim stream As Stream = e.Result
                Dim cityWeatherInfo As CityWeather = DirectCast(e.UserState, CityWeather)
                Task.Factory.StartNew(Sub()
                    Dim dc As New DataContractJsonSerializer(GetType(ForecastInfo))

                    Dim forecast_Renamed As ForecastInfo = DirectCast(dc.ReadObject(stream), ForecastInfo)
                    Me.uiDispatcher.Invoke(New Action(Sub() cityWeatherInfo.SetForecast(forecast_Renamed.list)))
                End Sub)
            End If
        End Sub
        Private Sub LoadCapitalsFromXML()
            Dim document As XDocument = DataLoader.LoadXmlFromResources("/Data/Capitals.xml")
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("Capitals").Elements()
                    capitals.Add(element.Value)
                Next element
            End If
        End Sub
        Private Sub UpdateCurrentTemperatureType()
            SyncLock weatherLocker
                If WeatherInCities IsNot Nothing Then
                    For Each weather As CityWeather In WeatherInCities
                        weather.SetCurrentTemperatureType(temperatureScale)
                    Next weather
                End If
            End SyncLock
        End Sub
        Public Sub GetWeatherAsync()
            Dim link As String = "http://api.openweathermap.org/data/2.5/box/city?bbox=-180,-90,180,90&cluster=yes&APPID=" & OpenWeatherKey
            Dim weatherClient As New WebClient()
            AddHandler weatherClient.OpenReadCompleted, AddressOf weatherClient_OpenReadCompleted
            weatherClient.OpenReadAsync(New Uri(link))
        End Sub
        Public Sub GetForecastForCityAsync(ByVal cityWeather As CityWeather)
            Dim link As String = String.Format("http://api.openweathermap.org/data/2.5/forecast?units=metric&id={0}&APPID={1}", cityWeather.CityID.ToString(), OpenWeatherKey)
            Dim forecastClient As New WebClient()
            AddHandler forecastClient.OpenReadCompleted, AddressOf forecastClient_OpenReadCompleted
            forecastClient.OpenReadAsync(New Uri(link), cityWeather)
        End Sub
        Public Sub SetCurrentTemperatureType(ByVal temperatureScale As TemperatureScale)
            Me.temperatureScale = temperatureScale
            UpdateCurrentTemperatureType()
        End Sub
    End Class
    Public NotInheritable Class HotelRoomTooltipHelper

        Private Sub New()
        End Sub

        #Region "inner class"
        Private Class HotelImagesGenerator
            Private Class PathsIndexPair
                Public Property Paths() As String()
                Public Property Index() As Integer
            End Class

            Private Shared ReadOnly Categories() As String = { "Restaurant", "MeetingRoom", "Bathroom", "Bedroom", "Outofdoors", "ServiceRoom", "Pool", "Lobby" }
            Private Const basePath As String = "/MapDemo;component/"


            Private hotelIndex_Renamed As Integer = 0
            Private filesWithIndices As New List(Of PathsIndexPair)()

            Public Property HotelIndex() As Integer
                Get
                    Return hotelIndex_Renamed
                End Get
                Set(ByVal value As Integer)
                    hotelIndex_Renamed = value
                    UpdateIndices()
                End Set
            End Property

            Public Sub New()
                For Each category As String In Categories
                    filesWithIndices.Add(New PathsIndexPair() With {.Index = 0, .Paths = GetAvailableFiles(category)})
                Next category
            End Sub
            Private Sub UpdateIndices()
                filesWithIndices(0).Index = hotelIndex_Renamed * 2
                filesWithIndices(1).Index = 0
                filesWithIndices(2).Index = hotelIndex_Renamed * 4
                filesWithIndices(6).Index = hotelIndex_Renamed
            End Sub
            Private Function GetAvailableFiles(ByVal category As String) As String()
                Dim asm = System.Reflection.Assembly.GetExecutingAssembly()
                Dim resName As String = asm.GetName().Name & ".g.resources"
                Using stream = asm.GetManifestResourceStream(resName)
                Using reader = New System.Resources.ResourceReader(stream)
                    Return reader.Cast(Of DictionaryEntry)().Select(Function(entry) CStr(entry.Key)).Where(Function(entry) entry.StartsWith("images/hotels/" & category.ToLowerInvariant())).ToArray()
                End Using
                End Using
            End Function
            Private Function GetImagePath(ByVal category As Integer, ByVal name As String, ByVal roomCat As Integer) As String
                If category = 4 Then
                    filesWithIndices(3).Index = roomCat
                End If
                Return GetCategoryImagePath(filesWithIndices(category - 1))
            End Function
            Private Function GetCategoryImagePath(ByVal pathsWithIndex As PathsIndexPair) As String
                If pathsWithIndex.Paths.Length = 0 Then
                    Return Nothing
                End If
                Dim index As Integer = pathsWithIndex.Index Mod pathsWithIndex.Paths.Length
                pathsWithIndex.Index += 1
                Return pathsWithIndex.Paths(index)
            End Function
            Public Function GetItemImagePath(ByVal item As MapItem) As String
                Dim imagePath As String = GetImagePath(CInt((item.Attributes("CATEGORY").Value)), item.Attributes("NAME").Value.ToString(), CInt((item.Attributes("ROOMCAT").Value)))
                If imagePath Is Nothing Then
                    Return Nothing
                End If
                Dim totalPath As String = basePath & imagePath
                item.Attributes.Add(New MapItemAttribute() With {.Name = "IMAGESOURCE", .Value = totalPath})
                Return totalPath
            End Function
        End Class
        #End Region

        Private Shared imagesGenerator As New HotelImagesGenerator()

        Public Shared Function CalculateTitle(ByVal item As MapItem) As String
            Dim category As Integer = CInt((item.Attributes("CATEGORY").Value))
            Dim text As String = CStr(item.Attributes("NAME").Value)
            Return If(category = 4, String.Format("Room: {0}", text), text)
        End Function
        Public Shared Function GetItemImageSource(ByVal item As MapItem) As String
            If item Is Nothing Then
                Return Nothing
            End If
            Dim attr As MapItemAttribute = item.Attributes("IMAGESOURCE")
            Return If(attr IsNot Nothing, CStr(attr.Value), imagesGenerator.GetItemImagePath(item))
        End Function
        Public Shared Sub UpdateHotelIndex(ByVal index As Integer)
            imagesGenerator.HotelIndex = index
        End Sub
    End Class
End Namespace
