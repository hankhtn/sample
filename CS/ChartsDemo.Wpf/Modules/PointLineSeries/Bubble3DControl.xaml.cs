using System;
using System.Windows;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Data;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/Bubble3DControl.xaml"),
     CodeFile("Modules/PointLineSeries/Bubble3DControl.xaml.(cs)")]
    public partial class Bubble3DControl : ChartsDemoModule {
        public Bubble3DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            e.LegendText = ((string)((DataRowView)e.SeriesPoint.Tag).Row["Title"]).Replace("\n", " ");
        }
        void Bubble3DDemo_ModuleLoaded(object sender, RoutedEventArgs e) {
            var sizeAnimation = Series.TryFindResource("SizeAnimationStoryboard") as Storyboard;
            if(sizeAnimation != null)
                sizeAnimation.Begin(Series);
        }
        void Storyboard_Completed(object sender, EventArgs e) {
            Series.MaxSize = 2.2;
            Series.MinSize = 0.4;
        }
    }
}
