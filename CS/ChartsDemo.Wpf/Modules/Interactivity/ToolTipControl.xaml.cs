using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System;
using System.Linq;
using System.Windows.Media;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/ToolTipControl.xaml")]
    [CodeFile("Modules/Interactivity/ToolTipControl.xaml.(cs)")]
    [CodeFile("ViewModels/G7Data.(cs)")]
    public partial class ToolTipControl : ChartsDemoModule {
        public ToolTipControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void ToolTipController_ToolTipOpening(object sender, ChartToolTipEventArgs e) {
            var colorIndex = e.Series.Points.IndexOf(e.SeriesPoint);
            var memberColor = e.ChartControl.Palette[colorIndex];
            e.Hint = new G7MemberTooltipData((G7Member)e.SeriesPoint.Tag, new SolidColorBrush(memberColor));
        }
    }
    public class G7MemberTooltipData {
        public G7Member Member { get; private set; }
        public SolidColorBrush MemberBrush { get; private set; }

        public G7MemberTooltipData(G7Member member, SolidColorBrush brush) {
            this.Member = member;
            this.MemberBrush = brush;
        }
    }
}
