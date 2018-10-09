Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace GridDemo
    Public Class FixedRowsViewModel
        Inherits StockViewModelBase


        Private fixedTopRows_Renamed() As String = { "MSFT", "AAPL", "ORCL" }
        Public Property FixedTopRows() As List(Of StockItem)

        Private fixedBottomRows_Renamed() As String = { "IBM", "CSCO", "MSI", "DELL", "XRX" }
        Public Property FixedBottomRows() As List(Of StockItem)
        Public Property CurrentItem() As StockItem

        Public Sub New()
            FixedTopRows = Data.Where(Function(x) fixedTopRows_Renamed.Contains(x.CompanyName)).ToList()
            FixedBottomRows = Data.Where(Function(x) fixedBottomRows_Renamed.Contains(x.CompanyName)).ToList()
            CurrentItem = Data.FirstOrDefault(Function(x) x.CompanyName = "EBAY")
        End Sub

        Protected Overrides Function GetDeltaPrice(ByVal currentPrice As Double, ByVal dataGeneration As Boolean) As Double
            Return Random.NextDouble() * 0.5 - 0.25
        End Function
    End Class
End Namespace
