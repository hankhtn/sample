Imports System

Namespace SchedulerDemo



    Partial Public Class BoundMode
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeSchedulerProperties(scheduler)
            scheduler.Start = New Date(2014, 05, 13)
        End Sub
    End Class
End Namespace
