using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DevExpress.DemoData;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Scheduling;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DragDropCustomDataViewModel.(cs)")]
    public partial class DragDropCustomData : GridDemoModule {
        public DragDropCustomData() {
            InitializeComponent();
        }

        void OnStartRecordDrag(object sender, StartRecordDragEventArgs e) {
            e.Data.SetData(typeof(IEnumerable<AppointmentItem>), e.Records.Cast<SchedulerTask>().Select(x => CreateAppointment(x)));
            e.Handled = true;
        }
        void OnDragRecordOver(object sender, DragRecordOverEventArgs e) {
            if(e.IsFromOutside && e.Data.GetDataPresent(typeof(IEnumerable<AppointmentItem>))) {
                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }
        }
        void OnDropRecord(object sender, DropRecordEventArgs e) {
            if(e.Data.GetDataPresent(typeof(IEnumerable<AppointmentItem>))) {
                var appointments = (IEnumerable<AppointmentItem>)e.Data.GetData(typeof(IEnumerable<AppointmentItem>));
                var dataObject = new DataObject();
                dataObject.SetData(new RecordDragDropData(appointments.Select(x => CreateTask(x)).ToArray()));
                e.Data = dataObject;
                e.Effects = DragDropEffects.Move;
            }
        }

        void OnAppointmentResize(object sender, AppointmentItemResizeEventArgs e) {
            e.Allow = e.State != ResizeState.Continue || (e.ViewModel.End - e.ViewModel.Start).TotalHours >= 1;
        }

        AppointmentItem CreateAppointment(SchedulerTask task) {
            return new AppointmentItem {
                Subject = task.Subject,
                LabelId = task.Priority,
                Description = task.Description,
                Start = new DateTime(),
                End = new DateTime() + task.Duration,
                AllDay = task.AllDay
            };
        }
        SchedulerTask CreateTask(AppointmentItem appointmentItem) {
            return new SchedulerTask {
                Subject = appointmentItem.Subject,
                Priority = appointmentItem.LabelId is IssuePriority ? (IssuePriority)appointmentItem.LabelId : IssuePriority.Low,
                Description = appointmentItem.Description,
                Duration = appointmentItem.End - appointmentItem.Start,
                AllDay = appointmentItem.AllDay
            };
        }
    }
}
