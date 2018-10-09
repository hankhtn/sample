Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData.Models

Namespace DevExpress.Diagram.Demos
    Public NotInheritable Class OrderDataGenerator

        Private Sub New()
        End Sub

        Private Shared Function ExtractCustomers(ByVal count As Integer) As List(Of CustomerData)
            Dim customers = NWindContext.Create().Customers.Take(count).ToList()
            Dim customerData = New List(Of CustomerData)(count)
            For Each row In customers
                customerData.Add(New CustomerData(row.ContactName, row.CompanyName, row.Phone, row.City))
            Next row
            Return customerData
        End Function
        Private Shared Function ExtractCategoryDataList(ByVal count As Integer) As List(Of CategoryData)
            Dim categoryData As New List(Of CategoryData)(count)
            Dim categories = NWindContext.Create().Categories.Take(count).ToList()
            For Each row In categories
                categoryData.Add(New CategoryData(row.CategoryName, row.Description, row.Icon17))
            Next row
            Return categoryData
        End Function
        Private Shared Function ExtractProductDataList(ByVal categoriesList As List(Of CategoryData)) As List(Of ProductData)
            Dim productData As New List(Of ProductData)()
            Dim categoryProducts = NWindContext.Create().CategoryProducts.ToList()
            productData.Capacity = categoryProducts.Count
            Dim rand As New Random()
            For Each row In categoryProducts
                Dim category = FindCategory(categoriesList, row.CategoryName)
                If category IsNot Nothing Then
                    productData.Add(New ProductData(row.ProductName, category, CDec(rand.Next(20) + rand.Next(99)) / 100D))
                End If
            Next row
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

        Private Shared Function GenerateOrders(ByVal orderCount As Integer, ByVal customerCount As Integer, ByVal categoryCount As Integer) As List(Of OrderData)
            Dim result As New List(Of OrderData)(orderCount)
            Dim customers As List(Of CustomerData) = ExtractCustomers(customerCount)
            Dim categoriesList As List(Of CategoryData) = ExtractCategoryDataList(categoryCount)
            Dim productsList As List(Of ProductData) = ExtractProductDataList(categoriesList)

            Dim rand As New Random()
            Dim generateCountPerCent As Integer = orderCount \ 100
            For i As Integer = 0 To orderCount - 1
                Dim randomProduct As ProductData = productsList(rand.Next(productsList.Count))
                Dim data As New OrderData(orderId:= i, orderDate:= Date.Today.Subtract(TimeSpan.FromDays(rand.Next(180))), customer:= customers(rand.Next(customers.Count)), quantity:= rand.Next(200) + 1, productCategory:= randomProduct.Category, productName:= randomProduct.Name, price:= randomProduct.Price)
                result.Add(data)
            Next i
            Return result
        End Function
        Public Shared Function GenerateProductFlowInfo() As ProductFlowInfo
            Dim orders = GenerateOrders(50, 4, 5).ToArray()
            Dim customers = orders.Select(Function(x) x.Customer).Distinct().ToArray()
            Dim categories = orders.Select(Function(x) x.Category).Distinct().ToArray()
            Dim groupedOrders = orders.GroupBy(Function(x) New With {Key x.Customer, Key x.Category}).ToArray()
            Dim minCount = groupedOrders.Min(Function(x) x.Sum(Function(y) y.Quantity))
            Dim maxCount = groupedOrders.Max(Function(x) x.Sum(Function(y) y.Quantity))
            Const minWeight As Single = 1, maxWeight As Single = 8
            Dim productFlows = orders.GroupBy(Function(x) New With {Key x.Customer, Key x.Category}).Select(Function(x) New ProductFlowData(x.Key.Customer, x.Key.Category, minWeight + (maxWeight - minWeight) * (x.Sum(Function(d) d.Quantity) - minCount) / (maxCount - minCount))).ToArray()
            Return New ProductFlowInfo(productFlows, orders, customers, categories)
        End Function
    End Class
    Public Class ProductFlowInfo
        Public Sub New(ByVal productFlows() As ProductFlowData, ByVal orders() As OrderData, ByVal customers() As CustomerData, ByVal categories() As CategoryData)
            Me.ProductFlows = productFlows
            Me.Orders = orders
            Me.Customers = customers
            Me.Categories = categories
            Items = Me.Customers.Cast(Of Object)().Concat(Me.Categories).ToArray()
        End Sub
        Private privateItems As Object()
        Public Property Items() As Object()
            Get
                Return privateItems
            End Get
            Private Set(ByVal value As Object())
                privateItems = value
            End Set
        End Property
        Private privateProductFlows As ProductFlowData()
        Public Property ProductFlows() As ProductFlowData()
            Get
                Return privateProductFlows
            End Get
            Private Set(ByVal value As ProductFlowData())
                privateProductFlows = value
            End Set
        End Property
        Private privateOrders As OrderData()
        Public Property Orders() As OrderData()
            Get
                Return privateOrders
            End Get
            Private Set(ByVal value As OrderData())
                privateOrders = value
            End Set
        End Property
        Private privateCustomers As CustomerData()
        Public Property Customers() As CustomerData()
            Get
                Return privateCustomers
            End Get
            Private Set(ByVal value As CustomerData())
                privateCustomers = value
            End Set
        End Property
        Private privateCategories As CategoryData()
        Public Property Categories() As CategoryData()
            Get
                Return privateCategories
            End Get
            Private Set(ByVal value As CategoryData())
                privateCategories = value
            End Set
        End Property
    End Class
    Public Class CategoryData
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Private Set(ByVal value As String)
                privateName = value
            End Set
        End Property
        Private privateDescription As String
        Public Property Description() As String
            Get
                Return privateDescription
            End Get
            Private Set(ByVal value As String)
                privateDescription = value
            End Set
        End Property
        Private privatePicture As Byte()
        Public Property Picture() As Byte()
            Get
                Return privatePicture
            End Get
            Private Set(ByVal value As Byte())
                privatePicture = value
            End Set
        End Property
        Public Sub New(ByVal name As String, ByVal description As String, ByVal picture() As Byte)
            Me.Name = name
            Me.Description = description
            Me.Picture = picture
        End Sub
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
    Public Class ProductData
        Public Sub New(ByVal name As String, ByVal category As CategoryData, ByVal price As Decimal)
            Me.Name = name
            Me.Category = category
            Me.Price = price
        End Sub
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Private Set(ByVal value As String)
                privateName = value
            End Set
        End Property
        Private privateCategory As CategoryData
        Public Property Category() As CategoryData
            Get
                Return privateCategory
            End Get
            Private Set(ByVal value As CategoryData)
                privateCategory = value
            End Set
        End Property
        Private privatePrice As Decimal
        Public Property Price() As Decimal
            Get
                Return privatePrice
            End Get
            Private Set(ByVal value As Decimal)
                privatePrice = value
            End Set
        End Property
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
    Public Class CustomerData
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Private Set(ByVal value As String)
                privateName = value
            End Set
        End Property
        Private privateCompanyName As String
        Public Property CompanyName() As String
            Get
                Return privateCompanyName
            End Get
            Private Set(ByVal value As String)
                privateCompanyName = value
            End Set
        End Property
        Private privatePhoneNumber As String
        Public Property PhoneNumber() As String
            Get
                Return privatePhoneNumber
            End Get
            Private Set(ByVal value As String)
                privatePhoneNumber = value
            End Set
        End Property
        Private privateCity As String
        Public Property City() As String
            Get
                Return privateCity
            End Get
            Private Set(ByVal value As String)
                privateCity = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal companyName As String, ByVal phoneNumber As String, ByVal city As String)
            Me.Name = name
            Me.CompanyName = companyName
            Me.PhoneNumber = phoneNumber
            Me.City = city
        End Sub
        Public Overrides Function ToString() As String
            Return CompanyName & ControlChars.CrLf & ControlChars.CrLf & City & ControlChars.CrLf & PhoneNumber
        End Function
    End Class
    Public Class OrderData
        Public Sub New(ByVal orderId As Integer, ByVal customer As CustomerData, ByVal productCategory As CategoryData, ByVal productName As String, ByVal orderDate As Date, ByVal quantity As Integer, ByVal price As Decimal)
            Me.OrderId = orderId
            Me.Customer = customer
            Category = productCategory
            Me.ProductName = productName
            Me.OrderDate = orderDate
            Me.Quantity = quantity
            Me.Price = price
        End Sub
        Private privateOrderId As Integer
        Public Property OrderId() As Integer
            Get
                Return privateOrderId
            End Get
            Private Set(ByVal value As Integer)
                privateOrderId = value
            End Set
        End Property
        Private privateCustomer As CustomerData
        Public Property Customer() As CustomerData
            Get
                Return privateCustomer
            End Get
            Private Set(ByVal value As CustomerData)
                privateCustomer = value
            End Set
        End Property
        Private privateCategory As CategoryData
        Public Property Category() As CategoryData
            Get
                Return privateCategory
            End Get
            Private Set(ByVal value As CategoryData)
                privateCategory = value
            End Set
        End Property
        Private privateProductName As String
        Public Property ProductName() As String
            Get
                Return privateProductName
            End Get
            Private Set(ByVal value As String)
                privateProductName = value
            End Set
        End Property
        Private privateOrderDate As Date
        Public Property OrderDate() As Date
            Get
                Return privateOrderDate
            End Get
            Private Set(ByVal value As Date)
                privateOrderDate = value
            End Set
        End Property
        Private privateQuantity As Integer
        Public Property Quantity() As Integer
            Get
                Return privateQuantity
            End Get
            Private Set(ByVal value As Integer)
                privateQuantity = value
            End Set
        End Property
        Public Property Price() As Decimal
    End Class
    Public Class ProductFlowData
        Public Sub New(ByVal customer As CustomerData, ByVal category As CategoryData, ByVal weight As Single)
            Me.Customer = customer
            Me.Category = category
            Me.Weight = weight
        End Sub
        Private privateCustomer As CustomerData
        Public Property Customer() As CustomerData
            Get
                Return privateCustomer
            End Get
            Private Set(ByVal value As CustomerData)
                privateCustomer = value
            End Set
        End Property
        Private privateCategory As CategoryData
        Public Property Category() As CategoryData
            Get
                Return privateCategory
            End Get
            Private Set(ByVal value As CategoryData)
                privateCategory = value
            End Set
        End Property
        Private privateWeight As Single
        Public Property Weight() As Single
            Get
                Return privateWeight
            End Get
            Private Set(ByVal value As Single)
                privateWeight = value
            End Set
        End Property
    End Class

End Namespace
