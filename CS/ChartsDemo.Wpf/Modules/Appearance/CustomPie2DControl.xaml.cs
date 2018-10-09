using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomPie2DControl.xaml"),
     CodeFile("Modules/Appearance/CustomPie2DControl.xaml.(cs)")]
    public partial class CustomPie2DControl : ChartsDemoModule {
        public CustomPie2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
