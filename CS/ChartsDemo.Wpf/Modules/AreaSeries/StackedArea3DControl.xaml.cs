using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/StackedArea3DControl.xaml"),
     CodeFile("Modules/AreaSeries/StackedArea3DControl.xaml.(cs)")]
    public partial class StackedArea3DControl : ChartsDemoModule {
        public StackedArea3DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
