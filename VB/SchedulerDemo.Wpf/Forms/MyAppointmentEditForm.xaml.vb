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
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler.UI
Imports DevExpress.Xpf.Core
Imports DevExpress.XtraScheduler.Localization
Imports System.Globalization
Imports DevExpress.Xpf.Editors
Imports DevExpress.XtraScheduler.UI
Imports DevExpress.XtraScheduler.Native


Namespace SchedulerDemo.Forms



    Partial Public Class MyAppointmentEditForm
        Inherits UserControl


        Private control_Renamed As SchedulerControl

        Private appointment_Renamed As Appointment

        Private controller_Renamed As MyAppointmentFormController

        Private recurrenceVisualController_Renamed As RecurrenceVisualController

        Public Sub New(ByVal control As SchedulerControl, ByVal appointment As Appointment)
            Me.control_Renamed = control
            Me.appointment_Renamed = appointment
            Me.controller_Renamed = New MyAppointmentFormController(Me.Control, Me.Appointment)
            Me.recurrenceVisualController_Renamed = New RecurrenceVisualController(Controller)
            RecurrenceVisualController.EnableYearlyRecurrence = False
            RecurrenceVisualController.EnableWeeklyRecurrence = False
            RecurrenceVisualController.EnableNoneRecurrence = False
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Public ReadOnly Property Control() As SchedulerControl
            Get
                Return control_Renamed
            End Get
        End Property
        Public ReadOnly Property Appointment() As Appointment
            Get
                Return appointment_Renamed
            End Get
        End Property
        Public ReadOnly Property Controller() As MyAppointmentFormController
            Get
                Return controller_Renamed
            End Get
        End Property
        Protected Friend ReadOnly Property IsNewAppointment() As Boolean
            Get
                Return If(controller_Renamed IsNot Nothing, controller_Renamed.IsNewAppointment, True)
            End Get
        End Property
        Public ReadOnly Property TimeEditMask() As String
            Get
                Return CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
            End Get
        End Property
        Public ReadOnly Property RecurrenceVisualController() As RecurrenceVisualController
            Get
                Return recurrenceVisualController_Renamed
            End Get
        End Property
        Public ReadOnly Property ShouldShowRecurrence() As Boolean
            Get
                Return (Not Appointment.IsOccurrence) AndAlso Controller.ShouldShowRecurrenceButton
            End Get
        End Property
        Public ReadOnly Property TimeZoneHelper() As TimeZoneHelper
            Get
                Return Controller.TimeZoneHelper
            End Get
        End Property

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim currentSize As Size = SchedulerFormBehavior.GetSize(Me)
            SchedulerFormBehavior.SetSize(Me, New Size(currentSize.Width + recurrenceGrid.Width, Double.NaN))
        End Sub
        Private Sub OnOKButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not controller_Renamed.IsConflictResolved() Then
                DXMessageBox.Show(SchedulerLocalizer.GetString(SchedulerStringId.Msg_Conflict), "DXScheduler Demo", System.Windows.MessageBoxButton.OK, MessageBoxImage.Exclamation)
                Return
            End If

            If ShouldShowRecurrence Then
                RecurrenceVisualController.ApplyRecurrence()
            End If

            Controller.ApplyChanges()

            SchedulerFormBehavior.Close(Me, True)
        End Sub
        Private Sub OnCancelButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SchedulerFormBehavior.Close(Me, False)
        End Sub
        Private Sub OnDeleteButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If IsNewAppointment Then
                Return
            End If

            Controller.DeleteAppointment()
            SchedulerFormBehavior.Close(Me, False)
        End Sub
        Protected Overrides Sub OnVisualParentChanged(ByVal oldParent As DependencyObject)
            MyBase.OnVisualParentChanged(oldParent)
            If oldParent Is Nothing Then
                UpdateContainerCaption(Controller.Subject)
            End If
        End Sub
        Private Sub UpdateContainerCaption(ByVal subject As String)
            If Controller.IsNewAppointment Then
                SchedulerFormBehavior.SetTitle(Me, "New appointment")
            Else
                SchedulerFormBehavior.SetTitle(Me, "Edit - [" & Appointment.Subject & "]")
            End If
        End Sub
        Private Sub OnEdtEndTimeValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            If e.Value Is Nothing Then
                Return
            End If
            e.IsValid = IsValidInterval(Controller.Start.Date, Controller.Start.TimeOfDay, Controller.End.Date, CDate(e.Value).TimeOfDay)
            e.ErrorContent = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate)
        End Sub
        Private Sub OnEdtEndDateValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            If e.Value Is Nothing Then
                Return
            End If
            e.IsValid = IsValidInterval(Controller.Start.Date, Controller.Start.TimeOfDay, CDate(e.Value), Controller.End.TimeOfDay)
            e.ErrorContent = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate)
        End Sub
        Protected Friend Overridable Function IsValidInterval(ByVal startDate As Date, ByVal startTime As TimeSpan, ByVal endDate As Date, ByVal endTime As TimeSpan) As Boolean
            Return AppointmentFormControllerBase.ValidateInterval(startDate, startTime, endDate, endTime)
        End Function
    End Class

    Public Class MyAppointmentFormController
        Inherits AppointmentFormController

        Public Property CustomName() As String
            Get
                Return CStr(EditedAppointmentCopy.CustomFields("CustomName"))
            End Get
            Set(ByVal value As String)
                EditedAppointmentCopy.CustomFields("CustomName") = value
            End Set
        End Property
        Public Property CustomStatus() As String
            Get
                Return CStr(EditedAppointmentCopy.CustomFields("CustomStatus"))
            End Get
            Set(ByVal value As String)
                EditedAppointmentCopy.CustomFields("CustomStatus") = value
            End Set
        End Property

        Private Property SourceCustomName() As String
            Get
                Return CStr(SourceAppointment.CustomFields("CustomName"))
            End Get
            Set(ByVal value As String)
                SourceAppointment.CustomFields("CustomName") = value
            End Set
        End Property
        Private Property SourceCustomStatus() As String
            Get
                Return CStr(SourceAppointment.CustomFields("CustomStatus"))
            End Get
            Set(ByVal value As String)
                SourceAppointment.CustomFields("CustomStatus") = value
            End Set
        End Property

        Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment)
            MyBase.New(control, apt)
        End Sub

        Public Overrides Function IsAppointmentChanged() As Boolean
            If MyBase.IsAppointmentChanged() Then
                Return True
            End If
            Return SourceCustomName <> CustomName OrElse SourceCustomStatus <> CustomStatus
        End Function

        Protected Overrides Sub ApplyCustomFieldsValues()
            SourceCustomName = CustomName
            SourceCustomStatus = CustomStatus
        End Sub
    End Class
End Namespace
