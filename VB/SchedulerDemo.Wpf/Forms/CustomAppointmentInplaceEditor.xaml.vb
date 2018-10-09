Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Scheduler.UI
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler

Namespace SchedulerDemo.Forms



    Partial Public Class CustomAppointmentInplaceEditor
        Inherits AppointmentInplaceEditorBase

        Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment)
            MyBase.New(control, apt)
            InitializeComponent()
        End Sub

        Private Sub OnOkButton(ByVal sender As Object, ByVal e As RoutedEventArgs)
            OnCommitChanges()
        End Sub
        Private Sub OnCancelButton(ByVal sender As Object, ByVal e As RoutedEventArgs)
            OnRollbackChanges()
        End Sub
        Public Overrides Sub Activate()
            Dispatcher.BeginInvoke(New Action(Sub()
                Me.txtSubject.Focus()
                If IsNewAppointment Then
                    Me.txtSubject.SelectionStart = Me.txtSubject.Text.Length
                    Me.txtSubject.SelectionLength = 0
                Else
                    Me.txtSubject.SelectAll()
                End If
            End Sub))
        End Sub
    End Class
End Namespace
