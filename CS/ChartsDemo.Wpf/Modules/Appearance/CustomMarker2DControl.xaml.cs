using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Collections.Generic;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomMarker2DControl.xaml"),
     CodeFile("Modules/Appearance/CustomMarker2DControl.xaml.(cs)")]
    public partial class CustomMarker2DControl : ChartsDemoModule {
        public static List<NumericPoint> CreateData() {
            return new List<NumericPoint> {
                new NumericPoint(1.0, 2.1, 1.0),
                new NumericPoint(2.0, 1.4, 2.0),
                new NumericPoint(3.0, 1.1, 4.0),
                new NumericPoint(4.0, 1.9, 3.0),
                new NumericPoint(5.0, 3.1, 2.5),
                new NumericPoint(6.0, 2.4, 1.7),
                new NumericPoint(7.0, 2.6, 3.9),
                new NumericPoint(8.0, 1.9, 2.8),
                new NumericPoint(9.0, 3.2, 2.1),
                new NumericPoint(10.0, 3.5, 1.3),
            };
        }

        public CustomMarker2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
