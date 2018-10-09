Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Scheduling

Namespace SchedulingDemo
    Public Class MedicalAppointmentWindowViewModel
        Inherits AppointmentWindowViewModel

        Public Shared Function Create(ByVal appointmentItem As AppointmentItem, ByVal scheduler As SchedulerControl, ByVal patients As ObservableCollection(Of Patient)) As MedicalAppointmentWindowViewModel
            Return ViewModelSource.Create(Function() New MedicalAppointmentWindowViewModel(appointmentItem, scheduler, patients))
        End Function


        Private ReadOnly patients_Renamed As ObservableCollection(Of Patient)

        Private patient_Renamed As Patient

        Protected Sub New(ByVal appointmentItem As AppointmentItem, ByVal scheduler As SchedulerControl, ByVal patients As ObservableCollection(Of Patient))
            MyBase.New(appointmentItem, scheduler)
            Me.patients_Renamed = patients
            Dim patientList As IList(Of Patient) = patients.Where(Function(x) x.Id.Equals(CustomFields("PatientId"))).ToList()
            If patientList IsNot Nothing AndAlso patientList.Count > 0 Then
                patient_Renamed = patientList(0)
            End If
        End Sub

        Public ReadOnly Property Patients() As ObservableCollection(Of Patient)
            Get
                Return Me.patients_Renamed
            End Get
        End Property

        <BindableProperty>
        Public Overridable Property Patient() As Patient
            Get
                Return patient_Renamed
            End Get
            Set(ByVal value As Patient)
                Dim newPatient As Patient = value
                If patient_Renamed Is newPatient Then
                    Return
                End If
                patient_Renamed = newPatient
                CustomFields("PatientId") = newPatient.Id
                Subject = newPatient.Name
            End Set
        End Property
    End Class
End Namespace
