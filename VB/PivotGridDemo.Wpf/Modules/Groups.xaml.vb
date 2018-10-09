Imports System
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo
    Partial Public Class Groups
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub UpdateGroupsExpanded(ByVal expanded As Boolean)
            pivotGrid.BeginUpdate()
            Try
                For Each group As PivotGridGroup In pivotGrid.Groups
                    For Each field As PivotGridField In group
                        field.ExpandedInFieldsGroup = expanded
                    Next field
                Next group
            Finally
                pivotGrid.EndUpdate()
            End Try
        End Sub
        Private Sub Collapse_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateGroupsExpanded(False)
        End Sub
        Private Sub Expand_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateGroupsExpanded(True)
        End Sub
    End Class
End Namespace
