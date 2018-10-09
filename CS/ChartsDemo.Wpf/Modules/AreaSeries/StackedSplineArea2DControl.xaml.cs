using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/StackedSplineArea2DControl.xaml"),
     CodeFile("Modules/AreaSeries/StackedSplineArea2DControl.xaml.(cs)")]
    public partial class StackedSplineArea2DControl : ChartsDemoModule {
        public StackedSplineArea2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
