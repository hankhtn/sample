Imports System
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Xpf.Grid
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Core

Namespace SchedulerDemo
    Partial Public Class DragDropData
        Inherits SchedulerDemoModule

        Private startPoint As Point
        Private startDrag As Boolean = False

        Public Sub New()
            InitializeComponent()
            InitializeSchedulerProperties(scheduler)
            DemoUtils.FillResources(scheduler.Storage, 5)
            grdTasks.ItemsSource = DemoUtils.GenerateScheduleTasks()

            AddHandler scheduler.AppointmentDrop, AddressOf scheduler_AppointmentDrop
            SubscribeGridEvents()
            scheduler.ShowBorder = True
        End Sub

        Private Sub SubscribeGridEvents()
            AddHandler grdTasks.View.PreviewMouseDown, AddressOf View_PreviewMouseDown
            AddHandler grdTasks.View.PreviewMouseMove, AddressOf View_PreviewMouseMove
        End Sub

        Private Sub View_PreviewMouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If e.LeftButton <> MouseButtonState.Pressed Then
                Return
            End If
            Me.startPoint = e.GetPosition(Nothing)
            Me.startDrag = IsGridRowAvailable(e)
        End Sub
        Private Sub View_PreviewMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim position As Point = e.GetPosition(Nothing)
            If Me.startDrag AndAlso e.LeftButton = MouseButtonState.Pressed AndAlso IsGridRowAvailable(e) AndAlso (Math.Abs(position.X - Me.startPoint.X) > SystemParameters.MinimumHorizontalDragDistance OrElse Math.Abs(position.Y - Me.startPoint.Y) > SystemParameters.MinimumVerticalDragDistance) Then
                Dim view As DataViewBase = grdTasks.View
                Dim rowHandler As Integer = view.GetRowHandleByMouseEventArgs(e)
                Dim schedulerData As Object = GetSchedulerData(rowHandler)
                Me.startDrag = False
                Dim de As DragDropEffects = DragDrop.DoDragDrop(grdTasks, schedulerData, DragDropEffects.Move)
            End If
            Me.startPoint = e.GetPosition(Nothing)
        End Sub
        Private Function GetSchedulerData(ByVal rowHandler As Integer) As Object
            Dim appointments As New AppointmentBaseCollection()
            Dim apt As Appointment = scheduler.Storage.CreateAppointment(AppointmentType.Normal)
            apt.Subject = DirectCast(ObtaiDataFromRow(rowHandler, "Subject"), String)
            apt.LabelKey = DirectCast(ObtaiDataFromRow(rowHandler, "Severity"), Integer)
            apt.StatusKey = DirectCast(ObtaiDataFromRow(rowHandler, "Priority"), Integer)
            apt.Duration = TimeSpan.FromHours(DirectCast(ObtaiDataFromRow(rowHandler, "Duration"), Integer))
            apt.Description = DirectCast(ObtaiDataFromRow(rowHandler, "Description"), String)
            appointments.Add(apt)
            Return New SchedulerDragData(appointments, 0)
        End Function
        Private Function ObtaiDataFromRow(ByVal rowHandler As Integer, ByVal column As String) As Object
            Return grdTasks.GetCellValue(rowHandler, grdTasks.Columns(column))
        End Function
        Private Function IsGridRowAvailable(ByVal e As MouseEventArgs) As Boolean
            Dim rowHandler As Integer = grdTasks.View.GetRowHandleByMouseEventArgs(e)
            Return grdTasks.GetRow(rowHandler) IsNot Nothing
        End Function

        Private Sub scheduler_AppointmentDrop(ByVal sender As Object, ByVal e As AppointmentDragEventArgs)
            Dim createEventMsg As String = "Creating an event at {0} on {1}."
            Dim moveEventMsg As String = "Moving the event from {0} on {1} to {2} on {3}."

            Dim srcStart As Date = e.SourceAppointment.Start
            Dim newStart As Date = e.EditedAppointment.Start

            Dim msg As String = If(srcStart = Date.MinValue, String.Format(createEventMsg, newStart.ToShortTimeString(), newStart.ToShortDateString()), String.Format(moveEventMsg, srcStart.ToShortTimeString(), srcStart.ToShortDateString(), newStart.ToShortTimeString(), newStart.ToShortDateString()))

            If DXMessageBox.Show(msg & ControlChars.CrLf & "Proceed?", "Demo", MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.No Then
                e.Allow = False
            End If

            scheduler.Focus()
        End Sub
    End Class
End Namespace
