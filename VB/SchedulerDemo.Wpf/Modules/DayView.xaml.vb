Imports System
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler

Namespace SchedulerDemo
    Partial Public Class DayView
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()
        End Sub
    End Class

    Public Class AppointmentSnapToCellsModeList
        Inherits List(Of AppointmentSnapToCellsMode)

    End Class
    Public Class AppointmentStatusDisplayTypeList
        Inherits List(Of AppointmentStatusDisplayType)

    End Class
    Public Class TimeIndicatorVisibilityTypeList
        Inherits List(Of TimeIndicatorVisibility)

    End Class
    Public Class TimeMarkerVisibilityTypeList
        Inherits List(Of TimeMarkerVisibility)

    End Class

End Namespace
