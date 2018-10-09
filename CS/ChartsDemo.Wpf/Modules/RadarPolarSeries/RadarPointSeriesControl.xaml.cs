using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/RadarPointSeriesControl.xaml")]
    [CodeFile("Modules/RadarPolarSeries/RadarPointSeriesControl.xaml.(cs)")]
    [CodeFile("ViewModels/TemperatureData.(cs)")]
    public partial class RadarPointSeriesControl : ChartsDemoModule {
        public RadarPointSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
