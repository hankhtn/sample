using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingForXYSeries.xaml")]
    [CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingForXYSeries.xaml.(cs)")]
    [CodeFile("ViewModels/MarsTemperatureData.(cs)")]
    public partial class ResolveLabelsOverlappingForXYSeries : ChartsDemoModule {
        public ResolveLabelsOverlappingForXYSeries() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
