Imports System
Imports DevExpress.Xpf.DemoBase
Imports System.IO
Imports DevExpress.Xpf.Diagram
Imports DevExpress.Diagram.Core
Imports DevExpress.Internal
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Markup
Imports DiagramDemo
Imports System.Windows.Media
Imports DevExpress.Utils
Imports System.Windows.Media.Imaging

Namespace DiagramDemo
    Public Class LayoutModuleBase
        Inherits DiagramDemoModule

        Public Sub New()
            AddHandler Me.Loaded, AddressOf OnLoad
        End Sub
        Private Sub OnLoad(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RelayoutDiagram()
        End Sub
        Private Sub RelayoutDiagram()
            If Not IsLoaded Then
                Return
            End If
            RelayoutDiagramCore()
        End Sub
        Protected Overridable Sub RelayoutDiagramCore()
        End Sub
        Protected Sub SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RelayoutDiagram()
        End Sub
        Protected Sub EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            RelayoutDiagram()
        End Sub
    End Class
End Namespace
