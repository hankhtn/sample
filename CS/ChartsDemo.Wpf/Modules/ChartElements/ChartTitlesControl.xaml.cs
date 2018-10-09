using System;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Charts;
using System.Diagnostics;
using System.Windows.Documents;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/ChartTitlesControl.xaml"),
     CodeFile("Modules/ChartElements/ChartTitlesControl.xaml.(cs)")]
    public partial class ChartTitlesControl : ChartsDemoModule {
        const int pointsCount = 40;

        public ChartTitlesControl() {
            InitializeComponent();
            ActualChart = chart;
            CreatePoints(chart.Diagram.Series[0]);
        }
        void ChartsDemoModule_ModuleLoaded(object sender, System.Windows.RoutedEventArgs e) {
            chart.Animate();
        }
        void CreatePoints(Series series) {
            Random random = new Random();
            for (int i = 0; i < pointsCount; i++)
                series.Points.Add(new SeriesPoint(i, random.NextDouble() + 1));
        }
        void Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e) {
            Hyperlink source = sender as Hyperlink;
            if (source != null) {
                Process.Start(source.NavigateUri.ToString());
            }
        }
    }
}
