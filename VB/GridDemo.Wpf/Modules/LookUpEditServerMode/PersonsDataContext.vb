Imports DevExpress.Xpf.DemoBase
Imports System.Data.Entity
Imports System.IO

Namespace GridDemo
    Public Class PersonsDataContext
        Inherits DbContext

        Public Shared ReadOnly FileName As String = Path.GetFullPath("PersonData.db")
        Public Sub New()
            MyBase.New(SQLiteDataBaseGenerator.CreateConnection(LookUpInstantFeedbackModeViewModel.DatabaseInfo), True)
        End Sub
        Public Property People() As DbSet(Of Person)
    End Class
    Public Class Person
        Public Property Id() As Integer
        Public Property FullName() As String
        Public Property Company() As String
        Public Property JobTitle() As String
        Public Property City() As String
        Public Property Address() As String
        Public Property Phone() As String
        Public Property Email() As String
    End Class
End Namespace
