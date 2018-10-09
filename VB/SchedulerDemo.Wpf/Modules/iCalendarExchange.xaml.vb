Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.XtraScheduler
Imports System.IO
Imports DevExpress.XtraScheduler.iCalendar
Imports DevExpress.Xpf.Core
Imports System.Text

Namespace SchedulerDemo



    Partial Public Class iCalendarExchange
        Inherits SchedulerDemoModule

        Private baseDate As Date = Date.Today
        Public Sub New()
            InitializeComponent()
            InitializeAppointments()
        End Sub

        Private Sub InitializeAppointments()
            Dim apt1 As Appointment = CreateAppointment(Me.baseDate, "Mr.Brown", 1)
            Dim apt2 As Appointment = CreateAppointment(baseDate.AddDays(1), "Repair", 2)

            Dim apt3 As Appointment = CreateAppointment(baseDate, "Wash", 3, AppointmentType.Pattern)
            apt3.RecurrenceInfo.OccurrenceCount = 3
            apt3.RecurrenceInfo.Periodicity = 2
            apt3.RecurrenceInfo.Start = apt3.Start
            apt3.RecurrenceInfo.Range = RecurrenceRange.OccurrenceCount

            Dim apt4 As Appointment = CreateAppointment(baseDate.AddDays(-2), "Mr.Green", 4)
            apt4.AllDay = True

            scheduler.Storage.AppointmentStorage.Add(apt1)
            scheduler.Storage.AppointmentStorage.Add(apt2)
            scheduler.Storage.AppointmentStorage.Add(apt3)
            scheduler.Storage.AppointmentStorage.Add(apt4)
        End Sub

        Private Function CreateAppointment(ByVal start As Date, ByVal subject As String, ByVal label As Integer, Optional ByVal type As AppointmentType = AppointmentType.Normal) As Appointment
            Dim apt As Appointment = scheduler.Storage.CreateAppointment(type)
            apt.Subject = subject
            Dim rnd As Random = DemoUtils.RandomInstance
            apt.AllDay = rnd.Next(0, 5) = 0

            Dim rangeInMinutes As Integer = 60 * 24
            apt.Start = start.Add(TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes)))
            Dim duration As TimeSpan = TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4))
            If duration.Ticks = 0 Then
                duration = TimeSpan.FromMinutes(5)
            End If

            apt.End = apt.Start + duration
            apt.LabelKey = label
            Return apt
        End Function

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

        Private Sub btnImport_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            BeforeImportActions()
            Using stream As Stream = CreateInputStream(txtVCalendar.Text)
                ImportAppointments(stream)
            End Using
            AfterImportActions()
        End Sub

        Private Sub BeforeImportActions()
            RemoveHandler txtVCalendar.TextChanged, AddressOf txtVCalendar_TextChanged
        End Sub

        Private Sub AfterImportActions()
            AddHandler txtVCalendar.TextChanged, AddressOf txtVCalendar_TextChanged
        End Sub

        Private Sub txtVCalendar_TextChanged(ByVal sender As Object, ByVal e As TextChangedEventArgs)
            btnImport.IsEnabled = If(txtVCalendar.Text.Length <> 0, True, False)
        End Sub

        Public Shared Function CreateInputStream(ByVal text As String) As Stream
            Dim buf() As Byte = Encoding.Unicode.GetBytes(text)
            Return New MemoryStream(buf, 0, buf.Length)
        End Function

        Private Sub ImportAppointments(ByVal stream As Stream)
            If stream Is Nothing Then
                Return
            End If
            scheduler.Storage.AppointmentStorage.Clear()
            Dim importer As New iCalendarImporter(scheduler.Storage.InnerStorage)
            AddHandler importer.CalendarStructureCreated, AddressOf importer_CalendarStructureCreated
            AddHandler importer.AppointmentImporting, AddressOf importer_AppointmentImporting
            AddHandler importer.OnException, AddressOf importer_OnException
            importer.Import(stream)
        End Sub

        Private Sub importer_OnException(ByVal sender As Object, ByVal e As ExchangeExceptionEventArgs)
            Dim args As iCalendarParseExceptionEventArgs = TryCast(e, iCalendarParseExceptionEventArgs)
            If args IsNot Nothing Then
                DXMessageBox.Show(String.Format("Can't parse line '{1}' at {0} index", args.LineIndex, args.LineText))
                Dim importer As iCalendarImporter = DirectCast(sender, iCalendarImporter)
                importer.Terminate()
            End If
            e.Handled = True
        End Sub
        Private Sub importer_AppointmentImporting(ByVal sender As Object, ByVal e As AppointmentImportingEventArgs)
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, New System.Threading.ThreadStart(Sub()
            End Sub))

            Dim cancel As Boolean = If(e.Appointment.IsRecurring, IsCancelForRecurring, IsCancelForNonRecurring)
            If cancel Then
                e.Cancel = True
            End If
        End Sub
        Private Sub importer_CalendarStructureCreated(ByVal sender As Object, ByVal e As iCalendarStructureCreatedEventArgs)
            Dim importer As iCalendarImporter = DirectCast(sender, iCalendarImporter)
        End Sub

        Private Sub btnExport_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim ms As New MemoryStream()
            Using sr As New StreamReader(ms)
                ExportAppointments(ms)
                ms.Seek(0, SeekOrigin.Begin)
                txtVCalendar.Text = sr.ReadToEnd()
            End Using
        End Sub

        Private Sub ExportAppointments(ByVal stream As Stream)
            If stream Is Nothing Then
                Return
            End If
            Dim exporter As New iCalendarExporter(scheduler.Storage.InnerStorage)
            Dim sourceObjectCount As Integer = exporter.SourceObjectCount
            exporter.ProductIdentifier = "-//Developer Express Inc.//DXScheduler iCalendarExchangeDemo//EN"
            AddHandler exporter.AppointmentExporting, AddressOf exporter_AppointmentExporting
            exporter.Export(stream)
        End Sub

        Private Sub exporter_AppointmentExporting(ByVal sender As Object, ByVal e As AppointmentExportingEventArgs)
            DispatcherHelper.DoEvents()
            Dim cancel As Boolean = If(e.Appointment.IsRecurring, IsCancelForRecurring, IsCancelForNonRecurring)
            If cancel Then
                e.Cancel = True
            End If
        End Sub
        Private Sub scheduler_Drop(ByVal sender As Object, ByVal e As DragEventArgs)
            Dim fileNames() As String = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
            If fileNames Is Nothing OrElse fileNames.Length = 0 Then
                Return
            End If

            For Each fileName As String In fileNames
                If File.Exists(fileName) Then
                    Using stream As Stream = File.Open(fileName, FileMode.Open)
                        ImportAppointments(stream)
                    End Using
                End If
            Next fileName
        End Sub
    End Class
End Namespace
