Imports System
Imports EditorsDemo.Utils

Namespace EditorsDemo
    Partial Public Class RangeControlAndScheduler
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            Me.scheduler.Start = New Date(2016, 7, 11)
            SchedulerDataHelper.LoadTo(Me.scheduler)
        End Sub
    End Class
End Namespace
