Imports System
Imports System.Windows
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Editors

Namespace SchedulerDemo



    Partial Public Class WorkWeekView
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()
        End Sub

        Private Sub WeekDaysCheckEditChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim edit As CheckEdit = DirectCast(sender, CheckEdit)
            Dim day As WeekDays = WeekDaysToBooleanConverter.GetDay(CStr(edit.Content))
            If CInt((day)) = 0 Then
                Return
            End If
            Dim weekDays As WeekDays = SchedulerDemoUtils.AddWeekDay(scheduler.WorkDays.GetWeekDays(), day)
            SchedulerDemoUtils.UpdateSchedulerWorkDays(scheduler, weekDays)
        End Sub
        Private Sub WeekDaysCheckEditUnchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim edit As CheckEdit = DirectCast(sender, CheckEdit)
            Dim day As WeekDays = WeekDaysToBooleanConverter.GetDay(CStr(edit.Content))
            If CInt((day)) = 0 Then
                Return
            End If
            Dim weekDays As WeekDays = SchedulerDemoUtils.RemoveWeekDay(scheduler.WorkDays.GetWeekDays(), day)
            SchedulerDemoUtils.UpdateSchedulerWorkDays(scheduler, weekDays)
        End Sub
    End Class
End Namespace
