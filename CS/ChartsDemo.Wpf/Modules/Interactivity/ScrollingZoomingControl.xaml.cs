using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/ScrollingZoomingControl.xaml")]
    [CodeFile("Modules/Interactivity/ScrollingZoomingControl.xaml.(cs)")]
    [CodeFile("ViewModels/PricesModel.(cs)")]
    public partial class ScrollingZoomingControl : ChartsDemoModule {
        public ScrollingZoomingControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
