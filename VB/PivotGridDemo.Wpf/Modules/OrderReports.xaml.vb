Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Partial Public Class OrderReports
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub ListBoxEdit_SelectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not IsLoaded Then
                Return
            End If
            pivotGrid.BeginUpdate()
            groupOptions.Visibility = System.Windows.Visibility.Collapsed
            fieldOrder.FilterValues.Clear()
            fieldOrder.FilterValues.FilterType = FieldFilterType.Excluded
            fieldOrder.Area = FieldArea.RowArea
            fieldUnitPrice.Area = FieldArea.DataArea
            fieldDiscount.Area = FieldArea.DataArea
            fieldExtendedPrice.Area = FieldArea.DataArea
            fieldQuantity.Area = FieldArea.DataArea

            Select Case reportsList.SelectedIndex
                Case 0
                Case 1
                    fieldOrder.AreaIndex = 0
                    groupOptions.Visibility = System.Windows.Visibility.Visible
                    If orderIDFilter.Items.Count = 0 Then
                        orderIDFilter.Items.AddRange(fieldOrder.GetUniqueValues())
                        orderIDFilter.SelectedIndex = 0
                    End If
                    SetFilter()
                Case 2
                    fieldOrder.Area = FieldArea.FilterArea
                    fieldUnitPrice.Area = FieldArea.FilterArea
                    fieldDiscount.Area = FieldArea.FilterArea
                    fieldExtendedPrice.Area = FieldArea.FilterArea
                Case 3
                    fieldOrder.Area = FieldArea.FilterArea
                    fieldQuantity.Area = FieldArea.FilterArea
                    fieldDiscount.Area = FieldArea.FilterArea
                    fieldExtendedPrice.Area = FieldArea.FilterArea
            End Select
            pivotGrid.EndUpdate()
        End Sub

        Private Sub orderIDFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetFilter()
        End Sub

        Private Sub SetFilter()
            fieldOrder.FilterValues.FilterType = FieldFilterType.Included
            fieldOrder.FilterValues.Clear()
            fieldOrder.FilterValues.Add(orderIDFilter.SelectedItem)
        End Sub
    End Class
End Namespace
