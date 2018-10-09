Imports System
Imports SchedulerDemo
Imports DevExpress.Xpf.Scheduler

Namespace SchedulerDemo



    Partial Public Class SchedulerBars
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()
            SetResourcePerPage()
        End Sub
        Private Sub SetResourcePerPage()
            For Each view As SchedulerViewBase In Views
                view.ResourcesPerPage = 3
            Next view
        End Sub
    End Class
End Namespace
