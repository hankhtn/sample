Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Xpf.Scheduler.Drawing
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native
Imports System.Windows.Markup
Imports System.IO
Imports DevExpress.Xpf.Core.Native
Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm
Imports System.Collections.ObjectModel



Namespace SchedulerDemo
    Partial Public Class MVVMAppointmentForm
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class AppointmentCustomizationViewModel
        Inherits ViewModelBase

        Public Sub New()
            FillEmployees()
            FillTasks()
        End Sub

        Public Property Doctors() As ObservableCollection(Of Doctor)
        Public Property Appointments() As ObservableCollection(Of HospitalAppointment)

        Private Sub FillEmployees()
            Doctors = New ObservableCollection(Of Doctor)()
            Doctors.Add(New Doctor() With {.Id = 1, .Name = "Stomatologist"})
            Doctors.Add(New Doctor() With {.Id = 2, .Name = "Ophthalmologist"})
            Doctors.Add(New Doctor() With {.Id = 3, .Name = "Surgeon"})
        End Sub

        Private Sub FillTasks()
            Dim rand As New Random(Date.Now.Millisecond)

            Appointments = New ObservableCollection(Of HospitalAppointment)()
            Appointments.Add(New HospitalAppointment() With {.StartDate = Date.Now.Date.AddHours(10), .EndDate = Date.Now.Date.AddHours(11), .DoctorId = 1, .Notes = "", .Location = "101", .PatientName = "Dave Murrel", .InsuranceNumber = GenerateNineNumbers(rand), .FirstVisit = True})
            Appointments.Add(New HospitalAppointment() With {.StartDate = Date.Now.Date.AddDays(2).AddHours(15), .EndDate = Date.Now.Date.AddDays(2).AddHours(16), .DoctorId = 1, .Notes = "", .Location = "101", .PatientName = "Mike Roller", .InsuranceNumber = GenerateNineNumbers(rand), .FirstVisit = True})

            Appointments.Add(New HospitalAppointment() With {.StartDate = Date.Now.Date.AddDays(1).AddHours(11), .EndDate = Date.Now.Date.AddDays(1).AddHours(12), .DoctorId = 2, .Notes = "", .Location = "103", .PatientName = "Bert Parkins", .InsuranceNumber = GenerateNineNumbers(rand), .FirstVisit = True})
            Appointments.Add(New HospitalAppointment() With {.StartDate = Date.Now.Date.AddDays(2).AddHours(10), .EndDate = Date.Now.Date.AddDays(2).AddHours(12), .DoctorId = 2, .Notes = "", .Location = "103", .PatientName = "Carl Lucas", .InsuranceNumber = GenerateNineNumbers(rand), .FirstVisit = False})

            Appointments.Add(New HospitalAppointment() With {.StartDate = Date.Now.Date.AddHours(12), .EndDate = Date.Now.Date.AddHours(13), .DoctorId = 3, .Notes = "Analysis results are required", .Location = "104", .PatientName = "Brad Barnes", .InsuranceNumber = GenerateNineNumbers(rand), .FirstVisit = False})
            Appointments.Add(New HospitalAppointment() With {.StartDate = Date.Now.Date.AddDays(1).AddHours(14), .EndDate = Date.Now.Date.AddDays(1).AddHours(15), .DoctorId = 3, .Notes = "", .Location = "104", .PatientName = "Richard Fisher", .InsuranceNumber = GenerateNineNumbers(rand), .FirstVisit = True})
        End Sub

        Private Function GenerateNineNumbers(ByVal rand As Random) As String
            Dim result As String = ""
            For i As Integer = 0 To 8
                result &= rand.Next(9).ToString()
            Next i
            Return result
        End Function
    End Class

    Public Class HospitalAppointment
        Public Property StartDate() As Date
        Public Property EndDate() As Date
        Public Property PatientName() As String
        Public Property Notes() As String
        Public Property Location() As String
        Public Property DoctorId() As Int64
        Public Property InsuranceNumber() As String
        Public Property FirstVisit() As Boolean
        Public Property Recurrence() As String
        Public Property Type() As Integer
    End Class
End Namespace
