Imports System
Imports System.IO
Imports System.Configuration
Imports DevExpress.DemoData.Helpers
Imports DevExpress.DemoReports
Imports DevExpress.XtraReports
Imports XtraReportsDemos

Namespace ProductsDemo.Modules
    Public Class ReportsModuleViewModel
        Shared Sub New()
            PatchDemoReportsConnectionStrings()
        End Sub
        Public Overridable Property DocumentSource() As IReport

        Public Sub OnLoaded()
            UpdateReport()
        End Sub
        Public Sub OnUnloaded()
            DocumentSource.StopPageBuilding()
        End Sub
        Private Sub UpdateReport()
            DocumentSource = New XtraReportsDemos.MasterDetailReport.Report()
            DocumentSource.CreateDocument(True)
        End Sub
        Private Shared Sub PatchDemoReportsConnectionStrings()
        Dim path As String = DevExpress.Utils.FilesHelper.FindingFileName(AppDomain.CurrentDomain.BaseDirectory, "Data\nwind.mdb", False)
            ConnectionHelper.SetDataDirectory(System.IO.Path.GetDirectoryName(path))
        End Sub
    End Class
End Namespace
