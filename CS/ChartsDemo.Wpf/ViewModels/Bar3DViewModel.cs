using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;

namespace ChartsDemo {
    public class Bar3DViewModel {
        const int pointsCount = 300;
        const int maxValue = 200;
        public virtual List<Point3D> Source { get; set; }

        public Bar3DViewModel() {
            GenerateData();
        }

        public void GenerateData() {
            Source = GenerateRandomData();
        }

        static List<Point3D> GenerateRandomData() {
            var rand = new Random();
            var data = new List<Point3D>();
            for(int i = 0; i < pointsCount; i++) {
                Point3D point;
                do {
                    double x = rand.NextDouble() * maxValue * 2;
                    double y = rand.NextDouble() * maxValue * 2;
                    double z = rand.NextDouble() * maxValue;
                    point = new Point3D(x, y, z);
                } while(data.Contains(point, new Point3DComparer(1.0)));
                data.Add(point);
            }
            return data;
        }
    }
}
