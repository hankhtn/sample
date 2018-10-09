Imports System
Imports System.Collections.Generic
Imports System.Linq


Namespace DevExpress.Diagram.Demos
    Public NotInheritable Class RelationshipsData

        Private Sub New()
        End Sub

        Public Shared Function GetEmployees() As Employee()
            Return EmployeesData.FilteredEmployees.Take(9).ToArray()
        End Function
        Public Shared Function GetRelationships(ByVal employees() As Employee) As RelationshipInfo()
            Dim index As Integer = 0
            Dim relations = New List(Of RelationshipInfo)()
            For i As Integer = 0 To employees.Length - 1
                For j As Integer = i + 1 To employees.Length - 1
                    If (index Mod 4) <= 1 Then
                        relations.Add(New RelationshipInfo(employees(i), employees(j), CType(index Mod 4, EmployeeRelationship)))
                    End If
                    index += 1
                Next j
            Next i
            Return relations.ToArray()
        End Function
        Public Shared Function GetEmployeeRelationships(ByVal employee As Employee, ByVal relationships As IEnumerable(Of RelationshipInfo), ByVal relationshipKind As EmployeeRelationship) As Employee()
            Return relationships.Where(Function(x) x.Relationship = relationshipKind).Select(Function(x) GetRelationship(employee, x)).Where(Function(x) x IsNot Nothing).ToArray()
        End Function
        Private Shared Function GetRelationship(ByVal employee As Employee, ByVal x As RelationshipInfo) As Employee
            If x.Source Is employee Then
                Return x.Target
            End If
            If x.Target Is employee Then
                Return x.Source
            End If
            Return Nothing
        End Function
    End Class
    Public Class RelationshipInfo
        Public Sub New(ByVal source As Employee, ByVal target As Employee, ByVal relationship As EmployeeRelationship)
            Me.Source = source
            Me.Target = target
            Me.Relationship = relationship
        End Sub

        Private privateSource As Employee
        Public Property Source() As Employee
            Get
                Return privateSource
            End Get
            Private Set(ByVal value As Employee)
                privateSource = value
            End Set
        End Property
        Private privateTarget As Employee
        Public Property Target() As Employee
            Get
                Return privateTarget
            End Get
            Private Set(ByVal value As Employee)
                privateTarget = value
            End Set
        End Property
        Private privateRelationship As EmployeeRelationship
        Public Property Relationship() As EmployeeRelationship
            Get
                Return privateRelationship
            End Get
            Private Set(ByVal value As EmployeeRelationship)
                privateRelationship = value
            End Set
        End Property
    End Class

    Public Enum EmployeeRelationship
        KnowEachOther
        Friends
    End Enum
End Namespace
