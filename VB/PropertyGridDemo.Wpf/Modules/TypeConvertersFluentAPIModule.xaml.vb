Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.PropertyGrid
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace PropertyGridDemo
    Partial Public Class TypeConvertersFluentAPIModule
        Inherits PropertyGridDemoModule

        Public Sub New()
            InitializeComponent()
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata(Of ProductDescriptorMetadata)()
            DataContext = New ProductDescriptor With {
                .Product = New Product With {.Name = "Macintosh"},
                .Tags = New String() { "Apple", "Fruit", "Round" }
            }
        End Sub
        Private Sub PropertyGridControl_OnCustomExpand(ByVal sender As Object, ByVal args As CustomExpandEventArgs)
            If args.IsInitializing Then
                args.IsExpanded = True
            End If
        End Sub
    End Class

    Public Class Product
        Public Property Name() As String
        Public Property Count() As Integer
    End Class
    Public Class ProductDescriptor
        Public Property Product() As Product
        Public Property Tags() As String()
    End Class
    Public Class ProductDescriptorMetadata
        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of ProductDescriptor))
            builder.Property(Function(x) x.Product).TypeConverter().ConvertFromRule(Function(str As String) New Product With {.Name = str, .Count = 0}).ConvertToRule(Of String)(Function(product)If(product Is Nothing, Nothing, product.Name)).PropertiesProvider(Function() TypeDescriptor.GetProperties(GetType(Product)).Cast(Of PropertyDescriptor)()).EndTypeConverter()

            builder.Property(Function(x) x.Tags).TypeConverter().ConvertFromRule(Function(str As String)If(String.IsNullOrEmpty(str), Nothing, str.Split(" "c))).ConvertToRule(Of String)(Function(strArray)If(strArray Is Nothing, Nothing, strArray.Aggregate(Function(sum, element) sum & " " & element))).EndTypeConverter()
        End Sub
    End Class
End Namespace
