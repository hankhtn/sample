using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.iCalendar;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;

namespace SchedulingDemo {
    public class ReportTemplateInfo {
        public ReportTemplateInfo() { }
        public ReportTemplateInfo(string displayName, string reportTemplatePath) {
            DisplayName = displayName;
            ReportTemplatePath = reportTemplatePath;
        }

        public string DisplayName { get; set; }
        public string ReportTemplatePath { get; set; }
    }
    public class Utils {
        public static List<ReportTemplateInfo> GetReportTemplateCollection() {
            List<ReportTemplateInfo> reportTemplateCollection = new List<ReportTemplateInfo>();
            string reportTemplatesDirectory = DataFilesHelper.FindFile("SchedulerReportTemplates", DataFilesHelper.DataPath);
            DirectoryInfo directoryInfo = new DirectoryInfo(reportTemplatesDirectory);
            foreach(FileInfo fileInfo in directoryInfo.GetFiles()) 
                reportTemplateCollection.Add(new ReportTemplateInfo(fileInfo.Name, fileInfo.FullName));
            return reportTemplateCollection;
        }

        public static void SendMail(SchedulerControl scheduler, ObservableCollection<AppointmentItem> appointments) {
            if(appointments.Count == 0)
                return;
            List<DirectoryInfo> tempdirInfos = new List<DirectoryInfo>(appointments.Count);
            try {
                string[] files = new string[appointments.Count];
                int i = 0;
                foreach (AppointmentItem appointment in appointments) {
                    string tempDirectoryName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                    DirectoryInfo tempdirInfo = new DirectoryInfo(tempDirectoryName);
                    if(tempdirInfo.Exists)
                        return;
                    tempdirInfo.Create();
                    tempdirInfos.Add(tempdirInfo);
                    AppointmentItemCollection items = new AppointmentItemCollection();
                    items.Add(appointment.Type == DevExpress.XtraScheduler.AppointmentType.Occurrence ? appointment.RecurrencePattern : appointment);
                    ICalendarExporter exporter = new ICalendarExporter(scheduler, items);
                    string filePath = string.Format("{0}/{1}.ics", tempDirectoryName, GetFileName(appointment));
                    using(FileStream output = File.OpenWrite(filePath)) {
                        exporter.Export(output);
                        files[i++] = filePath;
                        output.Flush();
                    }
                }
                RecipientCollection recipients = new RecipientCollection();
                recipients.Add(new Recipient());
                MAPI.SendMail(IntPtr.Zero, files, appointments.Count == 1 ? string.Format("FW:{0}", GetFileName(appointments[0])) : "", "", recipients);
            }
            finally {
                foreach(DirectoryInfo tempdirInfo in tempdirInfos)
                    tempdirInfo.Delete(true);
            }
        }

        static string GetFileName(AppointmentItem appointment) {
            return string.Format("{0}.ics", string.IsNullOrEmpty(appointment.Subject) ? "Untitled" : appointment.Subject);
        }
    }
}
