using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class SingleTotal : PivotGridDemoModule {
        public static IEnumerable<FieldSummaryType> FieldSummaryTypes {
            get {
                return typeof(FieldSummaryType).GetEnumValues()
                    .Cast<FieldSummaryType>()
                    .Except(FieldSummaryType.Custom.Yield());
            }
        }
        public SingleTotal() {
            InitializeComponent();
            cbField.ItemsSource = pivotGrid.Fields.Where(x => x.Area == FieldArea.DataArea && x.Visible);
        }
    }
}
