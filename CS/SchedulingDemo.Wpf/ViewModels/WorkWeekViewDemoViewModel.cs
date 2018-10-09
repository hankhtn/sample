using DevExpress.Mvvm;
using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SchedulingDemo.ViewModels {
    public class WorkWeekViewDemoViewModel {
        protected WorkWeekViewDemoViewModel() {
            Start = TeamData.Start;
            Calendars = new ObservableCollection<TeamCalendar>(TeamData.Calendars);
            Appointments = new ObservableCollection<TeamAppointment>(TeamData.AllAppointments);
            Days = CalendarSettingsViewModel.DefaultWorkDays;
            WorkTimeStart = TimeSpan.FromHours(9);
            WorkTimeEnd = TimeSpan.FromHours(18);
        }
        public virtual IEnumerable<TeamCalendar> Calendars { get; protected set; }
        public virtual IEnumerable<TeamAppointment> Appointments { get; protected set; }
        public virtual DateTime Start { get; set; }
        public virtual List<object> Days { get; set; }
        public virtual WeekDays WorkDays { get; set; }
        public virtual TimeSpan WorkTimeStart { get; set; }
        public virtual TimeSpan WorkTimeEnd { get; set; }
        public virtual TimeSpanRange WorkTime { get; set; }
        
        protected void OnDaysChanged() {
            if (Days == null) {
                Days = CalendarSettingsViewModel.DefaultWorkDays;
                return;
            }
            WorkDays = CalendarSettingsViewModel.ConvertWorkDays(Days);
        }
        protected void OnWorkTimeStartChanged() {
            var end = (WorkTimeEnd < WorkTimeStart) ? WorkTimeStart.Add(TimeSpan.FromHours(1)) : WorkTimeEnd;
            WorkTime = new TimeSpanRange(WorkTimeStart, end);
        }
        protected void OnWorkTimeEndChanged() {
            var start = (WorkTimeEnd < WorkTimeStart) ? WorkTimeEnd.Add(TimeSpan.FromHours(-1)) : WorkTimeStart;
            WorkTime = new TimeSpanRange(start, WorkTimeEnd);
        }
    }
}