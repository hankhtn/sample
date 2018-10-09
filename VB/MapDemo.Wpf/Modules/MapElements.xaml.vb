Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Threading
Imports System.Xml.Linq
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Map

Namespace MapDemo
    Partial Public Class MapElements
        Inherits MapDemoModule

        Private dataGenerator As FlightMapDataGenerator

        Public Sub New()
            InitializeComponent()
            dataGenerator = New FlightMapDataGenerator(TryCast(Resources("airportTemplate"), DataTemplate), TryCast(Resources("planeTemplate"), DataTemplate), planeInfoPanel)
            DataContext = dataGenerator
            dataGenerator.SpeedScale = Convert.ToDouble(tbSpeedScale.Value)
            AddHandler ModuleUnloaded, AddressOf MapElements_Unloaded
        End Sub

        Private Sub MapElements_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            dataGenerator.Dispose()
        End Sub
        Private Sub tbSpeedScale_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If dataGenerator IsNot Nothing Then
                dataGenerator.SpeedScale = Convert.ToDouble(e.NewValue)
            End If
        End Sub
    End Class

    Public Class PlaneTrajectory
        Private Class TrajectoryPart
            Private ReadOnly startPointField As GeoPoint
            Private ReadOnly endPointField As GeoPoint
            Private ReadOnly flightTimeField As Double
            Private ReadOnly courseField As Double

            Public ReadOnly Property StartPoint() As GeoPoint
                Get
                    Return startPointField
                End Get
            End Property
            Public ReadOnly Property EndPoint() As GeoPoint
                Get
                    Return endPointField
                End Get
            End Property
            Public ReadOnly Property FlightTime() As Double
                Get
                    Return flightTimeField
                End Get
            End Property
            Public ReadOnly Property Course() As Double
                Get
                    Return courseField
                End Get
            End Property

            Public Sub New(ByVal projection As ProjectionBase, ByVal startPoint As GeoPoint, ByVal endPoint As GeoPoint, ByVal speedInKmH As Double)
                Me.startPointField = startPoint
                Me.endPointField = endPoint
                Dim sizeInKm As Size = projection.GeoToKilometersSize(startPoint, New Size(Math.Abs(startPoint.Longitude - endPoint.Longitude), Math.Abs(startPoint.Latitude - endPoint.Latitude)))
                Dim partlength As Double = Math.Sqrt(sizeInKm.Width * sizeInKm.Width + sizeInKm.Height * sizeInKm.Height)
                flightTimeField = partlength / speedInKmH
                courseField = Math.Atan2((endPoint.Longitude - startPoint.Longitude), (endPoint.Latitude - startPoint.Latitude)) * 180 / Math.PI
            End Sub
            Public Function GetPointByCurrentFlightTime(ByVal currentFlightTime As Double, ByVal projection As ProjectionBase) As GeoPoint
                If currentFlightTime > FlightTime Then
                    Return endPointField
                End If
                Dim ratio As Double = currentFlightTime / FlightTime
                Return New GeoPoint(startPointField.Latitude + ratio * (endPointField.Latitude - startPointField.Latitude), startPointField.Longitude + ratio * (endPointField.Longitude - startPointField.Longitude))
            End Function
        End Class

        Private ReadOnly trajectory As New List(Of TrajectoryPart)()
        Private ReadOnly projection As New SphericalMercatorProjection()

        Public ReadOnly Property StartPoint() As GeoPoint
            Get
                Return If(trajectory.Count > 0, trajectory(0).StartPoint, New GeoPoint(0, 0))
            End Get
        End Property
        Public ReadOnly Property EndPoint() As GeoPoint
            Get
                Return If(trajectory.Count > 0, trajectory(trajectory.Count - 1).EndPoint, New GeoPoint(0, 0))
            End Get
        End Property
        Public ReadOnly Property FlightTime() As Double
            Get
                Dim result As Double = 0.0
                For Each part As TrajectoryPart In trajectory
                    result += part.FlightTime
                Next part
                Return result
            End Get
        End Property

        Public Sub New(ByVal points As List(Of GeoPoint), ByVal speedInKmH As Double)
            For i As Integer = 0 To points.Count - 2
                trajectory.Add(New TrajectoryPart(projection, points(i), points(i + 1), speedInKmH))
            Next i
        End Sub
        Public Function GetPointByCurrentFlightTime(ByVal currentFlightTime As Double) As GeoPoint
            Dim projection As New SphericalMercatorProjection()
            Dim time As Double = 0.0
            For i As Integer = 0 To trajectory.Count - 2
                If trajectory(i).FlightTime > currentFlightTime - time Then
                    Return trajectory(i).GetPointByCurrentFlightTime(currentFlightTime - time, projection)
                End If
                time += trajectory(i).FlightTime
            Next i
            Return trajectory(trajectory.Count - 1).GetPointByCurrentFlightTime(currentFlightTime - time, projection)
        End Function
        Public Function GetAirPath() As CoordPointCollection
            Dim result As New CoordPointCollection()
            For Each trajectoryPart As TrajectoryPart In trajectory
                result.Add(trajectoryPart.StartPoint)
            Next trajectoryPart
            If trajectory.Count > 0 Then
                result.Add(trajectory(trajectory.Count - 1).EndPoint)
            End If
            Return result
        End Function
        Public Function GetCourseByCurrentFlightTime(ByVal currentFlightTime As Double) As Double
            Dim time As Double = 0.0
            For i As Integer = 0 To trajectory.Count - 2
                If trajectory(i).FlightTime > currentFlightTime - time Then
                    Return trajectory(i).Course
                End If
                time += trajectory(i).FlightTime
            Next i
            Return trajectory(trajectory.Count - 1).Course
        End Function
        Public Sub UpdateTrajectory(ByVal points As List(Of CoordPoint), ByVal speedInKmH As Double)
            trajectory.Clear()
            For i As Integer = 0 To points.Count - 2
                trajectory.Add(New TrajectoryPart(projection, CType(points(i), GeoPoint), CType(points(i + 1), GeoPoint), speedInKmH))
            Next i
        End Sub
    End Class

    Public Class PlaneInfo
        Inherits BindableBase


        Private position_Renamed As New GeoPoint(0,0)
        Public Sub New()
            CurrentFlightTime = 0
            Course = 0
        End Sub

        Public Property CurrentFlightTime() As Double
            Get
                Return GetProperty(Function() CurrentFlightTime)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() CurrentFlightTime, value, AddressOf CurrentFlightTimePropertyChanged)
            End Set
        End Property
        Public Property Position() As GeoPoint
            Get
                Return GetProperty(Function() position_Renamed)
            End Get
            Set(ByVal value As GeoPoint)
                SetProperty(Function() position_Renamed, value)
            End Set
        End Property
        Public Property Course() As Double
            Get
                Return GetProperty(Function() Course)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() Course, value)
            End Set
        End Property

        Private Sub CurrentFlightTimePropertyChanged()
            Me.UpdatePosition(CurrentFlightTime)
        End Sub
        Private Shared Function ConvertPlaneNameToFilePath(ByVal PlaneName As String) As String
            Dim result As String = PlaneName.Replace(" ", "")
            result = "../Images/Planes/" & result.Replace("-", "") & ".png"
            Return result
        End Function

        Private isLandedField As Boolean = False
        Private ReadOnly planeIDField As String
        Private ReadOnly nameField As String
        Private ReadOnly endPointNameField As String
        Private ReadOnly startPointNameField As String
        Private ReadOnly speedInKmHField As Double
        Private ReadOnly flightAltitudeField As Double
        Private ReadOnly imagePathField As String
        Private ReadOnly trajectoryField As PlaneTrajectory

        Public ReadOnly Property PlaneID() As String
            Get
                Return planeIDField
            End Get
        End Property
        Public ReadOnly Property Name() As String
            Get
                Return nameField
            End Get
        End Property
        Public ReadOnly Property EndPointName() As String
            Get
                Return endPointNameField
            End Get
        End Property
        Public ReadOnly Property StartPointName() As String
            Get
                Return startPointNameField
            End Get
        End Property
        Public ReadOnly Property SpeedKmH() As Double
            Get
                Return If(isLandedField, 0.0, speedInKmHField)
            End Get
        End Property
        Public ReadOnly Property FlightAltitude() As Double
            Get
                Return If(isLandedField, 0.0, flightAltitudeField)
            End Get
        End Property
        Public ReadOnly Property ImagePath() As String
            Get
                Return imagePathField
            End Get
        End Property
        Public ReadOnly Property IsLanded() As Boolean
            Get
                Return isLandedField
            End Get
        End Property
        Public ReadOnly Property TotalFlightTime() As Double
            Get
                Return trajectoryField.FlightTime
            End Get
        End Property

        Public Sub New(ByVal name As String, ByVal id As String, ByVal endPointName As String, ByVal startPointName As String, ByVal speedInKmH As Double, ByVal flightAltitude As Double, ByVal points As List(Of GeoPoint))
            Me.nameField = name
            Me.planeIDField = id
            Me.endPointNameField = endPointName
            Me.startPointNameField = startPointName
            Me.speedInKmHField = speedInKmH
            Me.flightAltitudeField = flightAltitude
            imagePathField = ConvertPlaneNameToFilePath(name)
            trajectoryField = New PlaneTrajectory(points, speedInKmH)
            UpdatePosition(CurrentFlightTime)
        End Sub
        Private Sub UpdatePosition(ByVal flightTime As Double)
            Dim pos As GeoPoint = trajectoryField.GetPointByCurrentFlightTime(flightTime)

            Dim course_Renamed As Double = trajectoryField.GetCourseByCurrentFlightTime(flightTime)
            isLandedField = flightTime >= trajectoryField.FlightTime
            Position = pos
            Course = course_Renamed
        End Sub
        Public Function GetAirPath(ByVal airportTemplate As DataTemplate) As List(Of MapItem)
            Dim mapItemList As New List(Of MapItem)()
            Dim polyline As New MapPolyline() With {.Points = trajectoryField.GetAirPath(), .Fill = New SolidColorBrush(Colors.Transparent), .Stroke = New SolidColorBrush(Color.FromArgb(127, 255, 0, 199)), .StrokeStyle = New StrokeStyle() With {.Thickness = 4}, .IsGeodesic = True, .Tag = Me}
            trajectoryField.UpdateTrajectory(polyline.ActualPoints.ToList(), SpeedKmH)
            mapItemList.Add(polyline)
            mapItemList.Add(New MapCustomElement() With {.Location = trajectoryField.StartPoint, .ContentTemplate = airportTemplate, .Tag = Me})
            mapItemList.Add(New MapCustomElement() With {.Location = trajectoryField.EndPoint, .ContentTemplate = airportTemplate, .Tag = Me})
            Return mapItemList
        End Function
    End Class

    Public Class FlightMapDataGenerator
        Inherits BindableBase
        Implements IDisposable


        Private planes_Renamed As New ObservableCollection(Of MapCustomElement)()

        Private actualAirPaths_Renamed As New ObservableCollection(Of MapItem)()

        Private selectedPlaneInfo_Renamed As New PlaneInfo()
        Public Property Planes() As ObservableCollection(Of MapCustomElement)
            Get
                Return GetProperty(Function() planes_Renamed)
            End Get
            Set(ByVal value As ObservableCollection(Of MapCustomElement))
                SetProperty(Function() planes_Renamed, value)
            End Set
        End Property
        Public Property ActualAirPaths() As ObservableCollection(Of MapItem)
            Get
                Return GetProperty(Function() actualAirPaths_Renamed)
            End Get
            Set(ByVal value As ObservableCollection(Of MapItem))
                SetProperty(Function() actualAirPaths_Renamed, value)
            End Set
        End Property
        Public Property SelectedPlaneInfo() As PlaneInfo
            Get
                Return GetProperty(Function() selectedPlaneInfo_Renamed)
            End Get
            Set(ByVal value As PlaneInfo)
                SetProperty(Function() selectedPlaneInfo_Renamed, value, AddressOf SelectedPlaneProperyChanged)
            End Set
        End Property
        Public Property SpeedScale() As Double
            Get
                Return GetProperty(Function() SpeedScale)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() SpeedScale, value)
            End Set
        End Property

        Private Sub SelectedPlaneProperyChanged()
            Me.UpdatePlaneInfo()
        End Sub

        Private Const mSecPerHour As Double = 3600000

        Private ReadOnly timer As New DispatcherTimer()
        Private ReadOnly airportTemplate As DataTemplate
        Private ReadOnly planesInfo As New List(Of PlaneInfo)()
        Private ReadOnly infoPanel As PlaneInfoPanel
        Private lastTime As Date

        Public Sub New(ByVal airportTemplate As DataTemplate, ByVal planeTemplate As DataTemplate, ByVal infoPanel As PlaneInfoPanel)
            Planes = New ObservableCollection(Of MapCustomElement)()
            ActualAirPaths = New ObservableCollection(Of MapItem)()
            Me.airportTemplate = airportTemplate
            Me.infoPanel = infoPanel
            LoadFromXML(planeTemplate)
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Interval = New TimeSpan(0, 0, 2)
            lastTime = Date.Now
            timer.Start()
            If Planes IsNot Nothing Then
                SelectedPlaneInfo = TryCast(Planes(1).Content, PlaneInfo)
            End If
        End Sub
        Private Sub LoadFromXML(ByVal planeTemplate As DataTemplate)
            Dim document As XDocument = DataLoader.LoadXmlFromResources("/Data/FlightMap.xml")
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("Planes").Elements()
                    Dim points As New List(Of GeoPoint)()
                    For Each infoElement As XElement In element.Element("Path").Elements()
                        Dim geoPoint As New GeoPoint(Convert.ToDouble(infoElement.Element("Latitude").Value, CultureInfo.InvariantCulture), Convert.ToDouble(infoElement.Element("Longitude").Value, CultureInfo.InvariantCulture))
                        points.Add(geoPoint)
                    Next infoElement
                    Dim info As New PlaneInfo(element.Element("PlaneName").Value, element.Element("PlaneID").Value, element.Element("EndPointName").Value, element.Element("StartPointName").Value, Convert.ToInt32(element.Element("Speed").Value), Convert.ToInt32(element.Element("Altitude").Value), points)
                    info.CurrentFlightTime = Convert.ToDouble(element.Element("CurrentFlightTime").Value, CultureInfo.InvariantCulture)
                    planesInfo.Add(info)
                Next element
            End If
            For Each info As PlaneInfo In planesInfo
                Dim mapCustomElement As New MapCustomElement() With {.Content = info, .Tag = info, .ContentTemplate = planeTemplate}
                BindingOperations.SetBinding(mapCustomElement, MapCustomElement.LocationProperty, New Binding("Position") With {.Source = info})
                Planes.Add(mapCustomElement)
                AddPaths(info)
            Next info
        End Sub
        Private Sub AddPaths(ByVal planeInfo As PlaneInfo)
            If planeInfo IsNot Nothing Then
                For Each item As MapItem In planeInfo.GetAirPath(airportTemplate)
                    BindingOperations.SetBinding(item, MapItem.VisibleProperty, New Binding("SelectedPlaneInfo") With {.Source = Me, .Converter = New PlaneInfoToPathVisibilityConverter(), .ConverterParameter = item.Tag})
                    ActualAirPaths.Add(item)
                Next item
            End If
        End Sub
        Private Sub UpdatePlaneInfo()
            infoPanel.Visible = SelectedPlaneInfo IsNot Nothing
        End Sub
        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            Dim currentTime As Date = Date.Now
            Dim interval As TimeSpan = currentTime.Subtract(lastTime)
            For Each info As PlaneInfo In planesInfo
                If Not info.IsLanded Then
                    info.CurrentFlightTime += SpeedScale * interval.TotalMilliseconds / mSecPerHour
                End If
            Next info
            lastTime = currentTime
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf OnTimedEvent
        End Sub
    End Class
End Namespace
