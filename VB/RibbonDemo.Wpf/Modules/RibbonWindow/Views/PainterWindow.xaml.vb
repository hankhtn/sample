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
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.Xpf.Bars
Imports DevExpress.Utils
Imports System.ComponentModel
Imports DevExpress.Xpf.Core
Imports DevExpress.Mvvm

Namespace RibbonDemo
    Partial Public Class PainterWindow
        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnEditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            NavigationTree.ExitMenuMode(True, False)
        End Sub
    End Class
End Namespace
