Imports DevExpress.Diagram.Demos
Imports DevExpress.Mvvm

Namespace DiagramDemo
    Public Class DatabaseDiagramViewModel
        Inherits ViewModelBase

        Private privateDatabase As DatabaseDefinition
        Public Property Database() As DatabaseDefinition
            Get
                Return privateDatabase
            End Get
            Private Set(ByVal value As DatabaseDefinition)
                privateDatabase = value
            End Set
        End Property

        Public Sub New()
            Database = DatabaseData.GetDatabaseDefinition()
        End Sub
    End Class
End Namespace
