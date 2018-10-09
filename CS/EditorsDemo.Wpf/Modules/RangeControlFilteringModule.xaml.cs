using DevExpress.Xpf.DemoBase;

namespace EditorsDemo {
    [CodeFile("ViewModels/RangeControlFilteringViewModel.(cs)")]
    public partial class RangeControlFilteringModule : EditorsDemoModule {
        public RangeControlFilteringModule() {
            InitializeComponent();
            ModuleUnloaded += (s, e) => {
                grid.ItemsSource = null;
                WcfInstantFeedbackDataSource.Dispose();
            };
        }
    }
}
