using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/ManhattanBar3DControl.xaml"),
     CodeFile("Modules/BarSeries/ManhattanBar3DControl.xaml.(cs)")]
    public partial class ManhattanBar3DControl : ChartsDemoModule {
        public ManhattanBar3DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
