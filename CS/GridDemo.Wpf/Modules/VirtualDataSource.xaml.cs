using DevExpress.Data;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/VirtualDataSourceViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/VirtualDataSourceValuesProvider.(cs)")]
    public partial class VirtualDataSource : GridDemoModule {
        public const int ColumnsCount = 1000;
        public const int RowsCount = 100000000;

        public VirtualDataSource() {
            InitializeComponent();
        }

        public VirtualDataSourceViewModel ViewModel { get { return (VirtualDataSourceViewModel)DataContext; } }

        void virtualDataSource_ValueNeeded(object sender, VirtualSourceValueNeededEventArgs e) {
            e.Value = ViewModel.GetValue(e.RowIndex, e.ColumnIndex);
        }

        void virtualDataSource_ValuePushed(object sender, VirtualSourceValuePushedEventArgs e) {
            ViewModel.SetValue(e.RowIndex, e.ColumnIndex, e.Value);
        }

        void virtualDataSource_PropertyNeeded(object sender, VirtualSourcePropertyNeededEventArgs e) {
            VirtualDataSourceProperty property = ViewModel.CreateProperty(e.Index);
            e.ProperyName = property.Name;
            e.PropertyType = property.Type;
        }
    }
}
