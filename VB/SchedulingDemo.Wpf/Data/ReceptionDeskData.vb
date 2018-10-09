Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.XtraScheduler.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI.Native

Namespace SchedulingDemo
    Public Class Patient
        Public Shared Function Create() As Patient
            Return ViewModelSource.Create(Function() New Patient())
        End Function

        Protected Sub New()
        End Sub
        Public Overridable Property Id() As Integer
        Public Overridable Property Name() As String
        Public Overridable Property BirthDate() As Date
        Public Overridable Property Phone() As String
    End Class
    Public Class HospitalDepartment
        Public Shared Function Create() As HospitalDepartment
            Return ViewModelSource.Create(Function() New HospitalDepartment())
        End Function

        Protected Sub New()
            Doctors = New ObservableCollection(Of Doctor)()
        End Sub

        Public Overridable Property Id() As Integer
        Public Overridable Property Name() As String
        Private privateDoctors As ObservableCollection(Of Doctor)
        Public Property Doctors() As ObservableCollection(Of Doctor)
            Get
                Return privateDoctors
            End Get
            Private Set(ByVal value As ObservableCollection(Of Doctor))
                privateDoctors = value
            End Set
        End Property
    End Class
    Public Class Doctor
        Public Shared Function Create() As Doctor
            Return ViewModelSource.Create(Function() New Doctor())
        End Function
        Protected Sub New()
        End Sub

        Public Overridable Property Id() As Integer
        Public Overridable Property Name() As String
        Public Overridable Property Phone() As String
        Public Overridable Property Photo() As ImageSource
        Public Overridable Property DepartmentId() As Integer?
        Public Overridable Property DepartmentName() As String
        Public Overridable Property IsVisible() As Boolean
    End Class
    Public Class MedicalAppointmentType
        Public Shared Function Create() As MedicalAppointmentType
            Return ViewModelSource.Create(Function() New MedicalAppointmentType())
        End Function
        Protected Sub New()
        End Sub
        Public Overridable Property Id() As Integer
        Public Overridable Property Caption() As String
        Public Overridable Property Color() As Color
    End Class

    Public Class PaymentState
        Public Shared Function Create() As PaymentState
            Return ViewModelSource.Create(Function() New PaymentState())
        End Function

        Protected Sub New()
        End Sub
        Public Overridable Property Id() As Integer
        Public Overridable Property Caption() As String
        Public Overridable Property Brush() As Brush
    End Class
    Public Class MedicalAppointment
        Public Shared Function Create() As MedicalAppointment
            Return ViewModelSource.Create(Function() New MedicalAppointment())
        End Function

        Protected Sub New()
        End Sub
        Public Overridable Property Id() As Integer
        Public Overridable Property AllDay() As Boolean
        Public Overridable Property StartTime() As Date
        Public Overridable Property EndTime() As Date
        Public Overridable Property PatientId() As Integer?
        Public Overridable Property Note() As String
        Public Overridable Property Subject() As String
        Public Overridable Property PaymentStateId() As Integer
        Public Overridable Property IssueId() As Integer
        Public Overridable Property Type() As Integer
        Public Overridable Property Location() As String
        Public Overridable Property RecurrenceInfo() As String
        Public Overridable Property ReminderInfo() As String
        Public Overridable Property DoctorId() As Integer?
    End Class

    Public Class ReceptionDeskData
        Public Shared BaseDate As Date = Date.Today

        Public Shared AppointmentTypes() As String = { "Hospital", "Office", "Phone Consultation", "Home", "Hospice" }

        Public Shared PaymentStates() As String = { "Paid", "Unpaid" }
        Public Shared PaymentBrushStates() As SolidColorBrush = { BrushesCache.GetBrush(Color.FromRgb(&Hec, &H70, &H63)), BrushesCache.GetBrush(Color.FromRgb(&H45, &Hb3, &H9d)) }

        Public Shared PatientNames() As String = { "Andrew Glover", "Mark Oliver", "Taylor Riley", "Addison Davis", "Benjamin Hughes", "Lucas Smith", "Robert King", "Laura Callahan", "Miguel Simmons", "Isabella Carter", "Andrew Fuller", "Madeleine Russell", "Steven Buchanan", "Nancy Davolio", "Michael Suyama", "Margaret Peacock", "Janet Leverling", "Ariana Alexander", "Brad Farkus", "Bart Arnaz", "Arnie Schwartz", "Billy Zimmer", "Samantha Piper", "Maggie Boxter", "Terry Bradley", "Greta Sims", "Cindy Stanwick", "Marcus Orbison", "Sandy Bright", "Ken Samuelson", "Brett Wade", "Wally Hobbs", "Brad Jameson", "Karen Goodson", "Morgan Kennedy", "Violet Bailey", "John Heart", "Arthur Miller", "Robert Reagan", "Ed Holmes", "Sammy Hill", "Olivia Peyton", "Jim Packard", "Hannah Brookly", "Harv Mudd", "Todd Hoffman", "Kevin Carter","Mary Stern", "Robin Cosworth","Jenny Hobbs", "Dallas Lou"}

        Public Shared DepartmentCache As New Dictionary(Of String, String())()

        Private Shared rnd As New Random()

        Shared Sub New()
            DepartmentCache.Add("Therapy", New String() { "Lincoln Bartlett", "Amelia Harper", "Stu Pizaro", "Sandra Johnson", "Victor Norris" })
            DepartmentCache.Add("Ophthalmology", New String() { "Lucy Ball" })
            DepartmentCache.Add("Dentistry", New String() { "Clark Morgan", "Leah Simpson" })
            DepartmentCache.Add("Surgery", New String() { "Davey Jones" })
            DepartmentCache.Add("Neurology", New String() { "Samantha Bright" })
        End Sub
        Private Shared Function CalculateAppointmentDuration(ByVal resource As Doctor, ByVal density As Integer) As TimeSpan
            Select Case resource.DepartmentId
                Case 1
                    Return New TimeSpan(0, rnd.Next(2, 4) * 10, 0)
                Case 2
                    Return New TimeSpan(0, rnd.Next(3, 6) * 10, 0)
                Case 3
                    Return New TimeSpan(rnd.Next(0, 1), rnd.Next(3, 4) * 10, 0)
                Case 4
                    Return New TimeSpan(rnd.Next(0, 1), rnd.Next(2, 5) * 10, 0)
                Case 5
                    Return New TimeSpan(0, rnd.Next(2, 3) * 10, 0)
                Case Else
                    Return New TimeSpan(0, rnd.Next(2, 3) * 10, 0)
            End Select
        End Function

        Private Sub CreatePatients()
            Dim result As New ObservableCollection(Of Patient)()
            Dim patientCount As Integer = PatientNames.Length
            Dim patientId As Integer = 1
            Dim birthday As New Date(1975, 2, 5)
            For i As Integer = 0 To patientCount - 1

                Dim patient_Renamed As Patient = SchedulingDemo.Patient.Create()
                patient_Renamed.Id = patientId
                patientId += 1
                patient_Renamed.Name = PatientNames(i)
                patient_Renamed.BirthDate = birthday.AddMonths(rnd.Next(1, 12)).AddYears(rnd.Next(0, 20))
                patient_Renamed.Phone = "(" & rnd.Next(100, 999) & ") " & rnd.Next(100, 999) & "-" & rnd.Next(1000, 9999)
                result.Add(patient_Renamed)
            Next i
            Patients = result
        End Sub

        Private Sub CreateHospitalDepartments()
            Dim departments As New ObservableCollection(Of HospitalDepartment)()
            Dim departmentId As Integer = 1
            For Each name As String In DepartmentCache.Keys
                Dim department As HospitalDepartment = HospitalDepartment.Create()
                department.Id = departmentId
                departmentId += 1
                department.Name = name
                departments.Add(department)
            Next name
            HospitalDepartments = departments
        End Sub

        Private Sub CreateDoctors()

            Dim departmentCache_Renamed As Dictionary(Of String, String()) = ReceptionDeskData.DepartmentCache
            Dim allDoctors As New ObservableCollection(Of Doctor)()
            Dim medicId As Integer = 0

            For Each hospitalDepartment_Renamed As HospitalDepartment In HospitalDepartments

                Dim doctors_Renamed As ObservableCollection(Of Doctor) = hospitalDepartment_Renamed.Doctors
                Dim medicNames() As String = departmentCache_Renamed(hospitalDepartment_Renamed.Name)
                Dim medicCount As Integer = medicNames.Length
                For i As Integer = 0 To medicCount - 1

                    Dim doctor_Renamed As Doctor = SchedulingDemo.Doctor.Create()
                    doctor_Renamed.Id = medicId
                    medicId += 1
                    doctor_Renamed.Name = medicNames(i)
                    doctor_Renamed.Phone = "(" & rnd.Next(10, 99) & ") " & rnd.Next(100, 999) & "-" & rnd.Next(1000, 9999)
                    doctor_Renamed.DepartmentId = hospitalDepartment_Renamed.Id
                    doctor_Renamed.DepartmentName = hospitalDepartment_Renamed.Name
                    doctor_Renamed.IsVisible = hospitalDepartment_Renamed.Id = 1
                    Dim imageName As String = String.Format("Images/Medics/{0}.png", doctor_Renamed.Name.Replace(" ", ""))
                    doctor_Renamed.Photo = New BitmapImage(New Uri(String.Format("pack://application:,,,/SchedulingDemo;component/{0}", imageName)))
                    doctor_Renamed.Photo.Freeze()
                    doctors_Renamed.Add(doctor_Renamed)
                    allDoctors.Add(doctor_Renamed)
                Next i
            Next hospitalDepartment_Renamed
            Doctors = allDoctors
        End Sub

        Private Sub CreateMedicalAppointments(ByVal apptDensity As Integer)
            Dim appointmentId As Integer = 1
            Dim patientIndex As Integer = 0
            Dim [date] As Date = DateTimeHelper.GetStartOfWeek(BaseDate)
            Dim result As New ObservableCollection(Of MedicalAppointment)()

            For Each doctor_Renamed As Doctor In Doctors
                Dim duration As TimeSpan = CalculateAppointmentDuration(doctor_Renamed, apptDensity)
                Dim firstDate As New Date([date].Year, [date].Month, [date].Day, rnd.Next(9, 11), 0, 0)
                Dim startDate As Date = firstDate
                Do While startDate < firstDate.AddDays(10)
                    Dim endTime As New TimeSpan(18, 0, 0)
                    endTime = endTime.Add(-duration)
                    Dim endDate As New Date(startDate.Year, startDate.Month, startDate.Day, endTime.Hours, endTime.Minutes, 0)
                    Dim room As Integer = rnd.Next(1, 100)
                    Dim startTime As Date = startDate
                    Do While startTime < endDate
                        result.Add(CreateMedicAppointment(appointmentId, doctor_Renamed, Patients(patientIndex), startTime, duration, room))
                        appointmentId += 1
                        patientIndex += 1
                        If patientIndex >= Patients.Count - 1 Then
                            patientIndex = 1
                        End If
                        startTime = startTime.Add(duration.Add(New TimeSpan(0, rnd.Next(2, 4) * apptDensity * 10, 0)))
                    Loop
                    startDate = startDate.Add(TimeSpan.FromDays(1))
                Loop
            Next doctor_Renamed
            MedicalAppointments = result
        End Sub

        Private Sub CreateLabels()
            Dim result As New ObservableCollection(Of MedicalAppointmentType)()
            Dim count As Integer = AppointmentTypes.Length
            For i As Integer = 0 To count - 1
                Dim appointmentType As MedicalAppointmentType = MedicalAppointmentType.Create()
                appointmentType.Id = i
                appointmentType.Caption = AppointmentTypes(i)
                result.Add(appointmentType)
            Next i
            Labels = result
        End Sub

        Private Sub CreateStatuses()
            Dim result As New ObservableCollection(Of PaymentState)()
            Dim count As Integer = PaymentStates.Length
            For i As Integer = 0 To count - 1

                Dim paymentState_Renamed As PaymentState = SchedulingDemo.PaymentState.Create()
                paymentState_Renamed.Id = i
                paymentState_Renamed.Brush = PaymentBrushStates(i)
                paymentState_Renamed.Caption = PaymentStates(i)
                result.Add(paymentState_Renamed)
            Next i
            Statuses = result
        End Sub


        Private Function CreateMedicAppointment(ByVal appointmentId As Integer, ByVal doctor_Renamed As Doctor, ByVal patient_Renamed As Patient, ByVal start As Date, ByVal duration As TimeSpan, ByVal room As Integer) As MedicalAppointment

            Dim medicalAppointment_Renamed As MedicalAppointment = SchedulingDemo.MedicalAppointment.Create()
            medicalAppointment_Renamed.Id = appointmentId
            medicalAppointment_Renamed.StartTime = start
            medicalAppointment_Renamed.EndTime = start.Add(duration)
            medicalAppointment_Renamed.IssueId = Labels(rnd.Next(0, 5)).Id
            medicalAppointment_Renamed.PaymentStateId = Statuses(rnd.Next(0, 2)).Id
            medicalAppointment_Renamed.Subject = patient_Renamed.Name
            medicalAppointment_Renamed.PatientId = patient_Renamed.Id
            medicalAppointment_Renamed.DoctorId = doctor_Renamed.Id
            If medicalAppointment_Renamed.IssueId <> 3 Then
                medicalAppointment_Renamed.Location = String.Format("{0}", room)
            End If
            Return medicalAppointment_Renamed
        End Function

        Private privateDoctors As ObservableCollection(Of Doctor)
        Public Property Doctors() As ObservableCollection(Of Doctor)
            Get
                Return privateDoctors
            End Get
            Private Set(ByVal value As ObservableCollection(Of Doctor))
                privateDoctors = value
            End Set
        End Property
        Private privateMedicalAppointments As ObservableCollection(Of MedicalAppointment)
        Public Property MedicalAppointments() As ObservableCollection(Of MedicalAppointment)
            Get
                Return privateMedicalAppointments
            End Get
            Private Set(ByVal value As ObservableCollection(Of MedicalAppointment))
                privateMedicalAppointments = value
            End Set
        End Property
        Private privateHospitalDepartments As ObservableCollection(Of HospitalDepartment)
        Public Property HospitalDepartments() As ObservableCollection(Of HospitalDepartment)
            Get
                Return privateHospitalDepartments
            End Get
            Private Set(ByVal value As ObservableCollection(Of HospitalDepartment))
                privateHospitalDepartments = value
            End Set
        End Property
        Private privatePatients As ObservableCollection(Of Patient)
        Public Property Patients() As ObservableCollection(Of Patient)
            Get
                Return privatePatients
            End Get
            Private Set(ByVal value As ObservableCollection(Of Patient))
                privatePatients = value
            End Set
        End Property
        Private privateStatuses As ObservableCollection(Of PaymentState)
        Public Property Statuses() As ObservableCollection(Of PaymentState)
            Get
                Return privateStatuses
            End Get
            Private Set(ByVal value As ObservableCollection(Of PaymentState))
                privateStatuses = value
            End Set
        End Property
        Private privateLabels As ObservableCollection(Of MedicalAppointmentType)
        Public Property Labels() As ObservableCollection(Of MedicalAppointmentType)
            Get
                Return privateLabels
            End Get
            Private Set(ByVal value As ObservableCollection(Of MedicalAppointmentType))
                privateLabels = value
            End Set
        End Property

        Public Sub New(Optional ByVal apptDensity As Integer = 1)
            CreatePatients()
            CreateHospitalDepartments()
            CreateStatuses()
            CreateLabels()
            CreateDoctors()
            CreateMedicalAppointments(apptDensity)
        End Sub
    End Class
End Namespace
