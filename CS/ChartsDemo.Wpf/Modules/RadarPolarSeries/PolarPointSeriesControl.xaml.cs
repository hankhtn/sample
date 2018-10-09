using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/PolarPointSeriesControl.xaml"),
     CodeFile("Modules/RadarPolarSeries/PolarPointSeriesControl.xaml.(cs)")]
    [CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")]
    public partial class PolarPointSeriesControl : ChartsDemoModule {
        public PolarPointSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
