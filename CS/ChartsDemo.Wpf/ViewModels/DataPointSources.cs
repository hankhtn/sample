using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChartsDemo {
    public static class DataPointSources {
        #region Fields

        static List<List<DataPoint>> dataSource;
        static List<List<DataPoint>> dataSource2;
        static List<List<DataPoint>> pieDataSource;
        static List<List<DataPoint>> nestedDonutDataSource;
        static List<List<DataPoint>> barDataSource;
        static List<List<DataPoint>> barDataSource2;
        static List<List<DataPoint>> scatterDataSource;
        static List<List<DataPoint>> funnelDataSource;
        static List<List<PolarDataPoint>> polarDataSource;
        static List<List<PolarDataPoint>> polarRangeDataSource;
        static List<List<DataPoint>> bubbleDataSource;
        static List<List<DataPoint>> rangeDataSource;
        static List<List<DataPoint>> rangeDataSource2;
        static List<DataTable> financialDataSource;

        #endregion
        #region Properties

        public static List<List<DataPoint>> DataSource { get { return dataSource ?? (dataSource = CreateDataSource()); } }
        public static List<List<DataPoint>> DataSource2 { get { return dataSource2 ?? (dataSource2 = DataPointSources.DataSource.Take(1).ToList()); } }
        public static List<List<DataPoint>> PieDataSource { get { return pieDataSource ?? (pieDataSource = CreatePieDataSource().ToList()); } }
        public static List<List<DataPoint>> NestedDonutDataSource { get { return nestedDonutDataSource ?? (nestedDonutDataSource = CreatePieDataSource().Concat(CreatePieDataSource()).ToList()); } }
        public static List<List<DataPoint>> BarDataSource { get { return barDataSource ?? (barDataSource = CreateBarDataSource()); } }
        public static List<List<DataPoint>> BarDataSource2 { get { return barDataSource2 ?? (barDataSource2 = BarDataSource.Take(3).ToList()); } }
        public static List<List<DataPoint>> ScatterDataSource { get { return scatterDataSource ?? (scatterDataSource = CreateScatterDataSource().ToList()); } }
        public static List<List<DataPoint>> FunnelDataSource { get { return funnelDataSource ?? (funnelDataSource = CreateFunnelDataSource().ToList()); } }
        public static List<List<PolarDataPoint>> PolarDataSource { get { return polarDataSource ?? (polarDataSource = CreatePolarDataSource().ToList()); } }
        public static List<List<PolarDataPoint>> PolarRangeDataSource { get { return polarRangeDataSource ?? (polarRangeDataSource = CreatePolarRangeDataSource().ToList()); } }
        public static List<List<DataPoint>> BubbleDataSource { get { return bubbleDataSource ?? (bubbleDataSource = CreateBubbleDataSource().ToList()); } }
        public static List<List<DataPoint>> RangeDataSource { get { return rangeDataSource ?? (rangeDataSource = CreateRangeDataSource().ToList()); } }
        public static List<List<DataPoint>> RangeDataSource2 { get { return rangeDataSource2 ?? (rangeDataSource2 = RangeDataSource.Take(1).ToList()); } }
        public static List<DataTable> FinancialDataSource { get { return financialDataSource ?? (financialDataSource = CreateFinancialDataSource().ToList()); } }

        #endregion
        #region Methods

        static List<List<DataPoint>> CreateGroups(string[] arguments, List<double[]> groups) {
            return groups
                .Select(x => x.Zip(arguments, (value, argument) => new DataPoint(argument: argument, value: value)).ToList())
                .ToList();
        }
        static List<List<DataPoint>> CreateDataSource() {
            var args = new[] { "A", "B", "C", "D", "E", "F" };
            var group0 = new double[] { 15, 13, 7, 5, 23, 21 };
            var group1 = new double[] { 8, 12, 4, 9, 15, 19 };
            var group2 = new double[] { 3, 10, 6, 6, 8, 10 };
            return CreateGroups(args, new List<double[]> { group0, group1, group2 });
        }
        static IEnumerable<List<DataPoint>> CreatePieDataSource() {
            var dataSource = new List<DataPoint>();
            var random = new Random(0);
            for(int i = 0; i < 16; i++)
                dataSource.Add(new DataPoint("1", random.NextDouble() * 3 + 1));
            yield return dataSource;
        }
        static List<List<DataPoint>> CreateBarDataSource() {
            var dataSource = new List<DataPoint>();
            var args = new[] { "A", "B", "C", "D", "E", "F" };
            var group0 = new double[] { 1, 2, 5, -2, -2.1, -2.4 };
            var group1 = new double[] { 3, 10, 6, -3, -3.2, -3.8 };
            var group2 = new double[] { 8, 12, 7, -1.5, -1, -0.7 };
            var group3 = new double[] { 15, 13, 4, -1.3, -0.6, -4 };
            return CreateGroups(args, new List<double[]> { group0, group1, group2, group3 });
        }
        static IEnumerable<List<DataPoint>> CreateScatterDataSource() {
            yield return new List<DataPoint> {
                new DataPoint("A", 15),
                new DataPoint("B", 11),
                new DataPoint("C", 7),
                new DataPoint("D", 9),
                new DataPoint("C", 23),
                new DataPoint("B", 21)
            };
        }
        static IEnumerable<List<DataPoint>> CreateFunnelDataSource() {
            yield return new List<DataPoint> {
                new DataPoint("Visited a Website", 9152),
                new DataPoint("Downloaded a Trial", 6870),
                new DataPoint("Contacted to Support", 5121),
                new DataPoint("Subscribed", 2224),
                new DataPoint("Renewed", 1670)
            };
        }
        static IEnumerable<List<PolarDataPoint>> CreatePolarDataSource() {
            var random = new Random();
            yield return Enumerable.Range(0, 7)
                .Select(x => new PolarDataPoint(Math.Floor(random.NextDouble() * 360), Math.Floor(random.NextDouble() * 25)))
                .ToList();
        }
        static IEnumerable<List<PolarDataPoint>> CreatePolarRangeDataSource() {
            var random = new Random();
            var pointsCount = 6;
            yield return Enumerable.Range(0, 7)
                .Select(x => new PolarDataPoint(
                    Math.Floor(x / (double)pointsCount * 360), 
                    Math.Floor(random.NextDouble() * 10 + 30), 
                    Math.Floor(random.NextDouble() * 10 + 10)))
                .ToList();
        }
        static IEnumerable<List<DataPoint>> CreateBubbleDataSource() {
            yield return new List<DataPoint> {
                new DataPoint("A", value: 10, weight: 5.9),
                new DataPoint("B", value: 5, weight: 4.9),
                new DataPoint("C", value: 2, weight: 4.6),
                new DataPoint("D", value: 5, weight: 3),
                new DataPoint("E", value: 2, weight: 2.9),
                new DataPoint("F", value: 4, weight: 2.8),
                new DataPoint("G", value: 2, weight: 2.6),
                new DataPoint("H", value: 3, weight: 2.5),
                new DataPoint("I", value: 4, weight: 2.4),
            };
        }
        static IEnumerable<List<DataPoint>> CreateRangeDataSource() {
            yield return new List<DataPoint> {
                new DataPoint("A", 3, 13),
                new DataPoint("B", 5, 8),
                new DataPoint("C", 2, 9),
                new DataPoint("D", -2, -8),
                new DataPoint("E", -1, -6),
                new DataPoint("F", -3, -7),
            };
            yield return new List<DataPoint> {
                new DataPoint("A", 5, 15),
                new DataPoint("B", 3, 11),
                new DataPoint("C", 6, 11),
                new DataPoint("D", -1, -9),
                new DataPoint("E", -3, -9),
                new DataPoint("F", -2, -6),
            };
        }
        static IEnumerable<DataTable> CreateFinancialDataSource() {
            yield return new GoogleStockData().ShortData;
        }

        #endregion
    }
    public class DataPoint {
        public string Argument { get; set; }
        public double Value { get; set; }
        public double Value2 { get; set; }
        public double Weight { get; set; }

        public DataPoint(string argument, double value, double value2 = 0, double weight = 0) {
            this.Argument = argument;
            this.Value = value;
            this.Value2 = value2;
            this.Weight = weight;
        }
    }
    public class PolarDataPoint {
        public double Argument { get; set; }
        public double Value { get; set; }
        public double Value2 { get; set; }

        public PolarDataPoint(double argument, double value, double value2 = 0) {
            this.Argument = argument;
            this.Value = value;
            this.Value2 = value2;
        }
    }

}
