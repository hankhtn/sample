using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Charts.RangeControlClient;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/ChartClientForRangeControlModule.xaml")]
    [CodeFile("Modules/Interactivity/ChartClientForRangeControlModule.xaml.(cs)")]
    [CodeFile("ViewModels/ChartClientModel.(cs)")]
    public partial class ChartClientForRangeControlModule {
        public ChartClientForRangeControlModule() {
            InitializeComponent();
        }
    }

    public enum ChartViewType {
        Area,
        Bar,
        Line
    }

    public class ChartViewTypeConverter : IValueConverter {
        RangeControlClientView Parse(ChartViewType type) {
            if(type == ChartViewType.Area)
                return new RangeControlClientAreaView();
            if(type == ChartViewType.Bar)
                return new RangeControlClientBarView();
            if(type == ChartViewType.Line)
                return new RangeControlClientLineView();
            return null;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is ChartViewType)
                return Parse((ChartViewType)value);
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
