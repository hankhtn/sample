Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace SchedulingDemo.ViewModels
    Public Class ReceptionDeskDemoViewModel
        Private ReadOnly data As ReceptionDeskData

        Private selectedDoctors_Renamed As List(Of Object)

        Protected Sub New()
            data = New ReceptionDeskData()
            selectedDoctors_Renamed = New List(Of Object)()
            selectedDoctors_Renamed.AddRange(Doctors.Take(5))
        End Sub

        Public ReadOnly Property StartDate() As Date
            Get
                Return ReceptionDeskData.BaseDate
            End Get
        End Property
        Public Overridable ReadOnly Property Doctors() As ObservableCollection(Of Doctor)
            Get
                Return data.Doctors
            End Get
        End Property
        Public Overridable Property SelectedDoctors() As List(Of Object)
            Get
                Return selectedDoctors_Renamed
            End Get
            Set(ByVal value As List(Of Object))
                selectedDoctors_Renamed = value
            End Set
        End Property
        Public Overridable ReadOnly Property MedicalAppointments() As ObservableCollection(Of MedicalAppointment)
            Get
                Return data.MedicalAppointments
            End Get
        End Property
        Public Overridable ReadOnly Property HospitalDepartments() As ObservableCollection(Of HospitalDepartment)
            Get
                Return data.HospitalDepartments
            End Get
        End Property
        Public Overridable ReadOnly Property Patients() As ObservableCollection(Of Patient)
            Get
                Return data.Patients
            End Get
        End Property
        Public Overridable ReadOnly Property AppointmentTypes() As ObservableCollection(Of MedicalAppointmentType)
            Get
                Return data.Labels
            End Get
        End Property
        Public Overridable ReadOnly Property PaymentStates() As ObservableCollection(Of PaymentState)
            Get
                Return data.Statuses
            End Get
        End Property
    End Class
End Namespace
