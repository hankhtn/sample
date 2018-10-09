Imports System
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Exchange
Imports DevExpress.XtraScheduler.Outlook
Imports DevExpress.Xpf.Core
Imports SchedulerDemo.Internal

Namespace SchedulerDemo
    Partial Public Class SynchronizeWithOutlook
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            FillCalendarNamesCombo()
            Dim eventList As New SchedulerBindingList(Of CustomEvent)()
            scheduler.Storage.AppointmentStorage.DataSource = eventList

            scheduler.Storage.AppointmentStorage.Mappings.Start = "StartTime"
            scheduler.Storage.AppointmentStorage.Mappings.End = "EndTime"
            scheduler.Storage.AppointmentStorage.Mappings.Subject = "Subject"
            scheduler.Storage.AppointmentStorage.Mappings.AllDay = "AllDay"
            scheduler.Storage.AppointmentStorage.Mappings.Description = "Description"
            scheduler.Storage.AppointmentStorage.Mappings.Label = "Label"
            scheduler.Storage.AppointmentStorage.Mappings.Location = "Location"
            scheduler.Storage.AppointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo"
            scheduler.Storage.AppointmentStorage.Mappings.ReminderInfo = "ReminderInfo"
            scheduler.Storage.AppointmentStorage.Mappings.ResourceId = "OwnerId"
            scheduler.Storage.AppointmentStorage.Mappings.Status = "Status"
            scheduler.Storage.AppointmentStorage.Mappings.Type = "EventType"
        End Sub

        Protected ReadOnly Property IsCancelForRecurring() As Boolean
            Get
                Return Object.Equals(cbCancel.EditValue, UsedAppointmentType.Recurring)
            End Get
        End Property
        Protected ReadOnly Property IsCancelForNonRecurring() As Boolean
            Get
                Return Object.Equals(cbCancel.EditValue, UsedAppointmentType.NonRecurring)
            End Get
        End Property
        Protected ReadOnly Property CalendarName() As String
            Get
                Return cbCalendarFolderPaths.Text
            End Get
        End Property

        Private Sub FillCalendarNamesCombo()
            Try
                cbCalendarFolderPaths.Items.Clear()
                cbCalendarFolderPaths.Items.AddRange(DemoUtils.OutlookCalendarPaths)
            Finally
                cbCalendarFolderPaths.SelectedIndex = 0
            End Try
        End Sub

        #Region "supplementary functionality"
        Private Sub BeforeSynchronization(ByVal objectCount As Integer)
            progressBar.Value = 0
            progressBar.Maximum = objectCount
        End Sub
        Private Sub AfterSynchronization()
            progressBar.Value = 0
        End Sub
        #End Region

        Private Sub btnSynchronize_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Synchronize()
            Catch ex As Exception
                DemoUtils.ReportOutlookError("synchronize XtraScheduler with Microsoft Outlook", ex.Message)
            End Try
        End Sub
        Private Sub Synchronize()
            Dim synchronizer As New OutlookImportSynchronizer(scheduler.Storage.InnerStorage)
            DirectCast(synchronizer, ISupportCalendarFolders).CalendarFolderName = CalendarName
            synchronizer.ForeignIdFieldName = "EntryID"
            AddHandler synchronizer.AppointmentSynchronizing, AddressOf synchronizer_AppointmentSynchronizing
            AddHandler synchronizer.OnException, AddressOf synchronizer_OnException

            BeforeSynchronization(synchronizer.SourceObjectCount)
            Try
                synchronizer.Synchronize()
            Finally
                AfterSynchronization()
            End Try
        End Sub

        Private Sub synchronizer_OnException(ByVal sender As Object, ByVal e As ExchangeExceptionEventArgs)
            Dim errText As String = e.OriginalException.Message
            Dim args As OutlookExchangeExceptionEventArgs = TryCast(e, OutlookExchangeExceptionEventArgs)
            If args IsNot Nothing Then
                If args.OutlookAppointment IsNot Nothing Then
                    errText &= String.Format(ControlChars.CrLf & "Event '{0}' started on {1:D} at {2}" & ControlChars.Lf, args.OutlookAppointment.Subject, args.OutlookAppointment.Start, args.OutlookAppointment.Start.TimeOfDay)
                End If
            End If
            Dim synchronizer As AppointmentImportSynchronizer = DirectCast(sender, AppointmentImportSynchronizer)
            synchronizer.Terminate()
            e.Handled = True
            Throw e.OriginalException
        End Sub
        Private Sub synchronizer_AppointmentSynchronizing(ByVal sender As Object, ByVal e As AppointmentSynchronizingEventArgs)
            progressBar.Value += 1
            DispatcherHelper.DoEvents()

            Dim apt As Appointment = e.Appointment
            Dim cancel As Boolean = False
            If apt IsNot Nothing Then
                cancel = If(apt.IsRecurring, IsCancelForRecurring, IsCancelForNonRecurring)
            End If

            If cancel Then
                e.Cancel = True
                Return
            End If

            Select Case e.Operation
                Case SynchronizeOperation.Create
                    If CBool(chDontCreateNew.IsChecked) Then
                        e.Cancel = True
                    End If

                Case SynchronizeOperation.Delete
                    If CBool(chDontDeleteExisting.IsChecked) Then
                        e.Cancel = True
                    End If

                Case SynchronizeOperation.Replace
                    If CBool(chDeleteInsteadReplace.IsChecked) Then
                        e.Operation = SynchronizeOperation.Delete
                    End If
            End Select

        End Sub
    End Class
End Namespace
