using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Performance/LargeDataSourceControl.xaml"),
     CodeFile("Modules/Performance/LargeDataSourceControl.xaml.(cs)")]
    public partial class LargeDataSourceControl : ChartsDemoModule {
        public LargeDataSourceControl() {
            InitializeComponent();
            ActualChart = chart;
            LoadPoints();
        }
        void LoadPoints() {
            int pointCount = (int)cbPointCount.SelectedItem;
            double value = 0;
            Random random = new Random();
            series.Points.BeginInit();
            series.Points.Clear();
            for (int i = 0; i < pointCount; i++) {
                series.Points.Add(new SeriesPoint((double)i, value));
                value += (random.NextDouble() * 10.0 - 5.0);
                if (value > 1200)
                    value -= 500;
                if (value < -1200)
                    value += 500;
            }
            series.Points.EndInit();
            axisX.VisualRange.SetMinMaxValues(3 * pointCount / 8, 5 * pointCount / 8);
            axisX.WholeRange.SetMinMaxValues(0, pointCount);
        }
        void ExecuteIdle(Action operation) {
            Dispatcher.BeginInvoke(operation, DispatcherPriority.Background);
        }
        void HideLoadingPanel() {
            loadingDecorator.IsSplashScreenShown = false;
        }
        void ShowLoadingPanel() {
            loadingDecorator.IsSplashScreenShown = true;
        }
        void cbPointCount_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            ShowLoadingPanel();
            ExecuteIdle(LoadPoints);
            ExecuteIdle(HideLoadingPanel);
        }
    }
}
