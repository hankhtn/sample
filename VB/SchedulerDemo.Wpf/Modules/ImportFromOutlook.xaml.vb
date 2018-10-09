Imports System
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Exchange
Imports DevExpress.XtraScheduler.Outlook
Imports DevExpress.Xpf.Core

Namespace SchedulerDemo



    Partial Public Class ImportFromOutlook
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            FillCalendarNamesCombo()
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

        Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Try
                Import()
            Catch ex As Exception
                DemoUtils.ReportOutlookError("import appointments from Microsoft Outlook", ex.Message)
            End Try
        End Sub

        Private Sub Import()
            Dim importer As New DevExpress.XtraScheduler.Outlook.OutlookImport(scheduler.Storage.InnerStorage)
            DirectCast(importer, ISupportCalendarFolders).CalendarFolderName = CalendarName
            AddHandler importer.AppointmentImporting, AddressOf importer_AppointmentImporting
            AddHandler importer.OnException, AddressOf importer_OnException

            BeforeImport(importer.SourceObjectCount)
            Try
                importer.Import(System.IO.Stream.Null)
            Finally
                AfterImport()
            End Try
        End Sub

        Private Sub BeforeImport(ByVal objectCount As Integer)
            progressBar.Value = 0
            progressBar.Maximum = objectCount
        End Sub
        Private Sub AfterImport()
            progressBar.Value = 0
        End Sub

        Private Sub importer_OnException(ByVal sender As Object, ByVal e As ExchangeExceptionEventArgs)
            Dim errText As String = e.OriginalException.Message
            Dim args As OutlookExchangeExceptionEventArgs = TryCast(e, OutlookExchangeExceptionEventArgs)
            If args IsNot Nothing Then
                If args.OutlookAppointment IsNot Nothing Then
                    errText &= String.Format(ControlChars.CrLf & "Event '{0}' started on {1:D} at {2}" & ControlChars.Lf, args.OutlookAppointment.Subject, args.OutlookAppointment.Start, args.OutlookAppointment.Start.TimeOfDay)
                End If
            End If
            Dim importer As AppointmentImporter = DirectCast(sender, AppointmentImporter)
            importer.Terminate()
            e.Handled = True
            Throw e.OriginalException
        End Sub
        Private Sub importer_AppointmentImporting(ByVal sender As Object, ByVal e As AppointmentImportingEventArgs)
            Dim args As OutlookAppointmentImportingEventArgs = CType(e, OutlookAppointmentImportingEventArgs)

            progressBar.Value += 1
            DispatcherHelper.DoEvents()

            Dim cancel As Boolean = If(args.OutlookAppointment.IsRecurring, IsCancelForRecurring, IsCancelForNonRecurring)
            If cancel Then
                e.Cancel = True
            End If
        End Sub
    End Class
End Namespace
