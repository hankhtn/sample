Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData.Models
Imports DevExpress.Office.Services

Namespace RichEditDemo
    Partial Public Class MailMerge
        Inherits RichEditDemoModule

        Private employees As IList(Of Employee) = NWindContext.Create().Employees.ToList()

        Public Sub New()
            InitializeComponent()
            Dim uriService As IUriStreamService = Me.richEdit.GetService(Of IUriStreamService)()
            uriService.RegisterProvider(New DBUriStreamProvider(employees))

            Dim dataSource = CreateDataSource()
            Me.gridControl1.ItemsSource = dataSource
            Me.richEdit.MailMergeOptions.DataSource = dataSource
            AddHandler Me.gridControl1.View.FocusedRowHandleChanged, AddressOf View_FocusedRowChanged
        End Sub

        Private Function CreateDataSource() As IEnumerable(Of Object)
            Return (
                From e In NWindContext.Create().Employees , c In e.Customers
                Select New With {Key .EmployeeID = e.EmployeeID, Key .LastName = e.LastName, Key .FirstName = e.FirstName, Key .Title = e.Title, Key .TitleOfCourtesy = e.TitleOfCourtesy, Key .BirthDate = e.BirthDate, Key .HireDate = e.HireDate, Key .Employees = New With {Key .Address = e.Address, Key .City = e.City, Key .Region = e.Region, Key .PostalCode = e.PostalCode, Key .Country = e.Country}, Key .HomePhone = e.HomePhone, Key .Extension = e.Extension, Key .Photo = e.Photo, Key .Notes = e.Notes, Key .ReportsTo = e.ReportsTo, Key .Email = e.Email, Key .CustomerID = c.CustomerID, Key .CompanyName = c.CompanyName, Key .ContactName = c.ContactName, Key .ContactTitle = c.ContactTitle, Key .Customers = New With {Key .Address = c.Address, Key .City = c.City, Key .Region = c.Region, Key .PostalCode = c.PostalCode, Key .Country = c.Country}, Key .Phone = c.Phone, Key .Fax = c.Fax}).ToList()
        End Function
        Private Sub View_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.FocusedRowHandleChangedEventArgs)
            RichEditControl.MailMergeOptions.ActiveRecord = Me.gridControl1.GetListIndexByRowHandle(e.RowData.RowHandle.Value)
        End Sub
    End Class
End Namespace
