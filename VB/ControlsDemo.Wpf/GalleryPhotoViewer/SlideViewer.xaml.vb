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
Imports System.Windows.Media.Animation
Imports DevExpress.Xpf.Utils

Namespace ControlsDemo.GalleryDemo
    Partial Public Class SlideViewer
        Inherits UserControl

        Public Shared ReadOnly NextSlideImageSourceProperty As DependencyProperty = DependencyPropertyManager.Register("NextSlideImageSource", GetType(ImageSource), GetType(SlideViewer), New FrameworkPropertyMetadata(Nothing))
        Public Property NextSlideImageSource() As ImageSource
            Get
                Return DirectCast(GetValue(NextSlideImageSourceProperty), ImageSource)
            End Get
            Set(ByVal value As ImageSource)
                SetValue(NextSlideImageSourceProperty, value)
            End Set
        End Property

        Public Event BeforeCurrentSlideLoading As EventHandler

        Public Sub New()
            InitializeComponent()
        End Sub
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            Me.currentSlideControl = slide1Control
            Me.nextSlideControl = slide2Control
        End Sub
        Public Sub Play()
            Me.currentSlideControl = slide1Control
            Me.nextSlideControl = slide2Control
            Me.currentSlideControl.Source = Nothing
            Me.currentSlideControl.Opacity = 1
            Me.nextSlideControl.Source = NextSlideImageSource
            Me.nextSlideControl.Opacity = 0
            BeginSlideChanging()
        End Sub
        Public Sub [Stop]()
            Me.changeSlideStoryboard.Stop()
            RemoveHandler Me.changeSlideStoryboard.Completed, AddressOf ChangeSlideStoryboard_Completed
            Me.changeSlideStoryboard = Nothing
        End Sub

        Protected Function CreateChangedSlideStoryboard() As Storyboard
            Dim storyboard As New Storyboard()
            storyboard.Duration = New Duration(TimeSpan.FromSeconds(5))
            Dim rnd As New Random()
            Select Case rnd.Next(5)
                Case 0
                    CreateMoveUpAnimation(storyboard)
                Case 1
                    CreateMoveLeftAnimation(storyboard)
                Case 2
                    CreateMoveDownAnimation(storyboard)
                Case 3
                    CreateMoveRightAnimation(storyboard)
                Case 4
                    CreateOpacityAnimation(storyboard)
            End Select
            Return storyboard
        End Function
        Protected Sub CreateOpacityAnimation(ByVal storyboard As Storyboard)
            Me.currentSlideControl.RenderTransform = Nothing
            Me.nextSlideControl.RenderTransform = Nothing
            Me.currentSlideControl.Opacity = 1
            Me.nextSlideControl.Opacity = 0
            Dim slide1Animation As New DoubleAnimation()
            slide1Animation.To = 1
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            System.Windows.Media.Animation.Storyboard.SetTarget(slide1Animation, Me.nextSlideControl)
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(slide1Animation, New PropertyPath("Opacity"))
            Dim slide2Animation As New DoubleAnimation()
            slide2Animation.To = 0
            slide2Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            System.Windows.Media.Animation.Storyboard.SetTarget(slide2Animation, Me.currentSlideControl)
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(slide2Animation, New PropertyPath("Opacity"))
            storyboard.Children.Add(slide1Animation)
            storyboard.Children.Add(slide2Animation)
        End Sub
        Protected Sub CreateMoveRightAnimation(ByVal storyboard As Storyboard)
            Me.currentSlideControl.Opacity = 1
            Me.nextSlideControl.Opacity = 1
            Me.currentSlideControl.RenderTransform = New TranslateTransform()
            Dim slide1Animation As New DoubleAnimation()
            slide1Animation.To = Me.ActualWidth
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide1Animation, Me.currentSlideControl, "X")
            Me.nextSlideControl.RenderTransform = New TranslateTransform() With {.X = -Me.ActualWidth}
            Dim slide2Animation As New DoubleAnimation()
            slide2Animation.To = 0
            slide2Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide2Animation, Me.nextSlideControl, "X")
            storyboard.Children.Add(slide1Animation)
            storyboard.Children.Add(slide2Animation)
        End Sub
        Protected Sub CreateMoveLeftAnimation(ByVal storyboard As Storyboard)
            Me.currentSlideControl.Opacity = 1
            Me.nextSlideControl.Opacity = 1
            Me.currentSlideControl.RenderTransform = New TranslateTransform()
            Dim slide1Animation As New DoubleAnimation()
            slide1Animation.To = -Me.ActualWidth
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide1Animation, Me.currentSlideControl, "X")
            Me.nextSlideControl.RenderTransform = New TranslateTransform() With {.X = Me.ActualWidth}
            Dim slide2Animation As New DoubleAnimation()
            slide2Animation.To = 0
            slide2Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide2Animation, Me.nextSlideControl, "X")
            storyboard.Children.Add(slide1Animation)
            storyboard.Children.Add(slide2Animation)
        End Sub
        Protected Sub CreateMoveUpAnimation(ByVal storyboard As Storyboard)
            Me.currentSlideControl.Opacity = 1
            Me.nextSlideControl.Opacity = 1
            Me.currentSlideControl.RenderTransform = New TranslateTransform()
            Dim slide1Animation As New DoubleAnimation()
            slide1Animation.To = -Me.ActualHeight
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide1Animation, Me.currentSlideControl, "Y")
            Me.nextSlideControl.RenderTransform = New TranslateTransform() With {.Y = Me.ActualHeight}
            Dim slide2Animation As New DoubleAnimation()
            slide2Animation.To = 0
            slide2Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide2Animation, Me.nextSlideControl, "Y")
            storyboard.Children.Add(slide1Animation)
            storyboard.Children.Add(slide2Animation)
        End Sub
        Protected Sub CreateMoveDownAnimation(ByVal storyboard As Storyboard)
            Me.currentSlideControl.Opacity = 1
            Me.nextSlideControl.Opacity = 1
            Me.currentSlideControl.RenderTransform = New TranslateTransform()
            Dim slide1Animation As New DoubleAnimation()
            slide1Animation.To = Me.ActualHeight
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide1Animation, Me.currentSlideControl, "Y")
            Me.nextSlideControl.RenderTransform = New TranslateTransform() With {.Y = -Me.ActualHeight}
            Dim slide2Animation As New DoubleAnimation()
            slide2Animation.To = 0
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide2Animation, Me.nextSlideControl, "Y")
            storyboard.Children.Add(slide1Animation)
            storyboard.Children.Add(slide2Animation)
        End Sub

        Private Sub ChangeSlideStoryboard_Completed(ByVal sender As Object, ByVal e As EventArgs)
            RemoveHandler Me.changeSlideStoryboard.Completed, AddressOf ChangeSlideStoryboard_Completed
            Me.currentSlideControl.BeginAnimation(UIElement.OpacityProperty, Nothing)
            Me.nextSlideControl.BeginAnimation(UIElement.OpacityProperty, Nothing)
            Dim slideControl As Image = Me.currentSlideControl
            Me.currentSlideControl = Me.nextSlideControl
            Me.nextSlideControl = slideControl
            RaiseEvent BeforeCurrentSlideLoading(Me, New EventArgs())
            Me.nextSlideControl.Source = NextSlideImageSource
            BeginSlideChanging()
        End Sub
        Private Sub BeginSlideChanging()
            If Me.changeSlideStoryboard IsNot Nothing Then
                RemoveHandler Me.changeSlideStoryboard.Completed, AddressOf ChangeSlideStoryboard_Completed
            End If
            Me.changeSlideStoryboard = CreateChangedSlideStoryboard()
            AddHandler changeSlideStoryboard.Completed, AddressOf ChangeSlideStoryboard_Completed
            Me.changeSlideStoryboard.Begin()
        End Sub

        Private Sub SetAnimationTarget(ByVal animation As DoubleAnimation, ByVal target As Image, ByVal path As String)
            Storyboard.SetTarget(animation, target)
            Storyboard.SetTargetProperty(animation, New PropertyPath("RenderTransform.(TranslateTransform." & path & ")"))
        End Sub

        Private Property currentSlideControl() As Image
        Private Property nextSlideControl() As Image
        Private Property changeSlideStoryboard() As Storyboard
    End Class
End Namespace
