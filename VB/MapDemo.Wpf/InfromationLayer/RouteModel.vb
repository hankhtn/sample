Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media.Effects
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Map
Imports DevExpress.Map

Namespace MapDemo
    Public Enum RouteModelState
        Normal
        Drive
    End Enum

    <POCOViewModel>
    Public Class RouteModel
        Inherits ViewModelBase


        Private helpers_Renamed As ObservableCollection(Of MapItem)

        Private waypoints_Renamed As List(Of RouteWaypoint)

        Private routePath_Renamed As List(Of GeoPoint)
        Private waypointIndex As Integer
        Private searchPushpinsCount As Integer

        Public ReadOnly Property Helpers() As ObservableCollection(Of MapItem)
            Get
                Return Me.helpers_Renamed
            End Get
        End Property
        Public Overridable Property IsCalculating() As Boolean
        Public Overridable Property State() As RouteModelState
        Public Overridable Property DriveModel() As DriveModel
        Public Overridable Property Navigator() As Navigator
        Public ReadOnly Property RoutePath() As List(Of GeoPoint)
            Get
                Return routePath_Renamed
            End Get
        End Property
        Public Overridable Property DrivePathDistance() As Double
        Public Overridable Property ItineraryItems() As List(Of BingItineraryItem)
        Public Overridable Property RoutePushpins() As List(Of MapPushpin)
        Public ReadOnly Property ActionText() As String
            Get
                If State = RouteModelState.Drive AndAlso DriveModel IsNot Nothing Then
                    Return DriveModel.ActionText
                Else
                    If waypoints_Renamed.Count = 0 Then
                        If searchPushpinsCount > 0 Then
                            Return "Click the  pushpin to set a start point."
                        Else
                            Return "Click the map or use Search to find a location."
                        End If
                    ElseIf waypoints_Renamed.Count = 1 Then
                        Return "Set a finish point to calculate a route."
                    Else
                        Return "Set another finish point or click Drive."
                    End If
                End If
            End Get
        End Property
        Public Overridable Property MapCenter() As CoordPoint
        Public Overridable Property TimeScale() As Double
        Public ReadOnly Property Waypoints() As List(Of RouteWaypoint)
            Get
                Return Me.waypoints_Renamed
            End Get
        End Property

        Public Sub New(ByVal navigator As Navigator)
            TimeScale = 1
            MapCenter = New GeoPoint(34.158506, -118.255629)
            Me.Navigator = navigator
            Me.helpers_Renamed = New ObservableCollection(Of MapItem)()
            waypoints_Renamed = New List(Of RouteWaypoint)()
            RoutePushpins = New List(Of MapPushpin)()
            ItineraryItems = New List(Of BingItineraryItem)()
        End Sub

        Private Sub SendRouteRequest()
            IsCalculating = True
            If Waypoints.Count > 1 Then
                Me.Navigator.RouteProvider.CalculateRoute(Waypoints)
            End If
        End Sub
        Private Function NextWaypointLetter() As String
            Dim letter As String = "" & ChrW(CByte(AscW("A"c)) + waypointIndex Mod 26)
            waypointIndex += 1
            Return letter
        End Function
        Private Sub ExtractItineraryItems(ByVal result As BingRouteResult)
            ItineraryItems.Clear()
            For Each leg As BingRouteLeg In result.Legs
                For Each item As BingItineraryItem In leg.Itinerary
                    ItineraryItems.Add(item)
                Next item
            Next leg
        End Sub
        Private Sub BeginDrive()
            If (RoutePath IsNot Nothing) AndAlso (RoutePath.Count > 1) Then
                StopDrive()
                Me.DriveModel = New DriveModel(Me)
                AddHandler Me.DriveModel.CurrentLocationChanged, Sub(s, e) MapCenter = Me.DriveModel.CurrentLocation
            End If
        End Sub
        Private Sub StopDrive()
            If DriveModel IsNot Nothing Then
                Me.DriveModel.Cleanup()
                Me.DriveModel = Nothing
                MapCenter = CType(MapCenter, GeoPoint)
            End If
        End Sub
        Protected Sub OnStateChanged()
            Select Case State
                Case RouteModelState.Drive
                    BeginDrive()
                Case RouteModelState.Normal
                    StopDrive()
            End Select
            RaisePropertyChanged("ActionText")
        End Sub
        Private Sub CalculatePathDistance()
            DrivePathDistance = 0
            If routePath_Renamed IsNot Nothing Then
                Dim lastPoint? As MapUnit = Nothing
                For Each node As GeoPoint In routePath_Renamed
                    If lastPoint IsNot Nothing Then
                        Dim currentPoint As MapUnit = Me.Navigator.RouteLayer.GeoPointToMapUnit(node)
                        DrivePathDistance += DriveModel.DistanceBetweenPoints(currentPoint, lastPoint.Value)
                        lastPoint = currentPoint
                    Else
                        lastPoint = Me.Navigator.RouteLayer.GeoPointToMapUnit(node)
                    End If
                Next node
            End If
        End Sub

        Friend Sub RaiseDriveModelChanged()
            RaisePropertyChanged("ActionText")
        End Sub

        Friend Sub RaiseSearchPushpinsChanged(ByVal count As Integer)
            searchPushpinsCount = count
            RaisePropertyChanged("ActionText")
        End Sub

        Public Sub AddWaypoint(ByVal description As String, ByVal location As GeoPoint)
            Dim waypoint As New RouteWaypoint(description, location)
            If Not waypoints_Renamed.Contains(waypoint) Then
                Dim pushpin As New MapPushpin()
                pushpin.Location = location
                pushpin.Information = description
                pushpin.Text = NextWaypointLetter()
                pushpin.TraceDepth = 0
                pushpin.State = MapPushpinState.Busy
                Helpers.Add(pushpin)
                waypoints_Renamed.Add(waypoint)
                SendRouteRequest()
            End If
            RaisePropertyChanged("ActionText")
            RaisePropertyChanged("Waypoints")
        End Sub
        Public Sub ProcessRouteItems(ByVal items() As MapItem)
            waypointIndex = 0
            RoutePushpins.Clear()
            For Each item As MapItem In items
                Dim pushpin As MapPushpin = TryCast(item, MapPushpin)
                If pushpin IsNot Nothing Then
                    pushpin.Text = NextWaypointLetter()
                    RoutePushpins.Add(pushpin)
                End If
                Dim polyline As MapPolyline = TryCast(item, MapPolyline)
                If polyline IsNot Nothing Then
                    polyline.Effect = New DropShadowEffect() With {.Direction = -90, .ShadowDepth = 1}
                End If
            Next item
            Helpers.Clear()
        End Sub
        Public Sub ProcessRouteResult(ByVal result As BingRouteResult)
            If result IsNot Nothing Then
                ExtractItineraryItems(result)
                routePath_Renamed = result.RoutePath
            Else
                routePath_Renamed = Nothing
            End If
            CalculatePathDistance()
        End Sub
        Public Sub Clear()
            If RoutePath IsNot Nothing Then
                RoutePath.Clear()
            End If
            Helpers.Clear()
            RoutePushpins.Clear()
            ItineraryItems.Clear()
            DrivePathDistance = 0.0
            waypoints_Renamed.Clear()
            waypointIndex = 0
            RaiseSearchPushpinsChanged(0)
            RaisePropertyChanged("Waypoints")
        End Sub
    End Class

    Public Class RouteModelStateToButtonTextConverter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If (value IsNot Nothing) AndAlso (value.GetType() Is GetType(RouteModelState)) Then
                Dim state As RouteModelState = DirectCast(value, RouteModelState)
                Select Case state
                    Case RouteModelState.Drive
                        Return "Stop"
                    Case RouteModelState.Normal
                        Return "Drive"
                End Select
            End If
            Return String.Empty
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        #End Region
    End Class

    Public Class RouteModelNormalStateToBoolConverter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If (value IsNot Nothing) AndAlso (value.GetType() Is GetType(RouteModelState)) Then
                Return (DirectCast(value, RouteModelState)) = RouteModelState.Normal
            End If
            Return False
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        #End Region
    End Class

    Public Class WaypointsCountToDriveAbilityConverter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If (value IsNot Nothing) AndAlso (value.GetType() Is GetType(Integer)) Then
                Return (DirectCast(value, Integer)) > 1
            End If
            Return False
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        #End Region
    End Class
End Namespace
