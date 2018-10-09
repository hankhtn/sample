Imports System
Imports System.ComponentModel
Imports System.Windows
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DocumentViewer
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports

Namespace DevExpress.DevAV.Common.View
    Public MustInherit Class ReportServiceBase
        Inherits ServiceBase
        Implements IReportService


        Private isVisible_Renamed As Boolean
        Private defaultReportInfo As IReportInfo
        Private reportInfo As IReportInfo
        Private actualReportInfo As IReportInfo

        Protected Property IsVisible() As Boolean
            Get
                Return isVisible_Renamed
            End Get
            Set(ByVal value As Boolean)
                isVisible_Renamed = value
                UpdateReport()
                If Not isVisible_Renamed Then
                    Me.reportInfo = Nothing
                End If
            End Set
        End Property
        Protected Overridable Sub SetDefaultReport(ByVal reportInfo As IReportInfo)
            Me.defaultReportInfo = reportInfo
            UpdateReport()
        End Sub
        Protected Overridable Sub ShowReport(ByVal reportInfo As IReportInfo)
            Me.reportInfo = reportInfo
            UpdateReport()
        End Sub
        Private Sub UpdateReport()
            UpdateReportCore(If(IsVisible, (If(reportInfo, defaultReportInfo)), Nothing))
        End Sub
        Protected Overridable Sub UpdateReportCore(ByVal actualReportInfo As IReportInfo)
            UnsubscribeFromParametersViewModel()
            Me.actualReportInfo = actualReportInfo
            SubscribeToParametersViewModel()
            If Me.actualReportInfo Is Nothing Then
                DestroyReport()
            Else
                CreateReport()
            End If
        End Sub
        Private Sub OnParametersViewModelPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            CreateReport()
        End Sub

        Protected Sub CreateReport()
            Dim report As IReport = actualReportInfo.CreateReport()
            If report Is Nothing Then
                Return
            End If
            SetCustomSettingsViewModel(actualReportInfo.ParametersViewModel)
            SetDocumentSource(report)
            report.PrintingSystemBase.ClearContent()
            report.CreateDocument(True)
        End Sub
        Private Sub DestroyReport()
            SetCustomSettingsViewModel(Nothing)
        End Sub
        Protected MustOverride Sub SetDocumentSource(ByVal report As IReport)
        Protected MustOverride Sub SetCustomSettingsViewModel(ByVal customSettingsViewModel As Object)
        Private ReadOnly Property ActualParametersViewModel() As Object
            Get
                Return If(Me.actualReportInfo Is Nothing, Nothing, Me.actualReportInfo.ParametersViewModel)
            End Get
        End Property
        Private Sub SubscribeToParametersViewModel()
            Dim parametersViewModel As INotifyPropertyChanged = TryCast(ActualParametersViewModel, INotifyPropertyChanged)
            If parametersViewModel IsNot Nothing Then
                AddHandler parametersViewModel.PropertyChanged, AddressOf OnParametersViewModelPropertyChanged
            End If
        End Sub
        Private Sub UnsubscribeFromParametersViewModel()
            Dim parametersViewModel As INotifyPropertyChanged = TryCast(ActualParametersViewModel, INotifyPropertyChanged)
            If parametersViewModel IsNot Nothing Then
                RemoveHandler parametersViewModel.PropertyChanged, AddressOf OnParametersViewModelPropertyChanged
            End If
        End Sub
        #Region "IReportService"
        Private Sub IReportService_SetDefaultReport(ByVal reportInfo As IReportInfo) Implements IReportService.SetDefaultReport
            SetDefaultReport(reportInfo)
        End Sub
        Private Sub IReportService_ShowReport(ByVal reportInfo As IReportInfo) Implements IReportService.ShowReport
            ShowReport(reportInfo)
        End Sub
        #End Region
    End Class

    Public Class DocumentViewerReportService
        Inherits ReportServiceBase

        Private ReadOnly Property DocumentViewer() As DocumentPreviewControl
            Get
                Return CType(AssociatedObject, DocumentPreviewControl)
            End Get
        End Property

        Public Property ZoomMode() As ZoomMode

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            IsVisible = True
            ZoomMode = ZoomMode.FitToWidth
        End Sub
        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            IsVisible = False
        End Sub
        Protected Overrides Sub SetCustomSettingsViewModel(ByVal customSettingsViewModel As Object)
        End Sub

        Protected Overrides Sub SetDocumentSource(ByVal report As IReport)
            DocumentViewer.DocumentSource = report
            DocumentViewer.ZoomMode = ZoomMode
        End Sub
    End Class
End Namespace
