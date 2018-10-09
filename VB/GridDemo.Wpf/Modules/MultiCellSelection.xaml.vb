Imports DevExpress.Data
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Linq

Namespace GridDemo
    Partial Public Class MultiCellSelection
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            AssignDataSource()

            SelectCells()
        End Sub
        Private Shared ReadOnly dxLogo() As String = { "dd ", "dd ", "ddd", "d d", "d d", "d d", "d d", "d d", "ddd", "dd ", "dd ", "   ", "x x", "x x", "xxx", "xxx", " x ", " x ", " x ", "xxx", "xxx", "x x", "x x"}
        Private Sub SelectCells()
            grid.BeginSelection()
            Dim points = dxLogo.SelectMany(Function(s, y) s.Select(Function(c, x)If(c <> " "c, New Point(x, y), DirectCast(Nothing, Point?))))
            For Each point In points.Where(Function(x) x IsNot Nothing)
                view.SelectCell(point.Value.Y, view.VisibleColumns(point.Value.X + 1))
            Next point
            grid.EndSelection()
        End Sub

        Private Sub AssignDataSource()
            grid.ItemsSource = SalesByYearData.Data
            grid.Columns("Date").Visible = False
            grid.Columns("Date").ShowInColumnChooser = False
            For Each column As GridColumn In view.VisibleColumns
                grid.TotalSummary.Add(New GridSummaryItem() With {.FieldName = column.FieldName, .SummaryType = DevExpress.Data.SummaryItemType.Custom, .DisplayFormat = "${0:N}"})
                column.EditSettings = New SpinEditSettings() With {.MaskType = MaskType.Numeric, .MaskUseAsDisplayFormat = True, .Mask = "c", .MaskCulture = New CultureInfo("en-US")}
            Next column
        End Sub

        Private sum As Integer = 0
        Private Sub grid_CustomSummary(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs)
            If Object.Equals(e.SummaryProcess, CustomSummaryProcess.Start) Then
                sum = 0
            End If
            If e.SummaryProcess = CustomSummaryProcess.Calculate Then
                If grid.View IsNot Nothing Then
                    If (Not checkEdit.IsChecked.Value) OrElse view.IsCellSelected(e.RowHandle, grid.Columns(CType(e.Item, GridSummaryItem).FieldName)) Then
                        If e.FieldValue IsNot DBNull.Value AndAlso e.FieldValue IsNot Nothing Then
                            sum += CInt((e.FieldValue))
                        End If
                    End If
                End If
            End If
            If e.SummaryProcess = CustomSummaryProcess.Finalize Then
                e.TotalValue = sum
            End If
        End Sub

        Private Sub TableView_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.GridSelectionChangedEventArgs)
            grid.UpdateTotalSummary()
        End Sub

        Private Sub CheckEdit_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            grid.UpdateTotalSummary()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            SelectCells(True)
        End Sub
        Private Sub Button_Click_1(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            SelectCells(False)
        End Sub

        Private Sub SelectCells(ByVal shouldSelectTopValues As Boolean)
            Dim list As New List(Of KeyValuePair(Of GridCell, Integer))()
            For i As Integer = 0 To grid.VisibleRowCount - 1
                For j As Integer = 0 To view.VisibleColumns.Count - 1
                    list.Add(New KeyValuePair(Of GridCell, Integer)(New GridCell(i, view.VisibleColumns(j)), CInt((grid.GetCellValue(i, view.VisibleColumns(j))))))
                Next j
            Next i
            list.Sort(Function(x As KeyValuePair(Of GridCell, Integer), y As KeyValuePair(Of GridCell, Integer)) Compare(x, y, shouldSelectTopValues))

            grid.BeginSelection()
            view.DataControl.UnselectAll()
            For i As Integer = 0 To Math.Min(20, list.Count) - 1
                view.SelectCell(list(list.Count - i - 1).Key.RowHandle, list(list.Count - i - 1).Key.Column)
            Next i
            grid.EndSelection()
        End Sub

        Private Shared Function Compare(ByVal x As KeyValuePair(Of GridCell, Integer), ByVal y As KeyValuePair(Of GridCell, Integer), ByVal shouldSelectTopValues As Boolean) As Integer
            If shouldSelectTopValues Then
                Return Comparer(Of Integer).Default.Compare(x.Value, y.Value)
            Else
                Return Comparer(Of Integer).Default.Compare(y.Value, x.Value)
            End If
        End Function
    End Class
End Namespace
