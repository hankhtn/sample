Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports System.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.Xpf.Scheduling.Reporting
Imports DevExpress.XtraScheduler.Reporting

Namespace SchedulingDemo
    Public Enum CalendarDetailPrintStyle
        <Display(Name := "Daily Style"), Image("pack://application:,,,/SchedulingDemo;component/Images/OutlookInspired/ReportTemplates/small_dayonepage.png")>
        DailyStyle
        <Display(Name := "Weekly Agenda Style"), Image("pack://application:,,,/SchedulingDemo;component/Images/OutlookInspired/ReportTemplates/small_weekonepage.png")>
        WeeklyAgendaStyle
        <Display(Name := "Weekly Calendar Style"), Image("pack://application:,,,/SchedulingDemo;component/Images/OutlookInspired/ReportTemplates/small_weeklefttorightonepage.png")>
        WeeklyCalendarStyle
        <Display(Name := "Monthly Calendar Style"), Image("pack://application:,,,/SchedulingDemo;component/Images/OutlookInspired/ReportTemplates/small_monthonepage.png")>
        MonthlyCalendarStyle
        <Display(Name := "Tri-fold Style"), Image("pack://application:,,,/SchedulingDemo;component/Images/OutlookInspired/ReportTemplates/small_trifold.png")>
        TriFoldStyle
    End Enum

    Public NotInheritable Class SchedulerReportFactory

        Private Sub New()
        End Sub
        Private Shared reportPathDictionary As Dictionary(Of CalendarDetailPrintStyle, String)

        Shared Sub New()
            Dim reportTemplates As List(Of ReportTemplateInfo) = Utils.GetReportTemplateCollection()
            reportPathDictionary = New Dictionary(Of CalendarDetailPrintStyle, String)()
            RegisterReport("DailyStyleFitToPage", CalendarDetailPrintStyle.DailyStyle, reportTemplates)
            RegisterReport("WeeklyStyle", CalendarDetailPrintStyle.WeeklyAgendaStyle, reportTemplates)
            RegisterReport("TrifoldStandard", CalendarDetailPrintStyle.TriFoldStyle, reportTemplates)
            RegisterReport("MonthlyStyle", CalendarDetailPrintStyle.MonthlyCalendarStyle, reportTemplates)
            RegisterReport("DailyStyleFitToPage", CalendarDetailPrintStyle.WeeklyCalendarStyle, reportTemplates)
        End Sub

        Public Shared Function Create(ByVal type As CalendarDetailPrintStyle, ByVal scheduler As SchedulerControl) As XtraSchedulerReport
            Dim reportPath As String = String.Empty
            If Not reportPathDictionary.TryGetValue(type, reportPath) Then
                Return Nothing
            End If
            Dim dateTimeRange As New DateTimeRange(scheduler.VisibleIntervals.First().Start, scheduler.VisibleIntervals.Last().End)
            Return Create(reportPath, scheduler, dateTimeRange)
        End Function
        Public Shared Function Create(ByVal reportTemplatePath As String, ByVal scheduler As SchedulerControl, ByVal dateTimeRange As DateTimeRange) As XtraSchedulerReport
            Dim report As New XtraSchedulerReport()
            scheduler.SchedulerPrintAdapter.DateTimeRange = dateTimeRange
            scheduler.SchedulerPrintAdapter.AssignToReport(report)
            report.LoadLayout(reportTemplatePath)
            Return report
        End Function

        Private Shared Sub RegisterReport(ByVal name As String, ByVal reportType As CalendarDetailPrintStyle, ByVal reportTemplates As IEnumerable(Of ReportTemplateInfo))
            Dim reportTemplate As ReportTemplateInfo = reportTemplates.FirstOrDefault(Function(x) Path.GetFileNameWithoutExtension(x.ReportTemplatePath) = name)
            If reportTemplate Is Nothing Then
                Return
            End If
            reportPathDictionary.Add(reportType, reportTemplate.ReportTemplatePath)
        End Sub
    End Class
End Namespace
