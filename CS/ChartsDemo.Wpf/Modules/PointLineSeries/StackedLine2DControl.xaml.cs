using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/StackedLine2DControl.xaml"),
     CodeFile("Modules/PointLineSeries/StackedLine2DControl.xaml.(cs)")]
    public partial class StackedLine2DControl : ChartsDemoModule {      
        public StackedLine2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
