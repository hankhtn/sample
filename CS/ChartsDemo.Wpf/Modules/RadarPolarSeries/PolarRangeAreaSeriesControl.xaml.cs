using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Collections.Generic;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/PolarRangeAreaSeriesControl.xaml"),
     CodeFile("Modules/RadarPolarSeries/PolarRangeAreaSeriesControl.xaml.(cs)")]
    [CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")]
    public partial class PolarRangeAreaSeriesControl : ChartsDemoModule {
        public PolarRangeAreaSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
