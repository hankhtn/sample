using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Scheduling;
using DevExpress.XtraReports;

namespace SchedulingDemo.ViewModels {
    public class CalendarDetailPrintReportViewModel {
        public static CalendarDetailPrintReportViewModel Create() {
            return ViewModelSource.Create(() => new CalendarDetailPrintReportViewModel());
        }

        protected CalendarDetailPrintReportViewModel() {
            PrintStyle = CalendarDetailPrintStyle.DailyStyle;
        }

        public virtual CalendarDetailPrintStyle PrintStyle { get; set; }
        public virtual IReport Report { get; set; }

        public void UpdateReport(SchedulerControl scheduler) {
            Report = SchedulerReportFactory.Create(PrintStyle, scheduler);
            Report.PrintingSystemBase.ClearContent();
            Report.CreateDocument(true);
        }
    }
}
