Imports DevExpress.Mvvm
Imports System
Imports System.Collections.ObjectModel
Imports System.Collections.Generic

Namespace SchedulingDemo.ViewModels
    Public Class WeekViewDemoViewModel
        Protected Sub New()
            Start = TeamData.Start
            Calendars = New ObservableCollection(Of TeamCalendar)(TeamData.Calendars)
            Appointments = New ObservableCollection(Of TeamAppointment)(TeamData.AllAppointments)
            WorkTimeStart = TimeSpan.FromHours(9)
            WorkTimeEnd = TimeSpan.FromHours(18)
        End Sub
        Public Overridable Property Calendars() As IEnumerable(Of TeamCalendar)
        Public Overridable Property Appointments() As IEnumerable(Of TeamAppointment)
        Public Overridable Property Start() As Date
        Public Overridable Property WorkTimeStart() As TimeSpan
        Public Overridable Property WorkTimeEnd() As TimeSpan
        Public Overridable Property WorkTime() As TimeSpanRange

        Protected Sub OnWorkTimeStartChanged()
            Dim [end] = If(WorkTimeEnd < WorkTimeStart, WorkTimeStart.Add(TimeSpan.FromHours(1)), WorkTimeEnd)
            WorkTime = New TimeSpanRange(WorkTimeStart, [end])
        End Sub
        Protected Sub OnWorkTimeEndChanged()

            Dim start_Renamed = If(WorkTimeEnd < WorkTimeStart, WorkTimeEnd.Add(TimeSpan.FromHours(-1)), WorkTimeStart)
            WorkTime = New TimeSpanRange(start_Renamed, WorkTimeEnd)
        End Sub
    End Class
End Namespace
