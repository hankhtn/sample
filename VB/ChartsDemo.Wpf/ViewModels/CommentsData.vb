Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("Comments")>
    Public Class CommentsData
        Inherits List(Of CommentsInfo)


        Private Shared dataSource_Renamed As List(Of CommentsInfo) = Nothing
        Public Shared ReadOnly Property DataSource() As List(Of CommentsInfo)
            Get
                If dataSource_Renamed Is Nothing Then
                    Dim s = New XmlSerializer(GetType(CommentsData))
                    dataSource_Renamed = DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/CommentsData.xml")), List(Of CommentsInfo))
                End If
                Return dataSource_Renamed
            End Get
        End Property
    End Class
    Public Class CommentsInfo
        Public Property Category() As String
        Public Property Items() As List(Of CommentInfo)
    End Class
    Public Class CommentInfo
        Public Property [Date]() As Date
        Public Property CommentCount() As Integer
    End Class
End Namespace
