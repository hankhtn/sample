using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarSideBySideFullStacked2DControl.xaml"),
     CodeFile("Modules/BarSeries/BarSideBySideFullStacked2DControl.xaml.(cs)")]
    public partial class BarSideBySideFullStacked2DControl : ChartsDemoModule {
        public BarSideBySideFullStacked2DControl() {
            InitializeComponent();
            ActualChart = chart;
            chart.DataSource = AgeStructure.GetDataByAgeAndGender();
            GroupSeries();
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void chbPercent_Checked(object sender, RoutedEventArgs e) {
            foreach (BarFullStackedSeries2D series in chart.Diagram.Series)
                series.Label.TextPattern = "{VP:P0}";
        }
        void chbPercent_Unchecked(object sender, RoutedEventArgs e) {
            foreach (BarFullStackedSeries2D series in chart.Diagram.Series)
                series.Label.TextPattern = "{V:0,,.}M";
        }
        void lbGroupBy_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            GroupSeries();
            chart.Animate();
        }
        void GroupSeries() {
            foreach (BarSideBySideFullStackedSeries2D series in chart.Diagram.Series) {
                GenderAgeInfo genderAge = (GenderAgeInfo)series.Tag;
                series.StackedGroup = lbGroupBy.SelectedIndex == 0 ? genderAge.Gender : genderAge.Age;
            }
        }
    }
}
