Imports System
Imports SchedulerDemo

Namespace SchedulerDemo
    Partial Public Class SchedulerRibbonBars
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()
            scheduler.Start += TimeSpan.FromDays(1)
        End Sub
    End Class
End Namespace
