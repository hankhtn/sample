Imports System
Imports System.Collections.ObjectModel
Imports System.Windows.Media
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Scheduling

Namespace SchedulingDemo
    Public Enum EventPriority
        None = 0
        HighImportance = 1
        LowImportance = 2
    End Enum
    Public Class OutlookInspiredData
        Public Sub New()
            CreateCategories()
            CreateCalendars()
            CreateEvents()
        End Sub

        Private privateCalendars As ObservableCollection(Of Calendar)
        Public Property Calendars() As ObservableCollection(Of Calendar)
            Get
                Return privateCalendars
            End Get
            Private Set(ByVal value As ObservableCollection(Of Calendar))
                privateCalendars = value
            End Set
        End Property
        Private privateCategories As ObservableCollection(Of Category)
        Public Property Categories() As ObservableCollection(Of Category)
            Get
                Return privateCategories
            End Get
            Private Set(ByVal value As ObservableCollection(Of Category))
                privateCategories = value
            End Set
        End Property
        Private privateEvents As ObservableCollection(Of OutlookEvent)
        Public Property Events() As ObservableCollection(Of OutlookEvent)
            Get
                Return privateEvents
            End Get
            Private Set(ByVal value As ObservableCollection(Of OutlookEvent))
                privateEvents = value
            End Set
        End Property

        Private ReadOnly Property NoneCategory() As Category
            Get
                Return Categories(0)
            End Get
        End Property
        Private ReadOnly Property BlueCategory() As Category
            Get
                Return Categories(4)
            End Get
        End Property
        Private ReadOnly Property VioletCategory() As Category
            Get
                Return Categories(6)
            End Get
        End Property
        Private ReadOnly Property GreenCategory() As Category
            Get
                Return Categories(5)
            End Get
        End Property

        Private Property MyCalendar() As Calendar
        Private Property Birthdays() As Calendar
        Private Property TeamCalendar() As Calendar

        Private Sub CreateCategories()
            Categories = New ObservableCollection(Of Category)()
            Categories.Add(Category.Create(0, "None", Color.FromRgb(188, 188, 188)))
            Categories.Add(Category.Create(1, "Yellow category", Color.FromRgb(255, 241, 0)))
            Categories.Add(Category.Create(2, "Red category", Color.FromRgb(240, 125, 136)))
            Categories.Add(Category.Create(3, "Orange category", Color.FromRgb(255, 140, 0)))
            Categories.Add(Category.Create(4, "Blue category", Color.FromRgb(85, 171, 229)))
            Categories.Add(Category.Create(5, "Green category", Color.FromRgb(95, 190, 125)))
            Categories.Add(Category.Create(6, "Violet category", Color.FromRgb(168, 149, 226)))
        End Sub
        Private Sub CreateCalendars()
            Calendars = New ObservableCollection(Of Calendar)()
            MyCalendar = Calendar.Create("My Calendars", "My Calendar")
            Birthdays = Calendar.Create("My Calendars", "Birthdays")
            TeamCalendar = Calendar.Create("Work Calendars", "Team Calendar")
            Calendars.Add(MyCalendar)
            Calendars.Add(Birthdays)
            Calendars.Add(TeamCalendar)
        End Sub
        Private Sub CreateEvents()
            Events = New ObservableCollection(Of OutlookEvent)()
            If ViewModelBase.IsInDesignMode Then
                Return
            End If
            For Each appt As TeamAppointment In TeamData.VacationAppointments
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id))
            Next appt
            For Each appt As TeamAppointment In TeamData.PhoneCallsAppointments
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id))
            Next appt
            For Each appt As TeamAppointment In TeamData.CarWashAppointments
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id))
            Next appt
            For Each appt As TeamAppointment In TeamData.TrainingAppointments
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id))
            Next appt
            For Each appt As TeamAppointment In TeamData.PayBillsAppointments
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id))
            Next appt
            For Each appt As TeamAppointment In TeamData.DentistAppointments
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id))
            Next appt
            For Each appt As TeamAppointment In TeamData.RestaurantAppointments
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id))
            Next appt

            For Each appt As TeamAppointment In TeamData.BirthdayAppointments
                Events.Add(CreateEvent(appt, Birthdays.Id, VioletCategory.Id))
            Next appt

            For Each appt As TeamAppointment In TeamData.ConferenceAppointments
                Events.Add(CreateEvent(appt, TeamCalendar.Id, GreenCategory.Id))
            Next appt
            For Each appt As TeamAppointment In TeamData.MeetingAppointments
                Events.Add(CreateEvent(appt, TeamCalendar.Id, GreenCategory.Id))
            Next appt
            For Each appt As TeamAppointment In TeamData.CompanyBirthdayAppointments
                Events.Add(CreateEvent(appt, TeamCalendar.Id, GreenCategory.Id))
            Next appt

            Dim id As Integer = 0
            For Each appt As OutlookEvent In Events
                appt.Id = id
                id += 1
            Next appt
        End Sub
        Private Function CreateEvent(ByVal appt As TeamAppointment, ByVal calendarId? As Integer, ByVal categoryId As Integer) As OutlookEvent

            Dim outlookEvent_Renamed As OutlookEvent = SchedulingDemo.OutlookEvent.Create()
            outlookEvent_Renamed.AllDay = appt.AllDay
            outlookEvent_Renamed.StartTime = appt.Start
            outlookEvent_Renamed.EndTime = appt.End
            outlookEvent_Renamed.CalendarId = calendarId
            outlookEvent_Renamed.Subject = appt.Subject
            outlookEvent_Renamed.Location = appt.Location
            outlookEvent_Renamed.StatusId = appt.Status
            outlookEvent_Renamed.CategorizeId = categoryId
            outlookEvent_Renamed.Type = appt.AppointmentType
            outlookEvent_Renamed.RecurrenceInfo = appt.RecurrenceInfo
            outlookEvent_Renamed.ReminderInfo = appt.ReminderInfo
            Return outlookEvent_Renamed
        End Function
    End Class

    Public Class Calendar

        Private Shared id_Renamed As Integer = 0
        Public Shared Function Create(ByVal group As String, ByVal caption As String) As Calendar
            Return ViewModelSource.Create(Function() New Calendar() With {.Group = group, .Caption = caption, .IsVisible = True})
        End Function
        Public Shared Function Create() As Calendar
            Return ViewModelSource.Create(Function() New Calendar())
        End Function
        Protected Sub New()
            Id = id_Renamed
            id_Renamed += 1
        End Sub

        Private privateId As Integer
        Public Property Id() As Integer
            Get
                Return privateId
            End Get
            Private Set(ByVal value As Integer)
                privateId = value
            End Set
        End Property
        Public Overridable Property Caption() As String
        Public Overridable Property Group() As String
        Public Overridable Property IsVisible() As Boolean
    End Class
    Public Class OutlookEvent
        Public Shared Function Create() As OutlookEvent
            Return ViewModelSource.Create(Function() New OutlookEvent())
        End Function
        Protected Sub New()
            Priority = EventPriority.None
        End Sub

        Public Overridable Property Id() As Integer
        Public Overridable Property AllDay() As Boolean
        Public Overridable Property StartTime() As Date
        Public Overridable Property EndTime() As Date
        Public Overridable Property CalendarId() As Integer?
        Public Overridable Property Subject() As String
        Public Overridable Property Location() As String
        Public Overridable Property Description() As String
        Public Overridable Property StatusId() As Integer
        Public Overridable Property CategorizeId() As Integer
        Public Overridable Property Type() As Integer
        Public Overridable Property RecurrenceInfo() As String
        Public Overridable Property ReminderInfo() As String
        Public Overridable Property TimeZoneId() As String
        Public Overridable Property IsPrivate() As Boolean
        Public Overridable Property Priority() As EventPriority
    End Class
    Public Class Category
        Public Shared Function Create(ByVal id As Integer, ByVal caption As String, ByVal color As Color) As Category
            Return ViewModelSource.Create(Function() New Category(id, caption, color))
        End Function
        Protected Sub New(ByVal id As Integer, ByVal caption As String, ByVal color As Color)
            Me.Id = id
            Me.Caption = caption
            Me.Color = color
        End Sub

        Public Overridable Property Id() As Integer
        Public Overridable Property Caption() As String
        Public Overridable Property Color() As Color
        Public Overridable Property IsChecked() As Boolean
    End Class
    Public Class CheckedCategory

        Public Shared Function Create(ByVal category_Renamed As AppointmentLabelItem, ByVal isChecked As Boolean) As CheckedCategory
            Return ViewModelSource.Create(Function() New CheckedCategory(category_Renamed, isChecked))
        End Function

        Protected Sub New(ByVal category_Renamed As AppointmentLabelItem, ByVal isChecked As Boolean)
            Me.Category = category_Renamed
            Me.IsChecked = isChecked
        End Sub

        Public Overridable Property Category() As AppointmentLabelItem
        Public Overridable Property IsChecked() As Boolean
    End Class
End Namespace
