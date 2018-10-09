Imports System
Imports System.ComponentModel
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Public Class ReportPreviewViewModel
        Implements IDocumentContent

        Public Shared Function Create(ByVal reportInfo As IReportInfo) As ReportPreviewViewModel
            Return ViewModelSource.Create(Function() New ReportPreviewViewModel(reportInfo))
        End Function

        Private reportInfo As IReportInfo

        Protected Sub New(ByVal reportInfo As IReportInfo)
            Me.reportInfo = reportInfo
        End Sub
        Public Sub OnLoaded()
            Me.GetRequiredService(Of IReportService)().ShowReport(reportInfo)
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
        #Region "IDocumentContent"
        Private Sub IDocumentContent_OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
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
                Return Nothing
            End Get
        End Property
        #End Region
    End Class
End Namespace
