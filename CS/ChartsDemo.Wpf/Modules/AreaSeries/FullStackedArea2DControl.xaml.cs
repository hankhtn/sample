using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/FullStackedArea2DControl.xaml"),
     CodeFile("Modules/AreaSeries/FullStackedArea2DControl.xaml.(cs)")]
    public partial class FullStackedArea2DControl : ChartsDemoModule {
        public FullStackedArea2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void chbPercent_Checked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (AreaFullStackedSeries2D series in ((XYDiagram2D)chart.Diagram).Series)
                    series.Label.TextPattern = "{VP:P2}";
            }
        }
        void chbPercent_UnChecked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (AreaFullStackedSeries2D series in ((XYDiagram2D)chart.Diagram).Series)
                    series.Label.TextPattern = "${V}M";
            }
        }
    }
}
