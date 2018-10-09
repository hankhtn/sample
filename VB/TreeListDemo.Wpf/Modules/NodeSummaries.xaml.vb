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

Namespace TreeListDemo



    Partial Public Class NodeSummaries
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub OnShowingNodeFooter(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListShowingNodeFooterEventArgs)
            If Not chkHideSingleNodeFooters.IsChecked.Value Then
                Return
            End If
            If e.IsRootNode OrElse e.Node.Nodes.Where(Function(node) node.IsVisible).Count() = 1 Then
                e.Allow = False
            End If
        End Sub
    End Class
End Namespace
