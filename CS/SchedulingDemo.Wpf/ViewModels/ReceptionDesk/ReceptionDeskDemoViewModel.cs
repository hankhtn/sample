using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchedulingDemo.ViewModels {
    public class ReceptionDeskDemoViewModel {
        readonly ReceptionDeskData data;
        List<object> selectedDoctors;

        protected ReceptionDeskDemoViewModel() {
            data = new ReceptionDeskData();
            selectedDoctors = new List<object>();
            selectedDoctors.AddRange(Doctors.Take(5));
        }

        public DateTime StartDate { get { return ReceptionDeskData.BaseDate; } }
        public virtual ObservableCollection<Doctor> Doctors { get { return data.Doctors; } }
        public virtual List<object> SelectedDoctors {
            get { return selectedDoctors; }
            set { selectedDoctors = value; }
        }
        public virtual ObservableCollection<MedicalAppointment> MedicalAppointments { get { return data.MedicalAppointments; } }
        public virtual ObservableCollection<HospitalDepartment> HospitalDepartments { get { return data.HospitalDepartments; } }
        public virtual ObservableCollection<Patient> Patients { get { return data.Patients; } }
        public virtual ObservableCollection<MedicalAppointmentType> AppointmentTypes { get { return data.Labels; } }
        public virtual ObservableCollection<PaymentState> PaymentStates { get { return data.Statuses; } }
    }
}
