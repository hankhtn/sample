Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Xml.Serialization

Namespace MapDemo
    Public Class EarthquakeViewModel
        <XmlElement("yr")>
        Public Property Year() As Integer
        <XmlElement("mon")>
        Public Property Month() As Integer
        <XmlElement("day")>
        Public Property Day() As Integer
        <XmlElement("hr")>
        Public Property Hour() As Integer
        <XmlElement("min")>
        Public Property Minute() As Integer
        <XmlElement("sec")>
        Public Property Second() As Double
        <XmlElement("glat")>
        Public Property Latitude() As Double
        <XmlElement("glon")>
        Public Property Longitude() As Double
        <XmlElement("dep")>
        Public Property Depth() As Double
        <XmlElement("mag")>
        Public Property Magnitude() As Double
    End Class
    <XmlRoot("Data")>
    Public Class EarthquakesData

        Private Shared instance_Renamed As EarthquakesData

        Public Shared ReadOnly Property Instance() As EarthquakesData
            Get
                If instance_Renamed IsNot Nothing Then
                    Return instance_Renamed
                Else
                    instance_Renamed = CreateInstance()
                    Return instance_Renamed
                End If
            End Get
        End Property

        Shared Function CreateInstance() As EarthquakesData
            Dim serializer As New XmlSerializer(GetType(EarthquakesData))
            Dim documentStream As Stream = DataLoader.LoadStreamFromResources("/Data/Earthquakes.xml")
            Return DirectCast(serializer.Deserialize(documentStream), EarthquakesData)
        End Function

        <XmlElement("Row")>
        Public Property Items() As List(Of EarthquakeViewModel)

        Private Sub New()
        End Sub
    End Class
End Namespace
