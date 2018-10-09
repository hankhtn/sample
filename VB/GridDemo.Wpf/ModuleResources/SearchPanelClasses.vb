Imports DevExpress.Xpf.Grid
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace GridDemo
    Public Class ListColumn
        Public Sub New(ByVal column As ColumnBase)
            Me.Column = column
            HeaderCaption = column.HeaderCaption.ToString()
        End Sub
        Private privateColumn As ColumnBase
        Public Property Column() As ColumnBase
            Get
                Return privateColumn
            End Get
            Private Set(ByVal value As ColumnBase)
                privateColumn = value
            End Set
        End Property
        Private privateHeaderCaption As String
        Public Property HeaderCaption() As String
            Get
                Return privateHeaderCaption
            End Get
            Private Set(ByVal value As String)
                privateHeaderCaption = value
            End Set
        End Property
        Public Shared Function CreateCollection(ByVal gridColumns As GridColumnCollection) As IList(Of ListColumn)
            Dim collection = New ObservableCollection(Of ListColumn)(gridColumns.Select(Function(col) New ListColumn(col)))
            Return New ReadOnlyObservableCollection(Of ListColumn)(collection)
        End Function
    End Class
End Namespace
