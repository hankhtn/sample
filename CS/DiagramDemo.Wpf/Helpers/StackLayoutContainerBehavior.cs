using DevExpress.Diagram.Core;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DiagramDemo {
    public class StackLayoutDiagramContainerBehavior : Behavior<DiagramContainer> {
        static readonly DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(StackLayoutDiagramContainerBehavior), new PropertyMetadata(new Thickness(), (d, e) => ((StackLayoutDiagramContainerBehavior)d).OnPaddingChanged((Thickness)e.OldValue, (Thickness)e.NewValue)));

        DiagramControl diagram;
        DiagramControl Diagram {
            get { return diagram; }
            set {
                if(diagram == value) return;
                if(diagram != null)
                    DetachFromDiagram();
                diagram = value;
                if(diagram != null)
                    AttachToDiagram();
            }
        }
        void AttachToDiagram() {
            Diagram.ItemBoundsChanged += OnDiagramItemBoundsChanged;
            Diagram.ItemsResizing += OnDiagramItemsResizing;
            bool first = true;
            foreach(var item in AssociatedObject.Items) {
                AddChild(item, first);
                first = false;
            }
            BindingOperations.SetBinding(this, PaddingProperty, new Binding() { Path = new PropertyPath(DiagramContainer.ActualPaddingProperty), Source = AssociatedObject, Mode = BindingMode.OneWay });
            diagram.UpdateConnectorsRoute(AssociatedObject.Items.SelectMany(x => x.OutgoingConnectors.Concat(x.IncomingConnectors)).Distinct().Cast<DiagramConnector>());
        }
        void DetachFromDiagram() {
            BindingOperations.ClearBinding(this, PaddingProperty);
            Diagram.ItemsResizing -= OnDiagramItemsResizing;
            Diagram.ItemBoundsChanged -= OnDiagramItemBoundsChanged;
        }
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.MinHeight = 0.0;
            AssociatedObject.Height = 0.0;
            AssociatedObject.Loaded += OnAssociatedObjectLoadedOrUloaded;
            AssociatedObject.Unloaded += OnAssociatedObjectLoadedOrUloaded;
            UpdateDiagram();
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.Loaded -= OnAssociatedObjectLoadedOrUloaded;
            AssociatedObject.Unloaded -= OnAssociatedObjectLoadedOrUloaded;
            Diagram = null;
        }
        void OnAssociatedObjectLoadedOrUloaded(object sender, RoutedEventArgs e) {
            UpdateDiagram();
        }
        void UpdateDiagram() {
            Diagram = AssociatedObject.GetDiagram();
        }
        void AddChild(DiagramItem child, bool first) {
            var padding = (Thickness)GetValue(PaddingProperty);
            if(first)
                AssociatedObject.Height = padding.Top + padding.Bottom;
            child.Width = Math.Max(AssociatedObject.MinWidth, AssociatedObject.Width) - padding.Left - padding.Right;
            child.Anchors |= Sides.Left | Sides.Right;
            child.Position = new Point(0.0, AssociatedObject.Height - padding.Top - padding.Bottom);
            AssociatedObject.Height += Math.Max(child.MinHeight, double.IsNaN(child.Height) ? child.ActualHeight : child.Height);
        }
        void OnDiagramItemBoundsChanged(object sender, DiagramItemBoundsChangedEventArgs e) {
            if(Equals(e.NewParent, AssociatedObject) && !Equals(e.OldParent, AssociatedObject))
                AddChild(e.Item, e.NewIndex == 0);
            else if(Equals(e.OldParent, AssociatedObject) && !Equals(e.NewParent, AssociatedObject)) {
                AssociatedObject.Height -= e.OldSize.Height;
                foreach(var child in AssociatedObject.Items.Skip(e.OldIndex))
                    child.Position = new Point(child.Position.X, child.Position.Y - e.OldSize.Height);
            } else if(Equals(e.OldParent, AssociatedObject) && Equals(e.NewParent, AssociatedObject)) {
                var delta = e.NewSize.Height - e.OldSize.Height;
                if(delta == 0.0) return;
                AssociatedObject.Height += delta;
                foreach(var child in AssociatedObject.Items.SkipWhile(x => !Equals(x, e.Item)).Skip(1))
                    child.Position = new Point(child.Position.X, child.Position.Y + delta);
            }
        }
        void OnPaddingChanged(Thickness oldValue, Thickness newValue) {
            AssociatedObject.Height = Math.Max(0, AssociatedObject.Height + newValue.Top + newValue.Bottom - oldValue.Top - oldValue.Bottom);
            AssociatedObject.Width = Math.Max(0, AssociatedObject.Width + newValue.Left + newValue.Right - oldValue.Left - oldValue.Right);
        }
        void OnDiagramItemsResizing(object sender, DiagramItemsResizingEventArgs e) {
            var item = e.Items.FirstOrDefault(x => Equals(x.Item, AssociatedObject));
            if(item != null)
                item.NewSize = new Size(item.NewSize.Width, item.OldSize.Height);
        }
    }
}
