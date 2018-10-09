using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Windows;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/CrosshairCursorControl.xaml"),
     CodeFile("Modules/Interactivity/CrosshairCursorControl.xaml.(cs)")]
    public partial class CrosshairCursorControl : ChartsDemoModule {
        public CrosshairCursorControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
