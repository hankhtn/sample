using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace ChartsDemo {
    public class RealtimeViewModel {
        const int UpdateInterval = 40;

        readonly DataCollection dataSource = new DataCollection();
        readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Normal);
        readonly Random random = new Random();
        double value1 = 10.0;
        double value2 = -10.0;
        bool inProcess = true;

        public int TimeInterval { get; set; }
        public virtual bool IsTimerEnabled { get; set; }
        public DataCollection DataSource { get { return dataSource; } }
        public virtual DateTime MinTime { get; protected set; }
        public virtual DateTime MaxTime { get; protected set; }

        public RealtimeViewModel() {
            IsTimerEnabled = true;
            TimeInterval = 10;
            timer.Interval = TimeSpan.FromMilliseconds(UpdateInterval);
            timer.Tick += (_d, _e) => OnTimerTick();
        }

        public void DisableProcess() {
            inProcess = timer.IsEnabled;
            IsTimerEnabled = false;
        }
        public void RestoreProcess() {
            IsTimerEnabled = inProcess;
        }
        public void ToggleIsTimerEnabled() {
            IsTimerEnabled = !IsTimerEnabled;
        }

        protected void OnIsTimerEnabledChanged() {
            timer.IsEnabled = IsTimerEnabled;
        }
        void OnTimerTick() {
            DateTime argument = DateTime.Now;
            DateTime minDate = argument.AddSeconds(-TimeInterval);
            IList<ProcessItem> itemsToInsert = new List<ProcessItem>();
            for(int i = 0; i < UpdateInterval; i++) {
                itemsToInsert.Add(new ProcessItem() { DateAndTime = argument, Process1 = value1, Process2 = value2 });
                argument = argument.AddMilliseconds(1);
                value1 = CalculateNextValue(value1);
                value2 = CalculateNextValue(value2);
            }
            dataSource.AddRange(itemsToInsert);
            dataSource.RemoveRangeAt(0, dataSource.TakeWhile(item => item.DateAndTime < minDate).Count());

            MinTime = minDate;
            MaxTime = argument;
        }
        double CalculateNextValue(double value) {
            return value + (random.NextDouble() * 10.0 - 5.0);
        }
    }

    public struct ProcessItem {
        public DateTime DateAndTime { get; set; }
        public double Process1 { get; set; }
        public double Process2 { get; set; }
    }

    public class DataCollection : ObservableCollection<ProcessItem> {
        public void AddRange(IList<ProcessItem> items) {
            foreach(ProcessItem item in items)
                Items.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)items, Items.Count - items.Count));
        }
        public void RemoveRangeAt(int startingIndex, int count) {
            var removedItems = new List<ProcessItem>(count);
            for(int i = 0; i < count; i++) {
                removedItems.Add(Items[startingIndex]);
                Items.RemoveAt(startingIndex);
            }
            if(count > 0)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (IList)removedItems, startingIndex));
        }
    }
}
