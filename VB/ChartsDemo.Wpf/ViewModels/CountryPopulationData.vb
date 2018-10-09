Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("CountryPopulationData")>
    Public Class CountryPopulationData
        Inherits List(Of CountryPopulation)


        Private Shared data_Renamed As List(Of CountryPopulation) = Nothing

        Public Shared ReadOnly Property Data() As List(Of CountryPopulation)
            Get
                If data_Renamed Is Nothing Then
                    Dim s = New XmlSerializer(GetType(CountryPopulationData))
                    data_Renamed = DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/CountryPopulationData.xml")), List(Of CountryPopulation))
                End If
                Return data_Renamed
            End Get
        End Property
    End Class
    Public Class CountryPopulation
        Public Property Country() As String
        Public Property Population() As Integer
    End Class
End Namespace
