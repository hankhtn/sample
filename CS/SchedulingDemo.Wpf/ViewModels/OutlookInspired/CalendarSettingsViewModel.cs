using System;
using System.Collections.Generic;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Scheduling;
using DevExpress.XtraScheduler;

namespace SchedulingDemo.ViewModels {
    public class CalendarSettingsViewModel {
        public static List<object> DefaultWorkDays = new List<object> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        public static WeekDays ConvertDayOfWeek(DayOfWeek dayOfWeek) {
            WeekDays weekDays;
            switch(dayOfWeek) {
                case DayOfWeek.Monday:
                    weekDays = WeekDays.Monday;
                    break;
                case DayOfWeek.Tuesday:
                    weekDays = WeekDays.Tuesday;
                    break;
                case DayOfWeek.Wednesday:
                    weekDays = WeekDays.Wednesday;
                    break;
                case DayOfWeek.Thursday:
                    weekDays = WeekDays.Thursday;
                    break;
                case DayOfWeek.Friday:
                    weekDays = WeekDays.Friday;
                    break;
                case DayOfWeek.Saturday:
                    weekDays = WeekDays.Saturday;
                    break;
                default:
                    weekDays = WeekDays.Sunday;
                    break;

            }
            return weekDays;
        }
        public static WeekDays ConvertWorkDays(List<object> days) {
            WeekDays result = (WeekDays)0;
            foreach(object dayOfWeek in days)
                result = result | ConvertDayOfWeek((DayOfWeek)dayOfWeek);
            return result;
        }

        public static CalendarSettingsViewModel Create() {
            return ViewModelSource.Create(() => new CalendarSettingsViewModel());
        }

        protected CalendarSettingsViewModel() {
            ShowDayHeaders = true;
            ShowResourceHeaders = true;
            ShowResourceNavigator = true;
            SnapToCellsMode = SnapToCellsMode.Always;

            TimeIndicatorVisibility = TimeIndicatorVisibility.TodayView;
            TimeMarkerVisibility = TimeMarkerVisibility.TodayView;

            WorkTimeStart = new TimeSpan(6, 0, 0);
            WorkTimeEnd = new TimeSpan(20, 0, 0);

            YearTimeScaleIsEnabled = false;
            QuarterTimeScaleIsEnabled = false;
            MonthTimeScaleIsEnabled = false;
            WeekTimeScaleIsEnabled = false;
            DayTimeScaleIsEnabled = true;
            HourTimeScaleIsEnabled = true;

            IntervalCount = 10;
            FirstDayOfWeek = DayOfWeek.Sunday;
            DayCount = 1;

            Days = DefaultWorkDays;
            ShowAllDayArea = true;
            ShowWorkTimeOnly = true;
        }
        public virtual SnapToCellsMode SnapToCellsMode { get; set; }
        public virtual bool ShowDayHeaders { get; set; }
        public virtual bool ShowResourceHeaders { get; set; }
        public virtual bool ShowResourceNavigator { get; set; }

        
        public virtual bool ShowAllDayArea { get; set; }
        public virtual bool ShowWorkTimeOnly { get; set; }
        public virtual TimeSpan WorkTimeStart { get; set; }
        public virtual TimeSpan WorkTimeEnd { get; set; }
        public virtual TimeSpanRange WorkTime { get; set; }
        public virtual int DayCount { get; set; }
        public virtual TimeMarkerVisibility TimeMarkerVisibility { get; set; }
        public virtual TimeIndicatorVisibility TimeIndicatorVisibility { get; set; }

        
        public virtual WeekDays WorkDays { get; set; }
        public virtual List<object> Days { get; set; }

        
        public virtual DayOfWeek FirstDayOfWeek { get; set; }

        
        public virtual int IntervalCount { get; set; }
        public virtual bool YearTimeScaleIsEnabled { get; set; }
        public virtual bool QuarterTimeScaleIsEnabled { get; set; }
        public virtual bool MonthTimeScaleIsEnabled { get; set; }
        public virtual bool WeekTimeScaleIsEnabled { get; set; }
        public virtual bool DayTimeScaleIsEnabled { get; set; }
        public virtual bool HourTimeScaleIsEnabled { get; set; }

        public void ApplySettings(CalendarSettingsViewModel calendarSettings) {
            ShowDayHeaders = calendarSettings.ShowDayHeaders;
            ShowResourceHeaders = calendarSettings.ShowResourceHeaders;
            ShowResourceNavigator = calendarSettings.ShowResourceNavigator;
            SnapToCellsMode = calendarSettings.SnapToCellsMode;

            TimeIndicatorVisibility = calendarSettings.TimeIndicatorVisibility;
            TimeMarkerVisibility = calendarSettings.TimeMarkerVisibility;

            WorkTimeStart = calendarSettings.WorkTimeStart;
            WorkTimeEnd = calendarSettings.WorkTimeEnd;

            YearTimeScaleIsEnabled = calendarSettings.YearTimeScaleIsEnabled;
            QuarterTimeScaleIsEnabled = calendarSettings.QuarterTimeScaleIsEnabled;
            MonthTimeScaleIsEnabled = calendarSettings.MonthTimeScaleIsEnabled;
            WeekTimeScaleIsEnabled = calendarSettings.WeekTimeScaleIsEnabled;
            DayTimeScaleIsEnabled = calendarSettings.DayTimeScaleIsEnabled;
            HourTimeScaleIsEnabled = calendarSettings.HourTimeScaleIsEnabled;

            IntervalCount = calendarSettings.IntervalCount;
            FirstDayOfWeek = calendarSettings.FirstDayOfWeek;
            DayCount = calendarSettings.DayCount;

            Days = calendarSettings.Days;
            ShowAllDayArea = calendarSettings.ShowAllDayArea;
            ShowWorkTimeOnly = calendarSettings.ShowWorkTimeOnly;
        }

        protected void OnWorkTimeStartChanged() {
            TimeSpan end = (WorkTimeEnd < WorkTimeStart) ? WorkTimeStart.Add(TimeSpan.FromHours(1)) : WorkTimeEnd;
            WorkTime = new TimeSpanRange(WorkTimeStart, end);
        }
        protected void OnWorkTimeEndChanged() {
            TimeSpan start = (WorkTimeEnd < WorkTimeStart) ? WorkTimeEnd.Add(TimeSpan.FromHours(-1)) : WorkTimeStart;
            WorkTime = new TimeSpanRange(start, WorkTimeEnd);
        }
        protected void OnDaysChanged() {
            if(Days == null) {
                Days = DefaultWorkDays;
                return;
            }
            WorkDays = ConvertWorkDays(Days);
        }
    }
}
