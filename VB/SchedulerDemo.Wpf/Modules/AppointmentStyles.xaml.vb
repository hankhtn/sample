Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler.Drawing
Imports System.Collections.Generic
Imports System.IO
Imports DevExpress.Xpf.Core.Native
Imports System.Windows.Media.Imaging


Namespace SchedulerDemo
    Partial Public Class AppointmentStyles
        Inherits SchedulerDemoModule

        Private resourceImages As New Dictionary(Of Resource, BitmapImage)()

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()

            ApplyHorizontalAppointmentStyle()
            ApplyVerticalAppointmentStyle()

            ApplyAppointmentToolTipContentTemplate()
        End Sub
        Private Sub ApplyHorizontalAppointmentStyle()
            Dim styleSelector As StyleSelector = CType(Resources("HorizontalAppointmentStyleSelector"), StyleSelector)
            For Each view As SchedulerViewBase In Views
                view.HorizontalAppointmentStyleSelector = styleSelector
            Next view
        End Sub
        Private Sub ApplyVerticalAppointmentStyle()
            Dim styleSelector As StyleSelector = CType(FindResource("VerticalAppointmentStyleSelector"), StyleSelector)
            scheduler.DayView.VerticalAppointmentStyleSelector = styleSelector
            scheduler.WorkWeekView.VerticalAppointmentStyleSelector = styleSelector
        End Sub
        Private Sub ApplyAppointmentToolTipContentTemplate()
            Dim template As DataTemplate = CType(Me.FindResource("AppointmentTooltipContentTemplate"), DataTemplate)
            For Each view As SchedulerViewBase In Views
                view.AppointmentToolTipContentTemplate = template
            Next view
        End Sub
        Private Sub chHorizontalAppointmentStyle_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ApplyHorizontalAppointmentStyle()
        End Sub
        Private Sub chHorizontalAppointmentStyle_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each view As SchedulerViewBase In Views
                view.ClearValue(SchedulerViewBase.HorizontalAppointmentStyleSelectorProperty)
            Next view
        End Sub

        Private Sub chVerticalAppointmentStyle_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ApplyVerticalAppointmentStyle()
        End Sub
        Private Sub chVerticalAppointmentStyle_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.VerticalAppointmentStyleSelectorProperty)
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WorkWeekView.VerticalAppointmentStyleSelectorProperty)
        End Sub

        Private Sub scheduler_ActiveViewChanged(ByVal sender As Object, ByVal e As EventArgs)
            If Me.chVerticalAppointmentStyle Is Nothing Then
                Return
            End If
            Dim viewType As SchedulerViewType = DirectCast(sender, SchedulerControl).ActiveView.Type
            Me.chVerticalAppointmentStyle.IsEnabled = Object.Equals(viewType, SchedulerViewType.Day) OrElse Object.Equals(viewType, SchedulerViewType.WorkWeek)
        End Sub


        Private Sub scheduler_AppointmentViewInfoCustomizing(ByVal sender As Object, ByVal e As AppointmentViewInfoCustomizingEventArgs)
            If (Not chkAppointmentToolTip.IsChecked.GetValueOrDefault()) = True Then
                Return
            End If

            Dim viewInfo As AppointmentViewInfo = e.ViewInfo
            Dim resource As Resource = scheduler.Storage.ResourceStorage.GetResourceById(viewInfo.Appointment.ResourceId)
            If Object.Equals(resource.Id, EmptyResourceId.Id) OrElse resource.ImageBytes Is Nothing Then
                viewInfo.CustomViewInfo = Nothing
            Else
                If Not Me.resourceImages.ContainsKey(resource) Then
                    Me.resourceImages(resource) = TryCast(resource.GetImage(), BitmapImage)
                End If
                viewInfo.CustomViewInfo = Me.resourceImages(resource)
            End If

        End Sub

        Private Sub chkAppointmentToolTip_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ApplyAppointmentToolTipContentTemplate()
        End Sub

        Private Sub chkAppointmentToolTip_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each view As SchedulerViewBase In Views
                view.ClearValue(SchedulerViewBase.AppointmentToolTipContentTemplateProperty)
            Next view
        End Sub
    End Class









End Namespace
