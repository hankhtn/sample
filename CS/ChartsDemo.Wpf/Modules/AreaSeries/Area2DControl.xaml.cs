using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/Area2DControl.xaml"),
     CodeFile("Modules/AreaSeries/Area2DControl.xaml.(cs)")]
    public partial class Area2DControl : ChartsDemoModule {
        public Area2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
