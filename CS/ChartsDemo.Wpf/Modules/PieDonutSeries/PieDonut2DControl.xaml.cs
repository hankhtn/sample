using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Mvvm.UI.Interactivity;
using System.Windows.Data;
using System.Globalization;

namespace ChartsDemo {
    [CodeFile("Modules/PieDonutSeries/PieDonut2DControl.xaml"),
     CodeFile("Modules/PieDonutSeries/PieDonut2DControl.xaml.(cs)")]
    [CodeFile("Utils/PieChartSelectionBehavior.(cs)")]
    [CodeFile("Utils/PieChartRotationBehavior.(cs)")]
    public partial class PieDonut2DControl : ChartsDemoModule {
        public PieDonut2DControl() {
            InitializeComponent();
            ActualChart = chart;
            Series.ToolTipPointPattern = "{A}: {V:0.0}M km²";
        }
        void Animate() {
            if (chart != null)
                chart.Animate();
            if (pieTotalLabel != null)
                ((Storyboard)pieTotalLabel.Resources["pieTotalLabelStoryboard"]).Begin();
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            Animate();
        }
        void rblSweepDirection_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            Animate();
        }
        void chart_QueryChartCursor(object sender, QueryChartCursorEventArgs e) {
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.Position);
            if (hitInfo != null && hitInfo.SeriesPoint != null)
                e.Cursor = Cursors.Hand;
        }
    }

    public class ShowTotalInToTitleVisibleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string str = (string)value;
            return str == "Title";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }

    public class ShowTotalInToPieTotalLabelVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string str = (string)value;
            if (str == "Label")
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
