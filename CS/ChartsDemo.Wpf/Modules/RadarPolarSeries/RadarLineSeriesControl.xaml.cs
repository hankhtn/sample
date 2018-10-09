using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/RadarLineSeriesControl.xaml")]
    [CodeFile("Modules/RadarPolarSeries/RadarLineSeriesControl.xaml.(cs)")]
    [CodeFile("ViewModels/WindRoseData.(cs)")]
    public partial class RadarLineSeriesControl : ChartsDemoModule {
        public RadarLineSeriesControl() {
            InitializeComponent();
            ActualChart = chart;
            series.ToolTipPointPattern = "Direction: {A}\nSpeed: {V}";
        }
    }
}
