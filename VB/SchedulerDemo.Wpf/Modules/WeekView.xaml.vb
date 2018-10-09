Imports System
Imports System.Collections.Generic
Imports SchedulerDemo
Imports DevExpress.XtraScheduler

Namespace SchedulerDemo
    Partial Public Class WeekView
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()
            scheduler.WeekView.AppointmentDisplayOptions.StartTimeVisibility = AppointmentTimeVisibility.Always
            scheduler.WeekView.AppointmentDisplayOptions.EndTimeVisibility = AppointmentTimeVisibility.Always
        End Sub
    End Class

    Public Class FirstDayOfWeekList
        Inherits List(Of DevExpress.XtraScheduler.FirstDayOfWeek)

    End Class

    Public Class AppointmentTimeDisplayTypeList
        Inherits List(Of AppointmentTimeDisplayType)

    End Class

    Public Class AppointmentTimeVisibilityList
        Inherits List(Of AppointmentTimeVisibility)

    End Class
End Namespace
