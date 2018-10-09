using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataBinding/TagPropertyControl.xaml"),
     CodeFile("Modules/DataBinding/TagPropertyControl.xaml.(cs)"),
     CodeFile("ViewModels/CountryData.(cs)")]
    public partial class TagPropertyControl : ChartsDemoModule {
        public TagPropertyControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            e.LegendText = ((Country)e.SeriesPoint.Tag).OfficialName;
        }
    }
}
