using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Windows;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/RadarRangeAreaSeriesControl.xaml"),
     CodeFile("Modules/RadarPolarSeries/RadarRangeAreaSeriesControl.xaml.(cs)")]
    public partial class RadarRangeAreaSeriesControl : ChartsDemoModule {
        public RadarRangeAreaSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
