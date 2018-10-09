using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace ChartsDemo {
    public class FinancialDataViewModel {
        const int InitialUpdatesCount = 2500;
        const int MaxPointsCount = 1000;
        const double Interval = 150;
        readonly DispatcherTimer timer = new DispatcherTimer();
        DateTime currentDate;

        public ObservableCollection<TraderPoint> DataSource { get; private set; }
        public ObservableCollection<DateTime> Workdays { get; private set; }
        public virtual TraderPoint CurrentPoint { get; protected set; }
        public virtual bool IsTimerEnabled { get; set; }

        public FinancialDataViewModel() {
            Workdays = new ObservableCollection<DateTime>();
            SetCurrentDate(FinancialDataGenerator.GetStartDate());
            DataSource = new ObservableCollection<TraderPoint> { FinancialDataGenerator.CreatePoint(currentDate, FinancialDataGenerator.StartPrice) };
            for(int i = 0; i < InitialUpdatesCount; i++) 
                UpdateData();
            IsTimerEnabled = true;
            timer.Interval = TimeSpan.FromMilliseconds(Interval);
            timer.Tick += (d, e) => UpdateData();
        }

        void UpdateData() {
            SetCurrentDate(FinancialDataGenerator.GetNextDate(currentDate));

            var prevPoint = DataSource.Last();
            if(currentDate - prevPoint.Date >= FinancialDataGenerator.GeneratePointPeriod) {
                DataSource.Add(FinancialDataGenerator.CreatePoint(currentDate, prevPoint.PriceClose));
                if(DataSource.Count > MaxPointsCount) DataSource.RemoveAt(0);
            }
            else {
                DataSource[DataSource.Count - 1] = FinancialDataGenerator.UpdatePoint(prevPoint);
            }

            CurrentPoint = DataSource.Last();
        }
        void SetCurrentDate(DateTime newDate) {
            if(currentDate.Date != newDate.Date)
                Workdays.Add(newDate);
            currentDate = newDate;
        }

        #region POCO
        public void ToggleIsTimerEnabled() {
            IsTimerEnabled = !IsTimerEnabled;
        }
        protected void OnIsTimerEnabledChanged() {
            timer.IsEnabled = IsTimerEnabled;
        }
        #endregion
    }
    public static class FinancialDataGenerator {
        #region Fields

        public const double StartPrice = 500.0;
        const double MinPrice = 50.0;
        const double WorkStart = 9.0;
        const double WorkPeriod = 9.0;

        public static readonly TimeSpan GeneratePointPeriod = TimeSpan.FromMinutes(5);
        static readonly Random random = new Random(1);
        static readonly TimeSpan UpdatePointPeriod = TimeSpan.FromSeconds(30);

        #endregion
        #region Methods

        public static DateTime GetStartDate() {
            return GetNextWorkday(DateTime.Today);
        }
        public static DateTime GetNextDate(DateTime date) {
            if(date.Add(UpdatePointPeriod) > date.Date.AddHours(WorkStart + WorkPeriod))
                return GetNextWorkday(date);
            return date.Add(UpdatePointPeriod);
        }
        static DateTime GetNextWorkday(DateTime date) {
            var addDays = date.DayOfWeek == DayOfWeek.Friday ? 3
                    : date.DayOfWeek == DayOfWeek.Saturday ? 2 : 1;
            return date.Date.AddDays(addDays).AddHours(WorkStart);
        }

        public static TraderPoint CreatePoint(DateTime argument, double prevClose) {
            double open = prevClose;
            double close = CorrectPrice(open + GetPriceDelta());
            double low = Math.Min(open, close);
            double high = Math.Max(open, close);
            return new TraderPoint(argument, low, high, open, close, random.NextDouble() * 100);
        }
        public static TraderPoint UpdatePoint(TraderPoint point) {
            double open = point.PriceOpen;
            double close = CorrectPrice(point.PriceClose + GetPriceDelta());
            double low = Math.Min(point.PriceLow, close);
            double high = Math.Max(point.PriceHigh, close);
            return new TraderPoint(point.Date, low, high, open, close, point.Value + random.NextDouble() * 100);
        }
        static double CorrectPrice(double price) {
            if(price <= MinPrice)
                return 2 * MinPrice - price;
            return price;
        }
        static double GetPriceDelta() {
            return (random.NextDouble() - 0.5) * 40;
        }

        #endregion
    }
    public struct TraderPoint {
        readonly DateTime date;
        readonly double priceLow;
        readonly double priceHigh;
        readonly double priceOpen;
        readonly double priceClose;
        readonly double value;

        public DateTime Date { get { return date; } }
        public double PriceLow { get { return priceLow; } }
        public double PriceHigh { get { return priceHigh; } }
        public double PriceOpen { get { return priceOpen; } }
        public double PriceClose { get { return priceClose; } }
        public double Value { get { return value; } }

        public TraderPoint(DateTime date, double priceLow, double priceHigh, double priceOpen, double priceClose, double value) {
            this.date = date;
            this.priceLow = priceLow;
            this.priceHigh = priceHigh;
            this.priceOpen = priceOpen;
            this.priceClose = priceClose;
            this.value = value;
        }
    }
}
