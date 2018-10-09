Imports DevExpress.Xpf.Core
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Namespace RibbonDemo
    Public Class ThemedContentControl
        Inherits ContentControl

        #Region "static"
        Public Shared ReadOnly IsHoverStateProperty As DependencyProperty = DependencyProperty.Register("IsHoverState", GetType(Boolean), GetType(ThemedContentControl), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf OnHoverStatePropertyChanged)))
        Public Shared ReadOnly LightThemeNormalForegroundProperty As DependencyProperty = DependencyProperty.Register("LightThemeNormalForeground", GetType(Brush), GetType(ThemedContentControl), New PropertyMetadata(Nothing))
        Public Shared ReadOnly DarkThemeNormalForegroundProperty As DependencyProperty = DependencyProperty.Register("DarkThemeNormalForeground", GetType(Brush), GetType(ThemedContentControl), New PropertyMetadata(Nothing))
        Public Shared ReadOnly LightThemeHoverForegroundProperty As DependencyProperty = DependencyProperty.Register("LightThemeHoverForeground", GetType(Brush), GetType(ThemedContentControl), New PropertyMetadata(Nothing))
        Public Shared ReadOnly DarkThemeHoverForegroundProperty As DependencyProperty = DependencyProperty.Register("DarkThemeHoverForeground", GetType(Brush), GetType(ThemedContentControl), New PropertyMetadata(Nothing))
        Protected Shared Sub OnHoverStatePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ThemedContentControl).UpdateForeground()
        End Sub
        #End Region
        #Region "dep props"
        Public Property IsHoverState() As Boolean
            Get
                Return DirectCast(GetValue(IsHoverStateProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsHoverStateProperty, value)
            End Set
        End Property
        Public Property LightThemeNormalForeground() As Brush
            Get
                Return DirectCast(GetValue(LightThemeNormalForegroundProperty), Brush)
            End Get
            Set(ByVal value As Brush)
                SetValue(LightThemeNormalForegroundProperty, value)
            End Set
        End Property
        Public Property DarkThemeNormalForeground() As Brush
            Get
                Return DirectCast(GetValue(DarkThemeNormalForegroundProperty), Brush)
            End Get
            Set(ByVal value As Brush)
                SetValue(DarkThemeNormalForegroundProperty, value)
            End Set
        End Property
        Public Property LightThemeHoverForeground() As Brush
            Get
                Return DirectCast(GetValue(LightThemeHoverForegroundProperty), Brush)
            End Get
            Set(ByVal value As Brush)
                SetValue(LightThemeHoverForegroundProperty, value)
            End Set
        End Property
        Public Property DarkThemeHoverForeground() As Brush
            Get
                Return DirectCast(GetValue(DarkThemeHoverForegroundProperty), Brush)
            End Get
            Set(ByVal value As Brush)
                SetValue(DarkThemeHoverForegroundProperty, value)
            End Set
        End Property
        #End Region
        #Region "props        "
        Private ReadOnly Property IsDarkTheme() As Boolean
            Get
                Return ApplicationThemeHelper.ApplicationThemeName = Theme.MetropolisDarkName OrElse ApplicationThemeHelper.ApplicationThemeName = Theme.TouchlineDarkName OrElse ApplicationThemeHelper.ApplicationThemeName = Theme.Office2016DarkGraySEName
            End Get
        End Property
        #End Region
        Public Sub New()
            DefaultStyleKey = GetType(ThemedContentControl)
            AddHandler Loaded, AddressOf OnLoaded
        End Sub
        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateForeground()
        End Sub
        Private Sub UpdateForeground()
            If IsHoverState Then
                Foreground = If(IsDarkTheme, DarkThemeHoverForeground, LightThemeHoverForeground)
            Else
                Foreground = If(IsDarkTheme, DarkThemeNormalForeground, LightThemeNormalForeground)
            End If
        End Sub
    End Class
End Namespace
