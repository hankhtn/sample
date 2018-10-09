Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("WindRoseData")>
    Public Class WindRoseData
        Inherits List(Of WindInfo)


        Private Shared data_Renamed As List(Of WindInfo) = Nothing

        Public Shared ReadOnly Property Data() As List(Of WindInfo)
            Get
                If data_Renamed Is Nothing Then
                    Dim s = New XmlSerializer(GetType(WindRoseData))
                    data_Renamed = DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/WindRoseData.xml")), List(Of WindInfo))
                End If
                Return data_Renamed
            End Get
        End Property
    End Class
    Public Class WindInfo
        Public Property Direction() As String
        Public Property Speed() As Integer
    End Class
End Namespace
