using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarSideBySide2DControl.xaml"),
     CodeFile("Modules/BarSeries/BarSideBySide2DControl.xaml.(cs)")]
    public partial class BarSideBySide2DControl : ChartsDemoModule {
        public BarSideBySide2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
