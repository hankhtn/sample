using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarSideBySideStacked2DControl.xaml"),
     CodeFile("Modules/BarSeries/BarSideBySideStacked2DControl.xaml.(cs)")]
    public partial class BarSideBySideStacked2DControl : ChartsDemoModule {
        public BarSideBySideStacked2DControl() {
            InitializeComponent();
            ActualChart = chart;
            chart.DataSource = AgeStructure.GetDataByAgeAndGender();
            GroupSeries();
        }

        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void lbGroupBy_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            GroupSeries();
            chart.Animate();
        }
        void GroupSeries() {
            foreach (BarSideBySideStackedSeries2D series in chart.Diagram.Series) {
                GenderAgeInfo genderAge = (GenderAgeInfo)series.Tag;
                series.StackedGroup = lbGroupBy.SelectedIndex == 0 ? genderAge.Gender : genderAge.Age;
                if ((string)series.StackedGroup == "65 years and older") {
                    series.StackedGroup = "65+ years";
                }
            }
        }
    }
}
