Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports DevExpress.Xpf.DemoBase
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System
Imports DevExpress.Xpf.Carousel
Imports System.Windows.Markup
Imports System.Collections

Namespace CarouselDemo
    Partial Public Class AnimationEffects
        Inherits CarouselDemoModule

        Public Sub New()
            InitializeComponent()
            AddItems("/CarouselDemo;component/Data/Images/Logos", ItemType.BinaryImage, carousel)
        End Sub
    End Class
End Namespace
