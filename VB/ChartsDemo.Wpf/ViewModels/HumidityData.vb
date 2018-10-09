Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("HumidityData")>
    Public Class HumidityData
        Inherits List(Of HumidityInfo)


        Private Shared londonData_Renamed As List(Of HumidityInfo) = Nothing
        Public Shared ReadOnly Property LondonData() As List(Of HumidityInfo)
            Get
                If londonData_Renamed Is Nothing Then
                    Dim s = New XmlSerializer(GetType(HumidityData))
                    londonData_Renamed = DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/LondonHumidity.xml")), List(Of HumidityInfo))
                End If
                Return londonData_Renamed
            End Get
        End Property
    End Class
    Public Class HumidityInfo
        Public Property Timestamp() As Date
        Public Property Value() As Integer
    End Class
End Namespace
