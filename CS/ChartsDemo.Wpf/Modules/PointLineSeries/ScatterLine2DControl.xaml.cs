using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Collections.Generic;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/ScatterLine2DControl.xaml"),
     CodeFile("Modules/PointLineSeries/ScatterLine2DControl.xaml.(cs)")]
    [CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")]
    public partial class ScatterLine2DControl : ChartsDemoModule {
        public ScatterLine2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
