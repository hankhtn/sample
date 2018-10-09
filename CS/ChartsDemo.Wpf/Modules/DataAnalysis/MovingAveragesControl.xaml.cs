using System;
using System.Globalization;
using System.Windows.Data;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/MovingAveragesControl.xaml"),
     CodeFile("Modules/DataAnalysis/MovingAveragesControl.xaml.(cs)")]
    public partial class MovingAveragesControl : ChartsDemoModule {
        public MovingAveragesControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, System.Windows.RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
