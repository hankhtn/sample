using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataBinding/BindingUsingSeriesTemplate.xaml"),
     CodeFile("Modules/DataBinding/BindingUsingSeriesTemplate.xaml.(cs)"),
     CodeFile("ViewModels/BindingUsingSeriesTemplateViewModel.(cs)"),
     CodeFile("ViewModels/G7Data.(cs)"),
     CodeFile("ViewModels/IChartAnimationService.(cs)"),
     CodeFile("Utils/ChartAnimationService.(cs)")]
    public partial class BindingUsingSeriesTemplate : ChartsDemoModule {
        public BindingUsingSeriesTemplate() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
