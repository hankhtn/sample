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
    Public Class OrderInfoDataGenerator
        Private Shared SyncRoot As New Object()
        Private Shared customerNames As New List(Of String)()
        Private Shared categoryData As New List(Of CategoryData)()
        Private Shared productData As New List(Of ProductData)()

        Private count_Renamed As Integer
        Private cachedOrders As New List(Of OrderData)()

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
        Public Shared Function ExtractCategoryDataList() As List(Of CategoryData)
            If categoryData.Count = 0 Then
                Dim categories = NWindContext.Create().Categories.ToList()
                categoryData.Capacity = categories.Count
                For Each row In categories
                    categoryData.Add(New CategoryData() With {.Name = row.CategoryName, .Picture = row.Icon25})
                Next row
            End If
            Return categoryData
        End Function
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

        Private Shared Function FindCategory(ByVal categoriesList As List(Of CategoryData), ByVal name As String) As CategoryData
            For Each category As CategoryData In categoriesList
                If category.Name = name Then
                    Return category
                End If
            Next category
            Return Nothing
        End Function

        Private Function GenerateOrders(ByVal generateCount As Integer, ByVal startFrom As Integer) As List(Of OrderData)
            Dim result As New List(Of OrderData)(generateCount)
            Dim customerNames As List(Of String) = ExtractCustomerNames()
            Dim categoriesList As List(Of CategoryData) = ExtractCategoryDataList()
            Dim productsList As List(Of ProductData) = ExtractProductDataList(categoriesList)

            OnGenerateOrderDataStarted(EventArgs.Empty)
            Dim rand As New Random()
            Dim generateCountPerCent As Integer = generateCount \ 100
            For i As Integer = 0 To generateCount - 1
                Dim randomProduct As ProductData = productsList(rand.Next(productsList.Count))
                Dim randomName As String = customerNames(rand.Next(customerNames.Count))
                Dim data As New OrderData()
                data.OrderId = i + startFrom
                data.OrderDate = Date.Today.Subtract(TimeSpan.FromDays(rand.Next(180)))
                data.CustomerName = randomName
                data.Quantity = rand.Next(200) + 1
                data.CustomerID = i + 1
                data.ProductCategory = randomProduct.Category
                data.ProductName = randomProduct.Name
                data.Price = randomProduct.Price
                data.IsReady = (rand.Next(2) = 0)
                result.Add(data)
                If generateCountPerCent > 0 AndAlso ((i + 1) Mod generateCountPerCent) = 0 Then
                    OnGenerateOrderDataProgress(New GenerateOrderDataProgressEventArgs(Convert.ToDouble((i + 1) \ generateCountPerCent)))
                End If
            Next i
            OnGenerateOrderDataCompleted(EventArgs.Empty)
            Return result
        End Function

        Protected Overridable Sub OnGenerateOrderDataStarted(ByVal e As EventArgs)
            RaiseEvent GenerateOrderDataStarted(Me, e)
        End Sub
        Protected Overridable Sub OnGenerateOrderDataCompleted(ByVal e As EventArgs)
            RaiseEvent GenerateOrderDataCompleted(Me, e)
        End Sub
        Protected Overridable Sub OnGenerateOrderDataProgress(ByVal e As GenerateOrderDataProgressEventArgs)
            RaiseEvent GenerateOrderDataProgress(Me, e)
        End Sub

        Public Sub New(ByVal count As Integer)
            Me.count_Renamed = count
        End Sub

        Public Property Count() As Integer
            Get
                Return count_Renamed
            End Get
            Set(ByVal value As Integer)
                count_Renamed = value
            End Set
        End Property

        Public Function GetOrders() As List(Of OrderData)
            Dim result As List(Of OrderData)
            SyncLock SyncRoot
                If Count > cachedOrders.Count Then
                    cachedOrders.AddRange(GenerateOrders(Count - cachedOrders.Count, cachedOrders.Count + 1))
                End If
                result = cachedOrders.GetRange(0, Count)
            End SyncLock
            Return result
        End Function
        Public Function GetCategories() As List(Of CategoryData)
            Return ExtractCategoryDataList()
        End Function

        Public Event GenerateOrderDataStarted As EventHandler
        Public Event GenerateOrderDataCompleted As EventHandler
        Public Event GenerateOrderDataProgress As EventHandler(Of GenerateOrderDataProgressEventArgs)
    End Class

    Public Class GenerateOrderDataProgressEventArgs
        Inherits EventArgs


        Private progress_Renamed As Double

        Public Sub New(ByVal progress As Double)
            Me.progress_Renamed = progress
        End Sub
        Public ReadOnly Property Progress() As Double
            Get
                Return progress_Renamed
            End Get
        End Property
    End Class
End Namespace
