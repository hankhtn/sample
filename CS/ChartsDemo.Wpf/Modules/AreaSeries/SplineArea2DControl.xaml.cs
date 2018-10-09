using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/SplineArea2DControl.xaml"),
     CodeFile("Modules/AreaSeries/SplineArea2DControl.xaml.(cs)")]
    public partial class SplineArea2DControl : ChartsDemoModule {
        public SplineArea2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
