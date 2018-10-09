Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Map
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Threading

Namespace MapDemo

    Partial Public Class Clustering
        Inherits MapDemoModule

        Private viewModel As ClusteringViewModel

        Public Sub New()
            InitializeComponent()
            Me.viewModel = ViewModelSource.Create(Function() New ClusteringViewModel(TryCast(FindResource("clusterTemplate"), DataTemplate)))
            DataContext = Me.viewModel
        End Sub

        Private Sub Map_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim objects() As Object = Map.CalcHitInfo(e.GetPosition(Map)).HitObjects
            If objects.Length > 0 Then
               Map.ZoomToFit(DirectCast(objects(0), MapItem).ClusteredItems)
            End If
        End Sub
        Private Sub MapDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.viewModel.IsLoaded = True
        End Sub
    End Class

    <POCOViewModel>
    Public Class ClusteringViewModel
        Private ReadOnly Shared AttributeName As String = "location"

        Private ReadOnly clusterers_Renamed As IList(Of ClustererInfo) = New ObservableCollection(Of ClustererInfo) From {ViewModelSource.Create(Function() New ClustererInfo() With {.Name = "Marker clusterer", .Clusterer = New MarkerClusterer()}), ViewModelSource.Create(Function() New ClustererInfo() With {.Name = "Distance-based clusterer", .Clusterer = New DistanceBasedClusterer()})}

        Private clusterTemplate As DataTemplate

        Private customSettings_Renamed As MapCustomElementSettings

        Private groupProvider_Renamed As AttributeGroupProvider

        Private ReadOnly Property Clusterer() As MapClusterer
            Get
                Return If(ClustererInfo IsNot Nothing, ClustererInfo.Clusterer, Nothing)
            End Get
        End Property
        Private ReadOnly Property CustomSettings() As MapCustomElementSettings
            Get
                If customSettings_Renamed Is Nothing Then
                    customSettings_Renamed = New MapCustomElementSettings() With {.ContentTemplate = Me.clusterTemplate}
                End If
                Return customSettings_Renamed
            End Get
        End Property
        Private ReadOnly Property GroupProvider() As AttributeGroupProvider
            Get
                If groupProvider_Renamed Is Nothing Then
                    groupProvider_Renamed = New AttributeGroupProvider() With {.AttributeName = AttributeName}
                End If
                Return groupProvider_Renamed
            End Get
        End Property

        Public Sub New(ByVal clusterTemplate As DataTemplate)
            Me.ClustererInfo = clusterers_Renamed(0)
            Me.StepInPixels = 50
            Me.clusterTemplate = clusterTemplate
            Me.UsingCustomTemplate = True
        End Sub

        Public Overridable ReadOnly Property Clusterers() As IList(Of ClustererInfo)
            Get
                Return clusterers_Renamed
            End Get
        End Property
        Public Overridable Property ClustererInfo() As ClustererInfo
        Public Overridable Property StepInPixels() As Double
        Public Overridable Property AttributeClusteringEnabled() As Boolean
        Public Overridable Property IsClustering() As Boolean
        Public Overridable Property IsLoaded() As Boolean
        Public Overridable Property UsingCustomTemplate() As Boolean
        Public Overridable Property LegendHeaderText() As String
        Public Overridable Property DataContext() As Object

        Protected Sub OnStepInPixelsChanged()
            If Clusterer IsNot Nothing Then
                Clusterer.StepInPixels = CInt((StepInPixels))
            End If
        End Sub
        Protected Sub OnClustererInfoChanged()
            Update()
        End Sub
        Protected Sub OnClustererInfoChanging()
            If Clusterer IsNot Nothing Then
                UnsubscribeEvents()
            End If
        End Sub
        Protected Sub OnAttributeClusteringEnabledChanged()
            Update()
        End Sub
        Protected Sub OnUsingCustomTemplateChanged()
            Update()
        End Sub

        Private Sub SubscribeEvents()
            AddHandler Clusterer.Clustered, AddressOf OnClustered
            AddHandler Clusterer.Clustering, AddressOf OnClustering
        End Sub
        Private Sub UnsubscribeEvents()
            RemoveHandler Clusterer.Clustered, AddressOf OnClustered
            RemoveHandler Clusterer.Clustering, AddressOf OnClustering
        End Sub
        Private Sub OnClustering(ByVal sender As Object, ByVal e As System.EventArgs)
            IsClustering = True
        End Sub
        Private Sub OnClustered(ByVal sender As Object, ByVal e As ClusteredEventArgs)
            Dispatcher.CurrentDispatcher.BeginInvoke(New Action(Sub() IsClustering = False), DispatcherPriority.Render, Nothing)
        End Sub
        Private Sub Update()
            If Clusterer Is Nothing Then
                Return
            End If
            SubscribeEvents()
            Clusterer.GroupProvider = If(AttributeClusteringEnabled, GroupProvider, Nothing)
            Clusterer.ClusterSettings = If(UsingCustomTemplate, CustomSettings, Nothing)
            LegendHeaderText = If(AttributeClusteringEnabled, "Tree location", "Tree density")
        End Sub
    End Class

    <POCOViewModel>
    Public Class ClustererInfo
        Public Overridable Property Name() As String
        Public Overridable Property Clusterer() As MapClusterer
    End Class
End Namespace
