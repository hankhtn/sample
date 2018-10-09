using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;

namespace GridDemo {
    
    
    
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/AutoFilterRowTemplates.xaml")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/AutoFilterRowClasses.(cs)")]
	public partial class AutoFilterRow : GridDemoModule {
		public AutoFilterRow() {
			InitializeComponent();
			grid.ItemsSource = OutlookDataGenerator.CreateOutlookDataTable(1000);
            ComboBoxEditSettings settings = new ComboBoxEditSettings() { IsTextEditable = false };
            ComboBoxEdit.SetupComboBoxSettingsEnumItemSource<Priority>(settings);
            colPriority.EditSettings = settings;
		}
	}
}
