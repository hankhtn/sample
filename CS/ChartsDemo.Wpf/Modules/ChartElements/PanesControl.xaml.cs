using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/PanesControl.xaml")]
    [CodeFile("Modules/ChartElements/PanesControl.xaml.(cs)")]
    [CodeFile("ViewModels/WeatherData.(cs)")]
    public partial class PanesControl : ChartsDemoModule {
        public PanesControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
