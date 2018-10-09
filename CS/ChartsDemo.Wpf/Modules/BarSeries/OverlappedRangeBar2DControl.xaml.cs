using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/OverlappedRangeBar2DControl.xaml")]
    [CodeFile("Modules/BarSeries/OverlappedRangeBar2DControl.xaml.(cs)")]
    [CodeFile("ViewModels/OilPrices.(cs)")]
    public partial class OverlappedRangeBar2DControl : ChartsDemoModule {
        public OverlappedRangeBar2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
