Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB

Namespace PivotGridDemo.Helpers
    Public NotInheritable Class DatabaseHelper

        Private Sub New()
        End Sub
        #Region "Fields"

        Private Shared ReadOnly random As New Random()
        Private Shared ReadOnly FirstNames() As String = { "Julia", "Stephanie", "Alex", "John", "Curtis", "Keith", "Timothy", "Jack", "Miranda", "Alice" }
        Private Shared ReadOnly LastNames() As String = { "Black", "White", "Brown", "Smith", "Cooper", "Parker", "Walker", "Hunter", "Burton", "Douglas", "Fox", "Simpson" }
        Private Shared ReadOnly Adjectives() As String = { "Ancient", "Modern", "Mysterious", "Elegant", "Red", "Green", "Blue", "Amazing", "Wonderful", "Astonishing", "Lovely", "Beautiful", "Inexpensive", "Famous", "Magnificent", "Fancy" }
        Private Shared ReadOnly ProductNames() As String = { "Ice Cubes", "Bicycle", "Desk", "Hamburger", "Notebook", "Tea", "Cellphone", "Butter", "Frying Pan", "Napkin", "Armchair", "Chocolate", "Yoghurt", "Statuette", "Keychain" }
        Private Shared ReadOnly CategoryNames() As String = { "Business", "Presents", "Accessories", "Home", "Hobby" }

        Private Shared ReadOnly CustomersNames() As String
        Private Shared ReadOnly SalesPersonsNames() As String
        Private Shared ReadOnly Products() As ProductDataRecord

        #End Region

        Shared Sub New()
            CustomersNames = GenerateUniqueValues(random.Next(40, 50), AddressOf GeneratePersonName).ToArray()
            SalesPersonsNames = GenerateUniqueValues(random.Next(40, 50), AddressOf GeneratePersonName).ToArray()
            Products = GenerateProducts()
        End Sub

        #Region "Public"

        Public Shared Function GetOrderDate() As Date
            Return New Date(random.Next(2007, 2015), random.Next(1, 13), random.Next(1, 28))
        End Function
        Public Shared Function GetQuantity() As Integer
            Return random.Next(1, 100)
        End Function
        Public Shared Function GetProductPrice(ByVal product As ProductDataRecord) As Decimal
            Dim price = product.UnitPrice * CDec(0.5 + random.NextDouble())
            Return Math.Round(price, 2)
        End Function
        Public Shared Function GetProduct() As ProductDataRecord
            Return Products(random.Next(Products.Length))
        End Function
        Public Shared Function GetCustomerName() As String
            Return CustomersNames(random.Next(CustomersNames.Length))
        End Function
        Public Shared Function GetSalesPersonName() As String
            Return SalesPersonsNames(random.Next(SalesPersonsNames.Length))
        End Function

        #End Region
        #Region "Private"

        Private Shared Function GenerateUniqueValues(Of T)(ByVal count As Integer, ByVal generateValue As Func(Of T)) As List(Of T)
            Dim values = New HashSet(Of T)()
            Do While values.Count < count
                Dim value = generateValue()
                If Not values.Contains(value) Then
                    values.Add(value)
                End If
            Loop
            Return values.ToList()
        End Function

        Private Shared Function GenerateProducts() As ProductDataRecord()
            Return GenerateUniqueValues(random.Next(80, 100), AddressOf GenerateProductName).Select(Function(productName) New ProductDataRecord With {.ProductName = productName, .UnitPrice = random.Next(10, 500), .CategoryName = GenerateCategoryName()}).ToArray()
        End Function
        Private Shared Function GenerateCategoryName() As String
            Return CategoryNames(random.Next(CategoryNames.Length))
        End Function
        Private Shared Function GeneratePersonName() As String
            Return FirstNames(random.Next(FirstNames.Length)) & " " & LastNames(random.Next(LastNames.Length))
        End Function
        Private Shared Function GenerateProductName() As String
            Return Adjectives(random.Next(Adjectives.Length)) & " " & ProductNames(random.Next(ProductNames.Length))
        End Function
        #End Region
    End Class
End Namespace
