Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.Map

Namespace ProductsDemo
    Public Class PlaceInfo

        Private ReadOnly name_Renamed As String

        Private ReadOnly cityName_Renamed As String

        Private ReadOnly location_Renamed As GeoPoint

        Private ReadOnly description_Renamed As String

        Private ReadOnly imageSource_Renamed As BitmapImage

        Public ReadOnly Property Name() As String
            Get
                Return name_Renamed
            End Get
        End Property
        Public ReadOnly Property CityName() As String
            Get
                Return cityName_Renamed
            End Get
        End Property
        Public ReadOnly Property Location() As GeoPoint
            Get
                Return location_Renamed
            End Get
        End Property
        Public ReadOnly Property Description() As String
            Get
                Return description_Renamed
            End Get
        End Property
        Public ReadOnly Property ImageSource() As BitmapImage
            Get
                Return imageSource_Renamed
            End Get
        End Property

        Public Sub New(ByVal name As String, ByVal cityName As String, ByVal location As GeoPoint, ByVal description As String, ByVal imageSource As BitmapImage)
            Me.name_Renamed = name
            Me.cityName_Renamed = cityName
            Me.location_Renamed = location
            Me.description_Renamed = description
            Me.imageSource_Renamed = imageSource
        End Sub
    End Class
End Namespace
