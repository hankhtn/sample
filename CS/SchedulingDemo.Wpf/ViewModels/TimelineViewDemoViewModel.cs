using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SchedulingDemo.ViewModels {
    public class TimelineViewDemoViewModel {
        readonly ReceptionDeskData data = new ReceptionDeskData();
        protected TimelineViewDemoViewModel() {
            Start = ReceptionDeskData.BaseDate;
        }
        public virtual DateTime Start { get; set; }
        public virtual ObservableCollection<Doctor> Doctors { get { return this.data.Doctors; } }
        public virtual ObservableCollection<MedicalAppointment> MedicalAppointments { get { return this.data.MedicalAppointments; } }
        public virtual ObservableCollection<MedicalAppointmentType> AppointmentTypes { get { return this.data.Labels; } }
        public virtual ObservableCollection<PaymentState> PaymentStates { get { return this.data.Statuses; } }
    }
}
