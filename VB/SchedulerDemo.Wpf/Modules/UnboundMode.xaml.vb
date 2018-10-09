Imports System
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler

Namespace SchedulerDemo
    Partial Public Class UnboundMode
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            Me.scheduler.Start = Date.Now
            DemoUtils.FillResources(SchedulerStorage, 5)
            InitAppointments()
        End Sub

        Public ReadOnly Property SchedulerStorage() As SchedulerStorage
            Get
                Return Me.scheduler.Storage
            End Get
        End Property

        Private Sub InitAppointments()
            GenerateEvents(SchedulerStorage)
        End Sub

        Private Sub GenerateEvents(ByVal storage As SchedulerStorage)
            Dim count As Integer = storage.ResourceStorage.Count
            storage.BeginUpdate()
            For i As Integer = 0 To count - 1
                Dim resource As Resource = storage.ResourceStorage(i)
                CreateAppointentsForResource(storage, resource)
            Next i
            storage.EndUpdate()
        End Sub

        Private Sub CreateAppointentsForResource(ByVal storage As SchedulerStorage, ByVal resource As Resource)
            Dim rnd As Random = DemoUtils.RandomInstance
            Dim statusCount As Integer = storage.AppointmentStorage.Statuses.Count
            Dim appointmentKind() As String = { "appointment", "personal time", "meeting", "travel" }
            Dim labelKind() As Integer = { 1, 3, 5, 6 }

            Dim subjPrefix As String = resource.Caption & "'s "
            Dim appointments As AppointmentStorage = storage.AppointmentStorage

            For i As Integer = 0 To 49
                Dim statusId As Integer = rnd.Next(0, statusCount)
                Dim aptKindId As Integer = rnd.Next(0, appointmentKind.Length)
                Dim labelId As Integer = labelKind(aptKindId)

                Dim subject As String = subjPrefix & appointmentKind(aptKindId)
                Dim dateRange As Integer = rnd.Next(0, 20)
                Dim start As Date = Date.Today.AddDays(dateRange - 2)

                appointments.Add(CreateAppointment(start, subject, resource.Id, statusId, labelId))
            Next i
        End Sub

        Private Function CreateAppointment(ByVal start As Date, ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As Appointment
            Dim apt As Appointment = SchedulerStorage.CreateAppointment(AppointmentType.Normal)
            apt.Subject = subject
            apt.ResourceId = resourceId
            Dim rnd As Random = DemoUtils.RandomInstance
            apt.AllDay = rnd.Next(0, 5) = 0

            Dim rangeInMinutes As Integer = 60 * 24
            apt.Start = start.Add(TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes)))
            Dim duration As TimeSpan = TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4))
            If duration.Ticks = 0 Then
                duration = TimeSpan.FromMinutes(5)
            End If

            apt.End = apt.Start + duration
            apt.StatusKey = status
            apt.LabelKey = label
            Return apt
        End Function
    End Class
End Namespace
