using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Scheduling;

namespace SchedulingDemo.ViewModels {
    public class OutlookEventWindowViewModel : AppointmentWindowViewModel {
        public static OutlookEventWindowViewModel Create(SchedulerControl scheduler, AppointmentItem appointment) {
            return ViewModelSource.Create(() => new OutlookEventWindowViewModel(scheduler, appointment));
        }
        protected OutlookEventWindowViewModel(SchedulerControl scheduler, AppointmentItem appointment) : base(appointment, scheduler) {
            EventPriority priority = (EventPriority)appointment.CustomFields["Priority"];
            HighImportanceChecked = priority == EventPriority.HighImportance;
            LowImportanceChecked = priority == EventPriority.LowImportance;
            Categories = new ObservableCollection<CheckedCategory>();
            foreach(AppointmentLabelItem label in scheduler.LabelItems)
                Categories.Add(CheckedCategory.Create(label, object.Equals(Label.Id, label.Id)));
        }

        public virtual bool HighImportanceChecked { get; set; }
        public virtual bool LowImportanceChecked { get; set; }
        public virtual ObservableCollection<CheckedCategory> Categories { get; set; }

        public void Send() {
            if (ArePropertiesChanged) {
                DXMessageBox.Show("This item must be saved before it can be forwarded.",
                Assembly.GetEntryAssembly().GetName().Name, MessageBoxButton.OK,
                MessageBoxImage.Warning);
                return;
            }
            Utils.SendMail(Scheduler, new ObservableCollection<AppointmentItem>() { Appointment });
        }
        public void ChangeCategorize(AppointmentLabelItem label) {
            Label = label;
        }
        public void SetHighPriority() {
            EventPriority priority = (EventPriority)CustomFields["Priority"];
            CustomFields["Priority"] = priority == EventPriority.HighImportance ? EventPriority.None : EventPriority.HighImportance;
            if (LowImportanceChecked)
                LowImportanceChecked = !HighImportanceChecked;
        }
        public void SetLowPriority() {
            EventPriority priority = (EventPriority)CustomFields["Priority"];
            CustomFields["Priority"] = priority == EventPriority.LowImportance ? EventPriority.None : EventPriority.LowImportance;
            if (HighImportanceChecked)
                HighImportanceChecked = !LowImportanceChecked;
        }
    }
}
