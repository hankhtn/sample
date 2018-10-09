using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/RadarAreaSeriesControl.xaml")]
    [CodeFile("Modules/RadarPolarSeries/RadarAreaSeriesControl.xaml.(cs)")]
    [CodeFile("ViewModels/WindRoseData.(cs)")]
    public partial class RadarAreaSeriesControl : ChartsDemoModule {
        public RadarAreaSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
            series.ToolTipPointPattern = "Direction: {A}\nSpeed: {V}";
        }      
    }
}
