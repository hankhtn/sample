Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Xml
Imports DevExpress.DemoData.Models
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Scheduler.Reporting
Imports DevExpress.Xpf.Scheduler.Reporting.UI

Namespace SchedulerDemo
    Public Class DemoViewTypeListItem
        Inherits BindableBase

        Private captionCore As String
        Private viewTypeCore As SchedulerViewType

        Public Property Caption() As String
            Get
                Return captionCore
            End Get
            Set(ByVal value As String)
                SetProperty(captionCore, value, "Caption")
            End Set
        End Property
        Public Property ViewType() As SchedulerViewType
            Get
                Return viewTypeCore
            End Get
            Set(ByVal value As SchedulerViewType)
                SetProperty(viewTypeCore, value, "ViewType")
            End Set
        End Property
    End Class
    Public Class DemoGroupingListItem
        Inherits BindableBase

        Private captionCore As String
        Private groupTypeCore As SchedulerGroupType

        Public Property Caption() As String
            Get
                Return captionCore
            End Get
            Set(ByVal value As String)
                SetProperty(captionCore, value, "Caption")
            End Set
        End Property
        Public Property GroupType() As SchedulerGroupType
            Get
                Return groupTypeCore
            End Get
            Set(ByVal value As SchedulerGroupType)
                SetProperty(groupTypeCore, value, "GroupType")
            End Set
        End Property
    End Class
    #Region "SchedulerReportTemplateInfo"
    Public Class SchedulerReportTemplateInfo
        Implements ISchedulerReportTemplateInfo

        Private displayNameCore As String
        Private templatePath As String
        Public Sub New(ByVal displayName As String, ByVal templatePath As String)
            Me.displayNameCore = displayName
            Me.templatePath = templatePath
        End Sub

        Public ReadOnly Property DisplayName() As String
            Get
                Return displayNameCore
            End Get
        End Property
        Public ReadOnly Property ReportTemplatePath() As String
            Get
                Return templatePath
            End Get
        End Property
        Private ReadOnly Property ISchedulerReportTemplateInfo_DisplayName() As String Implements ISchedulerReportTemplateInfo.DisplayName
            Get
                Return displayNameCore
            End Get
        End Property
        Private ReadOnly Property ISchedulerReportTemplateInfo_ReportTemplatePath() As String Implements ISchedulerReportTemplateInfo.ReportTemplatePath
            Get
                Return templatePath
            End Get
        End Property
    End Class
    #End Region
    Public Class SchedulerDataHelper
        Private Shared staticContext As New CarsContext()
        Public Const DefaultAppointmentXmlSource As String = "Appointments.xml"
        Public Const DefaultResourceXmlSource As String = "Resources.xml"
        Public Const EventsXmlSource As String = "Events.xml"
        Public Const CarsXmlSource As String = "Cars.xml"

        Public Shared Function GetEventsXmlDataStream() As Stream
            Return GetResourceStream(EventsXmlSource)
        End Function
        Public Shared Function GetCarsXmlDataStream() As Stream
            Return GetResourceStream(CarsXmlSource)
        End Function
        Public Shared Sub DataBindToDatabase(ByVal scheduler As SchedulerControl)
            Dim context As New CarsContext()
            InitCustomAppointmentStatuses(scheduler.Storage)

            scheduler.Storage.ResourceStorage.Mappings.Caption = "Model"
            scheduler.Storage.ResourceStorage.Mappings.Id = "ID"
            scheduler.Storage.ResourceStorage.Mappings.Image = "Picture"
            scheduler.Storage.ResourceStorage.DataSource = context.Cars.ToList()

            scheduler.Storage.AppointmentStorage.Mappings.AllDay = "AllDay"
            scheduler.Storage.AppointmentStorage.Mappings.Description = "Description"
            scheduler.Storage.AppointmentStorage.Mappings.End = "EndTime"
            scheduler.Storage.AppointmentStorage.Mappings.Label = "Label"
            scheduler.Storage.AppointmentStorage.Mappings.Location = "Location"
            scheduler.Storage.AppointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo"
            scheduler.Storage.AppointmentStorage.Mappings.ReminderInfo = "ReminderInfo"
            scheduler.Storage.AppointmentStorage.Mappings.ResourceId = "CarId"
            scheduler.Storage.AppointmentStorage.Mappings.Start = "StartTime"
            scheduler.Storage.AppointmentStorage.Mappings.Status = "Status"
            scheduler.Storage.AppointmentStorage.Mappings.Subject = "Subject"
            scheduler.Storage.AppointmentStorage.Mappings.Type = "EventType"
            scheduler.Storage.AppointmentStorage.DataSource = context.CarSchedule.ToList()
            AddHandler scheduler.InitNewAppointment, AddressOf OnSchedulerInitNewAppointment
        End Sub
        Private Shared Sub OnSchedulerInitNewAppointment(ByVal sender As Object, ByVal e As AppointmentEventArgs)
            e.Appointment.StatusKey = 0
        End Sub
        Public Shared Sub DataBind(ByVal scheduler As SchedulerControl)
            DataBind(scheduler.Storage)
            AddHandler scheduler.InitNewAppointment, AddressOf OnSchedulerInitNewAppointment
        End Sub
        Public Shared Sub DataBind(ByVal storage As SchedulerStorage)
            InitCustomAppointmentStatuses(storage)
            FillStorageData(storage)
        End Sub
        Private Shared Sub InitCustomAppointmentStatuses(ByVal storage As SchedulerStorage)
            storage.BeginUpdate()
            Try
                Dim statuses As IAppointmentStatusStorage = storage.AppointmentStorage.Statuses
                statuses.Clear()
                statuses.Add(CreateNewAppointmentStatus(statuses, AppointmentStatusType.Free, Colors.White, "Free", "Free"))

                statuses.Add(CreateNewAppointmentStatus(statuses, AppointmentStatusType.Custom, Colors.SkyBlue, "Wash", "Wash"))
                statuses.Add(CreateNewAppointmentStatus(statuses, AppointmentStatusType.Custom, Colors.SteelBlue, "Maintenance", "Maintenance"))
                statuses.Add(CreateNewAppointmentStatus(statuses, AppointmentStatusType.Custom, Colors.YellowGreen, "Rent", "Rent"))
                statuses.Add(CreateNewAppointmentStatus(statuses, AppointmentStatusType.Custom, Colors.Coral, "CheckUp", "CheckUp"))
            Finally
                storage.EndUpdate()
            End Try

        End Sub
        Private Shared Function CreateNewAppointmentStatus(ByVal storage As IAppointmentStatusStorage, ByVal type As AppointmentStatusType, ByVal color As Color, ByVal name As String, ByVal caption As String) As IAppointmentStatus
            Dim status As IAppointmentStatus = storage.CreateNewStatus(Nothing, name, caption)
            status.Type = type
            status.SetBrush(New SolidColorBrush(color))
            Return status
        End Function

        Private Shared Sub CreateStorageMappings(ByVal storage As SchedulerStorage)

            storage.ResourceStorage.Mappings.Caption = "Caption"
            storage.ResourceStorage.Mappings.Id = "Id"
            storage.ResourceStorage.Mappings.Image = "Picture"


            storage.AppointmentStorage.Mappings.AllDay = "AllDay"
            storage.AppointmentStorage.Mappings.Description = "Description"
            storage.AppointmentStorage.Mappings.End = "EndTime"
            storage.AppointmentStorage.Mappings.Label = "Label"
            storage.AppointmentStorage.Mappings.Location = "Location"
            storage.AppointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo"
            storage.AppointmentStorage.Mappings.ReminderInfo = "ReminderInfo"
            storage.AppointmentStorage.Mappings.ResourceId = "ResourceId"
            storage.AppointmentStorage.Mappings.Start = "StartTime"
            storage.AppointmentStorage.Mappings.Status = "Status"
            storage.AppointmentStorage.Mappings.Subject = "Subject"
            storage.AppointmentStorage.Mappings.Type = "Type"
        End Sub
        Public Shared Sub FillStorageData(ByVal storage As SchedulerStorage)
            CreateStorageMappings(storage)
            storage.AppointmentStorage.DataSource = SchedulerDemo.Internal.SchedulerXmlHelper.LoadFromXml(Of SchedulerDemo.Internal.EventItem)(SchedulerDataHelper.EventsXmlSource)
            storage.ResourceStorage.DataSource = SchedulerDemo.Internal.SchedulerXmlHelper.LoadFromXml(Of SchedulerDemo.Internal.CarItem)(SchedulerDataHelper.CarsXmlSource)
        End Sub
        Public Shared Function LoadXmlDocumentFromResource(ByVal resourceName As String) As XmlDocument
            Dim doc As New XmlDocument()
            doc.Load(GetResourceStream(resourceName))
            Return doc
        End Function
        Private Shared Sub FillStorageCollection(ByVal c As AppointmentCollection, ByVal resourceName As String)
            Using stream As Stream = GetResourceStream(resourceName)
                c.ReadXml(stream)
                stream.Close()
            End Using
        End Sub
        Private Shared Sub FillStorageCollection(ByVal c As ResourceCollection, ByVal resourceName As String)
            Using stream As Stream = GetResourceStream(resourceName)
                c.ReadXml(stream)
                stream.Close()
            End Using
        End Sub
        Private Shared Function GetResourceStream(ByVal resourceName As String) As Stream
            Dim result As Stream = GetResourceStreamCore(GetFullResourceName(resourceName))
            If result Is Nothing Then
                result = GetResourceStreamCore(resourceName)
            End If
            Return result
        End Function
        Private Shared Function GetFullResourceName(ByVal resourceName As String) As String
            Return String.Format("{0}.{1}", "SchedulerDemo.Data", resourceName)
        End Function
        Public Shared Function GetImageFromResource(ByVal imageName As String) As BitmapImage
            Dim executingAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim result As BitmapImage = DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromEmbeddedResource(executingAssembly, imageName)
            If result Is Nothing Then
                result = DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromEmbeddedResource(executingAssembly, GetFullImageResourceName(imageName))
            End If
            Return result
        End Function
        Private Shared Function GetFullImageResourceName(ByVal imageResourceName As String) As String
            Return String.Format("SchedulerDemo.Data.Images.{0}", imageResourceName)
        End Function
        Private Shared Function GetResourceStreamCore(ByVal resourceName As String) As Stream
            Return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
        End Function
        Private Shared Function GetStatusColor(ByVal rowIndex As Integer) As System.Drawing.Color
            Dim statusColor As Integer = staticContext.UsageTypes.ElementAt(rowIndex).Color
            Return System.Drawing.Color.FromArgb(&HFF, (statusColor And &HFF0000) >> 16, (statusColor And &HFF00) >> 8, statusColor And &HFF)
        End Function
        Public Shared Sub DataUnbind(ByVal control As SchedulerControl)
            control.Storage.ResourceStorage.DataSource = Nothing
            control.Storage.AppointmentStorage.DataSource = Nothing
        End Sub
        Public Shared Function GetReportTemplateCollection() As List(Of ISchedulerReportTemplateInfo)
            Dim reportTemplateCollection As New List(Of ISchedulerReportTemplateInfo)()
            Dim reportTemplatesDirectory As String = DataFilesHelper.FindFile("SchedulerReportTemplates", DataFilesHelper.DataPath)
            Dim directoryInfo As New DirectoryInfo(reportTemplatesDirectory)
            For Each fileInfo As FileInfo In directoryInfo.GetFiles()
                reportTemplateCollection.Add(New SchedulerReportTemplateInfo(fileInfo.Name, fileInfo.FullName))
            Next fileInfo
            Return reportTemplateCollection
        End Function
    End Class

    Public Class SchedulerDemoUtils
        Public Shared Sub UpdateSchedulerWorkDays(ByVal scheduler As SchedulerControl, ByVal weekDays As WeekDays)
            Dim workDays As WorkDaysCollection = scheduler.WorkDays
            workDays.BeginUpdate()
            Try
                workDays.Clear()
                workDays.Add(New WeekDaysWorkDay(weekDays))
            Finally
                workDays.EndUpdate()
            End Try
        End Sub
        Public Shared Function AddWeekDay(ByVal workDays As WeekDays, ByVal day As WeekDays) As WeekDays
            Return workDays Or day
        End Function
        Public Shared Function RemoveWeekDay(ByVal workDays As WeekDays, ByVal day As WeekDays) As WeekDays
            Return workDays And Not day
        End Function
    End Class
    Public Class UsedAppointmentTypeToBoolConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim type As UsedAppointmentType = DirectCast(value, UsedAppointmentType)
            Return If(type.Equals(UsedAppointmentType.All), True, False)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(DirectCast(value, Boolean), UsedAppointmentType.All, UsedAppointmentType.None)
        End Function
    End Class
    Public Class AppointmentConflictsModeToBoolConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim mode As AppointmentConflictsMode = DirectCast(value, AppointmentConflictsMode)
            Return If(mode.Equals(AppointmentConflictsMode.Allowed), True, False)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(DirectCast(value, Boolean), AppointmentConflictsMode.Allowed, AppointmentConflictsMode.Forbidden)
        End Function
    End Class
    Public Class BitmapToBitmapSourceConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim source As System.Drawing.Bitmap = TryCast(value, System.Drawing.Bitmap)
            If source Is Nothing OrElse targetType IsNot GetType(ImageSource) Then
                Return Nothing
            End If
            Return DemoUtils.ConvertBitmapToBitmapImage(source)


        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class DateTimeToShortDateStringConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Dim dateTimeValue As Date = DirectCast(value, Date)

            Dim param As String = If(parameter IsNot Nothing, parameter.ToString(), String.Empty)
            If param = String.Empty Then
                param = "MM/dd"
            End If

            Return dateTimeValue.ToString(param)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class














    Public Class LengthToCenterConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is Double) OrElse targetType IsNot GetType(Double) Then
                Return Nothing
            End If
            Return DirectCast(value, Double) / 2
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If Not(TypeOf value Is Double) OrElse targetType IsNot GetType(Double) Then
                Return Nothing
            End If
            Return DirectCast(value, Double) * 2
        End Function
        #End Region
    End Class
    Public Class TimeIntervalToStringConverter
        Implements IValueConverter

        Public Const DefaultFormat As String = "MMMM, dd yyyy"

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim interval As TimeInterval = TryCast(value, TimeInterval)
            If interval Is Nothing OrElse targetType IsNot GetType(String) Then
                Return Nothing
            End If
            Dim format As String = TryCast(parameter, String)
            Dim actualFormat As String = If(format IsNot Nothing, format, DefaultFormat)
            If interval.Duration > TimeSpan.FromDays(1) Then
                Dim start As String = interval.Start.ToString(actualFormat)
                Dim [end] As String = interval.End.ToString(actualFormat)
                Return String.Format("{0} - {1}", start, [end])
            End If
            Return interval.Start.ToString(actualFormat)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If Not(TypeOf value Is String) OrElse targetType IsNot GetType(TimeInterval) Then
                Return Nothing
            End If
            Dim dateTimes() As String = DirectCast(value, String).Split("-"c)
            If dateTimes Is Nothing OrElse dateTimes.Length = 0 Then
                Return Nothing
            End If
            If dateTimes.Length > 1 Then
                Dim start As Date = Nothing
                Dim [end] As Date = Nothing
                If Date.TryParse(dateTimes(0), start) AndAlso Date.TryParse(dateTimes(1), [end]) Then
                    Return New TimeInterval(start, [end])
                End If
            End If
            Dim dt As Date = Nothing
            If Date.TryParse(dateTimes(0), dt) Then
                Return New TimeInterval(dt, dt)
            End If
            Return Nothing
        End Function
        #End Region
    End Class
    Public Class WeekDaysToBooleanConverter
        Implements IValueConverter

        Public Shared Function GetDay(ByVal day As String) As WeekDays
            Dim result As WeekDays = Nothing
            If Not System.Enum.TryParse(Of WeekDays)(day, result) Then
                Return CType(0, WeekDays)
            End If
            Return result
        End Function

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim workDays As WorkDaysCollection = TryCast(value, WorkDaysCollection)
            Dim day As String = TryCast(parameter, String)
            If workDays Is Nothing OrElse day Is Nothing Then
                Return Nothing
            End If
            Dim weekDay As WeekDays = GetDay(day)
            If CInt((weekDay)) = 0 Then
                Return Nothing
            End If
            Return (workDays.GetWeekDays() And weekDay) <> 0
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class TimelineViewActiveConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim viewType As SchedulerViewType = DirectCast(value, SchedulerViewType)
            If viewType.Equals(SchedulerViewType.Timeline) Then
                Return Visibility.Visible
            End If

            Return Visibility.Collapsed
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class
    Public MustInherit Class DemoNamedImageConverter
        Implements IValueConverter

        Private Const resourcePath As String = "SchedulerDemo.Data.Images"

        Protected Shared Function CreateImage(ByVal name As String) As BitmapImage
            Dim imagePath As String = String.Format("{0}.png", name)
            Dim stream As Stream = DemoUtils.GetResourceStream(resourcePath, imagePath)
            Debug.Assert(stream IsNot Nothing)
            Return ImageHelper.CreateImageFromStream(stream)
        End Function

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is Integer) OrElse targetType IsNot GetType(ImageSource) Then
                Return Nothing
            End If
            Dim image As BitmapImage = Nothing
            If TryGetImageById(DirectCast(value, Integer), image) Then
                Return image
            End If
            Return Nothing
        End Function
        Protected MustOverride Function TryGetImageById(ByVal id As Integer, <System.Runtime.InteropServices.Out()> ByRef image As BitmapImage) As Boolean

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        #End Region
    End Class

    Public Class AppointmentStatusIdToImageConverter
        Inherits DemoNamedImageConverter

        Private Shared imagesTable As Dictionary(Of Integer, BitmapImage) = CreateImagesTable()
        Private Shared Function CreateImagesTable() As Dictionary(Of Integer, BitmapImage)
            Dim result As New Dictionary(Of Integer, BitmapImage)()
            result.Add(0, CreateImage("Free"))
            result.Add(1, CreateImage("Wash"))
            result.Add(2, CreateImage("Maintance"))
            result.Add(3, CreateImage("Rent"))
            result.Add(4, CreateImage("CheckUp"))
            Return result
        End Function
        Public Sub New()
        End Sub
        Protected Overrides Function TryGetImageById(ByVal id As Integer, <System.Runtime.InteropServices.Out()> ByRef image As BitmapImage) As Boolean
            Return imagesTable.TryGetValue(id, image)
        End Function
    End Class
    Public Class AppointmentLabelIdToImageConverter
        Inherits DemoNamedImageConverter

        Private Shared imagesTable As Dictionary(Of Integer, BitmapImage) = CreateImagesTable()
        Private Shared Function CreateImagesTable() As Dictionary(Of Integer, BitmapImage)
            Dim result As New Dictionary(Of Integer, BitmapImage)()
            result.Add(0, CreateImage("Red"))
            result.Add(1, CreateImage("Blue"))
            result.Add(2, CreateImage("Yellow"))
            result.Add(3, CreateImage("Lilac"))
            result.Add(4, CreateImage("Beige"))
            Return result
        End Function
        Public Sub New()
        End Sub
        Protected Overrides Function TryGetImageById(ByVal id As Integer, <System.Runtime.InteropServices.Out()> ByRef image As BitmapImage) As Boolean
            Return imagesTable.TryGetValue(id, image)
        End Function
    End Class
    Public Class FilteredAppointmentTextColorConverter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse targetType IsNot GetType(Brush) Then
                Return Nothing
            End If
            Return If(DirectCast(value, Boolean), New SolidColorBrush(SystemColors.WindowTextColor), New SolidColorBrush(Colors.Gray))
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function

        #End Region

    End Class
    Public Class SchedulerResourceImageConverter
        Inherits DependencyObject
        Implements IValueConverter

        #Region "SchedulerControl"
        Public Property SchedulerControl() As SchedulerControl
            Get
                Return DirectCast(GetValue(SchedulerControlProperty), SchedulerControl)
            End Get
            Set(ByVal value As SchedulerControl)
                SetValue(SchedulerControlProperty, value)
            End Set
        End Property
        Public Shared ReadOnly SchedulerControlProperty As DependencyProperty = CreateSchedulerControlProperty()
        Private Shared Function CreateSchedulerControlProperty() As DependencyProperty
            Return DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty(Of SchedulerResourceImageConverter, SchedulerControl)("SchedulerControl", Nothing)
        End Function
        #End Region

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse targetType IsNot GetType(ImageSource) OrElse SchedulerControl Is Nothing Then
                Return Nothing
            End If
            If SchedulerControl.Storage Is Nothing Then
                Return Nothing
            End If
            Dim resourceId As Object = value

            Dim resource As Resource = SchedulerControl.Storage.ResourceStorage.GetResourceById(resourceId)
            If resource IsNot Nothing AndAlso resource.ImageBytes IsNot Nothing Then
                Return resource.GetImage()
            End If
            Return Nothing
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function

        #End Region
    End Class
    Public Class ObjectList
        Inherits List(Of Object)

    End Class
    Public Class ResourceNavigatorVisibilityTypes
        Inherits List(Of ResourceNavigatorVisibility)

    End Class
End Namespace
