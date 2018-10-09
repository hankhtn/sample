Imports System.Collections.Generic
Imports DevExpress.Xpf.Map

Namespace ProductsDemo
    Public Class CityInfo

        Private ReadOnly name_Renamed As String

        Private ReadOnly location_Renamed As GeoPoint

        Private ReadOnly places_Renamed As New List(Of PlaceInfo)()

        Public ReadOnly Property Name() As String
            Get
                Return name_Renamed
            End Get
        End Property
        Public ReadOnly Property Location() As GeoPoint
            Get
                Return location_Renamed
            End Get
        End Property
        Public ReadOnly Property Places() As List(Of PlaceInfo)
            Get
                Return places_Renamed
            End Get
        End Property

        Public Sub New(ByVal name As String, ByVal location As GeoPoint)
            Me.name_Renamed = name
            Me.location_Renamed = location
        End Sub
    End Class
End Namespace
