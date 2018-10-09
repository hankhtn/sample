Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports DevExpress.Xpf.DemoBase
Imports System.Windows.Controls
Imports DevExpress.Xpf.Carousel
Imports System.Collections.Generic
Imports System.Windows.Markup
Imports System.Windows.Data
Imports System.Windows.Media

Namespace CarouselDemo
    Partial Public Class DXBook
        Inherits CarouselDemoModule

        Public Sub New()
            InitializeComponent()
            carouselItemsControl.ItemsSource = CreateItems("/CarouselDemo;component/Data/Images/Logos", ItemType.BinaryImage)
        End Sub
        Private Sub Grid_PreviewMouseDown(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim item As DependencyObject = TryCast(e.Source, DependencyObject)
            Do While item IsNot Nothing
                Dim cp As CarouselPanel = TryCast(VisualTreeHelper.GetParent(item), CarouselPanel)
                If cp IsNot Nothing AndAlso cp.Children.IndexOf(TryCast(item, UIElement)) = cp.Children.IndexOf(cp.ActiveItem) Then
                    cp.Move(-1)
                End If
                item = VisualTreeHelper.GetParent(item)
            Loop
        End Sub
    End Class
End Namespace
