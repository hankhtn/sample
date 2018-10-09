using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/StepLine2DControl.xaml"),
     CodeFile("Modules/PointLineSeries/StepLine2DControl.xaml.(cs)")]
    public partial class StepLine2DControl : ChartsDemoModule {
        public StepLine2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
       
    }
}
