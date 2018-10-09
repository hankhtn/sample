using System;
using System.Windows;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Scheduling;

namespace SchedulingDemo {
    public class SchedulingDemoModule : DemoModule {
        static SchedulingDemoModule() {
            NWindContext.Preload();
        }

        static ResourceDictionary commonResources;
        static ResourceDictionary CommonResources  { get { return commonResources ?? (commonResources = new ResourceDictionary() { Source = new Uri("/SchedulingDemo;component/Themes/Common.xaml", UriKind.RelativeOrAbsolute) }); } }
        static ResourceDictionary outlookInspiredDemoResources;
        static ResourceDictionary OutlookInspiredDemoResources { get { return outlookInspiredDemoResources ?? (outlookInspiredDemoResources = new ResourceDictionary() { Source = new Uri("/SchedulingDemo;component/Themes/OutlookInspired.xaml", UriKind.RelativeOrAbsolute) }); } }

        bool addCommonResources;
        bool addOutlookInspiredDemoResources;
        public SchedulingDemoModule(bool addCommonResources = true, bool addOutlookInspiredDemoResources =false) {
            this.addCommonResources = addCommonResources;
            this.addOutlookInspiredDemoResources = addOutlookInspiredDemoResources;
            if (addCommonResources)
                Resources.MergedDictionaries.Add(CommonResources);
            if(addOutlookInspiredDemoResources)
                Resources.MergedDictionaries.Add(OutlookInspiredDemoResources);
            Loaded += OnLoaded;
        }

        protected virtual bool NeedChangeEditorsTheme { get { return false; } }
        SchedulerControl Scheduler { get; set; }
        
        protected virtual void LoadDemoData() {
        }

        protected override void Clear() {
            base.Clear();
            Scheduler.DataSource = null;
        }

        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);
            if (this.addCommonResources && !Resources.MergedDictionaries.Contains(CommonResources))
                throw new InvalidOperationException();
            if(this.addOutlookInspiredDemoResources && !Resources.MergedDictionaries.Contains(OutlookInspiredDemoResources))
                throw new InvalidOperationException();
        }

        void OnLoaded(object sender, RoutedEventArgs e) {            
            Scheduler = LayoutHelper.FindElementByType<SchedulerControl>(this);
        }
    }
}
