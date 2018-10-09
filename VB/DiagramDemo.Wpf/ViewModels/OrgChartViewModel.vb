Imports System
Imports DevExpress.Mvvm
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Diagram.Demos

Namespace DiagramDemo
    Public Class OrgChartViewModel
        Inherits ViewModelBase

        Public Event SelectedTemplateChanged As EventHandler

        Private privateTemplates As EmployeeTemplate()
        Public Property Templates() As EmployeeTemplate()
            Get
                Return privateTemplates
            End Get
            Private Set(ByVal value As EmployeeTemplate())
                privateTemplates = value
            End Set
        End Property
        Public Overridable Property SelectedTemplate() As EmployeeTemplate

        Private privateEmployees As Employee()
        Public Property Employees() As Employee()
            Get
                Return privateEmployees
            End Get
            Private Set(ByVal value As Employee())
                privateEmployees = value
            End Set
        End Property
        Public Overridable Property SelectedEmployee() As Employee

        Public Sub New(ByVal employees() As Employee, ByVal templates() As EmployeeTemplate)
            Me.Employees = employees
            Me.Templates = templates
            SelectedTemplate = Me.Templates.FirstOrDefault()
        End Sub

        Protected Sub OnSelectedTemplateChanged()
            RaiseEvent SelectedTemplateChanged(Me, EventArgs.Empty)
        End Sub
    End Class
    Public Class EmployeeTemplate
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

        Public Sub New(ByVal name As String, ByVal image As Object)
            Me.Name = name
            Me.Image = image
        End Sub
    End Class
End Namespace
