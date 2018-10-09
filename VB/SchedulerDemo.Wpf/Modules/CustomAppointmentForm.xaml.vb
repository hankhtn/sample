Imports System
Imports SchedulerDemo.Forms
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler.Native

Namespace SchedulerDemo



    Partial Public Class CustomAppointmentForm
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            AddCustomFieldsMapping(scheduler.Storage)
            AddHandler scheduler.Storage.FilterAppointment, AddressOf Storage_FilterAppointment
            InitializeScheduler()
            scheduler.OptionsBehavior.RecurrentAppointmentEditAction = RecurrentAppointmentAction.Ask
        End Sub
        Private Sub Storage_FilterAppointment(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
            Dim apt As Appointment = TryCast(e.Object, Appointment)
            If apt Is Nothing Then
                Return
            End If
            If apt.RecurrencePattern IsNot Nothing Then
                If Not IsAllowedRecurrenceType(apt.RecurrencePattern.RecurrenceInfo.Type) Then
                    e.Cancel = True
            ElseIf apt.RecurrenceInfo IsNot Nothing Then
                If Not IsAllowedRecurrenceType(apt.RecurrenceInfo.Type) Then
                    e.Cancel = True
                End If
            End If
            End If
        End Sub
        Private Function IsAllowedRecurrenceType(ByVal type As RecurrenceType) As Boolean
            Return Object.Equals(type, RecurrenceType.Daily) OrElse Object.Equals(type, RecurrenceType.Monthly)
        End Function
        Private Sub AddCustomFieldsMapping(ByVal schedulerStorage As SchedulerStorage)
            Dim customNameMapping As New SchedulerCustomFieldMapping("CustomName", "CustomName")
            Dim customStatusMapping As New SchedulerCustomFieldMapping("CustomStatus", "CustomStatus")
            schedulerStorage.AppointmentStorage.CustomFieldMappings.Add(customNameMapping)
            schedulerStorage.AppointmentStorage.CustomFieldMappings.Add(customStatusMapping)
        End Sub
        Private Sub scheduler_EditAppointmentFormShowing(ByVal sender As Object, ByVal e As DevExpress.Xpf.Scheduler.EditAppointmentFormEventArgs)
            Dim control As SchedulerControl = TryCast(sender, SchedulerControl)
            If Object.Equals(control,Nothing) Then
                Return
            End If
            e.Form = New MyAppointmentEditForm(TryCast(sender, SchedulerControl), e.Appointment)
            e.AllowResize = False
        End Sub
    End Class
End Namespace
