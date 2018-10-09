Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.WindowsUI
Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Interop
Namespace CommonDemo
    Partial Public Class Windows8StyleMessageBox
        Inherits DemoModule

        Public Sub New()
            InitializeComponent()
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of MessageBoxButton, MessageBoxButton)(buttons)
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of MessageBoxImage, MessageBoxImage)(icons)
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of FloatingMode, FloatingMode)(floatingMode)
            If BrowserInteropHelper.IsBrowserHosted Then
                floatingMode.Visibility = System.Windows.Visibility.Collapsed
                floatingModeLabel.Visibility = System.Windows.Visibility.Collapsed
            End If
        End Sub
        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            WinUIMessageBox.Show(Window.GetWindow(Me), contentEdit.DisplayText, captionEdit.DisplayText, CType(buttons.EditValue, MessageBoxButton), CType(icons.EditValue, MessageBoxImage), MessageBoxResult.None, MessageBoxOptions.None, CType(floatingMode.EditValue, FloatingMode))
        End Sub
    End Class
End Namespace
