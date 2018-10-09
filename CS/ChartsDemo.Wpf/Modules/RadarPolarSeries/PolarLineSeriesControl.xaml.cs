using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/PolarLineSeriesControl.xaml"),
     CodeFile("Modules/RadarPolarSeries/PolarLineSeriesControl.xaml.(cs)")]
    [CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")]
    public partial class PolarLineSeriesControl : ChartsDemoModule {
        public PolarLineSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
