Imports DevExpress.Xpf.Charts
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Windows

Namespace ChartsDemo
    Public NotInheritable Class DemoValuesProvider

        Private Sub New()
        End Sub
        #Region "Models"


        Private Shared ReadOnly predefinedMarker2DModels_Renamed() As Marker2DModel = CreateModels(Of Marker2DModel)(Marker2DModel.GetPredefinedKinds())

        Private Shared ReadOnly predefinedBar2DModels_Renamed() As Bar2DModel = CreateModels(Of Bar2DModel)(Bar2DModel.GetPredefinedKinds())

        Private Shared ReadOnly predefinedCandleStick2DModels_Renamed() As CandleStick2DModel = CreateModels(Of CandleStick2DModel)(CandleStick2DModel.GetPredefinedKinds())

        Private Shared ReadOnly predefinedStock2DModels_Renamed() As Stock2DModel = CreateModels(Of Stock2DModel)(Stock2DModel.GetPredefinedKinds())

        Private Shared ReadOnly predefinedPie2DModels_Renamed() As Pie2DModel = CreateModels(Of Pie2DModel)(Pie2DModel.GetPredefinedKinds())
        Private Shared ReadOnly predefinedRangeBarModels() As RangeBar2DModel = CreateModels(Of RangeBar2DModel)(RangeBar2DModel.GetPredefinedKinds())

        Public Shared ReadOnly Property PredefinedMarker2DModels() As IEnumerable(Of Marker2DModel)
            Get
                Return predefinedMarker2DModels_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedBar2DModels() As IEnumerable(Of Bar2DModel)
            Get
                Return predefinedBar2DModels_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedCandleStick2DModels() As IEnumerable(Of CandleStick2DModel)
            Get
                Return predefinedCandleStick2DModels_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedStock2DModels() As IEnumerable(Of Stock2DModel)
            Get
                Return predefinedStock2DModels_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedPie2DModels() As IEnumerable(Of Pie2DModel)
            Get
                Return predefinedPie2DModels_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedRangeBar2DModels() As IEnumerable(Of RangeBar2DModel)
            Get
                Return predefinedRangeBarModels
            End Get
        End Property
        Public Shared ReadOnly Iterator Property PredefinedBubble3DModels() As IEnumerable(Of Marker3DPointModel)
            Get
                Yield New Marker3DCubePointModel()
                Yield New Marker3DSpherePointModel()
            End Get
        End Property

        #End Region
        #Region "Models Instances"

        Public Shared ReadOnly Property PredefinedCrossMarker2DModel() As CrossMarker2DModel
            Get
                Return predefinedMarker2DModels_Renamed.OfType(Of CrossMarker2DModel)().First()
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedRingMarker2DModel() As RingMarker2DModel
            Get
                Return predefinedMarker2DModels_Renamed.OfType(Of RingMarker2DModel)().First()
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedCircleMarker2DModel() As CircleMarker2DModel
            Get
                Return predefinedMarker2DModels_Renamed.OfType(Of CircleMarker2DModel)().First()
            End Get
        End Property

        Public Shared ReadOnly Property PredefinedFlatGlassBar2DModel() As FlatGlassBar2DModel
            Get
                Return predefinedBar2DModels_Renamed.OfType(Of FlatGlassBar2DModel)().First()
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedSimpleBar2DModel() As SimpleBar2DModel
            Get
                Return predefinedBar2DModels_Renamed.OfType(Of SimpleBar2DModel)().First()
            End Get
        End Property

        Public Shared ReadOnly Property PredefinedOutsetRangeBar2DModel() As OutsetRangeBar2DModel
            Get
                Return predefinedRangeBarModels.OfType(Of OutsetRangeBar2DModel)().First()
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedThinStock2DModel() As ThinStock2DModel
            Get
                Return predefinedStock2DModels_Renamed.OfType(Of ThinStock2DModel)().First()
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedSimplePie2DModel() As SimplePie2DModel
            Get
                Return predefinedPie2DModels_Renamed.OfType(Of SimplePie2DModel)().First()
            End Get
        End Property
        Public Shared ReadOnly Property PredefinedSimpleCandleStick2DModel() As SimpleCandleStick2DModel
            Get
                Return predefinedCandleStick2DModels_Renamed.OfType(Of SimpleCandleStick2DModel)().First()
            End Get
        End Property

        #End Region

        Public Shared Function CreateModels(Of T As DependencyObject)(ByVal kinds As IEnumerable(Of PredefinedElementKind)) As T()
            Return kinds.Select(Function(x) (DirectCast(Activator.CreateInstance(x.Type), T))).ToArray()
        End Function
    End Class
    Public MustInherit Class Base3DModelsProvider(Of T As DependencyObject)
        Implements INotifyPropertyChanged


        Private predefined3DModels_Renamed() As T
        Public ReadOnly Property Predefined3DModels() As IEnumerable(Of T)
            Get
                If predefined3DModels_Renamed IsNot Nothing Then
                    Return predefined3DModels_Renamed
                Else
                    predefined3DModels_Renamed = DemoValuesProvider.CreateModels(Of T)(GetPredefinedKinds())
                    Return predefined3DModels_Renamed
                End If
            End Get
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Sub RaisePropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
        Protected MustOverride Function GetPredefinedKinds() As IEnumerable(Of PredefinedElementKind)
    End Class
    Public Class Pie3DModelsProvider
        Inherits Base3DModelsProvider(Of Pie3DModel)

        Public ReadOnly Property PredefinedPentagonPie3DModel() As PentagonPie3DModel
            Get
                Return Predefined3DModels.OfType(Of PentagonPie3DModel)().First()
            End Get
        End Property

        Protected Overrides Function GetPredefinedKinds() As IEnumerable(Of PredefinedElementKind)
            Return Pie3DModel.GetPredefinedKinds()
        End Function
    End Class
    Public Class Marker3DModelsProvider
        Inherits Base3DModelsProvider(Of Marker3DModel)

        Public ReadOnly Property PredefinedSphereMarker3DModel() As SphereMarker3DModel
            Get
                Return Predefined3DModels.OfType(Of SphereMarker3DModel)().First()
            End Get
        End Property

        Protected Overrides Function GetPredefinedKinds() As IEnumerable(Of PredefinedElementKind)
            Return Marker3DModel.GetPredefinedKinds()
        End Function
    End Class
    Public Class Bar3DModelsProvider
        Inherits Base3DModelsProvider(Of Bar3DModel)

        Public ReadOnly Property PredefinedBoxBar3DModel() As BoxBar3DModel
            Get
                Return Predefined3DModels.OfType(Of BoxBar3DModel)().First()
            End Get
        End Property
        Public ReadOnly Property PredefinedCylinderBar3DModel() As CylinderBar3DModel
            Get
                Return Predefined3DModels.OfType(Of CylinderBar3DModel)().First()
            End Get
        End Property

        Protected Overrides Function GetPredefinedKinds() As IEnumerable(Of PredefinedElementKind)
            Return Bar3DModel.GetPredefinedKinds()
        End Function
    End Class
End Namespace
