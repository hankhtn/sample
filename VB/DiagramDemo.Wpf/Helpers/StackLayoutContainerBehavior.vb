Imports DevExpress.Diagram.Core
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Diagram
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Data

Namespace DiagramDemo
    Public Class StackLayoutDiagramContainerBehavior
        Inherits Behavior(Of DiagramContainer)

        Private Shared ReadOnly PaddingProperty As DependencyProperty = DependencyProperty.Register("Padding", GetType(Thickness), GetType(StackLayoutDiagramContainerBehavior), New PropertyMetadata(New Thickness(), Sub(d, e) CType(d, StackLayoutDiagramContainerBehavior).OnPaddingChanged(CType(e.OldValue, Thickness), CType(e.NewValue, Thickness))))


        Private diagram_Renamed As DiagramControl
        Private Property Diagram() As DiagramControl
            Get
                Return diagram_Renamed
            End Get
            Set(ByVal value As DiagramControl)
                If diagram_Renamed Is value Then
                    Return
                End If
                If diagram_Renamed IsNot Nothing Then
                    DetachFromDiagram()
                End If
                diagram_Renamed = value
                If diagram_Renamed IsNot Nothing Then
                    AttachToDiagram()
                End If
            End Set
        End Property
        Private Sub AttachToDiagram()
            AddHandler Diagram.ItemBoundsChanged, AddressOf OnDiagramItemBoundsChanged
            AddHandler Diagram.ItemsResizing, AddressOf OnDiagramItemsResizing
            Dim first As Boolean = True
            For Each item In AssociatedObject.Items
                AddChild(item, first)
                first = False
            Next item
            BindingOperations.SetBinding(Me, PaddingProperty, New Binding() With {.Path = New PropertyPath(DiagramContainer.ActualPaddingProperty), .Source = AssociatedObject, .Mode = BindingMode.OneWay})
            diagram_Renamed.UpdateConnectorsRoute(AssociatedObject.Items.SelectMany(Function(x) x.OutgoingConnectors.Concat(x.IncomingConnectors)).Distinct().Cast(Of DiagramConnector)())
        End Sub
        Private Sub DetachFromDiagram()
            BindingOperations.ClearBinding(Me, PaddingProperty)
            RemoveHandler Diagram.ItemsResizing, AddressOf OnDiagramItemsResizing
            RemoveHandler Diagram.ItemBoundsChanged, AddressOf OnDiagramItemBoundsChanged
        End Sub
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AssociatedObject.MinHeight = 0.0
            AssociatedObject.Height = 0.0
            AddHandler AssociatedObject.Loaded, AddressOf OnAssociatedObjectLoadedOrUloaded
            AddHandler AssociatedObject.Unloaded, AddressOf OnAssociatedObjectLoadedOrUloaded
            UpdateDiagram()
        End Sub
        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.Loaded, AddressOf OnAssociatedObjectLoadedOrUloaded
            RemoveHandler AssociatedObject.Unloaded, AddressOf OnAssociatedObjectLoadedOrUloaded
            Diagram = Nothing
        End Sub
        Private Sub OnAssociatedObjectLoadedOrUloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateDiagram()
        End Sub
        Private Sub UpdateDiagram()
            Diagram = AssociatedObject.GetDiagram()
        End Sub
        Private Sub AddChild(ByVal child As DiagramItem, ByVal first As Boolean)
            Dim padding = CType(GetValue(PaddingProperty), Thickness)
            If first Then
                AssociatedObject.Height = padding.Top + padding.Bottom
            End If
            child.Width = Math.Max(AssociatedObject.MinWidth, AssociatedObject.Width) - padding.Left - padding.Right
            child.Anchors = child.Anchors Or Sides.Left Or Sides.Right
            child.Position = New Point(0.0, AssociatedObject.Height - padding.Top - padding.Bottom)
            AssociatedObject.Height += Math.Max(child.MinHeight,If(Double.IsNaN(child.Height), child.ActualHeight, child.Height))
        End Sub
        Private Sub OnDiagramItemBoundsChanged(ByVal sender As Object, ByVal e As DiagramItemBoundsChangedEventArgs)
            If Equals(e.NewParent, AssociatedObject) AndAlso (Not Equals(e.OldParent, AssociatedObject)) Then
                AddChild(e.Item, e.NewIndex = 0)
            ElseIf Equals(e.OldParent, AssociatedObject) AndAlso (Not Equals(e.NewParent, AssociatedObject)) Then
                AssociatedObject.Height -= e.OldSize.Height
                For Each child In AssociatedObject.Items.Skip(e.OldIndex)
                    child.Position = New Point(child.Position.X, child.Position.Y - e.OldSize.Height)
                Next child
            ElseIf Equals(e.OldParent, AssociatedObject) AndAlso Equals(e.NewParent, AssociatedObject) Then
                Dim delta = e.NewSize.Height - e.OldSize.Height
                If delta = 0.0 Then
                    Return
                End If
                AssociatedObject.Height += delta
                For Each child In AssociatedObject.Items.SkipWhile(Function(x) (Not Equals(x, e.Item))).Skip(1)
                    child.Position = New Point(child.Position.X, child.Position.Y + delta)
                Next child
            End If
        End Sub
        Private Sub OnPaddingChanged(ByVal oldValue As Thickness, ByVal newValue As Thickness)
            AssociatedObject.Height = Math.Max(0, AssociatedObject.Height + newValue.Top + newValue.Bottom - oldValue.Top - oldValue.Bottom)
            AssociatedObject.Width = Math.Max(0, AssociatedObject.Width + newValue.Left + newValue.Right - oldValue.Left - oldValue.Right)
        End Sub
        Private Sub OnDiagramItemsResizing(ByVal sender As Object, ByVal e As DiagramItemsResizingEventArgs)
            Dim item = e.Items.FirstOrDefault(Function(x) Equals(x.Item, AssociatedObject))
            If item IsNot Nothing Then
                item.NewSize = New Size(item.NewSize.Width, item.OldSize.Height)
            End If
        End Sub
    End Class
End Namespace
