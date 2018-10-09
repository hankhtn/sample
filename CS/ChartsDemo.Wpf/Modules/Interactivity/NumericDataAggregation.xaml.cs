using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/NumericDataAggregation.xaml")]
    [CodeFile("Modules/Interactivity/NumericDataAggregation.xaml.(cs)")]
    [CodeFile("ViewModels/NumericPointData.(cs)")]
    public partial class NumericDataAggregation : ChartsDemoModule {
        public NumericDataAggregation() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
