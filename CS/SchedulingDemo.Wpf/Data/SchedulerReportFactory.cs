using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.Reporting;
using DevExpress.XtraScheduler.Reporting;

namespace SchedulingDemo {
    public enum CalendarDetailPrintStyle {
        [Display(Name = "Daily Style"), Image("pack://application:,,,/SchedulingDemo;component/Images/OutlookInspired/ReportTemplates/small_dayonepage.png")]
        DailyStyle,
        [Display(Name = "Weekly Agenda Style"), Image("pack://application:,,,/SchedulingDemo;component/Images/OutlookInspired/ReportTemplates/small_weekonepage.png")]
        WeeklyAgendaStyle,
        [Display(Name = "Weekly Calendar Style"), Image("pack://application:,,,/SchedulingDemo;component/Images/OutlookInspired/ReportTemplates/small_weeklefttorightonepage.png")]
        WeeklyCalendarStyle,
        [Display(Name = "Monthly Calendar Style"), Image("pack://application:,,,/SchedulingDemo;component/Images/OutlookInspired/ReportTemplates/small_monthonepage.png")]
        MonthlyCalendarStyle,
        [Display(Name = "Tri-fold Style"), Image("pack://application:,,,/SchedulingDemo;component/Images/OutlookInspired/ReportTemplates/small_trifold.png")]
        TriFoldStyle
    }

    public static class SchedulerReportFactory {
        static Dictionary<CalendarDetailPrintStyle, string> reportPathDictionary;

        static SchedulerReportFactory() {
            List<ReportTemplateInfo> reportTemplates = Utils.GetReportTemplateCollection();
            reportPathDictionary = new Dictionary<CalendarDetailPrintStyle, string>();
            RegisterReport("DailyStyleFitToPage", CalendarDetailPrintStyle.DailyStyle, reportTemplates);
            RegisterReport("WeeklyStyle", CalendarDetailPrintStyle.WeeklyAgendaStyle, reportTemplates);
            RegisterReport("TrifoldStandard", CalendarDetailPrintStyle.TriFoldStyle, reportTemplates);
            RegisterReport("MonthlyStyle", CalendarDetailPrintStyle.MonthlyCalendarStyle, reportTemplates);
            RegisterReport("DailyStyleFitToPage", CalendarDetailPrintStyle.WeeklyCalendarStyle, reportTemplates);
        }

        public static XtraSchedulerReport Create(CalendarDetailPrintStyle type, SchedulerControl scheduler) {
            string reportPath = String.Empty;
            if (!reportPathDictionary.TryGetValue(type, out reportPath))
                return null;
            DateTimeRange dateTimeRange = new DateTimeRange(scheduler.VisibleIntervals.First().Start, scheduler.VisibleIntervals.Last().End);
            return Create(reportPath, scheduler, dateTimeRange);
        }
        public static XtraSchedulerReport Create(string reportTemplatePath, SchedulerControl scheduler, DateTimeRange dateTimeRange) {
            XtraSchedulerReport report = new XtraSchedulerReport();
            scheduler.SchedulerPrintAdapter.DateTimeRange = dateTimeRange;
            scheduler.SchedulerPrintAdapter.AssignToReport(report);
            report.LoadLayout(reportTemplatePath);
            return report;
        }

        static void RegisterReport(string name, CalendarDetailPrintStyle reportType, IEnumerable<ReportTemplateInfo> reportTemplates) {
            ReportTemplateInfo reportTemplate = reportTemplates.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x.ReportTemplatePath) == name);
            if(reportTemplate == null)
                return;
            reportPathDictionary.Add(reportType, reportTemplate.ReportTemplatePath);
        }
    }
}
