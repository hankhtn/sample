using System.Windows.Media.Media3D;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingFor3DPieSeries.xaml"),
     CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingFor3DPieSeries.xaml.(cs)")]
    public partial class ResolveLabelsOverlappingFor3DPieSeries : ChartsDemoModule {
        public ResolveLabelsOverlappingFor3DPieSeries() {
            InitializeComponent();
            ActualChart = chart;
            simpleDiagram3D.ContentTransform = new MatrixTransform3D(new Matrix3D(-0.719, -0.414, 0.558, 0, 
                                                                                   0.693, -0.389, 0.605, 0,
                                                                                  -0.032,  0.822, 0.567, 0,
                                                                                   0.000,  0.000, 0.000, 1));
        }
    }
}
