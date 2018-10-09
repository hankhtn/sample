Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Utils
Imports System.Windows

Namespace BarsDemo
    Partial Public Class ContainerItems
        Inherits BarsDemoModule

        Public Shared ReadOnly BarLinkContainerItemProperty As DependencyProperty = DependencyPropertyManager.Register("BarLinkContainerItem", GetType(BarLinkContainerItem), GetType(ContainerItems), New FrameworkPropertyMetadata(Nothing))
        Public Shared ReadOnly ToolbarListItemProperty As DependencyProperty = DependencyPropertyManager.Register("ToolbarListItem", GetType(ToolbarListItem), GetType(ContainerItems), New FrameworkPropertyMetadata(Nothing))
        Public Shared ReadOnly NewItemProperty As DependencyProperty = DependencyPropertyManager.Register("NewItem", GetType(BarButtonItem), GetType(ContainerItems), New FrameworkPropertyMetadata(Nothing))
        Public Shared ReadOnly OpenItemProperty As DependencyProperty = DependencyPropertyManager.Register("OpenItem", GetType(BarButtonItem), GetType(ContainerItems), New FrameworkPropertyMetadata(Nothing))
        Public Shared ReadOnly CloseItemProperty As DependencyProperty = DependencyPropertyManager.Register("CloseItem", GetType(BarButtonItem), GetType(ContainerItems), New FrameworkPropertyMetadata(Nothing))
        Public Shared ReadOnly SaveItemProperty As DependencyProperty = DependencyPropertyManager.Register("SaveItem", GetType(BarButtonItem), GetType(ContainerItems), New FrameworkPropertyMetadata(Nothing))
        Public Shared ReadOnly SaveAsItemProperty As DependencyProperty = DependencyPropertyManager.Register("SaveAsItem", GetType(BarButtonItem), GetType(ContainerItems), New FrameworkPropertyMetadata(Nothing))
        Public Shared ReadOnly PrintItemProperty As DependencyProperty = DependencyPropertyManager.Register("PrintItem", GetType(BarButtonItem), GetType(ContainerItems), New FrameworkPropertyMetadata(Nothing))
        Public Property BarLinkContainerItem() As BarLinkContainerItem
            Get
                Return CType(GetValue(BarLinkContainerItemProperty), BarLinkContainerItem)
            End Get
            Set(ByVal value As BarLinkContainerItem)
                SetValue(BarLinkContainerItemProperty, value)
            End Set
        End Property
        Public Property ToolbarListItem() As ToolbarListItem
            Get
                Return CType(GetValue(ToolbarListItemProperty), ToolbarListItem)
            End Get
            Set(ByVal value As ToolbarListItem)
                SetValue(ToolbarListItemProperty, value)
            End Set
        End Property
        Public Property NewItem() As BarButtonItem
            Get
                Return CType(GetValue(NewItemProperty), BarButtonItem)
            End Get
            Set(ByVal value As BarButtonItem)
                SetValue(NewItemProperty, value)
            End Set
        End Property
        Public Property OpenItem() As BarButtonItem
            Get
                Return CType(GetValue(OpenItemProperty), BarButtonItem)
            End Get
            Set(ByVal value As BarButtonItem)
                SetValue(OpenItemProperty, value)
            End Set
        End Property
        Public Property CloseItem() As BarButtonItem
            Get
                Return CType(GetValue(CloseItemProperty), BarButtonItem)
            End Get
            Set(ByVal value As BarButtonItem)
                SetValue(CloseItemProperty, value)
            End Set
        End Property
        Public Property SaveItem() As BarButtonItem
            Get
                Return CType(GetValue(SaveItemProperty), BarButtonItem)
            End Get
            Set(ByVal value As BarButtonItem)
                SetValue(SaveItemProperty, value)
            End Set
        End Property
        Public Property SaveAsItem() As BarButtonItem
            Get
                Return CType(GetValue(SaveAsItemProperty), BarButtonItem)
            End Get
            Set(ByVal value As BarButtonItem)
                SetValue(SaveAsItemProperty, value)
            End Set
        End Property
        Public Property PrintItem() As BarButtonItem
            Get
                Return CType(GetValue(PrintItemProperty), BarButtonItem)
            End Get
            Set(ByVal value As BarButtonItem)
                SetValue(PrintItemProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            NewItem = bNew
            OpenItem = bOpen
            CloseItem = bClose
            SaveItem = bSave
            SaveAsItem = bSaveAs
            PrintItem = bPrint
            BarLinkContainerItem = lcStandard
            ToolbarListItem = toolbarListItemCore
            DataContext = Me
        End Sub
    End Class
End Namespace
