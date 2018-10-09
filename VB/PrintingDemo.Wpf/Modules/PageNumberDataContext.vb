Imports System.Windows
Imports DevExpress.Xpf.Printing

Namespace PrintingDemo
    Public Class PageNumberDataContext

        Private kind_Renamed As PageNumberKind

        Private format_Renamed As String

        Private horizontalContentAlignment_Renamed As HorizontalAlignment

        Public ReadOnly Property Kind() As PageNumberKind
            Get
                Return kind_Renamed
            End Get
        End Property
        Public ReadOnly Property Format() As String
            Get
                Return format_Renamed
            End Get
        End Property
        Public ReadOnly Property HorizontalContentAlignment() As HorizontalAlignment
            Get
                Return horizontalContentAlignment_Renamed
            End Get
        End Property

        Public Sub New(ByVal kind As PageNumberKind, ByVal format As String, ByVal horizontalContentAlignment As HorizontalAlignment)
            Me.kind_Renamed = kind
            Me.format_Renamed = format
            Me.horizontalContentAlignment_Renamed = horizontalContentAlignment
        End Sub
    End Class
End Namespace
