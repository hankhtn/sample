Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports DevExpress.Xpf.DemoBase
Imports System.Windows.Controls
Imports DevExpress.Xpf.Carousel
Imports System.Windows.Media
Imports System.Windows.Markup
Imports System.Windows.Data
Imports System
Imports DevExpress.Xpf.Editors

Namespace CarouselDemo
    Partial Public Class MovingPath
        Inherits CarouselDemoModule

        Public Sub New()
            InitializeComponent()
            AddItems("/CarouselDemo;component/Data/Images/Logos", ItemType.BinaryImage, carousel)
            AddHandler paddingTopSlider.EditValueChanged, AddressOf TrackBarValueChanged

            AddHandler paddingBottomSlider.EditValueChanged, AddressOf TrackBarValueChanged
            AddHandler paddingLeftSlider.EditValueChanged, AddressOf TrackBarValueChanged
            AddHandler paddingRightSlider.EditValueChanged, AddressOf TrackBarValueChanged
            AddHandler carousel.Loaded, AddressOf carousel_Loaded
        End Sub
        Private Sub carousel_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            paddingLineVisualizer.InvalidateVisual()
        End Sub
        Private Sub TrackBarValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            paddingLineVisualizer.InvalidateVisual()
        End Sub
    End Class
    Public Class LineVisualizer
        Inherits Grid

        Public Shared ReadOnly CarouselProperty As DependencyProperty
        Shared Sub New()
            CarouselProperty = DependencyProperty.Register("Carousel", GetType(CarouselPanel), GetType(LineVisualizer), New FrameworkPropertyMetadata(Nothing))
        End Sub
        Public Property Carousel() As CarouselPanel
            Get
                Return DirectCast(GetValue(CarouselProperty), CarouselPanel)
            End Get
            Set(ByVal value As CarouselPanel)
                SetValue(CarouselProperty, value)
            End Set
        End Property
        Protected Overrides Sub OnRender(ByVal dc As DrawingContext)
            MyBase.OnRender(dc)
            If Carousel IsNot Nothing Then
                Dim pen As New Pen(Brushes.Gray, 1R)
                dc.DrawLine(pen, New Point(0, Carousel.PathPadding.Top / 100R * Carousel.ActualHeight), New Point(Carousel.ActualWidth, Carousel.PathPadding.Top / 100R * Carousel.ActualHeight))
                dc.DrawLine(pen, New Point(0, (1R - Carousel.PathPadding.Bottom / 100R) * Carousel.ActualHeight), New Point(Carousel.ActualWidth, (1R - Carousel.PathPadding.Bottom / 100R) * Carousel.ActualHeight))
                dc.DrawLine(pen, New Point(Carousel.PathPadding.Left / 100R * Carousel.ActualWidth, 0), New Point(Carousel.PathPadding.Left / 100R * Carousel.ActualWidth, ActualHeight))
                dc.DrawLine(pen, New Point((1R - Carousel.PathPadding.Right / 100R) * Carousel.ActualWidth, 0), New Point((1R - Carousel.PathPadding.Right / 100R) * Carousel.ActualWidth, ActualHeight))
            End If
        End Sub
    End Class
    Public Class PaddingConverter
        Inherits MarkupExtension
        Implements IMultiValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        #Region "IMultiValueConverter Members"
        Private Function IMultiValueConverter_Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
            Return New Thickness(DirectCast(values(0), Double), DirectCast(values(1), Double), DirectCast(values(2), Double), DirectCast(values(3), Double))
        End Function
        Private Function IMultiValueConverter_ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
End Namespace
