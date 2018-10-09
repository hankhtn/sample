using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System;
using System.Linq;

namespace ChartsDemo {
    [CodeFile("Modules/XyzChart/Bar3DSeriesView.xaml")]
    [CodeFile("Modules/XyzChart/Bar3DSeriesView.xaml.(cs)")]
    [CodeFile("ViewModels/Bar3DViewModel.(cs)")]
    public partial class Bar3DSeriesView : ChartsDemoModule {
        public Bar3DSeriesView() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
