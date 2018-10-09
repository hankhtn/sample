using System;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/XyzChart/Point3DSeriesView.xaml")]
    [CodeFile("Modules/XyzChart/Point3DSeriesView.xaml.(cs)")]
    [CodeFile("ViewModels/StarsData.(cs)")]
    public partial class Point3DSeriesView : ChartsDemoModule {
        public Point3DSeriesView() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
