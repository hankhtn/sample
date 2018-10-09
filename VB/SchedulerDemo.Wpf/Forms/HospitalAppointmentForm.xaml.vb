Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors
Imports DevExpress.XtraScheduler.Localization
Imports System
Imports DevExpress.XtraScheduler.UI
Imports System.Windows


Namespace SchedulerDemo.Forms
    Partial Public Class HospitalAppointmentForm
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnEdtEndTimeValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            If e.Value Is Nothing Then
                Return
            End If
            e.IsValid = IsValidInterval(edtStartDate.DateTime, Convert.ToDateTime(edtStartTime.EditValue).TimeOfDay, edtEndDate.DateTime, CDate(e.Value).TimeOfDay)
            e.ErrorContent = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate)
        End Sub
        Private Sub OnEdtEndDateValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            If e.Value Is Nothing Then
                Return
            End If
            e.IsValid = IsValidInterval(edtStartDate.DateTime, Convert.ToDateTime(edtStartTime.EditValue).TimeOfDay, CDate(e.Value), Convert.ToDateTime(edtEndTime.EditValue).TimeOfDay)
            e.ErrorContent = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate)
        End Sub
        Protected Friend Overridable Function IsValidInterval(ByVal startDate As Date, ByVal startTime As TimeSpan, ByVal endDate As Date, ByVal endTime As TimeSpan) As Boolean
            Return AppointmentFormControllerBase.ValidateInterval(startDate, startTime, endDate, endTime)
        End Function

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AddHandler LayoutUpdated, AddressOf OnLayoutUpdated
            subjectEdit.Focus()
        End Sub
        Private Sub OnLayoutUpdated(ByVal sender As Object, ByVal e As EventArgs)
            RemoveHandler LayoutUpdated, AddressOf OnLayoutUpdated
            subjectEdit.Focus()
        End Sub
    End Class
End Namespace
