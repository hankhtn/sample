using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/FullStackedStepArea2DControl.xaml")]
    [CodeFile("Modules/AreaSeries/FullStackedStepArea2DControl.xaml.(cs)")]
    [CodeFile("ViewModels/CommentsData.(cs)")]
    public partial class FullStackedStepArea2DControl : ChartsDemoModule {
        public FullStackedStepArea2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
