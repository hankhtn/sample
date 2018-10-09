Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.XtraReports

Namespace SchedulingDemo.ViewModels
    Public Class CalendarDetailPrintReportViewModel
        Public Shared Function Create() As CalendarDetailPrintReportViewModel
            Return ViewModelSource.Create(Function() New CalendarDetailPrintReportViewModel())
        End Function

        Protected Sub New()
            PrintStyle = CalendarDetailPrintStyle.DailyStyle
        End Sub

        Public Overridable Property PrintStyle() As CalendarDetailPrintStyle
        Public Overridable Property Report() As IReport

        Public Sub UpdateReport(ByVal scheduler As SchedulerControl)
            Report = SchedulerReportFactory.Create(PrintStyle, scheduler)
            Report.PrintingSystemBase.ClearContent()
            Report.CreateDocument(True)
        End Sub
    End Class
End Namespace
