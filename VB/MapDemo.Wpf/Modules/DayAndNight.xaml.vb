Imports DevExpress.Demos.DayAndNightLineCalculator
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Map
Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace MapDemo
    Partial Public Class DayAndNight
        Inherits MapDemoModule


        Private viewModel_Renamed As DayAndNightViewModel

        Private ReadOnly Property ViewModel() As DayAndNightViewModel
            Get
                Return viewModel_Renamed
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            Me.viewModel_Renamed = ViewModelSource.Create(Function() New DayAndNightViewModel(Map))
            Me.DataContext = viewModel_Renamed
        End Sub
        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.SetCurrentDateTime()
        End Sub
        Private Sub ButtonBackwardClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.SetPreviousDateTime()
        End Sub
        Private Sub ButtonForwardClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.SetNextDateTime()
        End Sub
        Private Sub lbProjection_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.ZoomToFit()
        End Sub
    End Class
    <POCOViewModel>
    Public Class DayAndNightViewModel
        Inherits BindableBase

        Private Const DiscreteHoursStep As Double = 0.5
        Private Const SteadilyHoursStep As Double = 24.5

        Private ReadOnly map_Renamed As MapControl
        Private timer As DispatcherTimer

        Protected ReadOnly Property Map() As MapControl
            Get
                Return map_Renamed
            End Get
        End Property
        Public Overridable Property SunPosition() As GeoPoint
        Public Overridable Property MoonPosition() As GeoPoint
        Public Overridable Property DayAndNightLineVertices() As CoordPointCollection
        Public Overridable Property IsSteady() As Boolean
        Public Overridable Property DataContext() As Object

        Protected Sub OnIsSteadyChanged()
            Me.timer.IsEnabled = IsSteady
        End Sub
        Public Overridable Property ActualDateTime() As Date
        Protected Sub OnActualDateTimeChanged()
            UpdateDayAndNightLine()
        End Sub

        Public Sub New(ByVal map As MapControl)
            Me.map_Renamed = map
            InitializeTimer()
            AddHandler Me.Map.Layers(0).Loaded, AddressOf DayAndNightViewModel_Loaded
            AddHandler Me.Map.Layers(0).Unloaded, AddressOf DayAndNightViewModel_Unloaded
            IsSteady = True
            SunPosition = New GeoPoint()
            MoonPosition = New GeoPoint()
            DayAndNightLineVertices = New CoordPointCollection()
            ActualDateTime = Date.UtcNow
        End Sub
        Private Sub InitializeTimer()
            Me.timer = New DispatcherTimer()
            Me.timer.Interval = TimeSpan.FromMilliseconds(100)
            Me.timer.IsEnabled = True
            AddHandler Me.timer.Tick, AddressOf timer_Tick
        End Sub
        Private Sub UpdateDayAndNightLine()
            Dim sun3DPosition() As Double = DayAndNightLineCalculator.CalculateSunPosition(ActualDateTime)

            Dim sunPosition_Renamed As New GeoPoint(sun3DPosition(1), sun3DPosition(0))

            Dim moonPosition_Renamed As GeoPoint = GetOppositePoint(sunPosition_Renamed)

            Dim dayAndNightLineVertices_Renamed As CoordPointCollection = GetdayAndNightLineVertices(sunPosition_Renamed, 0.1)
            Dim isNorthNight As Boolean = DayAndNightLineCalculator.CalculateIsNorthNight(sun3DPosition)
            If isNorthNight Then
                AddNorthContour(dayAndNightLineVertices_Renamed)
            Else
                AddSouthContour(dayAndNightLineVertices_Renamed)
            End If
            SunPosition = sunPosition_Renamed
            MoonPosition = moonPosition_Renamed
            DayAndNightLineVertices = dayAndNightLineVertices_Renamed
        End Sub
        Private Function GetOppositePoint(ByVal sunLocation As GeoPoint) As GeoPoint
            Dim lat As Double = -sunLocation.Latitude
            Dim lon As Double = sunLocation.Longitude + 180
            If lon > 180 Then
                lon -= 360
            End If
            Return New GeoPoint(lat, lon)
        End Function
        Private Function GetdayAndNightLineVertices(ByVal sunLocation As GeoPoint, ByVal [step] As Double) As CoordPointCollection
            Dim result As New CoordPointCollection()
            Dim latitudes As IList(Of Double) = DayAndNightLineCalculator.GetDayAndNightLineLatitudes(sunLocation.Latitude, sunLocation.Longitude, [step])
            Dim lon As Double = -180
            For Each lat As Double In latitudes
                result.Add(New GeoPoint(lat, lon))
                lon += [step]
            Next lat
            Return result
        End Function
        Private Sub AddNorthContour(ByVal dayAndNightLineVertices As CoordPointCollection)
            Dim initLat As Double = Math.Ceiling(CType(dayAndNightLineVertices(dayAndNightLineVertices.Count - 1), GeoPoint).Latitude)
            For latForward As Double = initLat To 90.0
                dayAndNightLineVertices.Add(New GeoPoint(latForward, 180))
            Next latForward
            For lon As Double = 180 To -180 Step -1
                dayAndNightLineVertices.Add(New GeoPoint(90, lon))
            Next lon
            initLat = Math.Ceiling(CType(dayAndNightLineVertices(0), GeoPoint).Latitude)
            Dim latBackward As Double = 90
            Do While latBackward >= initLat
                dayAndNightLineVertices.Add(New GeoPoint(latBackward, -180))
                latBackward -= 1
            Loop
        End Sub
        Private Sub AddSouthContour(ByVal dayAndNightLineVertices As CoordPointCollection)
            Dim initLat As Double = Math.Ceiling(CType(dayAndNightLineVertices(dayAndNightLineVertices.Count - 1), GeoPoint).Latitude)
            For lat As Double = initLat To -90.0 Step -1
                dayAndNightLineVertices.Add(New GeoPoint(lat, 180))
            Next lat
            For lon As Double = 180 To -180 Step -1
                dayAndNightLineVertices.Add(New GeoPoint(-90, lon))
            Next lon
            initLat = Math.Ceiling(CType(dayAndNightLineVertices(0), GeoPoint).Latitude)
            For lat As Double = -90 To initLat
                dayAndNightLineVertices.Add(New GeoPoint(lat, -180))
            Next lat
        End Sub
        Private Function GetNextDateTime(ByVal dt As Date) As Date
            Return dt.AddHours(If(IsSteady, SteadilyHoursStep, DiscreteHoursStep))
        End Function
        Private Function GetPreviousDateTime(ByVal dt As Date) As Date
            Return dt.AddHours(-DiscreteHoursStep)
        End Function
        Public Sub SetCurrentDateTime()
            IsSteady = False
            ActualDateTime = Date.UtcNow
        End Sub
        Public Sub SetPreviousDateTime()
            IsSteady = False
            ActualDateTime = GetPreviousDateTime(ActualDateTime)
        End Sub
        Public Sub SetNextDateTime()
            IsSteady = False
            ActualDateTime = GetNextDateTime(ActualDateTime)
        End Sub
        Public Sub ZoomToFit()
            Map.EnableZooming = True
            Map.ZoomToFitLayerItems(0.3)
            Map.EnableZooming = False
        End Sub
        Private Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            ActualDateTime = GetNextDateTime(ActualDateTime)
        End Sub
        Private Sub DayAndNightViewModel_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ZoomToFit()
        End Sub
        Private Sub DayAndNightViewModel_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.timer.Stop()
            RemoveHandler Me.timer.Tick, AddressOf timer_Tick
        End Sub
    End Class
End Namespace
