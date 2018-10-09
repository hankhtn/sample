Imports System
Imports System.Windows
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraScheduler.Reporting
Imports DevExpress.Xpf.Scheduler.Reporting
Imports DevExpress.Xpf.Scheduler.Reporting.UI

Namespace SchedulerDemo
    Partial Public Class DocumentPreview
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            SchedulerDataHelper.DataBind(storage)
            SubscribePrintAdapterEvents()
            storage.ResourceStorage.Mappings.Image = ""
            Me.DataContext = Me
            UpdateInterval()
            UpdatePreview()
        End Sub
        #Region "ReportTemplateInfoSource"
        Public Property ReportTemplateInfoSource() As List(Of ISchedulerReportTemplateInfo)
            Get
                Return CType(GetValue(ReportTemplateInfoSourceProperty), List(Of ISchedulerReportTemplateInfo))
            End Get
            Set(ByVal value As List(Of ISchedulerReportTemplateInfo))
                SetValue(ReportTemplateInfoSourceProperty, value)
            End Set
        End Property
        Public Shared ReadOnly ReportTemplateInfoSourceProperty As DependencyProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty(Of DocumentPreview, List(Of ISchedulerReportTemplateInfo))("ReportTemplateInfoSource", SchedulerDataHelper.GetReportTemplateCollection())
        #End Region
        #Region "ActiveReportTemplateIndex"
        Public Property ActiveReportTemplateIndex() As Integer
            Get
                Return CInt((GetValue(ActiveReportTemplateIndexProperty)))
            End Get
            Set(ByVal value As Integer)
                SetValue(ActiveReportTemplateIndexProperty, value)
            End Set
        End Property
        Public Shared ReadOnly ActiveReportTemplateIndexProperty As DependencyProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty(Of DocumentPreview, Integer)("ActiveReportTemplateIndex", 0)
        #End Region
        #Region "PrintInterval"
        Public Property PrintInterval() As TimeInterval
            Get
                Return CType(GetValue(PrintIntervalProperty), TimeInterval)
            End Get
            Set(ByVal value As TimeInterval)
                SetValue(PrintIntervalProperty, value)
            End Set
        End Property
        Public Shared ReadOnly PrintIntervalProperty As DependencyProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty(Of DocumentPreview, TimeInterval)("PrintInterval", Nothing)
        #End Region
        #Region "IntervalStart"
        Public Property IntervalStart() As Date
            Get
                Return CDate(GetValue(IntervalStartProperty))
            End Get
            Set(ByVal value As Date)
                SetValue(IntervalStartProperty, value)
            End Set
        End Property
        Public Shared ReadOnly IntervalStartProperty As DependencyProperty = DependencyProperty.Register("IntervalStart", GetType(Date), GetType(DocumentPreview), New PropertyMetadata(New Date(2016, 07, 13), New PropertyChangedCallback(AddressOf OnIntervalStartChanged)))
        Protected Shared Sub OnIntervalStartChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(o, DocumentPreview).UpdateInterval()
            CType(o, DocumentPreview).UpdatePreview()
        End Sub
        #End Region
        #Region "IntervalEnd"
        Public Property IntervalEnd() As Date
            Get
                Return CDate(GetValue(IntervalEndProperty))
            End Get
            Set(ByVal value As Date)
                SetValue(IntervalEndProperty, value)
            End Set
        End Property
        Public Shared ReadOnly IntervalEndProperty As DependencyProperty = DependencyProperty.Register("IntervalEnd", GetType(Date), GetType(DocumentPreview), New PropertyMetadata(New Date(2016, 07, 20), New PropertyChangedCallback(AddressOf OnIntervalEndChanged)))
        Protected Shared Sub OnIntervalEndChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(o, DocumentPreview).UpdateInterval()
            CType(o, DocumentPreview).UpdatePreview()
        End Sub
        #End Region
        #Region "Report"
        Public Property Report() As XtraSchedulerReport
            Get
                Return CType(GetValue(ReportProperty), XtraSchedulerReport)
            End Get
            Set(ByVal value As XtraSchedulerReport)
                SetValue(ReportProperty, value)
            End Set
        End Property
        Public Shared ReadOnly ReportProperty As DependencyProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty(Of DocumentPreview, XtraSchedulerReport)("Report", Nothing)
        #End Region

        Private Sub UpdateInterval()
            If IntervalStart > IntervalEnd Then
                PrintInterval = New TimeInterval(IntervalStart, IntervalStart.AddDays(1))
            End If
            PrintInterval = New TimeInterval(IntervalStart, IntervalEnd)
        End Sub

        Private Sub UpdatePreview()

            Dim report_Renamed As New XtraSchedulerReport()
            report_Renamed.LoadLayout(ReportTemplateInfoSource(ActiveReportTemplateIndex).ReportTemplatePath)
            adapter.TimeInterval = New TimeInterval(IntervalStart, IntervalEnd)
            report_Renamed.SchedulerAdapter = adapter.SchedulerAdapter
            report_Renamed.CreateDocument()
            Report = report_Renamed
        End Sub

        Private Function GetAvailableResources() As ResourceBaseCollection
            Return storage.ResourceStorage.Items
        End Function

        Private Sub PrintAdapter_ValidateResources(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.Reporting.ResourcesValidationEventArgs)
            e.Resources.Clear()
            e.Resources.AddRange(GetAvailableResources())
        End Sub

        Private Sub SubscribePrintAdapterEvents()
            adapter.SchedulerStorage = storage
            AddHandler adapter.ValidateResources, AddressOf PrintAdapter_ValidateResources
        End Sub

        Private Sub UnsubscribePrintAdapterEvents()
            RemoveHandler adapter.ValidateResources, AddressOf PrintAdapter_ValidateResources
        End Sub

        Private Sub DataContextChangedMethod(ByVal sender As Object, ByVal e As DependencyPropertyChangedEventArgs)
            Dim element As FrameworkElement = DirectCast(sender, FrameworkElement)
            If element.DataContext IsNot Me Then
                element.DataContext = Me
            End If
        End Sub

        Private Sub cbe_reportTemplate_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            UpdatePreview()
        End Sub
    End Class
End Namespace
