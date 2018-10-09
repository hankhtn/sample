Imports System
Imports SchedulerDemo.Forms
Imports System.Windows

Namespace SchedulerDemo



    Partial Public Class CustomInplaceEditor
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()
            SubscribeToInplaceEditorShowingEvent()
        End Sub

        Private Sub cheCustomInplaceEditor_Checked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            SubscribeToInplaceEditorShowingEvent()
        End Sub
        Private Sub cheCustomInplaceEditor_Unchecked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            UnsubscribeToInplaceEditorShowingEvent()
        End Sub
        Private Sub SubscribeToInplaceEditorShowingEvent()
            UnsubscribeToInplaceEditorShowingEvent()
            AddHandler Me.scheduler.InplaceEditorShowing, AddressOf OnInplaceEditorShowing
        End Sub
        Private Sub UnsubscribeToInplaceEditorShowingEvent()
            RemoveHandler Me.scheduler.InplaceEditorShowing, AddressOf OnInplaceEditorShowing
        End Sub
        Private Sub OnInplaceEditorShowing(ByVal sender As Object, ByVal e As DevExpress.Xpf.Scheduler.InplaceEditorEventArgs)
            Dim editor As New CustomAppointmentInplaceEditor(Me.scheduler, e.Appointment)
            e.InplaceEditor = editor
            editor.DataContext = editor
            Dim bounds As Rect = Me.scheduler.RenderTransform.TransformBounds(e.Bounds)
            Dim controlSize As Size = Me.scheduler.DesiredSize
            Dim location As Point = e.Bounds.TopRight
            If (controlSize.Width - location.X) > editor.Width Then
                e.Bounds = New Rect(location, New Size(editor.Width, editor.Height))
            ElseIf e.Bounds.X > editor.Width Then
                location = New Point(bounds.X - editor.Width, e.Bounds.Y)
                e.Bounds = New Rect(location, New Size(editor.Width, editor.Height))
            Else
                e.Bounds = New Rect(bounds.TopLeft, New Size(editor.Width, editor.Height))
            End If
        End Sub
    End Class
End Namespace
