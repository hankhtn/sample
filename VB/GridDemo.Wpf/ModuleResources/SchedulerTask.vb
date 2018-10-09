Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData
Imports DevExpress.DemoData.Models

Namespace GridDemo
    Public Class SchedulerTask
        Public Property Subject() As String
        Public Property Priority() As IssuePriority
        Public Property Duration() As TimeSpan
        Public ReadOnly Property TotalHours() As Integer
            Get
                Return CInt((Math.Ceiling(Duration.TotalHours)))
            End Get
        End Property
        Public Property Description() As String
        Public ReadOnly Property Name() As String
            Get
                Return If((Not String.IsNullOrEmpty(Description)), Description, Subject)
            End Get
        End Property
        Public Property AllDay() As Boolean

        Public Overrides Function ToString() As String
            Return Subject
        End Function
    End Class

    Public Class Appointment
        Public Property Id() As Integer
        Public Property LabelId() As IssuePriority
        Public Property Start() As Date
        Public Property [End]() As Date
        Public Property OwnerId() As Long
        Public Property Subject() As String
        Public Property Description() As String
        Public Property AllDay() As Boolean
    End Class

    Public Class AppointmentType
        Public Property Priority() As IssuePriority
        Public ReadOnly Property Caption() As String
            Get
                Return Priority.ToString()
            End Get
        End Property
    End Class

    Public Class SchedulerTaskProvider
        Private privateInbox As IList(Of SchedulerTask)
        Public Property Inbox() As IList(Of SchedulerTask)
            Get
                Return privateInbox
            End Get
            Protected Set(ByVal value As IList(Of SchedulerTask))
                privateInbox = value
            End Set
        End Property
        Private privateEmployees As IList(Of Employee)
        Public Property Employees() As IList(Of Employee)
            Get
                Return privateEmployees
            End Get
            Protected Set(ByVal value As IList(Of Employee))
                privateEmployees = value
            End Set
        End Property
        Private privateAppointmentTypes As IList(Of AppointmentType)
        Public Property AppointmentTypes() As IList(Of AppointmentType)
            Get
                Return privateAppointmentTypes
            End Get
            Protected Set(ByVal value As IList(Of AppointmentType))
                privateAppointmentTypes = value
            End Set
        End Property
        Private privateAppointments As IList(Of Appointment)
        Public Property Appointments() As IList(Of Appointment)
            Get
                Return privateAppointments
            End Get
            Protected Set(ByVal value As IList(Of Appointment))
                privateAppointments = value
            End Set
        End Property

        Public Sub New()
            GenerateData()
        End Sub
        Private Sub GenerateData()
            Const appointmentsCount As Integer = 4
            Dim inboxTaskDescriptions() As String = GetAllSubjects().Skip(appointmentsCount).ToArray()
            Dim appointmentsDescriptions() As String = GetAllSubjects().Take(appointmentsCount).ToArray()
            Inbox = GenerateSchedulerTasks(inboxTaskDescriptions)
            Employees = GenerateEmployees()
            Appointments = GenerateAppointments(appointmentsDescriptions, GetOwnerIds(Employees))
            AppointmentTypes = GenerateAppointmentTypes()
        End Sub

        Private Shared Function GetAllSubjects() As IEnumerable(Of String)
            Return OutlookDataGenerator.Subjects
        End Function
        Private Shared Function GenerateSchedulerTasks(ByVal descriptions() As String) As IList(Of SchedulerTask)
            Dim severityRandom = New Random()
            Dim priorityRandom = New Random()
            Dim durationRandom = New Random()
            Return descriptions.Select(Function(d) CreateSchedulerTask(d, severityRandom, priorityRandom, durationRandom)).ToList()
        End Function
        Private Shared Function CreateSchedulerTask(ByVal description As String, ByVal severityRandom As Random, ByVal priorityRandom As Random, ByVal durationRandom As Random) As SchedulerTask
            Return New SchedulerTask With {.Subject = GetSubject(description), .Priority = GetRandomEnumValue(Of IssuePriority)(priorityRandom), .Duration = TimeSpan.FromMinutes(durationRandom.Next(1, 5) * 60), .Description = description}
        End Function
        Private Shared Function GetSubject(ByVal description As String) As String
            If Not String.IsNullOrWhiteSpace(description) Then
                Dim charLocation As Integer = description.IndexOf("."c)
                If charLocation > 0 Then
                    Return description.Substring(0, charLocation)
                End If
            End If
            Return String.Empty
        End Function
        Private Shared Function GetRandomEnumValue(Of T)(ByVal random As Random) As T
            Dim values As Array = System.Enum.GetValues(GetType(T))
            Return DirectCast(values.GetValue(random.Next(values.Length)), T)
        End Function
        Private Shared Function GenerateEmployees() As IList(Of Employee)
            Const resourceCount As Integer = 3
            Return NWindDataProvider.Employees.Take(resourceCount).ToList()
        End Function
        Private Shared Function GetOwnerIds(ByVal employees As IEnumerable(Of Employee)) As Long()
            Return employees.Select(Function(x) x.EmployeeID).ToArray()
        End Function
        Private Shared Function GenerateAppointmentTypes() As IList(Of AppointmentType)
            Dim appointmentType = New List(Of AppointmentType)()
            appointmentType.Add(New AppointmentType With {.Priority = IssuePriority.Low})
            appointmentType.Add(New AppointmentType With {.Priority = IssuePriority.High})
            appointmentType.Add(New AppointmentType With {.Priority = IssuePriority.Medium})
            Return appointmentType
        End Function
        Private Shared Function GenerateAppointments(ByVal descriptions() As String, ByVal ownerIds() As Long) As IList(Of Appointment)

            Dim appointments_Renamed = New List(Of Appointment)()
            appointments_Renamed.Add(New Appointment With {.Id = 0, .Subject = GetSubject(descriptions(0)), .Description = descriptions(0), .OwnerId = ownerIds(0), .LabelId = IssuePriority.Low, .Start = GetTime(9, 30), .End = GetTime(10, 30)})
            appointments_Renamed.Add(New Appointment With {.Id = 1, .Subject = GetSubject(descriptions(1)), .Description = descriptions(1), .OwnerId = ownerIds(1), .LabelId = IssuePriority.Medium, .Start = GetTime(9, 30), .End = GetTime(10, 30)})
            appointments_Renamed.Add(New Appointment With {.Id = 2, .Subject = GetSubject(descriptions(2)), .Description = descriptions(2), .OwnerId = ownerIds(1), .LabelId = IssuePriority.Low, .Start = GetTime(11, 0), .End = GetTime(12, 0)})
            appointments_Renamed.Add(New Appointment With {.Id = 3, .Subject = GetSubject(descriptions(3)), .Description = descriptions(3), .OwnerId = ownerIds(2), .LabelId = IssuePriority.High, .Start = GetTime(9, 30), .End = GetTime(12, 0)})
            Return appointments_Renamed
        End Function
        Private Shared Function GetTime(ByVal hours As Integer, ByVal minutes As Integer) As Date
            Return Date.Today.AddHours(hours).AddMinutes(minutes)
        End Function
    End Class
End Namespace
