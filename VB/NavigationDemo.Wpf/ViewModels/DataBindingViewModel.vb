Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports NavigationDemo.Utils

Namespace NavigationDemo
    Public Class DataBindingViewModel
        Private privateDepartments As ReadOnlyCollection(Of EmployeeDepartment)
        Public Property Departments() As ReadOnlyCollection(Of EmployeeDepartment)
            Get
                Return privateDepartments
            End Get
            Private Set(ByVal value As ReadOnlyCollection(Of EmployeeDepartment))
                privateDepartments = value
            End Set
        End Property
        Public Overridable Property SelectedItem() As Object

        Public Sub New()

            Dim departments_Renamed = EmployeesWithPhotoData.DataSource.GroupBy(Function(x) x.GroupName).Select(Function(x) CreateEmployeeDepartment(x.Key, x.Take(10).ToArray())).ToArray()
            Departments = New ReadOnlyCollection(Of EmployeeDepartment)(departments_Renamed)
            SelectedItem = Departments(1).Employees(0)
        End Sub

        Private Shared Function CreateEmployeeDepartment(ByVal name As String, ByVal employees As IList(Of Employee)) As EmployeeDepartment
            Dim image = ImageHelper.GetEmployeeDepartmentImage(name)
            Return New EmployeeDepartment(name, image, employees)
        End Function
    End Class
    Public Class EmployeeDepartment
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Private Set(ByVal value As String)
                privateName = value
            End Set
        End Property
        Private privateImage As Object
        Public Property Image() As Object
            Get
                Return privateImage
            End Get
            Private Set(ByVal value As Object)
                privateImage = value
            End Set
        End Property
        Private privateEmployees As ReadOnlyCollection(Of Employee)
        Public Property Employees() As ReadOnlyCollection(Of Employee)
            Get
                Return privateEmployees
            End Get
            Private Set(ByVal value As ReadOnlyCollection(Of Employee))
                privateEmployees = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal image As Object, ByVal employees As IList(Of Employee))
            Me.Name = name
            Me.Image = image
            Me.Employees = New ReadOnlyCollection(Of Employee)(employees)
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
