Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports DevExpress.Xpf.DemoBase
Imports System.Windows.Controls
Imports DevExpress.Xpf.Carousel
Imports System.Collections.Generic
Imports System.Windows.Shapes
Imports System.Windows.Media

Namespace CarouselDemo
    Partial Public Class VisibleItemCount
        Inherits CarouselDemoModule

        Public Sub New()
            InitializeComponent()
            itemsControl.ItemsSource = CreateItems("/CarouselDemo;component/Data/Images/Icons", ItemType.BinaryImage)
        End Sub
    End Class
End Namespace
