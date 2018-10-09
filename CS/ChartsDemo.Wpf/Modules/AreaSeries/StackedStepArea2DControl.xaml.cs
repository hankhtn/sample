using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/StackedStepArea2DControl.xaml")]
    [CodeFile("Modules/AreaSeries/StackedStepArea2DControl.xaml.(cs)")]
    [CodeFile("ViewModels/CommentsData.(cs)")]
    public partial class StackedStepArea2DControl : ChartsDemoModule {
        public StackedStepArea2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
