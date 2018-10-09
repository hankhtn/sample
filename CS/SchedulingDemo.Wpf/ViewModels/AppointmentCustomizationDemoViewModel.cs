using System.Collections.Generic;
using DevExpress.Mvvm.POCO;

namespace SchedulingDemo.ViewModels {
    public class AppointmentCustomizationDemoViewModel {
        readonly CarsData data = new CarsData();
        protected AppointmentCustomizationDemoViewModel() {
            CustomFlyout = false;
            CustomTemplate = CustomTemplate_AllDay = false;
            ShowStatus = ShowStatus_AllDay = true;
            ShowLocation = ShowLocation_AllDay = true;
            ShowDescription = ShowDescription_AllDay = true;
            ShowInterval = ShowInterval_AllDay = true;
            IntervalStringFormat = IntervalStringFormat_AllDay = "{0:hh:mm tt} - {1:hh:mm tt}";
        }
        public virtual List<CarScheduling> Events { get { return this.data.Events; } }
        public virtual List<Car> Cars { get { return this.data.Cars; } }

        public virtual bool CustomFlyout { get; set; }

        public virtual bool CustomTemplate { get; set; }
        public virtual bool ShowStatus { get; set; }
        public virtual bool ShowLocation { get; set; }
        public virtual bool ShowDescription { get; set; }
        public virtual bool ShowInterval { get; set; }
        public virtual string IntervalStringFormat { get; set; }

        public virtual bool CustomTemplate_AllDay { get; set; }
        public virtual bool ShowStatus_AllDay { get; set; }
        public virtual bool ShowLocation_AllDay { get; set; }
        public virtual bool ShowDescription_AllDay { get; set; }
        public virtual bool ShowInterval_AllDay { get; set; }
        public virtual string IntervalStringFormat_AllDay { get; set; }
    }
}
