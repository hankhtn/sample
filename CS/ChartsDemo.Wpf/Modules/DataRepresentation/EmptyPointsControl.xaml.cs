using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Windows.Markup;
using System.Windows.Threading;
using System.ComponentModel;

namespace ChartsDemo {
    [CodeFile("Modules/DataRepresentation/EmptyPointsControl.xaml"),
     CodeFile("Modules/DataRepresentation/EmptyPointsControl.xaml.(cs)")]
    [CodeFile("ViewModels/WebSiteData.(cs)")]
    [CodeFile("Utils/SeriesInfo.(cs)")]
    public partial class EmptyPointsControl : ChartsDemoModule {
        public EmptyPointsControl() {
            InitializeComponent();
            ActualChart = chart;
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void lbSeriesType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            Dispatcher.BeginInvoke(new Action(chart.Animate), DispatcherPriority.Background);
        }
    }
}
