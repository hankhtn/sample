using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media.Media3D;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/XyzChart/DataTriangulation.xaml"),
    CodeFile("Modules/XyzChart/DataTriangulation.xaml.(cs)")]
    public partial class DataTriangulation : ChartsDemoModule {
        public DataTriangulation() {
            InitializeComponent();
            ActualChart = chart;
        }
    }

    public static class DataTriangulationModel {
        static List<Point3D> _points;

        public static List<Point3D> Points {
            get { return _points ?? (_points = CreatePoints()); }
        }

        static List<Point3D> CreatePoints() {
            List<Point3D> points = new List<Point3D>();
            for (double j = -25.0; j < 25.0; j += 0.75) {
                for (double i = -15.0; i < 15.0; i += 0.75) {
                    double x = 2 + i + Math.Sin(j / 4.0) * 2;
                    double y = 1 + j + Math.Cos(i / 4.0);
                    double z = 5.5 * Math.Sin(i / 3.0) * Math.Sin(j / 3.0);
                    points.Add(new Point3D(x, y, z));
                }
            }
            return points;
        }
    }
}
