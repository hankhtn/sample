using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.Reporting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraScheduler.Reporting;
using DevExpress.XtraPrinting.Native;
using DevExpress.Xpf.Printing.Native;

namespace SchedulingDemo.ViewModels {
    public class PrintingSettingsWindowViewModel {
        public static PrintingSettingsWindowViewModel Create(SchedulerControl scheduler, IList<ReportTemplateInfo> reportTemplateInfos) {
            return ViewModelSource.Create(() => new PrintingSettingsWindowViewModel(scheduler, reportTemplateInfos));
        }
        static void MoveSelectedItem(IList<ResourceItem> source, IList<ResourceItem> target, ResourceItem selectedItem) {
            int selectedItemIndex = source.IndexOf(selectedItem);
            if (selectedItemIndex < 0)
                return;
            target.Add(source[selectedItemIndex]);
            source.RemoveAt(selectedItemIndex);
        }

        readonly SchedulerControl scheduler;

        protected PrintingSettingsWindowViewModel(SchedulerControl scheduler, IList<ReportTemplateInfo> reportTemplateInfos) {
            this.scheduler = scheduler;
            this.scheduler.SchedulerPrintAdapter.ValidateResources += OnValidateResources;

            ReportTemplateInfos = new ObservableCollection<ReportTemplateInfo>(reportTemplateInfos);

            if (reportTemplateInfos.Count > 0)
                ActiveReportTemplateInfo = reportTemplateInfos[0];

            StartDate = scheduler.VisibleIntervals.First().Start;
            EndDate = scheduler.VisibleIntervals.Last().End;
            AvailableResources = new ObservableCollection<ResourceItem>(scheduler.VisibleResources);
            CustomResources = new ObservableCollection<ResourceItem>();
        }

        public bool UseSpecificResources { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReportTemplateInfo ActiveReportTemplateInfo { get; set; }
        public ObservableCollection<ReportTemplateInfo> ReportTemplateInfos { get; set; }
        public ObservableCollection<ResourceItem> AvailableResources { get; set; }
        public ResourceItem SelectedAvailableResource { get; set; }
        public ObservableCollection<ResourceItem> CustomResources { get; set; }
        public ResourceItem SelectedCustomResource { get; set; }
        public ObservableCollection<ResourceItem> PrintResources { get; set; }

        public void AddToCustomCollection() {
            MoveSelectedItem(AvailableResources, CustomResources, SelectedAvailableResource);
        }
        public void RemoveFromCustomCollection() {
            MoveSelectedItem(CustomResources, AvailableResources, SelectedCustomResource);
        }
        public void MoveUp() {
            int index = CustomResources.IndexOf(SelectedCustomResource);
            if (index <= 0)
                return;
            ResourceItem item = CustomResources[index];
            CustomResources.RemoveAt(index);
            CustomResources.Insert(index - 1, item);
        }
        public void MoveDown() {
            int index = CustomResources.IndexOf(SelectedCustomResource);
            if ((index < 0) || (index >= CustomResources.Count - 1))
                return;
            ResourceItem item = CustomResources[index];
            CustomResources.RemoveAt(index);
            CustomResources.Insert(index + 1, item);
        }
        public void Preview() {
            XtraSchedulerReport report = SchedulerReportFactory.Create(ActiveReportTemplateInfo.ReportTemplatePath, this.scheduler, new DateTimeRange(StartDate, EndDate));
            report.PrintingSystem.ReplaceService<BackgroundPageBuildEngineStrategy>(new DispatcherPageBuildStrategy());
            report.ShowPreview();
        }
        public void OpenFileDialog() {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckPathExists = true;
            dlg.Filter = "Report template files (*.schrepx)|*.schrepx|All files (*.*)|*.*";

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dlg.InitialDirectory = new DirectoryInfo(folderPath).FullName;

            bool? result = dlg.ShowDialog();
            if (result == null || result == false)
                return;

            ActiveReportTemplateInfo = new ReportTemplateInfo(dlg.SafeFileName, dlg.FileName);
            ReportTemplateInfos.Add(ActiveReportTemplateInfo);
        }
        public void WindowClosing() {
            this.scheduler.SchedulerPrintAdapter.ValidateResources -= OnValidateResources;
        }

        protected void OnStartDateChanged() {
            if (EndDate < StartDate)
                EndDate = StartDate;
        }
        protected void OnEndDateChanged() {
            if (EndDate < StartDate)
                StartDate = EndDate;
        }

        void OnValidateResources(object sender, ResourceItemsValidationEventArgs e) {
            IList<ResourceItem> resources = e.ResourceItems;
            resources.Clear();
            if (UseSpecificResources)
                CustomResources.ForEach(x => resources.Add(x));
            else
                AvailableResources.ForEach(x => resources.Add(x));
        }
    }
}
