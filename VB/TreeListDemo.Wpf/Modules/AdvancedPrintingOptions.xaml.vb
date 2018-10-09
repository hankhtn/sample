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
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Core

Namespace TreeListDemo
    Partial Public Class AdvancedPrintingOptions
        Inherits PrintTreeListDemoModule

        Public Sub New()
            InitializeComponent()
            DXTabControl = tabControl
            AddHandler Loaded, AddressOf AdvancedPrintingOptions_Loaded
        End Sub

        Private Sub AdvancedPrintingOptions_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ExpandNodes()
            OnShowPrintPreviewInNewTab("TreeList Preview")
        End Sub

        Private Sub ExpandNodes()
            View.Nodes(0).IsExpanded = True
            View.Nodes(0).Nodes(0).IsExpanded = True
            View.Nodes(0).Nodes(1).IsExpanded = True
        End Sub
        Private Sub printStyleChooser_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If printStyleChooser.SelectedIndex > 0 Then
                treeView.PrintColumnHeaderStyle = CType(Resources("customPrintColumnHeaderStyle"), Style)
                treeView.PrintCellStyle = CType(Resources("customPrintCellStyle"), Style)
                treeView.PrintRowIndentStyle = CType(Resources("customIndentStyle"), Style)
                treeView.PrintTotalSummaryStyle = CType(Resources("customPrintTotalSummaryStyle"), Style)
            Else
                treeView.ClearValue(TreeListView.PrintColumnHeaderStyleProperty)
                treeView.ClearValue(TreeListView.PrintCellStyleProperty)
                treeView.ClearValue(TreeListView.PrintRowIndentStyleProperty)
                treeView.ClearValue(TreeListView.PrintTotalSummaryStyleProperty)
            End If
        End Sub
    End Class
End Namespace
