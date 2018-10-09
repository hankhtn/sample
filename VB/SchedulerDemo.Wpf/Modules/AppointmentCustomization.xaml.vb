Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.Xpf.Scheduler.Drawing
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native
Imports System.Windows.Markup
Imports System.IO
Imports DevExpress.Xpf.Core.Native
Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm
Imports System.Windows.Media



Namespace SchedulerDemo
    Partial Public Class AppointmentCustomization
        Inherits SchedulerDemoModule

        Private resourceImages As New Dictionary(Of Resource, BitmapImage)()
        Public Sub New()
            InitializeComponent()
            InitializeScheduler()
        End Sub

        Private Sub scheduler_AppointmentViewInfoCustomizing(ByVal sender As Object, ByVal e As DevExpress.Xpf.Scheduler.AppointmentViewInfoCustomizingEventArgs)
            Dim viewInfo As AppointmentViewInfo = e.ViewInfo
            If chkCustomText.IsChecked.GetValueOrDefault() Then
                UpdateText(viewInfo)
            End If
            Dim data As New CustomAppointmentData()
            data.Filtered = False
            data.ShowImages = chkCustomImage.IsChecked.GetValueOrDefault()
            viewInfo.CustomViewInfo = data

            If Not IsEmptyFilter(cbSearch.Text) Then
                Dim searchText As String = cbSearch.Text.ToUpper()
                If IsAppointmentFiltered(searchText, e.ViewInfo.Appointment) Then
                    data.Filtered = True
                End If
            End If

            If Not data.Filtered Then
                Dim label As IAppointmentLabel = Me.scheduler.Storage.AppointmentStorage.Labels.CreateNewLabel(String.Empty)
                label.SetColor(Colors.DarkGray)
                viewInfo.Label = label
                Dim status As IAppointmentStatus = Me.scheduler.Storage.AppointmentStorage.Statuses.CreateNewStatus(String.Empty, String.Empty)
                status.Type = AppointmentStatusType.Custom
                status.SetBrush(New SolidColorBrush(Colors.LightGray))
                viewInfo.Status = status
            End If

        End Sub
        Private Function IsEmptyFilter(ByVal text As String) As Boolean
            Return String.IsNullOrEmpty(text) OrElse text = "(None)"
        End Function
        Private Shared Function IsAppointmentFiltered(ByVal searchText As String, ByVal apt As Appointment) As Boolean
            Return apt.Subject.ToUpper().Contains(searchText) OrElse apt.Location.ToUpper().Contains(searchText)
        End Function
        Private Sub UpdateText(ByVal viewInfo As AppointmentViewInfo)
            Dim appointment As Appointment = viewInfo.Appointment
            Dim description As String = viewInfo.Description
            If appointment.IsRecurring Then
                description = String.Format("{0}" & ControlChars.CrLf & "Occurs on {1}", description, RecurrenceInfo.GetDescription(appointment, DateTimeHelper.ConvertFirstDayOfWeek(scheduler.OptionsView.FirstDayOfWeek)))
            End If
            viewInfo.Description = description
            viewInfo.Location = If(String.IsNullOrEmpty(viewInfo.Appointment.Location), String.Empty, String.Format("- [{0}]", viewInfo.Appointment.Location))
        End Sub
        Private Sub EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            UpdateSchedulerView()
        End Sub
        Private Sub cbSearch_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            scheduler.ActiveView.LayoutChanged()
        End Sub
        Private Sub UpdateSchedulerView()
            scheduler.ActiveView.LayoutChanged()
        End Sub
    End Class

    Public Class CustomAppointmentData
        Inherits BindableBase

        Private filteredCore As Boolean
        Private showImagesCore As Boolean

        Public Property Filtered() As Boolean
            Get
                Return filteredCore
            End Get
            Set(ByVal value As Boolean)
                SetProperty(filteredCore, value, "Filtered")
            End Set
        End Property
        Public Property ShowImages() As Boolean
            Get
                Return showImagesCore
            End Get
            Set(ByVal value As Boolean)
                SetProperty(showImagesCore, value, "ShowImages")
            End Set
        End Property
    End Class
End Namespace
