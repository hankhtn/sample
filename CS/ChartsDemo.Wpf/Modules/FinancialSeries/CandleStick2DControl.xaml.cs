using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/FinancialSeries/CandleStick2DControl.xaml"),
     CodeFile("Modules/FinancialSeries/CandleStick2DControl.xaml.(cs)")]
    public partial class CandleStick2DControl : ChartsDemoModule {
        public CandleStick2DControl() {
            InitializeComponent();
            ActualChart = chart;
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
