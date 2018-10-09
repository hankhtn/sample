Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace PropertyGridDemo
    <MetadataType(GetType(Metadata.BaseOptionsMetadata))>
    Public MustInherit Class BaseOptions
        Implements ISupportParentViewModel

        Public Sub New()
        End Sub
        Public Sub New(ByVal root As SeriesOptions)
            Me.root = root
        End Sub
        Private root As SeriesOptions
        Private Property ISupportParentViewModel_ParentViewModel() As Object Implements ISupportParentViewModel.ParentViewModel
            Get
                Return root
            End Get
            Set(ByVal value As Object)
                root = DirectCast(value, SeriesOptions)
            End Set
        End Property
    End Class
End Namespace
