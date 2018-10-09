using System;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace CommonChartsDemo{
    [CodeFile("Modules/DataBinding/DataGridCharting.xaml"),
     CodeFile("Modules/DataBinding/DataGridCharting.xaml.(cs)"),
     CodeFile("ViewModels/SalesProductData.(cs)")]
    public partial class DataGridCharting : CommonChartsDemoModule {
        public DataGridCharting() {
            InitializeComponent();
            ActualChart = unitsChart;
        }
    }

    public enum AggregateType {
        Average = 1,
        Min = 2,
        Max = 3,
        Sum = 4
    }
}
