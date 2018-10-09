Imports DevExpress.DemoData.Models
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace GridDemo
    Public NotInheritable Class GroupIntervalData

        Private Sub New()
        End Sub

        Public Shared rnd As New Random()
        Public Shared ReadOnly Property Invoices() As List(Of Invoice)
            Get

                Dim invoices_Renamed = NWindContext.Create().Invoices.ToList()
                For Each invoice In invoices_Renamed
                    invoice.OrderDate = GetDate(True)
                Next invoice
                Return invoices_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property Products() As List(Of ProductInfo)
            Get
                Const rowCount As Integer = 1000

                Dim products_Renamed = NWindContext.Create().Products.ToList()
                Dim shuffledProducts = New List(Of ProductInfo)()
                For i As Integer = 0 To rowCount - 1
                    Dim product As Product = products_Renamed(rnd.Next(products_Renamed.Count - 1))
                    shuffledProducts.Add(New ProductInfo() With {.ProductName = product.ProductName, .UnitPrice = product.UnitPrice, .Count = GetCount(), .OrderDate = GetDate(False)})
                Next i
                Return shuffledProducts
            End Get
        End Property
        Private Shared Function GetDate(ByVal range As Boolean) As Date
            Dim result As Date = Date.Now
            Dim r As Integer = rnd.Next(20)
            If range Then
                If r > 1 Then
                    result = result.AddDays(rnd.Next(80) - 40)
                End If
                If r > 18 Then
                    result = result.AddMonths(rnd.Next(18))
                End If
            Else
                result = result.AddDays(rnd.Next(r * 30) - r * 15)
            End If
            Return result
        End Function
        Private Shared Function GetCount() As Decimal
            Return rnd.Next(50) + 10
        End Function
    End Class
    Public Class ProductInfo
        Public Property ProductName() As String
        Public Property UnitPrice() As Decimal?
        Public Property Count() As Decimal
        Public Property OrderDate() As Date
    End Class
End Namespace
