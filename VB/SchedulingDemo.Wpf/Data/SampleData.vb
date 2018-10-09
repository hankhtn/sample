Imports System
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Mvvm

Namespace SchedulingDemo
    Public Class SampleDataGenerator
        Private Shared Statuses() As Integer = { 0, 1, 2, 2, 2, 2, 2, 2, 4 }
        Private Shared Labels() As Integer = { 0, 2, 2, 2, 2, 2, 2, 2, 3, 4, 6, 6 }
        Private Shared rnd As New Random()

        Private Shared Function CreateResource(ByVal i As Integer) As ResourceData
            Dim resource As New ResourceData()
            resource.Caption = "Resource " & (i + 1)
            resource.Id = i
            Return resource
        End Function

        Private Shared Function CreateAppointment(ByVal i As Integer, ByVal startDate As Date, ByVal duration As TimeSpan, ByVal resourceCount As Integer) As AppointmentData
            Dim start As Date = startDate.Add(New TimeSpan(rnd.Next(0, 23), rnd.Next(0, 60), 0))
            Dim res As New AppointmentData()
            res.Id = i
            res.StartTime = start
            res.EndTime = start.Add(duration)
            res.Subject = "Apt #" & (i + 1).ToString()
            res.Location = "Location " & (i + 1).ToString()
            res.Description = "Appointment Description " & (i + 1)
            res.LabelKey = Labels(rnd.Next(0, Labels.Count()))
            res.ResourceId = rnd.Next(0, resourceCount)
            res.StatusKey = Statuses(rnd.Next(0, Statuses.Count()))
            Return res
        End Function


        Private appointmentsPerDay_Renamed As Integer
        Private useAllDayAppointments As Boolean

        Private startDate As Date
        Private endDate As Date
        Public Sub New()
            Me.New(Date.Today, False)
        End Sub
        Public Sub New(ByVal startDate As Date, ByVal useAllDayAppointments As Boolean)
            Appointments = New ObservableCollection(Of AppointmentData)()
            Resources = New ObservableCollection(Of ResourceData)()
            Me.startDate = startDate
            Me.endDate = startDate
            Me.appointmentsPerDay_Renamed = 0
            Me.useAllDayAppointments = useAllDayAppointments
        End Sub

        Private privateAppointments As ObservableCollection(Of AppointmentData)
        Public Property Appointments() As ObservableCollection(Of AppointmentData)
            Get
                Return privateAppointments
            End Get
            Private Set(ByVal value As ObservableCollection(Of AppointmentData))
                privateAppointments = value
            End Set
        End Property
        Private privateResources As ObservableCollection(Of ResourceData)
        Public Property Resources() As ObservableCollection(Of ResourceData)
            Get
                Return privateResources
            End Get
            Private Set(ByVal value As ObservableCollection(Of ResourceData))
                privateResources = value
            End Set
        End Property
        Private privateDayCount As Integer
        Public Property DayCount() As Integer
            Get
                Return privateDayCount
            End Get
            Private Set(ByVal value As Integer)
                privateDayCount = value
            End Set
        End Property
        Private privateResourceCount As Integer
        Public Property ResourceCount() As Integer
            Get
                Return privateResourceCount
            End Get
            Private Set(ByVal value As Integer)
                privateResourceCount = value
            End Set
        End Property
        Private privateAppointmentsPerDay As Integer
        Public Property AppointmentsPerDay() As Integer
            Get
                Return privateAppointmentsPerDay
            End Get
            Private Set(ByVal value As Integer)
                privateAppointmentsPerDay = value
            End Set
        End Property

        Public Sub Clear()
            Appointments.Clear()
            Resources.Clear()
            Me.endDate = Me.startDate
            Me.appointmentsPerDay_Renamed = 0
        End Sub

        Public Sub SetUp(ByVal dayCount As Integer, ByVal resourceCount As Integer, ByVal appointmentsPerDay As Integer)
            If dayCount <= 0 OrElse resourceCount <= 0 OrElse appointmentsPerDay <= 0 Then
                Return
            End If
            If Me.DayCount = dayCount AndAlso Me.ResourceCount = resourceCount AndAlso Me.AppointmentsPerDay = appointmentsPerDay Then
                Return
            End If
            Me.DayCount = dayCount
            Me.ResourceCount = resourceCount
            Me.AppointmentsPerDay = appointmentsPerDay
            Dim appointmentsPerDayChanged As Boolean = Me.appointmentsPerDay_Renamed <> appointmentsPerDay
            Dim resourcesUpdated As Boolean = UpdateResources(resourceCount)
            If appointmentsPerDayChanged Then
                Me.appointmentsPerDay_Renamed = appointmentsPerDay
                Appointments.Clear()
                Me.endDate = Me.startDate
                UpdateDayCount(dayCount)
                Return
            End If
            UpdateDayCount(dayCount)
            If resourcesUpdated Then
                UpdateAppointmentResources()
            End If
        End Sub

        Private Function UpdateResources(ByVal newResourceCount As Integer) As Boolean
            If newResourceCount = Resources.Count Then
                Return False
            End If
            Dim oldResourceCount As Integer = Resources.Count()
            For i As Integer = 0 To (oldResourceCount - newResourceCount) - 1
                Resources.RemoveAt(Resources.Count - 1)
            Next i
            For i As Integer = 0 To (newResourceCount - oldResourceCount) - 1
                Resources.Add(CreateResource(Resources.Count))
            Next i
            Return True
        End Function

        Private Function UpdateDayCount(ByVal newDayCount As Integer) As Boolean
            Dim newEndDate As Date = Me.startDate.AddDays(newDayCount)
            If newEndDate.Equals(Me.endDate) Then
                Return False
            End If
            Dim [date] As Date = Me.endDate
            Do While [date] > newEndDate
                For i As Integer = 0 To Me.appointmentsPerDay_Renamed - 1
                    Appointments.RemoveAt(Appointments.Count - 1)
                Next i
                [date] = [date].Subtract(TimeSpan.FromDays(1))
            Loop
            [date] = Me.endDate
            Do While [date] < newEndDate
                For i As Integer = 0 To Me.appointmentsPerDay_Renamed - 1
                    Appointments.Add(CreateAppointment(Appointments.Count - 1, [date], CalculateDuration(i), Resources.Count))
                Next i
                [date] = [date].Add(TimeSpan.FromDays(1))
            Loop
            Me.endDate = newEndDate
            Return True
        End Function

        Private Function CalculateDuration(ByVal i As Integer) As TimeSpan
            If Me.useAllDayAppointments Then
                Return If(i Mod 10 = 0, TimeSpan.FromDays(rnd.Next(1, 5)), TimeSpan.FromMinutes(rnd.Next(1, 20) * 10))
            End If
            Return TimeSpan.FromMinutes(rnd.Next(1, 8) * 10)
        End Function

        Private Sub UpdateAppointmentResources()
            For Each apt As AppointmentData In Appointments
                apt.ResourceId = rnd.Next(0, Resources.Count)
            Next apt
        End Sub
    End Class

    Public Class AppointmentData
        Inherits BindableBase

        Private Shared ResourceIdName As String = GetPropertyName(Function() DirectCast(Nothing, AppointmentData).ResourceId)


        Private resourceId_Renamed As Integer
        Public Sub New()
        End Sub

        Public Property StartTime() As Date
        Public Property EndTime() As Date
        Public Property Subject() As String
        Public Property Location() As String
        Public Property Description() As String
        Public Property TimeZoneId() As String
        Public Property Id() As Integer
        Public Property LabelKey() As Object
        Public Property StatusKey() As Object
        Public Property AllDay() As Boolean
        Public Property ReminderInfo() As String
        Public Property Type() As Integer?
        Public Property RecurrenceInfo() As String
        Public Property ResourceId() As Integer
            Get
                Return Me.resourceId_Renamed
            End Get
            Set(ByVal value As Integer)
                SetProperty(Me.resourceId_Renamed, value, ResourceIdName)
            End Set
        End Property
    End Class
    Public Class ResourceData
        Public Sub New()
        End Sub

        Public Property Caption() As String
        Public Property Id() As Integer
    End Class
End Namespace
