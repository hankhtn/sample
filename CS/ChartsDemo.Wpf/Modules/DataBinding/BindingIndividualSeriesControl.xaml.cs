using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataBinding/BindingIndividualSeriesControl.xaml"),
     CodeFile("Modules/DataBinding/BindingIndividualSeriesControl.xaml.(cs)")]
    public partial class BindingIndividualSeriesControl : ChartsDemoModule {
        public BindingIndividualSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
            series.ToolTipPointPattern = "X = {A}\nY = {V}";
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
       
    }
    public class PointCollection : ObservableCollection<Point> { 
    }
}
