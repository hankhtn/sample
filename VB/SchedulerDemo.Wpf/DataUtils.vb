Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler
Imports System.IO
Imports DevExpress.Xpf.Scheduler
Imports System.Windows.Media.Imaging
Imports System.Collections
Imports DevExpress.Utils.Serializing
Imports System.Xml
Imports DevExpress.XtraScheduler.Native
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports System.Reflection
Imports System.Windows
Imports DevExpress.XtraScheduler.Xml
Imports System.Xml.Serialization
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging

Namespace SchedulerDemo
    Public Class CustomEvent

        Private id_Renamed As Object
        Private start As Date
        Private [end] As Date

        Private subject_Renamed As String

        Private status_Renamed As Integer

        Private description_Renamed As String

        Private label_Renamed As Integer

        Private location_Renamed As String

        Private allday_Renamed As Boolean

        Private eventType_Renamed As Integer

        Private recurrenceInfo_Renamed As String

        Private reminderInfo_Renamed As String

        Private ownerId_Renamed As Object

        Private price_Renamed As Double

        Private contactInfo_Renamed As String

        Public Property StartTime() As Date
            Get
                Return start
            End Get
            Set(ByVal value As Date)
                start = value
            End Set
        End Property
        Public Property EndTime() As Date
            Get
                Return [end]
            End Get
            Set(ByVal value As Date)
                [end] = value
            End Set
        End Property
        Public Property Subject() As String
            Get
                Return subject_Renamed
            End Get
            Set(ByVal value As String)
                subject_Renamed = value
            End Set
        End Property
        Public Property Status() As Integer
            Get
                Return status_Renamed
            End Get
            Set(ByVal value As Integer)
                status_Renamed = value
            End Set
        End Property
        Public Property Description() As String
            Get
                Return description_Renamed
            End Get
            Set(ByVal value As String)
                description_Renamed = value
            End Set
        End Property
        Public Property Label() As Integer
            Get
                Return label_Renamed
            End Get
            Set(ByVal value As Integer)
                label_Renamed = value
            End Set
        End Property
        Public Property Location() As String
            Get
                Return location_Renamed
            End Get
            Set(ByVal value As String)
                location_Renamed = value
            End Set
        End Property
        Public Property AllDay() As Boolean
            Get
                Return allday_Renamed
            End Get
            Set(ByVal value As Boolean)
                allday_Renamed = value
            End Set
        End Property
        Public Property EventType() As Integer
            Get
                Return eventType_Renamed
            End Get
            Set(ByVal value As Integer)
                eventType_Renamed = value
            End Set
        End Property
        Public Property RecurrenceInfo() As String
            Get
                Return recurrenceInfo_Renamed
            End Get
            Set(ByVal value As String)
                recurrenceInfo_Renamed = value
            End Set
        End Property
        Public Property ReminderInfo() As String
            Get
                Return reminderInfo_Renamed
            End Get
            Set(ByVal value As String)
                reminderInfo_Renamed = value
            End Set
        End Property
        Public Property OwnerId() As Object
            Get
                Return ownerId_Renamed
            End Get
            Set(ByVal value As Object)
                ownerId_Renamed = value
            End Set
        End Property
        Public Property Id() As Object
            Get
                Return id_Renamed
            End Get
            Set(ByVal value As Object)
                id_Renamed = value
            End Set
        End Property
        Public Property Price() As Double
            Get
                Return price_Renamed
            End Get
            Set(ByVal value As Double)
                price_Renamed = value
            End Set
        End Property
        Public Property ContactInfo() As String
            Get
                Return contactInfo_Renamed
            End Get
            Set(ByVal value As String)
                contactInfo_Renamed = value
            End Set
        End Property
    End Class
    Public Class CustomResource

        Private id_Renamed As Object

        Private caption_Renamed As String

        Public Property Id() As Object
            Get
                Return id_Renamed
            End Get
            Set(ByVal value As Object)
                id_Renamed = value
            End Set
        End Property
        Public Property Caption() As String
            Get
                Return caption_Renamed
            End Get
            Set(ByVal value As String)
                caption_Renamed = value
            End Set
        End Property
    End Class

    Public Class SchedulerItemList(Of T As New)
        Inherits CollectionBase
        Implements IBindingList

        Default Public ReadOnly Property Item(ByVal idx As Integer) As T
            Get
                Return DirectCast(MyBase.List(idx), T)
            End Get
        End Property

        Public Sub Add(ByVal item As T)
            MyBase.List.Add(item)
        End Sub
        Public Function IndexOf(ByVal item As T) As Integer
            Return List.IndexOf(item)
        End Function
        Public Function AddNew() As Object Implements IBindingList.AddNew
            Dim app As New T()
            List.Add(app)
            Return app
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
        Friend Sub OnListChanged(ByVal args As ListChangedEventArgs)
            If listChangedHandler IsNot Nothing Then
                listChangedHandler(Me, args)
            End If
        End Sub
        Protected Overrides Sub OnClearComplete()
            MyBase.OnClearComplete()
            OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
        End Sub
        Protected Overrides Sub OnRemoveComplete(ByVal index As Integer, ByVal value As Object)
            OnListChanged(New ListChangedEventArgs(ListChangedType.ItemDeleted, index))
        End Sub
        Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
            OnListChanged(New ListChangedEventArgs(ListChangedType.ItemAdded, index))
        End Sub

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

    Public Class DemoUtils
        Private Const ResourcePathData As String = "SchedulerDemo.Data"
        Private Shared Users() As String = { "Peter Dolan", "Ryan Fischer", "Andrew Miller", "Tom Hamlett", "Jerry Campbell", "Carl Lucas", "Mark Hamilton", "Steve Lee" }
        Public Shared RandomInstance As New Random()
        Public Shared [Date] As New Date(2010, 7, 13)
        Public Shared ReadOnly ResourceList As New Dictionary(Of Integer, String)()
        Private Shared taskDescriptions() As String = { "Implementing Developer Express MasterView control into Accounting System.", "Web Edition: Data Entry Page. The issue with date validation.", "Payables Due Calculator. It is ready for testing.", "Web Edition: Search Page. It is ready for testing.", "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.", "Receivables Calculator. Where can I found the complete specs", "Ledger: Inconsistency. Please fix it.", "Receivables Printing. It is ready for testing.", "Screen Redraw. Somebody has to look at it.", "Email System. What library we are going to use?", "Adding New Vendors Fails. This module doesn't work completely!", "History. Will we track the sales history in our system?", "Main Menu: Add a File menu. File menu is missed!!!", "Currency Mask. The current currency mask in completely inconvinience.", "Drag & Drop. In the schedule module drag & drop is not available.", "Data Import. What competitors databases will we support?", "Reports. The list of incomplete reports.", "Data Archiving. This features is still missed in our application", "Email Attachments. How to add the multiple attachment? I did not find a way to do it.", "Check Register. We are using different paths for different modules.", "Data Export. Our customers asked for export into Excel"}

        Shared Sub New()
            Dim count As Integer = Users.Length
            For i As Integer = 0 To count - 1
                ResourceList.Add(i, Users(i))
            Next i
        End Sub
        Public Shared Function GetResourceStream(ByVal resourcePath As String, ByVal resourceName As String) As Stream
            Dim fullResourceName As String = String.Format("{0}.{1}", resourcePath, resourceName)
            Dim result As Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(fullResourceName)
            If result IsNot Nothing Then
                Return result
            End If
            Return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
        End Function
        Public Shared Function ConvertBitmapToBitmapImage(ByVal source As Image) As BitmapImage
            Dim stream As New MemoryStream()
            source.Save(stream, ImageFormat.Png)
            stream.Position = 0
            Dim result As New BitmapImage()
            result.BeginInit()
            result.StreamSource = stream
            result.EndInit()
            Return result
        End Function
        Public Shared Function ConvertImageToMemoryStream(ByVal img As Image) As MemoryStream
            Dim ms = New MemoryStream()
            img.Save(ms, ImageFormat.Png)
            Return ms
        End Function
        Public Shared Sub FillResources(ByVal storage As SchedulerStorage, ByVal maxCount As Integer)
            Dim resources As ResourceStorage = storage.ResourceStorage
            storage.BeginUpdate()
            Try
                resources.Clear()
                Dim count As Integer = Math.Min(maxCount, ResourceList.Count)
                For i As Integer = 0 To count - 1
                    Dim caption As String = String.Empty
                    If ResourceList.TryGetValue(i, caption) Then
                        If String.IsNullOrEmpty(caption) Then
                            Continue For
                        End If

                        Dim resource As Resource = storage.CreateResource(i)
                        resource.Caption = caption
                        resources.Add(resource)
                    End If
                Next i

            Finally
                storage.EndUpdate()
            End Try
        End Sub


        Private Shared outlookCalendarPaths_Renamed() As String = Nothing

        Public Shared Function GetSportEventsData() As DataTable
            Dim sportEventDS As New DataSet()
            Using stream As Stream = GetResourceStream(ResourcePathData, "sportevents.xml")
                sportEventDS.ReadXml(stream)
                stream.Close()
            End Using
            Return sportEventDS.Tables(0)
        End Function
        Public Shared Function GenerateScheduleTasks() As DataTable
            Dim tbl As New DataTable()
            tbl = New DataTable("ScheduleTasks")
            tbl.Columns.Add("ID", GetType(Integer))
            tbl.Columns.Add("Subject", GetType(String))
            tbl.Columns.Add("Severity", GetType(Integer))
            tbl.Columns.Add("Priority", GetType(Integer))
            tbl.Columns.Add("Duration", GetType(Integer))
            tbl.Columns.Add("Description", GetType(String))
            For i As Integer = 0 To 20
                Dim description As String = taskDescriptions(i)
                Dim index As Integer = description.IndexOf("."c)
                Dim subject As String
                If index <= 0 Then
                    subject = "task" & Convert.ToInt32(i + 1)
                Else
                    subject = description.Substring(0, index)
                End If
                tbl.Rows.Add(New Object() { i + 1, subject, RandomInstance.Next(3), RandomInstance.Next(3), Math.Max(1, RandomInstance.Next(8)), description })
            Next i
            Return tbl
        End Function

        Public Shared ReadOnly Property OutlookCalendarPaths() As String()
            Get
                If outlookCalendarPaths_Renamed IsNot Nothing Then
                    Return outlookCalendarPaths_Renamed
                End If

                Try
                    outlookCalendarPaths_Renamed = DevExpress.XtraScheduler.Outlook.OutlookExchangeHelper.GetOutlookCalendarPaths()
                Catch
                    ReportOutlookWarning("get the list of available calendars from Microsoft Outlook")
                    outlookCalendarPaths_Renamed = New String(){}
                End Try
                Return outlookCalendarPaths_Renamed
            End Get
        End Property
        Public Shared Sub ReportOutlookWarning(ByVal message As String)
            DXMessageBox.Show(String.Format("Unable to {0}." & ControlChars.Lf & "Check whether MS Outlook is installed.", message), "Import from MS Outlook", MessageBoxButton.OK, MessageBoxImage.Warning)
        End Sub
        Public Shared Sub ReportOutlookError(ByVal message As String)
            DXMessageBox.Show(String.Format("Impossible to {0}. Probably, Microsoft Outlook isn't installed on this machine.", message), System.Reflection.Assembly.GetEntryAssembly().FullName, MessageBoxButton.OK, MessageBoxImage.Error)
        End Sub
        Public Shared Sub ReportOutlookError(ByVal message As String, ByVal exceptionMessage As String)
            DXMessageBox.Show(String.Format("Failed to {0}. An exception has occured:" & ControlChars.CrLf & "{1}", message, exceptionMessage), System.Reflection.Assembly.GetEntryAssembly().FullName, MessageBoxButton.OK, MessageBoxImage.Error)
        End Sub
    End Class
End Namespace
Namespace SchedulerDemo.Internal
    Public MustInherit Class SchedulerDemoXmlPersistenceHelper(Of T As New)
        Inherits CollectionXmlPersistenceHelper

        Public Sub New()
            MyBase.New(Nothing)
        End Sub
        Public Sub New(ByVal eventList As SchedulerItemList(Of T))
            MyBase.New(eventList)
        End Sub

        Protected Overrides ReadOnly Property XmlCollectionName() As String
            Get
                Return AppointmentSR.XmlCollectionName
            End Get
        End Property
        Private ReadOnly Property Items() As SchedulerItemList(Of T)
            Get
                Return CType(Collection, SchedulerItemList(Of T))
            End Get
        End Property

        Protected Overrides Function CreateXmlContextItem(ByVal obj As Object) As IXmlContextItem
            Return Nothing
        End Function
    End Class
    Public Class EventItemXmlPersistenceHelper
        Inherits SchedulerDemoXmlPersistenceHelper(Of EventItem)

        Protected Overrides Function CreateObjectCollectionXmlLoader(ByVal root As XmlNode) As ObjectCollectionXmlLoader
            Return New EventItemListXmlLoader(root)
        End Function
    End Class
    Public Class CarItemXmlPersistenceHelper
        Inherits SchedulerDemoXmlPersistenceHelper(Of CarItem)

        Protected Overrides Function CreateObjectCollectionXmlLoader(ByVal root As XmlNode) As ObjectCollectionXmlLoader
            Return New CarItemListXmlLoader(root)
        End Function
    End Class

    Public MustInherit Class SchedulerDemoCollectionXmlLoader(Of T As New)
        Inherits ObjectCollectionXmlLoader


        Private items_Renamed As New SchedulerItemList(Of T)()

        Protected Sub New(ByVal root As XmlNode)
            MyBase.New(root)
        End Sub
        Protected Overrides ReadOnly Property Collection() As ICollection
            Get
                Return items_Renamed
            End Get
        End Property
        Protected Overrides ReadOnly Property XmlCollectionName() As String
            Get
                Return AppointmentSR.XmlCollectionName
            End Get
        End Property
        Public ReadOnly Property Items() As SchedulerItemList(Of T)
            Get
                Return items_Renamed
            End Get
        End Property

        Protected Overrides Sub AddObjectToCollection(ByVal obj As Object)
            Items.Add(DirectCast(obj, T))
        End Sub
        Protected Overrides Sub ClearCollectionObjects()
            Items.Clear()
        End Sub
        Protected Overridable Function CreateItemLoader(ByVal root As XmlNode) As ObjectXmlLoader
            Return New EventItemXmlLoader(root)
        End Function
        Protected Overrides Function LoadObject(ByVal root As XmlNode) As Object
            Dim loader As ObjectXmlLoader = CreateItemLoader(root)
            Return loader.ObjectFromXml()
        End Function
    End Class
    Public Class EventItemListXmlLoader
        Inherits SchedulerDemoCollectionXmlLoader(Of EventItem)

        Public Sub New(ByVal root As XmlNode)
            MyBase.New(root)
        End Sub
        Protected Overrides Function CreateItemLoader(ByVal root As XmlNode) As ObjectXmlLoader
            Return New EventItemXmlLoader(root)
        End Function
    End Class
    Public Class CarItemListXmlLoader
        Inherits SchedulerDemoCollectionXmlLoader(Of CarItem)

        Public Sub New(ByVal root As XmlNode)
            MyBase.New(root)
        End Sub
        Protected Overrides Function CreateItemLoader(ByVal root As XmlNode) As ObjectXmlLoader
            Return New CarItemXmlLoader(root)
        End Function
    End Class

    Public Class EventItemXmlLoader
        Inherits ObjectXmlLoader


        Private root_Renamed As XmlNode
        Public Sub New(ByVal root As XmlNode)
            MyBase.New(root)
            Me.root_Renamed = root
        End Sub
        Public ReadOnly Property Root() As XmlNode
            Get
                Return root_Renamed
            End Get
        End Property

        Public Overrides Function ObjectFromXml() As Object
            Dim ev As New EventItem()
            ev.Type = ReadAttributeAsInt("Type", 0)
            ev.StartTime = ReadAttributeAsDateTime("Start", Date.MinValue)
            ev.EndTime = ReadAttributeAsDateTime("End", Date.MinValue)
            ev.Label = ReadAttributeAsObject("Label", GetType(Integer), 0)
            ev.Description = ReadAttributeAsString("Description", String.Empty)
            ev.Location = ReadAttributeAsString("Location", String.Empty)
            ev.ResourceId = ReadAttributeAsObject("ResourceId", GetType(Integer), String.Empty)
            ev.Status = ReadAttributeAsObject("Status", GetType(Integer), 0)
            ev.Subject = ReadAttributeAsString("Subject", String.Empty)
            ev.Price = ReadAttributeAsInt("Price", 0)
            ev.RecurrenceInfo = ReadChildNodesAsString("RecurrenceInfo", String.Empty)
            ev.ReminderInfo = ReadChildNodesAsString("Reminder", String.Empty)
            Return ev
        End Function
        Private Function ReadChildNodesAsString(ByVal name As String, ByVal defaultValue As String) As String
            Dim obj As String = defaultValue
            For Each node As XmlNode In Root.ChildNodes
                If node.Name = name Then
                    obj = node.OuterXml
                End If
            Next node
            Return obj
        End Function
    End Class
    Public Class CarItemXmlLoader
        Inherits ObjectXmlLoader

        Public Sub New(ByVal root As XmlNode)
            MyBase.New(root)
        End Sub
        Public Overrides Function ObjectFromXml() As Object
            Dim car As New CarItem()
            car.Caption = ReadAttributeAsString("Caption", String.Empty)
            car.Id = ReadAttributeAsString("Id", String.Empty)
            car.Picture = CType(ReadAttributeValue("Picture", GetType(Byte())), Byte())
            Return car
        End Function
    End Class

    #Region "EventItem"
    Public Class EventItem
        Public Property Type() As Object
        Public Property StartTime() As Object
        Public Property EndTime() As Object
        Public Property Description() As String
        Public Property AllDay() As Boolean
        Public Property Label() As Object
        Public Property Location() As String
        Public Property ResourceId() As Object
        Public Property Status() As Object
        Public Property Subject() As String
        Public Property Price() As Object
        Public Property RecurrenceInfo() As String
        Public Property ReminderInfo() As String

        Public Sub New()
            Type = CInt((AppointmentType.Normal))
            StartTime = Date.MinValue
            EndTime = Date.MinValue + DateTimeHelper.HalfHourSpan
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
    #Region "CarItem"
    Public Class CarItem
        Public Property Caption() As String
        Public Property Id() As Object
        Public Property Picture() As Byte()

        Public Sub New()
            Caption = String.Empty
            Id = Nothing
            Picture = Nothing
        End Sub
    End Class
    #End Region
    #Region "SportItem"
    Public Class SportItem
        Public Property StartTime() As Object
        Public Property EndTime() As Object
        Public Property Caption() As String
        Public Property AllDay() As Boolean
        Public Property ResourceId() As Object
        Public Property SportId() As Object

        Public Sub New()
            StartTime = Date.MinValue
            EndTime = Date.MinValue + DateTimeHelper.HalfHourSpan
            Caption = String.Empty
            AllDay = False
            ResourceId = EmptyResourceId.Id
            SportId = 0
        End Sub
    End Class
    #End Region
    #Region "SchedulerBindingList<T>"
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
    #Region "XmlHelper"
    Public NotInheritable Class SchedulerXmlHelper

        Private Sub New()
        End Sub

        Private Delegate Function MapperHandler(Of T, U)(ByVal u As U) As T
        Private Const ResourcePathData As String = "SchedulerDemo.Data"
        Public Shared Sub WriteEventsToXml(ByVal collectoin As AppointmentCollection)
            WriteToXmlCore(Of EventItem, Appointment)(collectoin, SchedulerDataHelper.EventsXmlSource, New MapperHandler(Of EventItem, Appointment)(AddressOf CreateEventItemFromAppointment))
        End Sub
        Public Shared Sub WriteSportEventsToXml(ByVal collectoin As AppointmentCollection)
            WriteToXmlCore(Of SportItem, Appointment)(collectoin, "sportevents.xml", New MapperHandler(Of SportItem, Appointment)(AddressOf CreateSportItemFromAppointment))
        End Sub
        Public Shared Sub WriteCarsToXml(ByVal collection As ResourceCollection)
            WriteToXmlCore(Of CarItem, Resource)(collection, SchedulerDataHelper.CarsXmlSource, New MapperHandler(Of CarItem, Resource)(AddressOf MapResourceToCarItem))
        End Sub
        Public Shared Function GetDataStream(ByVal fileName As String) As Stream
            Return DemoUtils.GetResourceStream(ResourcePathData, fileName)
        End Function
        Public Shared Function LoadFromXml(Of T As New)(ByVal fileName As String) As SchedulerBindingList(Of T)
            Dim eventList As New SchedulerBindingList(Of T)()
            Using stream As Stream = GetDataStream(fileName)
                Dim s As New XmlSerializer(GetType(SchedulerBindingList(Of T)))
                eventList = DirectCast(s.Deserialize(stream), SchedulerBindingList(Of T))
                stream.Close()
            End Using
            Return eventList
        End Function
        Private Shared Function CreateEventItemFromAppointment(ByVal appointment As Appointment) As EventItem
            Dim item As New EventItem()
            If appointment.Reminder IsNot Nothing Then
                item.ReminderInfo = CreateReminderString(appointment)
            End If
            item.Description = appointment.Description
            item.AllDay = appointment.AllDay
            item.EndTime = appointment.End
            item.Label = appointment.LabelKey
            item.Location = appointment.Location
            If appointment.RecurrenceInfo IsNot Nothing Then
                item.RecurrenceInfo = appointment.RecurrenceInfo.ToXml()
            End If
            item.ResourceId = appointment.ResourceId
            item.StartTime = appointment.Start
            item.Status = appointment.StatusKey
            item.Subject = appointment.Subject
            item.Type = appointment.Type
            Return item
        End Function
        Private Shared Function CreateSportItemFromAppointment(ByVal appointment As Appointment) As SportItem
            Dim item As New SportItem()
            item.Caption = appointment.Subject
            item.AllDay = appointment.AllDay
            item.EndTime = appointment.End
            item.ResourceId = appointment.ResourceId
            item.SportId = appointment.LabelKey
            item.StartTime = appointment.Start
            Return item
        End Function
        Private Shared Function CreateReminderString(ByVal appointment As Appointment) As String
            Dim helper As New ReminderXmlPersistenceHelper(appointment.Reminder)
            Return helper.ToXml()
        End Function
        Private Shared Function MapResourceToCarItem(ByVal resource As Resource) As CarItem
            Dim item As New CarItem()
            item.Caption = resource.Caption
            item.Id = resource.Id
            Return item
        End Function
        Private Shared Sub WriteToXmlCore(Of T, U)(ByVal sourceCollection As NotificationCollection(Of U), ByVal fileName As String, ByVal map As MapperHandler(Of T, U))
            Using stream As New FileStream(fileName, FileMode.Open, FileAccess.Write)
                Dim result As New NotificationCollection(Of T)()
                For Each item As U In sourceCollection
                    result.Add(map(item))
                Next item
                Dim s As New XmlSerializer(GetType(NotificationCollection(Of T)))
                s.Serialize(stream, result)
                stream.Close()
            End Using
        End Sub
    End Class
    #End Region
End Namespace
