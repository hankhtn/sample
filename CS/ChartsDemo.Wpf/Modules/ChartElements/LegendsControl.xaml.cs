using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/LegendsControl.xaml"), 
     CodeFile("Modules/ChartElements/LegendsControl.xaml.(cs)")]
    [CodeFile("ViewModels/PerformanceDataSource.(cs)")]
    public partial class LegendsControl : ChartsDemoModule {
        public LegendsControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }

    enum LegendMode {
        Common,
        SeparateInsidePane,
        SeparateOutsidePane
    }
}
