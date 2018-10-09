using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.iCalendar;
using DevExpress.XtraScheduler;

namespace SchedulingDemo.ViewModels {
    public class OutlookInspiredDemoViewModel {
        static readonly IList<TimeSpan> TimeScales = new TimeSpan[] { TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(6), TimeSpan.FromMinutes(5) };
        static readonly OutlookInspiredData data = new OutlookInspiredData();
        AppointmentItem selectedAppointment;

        protected OutlookInspiredDemoViewModel() {
            MinZoomFactor = 0;
            MaxZoomFactor = 100;
            ZoomStep = 20;
            ZoomFactor = 20;
            LinksViewModel = LinksViewModel.Create();
            ViewSettings = ViewSettingsViewModel.Create();
            PrintStyles = CalendarDetailPrintReportViewModel.Create();
            CalendarSettings = CalendarSettingsViewModel.Create();
        }

        public virtual IBackstageViewService BackstageViewService { get { return null; } }
        public virtual LinksViewModel LinksViewModel { get; set; }
        public virtual ViewSettingsViewModel ViewSettings { get; set; }
        public virtual CalendarDetailPrintReportViewModel PrintStyles { get; set; }
        public virtual CalendarSettingsViewModel CalendarSettings { get; set; }
        public virtual ObservableCollection<Calendar> Calendars { get { return data.Calendars; } }
        public virtual ObservableCollection<OutlookEvent> Events { get { return data.Events; } }
        public virtual ObservableCollection<Category> Categories { get { return data.Categories; } }

        [BindableProperty]
        public virtual AppointmentItem SelectedAppointment {
            get { return this.selectedAppointment; }
            set {
                if (this.selectedAppointment != null)
                    this.selectedAppointment.PropertyChanged -= PropertyChanged;
                AppointmentItem newSelectedAppointment = value;
                if (newSelectedAppointment != null)
                    newSelectedAppointment.PropertyChanged += PropertyChanged;
                this.selectedAppointment = newSelectedAppointment;
                UpdatePriority(this.selectedAppointment);
            }
        }

        public virtual TimeSpan TimeScale { get; set; }
        public virtual double ZoomFactor { get; set; }
        public virtual int MinZoomFactor { get; set; }
        public virtual int MaxZoomFactor { get; set; }
        public virtual bool HighImportanceChecked { get; set; }
        public virtual bool LowImportanceChecked { get; set; }

        int ZoomStep { get; set; }

        public void Print(SchedulerControl scheduler) {
            PrintingSettingsWindow printingSettingsWindow = new PrintingSettingsWindow();
            printingSettingsWindow.DataContext = PrintingSettingsWindowViewModel.Create(scheduler, Utils.GetReportTemplateCollection());
            printingSettingsWindow.Owner = Window.GetWindow(scheduler);
            printingSettingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            printingSettingsWindow.ShowDialog();
        }
        public void OutlookImport(SchedulerControl scheduler) {
            OutlookExchange(scheduler, OutlookExchangeType.Import);
        }
        public void OutlookExport(SchedulerControl scheduler) {
            OutlookExchange(scheduler, OutlookExchangeType.Export);
        }
        public void iCalendarImport(SchedulerControl scheduler) {
            ICalendarImporter importer = new ICalendarImporter(scheduler);
            importer.AppointmentItemImported += AppointmentItemImported;
            using(Stream stream = OpenRead("Calendar", "iCalendar files (*.ics)|*.ics")) {
                if (stream != null) 
                    importer.Import(stream);
            }
        }
        
        public void iCalendarExport(SchedulerControl scheduler) {
            ICalendarExporter exporter = new ICalendarExporter(scheduler);
            using(Stream stream = OpenWrite("Calendar", "iCalendar files (*.ics)|*.ics")) {
                if(stream != null) {
                    exporter.Export(stream);
                    stream.Flush();
                }
            }
        }
        public void Send(SchedulerControl scheduler) {
            Utils.SendMail(scheduler, scheduler.SelectedAppointments);
        }
        public void SetHighPriority() {
            if (SelectedAppointment == null)
                return;
            EventPriority priority = (EventPriority)SelectedAppointment.CustomFields["Priority"];
            SelectedAppointment.CustomFields["Priority"] = priority == EventPriority.HighImportance ? EventPriority.None : EventPriority.HighImportance;
            if(LowImportanceChecked)
                LowImportanceChecked = !HighImportanceChecked;
        }
        public void SetLowPriority() {
            if (SelectedAppointment == null)
                return;
            EventPriority priority = (EventPriority)SelectedAppointment.CustomFields["Priority"];
            SelectedAppointment.CustomFields["Priority"] = priority == EventPriority.LowImportance ? EventPriority.None : EventPriority.LowImportance;
            if (HighImportanceChecked)
                HighImportanceChecked = !LowImportanceChecked;
        }
        public void OpenViewSettingsWindow(SchedulerControl scheduler) {
            CalendarSettingsWindow settingsWindow = new CalendarSettingsWindow();
            CalendarSettingsViewModel newCalendarSettings = CalendarSettingsViewModel.Create();
            newCalendarSettings.ApplySettings(CalendarSettings);
            settingsWindow.DataContext = newCalendarSettings;
            settingsWindow.Owner = Window.GetWindow(scheduler);
            settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (settingsWindow.ShowDialog() == true)
                CalendarSettings.ApplySettings(newCalendarSettings);

        }
        public void ResetViewSettings() {
            CalendarSettings = CalendarSettingsViewModel.Create();
        }
        public void CloseBackstageView() {
            BackstageViewService.Close();
        }

        protected void OnZoomFactorChanged() {
            int value = (int)(ZoomFactor / ZoomStep);
            TimeScale = TimeScales[value];
        }
        protected void OnTimeScaleChanged() {
            int value = TimeScales.IndexOf(TimeScale);
            ZoomFactor = ZoomStep * value;
        }

        void UpdatePriority(AppointmentItem appointment) {
            if (appointment == null)
                return;
            EventPriority priority = (EventPriority)appointment.CustomFields["Priority"];
            HighImportanceChecked = priority == EventPriority.HighImportance;
            LowImportanceChecked = priority == EventPriority.LowImportance;
        }
        void PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "Priority")
                UpdatePriority(sender as AppointmentItem);
        }

        Stream OpenRead(string fileName, string filter) {
            OpenFileDialog dialog = new OpenFileDialog() { FileName = fileName, Filter = filter, FilterIndex = 1 };
            if(dialog.ShowDialog() != true)
                return null;
            return dialog.OpenFile();
        }
        Stream OpenWrite(string fileName, string filter) {
            SaveFileDialog dialog = new SaveFileDialog() { FileName = fileName, Filter = filter, FilterIndex = 1 };
            if(dialog.ShowDialog() != true)
                return null;
            return dialog.OpenFile();
        }
        void OutlookExchange(SchedulerControl scheduler, OutlookExchangeType exchangeType) {
            try {
                string[] outlookCalendarPaths = DevExpress.XtraScheduler.Outlook.OutlookExchangeHelper.GetOutlookCalendarPaths();
                if (outlookCalendarPaths == null || outlookCalendarPaths.Length == 0)
                    return;

                OutlookExchangeOptionsWindow optionsWindow = new OutlookExchangeOptionsWindow();
                optionsWindow.DataContext = OutlookExchangeOptionsWindowViewModel.Create(scheduler, exchangeType, outlookCalendarPaths);
                optionsWindow.Owner = Window.GetWindow(scheduler);
                optionsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                optionsWindow.ShowDialog();
            }
            catch {
                DXMessageBox.Show(String.Format("Unable to {0}.\nCheck whether MS Outlook is installed.", "get the list of available calendars from Microsoft Outlook"),
                    "Import from MS Outlook", MessageBoxButton.OK, MessageBoxImage.Warning);
            }           
        }

        void AppointmentItemImported(object sender, AppointmentItemImportedEventArgs e) {
            CustomFieldCollection customFields = e.Appointment.CustomFields;
            customFields["Priority"] = ConvertToEventPriority(customFields["Priority"]);
            customFields["IsPrivate"] = ConvertToBoolean(customFields["IsPrivate"]);
        }

        bool ConvertToBoolean(object customField) {
            return customField == null ? false : AreStringsEqual(customField.ToString(), "true");
        }

        EventPriority ConvertToEventPriority(object customField) {
            if (customField == null)
                return EventPriority.None;
            EventPriority priority = EventPriority.HighImportance;
            string value = customField.ToString();
            if (AreStringsEqual(value, priority.ToString()))
                return priority;
            priority = EventPriority.LowImportance;
            if (AreStringsEqual(value, priority.ToString()))
                return priority;
            return EventPriority.None;
        }

        bool AreStringsEqual(string left, string right) {
            return String.Compare(left, right, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}
