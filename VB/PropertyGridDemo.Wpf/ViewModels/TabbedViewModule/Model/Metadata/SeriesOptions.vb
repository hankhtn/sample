Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace PropertyGridDemo.Metadata
    Public NotInheritable Class SeriesOptionsMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of SeriesOptions))
            builder.Property(Function(x) x.FillType).PropertyGridEditor("FillType")
            builder.Property(Function(x) x.BorderType).PropertyGridEditor("BorderType")
            builder.Property(Function(x) x.ShowLabel).PropertyGridEditor("ShowLabel")
            builder.Property(Function(x) x.Fill).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor")
            builder.Property(Function(x) x.Border).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor")
            builder.Property(Function(x) x.Mirror).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor")
            builder.Property(Function(x) x.Label).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor")
        End Sub
    End Class
    Public NotInheritable Class CommonSeriesOptionsMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of CommonSeriesOptions))
            builder.PropertyGridEditor("Options")
            builder.Property(Function(x) x.Model).PropertyGridEditor("Options.Model")
        End Sub
    End Class
End Namespace
