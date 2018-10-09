Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports DevExpress.Xpf.DemoBase
Imports System.Windows.Controls
Imports System.Windows.Media
Imports DevExpress.Xpf.Carousel
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace CarouselDemo
    Partial Public Class PhotoGallery
        Inherits CarouselDemoModule

        Public Sub New()
            InitializeComponent()
            AddItems("/CarouselDemo;component/Data/Images/Photos", ItemType.BinaryImage, carousel)
        End Sub
        Protected Overrides Sub AddItems(ByVal path As String, ByVal it As ItemType, ByVal carousel As CarouselPanel)
            Dim items = CreateItems(path, it)
            For Each item In items
                Dim control As New ContentControl()
                control.Template = TryCast(carousel.Resources("itemTemplate"), ControlTemplate)
                control.Style = TryCast(carousel.Resources("itemStyle"), Style)
                control.DataContext = CType(item, Image).Source
                carousel.Children.Add(control)
            Next item
        End Sub
    End Class
End Namespace
