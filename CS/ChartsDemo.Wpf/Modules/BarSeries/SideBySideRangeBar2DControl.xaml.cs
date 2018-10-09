using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.IO;
using System.Text;
using System.ComponentModel;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/SideBySideRangeBar2DControl.xaml")]
    [CodeFile("Modules/BarSeries/SideBySideRangeBar2DControl.xaml.(cs)")]
    [CodeFile("ViewModels/OilPrices.(cs)")]
    public partial class SideBySideRangeBar2DControl : ChartsDemoModule {
        public SideBySideRangeBar2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
