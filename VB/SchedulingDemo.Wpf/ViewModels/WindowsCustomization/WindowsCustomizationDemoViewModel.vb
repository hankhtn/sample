Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Scheduling
Imports System.Collections.ObjectModel

Namespace SchedulingDemo.ViewModels
    Public Class WindowsCustomizationDemoViewModel
        Private ReadOnly data As New GymData(14)
        Protected Sub New()
        End Sub

        Public Overridable ReadOnly Property GymEvents() As ObservableCollection(Of GymEvent)
            Get
                Return Me.data.GymEvents
            End Get
        End Property
        Public Overridable ReadOnly Property Trainers() As ObservableCollection(Of Trainer)
            Get
                Return Me.data.Trainers
            End Get
        End Property
        Public Overridable ReadOnly Property Trainings() As ObservableCollection(Of Training)
            Get
                Return Me.data.Trainings
            End Get
        End Property

        Public Sub InitNewAppointment(ByVal apt As AppointmentItem)
            apt.ResourceId = apt.LabelId
        End Sub
    End Class
End Namespace
