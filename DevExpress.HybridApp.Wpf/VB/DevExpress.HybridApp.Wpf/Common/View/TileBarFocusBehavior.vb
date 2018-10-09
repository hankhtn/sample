Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Navigation

Namespace DevExpress.DevAV.Common.View
    Public Class TileBarFocusBehavior
        Inherits Behavior(Of TileBar)

        Private Shared ReadOnly SelectedItemProperty As DependencyProperty = DependencyProperty.Register("SelectedItem", GetType(DevAVDbModuleDescription), GetType(TileBarFocusBehavior), New FrameworkPropertyMetadata(Nothing, Sub(d, e) CType(d, TileBarFocusBehavior).OnSelectedItemChanged(e)))
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            BindingOperations.SetBinding(Me, SelectedItemProperty, New Binding("SelectedItem") With {.Source = Me.AssociatedObject, .Mode = BindingMode.OneWay})
        End Sub
        Protected Overrides Sub OnDetaching()
            BindingOperations.ClearBinding(Me, SelectedItemProperty)
            MyBase.OnDetaching()
        End Sub
        Private Sub OnSelectedItemChanged(ByVal e As DependencyPropertyChangedEventArgs)
            Dim uiElement As UIElement = If(e.NewValue Is Nothing, Nothing, (CType(Me.AssociatedObject.ItemContainerGenerator.ContainerFromItem(e.NewValue), UIElement)))
            If uiElement IsNot Nothing Then
                uiElement.Focus()
            End If
        End Sub
    End Class
End Namespace
