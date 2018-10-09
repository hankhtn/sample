using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Grid;

namespace GridDemo {
    public partial class ExcelStyleFiltering : GridDemoModule {
        public ExcelStyleFiltering() {
            InitializeComponent();
            grid.ItemsSource = OrderDataGenerator.GenerateVehicleOrders(10000);
        }

        void OnShowFilterPopup(object sender, FilterPopupEventArgs e) {
            string fieldName = e.Column.FieldName;
            if(fieldName == "Model.MPGCity") {
                e.ExcelColumnFilterSettings.FilterItems = new List<object> {
                    new CustomComboBoxItem { DisplayValue = "Low", EditValue = CriteriaOperator.Parse("[Model.MPGCity] <= 15") },
                    new CustomComboBoxItem { DisplayValue = "Medium", EditValue = CriteriaOperator.Parse("[Model.MPGCity] > 15 AND [Model.MPGCity] < 25") },
                    new CustomComboBoxItem { DisplayValue = "High", EditValue = CriteriaOperator.Parse("[Model.MPGCity] >= 25") }
                };
            } else if(fieldName == "Model.MPGHighway") {
                e.ExcelColumnFilterSettings.FilterItems = new List<object> {
                    new CustomComboBoxItem { DisplayValue = "Low", EditValue = CriteriaOperator.Parse("[Model.MPGHighway] <= 20") },
                    new CustomComboBoxItem { DisplayValue = "Medium", EditValue = CriteriaOperator.Parse("[Model.MPGHighway] > 20 AND [Model.MPGHighway] < 30") },
                    new CustomComboBoxItem { DisplayValue = "High", EditValue = CriteriaOperator.Parse("[Model.MPGHighway] >= 30") }
                };
            } else if(fieldName == "Model.Modification") {
                e.ExcelColumnFilterSettings.FilterItems = new List<object> {
                    new CustomComboBoxItem { DisplayValue = "Automatic Transmission (6-speed)", EditValue = CriteriaOperator.Parse(@"Contains([Model.Modification], '6A')") },
                    new CustomComboBoxItem { DisplayValue = "Automatic Transmission (8-speed)", EditValue = CriteriaOperator.Parse("Contains([Model.Modification], '8A')") },
                    new CustomComboBoxItem { DisplayValue = "Manual Transmission (6-speed)", EditValue = CriteriaOperator.Parse("Contains([Model.Modification], '6M')") },
                    new CustomComboBoxItem { DisplayValue = "Manual Transmission (7-speed)", EditValue = CriteriaOperator.Parse("Contains([Model.Modification], '7M')") },
                    new CustomComboBoxItem { DisplayValue = "Variadic Transmission", EditValue = CriteriaOperator.Parse("Contains([Model.Modification], 'VA')") },
                    new CustomComboBoxItem { DisplayValue = "Limited Edition", EditValue = CriteriaOperator.Parse("Contains([Model.Modification], 'Limited')") }
                };
            }
        }
    }
}
