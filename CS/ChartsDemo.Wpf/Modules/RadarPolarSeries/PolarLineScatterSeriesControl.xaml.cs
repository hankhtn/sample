using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/PolarLineScatterSeriesControl.xaml"),
     CodeFile("Modules/RadarPolarSeries/PolarLineScatterSeriesControl.xaml.(cs)")]
    [CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")]
    public partial class PolarLineScatterSeriesControl : ChartsDemoModule {
        public PolarLineScatterSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
