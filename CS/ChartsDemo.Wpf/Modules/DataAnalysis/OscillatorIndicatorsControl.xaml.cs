using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/OscillatorIndicatorsControl.xaml"),
     CodeFile("Modules/DataAnalysis/OscillatorIndicatorsControl.xaml.(cs)")]
    public partial class OscillatorIndicatorsControl : ChartsDemoModule {
        public OscillatorIndicatorsControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void cbeIndicatorKind_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            foreach (Indicator indicator in series.Indicators)
                indicator.Animate();
        }
    }
}
