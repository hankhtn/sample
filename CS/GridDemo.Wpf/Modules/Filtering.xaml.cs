using DevExpress.Data.Filtering;
using DevExpress.Xpf.Editors;
using System;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/FilteringTemplates.xaml")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/FilteringClasses.(cs)")]
    public partial class Filtering : GridDemoModule {
        public Filtering() {
            InitializeComponent();

            grid.FilterCriteria = new OperandProperty("City") == "Bergamo"; 
            grid.FilterCriteria = new OperandProperty("UnboundOrderDate") >= new DateTime(DateTime.Today.Year, 1, 1);
            viewsListBox.EditValueChanging += ViewsListBox_EditValueChanging;
        }

        private void ViewsListBox_EditValueChanging(object sender, EditValueChangingEventArgs e) {
            grid.View.IncrementalSearchEnd();
        }
    }
}
