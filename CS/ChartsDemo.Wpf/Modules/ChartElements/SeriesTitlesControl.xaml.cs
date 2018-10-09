using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Windows;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/SeriesTitlesControl.xaml"),
     CodeFile("Modules/ChartElements/SeriesTitlesControl.xaml.(cs)")]
    public partial class SeriesTitlesControl : ChartsDemoModule {
        public SeriesTitlesControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void Chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            ChartControl chart = sender as ChartControl;
            if (chart == null)
                return;
            if (chart.Diagram.Series.Count > 0 && !chart.Diagram.Series[0].Equals(e.Series))
                e.LegendText = string.Empty;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
