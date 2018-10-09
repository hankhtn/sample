using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Linq;
using System;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/BasicFinancialIndicatorsControl.xaml"),
     CodeFile("Modules/DataAnalysis/BasicFinancialIndicatorsControl.xaml.(cs)")]
    public partial class BasicFinancialIndicatorsControl : ChartsDemoModule {
        public BasicFinancialIndicatorsControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void ComboBoxEdit_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            Dispatcher.BeginInvoke(new Action(() => {
                foreach(var indicator in series.Indicators) indicator.Animate();
            }));
        }
    }
}
