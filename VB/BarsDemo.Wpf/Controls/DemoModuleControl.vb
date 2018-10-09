Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Media
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Utils

Namespace BarsDemo
    Public Class BarsDemoModule
        Inherits DemoModule

        Public Shared ReadOnly BarManagerProperty As DependencyProperty = DependencyPropertyManager.Register("BarManager", GetType(BarManager), GetType(BarsDemoModule), New FrameworkPropertyMetadata(Nothing))
        Public Property Manager() As BarManager
            Get
                Return CType(GetValue(BarManagerProperty), BarManager)
            End Get
            Set(ByVal value As BarManager)
                SetValue(BarManagerProperty, value)
            End Set
        End Property
        Shared Sub New()
            BarNameScope.IsScopeOwnerProperty.OverrideMetadata(GetType(BarsDemoModule), New FrameworkPropertyMetadata(True))
        End Sub
        Public Sub New()
            AddHandler Loaded, AddressOf BarsDemoModule_Loaded
        End Sub
        Private Sub BarsDemoModule_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateBorder()
        End Sub
        Private Sub UpdateBorder()
            Margin = New Thickness(25)
            BorderThickness = New Thickness(1)
            If Theme.MetropolisLightName = ThemeManager.GetThemeName(Me) Then
                BorderBrush = New SolidColorBrush(Colors.DarkGray)
            Else
                Dim color As Color = (TryCast(TextElement.GetForeground(Me), SolidColorBrush)).Color
                BorderBrush = New SolidColorBrush(System.Windows.Media.Color.FromArgb(50, color.R, color.G, color.B))
            End If

        End Sub
    End Class
End Namespace
