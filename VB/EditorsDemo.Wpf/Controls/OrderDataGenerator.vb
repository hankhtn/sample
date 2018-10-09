Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.DemoBase
Imports System.Data
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports EmployeesWithPhotoData = DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData
Imports Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee
Imports Order = DevExpress.DemoData.Models.Order

Namespace GridDemo
    Public NotInheritable Class OrderDataGenerator

        Private Sub New()
        End Sub

        Private Shared SyncRoot As New Object()
        Private Shared customerNames As New List(Of String)()
        Private Shared categoryData As New List(Of CategoryData)()
        Private Shared productData As New List(Of ProductData)()

        Private Shared Function ExtractCustomerNames() As List(Of String)
            If customerNames.Count = 0 Then
                Dim customers = NWindContext.Create().Customers.ToList()
                customerNames.Capacity = customers.Count
                For Each row In customers
                    customerNames.Add(row.ContactName)
                Next row
            End If
            Return customerNames
        End Function
        Public Shared ReadOnly Property CategoryDataList() As List(Of CategoryData)
            Get
                If categoryData.Count = 0 Then
                    Dim categories = NWindContext.Create().Categories.ToList()
                    categoryData.Capacity = categories.Count
                    For Each row In categories
                        categoryData.Add(New CategoryData() With {.Name = row.CategoryName, .Picture = row.Icon25})
                    Next row
                End If
                Return categoryData
            End Get
        End Property
        Public Shared Function ExtractProductDataList(ByVal categoriesList As List(Of CategoryData)) As List(Of ProductData)
            If productData.Count = 0 Then
                Dim categoryProducts = NWindContext.Create().CategoryProducts.ToList()
                productData.Capacity = categoryProducts.Count
                Dim rand As New Random()
                For Each row In categoryProducts
                    productData.Add(New ProductData() With {.Category = FindCategory(categoriesList, row.CategoryName), .Name = row.ProductName, .Price = CDec(rand.Next(20) + rand.Next(99)) / 100D})
                Next row
            End If
            Return productData
        End Function

        Public Shared Function GenerateOrders(ByVal generateCount As Integer) As List(Of OrderData)
            Dim result As New List(Of OrderData)(generateCount)
            Dim customerNames As List(Of String) = ExtractCustomerNames()
            Dim categoriesList As List(Of CategoryData) = CategoryDataList
            Dim productsList As List(Of ProductData) = ExtractProductDataList(categoriesList)

            Dim rand As New Random()
            For i As Integer = 0 To generateCount - 1
                Dim randomProduct As ProductData = productsList(rand.Next(productsList.Count))
                Dim randomName As String = customerNames(rand.Next(customerNames.Count))
                Dim data As New OrderData()
                data.OrderId = i
                data.OrderDate = Date.Today.Subtract(TimeSpan.FromDays(rand.Next(180)))
                data.CustomerName = randomName
                data.Quantity = rand.Next(200) + 1
                data.CustomerID = i + 1
                data.ProductCategory = randomProduct.Category
                data.ProductName = randomProduct.Name
                data.Price = randomProduct.Price
                data.IsReady = (rand.Next(2) = 0)
                result.Add(data)
            Next i
            Return result
        End Function

        Private Shared Function FindCategory(ByVal categoriesList As List(Of CategoryData), ByVal name As String) As CategoryData
            For Each category As CategoryData In categoriesList
                If category.Name = name Then
                    Return category
                End If
            Next category
            Return Nothing
        End Function
    End Class

    Public Class CategoryData
        Implements IComparable, IComparable(Of CategoryData)

        Public Property Name() As String
        Public Property Picture() As Byte()
        Public Overrides Function ToString() As String
            Return Name
        End Function

        #Region "IComparable Members"
        Public Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo
            If TypeOf obj Is CategoryData Then
                Return CompareTo(DirectCast(obj, CategoryData))
            End If
            Return -1
        End Function
        #End Region
        #Region "IComparable<CategoryData> Members"
        Public Function CompareTo(ByVal other As CategoryData) As Integer Implements IComparable(Of CategoryData).CompareTo
            Return StringComparer.CurrentCulture.Compare(Name, other.Name)
        End Function
        #End Region
    End Class
    Public Class ProductData
        Public Property Name() As String
        Public Property Category() As CategoryData
        Public Property Price() As Decimal
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class OrderData
        Public Property OrderId() As Integer
        Public Property IsReady() As Boolean
        Public Property CustomerName() As String
        Public Property CustomerID() As Integer
        Public Property OrderDate() As Date
        Public Property ProductCategory() As CategoryData
        Public Property ProductName() As String
        Public Property Quantity() As Integer
        Public Property Price() As Decimal
    End Class
End Namespace
