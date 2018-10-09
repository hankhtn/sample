using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/RangeArea2DControl.xaml"),
     CodeFile("Modules/AreaSeries/RangeArea2DControl.xaml.(cs)")]
    public partial class RangeArea2DControl : ChartsDemoModule {
        static string[] predefinedSizes = new string[] { "8", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "30"};

        public RangeArea2DControl() {
            InitializeComponent();
            ActualChart = chart;
            cbeMarker1Size.Items.AddRange(predefinedSizes);
            cbeMarker2Size.Items.AddRange(predefinedSizes);
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
