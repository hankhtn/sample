Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace MapDemo
    Partial Public Class Navigator
        Inherits MapDemoModule


        Private routeModel_Renamed As RouteModel

        Private driveMarkerTemplate_Renamed As DataTemplate

        Private driveBrush_Renamed As SolidColorBrush

        Private defaultBrush_Renamed As SolidColorBrush

        Public ReadOnly Property RouteLayer() As InformationLayer
            Get
                Return routeInformationLayer
            End Get
        End Property
        Public ReadOnly Property RouteProvider() As BingRouteDataProvider
            Get
                Return Me.routeDataProvider
            End Get
        End Property
        Public ReadOnly Property DriveMarkerTemplate() As DataTemplate
            Get
                Return Me.driveMarkerTemplate_Renamed
            End Get
        End Property
        Public ReadOnly Property DriveBrush() As Brush
            Get
                Return driveBrush_Renamed
            End Get
        End Property
        Public ReadOnly Property DefaultBrush() As Brush
            Get
                Return defaultBrush_Renamed
            End Get
        End Property
        Public ReadOnly Property RouteModel() As RouteModel
            Get
                Return Me.routeModel_Renamed
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            driveBrush_Renamed = New SolidColorBrush(Color.FromArgb(&HFF, &HFE, &H72, &HFF))
            defaultBrush_Renamed = New SolidColorBrush(Color.FromArgb(&HFF, &H8A, &HFB, &HFF))
            driveMarkerTemplate_Renamed = CType(Me.Resources("DrivePushpinMarker"), DataTemplate)
            routeModel_Renamed = ViewModelSource.Create(Function() New RouteModel(Me))
            DataContext = routeModel_Renamed
        End Sub

        Private Sub PushpinMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim pushpin As MapPushpin = TryCast(sender, MapPushpin)
            If (pushpin IsNot Nothing) AndAlso (pushpin.State = MapPushpinState.Normal) Then
                Dim locationInformation As LocationInformation = TryCast(pushpin.Information, LocationInformation)
                routeModel_Renamed.AddWaypoint(If(locationInformation Is Nothing, String.Empty, locationInformation.DisplayName), CType(pushpin.Location, GeoPoint))
                searchInformationLayer.ClearResults()
                geocodeInformationLayer.ClearResults()
            End If
            e.Handled = True
        End Sub
        Private Sub GeoCodeAndSearchLayerItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            Dim pushpinsCount As Integer = 0
            For Each item As MapItem In args.Items
                Dim pushpin As MapPushpin = TryCast(item, MapPushpin)
                If pushpin IsNot Nothing Then
                    AddHandler pushpin.MouseLeftButtonDown, AddressOf PushpinMouseLeftButtonDown
                    pushpinsCount += 1
                End If
            Next item
            Me.RouteModel.RaiseSearchPushpinsChanged(pushpinsCount)
        End Sub
        Private Sub RouteItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            If (args.Error Is Nothing) AndAlso (Not args.Cancelled) Then
                routeModel_Renamed.ProcessRouteItems(args.Items)
            End If
        End Sub
        Private Sub RouteCalculated(ByVal sender As Object, ByVal e As BingRouteCalculatedEventArgs)
            If e.CalculationResult IsNot Nothing AndAlso e.CalculationResult.RouteResults.Count > 0 Then
                routeModel_Renamed.ProcessRouteResult(e.CalculationResult.RouteResults(0))
            Else
                routeModel_Renamed.ProcessRouteResult(Nothing)
            End If
        End Sub
        Private Sub DriveClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            e.Handled = True
            routeModel_Renamed.State = If(routeModel_Renamed.State = RouteModelState.Normal, RouteModelState.Drive, RouteModelState.Normal)
        End Sub
        Private Sub ClearClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            e.Handled = True
            routeModel_Renamed.Clear()
            routeInformationLayer.ClearResults()
            searchInformationLayer.ClearResults()
            geocodeInformationLayer.ClearResults()
        End Sub
    End Class
End Namespace
