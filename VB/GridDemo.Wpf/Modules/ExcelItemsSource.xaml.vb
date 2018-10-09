Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Spreadsheet
Imports System
Imports System.Windows

Namespace GridDemo
    Partial Public Class ExcelItemsSource
        Inherits GridDemoModule

        Public Shared ReadOnly SourceUri As New Uri("pack://application:,,,/GridDemo;component/Data/Orders.xls")
        Public Shared Source As New SpreadsheetDocumentSource(Application.GetResourceStream(SourceUri).Stream, DocumentFormat.Xls)

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
