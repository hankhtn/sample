Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Linq
Imports Microsoft.Win32
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.Xpf.Scheduling.Reporting
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraScheduler.Reporting
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.Xpf.Printing.Native

Namespace SchedulingDemo.ViewModels
    Public Class PrintingSettingsWindowViewModel
        Public Shared Function Create(ByVal scheduler As SchedulerControl, ByVal reportTemplateInfos As IList(Of ReportTemplateInfo)) As PrintingSettingsWindowViewModel
            Return ViewModelSource.Create(Function() New PrintingSettingsWindowViewModel(scheduler, reportTemplateInfos))
        End Function
        Private Shared Sub MoveSelectedItem(ByVal source As IList(Of ResourceItem), ByVal target As IList(Of ResourceItem), ByVal selectedItem As ResourceItem)
            Dim selectedItemIndex As Integer = source.IndexOf(selectedItem)
            If selectedItemIndex < 0 Then
                Return
            End If
            target.Add(source(selectedItemIndex))
            source.RemoveAt(selectedItemIndex)
        End Sub

        Private ReadOnly scheduler As SchedulerControl

        Protected Sub New(ByVal scheduler As SchedulerControl, ByVal reportTemplateInfos As IList(Of ReportTemplateInfo))
            Me.scheduler = scheduler
            AddHandler Me.scheduler.SchedulerPrintAdapter.ValidateResources, AddressOf OnValidateResources

            Me.ReportTemplateInfos = New ObservableCollection(Of ReportTemplateInfo)(reportTemplateInfos)

            If reportTemplateInfos.Count > 0 Then
                ActiveReportTemplateInfo = reportTemplateInfos(0)
            End If

            StartDate = scheduler.VisibleIntervals.First().Start
            EndDate = scheduler.VisibleIntervals.Last().End
            AvailableResources = New ObservableCollection(Of ResourceItem)(scheduler.VisibleResources)
            CustomResources = New ObservableCollection(Of ResourceItem)()
        End Sub

        Public Property UseSpecificResources() As Boolean
        Public Property StartDate() As Date
        Public Property EndDate() As Date
        Public Property ActiveReportTemplateInfo() As ReportTemplateInfo
        Public Property ReportTemplateInfos() As ObservableCollection(Of ReportTemplateInfo)
        Public Property AvailableResources() As ObservableCollection(Of ResourceItem)
        Public Property SelectedAvailableResource() As ResourceItem
        Public Property CustomResources() As ObservableCollection(Of ResourceItem)
        Public Property SelectedCustomResource() As ResourceItem
        Public Property PrintResources() As ObservableCollection(Of ResourceItem)

        Public Sub AddToCustomCollection()
            MoveSelectedItem(AvailableResources, CustomResources, SelectedAvailableResource)
        End Sub
        Public Sub RemoveFromCustomCollection()
            MoveSelectedItem(CustomResources, AvailableResources, SelectedCustomResource)
        End Sub
        Public Sub MoveUp()
            Dim index As Integer = CustomResources.IndexOf(SelectedCustomResource)
            If index <= 0 Then
                Return
            End If
            Dim item As ResourceItem = CustomResources(index)
            CustomResources.RemoveAt(index)
            CustomResources.Insert(index - 1, item)
        End Sub
        Public Sub MoveDown()
            Dim index As Integer = CustomResources.IndexOf(SelectedCustomResource)
            If (index < 0) OrElse (index >= CustomResources.Count - 1) Then
                Return
            End If
            Dim item As ResourceItem = CustomResources(index)
            CustomResources.RemoveAt(index)
            CustomResources.Insert(index + 1, item)
        End Sub
        Public Sub Preview()
            Dim report As XtraSchedulerReport = SchedulerReportFactory.Create(ActiveReportTemplateInfo.ReportTemplatePath, Me.scheduler, New DateTimeRange(StartDate, EndDate))
            report.PrintingSystem.ReplaceService(Of BackgroundPageBuildEngineStrategy)(New DispatcherPageBuildStrategy())
            report.ShowPreview()
        End Sub
        Public Sub OpenFileDialog()
            Dim dlg As New OpenFileDialog()
            dlg.CheckPathExists = True
            dlg.Filter = "Report template files (*.schrepx)|*.schrepx|All files (*.*)|*.*"

            Dim folderPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            dlg.InitialDirectory = (New DirectoryInfo(folderPath)).FullName

            Dim result? As Boolean = dlg.ShowDialog()
            If result Is Nothing OrElse result = False Then
                Return
            End If

            ActiveReportTemplateInfo = New ReportTemplateInfo(dlg.SafeFileName, dlg.FileName)
            ReportTemplateInfos.Add(ActiveReportTemplateInfo)
        End Sub
        Public Sub WindowClosing()
            RemoveHandler Me.scheduler.SchedulerPrintAdapter.ValidateResources, AddressOf OnValidateResources
        End Sub

        Protected Sub OnStartDateChanged()
            If EndDate < StartDate Then
                EndDate = StartDate
            End If
        End Sub
        Protected Sub OnEndDateChanged()
            If EndDate < StartDate Then
                StartDate = EndDate
            End If
        End Sub

        Private Sub OnValidateResources(ByVal sender As Object, ByVal e As ResourceItemsValidationEventArgs)
            Dim resources As IList(Of ResourceItem) = e.ResourceItems
            resources.Clear()
            If UseSpecificResources Then
                CustomResources.ForEach(Sub(x) resources.Add(x))
            Else
                AvailableResources.ForEach(Sub(x) resources.Add(x))
            End If
        End Sub
    End Class
End Namespace
