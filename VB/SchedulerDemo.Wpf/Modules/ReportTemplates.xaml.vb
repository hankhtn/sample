Imports System
Imports System.Windows
Imports System.Collections.Generic
Imports System.IO
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler.Reporting
Imports DevExpress.XtraReports.UI
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.Scheduler.Reporting
Imports DevExpress.Xpf.Scheduler.Reporting.UI

Namespace SchedulerDemo



    Partial Public Class ReportTemplates
        Inherits SchedulerDemoModule

        Public Sub New()
            ViewModel = New SchedulerPrintingSettingsFormViewModel()

            InitializeComponent()
            InitializeScheduler()
            scheduler.Storage.ResourceStorage.Mappings.Image = ""

            InitializeSettingsFormViewModel()

            Me.DataContext = Me
            SubscribePrintAdapterEvents()

        End Sub
        Private privateViewModel As SchedulerPrintingSettingsFormViewModel
        Public Property ViewModel() As SchedulerPrintingSettingsFormViewModel
            Get
                Return privateViewModel
            End Get
            Private Set(ByVal value As SchedulerPrintingSettingsFormViewModel)
                privateViewModel = value
            End Set
        End Property

        Private Sub InitializeSettingsFormViewModel()
            ViewModel.ReportInstance = New XtraSchedulerReport()
            ViewModel.SchedulerPrintAdapter = adapter
            ViewModel.AvailableResources = GetAvailableResources()
            ViewModel.TimeInterval = New TimeInterval(scheduler.Start, scheduler.Start.AddDays(7))
            ViewModel.ReportTemplateInfoSource = SchedulerDataHelper.GetReportTemplateCollection()
            ViewModel.ActiveReportTemplateIndex = 0
        End Sub

        Private Function GetAvailableResources() As ResourceBaseCollection
            Return scheduler.Storage.ResourceStorage.Items
        End Function

        Private Function GetOnScreenResources() As ResourceBaseCollection
            Return scheduler.ActiveView.GetResources()
        End Function

        Private Sub btnPreview_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.OnScreenResources = GetOnScreenResources()
            Dim printingSettingsForm As SchedulerPrintingSettingsForm = CreatePrintingSettingsForm()
            printingSettingsForm.ShowDialog()
        End Sub

        Private Function CreatePrintingSettingsForm() As SchedulerPrintingSettingsForm
            Dim form As New SchedulerPrintingSettingsForm(ViewModel)
            form.FlowDirection = scheduler.FlowDirection
            AddHandler form.PreviewButtonClick, AddressOf printingSettingsForm_PreviewButtonClick
            AddHandler form.Closed, AddressOf printingSettingsForm_Closed
            Return form
        End Function

        Private Sub printingSettingsForm_Closed(ByVal sender As Object, ByVal e As EventArgs)
            Dim form As SchedulerPrintingSettingsForm = TryCast(sender, SchedulerPrintingSettingsForm)
            RemoveHandler form.PreviewButtonClick, AddressOf printingSettingsForm_PreviewButtonClick
            RemoveHandler form.Closed, AddressOf printingSettingsForm_Closed
        End Sub

        Private Sub printingSettingsForm_PreviewButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            Dim form As SchedulerPrintingSettingsForm = TryCast(sender, SchedulerPrintingSettingsForm)
            Dim settings As ISchedulerPrintingSettings = DirectCast(form.PrintingSettings, ISchedulerPrintingSettings)
            Dim reportTemplatePath As String = settings.GetReportTemplatePath()
            If reportTemplatePath.ToLower().Contains("trifold") Then
                settings.SchedulerPrintAdapter.EnableSmartSync = True
            End If
        End Sub

        Private Sub PrintAdapter_ValidateResources(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.Reporting.ResourcesValidationEventArgs)
            e.Resources.Clear()
            e.Resources.AddRange(ViewModel.PrintResources)
        End Sub

        Private Sub SubscribePrintAdapterEvents()
            AddHandler adapter.ValidateResources, AddressOf PrintAdapter_ValidateResources
        End Sub

        Private Sub UnsubscribePrintAdapterEvents()
            RemoveHandler adapter.ValidateResources, AddressOf PrintAdapter_ValidateResources
        End Sub

    End Class
End Namespace
