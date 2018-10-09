Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler

Namespace SchedulerDemo
    Public Class SchedulerDemoModule
        Inherits DemoModule

        Private Shared ReadOnly SchedulerControlPropertyKey As DependencyPropertyKey
        Public Shared ReadOnly SchedulerControlProperty As DependencyProperty

        Shared Sub New()
            SchedulerControlPropertyKey = DependencyProperty.RegisterReadOnly("SchedulerControl", GetType(SchedulerControl), GetType(SchedulerDemoModule), New PropertyMetadata(Nothing))
            SchedulerControlProperty = SchedulerControlPropertyKey.DependencyProperty
        End Sub


        Private ReadOnly views_Renamed As New List(Of SchedulerViewBase)()

        Public Property SchedulerControl() As SchedulerControl
            Get
                Return CType(GetValue(SchedulerControlProperty), SchedulerControl)
            End Get
            Private Set(ByVal value As SchedulerControl)
                SetValue(SchedulerControlPropertyKey, value)
            End Set
        End Property

        Protected ReadOnly Property Views() As List(Of SchedulerViewBase)
            Get
                Return views_Renamed
            End Get
        End Property

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            If SchedulerControl Is Nothing Then
                SchedulerControl = FindScheduler()
            End If
            If OptionsDataContext Is Nothing Then
                OptionsDataContext = SchedulerControl
            End If
        End Sub

        Private Sub PopulateSchedulerViewList(ByVal scheduler As SchedulerControl)
            views_Renamed.Add(scheduler.DayView)
            views_Renamed.Add(scheduler.WorkWeekView)
            views_Renamed.Add(scheduler.WeekView)
            views_Renamed.Add(scheduler.MonthView)
            views_Renamed.Add(scheduler.TimelineView)
        End Sub

        Private Function FindScheduler() As SchedulerControl
            Return If(TryCast(Content, SchedulerControl), LayoutTreeHelper.GetVisualChildren(CType(Content, DependencyObject)).OfType(Of SchedulerControl)().FirstOrDefault())
        End Function

        Protected Sub InitializeScheduler()
            Dim scheduler As SchedulerControl = FindScheduler()
            If scheduler Is Nothing Then
                Return
            End If
            SchedulerDataHelper.DataBind(scheduler)
            InitializeSchedulerProperties(scheduler)
        End Sub
        Protected Sub LoadDefaultAppoinmentStatuses(ByVal scheduler As SchedulerControl)
            scheduler.Storage.InnerStorage.Appointments.Statuses.LoadDefaults()
        End Sub
        Protected Sub InitializeSchedulerProperties(ByVal scheduler As SchedulerControl)
            PopulateSchedulerViewList(scheduler)
            scheduler.ShowBorder = False
            scheduler.Start = New Date(2016, 7, 13)
            AddHandler scheduler.CustomizeMessageBoxCaption, AddressOf Scheduler_CustomizeMessageBoxCaption
        End Sub

        Protected Overrides Sub Hide()
            Dim scheduler As SchedulerControl = FindScheduler()
            If scheduler IsNot Nothing Then
                SchedulerDataHelper.DataUnbind(scheduler)
                RemoveHandler scheduler.CustomizeMessageBoxCaption, AddressOf Scheduler_CustomizeMessageBoxCaption
            End If

            MyBase.Hide()
        End Sub

        Private Sub Scheduler_CustomizeMessageBoxCaption(ByVal sender As Object, ByVal e As CustomizeMessageBoxCaptionEventArgs)
            e.Caption = "DX Scheduler for WPF"
        End Sub

















    End Class
End Namespace
