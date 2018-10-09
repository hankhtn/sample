Imports DevExpress.Xpf.DemoBase
Imports PivotGridDemo.Helpers
Imports System
Imports System.Collections.Generic
Imports System.Data.SQLite
Imports System.Linq
Imports System.Text

Namespace PivotGridDemo
    Public Class ServerModeViewModel
        Inherits ServerModeViewModelBase(Of OrderDataRecord)

        #Region "DatabaseInfo"
        Public Shared ReadOnly DatabaseInfo As New DatabaseInfo(OrderDataContext.FileName, "OrderDataRecords", GetType(OrderDataRecord), AddressOf CreateValues, Function(sql, connection) New SQLiteCommand(sql, CType(connection, SQLiteConnection)))
        Private Shared Function CreateValues() As Dictionary(Of String, Object)
            Dim dict = New Dictionary(Of String, Object)()
            Dim product = DatabaseHelper.GetProduct()
            dict.Add("OrderDate", DatabaseHelper.GetOrderDate())
            dict.Add("Quantity", DatabaseHelper.GetQuantity())
            dict.Add("UnitPrice", DatabaseHelper.GetProductPrice(product))
            dict.Add("CustomerName", DatabaseHelper.GetCustomerName())
            dict.Add("ProductName", product.ProductName)
            dict.Add("CategoryName", product.CategoryName)
            dict.Add("SalesPersonName", DatabaseHelper.GetSalesPersonName())
            Return dict
        End Function
        #End Region

        Protected Sub New()
            MyBase.New(DatabaseInfo, Function() (New OrderDataContext()).OrderDataRecords)
        End Sub
    End Class
End Namespace
