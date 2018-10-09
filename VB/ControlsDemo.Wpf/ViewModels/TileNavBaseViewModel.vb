Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Navigation.Internal

Namespace ControlsDemo
    Public Class TileNavViewLocator
        Inherits ViewLocator

        Public Sub New()
            MyBase.New(GetType(TileNavViewLocator).Assembly)
        End Sub
    End Class

    Public Class TileNavBaseViewModel
        Inherits ViewModelBase

        Private _Actions As ObservableCollection(Of TileNavBaseItemViewModel)
        Private _Categories As ObservableCollection(Of TileNavBaseItemViewModel)
        Private Selected As TileNavBaseItemViewModel

        Public Sub New()
            Messenger.Default.Register(Of NavigationMessage)(Me, AddressOf OnNavigationMessage)
            _Categories = New ObservableCollection(Of TileNavBaseItemViewModel)(TileNavBaseViewModelDataProvider.CreateCategories())
            _Actions = New ObservableCollection(Of TileNavBaseItemViewModel)(TileNavBaseViewModelDataProvider.CreateActions())
            BackCommand = DelegateCommandFactory.Create(AddressOf OnBackCommand, AddressOf CanBackCommand)
            ViewUnloadedCommand = New DelegateCommand(AddressOf OnViewUnloaded)
        End Sub

        Public ReadOnly Property Actions() As ObservableCollection(Of TileNavBaseItemViewModel)
            Get
                Return _Actions
            End Get
        End Property
        Private privateBackCommand As ICommand
        Public Property BackCommand() As ICommand
            Get
                Return privateBackCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateBackCommand = value
            End Set
        End Property
        Public ReadOnly Property Categories() As ObservableCollection(Of TileNavBaseItemViewModel)
            Get
                Return _Categories
            End Get
        End Property
        Private privateViewUnloadedCommand As ICommand
        Public Property ViewUnloadedCommand() As ICommand
            Get
                Return privateViewUnloadedCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateViewUnloadedCommand = value
            End Set
        End Property
        Private ReadOnly Property NavigationService() As INavigationService
            Get
                Return ServiceContainer.GetService(Of INavigationService)()
            End Get
        End Property

        Protected Overridable Sub OnNavigationMessage(ByVal message As NavigationMessage)
            Select Case message.MessageType
                Case NavigationMessageType.[New]
                    Dim vm As TileNavBaseItemViewModel = TryCast(message.Source, TileNavBaseItemViewModel)
                    If vm IsNot Nothing AndAlso vm.IsSelected Then
                        NavigationService.Navigate("TileNavDetailsView", vm)
                        If Selected IsNot Nothing Then
                            Selected.IsSelected = False
                        End If
                        Selected = vm
                    End If
                Case NavigationMessageType.Back
                    OnBackCommand()
            End Select
        End Sub
        Protected Overridable Sub OnViewUnloaded()
            Messenger.Default.Unregister(Of NavigationMessage)(Me, AddressOf OnNavigationMessage)
        End Sub
        Private Function CanBackCommand() As Boolean
            Return NavigationService IsNot Nothing AndAlso NavigationService.CanGoBack
        End Function
        Private Sub OnBackCommand()
            If NavigationService.CanGoBack Then
                NavigationService.GoBack()
                If Selected IsNot Nothing Then
                    Selected.IsSelected = False
                End If
                Selected = Nothing
            End If
        End Sub
    End Class
    Public Class TileNavBaseItemViewModel
        Protected Sub New()
            Items = New ObservableCollection(Of TileNavBaseItemViewModel)()
            BackCommand = DelegateCommandFactory.Create(AddressOf OnBackCommand)
            ShowNotificationCommand = DelegateCommandFactory.Create(Of String)(AddressOf OnShowNotificationCommand)
        End Sub

        Private privateBackCommand As ICommand
        Public Property BackCommand() As ICommand
            Get
                Return privateBackCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateBackCommand = value
            End Set
        End Property
        Public Overridable Property Color() As Color
        Public Overridable Property ContentImage() As ImageSource
        Public Overridable Property DisplayName() As String
        Public Overridable Property Image() As ImageSource
        Public Overridable Property IsSelected() As Boolean
        Private privateItems As ObservableCollection(Of TileNavBaseItemViewModel)
        Public Property Items() As ObservableCollection(Of TileNavBaseItemViewModel)
            Get
                Return privateItems
            End Get
            Private Set(ByVal value As ObservableCollection(Of TileNavBaseItemViewModel))
                privateItems = value
            End Set
        End Property
        Public Overridable Property ShowNotificationCommand() As ICommand
        Public Property Size() As TileSize

        Public Shared Function Create() As TileNavBaseItemViewModel
            Return ViewModelSource.Create(Function() New TileNavBaseItemViewModel())
        End Function
        Private Sub OnBackCommand()
            Messenger.Default.Send(New NavigationMessage(NavigationMessageType.Back))
        End Sub
        Protected Sub OnIsSelectedChanged()
            If IsSelected Then
                Messenger.Default.Send(New NavigationMessage(Me))
            End If
        End Sub
        Private Sub OnShowNotificationCommand(ByVal message As String)
            Messenger.Default.Send(New NotificationMessage(DisplayName & " clicked"))
        End Sub
    End Class
    Friend NotInheritable Class TileNavBaseViewModelDataProvider

        Private Sub New()
        End Sub

        Private Shared ContentImages() As String = { "/ControlsDemo;component/Images/Calc.png", "/ControlsDemo;component/Images/Rates.png", "/ControlsDemo;component/Images/Research.png", "/ControlsDemo;component/Images/Statistics.png","/ControlsDemo;component/Images/System.png","/ControlsDemo;component/Images/UserManagment.png","/ControlsDemo;component/Images/ZillowLogo.png"}
        Private Shared ItemImages() As String = { "/ControlsDemo;component/Images/TileNavPane/itemElement01.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement02.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement03.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement04.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement05.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement06.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement07.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement08.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement09.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement10.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement11.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement12.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement13.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement14.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement15.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement16.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement17.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement18.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement19.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement20.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement21.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement22.Glyph.png","/ControlsDemo;component/Images/TileNavPane/itemElement23.Glyph.png" }
        Private Shared SubItemImages() As String = { "/ControlsDemo;component/Images/TileNavPane/subItemElement01.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement02.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement03.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement04.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement05.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement06.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement07.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement08.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement09.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement10.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement11.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement12.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement13.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement14.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement15.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement16.Glyph.png","/ControlsDemo;component/Images/TileNavPane/subItemElement17.Glyph.png" }
        Private Shared Colors() As Color = { DirectCast(ColorConverter.ConvertFromString("#FFC3213F"), Color), DirectCast(ColorConverter.ConvertFromString("#FFE65E20"), Color), DirectCast(ColorConverter.ConvertFromString("#FFD4AF00"), Color), DirectCast(ColorConverter.ConvertFromString("#FF6652A2"), Color), DirectCast(ColorConverter.ConvertFromString("#FF54AF0E"), Color), DirectCast(ColorConverter.ConvertFromString("#FF00ABDC"), Color), DirectCast(ColorConverter.ConvertFromString("#FFDA8515"), Color) }
        Private Shared random As New Random(Date.Now.Millisecond)
        Private Shared Function CreateItem(ByVal displayName As String, ByVal Images() As String) As TileNavBaseItemViewModel
            Dim vm As TileNavBaseItemViewModel = TileNavBaseItemViewModel.Create()
            vm.Do(Sub(x)
                x.DisplayName = displayName
                x.Color = Colors(random.Next(Colors.Length))
                x.Image = New BitmapImage(New Uri(Images(random.Next(Images.Length)), UriKind.RelativeOrAbsolute))
                x.ContentImage = New BitmapImage(New Uri(ContentImages(random.Next(ContentImages.Length)), UriKind.RelativeOrAbsolute))
            End Sub)
            Return vm
        End Function
        Public Shared Function CreateCategories() As IEnumerable(Of TileNavBaseItemViewModel)
            Dim categories = New List(Of TileNavBaseItemViewModel)()
            For i As Integer = 1 To 5
                Dim category As TileNavBaseItemViewModel = CreateItem(String.Format("Category {0}", i), ItemImages)
                For j As Integer = 1 To 4
                    Dim item As TileNavBaseItemViewModel = CreateItem(String.Format("Item {0}.{1}", i, j), ItemImages)
                    For k As Integer = 1 To 3
                        Dim subItem As TileNavBaseItemViewModel = CreateItem(String.Format("SubItem {0}.{1}.{2}", i, j, k), SubItemImages)
                        item.Items.Add(subItem)
                    Next k
                    category.Items.Add(item)
                Next j
                categories.Add(category)
            Next i
            Return categories
        End Function
        Public Shared Function CreateActions() As IEnumerable(Of TileNavBaseItemViewModel)
            Dim actions = New List(Of TileNavBaseItemViewModel)()
            For i As Integer = 1 To 4
                Dim action As TileNavBaseItemViewModel = CreateItem(String.Format("Action {0}", i), ItemImages)
                If i < 3 Then
                    action.Size = TileSize.Small
                End If
                actions.Add(action)
            Next i
            Return actions
        End Function
    End Class
    Public Enum NavigationMessageType
        [New]
        Back
    End Enum
    Public NotInheritable Class NavigationMessage
        Public Sub New(ByVal source As Object)
            Me.Source = source
        End Sub
        Public Sub New(ByVal messageType As NavigationMessageType, Optional ByVal source As Object = Nothing)
            Me.New(source)
            Me.MessageType = messageType
        End Sub
        Private privateSource As Object
        Public Property Source() As Object
            Get
                Return privateSource
            End Get
            Private Set(ByVal value As Object)
                privateSource = value
            End Set
        End Property
        Private privateMessageType As NavigationMessageType
        Public Property MessageType() As NavigationMessageType
            Get
                Return privateMessageType
            End Get
            Private Set(ByVal value As NavigationMessageType)
                privateMessageType = value
            End Set
        End Property
    End Class
    Public NotInheritable Class NotificationMessage
        Public Sub New(ByVal source As String)
            Me.Source = source
        End Sub
        Private privateSource As String
        Public Property Source() As String
            Get
                Return privateSource
            End Get
            Private Set(ByVal value As String)
                privateSource = value
            End Set
        End Property
    End Class
End Namespace
