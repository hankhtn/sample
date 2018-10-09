Imports System.Collections.Generic
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("Countries")>
    Public Class CountryData
        Inherits List(Of Country)


        Private Shared data_Renamed As List(Of Country) = Nothing

        Public Shared ReadOnly Property Data() As List(Of Country)
            Get
                If data_Renamed Is Nothing Then
                    Dim serializer As New XmlSerializer(GetType(CountryData))
                    data_Renamed = DirectCast(serializer.Deserialize(DataLoader.LoadFromResources("/Data/Countries.xml")), List(Of Country))
                    data_Renamed.Sort(AddressOf CompareCoutriesByArea)
                End If
                Return data_Renamed
            End Get
        End Property

        Private Shared Function CompareCoutriesByArea(ByVal country1 As Country, ByVal country2 As Country) As Integer
            Return country2.Area.CompareTo(country1.Area)
        End Function
    End Class

    Public Class Country
        Public Property Area() As Double
        Public Property Name() As String
        Public Property OfficialName() As String
    End Class
End Namespace
