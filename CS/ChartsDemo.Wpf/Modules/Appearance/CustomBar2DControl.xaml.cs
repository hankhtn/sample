using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomBar2DControl.xaml"),
     CodeFile("Modules/Appearance/CustomBar2DControl.xaml.(cs)")]
    public partial class CustomBar2DControl : ChartsDemoModule {
        public CustomBar2DControl() {
            InitializeComponent();
            ActualChart = chart;
            series.ToolTipPointPattern = "Argument: {A}\nValue: {V:0.0}";
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }

    public class CustomBar2DAnimation : Bar2DDropInAnimation {
        public override Rect CreateAnimatedBarBounds(Rect barBounds, Rect viewport, bool isNegativeBar, bool axisXReverse, bool axisYReverse, bool diagramRotated, double progress) {
            Rect bounds = base.CreateAnimatedBarBounds(barBounds, viewport, isNegativeBar, axisXReverse, axisYReverse, diagramRotated, progress);
            bounds.X += Math.Sin(progress * Math.PI * 4) * viewport.Width / 12;
            return bounds;
        }
    }
}
