Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports DevExpress.Utils
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports DevExpress.Xpf.Charts
Imports System.Windows.Threading

Namespace SpreadsheetDemo
    Partial Public Class RangeAsDataSource
        Inherits SpreadsheetDemoModule

        Private updateLocked As Boolean = False
        Private operation As DispatcherOperation = Nothing

        Public Sub New()
            InitializeComponent()
            BindChartToWorksheetRange()
        End Sub

        Private Sub BindChartToWorksheetRange()
            Dim workbook As IWorkbook = Me.spreadsheetControl1.Document
            Dim sheet As Worksheet = workbook.Worksheets(0)

            Me.chartControl1.DataSource = sheet("B3:D103").GetDataSource()
            Dim series As Series = Me.chartControl1.Diagram.Series(0)
            series.ArgumentDataMember = "Column 0"
            series.ValueDataMember = "Column 1"
            series = Me.chartControl1.Diagram.Series(1)
            series.ArgumentDataMember = "Column 0"
            series.ValueDataMember = "Column 2"
        End Sub

        Private Sub trbMean_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If Not updateLocked Then
                UpdateValueAsync(AddressOf UpdateMean)
            End If
        End Sub

        Private Sub trbStdDev_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If Not updateLocked Then
                UpdateValueAsync(AddressOf UpdateStdDev)
            End If
        End Sub

        Private Sub spreadsheetControl1_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs)
            updateLocked = True
            Try
                If e.Cell.GetReferenceA1() = "F3" Then
                    trbMean.Value = e.Cell.Value.NumericValue
                ElseIf e.Cell.GetReferenceA1() = "F6" Then
                    trbStdDev.Value = e.Cell.Value.NumericValue * 100
                End If
            Finally
                updateLocked = False
            End Try
        End Sub

        Private Sub UpdateValueAsync(ByVal action As Action)
            If operation Is Nothing OrElse operation.Status = DispatcherOperationStatus.Aborted OrElse operation.Status = DispatcherOperationStatus.Completed Then
                operation = Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, action)
            End If
        End Sub

        Private Sub UpdateMean()
            spreadsheetControl1.ActiveWorksheet("F3").Value = trbMean.Value
        End Sub

        Private Sub UpdateStdDev()
            spreadsheetControl1.ActiveWorksheet("F6").Value = trbStdDev.Value / 100.0
        End Sub
    End Class
End Namespace
