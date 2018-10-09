using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase;

namespace SchedulingDemo {
    [NoAutogeneratedCodeFiles, CodeFiles(
        "Modules/CellCustomizationDemoModule.xaml",
        "ViewModels/CellCustomizationDemoViewModel.(cs)")]
    public partial class CellCustomizationDemoModule : SchedulingDemoModule {
        public CellCustomizationDemoModule() {
            InitializeComponent();
        }
    }
    public class CellBackgroundConverter : IValueConverter {
        public Brush LunchBackground { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            DateTimeRange range = (DateTimeRange)value;
            if (range.Start.TimeOfDay >= TimeSpan.FromHours(13) && range.End.TimeOfDay <= TimeSpan.FromHours(14))
                return LunchBackground;
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
