Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData.Models.Vehicles

Namespace LayoutControlDemo
    Public Class Brand
        Public Property Name() As String
        Public Property Image() As Byte()
    End Class

    Public Class Car
        Public Property Name() As String
        Public Property Image() As Byte()
        Public Property Price() As Decimal
        Public Property IsFirstInBrand() As Boolean
        Public Property IsLastInBrand() As Boolean
    End Class

    Public Class CarsViewModel
        Private privateItems As List(Of Object)
        Public Property Items() As List(Of Object)
            Get
                Return privateItems
            End Get
            Private Set(ByVal value As List(Of Object))
                privateItems = value
            End Set
        End Property

        Public Sub New()
            Items = New List(Of Object)()
            Using context = New VehiclesContext()
                Dim brands = context.Trademarks.OrderBy(Function(x) x.Name)
                For Each brand In brands
                    Dim cars = context.Models.Where(Function(x) x.TrademarkID = brand.ID).OrderBy(Function(x) x.Name)
                    If cars.Count() > 0 Then
                        Items.Add(New Brand() With {.Name = brand.Name, .Image = brand.Logo})
                        Dim isFirstInBrand As Boolean = True
                        For Each car In cars
                            Items.Add(New Car() With {.Image = car.Image, .Name = brand.Name & " " & car.Name, .Price = car.Price, .IsFirstInBrand = isFirstInBrand})
                            isFirstInBrand = False
                        Next car
                        TryCast(Items.Last(), Car).IsLastInBrand = True
                    End If
                Next brand
            End Using
        End Sub
    End Class
End Namespace
