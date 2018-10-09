using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    public class ChartsDemoModule : DemoModule {
        static readonly DependencyPropertyKey ActualChartPropertyKey =
            DependencyProperty.RegisterReadOnly("ActualChart", typeof(ChartControlBase), typeof(ChartsDemoModule), new PropertyMetadata(null));
        public static readonly DependencyProperty ActualChartProperty = ActualChartPropertyKey.DependencyProperty;
        public ChartControlBase ActualChart {
            get { return (ChartControlBase)GetValue(ActualChartProperty); }
            protected set { SetValue(ActualChartPropertyKey, value); }
        }

        public ChartsDemoModule() {

        }

        protected override void Show() {
            base.Show();
            if (ActualChart != null && ActualChart.Palette != null)
                ActualChart.Palette.ColorCycleLength = 10;
        }

    }
}

namespace CommonChartsDemo{
    public class CommonChartsDemoModule : ChartsDemo.ChartsDemoModule{
    }
}
