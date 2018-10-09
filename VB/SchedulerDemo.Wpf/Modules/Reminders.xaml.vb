Imports System
Imports System.Windows
Imports SchedulerDemo
Imports DevExpress.XtraScheduler

Namespace SchedulerDemo
    Partial Public Class Reminders
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()

            scheduler.Start = Date.Now.Date
            scheduler.Storage.RemindersCheckInterval = 3000
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim now As Date = Date.Now.Add(TimeSpan.FromMinutes(5))
            Dim apt As Appointment = scheduler.Storage.CreateAppointment(AppointmentType.Normal)
            apt.Start = now
            apt.Duration = TimeSpan.FromHours(1)
            apt.Subject = "Appointment with Reminder"
            apt.StatusKey = 1
            apt.LabelKey = 0
            apt.HasReminder = True
            scheduler.Storage.AppointmentStorage.Add(apt)
            scheduler.ActiveView.GotoTimeInterval(New TimeInterval(apt.Start, apt.Duration))
        End Sub
    End Class
End Namespace
