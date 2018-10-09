Imports DevExpress.Mvvm

Namespace RichEditDemo
    Public Class DocumentPropertyInfo
        Inherits BindableBase

        Public Sub New(ByVal displayName As String, Optional ByVal name As String = Nothing)
            Me.DisplayName = displayName
            Me.Name = If(name, displayName.ToUpperInvariant())
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
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Private Set(ByVal value As String)
                privateName = value
            End Set
        End Property
    End Class
End Namespace
