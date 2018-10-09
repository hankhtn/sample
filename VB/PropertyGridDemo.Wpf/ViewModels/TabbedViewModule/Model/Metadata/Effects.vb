Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace PropertyGridDemo.Metadata
    Public NotInheritable Class MirrorOptionsMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of MirrorOptions))
            builder.Property(Function(x) x.Show).PropertyGridEditor("MirrorOptions.Show")
            builder.Property(Function(x) x.MirrorLength).PropertyGridEditor("MirrorOptions.MirrorLength").LocatedAt(0)
        End Sub
    End Class
    Public NotInheritable Class LabelOptionsMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of LabelOptions))
            builder.Property(Function(x) x.ShowLabel).PropertyGridEditor("LabelOptions.ShowLabel").LocatedAt(0)
        End Sub
    End Class
    Public NotInheritable Class VisibleLabelOptionsMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of VisibleLabelOptions))
            builder.Property(Function(x) x.Position).PropertyGridEditor("VisibleLabelOptions.Position")
        End Sub
    End Class
End Namespace
