using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Charts;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;

namespace ChartsDemo {
    public class ChartAnimationService : ServiceBase, IChartAnimationService {
        ChartControl Chart { get { return (ChartControl)AssociatedObject; } }
        public void Animate() {
            Chart.Dispatcher.BeginInvoke(new Action(Chart.Animate));
        }
    }
}
