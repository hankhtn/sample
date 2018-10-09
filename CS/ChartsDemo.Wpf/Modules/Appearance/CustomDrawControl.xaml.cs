using System;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Collections.Generic;
using System.Linq;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomDrawControl.xaml"),
     CodeFile("Modules/Appearance/CustomDrawControl.xaml.(cs)")]
    public partial class CustomDrawControl : ChartsDemoModule {
        public CustomDrawControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            if((bool)chbCustomDraw.IsChecked && e.DrawOptions != null) {
                var drawOptions = GetDrawOptions(e.SeriesPoint.Value);
                if(!String.IsNullOrEmpty(drawOptions.Item1))
                    e.LabelText = drawOptions.Item1 + ": " + e.LabelText;
                e.DrawOptions.Color = drawOptions.Item2;
            }
        }
        static Tuple<string, Color> GetDrawOptions(double val) {
            if(val < 1)
                return new Tuple<string, Color>("Green", Color.FromArgb(0xFF, 0x51, 0x89, 0x03));
            if(val < 2)
                return new Tuple<string, Color>("Yellow", Color.FromArgb(0xFF, 0xF9, 0xAA, 0x0F));
            return new Tuple<string, Color>("Red", Color.FromArgb(0xFF, 0xC7, 0x39, 0x0C));
        }
    }

    public class CustomDrawViewModel {
        public virtual List<NumericPoint> Data { get; protected set; }
        public virtual IChartAnimationService ChartAnimationService { get { return null; } }

        public CustomDrawViewModel() {
            UpdateData();
        }

        public void UpdateData() {
            Data = CreateData();
            if(ChartAnimationService != null)
                ChartAnimationService.Animate();
        }
        public void OnModuleLoaded() {
            if(ChartAnimationService != null)
                ChartAnimationService.Animate();
        }

        static List<NumericPoint> CreateData() {
            var random = new Random();
            return Enumerable.Range(0, 20).Select(x => {
                var value = Math.Max(Math.Round(random.NextDouble() * 3, 1), 0.1);
                return new NumericPoint(x, value);
            }).ToList();
        }
    }
}
