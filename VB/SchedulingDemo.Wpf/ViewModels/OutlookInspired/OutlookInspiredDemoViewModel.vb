Imports System
Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Windows
Imports Microsoft.Win32
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.Xpf.Scheduling.iCalendar
Imports DevExpress.XtraScheduler

Namespace SchedulingDemo.ViewModels
    Public Class OutlookInspiredDemoViewModel
        Private Shared ReadOnly TimeScales As IList(Of TimeSpan) = New TimeSpan() { TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(6), TimeSpan.FromMinutes(5) }
        Private Shared ReadOnly data As New OutlookInspiredData()

        Private selectedAppointment_Renamed As AppointmentItem

        Protected Sub New()
            MinZoomFactor = 0
            MaxZoomFactor = 100
            ZoomStep = 20
            ZoomFactor = 20
            LinksViewModel = LinksViewModel.Create()
            ViewSettings = ViewSettingsViewModel.Create()
            PrintStyles = CalendarDetailPrintReportViewModel.Create()
            CalendarSettings = CalendarSettingsViewModel.Create()
        End Sub

        Public Overridable ReadOnly Property BackstageViewService() As IBackstageViewService
            Get
                Return Nothing
            End Get
        End Property
        Public Overridable Property LinksViewModel() As LinksViewModel
        Public Overridable Property ViewSettings() As ViewSettingsViewModel
        Public Overridable Property PrintStyles() As CalendarDetailPrintReportViewModel
        Public Overridable Property CalendarSettings() As CalendarSettingsViewModel
        Public Overridable ReadOnly Property Calendars() As ObservableCollection(Of Calendar)
            Get
                Return data.Calendars
            End Get
        End Property
        Public Overridable ReadOnly Property Events() As ObservableCollection(Of OutlookEvent)
            Get
                Return data.Events
            End Get
        End Property
        Public Overridable ReadOnly Property Categories() As ObservableCollection(Of Category)
            Get
                Return data.Categories
            End Get
        End Property

        <BindableProperty>
        Public Overridable Property SelectedAppointment() As AppointmentItem
            Get
                Return Me.selectedAppointment_Renamed
            End Get
            Set(ByVal value As AppointmentItem)
                If Me.selectedAppointment_Renamed IsNot Nothing Then
                    RemoveHandler Me.selectedAppointment_Renamed.PropertyChanged, AddressOf PropertyChanged
                End If
                Dim newSelectedAppointment As AppointmentItem = value
                If newSelectedAppointment IsNot Nothing Then
                    AddHandler newSelectedAppointment.PropertyChanged, AddressOf PropertyChanged
                End If
                Me.selectedAppointment_Renamed = newSelectedAppointment
                UpdatePriority(Me.selectedAppointment_Renamed)
            End Set
        End Property

        Public Overridable Property TimeScale() As TimeSpan
        Public Overridable Property ZoomFactor() As Double
        Public Overridable Property MinZoomFactor() As Integer
        Public Overridable Property MaxZoomFactor() As Integer
        Public Overridable Property HighImportanceChecked() As Boolean
        Public Overridable Property LowImportanceChecked() As Boolean

        Private Property ZoomStep() As Integer

        Public Sub Print(ByVal scheduler As SchedulerControl)
            Dim printingSettingsWindow As New PrintingSettingsWindow()
            printingSettingsWindow.DataContext = PrintingSettingsWindowViewModel.Create(scheduler, Utils.GetReportTemplateCollection())
            printingSettingsWindow.Owner = Window.GetWindow(scheduler)
            printingSettingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner
            printingSettingsWindow.ShowDialog()
        End Sub
        Public Sub OutlookImport(ByVal scheduler As SchedulerControl)
            OutlookExchange(scheduler, OutlookExchangeType.Import)
        End Sub
        Public Sub OutlookExport(ByVal scheduler As SchedulerControl)
            OutlookExchange(scheduler, OutlookExchangeType.Export)
        End Sub
        Public Sub iCalendarImport(ByVal scheduler As SchedulerControl)
            Dim importer As New ICalendarImporter(scheduler)
            AddHandler importer.AppointmentItemImported, AddressOf AppointmentItemImported
            Using stream As Stream = OpenRead("Calendar", "iCalendar files (*.ics)|*.ics")
                If stream IsNot Nothing Then
                    importer.Import(stream)
                End If
            End Using
        End Sub

        Public Sub iCalendarExport(ByVal scheduler As SchedulerControl)
            Dim exporter As New ICalendarExporter(scheduler)
            Using stream As Stream = OpenWrite("Calendar", "iCalendar files (*.ics)|*.ics")
                If stream IsNot Nothing Then
                    exporter.Export(stream)
                    stream.Flush()
                End If
            End Using
        End Sub
        Public Sub Send(ByVal scheduler As SchedulerControl)
            Utils.SendMail(scheduler, scheduler.SelectedAppointments)
        End Sub
        Public Sub SetHighPriority()
            If SelectedAppointment Is Nothing Then
                Return
            End If
            Dim priority As EventPriority = CType(SelectedAppointment.CustomFields("Priority"), EventPriority)
            SelectedAppointment.CustomFields("Priority") = If(priority = EventPriority.HighImportance, EventPriority.None, EventPriority.HighImportance)
            If LowImportanceChecked Then
                LowImportanceChecked = Not HighImportanceChecked
            End If
        End Sub
        Public Sub SetLowPriority()
            If SelectedAppointment Is Nothing Then
                Return
            End If
            Dim priority As EventPriority = CType(SelectedAppointment.CustomFields("Priority"), EventPriority)
            SelectedAppointment.CustomFields("Priority") = If(priority = EventPriority.LowImportance, EventPriority.None, EventPriority.LowImportance)
            If HighImportanceChecked Then
                HighImportanceChecked = Not LowImportanceChecked
            End If
        End Sub
        Public Sub OpenViewSettingsWindow(ByVal scheduler As SchedulerControl)
            Dim settingsWindow As New CalendarSettingsWindow()
            Dim newCalendarSettings As CalendarSettingsViewModel = CalendarSettingsViewModel.Create()
            newCalendarSettings.ApplySettings(CalendarSettings)
            settingsWindow.DataContext = newCalendarSettings
            settingsWindow.Owner = Window.GetWindow(scheduler)
            settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner
            If settingsWindow.ShowDialog() = True Then
                CalendarSettings.ApplySettings(newCalendarSettings)
            End If

        End Sub
        Public Sub ResetViewSettings()
            CalendarSettings = CalendarSettingsViewModel.Create()
        End Sub
        Public Sub CloseBackstageView()
            BackstageViewService.Close()
        End Sub

        Protected Sub OnZoomFactorChanged()
            Dim value As Integer = CInt((ZoomFactor / ZoomStep))
            TimeScale = TimeScales(value)
        End Sub
        Protected Sub OnTimeScaleChanged()
            Dim value As Integer = TimeScales.IndexOf(TimeScale)
            ZoomFactor = ZoomStep * value
        End Sub

        Private Sub UpdatePriority(ByVal appointment As AppointmentItem)
            If appointment Is Nothing Then
                Return
            End If
            Dim priority As EventPriority = CType(appointment.CustomFields("Priority"), EventPriority)
            HighImportanceChecked = priority = EventPriority.HighImportance
            LowImportanceChecked = priority = EventPriority.LowImportance
        End Sub
        Private Sub PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            If e.PropertyName = "Priority" Then
                UpdatePriority(TryCast(sender, AppointmentItem))
            End If
        End Sub

        Private Function OpenRead(ByVal fileName As String, ByVal filter As String) As Stream
            Dim dialog As New OpenFileDialog() With {.FileName = fileName, .Filter = filter, .FilterIndex = 1}
            If Not dialog.ShowDialog().Equals(True) Then
                Return Nothing
            End If
            Return dialog.OpenFile()
        End Function
        Private Function OpenWrite(ByVal fileName As String, ByVal filter As String) As Stream
            Dim dialog As New SaveFileDialog() With {.FileName = fileName, .Filter = filter, .FilterIndex = 1}
            If Not dialog.ShowDialog().Equals(True) Then
                Return Nothing
            End If
            Return dialog.OpenFile()
        End Function
        Private Sub OutlookExchange(ByVal scheduler As SchedulerControl, ByVal exchangeType As OutlookExchangeType)
            Try
                Dim outlookCalendarPaths() As String = DevExpress.XtraScheduler.Outlook.OutlookExchangeHelper.GetOutlookCalendarPaths()
                If outlookCalendarPaths Is Nothing OrElse outlookCalendarPaths.Length = 0 Then
                    Return
                End If

                Dim optionsWindow As New OutlookExchangeOptionsWindow()
                optionsWindow.DataContext = OutlookExchangeOptionsWindowViewModel.Create(scheduler, exchangeType, outlookCalendarPaths)
                optionsWindow.Owner = Window.GetWindow(scheduler)
                optionsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner
                optionsWindow.ShowDialog()
            Catch
                DXMessageBox.Show(String.Format("Unable to {0}." & ControlChars.Lf & "Check whether MS Outlook is installed.", "get the list of available calendars from Microsoft Outlook"), "Import from MS Outlook", MessageBoxButton.OK, MessageBoxImage.Warning)
            End Try
        End Sub

        Private Sub AppointmentItemImported(ByVal sender As Object, ByVal e As AppointmentItemImportedEventArgs)
            Dim customFields As CustomFieldCollection = e.Appointment.CustomFields
            customFields("Priority") = ConvertToEventPriority(customFields("Priority"))
            customFields("IsPrivate") = ConvertToBoolean(customFields("IsPrivate"))
        End Sub

        Private Function ConvertToBoolean(ByVal customField As Object) As Boolean
            Return If(customField Is Nothing, False, AreStringsEqual(customField.ToString(), "true"))
        End Function

        Private Function ConvertToEventPriority(ByVal customField As Object) As EventPriority
            If customField Is Nothing Then
                Return EventPriority.None
            End If
            Dim priority As EventPriority = EventPriority.HighImportance
            Dim value As String = customField.ToString()
            If AreStringsEqual(value, priority.ToString()) Then
                Return priority
            End If
            priority = EventPriority.LowImportance
            If AreStringsEqual(value, priority.ToString()) Then
                Return priority
            End If
            Return EventPriority.None
        End Function

        Private Function AreStringsEqual(ByVal left As String, ByVal right As String) As Boolean
            Return String.Compare(left, right, StringComparison.InvariantCultureIgnoreCase) = 0
        End Function
    End Class
End Namespace
