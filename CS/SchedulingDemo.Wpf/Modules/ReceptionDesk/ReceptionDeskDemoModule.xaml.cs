using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Scheduling;
using DevExpress.XtraScheduler.Native;
using SchedulingDemo.ViewModels;
using System;
using System.Reflection;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SchedulingDemo {
    [NoAutogeneratedCodeFiles, CodeFiles(
        "Modules/ReceptionDesk/ReceptionDeskDemoModule.xaml",
        "Modules/ReceptionDesk/ReceptionDeskDemoModule.xaml.(cs)",
        "Modules/ReceptionDesk/MedicalAppointmentWindow.xaml",
        "ViewModels/ReceptionDesk/ReceptionDeskDemoViewModel.(cs)",
        "ViewModels/ReceptionDesk/MedicalAppointmentWindowViewModel.(cs)")]
    public partial class ReceptionDeskDemoModule : SchedulingDemoModule {
        public ReceptionDeskDemoModule() {
            InitializeComponent();
        }
        void OnAppointmentWindowShowing(object sender, AppointmentWindowShowingEventArgs e) {
            ReceptionDeskDemoViewModel vm = scheduler.DataContext as ReceptionDeskDemoViewModel;
            if (vm != null)
                e.Window.DataContext = MedicalAppointmentWindowViewModel.Create(e.Appointment, scheduler, vm.Patients);
        }
        void OnAppointmentDrop(object sender, AppointmentItemDragDropEventArgs e) {
            AppointmentDragResizeViewModel viewModel = e.ViewModels[0];
            if (CheckTimeResourceChanges(viewModel))
                e.Allow = true;
            else if (DXMessageBox.Show(GetDropMessage(viewModel, e.CopyEffect, e.IsExternalDragDrop) + "\r\nProceed?",
                Assembly.GetEntryAssembly().GetName().Name, MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)
                e.Allow = false;
        }
        bool CheckTimeResourceChanges(AppointmentDragResizeViewModel viewModel) {
            AppointmentItem sourceAppointment = viewModel.Appointment;
            return
               viewModel.Start == sourceAppointment.Start &&
               viewModel.End == sourceAppointment.End &&
               viewModel.AllDay == sourceAppointment.AllDay &&
               Object.Equals(viewModel.Resource.Id, sourceAppointment.ResourceId);
        }
        string GetDropMessage(AppointmentDragResizeViewModel viewModel, bool copyEffect, bool isExternalDragDrop) {
            AppointmentItem sourceAppointment = viewModel.Appointment;
            string sourceStart = sourceAppointment.Start.ToShortTimeString();
            string viewModelStart = viewModel.Start.ToShortTimeString();

            ResourceItem sourceResource = this.scheduler.ResourceItems.GetById(sourceAppointment.ResourceId);
            string sourceResourceCaption = sourceResource == null ? ResourceItemEmpty.Resource.Caption : sourceResource.Caption;
            string viewModelResourceCaption = viewModel.Resource.Caption;
            if (isExternalDragDrop)
                return String.Format("Creating {0}'s event at {1}.", viewModelResourceCaption, viewModelStart);
            string beginResult = copyEffect ? "Coping " : "Moving ";
            if (sourceResourceCaption != viewModelResourceCaption)
                return beginResult + String.Format("{0}'s event at {1} to {2}'s event at {3}.", sourceResourceCaption, sourceStart, viewModelResourceCaption, viewModelStart);
            return beginResult + String.Format("{0}'s event from {1} to {2}.", viewModelResourceCaption, sourceStart, viewModelStart);
        }

        void OnStartRecordDrag(object sender, StartRecordDragEventArgs e) {
            e.Data.SetData(typeof(IEnumerable<AppointmentItem>), e.Records.Cast<Patient>().Select(x => CreateAppointment(x)));
            e.Handled = true;
        }

        AppointmentItem CreateAppointment(Patient patient) {
            AppointmentItem result = new AppointmentItem();
            result.CustomFields.Add(new CustomField("PatientId", patient.Id));
            result.Subject = patient.Name;
            result.LabelId = this.scheduler.LabelItems[0].Id;
            result.StatusId = this.scheduler.StatusItems[1].Id;
            return result;
        }
        void OnCompleteRecordDragDrop(object sender, CompleteRecordDragDropEventArgs e) {
            e.Handled = true;
        }
        void OnDragRecordOver(object sender, DragRecordOverEventArgs e) {
            e.Effects = DragDropEffects.Move;
            e.Handled = true;
        }
        void OnDropRecord(object sender, DropRecordEventArgs e) {
            e.Handled = true;
        }
    }
}
