Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("TemperatureData")>
    Public Class TemperatureData
        Inherits List(Of TemperaturesInfo)


        Private Shared londonData_Renamed As List(Of TemperaturesInfo) = Nothing
        Public Shared ReadOnly Property LondonData() As List(Of TemperaturesInfo)
            Get
                If londonData_Renamed Is Nothing Then
                    Dim s = New XmlSerializer(GetType(TemperatureData))
                    londonData_Renamed = DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/LondonTemperature.xml")), List(Of TemperaturesInfo))
                End If
                Return londonData_Renamed
            End Get
        End Property
    End Class
    Public Class TemperaturesInfo
        Public Property Description() As String
        Public Property Items() As List(Of TemperatureInfo)
    End Class
    Public Class TemperatureInfo
        Public Property [Date]() As Date
        Public Property Temperature() As Integer
    End Class
End Namespace
