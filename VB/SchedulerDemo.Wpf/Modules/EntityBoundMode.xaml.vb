Imports System.Data.Common
Imports System.IO
Imports DevExpress.Internal
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Data.SQLite
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace SchedulerDemo



    Partial Public Class EntityBoundMode
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    #Region "Data model"
    Public Class DoctorSchedule
        <System.ComponentModel.DataAnnotations.Key>
        Public Property Id() As Int64
        Public Property AllDay() As Boolean
        Public Property Start() As Date
        Public Property [End]() As Date
        Public Property PatientName() As String
        Public Property Note() As String
        Public Property PaymentStutusId() As Integer
        Public Property IssueId() As Integer
        Public Property EventType() As Integer
        Public Property Location() As String
        Public Property RecurrenceInfo() As String
        Public Property ReminderInfo() As String
        Public Property DoctorId() As Int64
    End Class
    Public Class Doctor
        <System.ComponentModel.DataAnnotations.Key>
        Public Property Id() As Int64
        Public Property Name() As String
    End Class

    Public Class DoctorScheduleContext
        Inherits DbContext

        Public Sub New()
            MyBase.New(CreateConnection(), True)
        End Sub
        Public Sub New(ByVal connectionString As String)
            MyBase.New(connectionString)
        End Sub
        Public Sub New(ByVal connection As DbConnection)
            MyBase.New(connection, True)
        End Sub

        Shared Sub New()
            Database.SetInitializer(Of DoctorScheduleContext)(Nothing)
        End Sub

        Public Property DoctorSchedules() As DbSet(Of DoctorSchedule)
        Public Property Doctors() As DbSet(Of Doctor)

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)
        End Sub

        Private Shared filePath As String
        Private Shared Function CreateConnection() As DbConnection
            If filePath Is Nothing Then
                filePath = DataDirectoryHelper.GetFile("doctors.db", DataDirectoryHelper.DataFolderName)
            End If
            File.SetAttributes(filePath, File.GetAttributes(filePath) And (Not FileAttributes.ReadOnly))
            Dim connection = DbProviderFactories.GetFactory("System.Data.SQLite.EF6").CreateConnection()
            connection.ConnectionString = New SQLiteConnectionStringBuilder With {.DataSource = filePath}.ConnectionString
            Return connection
        End Function
    End Class
    #End Region

    Public Class EntityBoundModeViewModel
        Inherits ViewModelBase

        Public Shared BaseDate As Date = Date.Today.AddDays(-1)
        Public Shared IssueList() As String = { "Consultation", "Treatment", "X-Ray" }
        Public Shared IssueColorList() As Color = { Colors.SteelBlue, Colors.Pink, Colors.SeaShell }
        Public Shared PatientsName() As String = { "Devid Mitchell", "Mark Oliver", "Addison Davis", "Benjamin Hughes", "Lucas Smith" }
        Public Shared DoctorsName() As String = { "Isabella Carter", "Miguel Simmons", "Madeline Russell", "Ariana Alexander" }
        Public Shared PaymentStatuses() As String = { "Paid", "Unpaid" }
        Public Shared PaymentColorStatuses() As Color = { Colors.Green, Colors.Red }

        Private dataContext As DoctorScheduleContext
        Private rnd As New Random()

        Public Sub New()
            Me.dataContext = New DoctorScheduleContext()
            Me.dataContext.Doctors.Load()
            Me.dataContext.DoctorSchedules.Load()
            Dim shouldCreateDoctors As Boolean = Me.dataContext.Doctors.Count() <= 0
            If shouldCreateDoctors Then
                CreateDoctors()
            End If
            Dim shouldCreateDoctorSchedules As Boolean = Me.dataContext.DoctorSchedules.Count() <= 0
            If shouldCreateDoctorSchedules Then
                CreateDoctorsSchedule()
            End If
            If shouldCreateDoctorSchedules OrElse shouldCreateDoctors Then
                Me.dataContext.SaveChanges()
            End If

            InitializeLabelsAndStutuses()
        End Sub

        Public ReadOnly Property Doctors() As IEnumerable
            Get
                Return Me.dataContext.Doctors.Local
            End Get
        End Property
        Public ReadOnly Property DoctorSchedules() As IEnumerable
            Get
                Return Me.dataContext.DoctorSchedules.Local
            End Get
        End Property
        Public Property Labels() As AppointmentLabelCollection
        Public Property Statuses() As AppointmentStatusCollection

        Private Sub CreateDoctors()
            Dim doctorCount As Integer = DoctorsName.Length

            Dim doctors_Renamed = Me.dataContext.Doctors
            For i As Integer = 0 To doctorCount - 1
                Dim doctor As Doctor = dataContext.Doctors.Create()
                doctor.Name = DoctorsName(i)
                doctors_Renamed.Add(doctor)
            Next i
        End Sub
        Private Sub CreateDoctorsSchedule()
            Dim rnd As New Random()
            Dim scheduleCount As Integer = dataContext.DoctorSchedules.Count()
            If scheduleCount > 0 Then
                Return
            End If
            Dim doctorCount As Integer = DoctorsName.Length
            For doctorId As Integer = 1 To doctorCount
                CreateDoctorSchedule(doctorId)
            Next doctorId
        End Sub
        Private Sub CreateDoctorSchedule(ByVal doctorId As Integer)
            Dim start As Date = BaseDate
            Do While start < BaseDate.AddDays(7)
                CreateDoctorSchedule(doctorId, start.AddHours(Me.rnd.Next(9, 12)))
                CreateDoctorSchedule(doctorId, start.AddHours(Me.rnd.Next(14, 16)))
                CreateDoctorSchedule(doctorId, start.AddHours(Me.rnd.Next(18, 20)))
                start = start.Add(TimeSpan.FromDays(1))
            Loop
        End Sub
        Private Sub CreateDoctorSchedule(ByVal doctorId As Integer, ByVal start As Date)
            Dim doctorSchedule As DoctorSchedule = dataContext.DoctorSchedules.Create()
            doctorSchedule.Start = start
            doctorSchedule.End = start.AddHours(Me.rnd.Next(1, 3))
            doctorSchedule.IssueId = Me.rnd.Next(0, 3)
            doctorSchedule.PaymentStutusId = Me.rnd.Next(0, 2)
            Dim patintsNameCount As Integer = PatientsName.Length
            doctorSchedule.PatientName = PatientsName(Me.rnd.Next(0, patintsNameCount - 1))
            doctorSchedule.DoctorId = doctorId
            Me.dataContext.DoctorSchedules.Add(doctorSchedule)
        End Sub
        Private Sub InitializeLabelsAndStutuses()
            Labels = New AppointmentLabelCollection()
            Dim count As Integer = IssueList.Length
            For i As Integer = 0 To count - 1
                Dim label As AppointmentLabel = Labels.CreateNewLabel(i, IssueList(i))
                label.SetColor(IssueColorList(i))
                Labels.Add(label)
            Next i

            Statuses = New AppointmentStatusCollection()
            count = PaymentStatuses.Length
            For i As Integer = 0 To count - 1
                Dim status As IAppointmentStatus = Statuses.CreateNewStatus(i, PaymentStatuses(i))
                status.Type = AppointmentStatusType.Custom
                status.SetBrush(New SolidColorBrush(PaymentColorStatuses(i)))
                Statuses.Add(status)
            Next i
        End Sub
    End Class
End Namespace
