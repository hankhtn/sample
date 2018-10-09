Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Reflection
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace DockingDemo.Helpers
    Public NotInheritable Class DataLoader

        Private Sub New()
        End Sub

        Private Shared Function LoadData(ByVal fileName As String) As DataSet
            Dim path As String = DataFilesHelper.FindFile(String.Format("{0}.xml", fileName), DataFilesHelper.DataPath)
            Dim ds As New DataSet()
            ds.ReadXml(path, XmlReadMode.ReadSchema)
            Return ds
        End Function
        Public Shared Function LoadSales() As DataSet
            Return LoadData("DashboardSales")
        End Function
    End Class
    Public MustInherit Class SalesDataGenerator
        Public Class Context
            Private ReadOnly st As String
            Private ReadOnly prodName As String
            Private ReadOnly catName As String
            Private ReadOnly lPrice As Decimal
            Private ReadOnly uSoldGenerator As UnitsSoldRandomGenerator

            Public ReadOnly Property State() As String
                Get
                    Return st
                End Get
            End Property
            Public ReadOnly Property ProductName() As String
                Get
                    Return prodName
                End Get
            End Property
            Public ReadOnly Property CategoryName() As String
                Get
                    Return catName
                End Get
            End Property
            Public ReadOnly Property ListPrice() As Decimal
                Get
                    Return lPrice
                End Get
            End Property
            Public ReadOnly Property UnitsSoldGenerator() As UnitsSoldRandomGenerator
                Get
                    Return uSoldGenerator
                End Get
            End Property

            Public Sub New(ByVal st As String, ByVal prodName As String, ByVal catName As String, ByVal lPrice As Decimal, ByVal uSoldGenerator As UnitsSoldRandomGenerator)
                Me.st = st
                Me.prodName = prodName
                Me.catName = catName
                Me.lPrice = lPrice
                Me.uSoldGenerator = uSoldGenerator
            End Sub
        End Class

        Protected Shared Function GetState(ByVal region As DataRow) As String
            Return DirectCast(region("Region"), String)
        End Function
        Protected Shared Function GetProductName(ByVal product As DataRow) As String
            Return DirectCast(product("Name"), String)
        End Function
        Protected Shared Function GetListPrice(ByVal product As DataRow) As Decimal
            Return DirectCast(product("ListPrice"), Decimal)
        End Function

        Private ReadOnly ds As DataSet
        Private ReadOnly categoriesTable As DataTable
        Private ReadOnly productsTable As DataTable
        Private ReadOnly regionsTable As DataTable
        Private ReadOnly rand As New Random(1)

        Private ReadOnly prodClasses_Renamed As ProductClasses

        Private ReadOnly regClasses_Renamed As RegionClasses

        Protected ReadOnly Property Regions() As DataRowCollection
            Get
                Return regionsTable.Rows
            End Get
        End Property
        Protected ReadOnly Property Products() As DataRowCollection
            Get
                Return productsTable.Rows
            End Get
        End Property
        Protected ReadOnly Property ProdClasses() As ProductClasses
            Get
                Return prodClasses_Renamed
            End Get
        End Property
        Protected ReadOnly Property RegClasses() As RegionClasses
            Get
                Return regClasses_Renamed
            End Get
        End Property
        Protected ReadOnly Property Random() As Random
            Get
                Return rand
            End Get
        End Property

        Protected Sub New(ByVal ds As DataSet)
            Me.ds = ds
            categoriesTable = ds.Tables("Categories")
            productsTable = ds.Tables("Products")
            regionsTable = ds.Tables("Regions")
            prodClasses_Renamed = New ProductClasses(productsTable.Rows)
            regClasses_Renamed = New RegionClasses(regionsTable.Rows)
        End Sub
        Protected Function GetRegionWeigtht(ByVal region As DataRow) As Double
            Return regClasses_Renamed(DirectCast(region("RegionID"), Integer))
        End Function
        Protected Function GetProductClass(ByVal product As DataRow) As ProductClass
            Return prodClasses_Renamed(DirectCast(product("ProductID"), Integer))
        End Function
        Protected Function GetCategoryName(ByVal product As DataRow) As String
            Return CStr(categoriesTable.Select(String.Format("CategoryID = {0}", product("CategoryID")))(0)("CategoryName"))
        End Function
        Protected Function CreateUnitsSoldGenerator(ByVal regionWeight As Double, ByVal productClass As ProductClass) As UnitsSoldRandomGenerator
            Return New UnitsSoldRandomGenerator(rand, CInt((Math.Ceiling(productClass.SaleProbability * regionWeight))))
        End Function
        Protected MustOverride Sub Generate(ByVal context As Context)
        Protected Overridable Sub EndGenerate()
        End Sub
        Public Sub Generate()
            For Each region As DataRow In Regions
                Dim state As String = GetState(region)
                Dim regionWeight As Double = GetRegionWeigtht(region)
                For Each product As DataRow In Products
                    Dim unitsSoldgenerator As UnitsSoldRandomGenerator = CreateUnitsSoldGenerator(regionWeight, GetProductClass(product))
                    Generate(New Context(state, GetProductName(product), GetCategoryName(product), GetListPrice(product), unitsSoldgenerator))
                Next product
            Next region
            EndGenerate()
        End Sub
    End Class
    Public Class SalesPerformanceDataGenerator
        Inherits SalesDataGenerator


        Private ReadOnly monthlySales_Renamed As New List(Of MonthlySalesItem)()

        Private ReadOnly totalSales_Renamed As New List(Of TotalSalesItem)()
        Private ReadOnly item As New KeyMetricsItem()

        Public ReadOnly Property MonthlySales() As IEnumerable(Of MonthlySalesItem)
            Get
                Return monthlySales_Renamed
            End Get
        End Property
        Public ReadOnly Property TotalSales() As IEnumerable(Of TotalSalesItem)
            Get
                Return totalSales_Renamed
            End Get
        End Property
        Public ReadOnly Property KeyMetrics() As IEnumerable(Of KeyMetricsItem)
            Get
                Return New KeyMetricsItem() { item }
            End Get
        End Property

        Public Sub New(ByVal dataSet As DataSet)
            MyBase.New(dataSet)
        End Sub
        Protected Overrides Sub Generate(ByVal context As Context)
            Dim tsItem As TotalSalesItem = New TotalSalesItem With {.State = context.State, .Category = context.CategoryName, .Product = context.ProductName}
            Dim y As Integer = Date.Today.Year - 1
            For month As Integer = 1 To 12
                Dim dt As New Date(y, month, 1)
                context.UnitsSoldGenerator.Next()
                Dim uSold As Integer = context.UnitsSoldGenerator.UnitsSold
                Dim uSoldTarget As Integer = context.UnitsSoldGenerator.UnitsSoldTarget
                Dim rev As Decimal = uSold * context.ListPrice
                Dim revTarget As Decimal = uSoldTarget * context.ListPrice
                monthlySales_Renamed.Add(New MonthlySalesItem With {.State = context.State, .Product = context.ProductName, .Category = context.CategoryName, .CurrentDate = dt, .UnitsSold = uSold, .UnitsSoldTarget = uSoldTarget, .Revenue = rev, .RevenueTarget = revTarget})
                tsItem.RevenueYTD += rev
                tsItem.RevenueYTDTarget += revTarget
                tsItem.UnitsSoldYTD += uSold
                tsItem.UnitsSoldYTDTarget += uSoldTarget
                If month >= 10 AndAlso month <= 12 Then
                    tsItem.RevenueQTD += rev
                    tsItem.RevenueQTDTarget += revTarget
                End If
                item.RevenueYTD += rev
                item.RevenueYTDTarget += revTarget
            Next month
            totalSales_Renamed.Add(tsItem)
        End Sub
        Protected Overrides Sub EndGenerate()
            MyBase.EndGenerate()
            item.ExpencesYTD = item.RevenueYTDTarget * 0.2D
            item.ExpencesYTDTarget = item.RevenueYTDTarget * 0.1999D
            item.ProfitYTD = item.RevenueYTD - item.ExpencesYTD
            item.ProfitYTDTarget = item.RevenueYTDTarget - item.ExpencesYTDTarget
            item.AvgOrderSizeYTD = item.RevenueYTD * 0.006D
            item.AvgOrderSizeYTDTarget = item.RevenueYTDTarget * 0.0055D
            item.NewCustomersYTD = CInt((Math.Round(item.RevenueYTD * 0.0013D)))
            item.NewCustomersYTDTarget = CInt((Math.Round(item.RevenueYTDTarget * 0.00125D)))
            item.MarketShare = 0.23F
        End Sub
    End Class
    Public Class UnitsSoldRandomGenerator
        Private Const MinUnitsSold As Integer = 5

        Private ReadOnly rand As Random
        Private ReadOnly startUnitsSold As Integer
        Private prevUnitsSold? As Integer
        Private prevPrevUnitsSold? As Integer

        Private unitsSold_Renamed As Integer

        Private unitsSoldTarget_Renamed As Integer
        Private isFirst As Boolean = True

        Public ReadOnly Property UnitsSold() As Integer
            Get
                Return unitsSold_Renamed
            End Get
        End Property
        Public ReadOnly Property UnitsSoldTarget() As Integer
            Get
                Return unitsSoldTarget_Renamed
            End Get
        End Property

        Public Sub New(ByVal rand As Random, ByVal startUnitsSold As Integer)
            Me.rand = rand
            Me.startUnitsSold = Math.Max(startUnitsSold, MinUnitsSold)
        End Sub
        Public Sub [Next]()
            If isFirst Then
                unitsSold_Renamed = startUnitsSold
                isFirst = False
            Else
                unitsSold_Renamed = unitsSold_Renamed + CInt((Math.Round(DataHelper.Random(rand, unitsSold_Renamed * 0.5))))
                unitsSold_Renamed = Math.Max(unitsSold_Renamed, MinUnitsSold)
            End If
            Dim unitsSoldSum As Integer = unitsSold_Renamed
            Dim count As Integer = 1
            If prevUnitsSold.HasValue Then
                unitsSoldSum += prevUnitsSold.Value
                count += 1
            End If
            If prevPrevUnitsSold.HasValue Then
                unitsSoldSum += prevPrevUnitsSold.Value
                count += 1
            End If
            unitsSoldTarget_Renamed = CInt((Math.Round(CDbl(unitsSoldSum) / count)))
            unitsSoldTarget_Renamed = unitsSoldTarget_Renamed + CInt((Math.Round(DataHelper.Random(rand, unitsSoldTarget_Renamed))))
            prevPrevUnitsSold = prevUnitsSold
            prevUnitsSold = unitsSold_Renamed
        End Sub
    End Class
    Public Class ProductClasses
        Inherits List(Of ProductClass)

        Default Public Shadows ReadOnly Property Item(ByVal productID As Integer) As ProductClass
            Get
                For Each productClass As ProductClass In Me
                    If productClass.ContainsProduct(productID) Then
                        Return productClass
                    End If
                Next productClass
                Throw New ArgumentException("procutID")
            End Get
        End Property
        Public Sub New(ByVal products As ICollection)
            Add(New ProductClass(Nothing, 100D, 0.5))
            Add(New ProductClass(100D, 500D, 0.4))
            Add(New ProductClass(500D, 1500D, 0.3))
            Add(New ProductClass(1500D, Nothing, 0.2))
            For Each product As DataRow In products
                Dim productID As Integer = DirectCast(product("ProductID"), Integer)
                Dim listPrice As Decimal = DirectCast(product("ListPrice"), Decimal)
                For Each productClass As ProductClass In Me
                    If productClass.AddProduct(productID, listPrice) Then
                        Exit For
                    End If
                Next productClass
            Next product
        End Sub
    End Class
    Public Class RegionClasses
        Inherits Dictionary(Of Integer, Double)

        Public Sub New(ByVal regions As ICollection)
            Dim numberEmployeesMin? As Integer = Nothing
            For Each region As DataRow In regions
                Dim numberEmployees As Short = DirectCast(region("NumberEmployees"), Short)
                numberEmployeesMin = If(numberEmployeesMin.HasValue, Math.Min(numberEmployeesMin.Value, numberEmployees), numberEmployees)
            Next region
            For Each region As DataRow In regions
                Add(DirectCast(region("RegionID"), Integer), DirectCast(region("NumberEmployees"), Short) / CDbl(numberEmployeesMin.Value))
            Next region
        End Sub
    End Class
    Public Class ProductClass
        Private ReadOnly productIDs As New List(Of Integer)()
        Private ReadOnly minPrice? As Decimal
        Private ReadOnly maxPrice? As Decimal

        Private ReadOnly saleProbability_Renamed As Double

        Public ReadOnly Property SaleProbability() As Double
            Get
                Return saleProbability_Renamed
            End Get
        End Property

        Public Sub New(ByVal minPrice? As Decimal, ByVal maxPrice? As Decimal, ByVal saleProbability As Double)
            Me.minPrice = minPrice
            Me.maxPrice = maxPrice
            Me.saleProbability_Renamed = saleProbability
        End Sub
        Public Function AddProduct(ByVal productID As Integer, ByVal price As Decimal) As Boolean
            Dim satisfyMinPrice As Boolean = (Not minPrice.HasValue) OrElse price >= minPrice.Value
            Dim satisfyMaxPrice As Boolean = (Not maxPrice.HasValue) OrElse price < maxPrice.Value
            If satisfyMinPrice AndAlso satisfyMaxPrice Then
                productIDs.Add(productID)
                Return True
            End If
            Return False
        End Function
        Public Function ContainsProduct(ByVal productID As Integer) As Boolean
            Return productIDs.Contains(productID)
        End Function
    End Class
    Public NotInheritable Class DataHelper

        Private Sub New()
        End Sub


        Public Shared Function Random(ByVal random_Renamed As Random, ByVal deviation As Double, ByVal positive As Boolean) As Double
            Dim rand As Integer = random_Renamed.Next(If(positive, 0, -1000000), 1000000)
            Return CDbl(rand) / 1000000 * deviation
        End Function

        Public Shared Function Random(ByVal random_Renamed As Random, ByVal deviation As Double) As Double
            Return DataHelper.Random(random_Renamed, deviation, False)
        End Function
    End Class
    Public Class TotalSalesItem
        Public Property State() As String
        Public Property Category() As String
        Public Property Product() As String
        Public Property RevenueYTD() As Decimal
        Public Property RevenueYTDTarget() As Decimal
        Public Property RevenueQTD() As Decimal
        Public Property RevenueQTDTarget() As Decimal
        Public Property UnitsSoldYTD() As Integer
        Public Property UnitsSoldYTDTarget() As Integer
    End Class
    Public Class MonthlySalesItem
        Public Property State() As String
        Public Property Product() As String
        Public Property Category() As String
        Public Property CurrentDate() As Date
        Public Property Revenue() As Decimal
        Public Property RevenueTarget() As Decimal
        Public Property UnitsSold() As Integer
        Public Property UnitsSoldTarget() As Integer
    End Class
    Public Class KeyMetricsItem
        Public Property RevenueYTD() As Decimal
        Public Property RevenueYTDTarget() As Decimal
        Public Property ExpencesYTD() As Decimal
        Public Property ExpencesYTDTarget() As Decimal
        Public Property ProfitYTD() As Decimal
        Public Property ProfitYTDTarget() As Decimal
        Public Property AvgOrderSizeYTD() As Decimal
        Public Property AvgOrderSizeYTDTarget() As Decimal
        Public Property NewCustomersYTD() As Integer
        Public Property NewCustomersYTDTarget() As Integer
        Public Property MarketShare() As Single
    End Class
End Namespace
