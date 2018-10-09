Imports System
Imports System.Globalization
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.XtraSpreadsheet.Demos

Namespace SpreadsheetDemo
    Partial Public Class ConditionalFormatting
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            UpdateConditionalFormatting()
        End Sub

        Private Sub OnCheckedChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            UpdateConditionalFormatting()
        End Sub

        Private Sub UpdateConditionalFormatting()
            If spreadsheetControl1 Is Nothing Then
                Return
            End If

            spreadsheetControl1.BeginUpdate()
            Try
                Dim sheet As Worksheet = spreadsheetControl1.ActiveWorksheet
                sheet.ConditionalFormattings.Clear()
                If chkImports.IsChecked = True Then
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyTopImportsConditionalFormatting(sheet)
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyImportsYearlyChangeConditionalFormatting(sheet)
                End If
                If chkExports.IsChecked = True Then
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyTopExportsConditionalFormatting(sheet)
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyExportsYearlyChangeConditionalFormatting(sheet)
                End If
                If chkBalance.IsChecked = True Then
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyBalanceTrendConditionalFormatting(sheet)
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyBalanceChangeConditionalFormatting(sheet)
                End If
                If chkAsiaRegion.IsChecked = True Then
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyAsiaCountriesConditionalFormatting(sheet)
                End If
            Finally
                spreadsheetControl1.EndUpdate()
            End Try
        End Sub
    End Class
End Namespace
