Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Reports
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.POCO
Imports DevExpress.XtraReports.UI
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Text

Namespace DevExpress.DevAV.ViewModels
    Public Class OrderQuickReportsViewModel
        Implements IDocumentContent

        Public Shared Function Create(ByVal selectedOrder As Order, ByVal format As ReportFormat) As OrderQuickReportsViewModel
            Return ViewModelSource.Create(Function() New OrderQuickReportsViewModel(selectedOrder, format))
        End Function
        Protected Sub New(ByVal selectedOrder As Order, ByVal format As ReportFormat)
            Me.Format = format
            Me.SelectedOrder = selectedOrder
        End Sub
        Public Sub Close()
            If DocumentOwner IsNot Nothing Then
                DocumentOwner.Close(Me)
            End If
        End Sub
        Private privateDocumentOwner As IDocumentOwner
        Protected Property DocumentOwner() As IDocumentOwner
            Get
                Return privateDocumentOwner
            End Get
            Private Set(ByVal value As IDocumentOwner)
                privateDocumentOwner = value
            End Set
        End Property

        Public Overridable Property SelectedOrder() As Order
        Public Property Format() As ReportFormat

        Public Overridable Property DocumentStream() As Stream
        Public Overridable Property DocumentDataSource() As Tuple(Of IDevAVDbUnitOfWork, Order)

        Public Overridable Sub OnLoaded()

            Dim documentStream_Renamed = New MemoryStream()
            Dim report = ReportFactory.SalesInvoice(SelectedOrder, True, False, False, False)
            Select Case Format
                Case ReportFormat.Pdf
                    report.ExportToPdf(documentStream_Renamed)
                Case ReportFormat.Xls
                    report.ExportToXls(documentStream_Renamed)
                Case ReportFormat.Doc
                    Dim options = New XtraPrinting.DocxExportOptions()
                    options.ExportMode = XtraPrinting.DocxExportMode.SingleFilePageByPage
                    options.TableLayout = True
                    report.ExportToDocx(documentStream_Renamed, options)
            End Select
            DocumentDataSource = New Tuple(Of IDevAVDbUnitOfWork, Order)(Nothing, SelectedOrder)
            DocumentStream = documentStream_Renamed
        End Sub
        #Region "IDocumentContent"
        Private Sub IDocumentContent_OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
            DocumentStream.Dispose()
        End Sub
        Private Sub IDocumentContent_OnDestroy() Implements IDocumentContent.OnDestroy
        End Sub
        Private Property IDocumentContent_DocumentOwner() As IDocumentOwner Implements IDocumentContent.DocumentOwner
            Get
                Return DocumentOwner
            End Get
            Set(ByVal value As IDocumentOwner)
                DocumentOwner = value
            End Set
        End Property
        Private ReadOnly Property IDocumentContent_Title() As Object Implements IDocumentContent.Title
            Get
                Return String.Format("Order - {0}", SelectedOrder.InvoiceNumber)
            End Get
        End Property
        #End Region
    End Class
    Public Enum ReportFormat
        Pdf
        Xls
        Doc
    End Enum
End Namespace
