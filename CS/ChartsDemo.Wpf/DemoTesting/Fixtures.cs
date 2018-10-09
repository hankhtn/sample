using System;
using System.Linq;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace ChartsDemo.Tests {
    public class ChartsCheckAllDemosFixture : CheckAllDemosFixture {
        Type[] skipMemoryLeaksCheckModules = new Type[] { };

        protected override bool CheckMemoryLeaks(Type moduleType) {
            return !skipMemoryLeaksCheckModules.Contains(moduleType);
        }
    }
}
