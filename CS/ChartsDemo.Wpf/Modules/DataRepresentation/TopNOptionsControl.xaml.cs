using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataRepresentation/TopNOptionsControl.xaml")]
    [CodeFile("Modules/DataRepresentation/TopNOptionsControl.xaml.(cs)")]
    [CodeFile("ViewModels/CountryData.(cs)")]
    public partial class TopNOptionsControl: ChartsDemoModule {
        public TopNOptionsControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void AnimateChart(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }

    public class SelectedIndexToVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                int index = Convert.ToInt32(value);
                int expectedIndex = Convert.ToInt32(parameter);
                if (index == expectedIndex)
                    return Visibility.Visible;
            }
            catch { }
            return Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
