Imports System
Imports System.Windows
Imports DevExpress.Xpf.Scheduler

Namespace SchedulerDemo
    Partial Public Class VisualElementStyles
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()

            ApplyMoreButtonsStyle()
            ApplyNavigationButtonsStyle()
        End Sub

        Private Sub ApplyNavigationButtonsStyle()
            Dim prevNavButtonStyle As Style = CType(Me.FindResource("PrevNavigationButton"), Style)
            Dim nextNavButtonStyle As Style = CType(Me.FindResource("NextNavigationButton"), Style)
            For Each view As SchedulerViewBase In Views
                view.NavigationButtonPrevStyle = prevNavButtonStyle
                view.NavigationButtonNextStyle = nextNavButtonStyle
            Next view
        End Sub
        Private Sub ApplyMoreButtonsStyle()
            Dim downStyle As Style = CType(Me.FindResource("MoreButtonDownStyle"), Style)
            scheduler.DayView.MoreButtonDownStyle = downStyle
            scheduler.WorkWeekView.MoreButtonDownStyle = downStyle
            Dim upStyle As Style = CType(Me.FindResource("MoreButtonUpStyle"), Style)
            scheduler.DayView.MoreButtonUpStyle = upStyle
            scheduler.WorkWeekView.MoreButtonUpStyle = upStyle

            scheduler.WeekView.MoreButtonStyle = downStyle
            scheduler.MonthView.MoreButtonStyle = downStyle
            scheduler.TimelineView.MoreButtonStyle = downStyle
        End Sub
        Private Sub ClearNavigationButtonsStyle()
            For Each view As SchedulerViewBase In Views
                view.ClearValue(SchedulerViewBase.NavigationButtonNextStyleProperty)
                view.ClearValue(SchedulerViewBase.NavigationButtonPrevStyleProperty)
            Next view
        End Sub
        Private Sub ClearMoreButtonsStyle()
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.MoreButtonDownStyleProperty)
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.MoreButtonUpStyleProperty)
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.DayView.MoreButtonDownStyleProperty)
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.DayView.MoreButtonUpStyleProperty)

            scheduler.WeekView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.MoreButtonStyleProperty)
            scheduler.MonthView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.MoreButtonStyleProperty)
            scheduler.TimelineView.ClearValue(DevExpress.Xpf.Scheduler.TimelineView.MoreButtonStyleProperty)
        End Sub
        Private Sub cheMoreButtons_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ApplyMoreButtonsStyle()
        End Sub
        Private Sub cheMoreButtons_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ClearMoreButtonsStyle()
        End Sub
        Private Sub cheNavigationButtons_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ApplyNavigationButtonsStyle()
        End Sub
        Private Sub cheNavigationButtons_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ClearNavigationButtonsStyle()
        End Sub
    End Class
End Namespace
