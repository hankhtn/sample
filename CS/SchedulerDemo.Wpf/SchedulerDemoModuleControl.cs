using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;

namespace SchedulerDemo {
    public class SchedulerDemoModule : DemoModule {
        static readonly DependencyPropertyKey SchedulerControlPropertyKey;
        public static readonly DependencyProperty SchedulerControlProperty;

        static SchedulerDemoModule() {
            SchedulerControlPropertyKey = DependencyProperty.RegisterReadOnly("SchedulerControl", typeof(SchedulerControl), typeof(SchedulerDemoModule), new PropertyMetadata(null));
            SchedulerControlProperty = SchedulerControlPropertyKey.DependencyProperty;
        }
        
        readonly List<SchedulerViewBase> views = new List<SchedulerViewBase>();

        public SchedulerControl SchedulerControl {
            get { return (SchedulerControl)GetValue(SchedulerControlProperty); }
            private set { SetValue(SchedulerControlPropertyKey, value); }
        }
        
        protected List<SchedulerViewBase> Views { get { return views; } }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            if(SchedulerControl == null)
                SchedulerControl = FindScheduler();
            if(OptionsDataContext == null)
                OptionsDataContext = SchedulerControl;
        }
        
        private void PopulateSchedulerViewList(SchedulerControl scheduler) {
            views.Add(scheduler.DayView);
            views.Add(scheduler.WorkWeekView);
            views.Add(scheduler.WeekView);
            views.Add(scheduler.MonthView);
            views.Add(scheduler.TimelineView);
        }

        SchedulerControl FindScheduler() {
            return Content as SchedulerControl ?? LayoutTreeHelper.GetVisualChildren((DependencyObject)Content).OfType<SchedulerControl>().FirstOrDefault();
        }

        protected void InitializeScheduler() {
            SchedulerControl scheduler = FindScheduler();
            if(scheduler == null)
                return;
            SchedulerDataHelper.DataBind(scheduler);
            InitializeSchedulerProperties(scheduler);
        }
        protected void LoadDefaultAppoinmentStatuses(SchedulerControl scheduler) {
            scheduler.Storage.InnerStorage.Appointments.Statuses.LoadDefaults();
        }
        protected void InitializeSchedulerProperties(SchedulerControl scheduler) {
            PopulateSchedulerViewList(scheduler);
            scheduler.ShowBorder = false;
            scheduler.Start = new DateTime(2016, 7, 13);
            scheduler.CustomizeMessageBoxCaption += Scheduler_CustomizeMessageBoxCaption;
        }

        protected override void Hide() {
            SchedulerControl scheduler = FindScheduler();
            if(scheduler != null) {
                SchedulerDataHelper.DataUnbind(scheduler);
                scheduler.CustomizeMessageBoxCaption -= Scheduler_CustomizeMessageBoxCaption;
            }

            base.Hide();
        }

        void Scheduler_CustomizeMessageBoxCaption(object sender, CustomizeMessageBoxCaptionEventArgs e) {
            e.Caption = "DX Scheduler for WPF";
        }

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}
