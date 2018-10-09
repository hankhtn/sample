Imports System
Imports System.Linq
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Printing
Imports DevExpress.DemoData.Models
Imports System.Collections.Generic

Namespace PrintingDemo
    Partial Public Class BadgesModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class BadgesViewModel
        Inherits ModuleViewModelBase

        Private employees As List(Of Employee) = NWindContext.Create().Employees.ToList()
        Protected Overrides Function CreateLink() As TemplatedLink

            Dim link_Renamed As New SimpleLink()
            link_Renamed.ReportHeaderTemplate = ReportHeaderTemplate
            link_Renamed.DetailTemplate = DetailTemplate
            link_Renamed.ReportFooterTemplate = ReportFooterTemplate
            link_Renamed.PageFooterTemplate = PageFooterTemplate
            link_Renamed.DetailCount = employees.Count
            link_Renamed.DocumentName = "Badges"
            link_Renamed.ReportFooterData = String.Format("Generated on {0}", Date.Now)
            link_Renamed.ReportFooterData = String.Concat(link_Renamed.ReportFooterData, String.Format(" by {0}\{1}", Environment.UserDomainName, Environment.UserName))
            AddHandler link_Renamed.CreateDetail, AddressOf link_CreateDetail
            Return link_Renamed
        End Function
        Private Sub link_CreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
            e.Data = employees(e.DetailIndex)
        End Sub
    End Class
End Namespace
