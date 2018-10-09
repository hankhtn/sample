Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.DemoData
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Scheduling

Namespace GridDemo
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DragDropCustomDataViewModel.(cs)")>
    Partial Public Class DragDropCustomData
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnStartRecordDrag(ByVal sender As Object, ByVal e As StartRecordDragEventArgs)
            e.Data.SetData(GetType(IEnumerable(Of AppointmentItem)), e.Records.Cast(Of SchedulerTask)().Select(Function(x) CreateAppointment(x)))
            e.Handled = True
        End Sub
        Private Sub OnDragRecordOver(ByVal sender As Object, ByVal e As DragRecordOverEventArgs)
            If e.IsFromOutside AndAlso e.Data.GetDataPresent(GetType(IEnumerable(Of AppointmentItem))) Then
                e.Effects = DragDropEffects.Move
                e.Handled = True
            End If
        End Sub
        Private Sub OnDropRecord(ByVal sender As Object, ByVal e As DropRecordEventArgs)
            If e.Data.GetDataPresent(GetType(IEnumerable(Of AppointmentItem))) Then
                Dim appointments = DirectCast(e.Data.GetData(GetType(IEnumerable(Of AppointmentItem))), IEnumerable(Of AppointmentItem))
                Dim dataObject = New DataObject()
                dataObject.SetData(New RecordDragDropData(appointments.Select(Function(x) CreateTask(x)).ToArray()))
                e.Data = dataObject
                e.Effects = DragDropEffects.Move
            End If
        End Sub

        Private Sub OnAppointmentResize(ByVal sender As Object, ByVal e As AppointmentItemResizeEventArgs)
            e.Allow = e.State <> ResizeState.Continue OrElse (e.ViewModel.End - e.ViewModel.Start).TotalHours >= 1
        End Sub

        Private Function CreateAppointment(ByVal task As SchedulerTask) As AppointmentItem
            Return New AppointmentItem With {.Subject = task.Subject, .LabelId = task.Priority, .Description = task.Description, .Start = New Date(), .End = New Date().Add(task.Duration), .AllDay = task.AllDay}
        End Function
        Private Function CreateTask(ByVal appointmentItem As AppointmentItem) As SchedulerTask
            Return New SchedulerTask With {.Subject = appointmentItem.Subject, .Priority = If(TypeOf appointmentItem.LabelId Is IssuePriority, CType(appointmentItem.LabelId, IssuePriority), IssuePriority.Low), .Description = appointmentItem.Description, .Duration = appointmentItem.End - appointmentItem.Start, .AllDay = appointmentItem.AllDay}
        End Function
    End Class
End Namespace
