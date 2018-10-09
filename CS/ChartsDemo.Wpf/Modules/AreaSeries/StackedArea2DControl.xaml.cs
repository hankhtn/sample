using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/StackedArea2DControl.xaml"),
     CodeFile("Modules/AreaSeries/StackedArea2DControl.xaml.(cs)")]
    public partial class StackedArea2DControl : ChartsDemoModule {
        public StackedArea2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
