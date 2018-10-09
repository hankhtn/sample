Imports System.Collections.ObjectModel
Imports DevExpress.DemoData.Models

Namespace GridDemo
    Public Class DragDropCustomDataViewModel
        Public Sub New()
            Dim provider = New SchedulerTaskProvider()
            Inbox = New ObservableCollection(Of SchedulerTask)(provider.Inbox)
            Appointments = New ObservableCollection(Of Appointment)(provider.Appointments)
            Employees = New ObservableCollection(Of Employee)(provider.Employees)
            AppointmentTypes = New ObservableCollection(Of AppointmentType)(provider.AppointmentTypes)
        End Sub
        Private privateInbox As ObservableCollection(Of SchedulerTask)
        Public Property Inbox() As ObservableCollection(Of SchedulerTask)
            Get
                Return privateInbox
            End Get
            Private Set(ByVal value As ObservableCollection(Of SchedulerTask))
                privateInbox = value
            End Set
        End Property
        Private privateAppointments As ObservableCollection(Of Appointment)
        Public Property Appointments() As ObservableCollection(Of Appointment)
            Get
                Return privateAppointments
            End Get
            Private Set(ByVal value As ObservableCollection(Of Appointment))
                privateAppointments = value
            End Set
        End Property
        Private privateEmployees As ObservableCollection(Of Employee)
        Public Property Employees() As ObservableCollection(Of Employee)
            Get
                Return privateEmployees
            End Get
            Private Set(ByVal value As ObservableCollection(Of Employee))
                privateEmployees = value
            End Set
        End Property
        Private privateAppointmentTypes As ObservableCollection(Of AppointmentType)
        Public Property AppointmentTypes() As ObservableCollection(Of AppointmentType)
            Get
                Return privateAppointmentTypes
            End Get
            Private Set(ByVal value As ObservableCollection(Of AppointmentType))
                privateAppointmentTypes = value
            End Set
        End Property
    End Class
End Namespace
