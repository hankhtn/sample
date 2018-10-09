Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("RegionPopulationData")>
    Public Class RegionPopulationData
        Inherits List(Of RegionPopulation)


        Private Shared data_Renamed As List(Of RegionPopulation) = Nothing

        Public Shared ReadOnly Property Data() As List(Of RegionPopulation)
            Get
                If data_Renamed Is Nothing Then
                    Dim s = New XmlSerializer(GetType(RegionPopulationData))
                    data_Renamed = DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/RegionPopulationData.xml")), List(Of RegionPopulation))
                End If
                Return data_Renamed
            End Get
        End Property
    End Class
    Public Class RegionPopulation
        Public Property Region() As String
        Public Property Items() As List(Of PopulationByYear)
    End Class
    Public Class PopulationByYear
        Public Property Year() As Integer
        Public Property Population() As Integer
    End Class
End Namespace
