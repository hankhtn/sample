using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/SelectionControl.xaml")]
    [CodeFile("Modules/Interactivity/SelectionControl.xaml.(cs)")]
    [CodeFile("ViewModels/DashboardViewModel.(cs)")]
    [CodeFile("Utils/MapControlUtils.(cs)")]
    public partial class SelectionControl : ChartsDemoModule {
        public SelectionControl() {
            InitializeComponent();
            ActualChart = pieChart;
        }
    }
}
