Imports System
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler.Native

Namespace SchedulerDemo



    Partial Public Class ResourceSharing
        Inherits SchedulerDemoModule

        Private Const AttendeeDescription As String = "The following persons are invited:" & ControlChars.CrLf

        Public Sub New()
            InitializeComponent()

            DemoUtils.FillResources(scheduler.Storage, Integer.MaxValue)
            Dim storage As SchedulerStorage = scheduler.Storage

            PrepareMeetings()

            AddHandler storage.AppointmentInserting, AddressOf Storage_AppointmentChanging
            AddHandler storage.AppointmentChanging, AddressOf Storage_AppointmentChanging
        End Sub

        #Region "data initialization"
        Private Sub PrepareMeetings()
            Dim schedulerStorage As SchedulerStorage = scheduler.Storage
            Dim resourceCount As Integer = schedulerStorage.ResourceStorage.Count
            System.Diagnostics.Debug.Assert(resourceCount = 8)

            schedulerStorage.BeginUpdate()
            Try
                Dim today As Date = Date.Today

                Dim apt As Appointment = CreateMeeting("Morning meeting", today.Add(TimeSpan.FromHours(9)), New Integer() { 1, 3, 5 })
                apt.StatusKey = 2
                apt.LabelKey = 2
                schedulerStorage.AppointmentStorage.Add(apt)

                Dim dayShift As Integer = DemoUtils.RandomInstance.Next(3)
                apt = CreateMeeting("Product delivery planning", today.Add(TimeSpan.FromDays(dayShift).Add(TimeSpan.FromHours(12))), New Integer() { 2, 4, 5 })
                apt.StatusKey = 2
                apt.LabelKey = 5
                schedulerStorage.AppointmentStorage.Add(apt)

                dayShift = DemoUtils.RandomInstance.Next(3)
                apt = CreateMeeting("New product concept presentation", today.Add(TimeSpan.FromDays(dayShift).Add(TimeSpan.FromHours(14))), New Integer() { 2, 3, 6 })
                apt.StatusKey = 1
                apt.LabelKey = 6
                schedulerStorage.AppointmentStorage.Add(apt)

                dayShift = DemoUtils.RandomInstance.Next(3)
                apt = CreateMeeting("Discussion", today.Add(TimeSpan.FromDays(dayShift).Add(TimeSpan.FromHours(16))), New Integer() { 1, 2, 3, 5 })
                apt.StatusKey = 2
                apt.LabelKey = 5
                schedulerStorage.AppointmentStorage.Add(apt)

                dayShift = DemoUtils.RandomInstance.Next(3)
                apt = CreateMeeting("New employee interview", today.Add(TimeSpan.FromDays(dayShift).Add(TimeSpan.FromHours(11))), New Integer() { 2, 3 })
                apt.StatusKey = 1
                apt.LabelKey = 4
                schedulerStorage.AppointmentStorage.Add(apt)
            Finally
                schedulerStorage.EndUpdate()
            End Try
        End Sub
        Private Function CreateMeeting(ByVal subject As String, ByVal [date] As Date, ByVal participants() As Integer) As Appointment
            Dim schedulerStorage As SchedulerStorage = scheduler.Storage
            Dim apt As Appointment = schedulerStorage.CreateAppointment(AppointmentType.Normal)
            apt.Start = [date]
            apt.Duration = TimeSpan.FromHours(1)
            apt.Subject = subject

            Dim description As String = AttendeeDescription
            Dim count As Integer = participants.Length
            For i As Integer = 0 To count - 1
                Dim resource As Resource = schedulerStorage.ResourceStorage(participants(i))
                description &= String.Format("{0}" & ControlChars.CrLf, resource.Caption)
                apt.ResourceIds.Add(resource.Id)
            Next i
            apt.Description = description
            Return apt
        End Function
        #End Region

        Private Sub Storage_AppointmentChanging(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
            Dim apt As Appointment = TryCast(e.Object, Appointment)
            apt.Description = AttendeeDescription & GetAttendeeNames(apt.ResourceIds)
        End Sub

        Private Function GetAttendeeNames(ByVal resIds As AppointmentResourceIdCollection) As String
            Dim result As String = String.Empty
            Dim resources As ResourceCollection = scheduler.Storage.ResourceStorage.Items
            For Each resource As Resource In resources
                If resIds.Contains(resource.Id) Then
                    result &= String.Format("{0}" & ControlChars.CrLf, resource.Caption)
                End If
            Next resource
            Return result
        End Function

    End Class

End Namespace
