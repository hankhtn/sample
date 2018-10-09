using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/XYDiagram2DControl.xaml"),
     CodeFile("Modules/ChartElements/XYDiagram2DControl.xaml.(cs)")]
    public partial class XYDiagram2DControl : ChartsDemoModule {
        public XYDiagram2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }       
    }
}
