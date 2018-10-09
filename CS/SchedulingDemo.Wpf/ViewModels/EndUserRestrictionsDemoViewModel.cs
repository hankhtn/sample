using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Scheduling;

namespace SchedulingDemo.ViewModels {
    public class EndUserRestrictionsDemoViewModel {
        protected EndUserRestrictionsDemoViewModel() {
            Start = TeamData.Start;
            Calendars = new ObservableCollection<TeamCalendar>(TeamData.Calendars);
            Appointments = new ObservableCollection<TeamAppointment>(TeamData.AllAppointments);
        }

        public virtual DateTime Start { get; set; }
        public virtual IEnumerable<TeamCalendar> Calendars { get; protected set; }
        public virtual IEnumerable<TeamAppointment> Appointments { get; protected set; }
        public virtual bool IsTeamCalendarReadonly { get; set; }
        public virtual ResourceItem SelectedResource { get; set; }

        public void CustomAllowAppointmentItemOperation(AppointmentItemOperationEventArgs e) {
            if (e.Appointment == null && SelectedResource == null)
                return;
            e.Allow = CalcRestriction(e.Appointment == null ? SelectedResource.Id : e.Appointment.ResourceId, e.Allow);
        }
        public void AppointmentDrop(AppointmentItemDragDropEventArgs e) {
            bool allow = e.Allow;
            foreach (AppointmentDragResizeViewModel viewModel in e.ViewModels) {
                allow = CalcRestriction(viewModel.Resource.Id, allow);
                if (!allow)
                    break;
            }
            e.Allow = allow;
        }
        bool CalcRestriction(object resourceId, bool controlRestriction) {
            return IsTeamCalendarReadonly && object.Equals(resourceId, 1) ? false : controlRestriction;
        }
    }
}
