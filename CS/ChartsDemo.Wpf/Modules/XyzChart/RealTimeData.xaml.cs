using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System;
using System.Windows;

namespace ChartsDemo {
    [CodeFile("Modules/XyzChart/RealTimeData.xaml")]
    [CodeFile("Modules/XyzChart/RealTimeData.xaml.(cs)")]
    [CodeFile("ViewModels/RealTimeSurfaceViewModel.(cs)")]
    public partial class RealTimeData : ChartsDemoModule {
        public RealTimeData() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
