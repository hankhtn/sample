Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Namespace SchedulingDemo.ViewModels
    Public Class MonthViewDemoViewModel
        Protected Sub New()
            Start = Date.Today.AddDays(-14)
            Calendars = New ObservableCollection(Of TeamCalendar)(TeamData.Calendars)
            Appointments = New ObservableCollection(Of TeamAppointment)(TeamData.AllAppointments)
        End Sub
        Public Overridable Property Calendars() As IEnumerable(Of TeamCalendar)
        Public Overridable Property Appointments() As IEnumerable(Of TeamAppointment)
        Public Overridable Property Start() As Date
    End Class
End Namespace
