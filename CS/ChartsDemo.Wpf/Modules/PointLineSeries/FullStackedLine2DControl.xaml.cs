using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/FullStackedLine2DControl.xaml"),
     CodeFile("Modules/PointLineSeries/FullStackedLine2DControl.xaml.(cs)")]
    public partial class FullStackedLine2DControl : ChartsDemoModule {
        public FullStackedLine2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
