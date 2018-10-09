Imports System
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler
Imports System.Collections
Imports System.ComponentModel
Imports DevExpress.Xpf.Scheduler
Imports SchedulerDemo.Internal

Namespace SchedulerDemo



    Partial Public Class ListBoundMode
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeSchedulerProperties(scheduler)
            InitializeResources()
            InitializeAppointments()
        End Sub
        Private Sub InitializeResources()
            Dim count As Integer = DemoUtils.ResourceList.Count
            Dim resources As New List(Of CustomResource)()
            For i As Integer = 0 To count - 1
                Dim customResource As New CustomResource()
                customResource.Caption = DemoUtils.ResourceList(i)
                customResource.Id = i
                resources.Add(customResource)
            Next i
            Me.scheduler.Storage.ResourceStorage.DataSource = resources
        End Sub
        Private Sub InitializeAppointments()
            Dim eventList As New SchedulerBindingList(Of CustomEvent)()
            GenerateEvents(eventList)
            Me.scheduler.Storage.AppointmentStorage.DataSource = eventList

        End Sub
        Private Sub GenerateEvents(ByVal eventList As SchedulerBindingList(Of CustomEvent))
            Dim count As Integer = Me.scheduler.Storage.ResourceStorage.Count
            For i As Integer = 0 To count - 1
                Dim resource As Resource = Me.scheduler.Storage.ResourceStorage(i)
                Dim subjPrefix As String = resource.Caption & "'s "
                eventList.Add(CreateEvent(eventList, subjPrefix & "meeting", resource.Id, 2, 5))
                eventList.Add(CreateEvent(eventList, subjPrefix & "travel", resource.Id, 3, 6))
                eventList.Add(CreateEvent(eventList, subjPrefix & "phone call", resource.Id, 0, 10))
            Next i
        End Sub
        Private Function CreateEvent(ByVal eventList As SchedulerBindingList(Of CustomEvent), ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As CustomEvent
            Dim apt As New CustomEvent()
            apt.Subject = subject
            apt.OwnerId = resourceId
            Dim rnd As Random = DemoUtils.RandomInstance
            Dim rangeInMinutes As Integer = 60 * 24
            apt.StartTime = New Date(2016, 7, 13) + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
            apt.EndTime = apt.StartTime.Add(TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4)))
            apt.Status = status
            apt.Label = label
            Return apt
        End Function
    End Class
End Namespace
