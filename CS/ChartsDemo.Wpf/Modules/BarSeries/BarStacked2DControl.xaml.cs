using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarStacked2DControl.xaml"),
     CodeFile("Modules/BarSeries/BarStacked2DControl.xaml.(cs)")]
    public partial class BarStacked2DControl : ChartsDemoModule {
        public BarStacked2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
