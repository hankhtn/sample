Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports DialogsDemo
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Interop
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace DialogsDemo
    <CodeFile("Modules/Views/DXWindowHeaderModuleWindow.xaml"), CodeFile("Modules/Views/DXWindowHeaderModuleWindow.xaml.cs"), CodeFile("Modules/DXWindowBorderHighlightingEffects.xaml"), CodeFile("Modules/DXMessageBoxModule.xaml")>
    Partial Public Class DXWindowCustomHeaderButton
        Inherits DialogsDemoModule

        Private window As DXWindow
        Private rootWindow As Window
        Private desiredWindowSize As New Size(400, 200)

        Public Sub New()
            InitializeComponent()
            Init()
        End Sub

        Public Property BrouwserInteropHlper() As Boolean

        Private Sub Init()
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of MessageBoxButton, MessageBoxButton)(buttons)
            Dim iconsCollection As New List(Of EnumHelperData)()
            For Each mbi As String In System.Enum.GetNames(GetType(MessageBoxImage))
                iconsCollection.Add(New EnumHelperData() With {.Text = mbi, .Value = System.Enum.Parse(GetType(MessageBoxImage), mbi)})
            Next mbi
            icons.ItemsSource = iconsCollection
            icons.DisplayMember = "Text"
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of FloatingMode, FloatingMode)(floatingMode)
            icons.SelectedIndex = 0
            buttons.SelectedIndex = 0
            floatingMode.SelectedIndex = 1
            If BrowserInteropHelper.IsBrowserHosted Then
                floatingMode.Visibility = System.Windows.Visibility.Collapsed
                floatingModeLabel.Visibility = System.Windows.Visibility.Collapsed
            End If
        End Sub

        Private Sub ButtonMessage_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DXMessageBox.Show(TryCast(LayoutHelper.GetRoot(Me), Window), contentEdit.DisplayText, captionEdit.DisplayText, CType(buttons.EditValue, MessageBoxButton), DirectCast(CType(icons.EditValue, EnumHelperData).Value, MessageBoxImage), MessageBoxResult.None, MessageBoxOptions.None, CType(floatingMode.EditValue, FloatingMode))
        End Sub

        Private Sub ShowCustomHeaderWindow()
            If window IsNot Nothing Then
                window.Close()
            End If
            window = New DXWindowHeaderModuleWindow()

            window.Show()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ShowCustomHeaderWindow()
        End Sub

        Protected Overrides Sub HidePopupContent()
            If window IsNot Nothing Then
                window.Close()
            End If
            MyBase.HidePopupContent()
        End Sub

        Protected Function GetWindowSuggestedRect(ByVal parentRect As Rect) As Rect
            Return New Rect(parentRect.Left + parentRect.Width / 2R - desiredWindowSize.Width, parentRect.Top + parentRect.Height / 2R - desiredWindowSize.Height, desiredWindowSize.Width, desiredWindowSize.Height)
        End Function

        Private Sub SetBorderEffectCustomColors()
            window.BorderEffectActiveColor = New SolidColorBrush(activeColor.Color)
            window.BorderEffectInactiveColor = New SolidColorBrush(inactiveColor.Color)
        End Sub

        Private Sub ShowWindow()
            If window IsNot Nothing Then
                window.Close()
            End If
            window = New DXWindow()

                window.BorderEffect = BorderEffect.Default
            If enableCustomization.IsChecked.Value Then
                SetBorderEffectCustomColors()
            End If
            rootWindow = TryCast(LayoutHelper.GetRoot(Me), Window)
            If rootWindow IsNot Nothing Then
                window.SetBounds(GetWindowSuggestedRect(rootWindow.GetBounds()))
                window.Icon = rootWindow.Icon
                AddHandler rootWindow.Closed, AddressOf RootWindowClosed
                window.Owner = rootWindow
            End If
            window.Title = "DXWindow"
            window.Topmost = True
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen
            window.Show()
        End Sub

        Private Sub ButtonEffectsClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ShowWindow()
        End Sub

        Private Sub RootWindowClosed(ByVal sender As Object, ByVal e As EventArgs)
            If window IsNot Nothing Then
                window.Close()
            End If
        End Sub

        Private Sub EnableCustomizationEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If window Is Nothing Then
                Return
            End If
            If CBool(e.NewValue) Then
                SetBorderEffectCustomColors()
            Else
                window.BorderEffectReset()
            End If
        End Sub

        Private Sub EnableEffectEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If window Is Nothing Then
                Return
            End If
            If CBool(e.NewValue) Then
                window.BorderEffect = BorderEffect.Default
            Else
                window.BorderEffectReset()
                window.BorderEffect = BorderEffect.None
            End If
            If enableCustomization.IsChecked.Value Then
                SetBorderEffectCustomColors()
            End If


        End Sub

        Private Sub ActiveColorColorChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If window IsNot Nothing Then
                window.BorderEffectActiveColor = New SolidColorBrush(activeColor.Color)
            End If
        End Sub

        Private Sub InactiveColorColorChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If window IsNot Nothing Then
                window.BorderEffectInactiveColor = New SolidColorBrush(inactiveColor.Color)
            End If
        End Sub
    End Class

    Public Class EnumHelperData
        Public Property Text() As String
        Public Property Value() As Object
    End Class
End Namespace
