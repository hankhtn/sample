Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("WeatherData")>
    Public Class WeatherData
        Inherits List(Of WeatherInfo)


        Private Shared data_Renamed As List(Of WeatherInfo) = Nothing

        Public Shared ReadOnly Property Data() As List(Of WeatherInfo)
            Get
                If data_Renamed Is Nothing Then
                    Dim s = New XmlSerializer(GetType(WeatherData))
                    data_Renamed = DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/WeatherData.xml")), List(Of WeatherInfo))
                End If
                Return data_Renamed
            End Get
        End Property
    End Class
    Public Class WeatherInfo
        Public Property Timestamp() As Date
        Public Property Temperature() As Double
        Public Property Pressure() As Double
        Public Property RelativeHumidity() As Double
    End Class
End Namespace
