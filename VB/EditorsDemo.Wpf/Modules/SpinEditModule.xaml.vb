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
    Partial Public Class SpinEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            InitSources()
        End Sub
        Private Sub InitSources()
            Dim styles As New List(Of SpinStyle)()
            styles.Add(SpinStyle.Vertical)
            styles.Add(SpinStyle.Horizontal)
            cboSpinStyle.ItemsSource = styles
        End Sub
        Private Sub cboSpinStyle_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            CType(editor.ActualButtons(0), SpinButtonInfo).SpinStyle = CType(e.NewValue, SpinStyle)
        End Sub
    End Class
End Namespace
