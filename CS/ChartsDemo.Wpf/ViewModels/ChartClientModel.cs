using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Charts.RangeControlClient;

namespace ChartsDemo {
    public class ChartClientModel {
        const int itemCount = 100;
        public object NumericItemsSource { get; private set; }
        public object DateTimeItemsSource { get; private set; }
        public virtual double MinimumGridSpacing { get; protected set; }
        public virtual double MiddleGridSpacing { get; set; }
        public virtual double MaximumGridSpacing { get; protected set; }
        public virtual Visibility GridSpacingVisibility { get; set; }
        public virtual DateTimeGridAlignment GridAlignment { get; set; }

        public ChartClientModel() {
            NumericItemsSource = GenerateNumericData();
            DateTimeItemsSource = GenerateDateTimeData();
            GridAlignment = DateTimeGridAlignment.Auto;
        }

        protected void OnGridAlignmentChanged() {
            GridSpacingVisibility = GridAlignment.Equals(DateTimeGridAlignment.Auto) ? Visibility.Collapsed : Visibility.Visible;
            MinimumGridSpacing = GetMinimumGridSpacing(GridAlignment);
            MaximumGridSpacing = GetMaximumGridSpacing(GridAlignment);
            MiddleGridSpacing = (MinimumGridSpacing + MaximumGridSpacing) / 2;
        }
        #region Static

        static double[] GenerateNumericData() {
            double[] data = new double[itemCount];
            Random random = new Random();
            double value = 0;
            for(int i = 0; i < itemCount; i++) {
                value += (random.NextDouble() - 0.5);
                data[i] = Math.Abs(value);
            }
            return data;
        }
        static List<DateTimeItem> GenerateDateTimeData() {
            List<DateTimeItem> data = new List<DateTimeItem>();
            DateTime now = DateTime.Now.Date;
            Random random = new Random();
            double value = 0;
            for(int i = 0; i < itemCount; i++) {
                now = now.AddDays(1);
                value += (random.NextDouble() - 0.5);
                data.Add(new DateTimeItem() { Argument = now, Value = Math.Abs(value + Math.Sin(i * 0.6)) });
            }
            return data;
        }
        static double GetMaximumGridSpacing(DateTimeGridAlignment gridAlignment) {
            switch(gridAlignment) {
                case DateTimeGridAlignment.Day:
                    return 15;
                case DateTimeGridAlignment.Month:
                    return 3;
                case DateTimeGridAlignment.Week:
                    return 6;
                case DateTimeGridAlignment.Auto:
                default:
                    return 5;
            }
        }
        static double GetMinimumGridSpacing(DateTimeGridAlignment gridAlignment) {
            switch(gridAlignment) {
                case DateTimeGridAlignment.Day:
                    return 5;
                case DateTimeGridAlignment.Month:
                    return 1;
                case DateTimeGridAlignment.Week:
                    return 2;
                case DateTimeGridAlignment.Auto:
                default:
                    return 1;
            }
        }

        #endregion
    }
    public class DateTimeItem {
        public DateTime Argument { get; set; }
        public double Value { get; set; }
    }
}
