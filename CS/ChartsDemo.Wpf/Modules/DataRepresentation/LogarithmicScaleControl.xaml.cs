using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataRepresentation/LogarithmicScaleControl.xaml")]
    [CodeFile("Modules/DataRepresentation/LogarithmicScaleControl.xaml.(cs)")]
    [CodeFile("ViewModels/RegionPopulationData.(cs)")]
    public partial class LogarithmicScaleControl : ChartsDemoModule {
        public LogarithmicScaleControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void AnimateChart(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
