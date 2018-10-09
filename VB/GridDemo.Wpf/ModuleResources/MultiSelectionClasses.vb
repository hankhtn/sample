Imports DevExpress.DemoData.Models
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Data

Namespace GridDemo
    Public Structure Range
        Public Property Text() As String
        Public Property Min() As Integer
        Public Property Max() As Integer
        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Structure
    Public Class ProductIdToProductNameConverter
        Implements IValueConverter


        Private Shared products_Renamed As Dictionary(Of Long, Product)

        Private Shared ReadOnly Property Products() As Dictionary(Of Long, Product)
            Get
                If products_Renamed Is Nothing Then
                    products_Renamed = NWindContext.Create().Products.ToDictionary(Function(p) p.ProductID)
                End If
                Return products_Renamed
            End Get
        End Property

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim id As Long = DirectCast(value, Long)
            If Products.ContainsKey(id) Then
                Dim product As Product = Products(id)
                Return If(product IsNot Nothing, product.ProductName, String.Empty)
            End If
            Return String.Empty
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
