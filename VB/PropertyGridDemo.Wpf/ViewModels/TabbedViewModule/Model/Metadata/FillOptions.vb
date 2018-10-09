Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace PropertyGridDemo.Metadata
    Public NotInheritable Class FillOptionsMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of FillOptions))
            builder.Property(Function(x) x.Result).Hidden()
            builder.Property(Function(x) x.FillType).PropertyGridEditor("FillOptions.FillType").LocatedAt(0)
        End Sub
    End Class
    Public NotInheritable Class SolidFillOptionsMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of SolidFillOptions))
            builder.Property(Function(x) x.Color).PropertyGridEditor("CommonBorderAndFillOptions.Color")
            builder.Property(Function(x) x.Opacity).PropertyGridEditor("CommonBorderAndFillOptions.Opacity")
        End Sub
    End Class
    Public NotInheritable Class PictureFillOptionsMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of PictureFillOptions))
            builder.Property(Function(x) x.Picture).PropertyGridEditor("PictureFillOptions.Picture")
            builder.Property(Function(x) x.Opacity).PropertyGridEditor("CommonBorderAndFillOptions.Opacity")
        End Sub
    End Class
End Namespace
