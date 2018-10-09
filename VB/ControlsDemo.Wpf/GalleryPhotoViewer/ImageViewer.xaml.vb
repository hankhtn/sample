Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Utils
Namespace ControlsDemo.GalleryDemo
    Partial Public Class ImageViewer
        Inherits UserControl

        #Region "Dependency Properties"
        Public Shared ReadOnly ScaleProperty As DependencyProperty = DependencyProperty.Register("Scale", GetType(Double), GetType(ImageViewer), New PropertyMetadata(1R, New PropertyChangedCallback(AddressOf OnScalePropertyChanged)))
        Public Shared ReadOnly RotationProperty As DependencyProperty = DependencyProperty.Register("Rotation", GetType(Rotation), GetType(ImageViewer), New PropertyMetadata(Rotation.Rotate0))
        Public Shared ReadOnly ImageSourceProperty As DependencyProperty = DependencyProperty.Register("ImageSource", GetType(BitmapSource), GetType(ImageViewer), New PropertyMetadata(Nothing))

        Public Shared ReadOnly MinScaleValueProperty As DependencyProperty = DependencyProperty.Register("MinScaleValue", GetType(Double), GetType(ImageViewer), New PropertyMetadata(0.1R))
        Public Shared ReadOnly MaxScaleValueProperty As DependencyProperty = DependencyProperty.Register("MaxScaleValue", GetType(Double), GetType(ImageViewer), New PropertyMetadata(4R))
        Protected Shared Sub OnScalePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ImageViewer).OnScaleChanged(DirectCast(e.OldValue, Double))
        End Sub
        #End Region ' Dependency Properties
        Public Property Scale() As Double
            Get
                Return DirectCast(GetValue(ScaleProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(ScaleProperty, value)
            End Set
        End Property
        Public Property Rotation() As Rotation
            Get
                Return DirectCast(GetValue(RotationProperty), Rotation)
            End Get
            Set(ByVal value As Rotation)
                SetValue(RotationProperty, value)
            End Set
        End Property
        Public Property ImageSource() As BitmapSource
            Get
                Return DirectCast(GetValue(ImageSourceProperty), BitmapSource)
            End Get
            Set(ByVal value As BitmapSource)
                SetValue(ImageSourceProperty, value)
            End Set
        End Property
        Public Property MinScaleValue() As Double
            Get
                Return DirectCast(GetValue(MinScaleValueProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(MinScaleValueProperty, value)
            End Set
        End Property
        Public Property MaxScaleValue() As Double
            Get
                Return DirectCast(GetValue(MaxScaleValueProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(MaxScaleValueProperty, value)
            End Set
        End Property
        Public ReadOnly Property HorizontalFitScale() As Double
            Get
                Dim horzScale As Double = OriginalViewportSize.Width / OriginalContentSize.Width * Scale
                If OriginalViewportSize.Height < OriginalContentSize.Height / Scale * horzScale Then
                    horzScale = (OriginalViewportSize.Width - scrollBarSize) / OriginalContentSize.Width * Scale
                End If
                Return horzScale
            End Get
        End Property
        Public ReadOnly Property VerticalFitScale() As Double
            Get
                Dim vertScale As Double = OriginalViewportSize.Height / OriginalContentSize.Height * Scale
                If OriginalViewportSize.Width < OriginalContentSize.Width / Scale * vertScale Then
                    vertScale = (OriginalViewportSize.Height - scrollBarSize) / OriginalContentSize.Height * Scale
                End If
                Return vertScale
            End Get
        End Property
        Public Event MouseWheelZoom As EventHandler
        Public ReadOnly Property PartImage() As Image
            Get
                Return image
            End Get
        End Property
        Public ReadOnly Property Viewport() As Border
            Get
                Return border
            End Get
        End Property
        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
            AddHandler Unloaded, AddressOf OnUnloaded
            AddHandler scrollHost.PreviewMouseWheel, AddressOf OnScrollHostMouseWheel
        End Sub
        Public Function GetBestFitScale() As Double
            Dim bestScale As Double = HorizontalFitScale
            If scrollHost.ViewportHeight < CType(scrollHost.Content, UIElement).DesiredSize.Height / Scale * bestScale Then
                Return VerticalFitScale
            End If
            Return bestScale
        End Function
        Public Sub ScaleCenter(ByVal scaleValue As Double)
            Dim scalePoint As New Point(scrollHost.HorizontalOffset + CDbl(OriginalViewportSize.Width) / 2, scrollHost.VerticalOffset + CDbl(OriginalViewportSize.Height) / 2)
            ScaleAndScroll(scalePoint, scaleValue)
        End Sub

        Protected Overrides Sub OnMouseWheel(ByVal e As MouseWheelEventArgs)
            e.Handled = True
            Dim point As Point = e.GetPosition(CType(scrollHost.Content, UIElement))
            If e.Delta > 0 Then
                ScaleAndScroll(point, Math.Min(Scale + 0.1, MaxScaleValue))
            Else
                ScaleAndScroll(point, Math.Max(Scale - 0.1, MinScaleValue))
            End If
            RaiseEvent MouseWheelZoom(Me, New EventArgs())
            Return
        End Sub
        Protected Overridable Sub OnScaleChanged(ByVal oldValue As Double)
            scaleIsChanged = True
        End Sub
        Protected Overridable Sub OnLayoutUpdated(ByVal sender As Object, ByVal e As EventArgs)
            UpdateScrollbarVisibility()
            UpdateMouseCursor()
            If Me.scaleIsChanged Then
                scrollHost.ScrollToHorizontalOffset(Me.scrollOffsetAfterScaleChanged.X)
                scrollHost.ScrollToVerticalOffset(Me.scrollOffsetAfterScaleChanged.Y)
                Me.scaleIsChanged = False
            End If
        End Sub
        Protected Overridable Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AddHandler LayoutUpdated, AddressOf OnLayoutUpdated
        End Sub
        Protected Overridable Sub OnUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler LayoutUpdated, AddressOf OnLayoutUpdated
        End Sub
        Protected Overridable Sub OnImageMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            If Not Me.isLeftButtonDown Then
                Return
            End If
            Dim point As Point = e.GetPosition(scrollHost)
            Dim horizontalDragDelta As Double = Me.dragPoint.X - point.X
            Dim verticalDragDelta As Double = Me.dragPoint.Y - point.Y
            scrollHost.ScrollToHorizontalOffset(Me.dragOffset.Width + horizontalDragDelta)
            scrollHost.ScrollToVerticalOffset(Me.dragOffset.Height + verticalDragDelta)
        End Sub
        Protected Overridable Sub OnImageMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            DirectCast(sender, UIElement).CaptureMouse()
            Me.isLeftButtonDown = True
            Me.dragPoint = e.GetPosition(scrollHost)
            Me.dragOffset = New Size(scrollHost.HorizontalOffset, scrollHost.VerticalOffset)
            e.Handled = True
        End Sub
        Protected Overridable Sub OnImageMouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            DirectCast(sender, UIElement).ReleaseMouseCapture()
            Me.isLeftButtonDown = False
            e.Handled = True
        End Sub
        Protected Overridable Sub OnImageLostMouseCapture(ByVal sender As Object, ByVal e As EventArgs)
            Me.isLeftButtonDown = False
        End Sub
        Protected Overridable Sub ScaleAndScroll(ByVal scalePoint As Point, ByVal scaleValue As Double)
            Dim originalImageSize As New Size(OriginalContentSize.Width / Scale, OriginalContentSize.Height / Scale)
            Dim viewportOffset As New Point(scalePoint.X - scrollHost.HorizontalOffset, scalePoint.Y - scrollHost.VerticalOffset)

            scrollOffsetAfterScaleChanged = New Point(scalePoint.X / Scale * scaleValue - viewportOffset.X, scalePoint.Y / Scale * scaleValue - viewportOffset.Y)
            Scale = scaleValue
        End Sub
        Protected Overridable Sub OnScrollHostMouseWheel(ByVal sender As Object, ByVal e As MouseWheelEventArgs)
            OnMouseWheel(e)
        End Sub

        Private Sub UpdateScrollbarVisibility()
            If OriginalContentSize.Height - OriginalViewportSize.Height > 0.5R Then
                scrollHost.VerticalScrollBarVisibility = ScrollBarVisibility.Visible
            Else
                scrollHost.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
            End If

            If OriginalContentSize.Width - OriginalViewportSize.Width > 0.5R Then
                scrollHost.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible
            Else
                scrollHost.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden
            End If
        End Sub
        Private Sub UpdateMouseCursor()
            If scrollHost.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible OrElse scrollHost.VerticalScrollBarVisibility = ScrollBarVisibility.Visible Then
                CType(scrollHost.Content, ImagePresenter).Cursor = Cursors.Hand
            Else
                CType(scrollHost.Content, ImagePresenter).Cursor = Cursors.Arrow
            End If
        End Sub

        Private ReadOnly Property OriginalContentSize() As Size
            Get
                Return CType(scrollHost.Content, UIElement).DesiredSize
            End Get
        End Property
        Private ReadOnly Property OriginalViewportSize() As Size
            Get
                Dim originViewportWidth As Double = If(scrollHost.ComputedVerticalScrollBarVisibility = System.Windows.Visibility.Visible, scrollHost.ViewportWidth + scrollBarSize, scrollHost.ViewportWidth)
                Dim originViewportHeight As Double = If(scrollHost.ComputedHorizontalScrollBarVisibility = System.Windows.Visibility.Visible, scrollHost.ViewportHeight + scrollBarSize, scrollHost.ViewportHeight)
                Return New Size(originViewportWidth, originViewportHeight)
            End Get
        End Property

        Private dragPoint As New Point()
        Private dragOffset As New Size()
        Private scrollOffsetAfterScaleChanged As New Point()
        Private scaleIsChanged As Boolean = False
        Private isLeftButtonDown As Boolean = False

        Private Const scrollBarSize As Double = 12
    End Class

    Public Class ImagePresenter
        Inherits Panel

        #Region "Dependency Properties"
        Public Shared ReadOnly RotationProperty As DependencyProperty = DependencyProperty.Register("Rotation", GetType(Rotation), GetType(ImagePresenter), New PropertyMetadata(Rotation.Rotate90, New PropertyChangedCallback(AddressOf OnRotationPropertyChanged)))
        Public Shared ReadOnly ContentProperty As DependencyProperty = DependencyProperty.Register("Content", GetType(UIElement), GetType(ImagePresenter), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnContentPropertyChanged)))
        Public Shared ReadOnly ScaleProperty As DependencyProperty = DependencyProperty.Register("Scale", GetType(Double), GetType(ImagePresenter), New PropertyMetadata(1R, New PropertyChangedCallback(AddressOf OnScalePropertyChanged)))
        Protected Shared Sub OnRotationPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ImagePresenter).OnRotationChanged(DirectCast(e.OldValue, Rotation))
        End Sub
        Protected Shared Sub OnContentPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ImagePresenter).OnContentChanged(DirectCast(e.OldValue, UIElement))
        End Sub
        Protected Shared Sub OnScalePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ImagePresenter).OnScaleChanged(DirectCast(e.OldValue, Double))
        End Sub
        #End Region ' Dependency Properties
        Public Property Rotation() As Rotation
            Get
                Return DirectCast(GetValue(RotationProperty), Rotation)
            End Get
            Set(ByVal value As Rotation)
                SetValue(RotationProperty, value)
            End Set
        End Property
        Public Property Content() As UIElement
            Get
                Return DirectCast(GetValue(ContentProperty), UIElement)
            End Get
            Set(ByVal value As UIElement)
                SetValue(ContentProperty, value)
            End Set
        End Property
        Public Property Scale() As Double
            Get
                Return DirectCast(GetValue(ScaleProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(ScaleProperty, value)
            End Set
        End Property

        Protected Overridable Sub OnContentChanged(ByVal oldValue As UIElement)
            If oldValue IsNot Nothing Then
                oldValue.RenderTransform = Nothing
                Children.Remove(oldValue)
            End If
            If Content IsNot Nothing Then
                Dim transformGroup As New TransformGroup()
                Dim scaleTransform As New ScaleTransform() With {.ScaleX = Scale, .ScaleY = Scale}
                Dim rotateTransform As New RotateTransform() With {.Angle = GetAngleByRotation(Rotation)}
                transformGroup.Children.Add(scaleTransform)
                transformGroup.Children.Add(rotateTransform)
                transformGroup.Children.Add(New TranslateTransform())
                Content.RenderTransform = transformGroup
                Children.Add(Content)
            End If
        End Sub
        Protected Overridable Sub OnScaleChanged(ByVal oldValue As Double)
            If Content IsNot Nothing Then
                TryCast((TryCast(Content.RenderTransform, TransformGroup)).Children(0), ScaleTransform).ScaleX = Scale
                TryCast((TryCast(Content.RenderTransform, TransformGroup)).Children(0), ScaleTransform).ScaleY = Scale
                Content.InvalidateArrange()
                InvalidateMeasure()
            End If
            InvalidateArrange()
            UpdateLayout()
        End Sub
        Protected Overridable Sub OnRotationChanged(ByVal oldValue As Rotation)
            If Content IsNot Nothing Then
                TryCast((TryCast(Content.RenderTransform, TransformGroup)).Children(1), RotateTransform).Angle = GetAngleByRotation(Rotation)
                Content.InvalidateMeasure()
                InvalidateMeasure()
            End If
        End Sub

        Protected Overrides Function MeasureOverride(ByVal availableSize As Size) As Size
            If Content Is Nothing Then
                Return New Size()
            End If
            Dim baseSize As New Size(Double.PositiveInfinity, Double.PositiveInfinity)
            Content.Measure(baseSize)
            TryCast((TryCast(Content.RenderTransform, TransformGroup)).Children(2), TranslateTransform).X = GetTranslatePoint().X
            TryCast((TryCast(Content.RenderTransform, TransformGroup)).Children(2), TranslateTransform).Y = GetTranslatePoint().Y
            Select Case Rotation
                Case Rotation.Rotate0, Rotation.Rotate180
                    Return New Size(Content.DesiredSize.Width * Scale, Content.DesiredSize.Height * Scale)
                Case Else
                    Return New Size(Content.DesiredSize.Height * Scale, Content.DesiredSize.Width * Scale)
            End Select
        End Function
        Protected Overrides Function ArrangeOverride(ByVal finalSize As Size) As Size
            If Content Is Nothing Then
                Return New Size()
            End If
            Content.Arrange(New Rect(0, 0, Content.DesiredSize.Width, Content.DesiredSize.Height))
            Return finalSize
        End Function

        Private Function GetAngleByRotation(ByVal rotation As Rotation) As Double
            If rotation = System.Windows.Media.Imaging.Rotation.Rotate0 Then
                Return 0
            ElseIf rotation = System.Windows.Media.Imaging.Rotation.Rotate90 Then
                Return 90
            ElseIf rotation = System.Windows.Media.Imaging.Rotation.Rotate180 Then
                Return 180
            ElseIf rotation = System.Windows.Media.Imaging.Rotation.Rotate270 Then
                Return 270
            End If
            Return 0
        End Function
        Private Function GetTranslatePoint() As Point
            Select Case Rotation
                Case Rotation.Rotate0
                    Return New Point(0, 0)
                Case Rotation.Rotate90
                    Return New Point(Content.DesiredSize.Height * Scale, 0)
                Case Rotation.Rotate180
                    Return New Point(Content.DesiredSize.Width * Scale, Content.DesiredSize.Height * Scale)
                Case Rotation.Rotate270
                    Return New Point(0, Content.DesiredSize.Width * Scale)

            End Select
            Return New Point()
        End Function
    End Class
End Namespace
