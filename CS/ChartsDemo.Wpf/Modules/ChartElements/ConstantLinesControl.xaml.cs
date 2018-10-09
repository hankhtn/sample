using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/ConstantLinesControl.xaml"),
     CodeFile("Modules/ChartElements/ConstantLinesControl.xaml.(cs)")]
    public partial class ConstantLinesControl : ChartsDemoModule {
        public ConstantLinesControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
