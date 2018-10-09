Imports System
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraSpreadsheet.Services
Imports DevExpress.XtraSpreadsheet.Commands

Namespace SpreadsheetDemo
    Partial Public Class PrintTitles
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub btnPageSetup_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim service As ISpreadsheetCommandFactoryService = DirectCast(spreadsheetControl1.GetService(GetType(ISpreadsheetCommandFactoryService)), ISpreadsheetCommandFactoryService)
            Dim command As SpreadsheetCommand = service.CreateCommand(SpreadsheetCommandId.PageSetupSheet)
            command.ForceExecute(command.CreateDefaultCommandUIState())
        End Sub
        Private Sub btnPrintPreview_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim service As ISpreadsheetCommandFactoryService = DirectCast(spreadsheetControl1.GetService(GetType(ISpreadsheetCommandFactoryService)), ISpreadsheetCommandFactoryService)
            Dim command As SpreadsheetCommand = service.CreateCommand(SpreadsheetCommandId.FilePrintPreview)
            command.ForceExecute(command.CreateDefaultCommandUIState())
        End Sub
    End Class
End Namespace
