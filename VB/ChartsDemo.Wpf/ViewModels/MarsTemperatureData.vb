Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("MarsTemperatureData")>
    Public Class MarsTemperatureData
        Inherits List(Of MarsTemperature)


        Private Shared fullData_Renamed As List(Of MarsTemperature) = Nothing

        Private Shared data_Renamed As List(Of MarsTemperature) = Nothing

        Public Shared ReadOnly Property FullData() As List(Of MarsTemperature)
            Get
                If fullData_Renamed Is Nothing Then
                    Dim s = New XmlSerializer(GetType(MarsTemperatureData))
                    fullData_Renamed = DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/MarsTemperatureData.xml")), List(Of MarsTemperature))
                End If
                Return fullData_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property Data() As List(Of MarsTemperature)
            Get
                If data_Renamed IsNot Nothing Then
                    Return data_Renamed
                Else
                    data_Renamed = FullData.Take(31).ToList()
                    Return data_Renamed
                End If
            End Get
        End Property
    End Class
    Public Class MarsTemperature
        Public Property Sol() As Double
        Public Property Temperature() As Double
    End Class
End Namespace
