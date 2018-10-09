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

Namespace EditorsDemo
    Partial Public Class FlyoutControlModule
        Inherits EditorsDemoModule

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OpenInnerFlyout(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim element As FrameworkElement = TryCast(sender, FrameworkElement)
            If element Is Nothing Then
                Return
            End If
            flyoutControl.PlacementTarget = LayoutRoot
            flyoutControl.Style = GetFlyoutStyle(element.Name)
            flyoutControl.IsOpen = True
        End Sub
        Private Sub OpenFlyout(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim element As FrameworkElement = TryCast(sender, FrameworkElement)
            If element Is Nothing Then
                Return
            End If
            flyoutControl.PlacementTarget = element
            flyoutControl.Style = GetFlyoutStyle(element.Name)
            flyoutControl.IsOpen = True
        End Sub
        Private Function GetFlyoutStyle(ByVal key As String) As Style
            Return TryCast(Resources(key), Style)
        End Function
    End Class
End Namespace
