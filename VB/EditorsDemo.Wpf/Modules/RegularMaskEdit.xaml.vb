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
Imports EditorsDemo
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo



    Partial Public Class RegularMaskEdit
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf RegularMaskEdit_Loaded
        End Sub
        Private Sub RegularMaskEdit_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            editor.Focus()
        End Sub
        Private Sub EditorGotFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
            mask.FocusedEditor = TryCast(sender, TextEdit)
            mask.UpdateMask()
        End Sub

    End Class
End Namespace
