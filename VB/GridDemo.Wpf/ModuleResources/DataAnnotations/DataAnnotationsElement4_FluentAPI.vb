Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.ComponentModel.DataAnnotations

Namespace GridDemo
    <MetadataType(GetType(DataAnnotationsElement4Metadata))>
    Public Class DataAnnotationsElement4_FluentAPI
        Public Property OrderId() As Integer
        Public Property ProductCategory() As CategoryData
        Public Property ProductName() As String
        Public Property CustomerName() As String
        Public Property OrderDate() As Date
        Public Property Quantity() As Integer
        Public Property Price() As Decimal
        Public Property IsReady() As Boolean
    End Class

    Public NotInheritable Class DataAnnotationsElement4Metadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of DataAnnotationsElement4_FluentAPI))
            builder.TableLayout().Group("Product Details").ContainsProperty(Function(p) p.ProductCategory).ContainsProperty(Function(p) p.ProductName).EndGroup().GroupContainer("Order Details").Group("Main").ContainsProperty(Function(p) p.CustomerName).ContainsProperty(Function(p) p.OrderDate).EndGroup().Group("Status").ContainsProperty(Function(p) p.Quantity).ContainsProperty(Function(p) p.Price).ContainsProperty(Function(p) p.IsReady).EndGroup().EndGroupContainer()

            builder.Property(Function(p) p.OrderId).DisplayName("Id").NotAutoGenerated().NotEditable()
            builder.Property(Function(p) p.ProductCategory).DisplayName("Category").NotEditable()
            builder.Property(Function(p) p.ProductName).DisplayName("Name").NotEditable()
            builder.Property(Function(p) p.CustomerName).DisplayName("Customer").NotEditable()
            builder.Property(Function(p) p.OrderDate).DisplayName("Date")
            builder.Property(Function(p) p.Price).CurrencyDataType()
            builder.Property(Function(p) p.IsReady).DisplayName("Is ready")
        End Sub
    End Class
End Namespace