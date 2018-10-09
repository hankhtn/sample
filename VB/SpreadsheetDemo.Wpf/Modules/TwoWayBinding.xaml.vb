Imports DevExpress.Spreadsheet
Imports System

Namespace SpreadsheetDemo
    Partial Public Class TwoWayBinding
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            BindGridToWorksheetRange()
        End Sub

        Private Sub BindGridToWorksheetRange()
            Dim workbook As IWorkbook = Me.spreadsheet.Document
            Dim sheet As Worksheet = workbook.Worksheets(0)
            Dim expenses As Table = sheet.Tables(0)
            Dim options As New RangeDataSourceOptions()
            options.PreserveFormulas = True
            options.SkipHiddenRows = True
            grid.ItemsSource = expenses.DataRange.GetDataSource(options)
        End Sub
    End Class
End Namespace
