Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace GridDemo
    <POCOViewModel>
    Public Class ValidationViewModel
        Private Const RowCount As Integer = 8
        Private Shared Function GetValidationData() As IEnumerable(Of ValidationData)
            Dim result = New List(Of ValidationData)()
            For Each employee As Employee In EmployeesData.DataSource
                Dim data = New ValidationData() With {.FirstName = employee.FirstName, .LastName = employee.LastName, .Email = employee.EmailAddress, .Address = employee.AddressLine1, .Phone = employee.Phone, .Title = employee.JobTitle, .ZipCode = employee.PostalCode, .HireDate = employee.HireDate, .Salary = DataGenerator.GetSalary(), .Facebook = DataGenerator.GetFacebookAddress(employee.FirstName, employee.LastName), .CreditCard = DataGenerator.GetCreditCardNumber()}
                result.Add(data)
                If result.Count > RowCount Then
                    Exit For
                End If
            Next employee
            result(GetRandomRowIndex()).FirstName = Nothing
            result(GetRandomRowIndex()).FirstName = Nothing
            result(GetRandomRowIndex()).LastName = Nothing
            result(GetRandomRowIndex()).LastName = Nothing
            result(GetRandomRowIndex()).HireDate = Date.Today.AddDays(2)
            result(GetRandomRowIndex()).HireDate = Date.Today.AddDays(3)
            result(GetRandomRowIndex()).Salary = -10000
            result(GetRandomRowIndex()).Salary = 1000001
            result(GetRandomRowIndex()).CreditCard = "0000 0000 0000 0000"
            result(GetRandomRowIndex()).CreditCard = "1234 1234 1234 1234"
            result(GetRandomRowIndex()).Address = Nothing
            result(GetRandomRowIndex()).Address = Nothing
            result(GetRandomRowIndex()).ZipCode = "000"
            result(GetRandomRowIndex()).ZipCode = "123"
            result(GetRandomRowIndex()).Phone = "555 1234"
            result(GetRandomRowIndex()).Phone = "(00o)555 1234"
            Dim dataItem = result(GetRandomRowIndex())
            dataItem.Email = dataItem.Email.Replace("@", " ")
            dataItem = result(GetRandomRowIndex())
            dataItem.Email = dataItem.Email.Replace("com", "")
            dataItem = result(GetRandomRowIndex())
            dataItem.Facebook = dataItem.Email
            dataItem = result(GetRandomRowIndex())
            dataItem.Facebook = dataItem.Facebook.Replace("com", "")
            Return result
        End Function
        Private Shared Function GetValidationData_FluentAPI() As IEnumerable(Of ValidationData_FluentAPI)
            Dim result = New List(Of ValidationData_FluentAPI)()
            For Each employee As Employee In EmployeesData.DataSource
                Dim data = New ValidationData_FluentAPI() With {.FirstName = employee.FirstName, .LastName = employee.LastName, .Email = employee.EmailAddress, .Address = employee.AddressLine1, .Phone = employee.Phone, .Title = employee.JobTitle, .ZipCode = employee.PostalCode, .HireDate = employee.HireDate, .Salary = DataGenerator.GetSalary(), .Facebook = DataGenerator.GetFacebookAddress(employee.FirstName, employee.LastName), .CreditCard = DataGenerator.GetCreditCardNumber()}
                result.Add(data)
                If result.Count > RowCount Then
                    Exit For
                End If
            Next employee
            result(GetRandomRowIndex()).FirstName = Nothing
            result(GetRandomRowIndex()).FirstName = Nothing
            result(GetRandomRowIndex()).LastName = Nothing
            result(GetRandomRowIndex()).LastName = Nothing
            result(GetRandomRowIndex()).HireDate = Date.Today.AddDays(2)
            result(GetRandomRowIndex()).HireDate = Date.Today.AddDays(3)
            result(GetRandomRowIndex()).Salary = -10000
            result(GetRandomRowIndex()).Salary = 1000001
            result(GetRandomRowIndex()).CreditCard = "0000 0000 0000 0000"
            result(GetRandomRowIndex()).CreditCard = "1234 1234 1234 1234"
            result(GetRandomRowIndex()).Address = Nothing
            result(GetRandomRowIndex()).Address = Nothing
            result(GetRandomRowIndex()).ZipCode = "000"
            result(GetRandomRowIndex()).ZipCode = "123"
            result(GetRandomRowIndex()).Phone = "555 1234"
            result(GetRandomRowIndex()).Phone = "(00o)555 1234"
            Dim dataItem = result(GetRandomRowIndex())
            dataItem.Email = dataItem.Email.Replace("@", " ")
            dataItem = result(GetRandomRowIndex())
            dataItem.Email = dataItem.Email.Replace("com", "")
            dataItem = result(GetRandomRowIndex())
            dataItem.Facebook = dataItem.Email
            dataItem = result(GetRandomRowIndex())
            dataItem.Facebook = dataItem.Facebook.Replace("com", "")
            Return result
        End Function
        Private Shared ReadOnly random As New Random()
        Private Shared Function GetRandomRowIndex() As Integer
            Return random.Next(RowCount)
        End Function

        Protected Sub New()
            Items = New List(Of CollectionInfo) From {
                New CollectionInfo(GetValidationData(), "Validation via Data Annotation attributes"),
                New CollectionInfo(GetValidationData_FluentAPI(), "Validation via Fluent API")
            }
            SelectedCollectionInfo = Items.First()
        End Sub

        Private privateItems As List(Of CollectionInfo)
        Public Property Items() As List(Of CollectionInfo)
            Get
                Return privateItems
            End Get
            Private Set(ByVal value As List(Of CollectionInfo))
                privateItems = value
            End Set
        End Property
        Public Overridable Property SelectedCollectionInfo() As CollectionInfo
    End Class
End Namespace
