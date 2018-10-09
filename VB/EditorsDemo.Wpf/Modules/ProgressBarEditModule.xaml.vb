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
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System.Windows.Markup
Imports EditorsDemo.Utils

Namespace EditorsDemo
    Partial Public Class ProgressBarEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            cbContentSettings.ItemsSource = New List(Of ContentDisplayMode) From {ContentDisplayMode.None, ContentDisplayMode.Value}
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of Orientation, Orientation)(cbOrientation)
            cbOrientation.EditValue = Orientation.Horizontal
            SetupOrientation()
            DataContext = New OutlookViewModel()
        End Sub
        Private Sub SetupOrientation()
            If Object.Equals(cbOrientation.EditValue, Orientation.Horizontal) Then
                editor.Width = 400
                editor.ClearValue(FrameworkElement.HeightProperty)
                editor.Orientation = Orientation.Horizontal
            Else
                editor.Height = 400
                editor.ClearValue(FrameworkElement.WidthProperty)
                editor.Orientation = Orientation.Vertical
            End If
        End Sub
        Private Sub cbBarStyle_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If cbBarStyle.SelectedIndex = 0 Then
                editor.StyleSettings = New ProgressBarStyleSettings()
            Else
                editor.StyleSettings = New ProgressBarMarqueeStyleSettings()
            End If
        End Sub
        Private Sub CheckEdit_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If String.IsNullOrEmpty(TryCast(cbDisplayFormat.EditValue, String)) OrElse cbDisplayFormat.SelectedIndex = 0 Then
                cbDisplayFormat.SelectedIndex = 1
            End If
        End Sub
        Private Sub CheckEdit_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If cbDisplayFormat.SelectedIndex = 1 Then
                cbDisplayFormat.SelectedIndex = 0
            End If
        End Sub

        Private Sub cbDisplayFormat_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Try
                editor.DisplayFormatString = CStr(e.NewValue)
            Catch
                editor.DisplayFormatString = ""
            End Try
        End Sub

        Private Sub cbOrientation_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            SetupOrientation()
        End Sub
    End Class
End Namespace
