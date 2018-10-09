using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarFullStacked2DControl.xaml"),
     CodeFile("Modules/BarSeries/BarFullStacked2DControl.xaml.(cs)")]
    public partial class BarFullStacked2DControl : ChartsDemoModule {
        public BarFullStacked2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
