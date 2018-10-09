using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarSideBySide3DControl.xaml"),
     CodeFile("Modules/BarSeries/BarSideBySide3DControl.xaml.(cs)")]
    public partial class BarSideBySide3DControl : ChartsDemoModule {
        public BarSideBySide3DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
