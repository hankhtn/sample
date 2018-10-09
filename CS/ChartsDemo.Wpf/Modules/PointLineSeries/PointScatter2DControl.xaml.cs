using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Windows;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/PointScatter2DControl.xaml"),
     CodeFile("Modules/PointLineSeries/PointScatter2DControl.xaml.(cs)")]
    public partial class PointScatter2DControl : ChartsDemoModule {
        public PointScatter2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
