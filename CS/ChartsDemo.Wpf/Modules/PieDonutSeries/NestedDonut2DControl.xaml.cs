using System.ComponentModel;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Collections.Generic;

namespace ChartsDemo {
    [CodeFile("Modules/PieDonutSeries/NestedDonut2DControl.xaml"),
     CodeFile("Modules/PieDonutSeries/NestedDonut2DControl.xaml.(cs)")]
    public partial class NestedDonut2DControl : ChartsDemoModule {
        public NestedDonut2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartControlBoundDataChanged(object sender, RoutedEventArgs e) {
            var seriesCollection = chart.Diagram.Series;
            if(seriesCollection.Count > 0) {
                foreach(NestedDonutSeries2D series in seriesCollection) {
                    series.ShowInLegend = false;
                    AgePopulation population = series.Points[0].Tag as AgePopulation;
                    if(population != null) series.Group = population.Name;
                }
                seriesCollection[0].ShowInLegend = true;
            }
        }
    }
}
