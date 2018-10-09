Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Media
Imports System.Xml
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("G7GDPs")>
    Public Class G7Data
        Inherits List(Of GDP)


        Private Shared members_Renamed As List(Of G7Member)

        Private Shared data_Renamed As List(Of GDP)

        Public Shared ReadOnly Property Members() As List(Of G7Member)
            Get
                If members_Renamed IsNot Nothing Then
                    Return members_Renamed
                Else
                    members_Renamed = GetG7Data()
                    Return members_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property Data() As List(Of GDP)
            Get
                If data_Renamed IsNot Nothing Then
                    Return data_Renamed
                Else
                    data_Renamed = GetGDPData()
                    Return data_Renamed
                End If
            End Get
        End Property

        Private Shared Function GetG7Data() As List(Of G7Member)
            Return Data.GroupBy(Function(x) x.Country).Select(Function(x) New G7Member(x.Key, x.ToList())).ToList()
        End Function
        Private Shared Function GetGDPData() As List(Of GDP)
            Dim s = New XmlSerializer(GetType(G7Data))
            Return DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/GDPofG7.xml")), List(Of GDP))
        End Function
    End Class

    Public Class G7Member
        Public ReadOnly Property GDPin2015() As Decimal
            Get
                Return GDPs.First(Function(gdp) gdp.Year = 2015).Product
            End Get
        End Property
        Public Property CountryName() As String
        Private privateGDPs As List(Of GDP)
        Public Property GDPs() As List(Of GDP)
            Get
                Return privateGDPs
            End Get
            Private Set(ByVal value As List(Of GDP))
                privateGDPs = value
            End Set
        End Property

        Public Sub New(ByVal countryName As String, ByVal items As List(Of GDP))
            Me.CountryName = countryName
            Me.GDPs = items
        End Sub
    End Class

    Public Class GDP
        Public Property Country() As String
        Public Property Year() As Integer
        Public Property Product() As Decimal
    End Class
End Namespace
