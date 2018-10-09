Imports System
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class OrderViewModel
        Public Overrides Sub OnLoaded()
            MyBase.OnLoaded()
            Me.GetRequiredService(Of IReportService)().ShowReport(ReportInfoFactory.SalesInvoice(Entity))
        End Sub
    End Class
End Namespace
