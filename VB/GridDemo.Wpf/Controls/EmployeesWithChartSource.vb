Imports DevExpress.DemoData.Models
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee
Imports EmployeesWithPhotoData = DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData
Imports Order = DevExpress.DemoData.Models.Order

Namespace GridDemo
    Public NotInheritable Class EmployeesWithChartSource

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property Employees() As List(Of EmployeeWithChart)
            Get
                Dim employeesWithChart = New List(Of EmployeeWithChart)()

                Dim employees_Renamed As List(Of Employee) = EmployeesWithPhotoData.DataSource
                Dim dict As Dictionary(Of Integer, Integer) = EmployeesWithPhotoData.OrdersRelations
                Dim context As NWindContext = NWindContext.Create()
                Dim orders As List(Of Order) = context.Orders.ToList()
                Dim invoices As List(Of Invoice) = context.Invoices.ToList()
                For Each empl As Employee In employees_Renamed
                    Dim chartPoints As New List(Of ChartPoint)()
                    For Each order As Order In orders.Where(Function(o) dict(CInt((o.OrderID))) = empl.Id)
                        Dim cp As New ChartPoint()
                        cp.ArgumentMember = order.OrderDate
                        invoices.Where(Function(invoice) invoice.OrderID = order.OrderID).ToList().ForEach(Sub(invoice) cp.ValueMember += Convert.ToInt32(invoice.Quantity * invoice.UnitPrice))
                        chartPoints.Add(cp)
                    Next order
                    employeesWithChart.Add(New EmployeeWithChart(empl, chartPoints))
                Next empl
                Return employeesWithChart
            End Get
        End Property
    End Class
    Public Class EmployeeWithChart
        Public Sub New(ByVal employee As Employee, ByVal chartSource As List(Of ChartPoint))
            Me.ChartSource = chartSource
            FullName = String.Format("{0} {1}", employee.FirstName, employee.LastName)
            JobTitle = employee.JobTitle
            CountryRegionName = employee.CountryRegionName
            BirthDate = employee.BirthDate
            EmailAddress = employee.EmailAddress
        End Sub

        Private privateJobTitle As String
        Public Property JobTitle() As String
            Get
                Return privateJobTitle
            End Get
            Private Set(ByVal value As String)
                privateJobTitle = value
            End Set
        End Property
        Private privateCountryRegionName As String
        Public Property CountryRegionName() As String
            Get
                Return privateCountryRegionName
            End Get
            Private Set(ByVal value As String)
                privateCountryRegionName = value
            End Set
        End Property
        Private privateBirthDate As Date
        Public Property BirthDate() As Date
            Get
                Return privateBirthDate
            End Get
            Private Set(ByVal value As Date)
                privateBirthDate = value
            End Set
        End Property
        Private privateEmailAddress As String
        Public Property EmailAddress() As String
            Get
                Return privateEmailAddress
            End Get
            Private Set(ByVal value As String)
                privateEmailAddress = value
            End Set
        End Property

        Private privateFullName As String
        Public Property FullName() As String
            Get
                Return privateFullName
            End Get
            Private Set(ByVal value As String)
                privateFullName = value
            End Set
        End Property
        Private privateChartSource As List(Of ChartPoint)
        Public Property ChartSource() As List(Of ChartPoint)
            Get
                Return privateChartSource
            End Get
            Private Set(ByVal value As List(Of ChartPoint))
                privateChartSource = value
            End Set
        End Property
    End Class
    Public Class ChartPoint
        Private privateArgumentMember? As Date
        Public Property ArgumentMember() As Date?
            Get
                Return privateArgumentMember
            End Get
            Friend Set(ByVal value? As Date)
                privateArgumentMember = value
            End Set
        End Property
        Public Property ValueMember() As Integer
    End Class

End Namespace
