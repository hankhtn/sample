using System;
using System.Windows.Data;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/FunnelSeries/Funnel2DControl.xaml"),
     CodeFile("Modules/FunnelSeries/Funnel2DControl.xaml.(cs)")]
    public partial class Funnel2DControl : ChartsDemoModule {
        public Funnel2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
