using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System;

namespace ChartsDemo {
    [CodeFile("Modules/Performance/RealtimeChartControl.xaml")]
    [CodeFile("Modules/Performance/RealtimeChartControl.xaml.(cs)")]
    [CodeFile("ViewModels/RealtimeViewModel.(cs)")]
    public partial class RealtimeChartControl : ChartsDemoModule {
        public RealtimeChartControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
