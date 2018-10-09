using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public class RealTimeSurfaceViewModel {
        const double ValueFactor = 0.5;
        const int Interval = 40;

        readonly DataGenerator dataGenerator;
        readonly DispatcherTimer timer;
        bool inProcess = true;

        public virtual IEnumerable<double> Values { get; set; }
        public virtual FillStyleBase FillStyle { get; set; }
        public virtual int Size { get; set; }
        public virtual bool IsTimerEnabled { get; set; }
        public virtual IEnumerable<object> ArgumentsX { get; protected set; }
        public virtual IEnumerable<object> ArgumentsY { get; protected set; }
        public virtual double MinValue { get; protected set; }
        public virtual double MaxValue { get; protected set; }
        
        public RealTimeSurfaceViewModel() {
            this.dataGenerator = new DataGenerator();
            this.timer = CreateTimer();
            IsTimerEnabled = true;
            Size = 50;
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
        protected void OnSizeChanged() {
            var oldIsEnabled = timer.IsEnabled;
            timer.IsEnabled = false;

            dataGenerator.Size = Size + 1;
            MinValue = -dataGenerator.Size / 3 - 1.5;
            MaxValue = dataGenerator.Size / 3 + 1.5;
            UpdateDataSource();
            UpdateValues();

            timer.IsEnabled = oldIsEnabled;
        }

        DispatcherTimer CreateTimer() {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(Interval);
            timer.Tick += (_d, _e) => UpdateValues();
            return timer;
        }
        FillStyleBase CreateGradientFillStyle() {
            GradientFillStyle style = new GradientFillStyle();
            style.ColorStops.Add(new ColorStop() { Offset = new Unit(MaxValue) });
            style.ColorStops.Add(new ColorStop() { Offset = new Unit(0) });
            style.ColorStops.Add(new ColorStop() { Offset = new Unit(MinValue) });
            return style;
        }
        void UpdateDataSource() {
            IEnumerable<object> arguments = dataGenerator.GenerateArguments();
            ArgumentsX = arguments;
            ArgumentsY = arguments;
            dataGenerator.RecreateElevations();
            FillStyle = CreateGradientFillStyle();
        }
        void UpdateValues() {
            Values = dataGenerator.GenerateValues();
        }
    }
}
