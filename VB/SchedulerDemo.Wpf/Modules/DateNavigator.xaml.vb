Imports System
Imports System.Windows
Imports DevExpress.Xpf.Editors
Imports DevExpress.XtraScheduler
Imports System.Globalization

Namespace SchedulerDemo



    Partial Public Class DateNavigator
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()

            cbWeekNumberRule.ItemsSource = CreateWeekNumberRuleList()

        End Sub

        Private Function CreateWeekNumberRuleList() As System.Collections.IEnumerable
            Dim list As New ObjectList()
            list.Add(CalendarWeekRule.FirstDay)
            list.Add(CalendarWeekRule.FirstFourDayWeek)
            list.Add(CalendarWeekRule.FirstFullWeek)
            Return list
        End Function
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
