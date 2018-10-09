Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Office
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Spreadsheet
Imports DevExpress.Xpf.Core

Namespace SpreadsheetDemo
    Partial Public Class FirstLook
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub spreadsheetControl1_InvalidFormatException(ByVal sender As Object, ByVal e As SpreadsheetInvalidFormatExceptionEventArgs)
            DXMessageBox.Show(String.Format("Cannot open the file '{0}' because the file format or file extension is not valid." & ControlChars.Lf & "Verify that file has not been corrupted and that the file extension matches the format of the file.", e.SourceUri), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Sub

        Private Sub spreadsheetControl1_DocumentClosing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
            If spreadsheetControl1.Modified Then
                Dim currentFileName As String = spreadsheetControl1.Options.Save.CurrentFileName
                Dim message As String = If((Not String.IsNullOrEmpty(currentFileName)), String.Format("Do you want to save the changes you made for '{0}'?", currentFileName), "Do you want to save the changes?")
                Dim result As MessageBoxResult = DXMessageBox.Show(message, "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning)
                If result = MessageBoxResult.Yes Then
                    spreadsheetControl1.SaveDocument()
                End If
                e.Cancel = result = MessageBoxResult.Cancel
            End If
        End Sub

        Private Sub spreadsheetControl1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim count As Integer = 0
            Dim sum As Double = 0.0
            Dim numericCount As Integer = 0
            Dim sheet As Worksheet = spreadsheetControl1.ActiveWorksheet
            Dim selectedCells As DevExpress.Spreadsheet.Range = sheet.Selection.Intersect(sheet.GetDataRange())
            If selectedCells IsNot Nothing Then
                For Each cell As Cell In selectedCells.ExistingCells
                    count += 1
                    If cell.Value.IsNumeric Then
                        numericCount += 1
                        sum += cell.Value.NumericValue
                    End If
                Next cell
            End If
            If count <= 1 Then
                lblSelection.Content = String.Empty
            ElseIf numericCount = 0 Then
                lblSelection.Content = String.Format("Count: {0}", count)
            Else
                Dim average As Double = sum / numericCount
                lblSelection.Content = String.Format(spreadsheetControl1.Options.Culture, "Average: {0:#0.######}  Count: {1}  Sum: {2}", average, count, sum)
            End If
        End Sub
    End Class
End Namespace
