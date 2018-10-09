using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/StepArea2DControl.xaml"),
     CodeFile("Modules/AreaSeries/StepArea2DControl.xaml.(cs)")]
    public partial class StepArea2DControl : ChartsDemoModule {
        public StepArea2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
