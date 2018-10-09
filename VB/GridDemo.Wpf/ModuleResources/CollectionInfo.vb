Imports System.Collections

Namespace GridDemo
    Public Class CollectionInfo
        Public Sub New(ByVal collection As IEnumerable, ByVal description As String)
            Me.Collection = collection
            Me.Description = description
        End Sub
        Private privateCollection As IEnumerable
        Public Property Collection() As IEnumerable
            Get
                Return privateCollection
            End Get
            Private Set(ByVal value As IEnumerable)
                privateCollection = value
            End Set
        End Property
        Private privateDescription As String
        Public Property Description() As String
            Get
                Return privateDescription
            End Get
            Private Set(ByVal value As String)
                privateDescription = value
            End Set
        End Property
    End Class
End Namespace
