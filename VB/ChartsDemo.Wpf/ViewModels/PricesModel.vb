Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace ChartsDemo
    Public Class PricesModel
        #Region "Data"

        Private Const PointCount As Integer = 1500

        Private Shared data_Renamed As List(Of PricesModel)

        Public Shared ReadOnly Property Data() As List(Of PricesModel)
            Get
                If data_Renamed IsNot Nothing Then
                    Return data_Renamed
                Else
                    data_Renamed = GetData()
                    Return data_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property MinVisibleDate() As Date
            Get
                Return Data.First().ProductData(PointCount - 150).TradeDate
            End Get
        End Property

        Private Shared Function GetData() As List(Of PricesModel)
            Dim startDate = Date.Now.Subtract(TimeSpan.FromDays(PointCount))
            Dim random = New Random()
            Return Enumerable.Range(1, 3).Select(Function(x) New PricesModel With {.ProductName = "Product" & x, .ProductData = CreateProductData(startDate, random)}).ToList()
        End Function

        #End Region
        #Region "Data Generator"

        Private Shared Function CreateProductData(ByVal [date] As Date, ByVal random As Random) As List(Of PriceByDate)
            Dim price = GenerateStartValue(random)

            Dim productData_Renamed = New List(Of PriceByDate)()
            For i As Integer = 0 To PointCount - 1
                productData_Renamed.Add(New PriceByDate([date], price))
                price += GenerateAddition(random)
                [date] = [date].AddDays(1)
            Next i
            Return productData_Renamed
        End Function
        Private Shared Function GenerateAddition(ByVal random As Random) As Double
            Dim factor As Double = random.NextDouble()
            If factor = 1 Then
                factor = 5
            ElseIf factor = 0 Then
                factor = -5
            End If
            Return (factor - 0.475) * 10
        End Function
        Private Shared Function GenerateStartValue(ByVal random As Random) As Double
            Return random.NextDouble() * 100
        End Function

        #End Region
        Private privateProductName As String
        Public Property ProductName() As String
            Get
                Return privateProductName
            End Get
            Private Set(ByVal value As String)
                privateProductName = value
            End Set
        End Property
        Private privateProductData As List(Of PriceByDate)
        Public Property ProductData() As List(Of PriceByDate)
            Get
                Return privateProductData
            End Get
            Private Set(ByVal value As List(Of PriceByDate))
                privateProductData = value
            End Set
        End Property
    End Class

    Public Class PriceByDate

        Private price_Renamed As Double

        Private tradeDate_Renamed As Date

        Public ReadOnly Property Price() As Double
            Get
                Return price_Renamed
            End Get
        End Property
        Public ReadOnly Property TradeDate() As Date
            Get
                Return tradeDate_Renamed
            End Get
        End Property

        Public Sub New(ByVal [date] As Date, ByVal price As Double)
            Me.tradeDate_Renamed = [date]
            Me.price_Renamed = price
        End Sub
    End Class
End Namespace
