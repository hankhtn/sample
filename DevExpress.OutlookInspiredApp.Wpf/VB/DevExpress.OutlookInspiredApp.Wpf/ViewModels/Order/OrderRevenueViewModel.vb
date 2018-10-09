Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports DevExpress.DevAV.Reports
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.XtraReports.UI

Namespace DevExpress.DevAV.ViewModels
    Public Class OrderRevenueViewModel
        Implements IDocumentContent

        Public Shared Function Create(ByVal selectedItems As IEnumerable(Of OrderItem), ByVal format As RevenueReportFormat) As OrderRevenueViewModel
            Return ViewModelSource.Create(Function() New OrderRevenueViewModel(selectedItems, format))
        End Function
        Protected Sub New(ByVal selectedItems As IEnumerable(Of OrderItem), ByVal format As RevenueReportFormat)
            Me.Format = format
            Me.SelectedItems = selectedItems
        End Sub
        Public Sub Close()
            If DocumentOwner IsNot Nothing Then
                DocumentOwner.Close(Me)
            End If
        End Sub
        Protected ReadOnly Property DocumentManagerService() As IDocumentManagerService
            Get
                Return Me.GetService(Of IDocumentManagerService)()
            End Get
        End Property

        Private privateDocumentOwner As IDocumentOwner
        Protected Property DocumentOwner() As IDocumentOwner
            Get
                Return privateDocumentOwner
            End Get
            Private Set(ByVal value As IDocumentOwner)
                privateDocumentOwner = value
            End Set
        End Property

        Public Property Format() As RevenueReportFormat
        Public Property SelectedItems() As IEnumerable(Of OrderItem)
        Public Overridable Property Report() As XtraReport

        Public Overridable Sub OnLoaded()
            Xpf.DemoBase.Helpers.Logger.Log(String.Format("OutlookInspiredApp: Create Report : Sales: Revenue{0}", Format.ToString()))
            Report = CreateReport()
            Report.CreateDocument()
        End Sub

        Public Sub ShowDesigner()
            Dim viewModel = ReportDesignerViewModel.Create(CloneReport(Report))
            Dim doc = DocumentManagerService.CreateDocument("ReportDesignerView", viewModel, Nothing, Me)
            doc.Title = CreateTitle()
            doc.Show()

            If viewModel.IsReportSaved Then
                Report = CloneReport(viewModel.Report)
                Report.CreateDocument()
                viewModel.Report.Dispose()
            End If
        End Sub

        Private Function CloneReport(ByVal report As XtraReport) As XtraReport
            Dim clonedReport = CloneReportLayout(report)
            InitReport(clonedReport)
            Return clonedReport
        End Function
        Private Sub InitReport(ByVal report As XtraReport)
            report.DataSource = SelectedItems
            report.Parameters("paramOrderDate").Value = True
        End Sub
        Private Function CreateReport() As XtraReport
            Return If(Format = RevenueReportFormat.Summary, ReportFactory.SalesRevenueReport(SelectedItems, True), ReportFactory.SalesRevenueAnalysisReport(SelectedItems, True))
        End Function
        Private Function CreateTitle() As String
            Return String.Format("DevAV - {0}",If(Format = RevenueReportFormat.Analysis, "Revenue Analysis", "Revenue"))
        End Function
        Private Shared Function CloneReportLayout(ByVal report As XtraReport) As XtraReport
            Using stream = New MemoryStream()
                report.SaveLayoutToXml(stream)
                stream.Position = 0
                Return XtraReport.FromStream(stream, True)
            End Using
        End Function
        #Region "IDocumentContent"
        Private Sub IDocumentContent_OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
            Report.Dispose()
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
                Return CreateTitle()
            End Get
        End Property
        #End Region
    End Class
    Public Enum RevenueReportFormat
        Summary
        Analysis
    End Enum
End Namespace
