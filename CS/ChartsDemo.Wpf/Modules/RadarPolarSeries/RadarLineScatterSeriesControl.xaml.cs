using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/RadarLineScatterSeriesControl.xaml"),
     CodeFile("Modules/RadarPolarSeries/RadarLineScatterSeriesControl.xaml.(cs)")]
    [CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")]
    public partial class RadarLineScatterSeriesControl : ChartsDemoModule {
        public RadarLineScatterSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
