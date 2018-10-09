using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/Line2DControl.xaml"),
     CodeFile("Modules/PointLineSeries/Line2DControl.xaml.(cs)")]
    public partial class Line2DControl : ChartsDemoModule {
        public Line2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
