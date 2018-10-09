using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/TrendIndicatorsControl.xaml"),
     CodeFile("Modules/DataAnalysis/TrendIndicatorsControl.xaml.(cs)")]
    public partial class TrendIndicatorsControl : ChartsDemoModule {
        public TrendIndicatorsControl() {
            InitializeComponent();
            ActualChart = chart;
            chart.Diagram.Series[0].DataSource = CsvReader.ReadFinancialData("USDJPYDaily.csv");
        }

        void ChartsDemoModule_ModuleLoaded(object sender, System.Windows.RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
