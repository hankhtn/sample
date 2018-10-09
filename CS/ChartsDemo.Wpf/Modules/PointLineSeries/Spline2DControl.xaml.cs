using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/Spline2DControl.xaml"),
     CodeFile("Modules/PointLineSeries/Spline2DControl.xaml.(cs)")]
    public partial class Spline2DControl : ChartsDemoModule {
        public Spline2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
