Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo
    Partial Public Class pageDockLayoutControl
        Inherits LayoutControlDemoModule

        Private _LastClientItemZIndex As Integer

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub DockRadioButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim item = CType(PropertiesControl.SelectedItem, FrameworkElement)
            If DockLayoutControl.GetDock(item) = DevExpress.Xpf.LayoutControl.Dock.None OrElse DockLayoutControl.GetDock(item) = DevExpress.Xpf.LayoutControl.Dock.Client Then
                _LastClientItemZIndex += 1
                item.SetZIndex(_LastClientItemZIndex)
            Else
                item.SetZIndex(0)
            End If
            If DockLayoutControl.GetDock(item) = DevExpress.Xpf.LayoutControl.Dock.Client Then
                item.ClearValue(WidthProperty)
                item.ClearValue(HeightProperty)
            End If
        End Sub
    End Class
End Namespace
