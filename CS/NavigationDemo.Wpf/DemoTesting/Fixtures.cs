using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Editors;

namespace NavigationDemo.Tests {
    public class AccordionCheckAllDemosFixture : CheckAllDemosFixture {
        Type[] skipMemoryLeaksCheckModules = new Type[] { typeof(FileSearchDemoModule) };
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return !skipMemoryLeaksCheckModules.Contains(moduleType);
        }
        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            return theme != Theme.HybridApp;
        }
    }
    public class AccordionDemoModulesAccessor : DemoModulesAccessor<AccordionDemoModule> {
        public AccordionDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseAccordionDemoTestingFixture : BaseDemoTestingFixture {
        protected AccordionDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseAccordionDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract AccordionDemoModulesAccessor GetModulesAccessor();
    }
}
