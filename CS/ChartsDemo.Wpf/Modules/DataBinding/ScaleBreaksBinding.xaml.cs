using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataBinding/ScaleBreaksBinding.xaml"),
     CodeFile("Modules/DataBinding/ScaleBreaksBinding.xaml.(cs)")]
    public partial class ScaleBreaksBinding : ChartsDemoModule {
        public ScaleBreaksBinding() {
            InitializeComponent();
            ActualChart = chart;
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
