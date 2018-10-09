Imports System
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media.Imaging
Imports System.Xml.Linq
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Map

Namespace ProductsDemo.Modules
    Partial Public Class PhotoGallery
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            Dim viewModel_Renamed As New CitiesViewModel(map, TryCast(Resources("citySmallIconTemplate"), DataTemplate))
            LayoutRoot.DataContext = viewModel_Renamed
            placePointer.Content = viewModel_Renamed
        End Sub
        Private Sub GalleryItemClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim item As PhotoGalleryItemControl = TryCast(sender, PhotoGalleryItemControl)
            If item IsNot Nothing Then
                ViewModel.SelectedPlace = TryCast(item.DataContext, PlaceInfo)
            End If
        End Sub
        Private Sub OnGalleryClose(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.SelectedCity = Nothing
        End Sub
        Private Sub OnBackClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.SelectedCity = Nothing
        End Sub
        Private Sub photoGallery_MouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            ViewModel.SelectedCity = Nothing
        End Sub
        Private Sub placeControl_ShowNextSight(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.ShowNextSight()
        End Sub
        Private Sub placeControl_ShowPreviousSight(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.ShowPrevSight()
        End Sub
        Private ReadOnly Property ViewModel() As CitiesViewModel
            Get
                Return TryCast(LayoutRoot.DataContext, CitiesViewModel)
            End Get
        End Property
    End Class


    Public Class CitiesViewModel
        Inherits BindableBase

        Private Const MaxZoomLevelInWorldView As Integer = 7
        Private Const MinZoomLevelInWorldView As Integer = 5
        Private Const MinZoomLevelInCityView As Integer = 15
        Private Const MaxZoomLevelInCityView As Integer = 18

        Private citiesCore As ObservableCollection(Of MapCustomElement) = Nothing
        Private selectedCityCore As CityInfo = Nothing
        Private selectedPlaceCore As PlaceInfo = Nothing
        Private viewTypeCore As PhotoGalleryViewType = PhotoGalleryViewType.Map
        Private centerPointCore As CoordPoint = New GeoPoint(47.5, 2)
        Private cityPointCore As New Point(0, 0)
        Private zoomLevelCore As Double = 5

        Private minZoomLevel_Renamed As Integer = MinZoomLevelInWorldView

        Private maxZoomLevel_Renamed As Integer = MaxZoomLevelInWorldView
        Private citySmallIconsCore As ObservableCollection(Of MapCustomElement) = Nothing

        Public Property Cities() As ObservableCollection(Of MapCustomElement)
            Get
                Return citiesCore
            End Get
            Private Set(ByVal value As ObservableCollection(Of MapCustomElement))
                SetProperty(citiesCore, value, "Cities")
            End Set
        End Property
        Public Property CitySmallIcons() As ObservableCollection(Of MapCustomElement)
            Get
                Return citySmallIconsCore
            End Get
            Private Set(ByVal value As ObservableCollection(Of MapCustomElement))
                SetProperty(citySmallIconsCore, value, "CitySmallIcons")
            End Set
        End Property
        Public Property SelectedCity() As CityInfo
            Get
                Return selectedCityCore
            End Get
            Set(ByVal value As CityInfo)
                SetProperty(selectedCityCore, value, "SelectedCity", AddressOf SelectedItemPropertyChanged)
            End Set
        End Property
        Public Property SelectedPlace() As PlaceInfo
            Get
                Return selectedPlaceCore
            End Get
            Set(ByVal value As PlaceInfo)
                SetProperty(selectedPlaceCore, value, "SelectedPlace", AddressOf SelectedItemPropertyChanged)
            End Set
        End Property
        Public Property ViewType() As PhotoGalleryViewType
            Get
                Return viewTypeCore
            End Get
            Set(ByVal value As PhotoGalleryViewType)
                SetProperty(viewTypeCore, value, "ViewType", AddressOf ViewTypePropertyChanged)
            End Set
        End Property
        Public Property CenterPoint() As CoordPoint
            Get
                Return centerPointCore
            End Get
            Set(ByVal value As CoordPoint)
                SetProperty(centerPointCore, value, "CenterPoint")
            End Set
        End Property
        Public Property CityPoint() As Point
            Get
                Return cityPointCore
            End Get
            Set(ByVal value As Point)
                SetProperty(cityPointCore, value, "CityPoint")
            End Set
        End Property
        Public Property ZoomLevel() As Double
            Get
                Return zoomLevelCore
            End Get
            Set(ByVal value As Double)
                SetProperty(zoomLevelCore, value, "ZoomLevel")
            End Set
        End Property
        Public Property MinZoomLevel() As Integer
            Get
                Return minZoomLevel_Renamed
            End Get
            Set(ByVal value As Integer)
                SetProperty(minZoomLevel_Renamed, value, "MinZoomLevel")
            End Set
        End Property
        Public Property MaxZoomLevel() As Integer
            Get
                Return maxZoomLevel_Renamed
            End Get
            Set(ByVal value As Integer)
                SetProperty(maxZoomLevel_Renamed, value, "MaxZoomLevel")
            End Set
        End Property

        Private Sub SelectedItemPropertyChanged()
            Me.UpdateViewType()
        End Sub
        Private Sub ViewTypePropertyChanged()
            Me.Update()
        End Sub

        Private ReadOnly map As MapControl

        Private ReadOnly Property Layer() As LayerBase
            Get
                Return If(map.Layers.Count > 0, map.Layers(0), Nothing)
            End Get
        End Property

        Public Sub New(ByVal map As MapControl, ByVal citySmallIconTemplate As DataTemplate)
            Me.map = map
            Cities = New ObservableCollection(Of MapCustomElement)()
            CitySmallIcons = New ObservableCollection(Of MapCustomElement)()
            LoadDataFromXML(citySmallIconTemplate)
        End Sub
        Private Sub LoadDataFromXML(ByVal citySmallIconTemplate As DataTemplate)
            Dim fileName As String = Utils.GetRelativePath("CitiesData.xml")
            Dim document As XDocument = DataLoader.LoadXmlFromFile(fileName)
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("Cities").Elements()
                    Dim cityName As String = element.Element("CityName").Value
                    Dim cityLocation As New GeoPoint(Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture), Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture))
                    Dim cityInfo As New CityInfo(cityName, cityLocation)
                    For Each placeElement As XElement In element.Element("Places").Elements()
                        Dim placeName As String = placeElement.Element("Name").Value
                        Dim placeLocation As New GeoPoint(Convert.ToDouble(placeElement.Element("Latitude").Value, CultureInfo.InvariantCulture), Convert.ToDouble(placeElement.Element("Longitude").Value, CultureInfo.InvariantCulture))
                        Dim placeDescription As String = placeElement.Element("Description").Value
                        Dim placeImageUri As New Uri(placeElement.Element("ImageUri").Value, UriKind.RelativeOrAbsolute)
                        cityInfo.Places.Add(New PlaceInfo(placeName, cityName, placeLocation, placeDescription, New BitmapImage(placeImageUri)))
                    Next placeElement
                    Dim binding As New Binding("ViewType") With {.Source = Me, .Converter = New ViewTypeToBoolConverter(), .ConverterParameter = PhotoGalleryViewType.Map}
                    Dim city As New CityInformationControl() With {.CityInfo = cityInfo}
                    city.SetBinding(CityInformationControl.VisibleProperty, binding)
                    Dim mapItem As New MapCustomElement() With {.Content = city, .Location = cityLocation}
                    AddHandler mapItem.MouseLeftButtonUp, AddressOf OnMouseLeftButtonUp
                    AddHandler mapItem.MouseLeftButtonDown, AddressOf OnMouseLeftButtonDown
                    AddHandler city.TouchDown, AddressOf OnCityTouchDown
                    Cities.Add(mapItem)
                    CitySmallIcons.Add(New MapCustomElement() With {.Content = cityInfo, .ContentTemplate = citySmallIconTemplate, .Location = cityInfo.Location})
                Next element
            End If
        End Sub
        Private Sub UpdateViewType()
            If SelectedCity IsNot Nothing Then
                ViewType = If(SelectedPlace IsNot Nothing, PhotoGalleryViewType.Detail, PhotoGalleryViewType.Gallery)
            Else
                ViewType = PhotoGalleryViewType.Map
            End If
        End Sub
        Private Sub Update()
            Select Case ViewType
                Case PhotoGalleryViewType.Map
                CaseLabel1:
                    MinZoomLevel = MinZoomLevelInWorldView
                    MaxZoomLevel = MaxZoomLevelInWorldView
                    ZoomLevel = 5
                Case PhotoGalleryViewType.Gallery
                    MinZoomLevel = MinZoomLevelInWorldView
                    MaxZoomLevel = MaxZoomLevelInWorldView
                    ZoomLevel = 5
                    CityPoint = Layer.GeoToScreenPoint(SelectedCity.Location)
                Case PhotoGalleryViewType.Detail
                    MinZoomLevel = MinZoomLevelInCityView
                    MaxZoomLevel = MaxZoomLevelInCityView
                    ZoomLevel = 17
                    CenterPoint = SelectedPlace.Location
                Case Else
                    GoTo CaseLabel1
            End Select
        End Sub
        Private Sub SelectCity(ByVal cityInfo As CityInfo)
            SelectedPlace = Nothing
            SelectedCity = cityInfo
            UpdateViewType()
        End Sub
        Private Sub OnMouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim element As MapCustomElement = TryCast(sender, MapCustomElement)
            If element IsNot Nothing Then
                SelectCity(CType(element.Content, CityInformationControl).CityInfo)
                e.Handled = True
            End If
        End Sub
        Private Sub OnMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If TypeOf sender Is MapCustomElement Then
                e.Handled = True
            End If
        End Sub
        Private Sub OnCityTouchDown(ByVal sender As Object, ByVal e As TouchEventArgs)
            SelectCity(DirectCast(sender, CityInformationControl).CityInfo)
        End Sub
        Public Sub ShowNextSight()
            If SelectedCity IsNot Nothing AndAlso SelectedPlace IsNot Nothing Then
                Dim index As Integer = SelectedCity.Places.IndexOf(SelectedPlace) + 1
                SelectedPlace = If(index < SelectedCity.Places.Count, SelectedCity.Places(index), SelectedCity.Places(0))
                CenterPoint = SelectedPlace.Location
            End If
        End Sub
        Public Sub ShowPrevSight()
            If SelectedCity IsNot Nothing AndAlso SelectedPlace IsNot Nothing Then
                Dim index As Integer = SelectedCity.Places.IndexOf(SelectedPlace) - 1
                SelectedPlace = If(index < 0, SelectedCity.Places(SelectedCity.Places.Count - 1), SelectedCity.Places(index))
                CenterPoint = SelectedPlace.Location
            End If
        End Sub
    End Class
End Namespace
