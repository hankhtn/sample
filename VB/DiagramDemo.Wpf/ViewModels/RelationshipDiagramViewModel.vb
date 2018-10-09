Imports System
Imports DevExpress.Mvvm
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Diagram.Demos
Imports DevExpress.Mvvm.DataAnnotations

Namespace DiagramDemo
    Public Class RelationshipDiagramViewModel
        Inherits ViewModelBase

        Private privateEmployees As Employee()
        Public Property Employees() As Employee()
            Get
                Return privateEmployees
            End Get
            Private Set(ByVal value As Employee())
                privateEmployees = value
            End Set
        End Property
        Private privateRelationships As RelationshipInfo()
        Public Property Relationships() As RelationshipInfo()
            Get
                Return privateRelationships
            End Get
            Private Set(ByVal value As RelationshipInfo())
                privateRelationships = value
            End Set
        End Property
        Private privateFriendsCollection As Employee()
        <BindableProperty>
        Public Overridable Property FriendsCollection() As Employee()
            Get
                Return privateFriendsCollection
            End Get
            Protected Set(ByVal value As Employee())
                privateFriendsCollection = value
            End Set
        End Property
        Private privateKnownPersonsCollection As Employee()
        <BindableProperty>
        Public Overridable Property KnownPersonsCollection() As Employee()
            Get
                Return privateKnownPersonsCollection
            End Get
            Protected Set(ByVal value As Employee())
                privateKnownPersonsCollection = value
            End Set
        End Property

        Public Overridable Property SelectedEmployee() As Employee

        Public Sub New(ByVal employees() As Employee)
            Me.Employees = RelationshipsData.GetEmployees()
            Relationships = RelationshipsData.GetRelationships(Me.Employees)
        End Sub

        Protected Sub OnSelectedEmployeeChanged()
            FriendsCollection = RelationshipsData.GetEmployeeRelationships(SelectedEmployee, Relationships, EmployeeRelationship.Friends)
            KnownPersonsCollection = RelationshipsData.GetEmployeeRelationships(SelectedEmployee, Relationships, EmployeeRelationship.KnowEachOther)
        End Sub
    End Class
End Namespace
