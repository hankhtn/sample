using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ResolveLabelsOverlapping/ResolveOverlappingForAxisLabels.xaml")]
    [CodeFile("Modules/ResolveLabelsOverlapping/ResolveOverlappingForAxisLabels.xaml.(cs)")]
    [CodeFile("ViewModels/HumidityData.(cs)")]
    public partial class ResolveOverlappingForAxisLabels : ChartsDemoModule {
        public ResolveOverlappingForAxisLabels() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
