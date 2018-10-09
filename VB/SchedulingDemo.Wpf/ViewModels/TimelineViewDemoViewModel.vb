Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Namespace SchedulingDemo.ViewModels
    Public Class TimelineViewDemoViewModel
        Private ReadOnly data As New ReceptionDeskData()
        Protected Sub New()
            Start = ReceptionDeskData.BaseDate
        End Sub
        Public Overridable Property Start() As Date
        Public Overridable ReadOnly Property Doctors() As ObservableCollection(Of Doctor)
            Get
                Return Me.data.Doctors
            End Get
        End Property
        Public Overridable ReadOnly Property MedicalAppointments() As ObservableCollection(Of MedicalAppointment)
            Get
                Return Me.data.MedicalAppointments
            End Get
        End Property
        Public Overridable ReadOnly Property AppointmentTypes() As ObservableCollection(Of MedicalAppointmentType)
            Get
                Return Me.data.Labels
            End Get
        End Property
        Public Overridable ReadOnly Property PaymentStates() As ObservableCollection(Of PaymentState)
            Get
                Return Me.data.Statuses
            End Get
        End Property
    End Class
End Namespace
