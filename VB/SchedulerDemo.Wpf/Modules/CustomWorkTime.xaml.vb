Imports System
Imports System.Windows
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler.Native
Imports SchedulerDemo.Internal

Namespace SchedulerDemo
    Partial Public Class CustomWorkTime
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeSchedulerProperties(scheduler)
            DemoUtils.FillResources(scheduler.Storage, 5)
            InitAppointments()

            scheduler.Start = Date.Now
        End Sub
        Private Sub InitAppointments()
            Dim storage As SchedulerStorage = scheduler.Storage
            Dim eventList As New SchedulerBindingList(Of CustomEvent)()
            GenerateEvents(storage, eventList)
            storage.AppointmentStorage.DataSource = eventList
        End Sub
        Private Sub GenerateEvents(ByVal storage As SchedulerStorage, ByVal eventList As SchedulerBindingList(Of CustomEvent))
            Dim count As Integer = storage.ResourceStorage.Count
            For i As Integer = 0 To count - 1
                Dim resource As Resource = CType(storage.ResourceStorage(i), Resource)
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
            apt.StartTime = Date.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
            apt.EndTime = apt.StartTime.Add(TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4)))
            apt.Status = status
            apt.Label = label
            Return apt
        End Function


        Private workTimes() As TimeOfDayInterval = {
            New TimeOfDayInterval(TimeSpan.FromHours(0), TimeSpan.FromHours(16)),
            New TimeOfDayInterval(TimeSpan.FromHours(10), TimeSpan.FromHours(20)),
            Nothing,
            New TimeOfDayInterval(TimeSpan.FromHours(7), TimeSpan.FromHours(15)),
            New TimeOfDayInterval(TimeSpan.FromHours(16), TimeSpan.FromHours(24))
        }

        Private Sub scheduler_QueryWorkTime(ByVal sender As Object, ByVal e As QueryWorkTimeEventArgs)
            If chkCustomWorkTime IsNot Nothing Then
                If Not CBool(chkCustomWorkTime.IsChecked) Then
                    Return
                End If
            End If

            If scheduler IsNot Nothing Then
                Dim schedulerStorage As SchedulerStorage = scheduler.Storage
                If schedulerStorage.ResourceStorage Is Nothing Then
                    Return
                End If

                Dim resourceIndex As Integer = schedulerStorage.ResourceStorage.Items.IndexOf(e.Resource)
                If resourceIndex >= 0 Then
                    If resourceIndex = 0 Then
                        If (e.Interval.Start.Day Mod 2) = 0 Then
                            e.WorkTime = workTimes(resourceIndex Mod workTimes.Length)
                        Else
                            e.WorkTimes.Add(New TimeOfDayInterval(TimeSpan.FromHours(8), TimeSpan.FromHours(13)))
                            e.WorkTimes.Add(New TimeOfDayInterval(TimeSpan.FromHours(14), TimeSpan.FromHours(18)))
                        End If
                    Else
                        If scheduler.WorkDays.IsWorkDay(e.Interval.Start.Date) Then
                            e.WorkTime = workTimes(resourceIndex Mod workTimes.Length)
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub chkCustomWorkTime_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            scheduler.ActiveView.LayoutChanged()
        End Sub
    End Class
End Namespace
