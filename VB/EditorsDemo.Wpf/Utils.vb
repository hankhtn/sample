Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Markup
Imports System.Windows.Data
Imports System.Collections
Imports System.ComponentModel
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler
Imports System.IO
Imports System.Xml.Serialization
Imports System.Windows.Media
Imports DevExpress.Xpf.Editors
Imports System.Collections.Generic
Imports GridDemo
Imports DevExpress.Mvvm.UI.Interactivity
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Diagnostics
Imports System.Reflection

Namespace EditorsDemo.Utils
    Public Class FormatWrapper
        Public Sub New()
        End Sub
        Public Sub New(ByVal name As String, ByVal format As String)
            FormatName = name
            FormatString = format
        End Sub
        Public Property FormatName() As String
        Public Property FormatString() As String
    End Class
    Public Class BaseKindHelper(Of T)
        Public Function GetEnumMemberList() As Array
            Return System.Enum.GetValues(GetType(T))
        End Function
    End Class
    Public Class ClickModeKindHelper
        Inherits BaseKindHelper(Of ClickMode)

    End Class
    Public Class TextWrappingKindHelper
        Inherits BaseKindHelper(Of TextWrapping)

    End Class
    Public Class ScrollBarVisibilityKindHelper
        Inherits BaseKindHelper(Of System.Windows.Controls.ScrollBarVisibility)

    End Class
    Public Class CharacterCasingKindHelper
        Inherits BaseKindHelper(Of CharacterCasing)

    End Class
    Public Class NullableToStringConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        Private nullString As String = "Null"
        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return nullString
            End If
            Return value.ToString()
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
    Public Class DecimalToConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim target As Type = TryCast(parameter, Type)
            If target Is Nothing Then
                Return value
            End If
            Return System.Convert.ChangeType(value, target, culture)
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return System.Convert.ToDecimal(value)
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public Class IConvertibleConverter
        Implements IValueConverter

        Public Property ToType() As String
        Public Property FromType() As String

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim target As Type = Type.GetType(ToType, False)
            If target Is Nothing Then
                Return value
            End If
            Return System.Convert.ChangeType(value, target, culture)
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim target As Type = Type.GetType(FromType, False)
            If target Is Nothing Then
                Return value
            End If
            Return System.Convert.ChangeType(value, target, culture)
        End Function

        #End Region
    End Class

    Public NotInheritable Class SchedulerDataHelper

        Private Sub New()
        End Sub
        Private Const ResourcePathData As String = "EditorsDemo.Data"

        Public Shared Sub LoadTo(ByVal scheduler As SchedulerControl)
            Dim storage As SchedulerStorage = scheduler.Storage

            InitCustomAppointmentStatuses(storage)

            storage.AppointmentStorage.Mappings.AllDay = "AllDay"
            storage.AppointmentStorage.Mappings.Description = "Description"
            storage.AppointmentStorage.Mappings.End = "EndTime"
            storage.AppointmentStorage.Mappings.Label = "Label"
            storage.AppointmentStorage.Mappings.Location = "Location"
            storage.AppointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo"
            storage.AppointmentStorage.Mappings.ReminderInfo = "ReminderInfo"
            storage.AppointmentStorage.Mappings.Start = "StartTime"
            storage.AppointmentStorage.Mappings.Status = "Status"
            storage.AppointmentStorage.Mappings.Subject = "Subject"
            storage.AppointmentStorage.Mappings.Type = "Type"

            scheduler.Storage.AppointmentStorage.DataSource = LoadFromXml(Of EventItem)("Events.xml")
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

        Private Shared Function LoadFromXml(Of T As New)(ByVal fileName As String) As IBindingList
            Dim eventList As New SchedulerBindingList(Of T)()
            Using stream As Stream = GetDataStream(fileName)
                Dim s As New XmlSerializer(GetType(SchedulerBindingList(Of T)))
                eventList = DirectCast(s.Deserialize(stream), SchedulerBindingList(Of T))
                stream.Close()
            End Using
            Return eventList
        End Function
        Private Shared Function GetDataStream(ByVal fileName As String) As Stream
            Return GetResourceStream(ResourcePathData, fileName)
        End Function
        Private Shared Function GetResourceStream(ByVal resourcePath As String, ByVal resourceName As String) As Stream
            Dim fullResourceName As String = String.Format("{0}.{1}", resourcePath, resourceName)
            Dim result As Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(fullResourceName)
            If result IsNot Nothing Then
                Return result
            End If
            Return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
        End Function
    End Class
    #Region "SchedulerBindingList"
    Public Class SchedulerBindingList(Of T As New)
        Inherits CollectionBase
        Implements IBindingList

        Default Public ReadOnly Property Item(ByVal idx As Integer) As T
            Get
                Return DirectCast(MyBase.List(idx), T)
            End Get
        End Property

        Public Sub Add(ByVal appointment As T)
            MyBase.List.Add(appointment)
        End Sub
        Public Overridable Function AddNew() As Object Implements IBindingList.AddNew
            Dim newItem As New T()
            List.Add(newItem)
            Return newItem
        End Function
        Public ReadOnly Property AllowEdit() As Boolean Implements IBindingList.AllowEdit
            Get
                Return True
            End Get
        End Property
        Public ReadOnly Property AllowNew() As Boolean Implements IBindingList.AllowNew
            Get
                Return True
            End Get
        End Property
        Public ReadOnly Property AllowRemove() As Boolean Implements IBindingList.AllowRemove
            Get
                Return True
            End Get
        End Property

        Private listChangedHandler As ListChangedEventHandler
        Public Custom Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged
            AddHandler(ByVal value As ListChangedEventHandler)
                listChangedHandler = DirectCast(System.Delegate.Combine(listChangedHandler, value), ListChangedEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As ListChangedEventHandler)
                listChangedHandler = DirectCast(System.Delegate.Remove(listChangedHandler, value), ListChangedEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
                If listChangedHandler IsNot Nothing Then
                    For Each d As ListChangedEventHandler In listChangedHandler.GetInvocationList()
                        d.Invoke(sender, e)
                    Next d
                End If
            End RaiseEvent
        End Event

        Public Sub AddIndex(ByVal pd As PropertyDescriptor) Implements IBindingList.AddIndex
            Throw New NotSupportedException()
        End Sub
        Public Sub ApplySort(ByVal pd As PropertyDescriptor, ByVal dir As ListSortDirection) Implements IBindingList.ApplySort
            Throw New NotSupportedException()
        End Sub
        Public Function Find(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer Implements IBindingList.Find
            Throw New NotSupportedException()
        End Function
        Public ReadOnly Property IsSorted() As Boolean Implements IBindingList.IsSorted
            Get
                Return False
            End Get
        End Property
        Public Sub RemoveIndex(ByVal pd As PropertyDescriptor) Implements IBindingList.RemoveIndex
            Throw New NotSupportedException()
        End Sub
        Public Sub RemoveSort() Implements IBindingList.RemoveSort
            Throw New NotSupportedException()
        End Sub
        Public ReadOnly Property SortDirection() As ListSortDirection Implements IBindingList.SortDirection
            Get
                Throw New NotSupportedException()
            End Get
        End Property
        Public ReadOnly Property SortProperty() As PropertyDescriptor Implements IBindingList.SortProperty
            Get
                Throw New NotSupportedException()
            End Get
        End Property
        Public ReadOnly Property SupportsChangeNotification() As Boolean Implements IBindingList.SupportsChangeNotification
            Get
                Return True
            End Get
        End Property
        Public ReadOnly Property SupportsSearching() As Boolean Implements IBindingList.SupportsSearching
            Get
                Return False
            End Get
        End Property
        Public ReadOnly Property SupportsSorting() As Boolean Implements IBindingList.SupportsSorting
            Get
                Return False
            End Get
        End Property
    End Class
    #End Region
    #Region "EventItem"
    Public Class EventItem
        Public Property Type() As Object
        Public Property StartTime() As Object
        Public Property EndTime() As Object
        Public Property Description() As String
        Public Property AllDay() As Boolean
        Public Property Label() As Integer
        Public Property Location() As String
        Public Property ResourceId() As Object
        Public Property Status() As Integer
        Public Property Subject() As String
        Public Property Price() As Object
        Public Property RecurrenceInfo() As String
        Public Property ReminderInfo() As String

        Public Sub New()
            Type = CInt((AppointmentType.Normal))
            StartTime = Date.MinValue
            EndTime = Date.MinValue + DevExpress.XtraScheduler.Native.DateTimeHelper.HalfHourSpan
            Description = String.Empty
            AllDay = False
            Label = 0
            Location = String.Empty
            ResourceId = EmptyResourceId.Id
            Status = CInt((AppointmentStatusType.Free))
            Subject = String.Empty
            Price = 0.0
            RecurrenceInfo = String.Empty
            ReminderInfo = String.Empty
        End Sub
    End Class
    #End Region

    Public Class CategoryDataToImageSourceConverter
        Inherits BytesToImageSourceConverter

        Private Shared cachedImages As New Dictionary(Of String, Object)()

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            Dim categoryData As CategoryData = TryCast(value, CategoryData)
            If value Is Nothing Then
                Return Nothing
            End If
            If cachedImages.ContainsKey(categoryData.Name) Then
                Return cachedImages(categoryData.Name)
            Else
                Dim image As Object = MyBase.Convert(categoryData.Picture, targetType, parameter, culture)
                cachedImages.Add(categoryData.Name, image)
                Return image
            End If
        End Function
    End Class
    Public Class HyperLinkAttachedBehavior
        Inherits Behavior(Of Hyperlink)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.RequestNavigate, AddressOf OnRequestNavigate
        End Sub
        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.RequestNavigate, AddressOf OnRequestNavigate
            MyBase.OnDetaching()
        End Sub
        Private Sub OnRequestNavigate(ByVal sender As Object, ByVal e As RequestNavigateEventArgs)
            Process.Start(New ProcessStartInfo(e.Uri.AbsoluteUri))
            e.Handled = True
        End Sub
    End Class
    Public Class IssueStatusImageConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Dim name As String = DirectCast(value, String).Replace(" ", "")
            Dim path As String = "EditorsDemo.Images.IssueIcons." & name & ".png"
            Return DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromEmbeddedResource(System.Reflection.Assembly.GetExecutingAssembly(), path)
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class IdToUriConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing Then
                Return "http://devexpress.com/Support/Center/p/" & value.ToString() & ".aspx"
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
End Namespace
