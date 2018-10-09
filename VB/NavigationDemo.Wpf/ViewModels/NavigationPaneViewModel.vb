Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows.Media
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Accordion

Namespace NavigationDemo
    Public Class NavigationPaneViewModel
        Protected ReadOnly Property DocumentManagerService() As IDocumentManagerService
            Get
                Return Me.GetRequiredService(Of IDocumentManagerService)()
            End Get
        End Property

        Public Overridable Property Groups() As ReadOnlyCollection(Of GroupDescription)
        Public Overridable Property SelectedGroup() As GroupDescription
        Public Overridable Property OnStartup() As Boolean

        Private contactsNavigationViewModel As ContactsNavigationViewModel

        Public Sub OnLoaded()
            If OnStartup Then
                OnSelectedItemChanged(New AccordionSelectedItemChangedEventArgs(Nothing, Nothing, Groups.Single(Function(x) x.Id = NavigationId.Mail).Items.First()))
            End If
        End Sub
        Public Sub OnSelectedItemChanged(ByVal e As AccordionSelectedItemChangedEventArgs)
            If e.OldItem IsNot Nothing Then
                OnStartup = False
            End If
            Dim newItem = TryCast(e.NewItem, ItemDescription)
            If newItem Is Nothing Then
                Return
            End If
            Dim document = DocumentManagerService.CreateDocument(GetDocumentType(newItem.Id), GetDocumentParameter(newItem.Id), Me)

            document.Show()
        End Sub
        Public Sub New()
            OnStartup = True
            Groups = New ReadOnlyCollection(Of GroupDescription)(CreateGroups())
            SelectedGroup = Groups(0)
        End Sub
        Private Function CreateGroups() As List(Of GroupDescription)

            Dim groups_Renamed = New List(Of GroupDescription)()
            contactsNavigationViewModel = NavigationDemo.ContactsNavigationViewModel.Create()

            groups_Renamed.Add(GroupDescription.Create(NavigationId.Mail, items:= New ItemDescription() { ItemDescription.Create(NavigationId.Inbox), ItemDescription.Create(NavigationId.Outbox), ItemDescription.Create(NavigationId.SentItems, "Sent Items"), ItemDescription.Create(NavigationId.DeletedItems, "Deleted Items"), ItemDescription.Create(NavigationId.Drafts)}))
            groups_Renamed.Add(GroupDescription.Create(NavigationId.Calendar, items:= New ItemDescription() { ItemDescription.Create(NavigationId.Calendar_Content) }))
            groups_Renamed.Add(GroupDescription.Create(NavigationId.Contacts, items:= New ItemDescription() { ItemDescription.Create(NavigationId.Contacts_Content, navigationViewModel:= contactsNavigationViewModel) }))
            groups_Renamed.Add(GroupDescription.Create(NavigationId.Tasks, items:= New ItemDescription() { ItemDescription.Create(NavigationId.Tasks) }))
            groups_Renamed.Add(GroupDescription.Create(NavigationId.Notes, items:= New ItemDescription() { ItemDescription.Create(NavigationId.Notes) }))
            groups_Renamed.Add(GroupDescription.Create(NavigationId.FolderList, "Folder List", items:= New ItemDescription() { ItemDescription.Create(NavigationId.PersonalFolders, "Personal Folders", New ItemDescription(){ ItemDescription.Create(NavigationId.Contacts), ItemDescription.Create(NavigationId.DeletedItems, "Deleted Items"), ItemDescription.Create(NavigationId.Drafts), ItemDescription.Create(NavigationId.Inbox), ItemDescription.Create(NavigationId.Journal), ItemDescription.Create(NavigationId.Notes), ItemDescription.Create(NavigationId.Outbox), ItemDescription.Create(NavigationId.SentItems, "Sent Items"), ItemDescription.Create(NavigationId.Tasks) }) }, showChildrenExpandButton:= True))
            groups_Renamed.Add(GroupDescription.Create(NavigationId.Shortcuts, items:= New ItemDescription() { ItemDescription.Create(NavigationId.Shortcuts) }))
            groups_Renamed.Add(GroupDescription.Create(NavigationId.Journal, items:= New ItemDescription() { ItemDescription.Create(NavigationId.Journal) }))
            Return groups_Renamed
        End Function

        Private Function GetDocumentType(ByVal itemId As NavigationId) As String
            Dim resultId = itemId
            If MailGroupContainsItem(itemId) Then
                resultId = NavigationId.Mail
            ElseIf itemId = NavigationId.Calendar_Content OrElse itemId = NavigationId.PersonalFolders Then
                Return "Info"
            ElseIf itemId = NavigationId.Contacts_Content Then
                Return "Contacts"
            End If
            Return resultId.ToString()
        End Function
        Private Function GetDocumentParameter(ByVal itemId As NavigationId) As Object
            If MailGroupContainsItem(itemId) Then
                Return itemId
            ElseIf itemId = NavigationId.Calendar_Content Then
                Return String.Empty
            ElseIf itemId = NavigationId.Contacts OrElse itemId = NavigationId.Contacts_Content Then
                Return contactsNavigationViewModel
            ElseIf itemId = NavigationId.PersonalFolders Then
                Return "Select an item from the accordion on the left..."
            End If
            Return Nothing
        End Function
        Private Function MailGroupContainsItem(ByVal itemId As NavigationId) As Boolean
            Return Groups.Single(Function(x) x.Id = NavigationId.Mail).Items.SingleOrDefault(Function(y) y.Id = itemId) IsNot Nothing
        End Function
    End Class

    Public Class GroupDescription
        Inherits ItemDescriptionBase

        Public Shared Function Create(ByVal id As NavigationId, Optional ByVal title As String = Nothing, Optional ByVal items As IList(Of ItemDescription) = Nothing, Optional ByVal showChildrenExpandButton As Boolean = False) As GroupDescription
            Return ViewModelSource.Create(Function() New GroupDescription(id, title, items, showChildrenExpandButton))
        End Function
        Protected Sub New(ByVal id As NavigationId, ByVal title As String, ByVal items As IList(Of ItemDescription), ByVal showChildrenExpandButton As Boolean)
            MyBase.New(id, title, items)
            Icon = NavigationPaneData.GetIcon(String.Format("{0}_16x16", Me.Id))
            If Me.Items.Any() Then
                SelectedItem = Me.Items.First()
            End If
            For Each item In Me.Items
                item.ShowExpandButton = showChildrenExpandButton
            Next item
        End Sub
        Public Overridable Property SelectedItem() As ItemDescription
    End Class

    Public Class ItemDescription
        Inherits ItemDescriptionBase

        Public Shared Function Create(ByVal id As NavigationId, Optional ByVal title As String = Nothing, Optional ByVal items As IList(Of ItemDescription) = Nothing, Optional ByVal navigationViewModel As NavigationViewModelBase = Nothing) As ItemDescription
            Return ViewModelSource.Create(Function() New ItemDescription(id, title, items, navigationViewModel))
        End Function
        Protected Sub New(ByVal id As NavigationId, ByVal title As String, ByVal items As IList(Of ItemDescription), ByVal navigationViewModel As NavigationViewModelBase)
            MyBase.New(id, title, items, navigationViewModel)
            If id <> NavigationId.Calendar_Content OrElse id <> NavigationId.Contacts_Content Then
                Icon = NavigationPaneData.GetIcon(String.Format("{0}_16x16", Me.Id))
            End If
        End Sub
    End Class
    Public Class ItemDescriptionBase
        Protected Sub New(ByVal id As NavigationId, ByVal title As String, ByVal items As IList(Of ItemDescription), Optional ByVal navigationViewModel As NavigationViewModelBase = Nothing)
            Me.Id = id
            Me.Title = If(title, Me.Id.ToString())
            Me.Items = New ObservableCollection(Of ItemDescription)(If(items, Enumerable.Empty(Of ItemDescription)()))
            ShowExpandButton = True
            Me.NavigationViewModel = navigationViewModel
        End Sub
        Private privateId As NavigationId
        Public Property Id() As NavigationId
            Get
                Return privateId
            End Get
            Protected Set(ByVal value As NavigationId)
                privateId = value
            End Set
        End Property
        Private privateTitle As String
        Public Property Title() As String
            Get
                Return privateTitle
            End Get
            Protected Set(ByVal value As String)
                privateTitle = value
            End Set
        End Property
        Private privateIcon As ImageSource
        Public Property Icon() As ImageSource
            Get
                Return privateIcon
            End Get
            Protected Set(ByVal value As ImageSource)
                privateIcon = value
            End Set
        End Property
        Public Overridable Property ShowExpandButton() As Boolean

        Private privateNavigationViewModel As NavigationViewModelBase
        Public Property NavigationViewModel() As NavigationViewModelBase
            Get
                Return privateNavigationViewModel
            End Get
            Protected Set(ByVal value As NavigationViewModelBase)
                privateNavigationViewModel = value
            End Set
        End Property

        Public Overridable Property Items() As ObservableCollection(Of ItemDescription)
    End Class

    Public Enum NavigationId
        Mail
        Inbox
        Outbox
        SentItems
        DeletedItems
        Drafts
        Calendar
        Calendar_Content
        Contacts
        Contacts_Content
        Tasks
        Notes
        FolderList
        PersonalFolders
        Shortcuts
        Journal
    End Enum
End Namespace
