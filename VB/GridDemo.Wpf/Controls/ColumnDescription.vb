Namespace GridDemo
    Public Class ColumnDescription
        Public Sub New(ByVal propertyName As String, Optional ByVal displayName As String = Nothing)
            Me.PropertyName = propertyName
            Me.DisplayName = If(displayName, propertyName)
        End Sub
        Private privatePropertyName As String
        Public Property PropertyName() As String
            Get
                Return privatePropertyName
            End Get
            Private Set(ByVal value As String)
                privatePropertyName = value
            End Set
        End Property
        Private privateDisplayName As String
        Public Property DisplayName() As String
            Get
                Return privateDisplayName
            End Get
            Private Set(ByVal value As String)
                privateDisplayName = value
            End Set
        End Property
    End Class
    Public Class BandDescription
        Public Sub New(ByVal displayName As String, ByVal columns() As ColumnDescription)
            Me.DisplayName = displayName
            Me.Columns = columns
        End Sub
        Private privateDisplayName As String
        Public Property DisplayName() As String
            Get
                Return privateDisplayName
            End Get
            Private Set(ByVal value As String)
                privateDisplayName = value
            End Set
        End Property
        Private privateColumns As ColumnDescription()
        Public Property Columns() As ColumnDescription()
            Get
                Return privateColumns
            End Get
            Private Set(ByVal value As ColumnDescription())
                privateColumns = value
            End Set
        End Property
    End Class
End Namespace
