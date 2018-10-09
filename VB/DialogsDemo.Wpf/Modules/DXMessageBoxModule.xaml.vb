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
Imports DialogsDemo

Namespace DialogsDemo



    Partial Public Class DXMessageBoxModule
        Inherits DialogsDemoModule

        Public Sub New()
            InitializeComponent()
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

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)

            DXMessageBox.Show(TryCast(LayoutHelper.GetRoot(Me), Window), contentEdit.DisplayText, captionEdit.DisplayText, CType(buttons.EditValue, MessageBoxButton), DirectCast(CType(icons.EditValue, EnumHelperData).Value, MessageBoxImage), MessageBoxResult.None, MessageBoxOptions.None, CType(floatingMode.EditValue, FloatingMode))
        End Sub

        Public Property BrouwserInteropHlper() As Boolean
    End Class




End Namespace
