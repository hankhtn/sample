Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Dynamic
Imports System.Linq

Namespace GridDemo
    Public Class SalesByEmployeeData
        Private privateData As List(Of ExpandoObject)
        Public Property Data() As List(Of ExpandoObject)
            Get
                Return privateData
            End Get
            Private Set(ByVal value As List(Of ExpandoObject))
                privateData = value
            End Set
        End Property
        Private privateBands As BandDescription()
        Public Property Bands() As BandDescription()
            Get
                Return privateBands
            End Get
            Private Set(ByVal value As BandDescription())
                privateBands = value
            End Set
        End Property
        Private privateColumns As ColumnDescription()
        Public Property Columns() As ColumnDescription()
            Get
                Return privateColumns
            End Get
            Private Set(ByVal value As ColumnDescription())
                privateColumns = value
            End Set
        End Property
        Public Sub New()
            GenerateBandsAndColumns()
            GenerateData()
        End Sub

        Private Sub GenerateBandsAndColumns()
            Bands = Enumerable.Range(0, 10).Reverse().Select(Function(yearIndex)
                Dim yearBandName = (Date.Now.Year - yearIndex).ToString()
                Dim quarters = Enumerable.Range(1, 4).Select(Function(quarter) New ColumnDescription(yearBandName & "-Q" & quarter, "Q" & quarter)).Concat( { New ColumnDescription(yearBandName & "-YearTotal", "Year Total") }).ToArray()
                Return New BandDescription(yearBandName, quarters)
            End Function).ToArray()
            Columns = Bands.SelectMany(Function(x) x.Columns).ToArray()
        End Sub

        Private Sub GenerateData()
            Dim random As New Random()
            Data = New List(Of ExpandoObject)()
            For Each employee In EmployeesWithPhotoData.DataSource
                Dim row As IDictionary(Of String, Object) = New ExpandoObject()
                row("Employee") = employee.FirstName & " " & employee.LastName
                row("GroupName") = employee.GroupName

                Dim total = 0R
                For Each band In Bands
                    Dim yearTotal = 0R
                    For Each column In band.Columns.Take(band.Columns.Length - 1)
                        Dim value = random.Next(100000)
                        yearTotal += value
                        total += value
                        row(column.PropertyName) = value
                    Next column
                    row(band.Columns.Last().PropertyName) = yearTotal
                Next band

                row("Total") = total
                Data.Add(DirectCast(row, ExpandoObject))
            Next employee
        End Sub
    End Class
End Namespace
