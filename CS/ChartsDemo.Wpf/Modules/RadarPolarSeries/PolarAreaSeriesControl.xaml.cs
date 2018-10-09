using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Collections.Generic;
using System.Linq;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/PolarAreaSeriesControl.xaml")]
    [CodeFile("Modules/RadarPolarSeries/PolarAreaSeriesControl.xaml.(cs)")]
    [CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")]
    public partial class PolarAreaSeriesControl : ChartsDemoModule {
        public PolarAreaSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
