using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataBinding/MVVMStyleBinding.xaml"),
     CodeFile("Modules/DataBinding/MVVMStyleBinding.xaml.(cs)"),
     CodeFile("ViewModels/DailyWeatherViewModel.(cs)")]
    public partial class MVVMStyleBinding : ChartsDemoModule {
        public MVVMStyleBinding() {
            InitializeComponent();
        }
    }

    public class WeatherTemplateSelector : DataTemplateSelector {
        public DataTemplate AverageTemplate { get; set; }
        public DataTemplate MinMaxTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            WeatherItem weatherItem = item as WeatherItem;
            if (weatherItem != null)
                return weatherItem.IsAverageWeather ? AverageTemplate : MinMaxTemplate;
            return null;
        }
    }
}
