Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.POCO
Imports DockingDemo.Helpers

Namespace DockingDemo.ViewModels
    Public Class ColorizedTabsViewModel
        Public Shared Function Create() As ColorizedTabsViewModel
            Return ViewModelSource.Create(Function() New ColorizedTabsViewModel())
        End Function
        Protected Sub New()
            If Me.IsInDesignMode() Then
                Return
            End If
            InitDataSource()
        End Sub

        Public Overridable Property Employees() As List(Of EmployeeWrapper)
        Private Sub InitDataSource()
            Employees = NWindContext.Create().Employees.ToList().Select(Function(x) EmployeeWrapper.Create(x)).ToList()
            Dim i As Integer = 0

            For Each employee_Renamed In Employees
                employee_Renamed.BackgroundColor = ColorPalette.GetColor(i)
                i += 1
            Next employee_Renamed
        End Sub

    End Class
    Public Class EmployeeWrapper

        Public Shared Function Create(ByVal employee_Renamed As DevExpress.DemoData.Models.Employee) As EmployeeWrapper
            Return ViewModelSource.Create(Function() New EmployeeWrapper(employee_Renamed))
        End Function

        Protected Sub New(ByVal employee_Renamed As DevExpress.DemoData.Models.Employee)
            Me.Employee = employee_Renamed
        End Sub

        Private privateEmployee As DevExpress.DemoData.Models.Employee
        Public Property Employee() As DevExpress.DemoData.Models.Employee
            Get
                Return privateEmployee
            End Get
            Private Set(ByVal value As DevExpress.DemoData.Models.Employee)
                privateEmployee = value
            End Set
        End Property

        Public Overridable Property BackgroundColor() As Color
    End Class
End Namespace
