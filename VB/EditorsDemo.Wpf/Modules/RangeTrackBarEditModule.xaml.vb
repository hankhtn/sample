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
Imports System.Windows.Controls.Primitives

Namespace EditorsDemo
    Partial Public Class RangeTrackBarEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            OptionsDataContext = editor

            cbTickPlacement.ItemsSource = (New BaseKindHelper(Of TickPlacement)()).GetEnumMemberList()
        End Sub

        Private Sub CheckEdit_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            editor.StyleSettings = New TrackBarZoomRangeStyleSettings()
        End Sub
        Private Sub CheckEdit_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            editor.StyleSettings = New TrackBarRangeStyleSettings()
        End Sub
    End Class
End Namespace
