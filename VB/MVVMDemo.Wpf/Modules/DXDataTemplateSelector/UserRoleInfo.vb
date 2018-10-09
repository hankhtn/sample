Imports System
Imports System.Linq

Namespace MVVMDemo.DXDataTemplateSelector
    Public Class UserRoleInfo
        #Region "UserRoles"
        Public Shared ReadOnly Property UserRoles() As UserRoleInfo()
            Get
                Dim roles() As UserRole = System.Enum.GetValues(GetType(UserRole)).Cast(Of UserRole)().ToArray()
                Return PersonInfo.Persons.Select(Function(person, i) New UserRoleInfo() With {.FullName = person.FullName, .UserRole = roles(i)}).ToArray()
            End Get
        End Property
        #End Region
        Public Property FullName() As String
        Public Property UserRole() As UserRole
        Public Function IsAdmin() As Boolean
            Return UserRole = UserRole.Admin
        End Function
    End Class
    Public Enum UserRole
        Admin
        Moderator
        User
    End Enum
End Namespace
