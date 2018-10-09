Imports System
Imports System.Collections.Generic
Imports DevExpress.XtraRichEdit.Services

Namespace RichEditDemo
    Public Class UserService
        Implements IUserListService


        Private ReadOnly users_Renamed As New List(Of String)()

        Public ReadOnly Property Users() As List(Of String)
            Get
                Return users_Renamed
            End Get
        End Property

        Private Function IUserListService_GetUsers() As IList(Of String) Implements IUserListService.GetUsers
            Return Users
        End Function
        Public Sub Update(ByVal userList As List(Of String))
            Me.users_Renamed.Clear()
            Me.users_Renamed.AddRange(userList)
        End Sub
    End Class
End Namespace
