Imports DevExpress.DemoData.Models
Imports System
Imports System.Collections.Generic
Imports System.Dynamic

Namespace GridDemo
    Public NotInheritable Class SalesByYearData

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property Data() As List(Of ExpandoObject)
            Get

                Dim data_Renamed = New List(Of ExpandoObject)()
                Dim random As New Random()
                For yearIndex As Integer = 10 To 1 Step -1
                    Dim year As Integer = Date.Now.Year - yearIndex
                    For month As Integer = 1 To 12
                        Dim row As IDictionary(Of String, Object) = New ExpandoObject()
                        row("Date") = New Date(year, month, 1)
                        For columnIndex As Integer = 0 To Columns.Count - 1
                            row(Columns(columnIndex).PropertyName) = random.Next(30000)
                        Next columnIndex
                        data_Renamed.Add(DirectCast(row, ExpandoObject))
                    Next month
                Next yearIndex
                Return data_Renamed
            End Get
        End Property
        Private Shared privateColumns As List(Of ColumnDescription)
        Public Shared Property Columns() As List(Of ColumnDescription)
            Get
                Return privateColumns
            End Get
            Private Set(ByVal value As List(Of ColumnDescription))
                privateColumns = value
            End Set
        End Property
        Shared Sub New()

            Dim columns_Renamed = New List(Of ColumnDescription)()
            For Each employee In NWindContext.Create().Employees
                Dim name As String = employee.FirstName & " " & employee.LastName
                columns_Renamed.Add(New ColumnDescription(name))
            Next employee
            Columns = columns_Renamed
        End Sub
    End Class
End Namespace
