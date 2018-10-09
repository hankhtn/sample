using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/ScaleBreaksControl.xaml"),
     CodeFile("Modules/ChartElements/ScaleBreaksControl.xaml.(cs)")]
    public partial class ScaleBreaksControl : ChartsDemoModule {
        public ScaleBreaksControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
