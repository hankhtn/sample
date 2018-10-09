Imports System
Imports System.Windows
Imports DevExpress.Xpf.Scheduler

Namespace SchedulerDemo
    Partial Public Class DateHeaderStyleModule
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()

            ApplyDateHeaderStyle()
        End Sub

        Private Sub ApplyDateHeaderStyle()
            Dim style As Style = CType(Me.FindResource("DateHeaderStyle"), Style)
            scheduler.DayView.DateHeaderStyle = style
            scheduler.WorkWeekView.DateHeaderStyle = style
            scheduler.TimelineView.DateHeaderStyle = style
        End Sub
        Private Sub ClearDateHeaderTemplate()
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.DateHeaderStyleProperty)
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WorkWeekView.DateHeaderStyleProperty)
            scheduler.TimelineView.ClearValue(DevExpress.Xpf.Scheduler.TimelineView.DateHeaderStyleProperty)
        End Sub
        Private Sub cheHeaderTemplate_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ApplyDateHeaderStyle()
        End Sub
        Private Sub cheHeaderTemplate_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ClearDateHeaderTemplate()
        End Sub
    End Class
End Namespace
