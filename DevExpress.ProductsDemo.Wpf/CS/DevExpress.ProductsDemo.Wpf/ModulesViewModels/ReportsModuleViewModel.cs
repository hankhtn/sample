using System;
using System.IO;
using System.Configuration;
using DevExpress.DemoData.Helpers;
using DevExpress.DemoReports;
using DevExpress.XtraReports;
using XtraReportsDemos;

namespace ProductsDemo.Modules {
    public class ReportsModuleViewModel {
        static ReportsModuleViewModel() {
            PatchDemoReportsConnectionStrings();
        }
        public virtual IReport DocumentSource { get; set; }

        public void OnLoaded() {
            UpdateReport();
        }
        public void OnUnloaded() {
            DocumentSource.StopPageBuilding();
        }
        void UpdateReport() {
            DocumentSource = new XtraReportsDemos.MasterDetailReport.Report();
            DocumentSource.CreateDocument(true);
        }
        static void PatchDemoReportsConnectionStrings() {
		string path = DevExpress.Utils.FilesHelper.FindingFileName(AppDomain.CurrentDomain.BaseDirectory, @"Data\nwind.mdb", false);
            ConnectionHelper.SetDataDirectory(Path.GetDirectoryName(path));
        }
    }
}
