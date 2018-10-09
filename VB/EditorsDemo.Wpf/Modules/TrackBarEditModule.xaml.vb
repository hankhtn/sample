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
Imports System.Windows.Media.Animation

Namespace EditorsDemo
    Partial Public Class TrackBarEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            OptionsDataContext = editor
            InitSources()
        End Sub
        Private Sub InitSources()
            cbTickPlacement.ItemsSource = New TickPlacement() { TickPlacement.Both, TickPlacement.BottomRight, TickPlacement.None, TickPlacement.TopLeft }
        End Sub
        Private Sub CheckEdit_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            editor.StyleSettings = New TrackBarZoomStyleSettings()
        End Sub
        Private Sub CheckEdit_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            editor.StyleSettings = New TrackBarStyleSettings()
        End Sub
    End Class
End Namespace
