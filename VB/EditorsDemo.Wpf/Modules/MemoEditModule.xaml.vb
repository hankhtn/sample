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
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace EditorsDemo
    Partial Public Class MemoEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            editor.EditValue = "MemoEdit is a multi-line text editor. In addition to the advanced text input features derived from the TextEdit control, it offers numerous options for multi-line text management." & ControlChars.CrLf & _
"- Optional ENTER and TAB key processing." & ControlChars.CrLf & _
"- Customizable visibility for vertical and horizontal scrollbars." & ControlChars.CrLf & _
"- Optional text word-wrapping."
            InitSources()
            DataContext = CarsData.DataSource
        End Sub
        Private Sub InitSources()
            Dim wrapping() As TextWrapping = { TextWrapping.Wrap, TextWrapping.NoWrap, TextWrapping.WrapWithOverflow }
            lbTextWrapping.ItemsSource = wrapping

            Dim scrollVisibilities() As ScrollBarVisibility = { ScrollBarVisibility.Auto, ScrollBarVisibility.Disabled, ScrollBarVisibility.Hidden, ScrollBarVisibility.Visible}
            cbHorizontalScroll.ItemsSource = scrollVisibilities
            cbVerticalScroll.ItemsSource = scrollVisibilities
        End Sub
    End Class
End Namespace
