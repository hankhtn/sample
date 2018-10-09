Imports DevExpress.Mvvm
Imports DevExpress.Xpf.WindowsUI
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows.Input
Imports System.Linq
Imports DevExpress.Mvvm.Native
Imports System.Data
Imports DevExpress.Xpf.DemoBase.Helpers
Imports System.Reflection
Imports System.Windows.Data
Imports System.Windows
Imports DevExpress.Mvvm.UI.Native
Imports DevExpress.Xpf.Editors
Imports System.ComponentModel
Imports DevExpress.Xpf.Grid
Imports DevExpress.Mvvm.DataAnnotations

Namespace WindowsUIDemo
#Region "HamburgerMenuItemsModels "
    Public MustInherit Class BottomBarItemViewModelBase
        Inherits BindableBase

        Private _placement As HamburgerMenuBottomBarItemPlacement = HamburgerMenuBottomBarItemPlacement.Left
        Private _icon As Uri = Nothing

        Public Property Placement() As HamburgerMenuBottomBarItemPlacement
            Get
                Return _placement
            End Get
            Set(ByVal value As HamburgerMenuBottomBarItemPlacement)
                SetProperty(_placement, value, "Placement")
            End Set
        End Property
        Public Property Icon() As Uri
            Get
                Return _icon
            End Get
            Set(ByVal value As Uri)
                SetProperty(_icon, value, "Icon")
            End Set
        End Property

        Public Sub New(ByVal icon As String)
            Me.Icon = HamburgerItemViewModelBase.GetIconUriFromName(icon)
        End Sub
    End Class
    Public Class BottomBarNavigationItemModel
        Inherits BottomBarItemViewModelBase

        Private _navigationTargetType As Type = Nothing
        Private _command As ICommand = Nothing

        Public Property NavigationTargetType() As Type
            Get
                Return _navigationTargetType
            End Get
            Set(ByVal value As Type)
                SetProperty(_navigationTargetType, value, "NavigationTargetType")
            End Set
        End Property
        Public Property Command() As ICommand
            Get
                Return _command
            End Get
            Set(ByVal value As ICommand)
                SetProperty(_command, value, "Command")
            End Set
        End Property

        Public Sub New(ByVal icon As String, ByVal navigationTargetType As Type, Optional ByVal command As ICommand = Nothing)
            MyBase.New(icon)
            Me.Command = command
            Me.NavigationTargetType = navigationTargetType
        End Sub
    End Class
    Public Class BottomBarCheckItemViewModel
        Inherits BottomBarItemViewModelBase

        Private _isChecked? As Boolean = Nothing

        Public Property IsChecked() As Boolean?
            Get
                Return _isChecked
            End Get
            Set(ByVal value? As Boolean)
                SetProperty(_isChecked, value, "IsChecked", AddressOf OnIsCheckedChanged)
            End Set
        End Property

        Private checkedChangedAction As Action(Of Boolean?)

        Public Sub New(ByVal icon As String, ByVal checkAction As Action(Of Boolean?))
            MyBase.New(icon)
            checkedChangedAction = checkAction
            IsChecked = False
        End Sub

        Protected Overridable Sub OnIsCheckedChanged()
            checkedChangedAction(IsChecked)
        End Sub
    End Class
    Public MustInherit Class HamburgerItemViewModelBase
        Inherits BindableBase

        Private _navigationTargetType As Type = Nothing
        Private _content As Object = Nothing
        Private _icon As Uri = Nothing
        Private _placement As HamburgerMenuItemPlacement = HamburgerMenuItemPlacement.Top

        Public Property NavigationTargetType() As Type
            Get
                Return _navigationTargetType
            End Get
            Set(ByVal value As Type)
                SetProperty(_navigationTargetType, value, "NavigationTargetType")
            End Set
        End Property
        Public Property Content() As Object
            Get
                Return _content
            End Get
            Set(ByVal value As Object)
                SetProperty(_content, value, "Content")
            End Set
        End Property
        Public Property Icon() As Uri
            Get
                Return _icon
            End Get
            Set(ByVal value As Uri)
                SetProperty(_icon, value, "Icon")
            End Set
        End Property
        Public Property Placement() As HamburgerMenuItemPlacement
            Get
                Return _placement
            End Get
            Set(ByVal value As HamburgerMenuItemPlacement)
                SetProperty(_placement, value, "Placement")
            End Set
        End Property
        Public Sub New()
            Placement = HamburgerMenuItemPlacement.Top
        End Sub

        Friend Shared Function GetIconUriFromName(ByVal name As String) As Uri
            If String.IsNullOrEmpty(name) Then
                Return Nothing
            End If
            Return New Uri(String.Format("pack://application:,,,/WindowsUIDemo;component/Images/Hamburger/{0}.png", name), UriKind.Absolute)
        End Function
    End Class
    Public MustInherit Class HamburgerSubMenuItemViewModelBase
        Inherits HamburgerItemViewModelBase

        Private _moreButtonVisibilityMode As HamburgerSubMenuMoreButtonVisibility = HamburgerSubMenuMoreButtonVisibility.Auto
        Private _isStandaloneSelectionItemMode As Boolean = True

        Public Property MoreButtonVisibilityMode() As HamburgerSubMenuMoreButtonVisibility
            Get
                Return _moreButtonVisibilityMode
            End Get
            Set(ByVal value As HamburgerSubMenuMoreButtonVisibility)
                SetProperty(_moreButtonVisibilityMode, value, "MoreButtonVisibilityMode")
            End Set
        End Property
        Public Property IsStandaloneSelectionItemMode() As Boolean
            Get
                Return _isStandaloneSelectionItemMode
            End Get
            Set(ByVal value As Boolean)
                SetProperty(_isStandaloneSelectionItemMode, value, "IsStandaloneSelectionItemMode")
            End Set
        End Property

        Public Sub New()
            MyBase.New()
            MoreButtonVisibilityMode = HamburgerSubMenuMoreButtonVisibility.Auto
        End Sub
    End Class
    Public Class HamburgerSubMenuItemViewModel
        Inherits HamburgerSubMenuItemViewModelBase

        Private _items As ReadOnlyCollection(Of HamburgerSubItemViewModel)
        Public ReadOnly Property Items() As ReadOnlyCollection(Of HamburgerSubItemViewModel)
            Get
                Return _items
            End Get
        End Property

        Public Sub New(ByVal content As Object, ByVal icon As String, ByVal items As IList(Of HamburgerSubItemViewModel))
            Me.Content = content
            Me.Icon = GetIconUriFromName(icon)
            _items = New ReadOnlyCollection(Of HamburgerSubItemViewModel)(items)
        End Sub
    End Class
    Public Class HamburgerThemeSwitcherItemViewModel
        Inherits HamburgerSubMenuItemViewModelBase

        Public Sub New(ByVal content As Object, ByVal icon As String)
            Me.Content = content
            Me.Icon = GetIconUriFromName(icon)
        End Sub
    End Class
    Public MustInherit Class HamburgerItemWithCommand
        Inherits HamburgerItemViewModelBase

        Private _command As ICommand = Nothing
        Private _commandParameter As Object = Nothing

        Public Property Command() As ICommand
            Get
                Return _command
            End Get
            Set(ByVal value As ICommand)
                SetProperty(_command, value, "Command")
            End Set
        End Property
        Public Property CommandParameter() As Object
            Get
                Return _commandParameter
            End Get
            Set(ByVal value As Object)
                SetProperty(_commandParameter, value, "CommandParameter")
            End Set
        End Property
    End Class

    Public Class HamburgerSubItemViewModel
        Inherits HamburgerItemWithCommand

        Private _rightContent As Object = Nothing
        Private _showInPreview As Boolean = True
        Private _previewContent As Object = Nothing

        Public Property RightContent() As Object
            Get
                Return _rightContent
            End Get
            Set(ByVal value As Object)
                SetProperty(_rightContent, value, "RightContent")
            End Set
        End Property
        Public Property ShowInPreview() As Boolean
            Get
                Return _showInPreview
            End Get
            Set(ByVal value As Boolean)
                SetProperty(_showInPreview, value, "ShowInPreview")
            End Set
        End Property
        Public Property PreviewContent() As Object
            Get
                Return _previewContent
            End Get
            Set(ByVal value As Object)
                SetProperty(_previewContent, value, "PreviewContent")
            End Set
        End Property

        Public Sub New(ByVal content As Object, ByVal rightContent As Object, ByVal navigationTargetType As Type, ByVal showInPreview As Boolean, ByVal parentViewModel As Object)
            Me.Content = content
            Me.RightContent = rightContent
            Me.NavigationTargetType = navigationTargetType
            Me.ShowInPreview = showInPreview
        End Sub
    End Class
    Public Class HamburgerLinkItemViewModel
        Inherits HamburgerItemViewModelBase

        Private _navigationUri As Uri = Nothing

        Public Property NavigationUri() As Uri
            Get
                Return _navigationUri
            End Get
            Set(ByVal value As Uri)
                SetProperty(_navigationUri, value, "NavigationUri")
            End Set
        End Property

        Public Sub New(ByVal content As Object, ByVal navigationUri As Uri)
            Me.Content = content
            Me.NavigationUri = navigationUri
        End Sub
    End Class
    Public Class HamburgerNavigationItemViewModel
        Inherits HamburgerItemWithCommand

        Private _hideMenuWhenSelected As Boolean = False
        Private _selectOnClick As Boolean = True

        Public Property HideMenuWhenSelected() As Boolean
            Get
                Return _hideMenuWhenSelected
            End Get
            Set(ByVal value As Boolean)
                SetProperty(_hideMenuWhenSelected, value, "HideMenuWhenSelected")
            End Set
        End Property
        Public Property SelectOnClick() As Boolean
            Get
                Return _selectOnClick
            End Get
            Set(ByVal value As Boolean)
                SetProperty(_selectOnClick, value, "SelectOnClick")
            End Set
        End Property

        Public Sub New(ByVal content As Object, ByVal icon As String, ByVal navigationTargetType As Type, Optional ByVal command As ICommand = Nothing)
            Me.Content = content
            Me.Icon = GetIconUriFromName(icon)
            Me.NavigationTargetType = navigationTargetType
            Me.Command = command
        End Sub
    End Class
    #End Region
#Region "MainViewModel"
    <POCOViewModel>
    Public Class HamburgerMenuDemoViewModel
        Public Overridable Property FocusedRow() As Message
        Public Overridable Property MailFilter() As String
        Public Overridable Property Header() As String
        Public Overridable Property DateColumnHeader() As String
        Public Overridable Property ShowMenuOnEmptySpaceBarClick() As Boolean
        Public Overridable Property AvailableViewStates() As HamburgerMenuAvailableViewStates
        Public Overridable Property IsFlyoutViewStateEnabled() As Boolean
        Public Overridable Property IsOverlayViewStateEnabled() As Boolean
        Public Overridable Property IsInlineViewStateEnabled() As Boolean

        Public ReadOnly Property Data() As IEnumerable(Of Message)
            Get
                Return MailItems.Messages
            End Get
        End Property
        Private privateItems As ReadOnlyCollection(Of HamburgerItemViewModelBase)
        Public Property Items() As ReadOnlyCollection(Of HamburgerItemViewModelBase)
            Get
                Return privateItems
            End Get
            Private Set(ByVal value As ReadOnlyCollection(Of HamburgerItemViewModelBase))
                privateItems = value
            End Set
        End Property
        Private privateBottomBarItems As ReadOnlyCollection(Of BottomBarItemViewModelBase)
        Public Property BottomBarItems() As ReadOnlyCollection(Of BottomBarItemViewModelBase)
            Get
                Return privateBottomBarItems
            End Get
            Private Set(ByVal value As ReadOnlyCollection(Of BottomBarItemViewModelBase))
                privateBottomBarItems = value
            End Set
        End Property
        Private privateCompactFilterItems As ObservableCollection(Of CompactModeFilterItem)
        Public Property CompactFilterItems() As ObservableCollection(Of CompactModeFilterItem)
            Get
                Return privateCompactFilterItems
            End Get
            Private Set(ByVal value As ObservableCollection(Of CompactModeFilterItem))
                privateCompactFilterItems = value
            End Set
        End Property

        Public Sub New()
            ShowMenuOnEmptySpaceBarClick = True
            IsInlineViewStateEnabled = True
            IsOverlayViewStateEnabled = True
            IsFlyoutViewStateEnabled = True
            CompactFilterItems = New ObservableCollection(Of CompactModeFilterItem)() From {
                New CompactModeFilterItem() With {.DisplayValue = "All"},
                New CompactModeFilterItem() With {.DisplayValue = "Unread", .EditValue = "[IsUnread] = True"}
            }
            Items = New ReadOnlyCollection(Of HamburgerItemViewModelBase)(CreateMenuItems())
            BottomBarItems = New ReadOnlyCollection(Of BottomBarItemViewModelBase)(CreateBottomBarItems())
            If Data IsNot Nothing AndAlso Data.Count() > 0 Then
                FocusedRow = Data.First()
            End If
            UpdateMailFilter(MailType.Inbox)
        End Sub

        Private Function CreateBottomBarItems() As IList(Of BottomBarItemViewModelBase)


            Dim items_Renamed = New List(Of BottomBarItemViewModelBase)()

            Return items_Renamed
        End Function

        Protected Sub OnIsFlyoutViewStateEnabledChanged(ByVal oldValue As Boolean)
            If (Not IsFlyoutViewStateEnabled) AndAlso IsAvailableViewStateEmpty() Then
                IsFlyoutViewStateEnabled = True
                Return
            End If
            UpdateAvailableViewState()
        End Sub
        Protected Sub OnIsOverlayViewStateEnabledChanged(ByVal oldValue As Boolean)
            If (Not IsOverlayViewStateEnabled) AndAlso IsAvailableViewStateEmpty() Then
                IsOverlayViewStateEnabled = True
                Return
            End If
            UpdateAvailableViewState()
        End Sub
        Protected Sub OnIsInlineViewStateEnabledChanged(ByVal oldValue As Boolean)
            If (Not IsInlineViewStateEnabled) AndAlso IsAvailableViewStateEmpty() Then
                IsInlineViewStateEnabled = True
                Return
            End If
            UpdateAvailableViewState()
        End Sub

        Private Sub UpdateMailFilter(ByVal mailType As MailType)
            MailFilter = "[MailType] = '" & mailType.ToString() & "'"
            Select Case mailType
                Case WindowsUIDemo.MailType.Deleted
                    DateColumnHeader = "Deleted"
                    Header = "Trash"
                Case WindowsUIDemo.MailType.Sent
                    DateColumnHeader = "Sent"
                    Header = "Sent"
                Case WindowsUIDemo.MailType.Draft
                    DateColumnHeader = "Created"
                    Header = "Drafts"
                Case Else
                    DateColumnHeader = "Recieved"
                    Header = "Inbox"
            End Select
            If CompactFilterItems IsNot Nothing Then
                CompactFilterItems.First().EditValue = MailFilter
                CompactFilterItems(1).EditValue = MailFilter & " And [IsUnread] = True"
            End If
            If Data IsNot Nothing AndAlso Data.Count() > 0 Then
                FocusedRow = Data.Where(Function(p) p.MailType = mailType).First()
            End If
        End Sub
        Private Function GetMailCountByType(ByVal type As MailType) As Integer
            Return Data.Where(Function(p) p.MailType = type).Count()
        End Function
        Private Function CreateMenuItems() As IList(Of HamburgerItemViewModelBase)
            Dim command = New DelegateCommand(Of MailType)(Sub(t) UpdateMailFilter(t))
            Dim subItems = New List(Of HamburgerSubItemViewModel)() From {
                New HamburgerSubItemViewModel("Inbox", GetMailCountByType(MailType.Inbox), Nothing, True, Me) With {.Command = command, .CommandParameter = MailType.Inbox},
                New HamburgerSubItemViewModel("Drafts", GetMailCountByType(MailType.Draft), Nothing, True, Me) With {.Command = command, .CommandParameter = MailType.Draft},
                New HamburgerSubItemViewModel("Sent", GetMailCountByType(MailType.Sent), Nothing, True, Me) With {.Command = command, .CommandParameter = MailType.Sent},
                New HamburgerSubItemViewModel("Trash", GetMailCountByType(MailType.Deleted), Nothing, False, Me) With {.Command = command, .CommandParameter = MailType.Deleted}
            }

            Dim items_Renamed = New List(Of HamburgerItemViewModelBase)() From {
                New HamburgerSubMenuItemViewModel("Folders", "Folders", subItems), New HamburgerLinkItemViewModel("Additional Information", New Uri("https://documentation.devexpress.com/")) With {.Placement = HamburgerMenuItemPlacement.Bottom},
                New HamburgerThemeSwitcherItemViewModel("Color Scheme", "ThemeSelector") With {.IsStandaloneSelectionItemMode = True, .MoreButtonVisibilityMode = HamburgerSubMenuMoreButtonVisibility.Hidden}
            }
            Return items_Renamed
        End Function
        Private Sub UpdateAvailableViewState()
            Dim state As HamburgerMenuAvailableViewStates = CType(0, HamburgerMenuAvailableViewStates)
            If IsInlineViewStateEnabled Then
                state = state Or HamburgerMenuAvailableViewStates.Inline
            End If
            If IsOverlayViewStateEnabled Then
                state = state Or HamburgerMenuAvailableViewStates.Overlay
            End If
            If IsFlyoutViewStateEnabled Then
                state = state Or HamburgerMenuAvailableViewStates.Flyout
            End If
            AvailableViewStates = state
        End Sub
        Private Function IsAvailableViewStateEmpty() As Boolean
            Return (Not IsInlineViewStateEnabled) AndAlso (Not IsOverlayViewStateEnabled) AndAlso Not IsFlyoutViewStateEnabled
        End Function
    End Class
#End Region

End Namespace
