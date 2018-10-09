using System;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Utils;
using DevExpress.Xpf.Gauges;

namespace GaugesDemo {
    public partial class CircularScales : GaugesDemoModule {
        DispatcherTimer timer = new DispatcherTimer();

        public CircularScales() {
            InitializeComponent();
            UpdateTime();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            CreateCustomLabels(watchNewYorkScale);
            CreateCustomLabels(watchLondonScale);
            CreateCustomLabels(watchMoscowScale);
        }
        void OnTimedEvent(object source, EventArgs e) {
            UpdateTime();
        }
        void UpdateTime() {
            DateTime nowUtc = DateTime.UtcNow;
            DateTime newYorkTime = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            DateTime londonTime = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            DateTime moscowTime = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"));
            hourIndicatorNewYork.Value = newYorkTime.Hour % 12;
            hourIndicatorLondon.Value = londonTime.Hour % 12;
            hourIndicatorMoscow.Value = moscowTime.Hour % 12;
            minuteIndicatorNewYork.Value = newYorkTime.Minute / 60.0 * 12.0;
            minuteIndicatorLondon.Value = londonTime.Minute / 60.0 * 12.0;
            minuteIndicatorMoscow.Value = moscowTime.Minute / 60.0 * 12.0;
            secondIndicatorNewYork.Value = newYorkTime.Second / 60.0 * 12.0;
            secondIndicatorLondon.Value = londonTime.Second / 60.0 * 12.0;
            secondIndicatorMoscow.Value = moscowTime.Second / 60.0 * 12.0;
        }
        void UserCustomLabels_Checked(object sender, RoutedEventArgs e) {
            ChangeVisibilityLabelsAndCustomLabels(watchNewYorkScale, userCustomLabelsCheckEdit.IsChecked.Value, !userCustomLabelsCheckEdit.IsChecked.Value);
            ChangeVisibilityLabelsAndCustomLabels(watchLondonScale, userCustomLabelsCheckEdit.IsChecked.Value, !userCustomLabelsCheckEdit.IsChecked.Value);
            ChangeVisibilityLabelsAndCustomLabels(watchMoscowScale, userCustomLabelsCheckEdit.IsChecked.Value, !userCustomLabelsCheckEdit.IsChecked.Value);
        }
        void ShowLabels_Unchecked(object sender, RoutedEventArgs e) {
            ChangeVisibilityLabelsAndCustomLabels(watchNewYorkScale, false, false);
            ChangeVisibilityLabelsAndCustomLabels(watchLondonScale, false, false);
            ChangeVisibilityLabelsAndCustomLabels(watchMoscowScale, false, false);
        }
        void CreateCustomLabels(ArcScale scale) {
            for (int i = 1; i < 13; i++) {
                ScaleCustomLabel label = new ScaleCustomLabel() { RenderTransformOrigin = new Point(0.5, 0.5) };
                label.Value = i;
                label.Offset = scale.LabelOptions.Offset;
                label.Content = Utils.ConvertArabicToRoman(i);
                label.Visible = false;
                scale.CustomLabels.Add(label);
            }
        }
        void ChangeVisibilityLabelsAndCustomLabels(ArcScale scale, bool showCustomLabels, bool showLabels) {
            foreach (ScaleCustomLabel label in scale.CustomLabels)
                label.Visible = showCustomLabels;
            scale.ShowLabels = showLabels ? DefaultBoolean.True : DefaultBoolean.False;
        }
    }
}
