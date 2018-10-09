Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows.Threading
Imports System.Xml.Serialization
Imports DevExpress.Internal
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace NavigationDemo
    <POCOViewModel>
    Public Class CustomizedContentViewModel
        Private Const HistoryLength As Integer = 20
        Private Const UpdateInterval As Integer = 1000
        Private random As New Random()

        Public Property Data() As IEnumerable(Of HierarchicalStockItem)

        Public Sub New()

            Dim data_Renamed = StockItems.GetData().Select(Function(x) New HierarchicalStockItem(x.CompanyName, x.Price, x.Volume, x.LowPrice, x.HighPrice)).ToList()
            For Each stockItem As HierarchicalStockItem In data_Renamed
                For i As Integer = 0 To HistoryLength - 1
                    UpdatePrice(stockItem.StockItemInfo)
                Next i
            Next stockItem
            Data = data_Renamed
        End Sub

        Private timer As DispatcherTimer
        Public Sub OnLoaded()
            timer = New DispatcherTimer() With {.Interval = TimeSpan.FromMilliseconds(UpdateInterval)}
            AddHandler timer.Tick, AddressOf timer_Tick
            timer.Start()
        End Sub
        Public Sub OnUnloaded()
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf timer_Tick
            timer = Nothing
        End Sub
        Private Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            For Each stockItem As HierarchicalStockItem In Data
                UpdatePrice(stockItem.StockItemInfo)
            Next stockItem
        End Sub

        Private Sub UpdatePrice(ByVal stockItem As StockItemInfo)
            Dim newDelta As Double = random.NextDouble() * 0.5 - 0.25
            If stockItem.Price + newDelta <= 0 Then
                newDelta = 0
            End If
            If Math.Sign(stockItem.DeltaPrice) = Math.Sign(newDelta) Then
                stockItem.DeltaChange = 0
            Else
                stockItem.DeltaChange = Math.Sign(newDelta)
            End If
            stockItem.DeltaPrice = newDelta
            stockItem.Price += stockItem.DeltaPrice
            If stockItem.Price < stockItem.LowPrice Then
                stockItem.LowPrice = stockItem.Price
            End If
            If stockItem.Price > stockItem.HighPrice Then
                stockItem.HighPrice = stockItem.Price
            End If
            stockItem.DeltaPricePercent = stockItem.DeltaPrice / stockItem.Price
            stockItem.Volume += Convert.ToInt32(random.NextDouble() * stockItem.Volume * 0.005 - 0.0025)
            UpdatePriceHistory(stockItem)
        End Sub
        Private Sub UpdatePriceHistory(ByVal stockItem As StockItemInfo)
            Dim newPriceHistory As New List(Of Double)(New Double(HistoryLength - 1){})
            For i As Integer = 1 To HistoryLength - 1
                newPriceHistory(i - 1) = If(i < stockItem.PriceHistory.Count, stockItem.PriceHistory(i), 0)
            Next i
            newPriceHistory(HistoryLength - 1) = stockItem.Price
            stockItem.PriceHistory = newPriceHistory
        End Sub
    End Class

    <XmlRoot("StockItems")>
    Public Class StockItems
        Inherits List(Of StockItem)

        Public Shared Function GetData() As List(Of StockItem)
            Dim s As New XmlSerializer(GetType(StockItems))
            Dim result As List(Of StockItem) = Nothing
            Dim path = DataDirectoryHelper.GetFile("StockSource.xml", DataDirectoryHelper.DataFolderName)
            Using fs As FileStream = File.OpenRead(path)
                result = DirectCast(s.Deserialize(fs), List(Of StockItem))
            End Using
            Return result
        End Function
    End Class

    Public Class StockItem
        Public Property CompanyName() As String
        Public Property Price() As Double
        Public Property Volume() As Integer
        Public Property LowPrice() As Double
        Public Property HighPrice() As Double
    End Class

    Public Class HierarchicalStockItem
        Private Property itemInfoSource() As List(Of StockItemInfo)
        Public Sub New()
            StockItemInfo = ViewModelSource.Create(Of StockItemInfo)()
            itemInfoSource = New List(Of StockItemInfo)() From {StockItemInfo}
        End Sub
        Public Sub New(ByVal companyName As String, ByVal price As Double, ByVal volume As Integer, ByVal lowPrice As Double, ByVal highPrice As Double)
            Me.New()
            StockItemInfo.CompanyName = companyName
            StockItemInfo.Price = price
            StockItemInfo.Volume = volume
            StockItemInfo.LowPrice = lowPrice
            StockItemInfo.HighPrice = highPrice
        End Sub
        Public Property StockItemInfo() As StockItemInfo
        Public ReadOnly Property StockItemInfoSource() As List(Of StockItemInfo)
            Get
                Return itemInfoSource
            End Get
        End Property
    End Class
    <POCOViewModel>
    Public Class StockItemInfo
        Public Overridable Property CompanyName() As String
        Public Overridable Property Price() As Double
        Public Overridable Property Volume() As Integer
        Public Overridable Property LowPrice() As Double
        Public Overridable Property HighPrice() As Double
        Public Overridable Property DeltaPrice() As Double
        Public Overridable Property DeltaPricePercent() As Double
        Public Overridable Property DeltaChange() As Integer

        Public Sub New()
            PriceHistory = New List(Of Double)()
        End Sub
        Public Overridable Property PriceHistory() As List(Of Double)
    End Class
End Namespace
