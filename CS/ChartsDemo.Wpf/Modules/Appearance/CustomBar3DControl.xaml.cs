using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.ComponentModel;
using DevExpress.Mvvm;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomBar3DControl.xaml"),
     CodeFile("Modules/Appearance/CustomBar3DControl.xaml.(cs)")]
    public partial class CustomBar3DControl : ChartsDemoModule {
        #region Data

        public static List<NumericPoint> PencilPoints { get { return GetPencilPoints(); } }
        public static List<NumericPoint> ChairPoints { get { return GetChairPoints(); } }

        static List<NumericPoint> GetPencilPoints() {
            var points = new List<NumericPoint>();
            points.Add(new NumericPoint(1, 65));
            points.Add(new NumericPoint(2, 78));
            points.Add(new NumericPoint(3, 95));
            points.Add(new NumericPoint(4, 110));
            points.Add(new NumericPoint(5, 108));
            points.Add(new NumericPoint(6, 52));
            points.Add(new NumericPoint(7, 46));
            return points;
        }
        static List<NumericPoint> GetChairPoints() {
            var points = new List<NumericPoint>();
            points.Add(new NumericPoint(1, 55));
            points.Add(new NumericPoint(2, 70));
            points.Add(new NumericPoint(3, 40));
            return points;
        }

        #endregion

        const int clickDelta = 200;

        int mouseDownTime;
        Storyboard seriesPointAnimationStoryboard;

        public CustomBar3DControl() {
            InitializeComponent();
            ActualChart = chart;
            seriesPointAnimationStoryboard = TryFindResource("SeriesPointAnimationStoryboard") as Storyboard;
            if(seriesPointAnimationStoryboard != null && seriesPointAnimationStoryboard.Children.Count > 0)
                Storyboard.SetTarget(seriesPointAnimationStoryboard.Children[0], SeriesPointAnimationRecord);
        }
        bool IsClick(int mouseUpTime) {
            return mouseUpTime - mouseDownTime < clickDelta;
        }
        void chart_MouseDown(object sender, MouseButtonEventArgs e) {
            mouseDownTime = e.Timestamp;
        }
        void chart_MouseUp(object sender, MouseButtonEventArgs e) {
            if(seriesPointAnimationStoryboard == null || SeriesPointAnimation == null || !IsClick(e.Timestamp))
                return;
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.GetPosition(chart));
            if(hitInfo != null && hitInfo.SeriesPoint != null) {
                SeriesPointAnimation.TargetSeriesPoint = hitInfo.SeriesPoint;
                seriesPointAnimationStoryboard.Begin(chart);
            }
        }
        void chart_QueryChartCursor(object sender, QueryChartCursorEventArgs e) {
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.Position);
            if(hitInfo != null && hitInfo.SeriesPoint != null && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Released)
                e.Cursor = Cursors.Hand;
        }
    }

    public class CustomBarModelInfo : BindableBase {
        CustomBar3DModel model;
        List<NumericPoint> points;

        public CustomBar3DModel Model {
            get { return model; }
            set { SetProperty(ref model, value, () => Model); }
        }
        public List<NumericPoint> Points {
            get { return points; }
            set { SetProperty(ref points, value, () => Points); }
        }
        public double BarWidth {
            get { return GetProperty(() => BarWidth); }
            set { SetProperty(() => BarWidth, value); }
        }
        public double SeriesPadding {
            get { return GetProperty(() => SeriesPadding); }
            set { SetProperty(() => SeriesPadding, value); }
        }
    }
}
