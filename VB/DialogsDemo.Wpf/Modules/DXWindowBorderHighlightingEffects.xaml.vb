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
Imports DevExpress.Xpf.Editors
Imports System.ComponentModel
Imports DevExpress.Xpf.Editors.Validation
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports System.Windows.Interop
Imports System.Windows.Threading
Imports DialogsDemo

Namespace DialogsDemo
    Partial Public Class DXWindowBorderHighlightingEffects
        Inherits DialogsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub HidePopupContent()
            If window IsNot Nothing Then
                window.Close()
            End If
            MyBase.HidePopupContent()
        End Sub

        Private desiredWindowSize As New Size(400, 200)
        Protected Function GetWindowSuggestedRect(ByVal parentRect As Rect) As Rect
            Return New Rect(parentRect.Left + parentRect.Width / 2R - desiredWindowSize.Width, parentRect.Top + parentRect.Height / 2R - desiredWindowSize.Height, desiredWindowSize.Width, desiredWindowSize.Height)
        End Function
        Private window As DXWindow
        Private rootWindow As Window

        Private Sub SetBorderEffectCustomColors()
            window.BorderEffectActiveColor = New SolidColorBrush(activeColor.Color)
            window.BorderEffectInactiveColor = New SolidColorBrush(inactiveColor.Color)
        End Sub

        Private Sub ShowWindow()
            If window IsNot Nothing Then
                window.Close()
            End If
            window = New DXWindow()

            If enableEffect.IsChecked.HasValue Then
                window.BorderEffect = If(enableEffect.IsChecked.Value, BorderEffect.Default, BorderEffect.None)
            End If
            If enableCustomization.IsChecked.Value Then
                SetBorderEffectCustomColors()
            End If
            rootWindow = TryCast(LayoutHelper.GetRoot(Me), Window)
            If rootWindow IsNot Nothing Then
                window.SetBounds(GetWindowSuggestedRect(rootWindow.GetBounds()))
                window.Icon = rootWindow.Icon
                AddHandler rootWindow.Closed, AddressOf rootWindow_Closed
                window.Owner = rootWindow
            End If
            window.Title = "DXWindow"

            window.Show()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ShowWindow()
        End Sub

        Private Sub rootWindow_Closed(ByVal sender As Object, ByVal e As EventArgs)
            If window IsNot Nothing Then
                window.Close()
            End If
        End Sub

        Private Sub enableCustomization_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If window Is Nothing Then
                Return
            End If
            If CBool(e.NewValue) Then
                SetBorderEffectCustomColors()
            Else
                window.BorderEffectReset()
            End If
        End Sub

        Private Sub enableEffect_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If window Is Nothing Then
                Return
            End If
            If enableCustomization.IsChecked.Value Then
                SetBorderEffectCustomColors()
            End If
            If CBool(e.NewValue) Then
                window.BorderEffect = BorderEffect.Default
            Else
                window.BorderEffectReset()
                window.BorderEffect = BorderEffect.None
            End If

        End Sub

        Private Sub activeColor_ColorChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If window IsNot Nothing Then
                window.BorderEffectActiveColor = New SolidColorBrush(activeColor.Color)
            End If
        End Sub

        Private Sub inactiveColor_ColorChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If window IsNot Nothing Then
                window.BorderEffectInactiveColor = New SolidColorBrush(inactiveColor.Color)
            End If
        End Sub
    End Class
End Namespace
