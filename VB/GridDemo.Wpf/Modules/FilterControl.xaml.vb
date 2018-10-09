Imports DevExpress.Data.Filtering

Namespace GridDemo
    Partial Public Class FilterControl
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            filterGrid.FilterCriteria = New BinaryOperator("OrderID", 10248, BinaryOperatorType.Equal)
        End Sub


        Private Sub ApplyFilterButtonClick(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            filterEditor.ApplyFilter()
        End Sub

        Private Sub TableView_FilterEditorCreated(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.FilterEditorEventArgs)
            e.FilterControl.ShowGroupCommandsIcon = filterEditor.ShowGroupCommandsIcon
            e.FilterControl.ShowOperandTypeIcon = filterEditor.ShowOperandTypeIcon
            e.FilterControl.ShowToolTips = filterEditor.ShowToolTips
        End Sub
    End Class
End Namespace
