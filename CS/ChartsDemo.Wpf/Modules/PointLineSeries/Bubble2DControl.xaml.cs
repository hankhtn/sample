using System.Data;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/Bubble2DControl.xaml"),
     CodeFile("Modules/PointLineSeries/Bubble2DControl.xaml.(cs)")]
    public partial class Bubble2DControl : ChartsDemoModule {
        public Bubble2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            e.LegendText = ((string)((DataRowView)e.SeriesPoint.Tag).Row["Title"]).Replace("\n", " ");
        }
    }
}
