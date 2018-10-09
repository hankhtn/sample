using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Scheduling;

namespace SchedulingDemo {
    public class MedicalAppointmentWindowViewModel : AppointmentWindowViewModel {
        public static MedicalAppointmentWindowViewModel Create(AppointmentItem appointmentItem, SchedulerControl scheduler, ObservableCollection<Patient> patients) {
            return ViewModelSource.Create(() => new MedicalAppointmentWindowViewModel(appointmentItem, scheduler, patients));
        }

        readonly ObservableCollection<Patient> patients;
        Patient patient;

        protected MedicalAppointmentWindowViewModel(AppointmentItem appointmentItem, SchedulerControl scheduler, ObservableCollection<Patient> patients) : base(appointmentItem, scheduler) {
            this.patients = patients;
            IList<Patient> patientList = patients.Where(x => x.Id.Equals(CustomFields["PatientId"])).ToList();
            if (patientList != null && patientList.Count > 0)
                patient = patientList[0];
        }

        public ObservableCollection<Patient> Patients { get { return this.patients; } }

        [BindableProperty]
        public virtual Patient Patient {
            get { return patient; }
            set {
                Patient newPatient = value;
                if (patient == newPatient)
                    return;
                patient = newPatient;
                CustomFields["PatientId"] = newPatient.Id;
                Subject = newPatient.Name;
            }
        }
     }
}
