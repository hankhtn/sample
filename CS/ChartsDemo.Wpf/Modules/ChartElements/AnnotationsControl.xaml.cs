using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Globalization;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/AnnotationsControl.xaml")]
    [CodeFile("Modules/ChartElements/AnnotationsControl.xaml.(cs)")]
    [CodeFile("ViewModels/MarsTemperatureData.(cs)")]
    public partial class AnnotationsControl : ChartsDemoModule {
        public AnnotationsControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void BoundDataChanged(object sender, RoutedEventArgs e) {
            var min = splineSeries.Points.Aggregate((seed, current) => seed.Value < current.Value ? seed : current);
            minimumPoint.SeriesPoint = min;
            minimumAnnotation.Content = "Minimum: " + min.Value;

            var max = splineSeries.Points.Aggregate((seed, current) => seed.Value > current.Value ? seed : current);
            maximumPoint.SeriesPoint = max;
            maximumAnnotation.Content = "Maximum: " + max.Value;
        }
    }
}
