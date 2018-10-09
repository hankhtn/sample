using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SchedulingDemo.ViewModels {
    public class TimelineScalesDemoViewModel {
        protected TimelineScalesDemoViewModel() {
            Start = TeamData.Start;
            Calendars = new ObservableCollection<TeamCalendar>(TeamData.Calendars);
            Appointments = new ObservableCollection<TeamAppointment>(TeamData.AllAppointments);
        }
        public virtual IEnumerable<TeamCalendar> Calendars { get; protected set; }
        public virtual IEnumerable<TeamAppointment> Appointments { get; protected set; }
        public virtual DateTime Start { get; set; }
    }
}
