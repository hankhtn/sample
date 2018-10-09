Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Scheduling

Namespace SchedulingDemo.ViewModels
    Public Class EndUserRestrictionsDemoViewModel
        Protected Sub New()
            Start = TeamData.Start
            Calendars = New ObservableCollection(Of TeamCalendar)(TeamData.Calendars)
            Appointments = New ObservableCollection(Of TeamAppointment)(TeamData.AllAppointments)
        End Sub

        Public Overridable Property Start() As Date
        Public Overridable Property Calendars() As IEnumerable(Of TeamCalendar)
        Public Overridable Property Appointments() As IEnumerable(Of TeamAppointment)
        Public Overridable Property IsTeamCalendarReadonly() As Boolean
        Public Overridable Property SelectedResource() As ResourceItem

        Public Sub CustomAllowAppointmentItemOperation(ByVal e As AppointmentItemOperationEventArgs)
            If e.Appointment Is Nothing AndAlso SelectedResource Is Nothing Then
                Return
            End If
            e.Allow = CalcRestriction(If(e.Appointment Is Nothing, SelectedResource.Id, e.Appointment.ResourceId), e.Allow)
        End Sub
        Public Sub AppointmentDrop(ByVal e As AppointmentItemDragDropEventArgs)
            Dim allow As Boolean = e.Allow
            For Each viewModel As AppointmentDragResizeViewModel In e.ViewModels
                allow = CalcRestriction(viewModel.Resource.Id, allow)
                If Not allow Then
                    Exit For
                End If
            Next viewModel
            e.Allow = allow
        End Sub
        Private Function CalcRestriction(ByVal resourceId As Object, ByVal controlRestriction As Boolean) As Boolean
            Return If(IsTeamCalendarReadonly AndAlso Object.Equals(resourceId, 1), False, controlRestriction)
        End Function
    End Class
End Namespace
