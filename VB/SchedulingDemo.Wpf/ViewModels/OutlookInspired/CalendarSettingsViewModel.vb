Imports System
Imports System.Collections.Generic
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.XtraScheduler

Namespace SchedulingDemo.ViewModels
    Public Class CalendarSettingsViewModel
        Public Shared DefaultWorkDays As New List(Of Object)() From {DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday}
        Public Shared Function ConvertDayOfWeek(ByVal dayOfWeek As DayOfWeek) As WeekDays
            Dim weekDays As WeekDays
            Select Case dayOfWeek
                Case System.DayOfWeek.Monday
                    weekDays = WeekDays.Monday
                Case System.DayOfWeek.Tuesday
                    weekDays = WeekDays.Tuesday
                Case System.DayOfWeek.Wednesday
                    weekDays = WeekDays.Wednesday
                Case System.DayOfWeek.Thursday
                    weekDays = WeekDays.Thursday
                Case System.DayOfWeek.Friday
                    weekDays = WeekDays.Friday
                Case System.DayOfWeek.Saturday
                    weekDays = WeekDays.Saturday
                Case Else
                    weekDays = WeekDays.Sunday

            End Select
            Return weekDays
        End Function
        Public Shared Function ConvertWorkDays(ByVal days As List(Of Object)) As WeekDays
            Dim result As WeekDays = CType(0, WeekDays)
            For Each dayOfWeek As Object In days
                result = result Or ConvertDayOfWeek(DirectCast(dayOfWeek, DayOfWeek))
            Next dayOfWeek
            Return result
        End Function

        Public Shared Function Create() As CalendarSettingsViewModel
            Return ViewModelSource.Create(Function() New CalendarSettingsViewModel())
        End Function

        Protected Sub New()
            ShowDayHeaders = True
            ShowResourceHeaders = True
            ShowResourceNavigator = True
            SnapToCellsMode = SnapToCellsMode.Always

            TimeIndicatorVisibility = TimeIndicatorVisibility.TodayView
            TimeMarkerVisibility = TimeMarkerVisibility.TodayView

            WorkTimeStart = New TimeSpan(6, 0, 0)
            WorkTimeEnd = New TimeSpan(20, 0, 0)

            YearTimeScaleIsEnabled = False
            QuarterTimeScaleIsEnabled = False
            MonthTimeScaleIsEnabled = False
            WeekTimeScaleIsEnabled = False
            DayTimeScaleIsEnabled = True
            HourTimeScaleIsEnabled = True

            IntervalCount = 10
            FirstDayOfWeek = DayOfWeek.Sunday
            DayCount = 1

            Days = DefaultWorkDays
            ShowAllDayArea = True
            ShowWorkTimeOnly = True
        End Sub
        Public Overridable Property SnapToCellsMode() As SnapToCellsMode
        Public Overridable Property ShowDayHeaders() As Boolean
        Public Overridable Property ShowResourceHeaders() As Boolean
        Public Overridable Property ShowResourceNavigator() As Boolean


        Public Overridable Property ShowAllDayArea() As Boolean
        Public Overridable Property ShowWorkTimeOnly() As Boolean
        Public Overridable Property WorkTimeStart() As TimeSpan
        Public Overridable Property WorkTimeEnd() As TimeSpan
        Public Overridable Property WorkTime() As TimeSpanRange
        Public Overridable Property DayCount() As Integer
        Public Overridable Property TimeMarkerVisibility() As TimeMarkerVisibility
        Public Overridable Property TimeIndicatorVisibility() As TimeIndicatorVisibility


        Public Overridable Property WorkDays() As WeekDays
        Public Overridable Property Days() As List(Of Object)


        Public Overridable Property FirstDayOfWeek() As DayOfWeek


        Public Overridable Property IntervalCount() As Integer
        Public Overridable Property YearTimeScaleIsEnabled() As Boolean
        Public Overridable Property QuarterTimeScaleIsEnabled() As Boolean
        Public Overridable Property MonthTimeScaleIsEnabled() As Boolean
        Public Overridable Property WeekTimeScaleIsEnabled() As Boolean
        Public Overridable Property DayTimeScaleIsEnabled() As Boolean
        Public Overridable Property HourTimeScaleIsEnabled() As Boolean

        Public Sub ApplySettings(ByVal calendarSettings As CalendarSettingsViewModel)
            ShowDayHeaders = calendarSettings.ShowDayHeaders
            ShowResourceHeaders = calendarSettings.ShowResourceHeaders
            ShowResourceNavigator = calendarSettings.ShowResourceNavigator
            SnapToCellsMode = calendarSettings.SnapToCellsMode

            TimeIndicatorVisibility = calendarSettings.TimeIndicatorVisibility
            TimeMarkerVisibility = calendarSettings.TimeMarkerVisibility

            WorkTimeStart = calendarSettings.WorkTimeStart
            WorkTimeEnd = calendarSettings.WorkTimeEnd

            YearTimeScaleIsEnabled = calendarSettings.YearTimeScaleIsEnabled
            QuarterTimeScaleIsEnabled = calendarSettings.QuarterTimeScaleIsEnabled
            MonthTimeScaleIsEnabled = calendarSettings.MonthTimeScaleIsEnabled
            WeekTimeScaleIsEnabled = calendarSettings.WeekTimeScaleIsEnabled
            DayTimeScaleIsEnabled = calendarSettings.DayTimeScaleIsEnabled
            HourTimeScaleIsEnabled = calendarSettings.HourTimeScaleIsEnabled

            IntervalCount = calendarSettings.IntervalCount
            FirstDayOfWeek = calendarSettings.FirstDayOfWeek
            DayCount = calendarSettings.DayCount

            Days = calendarSettings.Days
            ShowAllDayArea = calendarSettings.ShowAllDayArea
            ShowWorkTimeOnly = calendarSettings.ShowWorkTimeOnly
        End Sub

        Protected Sub OnWorkTimeStartChanged()
            Dim [end] As TimeSpan = If(WorkTimeEnd < WorkTimeStart, WorkTimeStart.Add(TimeSpan.FromHours(1)), WorkTimeEnd)
            WorkTime = New TimeSpanRange(WorkTimeStart, [end])
        End Sub
        Protected Sub OnWorkTimeEndChanged()
            Dim start As TimeSpan = If(WorkTimeEnd < WorkTimeStart, WorkTimeEnd.Add(TimeSpan.FromHours(-1)), WorkTimeStart)
            WorkTime = New TimeSpanRange(start, WorkTimeEnd)
        End Sub
        Protected Sub OnDaysChanged()
            If Days Is Nothing Then
                Days = DefaultWorkDays
                Return
            End If
            WorkDays = ConvertWorkDays(Days)
        End Sub
    End Class
End Namespace
