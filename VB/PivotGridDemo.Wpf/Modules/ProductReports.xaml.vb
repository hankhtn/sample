Imports System
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.PivotGrid


Namespace PivotGridDemo

    Partial Public Class ProductReports
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub ReportsList_SelectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If fieldCategory Is Nothing Then
                Return
            End If

            fieldCategory.Area = FieldArea.RowArea
            fieldProduct.Area = FieldArea.FilterArea
            fieldQuarter.Area = FieldArea.FilterArea
            fieldYear.Area = FieldArea.FilterArea
            fieldAverageSale.Visible = False
            fieldMinimumSale.Visible = False
            fieldProduct.SortByField = Nothing
            fieldProduct.SortOrder = FieldSortOrder.Ascending
            fieldProduct.TopValueCount = 0
            fieldMonth.Area = FieldArea.FilterArea
            fieldMonth.Visible = False
            fieldQuarter.Area = FieldArea.FilterArea
            fieldCategory.TotalsVisibility = FieldTotalsVisibility.AutomaticTotals
            fieldCategory.CustomTotals.Clear()
            cbxShowCategories.Visibility = System.Windows.Visibility.Collapsed
            spGroupingLayout.Visibility = System.Windows.Visibility.Collapsed

            Select Case reportsList.SelectedIndex
                Case 0
                    fieldCategory.Area = FieldArea.RowArea
                    fieldProduct.Area = FieldArea.FilterArea
                Case 1
                    fieldCategory.Area = FieldArea.FilterArea
                    fieldProduct.Area = FieldArea.RowArea
                    cbxShowCategories.Visibility = System.Windows.Visibility.Visible
                Case 2
                    fieldMonth.Visible = True
                    fieldCategory.Area = FieldArea.RowArea
                    fieldProduct.Area = FieldArea.RowArea
                    fieldQuarter.Area = FieldArea.ColumnArea
                    fieldProduct.AreaIndex = 1
                    spGroupingLayout.Visibility = System.Windows.Visibility.Visible
                    ComboBoxEdit_SelectedIndexChanged(cbeGroupingLayout, e)
                Case 3
                    fieldCategory.TotalsVisibility = FieldTotalsVisibility.CustomTotals
                    fieldCategory.Area = FieldArea.RowArea
                    fieldProduct.Area = FieldArea.RowArea
                    fieldCategory.CustomTotals.Add(FieldSummaryType.Average)
                    fieldCategory.CustomTotals.Add(FieldSummaryType.Sum)
                    fieldCategory.CustomTotals.Add(FieldSummaryType.Max)
                    fieldCategory.CustomTotals.Add(FieldSummaryType.Min)
                    fieldMonth.Visible = True
                    fieldQuarter.Area = FieldArea.ColumnArea
                    fieldYear.Area = FieldArea.ColumnArea
                    fieldProduct.AreaIndex = 1
                Case 4
                    fieldQuarter.SetAreaPosition(FieldArea.RowArea, 0)
                    fieldCategory.Area = FieldArea.RowArea
                    fieldAverageSale.SetAreaPosition(FieldArea.DataArea, 1)
                    fieldMinimumSale.SetAreaPosition(FieldArea.DataArea, 2)
                    fieldMinimumSale.Visible = True
                    fieldAverageSale.Visible = fieldMinimumSale.Visible
                    fieldCategory.AreaIndex = 1
                Case 5
                    fieldProduct.Area = FieldArea.RowArea
                    fieldCategory.Area = FieldArea.RowArea
                    fieldProduct.SortByField = fieldSales
                    fieldProduct.SortOrder = FieldSortOrder.Descending
                    fieldProduct.TopValueCount = 3
                    fieldProduct.AreaIndex = 1
            End Select
        End Sub
        Private Sub cbxShowCategories_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            fieldCategory.Area = If(cbxShowCategories.IsChecked.Value, FieldArea.RowArea, FieldArea.FilterArea)
            fieldCategory.AreaIndex = 0
        End Sub
        Private Sub ComboBoxEdit_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If fieldMonth Is Nothing Then
                Return
            End If
            Select Case DirectCast(sender, ComboBoxEdit).SelectedIndex
                Case 0
                    fieldQuarter.Area = FieldArea.FilterArea
                    fieldMonth.Area = FieldArea.FilterArea
                    fieldYear.Area = FieldArea.ColumnArea
                Case 1
                    fieldYear.Area = FieldArea.FilterArea
                    fieldMonth.Area = FieldArea.FilterArea
                    fieldQuarter.Area = FieldArea.ColumnArea
                Case 2
                    fieldYear.Area = FieldArea.FilterArea
                    fieldQuarter.Area = FieldArea.FilterArea
                    fieldMonth.Area = FieldArea.ColumnArea
                Case 3
                    fieldYear.Area = FieldArea.ColumnArea
                    fieldQuarter.Area = FieldArea.ColumnArea
                    fieldMonth.Area = FieldArea.ColumnArea
            End Select
        End Sub
    End Class
End Namespace
