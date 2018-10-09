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
Imports System.Windows.Threading
Imports DevExpress.Xpf.Editors

Namespace CarouselDemo
    Partial Public Class PositionEffects
        Inherits CarouselDemoModule

        Public Sub New()
            InitializeComponent()
            AddItems("/CarouselDemo;component/Data/Images/Logos", ItemType.BinaryImage, carousel)
        End Sub
        Private fixedF As Boolean = False
        Protected Overrides Function ArrangeOverride(ByVal arrangeBounds As Size) As Size
            Dim result As Size = MyBase.ArrangeOverride(arrangeBounds)
            If Not fixedF Then
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, New PrimeDelegate(AddressOf UpdateBindings))
                fixedF = Not fixedF
            End If
            Return result
        End Function
        Protected Delegate Sub PrimeDelegate()
        Protected Sub UpdateBindings()
            UpdateBinding(scaleListBox)
            UpdateBinding(angleListBox)
            UpdateBinding(opacityListBox)
        End Sub
        Protected Sub UpdateBinding(ByVal cb As ListBoxEdit)
            cb.SelectedIndex = 1
        End Sub
    End Class
End Namespace
