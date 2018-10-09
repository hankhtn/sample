using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/Point3DControl.xaml"),
     CodeFile("Modules/PointLineSeries/Point3DControl.xaml.(cs)")]
    public partial class Point3DControl : ChartsDemoModule {
        public Point3DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
