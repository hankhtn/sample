using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PieDonutSeries/PieDonut3DControl.xaml"),
     CodeFile("Modules/PieDonutSeries/PieDonut3DControl.xaml.(cs)")]
    [CodeFile("Utils/PieChartSelectionBehavior.(cs)")]
    public partial class PieDonut3DControl : ChartsDemoModule {
        public PieDonut3DControl() {
            InitializeComponent();
            ActualChart = chart;
            Series.ToolTipPointPattern = "{A}: {V:0.0}M km²";
        }
        void chart_QueryChartCursor(object sender, QueryChartCursorEventArgs e) {
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.Position);
            if (hitInfo != null && hitInfo.SeriesPoint != null && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Released)
                e.Cursor = Cursors.Hand;
        }
    }
}
