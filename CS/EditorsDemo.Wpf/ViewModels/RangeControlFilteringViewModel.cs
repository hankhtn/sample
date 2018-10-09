using DevExpress.Mvvm.DataAnnotations;
using EditorsDemo.SCService;
using System;

namespace EditorsDemo {
    public class RangeControlFilteringViewModel {
        public RangeControlFilteringViewModel() {
            WcfSCService = new SCEntities(new Uri("http://demos.devexpress.com/Services/WcfLinqSC/WcfSCService.svc/"));
            StartDate = new DateTime(2007, 1, 1);
            EndDate = new DateTime(2009, 1, 1);
            SelectedStart = new DateTime(2008, 1, 1);
            SelectedEnd = new DateTime(2008, 7, 1);
            VisibleStartDate = SelectedStart.AddMonths(-6);
            VisibleEndDate = SelectedEnd.AddMonths(6);
        }
        public virtual SCEntities WcfSCService { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        [BindableProperty(true, OnPropertyChangedMethodName = "UpdateFilter")]
        public virtual DateTime SelectedStart { get; set; }
        [BindableProperty(true, OnPropertyChangedMethodName = "UpdateFilter")]
        public virtual DateTime SelectedEnd { get; set; }
        public virtual DateTime VisibleStartDate { get; set; }
        public virtual DateTime VisibleEndDate { get; set; }
        public virtual string FilterString { get; set; }

        protected void UpdateFilter() {
            FilterString = String.Format("([CreatedOn] >= #{0}# AND [CreatedOn] < #{1}#)", SelectedStart, SelectedEnd);
        }
    }
}
