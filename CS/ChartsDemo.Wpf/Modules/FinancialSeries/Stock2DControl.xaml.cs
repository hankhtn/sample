using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Windows;

namespace ChartsDemo {
    [CodeFile("Modules/FinancialSeries/Stock2DControl.xaml"),
     CodeFile("Modules/FinancialSeries/Stock2DControl.xaml.(cs)")]
    public partial class Stock2DControl : ChartsDemoModule {
        public Stock2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
