using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/CustomAxisLabelsControl.xaml"),
     CodeFile("Modules/ChartElements/CustomAxisLabelsControl.xaml.(cs)")]
    public partial class CustomAxisLabelsControl : ChartsDemoModule {
        public static readonly List<TimeSpan> CustomLabels = Enumerable.Range(0, 9).Select(x => TimeSpan.FromHours(x * 2)).Reverse().ToList();

        public CustomAxisLabelsControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void chartToolTipControler_ToolTipOpening(object sender, ChartToolTipEventArgs e) {
            e.Hint = TimeSpan.FromMinutes(e.SeriesPoint.Value).ToString("hh\\:mm");
        }
    }
}
