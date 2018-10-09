Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Ribbon
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media

Namespace RibbonDemo
    Friend Class RibbonOptionsBehavior
        Inherits Behavior(Of RibbonControl)

        Public Shared ReadOnly CurrentToolBarShowModeProperty As DependencyProperty
        Public Shared ReadOnly CurrentPageCategoryAlignmentProperty As DependencyProperty
        Public Shared ReadOnly CurrentTitleBarVisibilityProperty As DependencyProperty

        Shared Sub New()
            CurrentToolBarShowModeProperty = DependencyProperty.Register("CurrentToolBarShowMode", GetType(RibbonQuickAccessToolbarShowMode), GetType(RibbonOptionsBehavior), New PropertyMetadata(RibbonQuickAccessToolbarShowMode.Default, Sub(d, e) CType(d, RibbonOptionsBehavior).Update()))
            CurrentPageCategoryAlignmentProperty = DependencyProperty.Register("CurrentPageCategoryAlignmentProperty", GetType(RibbonPageCategoryCaptionAlignment), GetType(RibbonOptionsBehavior), New PropertyMetadata(RibbonPageCategoryCaptionAlignment.Left, Sub(d, e) CType(d, RibbonOptionsBehavior).Update()))
            CurrentTitleBarVisibilityProperty = DependencyProperty.Register("CurrentTitleBarVisibilityProperty", GetType(RibbonTitleBarVisibility), GetType(RibbonOptionsBehavior), New PropertyMetadata(RibbonTitleBarVisibility.Auto, Sub(d, e) CType(d, RibbonOptionsBehavior).Update()))
        End Sub
        Public Property CurrentTitleBarVisibility() As RibbonTitleBarVisibility
            Get
                Return CType(GetValue(CurrentTitleBarVisibilityProperty), RibbonTitleBarVisibility)
            End Get
            Set(ByVal value As RibbonTitleBarVisibility)
                SetValue(CurrentTitleBarVisibilityProperty, value)
            End Set
        End Property
        Public Property CurrentToolBarShowMode() As RibbonQuickAccessToolbarShowMode
            Get
                Return CType(GetValue(CurrentToolBarShowModeProperty), RibbonQuickAccessToolbarShowMode)
            End Get
            Set(ByVal value As RibbonQuickAccessToolbarShowMode)
                SetValue(CurrentToolBarShowModeProperty, value)
            End Set
        End Property
        Public Property CurrentPageCategoryAlignment() As RibbonPageCategoryCaptionAlignment
            Get
                Return CType(GetValue(CurrentPageCategoryAlignmentProperty), RibbonPageCategoryCaptionAlignment)
            End Get
            Set(ByVal value As RibbonPageCategoryCaptionAlignment)
                SetValue(CurrentPageCategoryAlignmentProperty, value)
            End Set
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            BindingOperations.SetBinding(Me, CurrentToolBarShowModeProperty, New Binding() With {.Source = AssociatedObject, .Path = New PropertyPath(RibbonControl.ToolbarShowModeProperty)})
            BindingOperations.SetBinding(Me, CurrentTitleBarVisibilityProperty, New Binding() With {.Source = AssociatedObject, .Path = New PropertyPath(RibbonControl.PageCategoryAlignmentProperty)})
            BindingOperations.SetBinding(Me, CurrentPageCategoryAlignmentProperty, New Binding() With {.Source = AssociatedObject, .Path = New PropertyPath(RibbonControl.PageCategoryAlignmentProperty)})
        End Sub
        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            ClearValue(CurrentToolBarShowModeProperty)
            ClearValue(CurrentTitleBarVisibilityProperty)
            ClearValue(CurrentPageCategoryAlignmentProperty)
        End Sub

        Private Sub Update()
            If (Not IsAttached) OrElse AssociatedObject.ActualMergedParent Is Nothing Then
                Return
            End If
            AssociatedObject.ActualMergedParent.ToolbarShowMode = CurrentToolBarShowMode
            AssociatedObject.ActualMergedParent.PageCategoryAlignment = CurrentPageCategoryAlignment
            AssociatedObject.ActualMergedParent.RibbonTitleBarVisibility = CurrentTitleBarVisibility
        End Sub
    End Class
End Namespace
