Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Linq
Imports System.Text

Namespace GridDemo
    Public NotInheritable Class DevAVHelper

        Private Sub New()
        End Sub


        Private Shared employees_Renamed As List(Of DevExpress.DevAV.Employee) = Nothing
        Friend Shared ReadOnly Property Employees() As List(Of DevExpress.DevAV.Employee)
            Get
                If employees_Renamed Is Nothing Then
                    Dim devAvDb = New DevExpress.DevAV.DevAVDbContext()
                    devAvDb.Employees.Load()
                    employees_Renamed = devAvDb.Employees.Local.ToList()
                End If
                Return employees_Renamed
            End Get
        End Property
        Friend Shared Function GetEmployee(ByVal email As String) As DevExpress.DevAV.Employee
            Return Employees.SingleOrDefault(Function(x) x.Email.Equals(email))
        End Function
    End Class
End Namespace
