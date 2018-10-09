Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Printing

Namespace TreeListDemo
    Public Class TreeListDemoModule
        Inherits DemoModule

        Private Shared ReadOnly TreeListControlPropertyKey As DependencyPropertyKey
        Public Shared ReadOnly TreeListControlProperty As DependencyProperty
        Shared Sub New()
            NWindContext.Preload()
            TreeListControlPropertyKey = DependencyProperty.RegisterReadOnly("TreeListControl", GetType(TreeListControl), GetType(TreeListDemoModule), New PropertyMetadata(Nothing))
            TreeListControlProperty = TreeListControlPropertyKey.DependencyProperty
        End Sub

        Public Property TreeListControl() As TreeListControl
            Get
                Return CType(GetValue(TreeListControlProperty), TreeListControl)
            End Get
            Protected Set(ByVal value As TreeListControl)
                SetValue(TreeListControlPropertyKey, value)
            End Set
        End Property
        Protected Overrides Sub HidePopupContent()
            If TreeListControl IsNot Nothing Then
                TreeListControl.View.HideColumnChooser()
            End If
            MyBase.HidePopupContent()
        End Sub
        Protected Overridable ReadOnly Property ShowBorder() As Boolean
            Get
                Return False
            End Get
        End Property
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            If TreeListControl Is Nothing Then
                TreeListControl = If(TryCast(Content, TreeListControl), LayoutTreeHelper.GetVisualChildren(CType(Content, DependencyObject)).OfType(Of TreeListControl)().FirstOrDefault())

            End If
            If OptionsDataContext Is Nothing Then
                OptionsDataContext = TreeListControl
            End If
            If TreeListControl IsNot Nothing Then
                TreeListControl.ShowBorder = ShowBorder
            End If
        End Sub
    End Class

    Public Class PrintTreeListDemoModule
        Inherits TreeListDemoModule

        Private dxTabControlCore As DXTabControl
        Public Sub New()
            ShowPrintPreview = New DelegateCommand(Of Object)(Sub(p) OnShowPrintPreview(CStr(p)))
            ShowPrintPreviewInNewTab = New DelegateCommand(Of Object)(Sub(p) OnShowPrintPreviewInNewTab(CStr(p)))
        End Sub
        Private privateShowPrintPreview As ICommand
        Public Property ShowPrintPreview() As ICommand
            Get
                Return privateShowPrintPreview
            End Get
            Private Set(ByVal value As ICommand)
                privateShowPrintPreview = value
            End Set
        End Property
        Private privateShowPrintPreviewInNewTab As ICommand
        Public Property ShowPrintPreviewInNewTab() As ICommand
            Get
                Return privateShowPrintPreviewInNewTab
            End Get
            Private Set(ByVal value As ICommand)
                privateShowPrintPreviewInNewTab = value
            End Set
        End Property
        Public Property DXTabControl() As DXTabControl
            Get
                Return dxTabControlCore
            End Get
            Set(ByVal value As DXTabControl)
                If DXTabControl Is value Then
                    Return
                End If
                If DXTabControl IsNot Nothing Then
                    RemoveHandler DXTabControl.TabHidden, AddressOf OnTabHidden
                End If
                dxTabControlCore = value
                TreeListControl = If(dxTabControlCore Is Nothing, Nothing, CType(CType(dxTabControlCore.Items(0), DXTabItem).Content, TreeListControl))
                OptionsDataContext = TreeListControl
                If DXTabControl IsNot Nothing Then
                    AddHandler DXTabControl.TabHidden, AddressOf OnTabHidden
                End If
                If TreeListControl IsNot Nothing Then
                    TreeListControl.ShowBorder = ShowBorder
                End If
            End Set
        End Property
        Protected ReadOnly Property View() As TreeListView
            Get
                Return TreeListControl.View
            End Get
        End Property
        Protected Overridable Sub OnShowPrintPreview(ByVal documentName As String)
            PrintHelper.ShowPrintPreviewDialog(LayoutHelper.FindParentObject(Of Window)(Me), DirectCast(TreeListControl.View, IPrintableControl), documentName)
        End Sub
        Protected Overridable Sub OnShowPrintPreviewInNewTab(ByVal documentName As String)
            Dim link As New PrintableControlLink(DirectCast(TreeListControl.View, IPrintableControl))
            Dim preview As New DocumentPreviewControl() With {.DocumentSource = link}
            Dim tabItem As DXTabItem = CreateTabItem(preview, documentName)
            tabItem.Tag = link
            DXTabControl.Items.Add(tabItem)
            DXTabControl.SelectedItem = tabItem
            link.CreateDocument(True)
        End Sub
        Protected Overridable Function CreateTabItem(ByVal preview As DocumentPreviewControl, ByVal documentName As String) As DXTabItem
            Return New DXTabItem() With {.AllowHide = DefaultBoolean.True, .Content = preview, .Header = documentName}
        End Function
        Protected Sub RemoveTab(ByVal tabItem As DXTabItem)
            If tabItem.Tag IsNot Nothing Then
                CType(tabItem.Tag, PrintableControlLink).Dispose()
            End If
            tabItem.Content = Nothing
            DXTabControl.Items.Remove(tabItem)
        End Sub
        Protected Overrides Sub Clear()
            MyBase.Clear()
            For i As Integer = DXTabControl.Items.Count - 1 To 0 Step -1
                RemoveTab(CType(DXTabControl.Items(i), DXTabItem))
            Next i
            DXTabControl = Nothing
        End Sub
        Private Sub OnTabHidden(ByVal sender As Object, ByVal e As TabControlTabHiddenEventArgs)
            RemoveTab(CType(DXTabControl.Items(e.TabIndex), DXTabItem))
        End Sub
    End Class
End Namespace
