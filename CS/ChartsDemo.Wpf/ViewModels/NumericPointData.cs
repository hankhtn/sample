using System;
using System.Collections.Generic;

namespace ChartsDemo {
    public static class NumericPointData {
        const int pointCount = 100000;
        static List<NumericPoint> _data;
        public static List<NumericPoint> Data { get { return _data ?? (_data = GetData()); } }

        static List<NumericPoint> GetData() {
            double value = 0;
            double argument = 1;
            Random random = new Random();
            var points = new List<NumericPoint>();
            for(int i = 0; i < pointCount; i++) {
                points.Add(new NumericPoint(argument, value));
                value += (random.NextDouble() * 10.0 - 5.0);
                argument ++;
            }
            return points;
        }
    }
    public class NumericPoint {
        public double Argument { get; private set; }
        public double Value { get; private set; }
        public double Weight { get; private set; }

        public NumericPoint(double argument, double value, double weight = 0) {
            this.Argument = argument;
            this.Value = value;
        }
    }
}
