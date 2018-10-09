Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows.Media
Imports DevExpress.DemoData.Helpers
Imports DevExpress.DemoData.Utils
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace NavigationDemo
    Public NotInheritable Class NavigationPaneData

        Private Sub New()
        End Sub

        Public Shared rnd As New Random()
        Private Shared users() As String = {"Peter Dolan", "Ryan Fischer", "Richard Fisher", "Tom Hamlett", "Mark Hamilton", "Steve Lee", "Jimmy Lewis", "Jeffrey W McClain", "Andrew Miller", "Dave Murrel", "Bert Parkins", "Mike Roller", "Ray Shipman", "Paul Bailey", "Brad Barnes", "Carl Lucas", "Jerry Campbell"}
        Private Shared subject() As String = { "Implementing DevExpress MasterView control into Accounting System.", "Web Edition: Data Entry Page. The issue with date validation.", "Payables Due Calculator. It is ready for testing.", "Web Edition: Search Page. It is ready for testing.", "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.", "Receivables Calculator. Where can I found the complete specs", "Ledger: Inconsistency. Please fix it.", "Receivables Printing. It is ready for testing.", "Screen Redraw. Somebody has to look at it.", "Email System. What library we are going to use?", "Adding New Vendors Fails. This module doesn't work completely!", "History. Will we track the sales history in our system?", "Main Menu: Add a File menu. File menu is missed!!!", "Currency Mask. The current currency mask in completely inconvinience.", "Drag & Drop. In the schedule module drag & drop is not available.", "Data Import. What competitors databases will we support?", "Reports. The list of incomplete reports.", "Data Archiving. This features is still missed in our application", "Email Attachments. How to add the multiple attachment? I did not find a way to do it.", "Check Register. We are using different paths for different modules.", "Data Export. Our customers asked for export into Excel"}

        Private Shared Function GetBoolean() As Boolean
            Dim ret As Integer = rnd.Next(7)
            Return ret > 4
        End Function
        Private Shared Function GetIsRead() As Boolean
            Dim ret As Integer = rnd.Next(7)
            Return ret > 1
        End Function
        Private Shared Function GetImportance() As Integer
            Dim ret As Integer = rnd.Next(7)
            If ret > 2 Then
                ret = 1
            End If
            Return ret
        End Function
        Private Shared Function GetSent() As Date
            Dim ret As Date = Date.Now
            Dim r As Integer = rnd.Next(12)
            If r > 1 Then
                ret = ret.AddDays(-rnd.Next(50))
            End If
            ret = ret.AddMinutes(-rnd.Next(ret.Minute + ret.Hour * 60 + 360))
            Return ret
        End Function
        Private Shared Function GetReceived(ByVal sent As Date) As Date
            Return sent.AddMinutes(10 + rnd.Next(120))
        End Function
        Private Shared Function GetSubject() As String
            Return subject(rnd.Next(subject.Length - 1))
        End Function
        Private Shared Function GetRandomUser() As String
            Return users(rnd.Next(users.Length - 2))
        End Function
        Private Shared Function GetFixedUser() As String
            Return users(users.Length - 1)
        End Function

        Public Shared ReadOnly Property JournalData() As ObservableCollection(Of JournalItem)
            Get
                Return journalData_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property NotesData() As ObservableCollection(Of NotesItem)
            Get
                Return notesData_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property TasksData() As ObservableCollection(Of TasksItem)
            Get
                Return tasksData_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property ContactsData() As ObservableCollection(Of ContactItem)
            Get
                Return contactsData_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property MailData() As Dictionary(Of NavigationId, ObservableCollection(Of MailItem))
            Get
                Return mailData_Renamed
            End Get
        End Property


        Private Shared journalData_Renamed As ObservableCollection(Of JournalItem) = CreateJournalData()

        Private Shared notesData_Renamed As ObservableCollection(Of NotesItem) = CreateNotesData()

        Private Shared tasksData_Renamed As ObservableCollection(Of TasksItem) = CreateTasksData()

        Private Shared contactsData_Renamed As ObservableCollection(Of ContactItem) = CreateContactsData()

        Private Shared mailData_Renamed As Dictionary(Of NavigationId, ObservableCollection(Of MailItem)) = CreateMailData()

        Private Shared Function CreateJournalData() As ObservableCollection(Of JournalItem)
            Dim result = New ObservableCollection(Of JournalItem)()
            For i As Integer = 0 To 9
                result.Add(New JournalItem(GetSent(), GetSubject()))
            Next i
            Return result
        End Function
        Private Shared Function CreateNotesData() As ObservableCollection(Of NotesItem)
            Dim result = New ObservableCollection(Of NotesItem)()
            For i As Integer = 0 To 9
                result.Add(New NotesItem(GetRandomUser(), GetSubject()))
            Next i
            Return result
        End Function
        Private Shared Function CreateTasksData() As ObservableCollection(Of TasksItem)
            Dim result = New ObservableCollection(Of TasksItem)()
            For i As Integer = 0 To 9
                result.Add(New TasksItem(GetIcon("Tasks_16x16"), GetBoolean(), GetSubject(), GetSent()))
            Next i
            Return result
        End Function
        Private Shared Function CreateMailData() As Dictionary(Of NavigationId, ObservableCollection(Of MailItem))
            Dim result = New Dictionary(Of NavigationId, ObservableCollection(Of MailItem))()
            result.Add(NavigationId.Inbox, CreateMailCollection())
            result.Add(NavigationId.Outbox, CreateEmptyMailCollection())
            result.Add(NavigationId.SentItems, CreateMailCollection(allItemsRead:= True, fromFixedUser:= True))
            result.Add(NavigationId.DeletedItems, CreateMailCollection(allItemsRead:= True))
            result.Add(NavigationId.Drafts, CreateEmptyMailCollection())
            Return result
        End Function
        Private Shared Function CreateMailCollection(Optional ByVal allItemsRead? As Boolean = Nothing, Optional ByVal fromFixedUser As Boolean = False) As ObservableCollection(Of MailItem)
            Dim result = New ObservableCollection(Of MailItem)()
            For i As Integer = 0 To 79
                Dim sent As Date = GetSent()
                result.Add(New MailItem(GetImportance(), GetBoolean(), If(allItemsRead, GetIsRead()), GetSubject(),If(fromFixedUser, GetFixedUser(), GetRandomUser()),If(fromFixedUser, GetRandomUser(), GetFixedUser()), sent, GetReceived(sent)))
            Next i
            Return result
        End Function
        Private Shared Function CreateContactsData() As ObservableCollection(Of ContactItem)
            Return New ObservableCollection(Of ContactItem)(EmployeesWithPhotoData.DataSource.Take(15).Select(Function(x) New ContactItem() With {.FirstName = x.FirstName, .LastName = x.LastName, .Email = x.EmailAddress}))
        End Function
        Private Shared Function CreateEmptyMailCollection() As ObservableCollection(Of MailItem)
            Return New ObservableCollection(Of MailItem)()
        End Function
        Public Shared Function GetIcon(ByVal id As String) As ImageSource
            Return ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(GetType(JournalItem).Assembly, String.Format("Images/Navigation/{0}.png", id)))
        End Function
    End Class
    Public Class JournalItem
        Public Sub New()
            [Date] = Date.Now
            Time = Date.Now
        End Sub
        Public Sub New(ByVal dateTime As Date, ByVal description As String)
            [Date] = dateTime
            Time = dateTime
            Me.Description = description
        End Sub
        Public Property [Date]() As Date
        Public Property Time() As Date

        Public Property Description() As String
    End Class
    Public Class NotesItem
        Public Sub New()
        End Sub
        Public Sub New(ByVal name As String, ByVal description As String)
            Me.Name = name
            Me.Description = description
        End Sub
        Public Property Name() As String
        Public Property Description() As String
    End Class
    Public Class TasksItem
        Public Sub New()
            Image = NavigationPaneData.GetIcon("Tasks_16x16")
            [Date] = Date.Now
        End Sub
        Public Sub New(ByVal image As ImageSource, ByVal check As Boolean, ByVal subject As String, ByVal [date] As Date)
            Me.Image = image
            Me.Check = check
            Me.Subject = subject
            Me.Date = [date]
        End Sub
        Public Property Image() As ImageSource
        Public Property Check() As Boolean
        Public Property Subject() As String
        Public Property [Date]() As Date
    End Class

    Public Class MailItem
        Public Sub New(ByVal importance As Integer, ByVal hasAttachment As Boolean, ByVal isRead As Boolean, ByVal subject As String, ByVal [from] As String, ByVal [to] As String, ByVal sent As Date, ByVal receive As Date)
            Me.Importance = importance
            Me.HasAttachment = hasAttachment
            Me.IsRead = isRead
            Image = NavigationPaneData.GetIcon(If(Me.IsRead, "TextBox_16x16", "Mail_16x16"))
            Me.Subject = subject
            Me.From = [from]
            Me.To = [to]
            Me.Sent = sent
            Me.Receive = receive
        End Sub
        Public Property Importance() As Integer
        Public Property HasAttachment() As Boolean
        Public Property IsRead() As Boolean
        Public Property Image() As ImageSource
        Public Property Subject() As String
        Public Property [From]() As String
        Public Property [To]() As String
        Public Property Sent() As Date
        Public Property Receive() As Date
    End Class
    Public Class ContactItem
        Public Property FirstName() As String
        Public Property LastName() As String
        Public Property Email() As String
    End Class
End Namespace
