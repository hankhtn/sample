Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Scheduler

Namespace SchedulerDemo
    Partial Public Class CellStyles
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()

            ApplyCellStyles()
            ApplySelectionTemplate()
        End Sub

        Private Sub ClearCellStyles()
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.CellStyleProperty)
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.AllDayAreaCellStyleProperty)
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WorkWeekView.CellStyleProperty)
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WorkWeekView.AllDayAreaCellStyleProperty)
            scheduler.WeekView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.VerticalWeekCellStyleProperty)
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.VerticalWeekCellStyleProperty)
            scheduler.MonthView.ClearValue(DevExpress.Xpf.Scheduler.MonthView.HorizontalWeekCellStyleProperty)
            scheduler.TimelineView.ClearValue(DevExpress.Xpf.Scheduler.TimelineView.CellStyleProperty)
        End Sub
        Private Sub ClearSelectionTemplate()
            For Each view As SchedulerViewBase In Views
                view.ClearValue(SchedulerViewBase.SelectionTemplateProperty)
            Next view

            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.SelectionTemplateProperty)
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WorkWeekView.SelectionTemplateProperty)
            scheduler.WeekView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.SelectionTemplateProperty)
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.SelectionTemplateProperty)
            scheduler.MonthView.ClearValue(DevExpress.Xpf.Scheduler.MonthView.SelectionTemplateProperty)
            scheduler.TimelineView.ClearValue(DevExpress.Xpf.Scheduler.TimelineView.SelectionTemplateProperty)
        End Sub
        Private Sub ApplyCellStyles()
            Dim style As Style = CType(Me.FindResource("DayViewCellStyle"), Style)
            scheduler.DayView.CellStyle = style
            scheduler.WorkWeekView.CellStyle = style
            scheduler.FullWeekView.CellStyle = style

            Dim allDayAreaStyle As Style = CType(Me.FindResource("AllDayAreaCellStyle"), Style)
            scheduler.DayView.AllDayAreaCellStyle = allDayAreaStyle
            scheduler.WorkWeekView.AllDayAreaCellStyle = allDayAreaStyle

            scheduler.WeekView.VerticalWeekCellStyle = CType(Me.FindResource("VerticalWeekCellStyle"), Style)
            scheduler.MonthView.HorizontalWeekCellStyle = CType(Me.FindResource("HorizontalWeekCellStyle"), Style)

            scheduler.TimelineView.CellStyle = CType(Me.FindResource("TimelineViewCellStyle"), Style)

        End Sub
        Private Sub ApplySelectionTemplate()
            Dim template As ControlTemplate = CType(Me.FindResource("SelectionTemplate"), ControlTemplate)
            For Each view As SchedulerViewBase In Views
                view.SelectionTemplate = template
            Next view
        End Sub
        Private Sub chkCellStyles_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ApplyCellStyles()
        End Sub
        Private Sub chkCellStyles_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ClearCellStyles()
        End Sub
        Private Sub chkSelectionStyle_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ApplySelectionTemplate()
        End Sub
        Private Sub chkSelectionStyle_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ClearSelectionTemplate()
        End Sub
    End Class
End Namespace
