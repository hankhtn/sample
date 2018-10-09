Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Printing
Imports System.Linq
Imports System
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace GridDemo
    Public MustInherit Class PrintViewModelBase
        Private printableControl As IPrintableControl
        Protected Sub New()

        End Sub
        Public Overridable Sub OnLoaded(ByVal printableControl As IPrintableControl)
            Me.printableControl = printableControl
        End Sub
        Public Sub OnUnloaded()
            Dim service = Me.GetService(Of IDocumentManagerService)()
            For Each document In service.Documents.ToArray()
                OnTabClosed(document.Content)
                document.Close()
            Next document
        End Sub
        Public Sub ShowPreviewInNewTab()
            Logger.Log("ShowPreviewInNewTab")
            Dim service = Me.GetService(Of IDocumentManagerService)()
            Dim link = New PrintableControlLink(printableControl)
            link.CreateDocument(True)
            Dim doc = service.CreateDocument(link)
            doc.Title = GetTitle()
            doc.DestroyOnClose = True
            doc.Show()
        End Sub
        Public Sub OnTabClosed(ByVal tabContent As Object)
            DirectCast(tabContent, PrintableControlLink).Dispose()
        End Sub
        Protected MustOverride Function GetTitle() As String
    End Class
End Namespace
