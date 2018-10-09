Imports System.Collections.Generic
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Grid

Namespace GridDemo
    Partial Public Class ExcelStyleFiltering
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = OrderDataGenerator.GenerateVehicleOrders(10000)
        End Sub

        Private Sub OnShowFilterPopup(ByVal sender As Object, ByVal e As FilterPopupEventArgs)
            Dim fieldName As String = e.Column.FieldName
            If fieldName = "Model.MPGCity" Then
                e.ExcelColumnFilterSettings.FilterItems = New List(Of Object) From {
                    New CustomComboBoxItem With {.DisplayValue = "Low", .EditValue = CriteriaOperator.Parse("[Model.MPGCity] <= 15")},
                    New CustomComboBoxItem With {.DisplayValue = "Medium", .EditValue = CriteriaOperator.Parse("[Model.MPGCity] > 15 AND [Model.MPGCity] < 25")},
                    New CustomComboBoxItem With {.DisplayValue = "High", .EditValue = CriteriaOperator.Parse("[Model.MPGCity] >= 25")}
                }
            ElseIf fieldName = "Model.MPGHighway" Then
                e.ExcelColumnFilterSettings.FilterItems = New List(Of Object) From {
                    New CustomComboBoxItem With {.DisplayValue = "Low", .EditValue = CriteriaOperator.Parse("[Model.MPGHighway] <= 20")},
                    New CustomComboBoxItem With {.DisplayValue = "Medium", .EditValue = CriteriaOperator.Parse("[Model.MPGHighway] > 20 AND [Model.MPGHighway] < 30")},
                    New CustomComboBoxItem With {.DisplayValue = "High", .EditValue = CriteriaOperator.Parse("[Model.MPGHighway] >= 30")}
                }
            ElseIf fieldName = "Model.Modification" Then
                e.ExcelColumnFilterSettings.FilterItems = New List(Of Object) From {
                    New CustomComboBoxItem With {.DisplayValue = "Automatic Transmission (6-speed)", .EditValue = CriteriaOperator.Parse("Contains([Model.Modification], '6A')")},
                    New CustomComboBoxItem With {.DisplayValue = "Automatic Transmission (8-speed)", .EditValue = CriteriaOperator.Parse("Contains([Model.Modification], '8A')")},
                    New CustomComboBoxItem With {.DisplayValue = "Manual Transmission (6-speed)", .EditValue = CriteriaOperator.Parse("Contains([Model.Modification], '6M')")},
                    New CustomComboBoxItem With {.DisplayValue = "Manual Transmission (7-speed)", .EditValue = CriteriaOperator.Parse("Contains([Model.Modification], '7M')")},
                    New CustomComboBoxItem With {.DisplayValue = "Variadic Transmission", .EditValue = CriteriaOperator.Parse("Contains([Model.Modification], 'VA')")},
                    New CustomComboBoxItem With {.DisplayValue = "Limited Edition", .EditValue = CriteriaOperator.Parse("Contains([Model.Modification], 'Limited')")}
                }
            End If
        End Sub
    End Class
End Namespace
