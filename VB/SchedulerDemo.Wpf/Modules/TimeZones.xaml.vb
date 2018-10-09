Imports System
Imports System.Windows
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler
Imports System.Windows.Markup
Imports System.Windows.Data

Namespace SchedulerDemo



    Partial Public Class TimeZones
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()
        End Sub

        Private Sub edTimeZones_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim tzId As String = edTimeZones.TimeZoneId
            UpdateClientTimeRuler(scheduler.DayView.TimeRulers(2), tzId)
            UpdateClientTimeRuler(scheduler.WorkWeekView.TimeRulers(2), tzId)
        End Sub

        Private Shared Sub UpdateClientTimeRuler(ByVal ruler As SchedulerTimeRuler, ByVal tzId As String)
            ruler.Caption = tzId.ToString()
            ruler.TimeZoneId = tzId
        End Sub
    End Class

    Public Class DemoViewTypeListItemList
        Inherits List(Of DemoViewTypeListItem)

    End Class
End Namespace
