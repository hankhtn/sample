Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Media

Namespace PropertyGridDemo.Metadata
    Public NotInheritable Class BorderOptionsDataMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of BorderOptionsData))

        End Sub
    End Class
    Public NotInheritable Class BorderOptionsMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of BorderOptions))
            builder.Property(Function(x) x.Data).Hidden()
            builder.Property(Function(x) x.BorderType).PropertyGridEditor("BorderOptions.BorderType").LocatedAt(0)
        End Sub
    End Class
    Public NotInheritable Class SolidBorderOptionsMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of SolidBorderOptions))
            builder.Property(Function(x) x.Color).PropertyGridEditor("CommonBorderAndFillOptions.Color")
            builder.Property(Function(x) x.Opacity).PropertyGridEditor("CommonBorderAndFillOptions.Opacity")
            builder.Property(Function(x) x.Thickness).PropertyGridEditor("SolidBorderOptions.Thickness")
            builder.Property(Function(x) x.DashStyle).PropertyGridEditor("SolidBorderOptions.DashStyle")
        End Sub
    End Class
End Namespace
